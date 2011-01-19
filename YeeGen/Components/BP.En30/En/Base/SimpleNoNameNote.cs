using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.En
{
	/// <summary>
	/// 属性
	/// </summary>
	public class SimpleNoNaIEnoteAttr : EntityNoNameAttr
	{
		public const string Note="Note";
	}
	
	abstract public class SimpleNoNaIEnote : EntityNoName
	{
		/// <summary>
		/// Note
		/// </summary>
		public string Note
		{
			get
			{
				return this.GetValStringByKey(SimpleNoNaIEnoteAttr.Note);
			}
			set
			{
				this.SetValByKey(SimpleNoNaIEnoteAttr.Note,value);
			}
		}

		#region 构造
		/// <summary>
		/// 简单编码类实体
		/// </summary>
		public SimpleNoNaIEnote()
		{}
		/// <summary>
		/// 简单编码类实体
		/// </summary>
		/// <param name="_No">编号</param>
		protected SimpleNoNaIEnote(string _No) : base(_No){}
		/// <summary>
		/// 描述
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map(this.PhysicsTable);
				map.EnDesc=this.Desc;

				map.DepositaryOfEntity=Depositary.Application;
				map.DepositaryOfMap=Depositary.Application;


				map.AddTBStringPK(SimpleNoNaIEnoteAttr.No,null,null,true,false,1,20,10);
				map.AddTBString(SimpleNoNaIEnoteAttr.Name,null,null,true,false,0,200,10);
				map.AddTBString(SimpleNoNaIEnoteAttr.Note,null,null,true,false,0,500,10);
 
				this._enMap=map;
				return this._enMap; 
			}
		}		 
		#endregion 		

		#region 需要子类重写的方法
		/// <summary>
		/// 指定表
		/// </summary>
		protected abstract string PhysicsTable{get;}
		/// <summary>
		/// 描述
		/// </summary>
		protected abstract string Desc{get;}
		#endregion 

	
		#region  重写基类的方法。
		/// <summary>
		/// 增加逻辑处理
		/// </summary>
		/// <returns></returns>
		protected override bool beforeInsert()
		{
			base.beforeInsert();
			return true;
		}
		/// <summary>
		/// 更新逻辑处理
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			base.beforeUpdate();
			return true;
		}
		/// <summary>
		/// 逻辑处理
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete()
		{
			base.beforeDelete();
			return true;
		}
		/// <summary>
		/// 逻辑处理
		/// </summary>
		protected override void afterDelete()
		{
			base.afterDelete();
			return ;
		}
		/// <summary>
		/// 逻辑处理
		/// </summary>
		protected override  void afterInsert()
		{
			base.afterInsert();
			return ;
		}
		/// <summary>
		/// 逻辑处理
		/// </summary>
		protected override void afterUpdate()
		{
			base.afterUpdate();
			return ;
		}
		#endregion
	}
	abstract public class SimpleNoNaIEnotes : EntitiesNoName
	{	 
		public SimpleNoNaIEnotes()
		{}
	}
}
