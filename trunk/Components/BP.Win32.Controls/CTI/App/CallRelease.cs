using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.CTI.App
{
	/// <summary>
	/// 免催列表属性
	/// </summary>
	public class CTIReleaseAttr:EntityOIDAttr
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
		public const string CTITime="CTITime";
		/// <summary>
		/// 备注1
		/// </summary>
		public const string Note="Note";
		#endregion
	}
	/// <summary>
	/// 免催列表
	/// </summary> 
	public class CallRelease :Entity
	{
		#region 基本属性
		 
		public string  Note
		{
			get
			{
				return GetValStringByKey(CTIReleaseAttr.Note);
			}
			set
			{
				SetValByKey(CTIReleaseAttr.Note,value);
			}
		} 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 条目
		/// </summary>
		public CallRelease(){}
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
				Map map = new Map("CTI_Release");	
				//map.DepositaryOfMap=Depositary.Application;
				//map.DepositaryOfEntity=Depositary.Application;

				//				map.CodeStruct="3";
				map.EnDesc="免催列表";                
			 
				map.AddTBStringPK(CTIReleaseAttr.Tel,null,"电话",true,false,5,50,20);
				map.AddTBStringDoc(CTIReleaseAttr.Note,null,"备注",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		protected override void afterInsert()
		{
			base.afterInsert ();
			DBAccess.RunSQL("delete CTI_CallList where tel in (select tel from CTI_Release)");
		}

	}
	/// <summary>
	/// 免催列表s
	/// </summary>
	public class CallReleases :EntitiesOID
	{
		#region 方法
		 
		#endregion 

		#region 构造方法属性
		/// <summary>
		/// CTIReleases
		/// </summary>
		public CallReleases(){}
		 
		
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallRelease();
			}
		}
		#endregion
	}
}
