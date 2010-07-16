using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.CTI.App
{
	/// <summary>
	/// 呼叫内容属性
	/// </summary>
	public class CallContextAttr:EntityNoNameAttr
	{
		#region 属性
		public const string FK_YF="FK_YF";
		public const string FK_DTS="FK_DTS";
		/// <summary>
		/// 适合用户类型
		/// </summary>
		public const string FK_TelTypeOfFit="FK_TelTypeOfFit";		 
		/// <summary>
		///  Context
		/// </summary>
		public const string Context="Context";
		/// <summary>
		/// DTSDay
		/// </summary>
		public const string DTSDay="DTSDay";
		/// <summary>
		/// 调度时
		/// </summary>
		public const string DTSHH="DTSHH";
		/// <summary>
		/// 调度分
		/// </summary>
		public const string DTSmm="DTSmm";

		/// <summary>
		/// Note
		/// </summary>
		public const string Note="Note";



		#endregion
	}
	/// <summary>
	/// 呼叫内容
	/// </summary> 
	public class CallContext :EntityNoName
	{
		 
		public BP.DTS.SysDTS HisDTS
		{
			get
			{
				return new BP.DTS.SysDTS(this.Context);
			}
		}
		 

		#region 
		protected override bool beforeUpdateInsertAction()
		{
			//if (this.No=="1" || this.No=="2")
			///	throw new Exception("编号=1 ,与编号=2的是保留呼出内容有程序表自动完成更新。");
			return base.beforeUpdateInsertAction ();
		}

		#endregion

		#region 基本属性
		 
		public string  Context
		{
			get
			{
				return GetValStringByKey(CallContextAttr.Context);
			}
			set
			{
				SetValByKey(CallContextAttr.Context,value);
			}
		} 
		public string  FK_YF
		{
			get
			{
				return GetValStringByKey(CallContextAttr.FK_YF);
			}
			set
			{
				SetValByKey(CallContextAttr.FK_YF,value);
			}
		} 
		public string  FK_DTS
		{
			get
			{
				return GetValStringByKey(CallContextAttr.FK_DTS);
			}
			set
			{
				SetValByKey(CallContextAttr.FK_DTS,value);
			}
		} 
		public string  DTSDay
		{
			get
			{
				return GetValStringByKey(CallContextAttr.DTSDay);
			}
			set
			{
				SetValByKey(CallContextAttr.DTSDay,value);
			}
		} 

		public string  DTSHH
		{
			get
			{
				return GetValStringByKey(CallContextAttr.DTSHH);
			}
			set
			{
				SetValByKey(CallContextAttr.DTSHH,value);
			}
		} 
		public string  DTSmm
		{
			get
			{
				return GetValStringByKey(CallContextAttr.DTSmm);
			}
			set
			{
				SetValByKey(CallContextAttr.DTSmm,value);
			}
		} 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 呼叫内容
		/// </summary>
		public CallContext()
		{
		}
		public CallContext(string no):base(no)
		{
		}
		 
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
				Map map = new Map("CTI_Context");	
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.None;
				map.CodeStruct="1";
				map.EnDesc="呼出内容设置";
			 

				map.AddTBStringPK(CallContextAttr.No,null,"编号",true,false,0,50,20);				 
				map.AddTBString(CallContextAttr.Name,null,"名称",true,false,0,50,20);			 
				map.AddTBString(CallContextAttr.Context,null,"呼叫内容",true,false,0,50,20);
				map.AddTBStringDoc(CallContextAttr.Note,null,"内容描述",true,false);
				map.AddDDLEntities(CallContextAttr.FK_TelTypeOfFit, "0" ,DataType.AppInt,"适合用户类型", new BP.CTI.App.TelTypeOfFits(),TelTypeOfFitAttr.OID, TelTypeOfFitAttr.Name,true);
				map.AddDDLEntitiesNoName(CallContextAttr.FK_DTS, "000" ,"执行的调度", new BP.DTS.SysDTSs(),true);
				map.AddTBString(CallContextAttr.DTSDay,"00","调度每天",true,false,0,50,20);
				map.AddTBString(CallContextAttr.DTSHH,"00","调度每时",true,false,0,50,20);
				map.AddTBString(CallContextAttr.DTSmm,"00","调度每分",true,false,0,50,20);

				//map.AddSearchAttr(CallContextAttr.FK_YF);				
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		protected override void afterInsertUpdateAction()
		{
			
			base.afterInsertUpdateAction ();
		}

	}
	/// <summary>
	/// 呼叫内容s
	/// </summary>
	public class CallContexts :EntitiesNoName
	{
		

		#region 构造方法属性
		/// <summary>
		/// CallContexts
		/// </summary>
		public CallContexts()
		{
		}
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallContext();
			}
		}
		 
		public static void SetDTS(CallContext cc)
		{
			BP.DTS.SysDTS dts =new BP.DTS.SysDTS(cc.FK_DTS);
			dts.EveryDay=cc.DTSDay;
			dts.EveryHour=cc.DTSHH;
			dts.EveryMinute=cc.DTSmm;
			dts.Update();
		}
		#endregion
	}
}
