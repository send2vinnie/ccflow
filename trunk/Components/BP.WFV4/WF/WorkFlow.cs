using System;
using BP.En;
using BP.En;
using BP.DA;
using System.Collections;
using System.Data;
using BP.Port;
using BP.Sys;
using BP.Port;

namespace BP.WF
{
    /// <summary>
    /// WF 的摘要说明。
    /// 工作流
    /// 这里包含了两个方面
    /// 工作的信息．
    /// 流程的信息．
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

        #region 当前工作统计信息
        /// <summary>
        /// 正常范围的运行的个数。
        /// </summary>
        public static int NumOfRuning(string fk_emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_EMP='" + fk_emp + "' AND WorkTimeState=0";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        /// <summary>
        /// 进入警告期限的个数
        /// </summary>
        public static int NumOfAlert(string fk_emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_EMP='" + fk_emp + "' AND WorkTimeState=1";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        /// <summary>
        /// 逾期
        /// </summary>
        public static int NumOfTimeout(string fk_emp)
        {
            string sql = "SELECT COUNT(*) FROM V_WF_CURRWROKS WHERE FK_EMP='" + fk_emp + "' AND WorkTimeState=2";
            return DBAccess.RunSQLReturnValInt(sql);
        }
        #endregion

        #region  权限管理
        /// <summary>
        /// 是不是能够作当前的工作。
        /// </summary>
        /// <param name="empId">工作人员ID</param>
        /// <returns>是不是能够作当前的工作</returns>
        public bool IsCanDoCurrentWork(string empId)
        {
            //return true;
            // 找到当前的工作节点
            WorkNode wn = this.GetCurrentWorkNode();

            // 判断是不是开始工作节点..
            if (wn.HisNode.IsStartNode)
            {
                // 从物理上判断是不是有这个权限。
                return WorkFlow.IsCanDoWorkCheckByEmpStation(wn.HisNode.NodeID, empId);
            }

            // 判断他的工作生成的工作者.
            WorkerLists gwls = new WorkerLists(this.WorkID, wn.HisNode.NodeID);
            if (gwls.Count == 0)
            {
                //return true;
                //throw new Exception("@工作流程定义错误,没有找到能够执行此项工作的人员.相关信息:工作ID="+this.WorkID+",节点ID="+wn.HisNode.NodeID );
                throw new Exception("@" + this.ToE("WF0", "工作流程定义错误,没有找到能够执行此项工作的人员.相关信息:") + " WorkID=" + this.WorkID + ",NodeID=" + wn.HisNode.NodeID);
            }

            foreach (WorkerList en in gwls)
            {
                if (en.FK_Emp == empId)
                    return true;
            }
            return false;
        }
        #endregion

        #region 流程公共方法
        /// <summary>
        /// 真正的删除工作流程
        /// </summary>
        public void DoDeleteWorkFlowByReal()
        {
            BP.DA.Log.DefaultLogWriteLineInfo("@[" + this.HisFlow.Name + "]流程被[" + BP.Web.WebUser.No + BP.Web.WebUser.Name + "]删除，WorkID[" + this.WorkID + "]。");
            string msg = "";
            try
            {
                Int64 workId = this.WorkID;
                string flowNo = this.HisFlow.No;
            }
            catch (Exception ex)
            {
                throw new Exception("获取流程的 ID 与流程编号 出现错误。" + ex.Message);
            }

            try
            {
                // @,删除对他的流程考核信息.
                DBAccess.RunSQL("DELETE WF_CHOfNode WHERE WorkID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");
                DBAccess.RunSQL("DELETE WF_CHOfFlow WHERE WorkID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");

                //  WF_CHOfStation
                //DBAccess.RunSQL("DELETE WF_CHOfStation WHERE WorkID="+this.WorkID+" AND FK_Flow='"+this.HisFlow.No+"'");
                // @,删除对他的统计分析信息.
                // DBAccess.RunSQL("DELETE WF_CHQuality WHERE WorkID=" + this.WorkID + " AND FK_Flow='" + this.HisFlow.No + "'");

                // 删除文书信息.
                DBAccess.RunSQL("DELETE WF_Book WHERE WorkID=" + this.WorkID);

                // 2, 删除产生的工作.
                try
                {
                    GenerWorkFlow gwf = new GenerWorkFlow();
                    gwf.WorkID = this.WorkID;
                    gwf.DeleteHisRefEns();
                    gwf.Delete();
                }
                catch
                {
                }

                Nodes nds = this.HisFlow.HisNodes;
                foreach (Node nd in nds)
                {
                    Work wk = nd.HisWork;
                    if (nd.IsCheckNode)
                    {
                        wk.SetValByKey(GECheckStandAttr.NodeID, nd.NodeID);
                        wk.SetValByKey("MyPK", nd.NodeID + "_" + this.WorkID);
                    }
                    wk.OID = this.WorkID;
                    wk.Delete();
                }

                // 1, 删除与此流程相关的每一个工作节点信息。
                WorkNodes wns = this.HisWorkNodesOfWorkID;
                foreach (WorkNode nd in wns)
                {
                    try
                    {
                        //DBAccess.RunSQL("DELETE "+nd.HisWork.EnMap.PhysicsTable+" WHERE OID="+this.WorkID );
                        nd.HisWork.Delete();
                    }
                    catch
                    {
                        //msg+=ex.Message;
                    }
                }
                if (msg != "")
                {
                    Log.DebugWriteInfo(msg);
                    //throw new Exception("@已经从工作者列表里面清除了.删除节点信息其间出现错误:" + msg);
                }
            }
            catch (Exception ex)
            {
                string err = "@" + this.ToE("WF1", "删除工作流程") + "[" + this.HisStartWork.OID + "," + this.HisStartWork.Title + "] Err " + ex.Message;
                Log.DefaultLogWriteLine(LogType.Error, err);
                throw new Exception(err);
            }
        }
        /// <summary>
        /// 删除当前的工作流程用标记.
        /// </summary>
        public void DoDeleteWorkFlowByFlag(string msg)
        {
            try
            {
                //设置流程的状态为强制终止状态
                WorkNode nd = GetCurrentWorkNode();
                nd.HisWork.NodeState = NodeState.Stop;
                nd.HisWork.DirectUpdate();
                //设置当前的工作节点是强制终止状态
                StartWork sw = this.HisStartWork;
                sw.WFState = BP.WF.WFState.Delete;
                sw.WFLog += "\n@" + Web.WebUser.No + " " + Web.WebUser.Name + "在" + DataType.CurrentDataTime + " 逻辑删除,原因如下:" + msg;
                sw.DirectUpdate();
                //设置产生的工作流程为.
                GenerWorkFlow gwf = new GenerWorkFlow(sw.OID, this.HisFlow.No);
                gwf.WFState = 3;
                gwf.Update();
                // 删除消息.
                BP.WF.MsgsManager.DeleteByWorkID(sw.OID);
                //WorkerLists wls = new WorkerLists(this
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@逻辑删除出现错误:" + ex.Message);
                throw new Exception("@逻辑删除出现错误:" + ex.Message);
            }
        }

        #region 日志操作
        /// <summary>
        /// 写入流程日志[已经包含了记录时间]
        /// </summary>
        /// <param name="doc"></param>
        public void WritLog(string doc)
        {
            StartWork sw = this.HisStartWork;
            sw.WFLog += "\n@" + BP.DA.DataType.CurrentData + "  " + doc;
            sw.DirectUpdate();
        }
        #endregion 日志操作

        #region 流程的强制终止\删除 或者恢复使用流程,
        /// <summary>
        /// 强制终止流程. 
        ///  1, 设置流程的状态为强制终止状态.
        ///  2, 设置当前的工作节点是强制终止状态. 
        ///  3, 设置产生的工作流程为 强制终止状态 .
        ///  4, 除去当前工作人员的消息.
        /// </summary>
        /// <param name="msg"></param>
        public void DoStopWorkFlow(string msg)
        {
            try
            {
                //设置流程的状态为强制终止状态
                //			//	WorkNode nd = GetCurrentWorkNode();
                //				nd.HisWork.NodeState = 4;
                //				nd.HisWork.Update();

                //设置当前的工作节点是强制终止状态
                StartWork sw = this.HisStartWork;
                sw.WFState = BP.WF.WFState.Stop;
                //sw.NodeState=4;
                sw.WFLog += "\n@" + Web.WebUser.No + " " + Web.WebUser.Name + "在" + DateTime.Now.ToString(DataType.SysDataTimeFormat) + " 强制终止工作,原因如下:" + msg;
                sw.DirectUpdate();

                //设置产生的工作流程为
                GenerWorkFlow gwf = new GenerWorkFlow(sw.OID, this.HisFlow.No);
                gwf.WFState = 2;
                gwf.DirectUpdate();
                // 删除消息.
                BP.WF.MsgsManager.DeleteByWorkID(sw.OID);
                //WorkerLists wls = new WorkerLists(this
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@强制终止流程错误." + ex.Message);
                throw new Exception("@强制终止流程错误." + ex.Message);
            }
        }
        public void DoDeleteFlow()
        {
            Work wk = this.HisStartWork;
        }
        public string DoSelfTest()
        {
            string msg = "";
            if (this.IsComplete)
                return "流程已经结束您不能在体检。";

            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID, this.HisFlow.No);
            if (gwf.WFState == (int)WFState.Complete)
                return "流程已经结束您不能在体检。";


            // 判断是否有多个节点状态在等待办理的状态的错误。
            WorkNodes ens = this.HisWorkNodesOfWorkID;
            int i = 0;

            #region 首先判断当前的工作节点的 节点数据是否存在。
            int num = 0;
            foreach (WorkNode wn in ens)
            {
                Work wk = wn.HisWork;
                Node nd = wn.HisNode;
                if (wk.NodeState != NodeState.Complete)
                {
                    /*如果没有完成 */
                    if (nd.NodeID != gwf.FK_Node)
                    {
                        num++; ;
                    }
                }
            }

            if (num == 0)
            {
                return "没有找到当前的工作，流程错误。";
            }

            foreach (WorkNode wn in ens)
            {
                Work wk = wn.HisWork;
                Node nd = wn.HisNode;

                if (wk.NodeState != NodeState.Complete)
                {
                    /*如果没有完成 */
                    if (nd.NodeID != gwf.FK_Node)
                    {
                        /*如果不是当前的工作节点。*/
                        wk.NodeState = NodeState.Complete;
                        wk.Update();
                        msg += "流程调整完成：问题是一个工作具有多个当前工作，可能是因为手工的调整数据造成的。";
                    }
                }
            }
            #endregion


            if (msg == "")
                return "@流程没有问题，用户可以由两种方式删除流程1，退回到第一个环节。2，本区县的系统管理员进入删除。";
            else
                return msg;
        }
        /// <summary>
        /// 恢复流程.
        /// </summary>
        /// <param name="msg">回复流程的原因</param>
        public void DoComeBackWrokFlow(string msg)
        {
            try
            {
                // 设置流程的状态为强制终止状态
                //				WorkNode nd = GetCurrentWorkNode();
                //				nd.HisWork.NodeState =0;
                //				nd.HisWork.Update();

                // 设置当前的工作节点是强制终止状态
                StartWork sw = this.HisStartWork;
                sw.WFState = 0;
                sw.WFLog += "\n@" + Web.WebUser.No + " " + Web.WebUser.Name + "在" + DateTime.Now.ToString(DataType.SysDataTimeFormat) + " 回复使用流程,原因如下:" + msg;
                sw.DirectUpdate();

                //设置产生的工作流程为
                GenerWorkFlow gwf = new GenerWorkFlow(sw.OID, this.HisFlow.No);
                gwf.WFState = 0;
                gwf.DirectUpdate();

                // 增加消息 
                WorkNode wn = this.GetCurrentWorkNode();
                WorkerLists wls = new WorkerLists(wn.HisWork.OID, wn.HisNode.NodeID);
                if (wls.Count == 0)
                    throw new Exception("@恢复流程出现错误,产生的工作者列表");
                BP.WF.MsgsManager.AddMsgs(wls, "恢复的流程", wn.HisNode.Name, "回复的流程");
            }
            catch (Exception ex)
            {
                Log.DefaultLogWriteLine(LogType.Error, "@恢复流程出现错误." + ex.Message);
                throw new Exception("@恢复流程出现错误." + ex.Message);
            }
        }
        #endregion


        /// <summary>
        /// 得到当前的进行中的工作。
        /// </summary>
        /// <returns></returns>		 
        public WorkNode GetCurrentWorkNode()
        {
            //if (this.IsComplete)
            //    throw new Exception("@工作流程[" + this.HisStartWork.Title + "],已经完成。");

            int currNodeID = 0;
            string sql = "SELECT FK_Node FROM WF_GenerWorkFlow WHERE WorkID=" + this.WorkID;
            currNodeID = DBAccess.RunSQLReturnValInt(sql);
            if (currNodeID == 0)
            {
                this.DoFlowOver();
                throw new Exception("@" + this.ToEP1("WF2", "工作流程{0}已经完成。", this.HisStartWork.Title));
            }

            Node nd = new Node(currNodeID);
            Work work = nd.HisWork;
            work.OID = this.WorkID;
            work.NodeID = nd.NodeID;
            if (nd.IsCheckNode)
            {
                work.SetValByKey("NodeID", nd.NodeID);
            }

            work.SetValByKey("FK_Dept", Web.WebUser.FK_Dept);

            if (work.RetrieveFromDBSources() == 0)
            {
                Log.DefaultLogWriteLineError("@" + this.ToE("WF3", "没有找到当前的工作节点的数据，流程出现未知的异常。")); // 没有找到当前的工作节点的数据，流程出现未知的异常。
                work.Rec = Web.WebUser.No;

                try
                {
                    work.Insert();
                }
                catch
                {
                    Log.DefaultLogWriteLineError("@" + this.ToE("WF3", "没有找到当前的工作节点的数据，流程出现未知的异常。") + "。"); // 没有找到当前的工作节点的数据
                }
            }

            return new WorkNode(work, nd);
        }
        /// <summary>
        /// 处理合流流程结束
        /// </summary>
        /// <param name="sw"></param>
        public string DoDoFlowOverHeLiu_del()
        {
            GenerFH gh = new GenerFH();
            gh.FID = this.WorkID;
            if (gh.RetrieveFromDBSources() == 0)
                throw new Exception("系统异常");
            else
                gh.Delete();

            GenerWorkFlows ens = new GenerWorkFlows();
            ens.Retrieve(GenerWorkFlowAttr.FID, this.WorkID);

            string msg = "";
            foreach (GenerWorkFlow en in ens)
            {
                if (en.WorkID == en.FID)
                    continue;

                /*结束每一个子流程*/
                WorkFlow fl = new WorkFlow(en.FK_Flow, en.WorkID, en.FID);
                // msg += fl.DoFlowOverOrdinary();
            }
            return msg;
        }
        /// <summary>
        /// 结束分流的节点
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public string DoDoFlowOverFeiLiu(GenerWorkFlow gwf)
        {
            // 查询出来有少没有完成的流程。
            int i = BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM WF_GenerWorkflow WHERE FID=" + gwf.FID + " AND WFState!=1");
            switch (i)
            {
                case 0:
                    throw new Exception("@不应该的错误。");
                case 1:
                    BP.DA.DBAccess.RunSQL("DELETE WF_GenerWorkflow  WHERE FID=" + gwf.FID + " OR WorkID=" + gwf.FID);
                    BP.DA.DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + gwf.FID + " OR WorkID=" + gwf.FID);
                    BP.DA.DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID=" + gwf.FID);

                    StartWork wk = this.HisFlow.HisStartNode.HisWork as StartWork;
                    wk.OID = gwf.FID;
                    wk.WFState = WFState.Complete;
                    wk.NodeState = NodeState.Complete;
                    wk.Update();

                    return "@当前的工作已经完成，该流程上所有的工作都已经完成。";
                default:
                    BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkflow SET WFState=1 WHERE WorkID=" + this.WorkID);
                    BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET IsPass=1 WHERE WorkID=" + this.WorkID);
                    return "@当前的工作已经完成。";
            }
        }
        /// <summary>
        /// 结束流程
        /// </summary>
        /// <returns></returns>
        public string DoFlowOver()
        {
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID);
            Node nd = new Node(gwf.FK_Node);

            string msg = this.BeforeFlowOver();

            switch (nd.HisFNType)
            {
                case FNType.Plane:
                    msg += this.DoFlowOverPlane(nd);
                    break;
                case FNType.River:
                    msg += this.DoFlowOverRiver(nd);
                    break;
                case FNType.Branch:
                    msg += this.DoFlowOverBranch(nd);
                    break;
                default:
                    throw new Exception("@没有判断的情况。");
                    break;
            }

            return msg;

            //switch (nd.HisNodeWorkType)
            //{
            //    case NodeWorkType.WorkHL: // 合流
            //        return this.DoDoFlowOverHeLiu();
            //    case NodeWorkType.WorkFL: // 分流
            //        return this.DoDoFlowOverFeiLiu(gwf);
            //    default:
            //}
            //switch (nd.HisNodeWorkType)
            //{
            //    case NodeWorkType.WorkHL:
            //    default:
            //        return DoFlowOverOrdinary();
            //}
        }
        /// <summary>
        /// 在分流上结束流程。
        /// </summary>
        /// <returns></returns>
        public string DoFlowOverBranch(Node nd)
        {
            string sql = "";
            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=1 WHERE WorkID=" + this.WorkID);

            if (this.HisFlow.HisStartNode.HisFNType == FNType.River)
            {
                /* 开始节点是干流 */
            }
            else
            {
                BP.DA.DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE OID=" + this.WorkID); // 更新开始节点的状态。
            }

            string msg = "";
            // 判断流程中是否还没有没有完成的支流。
            sql = "SELECT COUNT(WORKID) FROM WF_GenerWorkFlow WHERE WFState!=1 AND FID=" + this.FID;

            DataTable dt = DBAccess.RunSQLReturnTable("SELECT Rec FROM ND" + nd.NodeID + " WHERE FID=" + this.FID);
            if (DBAccess.RunSQLReturnValInt(sql) == 0)
            {
                /* 如果全部完成 */
                if (this.HisFlow.HisStartNode.HisFNType == FNType.River)
                {
                    BP.DA.DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE FID=" + this.FID);
                }

                /*整个流程都结束了*/
                DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.FID);
                DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.FID);

                /* 输出整个流程完成的信息，给当前的用户。*/
                msg += "@整体流程完全结束，有{" + dt.Rows.Count + "}个人员参与了分支流程，您是最后一个完成此工作的人员。@分支流程参与者名单如下：";
                foreach (DataRow dr in dt.Rows)
                {
                    msg += dr[0].ToString() + "、";
                }
                return msg;
                //   return "@整个流程完全结束。" + this.GenerFHStartWorkInfo();
            }
            else
            {
                /* 还有其它人员没有完成此工作。*/

                msg += "@您的工作已经完。@整体流程目前还没有完全结束，有{" + dt.Rows.Count + "}个人员参与了分支流程，名单如下：";
                foreach (DataRow dr in dt.Rows)
                {
                    msg += dr[0].ToString() + "、";
                }
                return msg;
            }
        }
        /// <summary>
        /// 在分流上结束流程。
        /// </summary>
        /// <returns></returns>
        public string DoFlowOverBranch_Bak(Node nd)
        {
            string sql = "";
            if (this.HisFlow.HisStartNode.HisFNType == FNType.River)
            {
                /* 如果开始节点是干流，结束节点是支流。*/
                BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=1 WHERE WorkID=" + this.WorkID);

                // 判断是否还有没有结束的支流。
                sql = "SELECT COUNT(WORKID) FROM WF_GenerWorkFlow WHERE WFState!=1 AND FID=" + this.FID;
                if (DBAccess.RunSQLReturnValInt(sql) == 0)
                {
                    StartWork sw = this.HisStartWorkNode.HisWork as StartWork;
                    sw.FID = this.FID;
                    sw.OID = this.FID;
                    int i = sw.RetrieveFromDBSources();
                    if (i == 0)
                    {
                        throw new Exception("@开始节点信息丢失。");
                    }
                    else
                    {
                        sw.Update("WFState", (int)WFState.Complete);
                    }

                    /*整个流程都结束了*/
                    DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.FID);
                    DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.FID);
                }
                return "@流程在干流上结束。";
            }

            /*开始节点是支流，结束节点是支流。*/
            StartWork mysw = this.HisStartWorkNode.HisWork as StartWork;
            mysw.OID = this.WorkID;
            mysw.Update("WFState", (int)WFState.Complete);

            //i = sw.RetrieveFromDBSources();
            //if (i == 0)
            //{
            //    throw new Exception("@开始节点信息丢失。");
            //}
            //else
            //{
            //    sw.Update("WFState", (int)WFState.Complete);
            //}

            // 更新流程状态。
            BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkFlow SET WFState=1 WHERE WorkID=" + this.WorkID);
            //   BP.DA.DBAccess.RunSQL("UPDATE WF_GenerWorkerList SET WFState=0 WHERE WorkID=" + this.WorkID);

            // 判断是否还有没有结束的支流。
            sql = "SELECT COUNT(WORKID) FROM WF_GenerWorkFlow WHERE WFState!=1 AND FID=" + this.FID;
            if (DBAccess.RunSQLReturnValInt(sql) == 0)
            {
                /*整个流程都结束了*/
                DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.FID);
                DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.FID);
            }

            return "@流程在分流上结束。";

        }
        /// <summary>
        /// 在干流上结束流程
        /// </summary>
        /// <param name="nd">结束的节点</param>
        /// <returns>返回的信息</returns>
        public string DoFlowOverRiver(Node nd)
        {
            try
            {
                string msg = "";



                /* 更新开始节点的状态。*/
                DBAccess.RunSQL("UPDATE ND" + this.StartNodeID + " SET WFState=1 WHERE FID=" + this.FID);

                /*整个流程都结束了*/
                DBAccess.RunSQL("DELETE WF_GenerFH WHERE FID=" + this.FID);
                DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE FID=" + this.FID);
                DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE FID=" + this.FID);

                return msg;
            }
            catch (Exception ex)
            {
                throw new Exception("@结束流程时间出现异常：" + ex.Message);
            }
        }
        public string GenerFHStartWorkInfo()
        {
            string msg = "";
            DataTable dt = DBAccess.RunSQLReturnTable("SELECT Title,RDT,Rec,OID FROM ND" + this.StartNodeID + " WHERE FID=" + this.FID);
            switch (dt.Rows.Count)
            {
                case 0:
                    Node nd = new Node(this.StartNodeID);
                    throw new Exception("@没有找到他们开始节点的数据，流程异常。FID=" + this.FID + "，节点：" + nd.Name + "节点ID：" + nd.NodeID);
                case 1:
                    msg = string.Format("@发起人： {0}  日期：{1} 发起的流程 标题：{2} ，已经成功完成。",
                        dt.Rows[0]["Rec"].ToString(), dt.Rows[0]["RDT"].ToString(), dt.Rows[0]["Title"].ToString());
                    break;
                default:
                    msg = "@下列(" + dt.Rows.Count + ")位人员发起的流程已经完成。";
                    foreach (DataRow dr in dt.Rows)
                    {
                        msg += "<br>发起人：" + dr["Rec"] + " 发起日期：" + dr["RDT"] + " 标题：" + dr["Title"] + "<a href='./../../WF/WFRpt.aspx?WorkID=" + dr["OID"] + "&FK_Flow=" + this.HisFlow.No + "' target=_blank>详细...</a>";
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
        /// 正常的流程结束
        /// </summary>		 
        public string DoFlowOverPlane(Node nd)
        {
            StartWork sw = this.HisStartWorkNode.HisWork as StartWork;
            sw.OID = this.WorkID;
            sw.Update("WFState", (int)sw.WFState);

            this._IsComplete = 1;

            // 清除流程。
            DBAccess.RunSQL("DELETE WF_GenerWorkFlow WHERE WorkID=" + this.HisStartWork.OID + " AND FK_Flow='" + this.HisFlow.No + "'");

            // 清除其他的工作者。
            DBAccess.RunSQL("DELETE WF_GenerWorkerList WHERE WorkID=" + this.HisStartWork.OID + " AND FK_Node IN (SELECT NodeId FROM WF_Node WHERE FK_Flow='" + this.HisFlow.No + "') ");
            return "";

            //// 修改流程汇总中的流程状态。
            //CHOfFlow chf = new CHOfFlow();
            //chf.WorkID = this.WorkID;
            //chf.Update("WFState", (int)sw.WFState);
            // +"@" + this.ToEP2("WF5", "工作流程{0},{1}任务完成。", this.HisFlow.Name, this.HisStartWork.Title);  // 工作流程[" + HisFlow.Name + "] [" + HisStartWork.Title + "]任务完成。;
        }
        /// <summary>
        /// 流程完成之后要做的工作。
        /// </summary>
        private string BeforeFlowOver()
        {
            string ccmsg = "";
            if (this.HisFlow.IsCCAll == true)
            {
                ccmsg += "@抄送流程参与人员：";
                /*如果在流程结束后自动的发送给全体参与人员*/
                ccmsg += this.CCTo(BP.DA.DBAccess.RunSQLReturnTable("SELECT DISTINCT FK_Emp FROM WF_GenerWorkerList WHERE WorkID=" + this.WorkID + " AND IsEnable=1"));
            }

            #region 执行流程抄送
            if (this.HisFlow.CCStas.Length > 2)
            {
                ccmsg += "@按照岗位抄送如下人员：";
                /* 如果设置了抄送人员的岗位。*/
                string sql = "SELECT NO,NAME FROM PORT_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept + "%' AND NO IN (SELECT FK_EMP FROM PORT_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                DataTable dt = DBAccess.RunSQLReturnTable(sql);
                if (dt.Rows.Count == 0 || Web.WebUser.FK_Dept.Length > 3)
                {
                    sql = "SELECT NO,NAME FROM PORT_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept.Substring(0, Web.WebUser.FK_Dept.Length - 2) + "%' AND NO IN (SELECT FK_EMP FROM PORT_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                    dt = DBAccess.RunSQLReturnTable(sql);
                }

                if (dt.Rows.Count == 0 || Web.WebUser.FK_Dept.Length > 5)
                {
                    sql = "SELECT NO,NAME FROM PORT_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept.Substring(0, Web.WebUser.FK_Dept.Length - 4) + "%' AND NO IN (SELECT FK_EMP FROM PORT_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                    dt = DBAccess.RunSQLReturnTable(sql);
                }

                if (dt.Rows.Count == 0 || Web.WebUser.FK_Dept.Length > 7)
                {
                    sql = "SELECT NO,NAME FROM PORT_Emp WHERE FK_Dept Like '" + BP.Web.WebUser.FK_Dept.Substring(0, Web.WebUser.FK_Dept.Length - 6) + "%' AND NO IN (SELECT FK_EMP FROM PORT_EmpStation WHERE FK_STATION IN(SELECT FK_Station FROM WF_FlowStation WHERE FK_FLOW='" + this.HisFlow.No + "' ) )";
                    dt = DBAccess.RunSQLReturnTable(sql);
                }

                if (dt.Rows.Count == 0)
                {
                    ccmsg += "@系统没有获取到要抄送的人员。管理员设置的信息如下：" + this.HisFlow.CCStas + "，请确定该岗位下是否有此人员。";
                }
                ccmsg += this.CCTo(dt);
            }
            #endregion
            return ccmsg;
        }
        /// <summary>
        ///  抄送到
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
                    empsExt += no + "、";
                else
                    empsExt += no + "<" + dr[1] + ">、";
            }

            Paras pss = new Paras();
            pss.Add("Sender", Web.WebUser.No);
            pss.Add("Receivers", emps);
            pss.Add("Title", "工作流抄送：工作名称:" + this.HisFlow.Name + "，最后处理人：" + Web.WebUser.Name);
            pss.Add("Context", "工作报告 http://" + ip + "/Front/WF/WFRpt.aspx?WorkID=" + this.WorkID + "&FID=0");

            try
            {
                DBAccess.RunSP("CCstaff", pss);
                return "@" + empsExt;
            }
            catch (Exception ex)
            {
                return "@抄送出现错误，没有把该流程的信息抄送到(" + empsExt + ")请联系管理员检查系统异常" + ex.Message;
            }
        }
        #endregion

        #region 基本属性
        /// <summary>
        /// 他的节点
        /// </summary>
        private Nodes _HisNodes = null;
        /// <summary>
        /// 节点s
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
        /// 工作节点s(普通的工作节点)
        /// </summary>
        private WorkNodes _HisWorkNodesOfWorkID = null;
        /// <summary>
        /// 工作节点s
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
        /// 工作节点s
        /// </summary>
        private WorkNodes _HisWorkNodesOfFID = null;
        /// <summary>
        /// 工作节点s
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
        /// 工作流程
        /// </summary>
        private Flow _HisFlow = null;
        /// <summary>
        /// 工作流程
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
                return new GenerWorkFlow(this.WorkID, this.HisFlow.No);
            }
        }
        /// <summary>
        /// 工作ID
        /// </summary>
        private Int64 _WorkID = 0;
        /// <summary>
        /// 工作ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this._WorkID;
            }
        }
        /// <summary>
        /// 工作ID
        /// </summary>
        private Int64 _FID = 0;
        /// <summary>
        /// 工作ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this._FID;
            }
        }
        #endregion

        #region 构造方法
        public WorkFlow(string fk_flow, Int64 wkid)
        {
            GenerWorkFlow gwf = new GenerWorkFlow(wkid);
            this._FID = gwf.FID;

            if (wkid == 0)
                throw new Exception("@没有指定工作ID, 不能创建工作流程.");
            Flow flow = new Flow(fk_flow);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }

        public WorkFlow(Flow flow, Int64 wkid)
        {
            GenerWorkFlow gwf = new GenerWorkFlow(wkid);
            this._FID = gwf.FID;

            if (wkid == 0)
                throw new Exception("@没有指定工作ID, 不能创建工作流程.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        /// <summary>
        /// 建立一个工作流事例
        /// </summary>
        /// <param name="flow">流程No</param>
        /// <param name="wkid">工作ID</param>
        public WorkFlow(Flow flow, Int64 wkid, Int64 fid)
        {
            this._FID = fid;
            if (wkid == 0)
                throw new Exception("@没有指定工作ID, 不能创建工作流程.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        public WorkFlow(string FK_flow, Int64 wkid, Int64 fid)
        {
            this._FID = fid;

            Flow flow = new Flow(FK_flow);
            if (wkid == 0)
                throw new Exception("@没有指定工作ID, 不能创建工作流程.");
            //Flow flow= new Flow(FlowNo);
            this._HisFlow = flow;
            this._WorkID = wkid;
        }
        #endregion

        #region 公共属性

        /// <summary>
        /// 开始工作
        /// </summary>
        private StartWork _HisStartWork = null;
        /// <summary>
        /// 他开始的工作.
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
        /// 开始工作节点
        /// </summary>
        private WorkNode _HisStartWorkNode = null;
        /// <summary>
        /// 他开始的工作.
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

        #region 运算属性
        public int _IsComplete = -1;
        /// <summary>
        /// 是不是完成
        /// </summary>
        public bool IsComplete
        {
            get
            {
                if (_IsComplete == -1)
                {
                    bool s = !DBAccess.IsExits("select workid from WF_GenerWorkFlow where WorkID=" + this.WorkID + " and FK_Flow='" + this.HisFlow.No + "'");
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
        /// 是不是完成
        /// </summary>
        public string IsCompleteStr
        {
            get
            {
                if (this.IsComplete)
                    return this.ToE("Already", "已");
                else
                    return this.ToE("Not", "未");

            }
        }
        #endregion

        #region 静态方法

        /// <summary>
        /// 是否这个工作人员能执行这个工作
        /// </summary>
        /// <param name="nodeId">节点</param>
        /// <param name="empId">工作人员</param>
        /// <returns>能不能执行</returns> 
        public static bool IsCanDoWorkCheckByEmpStation(int nodeId, string empId)
        {
            bool isCan = false;
            // 判断岗位对应关系是不是能够执行.
            string sql = "SELECT a.FK_Node FROM WF_NodeStation a,  Port_EmpStation b WHERE (a.FK_Station=b.FK_Station) AND (a.FK_Node=" + nodeId + " AND b.FK_Emp='" + empId + "' )";
            isCan = DBAccess.IsExits(sql);
            if (isCan)
                return true;
            // 判断他的主要工作岗位能不能执行它.
            sql = "select fk_Node from WF_NodeStation where FK_Node=" + nodeId + " and ( FK_Station in (select FK_Station from port_empstation where fk_emp='" + empId + "') ) ";
            return DBAccess.IsExits(sql);
        }
        /// <summary>
        /// 是否这个工作人员能执行这个工作
        /// </summary>
        /// <param name="nodeId">节点</param>
        /// <param name="dutyNo">工作人员</param>
        /// <returns>能不能执行</returns> 
        public static bool IsCanDoWorkCheckByEmpDuty(int nodeId, string dutyNo)
        {
            string sql = "SELECT a.FK_Node FROM WF_NodeDuty  a,  Port_EmpDuty b WHERE (a.FK_Duty=b.FK_Duty) AND (a.FK_Node=" + nodeId + " AND b.FK_Duty=" + dutyNo + ")";
            if (DBAccess.RunSQLReturnTable(sql).Rows.Count == 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 是否这个工作人员能执行这个工作
        /// </summary>
        /// <param name="nodeId">节点</param>
        /// <param name="DeptNo">工作人员</param>
        /// <returns>能不能执行</returns> 
        public static bool IsCanDoWorkCheckByEmpDept(int nodeId, string DeptNo)
        {
            string sql = "SELECT a.FK_Node FROM WF_NodeDept  a,  Port_EmpDept b WHERE (a.FK_Dept=b.FK_Dept) AND (a.FK_Node=" + nodeId + " AND b.FK_Dept=" + DeptNo + ")";
            if (DBAccess.RunSQLReturnTable(sql).Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 在物理上能构作这项工作的人员。
        /// </summary>
        /// <param name="nodeId">节点ID</param>		 
        /// <returns></returns>
        public static DataTable CanDoWorkEmps(int nodeId)
        {
            string sql = "select a.FK_Node, b.EmpID from wf_Nodestation  a,  Port_EmpStation b where (a.FK_Station=b.FK_Station) and (a.FK_Node=" + nodeId + " )";
            return DBAccess.RunSQLReturnTable(sql);
        }
        /// <summary>
        /// GetEmpsBy
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Emps GetEmpsBy(DataTable dt)
        {
            // 形成能够处理这件事情的用户几何。
            Emps emps = new Emps();
            foreach (DataRow dr in dt.Rows)
            {
                emps.AddEntity(new Emp(dr["EmpID"].ToString()));
            }
            return emps;
        }

        #endregion

        #region 流程方法
        /// <summary>
        /// 执行撤消
        /// </summary>
        public string UnSend(string fk_emp)
        {
            GenerWorkFlow gwf = new GenerWorkFlow(this.WorkID, this.HisFlow.No);
            Node nd = new Node(gwf.FK_Node);
            if (nd.IsStartNode)
                return "您不能撤消发送，因为它是开始节点。<a href='../../WF/MyFlowInfo.aspx?DoType=DeleteFlow&WorkID=" + this.WorkID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='../../Images/Btn/Delete.gif' border=0/>结束流程</a>";

            WorkNode wn = this.GetCurrentWorkNode();
            WorkNode wnPri = wn.GetPreviousWorkNode();

            WorkerList wl = new WorkerList();
            int num = wl.Retrieve(WorkerListAttr.FK_Emp, Web.WebUser.No,
                WorkerListAttr.FK_Node, wnPri.HisNode.NodeID);

            if (num == 0)
                return "@" + this.ToE("WF6", "您不能执行撤消发送，因为当前工作不是您发送的。");

            // if (wl.RDT.IndexOf(DataType.CurrentData) == -1)
            //   return "@您不能执行撤消发送，因为当前工作不是您当天发送的，发送日期"+wl.RDT+"。" ;

            WorkerLists wls = new WorkerLists();
            wls.Delete(WorkerListAttr.WorkID, this.WorkID,
                WorkerListAttr.FK_Node, gwf.FK_Node.ToString());
            wn.HisWork.Delete();
            CHOfNodes chs = new CHOfNodes();
            chs.Delete(CHOfNodeAttr.FK_Node, wn.HisNode.NodeID,
                CHOfNodeAttr.WorkID, wn.WorkID.ToString());

            gwf.FK_Node = wl.FK_Node;
            gwf.Update();

            ForwardWorks fws = new ForwardWorks();
            fws.Delete(ForwardWorkAttr.NodeId, wn.HisNode.NodeID.ToString(), ForwardWorkAttr.WorkID, this.WorkID.ToString());

            ReturnWorks rws = new ReturnWorks();
            rws.Delete(ReturnWorkAttr.NodeId, wn.HisNode.NodeID.ToString(), ReturnWorkAttr.WorkID, this.WorkID.ToString());


            //if (wnPri.HisNode.IsStartNode)
            //{
            //    Reback rb = new Reback();
            //    rb.NodeId = wnPri.HisNode.NodeID;
            //    rb.WorkID = this.WorkID;
            //    rb.FK_Emp = Web.WebUser.No;
            //    try
            //    {
            //        rb.Insert();
            //    }
            //    catch
            //    { 
            //    }
            //}
            if (wnPri.HisNode.IsStartNode)
            {
                //工作{0}被您撤消成功，您可以点这里
                // return "@撤消执行成功，您可以点这里" + this.ToE("WF7", wn.HisNode.EnDesc) + "<a href='../../WF/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "执行工作") + "</A> , <a href='../../WF/Do.aspx?ActionType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='../../Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "此流程已经完成") + "</a>。";

                if (this.HisFlow.FK_FlowSort != "00")
                    return "@撤消执行成功，您可以点这里<a href='../../WF/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "执行工作") + "</A> , <a href='../../WF/MyFlowInfo.aspx?DoType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='../../Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "此流程已经完成(删除它)") + "</a>。";
                else
                    return "@撤消执行成功，您可以点这里<a href='../../GovDoc/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "执行工作") + "</A> , <a href='../../WF/Do.aspx?ActionType=DeleteFlow&WorkID=" + wn.HisWork.OID + "&FK_Flow=" + this.HisFlow.No + "' /><img src='../../Images/Btn/Delete.gif' border=0/>" + this.ToE("FlowOver", "此流程已经完成(删除它)") + "</a>。";
            }
            else
            {
                // 更新是否显示。
                DBAccess.RunSQL("UPDATE wf_forwardwork SET IsTakeBack=1 WHERE WORKID=" + this.WorkID + " AND NODEID=" + wnPri.HisNode.NodeID);
                //  return "@" + this.ToE("WF7", wn.HisNode.EnDesc) + "<a href='../../WF/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "执行工作") + "</A> 。";
                if (this.HisFlow.FK_FlowSort != "00")
                    return "@撤消执行成功，您可以点这里<a href='../../WF/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "执行工作") + "</A>。";
                else
                    return "@撤消执行成功，您可以点这里<a href='../../GovDoc/MyFlow.aspx?FK_Flow=" + this.HisFlow.No + "&WorkID=" + this.WorkID + "'>" + this.ToE("DoWork", "执行工作") + "</A>。";
            }
        }
        #endregion

    }
    /// <summary>
    /// 工作流程集合.
    /// </summary>
    public class WorkFlows : CollectionBase
    {
        #region 构造
        /// <summary>
        /// 工作流程
        /// </summary>
        /// <param name="flow">流程编号</param>
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
        /// 工作流程集合
        /// </summary>
        public WorkFlows()
        {
        }
        /// <summary>
        /// 工作流程集合
        /// </summary>
        /// <param name="flow">流程</param>
        /// <param name="flowState">工作ID</param> 
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

        #region 查询方法
        /// <summary>
        /// GetNotCompleteNode
        /// </summary>
        /// <param name="flowNo">流程编号</param>
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

        #region 方法
        /// <summary>
        /// 增加一个工作流程
        /// </summary>
        /// <param name="wn">工作流程</param>
        public void Add(WorkFlow wn)
        {
            this.InnerList.Add(wn);
        }
        /// <summary>
        /// 根据位置取得数据
        /// </summary>
        public WorkFlow this[int index]
        {
            get
            {
                return (WorkFlow)this.InnerList[index];
            }
        }
        #endregion

        #region 关于调度的自动方法
        /// <summary>
        /// 清除死节点。
        /// 死节点的产生，就是用户非法的操作，或者系统出现存储故障，造成的流程中的当前工作节点没有工作人员，从而不能正常的运行下去。
        /// 清除死节点，就是把他们放到死节点工作集合里面。
        /// </summary>
        /// <returns></returns>
        public static string ClearBadWorkNode()
        {
            string infoMsg = "清除死节点的信息：";
            string errMsg = "清除死节点的错误信息：";
            return infoMsg + errMsg;
        }
        #endregion
    }
}
