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
	public class CallListOfUserAttr:EntityOIDAttr
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
	public class CallListOfUser :EntityOID
	{
		#region 基本属性
		/// <summary>
		/// 电话
		/// </summary>
		public string  Tel
		{
			get
			{
				return GetValStringByKey(CallListOfUserAttr.Tel);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.Tel,value);
			}
		}
		/// <summary>
		/// 电话类型
		/// </summary>
		public string  FK_TelType
		{
			get
			{
				return GetValStringByKey(CallListOfUserAttr.FK_TelType);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.FK_TelType,value);
			}
		}
		/// <summary>
		/// 呼出内容
		/// </summary>
		public string  FK_Context
		{
			get
			{
				return GetValStringByKey(CallListOfUserAttr.FK_Context);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.FK_Context,value);
			}
		}
		/// <summary>
		/// 呼出内容
		/// </summary>
		public string  FK_ContextText
		{
			get
			{
				return GetValRefTextByKey(CallListOfUserAttr.FK_Context);
			}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string  Note
		{
			get
			{
				return GetValStringByKey(CallListOfUserAttr.Note);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.Note,value);
			}
		} 
		/// <summary>
		/// 呼出的时间
		/// </summary>
		public int CallTime
		{
			get
			{
				return GetValIntByKey(CallListOfUserAttr.CallTime);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.CallTime,value);
			}
		}
		/// <summary>
		/// 呼出状态
		/// </summary>
		public int CallingState
		{
			get
			{
				return GetValIntByKey(CallListOfUserAttr.CallingState);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.CallingState,value);
			}
		}
		/// <summary>
		/// 呼出时间
		/// </summary>
		public string  CallDateTime
		{
			get
			{
				return GetValStringByKey(CallListOfUserAttr.CallDateTime);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.CallDateTime,value);
			}
		} 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 呼出
		/// </summary>
		public CallListOfUser()
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
				Map map = new Map("CTI_CallListOfUser");
				//map.DepositaryOfMap=Depositary.Application;
				//map.DepositaryOfEntity=Depositary.Application;
				map.EnDesc="呼出列表";
				map.IsDelete=true;
				map.IsInsert=true;
				map.IsUpdate=true;
				map.IsView=true;
				map.AddTBStringPK(CallListOfUserAttr.Tel,null,"电话",true,false,5,50,20);
				map.AddDDLEntitiesNoName(CallListOfUserAttr.FK_TelType,"1","电话类型",new TelTypes(),true);
				map.AddDDLEntitiesNoName(CallListOfUserAttr.FK_Context,"1","呼出内容",new CallContexts(),true);
				//map.AddTBInt(CallListOfUserAttr.CallTime,0,"已呼出次数",true,true);
				//map.AddDDLSysEnum(CallListOfUserAttr.CallingState,0,"呼出状态",true,true);
				//map.AddTBDateTime(CallListOfUserAttr.CallDateTime,"呼出时间",true,true);
				//map.AddTBStringDoc(CallListOfUserAttr.Note,null,"备注",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 条目
	/// </summary>
	public class CallListOfUsers :EntitiesOID
	{

		#region 得到一个呼出
		/// <summary>
		/// 得到一个呼出
		/// </summary>
		/// <returns></returns>
		public static CallListOfUser GetCall()
		{
			CallListOfUser cl = new CallListOfUser();
			QueryObject qo = new QueryObject(cl);
			qo.AddWhere(CallListOfUserAttr.CallingState,0);
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
		/// CallListOfUsers
		/// </summary>
		public CallListOfUsers(){}
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallListOfUser();
			}
		}
		#endregion
	}
}
