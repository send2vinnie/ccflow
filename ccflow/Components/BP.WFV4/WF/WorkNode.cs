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
        #region ToE
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
        #endregion

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
        private string _VirPath = null;
        /// <summary>
        /// 虚拟目录的路径
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
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

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
               + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
               + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + ")";
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
                // sql = "SELECT No,Name FROM Port_Emp WHERE NO IN (SELECT FK_Dept FROM Port_EmpDept WHERE FK_EMP='" + WebUser.No + "') AND NO IN (SELECT FK_Emp FROM Port_EmpSTATION WHERE FK_Station='" + toSt.No + "')";
                sql = "SELECT No,Name FROM Port_Emp WHERE NO='" + WebUser.No + "'";  // IN (SELECT FK_Dept FROM Port_EmpDept WHERE FK_EMP='" + WebUser.No + "') AND NO IN (SELECT FK_Emp FROM Port_EmpSTATION WHERE FK_Station='" + toSt.No + "')";
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

        public WorkerLists GenerWorkerLists(WorkNode town)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            string sql;
            string fk_emp;

            // 如果执行了两次发送，那前一次的轨迹就需要被删除。这里是为了避免错误。
            DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node =" + town.HisNode.NodeID);

            //首先判断是否配置了获取下一步接受人员的sql.
            if (this.HisNode.RecipientSQL.Length > 4)
            {
                Attrs attrs = this.HisWork.EnMap.Attrs;
                sql = this.HisNode.RecipientSQL;
                foreach (Attr attr in attrs)
                {
                    if (attr.MyDataType == DataType.AppString)
                        sql = sql.Replace("@" + attr.Key, "'" + this.HisWork.GetValStrByKey(attr.Key) + "'");
                    else
                        sql = sql.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                }

                dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0)
                {
                    if (SystemConfig.IsDebug)
                        throw new Exception("@没有找到可接受的工作人员。@技术信息：执行的sql没有发现人员:" + sql);
                    else
                        throw new Exception("@没有找到可接受的工作人员。");
                }
                return WorkerListWayOfDept(town, dt);
            }

            if (this.HisNode.HisDeliveryWay== DeliveryWay.BySelected)
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
                }
                return WorkerListWayOfDept(town, dt);
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
                    sql = "SELECT NO FROM Port_Emp WHERE NO IN ";
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
                   + "(SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") "
                   + "AND FK_Emp IN (SELECT FK_Emp FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node IN (" + DataType.PraseAtToInSql(town.HisNode.GroupStaNDs,true) + ") )";
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
            sql = "SELECT NO FROM Port_Emp WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept = '" + WebUser.FK_Dept + "')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
                // throw new Exception(this.ToEP1("WN2", "@工作流程{0}已经完成。", town.HisNode.Name));
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }


            // 没有查询到的情况下, 按照最大匹配数计算。
            sql = "SELECT NO FROM Port_Emp WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept + "%')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);

                //  throw new Exception(this.ToEP1("WN2", "@工作流程{0}已经完成。", town.HisNode.Name));
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }

            // 没有查询到的情况下, 按照最大匹配数 提高一个级别 计算。
            sql = "SELECT NO FROM Port_Emp WHERE NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + ") )"
               + " AND  NO IN "
               + "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 2) + "%')";

            dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                Stations nextStations = town.HisNode.HisStations;
                if (nextStations.Count == 0)
                    throw new Exception(this.ToE("WN19", "节点没有岗位:") + town.HisNode.NodeID + "  " + town.HisNode.Name);
                //throw new Exception(this.ToEP1("WF2", "@工作流程{0}已经完成。", town.HisNode.Name));
                else
                {
                    sql = "SELECT No FROM Port_Emp WHERE NO IN ";
                    sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                    sql += " AND  No IN ";
                    if (WebUser.FK_Dept.Length == 2)
                        sql += "(SELECT  FK_Emp  FROM Port_EmpDept )";
                    else
                        sql += "(SELECT  FK_Emp  FROM Port_EmpDept WHERE FK_Dept LIKE '" + WebUser.FK_Dept.Substring(0, WebUser.FK_Dept.Length - 4) + "%')";

                    dt = DBAccess.RunSQLReturnTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sql = "SELECT No FROM Port_Emp WHERE No IN ";
                        sql += "(SELECT  FK_Emp  FROM Port_EmpStation WHERE FK_Station IN (SELECT FK_Station FROM WF_NodeStation WHERE FK_Node=" + town.HisNode.NodeID + " ) )";
                        dt = DBAccess.RunSQLReturnTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            string msg = town.HisNode.HisStationsStr;
                            throw new Exception(this.ToE("WF3", "岗位(" + msg + ")下没有人员，对应节点:") + town.HisNode.Name);
                            //"维护错误，请检查[" + town.HisNode.Name + "]维护的岗位中是否有人员？"
                        }
                    }
                    return WorkerListWayOfDept(town, dt);
                }
            }
            else
            {
                return WorkerListWayOfDept(town, dt);
            }
        }
        public bool IsSubFlowWorkNode
        {
            get
            {
                if (this.HisWork.FID == 0)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 生成一个word
        /// </summary>
        public void DoPrint()
        {
            string tempFile = SystemConfig.PathOfTemp + "\\" + this.WorkID + ".doc";
            Work wk = this.HisNode.HisWork;
            wk.OID = this.WorkID;
            wk.Retrieve();
            Glo.GenerWord(tempFile, wk);
            PubClass.OpenWordDocV2(tempFile, this.HisNode.Name + ".doc");
        }
        public WorkNode DoReturnWork(int backtoNodeID, string msg)
        {
            return DoReturnWork(backtoNodeID, msg, false);
        }
        private WorkNode DoReturnSubFlow(int backtoNodeID, string msg, bool isHiden)
        {
            Node nd = new Node(backtoNodeID);

            // 删除可能存在的数据.
            string sql = "DELETE  FROM WF_GenerWorkerList WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.HisWork.OID + "  AND FID=" + this.HisWork.FID;
            BP.DA.DBAccess.RunSQL(sql);

            // 找出分合流点处理的人员.
            sql = "SELECT FK_Emp FROM WF_GenerWorkerList WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.HisWork.FID;
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count != 1)
                throw new Exception("@ system error , this values must be =1");

            string fk_emp = dt.Rows[0][0].ToString();

            // 获取当前工作的信息.
            WorkerList wl = new WorkerList(this.HisWork.FID, this.HisNode.NodeID, fk_emp);

            Emp emp = new Emp(fk_emp);

            // 改变部分属性让它适应新的数据,并显示一条新的待办工作让用户看到。
            wl.IsPass = false;
            wl.WorkID = this.HisWork.OID;
            wl.FID = this.HisWork.FID;
            wl.RDT = DataType.CurrentDataTime;
            wl.FK_Emp = fk_emp;
            wl.FK_Node = backtoNodeID;
            wl.WarningDays = nd.WarningDays;
            wl.FK_Dept = emp.FK_Dept;

            DateTime dtNew = DateTime.Now;
            dtNew = dtNew.AddDays(nd.WarningDays);
            wl.SDT = dtNew.ToString(DataType.SysDataFormat); // DataType.CurrentDataTime;
            wl.FK_Flow = this.HisNode.FK_Flow;
            wl.Insert();

            GenerWorkFlow gwf = new GenerWorkFlow(this.HisWork.OID);
            gwf.FK_Node = backtoNodeID;
            gwf.DirectUpdate();


            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=3 WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID);

            /* 如果是隐性退回。*/
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = wl.WorkID;
            rw.NodeId = wl.FK_Node;
            rw.Note = msg;
            rw.Save();

            //  WorkerLists wls = new WorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);

            // 删除退回时当前节点的工作信息。
            //    this.HisWork.Delete();

            // 删除当前工作者.
            //WorkerLists wkls = new WorkerLists(this.HisWork.OID, this.HisNode.NodeID);
            //wkls.Delete();

            WorkNode wn = new WorkNode(this.HisWork.FID, backtoNodeID);
            return wn;

            //// 以退回到的节点向前删除。
            //Node nd = new Node(backtoNodeID);
            //Nodes nds = nd.HisToNodes;
            //foreach (Node item in nds)
            //{
            //    Work wk = item.HisWork;
            //    wk.OID = this.WorkID;
            //    if (wk.Delete() == 0)
            //        continue;

            //    wkls.Delete(WorkerListAttr.WorkID, this.WorkID, WorkerListAttr.FK_Node, item.NodeID);
            //    Nodes nds2 = item.HisToNodes;
            //    foreach (Node myitem in nds2)
            //    {
            //        Work w2k = myitem.HisWork;
            //        w2k.OID = this.WorkID;
            //        if (w2k.Delete() == 0)
            //            continue;
            //        wkls.Delete(WorkerListAttr.WorkID, this.WorkID, WorkerListAttr.FK_Node, myitem.NodeID);
            //    }
            //}
            //return wn;

            throw new Exception("分流子线程退回到分流点或者分合流点功能，没有实现。");
        }
        /// <summary>
        /// 工作退回
        /// </summary>
        /// <param name="backtoNodeID">退回到节点</param>
        /// <param name="msg">退回信息</param>
        /// <param name="isHiden">是否隐性退回</param>
        /// <returns></returns>
        public WorkNode DoReturnWork(int backtoNodeID, string msg, bool isHiden)
        {
            Node backToNode = new Node(backtoNodeID);
            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.WorkHL: /*如果当前是合流点 */
                    /* 杀掉进程*/
                    WorkerLists wlsSubs = new WorkerLists();
                    wlsSubs.Retrieve(WorkerListAttr.FID, this.HisWork.OID);
                    foreach (WorkerList sub in wlsSubs)
                    {
                        GenerWorkFlow gw1f = new GenerWorkFlow();
                        gw1f.WorkID = sub.WorkID;
                        gw1f.DirectDelete();

                        Node subNode = new Node(sub.FK_Node);
                        Work wk = subNode.GetWork(sub.WorkID);
                        wk.Delete();
                        sub.Delete();
                    }
                    GenerFH fh = new GenerFH();
                    fh.FID = this.WorkID;
                    fh.Delete();
                    //DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID="+this.HisWork.OID);
                    break;
                default:
                    switch (backToNode.HisNodeWorkType)
                    {
                        case NodeWorkType.StartWorkFL:
                        case NodeWorkType.WorkFL:
                        case NodeWorkType.WorkFHL:
                            if (this.IsSubFlowWorkNode)
                            {
                                /* 如果是支流，并且向分流或者分河流节点上退回. */
                                return DoReturnSubFlow(backtoNodeID, msg, isHiden);
                            }
                            // return DoReturnSubFlow(backtoNodeID, msg, isHiden);
                            break;
                        default:
                            break;
                    }
                    break;
            }

            // 改变当前的工作节点．
            WorkNode wn = new WorkNode(this.WorkID, backtoNodeID);
            // 更新 return work 状态．
            wn.HisWork.NodeState = NodeState.Back;
            wn.HisWork.DirectUpdate();

            GenerWorkFlow gwf = new GenerWorkFlow(this.HisWork.OID);
            gwf.FK_Node = backtoNodeID;
            gwf.DirectUpdate();
            DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.WorkID);

            //恢复抢办状态。
            //if (backToNode.IsStartNode == false)
            //{
            //    RememberMe rm = new RememberMe();
            //    rm.FK_Node = backtoNodeID;
            //    WorkNode mywn = new WorkNode(this.WorkID, backtoNodeID);
            //    WorkNode pwn = mywn.GetPreviousWorkNode();
            //    rm.Retrieve(RememberMeAttr.FK_Emp, pwn.HisWork.Rec, RememberMeAttr.FK_Node, backtoNodeID);
            //    WorkerList mywl =new WorkerList(this.WorkID,backtoNodeID,
            //    string[] emps = rm.Objs.Split('@');
            //    foreach (string emp in emps)
            //    {
            //        WorkerList wl = new WorkerList();
            //        wl.FK_Dept
            //    }
            //}
            //   DBAccess.RunSQL("DELETE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.WorkID);
            //   DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node=" + backtoNodeID + " AND WorkID=" + this.WorkID);


            // 记录退回轨迹。
            ReturnWork rw = new ReturnWork();
            rw.WorkID = wn.HisWork.OID;
            rw.NodeId = wn.HisNode.NodeID;
            rw.Note = msg;
            rw.Save();

            // 删除退回时当前节点的工作信息
            this.HisWork.Delete();

            // 删除明细表信息。
            MapDtls dtls = new MapDtls("ND" + this.HisNode.NodeID);
            foreach (MapDtl dtl in dtls)
            {
                BP.DA.DBAccess.RunSQL("DELETE "+dtl.PTable+" WHERE RefPK='"+this.WorkID+"'");
            }


            // 删除当前工作者.
            WorkerLists wkls = new WorkerLists(this.HisWork.OID, this.HisNode.NodeID);
            wkls.Delete();

            // 以退回到的节点向前数据用递归删除它。
            Node nd = new Node(backtoNodeID);
            DeleteToNodesData(nd.HisToNodes);
            return wn;
        }
        /// <summary>
        /// 递归删除两个节点之间的数据
        /// </summary>
        /// <param name="nds">到达的节点集合</param>
        public void DeleteToNodesData(Nodes nds)
        {
            /*开始遍历到达的节点集合*/
            foreach (Node nd in nds)
            {
                Work wk = nd.HisWork;
                wk.OID = this.WorkID;
                if (wk.Delete() == 0)
                {
                    wk.FID = this.WorkID;
                    if (wk.Delete(WorkAttr.FID, this.WorkID) == 0)
                        continue;
                }

                /*说明:已经删除该节点数据。*/
                DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE (WorkID=" + this.WorkID + " OR FID=" + this.WorkID + ") AND FK_Node=" + nd.NodeID);

                //删除正常的垃圾数据。
                DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE WorkID NOT IN (SELECT WorkID FROM WF_GenerWorkerList )");
                DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID NOT IN (SELECT FID FROM WF_GenerWorkerList)");

                if (nd.IsFL)
                {
                    /* 如果是分流 */
                    WorkerLists wls = new WorkerLists();
                    wls.Retrieve(WorkerListAttr.FID, this.WorkID);
                    foreach (WorkerList wl in wls)
                    {
                        Node subNd = new Node(wl.FK_Node);
                        Work subWK = subNd.GetWork(wl.WorkID);
                        subWK.Delete();

                        //删除分流下步骤的节点信息.
                        DeleteToNodesData(subNd.HisToNodes);
                    }

                    DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.WorkID);
                    DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.WorkID);
                    DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID=" + this.WorkID);
                }
                DeleteToNodesData(nd.HisToNodes);
            }
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
            GenerWorkFlow gwf = new GenerWorkFlow(this.HisWork.OID);
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

            //WorkFlow wf = this.HisWorkFlow;
            //wf.WritLog(msg);
            // 消息通知上一步工作人员处理．
            WorkerLists wls = new WorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);
            BP.WF.MsgsManager.AddMsgs(wls, "退回的工作", wn.HisNode.Name, "退回的工作");

            // 删除退回时当前节点的工作信息。
            this.HisWork.Delete();
            return wn;
        }
        /// <summary>
        /// 执行退回(在合流节点上执行退回到上一个步骤)
        /// </summary>
        /// <param name="workid">线程ID</param>
        /// <param name="msg">退回工作的原因</param>
        /// <returns></returns>
        public WorkNode DoReturnWorkHL(Int64 workid, string msg)
        {
            // 改变当前的工作节点．
            WorkNode wn = this.GetPreviousWorkNode_FHL(workid);

            GenerWorkFlow gwf = new GenerWorkFlow(workid);
            gwf.FK_Node = wn.HisNode.NodeID;
            gwf.DirectUpdate();

            // 更新 return work 状态．
            wn.HisWork.NodeState = NodeState.Back;
            wn.HisWork.DirectUpdate();

            // 删除工作者.
            WorkerLists wkls = new WorkerLists(workid, this.HisNode.NodeID);
            wkls.Delete();

            // 写入日志.
            BP.WF.ReturnWork rw = new ReturnWork();
            rw.WorkID = workid;
            rw.NodeId = wn.HisNode.NodeID;
            rw.Note = msg;
            rw.Save();

            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0 WHERE FK_Node='" + wn.HisNode.NodeID + "' and workid=" + workid);

            //WorkFlow wf = this.HisWorkFlow;
            //wf.WritLog(msg);
            // 消息通知上一步工作人员处理．
            WorkerLists wls = new WorkerLists(workid, wn.HisNode.NodeID);
            BP.WF.MsgsManager.AddMsgs(wls, "退回的工作", wn.HisNode.Name, "退回的工作");

            // 删除退回时当前节点的工作信息。
           // this.HisWork.Delete();
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
            sql = "DELETE FROM WF_GenerWorkerlist WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.HisWork.OID + " AND FK_Emp <> '" + this.HisWork.Rec.ToString() + "'";
            DBAccess.RunSQL(sql);
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
        private WorkerLists WorkerListWayOfDept(WorkNode town, DataTable dt)
        {
            return WorkerListWayOfDept(town,dt,0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="town"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private WorkerLists WorkerListWayOfDept(WorkNode town, DataTable dt, Int64 fid)
        {
            if (dt.Rows.Count == 0)
                throw new Exception(this.ToE("WN4", "接受人员列表为空")); // 接受人员列表为空

            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (dr[0].ToString() == WebUser.No)
            //    {
            //        /*如果包含我*/
            //    }
            //}

            Int64 workID = fid;
            if (workID==0)
                workID=this.HisWork.OID;

            int toNodeId = town.HisNode.NodeID;

            this.HisWorkerLists = new WorkerLists();
            this.HisWorkerLists.Clear();


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

            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.StartWorkFL:
                case NodeWorkType.WorkFHL:
                case NodeWorkType.WorkFL:
                case NodeWorkType.WorkHL:
                    break;
                default:
                    this.HisGenerWorkFlow.Update(GenerWorkFlowAttr.FK_Node,
                        town.HisNode.NodeID,
               "SDT", dtOfShould.ToString("yyyy-MM-dd"));
                    break;
            }
           

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
                wl.RDT = DateTime.Now.ToString(DataType.SysDataTimeFormat);
                wl.FK_Flow = town.HisNode.FK_Flow;
                wl.FID = town.HisWork.FID;
                //if (wl.FID == 0)
                //    wl.FID = this.WorkID;

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
                        wl.WarningDays = town.HisNode.WarningDays;
                        wl.SDT = dtOfShould.ToString(DataType.SysDataFormat);
                        wl.DTOfWarning = dtOfWarning.ToString(DataType.SysDataFormat);
                        wl.RDT = DateTime.Now.ToString(DataType.SysDataTimeFormat);
                        wl.FK_Flow = town.HisNode.FK_Flow;
                        wl.FID = town.HisWork.FID;

                        //if (wl.FID == 0)
                        //    wl.FID = this.WorkID;
                        try
                        {
                            wl.DirectInsert();
                            this.HisWorkerLists.AddEntity(wl);
                        }
                        catch (Exception ex)
                        {
                            Log.DefaultLogWriteLineError("不应该出现的异常信息：" + ex.Message);
                        }
                    }
                }
                else
                {
                    string[] strs = rm.Objs.Split('@');
                    string myemps = "";
                    foreach (string s in strs)
                    {
                        if (s.Length < 1)
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
                        wl.RDT = DateTime.Now.ToString(DataType.SysDataTimeFormat);
                        wl.FK_Flow = town.HisNode.FK_Flow;
                        wl.FID = town.HisWork.FID;
                        //if (wl.FID == 0)
                        //    wl.FID = this.WorkID;

                        try
                        {
                            if (town.HisNode.IsFLHL == false)
                            {
                                wl.DirectInsert();
                                this.HisWorkerLists.AddEntity(wl);
                            }
                        }
                        catch
                        {
                            continue;
                        }
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
                    {
                        throw new Exception("您没有给[" + this.HisWork.Rec + "]设置部门。");
                    }

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
            DBAccess.DoTransactionBegin();
            DateTime dt = DateTime.Now;
            this.HisWork.Rec = Web.WebUser.No;
            this.WorkID = this.HisWork.OID;

            // 调用发送前的接口。
            string msg = this.HisNode.HisNDEvents.DoEventNode(EventListOfNode.SendWhen, this.HisWork);
            // this.NodeID = this.HisNode.NodeID;
            try
            {
                msg += AfterNodeSave_Do();


                #region 调用接口
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
                #endregion 调用接口

                // 调起发送成功后的事务。
                msg += this.HisNode.HisNDEvents.DoEventNode(EventListOfNode.SendSuccess, this.HisWork);
                try
                {
                    DBAccess.DoTransactionCommit(); // 提交事务.
                }
                catch
                {
                }


                string msgOfSend = this.HisNode.MsgSend;
                if (msgOfSend.Length > 3)
                {
                    if (msgOfSend.Contains("@"))
                    {
                        Attrs attrs = this.HisWork.EnMap.Attrs;
                        foreach (Attr attr in attrs)
                        {
                            msgOfSend = msgOfSend.Replace("@" + attr.Key, this.HisWork.GetValStrByKey(attr.Key));
                        }
                    }
                    return msgOfSend;
                }
                return msg;
            }
            catch (Exception ex)
            {
                try
                {
                    // 回滚。
                    DBAccess.DoTransactionRollback();
                    this.HisNode.HisFlow.DoCheck();
                    throw ex;
                }
                catch (Exception exE)
                {
                    try
                    {
                        this.WhenTranscactionRollbackError();
                    }
                    catch (Exception ex11)
                    {
                        throw ex;
                        //   throw new Exception(ex.Message + " @DoTransactionRollback=" + exE.Message + "" + ex11.Message);
                    }
                    throw ex;
                    //throw new Exception(ex.Message + " @DoTransactionRollback=" + exE.Message);
                    //throw new Exception(ex.Message + " @DoTransactionRollback=" + exE.Message);
                }
            }
        }
        private void WhenTranscactionRollbackError()
        {
            #region 以下代码不在使用。
            /*在提交错误的情况下，回滚数据。*/
            try
            {
                this.HisNode.HisFlow.DoCheck();

                // 把工作的状态设置回来。
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Init);

                // 把流程的状态设置回来。
                GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
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
                    mwk.DirectDelete();
                }
                this.HisNode.HisNDEvents.DoEventNode(EventListOfNode.SendError, this.HisWork);
            }
            catch (Exception ex1)
            {
                if (this.rptGe != null)
                    this.rptGe.CheckPhysicsTable();
                throw new Exception(ex1.Message + "@回滚发送失败数据出现错误：" + ex1.Message + "@有可能系统已经自动修复错误，请您在重新执行一次。");
            }
            #endregion 以下代码不在使用。
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
                    _HisGenerWorkFlow = new GenerWorkFlow(this.WorkID);
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
        /// <summary>
        /// 启动分流
        /// </summary>
        /// <param name="toNode"></param>
        /// <returns></returns>
        public string FeiLiuStartUp(Node toNode)
        {
            // 发起
            Work wk = toNode.HisWork;
            WorkNode town = new WorkNode(wk, toNode);

            // 产生下一步骤要执行的人员
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
                        mywk.BeforeSave();
                        mywk.OID = DBAccess.GenerOID(BP.Web.WebUser.FK_Dept);  //BP.DA.DBAccess.GenerOID();
                        mywk.BeforeSave();
                        // 跳过特殊的规则约束。
                        mywk.InsertAsOID(mywk.OID);

                        // 产生工作的信息。
                        GenerWorkFlow gwf = new GenerWorkFlow();
                        gwf.FID = this.WorkID;
                        gwf.WorkID = mywk.OID;

                        if (BP.WF.Glo.IsShowUserNoOnly)
                            gwf.Title = WebUser.No + "," + WebUser.Name + " 发起：" + toNode.Name + "(分流节点)";
                        else
                            gwf.Title = WebUser.No + " 发起：" + toNode.Name + "(分流节点)";

                        gwf.WFState = 0;
                        gwf.RDT = DataType.CurrentDataTime;
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
                        DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET WorkID=" + mywk.OID + ",FID=" + this.WorkID + " WHERE FK_Emp='" + wl.FK_Emp + "' AND WorkID=" + this.WorkID + " AND FK_Node=" + toNode.NodeID);
                    }
                    break;
                default:
                    throw new Exception("没有处理的类型：" + this.HisNode.HisFLRole.ToString());
            }
            // return 。系统自动下达给如下几位同事。";

            string info = this.ToE("WN28", "@分流节点:{0}已经发起。@任务自动下达给{1}如下{2}位同事,{3}.");

            msg += string.Format(info, toNode.Name,
                this.nextStationName,
                this._RememberMe.NumOfObjs.ToString(),
                this._RememberMe.EmpsExt);

            // 如果是开始节点，就可以允许选择接受人。
            if (this.HisNode.IsStartNode)
            {
                if (gwls.Count >= 2)
                    msg += "@<img src='" + this.VirPath + "/WF/Img/AllotTask.gif' border=0 /><a href=\"javascript:WinOpen('" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&FID=" + this.WorkID + "&NodeID=" + toNode.NodeID + "')\" >" + this.ToE("W29", "修改接受对象") + "</a>.";
            }

            if (this.HisNode.IsStartNode)
                msg += "@<a href='" + this.VirPath + "/WF/MyFlowInfo"+Glo.FromPageType+".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "撤销本次发送") + "</a>， <a href='" + this.VirPath + "/WF/MyFlow"+Glo.FromPageType+".aspx?FK_Flow=" + toNode.FK_Flow + "'><img src=./Img/New.gif border=0/>" + this.ToE("NewFlow", "新建流程") + "</a>。";
            else
                msg += "@<a href='" + this.VirPath + "/WF/MyFlowInfo"+Glo.FromPageType+".aspx?DoType=UnSend&WorkID=" + this.WorkID + "&FK_Flow=" + toNode.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "撤销本次发送") + "</a>。";

            msg += this.GenerWhySendToThem(this.HisNode.NodeID, toNode.NodeID);

            //更新节点状态。
            this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete);

            msg += "@<a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=" + wk.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >工作报告</a>";
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
                try
                {
                    fh.Title = this.HisWork.GetValStringByKey(StartWorkAttr.Title);
                }
                catch (Exception ex)
                {
                    BP.Sys.MapAttr attr = new BP.Sys.MapAttr();

                    attr.FK_MapData = "ND" + this.HisNode.NodeID;
                    attr.HisEditType = BP.En.EditType.UnDel;
                    attr.KeyOfEn = "Title";

                    int i = attr.Retrieve(MapAttrAttr.FK_MapData, attr.FK_MapData, MapAttrAttr.KeyOfEn, attr.KeyOfEn);
                    if (i == 0)
                    {
                        attr.KeyOfEn = "Title";
                        attr.Name = BP.Sys.Language.GetValByUserLang("Title", "标题"); // "流程标题";
                        attr.MyDataType = BP.DA.DataType.AppString;
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;
                        attr.UIVisible = true;
                        attr.UIIsEnable = true;
                        attr.UIIsLine = true;
                        attr.MinLen = 0;
                        attr.MaxLen = 200;
                        attr.IDX = -100;
                        attr.Insert();
                    }

                    fh.Title = WebUser.No + "-" + WebUser.Name + " @ " + DataType.CurrentDataTime + " ";
                }

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

#warning del those code 2011-05-22.
                //qo.addAnd();
                //qo.AddWhere(CondAttr.CondType, (int)CondType.FLRole);
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
                    wk.Delete();
                   
                }
            }
            return msg;
        }
        #endregion

        public GEEntity rptGe = null;
       
        /// <summary>
        /// 
        /// </summary>
        private void InitStartWorkData()
        {
            /* 产生开始工作流程记录. */
            GenerWorkFlow gwf = new GenerWorkFlow();
            gwf.WorkID = this.HisWork.OID;
            string title = this.HisWork.GetValStringByKey(StartWorkAttr.Title);
            if (title.Trim() == "")
            {
                title = WebUser.No + "," + WebUser.Name + " 在 " + DataType.CurrentDataCNOfShort + " 发起.";
                this.HisWork.SetValByKey(StartWorkAttr.Title, title);
            }

            gwf.Title = title;
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

            StartWork sw = (StartWork)this.HisWork;
            sw.InitBillNo();

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
            wl.FK_Dept = WebUser.FK_Dept;

            // wl.UseDept = this.HisDeptOfUse.No;
            // wl.UseStation = this.HisStationOfUse.No;

            wl.WarningDays = this.HisNode.WarningDays;
            wl.SDT = DataType.CurrentData;
            wl.DTOfWarning = DataType.CurrentData;
            wl.RDT = DataType.CurrentDataTime;
            try
            {
                wl.Insert(); // 先插入，后更新。
            }
            catch (Exception ex)
            {
                //  throw ex;
                wl.Update();
            }
            #endregion
        }
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
                    this.InitStartWorkData();

                string msg = "";
                switch (this.HisNode.HisNodeWorkType)
                {
                    case NodeWorkType.StartWorkFL:
                    case NodeWorkType.WorkFL:  /*  启动分流 */
                        this.HisWork.FID = this.HisWork.OID;
                        msg = this.FeiLiuStartUp();
                        break;
                    case NodeWorkType.WorkFHL:   /* 启动分流 */
                        this.HisWork.FID = this.HisWork.OID;
                        msg = this.FeiLiuStartUp();
                        break;
                    case NodeWorkType.WorkHL:   /* 当前工作节点是合流 */
                        msg = this.StartupNewNodeWork();
                        msg += this.DoSetThisWorkOver(); // 执行此工作结束。

                        break;
                    default: /* 其他的点的逻辑 */
                        msg = this.StartupNewNodeWork();
                        msg += this.DoSetThisWorkOver();
                        break;
                }

                #region 把数据放到报表表里面.
                this.rptGe = this.HisNode.HisFlow.HisFlowData;
                rptGe.SetValByKey("OID", this.WorkID);
                if (this.HisNode.IsStartNode)
                {
                    rptGe.Delete();
                    rptGe.SetValByKey("OID", this.WorkID);
                    rptGe.SetValByKey(GERptAttr.FlowEmps, "@" + WebUser.No + "," + WebUser.Name);
                    rptGe.SetValByKey(GERptAttr.FlowStarter, WebUser.No );
                    rptGe.SetValByKey(GERptAttr.FlowStartRDT, DataType.CurrentDataTime);
                    rptGe.SetValByKey(GERptAttr.WFState, 0);
                    rptGe.SetValByKey(GERptAttr.FK_Dept, WebUser.FK_Dept);
                   // rptGe.SetValByKey(GERptAttr.Title, this.WorkID);
                    rptGe.Copy(this.HisWork);
                    rptGe.Insert();
                }
                else
                {
                   int i= rptGe.RetrieveFromDBSources();
                   if (i == 0)
                   {
                       /*没有查询到这条数据。
                        * 有两种情况：
                        * 1，报表的数据表没有创建，或者丢失。
                        * 2，它在分合流节点上。
                        */
#warning 不处理分合流程的分支流程数据报表的情况。
                   }

                   foreach (Attr attr in rptGe.EnMap.Attrs)
                   {
                       switch (attr.Key)
                       {
                           case StartWorkAttr.FK_Dept:
                           case StartWorkAttr.FID:
                           case StartWorkAttr.CDT:
                           case StartWorkAttr.RDT:
                           case StartWorkAttr.Rec:
                           case StartWorkAttr.Sender:
                           case StartWorkAttr.NodeState:
                               continue;
                           default:
                               break;
                       }
                       object obj = this.HisWork.GetValByKey(attr.Key);
                       if (obj == null)
                           continue;
                       rptGe.SetValByKey(attr.Key, obj);
                   }

                    try
                    {
                        string str = rptGe.GetValStrByKey(GERptAttr.FlowEmps);
                        if (str.Contains("@" + WebUser.No + "," + WebUser.Name) == false)
                        {
                            rptGe.SetValByKey(GERptAttr.FlowEmps, str + "@" + WebUser.No + "," + WebUser.Name);
                        }
                    }
                    catch
                    {
                        this.HisNode.HisFlow.DoCheck();
                    }

                    if (this.HisNode.IsEndNode)
                        rptGe.SetValByKey(GERptAttr.WFState, 1); // 更新状态。

                    rptGe.DirectUpdate();
                }
                #endregion

                #region 处理收听
                Listens lts = new Listens();
                lts.RetrieveByLike(ListenAttr.Nodes, "%" + this.HisNode.NodeID + "%");
                foreach (Listen lt in lts)
                {
                    string sql = "SELECT FK_Emp FROM WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID;
                    DataTable dtRem = BP.DA.DBAccess.RunSQLReturnTable(sql);
                    foreach (DataRow dr in dtRem.Rows)
                    {
                        string fk_emp = dr["FK_Emp"] as string;

                        Port.WFEmp emp = new BP.WF.Port.WFEmp(fk_emp);
                        if (emp.HisAlertWay == BP.WF.Port.AlertWay.None)
                        {
                            // msg += "@<font color=red>此信息无法发送给：" + emp.Name + "，因为他关闭了信息提醒，给他打电话："+emp.Tel+"</font>";
                            msg += "@<font color=red>" + this.ToEP2("WN25", "此信息无法发送给：{0}，因为他关闭了信息提醒，给他打电话：{1}。", emp.Name, emp.Tel) + "</font>";
                            continue;
                        }
                        else
                        {
                            // msg += "@您的操作已经通过（<font color=green><b>" + emp.HisAlertWayT + "</b></font>）的方式发送给：" + emp.Name;
                            msg += this.ToEP2("WN26", "@您的操作已经通过（<font color=green><b>{0}</b></font>）的方式发送给：{1}", emp.HisAlertWayT, emp.Name);
                        }

                        string title = lt.Title.Clone() as string;

                        title = title.Replace("@WebUser.No", WebUser.No);
                        title = title.Replace("@WebUser.Name", WebUser.Name);

                        string doc = lt.Doc.Clone() as string;
                        doc = doc.Replace("@WebUser.No", WebUser.No);
                        doc = doc.Replace("@WebUser.Name", WebUser.Name);

                        Attrs attrs = this.rptGe.EnMap.Attrs;
                        foreach (Attr attr in attrs)
                        {
                            title = title.Replace("@" + attr.Key, this.rptGe.GetValStrByKey(attr.Key));
                            doc = doc.Replace("@" + attr.Key, this.rptGe.GetValStrByKey(attr.Key));
                        }

                        BP.TA.SMS.AddMsg(lt.OID + "_" + this.WorkID, fk_emp, emp.HisAlertWay, emp.Tel, title, emp.Email, title, doc);
                    }
                }
                #endregion

                #region 生成单据
                BillTemplates reffunc = this.HisNode.HisBillTemplates;
                if (reffunc.Count > 0)
                {
                    #region 生成单据信息
                    Int64 workid = this.HisWork.OID;
                    int nodeId = this.HisNode.NodeID;
                    //string BillTable=this.HisNode.HisFlow.BillTable;
                    string flowNo = this.HisNode.FK_Flow;

                    // 删除这个sql, 在后面已经做了异常判断。
                    //DBAccess.RunSQL("DELETE FROM WF_Bill WHERE WorkID='" + workid + "' AND (FK_Node='" + nodeId + "') ");
                    #endregion

                    rptGe.RetrieveFromDBSources();
                    DateTime dt = DateTime.Now;
                    string BillNo = this.HisWorkFlow.HisStartWork.BillNo;
                    Flow fl = new Flow(this.HisNode.FK_Flow);
                    string year = dt.Year.ToString();
                    string billInfo = "";
                    foreach (BillTemplate func in reffunc)
                    {
                        string file = year + "_" + WebUser.FK_Dept + "_" + func.No + "_" + workid + ".doc";
                        BP.Rpt.RTF.RTFEngine rtf = new BP.Rpt.RTF.RTFEngine();

                        Works works;
                        string[] paths;
                        string path;
                        try
                        {
                            #region 生成单据
                            rtf.HisEns.Clear();
                            rtf.EnsDataDtls.Clear();
                            if (func.NodeID == 0)
                            {
                                // 判断是否是受理回执
                                //if (fl.DateLit == 0)
                                //    continue;
                                //HisCHOfFlow.DateLitFrom = DateTime.Now.AddDays(fl.DateLit).ToString(DataType.SysDataFormat);
                                //HisCHOfFlow.DateLitTo = DateTime.Now.AddDays(fl.DateLit + 10).ToString(DataType.SysDataFormat);
                                //HisCHOfFlow.Update();
                                rtf.AddEn(HisCHOfFlow);
                            }
                            else
                            {
                                WorkNodes wns = new WorkNodes();
                                if (this.HisNode.HisFNType == FNType.River)
                                    wns.GenerByFID(this.HisNode.HisFlow, this.WorkID);
                                else
                                    wns.GenerByWorkID(this.HisNode.HisFlow, this.WorkID);

                                rtf.HisGEEntity = rptGe;
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

                            string billUrl = this.VirPath + "/DataUser/Bill/" + path + file;

                            if (func.HisBillFileType == BillFileType.PDF)
                            {
                                billUrl = billUrl.Replace(".doc", ".pdf");
                                billInfo += "<img src='" + this.VirPath + "/Images/FileType/PDF.gif' /><a href='" + billUrl + "' target=_blank >" + func.Name + "</a>";
                            }
                            else
                            {
                                billInfo += "<img src='" + this.VirPath + "/Images/FileType/Word.gif' /><a href='" + billUrl + "' target=_blank >" + func.Name + "</a>";
                            }

                            //  string  = BP.SystemConfig.GetConfig("FtpPath") + file;
                            path = BP.WF.Glo.FlowFileBill + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";

                            if (System.IO.Directory.Exists(path) == false)
                                System.IO.Directory.CreateDirectory(path);

                            rtf.MakeDoc(func.Url + ".rtf",
                                path, file, func.ReplaceVal, false);
                            #endregion

                            #region 转化成pdf.
                            if (func.HisBillFileType == BillFileType.PDF)
                            {
                                string rtfPath = path + file;
                                string pdfPath = rtfPath.Replace(".doc", ".pdf");
                                //  string pdfPath = path + func.Url + ".pdf";
                                try
                                {
                                    Glo.Rtf2PDF(rtfPath, pdfPath);
                                }
                                catch (Exception ex)
                                {
                                    msg += ex.Message;
                                }
                            }
                            #endregion

                            #region 保存单据
                            Bill bill = new Bill();
                            bill.MyPK = this.HisWork.FID + "_" + this.HisWork.OID + "_" + this.HisNode.NodeID + "_" + func.No;
                            bill.FID = this.HisWork.FID;
                            bill.WorkID = this.HisWork.OID;
                            bill.FK_Node = this.HisNode.NodeID;
                            bill.FK_Bill = func.No;
                            bill.FK_Dept = WebUser.FK_Dept;
                            bill.FK_Emp = WebUser.No;
                            bill.Url = billUrl;
                            bill.RDT = DataType.CurrentDataTime;
                            bill.FK_NY = DataType.CurrentYearMonth;
                            bill.FK_Flow = this.HisNode.FK_Flow;
                            bill.BillNo = BillNo;
                            bill.FK_BillType = func.FK_BillType;
                            bill.FK_Flow = this.HisNode.FK_Flow;
                            bill.Emps = this.rptGe.GetValStrByKey("Emps");
                            bill.FK_Starter = this.rptGe.GetValStrByKey("Rec");
                            bill.StartDT = this.rptGe.GetValStrByKey("RDT");
                            bill.Title = this.rptGe.GetValStrByKey("Title");
                            bill.FK_Dept = this.rptGe.GetValStrByKey("FK_Dept");
                            try
                            {
                                bill.Insert();
                            }
                            catch
                            {
                                bill.Update();
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            BP.WF.DTS.InitBillDir dir = new BP.WF.DTS.InitBillDir();
                            dir.Do();

                            path = BP.WF.Glo.FlowFileBill + year + "\\" + WebUser.FK_Dept + "\\" + func.No + "\\";
                            string msgErr = "@" + this.ToE("WN5", "生成单据失败，请让管理员检查目录设置") + "[" + BP.WF.Glo.FlowFileBill + "]。@Err：" + ex.Message + " @File=" + file + " @Path:" + path;
                            billInfo += "@<font color=red>" + msgErr + "</font>";
                            throw new Exception(msgErr+"@其它信息:"+ex.Message);
                        }

                    } // end 生成循环单据。

                    if (billInfo != "")
                        billInfo = "@" + billInfo;
                    msg += billInfo;
                }
                #endregion

                this.HisWork.DoCopy(); // copy 本地的数据到指定的系统.
                DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=1 WHERE FK_Node=" + this.HisNode.NodeID + " AND WorkID=" + this.WorkID);

                
                return msg;
            }
            catch (Exception ex)  // 如果抛出异常，说明没有正确的执行。当前的工作，不能完成。工作流程不能完成。
            {
                try
                {
                    if (this.HisWork.FID == 0)
                    {
                        WorkNode wn = this.HisWorkFlow.HisStartWorkNode;
                        //wn.HisWork.SetValByKey("WFState",0);
                        wn.HisWork.Update("WFState", (int)WFState.Runing);
                        this.HisWork.NodeState = NodeState.Init;
                        this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Init);

                        // 更新当前的节点信息。
                        this.HisGenerWorkFlow.Update(GenerWorkFlowAttr.FK_Node, this.HisNode.NodeID);
                    }
                }
                catch (Exception ex1)
                {
                    throw new Exception(ex.Message + " - @回滚出现的错误:" + ex1.Message);
                }
                throw ex;
                //throw new Exception("@回滚数据期间发生错误:" + ex1.Message + ex.Message);
            }
        }
        /// <summary>
        /// 加入工作记录
        /// </summary>
        /// <param name="gwls"></param>
        public void AddIntoWacthDog(WorkerLists gwls)
        {
            string basePath = "http://" + SystemConfig.AppSettings["BPMHost"];
            string mailTemp = BP.DA.DataType.ReadTextFile2Html(BP.SystemConfig.PathOfDataUser + "\\EmailTemplete\\"+WebUser.SysLang+".txt");
            //foreach (WorkerList wl in HisWorkerLists)
            foreach (WorkerList wl in gwls)
            {
                if (wl.IsEnable == false)
                    continue;

                string sid = wl.FK_Emp + "_" + wl.WorkID + "_" + wl.FK_Node + "_" + wl.RDT;
                string url = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid;
                string urlWap = basePath + "/WF/Do.aspx?DoType=OF&SID=" + sid + "&IsWap=1";


                //string mytemp ="您好" + wl.FK_EmpText + ":  <br><br>&nbsp;&nbsp; "+WebUser.Name+"发来的工作需要您处理，点这里<a href='" + url + "'>打开工作</a>。 \t\n <br>&nbsp;&nbsp;如果打不开请复制到浏览器地址栏里。<br>&nbsp;&nbsp;" + url + " <br><br>&nbsp;&nbsp;此邮件由驰骋工作流程引擎自动发出，请不要回复。<br>*^_^*  谢谢 ";
                string mytemp = mailTemp.Clone() as string;
                mytemp = string.Format(mytemp, wl.FK_EmpText, WebUser.Name, url, urlWap);

                //执行消息发送。
                BP.WF.Port.WFEmp wfemp = new BP.WF.Port.WFEmp(wl.FK_Emp);
               // wfemp.No = wl.FK_Emp;
                BP.TA.SMS.AddMsg(wl.WorkID + "_" + wl.HisNode.NodeID + "_" + wfemp.No, wfemp.No, wfemp.HisAlertWay, wfemp.Tel,
                    this.ToEP3("WN27", "流程:{0}.工作:{1},发送人:{2},需您处理", wl.HisNode.FlowName, wl.HisNode.Name, WebUser.Name),
                    wfemp.Email, null, mytemp);
            }
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
        private bool IsStopFlow = false;
        /// <summary>
        /// 检查流程、节点的完成条件
        /// </summary>
        /// <returns></returns>
        private string CheckCompleteCondition()
        {
            this.IsStopFlow = false;
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
                    this.IsStopFlow = true;
                    // string path = System.Web.HttpContext.Current.Request.ApplicationPath;
                    return msg + "@符合工作流程完成条件" + this.HisFlowCompleteConditions.ConditionDesc + "" + overMsg + " @查看<img src='./../Images/Btn/PrintWorkRpt.gif' ><a href='WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "'target='_blank' >工作报告</a>";
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
            #region 启动下一个节点之前进行必要的判断。
            string msg = this.CheckCompleteCondition();
            if (this.IsStopFlow == true)
            {
                /*在检查完后，流程已经停止了。*/
                return msg;
            }
            switch (this.HisNode.HisNodeWorkType)
            {
                case NodeWorkType.WorkHL:
                case NodeWorkType.WorkFHL:

                    if (this.HisNode.IsForceKill)
                    {
                        // 检查是否还有没有完成的进程。
                        WorkerLists wls = new WorkerLists();
                        wls.Retrieve(WorkerListAttr.FID, this.HisWork.OID,
                            WorkerListAttr.IsPass, 0);
                        if (wls.Count > 0)
                        {
                            msg += "@开始进行对没有完成分流工作的同事强制删除。";
                            foreach (WorkerList wl in wls)
                            {
                                WorkFlow wf = new WorkFlow(wl.FK_Flow, wl.WorkID, wl.FID);
                                wf.DoDeleteWorkFlowByReal();
                                msg += "@已经把（" + wl.FK_Emp + " , " + wl.FK_EmpText + "）的工作删除。";
                            }
                        }
                    }
                    else
                    {
                        DataTable dt = DBAccess.RunSQLReturnTable("SELECT b.No,b.Name FROM WF_GenerWorkerList a,Port_Emp b WHERE a.FK_Emp=b.No AND IsPass=0 AND FID=" + this.HisWork.OID);
                        if (dt.Rows.Count != 0)
                        {
                            msg = "@执行完成错误，有如下人员没有完成工作。";
                            foreach (DataRow dr in dt.Rows)
                            {
                                msg += "@" + dr[0].ToString() + " - " + dr[1].ToString();
                            }
                            msg += "@以上每个成员都完成后，才可以执行此操作。";
                            throw new Exception(msg);
                        }
                    }
                    break;
                case NodeWorkType.Work:
                case NodeWorkType.StartWork:
                    // 合理的结束。
                    break;
                default:
                    break;
                //throw new Exception("@流程设计错误，结束节点的类型不能为:(" + this.HisNode.HisNodeWorkTypeT + ") 类型.");
            }
            #endregion

            // 取当前节点Nodes.
            Nodes toNodes = this.HisNode.HisToNodes;
            if (toNodes.Count == 0)
            {
                /* 如果是最后一个节点，就设置流程结束。*/
                string ovrMsg = this.HisWorkFlow.DoFlowOver();
                if (this.HisNode.HisFormType == FormType.SDKForm)
                    return ovrMsg + this.ToE("WN0", "@此工作流程运行到最后一个环节，工作成功结束！");
                else
                    return ovrMsg + this.ToE("WN0", "@此工作流程运行到最后一个环节，工作成功结束！") + "<img src='" + this.VirPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + this.HisWork.OID + "&FID=" + this.HisWork.FID + "&FK_Flow=" + this.HisNode.FK_Flow + "&FK_Node=" + this.HisNode.NodeID + "'target='_blank' >" + this.ToE("WorkRpt", "工作报告") + "</a>";
            }

            // 如果只有一个转向节点, 就不用判断条件了,直接转向他.
            if (toNodes.Count == 1)
                return msg + StartNextNode((Node)toNodes[0]);

            Node toNode = null;
            int numOfWay = 0;
            string condMsg = "";
            foreach (Node nd in toNodes)
            {
                Conds dcs = new Conds();
                QueryObject qo = new QueryObject(dcs);
                qo.AddWhere(CondAttr.NodeID, this.HisNode.NodeID);
                qo.addAnd();
                qo.AddWhere(CondAttr.ToNodeID, nd.NodeID);
                qo.DoQuery();
                foreach (Cond dc in dcs)
                {
                    dc.WorkID = this.HisWork.OID;
                }


                if (dcs.Count == 0)
                {
                    throw new Exception(string.Format(this.ToE("WN10",
                        "@定义节点的方向条件错误:没有给从{0}节点到{1},定义转向条件."),
                        this.HisNode.NodeID + this.HisNode.Name, nd.NodeID + nd.Name));
                }

                if (dcs.IsPass) // 如果多个转向条件中有一个成立.
                {
                    numOfWay++;
                    toNode = nd;
                    break;
                }
                condMsg += "<b>@检查方向条件：到节点：" + nd.Name + "</b>";
                condMsg += dcs.MsgOfDesc;
            }

            if (toNode == null)
                throw new Exception(string.Format(this.ToE("WN11", "@转向条件设置错误:节点名称{0}, 系统无法投递。"),
                    this.HisNode.Name));

            /* 删除曾经在这个步骤上的流程运行数据。
             * 比如说：方向条件，发生了变化后可能产生两个工作上的数据。是为了工作报告上面体现了两个步骤。 */
            foreach (Node nd in toNodes)
            {
                if (nd.NodeID == toNode.NodeID)
                    continue;

                // 删除这个工作，因为这个工作的数据不在有用了。
                Work wk = nd.HisWork;
                wk.OID = this.HisWork.OID;
                if (wk.Delete() != 0)
                {
                }
            }
            msg += StartNextNode(toNode);
            return msg;
        }
        #region 启动审核节点
        /// <summary>
        /// 启动指定的下一个节点 .
        /// </summary>
        /// <param name="nd">要启动的节点</param>
        public string StartNextNode(Node nd)
        {
            string msg = "";
            try
            {
                switch (nd.HisNodeWorkType)
                {
                    case NodeWorkType.WorkHL:
                    case NodeWorkType.WorkFHL:  // 如果下一个节点是合流节点，或者是分合流节点。
                        // 判断当前节点的类型是什么.
                        switch (this.HisNode.HisNodeWorkType)
                        {
                            case NodeWorkType.Work:
                            case NodeWorkType.StartWork:
                                if (this.HisWork.FID == 0 )
                                    msg = StartNextWorkNodeHeLiu_WithOutFID(nd);  /* 没有流程ID,比如: */
                                else
                                    msg = StartNextWorkNodeHeLiu_WithFID(nd);  /* 合流节点, 开始节点是分流节点 */
                                break;
                            default:
                                throw new Exception("@没有判断的情况。");
                        }

                        msg += "@<a href='" + this.VirPath + "/WF/MyFlowInfo"+Glo.FromPageType+".aspx?DoType=UnSend&FID=" + this.HisWork.FID + "&WorkID=" + this.WorkID + "&FK_Flow=" + nd.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "撤销本次发送") + "</a>。";
                        return msg;
                    case NodeWorkType.StartWork:
                    case NodeWorkType.StartWorkFL:
                        throw new Exception("System Error ");
                        break;
                    default:
                        return StartNextWorkNodeOrdinary(nd);  /* 普通节点 */
                }
                // this.InitEmps(nd);
            }
            catch (Exception ex)
            {
                throw new Exception("@" + this.ToE("StartNextNodeErr", "@启动下一个节点出现错误") + ":" + ex.Message); //启动下一个节点出现错误
            }
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

#warning 调试使用，一直没有正确的获取到gwls
            WorkerLists mygwls = new WorkerLists();
            mygwls.Retrieve(WorkerListAttr.WorkID, this.HisWork.OID, WorkerListAttr.FK_Node, nd.NodeID);
            if (mygwls.Count != gwls.Count)
            {
                Log.DefaultLogWriteLineError("@仍然没有解决邮件发送到错误人的问题。Workid="+this.HisWork.OID+" =FK_Node="+nd.NodeID);
            }
#warning 调试使用，一直没有正确的获取到gwls

            this.AddIntoWacthDog(mygwls); //@加入消息集合里。


            string msg = "";
            //msg = "@任务自动下达给'<font color=blue><b>" + this.nextStationName + "</b></font>'如下" + this._RememberMe.NumOfObjs + "位同事：@" + this._RememberMe.EmpsExt;
            //msg = "@" + this.ToE("TaskAutoSendTo") + " <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ：@" + this._RememberMe.EmpsExt;

            msg = this.ToEP3("TaskAutoSendTo", "@任务自动下达给{0}如下{1}位同事,{2}.", this.nextStationName,
                this._RememberMe.NumOfObjs.ToString(), this._RememberMe.EmpsExt);  // +" <font color=blue><b>" + this.nextStationName + "</b></font>   " + this._RememberMe.NumOfObjs + " ：@" + this._RememberMe.EmpsExt;
            if (this._RememberMe.NumOfEmps >= 2)
            {
                if (WebUser.IsWap)
                    msg += "<a href=\"" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "&FK_Flow=" + nd.FK_Flow + "')\"><img src='./Img/AllotTask.gif' border=0/>" + this.ToE("WN24", "指定特定的同事处理") + "</a>。";
                else
                    msg += "<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/AllotTask.aspx?WorkID=" + this.WorkID + "&NodeID=" + nd.NodeID + "&FK_Flow=" + nd.FK_Flow + "')\"><img src='./Img/AllotTask.gif' border=0/>" + this.ToE("WN24", "指定特定的同事处理") + "</a>。";
            }
            msg += this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID);

            if (WebUser.IsWap == false)
                msg += "@<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "');\" ><img src='" + this.VirPath + "/WF/Img/SMS.gif' border=0 />" + this.ToE("WN21", "发手机短信提醒他(们)") + "</a>";

            if (this.HisNode.HisFormType != FormType.SDKForm)
            {
                if (this.HisNode.IsStartNode)
                    msg += "@<a href='" + this.VirPath + "/WF/MyFlowInfo"+Glo.FromPageType+".aspx?DoType=UnSend&WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "撤销本次发送") + "</a>， <a href='" + this.VirPath + "/WF/MyFlow"+Glo.FromPageType+".aspx?FK_Flow=" + nd.FK_Flow + "'><img src=" + this.VirPath + "/WF/Img/New.gif border=0/>" + this.ToE("NewFlow", "新建流程") + "</a>。";
                else
                    msg += "@<a href='" + this.VirPath + "/WF/MyFlowInfo"+Glo.FromPageType+".aspx?DoType=UnSend&WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'><img src='" + this.VirPath + "/WF/Img/UnDo.gif' border=0/>" + this.ToE("WN22", "撤销本次发送") + "</a>。";
            }

            /* 形成消息通知他们 */
            if (nd.IsPCNode == false && SystemConfig.IsBSsystem)
            {
                MsgsManager.AddMsgs(gwls, town.HisNode.FlowName, nd.Name, "Test");
            }


            string str = "";
            //if (this._RememberMe.NumOfEmps > 1)
            //    str = "<a href='" + appPath + "/WF/AllotTask.aspx?NodeId=" + nd.NodeID + "&WorkID=" + wk.OID + "&FlowNo=" + nd.FK_Flow + "&dt='" + DateTime.Now.ToString("MMddhhmmss") + " target='_self' >" + this.ToE("AllotWork", "分配工作") + "</a>";
            //return msg + "@您可以" + str + "查看<img src='" + appPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + appPath + "/WF/WFRpt.aspx?WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "'target='_blank' >工作报告</a>,进行<img src='" + appPath + "/Images/Btn/WorkFlowOp.gif' ><a href='" + appPath + "/WF/OpWorkFlow.aspx?WorkID=" + wk.OID + "&FK_Flow=" + nd.FK_Flow + "' target='_blank' >流程操作</a> 。";

            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FK_Node=" + nd.NodeID + " WHERE WorkID=" + this.HisWork.OID);


            if (this.HisNode.HisFormType == FormType.SDKForm)
                return msg;
            else
                return msg + "@" + str + " <img src='" + this.VirPath + "/Images/Btn/PrintWorkRpt.gif' ><a href='" + this.VirPath + "/WF/WFRpt.aspx?WorkID=" + wk.OID + "&FID=" + wk.FID + "&FK_Flow=" + nd.FK_Flow + "'target='_blank' >" + this.ToE("WorkRpt", "工作报告") + "</a>。";
        }
        public string GenerWhySendToThem(int fNodeID, int toNodeID)
        {
            return "";
            //return "@<a href='WhySendToThem.aspx?NodeID=" + fNodeID + "&ToNodeID=" + toNodeID + "&WorkID=" + this.WorkID + "' target=_blank >" + this.ToE("WN20", "为什么要发送给他们？") + "</a>";
        }
        /// <summary>
        /// 工作流程ID
        /// </summary>
        public static Int64 FID = 0;
        /// <summary>
        /// 没有FID
        /// </summary>
        /// <param name="nd"></param>
        /// <returns></returns>
        private string StartNextWorkNodeHeLiu_WithOutFID(Node nd)
        {
            return null;
        }
        /// <summary>
        /// 有FID
        /// </summary>
        /// <param name="nd"></param>
        /// <returns></returns>
        private string StartNextWorkNodeHeLiu_WithFID(Node nd)
        {
            GenerFH myfh = new GenerFH(this.HisWork.FID);
            if (myfh.FK_Node == nd.NodeID)
            {
                /* 说明不是第一次到这个节点上来了, 
                 * 比如：一条流程：
                 * A分流-> B普通-> C合流
                 * 从B 到C 中, B中有N 个线程，在之前已经有一个线程到达过C.
                 */

                /* 
                 * 首先:更新它的节点 worklist 信息, 说明当前节点已经完成了.
                 * 不让当前的操作员能看到自己的工作。
                 */

                DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=1  WHERE WorkID=" + this.WorkID + " AND FID=" + this.HisWork.FID + " AND FK_Node=" + this.HisNode.NodeID);
                DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FK_Node=" + nd.NodeID + " WHERE WorkID=" + this.HisWork.OID);

                /*
                 * 其次更新当前节点的状态与完成时间.
                 */
                this.HisWork.Update(WorkAttr.NodeState, (int)NodeState.Complete,
                    WorkAttr.CDT, BP.DA.DataType.CurrentDataTime);

                #region 处理完成率
                string sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND FID=" + this.HisWork.FID + " AND IsPass=1";
                decimal ok = (decimal)DBAccess.RunSQLReturnValInt(sql);
                sql = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND FID=" + this.HisWork.FID;
                decimal all = (decimal)DBAccess.RunSQLReturnValInt(sql);
                decimal passRate = ok / all * 100;
                string numStr = "@您是第(" + ok + ")到达此节点上的同事。";
                if (nd.PassRate <= passRate)
                {
                    /*说明全部的人员都完成了，就让河流点显示它。*/
                    DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0  WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.FID);
                    numStr += "@下一步工作(" + nd.Name + ")已经启动。";
                }
                #endregion 处理完成率


                return "@流程已经运行到合流节点[" + nd.Name + "]，当前工作已经完成.@您的工作已经发送给如下人员[" + myfh.ToEmpsMsg + "]，<a href=\"javascript:WinOpen('./Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >短信通知他们</a>。" + this.GenerWhySendToThem(this.HisNode.NodeID, nd.NodeID) + numStr;
            }
            /* 已经有FID，说明：以前已经有分流或者合流节点。*/
            /*
             * 以下处理的是没有流程到达此位置
             * 说明是第一次到这个节点上来了.
             * 比如：一条流程:
             * A分流-> B普通-> C合流
             * 从B 到C 中, B中有N 个线程，在之前他是第一个到达C.
             */

            // 首先找到此节点的接受人员的集合。做为 FID 合流分流的FID。
            WorkNode town = new WorkNode(nd.HisWork, nd);

            // 初试化他们的工作人员．
            WorkerLists gwls = this.GenerWorkerLists(town);
            string fk_emp = "";
            string toEmpsStr = "";
            string emps = "";
            foreach (WorkerList wl in gwls)
            {
                fk_emp = wl.FK_Emp;
                if (Glo.IsShowUserNoOnly)
                    toEmpsStr += wl.FK_Emp + "、";
                else
                    toEmpsStr += wl.FK_Emp + "<" + wl.FK_EmpText + ">、";

                if (gwls.Count == 1)
                    emps = fk_emp;
                else
                    emps += "@" + fk_emp;
            }

            /* 
            * 更新它的节点 worklist 信息, 说明当前节点已经完成了.
            * 不让当前的操作员能看到自己的工作。
            */
            // DBAccess.RunSQL("UPDATE WF_GenerWorkerlist SET IsPass=1,FK_Node="+nd.NodeID+" WHERE WorkID=" + this.WorkID + " AND FID=" + this.HisWork.FID + " AND FK_Node=" + this.HisNode.NodeID);


            #region 设置父流程状态 设置当前的节点为:
            myfh.Update(GenerFHAttr.FK_Node, nd.NodeID,
                GenerFHAttr.ToEmpsMsg, toEmpsStr);

            Work mainWK = town.HisWork;
            mainWK.OID = this.HisWork.FID;
            if (mainWK.RetrieveFromDBSources() == 1)
                mainWK.Delete();

            mainWK.NodeState = NodeState.Init;
            mainWK.Rec = fk_emp;
            mainWK.Emps = emps;
            mainWK.Insert();

            /* 
             * 为了解决 大连需求
             *  河流点需要等待各个分流点全部处理完后才能看到它。
             */
            string sql1 = "SELECT COUNT(*) AS Num FROM WF_GenerWorkerList WHERE FK_Node=" + this.HisNode.NodeID + " AND FID=" + this.HisWork.FID;
            decimal numAll1 = (decimal)DBAccess.RunSQLReturnValInt(sql1);
            decimal passRate1 = 1 / numAll1 * 100;
            if (nd.PassRate <= passRate1)
            {
                DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=0,WorkID=" + this.HisWork.FID + ",FID=0 WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.OID);
            }
            else
            {

#warning 为了不让其显示在途的工作需要， =3 不是正常的处理模式。
                DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=3,WorkID=" + this.HisWork.FID + ",FID=0 WHERE FK_Node=" + nd.NodeID + " AND WorkID=" + this.HisWork.OID);
            }

            DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET FK_Node=" + nd.NodeID + " WHERE WorkID=" + this.HisWork.FID);
            #endregion 设置父流程状态

            return "@当前工作已经完成，流程已经运行到合流节点[" + nd.Name + "]。@您的工作已经发送给如下人员[" + toEmpsStr + "]，<a href=\"javascript:WinOpen('" + this.VirPath + "/WF/Msg/SMS.aspx?WorkID=" + this.WorkID + "&FK_Node=" + nd.NodeID + "')\" >短信通知他们</a>。" + "@您是第一个到达此节点的同事.";
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
                    wk.Update();
                }

                #region 复制多选数据
                M2Ms m2ms = new M2Ms(this.HisNode.NodeID, this.WorkID);
                if (m2ms.Count >= 1)
                {
                    foreach (M2M item in m2ms)
                    {
                        M2M m2 = new M2M();
                        m2.Copy(item);
                        m2.FK_Node = nd.NodeID;
                        m2.WorkID = this.WorkID;
                        m2.MapM2M = m2.MapM2M.Replace("ND" + this.HisNode.NodeID, "ND" + nd.NodeID);
                        try
                        {
                            m2.Insert();
                        }
                        catch
                        {
                            m2.Update();
                        }
                    }
                }
                #endregion

                #region 复制明细数据。
                Sys.MapDtls dtls = new BP.Sys.MapDtls("ND" + this.HisNode.NodeID);
                if (dtls.Count >= 1)
                {
                    Sys.MapDtls toDtls = new BP.Sys.MapDtls("ND" + nd.NodeID);
                    Sys.MapDtls startDtls = null;
                    foreach (MapDtl dtl in toDtls)
                    {
                        if (dtl.IsEnablePass)
                        {
                            startDtls = new BP.Sys.MapDtls("ND" + int.Parse(nd.FK_Flow) + "01");
                        }
                    }

                    int i = -1;
                    foreach (Sys.MapDtl dtl in dtls)
                    {
                        i++;
                        if (toDtls.Count <= i)
                            continue;

                        Sys.MapDtl toDtl = (Sys.MapDtl)toDtls[i];

                        if (dtl.IsEnablePass == true)
                        {
                            /*如果启用了是否明细表的审核通过机制,就允许copy节点数据。*/
                            toDtl.IsCopyNDData = true;
                        }

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
                                qo.AddWhere(GEDtlAttr.RefPK, this.WorkID);
                                break;
                            case DtlOpenType.ForFID:
                                qo.AddWhere(GEDtlAttr.FID, this.WorkID);
                                break;
                        }
                        qo.DoQuery();
                        int unPass = 0;
                        // 是否起用审核机制。
                        bool isEnablePass = dtl.IsEnablePass;
                        if (isEnablePass == true)
                        {
                            /*判断当前节点该明细表上是否有，isPass 审核字段，如果没有抛出异常信息。*/
                        }
                        foreach (GEDtl gedtl in gedtls)
                        {
                            if (isEnablePass)
                            {
                                if (gedtl.GetValBooleanByKey("IsPass") == false)
                                {
                                    /*没有审核通过的就 continue 它们，仅复制已经审批通过的.*/
                                    continue;
                                }
                            }

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

                        if (isEnablePass)
                        {
                            /* 如果启用了审核通过机制，就把未审核的数据copy到第一个节点上去 
                             * 1, 找到对应的明细点.
                             * 2, 把未审核通过的数据复制到开始明细表里.
                             */

                            string startTable = "ND" + int.Parse(nd.FK_Flow) + "01";
                            string startUser = "SELECT Rec FROM " + startTable + " WHERE OID=" + this.WorkID;
                            startUser = DBAccess.RunSQLReturnString(startUser);

                            //this.HisWorkFlow.StartNodeID;

                            MapDtl startDtl = (MapDtl)startDtls[i];
                            foreach (GEDtl gedtl in gedtls)
                            {
                                if (gedtl.GetValBooleanByKey("IsPass"))
                                    continue; /* 排除审核通过的 */

                                BP.Sys.GEDtl dtCopy = new GEDtl(startDtl.No);
                                dtCopy.Copy(gedtl);
                                dtCopy.OID = 0;
                                dtCopy.FK_MapDtl = startDtl.No;
                                dtCopy.RefPK = gedtl.OID.ToString();// this.WorkID.ToString();

                                dtCopy.SetValByKey("BatchID", this.WorkID);
                                dtCopy.SetValByKey("IsPass", 0);
                                dtCopy.SetValByKey("Rec", startUser);
                                dtCopy.SetValByKey("Checker", BP.Web.WebUser.Name);
                                dtCopy.Insert();
                            }
                            DBAccess.RunSQL("UPDATE " + startDtl.PTable + " SET Rec='" + startUser + "',Checker='" + WebUser.No + "' WHERE BatchID=" + this.WorkID + " AND Rec='" + WebUser.No + "'");
                        }
                    }
                }
                #endregion 复制明细数据
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
                catch
                {
                    #region 解决工作节点字段修复问题
                    try
                    {
                        wk.CheckPhysicsTable();
                        wk.DirectSave();
                    }
                    catch (Exception ex)
                    {
                        Log.DebugWriteInfo(this.ToE("SaveWorkErr", "保存工作错误") + "：" + ex.Message);
                        throw new Exception(this.ToE("SaveWorkErr", "保存工作错误  ") + wk.EnDesc + ex.Message);
                    }
                    #endregion 解决字段修复问题
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
        public WorkNode GetPreviousWorkNode_FHL(Int64 workid)
        {
            WorkNodes wns = this.GetPreviousWorkNodes_FHL();
            foreach (WorkNode wn in wns)
            {
                if (wn.HisWork.OID == workid)
                    return wn;
            }
            return null;
        }
        public WorkNodes GetPreviousWorkNodes_FHL()
        {
            // 如果没有找到转向他的节点,就返回,当前的工作.
            if (this.HisNode.IsStartNode)
                throw new Exception("@" + this.ToE("WN14", "此节点是开始节点,没有上一步工作")); //此节点是开始节点,没有上一步工作.

            if (this.HisNode.HisNodeWorkType == NodeWorkType.WorkHL
               || this.HisNode.HisNodeWorkType == NodeWorkType.WorkFHL)
            {
            }
            else
            {
                throw new Exception("@当前工作节 - 非是分合流节点。");
            }

            WorkNodes wns = new WorkNodes();
            Nodes nds = this.HisNode.HisFromNodes;
            foreach (Node nd in nds)
            {
                Works wks = (Works)nd.HisWorks;
                wks.Retrieve(WorkAttr.FID, this.HisWork.OID);

                if (wks.Count == 0)
                    continue;

                foreach (Work wk in wks)
                {
                    WorkNode wn = new WorkNode(wk, nd);
                    wns.Add(wn);
                }
            }
            return wns;
        }
        /// <summary>
        /// 得当他的上一步工作
        /// 1, 从当前的找到他的上一步工作的节点集合.		 
        /// 如果没有找到转向他的节点,就返回,当前的工作.
        /// 
        /// </summary>
        /// <returns>得当他的上一步工作</returns>
        public WorkNode GetPreviousWorkNode()
        {
            // 如果没有找到转向他的节点,就返回,当前的工作.
            if (this.HisNode.IsStartNode)
                throw new Exception("@" + this.ToE("WN14", "此节点是开始节点,没有上一步工作")); //此节点是开始节点,没有上一步工作.

            WorkNodes wns = new WorkNodes();
            Nodes nds = this.HisNode.HisFromNodes;
            foreach (Node nd in nds)
            {
                switch (this.HisNode.HisNodeWorkType)
                {
                    case NodeWorkType.WorkHL: /* 如果是合流 */
                        if (this.IsSubFlowWorkNode == false)
                        {
                            /* 如果不是线程 */
                            Node pnd = nd.HisPriFLNode;
                            if (pnd == null)
                                throw new Exception("@没有取道它的上一步骤的分流节点，请确认设计是否错误？");

                            Work wk1 = (Work)pnd.HisWorks.GetNewEntity;
                            wk1.OID = this.HisWork.OID;
                            if (wk1.RetrieveFromDBSources() == 0)
                                continue;
                            WorkNode wn11 = new WorkNode(wk1, pnd);
                            return wn11;
                            break;
                        }
                        break;
                    default:
                        break;
                }

                Work wk = (Work)nd.HisWorks.GetNewEntity;
                wk.OID = this.HisWork.OID;
                if (wk.RetrieveFromDBSources() == 0)
                    continue;

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
                    GenerWorkFlow wgf = new GenerWorkFlow(this.HisWork.OID);
                    throw new Exception("@当前的工作没有正确的处理, 没有下一步骤工作节点.也可能是你非法删除了工作表中的记录,造成的没有找到,下一步工作内容,但是系统已经恢复当前节点为为完成状态,此流程可以正常的运行下去.");
                }
                else
                {
                    /*说名已经产生工作者列表.*/
                    DBAccess.RunSQL("DELETE FROM WF_GenerWorkerlist WHERE WorkID=" + this.HisWork.OID + " AND FK_Node=" + nd.NodeID);
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
            this.Clear();

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
                //if (nd.HisFNType == FNType.River)
                //    continue;

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
