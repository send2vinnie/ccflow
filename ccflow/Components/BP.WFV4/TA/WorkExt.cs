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
	/// 任务节点属性
	/// </summary>
	public class WorkExtAttr:WorkAttr
	{
		/// <summary>
		/// 最高扣分
		/// </summary>
		public const string FK_ZSJG="FK_ZSJG";
		/// <summary>
		/// FK_Dept
		/// </summary>
		public const string FK_Dept="FK_Dept";
		/// <summary>
		/// FK_Station
		/// </summary>
		public const string FK_Station="FK_Station";
		/// <summary>
		/// FK_NY
		/// </summary>
		public const string FK_NY="FK_NY";
		/// <summary>
		/// FK_JD
		/// </summary>
		public const string FK_JD="FK_JD";
        public const string DateTimeOfAccept = "DateTimeOfAccept";
        public const string Executer = "Executer";
        public const string DateTimeOfTaskEnd = "DateTimeOfTaskEnd";
        public const string DateTimeOfTaskStart = "DateTimeOfTaskStart";

        
	}
	/// <summary>
	/// 任务节点
	/// </summary> 
	public class WorkExt : Work
	{
		 
		 
		#region 构造函数
		
		 
		/// <summary>
		/// 任务节点
		/// </summary>
		public WorkExt()
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

				Map map = new Map("V_TA_Work");
				map.EnDesc="任务节点";
				map.AddTBIntPKOID();
				//map.AddTBInt(WorkExtAttr.ParentID,0,"父节点ID",true,true);
				//map.AddDDLEntities(WorkExtAttr.FK_Task,0,DataType.AppInt,"任务",new Tasks(),TaskAttr.TaskID, TaskAttr.Title,false);
				//map.AddTBInt(WorkExtAttr.Step,1,"步骤",true,true);
				//map.AddBoolean(WorkExtAttr.IsRe,false,"阅读回执",true,false);
				map.AddDDLSysEnum(WorkExtAttr.WorkState, (int) WorkState.None ,"节点状态",true,true);
				map.AddDDLSysEnum(WorkExtAttr.PRI,0,"优先级",true,true);
				//map.AddTBString(WorkExtAttr.Emps,null,"接受人",true,false,0,4000,15);
				map.AddTBString(WorkExtAttr.Title,null,"任务标题",true,false,0,500,15);	
				//map.AddTBString(WorkExtAttr.Docs,null,"任务内容",true,false,0,4000,15);
				map.AddDDLEntities(WorkExtAttr.Sender,null,"任务下达人", new Emps(),false );
				map.AddTBDateTime(WorkExtAttr.DateTimeOfAccept,"任务下达时间",false,false );

                map.AddDDLEntities(WorkExtAttr.Executer, null, "执行人", new Emps(), false);
				map.AddDDLEntities(WorkExtAttr.FK_Dept,null,"执行人部门", new BP.Port.Depts(),false );
			//	map.AddDDLEntities(WorkExtAttr.FK_ZSJG,null,"执行人管理机关", new BP.Tax.ZSJGs(),false );
				map.AddDDLEntities(WorkExtAttr.FK_Station,null,"执行人岗位", new BP.Port.Stations(),false );

				map.AddDDLEntities(WorkExtAttr.FK_JD,null,"季度", new BP.Pub.APs(),false );
				map.AddDDLEntities(WorkExtAttr.FK_NY,null,"年月", new BP.Pub.NYs(),false );


				map.AddTBDateTime(WorkExtAttr.DateTimeOfTaskStart,"任务开始时间",false,false );
				map.AddTBDateTime(WorkExtAttr.DateTimeOfTaskEnd,"任务结束时间",false,false );


				map.AddDDLSysEnum(WorkExtAttr.CheckWay,(int)CheckWay.UnSet,"考核方式",true,true);
				map.AddTBInt(WorkExtAttr.CentOfCheck,10,"考核分",true,true);

			 
				map.AddSearchAttr(WorkExtAttr.CheckWay);
				map.AddSearchAttr(WorkExtAttr.FK_Station);
				map.AddSearchAttr(WorkExtAttr.FK_Station);


				//map.AttrsOfSearch.AddHidden(WorkExtAttr.Sender,"=",Web.WebUser.No);
				map.AttrsOfSearch.AddFromTo("开始日期",WorkExtAttr.DateTimeOfTaskStart,DateTime.Now.AddDays(-30).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);

 
 
				this._enMap=map;
				return this._enMap;
			}
		}
		 

		#endregion 

	}
	/// <summary>
	/// 任务节点s
	/// </summary> 
	public class WorkExts: Works
	{
		/// <summary>
		/// 获取entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkExt();
			}
		}
		/// <summary>
		/// WorkExts
		/// </summary>
		public WorkExts()
		{

		}
	 
				  
				  
	}
}
 