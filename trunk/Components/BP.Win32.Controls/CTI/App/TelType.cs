using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.CTI.App
{
	 
	/// <summary>
	/// 电话类型属性
	/// </summary>
	public class TelTypeAttr:EntityOIDNameAttr
	{
		#region 属性 
		/// <summary>
		/// 呼出时间从
		/// </summary>
		public const string FromTime0="FromTime0";
		/// <summary>
		/// 呼出时间到
		/// </summary>
		public const string ToTime0="ToTime0";
		/// <summary>
		/// 呼出时间从
		/// </summary>
		public const string FromTime1="FromTime1";
		/// <summary>
		/// 呼出时间到
		/// </summary>
		public const string ToTime1="ToTime1";
		/// <summary>
		/// 最高呼叫次数
		/// </summary>
		public const string MaxCallTime="MaxCallTime";
		/// <summary>
		/// 响铃长度
		/// </summary>
		public const string RingLong="RingLong";
		/// <summary>
		/// 备注
		/// </summary>
		public const string Note="Note";
		#endregion		
	}
	/// <summary>
	/// 电话类型
	/// </summary> 
	public class TelType :EntityOIDName
	{
		#region 基本属性
		/// <summary>
		/// 建立时间。
		/// </summary>
		public string  FromTime0
		{
			get
			{
				return GetValStringByKey(TelTypeAttr.FromTime0);
			}
			set
			{
				SetValByKey(TelTypeAttr.FromTime0,value);
			}
		} 
		public string  FromTime1
		{
			get
			{
				return GetValStringByKey(TelTypeAttr.FromTime1);
			}
			set
			{
				SetValByKey(TelTypeAttr.FromTime1,value);
			}
		} 
		/// <summary>
		/// 响铃长度
		/// </summary>
		public int  RingLong
		{
			get
			{
				return GetValIntByKey(TelTypeAttr.RingLong);
			}
			set
			{
				SetValByKey(TelTypeAttr.RingLong,value);
			}
		} 
		public int  MaxCallTime
		{
			get
			{
				return GetValIntByKey(TelTypeAttr.MaxCallTime);
			}
			set
			{
				SetValByKey(TelTypeAttr.MaxCallTime,value);
			}
		} 
		public string  Note
		{
			get
			{
				return GetValStringByKey(TelTypeAttr.Note);
			}
			set
			{
				SetValByKey(TelTypeAttr.Note,value);
			}
		} 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 电话类型
		/// </summary>
		public TelType(){}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="no"></param>
		public TelType(int no):base(no){}

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
				Map map = new Map("CTI_TelType");	
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.None;

				map.CodeStruct="1";
				map.EnDesc="用户类型设置";
			 
				map.AddTBIntPK("OID",0,"ID",true,false);
				map.AddTBString(TelTypeAttr.Name,null,"名称",true,false,1,100,4);
				map.AddTBString(TelTypeAttr.FromTime0,"08:00","时间1从",true,false,5,5,5);
				map.AddTBString(TelTypeAttr.ToTime0,"12:00","到",true,false,5,5,5);

				map.AddTBString(TelTypeAttr.FromTime1,"14:00","时间2从",true,false,5,5,5);
				map.AddTBString(TelTypeAttr.ToTime1,"20:00","到",true,false,5,5,5);

				map.AddTBInt(TelTypeAttr.MaxCallTime,10,"最高呼叫次数",true,false);
				map.AddTBInt(TelTypeAttr.RingLong,20,"响铃长度秒",true,false);
				map.AddTBStringDoc(TelTypeAttr.Note,null,"备注",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 电话类型
	/// </summary>
	public class TelTypes :EntitiesOIDName
	{
		#region 方法
		 
		#endregion 

		#region 构造方法属性
		/// <summary>
		/// TelTypes
		/// </summary>
		public TelTypes(){}
		 
		
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TelType();
			}
		}
		#endregion
	}
}
