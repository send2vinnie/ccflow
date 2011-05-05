using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.TA
{
	/// <summary>
	/// 计划状态
	/// </summary>
	public enum TaskSta
	{
		/// <summary>
		/// 未开始
		/// </summary>
		UnRun,
		/// <summary>
		/// 进行中
		/// </summary>
		Run,
		/// <summary>
		/// 完成
		/// </summary>
		Complete,
		/// <summary>
		/// 推迟
		/// </summary>
		Suspend
	}
	/// <summary>
	/// 共享类型
	/// </summary>
	public enum Sharing
	{
		SPublic,
		SPrivate
	}
	/// <summary>
	/// 计划属性
	/// </summary>
	public class TaskAttr:EntityOIDNameAttr
	{
		/// <summary>
		/// 标题
		/// </summary>
		public const string Title="Title";
		/// <summary>
		/// 日期
		/// </summary>
		public const string TaskSta="TaskSta";
		/// <summary>
		/// PRI
		/// </summary>
		public const string PRI="PRI";
		/// <summary>
		/// 是否有开始时间
		/// </summary>
		public const string SharingType="SharingType";
		/// <summary>
		/// 是否有结束时间
		/// </summary>
		public const string IsHaveEndDate="IsHaveEndDate";
		/// <summary>
		/// 结束日期
		/// </summary>
		public const string TaskEndDate="TaskEndDate";
		/// <summary>
		/// 实际结束日期
		/// </summary>
		public const string InfactEndDate="InfactEndDate";
		/// <summary>
		/// 记录人
		/// </summary>
		public const string Recorder="Recorder"; 
		/// <summary>
		/// 记录日期
		/// </summary>
		public const string RDT="RDT"; 	
		/// <summary>
		/// 备注
		/// </summary>
		public const string Notes="Notes"; 	
		/// <summary>
		/// 任务类别
		/// </summary>
		public const string FK_TaskGroup="FK_TaskGroup";


        public const string Doc = "Doc";
	}
	/// <summary>
	/// 计划
	/// </summary> 
	public class Task : EntityOIDName
	{
		#region  属性
		public TaskSta HisTaskSta
		{
			get
			{
				return (TaskSta)this.GetValIntByKey(TaskAttr.TaskSta);
			}
			set
			{
				this.SetValByKey(TaskAttr.TaskSta,(int)value);
			}
		}
		
		public string MyTaskStateStr
		{
			get
			{
				return "<img src='./images/Task.gif' border=0 /><a href=\"javascript:OpenWork('"+this.OID+"')\" >"+this.Name+"</a>"+BP.PubClass.FilesViewStr(this.ToString(),this.OID);
			}
		}
		public string NameExt
		{
			get
			{
                switch (this.HisTaskSta)
				{
					case TaskSta.Complete:
						return "<img src='./images/Task.gif' border=0 /><a href=\"javascript:OpenWork('"+this.OID+"')\" ><strike>"+this.Name+"</strike></a>"+BP.PubClass.FilesViewStr(this.ToString(),this.OID);
					default:
						return "<img src='./images/Task.gif' border=0 /><a href=\"javascript:OpenWork('"+this.OID+"')\" >"+this.Name+"</a>"+BP.PubClass.FilesViewStr(this.ToString(),this.OID);
				}
			}
			 
		}
		public string NameExtNoImg
		{
			get
			{
				switch(this.HisTaskSta)
				{
					case TaskSta.Complete:
						return "<a ondblclick=\"javascript:WinOpen('Task.aspx?RefID="+this.OID+"')\" ><strike>"+this.Name+"</strike></a>"+BP.PubClass.FilesViewStr(this.ToString(),this.OID);
					default:
						return "<a ondblclick=\"javascript:WinOpen('Task.aspx?RefID="+this.OID+"')\" >"+this.Name+"</a>"+BP.PubClass.FilesViewStr(this.ToString(),this.OID);
				}
			}
			 
		}
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(TaskAttr.Doc);
            }
            set
            {
                SetValByKey(TaskAttr.Doc, value);
            }
        } 
		 
		public string PRI
		{
			get
			{
				return this.GetValStringByKey(TaskAttr.PRI);
			}
			set
			{
				SetValByKey(TaskAttr.PRI,value);
			}
		} 
		/// <summary>
		/// 
		/// </summary>
		public string Recorder
		{
			get
			{
				return this.GetValStringByKey(TaskAttr.Recorder);
			}
			set
			{
				SetValByKey(TaskAttr.Recorder,value);
			}
		}

        public int TaskStaInt
        {
            get
            {
                return this.GetValIntByKey(TaskAttr.TaskSta);
            }
            set
            {
                SetValByKey(TaskAttr.TaskSta, value);
            }
        }
		 
        
		public string TaskEndDate
		{
			get
			{
				return this.GetValStringByKey(TaskAttr.TaskEndDate);
			}
			set
			{
				SetValByKey(TaskAttr.TaskEndDate,value);
			}
		}
		public string InfactEndDate
		{
			get
			{
				return this.GetValStringByKey(TaskAttr.InfactEndDate);
			}
			set
			{
				SetValByKey(TaskAttr.InfactEndDate,value);
			}
		}
		public DateTime InfactEndDate_S
		{
			get
			{
				return this.GetValDateTime(TaskAttr.InfactEndDate);
			}
		}
		public bool IsHaveEndDate
		{
			get
			{
				return this.GetValBooleanByKey(TaskAttr.IsHaveEndDate);
			}
			set
			{
				this.SetValByKey(TaskAttr.IsHaveEndDate,value);
			}
		}
		public Sharing MySharingType
		{
			get
			{
				return (Sharing)this.GetValIntByKey(TaskAttr.SharingType);
			}
			set
			{
				this.SetValByKey(TaskAttr.SharingType,(int)value);
			}
		}
		public string Notes
		{
			get
			{
				return this.GetValStringByKey(TaskAttr.Notes);
			}
			set
			{
				SetValByKey(TaskAttr.Notes,value);
			}
		}


        public void SetUnComplete()
        {
            this.HisTaskSta = TaskSta.Run;
            this.Update();
        }
		public void SetComplete()
		{

            this.HisTaskSta = TaskSta.Complete;
			this.Update();

				/*如果已经完成了。*/
//				TaLog tl = new TaLog();
//			tl.Recorder=this.Recorder;
//			tl.Title =this.Name;
//			tl.Title="任务计划开始时间:"+this.StartDate+"结束时间"+this.EndDate;
//			tl.OID=this.OID;
//			tl.Save();
//			pl.Update("TaskState", (int)TaskState.Complete );

			 
		}

		#endregion 
		 
		#region 构造函数
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenAll();
				return uac;
			}
		}

		/// <summary>
		/// 计划
		/// </summary>
		public Task()
		{
		  
		}
		/// <summary>
		/// 计划
		/// </summary>
		/// <param name="_No">No</param>
		public Task(int oid):base(oid)
		{
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

				Map map = new Map("TA_Task");
				map.EnDesc="待办事项";
				map.AddTBIntPKOID();
                map.Icon = "../TA/Images/Task_s.gif";

				map.AddTBString(TaskAttr.Name,null,"标题",true,false,0,500,500);

				map.AddDDLEntities(TaskAttr.FK_TaskGroup,null,"类别",new TaskGroups(),true);
                map.AddDDLSysEnum(TaskAttr.TaskSta, (int)TaskSta.UnRun, "状态", true, true, TaskAttr.TaskSta, "@0=未开始@1=进行中@2=完成@3=推迟");
                map.AddDDLSysEnum(TaskAttr.PRI, 0, "优先级", false, true,TaskAttr.SharingType, "@0=高@1=中@2=低");
                map.AddDDLSysEnum(TaskAttr.SharingType, 0, "共享类型", false, true, TaskAttr.SharingType,"@0=共享@1=私有");
				//map.AddTBDateTime(TaskAttr.TaskStartDate,"开始日期",true,false );
				map.AddTBDateTime(TaskAttr.TaskEndDate,"结束日期",true,false );

				map.AddTBDateTime(TaskAttr.InfactEndDate,"实际结束日期",false,false );
				map.AddBoolean(TaskAttr.IsHaveEndDate,false,"结束时间",false,true);

                map.AddTBString(TaskAttr.Recorder, null, "记录人", false, false, 0, 30, 150);

                map.AddTBStringDoc();

				map.AddSearchAttr(TaskAttr.FK_TaskGroup);
				map.AddSearchAttr(TaskAttr.TaskSta);
				map.AddSearchAttr(TaskAttr.PRI);

				map.AttrsOfSearch.AddHidden(TaskAttr.Recorder,"=",Web.WebUser.No);
				this._enMap=map;
				return this._enMap;
			}
		}
		protected override bool beforeUpdateInsertAction()
		{
			this.InfactEndDate=DataType.CurrentDataTime;
			return base.beforeUpdateInsertAction ();
		}


		#endregion 

	}
	/// <summary>
	/// 计划s
	/// </summary> 
	public class Tasks: Entities
	{
		public override Entity GetNewEntity
		{
			get
			{
				return new Task();
			}
		}
		public Tasks()
		{
		}
		
		/// <summary>
		/// 集合
		/// </summary>
		/// <param name="emp">人员</param>
		/// <param name="ny">年月</param>
		public Tasks(string emp)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(TaskAttr.Recorder,emp);
			qo.addOrderBy(TaskAttr.PRI);
			qo.DoQuery();
		}
		public Tasks(string emp, string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(TaskAttr.Recorder,emp);
			qo.addAnd();
			qo.AddWhere(TaskAttr.TaskSta,(int)TaskSta.Complete );
			qo.addAnd();
			qo.AddWhere(TaskAttr.InfactEndDate, " like ", ny+"%" );
			qo.DoQuery();
		}
	}
}
 