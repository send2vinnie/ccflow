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
	public class CallListSimpleAttr:EntityOIDAttr
	{
		#region 属性
		/// <summary>
		/// tel
		/// </summary>
		public const string Tel="Tel";
		public const string TelName="TelName";

		/// <summary>
		/// 金额
		/// </summary>
		public const string JE="JE";
		/// <summary>
		/// 呼出时间从
		/// </summary>
		public const string FK_TelType="FK_TelType";
		/// <summary>
		/// 呼出时间到
		/// </summary>
		public const string FK_Context="FK_Context";
		/// <summary>
		/// 呼叫次数
		/// </summary>
		public const string CallDegree="CallDegree";		 
		/// <summary>
		/// 呼叫时间
		/// </summary>
		public const string CallDateTime="CallDateTime";
		/// <summary>
		/// 呼出状态
		/// </summary>
		public const string CallingState="CallingState";
		/// <summary>
		/// 呼出日期
		/// </summary>
		public const string CallDate="CallDate";
		/// <summary>
		/// 备注1
		/// </summary>
		public const string Note="Note";
		#endregion
	}
	/// <summary>
	/// 呼出列表
	/// </summary> 
	public class CallListSimple :Entity
	{
		#region 基本属性
		public TelType HisTelType
		{
			get
			{
				return new TelType(this.FK_TelType);
			}
		}
		 
		public string TelOfFile
		{
			get
			{
				string telstr=this.Tel;
				telstr=telstr.Replace("0","D0.TW,");
				telstr=telstr.Replace("1","D1.TW,");
				telstr=telstr.Replace("2","D2.TW,");
				telstr=telstr.Replace("3","D3.TW,");
				telstr=telstr.Replace("4","D4.TW,");
				telstr=telstr.Replace("5","D5.TW,");
				telstr=telstr.Replace("6","D6.TW,");
				telstr=telstr.Replace("7","D7.TW,");
				telstr=telstr.Replace("8","D8.TW,");
				telstr=telstr.Replace("9","D9.TW,");
				return telstr; 
			}
		}
		 
		/// <summary>
		/// 电话
		/// </summary>
		public string  Tel
		{
			get
			{
				return GetValStringByKey(CallListSimpleAttr.Tel);
			}
			set
			{
				SetValByKey(CallListSimpleAttr.Tel,value);
			}
		}
		public string JEOfFile
		{
			get
			{
				return DataType.TurnToFiels(this.JE);
			}
		}
		/// <summary>
		/// JE
		/// </summary>
		public float  JE
		{
			get
			{
				return GetValFloatByKey(CallListSimpleAttr.JE);
			}
		}
		/// <summary>
		/// 电话类型
		/// </summary>
		public int  FK_TelType
		{
			get
			{
				return GetValIntByKey(CallListSimpleAttr.FK_TelType);
			}
			set
			{
				SetValByKey(CallListSimpleAttr.FK_TelType,value);
			}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string  Note
		{
			get
			{
				return GetValStringByKey(CallListSimpleAttr.Note);
			}
			set
			{
				SetValByKey(CallListSimpleAttr.Note,value);
			}
		} 
		/// <summary>
		/// 呼出日其
		/// </summary>
		public string  CallDate
		{
			get
			{
				return GetValStringByKey(CallListSimpleAttr.CallDate);
			}
			set
			{
				SetValByKey(CallListSimpleAttr.CallDate,value);
			}
		}
		/// <summary>
		/// 呼出的时间
		/// </summary>
		public int CallDegree
		{
			get
			{
				return GetValIntByKey(CallListSimpleAttr.CallDegree);
			}
			set
			{
				SetValByKey(CallListSimpleAttr.CallDegree,value);
			}
		}
		public string CallDateTime
		{
			get
			{
				return GetValStringByKey(CallListSimpleAttr.CallDateTime);
			}
			set
			{
				SetValByKey(CallListSimpleAttr.CallDateTime,value);
			}
		}
		/// <summary>
		/// 呼出状态
		/// </summary>
		public int CallingState
		{
			get
			{
				return GetValIntByKey(CallListSimpleAttr.CallingState);
			}
			set
			{
				SetValByKey(CallListSimpleAttr.CallingState,value);
			}
		}
		public CallingState CallingStateOfEnum
		{
			get
			{
				return (CallingState)this.CallingState;
			}
			set
			{
				SetValByKey(CallListSimpleAttr.CallingState,(int)value);
			}
		}
		 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 呼出
		/// </summary>
		public CallListSimple()
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
				Map map = new Map("CTI_CallList");
				//map.DepositaryOfMap=Depositary.Application;
				//map.DepositaryOfEntity=Depositary.Application;
				map.EnDesc="呼出列表";
				 
				map.AddTBStringPK(CallListSimpleAttr.Tel,null,"电话",true,false,5,12,20);
				map.AddTBString(CallListSimpleAttr.TelName,null,"客户",true,false,0,200,20);
				map.AddTBFloat(CallListSimpleAttr.JE,100,"金额",true,false);
				map.AddDDLEntities(CallListSimpleAttr.FK_TelType,1,DataType.AppInt,"电话类型",new TelTypes(),TelTypeAttr.OID,TelTypeAttr.Name,true);
				//map.AddDDLEntitiesNoName(CallListSimpleAttr.FK_Context,"0","呼出内容",new CallContexts(),true);
				//map.AddTBInt(CallListSimpleAttr.CallDegree,0,"已呼出次数",true,false);
				//map.AddDDLSysEnum(CallListSimpleAttr.CallingState,0,"呼出状态",true,false);
				//map.AddTBString(CallListSimpleAttr.CallDate,DataType.CurrentData, "呼出日期",true,false,0,10,10);
				//map.AddTBString(CallListSimpleAttr.CallDateTime,DataType.CurrentTime,"呼出时间",true,false,0,5,5);
				map.AddTBStringDoc(CallListSimpleAttr.Note,null,"备注",true,false);

				map.AddSearchAttr(CallListSimpleAttr.FK_TelType);
				map.AddSearchAttr(CallListSimpleAttr.CallingState);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		protected override bool beforeUpdateInsertAction()
		{
			this.CallDate=DataType.CurrentData;
			this.CallDateTime=DataType.CurrentTime;
			if (this.HisTelType.MaxCallTime<=this.CallDegree)
				this.CallingStateOfEnum=BP.CTI.App.CallingState.TimeOut;
			return base.beforeUpdateInsertAction ();
		}

	}
	/// <summary>
	/// 条目
	/// </summary>
	public class CallListSimples :EntitiesOID
	{

		#region 得到一个呼出
		/// <summary>
		/// 得到一个呼出
		/// </summary>
		/// <returns></returns>
		public static CallListSimple GetCall()
		{
			string time =DateTime.Now.ToString("HH:mm");
			CallListSimples cls = new CallListSimples();
			QueryObject qo = new QueryObject(cls);
			qo.Top=1;
			qo.AddWhere(CallListSimpleAttr.CallingState,0);
			qo.addAnd();
			qo.AddWhereInSQL(CallListSimpleAttr.FK_TelType,"SELECT OID from CTI_TelType where (FromTime1 < '"+time+"'  and ToTime1 > '"+time+"') OR ( FromTime0 < '"+time+"'  and ToTime0 > '"+time+"') ");
			qo.addAnd();
			qo.AddWhere(CallListSimpleAttr.JE, ">=",Card.DefaultMinJE);

			qo.addOrderBy(CallListSimpleAttr.CallDegree);
			if (qo.DoQuery() == 1)
			{
				CallListSimple cl = (CallListSimple)cls[0] ;
				//cl.CallDateTime=DataType.CurrentDataTime;
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
		/// CallListSimples
		/// </summary>
		public CallListSimples(){}
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallListSimple();
			}
		}
		#endregion
		 
	}
}
