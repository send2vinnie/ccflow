using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Text;
using BP.WF;
using BP.DA;
using BP.Web;

namespace BP.WF
{
    /// <summary>
    /// 此接口为程序员二次开发使用
    /// </summary>
    public class Dev2Interface
    {
        #region 数据接口

        #region 获取当前操作员可以发起的流程集合
        /// <summary>
        /// 获取当前操作员可以发起的流程集合
        /// </summary>
        /// <returns>bp.wf.flows</returns>
        public static Flows DB_GenerCanStartFlowsOfEntities()
        {
            string sql = "SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND NodeID IN ( SELECT FK_Node FROM WF_NodeStation WHERE FK_Station IN (SELECT FK_Station FROM Port_EmpStation WHERE FK_EMP='" + WebUser.No + "')) ";
            string sql2 = " UNION  SELECT FK_Flow FROM WF_Node WHERE NodePosType=0 AND NodeID IN ( SELECT FK_Node FROM WF_NodeEmp WHERE FK_Emp='" + WebUser.No + "' ) ";
            Flows fls = new Flows();
            BP.En.QueryObject qo = new BP.En.QueryObject(fls);
            qo.AddWhereInSQL("No", sql + sql2);
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
        /// <returns>datatable</returns>
        public static DataTable DB_GenerCanStartFlowsOfDataTable()
        {
            return DB_GenerCanStartFlowsOfEntities().ToDataTableField();
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
            string sql = null;
            sql = "SELECT * FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'  ORDER BY WorkID ";
            return BP.DA.DBAccess.RunSQLReturnTable(sql);
        }
        #endregion 获取当前操作员的待办工作


        #region 获取当前操作员的在途工作
        /// <summary>
        /// 获取当前操作员的在途工作
        /// </summary>
        /// <returns>在途工作</returns>
        public static GenerWorkFlowExts DB_GenerRuningOfEntities()
        {
            string sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE A.WorkID=B.WorkID   AND B.FK_EMP='" + BP.Web.WebUser.No + "' AND B.IsEnable=1 AND B.IsPass=1 ";
            GenerWorkFlowExts gwfs = new GenerWorkFlowExts();
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
        /// <returns>执行信息</returns>
        public static string Flow_DoFlowOver(string flowNo, Int64 workID)
        {
            WorkFlow wf = new WorkFlow(flowNo, workID);
            return wf.DoFlowOver();
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
            sw.BeforeSave();
            sw.Title = sw.Title + "(自动发起)";
            sw.BeforeSend();
            sw.Save();
            WorkNode wn = new WorkNode(sw, nd);
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
            //sw.RDT = DataType.CurrentDataTime;
            //sw.CDT = DataType.CurrentDataTime;
            //sw.Title = sw.Title + "(自动发起)";
            sw.BeforeSend();
            sw.Save();
            WorkNode wn = new WorkNode(sw, nd);
            return wn.AfterNodeSave();
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
