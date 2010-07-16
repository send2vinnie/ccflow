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
	public class TelTypeOfFitAttr:EntityOIDNameAttr
	{
		#region 属性 
		#endregion		
	}
	/// <summary>
	/// 电话类型
	/// </summary> 
	public class TelTypeOfFit :EntityOIDName
	{
		#region 基本属性		 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 电话类型
		/// </summary>
		public TelTypeOfFit(){}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="no"></param>
		public TelTypeOfFit(int no):base(no){}
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
				Map map = new Map("V_CTI_TelTypeOfFit");	
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.Application;
				map.EnType=EnType.View; 
				map.EnDesc="适合用户类型";
				 
				map.AddTBIntPKOID();
				map.AddTBString(TelTypeOfFitAttr.Name,null,"名称",true,false,1,100,4);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 电话类型
	/// </summary>
	public class TelTypeOfFits :EntitiesOIDName
	{
		#region 方法
		#endregion 

		#region 构造方法属性
		/// <summary>
		/// TelTypeOfFits
		/// </summary>
		public TelTypeOfFits(){}
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TelTypeOfFit();
			}
		}
		#endregion
	}
}
