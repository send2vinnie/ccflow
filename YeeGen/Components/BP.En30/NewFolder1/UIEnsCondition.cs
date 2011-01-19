using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;

namespace BP.Sys
{
	 
	public class UIEnsConditionAttr : EntityClassNameAttr
	{
		public const string PKey="PKey";

	}
	 
	 
	public class UIEnsCondition: EntityClassName
	{
		#region 基本属性
		public string PKey
		{
			get
			{
				return this.GetValStringByKey(UIEnsConditionAttr.PKey ) ; 
			}
			set
			{
				this.SetValByKey(UIEnsConditionAttr.PKey,value) ; 
			}
		}
		#endregion

		#region 构造方法
		public UIEnsCondition(){}		 
		public UIEnsCondition(string _No ): base(_No){}
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_UIEnsCondition");
				map.EnType=EnType.Sys;
				map.EnDesc="实体键值信息";
				map.DepositaryOfEntity=Depositary.Application;
				map.AddTBStringPK(UIEnsConditionAttr.EnsClassName ,null,"实体类名称",true,true,2,20,10);
				map.AddTBString(UIEnsConditionAttr.PKey,null,"主键值",true,false,2,10,10);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 


	}
	
	/// <summary>
	/// 实体集合
	/// </summary>
	public class UIEnsConditions : Entities
	{		 
		public UIEnsConditions(){}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new UIEnsCondition();
			}
		}
		
	}
}
