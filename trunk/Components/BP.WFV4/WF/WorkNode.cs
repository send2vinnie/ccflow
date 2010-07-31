using System;
using BP.En;
using BP.DA;
using System.Collections;
using System.Data;
using BP.Port;
using BP.Web;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// WF 的摘要说明。
    /// 工作流.
    /// 这里包含了两个方面
    /// 工作的信息．
    /// 流程的信息．
    /// </summary>
    public class WorkNode
    {
        public string ToE(string s, string chName)
        {
            return BP.Sys.Language.GetValByUserLang(s, chName);
        }
        public string ToEP1(string s, string chName, string v)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(s, chName), v);
        }
        public string ToEP2(string s, string chName, string v, string v1)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(s, chName), v, v1);
        }
        public string ToEP3(string s, string chName, string v, string v1, string v2)
        {
            return string.Format(BP.Sys.Language.GetValByUserLang(s, chName), v, v1, v2);
        }

        #region 权限判断
        /// <summary>
        /// 判断一个人能不能对这个工作节点进行操作。
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        private bool IsCanOpenCurrentWorkNode(string empId)
        {
            NodeState stat = this.HisWork.NodeState;
            if (stat == NodeState.Init)
            {
                if (this.HisNode.IsStartNode)
                {
                    /*如果是开始工作节点，从工作岗位判断他有没有工作的权限。*/
                    return WorkFlow.IsCanDoWorkCheckByEmpStation(this.HisNode.NodeID, empId);
                }
                else
                {
                    /* 如果是初始化阶段,判断他的初始化节点 */
                    WorkerList wl = new WorkerList();
                    wl.WorkID = this.HisWork.OID;
                    wl.FK_Emp = empId;
                    wl.FK_Node = this.HisNode.NodeID;
                    return wl.IsExits;
                }
            }
            else
            {
                /* 如果是初始化阶段 */
                return false;
            }
        }
        #endregion

        //查询出每个节点表里的接收人集合（Emps）。
        public string GenerEmps(Node nd)
        {
            string str = "";
            foreach (WorkerList wl in this.HisWorkerLists)
                str = wl.FK_Emp + ",";

            if (this.HisWorkerLists.Count == 0)
            {
                try
                {
                    Log.DefaultLogWriteLineError("@" + this.ToE("ErrEmpNull", "产生人员集合为空") + "，WorkID=" + this.WorkID + "@FK_Flow=" + this.HisNode.FK_Flow);
                }
                catch
                {
                }
            }
            return str;
        }
        /// <summary>
        ///  把数据调度到考核系统中
        /// </summary>
        /// <param name="wn"></param>
        private CHOfNode DTSDataToChOfNode(WorkNode wn)
        {
            return null;
            if (SystemConfigOfTax.IsAutoGenerCHOfNode == false)
                return null;

            //   return null;
            Work wk = wn.HisWork;
            Emp emp = wk.HisRec;
            Node nd = wn.HisNode;

            CHOfNode cn = new CHOfNode();
            cn.NodeState = NodeState.Complete;
            cn.WorkID = wk.OID;
            cn.FK_Node = wn.HisNode.NodeID;
            cn.RDT = wk.RDT;
            cn.CDT = wk.CDT;
            cn.SDT = DataType.AddDays(wk.RDT, nd.NeedCompleteDays).ToString(DataType.SysDataFormat);
            // cn.FK_NY = wk.Record_FK_NY;
            cn.FK_NY = DataType.CurrentYearMonth;
            cn.FK_Emp = wk.RecOfEmp.No;
            cn.FK_Station = wn.HisStationOfUse.No;
            cn.FK_Dept = wn.HisDeptOfUse.No;
            cn.NodeState = wk.NodeState;
            cn.FK_Dept = emp.FK_Dept;
            cn.FK_Flow = nd.FK_Flow;
            cn.SpanDays = DataType.SpanDays(wk.RDT, wk.CDT);
            cn.Note = "null "; // 扣分原因.

            //cn.Emps = wk.Emps ; //Emps ;
            string emps = "";
            //if (wk.Emps.IndexOf(",") == -1)
            //{
            //    emps = wk.Emps + ",";
            //}
            //if (wk.Emps.IndexOf(",") != -1)
            //{
            //    emps = wk.Emps;
            //}
            //cn.Emps = emps;


            //只要在某个节点里出现此字段：Spec,那么就把Spec里的内容写入 WF_CHOfNode 表里
            if (wk.EnMap.Attrs.Contains("SPEC"))
                cn.Spec = wk.GetValStringByKey("SPEC");

            cn.CentOfAdd = nd.SwinkCent; //工作量得分。
            cn.NodeDeductCent = nd.DeductCent;
            cn.NodeDeductDays = nd.DeductDays;
            cn.NodeMaxDeductCent = nd.MaxDeductCent;

            if (cn.SpanDays > 0)
            {
                cn.CentOfCut = cn.NodeDeductCent * (cn.SpanDays - nd.NeedCompleteDays);
                if (cn.CentOfCut > cn.NodeMaxDeductCent)
                    cn.CentOfCut = cn.NodeMaxDeductCent;

                cn.Note = this.ToE("WN1", "未按期完成工作。"); // "未按期完成工作。"; // 扣分原因
                if (cn.CentOfCut <= 0)
                    cn.CentOfCut = 0;
            }

            cn.Cent = cn.CentOfCut + cn.CentOfAdd;

            //if (cn.NodeState == NodeState.Complete)
            //{
            int i = DBAccess.RunSQLReturnValInt("select count(*) from WF_CHOfNode where fk_emp='" + emp.No + "' AND fk_node='" + nd.NodeID + "'");
            if (i == 0)
                cn.Insert();
            else
                cn.Update();
            //}
            //else
            //{
            //    cn.Save();
            //}
            return cn;
        }
        private string nextStationName = "";
        /// <summary>
        /// 产生下一步的工作者
        /// </summary>
        /// <param name="town">WorkNode</param>
        /// <returns></returns>
        public WorkerLists InitWorkerLists_QingHai(WorkNode town)
        {
            DataTable dt = new DataTable();
            // dt = DBAccess.RunSQLReturnTable("SELECT NO,NAME FROM PUB_EMP WHERE 1=2 ");
            dt.Columns.Add("No", typeof(string));

            string sql;
            string fk_emp;

            // 如果执行了两次发送，那前一次的轨迹就需要被删除。这里是为了避免错误。
            DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

            // 判断当前节点是否采集了目标人员.(分配责任区)
            if (this.HisWork.EnMap.Attrs.Contains("FK_Emp"))
            {
                //sql="SELECT No,Name FROM PUB_EMP WHERE NO='"+this.HisWork.GetValStringByKey("FK_Emp")+"'";
                fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);
                //dt=DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }
            //分配分局(所)长
            if (this.HisWork.EnMap.Attrs.Contains("FK_FJZ"))
            {
                fk_emp = this.HisWork.GetValStringByKey("FK_FJZ");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);

                //sql = "SELECT No,Name FROM PUB_EMP WHERE NO='" + this.HisWork.GetValStringByKey("FK_FJZ") + "'";
                //dt = DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }

            //从轨迹里面查询.
            sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
               + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') "
               + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerList WHERE WorkID=" + this.HisWork.OID + ")";
            dt = DBAccess.RunSQLReturnTable(sql);

            // 如果能够找到.
            if (dt.Rows.Count >= 1)
            {
                return WorkerListWayOfDept(town, dt);
            }

            // 没有查询到的情况下。
            Stations nextStations = town.HisNode.HisStations;
            // 找不到, 就要判断流向问题。
            Station fromSt = this.HisStationOfUse;
            string DeptofUse = this.HisDeptOfUse.No;
            if (nextStations.Count == 0)
                throw new Exception(this.ToEP1("WF2", "@工作流程{0}已经完成。", town.HisNode.Name));  //管理员维护错误，您没有给节点["+town.HisNode.Name+"]设置工作岗位。

            Station toSt = (Station)nextStations[0];
            if (fromSt.No == toSt.No)
            {
#warning edit 2008 - 05-28 .
                // sql = "SELECT No,Name FROM Port_Emp WHERE NO IN (SELECT FK_Dept FROM PORT_EMPDept WHERE FK_EMP='" + WebUser.No + "') AND NO IN (SELECT FK_Emp FROM PORT_EMPSTATION WHERE FK_Station='" + toSt.No + "')";
                sql = "SELECT No,Name FROM Port_Emp WHERE NO='" + WebUser.No + "'";  // IN (SELECT FK_Dept FROM PORT_EMPDept WHERE FK_EMP='" + WebUser.No + "') AND NO IN (SELECT FK_Emp FROM PORT_EMPSTATION WHERE FK_Station='" + toSt.No + "')";
                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    fk_emp = this.HisWork.GetValStringByKey("FK_FJZ");
                    DataRow dr = dt.NewRow();
                    dr[0] = fk_emp;
                    dt.Rows.Add(dr);
                    return WorkerListWayOfDept(town, dt);
                }
                else
                {
                    return WorkerListWayOfDept(town, dt);
                }
            }

            if (fromSt.Grade < toSt.Grade)
            {
            }
            else if (fromSt.Grade == toSt.Grade)
            {
            }
            else
            {
            }
            return WorkerListWayOfDept(town, dt);
        }

        public WorkerLists InitWorkerLists_Gener(WorkNode town)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            string sql;
            string fk_emp;

            // 如果执行了两次发送，那前一次的轨迹就需要被删除。这里是为了避免错误。
            DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

            if (this.HisNode.IsSelectEmp)
            {
                sql = "SELECT  FK_Emp  FROM WF_SelectAccper WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID;
                dt = DBAccess.RunSQLReturnTable(sql);
                return WorkerListWayOfDept(town, dt);
            }

            // 判断当前节点是否采集了目标人员
            if (this.HisWork.EnMap.Attrs.Contains("FK_Emp"))
            {
                fk_emp = this.HisWork.GetValStringByKey("FK_Emp");
                DataRow dr = dt.NewRow();
                dr[0] = fk_emp;
                dt.Rows.Add(dr);
                return WorkerListWayOfDept(town, dt);
            }

            // 判断 节点人员里是否有设置？  如果有就不考虑岗位设置了。从节点人员设置里面查询。
            if (town.HisNode.HisEmps.Length > 2)
            {
                string[] emps = town.HisNode.HisEmps.Split('@');
                foreach (string emp in emps)
                {
                    if (emp == null || emp == "")
                        continue;
                    DataRow dr = dt.NewRow();
                    dr[0] = emp;
                    dt.Rows.Add(dr);
                    return WorkerListWayOfDept(town, dt);
                }
            }



            // 判断节点部门里面是否设置了部门，如果设置了，就按照它的部门处理。
            if (town.HisNode.IsSetDept)
            {
                if (town.HisNode.HisStations.Count == 0)
                {
                    sql = "SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count > 0)
                        return WorkerListWayOfDept(town, dt);
                }
                else
                {
                    sql = "SELECT NO FROM PORT_EMP WHERE NO IN ";
                    sql += "(SELECT FK_Emp FROM Port_EmpDept WHERE FK_Dept IN ";
                    sql += "( SELECT FK_Dept FROM WF_NodeDept WHERE FK_Node=" + town.HisNode.NodeID + ")";
                    sql += ")";
                    sql += "AND NO IN ";
                    sql += "(";
                    sql += "SELECT FK_Emp FROM Port_EmpStation WHERE FK_Station IN ";
                    sql += "( SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ")";
                    sql += ")";
                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count > 0)
                        return WorkerListWayOfDept(town, dt);
                }
            }

            if (this.HisNode.IsStartNode == false)
            {
                // 如果当前的节点不是开始节点， 从轨迹里面查询。
                sql = "SELECT DISTINCT FK_Emp  FROM Port_EmpStation WHERE FK_Station IN "
                   + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') "
                   + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerList WHERE WorkID=" + this.HisWork.OID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs) + ") )";
                dt = DBAccess.RunSQLReturnTable(sql);

                // 如果能够找到.
                if (dt.Rows.Count >= 1)
                {
                    if (dt.Rows.Count == 1)
                    {
                        /*如果人员只有一个的情况，说明他可能要 */
                    }
                    return WorkerListWayOfDept(town, dt);
                }
            }

            // 没有查询到的情况下, 先按照本部门计算。
            sql = "SELECT NO FROM PORT_EMP WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept = '" + WebUser.FK_Dept + "')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception("节点没有岗位:" + town.HisNode.NodeID + "  " + town.HisNode.Name);
                // throw new Exception(this.ToEP1("WN2", "@工作流程{0}已经完成。", town.HisNode.Name));
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }


            // 没有查询到的情况下, 按照最大匹配数计算。
            sql = "SELECT NO FROM PORT_EMP WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept + "%')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception("节点没有岗位:" + town.HisNode.NodeID+"  " + town.HisNode.Name);

                //  throw new Exception(this.ToEP1("WN2", "@工作流程{0}已经完成。", town.HisNode.Name));
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }

            // 没有查询到的情况下, 按照最大匹配数 提高一个级别 计算。
            sql = "SELECT NO FROM PORT_EMP WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 2) + "%')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception("节点没有岗位:" + town.HisNode.NodeID+"  " + town.HisNode.Name);
                    //throw new Exception(this.ToEP1("WF2", "@工作流程{0}已经完成。", town.HisNode.Name));
                else
                {
                    try
                    {
                        sql = "SELECT NO FROM PORT_EMP WHERE NO IN "
                 + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') )"
                 + " AND  NO IN "
                 + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 4) + "%')";
                        dt = DBAccess.RunSQLReturnTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            sql = "SELECT NO FROM PORT_EMP WHERE NO IN "
                + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node='" + town.HisNode.NodeID + "') )";
                            dt = DBAccess.RunSQLReturnTable(sql);

                            if (dt.Rows.Count == 0)
                                throw new Exception(this.ToE("WF3", "没有找到当前的工作节点的数据，流程出现未知的异常。") + town.HisNode.Name); //"维护错误，请检查[" + town.HisNode.Name + "]维护的岗位中是否有人员？"
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(this.ToE("WF3", "没有找到当前的工作节点的数据，流程出现未知的异常。") + town.HisNode.Name); //"维护错误，请检查[" + town.HisNode.Name + "]维护的岗位中是否有人员？"
                    }

                    return WorkerListWayOfDept(town, dt);
                }
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backtoNodeID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public WorkNode DoReturnWork(int backtoNodeID, string msg)
        {
            // 改变当前的工作节点．
            WorkNode wn = new WorkNode(this.WorkID, backtoNodeID);
            // 更新 return work 状态．
            wn.HisWork.NodeState = NodeState.Back;
            wn.HisWork.DirectUpdate();

            GenerWorkFlow gwf = new GenerWorkFlow(this.HisWork.OID, this.HisNode.FK_Flow);
            gwf.FK_Node = backtoNodeID;
            gwf.DirectUpdate();

            // 删除工作者.
            WorkerLists wkls = new WorkerLists(this.HisWork.OID, this.HisNode.NodeID);
            wkls.Delete();

            // 写入日志.
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = wn.HisWork.OID;
            rw.NodeId = wn.HisNode.NodeID;
            rw.Note = msg;
            rw.Save();

            WorkerLists wls = new WorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);

            // 删除退回时当前节点的工作信息。
            this.HisWork.Delete();

            //删除退回时当前节点的考核信息。
            CHOfNode ch = new CHOfNode();
            int i = ch.Delete(CHOfNodeAttr.FK_Node, wn.HisNode.NodeID, CHOfNodeAttr.WorkID, wn.HisWork.OID);
            return wn;
        }

        /// <summary>
        /// 根据节点ID，删除它的数据
        /// </summary>
        /// <param name="nodeid">节点ID</param>
        /// <param name="workid">工作ID</param>
        public void DeleteNodeData(int nodeid, Int32 workid)
        {
        }
        /// <summary>
        /// 执行退回
        /// </summary>
        /// <param name="msg">退回工作的原因</param>
        /// <returns></returns>
        public WorkNode DoReturnWork(string msg)
        {
            // 改变当前的工作节点．
            WorkNode wn = this.GetPreviousWorkNode();
            GenerWorkFlow gwf = new GenerWorkFlow(this.HisWork.OID, this.HisNode.FK_Flow);
            gwf.FK_Node = wn.HisNode.NodeID;
            gwf.DirectUpdate();

            // 更新 return work 状态．
            wn.HisWork.NodeState = NodeState.Back;
            wn.HisWork.DirectUpdate();

            // 删除工作者.
            WorkerLists wkls = new WorkerLists(this.HisWork.OID, this.HisNode.NodeID);
            wkls.Delete();

            // 写入日志.
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = wn.HisWork.OID;
            rw.NodeId = wn.HisNode.NodeID;
            rw.Note = msg;
            rw.Save();

            //			WorkFlow wf = this.HisWorkFlow;
            //			wf.WritLog(msg);
            // 消息通知上一步工作人员处理．
            WorkerLists wls = new WorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);
            BP.WF.MsgsManager.AddMsgs(wls, "退回的工作", wn.HisNode.Name, "退回的工作");

            // 删除退回时当前节点的工作信息。
            this.HisWork.Delete();

            //删除退回时当前节点的考核信息。
            CHOfNode ch = new CHOfNode();
            int i = ch.Delete(CHOfNodeAttr.FK_Node, wn.HisNode.NodeID, CHOfNodeAttr.WorkID, wn.HisWork.OID);
            //if (i == 0)
            //    throw new Exception("删除错误。");
            /*
            // 发送消息给响应的工作人员．
            Sys.SysMsg sm = new BP.Sys.SysMsg();
            sm.ToEmp=","+wn.HisWork.RecOfEmp.No+",";
            sm.FromEmp=Web.WebUser.No;
            sm.Title="工作退回通知";
            sm.Doc=msg;
            sm.PRI=1;
            sm.Save();
            */
            return wn;
        }
        /// <summary>
        /// 执行工作完成
        /// </summary>
        public string DoSetThisWorkOver()
        {
            this.HisWork.AfterWorkNodeComplete();

            // 更新状态。
            this.HisWork.NodeState = NodeState.Complete;
            this.HisWork.SetValByKey("CDT", DataType.CurrentDataTime);
            this.HisWork.Rec = Web.WebUser.No;
            this.HisWork.DirectUpdate();

            // 清除其他的工作者.
            string sql = "";
            switch (this.HisNode.HisNodeWorkType)
            {
                //case NodeWorkType.MulWorks:
                case NodeWorkType.GECheckMuls:
                    break;
                default:
                    sql = "DELETE WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.HisWork.OID + " AND FK_Emp!='" + this.HisWork.Rec.ToString() + "'";
                    DBAccess.RunSQL(sql);
                    break;
            }
            return "";
        }
        /// <summary>
        /// 按照 "职务" 得到的能够执行这项工作的集合
        /// </summary>
        /// <returns></returns>
        private DataTable GetCanDoWorkEmpsByDuty(int nodeId)
        {
            string sql = "SELECT  b.FK_Emp FROM WF_NodeDuty  a, Port_EmpDuty b WHERE (a.FK_Duty=b.FK_Duty ) AND a.FK_Node=" + nodeId;
            return DBAccess.RunSQLReturnTable(sql);
        }
        /// <summary>
        /// 按照 "部门" 得到的能够执行这项工作的集合
        /// </summary>
        /// <returns></returns>
        private DataTable GetCanDoWorkEmpsByDept(int nodeId)
        {
            string sql = "SELECT  b.FK_Emp FROM WF_NodeDept  a, Port_EmpDept b WHERE (a.FK_Dept=b.FK_Dept) AND a.FK_Node=" + nodeId;
            return DBAccess.RunSQLReturnTable(sql);
        }

        #region 根据工作岗位生成工作者
        /// <summary>
        /// 
        /// </summary>
        /// <param name="town"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private WorkerLists WorkerListWayOfDept(WorkNode town, DataTable dt)
        {
            if (dt.Rows.Count == 0)
                throw new Exception(this.ToE("WN4", "接受人员列表为空")); // 接受人员列表为空

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0].ToString() == WebUser.No)
                {
                    /*如果包含我*/

                }
            }

            Int64 workID = this.HisWork.OID;
            int toNodeId = town.HisNode.NodeID;

            this.HisWorkerLists = new WorkerLists();

#warning 限期时间  town.HisNode.DeductDays-1

            // 2008-01-22 之前的东西。
            //int i = town.HisNode.DeductDays;
            //dtOfShould = DataType.AddDays(dtOfShould, i);
            //if (town.HisNode.WarningDays > 0)
            //    dtOfWarning = DataType.AddDays(dtOfWarning, i - town.HisNode.WarningDays);
            // edit at 2008-01-22 , 处理预警日期的问题。

            DateTime dtOfShould = DataType.AddDays(DateTime.Now, town.HisNode.DeductDays);
            DateTime dtOfWarning = DateTime.Now;
            if (town.HisNode.WarningDays > 0)
                dtOfWarning = DataType.AddDays(dtOfShould, -town.HisNode.WarningDays); // dtOfShould.AddDays(-town.HisNode.WarningDays);


            this.HisGenerWorkFlow.Update(GenerWorkFlowAttr.FK_Node, town.HisNode.NodeID,
                "SDT", dtOfShould.ToString("MM-dd"));

            if (dt.Rows.Count == 1)
            {
                /* 如果只有一个人员 */
                WorkerList wl = new WorkerList();
                wl.WorkID = workID;
                wl.FK_Node = toNodeId;
                wl.FK_Emp = dt.Rows[0][0].ToString();

                Emp emp = new Emp(wl.FK_Emp);
                wl.FK_EmpText = emp.Name;
                wl.FK_Dept = emp.FK_Dept;
                wl.WarningDays = town.HisNode.WarningDays;
                wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);

                wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                wl.RDT = DateTime.Now.ToString(DataType.SysDataFormat);
                wl.FK_Flow = town.HisNode.FK_Flow;
                wl.FID = town.HisWork.FID;
                wl.DirectInsert();

                this.HisWorkerLists.AddEntity(wl);

                RememberMe rm = new RememberMe(); // this.GetHisRememberMe(town.HisNode);
                rm.Objs = "@" + wl.FK_Emp + "@";
                rm.ObjsExt = wl.FK_Emp + "<" + wl.FK_EmpText + ">";
                rm.Emps = "@" + wl.FK_Emp + "@";
                rm.EmpsExt = wl.FK_Emp + "<" + wl.FK_EmpText + ">";
                this._RememberMe = rm;
            }
            else
            {
                /* 如果有多个人员 */

                RememberMe rm = this.GetHisRememberMe(town.HisNode);

                // 记忆中是否存在当前的人员。
                bool isHaveIt = false;
                string emps = "@";
                foreach (DataRow dr in dt.Rows)
                {
                    string fk_emp = dr[0].ToString();
                    if (rm.Objs.IndexOf("@" + fk_emp) != -1)
                        isHaveIt = true;

                    emps += fk_emp + "@";
                }

                if (isHaveIt == false)
                {
                    /* 记忆里面没有当前生成的操作人员 */
                    /* 已经保证了没有重复的人员。*/
                    string myemps = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (myemps.IndexOf(dr[0].ToString()) != -1)
                            continue;

                        myemps += "@" + dr[0].ToString();

                        WorkerList wl = new WorkerList();
                        wl.IsEnable = true;
                        wl.WorkID = workID;
                        wl.FK_Node = toNodeId;
                        wl.FK_Emp = dr[0].ToString();

                        Emp emp = new Emp();
                        try
                        {
                            emp = new Emp(wl.FK_Emp);
                        }
                        catch
                        {
                            continue;
                        }

                        wl.FK_EmpText = emp.Name;

                        wl.FK_Dept = emp.FK_Dept;

                        //    wl.UseDept = this.HisDeptOfUse.No;
                        //    wl.UseStation = this.HisStationOfUse.No;
                        wl.WarningDays = town.HisNode.WarningDays;

                        wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);
                        wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                        wl.RDT = DateTime.Now.ToString(DataType.SysDataFormat);
                        wl.FK_Flow = town.HisNode.FK_Flow;
                        wl.FID = town.HisWork.FID;

                        try
                        {
                            wl.DirectInsert();
                        }
                        catch (Exception ex)
                        {
                            Log.DefaultLogWriteLineError("不应该出现的异常信息：" + ex.Message);
                        }
                        this.HisWorkerLists.AddEntity(wl);
                    }
                }
                else
                {
                    string[] strs = rm.Objs.Split('@');
                    string myemps = "";
                    foreach (string s in strs)
                    {
                        if (s.Length < 3)
                            continue;

                        if (myemps.IndexOf(s) != -1)
                            continue;

                        myemps += "@" + s;

                        WorkerList wl = new WorkerList();
                        wl.IsEnable = true;
                        wl.WorkID = workID;
                        wl.FK_Node = toNodeId;
                        wl.FK_Emp = s;

                        Emp emp = new Emp();
                        try
                        {
                            emp = new Emp(wl.FK_Emp);
                        }
                        catch
                        {
                            continue;
                        }

                        wl.FK_EmpText = emp.Name;

                        wl.FK_Dept = emp.FK_Dept;

                        //wl.UseDept = this.HisDeptOfUse.No;
                        //wl.UseStation = this.HisStationOfUse.No;
                        wl.WarningDays = town.HisNode.WarningDays;
                        wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);
                        wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                        wl.RDT = DateTime.Now.ToString(DataType.SysDataFormat);
                        wl.FK_Flow = town.HisNode.FK_Flow;
                        wl.FID = town.HisWork.FID;


                        try
                        {
                            if (town.HisNode.IsFLHL == false)
                                wl.DirectInsert();
                        }
                        catch
                        {
                            continue;
                        }
                        this.HisWorkerLists.AddEntity(wl);
                    }
                }

                string objsmy = "@";
                foreach (WorkerList wl in this.HisWorkerLists)
                {
                    objsmy += wl.FK_Emp + "@";
                }

                if (rm.Emps != emps || rm.Objs != objsmy)
                {
                    /* 工作人员列表发生了变化 */
                    rm.Emps = emps;
                    rm.Objs = objsmy;

                    string objExts = "";
                    foreach (WorkerList wl in this.HisWorkerLists)
                    {
                        if (Glo.IsShowUserNoOnly)
                            objExts += wl.FK_Emp + "、";
                        else
                            objExts += wl.FK_Emp + "<" + wl.FK_EmpText + ">、";
                    }
                    rm.ObjsExt = objExts;

                    string empExts = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        Emp emp = new Emp(dr[0].ToString());
                        if (rm.Objs.IndexOf(emp.No) != -1)
                        {
                            if (Glo.IsShowUserNoOnly)
                                empExts += emp.No + "、";
                            else
                                empExts += emp.No + "<" + emp.Name + ">、";
                        }
                        else
                        {
                            if (Glo.IsShowUserNoOnly)
                                empExts += "<strike><font color=red>" + emp.No + "</font></strike>、";
                            else
                                empExts += "<strike><font color=red>" + emp.No + "<" + emp.Name + "></font></strike>、";
                        }
                    }
                    rm.EmpsExt = empExts;
                    rm.Save();
                }
            }

            if (this.HisWorkerLists.Count == 0)
                throw new Exception("@根据部门产生工作人员出现错误，流程[" + this.HisWorkFlow.HisFlow.Name + "],中节点[" + town.HisNode.Name + "]定义错误,没有找到接受此工作的工作人员.");
            return this.HisWorkerLists;
        }
        #endregion


        #region 属性

        #region 工作岗位
        private Station _HisStationOfUse = null;
        /// <summary>
        /// 取出当前工作用的岗位.
        /// 这对与多个工作岗位的节点来说的.
        /// 
        /// </summary>
        /// <returns></returns>
        public Station HisStationOfUse
        {
            get
            {
                if (_HisStationOfUse == null)
                {
                    Stations sts = this.HisNode.HisStations;
                    if (sts.Count == 0)
                        throw new Exception(this.ToE("WN18", this.HisNode.Name));

                    _HisStationOfUse = (Station)this.HisNode.HisStations[0];
                    return _HisStationOfUse;

                    if (this.HisStations.Count == 1)
                    {
                        /* 如果这个工作节点只允许一个工作岗位访问，那末这个工作节点就是这个工作节点用的节点。*/
                        _HisStationOfUse = (Station)HisStations[0];
                        return _HisStationOfUse;
                    }

                    // 如果当前节点允许多个工作岗位访问。 找出工作人员的工作岗位集合					
                    Stations mySts = this.HisWork.RecOfEmp.HisStations;
                    if (mySts.Count == 1)
                    {
                        _HisStationOfUse = (Station)mySts[0];
                        return _HisStationOfUse;
                    }

                    // 取出，节点的工作岗位与工作人员工作岗位的交集合
                    Stations gainSts = (Stations)mySts.GainIntersection(this.HisStations);
                    if (gainSts.Count == 0)
                    {
                        if (this.HisStations.Count == 0)
                            throw new Exception("@您没有为节点[" + this.HisNode.Name + "],设置工作岗位.");

                        _HisStationOfUse = (Station)mySts[0];
                        return _HisStationOfUse;
                        ///// //throw new Exception("@获取使用工作岗位出现错误,工作人员["+this.HisWork.RecOfEmp.Name+"]的工作没有处理完成,您改变了他的工作岗位.导致工作不能正常的流转下去.");
                    }

                    /* 判断他的主要工作岗位,主要工作岗位优先. */
                    string mainStatNo = this.HisWork.HisRec.No;
#warning
                    foreach (Station myst in gainSts)
                    {
                        if (myst.No == mainStatNo)
                        {
                            _HisStationOfUse = myst;
                            return _HisStationOfUse;
                        }
                    }

                    /*扫描交集,外勤优先. */
                    foreach (Station myst in gainSts)
                    {

                    }

                    /* 没有扫描到就, 返回第一个station.*/
                    if (_HisStationOfUse == null)
                        _HisStationOfUse = (Station)gainSts[0];

                }
                return _HisStationOfUse;
            }
        }
        /// <summary>
        /// 工作岗位
        /// </summary>
        public Stations HisStations
        {
            get
            {
                return this.HisNode.HisStations;
            }
        }
        /// <summary>
        /// 返回第一个工作节点
        /// </summary>
        public Station HisStationOfFirst
        {
            get
            {
                return (Station)this.HisNode.HisStations[0];
            }
        }
        #endregion

        #region 判断一人多部门的情况
        /// <summary>
        /// HisDeptOfUse
        /// </summary>
        private Dept _HisDeptOfUse = null;
        /// <summary>
        /// HisDeptOfUse
        /// </summary>
        public Dept HisDeptOfUse
        {
            get
            {
                if (_HisDeptOfUse == null)
                {
                    //找到全部的部门。
                    Depts depts;
                    if (this.HisWork.Rec == WebUser.No)
                        depts = WebUser.HisDepts;
                    else
                        depts = this.HisWork.RecOfEmp.HisDepts;

                    if (depts.Count == 0)
                        throw new Exception("您没有给[" + this.HisWork.Rec + "]设置部门。");

                    if (depts.Count == 1) /* 如果全部的部门只有一个，就返回它。*/
                    {
                        _HisDeptOfUse = (Dept)depts[0];
                        return _HisDeptOfUse;
                    }

                    if (_HisDeptOfUse == null)
                    {
                        /* 如果还没找到，就返回第一个部门。 */
                        _HisDeptOfUse = depts[0] as Dept;
                    }
                }
                return _HisDeptOfUse;
            }
        }
        #endregion

        #endregion

        #region 条件
        private Conds _HisNodeCompleteConditions = null;
        /// <summary>
        /// 节点完成任务的条件
        /// 条件与条件之间是or 的关系, 就是说,如果任何一个条件满足,这个工作人员在这个节点上的任务就完成了.
        /// </summary>
        public Conds HisNodeCompleteConditions
        {
            get
            {
                if (this._HisNodeCompleteConditions == null)
                {
                    _HisNodeCompleteConditions = new Conds(CondType.Node, this.HisNode.NodeID, this.WorkID);

                    return _HisNodeCompleteConditions;
                }
                return _HisNodeCompleteConditions;
            }
        }
        private Conds _HisFlowCompleteConditions = null;
        /// <summary>
        /// 他的完成任务的条件,此节点是完成任务的条件集合
        /// 条件与条件之间是or 的关系, 就是说,如果任何一个条件满足,这个任务就完成了.
        /// </summary>
        public Conds HisFlowCompleteConditions
        {
            get
            {
                if (this._HisFlowCompleteConditions == null)
                {
                    _HisFlowCompleteConditions = new Conds(CondType.Flow, this.HisNode.NodeID, this.WorkID);

                }
                return _HisFlowCompleteConditions;
            }
        }
        #endregion

        #region 关于质量考核
        ///// <summary>
        ///// 得到以前的已经完成的工作节点.
        ///// </summary>
        ///// <returns></returns>
        //public WorkNodes GetHadCompleteWorkNodes()
        //{
        //    WorkNodes mywns = new WorkNodes();
        //    WorkNodes wns = new WorkNodes(this.HisNode.HisFlow, this.HisWork.OID);
        //    foreach (WorkNode wn in wns)
        //    {
        //        if (wn.IsComplete)
        //            mywns.Add(wn);
        //    }
        //    return mywns;
        //}
        #endregion

        #region 流程公共方法
        /// <summary>
        /// 检查全局的工作流程是不是完成任务
        /// 如果完成了，就返回不做任何处理。
        /// 如果符合流程完成的条件，就返回相应的信息。
        /// 如果不符合流程的完成条件，return null;  
        /// </summary>
        public string CheckGlobalCompleteCondition()
        {
            if (this.HisWorkFlow.IsComplete)  // 如果全局的工作已经完成.
                return this.ToE("FlowOver", "当前的任务已经完成"); //当前的任务已经完成

            #region 检查全局的完成条件,由于不经常用到,所以就暂时删除.
            /*
			GlobalCompleteConditions gcc = new GlobalCompleteConditions(this.HisNode.FK_Flow,this.HisWork.OID);
			if (gcc.IsOneOfConditionPassed)
			{
				this.HisWorkFlow.DoFlowOver();
				return "@工作流程["+this.HisNode.HisFlow.Name+"]执行过程中，在节点["+this.HisNode.Name+"]符合流程完成条件，"+gcc.GetOneOfConditionPassed.ConditionDesc+"，工作正常结束！";
			}
			*/
            #endregion

            return null;
        }
        /// <summary>
        /// 解决流程回滚的问题
        /// </summary>
        /// <param name="TransferPC"></param>
        /// <param name="ByTransfered"></param>
        /// <returns></returns>
        public string AfterNodeSave()
        {
            DateTime dt = DateTime.Now;
            this.HisWork.Rec = Web.WebUser.No;
            this.WorkID = this.HisWork.OID;
            // this.NodeID = this.HisNode.NodeID;
            try
            {
                string msg = AfterNodeSave_Do();

                string doc = this.HisNode.DoWhat;
                if (doc.Length > 10)
                {
                    Attrs attrs = this.HisWork.EnMap.Attrs;
                    foreach (Attr attr in attrs)
                    {
                        if (doc.Contains("@" + attr.Key) == false)
                            continue;

                        switch (attr.MyDataType)
                        {
                            case BP.DA.DataType.AppString:
                                doc = doc.Replace("@" + attr.Key, "'" + this.HisWork.GetValStrByKey(attr.Key) + "'");
                                break;
                            default:
                                doc = doc.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                                break;
                        }
                    }

                    BP.DA.DBAccess.RunSQL(doc);
                }
                return msg;
            }
            catch (Exception ex)
            {
                /*在提交错误的情况下，回滚数据。*/
                try
                {

                    // 把工作的状态设置回来。
                    this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Init);

                    // 把流程的状态设置回来。
                    GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID, this.HisNode.FK_Flow);
                    if (gwf.WFState != 0 || gwf.FK_Node != this.HisNode.NodeID)
                    {
                        /* 如果这两项其中有一项有变化。*/
                        gwf.FK_Node = this.HisNode.NodeID;
                        gwf.WFState = 0;
                        gwf.Update();
                    }


                    Node startND = this.HisNode.HisFlow.HisStartNode;
                    StartWork wk = startND.HisWork as StartWork;
                    switch (startND.HisNodeWorkType)
                    {
                        case NodeWorkType.StartWorkFL:
                        case NodeWorkType.WorkFL:

                            break;
                        default:
                            // 把开始节点的装态设置回来。
                            wk.OID = this.WorkID;
                            wk.Retrieve();
                            if (wk.WFState == WFState.Complete)
                            {
                                wk.Update("WFState", (int)WFState.Runing);
                            }
                            break;
                    }


                    Nodes nds = this.HisNode.HisToNodes;
                    foreach (Node nd in nds)
                    {
                        Work mwk = nd.HisWork;
                        mwk.OID = this.WorkID;
                        if (mwk.IsGECheckStand)
                            mwk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);
                        mwk.Delete();
                    }

                    throw ex;
                }
                catch (Exception ex1)
                {
                    throw new Exception("@发送期间出现错误:" + ex.Message + " @回滚发送失败数据出现错误：" + ex1.Message);
                }
            }
        }
        #region 用户到的变量
        public WorkerLists HisWorkerLists = null;
        public CHOfFlow HisCHOfFlow = null;
        public GenerWorkFlow _HisGenerWorkFlow;
        public GenerWorkFlow HisGenerWorkFlow
        {
            get
            {
                if (_HisGenerWorkFlow == null)
                    _HisGenerWorkFlow = new GenerWorkFlow(this.WorkID, this.HisNode.FK_Flow);
                return _HisGenerWorkFlow;
            }
            set
            {
                _HisGenerWorkFlow = value;
            }
        }
        private Int64 _WorkID = 0;
        public Int64 WorkID
        {
            get
            {
                return _WorkID;
            }
            set
            {
                _WorkID = value;
            }
        }
        #endregion


        public WorkerLists GenerWorkerLists(WorkNode town)
        {
            // 初试化他们的工作人员．
            WorkerLists gwls = null;
            switch (SystemConfig.CustomerNo)
            {
                case BP.CustomerNoList.QHDS:
                case BP.CustomerNoList.HBDS:
                    gwls = this.InitWorkerLists_QingHai(town);
                    break;
                case BP.CustomerNoList.HNDS:
                default:
                    gwls = this.InitWorkerLists_Gener(town);
                    break;
            }
            return gwls;
        }
        /// <summary>
        /// 启动分流
        /// </summary>
        /// <param name="toNode"></param>
        /// <returns></returns>
        public string FeiLiuStartUp(Node toNode)
        {
            // ssdd
            Work wk = toNode.HisWork;
            WorkNode town = new WorkNode(wk, toNode);

            // 产生下一步骤要执行的人员。
            WorkerLists gwls = this.GenerWorkerLists(town);
            this.AddIntoWacthDog(gwls);  //@加入消息集合里。


            //清除以前的数据，比如两次发送。
            wk.Delete(WorkAttr.FID, this.HisWork.OID);

            string msg = "";

            // 按照部门分组，分别启动流程。
            switch (this.HisNode.HisFLRole)
            {
                case FLRole.ByStation:
                case FLRole.ByDept:
                case FLRole.ByEmp:
                    foreach (WorkerList wl in gwls)
                    {
                        Work mywk = toNode.HisWork;
                        mywk.Copy(this.HisWork);  //复制过来信息。
                        mywk.FID = this.HisWork.FID;
                        mywk.Rec = wl.FK_Emp;
                        mywk.Emps = wl.FK_Emp;
                        try
                        {
                            mywk.BeforeSave();
                        }
                        catch
                        {
                        }

                        mywk.OID = DBAccess.GenerOID(BP.Web.WebUser.FK_Dept.Substring(2));  //BP.DA.DBAccess.GenerOID();
                        // 跳过特殊的规则约束。
                        mywk.InsertAsOID(mywk.OID);


                        // 产生工作的信息。
                        GenerWorkFlow gwf = new GenerWorkFlow();
                        gwf.FID = this.WorkID;
                        gwf.WorkID = mywk.OID;

                        if (BP.WF.Glo.IsShowUserNoOnly)
                            gwf.Title = WebUser.No + "," + WebUser.Name + " 自动发起：" + toNode.Name + "(分流节点)";
                        else
                            gwf.Title = WebUser.No + " 自动发起：" + toNode.Name + "(分流节点)";

                        gwf.WFState = 0;
                        gwf.RDT = DataType.CurrentData;
                        gwf.Rec = Web.WebUser.No;
                        gwf.FK_Flow = toNode.FK_Flow;
                        gwf.FK_Node = toNode.NodeID;
                        gwf.FK_Dept = wl.FK_Dept;
                        try
                        {
                            gwf.DirectInsert();
                        }
                        catch
                        {
                            gwf.DirectUpdate();
                        }

                        // 更新work中的信息。
                        BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET WorkID=" + mywk.OID + ",FID=" + this.WorkID + " WHERE FK_Emp='" + wl.FK_Emp + "' AND WorkID=" + this.WorkID + " AND FK_Node=" + toNode.NodeID);
                    }
                    break;
                default:
                    throw new Exception("没有处理的类型：" + this.HisNode.HisFLRole.ToString());
            }
            // return 。系统自动下达给如下几位同事。";

            msg += "@分流节点：" + toNode.Name + " 已经发起。" + string.Format("@任务自动下达给{0}如下{1}位同事,{2}.", this.nextStationName, this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);
            msg += this.GenerWhySendToThem(this.HisNode.NodeID, toNode.NodeID);

            //更新节点状态。
            this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete);

            string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            msg += "@<a href='" + path + "/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=" + wk.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >工作报告</a>";

            return msg;
            // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ：@" + this._RememberMe.EmpsExt;
            // return this.ToEP3("TaskAutoSendTo", "@任务自动下达给{0}如下{1}位同事,{2}.", this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);  // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ：@" + this._RememberMe.EmpsExt;
        }

        public string FeiLiuStartUp()
        {
            GenerFH fh = new GenerFH();
            fh.FID = this.WorkID;
            if (this.HisNode.IsStartNode || fh.IsExits == false)
            {
                fh.Title = this.HisWork.GetValStringByKey(StartWorkAttr.Title);
                fh.RDT = DataType.CurrentData;
                fh.FID = this.WorkID;
                fh.FK_Flow = this.HisNode.FK_Flow;
                fh.FK_Node = this.HisNode.NodeID;
                fh.GroupKey = WebUser.No;
                fh.WFState = 0;
                try
                {
                    fh.DirectInsert();
                }
                catch
                {
                    fh.DirectUpdate();
                }
            }


            string msg = "";
            Nodes toNodes = this.HisNode.HisToNodes;

            // 如果只有一个转向节点, 就不用判断条件了,直接转向他.
            if (toNodes.Count == 1)
                return FeiLiuStartUp((Node)toNodes[0]);

            int toNodeId = 0;
            int numOfWay = 0;
            foreach (Node nd in toNodes)
            {

                Conds dcs = new Conds();
                QueryObject qo = new QueryObject(dcs);
                qo.AddWhere(CondAttr.NodeID, this.HisNode.NodeID);
                qo.addAnd();
                qo.AddWhere(CondAttr.ToNodeID, nd.NodeID);
                qo.addAnd();
                qo.AddWhere(CondAttr.CondType, (int)CondType.FLRole);
                qo.DoQuery();
                foreach (Cond dc in dcs)
                {
                    dc.WorkID = this.HisWork.OID;
                }


                if (dcs.Count == 0)
                {
                    throw new Exception(string.Format(this.ToE("WN10", "@定义节点的方向条件错误:没有给从{0}节点到{1},定义转向条件."), this.HisNode.NodeID + this.HisNode.Name, nd.NodeID + nd.Name));
                    //throw new Exception("@定义节点的方向条件错误:没有给从[" + this.HisNode.NodeID + this.HisNode.Name + "]节点到[" + nd.NodeID + nd.Name + "],定义转向条件");
                }

                if (dcs.IsPass) // 如果多个转向条件中有一个成立.
                {
                    numOfWay++;
                    toNodeId = nd.NodeID;
                    msg = FeiLiuStartUp(nd);
                }
            }


            if (toNodeId == 0)
            {
                //throw new Exception("转向条件设置错误:节点名称" + this.HisNode.Name +" , 工作流程引擎无法投递。");
                throw new Exception(string.Format(this.ToE("WN11", "@转向条件设置错误:节点名称{0}, 系统无法投递。"), this.HisNode.Name));  // 转向条件设置错误:节点名称" + this.HisNode.Name + " , 工作流程引擎无法投递。"
            }
            else
            {
                /* 删除曾经在这个步骤上的流程运行数据。
                 * 比如说：方向条件，发生了变化后可能产生两个工作上的数据。是为了工作报告上面体现了两个步骤。 */
                foreach (Node nd in toNodes)
                {
                    if (nd.NodeID == toNodeId)
                        continue;

                    // 删除这个工作，因为这个工作的数据不在有用了。
                    Work wk = nd.HisWork;
                    wk.OID = this.HisWork.OID;
                    if (wk.IsGECheckStand)
                        wk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);

                    wk.Delete();

                    // 删除这个工作的考核信息。
                    if (SystemConfigOfTax.IsAutoGenerCHOfNode)
                    {
                        CHOfNode cn = new CHOfNode();
                        cn.WorkID = this.HisWork.OID;
                        cn.FK_Node = nd.NodeID;
                        cn.Delete();
                    }
                }
            }
            return msg;
        }
        #endregion

        /// <summary>
        /// 在流程节点保存后的操作.
        /// 1, 判断节点的任务是不是完成,如果完成,就设置节点的完成状态.
        /// 2, 判断是不是符合工作流完成任务任务的条件, 如果完成,就设置,工作流程任务完成. return ;
        /// 3, 判断节点的方向.
        /// 1, 一个一个的判断此节点的方向, 满足就启动节点工作.
        /// 2, 如果没有任何一个节点的工作.那末就抛出异常, 流程的节点方向条件定义错误. .
        /// </summary>
        /// <param name="TransferPC">是否要执行获取外部信息工组</param>
        /// <param name="ByTransfered">是不是被调用的,只用在开始节点,并且是外部调用的自动启动的流程,如果设置为true, 流程在当前的节点完成之后就不做下一步骤的工作,如果设置为false, 就做下一步工作.</param>
        /// <returns>执行后的内容</returns>
        private string AfterNodeSave_Do()
        {
            try
            {
                if (this.HisNode.IsStartNode)
                {
                    /* 产生开始工作流程记录. */
                    GenerWorkFlow gwf = new GenerWorkFlow();
                    gwf.WorkID = this.HisWork.OID;
                    gwf.Title = this.HisWork.GetValStringByKey(StartWorkAttr.Title);
                    gwf.WFState = 0;
                    gwf.RDT = this.HisWork.RDT;
                    gwf.Rec = Web.WebUser.No;
                    gwf.FK_Flow = this.HisNode.FK_Flow;
                    gwf.FK_Node = this.HisNode.NodeID;
                    //  gwf.FK_Station = this.HisStationOfUse.No;
                    gwf.FK_Dept = this.HisWork.RecOfEmp.FK_Dept;
                    try
                    {
                        gwf.DirectInsert();
                    }
                    catch
                    {
                        gwf.DirectUpdate();
                    }

                    #region 设置  HisGenerWorkFlow

                    this.HisGenerWorkFlow = gwf;

                    //#warning 去掉这个工作没有想到如何去写？
                    // 记录到流程统计分析中去。
                    this.HisCHOfFlow = new CHOfFlow();
                    this.HisCHOfFlow.Copy(gwf);
                    this.HisCHOfFlow.WorkID = this.HisWork.OID;
                    this.HisCHOfFlow.WFState = (int)WFState.Runing;

                    /* 说明没有这个记录 */
                    this.HisCHOfFlow.FK_Flow = this.HisNode.FK_Flow;
                    this.HisCHOfFlow.WFState = 0;
                    this.HisCHOfFlow.Title = gwf.Title;
                    this.HisCHOfFlow.FK_Emp = this.HisWork.Rec.ToString();
                    this.HisCHOfFlow.RDT = this.HisWork.RDT;
                    this.HisCHOfFlow.CDT = DataType.CurrentDataTime;
                    this.HisCHOfFlow.SpanDays = 0;
                    this.HisCHOfFlow.FK_Dept = this.HisDeptOfUse.No;
                    this.HisCHOfFlow.FK_NY = DataType.CurrentYearMonth;
                    try
                    {
                        this.HisCHOfFlow.Insert();
                    }
                    catch
                    {
                        this.HisCHOfFlow.Update();
                    }
                    #endregion HisCHOfFlow


                    #region  产生开始工作者,能够执行他们的人员.
                    WorkerList wl = new WorkerList();
                    wl.WorkID = this.HisWork.OID;
                    wl.FK_Node = this.HisNode.NodeID;
                    wl.FK_Emp = this.HisWork.Rec;
                    wl.FK_Flow = this.HisNode.FK_Flow;
                    // wl.UseDept = this.HisDeptOfUse.No;
                    // wl.UseStation = this.HisStationOfUse.No;
                    wl.WarningDays = this.HisNode.WarningDays;
                    wl.SDT = DataType.CurrentData;
                    wl.DTOfWarning = DataType.CurrentData;
                    wl.RDT = DataType.CurrentData;
                    try
                    {
                        wl.Insert(); // 先插入，后更新。
                    }
                    catch
                    {
                        wl.Update();
                    }
                    #endregion
                }


                string msg = "";
                switch (this.HisNode.HisNodeWorkType)
                {
                    case NodeWorkType.GECheckMuls:
                    case NodeWorkType.GECheckStands:
                    case NodeWorkType.NumChecks: /* 审核点的逻辑 */
                        msg = this.AfterWorkOfCheckSave();
                        msg += this.DoSetThisWorkOver();
                        break;
                    case NodeWorkType.StartWorkFL:
                    case NodeWorkType.WorkFL:  /* 启动分流 */
                        this.HisWork.FID = this.HisWork.OID;
                        msg = this.FeiLiuStartUp();
                        break;
                    case NodeWorkType.WorkFHL:   /* 启动分流 */
                        this.HisWork.FID = this.HisWork.OID;
                        msg = this.FeiLiuStartUp();
                        break;
                    default: /* 其他的点的逻辑 */
                        msg = this.StartupNewNodeWork();
                        msg += this.DoSetThisWorkOver();
                        break;
                }

                #region 生成文书
                BookTemplates reffunc = this.HisNode.HisBookTemplates;
                if (reffunc.Count > 0)
                {
                    #region 生成文书信息
                    Int64 workid = this.HisWork.OID;
                    int nodeId = this.HisNode.NodeID;
                    //string bookTable=this.HisNode.HisFlow.BookTable;
                    string flowNo = this.HisNode.FK_Flow;

                    // 删除这个sql, 在后面已经做了异常判断。
                    //DBAccess.RunSQL("DELETE WF_Book WHERE WorkID='" + workid + "' and (FK_Node='" + nodeId + "') ");
                    #endregion

                    DateTime dt = DateTime.Now;
                    string bookNo = this.HisWorkFlow.HisStartWork.BillNo;
                    Flow fl = new Flow(this.HisNode.FK_Flow);
                    string year = dt.Year.ToString();
                    string bookInfo = "";
                    foreach (BookTemplate func in reffunc)
                    {
                        string file = year + "_" + WebUser.FK_Dept + "_" + func.No + "_" + workid + ".doc";
                        BP.Rpt.RTF.RTFEngine rtf = new BP.Rpt.RTF.RTFEngine();

                        Works works;
                        string[] paths;
                        string path;

                        try
                        {
                            #region 生成文书
                            rtf.HisEns.Clear();
                            rtf.EnsDataDtls.Clear();

                            if (func.NodeID == 0)
                            {
                                // 判断是否是受理回执
                                if (fl.DateLit == 0)
                                    continue;

                                HisCHOfFlow.DateLitFrom = DateTime.Now.AddDays(fl.DateLit).ToString(DataType.SysDataFormat);
                                HisCHOfFlow.DateLitTo = DateTime.Now.AddDays(fl.DateLit + 10).ToString(DataType.SysDataFormat);
                                HisCHOfFlow.Update();
                                rtf.AddEn(HisCHOfFlow);
                            }
                            else
                            {
                                WorkNodes wns = new WorkNodes();
                                if (this.HisNode.HisFNType == FNType.River)
                                    wns.GenerByFID(this.HisNode.HisFlow, this.WorkID);
                                else
                                    wns.GenerByWorkID(this.HisNode.HisFlow, this.WorkID);


                                works = wns.GetWorks;
                                foreach (Work wk in works)
                                {
                                    if (wk.OID == 0)
                                        continue;

                                    rtf.AddEn(wk);
                                    rtf.ensStrs += ".ND" + wk.NodeID;
                                    ArrayList al = wk.GetDtlsDatasOfArrayList();
                                    foreach (Entities ens in al)
                                        rtf.AddEns(ens);
                                }
                                //    w = new BP.Port.WordNo(WebUser.FK_DeptOfXJ);
                                // rtf.AddEn(w);
                            }

                            paths = file.Split('_');
                            path = paths[0] + "/" + paths[1] + "/" + paths[2] + "/";

                            bookInfo += "<img src='/" + SystemConfig.AppName + "/Images/Btn/Word.gif' /><a href='" + System.Web.HttpContext.Current.Request.ApplicationPath + "/FlowFile/Book/" + path + file + "' target=_blank >" + func.Name + "</a>";

                            //  string  = BP.SystemConfig.GetConfig("FtpPath") + file;
                            path = BP.WF.Glo.FlowFile + "\\Book\\" + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";


                            if (System.IO.Directory.Exists(path) == false)
                                System.IO.Directory.CreateDirectory(path);

                            rtf.MakeDoc(func.Url + ".rtf",
                                path, file, func.ReplaceVal, false);

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            BP.WF.DTS.InitBookDir dir = new BP.WF.DTS.InitBookDir();
                            dir.Do();

                            path = BP.WF.Glo.FlowFile + "\\Book\\" + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";
                            string msgErr = "@" + this.ToE("WN5", "生成文书失败，请让管理员检查目录设置") + "[" + BP.WF.Glo.FlowFile + "]。@Err：" + ex.Message + " @File=" + file + " @Path:" + path;
                            bookInfo += "@<font color=red>" + msgErr + "</font>";
                            throw new Exception(msgErr);
                        }

                        Book bk = new Book();
                        bk.WorkID = this.HisWork.OID;
                        bk.FK_Node = this.HisNode.NodeID;
                        bk.BookNo = bookNo;

                        if (func.IsNeedSend)
                            bk.BookState = BookState.UnSend;
                        else
                            bk.BookState = BookState.Send;


                        bk.FK_NodeRefFunc = func.No;
                        bk.FilePrix = func.No;
                        bk.RDT = DataType.CurrentDataTime;
                        //string fk_flowSort = this.HisNode.HisFlow.FK_FlowSort;
                        //dt = dt.AddDays(func.TimeLimit);
                        bk.ShouldSendDT = dt.ToString("yyyy-MM-dd");
                        bk.FileName = file;
                        bk.BookName = func.Name;
                        try
                        {
                            bk.Insert();
                        }
                        catch
                        {
                            bk.Update();
                        }

                        #region 生成文书回证
                        if (func.IsNeedSend)
                        {
                            //func.IDX = bookNo;
                            ///* 如果需要送 */
                            //file = year + "_" + WebUser.FK_Dept + "_" + func.No + "_" + workid + ".doc";
                            //rtf.AddEn(func);

                            //paths = file.Split('_');
                            //path = paths[0] + "/" + paths[1] + "/" + paths[2] + "/";

                            //bookInfo += "<img src='/" + SystemConfig.AppName + "/Images/Btn/Word.gif' /><a href='/FlowFile/" + path + file.Replace(".doc", "HZ.doc") + "' target=_blank >回证</a>";

                            ////path = BP.WF.Glo.FlowFileBook  + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";
                            ////rtf.MakeDoc("/回证.rtf",
                            ////    path, file.Replace(".doc", "HZ.doc"), false);
                        }
                        #endregion

                    } // end 生成循环文书。

                    if (bookInfo != "")
                        bookInfo = "@Book:" + bookInfo;
                    msg += bookInfo;
                }
                #endregion


                #region 处理当前人员的考核问题
                if (SystemConfigOfTax.IsAutoGenerCHOfNode == true)
                {
                    this.DTSDataToChOfNode(this);
                    int i = 0;
                    CHOfNodes cns = new CHOfNodes();
                    int num = cns.Retrieve(CHOfNodeAttr.WorkID, this.HisWork.OID,
                        CHOfNodeAttr.FK_Node, this.HisNode.NodeID);

                    if (num == 0)
                    {
                        Log.DebugWriteError("@不可能出现的情况，因为已经做过了调度。");
                        //cns.AddEntity(this.DTSDataToChOfNode(this));
                        num = 1;
                        //  throw new Exception("不可能出现的情况。");
                    }

                    if (num > 1)
                    {
                        /* 说明已经加工过这笔数据 ， 就执行给当前操作员加分。*/
                        //更新完成日期 与 节点状态。
                        i = DBAccess.RunSQL("UPDATE WF_CHOfNode SET CDT='" + DataType.CurrentDataTime + "',NodeState=1, CentOfAdd=0 WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID='" + this.HisWork.OID + "'");
                        if (i == 0)
                        {
                            Log.DebugWriteError("@不应该运行到这里。");
                            //  this.DTSDataToChOfNode(this);
                        }

                        //  给当前操作人员加分。 
                        i = DBAccess.RunSQL("UPDATE WF_CHOfNode SET NodeSwinkCent=" + this.HisNode.SwinkCent + " WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.HisWork.OID + " AND FK_Emp='" + Web.WebUser.No + "'");
                        if (i == 0)
                            Log.DebugWriteError("@不可能出现的情况，没有给当前人员加分。");
                    }

                    //  Node nd = this.HisNode;
                    // 执行扣分。 如果应完成时间超过了当前系统时间,那么给其扣分。
                    // DBAccess.RunSQL("UPDATE WF_CHOfNode SET CentOfCut='" + this.HisNode.DeductCent + "', Cent=CentofCut+CentOfQu+CentOfAdd  WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID='" + this.HisWork.OID + "' AND FK_Node='" + this.HisNode.NodeID + "' AND SUBSTR(SDT,0,11)<'" + DataType.CurrentData + "' ");
                    foreach (CHOfNode cn in cns)
                    {
                        if (cn.SpanDays > 0)
                        {
                            cn.CentOfCut = cn.NodeDeductCent * (cn.SpanDays - this.HisNode.NeedCompleteDays);
                            if (cn.CentOfCut > cn.NodeMaxDeductCent)
                                cn.CentOfCut = cn.NodeMaxDeductCent;

                            cn.Note = this.ToE("WN1", "未按期完成工作。"); //"未按期完成工作。"; // 扣分原因
                            if (cn.CentOfCut <= 0)
                                cn.CentOfCut = 0;
                        }
                        cn.Cent = cn.CentOfCut + cn.CentOfAdd;
                        cn.Update();
                    }
                    DBAccess.RunSQL("UPDATE WF_CHOfNode SET CDT='" + DataType.CurrentDataTime + "', FK_NY='" + DataType.CurrentYearMonth + "', NodeState=1, CentOfAdd=" + this.HisNode.SwinkCent + "  WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.HisWork.OID + " AND FK_Emp='" + WebUser.No + "'");
                }
                #endregion

                this.HisWork.DoCopy(); // copy 本地的数据到指定的系统.
                return msg;
            }
            catch (Exception ex)  // 如果抛出异常，说明没有正确的执行。当前的工作，不能完成。工作流程不能完成。
            {
                WorkNode wn = this.HisWorkFlow.HisStartWorkNode;
                //wn.HisWork.SetValByKey("WFState",0);
                wn.HisWork.Update("WFState", (int)WFState.Runing);
                this.HisWork.NodeState = NodeState.Init;
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Init);

                // 更新当前的节点信息。
                this.HisGenerWorkFlow.Update(GenerWorkFlowAttr.FK_Node, this.HisNode.NodeID);
                this.RollbackCHOfNode(this.HisNode.HisToNodes);
                throw ex;
            }
        }
        private void RollbackCHOfNode(Nodes toNodes)
        {
            if (SystemConfigOfTax.IsAutoGenerCHOfNode == false)
                return;

            foreach (Node nd in toNodes)
            {
                DBAccess.RunSQL("DELETE WF_CHOFNODE WHERE WorkID=" + this.HisWork.OID + " AND FK_Node=" + nd.NodeID);

             //   DBAccess.RunSQL("DELETE WF_CHOFNODE WHERE WorkID=@wORKid" + this.HisWork.OID + " AND FK_Node=" + nd.NodeID,);

            }
            return;

            // 更新当前节点的考核数据。
            CHOfNode chn = new CHOfNode();
            chn.WorkID = this.HisWork.OID;
            chn.FK_Node = this.HisNode.NodeID;
            chn.FK_Emp = Web.WebUser.No;

            int num = chn.Retrieve(CHOfNodeAttr.WorkID, this.HisWork.OID,
                CHOfNodeAttr.FK_Node, this.HisNode.NodeID,
                CHOfNodeAttr.FK_Emp, Web.WebUser.No);

            /*
            if (num == 0)
                throw new Exception("不可能出现。");

            if (num > 1)
                throw new Exception("不可能出现。num > 1 ");
             * */

            chn.CentOfAdd = 0;
            chn.CDT = " ";

            //if (chn.SDT <DataType.CurrentData)
            //	chn.ch
            chn.CentOfCut = 0;
            chn.Update();

            //调度到此数据到 考核系统表中。
            //this.DTSDataToChOfNode(this);
        }
        /// <summary>
        /// 加入工作记录
        /// </summary>
        /// <param name="gwls"></param>
        public void AddIntoWacthDog(WorkerLists gwls)
        {
            return;
            /*
            string workers="";
            // 工作者
            foreach(WorkerList wl in gwls)
            {
                workers+=","+wl.FK_Emp;
            }
            Watchdog wd =new Watchdog();
            wd.InitDateTime=DataType.CurrentDataTime ;
            wd.WorkID=this.HisWork.OID;
            wd.NodeId =this.HisNode.OID;
            wd.Workers = workers+",";
            wd.FK_Dept =this.HisDeptOfUse.No ;
            wd.FK_Station=this.HisStationOfUse.No;
            wd.Save();
            */

        }
        /// <summary>
        /// 会签节点是否全部完成？
        /// </summary>
        private bool IsOverMGECheckStand = false;
        /// <summary>
        /// 调用审核节点任务完成
        /// </summary>		 
        private string AfterWorkOfCheckSave()
        {
            // 调用检查全局的工作.
            string msg = this.CheckGlobalCompleteCondition();
            if (msg != null)
                return msg;

            GECheckStand en = this.HisWork as GECheckStand;
            if (this.HisNode.HisNodeWorkType == NodeWorkType.GECheckMuls)
            {

            }
            else
            {
                return AfterWorkOfCheckSave_Stand(en);
            }

            WorkerLists ens = new WorkerLists();
            QueryObject qo = new QueryObject(ens);
            qo.AddWhere(WorkerListAttr.WorkID, this.WorkID);
            qo.addAnd();
            qo.AddWhere(WorkerListAttr.FK_Node, en.NodeID);
            qo.addAnd();
            qo.AddWhere(WorkerListAttr.IsEnable, true);
            qo.DoQuery();

            if (ens.Count == 1)
                return AfterWorkOfCheckSave_Stand(en);

            // 处理我的当前状态。
            foreach (WorkerList wl in ens)
            {
                if (wl.FK_Emp != WebUser.No)
                    continue;

                wl.IsPass = true;
                wl.Update(WorkerListAttr.IsPass, 1);
            }

            string msgInfo = "";
            foreach (WorkerList wl in ens)
            {
                if (wl.IsPass == true)
                    continue;

                msgInfo += wl.FK_EmpText + "、";
            }


            if (msgInfo.Length > 2)
            {
                IsOverMGECheckStand = false;
                msgInfo = "@当前工作[" + this.HisNode.Name + "]已经完成。@在此是会签节点，下列人员没有处理工作：" + msgInfo + "。@需要等待他们都处理完成后才能执行下一步骤。";
                return msgInfo;
            }
            else
            {
                IsOverMGECheckStand = true;
                return "@当前工作[" + this.HisNode.Name + "]是会签节点，所有的人员都已经签署通过完毕。" + this.StartupNewNodeWork();
            }
        }
        private string AfterWorkOfCheckSave_Stand(GECheckStand en)
        {
            switch (en.CheckState)
            {
                case CheckState.Agree:  //如果是同意.
                    if (this.HisNode.IsEndNode) // 如果是最后的节点
                        return this.HisWorkFlow.DoFlowOver(); // 设置他的流程结束.
                    else // 如果不是最后节点.
                        return this.StartupNewNodeWork();   //启动新的节任务.
                    break;
                case CheckState.Dissent: // 如果是不同意, 就设置此工作已经结束.
                    return this.HisWorkFlow.DoFlowOver();
                    break;
                case CheckState.Pause: // 如果是挂起.
                    return "审核[" + this.HisNode.Name + "]挂起.";
                    break;
                default:
                    throw new Exception("@节的[" + this.HisNode.Name + "]状态错误.");
            }
        }
        /// <summary>
        /// 检查流程、节点的完成条件
        /// </summary>
        /// <returns></returns>
        private string CheckCompleteCondition()
        {
            // 调用检查全局的工作.
            string msg = this.CheckGlobalCompleteCondition();
            if (msg != null)
                return msg;

            #region 判断节点完成条件
            try
            {
                // 如果没有条件,就说明了,保存为完成节点任务的条件.
                if (this.HisNode.IsCCNode == false)
                {
                    msg = string.Format(this.ToE("WN6", "当前工作[{0}]已经完成"), this.HisNode.Name);
                }
                else
                {
                    int i = this.HisNodeCompleteConditions.Count;
                    if (i == 0)
                    {
                        this.HisNode.IsCCNode = false;
                        this.HisNode.Update();
                    }

                    if (this.HisNodeCompleteConditions.IsPass)
                    {
                        if (SystemConfig.IsDebug)
                            msg = "@当前工作[" + this.HisNode.Name + "]符合完成条件[" + this.HisNodeCompleteConditions.ConditionDesc + "],已经完成.";
                        else
                            msg = string.Format(this.ToE("WN6", "当前工作{0}已经完成"), this.HisNode.Name);  //"@"; //当前工作[" + this.HisNode.Name + "],已经完成.
                    }
                    else
                    {
                        // "@当前工作[" + this.HisNode.Name + "]没有完成,下一步工作不能启动."
                        throw new Exception(string.Format(this.ToE("WN7", "@当前工作{0}没有完成,下一步工作不能启动."), this.HisNode.Name));
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(this.ToE("WN8", "@判断节点{0}完成条件出现错误.") + ex.Message, this.HisNode.Name)); //"@判断节点[" + this.HisNode.Name + "]完成条件出现错误:" + ex.Message;
            }
            #endregion

            #region 判断流程条件.
            try
            {
                if (this.HisNode.IsCCFlow && this.HisFlowCompleteConditions.IsPass)
                {
                    /* 如果流程完成 */
                    string overMsg = this.HisWorkFlow.DoFlowOver();
                    string path = System.Web.HttpContext.Current.Request.ApplicationPath;
                    return msg + "@符合工作流程完成条件[" + this.HisFlowCompleteConditions.ConditionDesc + "]" + overMsg + " @查看<img src='" + path + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + path + "/WF/WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >工作报告</a>";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(this.ToE("WN9", "@判断流程{0}完成条件出现错误.") + ex.Message, this.HisNode.Name));
            }
            #endregion

            return msg;
        }
        /// <summary>
        /// 启动新的节点
        /// </summary>
        /// <returns></returns>
        public string StartupNewNodeWork()
        {
            string msg = this.CheckCompleteCondition();
            // 取当前节点Nodes.
            Nodes toNodes = this.HisNode.HisToNodes;
            if (toNodes.Count == 0)
            {
                /* 如果是最后一个节点，就设置流程结束。*/
                string ovrMsg = this.HisWorkFlow.DoFlowOver();
                return ovrMsg + this.ToE("WN0", "@此工作流程运行到最后一个环节，工作成功结束！") + "<img src='./../../Images/Btn/PrintWorkRpt.gif' ><a href='./../../WF/WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "&FK_Node=" + this.HisNode.NodeID + "'target='_blank' >" + this.ToE("WorkRpt", "工作报告") + "</a>";
            }

            // 如果只有一个转向节点, 就不用判断条件了,直接转向他.
            if (toNodes.Count == 1)
                return StartNextNode((Node)toNodes[0]);

            int toNodeId = 0;
            int numOfWay = 0;
            foreach (Node nd in toNodes)
            {
                Conds dcs = new Conds();
                QueryObject qo = new QueryObject(dcs);
                qo.AddWhere(CondAttr.NodeID, this.HisNode.NodeID);
                qo.addAnd();
                qo.AddWhere(CondAttr.ToNodeID, nd.NodeID );
                //qo.addAnd();
                //qo.AddWhere(CondAttr.CondType, (int)CondType.Node);
                qo.DoQuery();
                foreach (Cond dc in dcs)
                {
                    dc.WorkID = this.HisWork.OID;
                }


                if (dcs.Count == 0)
                {
                    throw new Exception(string.Format(this.ToE("WN10", "@定义节点的方向条件错误:没有给从{0}节点到{1},定义转向条件."), this.HisNode.NodeID + this.HisNode.Name, nd.NodeID + nd.Name));
                    //throw new Exception("@定义节点的方向条件错误:没有给从[" + this.HisNode.NodeID + this.HisNode.Name + "]节点到[" + nd.NodeID + nd.Name + "],定义转向条件");
                }

                if (dcs.IsPass) // 如果多个转向条件中有一个成立.
                {
                    numOfWay++;
                    toNodeId = nd.NodeID;
                    //msg += "@符合流程转向节点[<font color=blue>" + nd.Name + "</font>]的条件[<font color=blue>" + dcs.ConditionDesc + "</font>]" ;
                    msg += StartNextNode(nd);
                    if (SystemConfig.IsDebug)
                    {
                        if (numOfWay > 1)
                            throw new Exception("@您制定的方向条件有矛盾，在一个流程上的路径不唯一，请仔细检查您的方向条件设置的取值范围。");
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (toNodeId == 0)
                throw new Exception(string.Format(this.ToE("WN11", "@转向条件设置错误:节点名称{0}, 系统无法投递。"), this.HisNode.Name));

            /* 删除曾经在这个步骤上的流程运行数据。
             * 比如说：方向条件，发生了变化后可能产生两个工作上的数据。是为了工作报告上面体现了两个步骤。 */
            foreach (Node nd in toNodes)
            {
                if (nd.NodeID == toNodeId)
                    continue;

                // 删除这个工作，因为这个工作的数据不在有用了。
                Work wk = nd.HisWork;
                wk.OID = this.HisWork.OID;
                if (wk.IsGECheckStand)
                    wk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);

                if (wk.Delete() != 0)
                {
                    // 删除这个工作的考核信息。
                    if (SystemConfigOfTax.IsAutoGenerCHOfNode)
                    {
                        CHOfNode cn = new CHOfNode();
                        cn.WorkID = this.HisWork.OID;
                        cn.FK_Node = nd.NodeID;
                        cn.Delete();
                    }
                }
            }
            return msg;
        }

        #region 启动审核节点
        /// <summary>
        /// 启动指定的下一个节点...
        /// </summary>
        /// <param name="nd">要启动的节点</param>
        public string StartNextNode(Node nd)
        {
            try
            {
                // 工作完成之后要做的事情。
                switch (nd.HisNodeWorkType)
                {
                    case NodeWorkType.GECheckMuls:
                    case NodeWorkType.NumChecks:
                    case NodeWorkType.GECheckStands:
                        return StartNextCheckNode(nd); /* 审核节点 */
                    case NodeWorkType.WorkHL:
                        return StartNextWorkNodeHeLiu(nd);  /* 合流节点 */
                    default:
                        return StartNextWorkNodeOrdinary(nd);  /* 普通节点 */
                }
                this.InitEmps(nd);
            }
            catch (Exception ex)
            {
                throw new Exception("@" + this.ToE("StartNextNodeErr", "@启动下一个节点出现错误") + ":" + ex.Message); //启动下一个节点出现错误
            }
        }
        /// <summary>
        /// InitEmps
        /// </summary>
        /// <param name="nd"></param>
        public void InitEmps(Node nd)
        {
            return;

            #region 更新每个节点表里的接收人集合（Emps）。
            try
            {
                int i = 0;
                if (nd.IsCheckNode)
                {
                    //更新标准审核节点的Emps
                    i = DBAccess.RunSQL("update " + nd.PTable + " set Emps='" + this.GenerEmps(nd) + "' where NodeID='" + nd.NodeID + "'  and oid='" + this.HisWork.OID + "'");
                }
                else
                {
                    //更新普通节点的Emps
                    i = DBAccess.RunSQL("update " + nd.PTable + " set Emps='" + this.GenerEmps(nd) + "' where oid='" + this.HisWork.OID + "'");
                }
            }
            catch (Exception ex)
            {
                nd.HisWork.CheckPhysicsTable();
                throw new Exception("@更新Emps时出现一下错误：<br>" + ex.Message);
            }
            #endregion

            #region 工作发送后向WF_CHOFNODE插入一条数据,
            if (SystemConfigOfTax.IsAutoGenerCHOfNode == false)
                return;

            // 删除可能出现的 垃圾数据。
            //CHOfNodes cns = new CHOfNodes();
            //cns.Delete(CHOfNodeAttr.FK_Node,nd.NodeID.ToString(),
            //    CHOfNodeAttr.WorkID,this.HisWork.OID.ToString());

            Work wk = nd.HisWork;
            Emp emp = wk.HisRec;
            //Node nd = new Node(); //wn.HisNode;
            string msg = "";
            if (SystemConfigOfTax.IsAutoGenerCHOfNode == true)
            {
                CHOfNode cn = new CHOfNode();
                cn.WorkID = wk.OID;
                cn.FK_Node = nd.NodeID;
                cn.RDT = wk.RDT;
                cn.CDT = " ";
                cn.SDT = DataType.AddDays(wk.RDT, nd.NeedCompleteDays).ToString(DataType.SysDataFormat);
                cn.FK_NY = wk.Record_FK_NY;
                cn.FK_Emp = wk.RecOfEmp.No;
                cn.FK_Station = HisStationOfUse.No;//wn.HisStationOfUse.No;
                cn.FK_Dept = HisDeptOfUse.No;
                cn.NodeState = wk.NodeState;
                cn.FK_Dept = emp.FK_Dept;
                cn.FK_Flow = nd.FK_Flow;
                cn.SpanDays = DataType.SpanDays(wk.RDT, wk.CDT);
                cn.Note = " "; //
                //cn.Emps = wk.Emps ; //Emps
                cn.Emps = this.GenerEmps(nd).ToString();

                //只要在某个节点里出现此字段：Spec,那么就把Spec里的内容写入WF_CHOfNode表里
                if (wk.EnMap.Attrs.Contains("SPEC"))
                    cn.Spec = wk.GetValStringByKey("SPEC");

                cn.CentOfAdd = 0; //工作量得分。
                cn.NodeDeductCent = nd.DeductCent;
                cn.NodeDeductDays = nd.DeductDays;
                cn.NodeMaxDeductCent = nd.MaxDeductCent;
                if (wk.EnMap.Attrs.Contains("FK_Taxpayer"))
                {
                    cn.FK_Taxpayer = wk.GetValStringByKey("FK_Taxpayer");
                    if (wk.EnMap.Attrs.Contains("TaxpayerName"))
                        cn.TaxpayerName = wk.GetValStringByKey("TaxpayerName");
                }

                cn.CentOfCut = 0;
                //cn.CentOfQU=0;
                cn.Cent = 0;
                cn.Insert();
            }
            #endregion
        }
        /// <summary>
        /// 在启动下个工作要做的工作。
        /// </summary>
        /// <param name="wk">要启动的工作</param>
        /// <param name="nd">要启动的节点。</param>
        /// <returns>启动的信息</returns>
        private string beforeStartNode(Work wk, Node nd)
        {
            WorkNode town = new WorkNode(wk, nd);

            // 初试化他们的工作人员．
            WorkerLists gwls = this.GenerWorkerLists(town);


            this.AddIntoWacthDog(gwls); //@加入消息集合里。
            string msg = "";
            //msg = "@任务自动下达给'<font color=blue><b>" + this.nextStationName + "</b></font>'如下" + this._RememberMe.NumOfObjs + "位同事：@" + this._RememberMe.EmpsExt;
            //msg = "@" + this.ToE("TaskAutoSendTo") + " <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ：@" + this._RememberMe.EmpsExt;
            if (nd.HisNodeWorkType == NodeWorkType.GECheckMuls)
            {
                msg = this.ToEP3("TaskAutoSendToM", "@任务自动下达给{0}如下{1}位同事,{2}.需要等待他们全部处理完才能执行下一步骤。<a href=\"javascript:WinOpen('WFRpt.aspx?WorkID=" + this.WorkID + "&FK_Flow=" + nd.FK_Flow + "&FID=0')\" >如果当前的操作有错误，您可以撤销它。</a>", this.nextStationName, this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);  // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ：@" + this._RememberMe.EmpsExt;
                msg += this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID);
            }
            else
            {
                msg = this.ToEP3("TaskAutoSendTo", "@任务自动下达给{0}如下{1}位同事,{2}.", this.nextStationName, this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);  // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ：@" + this._RememberMe.EmpsExt;
                if (this._RememberMe.NumOfEmps >= 2)
                {
                    msg += "<a href=\"javascript:WinOpen('AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "&FK_Flow=" + nd.FK_Flow + "')\"><img src='./Img/AllotTask.gif' border=0/>指定特定的同事处理</a>。";
                }
                msg += this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID);
            }

            msg += "@<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "');\" ><img src='./Img/SMS.gif' border=0 />发手机短信提醒</a>";

            if (this.HisNode.IsStartNode)
                msg += "@<a href='MyFlowInfo.aspx?DoType=UnSend&WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'><img src=./Img/UnDo.gif border=0/>撤销本次发送</a>， <a href='MyFlow.aspx?FK_Flow=" + nd.FK_Flow + "'>新建流程</a>。";
            else
                msg += "@<a href='MyFlowInfo.aspx?DoType=UnSend&WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'><img src=./Img/UnDo.gif border=0/>撤销本次发送</a>。";

            if (nd.IsCheckNode)
            {
                foreach (WorkerList wl in gwls)
                {
                    break;
                    /* 去掉自动审核的需求。*/
                    wk.Rec = wl.FK_Emp;
                    /* 如果是审核节点 */
                    //if (    wk.RecOfEmp.IsAuthorizedTo(this.HisWork.Rec))
                    //{
                    //    /* 如果，要启动的工作节点人员授权给当前的的工作人员。就直接pass. */
                    //    //wk.Rec = 
                    //    wk.SetValByKey(GECheckStandAttr.CheckState, (int)CheckState.Agree);
                    //    wk.DirectSave();
                    //    WorkNode mywn = new WorkNode(wk, nd);
                    //    //wk.SetValByKey(GECheckStandAttr.CheckState, (int)CheckState.Agree);
                    //    try
                    //    {
                    //        msg += "@<b>自动授权审批</b>信息如下:" + mywn.AfterNodeSave(false, true, DateTime.Now); // 直接启动下一步工作。直接审核。
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        msg += "@<font color=red>自动授权审批出现如下错误:</font>" + ex.Message;
                    //    }
                    //    break;
                    //}
                }
            }
            else
            {
                /* 形成消息通知他们 */
                if (nd.IsPCNode == false && SystemConfig.IsBSsystem)
                {
                    MsgsManager.AddMsgs(gwls, town.HisNode.FlowName, nd.Name, "Test");
                }
            }

            string appPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            string str = "";
            //if (this._RememberMe.NumOfEmps > 1)
            //    str = "<a href='" + appPath + "/WF/AllotTask.aspx?NodeId=" + nd.NodeID + "&WorkID=" + wk.OID + "&FlowNo=" + nd.FK_Flow + "&dt='" + DateTime.Now.ToString("MMddhhmmss") + " target='_self' >" + this.ToE("AllotWork", "分配工作") + "</a>";

            //return msg + "@您可以" + str + "查看<img src='" + appPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + appPath + "/WF/WFRpt.aspx?WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'target='_blank' >工作报告</a>,进行<img src='" + appPath + "/Images/Btn/WorkFlowOp.gif' ><a href='" + appPath + "/WF/OpWorkFlow.aspx?WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "' target='_blank' >流程操作</a> 。";
            return msg + "@" + str + " <img src='" + appPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + appPath + "/WF/WFRpt.aspx?WorkID=" + wk.OID + "&FID=" + wk.FID + "&FK_Flow=" + nd.FK_Flow + "'target='_blank' >" + this.ToE("WorkRpt", "工作报告") + "</a>。";
        }
        /// <summary>
        /// 启动下一个审核节点
        /// </summary>
        /// <param name="nd">工作节点</param>
        private string StartNextCheckNode(Node nd)
        {
            GECheckStand wk = (GECheckStand)nd.HisWork;
            try
            {
                //wk.MyPK = nd.NodeID + "_" + this.HisWork.OID;
                wk.OID = this.HisWork.OID;
                wk.NodeID = nd.NodeID;
                wk.NodeState = NodeState.Init;
                wk.CheckState = CheckState.Agree;//默认是同意状态，因为没有不同意和挂起状态，默认传递同意状态
                wk.Rec = BP.Web.WebUser.No;

                try
                {
                    wk.Insert();
                }
                catch
                {
                    wk.Update();
                }

                string msg = this.beforeStartNode(wk, nd);

                if (wk.OID == BP.WF.WFPubClass.DefaultWorkID)
                {
                    /*正常的指定传递．*/
                    string refMsg = BP.WF.WFPubClass.DefaultRefMsg;
                    if (refMsg == "")
                    {
                        /* 如果当前节点没有辅助信息，就取上一个节点的辅助信息。*/
                        if (this.HisNode.IsCheckNode)
                        {
                            wk.RefMsg = this.HisWork.GetValStringByKey("RefMsg");
                        }
                    }
                    wk.RefMsg = refMsg;

                }

                if (this.HisNode.IsCheckNode)
                {
                    /* 如果当前的节点是 checkNode */
                    GECheckStand mych = (GECheckStand)this.HisWork;
                    wk.RefMsg = mych.RefMsg + "\n --- " + this.HisWork.RecText + " , " + this.HisWork.RDT + this.ToE("CheckNoteD", "审批意见如下：") + ":\n" + mych.Note + "\n\n";
                }


                wk.Sender = Web.WebUser.No;
                wk.NodeState = NodeState.Init; //节点状态。
                if (nd.HisNodeWorkType == NodeWorkType.GECheckMuls)
                {
                    WorkerLists wls = new WorkerLists();
                    wls.Retrieve(WorkerListAttr.FK_Node, nd.NodeID, WorkerListAttr.WorkID, wk.OID);
                    foreach (WorkerList wl in wls)
                    {
                        GECheckMul mc = new GECheckMul();
                        mc.OID = this.HisWork.OID;
                        mc.NodeID = nd.NodeID;
                        mc.NodeState = NodeState.Init;
                        mc.CheckState = CheckState.Agree;//默认是同意状态，因为没有不同意和挂起状态，默认传递同意状态
                        mc.Rec = wl.FK_Emp;
                        mc.RefMsg = wk.RefMsg;
                        mc.Sender = WebUser.No;
                        try
                        {
                            mc.Insert();
                        }
                        catch
                        {
                            mc.Update();
                        }
                    }

                    wk.Delete();
                }
                else
                {
                    if (wk.Update() == 0)
                        wk.Insert();
                }
                return "@<font color=blue>" + nd.Name + "</font> " + this.ToE("WN12", "审核工作成功启动") + "." + msg;
            }
            catch (Exception ex)
            {
                wk.CheckPhysicsTable();
                throw new Exception("@" + this.ToE("WN13", "启动审核节点任务失败") + ex.Message); //启动审核节点任务失败
            }
        }
        public string GenerWhySendToThem(int fNodeID, int toNodeID)
        {
            return "@<a href='../../WF/WhySendToThem.aspx?NodeID=" + fNodeID + "&ToNodeID=" + toNodeID + "&WorkID=" + this.WorkID + "' target=_blank >为什么要发送给他们？</a>";
        }
        /// <summary>
        /// 工作流程ID
        /// </summary>
        public static Int64 FID = 0;
        private string StartNextWorkNodeHeLiu(Node nd)
        {
            BP.Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + nd.NodeID);
            if (this.HisWork.FID == 0 || this.HisWork.FID == this.HisWork.OID)
            {
                /* 没有FID */
            }
            else
            {
                /* 已经有FID，说明：以前已经有分流或者合流节点。*/
                GenerFH myfh = new GenerFH(this.HisWork.FID);
                if (myfh.FK_Node == nd.NodeID)
                {
                    // 更新它的节点 worklist 信息。
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET FID=" + myfh.FID + " WHERE WorkID=" + this.WorkID);

                    if (nd.HisFJOpen != FJOpen.None)
                        DBAccess.RunSQL("UPDATE WF_FileManager SET FID=" + myfh.FID + " WHERE WorkID=" + this.WorkID);


                    // 开始更新明细的权限。
                    foreach (BP.Sys.MapDtl dtl in dtls)
                    {
                        DBAccess.RunSQL("Update " + dtl.PTable + " SET FID=" + myfh.FID + " WHERE RefPK='" + this.HisWork.OID + "'");
                    }
                    return "@流程已经运行到合流节点[" + nd.Name + "]，当前工作已经完成。@您的工作已经发送给如下人员[" + myfh.ToEmpsMsg + "]，<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "')\" >短信通知他们</a>。" + this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID);
                }
            }


            //首先找到此节点的接受人员的集合。做为 FID 合流分流的FID。
            WorkNode town = new WorkNode(nd.HisWork, nd);

            // 初试化他们的工作人员．
            WorkerLists gwls = this.GenerWorkerLists(town);

            string groupKeys = "";
            string fk_emp = "";
            string toEmpsStr = "";
            foreach (WorkerList wl in gwls)
            {
                groupKeys += "@" + wl.FK_Emp;
                fk_emp = wl.FK_Emp;
                if (Glo.IsShowUserNoOnly)
                    toEmpsStr += wl.FK_Emp + "、";
                else
                    toEmpsStr += wl.FK_Emp + "<" + wl.FK_EmpText + ">、";
            }

            GenerFH fh = new GenerFH();
            // 根据 groupKeys 能否查询到这个FID 。
            int resu = fh.Retrieve(GenerFHAttr.FK_Node, nd.NodeID, GenerFHAttr.GroupKey, groupKeys);
            if (resu == 0)
            {
                /* 如果以前的节点没有产生FID ，这个节点第一次到达。*/
                fh.FID = BP.DA.DBAccess.GenerOID();
                fh.WFState = (int)WFState.Runing;
                fh.GroupKey = groupKeys;
                fh.FK_Flow = nd.FK_Flow;
                fh.FK_Node = nd.NodeID;
                fh.ToEmpsMsg = toEmpsStr;
                fh.Title = "在" + DataType.CurrentData + " 接受的工作." + nd.Name;
                fh.RDT = DataType.CurrentData;
                fh.Insert();

                // 产生工作流程记录
                GenerWorkFlow gwf = new GenerWorkFlow();
                gwf.WorkID = fh.FID;
                gwf.FID = fh.FID;
                gwf.Title = fh.Title;
                gwf.WFState = 0;
                gwf.RDT = fh.RDT;
                gwf.Rec = Web.WebUser.No;
                gwf.FK_Flow = this.HisNode.FK_Flow;
                gwf.FK_Node = nd.NodeID;
                // gwf.FK_Station = this.HisStationOfUse.No;
                gwf.FK_Dept = this.HisWork.RecOfEmp.FK_Dept;
                try
                {
                    gwf.DirectInsert();
                }
                catch
                {
                    gwf.DirectUpdate();
                }



                /* 产生合流工作的人员 */
                // 更新此前所有节点工作的FID。

                if (this.HisWork.FID == 0)
                {
                    /*如果FID=0，说明以前的N个节点都没有产生过 FID，把FID 更新到前面去。*/
                    this.HisWork.Update(WorkAttr.FID, fh.FID);

                    // 更新以前节点的 FID。
                    Nodes nds = new Nodes(this.HisNode.FK_Flow);
                    foreach (Node mynd in nds)
                    {
                        if (mynd.NodeID == this.HisNode.NodeID || mynd.NodeID == nd.NodeID)
                            continue;

                        if (mynd.IsEndNode)
                            continue;

                        Work wk1 = mynd.HisWork;
                        wk1.OID = this.WorkID;
                        if (wk1.IsGECheckStand)
                            wk1.SetValByKey(GECheckStandAttr.NodeID, mynd.NodeID);

                        wk1.Update(WorkAttr.FID, fh.FID);
                    }
                }



                /* 更新 WF_GenerWorkFlow WF_WorkerList */
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FID=" + fh.FID + "   WHERE WorkID=" + this.WorkID);
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET FID=" + fh.FID + " WHERE WorkID=" + this.WorkID);
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET FID=" + fh.FID + ",WorkID=" + fh.FID + " WHERE WorkID=" + this.WorkID + " AND FK_Node=" + nd.NodeID);

                if (nd.HisFJOpen != FJOpen.None)
                    DBAccess.RunSQL("UPDATE WF_FileManager SET FID=" + fh.FID + " WHERE WorkID=" + this.WorkID);



                // 产生当前节点的工作记录.
                Work wk = nd.HisWork;
                wk.NodeID = nd.NodeID;
                wk.OID = fh.FID;
                wk.FID = fh.FID;
                wk.Rec = fk_emp;
                try
                {
                    wk.Insert();
                }
                catch
                {

                }


                // 开始更新明细的权限问题。
                foreach (BP.Sys.MapDtl dtl in dtls)
                {
                    DBAccess.RunSQL("Update " + dtl.PTable + " SET FID=" + fh.FID + " WHERE RefPK='"   + this.WorkID + "'");
                }

                return "@当前工作已经完成，流程已经运行到合流节点[" + nd.Name + "]。您是第一位到达此节点的人员。@您的工作已经发送给如下人员[" + toEmpsStr + "]，<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID="+this.WorkID+"&NodeID="+nd.NodeID+"')\" >短信通知他们</a>，";
            }
            else
            {
                /* 删除已经产生的人员集合，因为遇到合流已经没有作用，前期已经分配过了。不删除就会出现重复的问题。*/
                BP.DA.DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE WorkID=" + this.WorkID + " AND FK_Node=" + nd.NodeID);


                /* 更新 WF_GenerWorkFlow WF_WorkerList */
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FID=" + fh.FID + "   WHERE WorkID=" + this.WorkID);
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET FID=" + fh.FID + " WHERE WorkID=" + this.WorkID);

                if (nd.HisFJOpen != FJOpen.None)
                    DBAccess.RunSQL("UPDATE WF_FileManager SET FID=" + fh.FID + " WHERE WorkID=" + this.WorkID);



                if (this.HisWork.FID == 0)
                {
                    /*如果FID=0，说明以前的N个节点都没有产生过 FID，把FID 更新到前面去。*/
                    this.HisWork.Update(WorkAttr.FID, fh.FID);

                    // 更新以前节点的 FID。
                    Nodes nds = new Nodes(this.HisNode.FK_Flow);
                    foreach (Node mynd in nds)
                    {
                        if (mynd.NodeID == this.HisNode.NodeID || mynd.NodeID == nd.NodeID)
                            continue;

                        if (mynd.IsEndNode)
                            continue;

                        Work wk = mynd.HisWork;
                        wk.OID = this.WorkID;
                        if (wk.IsGECheckStand)
                            wk.SetValByKey(GECheckStandAttr.NodeID, mynd.NodeID);

                        wk.Update(WorkAttr.FID, fh.FID);
                    }
                }

                /* 如果 产生了 FID，就更新当前的状态 让其返回。*/
                //fh.FID = this.HisWork.FID;
                //fh.Retrieve();
                //fh.FK_Node = nd.NodeID;
                //fh.GroupKey = groupKeys;
                //fh.Update();

                GenerWorkFlow mygwf = new GenerWorkFlow();
                mygwf.WorkID = this.WorkID;
                int i = mygwf.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("系统异常");

                mygwf.FK_Node = nd.NodeID;
                mygwf.Update(GenerWorkFlowAttr.FK_Node, nd.NodeID);

                // 开始更新明细的权限问题。
                foreach (BP.Sys.MapDtl dtl in dtls)
                {
                    DBAccess.RunSQL("Update " + dtl.PTable + " SET FID=" + fh.FID + " WHERE RefPK='"  + this.WorkID + "'");
                }

                return "@当前工作已经完成，流程已经运行到合流节点[" + nd.Name + "]。您不是第一个到达此节点的人员。@您的工作已经发送给如下人员[" + toEmpsStr + "]，<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "')\" >短信通知他们</a>。";
            }
        }
        private void UpdateHeLiuFID()
        {
        }
        /// <summary>
        /// 启动下一个工作节点
        /// </summary>
        /// <param name="nd">节点</param>		 
        /// <returns></returns>
        private string StartNextWorkNodeOrdinary(Node nd)
        {
            string sql = "";
            try
            {
                #region  初始化发起的工作节点。
                Work wk = nd.HisWork;
                wk.SetValByKey("OID", this.HisWork.OID); //设定它的ID.
                wk.Copy(this.HisWork); // 执行copy 上一个节点的数据。
                wk.Rec = "";
                wk.NodeState = NodeState.Init; //节点状态。
                wk.Rec = BP.Web.WebUser.No;
                try
                {
                    wk.Insert();
                }
                catch
                {
                }

                // 复制明细数据。
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + this.HisNode.NodeID);
                Sys.MapDtls toDtls = new BP.Sys.MapDtls("ND" + nd.NodeID);

                int i = -1;
                foreach (Sys.MapDtl dtl in dtls)
                {
                    i++;
                    if (toDtls.Count < i)
                        continue;

                    Sys.MapDtl toDtl = (Sys.MapDtl)toDtls[i];
                    if (toDtl.IsCopyNDData == false)
                        continue;


                    //获取明细数据。
                    GEDtls gedtls = new GEDtls(dtl.No);
                    QueryObject qo = null;
                    qo = new QueryObject(gedtls);
                    switch (dtl.DtlOpenType)
                    {
                        case DtlOpenType.ForEmp:
                            qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                            //qo.addAnd();
                            //qo.AddWhere(GEDtlAttr.Rec, WebUser.No);
                            break;
                        case DtlOpenType.ForWorkID:
                            qo.AddWhere(GEDtlAttr.RefPK,  this.WorkID);
                            break;
                        case DtlOpenType.ForFID:
                            qo.AddWhere(GEDtlAttr.FID,  this.WorkID);
                            break;
                    }
                    qo.DoQuery();

                    foreach (GEDtl gedtl in gedtls)
                    {
                        BP.Sys.GEDtl dtCopy = new GEDtl(toDtl.No);
                        dtCopy.Copy(gedtl);
                        dtCopy.FK_MapDtl = toDtl.No;

                        dtCopy.RefPK = this.WorkID.ToString();
                        try
                        {
                            dtCopy.InsertAsOID(dtCopy.OID);
                        }
                        catch
                        {
                        }
                    }

                }


              //  wk.CopyCellsData("ND" + this.HisNode.NodeID + "_" + this.HisWork.OID);

                #endregion

                try
                {
                    wk.BeforeSave();
                }
                catch
                {
                }

                try
                {
                    wk.DirectSave();
                }
                catch (Exception ex)
                {
                    Log.DebugWriteInfo(this.ToE("SaveWorkErr", "保存工作错误") + "：" + ex.Message);
                    throw new Exception(this.ToE("SaveWorkErr", "保存工作错误  ") + wk.EnDesc + ex.Message);
                }
                // 启动一个工作节点.
                string msg = this.beforeStartNode(wk, nd);
                return "@" + string.Format(this.ToE("NStep", "@第{0}步"), nd.Step.ToString()) + "<font color=blue>" + nd.Name + "</font>" + this.ToE("WorkStartOK", "工作成功启动") + "." + msg;
            }
            catch (Exception ex)
            {
                nd.HisWorks.DoDBCheck(DBLevel.Middle);
                throw new Exception(string.Format("StartGEWorkErr", nd.Name) + "@" + ex.Message + sql);
            }
        }
        #endregion

        #region 基本属性
        /// <summary>
        /// 工作
        /// </summary>
        private Work _HisWork = null;
        /// <summary>
        /// 工作
        /// </summary>
        public Work HisWork
        {
            get
            {
                return this._HisWork;
            }
        }
        /// <summary>
        /// 节点
        /// </summary>
        private Node _HisNode = null;
        /// <summary>
        /// 节点
        /// </summary>
        public Node HisNode
        {
            get
            {
                return this._HisNode;
            }
        }
        private RememberMe _RememberMe = null;
        public RememberMe GetHisRememberMe(Node nd)
        {
            if (_RememberMe == null || _RememberMe.FK_Node != nd.NodeID)
            {
                _RememberMe = new RememberMe();
                _RememberMe.FK_Emp = Web.WebUser.No;
                _RememberMe.FK_Node = nd.NodeID;
                _RememberMe.RetrieveFromDBSources();
            }
            return this._RememberMe;
        }
        private WorkFlow _HisWorkFlow = null;
        /// <summary>
        /// 工作流程
        /// </summary>
        public WorkFlow HisWorkFlow
        {
            get
            {
                if (_HisWorkFlow == null)
                    _HisWorkFlow = new WorkFlow(this.HisNode.HisFlow, this.HisWork.OID, this.HisWork.FID);
                return _HisWorkFlow;
            }
        }
        /// <summary>
        /// 标准的check, 如果当前的工作不是审核工作.就throw new exception
        /// </summary>
        /// <returns></returns>
        private GECheckStand GetGECheckStand()
        {
            return (GECheckStand)this.HisWork;
        }
        /// <summary>
        /// 当前节点的工作是不是完成。
        /// </summary>
        public bool IsComplete
        {
            get
            {
                if (this.HisWork.NodeState == NodeState.Complete)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 建立一个工作节点事例.
        /// </summary>
        /// <param name="workId">工作ID</param>
        /// <param name="nodeId">节点ID</param>
        public WorkNode(Int64 workId, int nodeId)
        {
            this.WorkID = workId;



            Node nd = new Node(nodeId);
            Work wk = nd.HisWork;
            wk.OID = workId;
            if (nd.IsCheckNode)
            {
                wk.SetValByKey(GECheckStandAttr.NodeID, nodeId);
            }

            wk.Retrieve();

            this._HisWork = wk;
            this._HisNode = nd;
        }
        /// <summary>
        /// 建立一个工作节点事例
        /// </summary>
        /// <param name="nd">节点ID</param>
        /// <param name="wk">工作</param>
        public WorkNode(Work wk, Node nd)
        {
            this.WorkID = wk.OID;

            //Node nd  = new Node(ndId);
            // if (nd.HisWorks.GetNewEntity.ToString() != wk.ToString())
            //   throw new Exception("@创建工作接点失败:定义节点［" + nd.Name + "］采集信息存放的实体[" + nd.WorksEnsName + "]，与数据实体[" + wk.ToString() + "]不一致．");
            this._HisWork = wk;
            this._HisNode = nd;
        }
        #endregion

        #region 运算属性
        private void Repair()
        {


        }
        /// <summary>
        /// 得当他的上一步工作
        /// 1, 从当前的找到他的上一步工作的节点集合.		 
        /// 如果没有找到转向他的节点,就返回,当前的工作.
        /// </summary>
        /// <returns>得当他的上一步工作</returns>
        public WorkNode GetPreviousWorkNode()
        {
            // 如果没有找到转向他的节点,就返回,当前的工作.
            if (this.HisNode.IsStartNode)
                throw new Exception("@" + this.ToE("WN14", "此节点是开始节点,没有上一步工作")); //此节点是开始节点,没有上一步工作.

            WorkNodes wns = new WorkNodes();
            foreach (Node nd in this.HisNode.HisFromNodes)
            {

                Work wk = (Work)nd.HisWorks.GetNewEntity;
                wk.OID = this.HisWork.OID;
                if (nd.IsCheckNode)
                {
                    wk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);
                }

                if (wk.RetrieveFromDBSources() == 0)
                {
                    continue;
                    //if (this.HisNode.HisFromNodes.Count == 1)
                    //{
                    //    throw new Exception("@系统遇到了未知的异常，请通知管理员来处理Workid=[" + wk.OID + "]，请上让上一步同事撤消发送、或者用本区县管理员用户登陆=》待办工作=》流程查询=》在关键字中输入["+this.WorkID+"]其它条件选择全部，查询到该流程删除它。技术信息:currNodeID ="+ this.HisNode.NodeID );

                    //    ///*恢复它*/
                    //    //wk.NodeState = NodeState.Complete;
                    //    //wl.FK_Node = nd.NodeID;
                    //    //wl.RDT = DataType.CurrentData;
                    //    //wl.FK_Flow = nd.FK_Flow;
                    //    //wl.SDT = wl.RDT;
                    //    //wl.DTOfWarning = wl.RDT;
                    //    //wl.IsEnable = true;
                    //    //wl.Insert();
                    //    //Log.DefaultLogWriteLineWarning("@自动修复了，没有找到上一步工作的错误。WorkID=" + this.HisWork.OID);
                    //}
                    //else
                    //{
                    //    continue;
                    //}
                }

                //WorkerList wl = new WorkerList();
                //if (wl.Retrieve(WorkerListAttr.FK_Node,nd.NodeID, WorkerListAttr.WorkID, wk.OID,
                WorkNode wn = new WorkNode(wk, nd);
                wns.Add(wn);
            }
            switch (wns.Count)
            {
                case 0:
                    throw new Exception(this.ToE("WN15", "没有找到他的上一步工作,系统错误。请通知管理员来处理，请上让上一步同事撤消发送、或者用本区县管理员用户登陆=》待办工作=》流程查询=》在关键字中输入Workid其它条件选择全部，查询到该流程删除它。") + "@WorkID=" + this.WorkID);
                case 1:
                    return (WorkNode)wns[0];
                default:
                    break;
            }
            Node nd1 = wns[0].HisNode;
            Node nd2 = wns[1].HisNode;
            if (nd1.HisFromNodes.Contains(NodeAttr.NodeID, nd2.NodeID))
            {
                return wns[0];
            }
            else
            {
                return wns[1];
            }
        }
        /// <summary>
        /// 得当他的下一步工作.
        /// 如果当前工作在没有处理完毕状态,就返回当前的工作.
        /// </summary>
        /// <returns>得当他的下一步工作</returns>
        private WorkNode GetNextWorkNode()
        {
            // 如果当前工作在没有处理完毕状态,就返回当前的工作.
            if (this.HisWork.NodeState != NodeState.Complete)
                throw new Exception(this.ToE("WN16", "@此节点的工作任务还没有完成,没有下一步工作.")); //"@此节点的工作任务还没有完成,没有下一步工作."

            // 如果他是一个结束节点
            if (this.HisNode.IsEndNode)
                throw new Exception(this.ToE("ND17", "@此节点是结束节点,没有下一步工作.")); // "@此节点是结束节点,没有下一步工作."

            // throw new Exception("@当前的工作没有完成任务,这是最");
            Nodes nds = this.HisNode.HisToNodes;
            if (nds.Count == 0)
                throw new Exception("@没有找到从当前节点【" + this.HisNode.Name + "】的转向节点，可能这是一个最后节点，不能取道下一个工作节点。");

            foreach (Node nd in nds)
            {
                Work wk = (Work)nd.HisWorks.GetNewEntity;
                if (nd.IsCheckNode)
                {
                    wk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);
                }
                wk.OID = this.HisWork.OID;

                if (wk.IsExits == false)
                    continue;

                wk.Retrieve();
                WorkNode wn = new WorkNode(wk, nd);
                return wn;
            }

            if (nds.Count == 1)
            {
                /* 这一中情况是,工作表的记录被非法删除.
                 * 补救办法是:
                 * 1,把当前的流程的当前工作节点设置为当前的节点.,
                 * 2,节设置为工作人员.
                 * */
                // 判断是不是用下一步的工作人员列表.
                Node nd = (Node)nds[0];
                WorkerLists wls = new WorkerLists(this.HisWork.OID, nd.NodeID);
                if (wls.Count == 0)
                {
                    /*说名没有产生工作者列表.*/
                    this.HisWork.NodeState = NodeState.Init;
                    this.HisWork.DirectUpdate();
                    GenerWorkFlow wgf = new GenerWorkFlow(this.HisWork.OID, this.HisNode.FK_Flow);
                    throw new Exception("@当前的工作没有正确的处理, 没有下一步骤工作节点.也可能是你非法删除了工作表中的记录,造成的没有找到,下一步工作内容,但是系统已经恢复当前节点为为完成状态,此流程可以正常的运行下去.");
                }
                else
                {
                    /*说名已经产生工作者列表.*/
                    DBAccess.RunSQL("delete WF_GenerWorkerList where WorkID=" + this.HisWork.OID + " and FK_Node=" + nd.NodeID);
                }
            }
            throw new Exception("@没有找到下一步骤工作节点[" + this.HisNode.Name + "]的下一步工作,流程错误.请把此问题发送给管理人员．");
        }
        #endregion
    }
    /// <summary>
    /// 工作节点集合.
    /// </summary>
    public class WorkNodes : CollectionBase
    {
        #region 构造
        /// <summary>
        /// 他的工作s
        /// </summary> 
        public Works GetWorks
        {
            get
            {
                if (this.Count == 0)
                    throw new Exception("@初始化失败，没有找到任何节点。");

                Works ens = this[0].HisNode.HisWorks;
                ens.Clear();

                foreach (WorkNode wn in this)
                {
                    ens.AddEntity(wn.HisWork);
                }
                return ens;
            }
        }
        /// <summary>
        /// 工作节点集合
        /// </summary>
        public WorkNodes()
        {
        }

        public int GenerByFID(Flow flow, Int64 fid)
        {
            Nodes nds = flow.HisNodes;
            foreach (Node nd in nds)
            {
                if (nd.HisFNType != FNType.River)
                    continue;

                Work wk = nd.GetWork(fid);
                if (wk == null)
                    continue;

                this.Add(new WorkNode(wk, nd));
            }
            return this.Count;
        }

        public int GenerByWorkID(Flow flow, Int64 oid)
        {
            Nodes nds = flow.HisNodes;
            foreach (Node nd in nds)
            {
                if (nd.HisFNType == FNType.River)
                    continue;

                Work wk = nd.GetWork(oid);
                if (wk == null)
                    continue;

                this.Add(new WorkNode(wk, nd));
            }
            return this.Count;
        }
        /// <summary>
        /// 删除工作流程
        /// </summary>
        public void DeleteWorks()
        {
            foreach (WorkNode wn in this)
            {
                wn.HisWork.Delete();
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 增加一个WorkNode
        /// </summary>
        /// <param name="wn">工作 节点</param>
        public void Add(WorkNode wn)
        {
            this.InnerList.Add(wn);
        }
        /// <summary>
        /// 根据位置取得数据
        /// </summary>
        public WorkNode this[int index]
        {
            get
            {
                return (WorkNode)this.InnerList[index];
            }
        }
        #endregion
    }
}
