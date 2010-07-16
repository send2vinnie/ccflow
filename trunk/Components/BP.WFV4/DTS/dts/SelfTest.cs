using System;
using System.Data;
using BP.DA ; 
using System.Collections;
using BP.En.Base;
using BP.WF;
using BP.Port ; 
using BP.En;
using BP.DTS;
using BP.Tax;

namespace BP.WF.DTS
{
    public class SelfTest : DataIOEn
    {
        /// <summary>
        /// 流程时效考核
        /// </summary>
        public SelfTest()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "流程自动检测";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        /// <summary>
        /// 创建文书目录
        /// </summary>
        public override void Do()
        {
            string msg = "";
            string info = "";
            // --------------- 微机评税自动检测信息
            try
            {
                msg += "==========================================================================";
                info = this.CheckCT();
                msg += "@<b>微机评税自动检测信息:</b>@" + info;


                Log.DefaultLogWriteLineInfo("微机评税体检信息：" + info);
            }
            catch (Exception ex1)
            {
                msg += "\n@微机评税自动检测错误信息:@" + ex1.Message;
                Log.DefaultLogWriteLineError("微机评税自动检测信息：" + ex1.Message);
            }

            // --------------- 流程丢失自动检测
            try
            {
                msg += "==========================================================================";
                info = this.LostFlowTest();

                msg += "@<B>流程丢失自动检测<B>@" + info;

                Log.DefaultLogWriteLineError("流程丢失自动检测：完成，" + info);
            }
            catch (Exception ex)
            {
                msg += "\n@流程丢失自动检测出现错误:" + ex.Message;

                Log.DefaultLogWriteLineError("流程丢失自动检测出现错误：" + ex.Message);
            }


            // --------------- 流程自动检测信息
            try
            {
                msg += "==========================================================================";
                info = this.FlowTest();
                msg += "@<B>流程自动检测信息</b>@" + info;
            }
            catch (Exception ex)
            {
                msg += "\n@在流程自动检测时间出错:" + ex.Message;
                Log.DefaultLogWriteLineError("流程丢失自动检测出现错误：" + ex.Message);
            }

            //  Log.DefaultLogWriteLineInfo( msg.Replace("@", "\n@"));

            if (BP.SystemConfig.IsBSsystem)
                PubClass.Alert(msg);
        }
        /// <summary>
        /// 不能退回自动检测， 当前节点不在本流程中。
        /// </summary>
        /// <returns></returns>
        public string CannotbackTest()
        {
            GenerWorkFlows ens = new GenerWorkFlows();
            ens.RetrieveAll(100000);
            foreach (GenerWorkFlow en in ens)
            {
                


            }

            return "";

        }
        /// <summary>
        /// 流程丢失自动检测
        /// </summary>
        /// <returns></returns>
        public string LostFlowTest()
        {
            int num = 0;
            string msg = "";
            string sql = "UPDATE dswf.WF_GenerWorkerList SET dswf.WF_GenerWorkerList.IsEnable=1 WHERE WORKID IN (SELECT WORKID FROM ( SELECT WORKID, COUNT(*) AS NUM FROM dswf.WF_GenerWorkerList WHERE dswf.WF_GenerWorkerList.Isenable=1 AND  WORKID IN (SELECT WORKID FROM dswf.WF_GENERWORKFLOW WHERE dswf.WF_GENERWORKFLOW.FK_CURRENTNODE = dswf.WF_GenerWorkerList.Fk_Node)    GROUP BY WORKID)  WHERE  NUM =0 ) AND WF_GenerWorkerList.Fk_Node IN ( SELECT WF_GENERWORKFLOW.FK_CURRENTNODE FROM dswf.WF_GENERWORKFLOW WHERE dswf.WF_GenerWorkerList.Workid = dswf.WF_GENERWORKFLOW.WORKID )";

            num = DBAccess.RunSQL(sql);
            msg += "@丢失流程并且自动恢复个数 " + num + "条。";


            // 查询出来，在注册表中没有完成的流程，但是也没有流程运行表中。
            sql = "SELECT WorkID FROM WF_CHOFFLOW WHERE WORKID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW) AND WFSTATE=0";
            BP.WF.CHOfFlows fls = new CHOfFlows();
            fls.RetrieveInSQL(BP.WF.CHOfFlowAttr.WorkId, sql);
            if (fls.Count == 0)
                return  msg;

            msg += "@流程注册表中有" + fls.Count + "条不正常数据，查询语句：" + sql + "。@系统会自动修复它们，修复结果如下：";
            foreach (CHOfFlow fl in fls)
            {
                msg += "@对" + fl.FK_Taxpayer + fl.TaxpayerName + "流程:" + fl.FK_Flow + "日期:" + fl.RDT + "，workid=" + fl.WorkId + "开始节点表中不存在它，现在准备删除。";
                Flow flow = new Flow(fl.FK_Flow);
                StartWork wk = flow.HisStartNode.HisWork as StartWork;
                wk.OID = fl.WorkId;
                if (wk.RetrieveFromDBSources() == 0)
                {
                    fl.Delete(); //如果开始节点中不存在这条记录，就删除它。
                    // 删除其它节点上的数据，但是这中不太可能存在。
                    Nodes nds = flow.HisNodes;
                    foreach (Node nd in nds)
                    {
                        try
                        {
                            Work mywk = nd.HisWork;
                            mywk.OID = fl.WorkId;
                            if (mywk.IsCheckWork)
                                mywk.SetValByKey(CheckWorkAttr.NodeID, nd.NodeID);
                            mywk.Delete();
                        }
                        catch
                        {

                        }
                    }
                    msg += "@开始节点表中不存在它，现在已经删除。";
                }
                else
                {
                    if (wk.WFState == WFState.Complete)
                    {
                        /*如果已经完成*/
                        fl.WFState = (int)WFState.Complete;
                        fl.DirectUpdate();
                        msg += "@由于节点表中的状态是1，流程注册表中的状态不为1，自动更正。";
                    }
                }
            }

            Log.DefaultLogWriteLineInfo(msg);
            return msg;
        }
        public string CheckCT()
        {
            string msg = "";
            string sql = "";

            // 节点停在



            sql = " DELETE dswf.WF_GENERWORKFLOW WHERE WORKID IN (SELECT WorkID FROM  DSCT.CT_HD WHERE NO NOT IN ( select qybm from dsbm.djsw ) )";
            int i = DBAccess.RunSQL(sql);
            msg += "@共有(" + i + ")户注销户，在微机评税系统中被删除。";

            sql = " DELETE dswf.wf_generworkerlist WHERE WORKID IN (SELECT WorkID FROM  DSCT.CT_HD WHERE NO NOT IN (SELECT qybm FROM dsbm.djsw) )";
            DBAccess.RunSQL(sql);

            sql = "DELETE DSCT.CT_HD WHERE NO NOT IN (SELECT qybm FROM dsbm.djsw )";
            DBAccess.RunSQL(sql);

            sql = "DELETE FROM DSWF.WF_GENERWORKFLOW WHERE FK_FLOW='220' AND WORKID NOT IN ( SELECT WORKID FROM  DSCT.CT_HD WHERE (STATEOFHD IN ('2','3','4','1') ) )";
            DBAccess.RunSQL(sql);

            try
            {
                // 同步税务管理员，纳税人名称信息。 防止税务管理员变化，造成这里没有变化。
                sql = "update dsct.ct_hd SET (FK_Emp, Name )  = ( select sgy,qymc from dsbm.djsw where dsbm.djsw.qybm =dsct.ct_hd.no)  ";
                DBAccess.RunSQL(sql);
            }
            catch (Exception ex)
            {
                msg += "@请确定 dsbm.djsw 中的纳税人编号生成 是否重复. 检查sql = select * from ( select no, count(*) n from dswf.ds_taxpayer group by no) WHERE  N>1 ";
            }
            Log.DefaultLogWriteLineInfo(msg);
            return msg;
        }
        /// <summary>
        /// 流程已经完成但是是流程注册表中不存在
        /// </summary>
        public string FlowTestV2()
        {
            string msg = "";

            Flows fls = new Flows();
            fls.RetrieveAll();
            int i = 1;
            foreach (Flow fl in fls)
            {
                BP.WF.Node nd = new BP.WF.Node();
                if (fl.No.IndexOf("XN") != -1)
                    continue;

                try
                {
                    nd = fl.HisStartNode;
                }
                catch
                {
                    continue;
                }

                string sql = "SELECT OID FROM " + nd.PTable + " WHERE WFState=1 AND OID NOT IN (SELECT WORKID FROM WF_CHOFFLOW) ";
                DataTable dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                    continue;

                StartWorks sws = fl.HisStartNode.HisWorks as StartWorks;
                sws.RetrieveInSQL(StartWorkAttr.OID, sql);
                foreach (StartWork sw in sws)
                {
                    CHOfFlow cf = new CHOfFlow();
                    cf.Copy(sw);
                    cf.WorkId = sw.OID;
                    cf.FK_Flow = fl.No;
                    cf.FK_Emp = sw.Recorder;
                    cf.WFState = (int)WFState.Complete;
                    cf.FK_NY = sw.Record_FK_NY;
                    cf.Insert();
                    msg += "@流程注册表:" + fl.Name + " WorkID=" + sw.OID + " 已经修复。";
                }
            }
            return msg;
        }
        /// <summary>
        /// 流程自动检测
        /// </summary>
        public string FlowTest()
        {
            string msg = "";

            Flows fls = new Flows();
            fls.RetrieveAll();

            int i = 1;
            foreach (Flow fl in fls)
            {
                // StartWork sw = fl.HisStartNode.HisWorks;
                BP.WF.Node nd = new BP.WF.Node();
                if (fl.No.IndexOf("XN") != -1)
                    continue;

                try
                {
                    nd = fl.HisStartNode;
                }
                catch
                {
                    continue;
                }

                string delsql = "DELETE  " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW ) ";
                DBAccess.RunSQL(delsql);


                //string sql = "SELECT COUNT(*) FROM " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW ) ";
                //if (DBAccess.RunSQLReturnValInt(sql) == 0)
                //    continue;

                //string sqlIn = "SELECT OID FROM " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW )  ";
                //string delsql = "DELETE  " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW ) ";


                //BP.WF.Nodes nds = fl.HisNodes;
                //foreach (BP.WF.Node mynd in nds)
                //{
                //    if (mynd.IsStartNode)
                //        continue;
                //    sql = "DELETE " + mynd.PTable + " WHERE OID IN ( " + sqlIn + " )";
                //    DBAccess.RunSQL(sql); // 删除其它节点的数据.
                //}
                //// 删除开始点的数据。
                //DBAccess.RunSQL(delsql);
                //msg += "@对流程:" + fl.Name + "删除方式修复" + delsql;
            }

            Log.DefaultLogWriteLineInfo(msg);
            return msg;
        }

        public string FlowTest_bak1214()
        {
            string msg = "";

            Flows fls = new Flows();
            fls.RetrieveAll();

            int i = 1;
            foreach (Flow fl in fls)
            {
                // StartWork sw = fl.HisStartNode.HisWorks;
                BP.WF.Node nd = new BP.WF.Node();
                if (fl.No.IndexOf("XN") != -1)
                    continue;

                try
                {
                    nd = fl.HisStartNode;
                }
                catch
                {
                    continue;
                }

                string sql = "SELECT COUNT(*) FROM " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW ) ";
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                    continue;


                string sqlIn = "SELECT OID FROM " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW )  ";
                string delsql = "DELETE  " + nd.PTable + " WHERE WFSTATE=0 AND OID NOT IN (SELECT WORKID FROM WF_GENERWORKFLOW ) ";


                BP.WF.Nodes nds = fl.HisNodes;
                foreach (BP.WF.Node mynd in nds)
                {
                    if (mynd.IsStartNode)
                        continue;
                    sql = "DELETE " + mynd.PTable + " WHERE OID IN ( " + sqlIn + " )";
                    DBAccess.RunSQL(sql); // 删除其它节点的数据.
                }
                // 删除开始点的数据。
                DBAccess.RunSQL(delsql);
                msg += "@对流程:" + fl.Name + "删除方式修复" + delsql;
            }

            return msg;
        }
    }
}
