using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Sys;
using BP.Port;

namespace BP.TA
{
 
	/// <summary>
	/// 考核方式
	/// </summary>
	public enum CheckWay
	{
		/// <summary>
		/// 没有设置
		/// </summary>
		UnSet,
		/// <summary>
		/// 一次性设定
		/// </summary>
		ByOnce,
		/// <summary>
		/// 按天设定
		/// </summary>
		ByDays
	}
	/// <summary>
	/// 工作状态
	/// </summary>
    public enum WorkState
    {
        /// <summary>
        /// 无(草稿)
        /// </summary>
        None,
        /// <summary>
        /// 分配(执行发送后)
        /// </summary>
        Runing,
        /// <summary>
        /// 完成(各个工作接受人,都已经回复了,并且已经认可.)
        /// </summary>
        Over
    }
	/// <summary>
	/// 工作节点属性
	/// </summary>
	public class WorkAttr:EntityOIDAttr
	{
        /// <summary>
        /// ToEmpStrs
        /// </summary>
        public const string ToEmpStrs = "ToEmpStrs";
        /// <summary>
        /// 发起日期
        /// </summary>
        public const string RDT = "RDT";
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
		/// 工作状态
		/// </summary>
		public const string WorkState="WorkState"; 	
		/// <summary>
		/// 优先级
		/// </summary>
		public const string PRI="PRI"; 
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
        public const string Doc = "Doc"; 
		/// <summary>
		/// ToEmps
		/// </summary>
		public const string ToEmps="ToEmps"; 	
		/// <summary>
		/// 发送人
		/// </summary>
		public const string Sender="Sender";
		/// <summary>
		/// 要求开始时间
		/// </summary>
		public const string DTOfStart="DTOfStart";
		/// <summary>
		/// 要求完成时间
		/// </summary>
		public const string DTOfEnd="DTOfEnd";

        public const string FK_WorkTemplate = "FK_WorkTemplate";


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

		#region 工作进度
		/// <summary>
		/// 执行人个数
		/// </summary>
		public const string NumOfUnRead="NumOfUnRead";
		/// <summary>
		/// 读取人个数
		/// </summary>
		public const string NumOfRead="NumOfRead";
		/// <summary>
		/// 回复中
		/// </summary>
		public const string NumOfReing="NumOfReing";
		/// <summary>
		/// 退回中
		/// </summary>
		public const string NumOfReturning="NumOfReturning";
		/// <summary>
		/// 回复完成个数
		/// </summary>
		public const string NumOfOverByRe="NumOfOverByRe";
		/// <summary>
		/// 退回完成个数
		/// </summary>
		public const string NumOfOverByReturn="NumOfOverByReturn";
		/// <summary>
		/// 附件个数
		/// </summary>
		public const string AdjunctNum="AdjunctNum";
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
	/// 工作节点
	/// </summary> 
	public class Work : EntityOID
	{
		public static int GetMyReturnWorkNum
		{
			get
			{
				string sql="SELECT COUNT(*) FROM TA_ReturnWork WHERE ReturnWorkState="+(int)ReturnWorkState.Sending+" AND Sender='"+WebUser.No+"'";
              //  throw new Exception(sql);
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		public static int GetMyReWorkNum
		{
			get
			{
				string sql="SELECT COUNT(*) FROM TA_ReWork WHERE ReWorkState="+(int)ReWorkState.Sending+" AND Sender='"+WebUser.No+"'";
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
      
        /// <summary>
        /// 等待处理的
        /// </summary>
        public static int GetMyWorkNum
        {
            get
            {
                string sql = "SELECT COUNT(*) FROM TA_WorkDtl WHERE ( WorkDtlState=" + (int)WorkDtlState.Read + " OR WorkDtlState=" + (int)WorkDtlState.UnRead + " ) AND Executer='" + WebUser.No + "'";
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }

        public static int GetMyWorkNumOfHistory
        {
            get
            {
                string sql = "SELECT COUNT(*) FROM TA_WorkDtl WHERE   Executer='" + WebUser.No + "' AND ( WorkDtlState=" + (int)WorkDtlState.OverByRe + " OR WorkDtlState=" + (int)WorkDtlState.OverByRead + "  OR WorkDtlState=" + (int)WorkDtlState.OverByReturn + "  ) ";
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }

        public static int GetMyWorkNumOfDeal
        {
            get
            {
                string sql = "SELECT COUNT(*) FROM TA_WorkDtl WHERE   Executer='" + WebUser.No + "' AND ( WorkDtlState=" + (int)WorkDtlState.Read + " OR WorkDtlState=" + (int)WorkDtlState.UnRead + " ) ";
                return DBAccess.RunSQLReturnValInt(sql);
            }
        }

		/// <summary>
		/// 产生一个草稿工作
		/// </summary>
		/// <returns></returns>
		public static Work GenerDraftWork()
		{
			Work tn = new Work();
			if (tn.IsExit(WorkAttr.WorkState, (int)WorkState.None, WorkAttr.Sender,WebUser.No)==false)
			{
				tn.WorkState=WorkState.None;
				tn.Sender=WebUser.No;
				tn.Insert();
			}
			else
			{
				tn.RetrieveByAttrAnd(WorkAttr.WorkState, (int)WorkState.None, WorkAttr.Sender,WebUser.No);
			}
			return tn;
		}
		/// <summary>
		/// 产生一个草稿工作
		/// </summary>
		/// <returns></returns>
		public static Work GenerParentWork(int dtlWorkId)
		{
			Work tn = new Work();
			WorkDtl dtl = new WorkDtl(dtlWorkId);
			if (tn.IsExit(WorkAttr.ParentID, dtl.ParentID )==false)
			{
				Work wk = dtl.HisWork;
				tn.WorkState=WorkState.None;
				tn.Sender=WebUser.No;
				tn.Title="FW:"+wk.Title;
				tn.Doc="\n\n\n---- 以下是上一节点的工作内容 -----\n"+wk.Doc;
				tn.ParentID=wk.OID;
				tn.Step=wk.Step+1;
				tn.Insert();
			}
			else
			{
				tn.RetrieveByAttr(WorkAttr.ParentID, dtlWorkId );
			}
			return tn;
		}

		#region 扩展 属性
		#endregion

		#region 基本属性。
		/// <summary>
		/// 是否是结束节点
		/// </summary>
		public bool IsEndNode
		{
			get
			{
				return false;
			}
		}
		/// <summary>
		/// 是否是开始节点
		/// </summary>
		public bool IsStartNode
		{
			get
			{
				if (this.ParentID==0)
					return true;
				else
					return false;
			}
		}
		/// <summary>
		/// 是否是中间节点
		/// </summary>
		public bool IsMiddle
		{
			get
			{
				if (this.IsStartNode)
					return false;
				if (this.IsEndNode)
					return false;

				return true;
			}
		}
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
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(WorkAttr.RDT);
            }
            set
            {
                SetValByKey(WorkAttr.RDT, value);
            }
        }
		public string IsReText
		{
			get
			{
				if (this.IsRe)
					return "是";
				else
					return "否";
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

        public string FK_WorkTemplate
        {
            get
            {
                return this.GetValStringByKey(WorkAttr.FK_WorkTemplate);
            }
            set
            {
                SetValByKey(WorkAttr.FK_WorkTemplate, value);
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
		public string DocHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(WorkAttr.Doc);
			}
			
		}
		public string Doc
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.Doc);
			}
			set
			{
				SetValByKey(WorkAttr.Doc,value);
			}
		}
        public string ToEmpStrs
        {
            get
            {
                return this.GetValStringByKey(WorkAttr.ToEmpStrs);
            }
            set
            {
                SetValByKey(WorkAttr.ToEmpStrs, value);
            }
        }
		/// <summary>
		/// Emps
		/// </summary>
		public string ToEmps
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.ToEmps );
			}
			set
			{
				string strs=value;
				if (strs.Substring(0,1)==",")
					strs=strs.Substring(1);

				SetValByKey(WorkAttr.ToEmps,strs);
                this.ToEmpStrs = Web.WebUser.Tag;
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
		/// 附件个数
		/// </summary>
		public int AdjunctNum 
		{
			get
			{
				return this.GetValIntByKey(WorkAttr.AdjunctNum);
			}
			set
			{
				SetValByKey(WorkAttr.AdjunctNum,value);
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
//		public string DTOfAccept_del
//		{
//			get
//			{
//				return this.GetValStringByKey(WorkAttr.DTOfAccept);
//			}
//			set
//			{
//				SetValByKey(WorkAttr.DTOfAccept,value);
//			}
//		}
//		public string DateOfAccept
//		{
//			get
//			{
//				return DataType.ParseSysDateTime2SysDate(this.DTOfAccept);
//			}
//		}
//		 
//		/// <summary>
//		/// DTOfAccept_S
//		/// </summary>
//		public DateTime DTOfAccept_S
//		{
//			get
//			{
//				return DataType.ParseSysDate2DateTime(this.DTOfAccept);
//			}
//		}
		/// <summary>
		/// 操作
		/// </summary>
		public string WorkStateOpStr
		{
			get
			{
				return null;
				//				if (WebUser.No==this.Sender && this.ParentID==0)
				//				{
				//					/* 如果我是此工作的发起人 */					  
				//					return this.WorkStateTextImg+"<a href=\"javascript:TrackTree('"+this.OID+"','"+(int)BP.TA.ActionType.ViewNode+"')\" >"+ this.Title +"</a>"+BP.Web.Comm.UC.UCSys.FilesViewStr(this.ToString(),this.OID)+"分配给"+this.ToEmps;
				//				}
				//
				//				string title=this.Title;
				//				switch(this.WorkState)
				//				{
				//					case WorkState.ReOver: //如果已经认可
				//					case WorkState.Re: //如果已经认可
				//					case WorkState.Returning: //如果已经认可
				//					case WorkState.ReturnOver: //如果已经认可
				//					case WorkState.CallBack: //如果已经认可
				//						//title="<strike>"+title+"</strike>";
				//						break;
				//					default:
				//						break;
				//				}
				//				return this.WorkStateTextImg+"<b>分配人:</b>"+this.SenderText+"<b>执行人:</b>"+this.ExecuterText+"<a href=\"javascript:Task('"+this.OID+"','"+(int)BP.TA.ActionType.ViewNode+"')\" >"+ title +"</a>"+BP.Web.Comm.UC.UCSys.FilesViewStr(this.ToString(),this.OID);
			}
		}
		/// <summary>
		/// 节点状态
		/// </summary>
		public WorkState WorkState
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
		public string WorkStateImg
		{
			get
			{
                return "./Images/" + this.WorkState.ToString() + ".gif";
			}
		}
		public string WorkStateImgExt
		{
			get
			{
				return  "<img src='"+this.WorkStateImg+"' border=0 />"+this.WorkStateText;
			}
		}
		/// <summary>
		/// 节点状态 标题
		/// </summary>
		public string WorkStateText
		{
			get
			{
				return  GetValRefTextByKey(WorkAttr.WorkState);
			}
		}
		/// <summary>
		/// 任务开始时间
		/// </summary>
		public string DTOfStart 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.DTOfStart);
			}
			set
			{
				SetValByKey(WorkAttr.DTOfStart,value);
                SetValByKey(WorkAttr.RDT, DataType.CurrentDataTime);
			}
		}
		/// <summary>
		/// 任务开始时间
		/// </summary>
		public DateTime DTOfStart_S
		{
			get
			{
				return DataType.ParseSysDateTime2DateTime(this.DTOfStart);
			}
		}
		/// <summary>
		/// 任务 结束 时间
		/// </summary>
		public string DTOfEnd 
		{
			get
			{
				return this.GetValStringByKey(WorkAttr.DTOfEnd);
			}
			set
			{
				SetValByKey(WorkAttr.DTOfEnd,value);
			}
		}
		public DateTime DTOfEnd_S
		{
			get
			{
				return DataType.ParseSysDateTime2DateTime(this.DTOfEnd);
			}
		}
		/// <summary>
		/// 逾期的时间段.
		/// </summary>
		public TimeSpan DateTimeOfOverTime
		{
			get
			{
				TimeSpan ts=this.DTOfStart_S - this.DTOfEnd_S;
				return ts;
			}
		}
		#endregion

		#region 考核属性
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
				return this.MyCheckWayText;
                //switch (this.MyCheckWay)
                //{
                //    case CheckWay.UnSet:
                //        return this.MyCheckWayText;
                //    case CheckWay.ByDays:
                //    case CheckWay.ByOnce:
                //        return " <a href=\"javascript:WinOpen('WorkOption.aspx?RefOID=" + this.OID + "','ParentID')\" >" + this.MyCheckWayText + "</a>";
                //    default:
                //        break;
                //}
                //return null;
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
 				return base.HisUAC;
			}
		}
		/// <summary>
		/// 工作节点
		/// </summary>
		public Work()
		{
		  
		}
		/// <summary>
		/// 工作节点
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
				map.EnDesc="工作节点";
                map.Icon = "./Images/Work_s.gif";

				map.AddTBIntPKOID();
				map.AddTBInt(WorkAttr.ParentID,0,"父节点ID",true,true);
				map.AddTBInt(WorkAttr.Step,1,"步骤",true,true);
				map.AddBoolean(WorkAttr.IsRe,false,"是否阅读回执",true,false);

                map.AddDDLSysEnum(WorkAttr.WorkState, (int)WorkState.None, "发送人节点状态", true, true, "WorkState", "@0=无(草稿)@1=分配@2=完成");
                map.AddDDLSysEnum(TaskAttr.PRI, 0, "优先级", false, true, TaskAttr.SharingType, "@0=高@1=中@2=低");

                map.AddTBString(WorkAttr.ToEmps, null, "接受人", true, false, 0, 3000, 15);
                map.AddTBString(WorkAttr.ToEmpStrs, null, "ToEmpStrs", true, false, 0, 3000, 15);

				map.AddTBString(WorkAttr.Title,null,"任务标题",true,false,0,500,15);

				map.AddTBString(WorkAttr.Doc,null,"任务内容",true,false,0,4000,15);

                map.AddTBDateTime(WorkAttr.RDT, "发起日期", false, false);


				map.AddDDLEntities(WorkAttr.Sender,null,"任务下达人", new Emps(),false );
				map.AddTBDateTime(WorkAttr.DTOfStart,"任务开始时间",false,false );
				map.AddTBDateTime(WorkAttr.DTOfEnd,"结束时间",false,false );

                map.AddDDLSysEnum(WorkAttr.CheckWay, (int)CheckWay.UnSet, "考核方式", true, true, TaskAttr.SharingType, "@0=未设置@1=按天@2=按次");
				map.AddTBInt(WorkAttr.CentOfOnce,5,"一次性扣分",true,true);
				map.AddTBInt(WorkAttr.CentOfPerDay,2,"扣分尺度",true,true);
				map.AddTBInt(WorkAttr.CentOfMax,10,"最高扣分",true,true);
				map.AddTBInt(WorkAttr.CentOfCheck,10,"考核分",true,true);

                map.AddDDLSysEnum(WorkAttr.CycleWay, (int)CycleWay.UnSet, "循环方式", true, true, "CycleWay", "@0=未设置@1=按周@2=按月@3=按年");
				map.AddTBString(WorkAttr.CycleWeek,"1","Weeks",true,false,0,50,15);
				map.AddTBString(WorkAttr.CycleDay,"1","Days",true,false,0,50,15);
				map.AddTBString(WorkAttr.CycleMonth,"1","Months",true,false,0,50,15);
				map.AddTBString(WorkAttr.CycleYearDay,"1","Days",true,false,0,50,15);


				map.AddTBInt(WorkAttr.AdjunctNum,0,"附件个数",true,true);

				// 工作进度
				map.AddTBInt(WorkAttr.CentOfMax,10,"最高扣分",true,true);
                map.AddTBString(WorkAttr.FK_WorkTemplate, null, "模板ID", true, false, 0, 500, 15);

              //  map.AddDDLEntities(WorkAttr.FK_WorkTemplate, "99", "模板ID", new WorkTemplates(), false);
                

				//map.AddSearchAttr(WorkAttr.CheckWay);
				//map.AttrsOfSearch.AddHidden(WorkAttr.Sender,"=",Web.WebUser.No);
				//map.AttrsOfSearch.AddFromTo("开始日期",WorkAttr.DTOfStart,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			//			if (this.ParentID==0)
			//				this.DTOfAccept=this.DateOfTaskStart;
			return base.beforeUpdateInsertAction ();
		}
		#endregion 

		#region 工作进度统计
		/// <summary>
		/// 没有阅读的工作个数
		/// </summary>
		public int NumOfUnRead
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.UnRead);
			}
		}
		public int NumOfRead
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.Read);
			}
		}
		public int NumOfReing
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.DoReing);
			}
		}
		public int NumOfReturning
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.DoReturning);
			}
		}
		public int NumOfOverByRe
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByRe);
			}
		}
		public int NumOfOverByRead
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByRead);
			}
		}
		public int NumOfOverByReturn
		{
			get
			{
				return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByReturn);
			}
		}
		public int NumOfComplete
		{
			get
			{
				return this.NumOfOverByRe+this.NumOfOverByReturn;
				//return this.HisWorkDtls.GetCountByKey(WorkDtlAttr.WorkDtlState,(int)WorkDtlState.OverByReturn);
			}
		}
		/// <summary>
		/// 任务完成率。
		/// </summary>
		public string RateOfComplete
		{
			get
			{
				if (this.HisWorkDtls.Count==0)
					return "0";

				int num=this.NumOfOverByRe+this.NumOfOverByReturn+this.NumOfOverByRead;
				decimal rate = decimal.Parse(num.ToString())  / decimal.Parse( this.HisWorkDtls.Count.ToString());
				return rate.ToString("0.00%");
			}
		}
		#endregion



		private WorkDtls _WorkDtls=null;
		public WorkDtls HisWorkDtls
		{
			get
			{
				if (_WorkDtls==null)
				{
					WorkDtls dtls = new WorkDtls();
					dtls.Retrieve(WorkDtlAttr.ParentID, this.OID);
					_WorkDtls= dtls;
				}
				return _WorkDtls;
			}
		}
		/// <summary>
		/// 它的节点。
		/// </summary>
		public Work HisParentWork
		{
			get
			{
				return new Work(this.ParentID);
			}
		}
		public Works HisCharedWork
		{
			get
			{
				Works wks = new Works();
				QueryObject qo = new QueryObject(wks);
				qo.AddWhere(WorkAttr.ParentID,this.OID);
				qo.DoQuery();
				return wks;
			}
		}
		/// <summary>
		/// 在每一个分配工作执行完毕后就调用它。
		/// 调用他主要的目的是，判断整体工作是否结束的问题。
		/// </summary>
        public void DoSettleAccounts()
        {
            WorkDtls dtls = new WorkDtls();
            QueryObject qo = new QueryObject(dtls);
            qo.AddWhere(WorkDtlAttr.ParentID, this.OID);
            qo.DoQuery();


            foreach (WorkDtl dtl in dtls)
            {
                switch (dtl.WorkDtlState)
                {
                    case WorkDtlState.UnRead:
                    case WorkDtlState.Read:
                    case WorkDtlState.DoReing:
                    case WorkDtlState.DoReturning:
                        return;
                    default:
                        break;
                }
            }

            this.WorkState = WorkState.Over;
            this.Update();
            return;
        }
		/// <summary>
		/// 执行终止工作
		/// </summary>
		public void DoStopWork()
		{

		}
		/// <summary>
		/// 执行发送。
		/// </summary>
		public WorkDtls DoSend()
		{
			//判断一下是否可以分配工作。
			WorkDtls dtls = new WorkDtls();
			dtls.Retrieve(WorkDtlAttr.ParentID,this.OID, WorkDtlAttr.WorkDtlState, (int)WorkDtlState.Read);
			if (dtls.Count > 0)
				throw new Exception("您以前分配的工作已经开始启动，您不能在分配工作。");

            this.RDT = DataType.CurrentDataTime;

			dtls.Clear();
			try
			{

				//首先检查接受人是否正确。
				string empsstrs= PubClass.CheckEmps(this.ToEmps); // 整理人员字符串。
                this.ToEmpStrs = BP.Web.WebUser.Tag;

				if (this.ParentID==0)
				{
					/*如果当前人是任务的发送人 */
					this.Sender=WebUser.No;
				}
				 
				// 删除以前分配的工作。
				DBAccess.RunSQL("DELETE FROM TA_WorkDtl WHERE  ParentID="+this.OID );

				/// 用于返回结果。
				// 删除原来的附件
				string[] strs=empsstrs.Split(',');
				foreach(string str in strs)
				{
					if (str=="")
						continue;
					if (str==WebUser.No)
						continue;

					WorkDtl dtl = new WorkDtl();
					dtl.ParentID=this.OID; // 它的父节点是这个的节点。
					dtl.WorkDtlState=WorkDtlState.UnRead;
					dtl.Executer=str;
					dtl.DTOfActive=DataType.CurrentDataTime;  // 子节点的接受时间是父节点的发送时间。
                    dtl.DTOfSend = DataType.CurrentDataTime;  // 子节点的接受时间是父节点的发送时间。
					dtl.Insert();
					//dtl.Retrieve();
					dtls.AddEntity(dtl); // 增加到这个集合里面去。
				}

				// 判断是否有接受人。
				if (dtls.Count==0)
					throw new Exception("@您没有选择要发送的人。");

				// 设置分配状态。
				this.WorkState=WorkState.Runing;
				this.AdjunctNum = this.HisSysFileManagers.Count;
				this.Update();
				return dtls; // 返回结果
			}
			catch(Exception ex)
			{
				// 如果发送出现错误后，就删除已经发送的任务。
				DBAccess.RunSQL("DELETE TA_WorkDtl WHERE  ParentID="+this.OID ); 
				throw new Exception("发送期间出现如下错误:"+ex.Message);
			}
		}
		 
		#region 关于考核
		/// <summary>
		/// 逾期天数
		/// </summary>
		/// <returns></returns>
		public TimeSpan TimeOutDays()
		{
			TimeSpan ts =DateTime.Now - this.DTOfEnd_S;
			return ts;
		}
		/// <summary>
		/// 执行重新设定
		/// </summary>
		public void DoResetWorkState()
		{
			bool isEnd=true;
			WorkDtls dtls = this.HisWorkDtls;
			foreach(WorkDtl dtl in dtls)
			{
				if (dtl.WDS==WDS.Checking || dtl.WDS == WDS.UnComplete )
				{
					isEnd=false;
					break; /* 如果有一个没有完成的，这个人分配的工作都没有完成。*/
				}
			}

			if (isEnd)
				if (this.WorkState == BP.TA.WorkState.Over)
					this.Update();
		}
		#endregion


	}
	/// <summary>
	/// 工作节点s
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
		public Works(string userNo,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkAttr.DTOfStart, " like ", ny+"%" );
			qo.addAnd();
			qo.AddWhere(WorkAttr.Sender, userNo );
//			qo.addAnd();
//			qo.AddWhere(WorkAttr.WorkState, "<>" , 0 );

			qo.DoQuery();
		}
			 
	}
}
 