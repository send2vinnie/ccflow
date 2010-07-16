using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.CTI.App
{
	 
	/// <summary>
	/// 呼出列表属性
	/// </summary>
	public class CallListViewAttr:EntityOIDAttr
	{
		#region 属性
		/// <summary>
		/// tel
		/// </summary>
		public const string Tel="Tel";
		/// <summary>
		/// 呼出时间从
		/// </summary>
		public const string FK_TelType="FK_TelType";
		/// <summary>
		/// 呼出时间到
		/// </summary>
		public const string FK_Context="FK_Context";
		/// <summary>
		/// 最高呼叫次数
		/// </summary>
		public const string CallTime="CallTime";
		/// <summary>
		/// 呼出状态
		/// </summary>
		public const string CallingState="CallingState";
		/// <summary>
		/// 呼出时间
		/// </summary>
		public const string CallDateTime="CallDateTime";
		/// <summary>
		/// 备注1
		/// </summary>
		public const string Note="Note";
		#endregion
	}
	/// <summary>
	/// 呼出列表
	/// </summary> 
	public class CallListView :EntityOID
	{
		#region 基本属性
		/// <summary>
		/// 电话
		/// </summary>
		public string  Tel
		{
			get
			{
				return GetValStringByKey(CallListViewAttr.Tel);
			}
			set
			{
				SetValByKey(CallListViewAttr.Tel,value);
			}
		}
		/// <summary>
		/// 电话类型
		/// </summary>
		public string  FK_TelType
		{
			get
			{
				return GetValStringByKey(CallListViewAttr.FK_TelType);
			}
			set
			{
				SetValByKey(CallListViewAttr.FK_TelType,value);
			}
		}
		/// <summary>
		/// 呼出内容
		/// </summary>
		public string  FK_Context
		{
			get
			{
				return GetValStringByKey(CallListViewAttr.FK_Context);
			}
			set
			{
				SetValByKey(CallListViewAttr.FK_Context,value);
			}
		}
		/// <summary>
		/// 呼出内容
		/// </summary>
		public string  FK_ContextText
		{
			get
			{
				return GetValRefTextByKey(CallListViewAttr.FK_Context);
			}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string  Note
		{
			get
			{
				return GetValStringByKey(CallListViewAttr.Note);
			}
			set
			{
				SetValByKey(CallListViewAttr.Note,value);
			}
		} 
		/// <summary>
		/// 呼出的时间
		/// </summary>
		public int CallTime
		{
			get
			{
				return GetValIntByKey(CallListViewAttr.CallTime);
			}
			set
			{
				SetValByKey(CallListViewAttr.CallTime,value);
			}
		}
		/// <summary>
		/// 呼出状态
		/// </summary>
		public int CallingState
		{
			get
			{
				return GetValIntByKey(CallListViewAttr.CallingState);
			}
			set
			{
				SetValByKey(CallListViewAttr.CallingState,value);
			}
		}
		/// <summary>
		/// 呼出时间
		/// </summary>
		public string  CallDateTime
		{
			get
			{
				return GetValStringByKey(CallListViewAttr.CallDateTime);
			}
			set
			{
				SetValByKey(CallListViewAttr.CallDateTime,value);
			}
		} 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 呼出
		/// </summary>
		public CallListView()
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
				Map map = new Map("CTI_CallListView");
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.Application;
				map.EnDesc="呼出列表";
				map.IsDelete=true;
				map.IsInsert=true;
				map.IsUpdate=true;
				map.IsView=true;
				map.AddTBStringPK(CallListViewAttr.Tel,null,"电话",true,false,5,50,20);
				map.AddDDLEntitiesNoName(CallListViewAttr.FK_TelType,"0","电话类型", new TelTypes(),true);
				//map.AddDDLEntitiesNoName(CallListViewAttr.FK_Context,"0","呼出内容",new CallContexts(),true);
				map.AddTBInt(CallListViewAttr.CallTime,0,"已呼出次数",true,false);
				map.AddDDLSysEnum(CallListViewAttr.CallingState,0,"呼出状态",true,false);
				map.AddTBDateTime(CallListViewAttr.CallDateTime,"呼出时间",true,false);
				map.AddTBStringDoc(CallListViewAttr.Note,null,"备注",true,false);

				map.AddSearchAttr(CallListViewAttr.FK_TelType);
				map.AddSearchAttr(CallListViewAttr.CallingState);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 条目
	/// </summary>
	public class CallListViews :EntitiesOID
	{

		#region 得到一个呼出
		/// <summary>
		/// 得到一个呼出
		/// </summary>
		/// <returns></returns>
		public static CallListView GetCall()
		{
			CallListView cl = new CallListView();
			QueryObject qo = new QueryObject(cl);
			qo.AddWhere(CallListViewAttr.CallingState,0);
			if (qo.DoQuery() >= 1)		
			{
				cl.CallDateTime=DataType.SysDataTimeFormat;
				return cl;
			}
			else
				return null;
		}
		#endregion

		#region 方法
		#endregion 

		#region 构造方法属性
		/// <summary>
		/// CallListViews
		/// </summary>
		public CallListViews(){}
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallListView();
			}
		}
		#endregion

		 
	}
}
