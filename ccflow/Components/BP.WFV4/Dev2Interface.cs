using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Text;
using BP.WF;
using BP.DA;
using BP.Web;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// 此接口为程序员二次开发使用
    /// </summary>
    public class Dev2Interface
    {
        #region 自动执行
        /// <summary>
        /// 自动执行开始节点数据
        /// </summary>
        public static void DTS_AutoStarterFlow(Flow fl)
        {
            #region 读取数据.
            BP.Sys.MapExt me = new Sys.MapExt();
            int i = me.Retrieve(MapExtAttr.FK_MapData, "ND" + int.Parse(fl.No) + "01",
                MapExtAttr.ExtType, "PageLoadFull");
            if (i == 0)
            {
                BP.DA.Log.DefaultLogWriteLineError("没有为流程(" + fl.Name + ")的开始节点设置发起数据,请参考说明书解决.");
                return;
            }

            // 获取从表数据.
            DataSet ds = new DataSet();
            string[] dtlSQLs = me.Tag1.Split('*');
            foreach (string sql in dtlSQLs)
            {
                if (string.IsNullOrEmpty(sql))
                    continue;

                string[] tempStrs = sql.Split('=');
                string dtlName = tempStrs[0];
                DataTable dtlTable = BP.DA.DBAccess.RunSQLReturnTable(sql.Replace(dtlName + "=", ""));
                dtlTable.TableName = dtlName;
                ds.Tables.Add(dtlTable);
            }
            #endregion 读取数据.

            #region 检查数据源是否正确.
            string errMsg = "";
            // 获取主表数据.
            DataTable dtMain = BP.DA.DBAccess.RunSQLReturnTable(me.Tag);
            if (dtMain.Columns.Contains("Starter") == false)
                errMsg += "@配值的主表中没有Starter列.";

            if (dtMain.Columns.Contains("MainPK") == false)
                errMsg += "@配值的主表中没有MainPK列.";
           
            if (errMsg.Length > 2)
            {
                BP.DA.Log.DefaultLogWriteLineError("流程(" + fl.Name + ")的开始节点设置发起数据,不完整." + errMsg);
                return;
            }
            #endregion 检查数据源是否正确.

            #region 处理流程发起.
            
            string nodeTable = "ND" + int.Parse(fl.No) + "01";            
            foreach (DataRow dr in dtMain.Rows)
            {
                string mainPK = dr["MainPK"].ToString();
                string sql = "SELECT OID FROM " + nodeTable + " WHERE MainPK='" + mainPK + "'";
                if (DBAccess.RunSQLReturnTable(sql).Rows.Count != 0)
                    continue; /*说明已经调度过了*/

                string starter = dr["Starter"].ToString();
                if (Web.WebUser.No != starter)
                {
                    BP.Web.WebUser.Exit();
                    BP.Port.Emp emp = new BP.Port.Emp();
                    emp.No = starter;
                    if (emp.RetrieveFromDBSources() == 0)
                    {
                        BP.DA.Log.DefaultLogWriteLineInfo("@数据驱动方式发起流程("+fl.Name+")设置的发起人员:" + emp.No + "不存在。");
                        continue;
                    }

                    BP.Web.WebUser.SignInOfGener(emp);
                }

                #region  给值.
                Work wk = fl.NewWork();
                foreach (DataColumn dc in dtMain.Columns)
                    wk.SetValByKey(dc.ColumnName, dr[dc.ColumnName].ToString());

                if (ds.Tables.Count != 0)
                {
                    string refPK = dr["MainPK"].ToString();
                    // MapData md = new MapData(nodeTable);
                    MapDtls dtls = new MapDtls(nodeTable);
                    foreach (MapDtl dtl in dtls)
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (dt.TableName != dtl.No)
                                continue;

                            //删除原来的数据。
                            GEDtl dtlEn = dtl.HisGEDtl;
                            dtlEn.Delete(GEDtlAttr.RefPK, wk.OID.ToString());

                            // 执行数据插入。
                            foreach (DataRow drDtl in dt.Rows)
                            {
                                if (drDtl["RefMainPK"].ToString() != refPK)
                                    continue;

                                dtlEn = dtl.HisGEDtl;

                                foreach (DataColumn dc in dt.Columns)
                                    dtlEn.SetValByKey(dc.ColumnName, drDtl[dc.ColumnName].ToString());

                                dtlEn.RefPK = wk.OID.ToString();
                                dtlEn.Insert();
                            }
                        }
                    }
                }
                #endregion  给值.

                // 处理发送信息.
                Node nd = fl.HisStartNode;
                try
                {
                    WorkNode wn = new WorkNode(wk, nd);
                    string msg = wn.AfterNodeSave();
                    BP.DA.Log.DefaultLogWriteLineInfo(msg);
                }
                catch (Exception ex)
                {
                    BP.DA.Log.DefaultLogWriteLineWarning(ex.Message);
                }
            }
            #endregion 处理流程发起.

        }
        #endregion

        #region 数据接口

        #region 获取当前操作员可以发起的流程集合
        /// <summary>
        /// 获取当前操作员可以发起的流程集合
        /// </summary>
        /// <returns>bp.wf.flows</returns>
        public static Flows DB_GenerCanStartFlowsOfEntities()
        {
            return DB_GenerCanStartFlowsOfEntities(WebUser.No);
        }
        public static Flows DB_GenerCanStartFlowsOfEntities(string userNo)
        {
            string sql = "SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND ( WhoExeIt=0 OR WhoExeIt=2 ) AND NodeID IN ( SELECT FK_Node FROM WF_NodeStation WHERE FK_Station IN (SELECT FK_Station FROM Port_EmpStation WHERE FK_EMP='" + WebUser.No + "')) ";
            string sql2 = " UNION  SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND ( WhoExeIt=0 OR WhoExeIt=2 ) AND NodeID IN ( SELECT FK_Node FROM WF_NodeEmp WHERE FK_Emp='" + userNo + "' ) ";
         //   string sql3 = " UNION  SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND NodeID IN ( SELECT FK_Node FROM WF_NodeEmp WHERE FK_Emp='" + userNo + "' ) ";
           // System.Web.cont
            Flows fls = new Flows();
            BP.En.QueryObject qo = new BP.En.QueryObject(fls);
            qo.AddWhereInSQL("No", sql + sql2 );
            qo.addAnd();
            qo.AddWhere(FlowAttr.IsOK, true);
            qo.addAnd();
            qo.AddWhere(FlowAttr.IsCanStart, true);
            qo.addOrderBy("FK_FlowSort", "No");
            qo.DoQuery();
            return fls;
        }
        /// <summary>
        /// 获取当前操作员可以发起的流程集合
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public static DataTable DB_GenerCanStartFlowsOfDataTable(string userNo)
        {
            return DB_GenerCanStartFlowsOfEntities(userNo).ToDataTableField();
        }
        #endregion 获取当前操作员可以发起的流程集合


        #region 获取当前操作员的待办工作
        /// <summary>
        /// 获取当前操作员的待办工作
        /// </summary>
        /// <param name="fk_flow">根据流程编号，如果流程编号为空则返回全部</param>
        /// <returns>当前操作员待办工作</returns>
        public static DataTable DB_GenerEmpWorksOfDataTable()
        {
            return DB_GenerEmpWorksOfDataTable(WebUser.No);
        }
        public static DataTable DB_GenerEmpWorksOfDataTable(string fk_emp)
        {
            return BP.DA.DBAccess.RunSQLReturnTable("SELECT * FROM WF_EmpWorks WHERE FK_Emp='" + fk_emp + "'  ORDER BY ADT DESC ");
        }
        #endregion 获取当前操作员的待办工作


        #region 获取当前操作员的待办工作
        /// <summary>
        /// 获取当前节点可以退回的节点，以方便退回的二次开发。
        /// </summary>
        /// <param name="fk_node">当前节点</param>
        /// <param name="workid">工作ID</param>
        /// <returns></returns>
        public static DataTable DB_GenerWillReturnNodes(int fk_node, Int64 workid)
        {
            DataTable dt = new DataTable("obt");
            dt.Columns.Add("No");
            dt.Columns.Add("Name");

            Node nd = new Node(fk_node);
            WorkNode wn = new WorkNode(workid, fk_node);
            WorkNodes wns = new WorkNodes();
            switch (nd.HisReturnRole)
            {
                case ReturnRole.CanNotReturn:
                    return dt;
                case ReturnRole.ReturnAnyNodes:
                    if (wns.Count == 0)
                        wns.GenerByWorkID(wn.HisNode.HisFlow, workid);

                    foreach (WorkNode mywn in wns)
                    {
                        if (mywn.HisNode.NodeID == fk_node)
                            continue;

                        DataRow dr = dt.NewRow();
                        dr["No"] = mywn.HisNode.NodeID.ToString();
                        dr["Name"] = mywn.HisWork.RecText + "=>" + mywn.HisNode.Name;
                        dt.Rows.Add(dr);
                    }
                    break;
                case ReturnRole.ReturnPreviousNode:
                    WorkNode mywnP = wn.GetPreviousWorkNode();
                  //  turnTo = mywnP.HisWork.Rec + mywnP.HisWork.RecText;
                    DataRow dr1 = dt.NewRow();
                    dr1["No"] = mywnP.HisNode.NodeID.ToString();
                    dr1["Name"] = mywnP.HisWork.RecText + "=>" + mywnP.HisNode.Name;
                    dt.Rows.Add(dr1);
                    break;
                case ReturnRole.ReturnSpecifiedNodes: //退回指定的节点。
                    if (wns.Count == 0)
                        wns.GenerByWorkID(wn.HisNode.HisFlow, workid);
                    NodeReturns rnds = new NodeReturns();
                    rnds.Retrieve(NodeReturnAttr.FK_Node, fk_node);
                    if (rnds.Count == 0)
                        throw new Exception("@流程设计错误，您设置该节点可以退回指定的节点，但是指定的节点集合为空，请在节点属性设置它的制订节点。");
                    foreach (WorkNode mywn in wns)
                    {
                        if (mywn.HisNode.NodeID == fk_node)
                            continue;

                        if (rnds.Contains(NodeReturnAttr.ReturnN,
                            mywn.HisNode.NodeID) == false)
                            continue;

                        DataRow dr = dt.NewRow();
                        dr["No"] = mywn.HisNode.NodeID.ToString();
                        dr["Name"] = mywn.HisWork.RecText + "=>" + mywn.HisNode.Name;
                        dt.Rows.Add(dr);
                    }
                    break;
                default:
                    throw new Exception("@没有判断的退回类型。");
            }
            return dt;
        }
        #endregion 获取当前操作员的待办工作



        #region 获取当前操作员的在途工作
        /// <summary>m
        /// 获取当前操作员的在途工作
        /// </summary>
        /// <returns>在途工作</returns>
        public static GenerWorkFlows DB_GenerRuningOfEntities()
        {
            return DB_GenerRuningOfEntities(WebUser.No);
        }
        public static GenerWorkFlows DB_GenerRuningOfEntities(string userNo)
        {
            string sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE A.WorkID=B.WorkID AND B.FK_EMP='" + userNo + "' AND B.IsEnable=1 AND B.IsPass=1 ";
            GenerWorkFlows gwfs = new GenerWorkFlows();
            gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
            return gwfs;
        }
        /// <summary>
        /// 获取当前操作员的在途工作
        /// </summary>
        /// <returns>在途工作</returns>
        public static DataTable DB_GenerRuningOfDataTable()
        {
            return DB_GenerRuningOfEntities().ToDataTableField();
        }
        public static DataTable DB_GenerRuningOfDataTable(string userNo)
        {
            return DB_GenerRuningOfEntities(userNo).ToDataTableField();
        }
        #endregion 获取当前操作员的待办工作


        #endregion

        #region UI 接口
        /// <summary>
        /// 获取按钮状态
        /// </summary>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="workid">流程ID</param>
        /// <returns>返回按钮状态</returns>
        public static ButtonState UI_GetButtonState(string fk_flow, int fk_node, Int64 workid)
        {
            ButtonState bs = new ButtonState(fk_flow,fk_node, workid);
            return bs;
        }
        /// <summary>
        /// 打开退回窗口
        /// </summary>
        public static void UI_OpenReturnWindow(Int64 workid)
        {
        }
        #endregion UI 接口

        #region 登录接口
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userNo">用户名</param>
        /// <param name="sid">安全ID</param>
        public static void Port_Login(string userNo, string sid)
        {
            string sql = "select sid from port_emp where no='"+userNo+"'";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
                throw new Exception("用户不存在或者SID错误。");

            if (dt.Rows[0]["SID"].ToString()!=sid)
                throw new Exception("用户不存在或者SID错误。");

            BP.Port.Emp emp = new BP.Port.Emp(userNo);
            WebUser.SignInOfGener(emp, true);
            WebUser.IsWap = false;
            return;
        }
        /// <summary>
        /// 注销
        /// </summary>
        public static void Port_SigOut()
        {
            WebUser.Exit(); 
        }
        /// <summary>
        /// 发送邮件与消息
        /// </summary>
        /// <param name="userNo">信息接收人</param>
        /// <param name="msgTitle">标题</param>
        /// <param name="msgDoc">内容</param>
        public static void Port_SendMail(string userNo, string msgTitle, string msgDoc)
        {
            WF.Port.WFEmp emp = new BP.WF.Port.WFEmp(userNo);
            BP.TA.SMS.AddMsg(DateTime.Now.ToString(), userNo, BP.WF.Port.AlertWay.Email, emp.Tel, msgTitle,
                emp.Email,msgTitle,msgDoc);
        }
        /// <summary>
        /// 发送SMS
        /// </summary>
        /// <param name="userNo">信息接收人</param>
        /// <param name="msgTitle">标题</param>
        /// <param name="msgDoc">内容</param>
        public static void Port_SendSMS(string userNo, string msgTitle, string msgDoc)
        {
            WF.Port.WFEmp emp = new BP.WF.Port.WFEmp(userNo);
            BP.TA.SMS.AddMsg(DateTime.Now.ToString(), userNo, BP.WF.Port.AlertWay.Email, emp.Tel, msgTitle,
                emp.Email, msgTitle, msgDoc);
        }
        #endregion 登录接口

        #region 与流程有关的接口
        /// <summary>
        /// 执行流程自检
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <returns>执行信息</returns>
        public static string Flow_DoSelfTest(string flowNo, Int64 workID)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            return wf.DoSelfTest();
        }
        /// <summary>
        /// 暂停流程
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">原因</param>
        /// <returns>执行信息</returns>
        public static void Flow_DoStopWorkFlow(string flowNo, Int64 workID, string msg)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            wf.DoStopWorkFlow(msg);
        }
        /// <summary>
        /// 恢复流程
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">原因</param>
        /// <returns>执行信息</returns>
        public static void Flow_DoComeBackWrokFlow(string flowNo, Int64 workID,string msg)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            wf.DoComeBackWrokFlow(msg);
        }
        /// <summary>
        /// 执行删除流程
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <returns>执行信息</returns>
        public static string Flow_DoDeleteFlow(string flowNo, Int64 workID)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            wf.DoDeleteWorkFlowByReal();
            return "删除成功";
        }
        /// <summary>
        /// 执行撤销发送
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <returns>返回撤销信息,它会抛出异常.有可能流程转入了下一个节点</returns>
        public static string Flow_DoUnSend(string flowNo, Int64 workID)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            return wf.DoUnSend();
        }
        /// <summary>
        /// 按照标记删除流程
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">原因</param>
        /// <returns></returns>
        public static void Flow_DoDeleteWorkFlowByFlag(string flowNo, Int64 workID,string msg)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
             wf.DoDeleteWorkFlowByFlag(msg);
        }
        /// <summary>
        /// 执行流程结束
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">流程结束原因</param>
        /// <returns>执行信息</returns>
        public static string Flow_DoFlowOver(string flowNo, Int64 workID,string msg)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            return wf.DoFlowOver(msg);
        }
        #endregion 与流程有关的接口

        #region 工作有关接口
        /// <summary>
        /// 发起一个工作
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="ht">数据集合</param>
        /// <returns>返回执行信息</returns>
        public static string Node_StartWork(string flowNo, Hashtable ht)
        {
            Node nd = new Node(int.Parse(flowNo + "01"));
            StartWork sw = nd.HisWork as StartWork;
            if (ht != null)
            {
                foreach (string str in ht.Keys)
                    sw.SetValByKey(str, ht[str]);
            }

            sw.Title = sw.Title + "(自动发起)";


            sw.SetValByKey("RDT", DataType.CurrentDataTime);
            sw.SetValByKey("CDT", DataType.CurrentDataTime);
            sw.SetValByKey("FK_NY", DataType.CurrentYearMonth);
            sw.SetValByKey("Rec", WebUser.No);
            sw.SetValByKey("Emps", WebUser.No);
            sw.SetValByKey("FK_Dept", WebUser.FK_Dept);
            sw.InsertAsOID(BP.DA.DBAccess.GenerOID());

            WorkNode wn = new WorkNode(sw, nd);
            return wn.AfterNodeSave();
        }
        /// <summary>
        /// 发起一个工作
        /// </summary>
        /// <param name="flowNo">流程编号</param>
        /// <param name="ht">开始节点数据</param>
        /// <param name="fk_nodeOfJumpTo">将要跳转的节点</param>
        /// <returns>执行信息</returns>
        public static string Node_StartWork(string flowNo, Hashtable ht,int fk_nodeOfJumpTo)
        {
            Node nd = new Node(int.Parse(flowNo + "01"));
            StartWork sw = nd.HisWork as StartWork;
            if (ht != null)
            {
                foreach (string str in ht.Keys)
                    sw.SetValByKey(str, ht[str]);
            }
            sw.Title = sw.Title + "(自动发起)";
            sw.SetValByKey("RDT", DataType.CurrentDataTime);
            sw.SetValByKey("CDT", DataType.CurrentDataTime);
            sw.SetValByKey("FK_NY", DataType.CurrentYearMonth);
            sw.SetValByKey("Rec", WebUser.No);
            sw.SetValByKey("Emps", WebUser.No);
            sw.SetValByKey("FK_Dept", WebUser.FK_Dept);
            sw.InsertAsOID(BP.DA.DBAccess.GenerOID());

            WorkNode wn = new WorkNode(sw, nd);
            wn.JumpToNode = new Node(fk_nodeOfJumpTo);
            return wn.AfterNodeSave();
        }
        /// <summary>
        /// 发送工作
        /// </summary>
        /// <param name="nodeID">节点编号</param>
        /// <param name="workID">工作ID</param>
        /// <returns>返回执行信息</returns>
        public static string Node_SendWork(string fk_flow, Int64 workID)
        {
            return Node_SendWork(fk_flow,workID, new Hashtable());
        }
        /// <summary>
        /// 发送工作
        /// </summary>
        /// <param name="workID">工作ID</param>
        /// <param name="htWork">工作数据</param>
        /// <returns>返回执行信息</returns>
        public static string Node_SendWork(string fk_flow,Int64 workID, Hashtable htWork)
        {
            Node nd = new Node(Dev2Interface.Node_GetCurrentNodeID(fk_flow,workID));
            Work sw = nd.HisWork;
            if (workID != 0)
            {
                sw.OID = workID;
                sw.RetrieveFromDBSources();
            }

            if (htWork != null)
            {
                foreach (string str in htWork.Keys)
                    sw.SetValByKey(str, htWork[str]);
            }
            sw.BeforeSave();
            sw.Save();
            WorkNode wn = new WorkNode(sw, nd);
            return wn.AfterNodeSave();
        }
        /// <summary>
        /// 执行抄送
        /// </summary>
        /// <param name="empNo">抄送人员编号</param>
        /// <param name="empName">名称</param>
        /// <param name="msgTitle">表态</param>
        /// <param name="msgDoc">消息</param>
        /// <param name="fk_node">节点</param>
        /// <param name="fk_flow">流程编号</param>
        /// <param name="workid">工作ID</param>
        /// <param name="fid">流程ID</param>
        public static void Node_CC(string empNo, string empName, string msgTitle, string msgDoc, int fk_node, string fk_flow, Int64 workid, Int64 fid)
        {
            CCList list = new CCList();
            list.MyPK = workid + "_" + fk_node + "_" + empNo;
            list.FK_Flow = fk_flow;
            Flow fl = new Flow(fk_flow);
            list.FlowName = fl.Name;
            list.FK_Node = fk_node;

            Node nd = new Node(fk_node);
            list.NodeName = nd.Name;
            list.Title = msgTitle;
            list.Doc = msgDoc;
            list.CCTo = empNo;
            list.RDT = DataType.CurrentDataTime;
            list.Rec = WebUser.No;
            list.RefWorkID = workid;
            list.FID = fid;
            try
            {
                list.Insert();
            }
            catch
            {
                list.CheckPhysicsTable();
                list.Update();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="workID">工作ID</param>
        /// <returns>返回保存的信息</returns>
        public static string Node_SaveWork(string fk_flow,Int64 workID)
        {
            return Node_SaveWork(fk_flow, workID, new Hashtable());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="workID">工作ID</param>
        /// <param name="htWork">工作数据</param>
        /// <returns>返回执行信息</returns>
        public static string Node_SaveWork(string fk_flow,Int64 workID, Hashtable htWork)
        {
            try
            {
                Node nd = new Node(Dev2Interface.Node_GetCurrentNodeID(fk_flow, workID));
                Work sw = nd.HisWork;
                sw.OID = workID;
                sw.Retrieve();
                if (htWork != null)
                {
                    foreach (string str in htWork.Keys)
                        sw.SetValByKey(str, htWork[str]);
                }
                sw.BeforeSave();
                //if (sw.GetValStringByKey("Title") == "")
                //{
                //}
                sw.Save();
                return "保存成功.";
            }
            catch (Exception ex)
            {
                return "保存失败:" + ex.Message;
            }
        }
        /// <summary>
        /// 执行工作退回
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">信息</param>
        /// <param name="returnToNode">要退回的节点</param>
        /// <returns>返回执行信息</returns>
        public static void Node_ReturnWork(string fk_flow, Int64 workID, string msg, int returnToNode)
        {
            WorkNode wn = new WorkNode(workID, returnToNode);
            wn.DoReturnWork(msg);
        }
        /// <summary>
        /// 执行工作退回
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="workID">工作ID</param>
        /// <param name="msg">信息</param>
        /// <returns>返回执行信息</returns>
        public static void Node_ReturnWork(string fk_flow,Int64 workID, string msg)
        {
            WorkNode wn = new WorkNode(workID, Dev2Interface.Node_GetCurrentNodeID(fk_flow, workID));
            wn.DoReturnWork(msg);
        }
        public static int Node_GetCurrentNodeID(string fk_flow,Int64 workid)
        {
            int nodeID = BP.DA.DBAccess.RunSQLReturnValInt("SELECT FK_Node FROM WF_GenerWorkFlow WHERE WorkID=" + workid+" AND FK_Flow='"+fk_flow+"'", 0);
            if (nodeID == 0)
                return int.Parse(fk_flow + "01"); 
            return nodeID;
        }
        /// <summary>
        /// 工作结束
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="workID">工作ID</param>
        /// <returns>返回执行信息</returns>
        public static string Node_SetThisWorkOver(int nodeID, Int64 workID)
        {
            WorkNode wn = new WorkNode(workID, nodeID);
            return wn.DoSetThisWorkOver();
        }
        /// <summary>
        /// 创建一个工作
        /// </summary>
        /// <param name="nodeId">节点ID</param>
        /// <returns>返回一个工作实例</returns>
        public static Work Node_CreateWork(int nodeId)
        {
            Node nd = new Node(nodeId);
            return nd.HisWork;
        }
        #endregion 工作有关接口
    }
}
