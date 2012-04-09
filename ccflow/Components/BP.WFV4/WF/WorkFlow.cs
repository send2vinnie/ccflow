using System;
using BP.En;
using BP.Web;
using BP.DA;
using System.Collections;
using System.Data;
using BP.Port;
using BP.Sys;
using BP.Port;

namespace BP.WF
{
    /// <summary>
    /// WF ��ժҪ˵����
    /// ������
    /// �����������������
    /// ��������Ϣ��
    /// ���̵���Ϣ��
    /// </summary>
    public class WorkFlow
    {
        public string ToE(string no, string chName)
        {
            return BP.Sys.Language.GetValByUserLang(no, chName);
        }
        public string ToEP1(string no, string chName, string v)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(no, chName), v);
        }
        public string ToEP2(string no, string chName, string v, string v2)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(no, chName), v, v2);
        }

        #region ��ǰ����ͳ����Ϣ
        /// <summary>
        /// ������Χ�����еĸ�����
        /// </summary>
        public static int NumOfRuning(string fk_emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_EMP='" + fk_emp + "' AND WorkTimeState=0";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        /// <summary>
        /// ���뾯�����޵ĸ���
        /// </summary>
        public static int NumOfAlert(string fk_emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_EMP='" + fk_emp + "' AND WorkTimeState=1";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        /// <summary>
        /// ����
        /// </summary>
        public static int NumOfTimeout(string fk_emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_EMP='" + fk_emp + "' AND WorkTimeState=2";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        #endregion

        #region  Ȩ�޹���
        /// <summary>
        /// �ǲ����ܹ�����ǰ�Ĺ�����
        /// </summary>
        /// <param name="empId">������ԱID</param>
        /// <returns>�ǲ����ܹ�����ǰ�Ĺ���</returns>
        public bool IsCanDoCurrentWork(string empId)
        {
            //return true;
            // �ҵ���ǰ�Ĺ����ڵ�
            WorkNode wn = this.GetCurrentWorkNode();

            // �ж��ǲ��ǿ�ʼ�����ڵ�..
            if (wn.HisNode.IsStartNode)
            {
                // ���������ж��ǲ��������Ȩ�ޡ�
                return WorkFlow.IsCanDoWorkCheckByEmpStation(wn.HisNode.NodeID, empId);
            }

            // �ж����Ĺ������ɵĹ�����.
            WorkerLists gwls = new WorkerLists(this.WorkID, wn.HisNode.NodeID);
            if (gwls.Count == 0)
            {
                //return true;
                //throw new Exception("@�������̶������,û���ҵ��ܹ�ִ�д��������Ա.�����Ϣ:����ID="+this.WorkID+",�ڵ�ID="+wn.HisNode.NodeID );
                throw new Exception("@" + this.ToE("WF0", "�������̶������,û���ҵ��ܹ�ִ�д��������Ա.�����Ϣ:") + " WorkID=" + this.WorkID + ",NodeID=" + wn.HisNode.NodeID);
            }

            foreach (WorkerList en in gwls)
            {
                if (en.FK_Emp == empId)
                    return true;
            }
            return false;
        }
        #endregion

        #region ���̹�������
        /// <summary>
        /// ִ�в���
        /// Ӧ�ó���:��������ֺϵ㲵��ʱ
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="fk_node"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string DoReject(Int64 fid, int fk_node, string msg)
        {
            WorkerList wl = new WorkerList();
            int i = wl.Retrieve(WorkerListAttr.FID, fid,
                WorkerListAttr.WorkID, this.WorkID,
                WorkerListAttr.FK_Node, fk_node);
            if (i == 0)
                throw new Exception("ϵͳ����û���ҵ�Ӧ���ҵ������ݡ�");

            i = wl.Delete();
            if (i == 0)
                throw new Exception("ϵͳ����û��ɾ��Ӧ��ɾ�������ݡ�");

            wl = new WorkerList();
            i = wl.Retrieve(WorkerListAttr.FID, fid,
                WorkerListAttr.WorkID, this.WorkID,
                WorkerListAttr.IsPass, 3);

            if (i == 0)
                throw new Exception("ϵͳ�������ҵ��˻ص�ԭʼ���û���ҵ���");

            // ���µ�ǰ���̹��������õ�ǰ�Ľڵ㡣
            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FK_Node=" + wl.FK_Node + ", NodeName='" + wl.FK_NodeText + "' WHERE WorkID=" + this.WorkID);
            
            wl.RDT = DataType.CurrentDataTime;
            wl.IsPass = false;
            wl.Update();

            return "�����Ѿ����ص�(" + wl.FK_Emp + " , " + wl.FK_EmpText + ")";
            // wl.HisNode
        }
        /// <summary>
        /// ������ɾ����������
        /// </summary>
        public string DoDeleteWorkFlowByReal()
        {


            string info = "";
            WorkNode wn = this.GetCurrentWorkNode();

            // �����¼���
             wn.HisNode.HisNDEvents.DoEventNode(EventListOfNode.BeforeFlowDel, wn.HisWork);


            DBAccess.RunSQL("DELETE FROM WF_Track WHERE WorkID=" + this.WorkID);
            DBAccess.RunSQL("DELETE FROM ND"+int.Parse(this.HisFlow.No)+"Rpt WHERE OID=" + this.WorkID);


            #region ������ɾ����Ϣ.
            BP.DA.Log.DefaultLogWriteLineInfo("@[" + this.HisFlow.Name + "]���̱�[" + BP.Web.WebUser.No + BP.Web.WebUser.Name + "]ɾ����WorkID[" + this.WorkID + "]��");
            string msg = "";
            try
            {
                Int64 workId = this.WorkID;
                string flowNo = this.HisFlow.No;
            }
            catch (Exception ex)
            {
                throw new Exception("��ȡ���̵� ID �����̱�� ���ִ���" + ex.Message);
            }

            try
            {
                //@,ɾ�����������̿�����Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_CHOfFlow WHERE WorkID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");

                // ɾ��������Ϣ.
                DBAccess.RunSQL("DELETE FROM WF_Bill WHERE WorkID=" + this.WorkID);

                // ɾ��track.
                DBAccess.RunSQL("DELETE FROM wf_track WHERE WorkID=" + this.WorkID);


                //ɾ�����Ĺ���.
                DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE  FID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE (WorkID=" + this.WorkID + " OR FID=" + this.WorkID + " ) AND FK_Flow='" + this.HisFlow.No + "'");
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkerList WHERE (WorkID=" + this.WorkID + " OR FID=" + this.WorkID + " ) AND FK_Flow='" + this.HisFlow.No + "'");

                Nodes nds = this.HisFlow.HisNodes;
                foreach (Node nd in nds)
                {
                    try
                    {
                        DBAccess.RunSQL("DELETE FROM ND" + nd.NodeID + " WHERE OID=" + this.WorkID + " OR FID=" + this.WorkID);
                    }
                    catch (Exception ex)
                    {
                        msg += "@ delete data error " + ex.Message;
                        // Log.DefaultLogWriteLineError(ex.Message);
                    }
                }
                if (msg != "")
                {
                    Log.DebugWriteInfo(msg);
                    //throw new Exception("@�Ѿ��ӹ������б����������.ɾ���ڵ���Ϣ�����ִ���:" + msg);
                }
            }
            catch (Exception ex)
            {
                string err = "@" + this.ToE("WF1", "ɾ����������") + "[" + this.HisStartWork.OID + "," + this.HisStartWork.Title + "] Err " + ex.Message;
                Log.DefaultLogWriteLine(LogType.Error, err);
                throw new Exception(err);
            }
            info = "@ɾ������ɾ���ɹ�";
            #endregion ������ɾ����Ϣ.

            #region ���������ɾ������������ʵ����⡣
            if (this.FID != 0)
            {
                string sql = "";
                /* 
                 * ȡ������ȡͣ����,û�л�ȡ��˵��û���κ����̵߳���������λ��.
                 */
                sql = "SELECT FK_Node FROM WF_GenerWorkerList WHERE WorkID=" + wn.HisWork.FID + " AND IsPass=3";
                int fk_node = DBAccess.RunSQLReturnValInt(sql, 0);
                if (fk_node != 0)
                {
                    /* ˵�����Ǵ�����״̬ */
                    Node nextNode = new Node(fk_node);
                    if (nextNode.PassRate > 0)
                    {
                        /* �ҵ��ȴ�����ڵ����һ���� */
                        Nodes priNodes = nextNode.HisFromNodes;
                        if (priNodes.Count != 1)
                            throw new Exception("@û��ʵ�������̲�ͬ�̵߳�����");

                        Node priNode = (Node)priNodes[0];

                        #region ���������
                        sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + priNode.NodeID + " AND FID=" + wn.HisWork.FID + " AND IsPass=1";
                        decimal ok = (decimal)DBAccess.RunSQLReturnValInt(sql);
                        sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + priNode.NodeID + " AND FID=" + wn.HisWork.FID;
                        decimal all = (decimal)DBAccess.RunSQLReturnValInt(sql);
                        if (all == 0)
                        {
                            /*˵��:���е����̶߳���ɱ����, ��Ӧ���������̽�����*/
                            WorkFlow wf = new WorkFlow(this.HisFlow, this.FID);
                            info += "@���е����߳��Ѿ�������";
                            info += "@������������Ϣ��";
                            info += "@" + wf.DoFlowOver("");
                        }

                        decimal passRate = ok / all * 100;
                        if (nextNode.PassRate <= passRate)
                        {
                            /*˵��ȫ������Ա������ˣ����ú�������ʾ����*/
                            DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE IsPass=3  AND WorkID=" + wn.HisWork.FID + " AND FK_Node=" + fk_node);
                        }
                        #endregion ���������
                    }
                } /* �����д�����״̬�жϡ�*/

                if (fk_node == 0)
                {
                    /* ˵��:û���ҵ��ȴ����������ĺ����ڵ�. */
                    GenerWorkFlow gwf = new GenerWorkFlow(this.FID);
                    Node fND = new Node(gwf.FK_Node);
                    switch (fND.HisNodeWorkType)
                    {
                        case NodeWorkType.WorkHL: /*���������е�����������*/
                            break;
                        default:
                            /* ���ɾ�����һ��������ʱҪ�Ѹ�����ҲҪɾ����*/
                            sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + wn.HisNode.NodeID + " AND FID=" + wn.HisWork.FID;
                            int num = DBAccess.RunSQLReturnValInt(sql);
                            if (num == 0)
                            {
                                /*˵��û���ӽ��̣���Ҫ���������ִ����ɡ�*/
                                WorkFlow wf = new WorkFlow(this.HisFlow, this.FID);
                                info += "@���е����߳��Ѿ�������";
                                info += "@������������Ϣ��";
                                info += "@" + wf.DoFlowOver("");
                            }
                            break;
                    }
                }
            }
            #endregion

            return info;
        }
        /// <summary>
        /// ɾ����ǰ�Ĺ��������ñ��.
        /// </summary>
        public void DoDeleteWorkFlowByFlag(string msg)
        {
            try
            {
                //�������̵�״̬Ϊǿ����ֹ״̬
                WorkNode nd = GetCurrentWorkNode();
                nd.HisWork.NodeState = NodeState.Stop;
                nd.HisWork.DirectUpdate();
                //���õ�ǰ�Ĺ����ڵ���ǿ����ֹ״̬
                StartWork sw = this.HisStartWork;
                sw.WFState = BP.WF.WFState.Delete;
                sw.DirectUpdate();
                //���ò����Ĺ�������Ϊ.
                GenerWorkFlow gwf = new GenerWorkFlow(sw.OID);
                gwf.WFState = 3;
                gwf.Update();
                // ɾ����Ϣ.
                BP.WF.MsgsManager.DeleteByWorkID(sw.OID);
                //WorkerLists wls = new WorkerLists(this
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@�߼�ɾ�����ִ���:" + ex.Message);
                throw new Exception("@�߼�ɾ�����ִ���:" + ex.Message);
            }
        }

       

        #region ���̵�ǿ����ֹ\ɾ�� ���߻ָ�ʹ������,
        /// <summary>
        /// ǿ����ֹ����. 
        ///  1, �������̵�״̬Ϊǿ����ֹ״̬.
        ///  2, ���õ�ǰ�Ĺ����ڵ���ǿ����ֹ״̬. 
        ///  3, ���ò����Ĺ�������Ϊ ǿ����ֹ״̬ .
        ///  4, ��ȥ��ǰ������Ա����Ϣ.
        /// </summary>
        /// <param name="msg"></param>
        public void DoStopWorkFlow(string msg)
        {
            try
            {
                //�������̵�״̬Ϊǿ����ֹ״̬
                //			//	WorkNode nd = GetCurrentWorkNode();
                //				nd.HisWork.NodeState = 4;
                //				nd.HisWork.Update();

                //���õ�ǰ�Ĺ����ڵ���ǿ����ֹ״̬
                StartWork sw = this.HisStartWork;
                sw.WFState = BP.WF.WFState.Stop;
                //sw.NodeState=4;
                sw.DirectUpdate();

                //���ò����Ĺ�������Ϊ
                GenerWorkFlow gwf = new GenerWorkFlow(sw.OID);
                gwf.WFState = 2;
                gwf.DirectUpdate();
                // ɾ����Ϣ.
                BP.WF.MsgsManager.DeleteByWorkID(sw.OID);
                //WorkerLists wls = new WorkerLists(this
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@ǿ����ֹ���̴���." + ex.Message);
                throw new Exception("@ǿ����ֹ���̴���." + ex.Message);
            }
        }
        public string DoSelfTest()
        {
            string msg = "";
            if (this.IsComplete)
                return "�����Ѿ���������������졣";

            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            if (gwf.WFState == (int)WFState.Complete)
                return "�����Ѿ���������������졣";


            // �ж��Ƿ��ж���ڵ�״̬�ڵȴ������״̬�Ĵ���
            WorkNodes ens = this.HisWorkNodesOfWorkID;
            int i = 0;

            #region �����жϵ�ǰ�Ĺ����ڵ�� �ڵ������Ƿ���ڡ�
            int num = 0;
            foreach (WorkNode wn in ens)
            {
                Work wk = wn.HisWork;
                Node nd = wn.HisNode;
                if (wk.NodeState != NodeState.Complete)
                {
                    /*���û����� */
                    if (nd.NodeID != gwf.FK_Node)
                    {
                        num++; ;
                    }
                }
            }

            if (num == 0)
            {
                return "û���ҵ���ǰ�Ĺ��������̴���";
            }

            foreach (WorkNode wn in ens)
            {
                Work wk = wn.HisWork;
                Node nd = wn.HisNode;

                if (wk.NodeState != NodeState.Complete)
                {
                    /*���û����� */
                    if (nd.NodeID != gwf.FK_Node)
                    {
                        /*������ǵ�ǰ�Ĺ����ڵ㡣*/
                        wk.NodeState = NodeState.Complete;
                        wk.Update();
                        msg += "���̵�����ɣ�������һ���������ж����ǰ��������������Ϊ�ֹ��ĵ���������ɵġ�";
                    }
                }
            }
            #endregion


            if (msg == "")
                return "@����û�����⣬�û����������ַ�ʽɾ������1���˻ص���һ�����ڡ�2�������ص�ϵͳ����Ա����ɾ����";
            else
                return msg;
        }
        /// <summary>
        /// �ָ�����.
        /// </summary>
        /// <param name="msg">�ظ����̵�ԭ��</param>
        public void DoComeBackWrokFlow(string msg)
        {
            try
            {
                // ���õ�ǰ�Ĺ����ڵ���ǿ����ֹ״̬
                StartWork sw = this.HisStartWork;
                sw.WFState = 0;
                sw.DirectUpdate();

                //���ò����Ĺ�������Ϊ
                GenerWorkFlow gwf = new GenerWorkFlow(sw.OID);
                gwf.WFState = 0;
                gwf.DirectUpdate();

                // ������Ϣ 
                WorkNode wn = this.GetCurrentWorkNode();
                WorkerLists wls = new WorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);
                if (wls.Count == 0)
                    throw new Exception("@�ָ����̳��ִ���,�����Ĺ������б�");
                BP.WF.MsgsManager.AddMsgs(wls, "�ָ�������", wn.HisNode.Name, "�ظ�������");
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@�ָ����̳��ִ���." + ex.Message);
                throw new Exception("@�ָ����̳��ִ���." + ex.Message);
            }
        }
        #endregion


        /// <summary>
        /// �õ���ǰ�Ľ����еĹ�����
        /// </summary>
        /// <returns></returns>		 
        public WorkNode GetCurrentWorkNode()
        {
            //if (this.IsComplete)
            //    throw new Exception("@��������[" + this.HisStartWork.Title + "],�Ѿ���ɡ�");

            int currNodeID = 0;
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            gwf.WorkID = this.WorkID;
            if (gwf.RetrieveFromDBSources() == 0)
            {
                this.DoFlowOver("������������û���ҵ���ǰ�����̼�¼��");
                throw new Exception("@" + this.ToEP1("WF2", "��������{0}�Ѿ���ɡ�", this.HisStartWork.Title));
            }

            Node nd = new Node(gwf.FK_Node);
            Work work = nd.HisWork;
            work.OID = this.WorkID;
            work.NodeID = nd.NodeID;
            work.SetValByKey("FK_Dept", Web.WebUser.FK_Dept);

            if (work.RetrieveFromDBSources() == 0)
            {
                Log.DefaultLogWriteLineError("@" + this.ToE("WF3", "û���ҵ���ǰ�Ĺ����ڵ�����ݣ����̳���δ֪���쳣��")); // û���ҵ���ǰ�Ĺ����ڵ�����ݣ����̳���δ֪���쳣��
                work.Rec = Web.WebUser.No;
                try
                {
                    work.Insert();
                }
                catch
                {
                    Log.DefaultLogWriteLineError("@" + this.ToE("WF3", "û���ҵ���ǰ�Ĺ����ڵ�����ݣ����̳���δ֪���쳣��") + "��"); // û���ҵ���ǰ�Ĺ����ڵ������
                }
            }
            work.FID = gwf.FID;

            WorkNode wn = new WorkNode(work, nd);
            return wn;
        }
        /// <summary>
        /// ����������̽���
        /// </summary>
        /// <param name="sw"></param>
        public string DoDoFlowOverHeLiu_del()
        {
            GenerFH gh = new GenerFH();
            gh.FID = this.WorkID;
            if (gh.RetrieveFromDBSources() == 0)
                throw new Exception("ϵͳ�쳣");
            else
                gh.Delete();

            GenerWorkFlows ens = new GenerWorkFlows();
            ens.Retrieve(GenerWorkFlowAttr.FID, this.WorkID);

            string msg = "";
            foreach (GenerWorkFlow en in ens)
            {
                if (en.WorkID == en.FID)
                    continue;

                /*����ÿһ��������*/
                WorkFlow fl = new WorkFlow(en.FK_Flow, en.WorkID, en.FID);
                // msg += fl.DoFlowOverOrdinary();
            }
            return msg;
        }
        /// <summary>
        /// ���������Ľڵ�
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public string DoDoFlowOverFeiLiu(GenerWorkFlow gwf)
        {
            // ��ѯ��������û����ɵ����̡�
            int i = BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_GenerWorkFlow WHERE FID=" + gwf.FID + " AND WFState!=1");
            switch (i)
            {
                case 0:
                    throw new Exception("@��Ӧ�õĴ���");
                case 1:
                    BP.DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow  WHERE FID=" + gwf.FID + " OR WorkID=" + gwf.FID);
                    BP.DA.DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FID=" + gwf.FID + " OR WorkID=" + gwf.FID);
                    BP.DA.DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE FID=" + gwf.FID);

                    StartWork wk = this.HisFlow.HisStartNode.HisWork as StartWork;
                    wk.OID = gwf.FID;
                    wk.WFState = WFState.Complete;
                    wk.NodeState = NodeState.Complete;
                    wk.Update();

                    return "@��ǰ�Ĺ����Ѿ���ɣ������������еĹ������Ѿ���ɡ�";
                default:
                    BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=1 WHERE WorkID=" + this.WorkID);
                    BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=1 WHERE WorkID=" + this.WorkID);
                    return "@��ǰ�Ĺ����Ѿ���ɡ�";
            }
        }
        /// <summary>
        /// �������������.
        /// </summary>
        /// <returns></returns>
        public string DoFlowSubOver()
        {
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            Node nd = new Node(gwf.FK_Node);
            BP.DA.DBAccess.RunSQL("UPDATE  " + nd.PTable + " SET NodeState=1 WHERE OID=" + this.WorkID); // ���¿�ʼ�ڵ��״̬��

            DBAccess.RunSQL("DELETE WF_GenerWorkFlow   WHERE WorkID=" + this.WorkID);
            DBAccess.RunSQL("DELETE WF_GenerWorkerlist WHERE WorkID=" + this.WorkID);

            string sql = "SELECT count(*) FROM WF_GenerWorkFlow WHERE  FID=" + this.FID;
            int num = DBAccess.RunSQLReturnValInt(sql);
            if (DBAccess.RunSQLReturnValInt(sql) == 0)
            {
                /*˵���������һ��*/
                WorkFlow wf = new WorkFlow(gwf.FK_Flow, this.FID);
                wf.DoFlowOver("");
                return "@��ǰ����������ɣ�����������ɡ�";
            }
            else
            {
                return "@��ǰ����������ɣ������̻���(" + num + ")��������δ��ɡ�";
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="stopMsg">����ԭ��</param>
        /// <returns></returns>
        public string DoFlowOver(string stopMsg)
        {
            string msg = this.BeforeFlowOver();
            if (this.IsMainFlow == false)
            {
                /* �������������*/
                return this.DoFlowSubOver();
            }

            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            Node nd = new Node(gwf.FK_Node);

            /* �����һ�����߳� */
            BP.DA.DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE OID=" + this.WorkID); // ���¿�ʼ�ڵ��״̬��

            // ��ѯ������������ݣ���������ݣ����Թ���ϸ���ơ� 
            BP.Sys.GEEntity geRpt = new GEEntity("ND" + int.Parse(this.HisFlow.No) + "Rpt");
            geRpt.SetValByKey("OID", this.WorkID);
            geRpt.RetrieveFromDBSources();
            geRpt.SetValByKey("FK_NY", DataType.CurrentYearMonth);

            string emps = "";
            WorkerLists wlss = new WorkerLists();
            QueryObject qo = new QueryObject(wlss);

            qo.AddWhere(WorkerListAttr.FID, this.WorkID);
            qo.addOr();
            qo.AddWhere(WorkerListAttr.WorkID, this.WorkID);

            qo.addOrderBy(WorkerListAttr.RDT);
            qo.DoQuery();

            foreach (WorkerList wl in wlss)
            {
                if (wl.IsEnable == false)
                    continue;
                string str = "@" + wl.FK_Emp + "," + wl.FK_EmpText;
                if (emps.Contains(str))
                    continue;
                emps += str;
            }

            geRpt.SetValByKey(GERptAttr.FlowEmps, emps);
            geRpt.SetValByKey(GERptAttr.FlowEnder, Web.WebUser.No);
            geRpt.SetValByKey(GERptAttr.FlowEnderRDT, DataType.CurrentDataTime);
            geRpt.SetValByKey(GERptAttr.WFState, (int)WFState.Complete);
            geRpt.SetValByKey(GERptAttr.MyNum, 1);

            //����ʱ���ȡ�
            geRpt.SetValByKey(GERptAttr.FlowDaySpan,
                DataType.GetSpanDays(geRpt.GetValStringByKey(GERptAttr.FlowStartRDT), DataType.CurrentDataTime));

            geRpt.Save();

            //geRpt.Update("Emps", emps);
            //������ϸ���ݵ�copy���⡣ ���ȼ�飺��ǰ�ڵ㣨���ڵ㣩�Ƿ�����ϸ��
            MapDtls dtls = new MapDtls("ND" + nd.NodeID);
            int i = 0;
            foreach (MapDtl dtl in dtls)
            {
                i++;
                // ��ѯ������ϸ���е����ݡ�
                GEDtls dtlDatas = new GEDtls(dtl.No);
                dtlDatas.Retrieve(GEDtlAttr.RefPK, this.WorkID);

                GEDtl geDtl = null;
                try
                {
                    // ����һ��Rpt����
                    geDtl = new GEDtl("ND" + int.Parse(this.HisFlow.No) + "RptDtl" + i.ToString());
                    geDtl.ResetDefaultVal();
                }
                catch
                {
#warning �˴���Ҫ�޸���
                    continue;
                }

                // ���Ƶ�ָ���ı����С�
                foreach (GEDtl dtlData in dtlDatas)
                {
                    geDtl.ResetDefaultVal();
                    try
                    {
                        geDtl.Copy(geRpt); // ������������ݡ�
                        geDtl.Copy(dtlData);
                        geDtl.SetValByKey("FlowStarterDept", geRpt.GetValStrByKey("FK_Dept")); // �����˲���.
                        geDtl.SetValByKey("FlowStartRDT", geRpt.GetValStrByKey("RDT")); //����ʱ�䡣
                        geDtl.Insert();
                    }
                    catch
                    {
                        geDtl.Update();
                    }
                }
            }
            this._IsComplete = 1;
            DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE FID=" + this.WorkID);

            // ������̱��.
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE (WorkID=" + this.WorkID + " OR FID=" + this.WorkID + ")  AND FK_Flow='" + this.HisFlow.No + "'");
            // ��������Ĺ����ߡ�
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE (WorkID=" + this.WorkID + " OR FID=" + this.WorkID + ")  AND FK_Node IN (SELECT NodeId FROM WF_Node WHERE FK_Flow='" + this.HisFlow.No + "') ");
            return msg;
        }
        /// <summary>
        /// �ڷ����Ͻ������̡�
        /// </summary>
        /// <returns></returns>
        public string DoFlowOverBranch123(Node nd)
        {
            string sql = "";
            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=1 WHERE WorkID=" + this.WorkID);

            if (this.HisFlow.HisStartNode.HisFNType == FNType.River)
            {
                /* ��ʼ�ڵ��Ǹ��� */
            }
            else
            {
                BP.DA.DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE OID=" + this.WorkID); // ���¿�ʼ�ڵ��״̬��
            }

            string msg = "";
            // �ж��������Ƿ�û��û����ɵ�֧����
            sql = "SELECT COUNT(WORKID) FROM WF_GenerWorkFlow WHERE WFState!=1 AND FID=" + this.FID;

            DataTable dt = DBAccess.RunSQLReturnTable("SELECT Rec FROM ND" + nd.NodeID + " WHERE FID=" + this.FID);
            if (DBAccess.RunSQLReturnValInt(sql) == 0)
            {
                /* ���ȫ����� */
                if (this.HisFlow.HisStartNode.HisFNType == FNType.River)
                {
                    BP.DA.DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE FID=" + this.FID);
                }

                /*�������̶�������*/
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE FID=" + this.FID);
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FID=" + this.FID);

                /* �������������ɵ���Ϣ������ǰ���û���*/
                msg += "@����������ȫ��������{" + dt.Rows.Count + "}����Ա�����˷�֧���̣��������һ����ɴ˹�������Ա��@��֧���̲������������£�";
                foreach (DataRow dr in dt.Rows)
                {
                    msg += dr[0].ToString() + "��";
                }
                return msg;
                //   return "@����������ȫ������" + this.GenerFHStartWorkInfo();
            }
            else
            {
                /* ����������Աû����ɴ˹�����*/

                msg += "@���Ĺ����Ѿ��ꡣ@��������Ŀǰ��û����ȫ��������{" + dt.Rows.Count + "}����Ա�����˷�֧���̣��������£�";
                foreach (DataRow dr in dt.Rows)
                {
                    msg += dr[0].ToString() + "��";
                }
                return msg;
            }
        }
        /// <summary>
        /// �ڷ����Ͻ������̡�
        /// </summary>
        /// <returns></returns>
        public string DoFlowOverBranch_Bak(Node nd)
        {
            string sql = "";
            if (this.HisFlow.HisStartNode.HisFNType == FNType.River)
            {
                /* �����ʼ�ڵ��Ǹ����������ڵ���֧����*/
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=1 WHERE WorkID=" + this.WorkID);

                // �ж��Ƿ���û�н�����֧����
                sql = "SELECT COUNT(WORKID) FROM WF_GenerWorkFlow WHERE WFState!=1 AND FID=" + this.FID;
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    StartWork sw = this.HisStartWorkNode.HisWork as StartWork;
                    sw.FID = this.FID;
                    sw.OID = this.FID;
                    int i = sw.RetrieveFromDBSources();
                    if (i == 0)
                    {
                        throw new Exception("@��ʼ�ڵ���Ϣ��ʧ��");
                    }
                    else
                    {
                        sw.Update("WFState", (int)WFState.Complete);
                    }

                    /*�������̶�������*/
                    DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE FID=" + this.FID);
                    DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FID=" + this.FID);
                }
                return "@�����ڸ����Ͻ�����";
            }

            /*��ʼ�ڵ���֧���������ڵ���֧����*/
            StartWork mysw = this.HisStartWorkNode.HisWork as StartWork;
            mysw.OID = this.WorkID;
            mysw.Update("WFState", (int)WFState.Complete);

            //i = sw.RetrieveFromDBSources();
            //if (i == 0)
            //{
            //    throw new Exception("@��ʼ�ڵ���Ϣ��ʧ��");
            //}
            //else
            //{
            //    sw.Update("WFState", (int)WFState.Complete);
            //}

            // ��������״̬��
            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=1 WHERE WorkID=" + this.WorkID);
            //   BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET WFState=0 WHERE WorkID=" + this.WorkID);

            // �ж��Ƿ���û�н�����֧����
            sql = "SELECT COUNT(WORKID) FROM WF_GenerWorkFlow WHERE WFState!=1 AND FID=" + this.FID;
            if (DBAccess.RunSQLReturnValInt(sql) == 0)
            {
                /*�������̶�������*/
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE FID=" + this.FID);
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FID=" + this.FID);
            }

            return "@�����ڷ����Ͻ�����";
        }
        /// <summary>
        /// �ڸ����Ͻ�������
        /// </summary>
        /// <param name="nd">�����Ľڵ�</param>
        /// <returns>���ص���Ϣ</returns>
        public string DoFlowOverRiver(Node nd)
        {
            try
            {
                string msg = "";

                /* ���¿�ʼ�ڵ��״̬��*/
                DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE OID=" + this.WorkID);

                /*�������̶�������*/
                DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE FID=" + this.WorkID);
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE FID=" + this.WorkID + " OR WorkID=" + this.WorkID);
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FID=" + this.WorkID + " OR WorkID=" + this.WorkID);
                return msg;
            }
            catch (Exception ex)
            {
                throw new Exception("@��������ʱ������쳣��" + ex.Message);
            }
        }
        /// <summary>
        /// �ڸ����Ͻ�������
        /// </summary>
        /// <param name="nd">�����Ľڵ�</param>
        /// <returns>���ص���Ϣ</returns>
        public string DoFlowOverRiver_bak(Node nd)
        {
            try
            {
                string msg = "";

                /* ���¿�ʼ�ڵ��״̬��*/
                DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE OID=" + this.WorkID);

                /*�������̶�������*/
                DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE FID=" + this.WorkID);
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE FID=" + this.WorkID);
                DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FID=" + this.FID);
                return msg;
            }
            catch (Exception ex)
            {
                throw new Exception("@��������ʱ������쳣��" + ex.Message);
            }

            //try
            //{
            //    string msg = "";

            //    /* ���¿�ʼ�ڵ��״̬��*/
            //    DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE OID=" + this.WorkID);

            //    /*�������̶�������*/
            //    DBAccess.RunSQL("DELETE FROM WF_GenerFH WHERE FID=" + this.WorkID);
            //    DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE FID=" + this.WorkID);
            //    DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE FID=" + this.FID);
            //    return msg;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("@��������ʱ������쳣��" + ex.Message);
            //}
        }
        public string GenerFHStartWorkInfo()
        {
            string msg = "";
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT Title,RDT,Rec,OID FROM ND" + this.StartNodeID + " WHERE FID=" + this.FID);
            switch (dt.Rows.Count)
            {
                case 0:
                    Node nd = new Node(this.StartNodeID);
                    throw new Exception("@û���ҵ����ǿ�ʼ�ڵ�����ݣ������쳣��FID=" + this.FID + "���ڵ㣺" + nd.Name + "�ڵ�ID��" + nd.NodeID);
                case 1:
                    msg = string.Format("@�����ˣ� {0}  ���ڣ�{1} ��������� ���⣺{2} ���Ѿ��ɹ���ɡ�",
                        dt.Rows[0]["Rec"].ToString(), dt.Rows[0]["RDT"].ToString(), dt.Rows[0]["Title"].ToString());
                    break;
                default:
                    msg = "@����(" + dt.Rows.Count + ")λ��Ա����������Ѿ���ɡ�";
                    foreach (DataRow dr in dt.Rows)
                    {
                        msg += "<br>�����ˣ�" + dr["Rec"] + " �������ڣ�" + dr["RDT"] + " ���⣺" + dr["Title"] + "<a href='./../../WF/WFRpt.aspx?WorkID=" + dr["OID"] + "&FK_Flow=" + this.HisFlow.No + "' target=_blank>��ϸ...</a>";
                    }
                    break;
            }
            return msg;
        }
        public int StartNodeID
        {
            get
            {
                return int.Parse(this.HisFlow.No + "01");
            }
        }
        /// <summary>
        /// ���������̽���
        /// </summary>		 
        public string DoFlowOverPlane(Node nd)
        {

            // ���ÿ�ʼ�ڵ��״̬��
            StartWork sw = this.HisStartWorkNode.HisWork as StartWork;
            sw.OID = this.WorkID;
            //sw.Update("WFState", (int)sw.WFState);
            sw.Update("WFState", (int)WFState.Complete);

            //��ѯ������������ݣ���������ݣ����Թ���ϸ���ơ�
            BP.Sys.GEEntity geRpt = new GEEntity("ND" + int.Parse(this.HisFlow.No) + "Rpt");
            geRpt.SetValByKey("OID", this.WorkID);
            geRpt.Retrieve();
            geRpt.SetValByKey("FK_NY", DataType.CurrentYearMonth);


            string emps = ",";
            WorkerLists wls = new WorkerLists(this.WorkID, this.HisFlow.No);
            foreach (WorkerList wl in wls)
            {
                if (wl.IsEnable == false)
                    continue;
                emps += wl.FK_Emp + ",";
            }
            geRpt.SetValByKey("Emps", emps);
            geRpt.Update();

            //geRpt.Update("Emps", emps);
            //������ϸ���ݵ�copy���⡣ ���ȼ�飺��ǰ�ڵ㣨���ڵ㣩�Ƿ�����ϸ��

            MapDtls dtls = new MapDtls("ND" + nd.NodeID);
            int i = 0;
            foreach (MapDtl dtl in dtls)
            {
                i++;
                // ��ѯ������ϸ���е����ݡ�
                GEDtls dtlDatas = new GEDtls(dtl.No);
                dtlDatas.Retrieve(GEDtlAttr.RefPK, this.WorkID);

                // ����һ��Rpt����
                GEEntity geDtl = new GEEntity("ND" + int.Parse(this.HisFlow.No) + "RptDtl" + i.ToString());
                // ���Ƶ�ָ���ı����С�
                foreach (GEDtl dtlData in dtlDatas)
                {
                    geDtl.ResetDefaultVal();
                    try
                    {
                        geDtl.Copy(geRpt); // ������������ݡ�
                        geDtl.Copy(dtlData);
                        geDtl.SetValByKey("FlowStarterDept", geRpt.GetValStrByKey("FK_Dept")); // �����˲���.
                        geDtl.SetValByKey("FlowStartRDT", geRpt.GetValStrByKey("RDT")); //����ʱ�䡣
                        geDtl.Insert();
                    }
                    catch
                    {
                        geDtl.Update();
                    }
                }
            }
            this._IsComplete = 1;


            // ������̡�
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkFlow WHERE (WorkID=" + this.WorkID + " OR FID=" + this.WorkID + ")  AND FK_Flow='" + this.HisFlow.No + "'");

            // ��������Ĺ����ߡ�
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE (WorkID=" + this.WorkID + " OR FID=" + this.WorkID + ")  AND FK_Node IN (SELECT NodeId FROM WF_Node WHERE FK_Flow='" + this.HisFlow.No + "') ");
            return "";


            //// �޸����̻����е�����״̬��
            //CHOfFlow chf = new CHOfFlow();
            //chf.WorkID = this.WorkID;
            //chf.Update("WFState", (int)sw.WFState);
            // +"@" + this.ToEP2("WF5", "��������{0},{1}������ɡ�", this.HisFlow.Name, this.HisStartWork.Title);  // ��������[" + HisFlow.Name + "] [" + HisStartWork.Title + "]������ɡ�;
        }
        /// <summary>
        /// �������֮��Ҫ���Ĺ�����
        /// </summary>
        private string BeforeFlowOver()
        {
            string ccmsg = "";
            if (this.HisFlow.IsCCAll == true)
            {
                ccmsg += "@�������̲�����Ա��";
                /*��������̽������Զ��ķ��͸�ȫ�������Ա*/
                ccmsg += this.CCTo(BP.DA.DBAccess.RunSQLReturnTable("SELECT DISTINCT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.WorkID + " AND IsEnable=1"));
            }


            #region ִ�����̳���
            if (this.HisFlow.CCStas.Length > 2)
            {
                //ccmsg += "@���ո�λ����������Ա��";
                ///* ��������˳�����Ա�ĸ�λ��*/
                //string sql = "SELECT No,Name FROM Port_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept + "%' AND NO IN (SELECT FK_EMP FROM Port_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                //DataTable dt = DBAccess.RunSQLReturnTable(sql);
                //if (dt.Rows.Count == 0 || Web.WebUser.FK_Dept.Length > 3)
                //{
                //    sql = "SELECT No,Name FROM Port_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept.Substring(0, Web.WebUser.FK_Dept.Length - 2) + "%' AND NO IN (SELECT FK_EMP FROM Port_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                //    dt = DBAccess.RunSQLReturnTable(sql);
                //}

                //if (dt.Rows.Count == 0 || Web.WebUser.FK_Dept.Length > 5)
                //{
                //    sql = "SELECT No,Name FROM Port_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept.Substring(0, Web.WebUser.FK_Dept.Length - 4) + "%' AND NO IN (SELECT FK_EMP FROM Port_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                //    dt = DBAccess.RunSQLReturnTable(sql);
                //}

                //if (dt.Rows.Count == 0 || Web.WebUser.FK_Dept.Length > 7)
                //{
                //    sql = "SELECT No,Name FROM Port_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept.Substring(0, Web.WebUser.FK_Dept.Length - 6) + "%' AND NO IN (SELECT FK_EMP FROM Port_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                //    dt = DBAccess.RunSQLReturnTable(sql);
                //}

                //if (dt.Rows.Count == 0)
                //{
                //    ccmsg += "@ϵͳû�л�ȡ��Ҫ���͵���Ա������Ա���õ���Ϣ���£�" + this.HisFlow.CCStas + "����ȷ���ø�λ���Ƿ��д���Ա��";
                //}
                //ccmsg += this.CCTo(dt);
            }
            #endregion
            return ccmsg;
        }
        /// <summary>
        ///  ���͵�
        /// </summary>
        /// <param name="dt"></param>
        public string CCTo(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return "";

            string emps = "";
            string empsExt = "";

            string ip = "127.0.0.1";
            System.Net.IPAddress[] addressList = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList;
            if (addressList.Length > 1)
                ip = addressList[1].ToString();
            else
                ip = addressList[0].ToString();


            foreach (DataRow dr in dt.Rows)
            {
                string no = dr[0].ToString();
                emps += no + ",";

                if (Glo.IsShowUserNoOnly)
                    empsExt += no + "��";
                else
                    empsExt += no + "<" + dr[1] + ">��";
            }

            Paras pss = new Paras();
            pss.Add("Sender", Web.WebUser.No);
            pss.Add("Receivers", emps);
            pss.Add("Title", "���������ͣ���������:" + this.HisFlow.Name + "��������ˣ�" + Web.WebUser.Name);
            pss.Add("Context", "�������� http://" + ip + "/" + System.Web.HttpContext.Current.Request.ApplicationPath + "/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=0");

            try
            {
                DBAccess.RunSP("CCstaff", pss);
                return "@" + empsExt;
            }
            catch (Exception ex)
            {
                return "@���ͳ��ִ���û�аѸ����̵���Ϣ���͵�(" + empsExt + ")����ϵ����Ա���ϵͳ�쳣" + ex.Message;
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ���Ľڵ�
        /// </summary>
        private Nodes _HisNodes = null;
        /// <summary>
        /// �ڵ�s
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                if (this._HisNodes == null)
                    this._HisNodes = this.HisFlow.HisNodes;
                return this._HisNodes;
            }
        }
        /// <summary>
        /// �����ڵ�s(��ͨ�Ĺ����ڵ�)
        /// </summary>
        private WorkNodes _HisWorkNodesOfWorkID = null;
        /// <summary>
        /// �����ڵ�s
        /// </summary>
        public WorkNodes HisWorkNodesOfWorkID
        {
            get
            {
                if (this._HisWorkNodesOfWorkID == null)
                {
                    this._HisWorkNodesOfWorkID = new WorkNodes();
                    this._HisWorkNodesOfWorkID.GenerByWorkID(this.HisFlow, this.WorkID);
                }
                return this._HisWorkNodesOfWorkID;
            }
        }
        /// <summary>
        /// �����ڵ�s
        /// </summary>
        private WorkNodes _HisWorkNodesOfFID = null;
        /// <summary>
        /// �����ڵ�s
        /// </summary>
        public WorkNodes HisWorkNodesOfFID
        {
            get
            {
                if (this._HisWorkNodesOfFID == null)
                {
                    this._HisWorkNodesOfFID = new WorkNodes();
                    this._HisWorkNodesOfFID.GenerByFID(this.HisFlow, this.FID);
                }
                return this._HisWorkNodesOfFID;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        private Flow _HisFlow = null;
        /// <summary>
        /// ��������
        /// </summary>
        public Flow HisFlow
        {
            get
            {
                return this._HisFlow;
            }
        }
        public GenerWorkFlow HisGenerWorkFlow
        {
            get
            {
                return new GenerWorkFlow(this.WorkID);
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        private Int64 _WorkID = 0;
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this._WorkID;
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        private Int64 _FID = 0;
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this._FID;
            }
        }
        /// <summary>
        /// �Ƿ��Ǹ���
        /// </summary>
        public bool IsMainFlow
        {
            get
            {
                if (this.FID != 0 && this.FID != this.WorkID)
                    return false;
                else
                    return true;
            }
        }
        #endregion

        #region ���췽��
        public WorkFlow(string fk_flow, Int64 wkid)
        {
            GenerWorkFlow gwf = new GenerWorkFlow(wkid);
            this._FID = gwf.FID;
            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            Flow flow = new Flow(fk_flow);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }

        public WorkFlow(Flow flow, Int64 wkid)
        {
            GenerWorkFlow gwf = new GenerWorkFlow();
            gwf.WorkID = wkid;
            gwf.RetrieveFromDBSources();

            this._FID = gwf.FID;
            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        /// <summary>
        /// ����һ������������
        /// </summary>
        /// <param name="flow">����No</param>
        /// <param name="wkid">����ID</param>
        public WorkFlow(Flow flow, Int64 wkid, Int64 fid)
        {
            this._FID = fid;
            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        public WorkFlow(string FK_flow, Int64 wkid, Int64 fid)
        {
            this._FID = fid;

            Flow flow = new Flow(FK_flow);
            if (wkid == 0)
                throw new Exception("@û��ָ������ID, ���ܴ�����������.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        #endregion

        #region ��������

        /// <summary>
        /// ��ʼ����
        /// </summary>
        private StartWork _HisStartWork = null;
        /// <summary>
        /// ����ʼ�Ĺ���.
        /// </summary>
        public StartWork HisStartWork
        {
            get
            {
                if (_HisStartWork == null)
                {
                    StartWork en = (StartWork)this.HisFlow.HisStartNode.HisWork;
                    en.OID = this.WorkID;
                    en.FID = this.FID;
                    if (en.RetrieveFromDBSources() == 0)
                        en.RetrieveFID();
                    _HisStartWork = en;
                }
                return _HisStartWork;
            }
        }
        /// <summary>
        /// ��ʼ�����ڵ�
        /// </summary>
        private WorkNode _HisStartWorkNode = null;
        /// <summary>
        /// ����ʼ�Ĺ���.
        /// </summary>
        public WorkNode HisStartWorkNode
        {
            get
            {
                if (_HisStartWorkNode == null)
                {
                    Node nd = this.HisFlow.HisStartNode;
                    StartWork en = (StartWork)nd.HisWork;
                    en.OID = this.WorkID;
                    en.Retrieve();

                    WorkNode wn = new WorkNode(en, nd);
                    _HisStartWorkNode = wn;

                }
                return _HisStartWorkNode;
            }
        }
        #endregion

        #region ��������
        public int _IsComplete = -1;
        /// <summary>
        /// �ǲ������
        /// </summary>
        public bool IsComplete
        {
            get
            {
                if (_IsComplete == -1)
                {
                    bool s = !DBAccess.IsExits("select workid from WF_GenerWorkFlow WHERE WorkID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");
                    if (s)
                        _IsComplete = 1;
                    else
                        _IsComplete = 0;
                }

                if (_IsComplete == 0)
                    return false;

                return true;
            }
        }
        /// <summary>
        /// �ǲ������
        /// </summary>
        public string IsCompleteStr
        {
            get
            {
                if (this.IsComplete)
                    return this.ToE("Already", "��");
                else
                    return this.ToE("Not", "δ");

            }
        }
        #endregion

        #region ��̬����

        /// <summary>
        /// �Ƿ����������Ա��ִ���������
        /// </summary>
        /// <param name="nodeId">�ڵ�</param>
        /// <param name="empId">������Ա</param>
        /// <returns>�ܲ���ִ��</returns> 
        public static bool IsCanDoWorkCheckByEmpStation(int nodeId, string empId)
        {
            bool isCan = false;
            // �жϸ�λ��Ӧ��ϵ�ǲ����ܹ�ִ��.
            string sql = "SELECT a.FK_Node FROM WF_NodeStation a,  Port_EmpStation b WHERE (a.FK_Station=b.FK_Station) AND (a.FK_Node=" + nodeId + " AND b.FK_Emp='" + empId + "' )";
            isCan = DBAccess.IsExits(sql);
            if (isCan)
                return true;
            // �ж�������Ҫ������λ�ܲ���ִ����.
            sql = "select FK_Node from WF_NodeStation WHERE FK_Node=" + nodeId + " AND ( FK_Station in (select FK_Station from Port_Empstation WHERE fk_emp='" + empId + "') ) ";
            return DBAccess.IsExits(sql);
        }
        /// <summary>
        /// �Ƿ����������Ա��ִ���������
        /// </summary>
        /// <param name="nodeId">�ڵ�</param>
        /// <param name="dutyNo">������Ա</param>
        /// <returns>�ܲ���ִ��</returns> 
        public static bool IsCanDoWorkCheckByEmpDuty(int nodeId, string dutyNo)
        {
            string sql = "SELECT a.FK_Node FROM WF_NodeDuty  a,  Port_EmpDuty b WHERE (a.FK_Duty=b.FK_Duty) AND (a.FK_Node=" + nodeId + " AND b.FK_Duty=" + dutyNo + ")";
            if (DBAccess.RunSQLReturnTable(sql).Rows.Count == 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// �Ƿ����������Ա��ִ���������
        /// </summary>
        /// <param name="nodeId">�ڵ�</param>
        /// <param name="DeptNo">������Ա</param>
        /// <returns>�ܲ���ִ��</returns> 
        public static bool IsCanDoWorkCheckByEmpDept(int nodeId, string DeptNo)
        {
            string sql = "SELECT a.FK_Node FROM WF_NodeDept  a,  Port_EmpDept b WHERE (a.FK_Dept=b.FK_Dept) AND (a.FK_Node=" + nodeId + " AND b.FK_Dept=" + DeptNo + ")";
            if (DBAccess.RunSQLReturnTable(sql).Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ���������ܹ������������Ա��
        /// </summary>
        /// <param name="nodeId">�ڵ�ID</param>		 
        /// <returns></returns>
        public static DataTable CanDoWorkEmps(int nodeId)
        {
            string sql = "select a.FK_Node, b.EmpID from WF_NodeStation  a,  Port_EmpStation b WHERE (a.FK_Station=b.FK_Station) AND (a.FK_Node=" + nodeId + " )";
            return DBAccess.RunSQLReturnTable(sql);
        }
        /// <summary>
        /// GetEmpsBy
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Emps GetEmpsBy(DataTable dt)
        {
            // �γ��ܹ��������������û����Ρ�
            Emps emps = new Emps();
            foreach (DataRow dr in dt.Rows)
            {
                emps.AddEntity(new Emp(dr["EmpID"].ToString()));
            }
            return emps;
        }

        #endregion

        #region ���̷���
        public string DoUnSendSubFlow(GenerWorkFlow gwf)
        {
            WorkNode wn = this.GetCurrentWorkNode();
            WorkNode wnPri = wn.GetPreviousWorkNode();

            WorkerList wl = new WorkerList();
            int num = wl.Retrieve(WorkerListAttr.FK_Emp, Web.WebUser.No,
                WorkerListAttr.FK_Node, wnPri.HisNode.NodeID);
            if (num == 0)
                return "@" + this.ToE("WF6", "������ִ�г������ͣ���Ϊ��ǰ�������������͵ġ�");

            // �����¼���
            string msg = wn.HisNode.HisNDEvents.DoEventNode(EventListOfNode.UndoneBefore, wn.HisWork);

            // ɾ�������ߡ�
            WorkerLists wls = new WorkerLists();
            wls.Delete(WorkerListAttr.WorkID, this.WorkID, WorkerListAttr.FK_Node, gwf.FK_Node.ToString());
            wn.HisWork.Delete();

            gwf.FK_Node = wnPri.HisNode.NodeID;
            gwf.NodeName = wnPri.HisNode.Name;

            gwf.Update();

            wnPri.HisWork.Update(WorkAttr.NodeState,
                (int)NodeState.Init);

            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=0 WHERE WorkID=" + this.WorkID + " AND FK_Node=" + gwf.FK_Node);
            ForwardWorks fws = new ForwardWorks();
            fws.Delete(ForwardWorkAttr.FK_Node, wn.HisNode.NodeID.ToString(), ForwardWorkAttr.WorkID, this.WorkID.ToString());

            #region �жϳ����İٷֱ��������ٽ������
            if (wn.HisNode.PassRate != 0)
            {
                decimal all = (decimal)BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) NUM FROM dbo.WF_GenerWorkerList WHERE FID=" + this.FID + " AND FK_Node=" + wnPri.HisNode.NodeID);
                decimal ok = (decimal)BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) NUM FROM dbo.WF_GenerWorkerList WHERE FID=" + this.FID + " AND IsPass=1 AND FK_Node=" + wnPri.HisNode.NodeID);
                decimal rate = ok / all * 100;
                if (wn.HisNode.PassRate <= rate)
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node=" + wn.HisNode.NodeID + " AND WorkID=" + this.FID);
                else
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=3 WHERE FK_Node=" + wn.HisNode.NodeID + " AND WorkID=" + this.FID);
            }
            #endregion

            // �����¼���
            msg += wn.HisNode.HisNDEvents.DoEventNode(EventListOfNode.UndoneAfter, wn.HisWork);

            // ��¼��־..
            wn.AddToTrack(ActionType.Undo, WebUser.No, WebUser.Name, wn.HisNode.NodeID, wn.HisNode.Name, "��");

            if (wnPri.HisNode.IsStartNode)
            {
                if (Web.WebUser.IsWap)
                {
                    return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=" + gwf.FID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='" + this.VirPath + "/WF/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='" + this.VirPath + "/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��" + msg;
                }
                else
                {
                    if (this.HisFlow.FK_FlowSort != "00")
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=" + gwf.FID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='" + this.VirPath + "/WF/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='" + this.VirPath + "/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��" + msg;
                    else
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=" + gwf.FID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='" + this.VirPath + "/WF/Do.aspx?ActionType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='" + this.VirPath + "/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��" + msg;
                }
            }
            else
            {
                // �����Ƿ���ʾ��
                DBAccess.RunSQL("UPDATE WF_ForwardWork SET IsRead=1 WHERE WORKID=" + this.WorkID + " AND FK_Node=" + wnPri.HisNode.NodeID);

                if (Web.WebUser.IsWap == false)
                {
                    if (this.HisFlow.FK_FlowSort != "00")
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=" + gwf.FID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��" + msg;
                    else
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=" + gwf.FID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��" + msg;
                }
                else
                {
                    return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=" + gwf.FID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��" + msg;
                }
            }
        }
        private string _AppType = null;
        /// <summary>
        /// ����Ŀ¼��·��
        /// </summary>
        public string AppType
        {
            get
            {
                if (_AppType == null)
                {
                    if (BP.Web.WebUser.IsWap)
                        _AppType = "/WF/WAP";
                    else
                        _AppType = "/WF";
                }
                return _AppType;
            }
        }
        private string _VirPath = null;
        /// <summary>
        /// ����Ŀ¼��·��
        /// </summary>
        public string VirPath
        {
            get
            {
                if (_VirPath == null)
                    _VirPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                return _VirPath;
            }
        }
        /// <summary>
        /// �����ƽ�
        /// </summary>
        /// <returns></returns>
        public string DoUnShift()
        {
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            WorkerLists wls = new WorkerLists();
            wls.Retrieve(WorkerListAttr.WorkID, this.WorkID, WorkerListAttr.FK_Node, gwf.FK_Node);
            if (wls.Count == 0)
                return "�ƽ�ʧ��û�е�ǰ�Ĺ�����";  

            Node nd = new Node(gwf.FK_Node);
            Work wk1 = nd.HisWork;
            wk1.OID = this.WorkID;
            wk1.Retrieve();

            // ��¼��־.
            WorkNode wn = new WorkNode(wk1, nd);
            wn.AddToTrack(ActionType.UnShift, WebUser.No, WebUser.Name, nd.NodeID, nd.Name, "�����ƽ�");

            if (wls.Count == 1)
            {
                WorkerList wl = (WorkerList)wls[0];
                wl.FK_Emp = WebUser.No;
                wl.FK_EmpText = WebUser.Name;
                wl.IsEnable = true;
                wl.IsPass = false;
                wl.Update();
                return "@�����ƽ��ɹ���<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>";
            }

            bool isHaveMe = false;
            foreach (WorkerList wl in wls)
            {
                if (wl.FK_Emp == WebUser.No)
                {
                    wl.FK_Emp = WebUser.No;
                    wl.FK_EmpText = WebUser.Name;
                    wl.IsEnable = true;
                    wl.IsPass = false;
                    wl.Update();
                    return "@�����ƽ��ɹ���<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>";
                }
            }

            WorkerList wk = (WorkerList)wls[0];
            WorkerList wkNew = new WorkerList();
            wkNew.Copy(wk);
            wkNew.FK_Emp = WebUser.No;
            wkNew.FK_EmpText = WebUser.Name;
            wkNew.IsEnable = true;
            wkNew.IsPass = false;
            wkNew.Insert();

            return "@�����ƽ��ɹ���<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>";
        }
        /// <summary>
        /// ִ�г���
        /// </summary>
        public string DoUnSend()
        {
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            // ���ͣ���Ľڵ��Ƿֺ�����
            Node nd = new Node(gwf.FK_Node);
            switch (nd.HisNodeWorkType)
            {
                case NodeWorkType.WorkFHL:
                    throw new Exception("�ֺ����㲻��������");
                case NodeWorkType.WorkFL:
                    /*�����˷�����, ���������1��δ������� 2���Ѿ��������.
                     *  �������������ķ�ʽ��ͬ�ġ�
                     *  δ�����ֱ��ͨ��������ģʽ�˻ء�
                     *  �Ѿ��������Ҫɱ�����е��Ѿ�����Ľ��̡�
                     */
                    DataTable mydt = DBAccess.RunSQLReturnTable("SELECT * FROM WF_GenerWorkerList WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.WorkID + "  AND IsPass=1");
                    if (mydt.Rows.Count >= 1)
                        return this.DoUnSendFeiLiu(gwf);
                    break;
                case NodeWorkType.StartWorkFL:
                    return this.DoUnSendFeiLiu(gwf);
                case NodeWorkType.WorkHL:
                    if (this.IsMainFlow)
                    {
                        /* �����ҵ����������һ�������㣬�����жϵ�ǰ�Ĳ���Ա�ǲ��Ƿ������ϵĹ�����Ա��*/
                        return this.DoUnSendHeiLiu_Main(gwf);
                    }
                    else
                    {
                        return this.DoUnSendSubFlow(gwf); //��������ʱ.
                        //return this.DoUnSendSubFlow(gwf); //��������ʱ.
                    }
                    break;
                case NodeWorkType.SubThreadWork:
                    break;
                default:
                    break;
            }

            if (nd.IsStartNode)
                return "�����ܳ������ͣ���Ϊ���ǿ�ʼ�ڵ㡣";

            WorkNode wn = this.GetCurrentWorkNode();
            WorkNode wnPri = wn.GetPreviousWorkNode();
            WorkerList wl = new WorkerList();
            int num = wl.Retrieve(WorkerListAttr.FK_Emp, Web.WebUser.No,
                WorkerListAttr.FK_Node, wnPri.HisNode.NodeID);

            if (num == 0)
                return "@" + this.ToE("WF6", "������ִ�г������ͣ���Ϊ��ǰ�������������͵ġ�");

            // ���ó�������ǰ�¼���
            string msg = nd.HisNDEvents.DoEventNode(EventListOfNode.UndoneBefore, wn.HisWork);

            #region ɾ����ǰ�ڵ����ݡ�
            // ɾ�������Ĺ����б�
            WorkerLists wls = new WorkerLists();
            wls.Delete(WorkerListAttr.WorkID, this.WorkID, WorkerListAttr.FK_Node, gwf.FK_Node.ToString());

            // ɾ��������Ϣ��
            wn.HisWork.Delete();

            // ɾ��������Ϣ��
            DBAccess.RunSQL("DELETE FROM Sys_FrmAttachmentDB WHERE FK_MapData='ND" + gwf.FK_Node + "' AND RefPKVal='" + this.WorkID + "'");
            #endregion ɾ����ǰ�ڵ����ݡ�

            // ����.
            gwf.FK_Node = wnPri.HisNode.NodeID;
            gwf.NodeName = wnPri.HisNode.Name;
            gwf.Update();
            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=0 WHERE WorkID=" + this.WorkID + " AND FK_Node=" + gwf.FK_Node);

            // ��¼��־..
            wnPri.AddToTrack(ActionType.Undo, WebUser.No, WebUser.Name, wnPri.HisNode.NodeID, wnPri.HisNode.Name, "��");

            // ɾ������.
            if (wn.HisNode.IsStartNode)
            {
                DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID=" + this.WorkID);
                DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE WorkID=" + this.WorkID);
                DBAccess.RunSQL("DELETE WF_GenerWorkerlist WHERE WorkID=" + this.WorkID + " AND FK_Node=" + nd.NodeID);
            }


            #region �ָ������켣������������졣
            if (wnPri.HisNode.IsStartNode == false)
            {
                WorkNode ppPri = wnPri.GetPreviousWorkNode();
                wl = new WorkerList();
                wl.Retrieve(WorkerListAttr.FK_Node, wnPri.HisNode.NodeID, WorkerListAttr.WorkID, this.WorkID);
                // BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.WorkID);
                RememberMe rm = new RememberMe();
                rm.Retrieve(RememberMeAttr.FK_Node, wnPri.HisNode.NodeID, RememberMeAttr.FK_Emp, ppPri.HisWork.Rec);

                string[] empStrs = rm.Objs.Split('@');
                foreach (string s in empStrs)
                {
                    if (s == "" || s == null)
                        continue;

                    if (s == wl.FK_Emp)
                        continue;
                    WorkerList wlN = new WorkerList();
                    wlN.Copy(wl);
                    wlN.FK_Emp = s;

                    WF.Port.WFEmp myEmp = new Port.WFEmp(s);
                    wlN.FK_EmpText = myEmp.Name;

                    wlN.Insert();
                }
            }
            #endregion �ָ������켣������������졣

            //���ó������ͺ��¼���
            msg += nd.HisNDEvents.DoEventNode(EventListOfNode.UndoneAfter, wn.HisWork);

            if (wnPri.HisNode.IsStartNode)
            {
                if (Web.WebUser.IsWap)
                {
                    if (wnPri.HisNode.HisFormType != FormType.SDKForm)
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='" + this.VirPath + "/WF/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='" + this.VirPath + "/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��" + msg;
                    else
                        return this.ToE("UnSendOK", "�����ɹ�.") + msg;
                }
                else
                {
                    if (this.HisFlow.FK_FlowSort != "00")
                    {
                        if (wnPri.HisNode.HisFormType != FormType.SDKForm)
                            return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='" + this.VirPath + "/WF/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='" + this.VirPath + "/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��" + msg;
                        else
                            return this.ToE("UnSendOK", "�����ɹ�.");
                    }
                    else
                    {
                        if (wnPri.HisNode.HisFormType != FormType.SDKForm)
                            return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='" + this.VirPath + this.AppType + "/Do.aspx?ActionType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='" + this.VirPath + "/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��" + msg;
                        else
                            return this.ToE("UnSendOK", "�����ɹ�.") + msg;
                    }
                }
            }
            else
            {
                // �����Ƿ���ʾ��
                DBAccess.RunSQL("UPDATE WF_ForwardWork SET IsRead=1 WHERE WORKID=" + this.WorkID + " AND FK_Node=" + wnPri.HisNode.NodeID);
                if (Web.WebUser.IsWap == false)
                {
                    if (this.HisFlow.FK_FlowSort != "00")
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��" + msg;
                    else
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��" + msg;
                }
                else
                {
                    return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��" + msg;
                }
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="gwf"></param>
        /// <returns></returns>
        private string DoUnSendFeiLiu(GenerWorkFlow gwf)
        {
            string sql = "SELECT FK_Node FROM WF_GenerWorkerList WHERE WorkID=" + this.WorkID + " AND FK_Emp='" + Web.WebUser.No + "' AND FK_Node='" + gwf.FK_Node + "'";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
                return "@" + this.ToE("WF6", "������ִ�г������ͣ���Ϊ��ǰ�������������͵ġ�");

            //�����¼�.
            Node nd = new Node(gwf.FK_Node);
            Work wk = nd.HisWork;
            wk.OID = gwf.WorkID;
            wk.RetrieveFromDBSources();
            string msg = nd.HisNDEvents.DoEventNode(EventListOfNode.UndoneBefore, wk);

            // ��¼��־..
            WorkNode wn = new WorkNode(wk, nd);
            wn.AddToTrack(ActionType.Undo, WebUser.No, WebUser.Name, gwf.FK_Node, gwf.NodeName, "");


            // ɾ���ֺ�����¼��
            if (nd.IsStartNode)
            {
                DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID=" + this.WorkID);
                DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE WorkID=" + this.WorkID);
                DBAccess.RunSQL("DELETE WF_GenerWorkerlist WHERE WorkID=" + this.WorkID + " AND FK_Node=" + nd.NodeID);
            }


            //ɾ����һ���ڵ�����ݡ�
            foreach (Node ndNext in nd.HisToNodes)
            {
                int i = DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.WorkID + " AND FK_Node=" + ndNext.NodeID);
                if (i == 0)
                    continue;

                // ɾ��������¼��
                Works wks = ndNext.HisWorks;
                wks.Delete(WorkerListAttr.FID, this.WorkID);

                // ɾ���Ѿ���������̡�
                DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.WorkID + " AND FK_Node=" + ndNext.NodeID);
            }

            //���õ�ǰ�ڵ㡣
            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=0 WHERE WorkID=" + this.WorkID + " AND FK_Node=" + gwf.FK_Node + " AND IsPass=1");
            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerFH SET FK_Node=" + gwf.FK_Node + " WHERE FID=" + this.WorkID);


            // ���õ�ǰ�ڵ��״̬.
            Node cNode = new Node(gwf.FK_Node);
            Work cWork = cNode.HisWork;
            cWork.OID = this.WorkID;
            cWork.Update(WorkAttr.NodeState, 0);

            msg += nd.HisNDEvents.DoEventNode(EventListOfNode.UndoneAfter, wk);

            if (cNode.IsStartNode)
            {
                if (Web.WebUser.IsWap)
                {
                    return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=0&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=DeleteFlow&WorkID=" + cWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='"+this.VirPath+"/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��" + msg;
                }
                else
                {
                    if (this.HisFlow.FK_FlowSort != "00")
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=0&FK_Node=" + gwf.FK_Node + "' ><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=DeleteFlow&WorkID=" + cWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='" + this.VirPath + "/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��" + msg;
                    else
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=0&FK_Node=" + gwf.FK_Node + "' ><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='Do.aspx?ActionType=DeleteFlow&WorkID=" + cWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='" + this.VirPath + "/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��" + msg;
                }
            }
            else
            {
                // �����Ƿ���ʾ��
                DBAccess.RunSQL("UPDATE WF_ForwardWork SET IsRead=1 WHERE WORKID=" + this.WorkID + " AND FK_Node=" + cNode.NodeID);
                if (Web.WebUser.IsWap == false)
                {
                    if (this.HisFlow.FK_FlowSort != "00")
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=0&FK_Node=" + gwf.FK_Node + "' ><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��" + msg;
                    else
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=0&FK_Node=" + gwf.FK_Node + "' ><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��" + msg;
                }
                else
                {
                    return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FID=0&FK_Node=" + gwf.FK_Node + "' ><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��" + msg;
                }
            }
        }
        /// <summary>
        /// ִ�г�������
        /// </summary>
        /// <param name="gwf"></param>
        /// <returns></returns>
        public string DoUnSendHeiLiu_Main(GenerWorkFlow gwf)
        {
            Node currNode = new Node(gwf.FK_Node);
            Node priFLNode = currNode.HisPriFLNode;
            WorkerList wl = new WorkerList();
            int i = wl.Retrieve(WorkerListAttr.FK_Node, priFLNode.NodeID, WorkerListAttr.FK_Emp, Web.WebUser.No);
            if (i == 0)
                return "@�������ѹ������͵���ǰ�ڵ��ϣ����������ܳ�����";

            WorkNode wn = this.GetCurrentWorkNode();
            WorkNode wnPri = new WorkNode(this.WorkID, priFLNode.NodeID);

            // ��¼��־..
            wnPri.AddToTrack(ActionType.Undo, WebUser.No, WebUser.Name, wnPri.HisNode.NodeID, wnPri.HisNode.Name, "��");

            WorkerLists wls = new WorkerLists();
            wls.Delete(WorkerListAttr.WorkID, this.WorkID, WorkerListAttr.FK_Node, gwf.FK_Node.ToString());

            wn.HisWork.Delete();
            gwf.FK_Node = wnPri.HisNode.NodeID;
            gwf.NodeName = wnPri.HisNode.Name;
            gwf.Update();

            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=0 WHERE WorkID=" + this.WorkID + " AND FK_Node=" + gwf.FK_Node);
            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerFH SET FK_Node=" + gwf.FK_Node + " WHERE FID=" + this.WorkID);

            ForwardWorks fws = new ForwardWorks();
            fws.Delete(ForwardWorkAttr.FK_Node, wn.HisNode.NodeID.ToString(),
                ForwardWorkAttr.WorkID, this.WorkID.ToString());

            //ReturnWorks rws = new ReturnWorks();
            //rws.Delete(ReturnWorkAttr.FK_Node, wn.HisNode.NodeID.ToString(),
            //    ReturnWorkAttr.WorkID, this.WorkID.ToString());

            #region �ָ������켣������������졣
            if (wnPri.HisNode.IsStartNode == false)
            {
                WorkNode ppPri = wnPri.GetPreviousWorkNode();
                wl = new WorkerList();
                wl.Retrieve(WorkerListAttr.FK_Node, wnPri.HisNode.NodeID, WorkerListAttr.WorkID, this.WorkID);
                // BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.WorkID);
                RememberMe rm = new RememberMe();
                rm.Retrieve(RememberMeAttr.FK_Node, wnPri.HisNode.NodeID, RememberMeAttr.FK_Emp, ppPri.HisWork.Rec);

                string[] empStrs = rm.Objs.Split('@');
                foreach (string s in empStrs)
                {
                    if (s == "" || s == null)
                        continue;

                    if (s == wl.FK_Emp)
                        continue;
                    WorkerList wlN = new WorkerList();
                    wlN.Copy(wl);
                    wlN.FK_Emp = s;

                    WF.Port.WFEmp myEmp = new Port.WFEmp(s);
                    wlN.FK_EmpText = myEmp.Name;

                    wlN.Insert();
                }
            }
            #endregion �ָ������켣������������졣

            // ɾ����ǰ�Ľڵ�����.
            wnPri.DeleteToNodesData(priFLNode.HisToNodes);

            if (wnPri.HisNode.IsStartNode)
            {
                if (Web.WebUser.IsWap)
                {
                    if (wnPri.HisNode.HisFormType != FormType.SDKForm)
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='" + this.VirPath + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='" + this.VirPath + "/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��";
                    else
                        return this.ToE("UnSendOK", "�����ɹ�.");
                }
                else
                {
                    if (this.HisFlow.FK_FlowSort != "00")
                    {
                        if (wnPri.HisNode.HisFormType != FormType.SDKForm)
                            return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='" + this.VirPath + this.AppType + "/MyFlowInfo" + Glo.FromPageType + ".aspx?DoType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='" + this.VirPath + "/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��";
                        else
                            return this.ToE("UnSendOK", "�����ɹ�.");
                    }
                    else
                    {
                        if (wnPri.HisNode.HisFormType != FormType.SDKForm)
                            return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A> , <a href='" + this.VirPath + this.AppType + "/Do.aspx?ActionType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='" + this.VirPath + "/Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "�������Ѿ����(ɾ����)") + "</a>��";
                        else
                            return this.ToE("UnSendOK", "�����ɹ�.");
                    }
                }
            }
            else
            {
                // �����Ƿ���ʾ��
                DBAccess.RunSQL("UPDATE WF_ForwardWork SET IsRead=1 WHERE WORKID=" + this.WorkID + " AND FK_Node=" + wnPri.HisNode.NodeID);
                if (Web.WebUser.IsWap == false)
                {
                    if (this.HisFlow.FK_FlowSort != "00")
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��";
                    else
                        return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��";
                }
                else
                {
                    return this.ToE("WN23", "@����ִ�гɹ��������Ե�����") + "<a href='" + this.VirPath + this.AppType + "/MyFlow" + Glo.FromPageType + ".aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "&FK_Node=" + gwf.FK_Node + "'><img src='" + this.VirPath + "/Images/Btn/Do.gif' border=0/>" + this.ToE("DoWork", "ִ�й���") + "</A>��";
                }
            }
        }
        #endregion
    }
    /// <summary>
    /// �������̼���.
    /// </summary>
    public class WorkFlows : CollectionBase
    {
        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="flow">���̱��</param>
        public WorkFlows(Flow flow)
        {
            StartWorks ens = (StartWorks)flow.HisStartNode.HisWorks;
            ens.RetrieveAll(10000);
            foreach (StartWork sw in ens)
            {
                this.Add(new WorkFlow(flow, sw.OID, sw.FID));
            }
        }
        /// <summary>
        /// �������̼���
        /// </summary>
        public WorkFlows()
        {
        }
        /// <summary>
        /// �������̼���
        /// </summary>
        /// <param name="flow">����</param>
        /// <param name="flowState">����ID</param> 
        public WorkFlows(Flow flow, int flowState)
        {
            StartWorks ens = (StartWorks)flow.HisStartNode.HisWorks;
            QueryObject qo = new QueryObject(ens);
            qo.AddWhere(StartWorkAttr.WFState, flowState);
            qo.DoQuery();
            foreach (StartWork sw in ens)
            {
                this.Add(new WorkFlow(flow, sw.OID, sw.FID));
            }
        }

        #endregion

        #region ��ѯ����
        /// <summary>
        /// GetNotCompleteNode
        /// </summary>
        /// <param name="flowNo">���̱��</param>
        /// <returns>StartWorks</returns>
        public static StartWorks GetNotCompleteWork(string flowNo)
        {
            Flow flow = new Flow(flowNo);
            StartWorks ens = (StartWorks)flow.HisStartNode.HisWorks;
            QueryObject qo = new QueryObject(ens);
            qo.AddWhere(StartWorkAttr.WFState, "!=", 1);
            qo.DoQuery();
            return ens;

            /*
            foreach(StartWork sw in ens)
            {
                ens.AddEntity( new WorkFlow( flow, sw.OID) ) ; 
            }
            */
        }
        #endregion

        #region ����
        /// <summary>
        /// ����һ����������
        /// </summary>
        /// <param name="wn">��������</param>
        public void Add(WorkFlow wn)
        {
            this.InnerList.Add(wn);
        }
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public WorkFlow this[int index]
        {
            get
            {
                return (WorkFlow)this.InnerList[index];
            }
        }
        #endregion

        #region ���ڵ��ȵ��Զ�����
        /// <summary>
        /// ������ڵ㡣
        /// ���ڵ�Ĳ����������û��Ƿ��Ĳ���������ϵͳ���ִ洢���ϣ���ɵ������еĵ�ǰ�����ڵ�û�й�����Ա���Ӷ�����������������ȥ��
        /// ������ڵ㣬���ǰ����Ƿŵ����ڵ㹤���������档
        /// </summary>
        /// <returns></returns>
        public static string ClearBadWorkNode()
        {
            string infoMsg = "������ڵ����Ϣ��";
            string errMsg = "������ڵ�Ĵ�����Ϣ��";
            return infoMsg + errMsg;
        }
        #endregion
    }
}
