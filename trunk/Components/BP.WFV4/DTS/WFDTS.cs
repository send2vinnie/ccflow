using System;
using System.Data;
using BP.DA ; 
using System.Collections;
using BP.En;
using BP.WF;
using BP.Port ; 
using BP.En;
using BP.DTS;

namespace BP.WF.DTS
{
    public class CheckNodes : DataIOEn
    {
        /// <summary>
        /// 调度人员,岗位,部门
        /// </summary>
        public CheckNodes()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "修复节点信息";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {

            MDCheck md = new MDCheck();
            md.Do();

            //执行调度部门。
            //BP.Port.DTS.GenerDept gd = new BP.Port.DTS.GenerDept();
            //gd.Do();

            // 调度人员信息。
            // Emp emp = new Emp(Web.WebUser.No);
            // emp.DoDTSEmpDeptStation();
        }
    }


    public class UserPort : DataIOEn2
    {
        /// <summary>
        /// 调度人员,岗位,部门
        /// </summary>
        public UserPort()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "生成流程部门(运行在系统第一次安装时或者部门变化时)";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {

            //执行调度部门。
            //BP.Port.DTS.GenerDept gd = new BP.Port.DTS.GenerDept();
            //gd.Do();

            // 调度人员信息。
            // Emp emp = new Emp(Web.WebUser.No);
            // emp.DoDTSEmpDeptStation();
        }
    }
    public class AddEmpLeng : DataIOEn2
    {
        public AddEmpLeng()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "为操作员编号长度生级";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            string sql = "";
            string sql2 = "";
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            foreach (Node nd in nds)
            {
                sql += "\n UPDATE " + nd.PTable + " SET EMPS=','||EMPS  WHERE substr(emps,0,1)='0';";
                sql2 += "\n UPDATE " + nd.PTable + " SET EMPS=REPLACE( EMPS, ',0',',010' )  WHERE EMPS NOT LIKE '%,010%';";
            }

            Log.DebugWriteInfo(sql);
            Log.DebugWriteInfo("===========================" + sql2);
        }

        public void Do1()
        {
            string sql = "";
            string sql2 = "";

            ArrayList al = ClassFactory.GetObjects("BP.En.Entity");
            foreach (object obj in al)
            {
                Entity en = obj as Entity;
                Map map = en.EnMap;

                try
                {
                    if (map.IsView)
                        continue;
                }
                catch
                {
                }

                //en.CheckPhysicsTable

                string table = en.EnMap.PhysicsTable;
                foreach (Attr attr in map.Attrs)
                {
                    if (attr.Key.IndexOf("Text") != -1)
                        continue;

                    if (attr.Key == WorkAttr.Rec || attr.Key == "FK_Emp" || attr.UIBindKey == "BP.Port.Emps")
                    {
                        sql += "\n update " + table + " set " + attr.Key + "='01'||" + attr.Key + " where length(" + attr.Key + ")=6;";
                    }
                    else if (attr.Key == "Checker")
                    {
                        sql2 += "\n update " + table + " set " + attr.Key + "='01'||" + attr.Key + " where length(" + attr.Key + ")=6;";
                    }
                }
            }
            Log.DebugWriteInfo(sql);
            Log.DebugWriteInfo("===========================" + sql2);
        }
    }
    public class DelWorkFlowData : DataIOEn
    {
        public DelWorkFlowData()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "<font color=red><b>清除流程数据</b></font>";
            
            //this.HisRunTimeType = RunTimeType.UnName;
            //this.FromDBUrl = DBUrlType.AppCenterDSN;
            //this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            if (BP.Web.WebUser.No != "admin")
            {
                throw new Exception("非法用户。");
            }

            DA.DBAccess.RunSQL("delete WF_CHOfNode");
            DA.DBAccess.RunSQL("delete WF_CHOfFlow");
            DA.DBAccess.RunSQL("delete WF_Book");
            DA.DBAccess.RunSQL("delete WF_GENERWORKERLIST");
            DA.DBAccess.RunSQL("delete WF_GENERWORKFLOW");
            DA.DBAccess.RunSQL("delete WF_WORKLIST");
            DA.DBAccess.RunSQL("delete WF_ReturnWork");
            DA.DBAccess.RunSQL("delete WF_GECheckStand");
            DA.DBAccess.RunSQL("delete WF_GECheckMul");
            DA.DBAccess.RunSQL("delete WF_ForwardWork");
            DA.DBAccess.RunSQL("delete WF_SelectAccper");

            Nodes nds = new Nodes();
            nds.RetrieveAll();

            string msg = "";
            foreach (Node nd in nds)
            {
                if (nd.IsCheckNode)
                    continue;
                Work wk =  null;

                try
                {
                    wk = nd.HisWork;
                    DA.DBAccess.RunSQL("DELETE " + wk.EnMap.PhysicsTable);
                }
                catch (Exception ex)
                {
                    wk.CheckPhysicsTable();
                    msg += "@" + ex.Message;
                }
            }

            if (msg != "")
                throw new Exception(msg);
        }
    }
    public class InitBookDir : DataIOEn
    {
        /// <summary>
        /// 流程时效考核
        /// </summary>
        public InitBookDir()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "<font color=green><b>创建文书目录(运行在每次更改文书文号或每年一天)</b></font>";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        /// <summary>
        /// 创建文书目录
        /// </summary>
        public override void Do()
        {

            Depts Depts = new Depts();
            QueryObject qo = new QueryObject(Depts);
      //      qo.AddWhere("Grade", " < ", 4);
            qo.DoQuery();

            BookTemplates funcs = new BookTemplates();
            funcs.RetrieveAll();


            string path = BP.WF.Glo.FlowFile + "\\Book\\" ;
            string year = DateTime.Now.Year.ToString();

            if (System.IO.Directory.Exists(path) == false)
                System.IO.Directory.CreateDirectory(path);

            if (System.IO.Directory.Exists(path + "\\\\" + year) == false)
                System.IO.Directory.CreateDirectory(path + "\\\\" + year);


            foreach (Dept Dept in Depts)
            {
                if (System.IO.Directory.Exists(path + "\\\\" + year + "\\\\" + Dept.No) == false)
                    System.IO.Directory.CreateDirectory(path + "\\\\" + year + "\\\\" + Dept.No);

                foreach (BookTemplate func in funcs)
                {
                    if (System.IO.Directory.Exists(path + "\\\\" + year + "\\\\" + Dept.No + "\\\\" + func.No) == false)
                        System.IO.Directory.CreateDirectory(path + "\\\\" + year + "\\\\" + Dept.No + "\\\\" + func.No);
                }
            }
        }
    }

    public class OutputSQLs : DataIOEn
    {
        /// <summary>
        /// 流程时效考核
        /// </summary>
        public OutputSQLs()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "OutputSQLs for produces DTSCHofNode";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            string sql = this.GenerSqls();
            PubClass.ResponseWriteBlueMsg(sql.Replace("\n", "<BR>"));
        }
        public string GenerSqls()
        {
            Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + "开始调度考核信息:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            string infoMsg = "", errMsg = "";

            Nodes nds = new Nodes();
            nds.RetrieveAll();

            string fromDateTime = DateTime.Now.Year + "-01-01";
            fromDateTime = "2004-01-01 00:00";
            //string fromDateTime=DateTime.Now.Year+"-01-01 00:00";
            //string fromDateTime=DateTime.Now.Year+"-01-01 00:00";
            string insertSql = "";
            string delSQL = "";
            string updateSQL = "";

            string sqls = "";
            int i = 0;
            foreach (Node nd in nds)
            {
                if (nd.IsPCNode)  /* 如果是计算机节点.*/
                    continue;

                if (nd.IsCheckNode)
                    continue;

                i++;

                Map map = nd.HisWork.EnMap;
                delSQL = "\n DELETE " + map.PhysicsTable + " WHERE  OID  NOT IN (SELECT WORKID FROM wf_generworkflow ) AND NODESTATE=0 ";

                if (map.Attrs.Contains("FK_Taxpayer") && map.Attrs.Contains("TaxpayerName"))
                {
                    insertSql = "INSERT INTO WF_CHOfNode (FK_Node,FK_Flow,WorkID, NodeState,FK_Emp,Emps,RDT,CDT,FK_Taxpayer,TaxpayerName )"
                        + " "
                        + "  SELECT " + nd.NodeID + " as FK_Node, '" + nd.FK_Flow + "' as FK_Flow, OID as WorkID, NodeState, Rec,Emps,RDT,CDT,FK_Taxpayer, TaxpayerName "
                        + "  FROM " + map.PhysicsTable
                        + "  WHERE  OID NOT IN ( SELECT WorkID FROM WF_CHOfNode WHERE FK_Node=" + nd.NodeID + " ) AND Rec IS NOT NULL ";
                }
                else
                {
                    insertSql = "INSERT INTO WF_CHOfNode (FK_Node,FK_Flow,WorkID,NodeState,FK_Emp,Emps,RDT,CDT  ) "
                + " "
                + "  SELECT " + nd.NodeID + " as FK_Node, '" + nd.FK_Flow + "' as FK_Flow, OID as WorkID, NodeState, Rec,Emps,RDT,CDT "
                + "  FROM " + nd.HisWork.EnMap.PhysicsTable
                + "  WHERE  OID NOT IN ( SELECT WorkID FROM WF_CHOfNode WHERE FK_Node=" + nd.NodeID + " )  AND Rec IS NOT NULL ";
                }

                // 更新状态的sql.
                updateSQL = " UPDATE WF_CHOfNode  SET (WF_CHOfNode.NODESTATE,RDT,CDT) = ( SELECT NODESTATE, RDT,CDT FROM " + nd.PTable + "  WHERE WF_CHOFNODE.WorkID=" + nd.PTable + ".OID ) WHERE WF_CHOfNode.FK_NODE='" + nd.NodeID + "'";

                sqls += "\n\n\n -- NO:" + i + "、" + nd.FK_Flow + nd.FlowName + " :  " + map.EnDesc + " \n" + delSQL + "; \n" + insertSql + "; \n" + updateSQL + ";";
            }

            Log.DefaultLogWriteLineInfo(sqls);
            return sqls;
        }
    }
    public class OutputSQLOfDeleteWork : DataIOEn
    {
        /// <summary>
        /// 流程时效考核
        /// </summary>
        public OutputSQLOfDeleteWork()
        {
            this.HisDoType = DoType.UnName;
            this.Title = "生成删除节点数据的sql.";
            this.HisRunTimeType = RunTimeType.UnName;
            this.FromDBUrl = DBUrlType.AppCenterDSN;
            this.ToDBUrl = DBUrlType.AppCenterDSN;
        }
        public override void Do()
        {
            string sql = this.GenerSqls();
            PubClass.ResponseWriteBlueMsg(sql.Replace("\n", "<BR>"));
        }
        public string GenerSqls()
        {
            Nodes nds = new Nodes();
            nds.RetrieveAll();
            string delSQL = "";
            foreach (Node nd in nds)
            {
                delSQL += "\n DELETE " + nd.PTable + "  ; ";
            }
            return delSQL;
        }
    }
	
    /// <summary>
    /// 流程中应用到的静态方法。
    /// </summary>
    public class WFDTS
    {
        
        /// <summary>
        /// 流程统计分析
        /// </summary>
        /// <param name="fromDateTime"></param>
        /// <returns></returns>
        public static string InitFlows(string fromDateTime)
        {
            return null; /* 好像这个不再应用它了。*/
            Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + " ################# Start 执行统计 #####################");
            //删除部门错误的流程
            //DBAccess.RunSQL("DELETE WF_BadWF WHERE BadFlag='FlowDeptBad'");
            fromDateTime = "2004-01-01 00:00";

            Flows fls = new Flows();
            fls.RetrieveAll();

            CHOfFlow fs = new CHOfFlow();
            foreach (Flow fl in fls)
            {
                Node nd = fl.HisStartNode;
                try
                {
                    string sql = "INSERT INTO WF_CHOfFlow SELECT OID WorkID, " + fl.No + " as FK_Flow, WFState, ltrim(rtrim(Title)) as Title,ltrim(rtrim(WFLog)) as WFLog, Rec as FK_Emp,"
                        + " RDT, CDT, 0 as SpanDays,'' FK_Dept,"
                        + "'' as FK_Dept,'' AS FK_NY,'' as FK_AP,'' AS FK_ND, '' AS FK_YF, Rec ,'' as FK_XJ, '' as FK_Station   "
                        + " FROM " + nd.HisWork.EnMap.PhysicsTable + " WHERE RDT>='" + fromDateTime + "' AND OID NOT IN ( SELECT WorkID FROM WF_CHOfFlow  )";
                    DBAccess.RunSQL(sql);
                }
                catch (Exception ex)
                {
                    throw new Exception(fl.Name + "   " + nd.Name + "" + ex.Message);
                }
            }
            DBAccess.RunSP("WF_UpdateCHOfFlow");
            Log.DefaultLogWriteLine(LogType.Info, Web.WebUser.Name + " End 执行统计调度");
            return "";
        }
 
       
    }

    
     
}
