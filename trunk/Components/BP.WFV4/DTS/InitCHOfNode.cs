using System;
using System.Collections.Generic;
using System;
using System.Data;
using BP.DA;
using System.Collections;
using BP.En.Base;
using BP.WF;
using BP.Port;
using BP.En;
using BP.DTS;
using BP.Tax;

namespace BP.WF.DTS
{
    /// <summary>
    /// 流程时效考核
    /// </summary>
    public class InitCHOfNode : DataIOEn
    {
        /// <summary>
        /// 流程时效考核
        /// </summary>
        public InitCHOfNode()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "节点时效考核(1,汇总节点工作信息到chofNode．2,对预期的工作追加责任人．3,同步节点状态. 4, 为加工程序准备数据。)这里可以手工执行它．";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
            this.Note = "每个月份首先要执行的调度，其次是发起执法考核任务的调度。";
        }
        /// <summary>
        /// 流程时效考核
        /// </summary>
        public override void Do()
        {
            try
            {
                Log.DefaultLogWriteLineInfo("* 开始执行节点数据同步，为执法考核准备数据。 ");

                //DBAccess.RunSQL("delete dszf.zf_dtslog");

                // 执行数据的同步与调度。
                DBAccess.RunSP("dswf.DTS_NodeDate2CHofNode");

                this.InitOneToMore();

                DateTime dt = DateTime.Now;
                dt.AddMonths(-1);

                DBAccess.RunSP("dszf.heard", "ny", dt.ToString("yyyy-MM"));

                Log.DefaultLogWriteLineInfo("* 为执法考核准备数据执行完成。");
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLineError("* 执行错误：" + ex.Message);
                throw ex;
            }
        }
        public void InitOneToMore()
        {
            //DoDelete();
            // return "";

            string noInsql = "SELECT FROMND||','||TOND  FROM DSZF.ZF_RATE WHERE FROMND NOT LIKE '%QJC%'";
            // string noInsql = "SELECT FROMND||'@'||TOND FROM DSZF.ZF_RATE";
            DataTable dt_Rate = DBAccess.RunSQLReturnTable(noInsql);
            string zfrates = "";
            foreach (DataRow dr in dt_Rate.Rows)
            {
                zfrates += dr[0].ToString();
            }

            noInsql = "select nodeid from DSZF.Zf_Dot WHERE NODEID NOT LIKE '%QJC%'";
            dt_Rate = DBAccess.RunSQLReturnTable(noInsql);
            foreach (DataRow dr in dt_Rate.Rows)
            {
                zfrates += dr[0].ToString() + ",";
            }

            string sql = "";

            #region /* 查询出还没做完但是逾期的情况 */
            sql = "SELECT * FROM (SELECT WorkID, FK_Node, EMPS, COUNT(*) AS NUM FROM WF_CHOFNODE WHERE FK_NODE IN (SELECT NODEID FROM WF_NODE) AND NodeState=0 AND SUBSTR(SDT,0,11) < '" + DataType.CurrentData + "' AND LENGTH(EMPS) > 10 GROUP BY WORKID,FK_NODE,EMPS) WHERE NUM >1";
            DataTable dt_no = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr_no in dt_no.Rows)
            {
                string node = dr_no["FK_Node"].ToString();

                if (IsInit(zfrates, node))
                    continue; /*如果存在里面*/

                string myemps = dr_no["Emps"].ToString();
                string WorkID = dr_no["WorkID"].ToString();
                int fk_node;
                try
                {
                    fk_node = int.Parse(node);
                }
                catch
                {
                    continue;
                }

                // 找到其中的一个Entity.
                CHOfNode cn = new CHOfNode();
                int i = cn.Retrieve(CHOfNodeAttr.WorkId, WorkID, CHOfNodeAttr.FK_Node, fk_node);
                if (i == 0)
                    continue;

                // throw new Exception("不可能出现的情况。");

                Node nd = new Node();
                nd.NodeID = fk_node;
                nd.RetrieveFromDBSources();

                //NodeExt nd = new NodeExt(fk_node);

                string[] emps = myemps.Split(',');
                foreach (string emp in emps)
                {
                    if (emp == "" || emp == null)
                        continue;

                    if (emp == cn.FK_Emp)
                    {
                        /*如果是已经存在人记录。*/
                        continue;
                    }

                    CHOfNode mycn = new CHOfNode();
                    mycn.Copy(cn);
                    mycn.NodeState = 0;
                    mycn.FK_Emp = emp;
                    mycn.CDT = "无";
                    mycn.CentOfAdd = 0;
                    mycn.CentOfCut = nd.DeductCent;
                    mycn.Cent = 0;
                    mycn.IsMyDeal = false;
                    mycn.Save();
                }
                /*执行扣分*/
                DBAccess.RunSQL("UPDATE WF_CHofNode SET CentOfCut=" + nd.DeductCent + " WHERE FK_Node=" + nd.NodeID + " and workid='" + WorkID + "'");
            }
            #endregion

            #region /* 查询出已经做完但是逾期的情况 */ 
            sql = "SELECT * FROM (SELECT WorkID, FK_Node, EMPS, COUNT(*) AS NUM FROM WF_CHOFNODE WHERE NodeState=1 AND SUBSTR(CDT,0,11) >= SUBSTR(SDT,0,11 ) GROUP BY WORKID,FK_NODE,EMPS) WHERE NUM>1";
            DataTable dt_yes = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr_yes in dt_yes.Rows)
            {
                
                string node = dr_yes["FK_Node"].ToString();
                if (IsInit(zfrates, node))
                    continue; /*如果存在里面*/

                if (zfrates.IndexOf(node) >= 0)
                    continue;

                string myemps = dr_yes["Emps"].ToString();
                string WorkID = dr_yes["WorkID"].ToString();
                int fk_node = int.Parse(dr_yes["FK_Node"].ToString());


                // 找到其中的一个Entity.
                CHOfNode cn = new CHOfNode();
                int i = cn.Retrieve(CHOfNodeAttr.WorkId, WorkID, CHOfNodeAttr.FK_Node, fk_node);
                if (i == 0)
                    continue;

                Node nd = new Node(fk_node);

                string[] emps = myemps.Split(',');
                foreach (string emp in emps)
                {
                    if (emp == "" || emp == null)
                        continue;

                    if (emp == cn.FK_Emp)
                    {
                        /*如果是已经存在人记录。*/
                        continue;
                    }

                    CHOfNode mycn = new CHOfNode();
                    mycn.Copy(cn);
                    mycn.FK_Emp = emp;
                    //如果在逾期的情况下已经做完了此工作，那么程序跳出
                    int a = mycn.Retrieve(CHOfNodeAttr.WorkId, WorkID, CHOfNodeAttr.FK_Node, fk_node, CHOfNodeAttr.FK_Emp, mycn.FK_Emp);
                    if (a == 1)
                        continue;

                    mycn.CDT = "无";
                    mycn.NodeState = 0;
                    mycn.CentOfAdd = 0;
                    mycn.CentOfCut = nd.DeductCent;
                    mycn.Cent = 0;
                    mycn.Save();
                }
            }
            #endregion
        }

        /// <summary>
        /// 是否在里面
        /// </summary>
        /// <param name="zfrates"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsInit(string zfrates, string node)
        {
            if (zfrates.IndexOf(node + "@") != -1)
                return true;

            if (zfrates.IndexOf("," + node + ",") != -1)
                return true;

            if (zfrates.IndexOf(node + ";") != -1)
                return true;

            if (zfrates.IndexOf("@" + node) != -1)
                return true;

            return false;
        }
        /// <summary>
        /// 删除多分解出来的数据
        /// </summary>
        public void DoDelete()
        {
            string sql = "SELECT * FROM (SELECT WorkID, FK_Node, EMPS, COUNT(*) AS NUM FROM DSWF.WF_CHOFNODE WHERE LENGTH(EMPS) > 10 GROUP BY WORKID, FK_NODE, EMPS ) WHERE NUM>1 ";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);

            string noInsql = "SELECT FROMND||','||TOND  FROM DSZF.ZF_RATE WHERE FROMND NOT LIKE '%QJC%'";
            DataTable dt_Rate = DBAccess.RunSQLReturnTable(noInsql);
            string zfrates = "";
            foreach (DataRow dr in dt_Rate.Rows)
                zfrates += dr[0].ToString();

            noInsql = "select nodeid from DSZF.ZF_Dot WHERE NODEID NOT LIKE '%QJC%'";
            dt_Rate = DBAccess.RunSQLReturnTable(noInsql);
            foreach (DataRow dr in dt_Rate.Rows)
            {
                zfrates += dr[0].ToString() + ",";
            }

            foreach (DataRow dr in dt.Rows)
            {
                int workid = int.Parse(dr["WorkID"].ToString());
                string node = dr["FK_Node"].ToString();

                if (IsInit(zfrates, node) == false)
                    continue;

                int fk_node = int.Parse(node);
                Node nd = new Node(fk_node);
                Work wk = nd.HisWork;

                if (wk.IsCheckWork)
                {
                    wk.SetValByKey(CheckWorkAttr.NodeID, fk_node);
                }

                wk.OID = workid;
                try
                {
                    wk.Retrieve();
                }
                catch
                {
                    continue;
                }

                //sql = "SELECT * FROM  DSWF.WF_CHOFNODE WHERE FK_NODE='"+node+"' AND WORKID='"+workid+"' AND FK_EMP='"+wk.Recorder+"'";
                //if (

                sql = "DELETE DSWF.WF_CHOFNODE WHERE FK_NODE='" + node + "' AND WORKID='" + workid + "' AND FK_EMP!='" + wk.Recorder + "' ";
                // Log.DebugWriteInfo(sql);
                DBAccess.RunSQL(sql);
            }
        }
    }
}
