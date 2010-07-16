using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
using BP.CTI;


namespace BP.CTI.App
{
	/// <summary>
	/// 呼出状态
	/// </summary>
	public enum CallingState
	{
		/// <summary>
		/// 等待呼出
		/// </summary>
		Init,
		/// <summary>
		/// 正在呼出
		/// </summary>
		Calling,
		/// <summary>
		/// 呼出成功
		/// </summary>
		OK,
		/// <summary>
		/// 呼出超时
		/// </summary>
		TimeOut,
		/// <summary>
		/// 呼出错误
		/// </summary>
		Error
	}
	/// <summary>
	/// 呼出列表属性
	/// </summary>
	public class CallListAttr:EntityOIDAttr
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
		/// 征收机关
		/// </summary>
		public const string FK_ZSJG="FK_ZSJG";

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
	public class CallList :Entity
	{
		#region 执行操作
		/// <summary>
		/// 呼叫文件
		/// </summary>
		public string[] CallFiles=null;
		/// <summary>
		/// 设置呼出内容.
		/// </summary>
		/// <param name="context">要呼出的内容</param>
		public void DoCalling(string context)
		{
			if (context.IndexOf(",")!=-1)
			{
				/*说明是多文件播放*/
				context=context.Replace("@YF@", "D"+DateTime.Now.AddMonths(-1).Month+".TW");
				context=context.Replace("@Tel@", this.TelOfFile);
				context=context.Replace("@JE@", this.JEOfFile);
			}
			context=context.Replace(",,", ",");
			this.CallFiles=context.Split(',');

			this.Update(CallListAttr.CallingState,(int)BP.CTI.App.CallingState.Calling); // 避免下次再取出他.
		}
		public void DoInit(string msg)
		{
			this.Update(CallListAttr.CallingState,(int)BP.CTI.App.CallingState.Init,
				CallListAttr.CallDate,DataType.CurrentData,
				CallListAttr.CallDateTime,DataType.CurrentTime,
				CallListAttr.Note,msg);
		}
		public void DoTimeOut(string msg)
		{
			this.Update(CallListAttr.CallingState,(int)BP.CTI.App.CallingState.Init,
				CallListAttr.CallDegree, this.CallDegree+1 , 
				CallListAttr.CallDate,DataType.CurrentData,
				CallListAttr.CallDateTime,DataType.CurrentTime,
				CallListAttr.Note,msg);
		}
		public void DoError(string msg)
		{
			this.Update(CallListAttr.CallingState,(int)BP.CTI.App.CallingState.Error,				
				CallListAttr.CallDate,DataType.CurrentData,
				CallListAttr.CallDateTime,DataType.CurrentTime,
				CallListAttr.Note,msg);
		}
		public void DoOK()
		{
			this.Update(CallListAttr.CallingState,(int)BP.CTI.App.CallingState.OK,	
				CallListAttr.CallDate,DataType.CurrentData,
				CallListAttr.CallDateTime,DataType.CurrentTime,
				CallListAttr.Note,"成功呼出");
		}
		#endregion

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
				return BP.SystemConfig.AppSettings["BeforeDial"]+GetValStringByKey(CallListAttr.Tel);
			}
			set
			{
				SetValByKey(CallListAttr.Tel,value);
			}
		}
		/// <summary>
		/// TelName
		/// </summary>
		public string  TelName
		{
			get
			{
				return GetValStringByKey(CallListAttr.TelName);
			}
			set
			{
				SetValByKey(CallListAttr.TelName,value);
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
				return GetValFloatByKey(CallListAttr.JE);
			}
			set
			{
				this.SetValByKey(CallListAttr.JE,value);
			}
		}
		/// <summary>
		/// 电话类型
		/// </summary>
		public int  FK_TelType
		{
			get
			{
				return GetValIntByKey(CallListAttr.FK_TelType);
			}
			set
			{
				SetValByKey(CallListAttr.FK_TelType,value);
			}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string  Note
		{
			get
			{
				return GetValStringByKey(CallListAttr.Note);
			}
			set
			{
				SetValByKey(CallListAttr.Note,value);
			}
		} 
		/// <summary>
		/// 呼出日其
		/// </summary>
		public string  CallDate
		{
			get
			{
				return GetValStringByKey(CallListAttr.CallDate);
			}
			set
			{
				SetValByKey(CallListAttr.CallDate,value);
			}
		}
		/// <summary>
		/// 呼出的时间
		/// </summary>
		public int CallDegree
		{
			get
			{
				return GetValIntByKey(CallListAttr.CallDegree);
			}
			set
			{
				SetValByKey(CallListAttr.CallDegree,value);
			}
		}
		public string CallDateTime
		{
			get
			{
				return GetValStringByKey(CallListAttr.CallDateTime);
			}
			set
			{
				SetValByKey(CallListAttr.CallDateTime,value);
			}
		}
		/// <summary>
		/// 呼出状态
		/// </summary>
		public int CallingState
		{
			get
			{
				return GetValIntByKey(CallListAttr.CallingState);
			}
			set
			{
				SetValByKey(CallListAttr.CallingState,value);
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
				SetValByKey(CallListAttr.CallingState,(int)value);
			}
		}
		 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 呼出
		/// </summary>
		public CallList()
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
				map.EnDesc="工作列表";
			 
				map.AddTBStringPK(CallListAttr.Tel,null,"电话",true,false,5,12,20);
				map.AddTBString(CallListAttr.TelName,null,"客户",true,false,0,200,20);
				map.AddTBFloat(CallListAttr.JE,100,"金额",true,false);
				map.AddDDLEntities(CallListAttr.FK_TelType,1,DataType.AppInt,"电话类型",new TelTypes(),TelTypeAttr.OID,TelTypeAttr.Name,true);
				//map.AddDDLEntitiesNoName(CallListAttr.FK_Context,"0","呼出内容",new CallContexts(),true);
				map.AddTBInt(CallListAttr.CallDegree,0,"已呼出次数",true,false);
				map.AddDDLSysEnum(CallListAttr.CallingState,0,"呼出状态",true,false);

				map.AddTBString(CallListAttr.CallDate,DataType.CurrentData, "呼出日期",true,false,0,10,10);
				map.AddTBString(CallListAttr.CallDateTime,DataType.CurrentTime,"呼出时间",true,false,0,5,5);
				map.AddTBStringDoc(CallListAttr.Note,null,"备注",true,false);


				if (SystemConfig.CustomerNo == CustomerNoList.LYTax)
				{
					map.AddDDLEntitiesNoName(CallListAttr.FK_ZSJG, BP.Web.TaxUser.FK_ZSJG, "县局", new BP.Tax.ZSJGs(),false);
					map.AddSearchAttr(CallListAttr.FK_ZSJG);
				}


				map.AddSearchAttr(CallListAttr.FK_TelType);
				map.AddSearchAttr(CallListAttr.CallingState);

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
	public class CallLists :EntitiesOID
	{
		public static DataTable GetCurrentCall
		{
			get
			{
				CallLists cls =new CallLists();
				string time =DateTime.Now.ToString("HH:mm");					 
				QueryObject qo = new QueryObject(cls);
				if (SystemConfig.CustomerNo==CustomerNoList.YSNet)
					qo.Top=1000;
				else
					qo.Top=1000;

				qo.AddWhere(CallListAttr.CallingState,0);
				qo.addAnd();
				qo.AddWhereInSQL(CallListAttr.FK_TelType, "SELECT OID from CTI_TelType where (FromTime1 < '"+time+"'  and ToTime1 > '"+time+"') OR ( FromTime0 < '"+time+"'  and ToTime0 > '"+time+"') ");
				qo.addAnd();
				qo.AddWhere(CallListAttr.JE, ">=",Card.DefaultMinJE);

					 
				// 判断是否有全部的电话类型。
				bool isHaveO=false;
				if (Card.dtOfContext==null)
					Card.GetCurrentContextByTelType(0);
				foreach(DataRow dr in Card.dtOfContext.Rows)
				{
					if (dr["适合用户类型"].ToString()=="0")
					{
						isHaveO=true;
					}
				}

				if (isHaveO==false)
				{
					/* 如果没有全部 的电话类型 */
					qo.addAnd();
					string sql="SELECT b.FK_TelTypeOfFit   FROM CTI_Schedule a, CTI_Context b WHERE "
						+"a.FK_YF='"+DataType.CurrentMonth+"' and ( a.DateFrom <='"+DataType.CurrentDay+"' and a.DateTo>='"+DataType.CurrentDay+"') and a.FK_Context=b.No";
					qo.AddWhereInSQL(CallListAttr.FK_TelType, sql );
				}
				qo.addOrderBy(CallListAttr.CallDegree);

				//qo.addOrderBy(CallListAttr.Tel);
				//Log.DefaultLogWriteLineInfo(qo.SQL);

				return qo.DoQueryToTable();
			}
		}
 
		 

		#region 得到一个呼出
		public static CallLists _HisCallList=null;		
		public static CallLists HisCallList
		{
			get
			{
				if (_HisCallList==null)
					_HisCallList = new CallLists();

				if (_HisCallList.Count==0)
				{
					/*如果没有可以呼出的内容，就开始装在。*/

					string time =DateTime.Now.ToString("HH:mm");					 
					QueryObject qo = new QueryObject(_HisCallList);
					if (SystemConfig.CustomerNo==CustomerNoList.YSNet)
						qo.Top=100;
					else
						qo.Top=10;

					qo.AddWhere(CallListAttr.CallingState,0);
					qo.addAnd();
					qo.AddWhereInSQL(CallListAttr.FK_TelType, "SELECT OID from CTI_TelType where (FromTime1 < '"+time+"'  and ToTime1 > '"+time+"') OR ( FromTime0 < '"+time+"'  and ToTime0 > '"+time+"') ");
					qo.addAnd();
					qo.AddWhere(CallListAttr.JE, ">=",Card.DefaultMinJE);

					 
					// 判断是否有全部的电话类型。
					bool isHaveO=false;
					if (Card.dtOfContext==null)
						Card.GetCurrentContextByTelType(0);
					foreach(DataRow dr in Card.dtOfContext.Rows)
					{
						if (dr["适合用户类型"].ToString()=="0")
						{
							isHaveO=true;
						}
					}

					if (isHaveO==false)
					{
						/* 如果没有全部 的电话类型 */
						qo.addAnd();
						string sql="SELECT b.FK_TelTypeOfFit   FROM CTI_Schedule a, CTI_Context b WHERE "
							+"a.FK_YF='"+DataType.CurrentMonth+"' and ( a.DateFrom <='"+DataType.CurrentDay+"' and a.DateTo>='"+DataType.CurrentDay+"') and a.FK_Context=b.No";
						qo.AddWhereInSQL(CallListAttr.FK_TelType, sql );
					}

					if (SystemConfig.CustomerNo==CustomerNoList.YSNet)
						qo.addOrderBy(CallListAttr.CallDegree);
					else
						qo.addOrderBy(CallListAttr.FK_TelType+" desc " , CallListAttr.CallDegree ); // 首先呼企业的。
					qo.DoQuery();
				}

				return _HisCallList;				
			}
		}
		/// <summary>
		/// 得到一个呼出
		/// </summary>
		/// <returns></returns>
		public static CallList GetCall()
		{
			if (HisCallList.Count==0)
			{
				/* 如果没有可以呼出 */
				return null;
			}
			else
			{
				CallList en= (CallList)HisCallList[0];
				_HisCallList.RemoveEn(en);
				return en;
			}
		}
		#endregion

		#region 方法
		#endregion 

		#region 构造方法属性
		/// <summary>
		/// CallLists
		/// </summary>
		public CallLists(){}
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallList();
			}
		}
		#endregion

		 
	}
}
