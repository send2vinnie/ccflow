using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
using BP.Rpt;

namespace BP.CTI.App
{
	/// <summary>
	/// 工作统计属性
	/// </summary>
	public class CallStatAttr:EntityOIDAttr
	{
		#region 属性
		/// <summary>
		/// 呼出日期
		/// </summary>
		public const string CallDate="CallDate";
		/// <summary>
		/// 呼出数量
		/// </summary>
		public const string Num="Num";
		/// <summary>
		/// 电话类型
		/// </summary>
		public const string FK_TelType="FK_TelType";
		/// <summary>
		/// 呼出状态
		/// </summary>
		public const string CallingState="CallingState";
		#endregion
	}
	/// <summary>
	/// 工作统计
	/// </summary> 
	public class CallStat :Entity
	{
		#region 基本属性
		 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 条目
		/// </summary>
		public CallStat(){}
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
				Map map = new Map("V_CTI_CallStat");	
				map.DepositaryOfMap=Depositary.Application;
				map.EnType=EnType.View;
				//				map.CodeStruct="3";
				map.EnDesc="工作统计";                
 

				//map.AddTBStringPK(CallStatAttr.CallDate,null,"日期",true,false,5,100,4);
				map.AddDDLEntitiesNoNamePK(CallListAttr.CallDate,null,"日期",new BP.Pub.Days(),false);


				map.AddDDLEntities(CallListAttr.FK_TelType,0,DataType.AppInt,"电话类型",new TelTypes(),TelTypeAttr.OID,TelTypeAttr.Name,false);
				map.AddDDLSysEnum(CallStatAttr.CallingState,0,"呼出状态",true,false);
				map.AddDDLEntitiesNoName(CallListAttr.CallingState,null,"呼出状态",new CallStates(),false);

				map.AddTBInt(CallStatAttr.Num,0,"个数",true,false);

				map.AddSearchAttr(CallListAttr.FK_TelType);
				//map.AddSearchAttr(CallListAttr.CallDate);
				map.AddSearchAttr(CallListAttr.CallingState);

				 


				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 条目
	/// </summary>
	public class CallStats :EntitiesOID
	{
		public static Rpt3DEntity GetRpt3D
		{
			get
			{
				DataTable dt =DBAccess.RunSQLReturnTable("select * from V_CTI_CallStat");

				BP.Pub.Days days = new BP.Pub.Days();
				days.RetrieveAll();

				TelTypes tels = new TelTypes();
				tels.RetrieveAll();

				CallStates stas = new CallStates();
				stas.RetrieveAll();



				Rpt3DEntity rpt = new Rpt3DEntity(days,tels,stas,dt) ;
				rpt.CutNotRefD1();
				rpt.CutNotRefD2();
				rpt.CutNotRefD3();			 
				rpt.Title="呼出统计报表";

				return rpt;


			}
		}

		#region 构造方法属性
		/// <summary>
		/// CallStats
		/// </summary>
		public CallStats(){}
		 
		
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallStat();
			}
		}
		#endregion
	}
}
