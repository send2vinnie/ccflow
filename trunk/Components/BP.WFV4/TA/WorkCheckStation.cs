using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;
using BP.Web;
using BP.Sys;

namespace BP.TA
{
	/// <summary>
	/// 任务节点属性
	/// </summary>
	public class WorkAttr:EntityOIDAttr
	{
		/// <summary>
		/// 父节点ID
		/// </summary>
		public const string ParentID="ParentID"; 
		/// <summary>
		/// 步骤
		/// </summary>
		public const string Step="Step"; 
		/// <summary>
		/// 考核分
		/// </summary>
		public const string CentOfCheck="CentOfCheck"; 
		/// <summary>
		/// 节点状态
		/// </summary>
		public const string WorkState="WorkState"; 	
		/// <summary>
		/// 优先级
		/// </summary>
		public const string PRI="PRI"; 
		/// <summary>
		/// 考核范围
		/// </summary>
		public const string CheckScope="CheckScope"; 
		/// <summary>
		/// 是否需要阅读回执
		/// </summary>
		public const string IsRe="IsRe"; 
		/// <summary>
		/// 标题
		/// </summary>
		public const string Title="Title"; 
		/// <summary>
		/// 内容
		/// </summary>
		public const string Docs="Docs"; 
		/// <summary>
		/// Emps
		/// </summary>
		public const string Emps="Emps"; 	
		/// <summary>
		/// 执行人
		/// </summary>
		public const string Executer="Executer";
		/// <summary>
		/// 发送人
		/// </summary>
		public const string Sender="Sender";
		/// <summary>
		/// 接受时间
		/// </summary>
		public const string DateTimeOfAccept="DateTimeOfAccept";
		/// <summary>
		/// 完成时间
		/// </summary>
		public const string DateTimeOfSend_del="DateTimeOfSend";

		/// <summary>
		/// 任务开始时间
		/// </summary>
		public const string DateTimeOfTaskStart="DateTimeOfTaskStart";
		/// <summary>
		/// 要求完成时间
		/// </summary>
		public const string DateTimeOfTaskEnd="DateTimeOfTaskEnd";


		#region 考核方式
		/// <summary>
		/// 考核方式
		/// </summary>
		public const string CheckWay="CheckWay";

		/// <summary>
		/// 一次性扣
		/// </summary>
		public const string CentOfOnce="CentOfOnce";
		/// <summary>
		/// 每天扣
		/// </summary>
		public const string CentOfPerDay="CentOfPerDay";
		/// <summary>
		/// 最高扣分
		/// </summary>
		public const string CentOfMax="CentOfMax";
		#endregion


		#region 循环方式
		/// <summary>
		/// 循环方式
		/// </summary>
		public const string CycleWay="CycleWay";
		/// <summary>
		/// 周
		/// </summary>
		public const string CycleWeek="CycleWeek";
		/// <summary>
		/// 天
		/// </summary>
		public const string CycleDay="CycleDay";
		/// <summary>
		/// 年天
		/// </summary>
		public const string CycleYearDay="CycleYearDay";
		/// <summary>
		/// 月
		/// </summary>
		public const string CycleMonth="CycleMonth";
		#endregion


	}
	/// <summary>
	/// 任务节点
	/// </summary> 
	public class Work : EntityOID
	{
		public static Work GenerDraftWork()
		{
			Work tn = new Work();

			if (tn.IsExit(WorkAttr.WorkState, (int)WorkState.None, WorkAttr.Sender,WebUser.No)==false)
			{
				tn.MyWorkState=WorkState.None;
				tn.Sender=WebUser.No;
				tn.Insert();
			}
			else
			{
				tn.RetrieveByAttrAnd(WorkAttr.WorkState, (int)WorkState.None, WorkAttr.Sender,WebUser.No);
			}

			return tn;			 
		}
		#region 扩展 属性
		/// <summary>
		/// 它的回复节点
		/// </summary>
		public ReWork HisReWork
		{
			get
			{
				ReWork nd = new ReWork();
				nd.OID=this.OID;
				if (nd.IsExit(WorkAttr.OID, this.OID)==false)
				{
					nd.InsertAsOID(this.OID);
					
				}
				else
				{
					nd.Retrieve();
					return nd;
				}			 

				nd.Title="答复:"+this.Title;
				//nd.Docs="您好"+this.SenderText+": \n\n  您分配的《"+this.Title+"》任务现完成情况如下: \n  1、\n  2、     \n\n "+WebUser.Name+"\n"+DataType.CurrentDataTimeCN +"\n------------------ 以下是原文 -------------------\n标题:"+this.Title+"\n时间:"+this.DateTimeOfTaskStart+"\n\n"+this.Docs;
				nd.Docs="您好"+this.SenderText+": \n\n  您分配的《"+this.Title+"》任务现完成情况如下: \n  1、\n  2、     \n\n "+WebUser.Name+"\n"+DataType.CurrentDataTimeCN;

				nd.ParentID=this.ParentID;
				nd.Reer=this.Executer;
				nd.ReActionDateTime=DataType.CurrentDataTime;
				nd.MyReWorkState=ReWorkState.None; 
				nd.Accepter=this.Sender; 
				nd.Update();
				return nd;
			}
		}
		/// <summary>
		/// 它的退回节点
		/// </summary>
		public ReturnWork HisReturnWork
		{
			get
			{
				ReturnWork nd = new ReturnWork();
				nd.OID=this.OID;
				if (nd.IsExit(WorkAttr.OID, this.OID)==false)
				{
					nd.InsertAsOID(this.OID);
				}
				else
				{
					nd.Retrieve();
					return nd;
				}
				

				nd.ParentID=this.ParentID;
				//nd.ReturnReason=reason;
				nd.Returner=this.Executer;
				nd.ReturnActionDateTime=DataType.CurrentDataTime;
				//nd.MyReturnWorkState=ReturnWorkState.None; 
			
				nd.Accepter=this.Sender; 
				nd.Update();

				return nd;
			}
		}
		/// <summary>
		/// 它的父节点
		/// </summary>
		public Work HisParentNode
		{
			get
			{
				if (this.ParentID==0)
					return this;

				return new Work(this.ParentID);
			}
		}

		/// <summary>
		/// 它的下一级节点
		/// </summary>
		public Works HisNextChildNodes
		{
			get
			{
				Works tns  = new Works();
				QueryObject qo = new QueryObject(tns);
				qo.AddWhere(WorkAttr.ParentID,this.OID);
				qo.DoQuery();
				return tns;
			}
		}
		#endregion

		#region 基本属性。
		/// <summary>
		/// 紧急程度
		/// </summary>
		public int PRI 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.PRI);
			}
			set
			{
				SetValByKey(WorkAttr.PRI,value);
			}
		}
		/// <summary>
		/// 是否阅读回执
		/// </summary>
		public bool IsRe 
		{
			get
			{
				return this.GetValBooleanByKey(WorkAttr.IsRe);
			}
			set
			{
				SetValByKey(WorkAttr.IsRe,value);
			}
		}
		/// <summary>
		/// 父节点
		/// </summary>
		public int ParentID 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.ParentID);
			}
			set
			{
				SetValByKey(WorkAttr.ParentID,value);
			}
		}
		 
		/// <summary>
		/// 步骤
		/// </summary>
		public int Step 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.Step);
			}
			set
			{
				SetValByKey(WorkAttr.Step,value);
			}
		}
		/// <summary>
		/// 考核分
		/// </summary>
		public int CentOfCheck
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CentOfCheck);
			}
			set
			{
				SetValByKey(WorkAttr.CentOfCheck,value);
			}
		}
		/// <summary>
		/// 执行人
		/// </summary>
		public string Executer 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Executer);
			}
			set
			{
				SetValByKey(WorkAttr.Executer,value);
			}
		}
		/// <summary>
		/// 执行人
		/// </summary>
		public string ExecuterText
		{
			get
			{
				return this.GetValRefTextByKey(WorkAttr.Executer);
			}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Title 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Title);
			}
			set
			{
				SetValByKey(WorkAttr.Title,value);
			}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public string DocsHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(WorkAttr.Docs);
			}
			
		}
		public string Docs
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Docs);
			}
			set
			{
				SetValByKey(WorkAttr.Docs,value);
			}
		}
		/// <summary>
		/// Emps
		/// </summary>
		public string EmpStrs
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Emps);
			}
			set
			{
				SetValByKey(WorkAttr.Emps,value);
			}
		}
		/// <summary>
		/// 发送人
		/// </summary>
		public string Sender 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Sender);
			}
			set
			{
				SetValByKey(WorkAttr.Sender,value);
			}
		}
		/// <summary>
		/// 发送人
		/// </summary>
		public string SenderText
		{
			get
			{
				return this.GetValRefTextByKey(WorkAttr.Sender);
			}
		}
//		/// <summary>
//		/// 接受时间
//		/// </summary>
//		public string DateTimeOfAccept_del
//		{
//			get
//			{
//				return this.GetValStringByKey(WorkAttr.DateTimeOfAccept);
//			}
//			set
//			{
//				SetValByKey(WorkAttr.DateTimeOfAccept,value);
//			}
//		}
//		public string DateOfAccept
//		{
//			get
//			{
//				return DataType.ParseSysDateTime2SysDate(this.DateTimeOfAccept);
//			}
//		}
//		 
//		/// <summary>
//		/// DateTimeOfAccept_S
//		/// </summary>
//		public DateTime DateTimeOfAccept_S
//		{
//			get
//			{
//				return DataType.ParseSysDate2DateTime(this.DateTimeOfAccept);
//			}
//		}
		/// <summary>
		/// 操作
		/// </summary>
		public string MyWorkStateOpStr
		{
			get
			{
				if (WebUser.No==this.Sender && this.ParentID==0)
				{
					/* 如果我是此工作的发起人 */					  
					return this.MyWorkStateTextImg+"<a href=\"javascript:TaskTree('"+this.OID+"','"+(int)BP.Web.TA.ActionType.ViewNode+"')\" >"+ this.Title +"</a>"+BP.Web.Comm.UC.UCSys.FilesViewStr(this.ToString(),this.OID)+"分配给"+this.EmpStrs;
				}

				string title=this.Title;
				switch(this.MyWorkState)
				{
					case WorkState.ReOver: //如果已经认可
					case WorkState.Re: //如果已经认可
					case WorkState.Returning: //如果已经认可
					case WorkState.ReturnOver: //如果已经认可
					case WorkState.CallBack: //如果已经认可
						title="<strike>"+title+"</strike>";
						break;
					default:
						break;
				}
				return this.MyWorkStateTextImg+"<b>分配人:</b>"+this.SenderText+"<b>执行人:</b>"+this.ExecuterText+"<a href=\"javascript:Task('"+this.OID+"','"+(int)BP.Web.TA.ActionType.ViewNode+"')\" >"+ title +"</a>"+BP.Web.Comm.UC.UCSys.FilesViewStr(this.ToString(),this.OID);
			}
		}
		public string MyWorkStateOpStrShort
		{
			get
			{
				if (WebUser.No==this.Sender && this.ParentID==0)
				{
					/* 如果我是此工作的发起人 */					  
					return "<a href=\"javascript:TaskTree('"+this.OID+"','"+(int)BP.Web.TA.ActionType.ViewNode+"')\" >"+ this.Title +"</a>";
				}

				string title=this.Title;
				switch(this.MyWorkState)
				{
					case WorkState.ReOver: //如果已经认可
					case WorkState.Re: //如果已经认可
					case WorkState.Returning: //如果已经认可
					case WorkState.ReturnOver: //如果已经认可
					case WorkState.CallBack: //如果已经认可
						title="<strike>"+title+"</strike>";
						break;
					default:
						break;
				}
				return "<a href=\"javascript:Task('"+this.OID+"','"+(int)BP.Web.TA.ActionType.ViewNode+"')\" >"+ title +"</a>";
			}
		}
		public string MyWorkStateTextImg
		{
			get
			{
				return "<img src='./images/MyWork.gif' border=0 /><img src='./images/"+this.MyWorkState+".gif' border=0 />"+this.MyWorkStateText;
			}
		}
		public string MyWorkStateImg
		{
			get
			{
				return "<img src='./images/"+this.MyWorkState+".gif' border=0 />";
			}
		}
		/// <summary>
		/// 节点状态
		/// </summary>
		public WorkState MyWorkState
		{
			get
			{
				return (WorkState)this.GetValIntByKey(WorkAttr.WorkState);
			}
			set
			{
				SetValByKey(WorkAttr.WorkState,(int)value);
			}
		}
		/// <summary>
		/// 节点状态 标题
		/// </summary>
		public string MyWorkStateText
		{
			get
			{
				return  GetValRefTextByKey(WorkAttr.WorkState);
			}
		}
		public string DateOfTaskStart
		{
			get
			{
				return DataType.ParseSysDateTime2SysDate(this.DateTimeOfTaskStart);
			}
		}

		/// <summary>
		/// 任务开始时间
		/// </summary>
		public string DateTimeOfTaskStart 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.DateTimeOfTaskStart);
			}
			set
			{
				SetValByKey(WorkAttr.DateTimeOfTaskStart,value);
			}
		}
		/// <summary>
		/// 任务开始时间
		/// </summary>
		public DateTime DateTimeOfTaskStart_S
		{
			get
			{
				return DataType.ParseSysDateTime2DateTime(this.DateTimeOfTaskStart);
			}
		}

		/// <summary>
		/// 任务 结束 时间
		/// </summary>
		public string DateTimeOfTaskEnd 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.DateTimeOfTaskEnd);
			}
			set
			{
				SetValByKey(WorkAttr.DateTimeOfTaskEnd,value);
			}
		}
		/// <summary>
		/// 任务 结束 时间
		/// </summary>
		public DateTime DateTimeOfTaskEnd_S
		{
			get
			{
				return DataType.ParseSysDateTime2DateTime(this.DateTimeOfTaskEnd);
			}
		}
		#endregion

		#region 考核属性
		/// <summary>
		/// 考核范围
		/// </summary>
		public CheckScope MyCheckScope
		{
			get
			{
				return (CheckScope)this.GetValIntByKey(WorkAttr.CheckScope);
			}
			set
			{
				this.SetValByKey(WorkAttr.CheckScope,(int)value);
			}
		}
		/// <summary>
		/// 考核方式
		/// </summary>
		public CheckWay MyCheckWay
		{
			get
			{
				return (CheckWay)this.GetValIntByKey(WorkAttr.CheckWay);
			}
			set
			{
				this.SetValByKey(WorkAttr.CheckWay,(int)value);
			}
		}
		public string MyCheckWayText
		{
			get
			{
				return this.GetValRefTextByKey(WorkAttr.CheckWay);
			}
		}
		public string MyCheckWayTextExt
		{
			get
			{
				
				switch(this.MyCheckWay)
				{
					case CheckWay.UnSet:
						return this.MyCheckWayText;
						break;
					case CheckWay.ByDays:
					case CheckWay.ByOnce:
						return " <a href=\"javascript:WinOpen('WorkOption.aspx?RefOID="+this.OID+"','ParentID')\" >"+this.MyCheckWayText+"</a>";
						break;
					default:
						break;
				}
				return null;
			}
		}
		/// <summary>
		/// 扣分尺度
		/// </summary>
		public int CentOfOnce 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CentOfOnce);
			}
			set
			{
				SetValByKey(WorkAttr.CentOfOnce,value);
			}
		}
		public int CentOfPerDay 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CentOfPerDay);
			}
			set
			{
				SetValByKey(WorkAttr.CentOfPerDay,value);
			}
		}
		/// <summary>
		/// 最高扣分(在按延期天扣分时间有用) 
		/// </summary>
		public int CentOfMax 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CentOfMax);
			}
			set
			{
				SetValByKey(WorkAttr.CentOfMax,value);
			}
		}
		#endregion 

		#region 事件循环属性
		/// <summary>
		/// 考核方式
		/// </summary>
		public CycleWay MyCycleWay
		{
			get
			{
				return (CycleWay)this.GetValIntByKey(WorkAttr.CycleWay);
			}
			set
			{
				this.SetValByKey(WorkAttr.CycleWay,(int)value);
			}
		}
		/// <summary>
		/// CycleWeek
		/// </summary>
		public string CycleWeek 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.CycleWeek);
			}
			set
			{
				SetValByKey(WorkAttr.CycleWeek,value);
			}
		}
		/// <summary>
		/// CycleDay
		/// </summary>
		public string CycleDay 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.CycleDay);
			}
			set
			{
				SetValByKey(WorkAttr.CycleDay,value);
			}
		}
		/// <summary>
		/// CycleYearDay
		/// </summary>
		public string CycleYearDay 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.CycleYearDay);
			}
			set
			{
				SetValByKey(WorkAttr.CycleYearDay,value);
			}
		}
		/// <summary>
		/// CycleMonth
		/// </summary>
		public string CycleMonth 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.CycleMonth);
			}
			set
			{
				SetValByKey(WorkAttr.CycleMonth,value);
			}
		}


		/// <summary>
		/// CycleWeekInt
		/// </summary>
		public int CycleWeekInt
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CycleWeek);
			}
			set
			{
				SetValByKey(WorkAttr.CycleWeek,value);
			}
		}
	 
		/// <summary>
		/// CycleMonthInt
		/// </summary>
		public int CycleMonthInt
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CycleMonth);
			}
			set
			{
				SetValByKey(WorkAttr.CycleMonth,value);
			}
		}
		public int CycleDayInt
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.CycleDay);
			}
			set
			{
				SetValByKey(WorkAttr.CycleDay,value);
			}
		}
		#endregion 
		 
		#region 构造函数
		public override UAC HisUAC
		{
			get
			{
//				UAC uac = new UAC();
//				uac.OpenAll();
				//return uac;
				return base.HisUAC;
			}
		}
		/// <summary>
		/// 任务节点
		/// </summary>
		public Work()
		{
		  
		}
		/// <summary>
		/// 任务节点
		/// </summary>
		/// <param name="_No">No</param>
		public Work(int oid):base(oid)
		{
			this.OID=oid;
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("TA_Work");
				map.EnDesc="任务节点";
				map.Icon="./Images/Work_s.ico";

				map.AddTBIntPKOID();
				map.AddTBInt(WorkAttr.ParentID,0,"父节点ID",true,true);
				map.AddTBInt(WorkAttr.Step,1,"步骤",true,true);
				map.AddBoolean(WorkAttr.IsRe,false,"阅读回执",true,false);
				map.AddDDLSysEnum(WorkAttr.WorkState, (int) WorkState.None ,"节点状态",true,true);
				map.AddDDLSysEnum(WorkAttr.PRI,0,"优先级",true,true);
				map.AddTBString(WorkAttr.Emps,null,"接受人",true,false,0,5000,15);
				map.AddTBString(WorkAttr.Title,null,"任务标题",true,false,0,500,15);	
				map.AddTBString(WorkAttr.Docs,null,"任务内容",true,false,0,5000,15);
				map.AddDDLEntitiesNoName(WorkAttr.Sender,null,"任务下达人", new EmpExts(),false );
				map.AddTBDateTime(WorkAttr.DateTimeOfAccept,"任务下达时间",false,false );

				map.AddDDLEntitiesNoName(WorkAttr.Executer,null,"执行人", new EmpExts(),false );

				map.AddTBDateTime(WorkAttr.DateTimeOfTaskStart,"任务开始时间",false,false );
				map.AddTBDateTime(WorkAttr.DateTimeOfTaskEnd,"任务结束时间",false,false );


				map.AddDDLSysEnum(WorkAttr.CheckWay,(int)CheckWay.UnSet,"考核方式",true,true);
				map.AddDDLSysEnum(WorkAttr.CheckScope,(int)CheckScope.ToEmp,"考核范围",true,true);

				map.AddTBInt(WorkAttr.CentOfOnce,5,"扣分尺度",true,true);
				map.AddTBInt(WorkAttr.CentOfPerDay,5,"扣分尺度",true,true);
				map.AddTBInt(WorkAttr.CentOfMax,10,"最高扣分",true,true);

				map.AddTBInt(WorkAttr.CentOfCheck,10,"考核分",true,true);

				map.AddDDLSysEnum(WorkAttr.CycleWay,(int)CycleWay.UnSet,"循环方式",true,true);
				map.AddTBString(WorkAttr.CycleWeek,"1","Weeks",true,false,0,5000,15);
				map.AddTBString(WorkAttr.CycleDay,"1","Days",true,false,0,5000,15);
				map.AddTBString(WorkAttr.CycleMonth,"1","Months",true,false,0,5000,15);
				map.AddTBString(WorkAttr.CycleYearDay,"1","Days",true,false,0,5000,15);


				
				map.AddSearchAttr(WorkAttr.CheckWay);
				map.AttrsOfSearch.AddHidden(WorkAttr.Sender,"=",Web.WebUser.No);
				map.AttrsOfSearch.AddFromTo("开始日期",WorkAttr.DateTimeOfTaskStart,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);

 
 
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
//			if (this.ParentID==0)
//				this.DateTimeOfAccept=this.DateOfTaskStart;
			return base.beforeUpdateInsertAction ();
		}

		#endregion 

		 

		/// <summary>
		/// 执行签收
		/// </summary>
		public void DoRead()
		{
			
			if (this.MyWorkState == WorkState.Allot )
				return;
			 
			this.Update( WorkAttr.WorkState,(int)WorkState.Read);
			 
		}
		/// <summary>
		/// 执行收回任务。
		/// </summary>
		public void DoTakeBack()
		{
			this.Delete();
		}
		/// <summary>
		/// 执行读取退回节点
		/// </summary>
		public void DoReadReturnWork()
		{
			ReturnWork rn =this.HisReturnWork;
			rn.Update(ReturnWorkAttr.ReturnWorkState, (int)ReturnWorkState.Read);
			rn.Update();
		}
		/// <summary>
		/// 执行读取回复节点
		/// </summary>
		public void DoReadReWork()
		{
			ReWork rn =this.HisReWork;
			rn.Update(ReWorkAttr.ReWorkState, (int)ReWorkState.Read);
			rn.Update();
		}
		/// <summary>
		/// 执行退回
		/// </summary>
		/// <param name="reason">退回原因</param>
		public ReturnWork DoReturn(string reason)
		{
			ReturnWork nd = new ReturnWork();
			if (nd.IsExit(WorkAttr.OID, this.OID)==false)
				nd.InsertAsOID(this.OID);

			nd.ParentID=this.ParentID;
			nd.ReturnReason=reason;
			nd.Returner=this.Executer;
			nd.ReturnActionDateTime=DataType.CurrentDataTime;
			nd.MyReturnWorkState=ReturnWorkState.UnRead; //设置未读.
			
			nd.Accepter=this.Sender; 
			nd.Update();			 
			this.Update(WorkAttr.WorkState,(int)WorkState.Returning);  // 设置当前的状态为退回。
			return nd;
		}
		/// <summary>
		/// 执行撤消退回
		/// </summary>
		public void DoReturnOfRecall()
		{
			/* 执行撤消退回 */
			ReturnWork rn = this.HisReturnWork;			 
			rn.Update(ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.CallBack  );

			this.Update(WorkAttr.WorkState,(int)WorkState.Read); //设置任务节点为已经阅读
		}
		/// <summary>
		/// 执行回复
		/// </summary>
		public void DoRe(string title,string docs)
		{
			ReWork rn = this.HisReWork;
			rn.Title=title;
			rn.Docs=docs;
			rn.ReActionDateTime=DataType.CurrentDataTime;
			rn.MyReWorkState=ReWorkState.UnRead;
			rn.Update();

			this.MyWorkState=WorkState.Re;
			//this.Sender=WebUser.No;
			this.Update();
		}
		/// <summary>
		/// 执行分配
		/// </summary>
		public void DoAllot()
		{
			this.MyWorkState=WorkState.Allot;
			this.Update();
		}
		/// <summary>
		/// 执行终止工作
		/// </summary>
		public void DoStopWork()
		{

		}

		#region 回复
		/// <summary>
		/// 执行执行
		/// </summary>
		public void DoStop()
		{
			ReWork rn =this.HisReWork;			 
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.Stop);
		}
		/// <summary>
		/// 执行不认可退回
		/// </summary>
		public void DoUnRatifyReWork()
		{
			ReWork rn =this.HisReWork;			 
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.UnRatify);
		}
		 
		/// <summary>
		/// 执行认可退回
		/// </summary>
		public void DoRatifyReWork()
		{
			ReWork rn =this.HisReWork;
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.Ratify);

			this.Update( WorkAttr.WorkState,(int)WorkState.ReOver); // 设置为已经退回状态。
		}
		/// <summary>
		/// 撤消回复
		/// </summary>
		public void DoTakeBackRe()
		{
			ReWork rn =this.HisReWork;
			rn.Update( ReWorkAttr.ReWorkState,(int)ReWorkState.None);

			this.Update( WorkAttr.WorkState,(int)WorkState.Read ); // 设置为已经退回状态。
		}

		#endregion

		#region 退回
		/// <summary>
		/// 执行不认可退回
		/// </summary>
		public void DoUnRatifyReturnWork()
		{
			ReturnWork rn =this.HisReturnWork;			 
			rn.Update( ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.UnRatify);
		}
		 
		/// <summary>
		/// 执行认可退回
		/// </summary>
		public void DoRatifyReturnWork()
		{
			ReturnWork rn =this.HisReturnWork;			 
			rn.Update( ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.Ratify);

			this.Update( WorkAttr.WorkState,(int)WorkState.ReturnOver); // 设置为已经退回状态。
		}
		#endregion

		/// <summary>
		/// 执行发送。
		/// </summary>
		public Works DoSend()
		{
			try
			{
				//首先检查接受人是否正确。
				string empsstrs= PubClass.CheckEmps(this.EmpStrs); // 整理人员字符串。
				if (this.ParentID==0)
				{
					/*如果当前人是任务的发送人 */
					this.Executer=WebUser.No;
					this.Sender=WebUser.No;
					//this.FK_Task=this.OID;
				}


				// 取出当前节点的附件
				SysFileManagers fils = this.HisSysFileManagers;
				if (fils.Count!=0)
				{
					// 删除原来发送的信息如果有。/ 
					Works nds  =new Works();
					nds.SearchByParentID(this.OID);
					foreach(Work tn in nds)
					{
						SysFileManagers fs = tn.HisSysFileManagers;
						foreach(SysFileManager f in fs)
							f.DirectDelete(); // 这种方式可以避免删除文件。
					}
				}
				 
				DBAccess.RunSQL("DELETE TA_Work WHERE  ParentID="+this.OID );

				/// 用于返回结果。
				Works tns = new Works();
				// 删除原来的附件
				string[] strs=empsstrs.Split(',');
				foreach(string str in strs)
				{
					if (str=="")
						continue;
					if (str==WebUser.No)
						continue;

					Work tn = new Work();
					tn.ParentID=this.OID; // 它的父节点是这个的节点。
					//tn.FK_Task=this.FK_Task;
					tn.MyWorkState=WorkState.UnRead;
					tn.Sender=this.Executer; // 发送人
					tn.Executer=str;
					tn.DateTimeOfTaskStart=this.DateTimeOfTaskStart;  // 子节点的接受时间是父节点的发送时间。
					tn.DateTimeOfTaskEnd=tn.DateTimeOfTaskEnd;        // 子节点的接受时间是父节点的发送时间。
					tn.Title = this.Title;
					tn.Docs=this.Docs;
					tn.Step=this.Step+1; //步骤.
					tn.Insert();

					// 给下面的工作的接受者加附件
					foreach(SysFileManager f in fils)
					{
						f.Copy();
						f.RefTable=tn.ToString();
						f.RefKey=tn.OID;
						f.Recorder=WebUser.No;
						f.Insert();
					}
					tns.AddEntity(tn); // 增加到这个集合里面去。
				}

				// 判断是否有接受人。
				if (tns.Count==0)
					throw new Exception("@您没有选择要发送的人。");

				// 设置分配状态。
				//this.MyWorkState=WorkState.Allot;
				
 
				this.Update();
				return tns; // 返回结果
			}
			catch(Exception ex)
			{
				// 如果发送出现错误后，就删除已经发送的任务。
				DBAccess.RunSQL("DELETE TA_Work WHERE  ParentID="+this.OID ); 
				throw new Exception("发送期间出现如下错误:"+ex.Message);
			}
		}
		 
		/// <summary>
		/// 逾期天数
		/// </summary>
		/// <returns></returns>
		public int TimeOutDays()
		{
			TimeSpan ts;
			DateTime dtfrom;
			switch(this.MyWorkState)
			{
				case WorkState.Allot:
				case WorkState.Read:
				case WorkState.UnRead:
					dtfrom=DateTime.Now; // 如果在已经分配，读取，未读取的状态按照当前的日期为计算日期。
					break;
				case WorkState.Re:
				case WorkState.ReOver:
				case WorkState.CallBack: // 回复终止
					dtfrom=this.HisReWork.ReActionDateTime_S; //如果在回复，回复认可后，日期按回复人活动的日期计算。
					break;
				case WorkState.Returning:
				case WorkState.ReturnOver:
					dtfrom=this.HisReWork.ReActionDateTime_S; //如果在退回，退回认可后，日期按退回人活动的日期计算。
					break;
				default:
					throw new Exception("没有涉及到的情况。");
			}

			ts=dtfrom-this.DateTimeOfTaskEnd_S;
			return ts.Days;
		}
		/// <summary>
		/// 产生考核的分
		/// </summary>
		public void GenerCheckCent()
		{
			//如果当前是第一个节点，就不考核。
			if (this.ParentID==0) 
			{
				this.CentOfCheck=0;
				return; 
			}
			// 如果没有设置考核就不考核。
			if (this.MyCheckWay==CheckWay.UnSet)
			{
				this.CentOfCheck=0;
				return; 
			}


			//Work tn =this.HisParentNode;
			switch(this.MyWorkState)
			{
				case WorkState.CallBack: // 收回了。
					if (this.MyCheckWay==CheckWay.ByDays)
						this.CentOfCheck=this.CentOfMax; /* 如果是按照天来考核。*/
					else
						this.CentOfCheck=this.CentOfOnce;
					break;
				default:					 
					if (this.MyCheckWay==CheckWay.ByDays)
					{
						int cent=this.TimeOutDays()*this.CentOfPerDay;
						if (cent>this.CentOfMax)
						{
							cent=this.CentOfMax;
						}
						this.CentOfCheck=cent; /* 如果是按照天来考核。*/
					}
					else if (this.MyCheckWay==CheckWay.ByOnce)
					{
						if (this.TimeOutDays() >=1)
						{
							/*如果延期就扣分*/
							this.CentOfCheck=this.CentOfOnce;
						}
					}
					break;
			}

			this.Update(WorkAttr.CentOfCheck, this.CentOfCheck);
		}

	}
	/// <summary>
	/// 任务节点s
	/// </summary> 
	public class Works: Entities
	{
		/// <summary>
		/// 获取entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Work();
			}
		}
		/// <summary>
		/// Works
		/// </summary>
		public Works()
		{

		}
		
		/// <summary>
		/// 集合
		/// </summary>
		/// <param name="emp">人员</param>
		/// <param name="ny">年月</param>
		public Works(string ExecuterEmp,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkAttr.DateTimeOfTaskStart, " like ", ny+"%");
			qo.addAnd();
			qo.AddWhere(WorkAttr.Executer,ExecuterEmp);
			qo.addAnd();
			qo.AddWhere(WorkAttr.WorkState,">",0);
  			qo.DoQuery();
		}
		 
		/// <summary>
		/// Works
		/// </summary>
		/// <param name="parentNodeID"></param>
		public int SearchByParentID(int parentNodeID)
		{
			if (parentNodeID==0)
				return 0 ;

			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkAttr.ParentID, parentNodeID);
			return qo.DoQuery();
		}
		 
				   
				  
				  
	}
}
 