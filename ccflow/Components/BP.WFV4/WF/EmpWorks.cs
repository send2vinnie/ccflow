
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
	/// 人员工作
	/// </summary>
	public class EmpWorkAttr 
	{
		#region 基本属性
        public const string FK_Emp = "FK_Emp";

		/// <summary>
		/// 地税编号
		/// </summary>
		public const  string WorkID="WorkID";
		/// <summary>
		/// 工作流
		/// </summary>
		public const  string FK_Flow="FK_Flow";
		/// <summary>
		/// 流程状态
		/// </summary>
		public const  string WFState="WFState";
		/// <summary>
		/// 标题
		/// </summary>
		public const  string Title="Title";
		/// <summary>
		/// 记录人
		/// </summary>
		public const  string Rec="Rec";
		/// <summary>
		/// 产生时间
		/// </summary>
		public const  string RDT="RDT";
        /// <summary>
        /// 当前点应完成日期
        /// </summary>
        public const string SDT = "SDT";
		/// <summary>
		/// 完成时间
		/// </summary>
		public const  string CDT="CDT";		
		/// <summary>
		/// 得分
		/// </summary>
		public const  string Cent="Cent";		
		/// <summary>
		/// note
		/// </summary>
		public const  string FlowNote="FlowNote";
		/// <summary>
		/// 当前工作到的节点.
		/// </summary>
		public const  string FK_Node="FK_Node";
		/// <summary>
		/// 当前工作岗位
		/// </summary>
		public const  string FK_Station="FK_Station";

		/// <summary>
		/// 部门
		/// </summary>
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// 税务管理码
		/// </summary>
		public const  string FK_Taxpayer="FK_Taxpayer";
		/// <summary>
		/// 纳税人名称
		/// </summary>
		public const  string TaxpayerName="TaxpayerName";


		#endregion
	}
	/// <summary>
	/// 人员工作
	/// </summary>
	public class EmpWork : Entity
	{	
		#region 基本属性
		/// <summary>
		/// HisFlow
		/// </summary>
		public Flow HisFlow
		{
			get
			{
				return new Flow(this.FK_Flow); 
			}
		}
		/// <summary>
		/// 工作流程编号
		/// </summary>
		public string  FK_Flow
		{
			get
			{
				return this.GetValStringByKey(EmpWorkAttr.FK_Flow);
			}
			set
			{
				SetValByKey(EmpWorkAttr.FK_Flow,value);
			}
		}
		public string  FK_FlowText
		{
			get
			{
                //Flow fl = new Flow(this.FK_Flow);
                //return fl.Name;
				return this.GetValRefTextByKey(EmpWorkAttr.FK_Flow);
			}
		}
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(EmpWorkAttr.WorkID);
            }
            set
            {
                SetValByKey(EmpWorkAttr.WorkID, value);
            }
        }
		/// <summary>
		/// 当前的工作岗位
		/// </summary>
		public string  FK_Station
		{
			get
			{
				return this.GetValStringByKey(EmpWorkAttr.FK_Station);
			}
			set
			{
				SetValByKey(EmpWorkAttr.FK_Station,value);
			}
		}
		public string  FK_Dept
		{
			get
			{
				return this.GetValStringByKey(EmpWorkAttr.FK_Dept);
			}
			set
			{
				SetValByKey(EmpWorkAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string  Title
		{
			get
			{
				return this.GetValStringByKey(EmpWorkAttr.Title);
			}
			set
			{
				SetValByKey(EmpWorkAttr.Title,value);
			}
		}
		/// <summary>
		/// 产生时间
		/// </summary>
		public string  RDT
		{
			get
			{
				return this.GetValStringByKey(EmpWorkAttr.RDT);
			}
			set
			{
				SetValByKey(EmpWorkAttr.RDT,value);
			}
		}
        /// <summary>
        /// /应该完成日期
        /// </summary>
        public string SDT
        {
            get
            {
                return this.GetValStringByKey(EmpWorkAttr.SDT);
            }
            set
            {
                SetValByKey(EmpWorkAttr.SDT, value);
            }
        }
//		/// <summary>
//		/// 流程备注
//		/// </summary>
//		public string  FlowNote
//		{
//			get
//			{
//				return this.GetValStringByKey(EmpWorkAttr.FlowNote);
//			}
//			set
//			{
//				SetValByKey(EmpWorkAttr.FlowNote,value);
//			}
//		}		
		 
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(EmpWorkAttr.Rec);
            }
            set
            {
                this.SetValByKey(EmpWorkAttr.Rec, value);
            }
        }
		public string RecText
		{
			get
			{
              //  return this.Rec;
				return this.GetValRefTextByKey(EmpWorkAttr.Rec );
			}
		}
		/// <summary>
		/// 当前工作到的节点
		/// </summary>
		public int  FK_Node
		{
			get
			{
				return this.GetValIntByKey(EmpWorkAttr.FK_Node);
			}
			set
			{
				SetValByKey(EmpWorkAttr.FK_Node,value);
			}
		}		
		public string  FK_NodeText
		{
            get
            {
                return this.GetValRefTextByKey(EmpWorkAttr.FK_Node);
            }
		}
		/// <summary>
		/// 工作流程状态( 0, 未完成,1 完成, 2 强制终止 3, 删除状态,) 
		/// </summary>
		public int  WFState
		{
			get
			{
				return this.GetValIntByKey(EmpWorkAttr.WFState);
			}
			set
			{
				SetValByKey(EmpWorkAttr.WFState,value);
			}
		}
		public string  WFStateText
		{
			get
			{
				return this.GetValRefTextByKey(EmpWorkAttr.WFState);
			}
		} 
		/// <summary>
		/// 流程状态
		/// </summary>
		public string WFStateLab
		{
			get
			{
				return this.GetValRefTextByKey(EmpWorkAttr.WFState);
			}
		}
		#endregion

		#region 构造函数
		/// <summary>
		/// 人员工作流程
		/// </summary>
		public EmpWork()
		{
		}		 
		public EmpWork(int workId)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(EmpWorkAttr.WorkID, workId);
			if (qo.DoQuery()==0)
				throw new Exception("工作["+workId+"]不存在，可能是已经完成。");
		}
		/// <summary>
		/// 人员工作流程
		/// </summary>
		/// <param name="workId">工作流程ID</param>
		/// <param name="flowNo">流程编号</param>
		public EmpWork(Int64 workId, string flowNo)
		{
            try
            {
                this.WorkID = workId;
                this.FK_Flow = flowNo;
                this.Retrieve();
            }
            catch (Exception ex)
            {
                WorkFlow wf = new WorkFlow(new Flow(flowNo), workId, this.FID);
                StartWork wk = wf.HisStartWork;
                if (wk.WFState == BP.WF.WFState.Complete)
                {
                    throw new Exception("@已经完成流程，不存在于当前工作集合里，如果要得到此流程的详细信请查看历史工作。技术信息:" + ex.Message);
                }
                else
                {
                    this.Copy(wk);
                    //string msg = "@流程内部错误，给您带来的不便，深表示抱歉，请把此情况通知给系统管理员。error code:0001更多的信息:" + ex.Message;
                    string msg = "@流程内部错误，给您带来的不便，深表示抱歉，请把此情况通知给系统管理员。error code:0001更多的信息:" + ex.Message;
                    Log.DefaultLogWriteLine(LogType.Error, "@工作完成后在使用它抛出的异常：" + msg);
                    //throw new Exception(msg);
                }
            }
		}
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
                Map map = new Map("WF_GenerEmpWorkDtls");
                map.EnDesc = this.ToE("UnOvFlow", "未完成流程"); // "未完成流程";
                map.EnType = EnType.View;

                map.AddTBIntPK(EmpWorkAttr.WorkID, 0, "WorkID", true, true);

               // map.AddTBInt(EmpWorkAttr.WFState, 0, "WFState", true, true);


                map.AddDDLEntities(EmpWorkAttr.FK_Flow, null, null, new Flows(), false);
                map.AddDDLEntities(EmpWorkAttr.FK_Emp, null, null, new Emps(), false);

                map.AddTBString(EmpWorkAttr.Title, null, null, true, false, 0, 500, 10);

                map.AddTBInt(EmpWorkAttr.WFState, 0, "WFState", true, true);

                map.AddTBDateTime(EmpWorkAttr.RDT, null, true, true);
                map.AddTBDate(EmpWorkAttr.SDT, null, null, true, true);

                map.AddDDLEntities(EmpWorkAttr.FK_Node, 0, DataType.AppInt, null, new Nodes(), NodeAttr.NodeID, NodeAttr.Name, false);

                ////  map.AddTBString(EmpWorkAttr.FK_Station, null,null, true, false, 0, 500, 10);
                ////  map.AddDDLEntities(EmpWorkAttr.FK_Dept, null, null, new Port.Depts(), false);
                ////  map.AddDtl(new WorkerLists(), EmpWorkAttr.WorkID); //他的工作者

                ////map.AddSearchAttr(EmpWorkAttr.FK_Dept);
                ////map.AddSearchAttr(EmpWorkAttr.FK_Flow);
                ////map.AddSearchAttr(EmpWorkAttr.Rec);

                ////map.AttrsOfSearch.AddFromTo("记录从",EmpWorkAttr.RDT,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);

                //RefMethod rm = new RefMethod();
                //rm.Title = this.ToE("WorkRpt", "工作报告"); // "工作报告";
                //rm.ClassMethodName = this.ToString() + ".DoRpt";
                //rm.Icon = "../Images/Btn/Word.gif";
                //map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("FlowSelfTest", "流程自检"); // "流程自检";
                //rm.ClassMethodName = this.ToString() + ".DoSelfTestInfo";
                //map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = this.ToE("FlowRepare", "流程自检并修复");  //"流程自检并修复";
                //rm.ClassMethodName = this.ToString() + ".DoRepare";
                //rm.Warning = "您确定要执行此功能吗？ \t\n 1)如果是断流程，并且停留在第一个节点上，系统为执行删除它。\t\n 2)如果是非地第一个节点，系统会返回到上次发起的位置。";
                //map.AddRefMethod(rm);

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
            DBAccess.RunSQLReturnTable("delete WF_GenerWorkerList where WorkID in  ( select WorkID from WF_GenerWorkerList where WorkID not in (select WorkID from WF_EmpWork) )");

            WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID);
            wf.DoDeleteWorkFlowByReal(); /* 删除下面的工作。*/

            base.afterDelete();
        }
		#endregion 

		#region 执行诊断
        public string DoRpt()
        {
            PubClass.WinOpen("WFRpt.aspx?WorkID=" + this.WorkID + "&FK_Flow"+this.FK_Flow);
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

            string sql = "SELECT FK_NODE FROM WF_GENERWORKERLIST WHERE WORKID='" + this.WorkID + "' ORDER BY FK_Node desc";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            if (dt.Rows.Count == 0)
            {
                /*如果是开始工作节点，就删除它。*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID);
                wf.DoDeleteWorkFlowByReal();
                return "此流程是因为发起工作失败被系统删除。";
            }

            int fk_node = int.Parse(dt.Rows[0][0].ToString());

            Node nd = new Node(fk_node);
            if (nd.IsStartNode)
            {
                /*如果是开始工作节点，就删除它。*/
                WorkFlow wf = new WorkFlow(new Flow(this.FK_Flow), this.WorkID);
                wf.DoDeleteWorkFlowByReal();
                return "此流程是因为发起工作失败被系统删除。";
            }

            this.FK_Node = fk_node;
            this.Update();

            string str = "";
            WorkerLists wls = new WorkerLists();
            wls.Retrieve(WorkerListAttr.FK_Node, fk_node, WorkerListAttr.WorkID, this.WorkID);
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
	/// 人员工作s
	/// </summary>
	public class EmpWorks : Entities
	{
		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new EmpWork();
			}
		}
		/// <summary>
		/// 产生工作流程集合
		/// </summary>
		public EmpWorks(){}
		#endregion
	}
	
}
