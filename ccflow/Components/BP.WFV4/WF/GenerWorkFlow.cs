
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port ; 
using BP.En;


namespace BP.WF
{
	/// <summary>
	/// 产生的工作
	/// </summary>
    public class GenerWorkFlowAttr
    {
        #region 基本属性
        /// <summary>
        /// 地税编号
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 工作流
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 流程状态
        /// </summary>
        public const string WFState = "WFState";
        /// <summary>
        /// 标题
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// 记录人
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// 产生时间
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 当前点应完成日期
        /// </summary>
        public const string SDT = "SDT";
        /// <summary>
        /// 完成时间
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// 得分
        /// </summary>
        public const string Cent = "Cent";
        /// <summary>
        /// note
        /// </summary>
        public const string FlowNote = "FlowNote";
        /// <summary>
        /// 当前工作到的节点.
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 当前工作岗位
        /// </summary>
        public const string FK_Station = "FK_Station";
        /// <summary>
        /// 部门
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// 流程ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// 是否启用
        /// </summary>
        public const string IsEnable = "IsEnable";
        /// <summary>
        /// 流程名称
        /// </summary>
        public const string FlowName = "FlowName";
        /// <summary>
        /// 发起人名称
        /// </summary>
        public const string RecName = "RecName";
        /// <summary>
        /// 节点名称
        /// </summary>
        public const string NodeName = "NodeName";
        /// <summary>
        /// 部门名称
        /// </summary>
        public const string DeptName = "DeptName";
        /// <summary>
        /// 流程类别
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        #endregion
    }
	/// <summary>
	/// 产生的工作
	/// </summary>
	public class GenerWorkFlow : Entity
	{	
		#region 基本属性
        ///// <summary>
        ///// HisFlow
        ///// </summary>
        //public Flow HisFlow
        //{
        //    get
        //    {
        //        return new Flow(this.FK_Flow); 
        //    }
        //}
		/// <summary>
		/// 工作流程编号
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.FK_Flow);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.FK_Flow,value);
			}
		}
        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.FlowName);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FlowName, value);
            }
        }
        /// <summary>
        /// 类别编号
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.FK_FlowSort);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FK_FlowSort, value);
            }
        }
//		/// <summary>
//		/// 部门
//		/// </summary>
//		public string  FK_Dept
//		{
//			get
//			{
//				return this.GetValStrByKey(GenerWorkFlowAttr.FK_Dept);
//			}
//			set
//			{
//				SetValByKey(GenerWorkFlowAttr.FK_Dept,value);
//			}
//		}
		public string  FK_Dept
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.FK_Dept);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.Title);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.Title,value);
			}
		}
		/// <summary>
		/// 产生时间
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStrByKey(GenerWorkFlowAttr.RDT);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.RDT,value);
			}
		}
//		/// <summary>
//		/// 流程备注
//		/// </summary>
//		public string  FlowNote
//		{
//			get
//			{
//				return this.GetValStrByKey(GenerWorkFlowAttr.FlowNote);
//			}
//			set
//			{
//				SetValByKey(GenerWorkFlowAttr.FlowNote,value);
//			}
//		}		
		/// <summary>
		/// 流程ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
                return this.GetValInt64ByKey(GenerWorkFlowAttr.WorkID);
			}
			set
			{
				SetValByKey(GenerWorkFlowAttr.WorkID,value);
			}
		}
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(GenerWorkFlowAttr.FID);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.FID, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.Rec);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.Rec, value);
            }
        }
        public string RecName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.RecName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.RecName, value);
            }
        }
        public string DeptName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.DeptName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.DeptName, value);
            }
        }
        public string NodeName
        {
            get
            {
                return this.GetValStrByKey(GenerWorkFlowAttr.NodeName);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.NodeName, value);
            }
        }
		/// <summary>
		/// 当前工作到的节点
		/// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(GenerWorkFlowAttr.FK_Node, value);
            }
        }
        /// <summary>
		/// 工作流程状态( 0, 未完成,1 完成, 2 强制终止 3, 删除状态,) 
		/// </summary>
        public int WFState
        {
            get
            {
                return this.GetValIntByKey(GenerWorkFlowAttr.WFState);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.WFState, value);
            }
        }
        public string WFStateText
        {
            get
            {
                BP.WF.WFState ws = (WFState)this.WFState;
                switch(ws)
                {
                    case WF.WFState.Complete:
                        return "已完成";
                    case WF.WFState.Delete:
                        return "已删除";
                    case WF.WFState.Runing:
                        return "在运行";
                    case WF.WFState.Stop:
                        return "已停止";
                    default:
                        return "未判断";
                }
            }
        }
        /// <summary>
        /// 流程是否启用？
        /// </summary>
        public bool IsEnable_del
        {
            get
            {
                return this.GetValBooleanByKey(GenerWorkFlowAttr.IsEnable);
            }
            set
            {
                SetValByKey(GenerWorkFlowAttr.IsEnable, value);
            }
        }
		#endregion

		#region 构造函数
		/// <summary>
		/// 产生的工作流程
		/// </summary>
		public GenerWorkFlow()
		{
		}		 
		public GenerWorkFlow(Int64 workId)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GenerWorkFlowAttr.WorkID, workId);
			if (qo.DoQuery()==0)
				throw new Exception("工作["+workId+"]不存在，可能是已经完成。");
		}
        ///// <summary>
        ///// 产生的工作流程
        ///// </summary>
        ///// <param name="workId">工作流程ID</param>
        ///// <param name="flowNo">流程编号</param>
        //public GenerWorkFlow(Int64 workId, string flowNo)
        //{
        //    try
        //    {
        //        this.WorkID = workId;
        //        this.FK_Flow = flowNo;
        //        this.Retrieve();
        //    }
        //    catch (Exception ex)
        //    {
        //        WorkFlow wf = new WorkFlow(new Flow(flowNo), workId, this.FID);
        //        StartWork wk = wf.HisStartWork;
        //        if (wk.WFState == BP.WF.WFState.Complete)
        //        {
        //            throw new Exception("@已经完成流程，不存在于当前工作集合里，如果要得到此流程的详细信请查看历史工作。技术信息:" + ex.Message);
        //        }
        //        else
        //        {
        //            this.Copy(wk);
        //            //string msg = "@流程内部错误，给您带来的不便，深表示抱歉，请把此情况通知给系统管理员。error code:0001更多的信息:" + ex.Message;
        //            string msg = "@流程内部错误，给您带来的不便，深表示抱歉，请把此情况通知给系统管理员。error code:0001更多的信息:" + ex.Message;
        //            Log.DefaultLogWriteLine(LogType.Error, "@工作完成后在使用它抛出的异常：" + msg);
        //            //throw new Exception(msg);
        //        }
        //    }
        //}
        /// <summary>
        /// 执行修复
        /// </summary>
        public void DoRepair()
        { 
        }
		/// <summary>
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_GenerWorkFlow");
                map.EnDesc =  "未完成流程";

                map.AddTBIntPK(GenerWorkFlowAttr.WorkID, 0, "WorkID", true, true);
                map.AddTBInt(GenerWorkFlowAttr.FID, 0, "流程ID", true, true);

                map.AddTBString(GenerWorkFlowAttr.FK_FlowSort, null, "流程类别", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.FK_Flow, null, "流程", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.FlowName, null, "流程名称", true, false, 0, 500, 10);

                map.AddTBString(GenerWorkFlowAttr.Title, null, "标题", true, false, 0, 500, 10);
                map.AddTBInt(GenerWorkFlowAttr.WFState, 0, "流程状态", true, false);

                map.AddTBString(GenerWorkFlowAttr.Rec, null, "发起人", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.RecName, null, "发起人名称", true, false, 0, 500, 10);

                map.AddTBDateTime(GenerWorkFlowAttr.RDT, "记录日期", true, true);
                map.AddTBInt(GenerWorkFlowAttr.FK_Node, 0, "节点", true, false);
                map.AddTBString(GenerWorkFlowAttr.NodeName, null, "节点名称", true, false, 0, 500, 10);

                map.AddTBString(GenerWorkFlowAttr.FK_Dept, null, "部门", true, false, 0, 500, 10);
                map.AddTBString(GenerWorkFlowAttr.DeptName, null, "部门名称", true, false, 0, 500, 10);


                RefMethod rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "工作报告");  // "工作报告";
                rm.ClassMethodName = this.ToString() + ".DoRpt";
                rm.Icon = "../Images/FileType/doc.gif";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("FlowSelfTest", "流程自检"); // "流程自检";
                rm.ClassMethodName = this.ToString() + ".DoSelfTestInfo";
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "流程自检并修复";
                rm.ClassMethodName = this.ToString() + ".DoRepare";
                rm.Warning = "您确定要执行此功能吗？ \t\n 1)如果是断流程，并且停留在第一个节点上，系统为执行删除它。\t\n 2)如果是非地第一个节点，系统会返回到上次发起的位置。";
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion 

		#region 重载基类方法
		/// <summary>
		/// 删除后,需要把工作者列表也要删除.
		/// </summary>
        protected override void afterDelete()
        {
            // . clear bad worker .  
            DBAccess.RunSQLReturnTable("DELETE FROM WF_GenerWorkerlist WHERE WorkID in  ( select WorkID from WF_GenerWorkerlist WHERE WorkID not in (select WorkID from WF_GenerWorkFlow) )");

            WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID,this.FID);
            wf.DoDeleteWorkFlowByReal(); /* 删除下面的工作。*/

            base.afterDelete();
        }
		#endregion 

		#region 执行诊断
        public string DoRpt()
        {
            PubClass.WinOpen("WFRpt.aspx?WorkID=" + this.WorkID + "&FID="+this.FID+"&FK_Flow="+this.FK_Flow);
            return null;
        }
		/// <summary>
		/// 执行修复
		/// </summary>
		/// <returns></returns>
        public string DoRepare()
        {
            if (this.DoSelfTestInfo() == "没有发现异常。")
                return "没有发现异常。";

            string sql = "SELECT FK_Node FROM WF_GenerWorkerlist WHERE WORKID='" + this.WorkID + "' ORDER BY FK_Node desc";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                /*如果是开始工作节点，就删除它。*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID );
                wf.DoDeleteWorkFlowByReal();
                return "此流程是因为发起工作失败被系统删除。";
            }

            int FK_Node = int.Parse(dt.Rows[0][0].ToString());

            Node nd = new Node(FK_Node);
            if (nd.IsStartNode)
            {
                /*如果是开始工作节点，就删除它。*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID, this.FID);
                wf.DoDeleteWorkFlowByReal();
                return "此流程是因为发起工作失败被系统删除。";
            }

            this.FK_Node = nd.NodeID;
            this.NodeName = nd.Name;
            this.Update();

            string str = "";
            WorkerLists wls = new WorkerLists();
            wls.Retrieve(WorkerListAttr.FK_Node, FK_Node, WorkerListAttr.WorkID, this.WorkID);
            foreach (WorkerList wl in wls)
            {
                str += wl.FK_Emp + wl.FK_EmpText + ",";
            }

            return "此流程是因为[" + nd.Name + "]工作发送失败被回滚到当前位置，请转告[" + str + "]流程修复成功。";
        }
		public string DoSelfTestInfo()
		{
			WorkerLists wls = new WorkerLists(this.WorkID,this.FK_Flow );

			#region  查看一下当前的节点是否开始工作节点。
			Node nd = new Node(this.FK_Node);
			if (nd.IsStartNode)
			{
				/* 判断是否是退回的节点 */
				Work wk = nd.HisWork;
				wk.OID = this.WorkID;
				wk.Retrieve();

				if (wk.NodeState!=NodeState.Back)
				{
					return "当前的工作节点 存在于开始工作节点上 还不是退回流程 不应该出现的情况。建议删除当前的工作节点。 ";
				}
			}
			#endregion


			#region  查看一下是否有当前的工作节点信息。
			bool isHave=false;
			foreach(WorkerList wl in wls)
			{
				if (wl.FK_Node==this.FK_Node)
					isHave=true;
			}

			if (isHave==false)
			{
				/*  */
				return "已经不存在当前的工作节点信息，造成此流程的原因可能是没有捕获的系统异常，建议删除此流程或者交给系统自动修复它。";
			}
			#endregion

			return "没有发现异常。";
		}
		#endregion
	}
	/// <summary>
	/// 产生的工作s
	/// </summary>
	public class GenerWorkFlows : Entities
	{
		/// <summary>
		/// 根据工作流程,工作人员ID 查询出来他当前的能做的工作.
		/// </summary>
		/// <param name="flowNo">流程编号</param>
		/// <param name="empId">工作人员ID</param>
		/// <returns></returns>
		public static DataTable QuByFlowAndEmp(string flowNo, int empId)
		{
			string sql="SELECT a.WorkID FROM WF_GenerWorkFlow a, WF_GenerWorkerlist b WHERE a.WorkID=b.WorkID   AND b.FK_Node=a.FK_Node  AND b.FK_Emp='"+empId.ToString()+"' AND a.FK_Flow='"+flowNo+"'";
			return DBAccess.RunSQLReturnTable(sql);
		}

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new GenerWorkFlow();
			}
		}
		/// <summary>
		/// 产生工作流程集合
		/// </summary>
		public GenerWorkFlows(){}
		#endregion
	}
	
}
