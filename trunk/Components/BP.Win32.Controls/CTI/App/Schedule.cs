using System;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.CTI.App
{
	/// <summary>
	/// 时间表属性
	/// </summary>
	public class ScheduleAttr
	{
		#region 属性
		/// <summary>
		/// 月份
		/// </summary>
		public const string FK_YF="FK_YF";
		/// <summary>
		/// 内容
		/// </summary>
		public const string FK_Context="FK_Context";

		/// <summary>
		/// 开始日期
		/// </summary>
		public const string DateFrom="DateFrom";
		/// <summary>
		/// 平衡日期
		/// </summary>
		public const string DateTo="DateTo";	 
		/// <summary>
		/// 小时
		/// </summary>
		public const string RunHH="RunHH";
		/// <summary>
		/// 分钟
		/// </summary>
		public const string Runmm="Runmm";

		/// <summary>
		/// 备注
		/// </summary>
		public const string Note="Note";

		#endregion
	}
	/// <summary>
	/// 时间表
	/// </summary> 
	public class Schedule :EntityOIDName
	{
		#region 基本属性
		public BP.CTI.App.CallContext HisCallContext
		{
			get
			{
				return new CallContext(this.FK_Context);
			}
		}
		public string  FK_Context
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.FK_Context);
			}
			set
			{
				SetValByKey(ScheduleAttr.FK_Context,value);
			}
		} 
		public string  FK_YF
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.FK_YF);
			}
			set
			{
				SetValByKey(ScheduleAttr.FK_YF,value);
			}
		} 
		 
		public string  DateTo
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.DateTo);
			}
			set
			{
				SetValByKey(ScheduleAttr.DateTo,value);
			}
		}
		public string  DateFrom
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.DateFrom);
			}
			set
			{
				SetValByKey(ScheduleAttr.DateFrom,value);
			}
		}

		public string  RunHH
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.RunHH);
			}
			set
			{
				SetValByKey(ScheduleAttr.RunHH,value);
			}
		}
		public string  Runmm
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.Runmm);
			}
			set
			{
				SetValByKey(ScheduleAttr.Runmm,value);
			}
		}
		public string  Note
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.Note);
			}
			set
			{
				SetValByKey(ScheduleAttr.Note,value);
			}
		} 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 时间表
		/// </summary>
		public Schedule(){}
		public Schedule(string day){}

		 
		#endregion 

		#region Map
		/// <summary>
		/// EnMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("CTI_Schedule");	
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.None;
				//				map.CodeStruct="3";
				map.EnDesc="时间表";                
				 

				map.AddTBIntPKOID();
				map.AddDDLEntitiesNoName(ScheduleAttr.FK_YF, DataType.CurrentMonth,"月份", new Pub.YFs(),true);
				map.AddDDLEntitiesNoName(ScheduleAttr.FK_Context,"00","呼叫内容", new CallContexts(),true);
				map.AddTBString(ScheduleAttr.DateFrom,"10","日期从",true,false,2,2,2);
				map.AddTBString(ScheduleAttr.DateTo,"20","到",true,false,2,2,2);				 
				map.AddTBString(CallContextAttr.Name,null,"备注",true,false,0,50,20);

				//map.AddTBStringDoc(ScheduleAttr.Note,null,"备注",true,false);

				map.AddSearchAttr(CallContextAttr.FK_YF);				

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 条目
	/// </summary>
	public class Schedules :EntitiesOIDName
	{
		public static Schedule CurrentSchedule
		{
			get
			{
				Schedules ens = new Schedules();
				QueryObject qo = new QueryObject(ens);
				qo.AddWhere(ScheduleAttr.DateFrom, "<=" ,DateTime.Now.Day);
				qo.addAnd();
				qo.AddWhere(ScheduleAttr.DateTo, ">=" ,DateTime.Now.Day);
				qo.addAnd();
				qo.AddWhere(CallContextAttr.FK_YF, "=" ,DataType.CurrentMonth);
				int i= qo.DoQuery();

				if (i==0)
					return null;
				else
					return (Schedule)ens[0];
			}
		}

		#region 构造方法属性
		/// <summary>
		/// Schedules
		/// </summary>
		public Schedules(){}
		
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Schedule();
			}
		}
		#endregion
	}
}
