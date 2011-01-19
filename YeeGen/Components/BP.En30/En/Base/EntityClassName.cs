using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// EntityEnsNameAttr
	/// </summary>
	public class EntityEnsNameAttr  
	{	
		/// <summary>
		/// className
		/// </summary>
		public const string EnsEnsName="EnsEnsName";
	}
	/// <summary>
	/// NoEntity 的摘要说明。
	/// </summary>
	abstract public class EntityEnsName : Entity
	{
		#region 基本属性
		/// <summary>
		/// 集合类名称
		/// </summary>
		public string EnsEnsName
		{
			get
			{
				return this.GetValStringByKey(EntityEnsNameAttr.EnsEnsName) ; 
			}
			set
			{
				this.SetValByKey(EntityEnsNameAttr.EnsEnsName,value) ; 
			}
		}
		#endregion 

		#region 扩展属性
		/// <summary>
		/// 他的描述
		/// </summary>
		public string HisDesc
		{
			get
			{
				return this.HisEntity.EnDesc;
			}
		}
		/// <summary>
		/// 他的实体
		/// </summary>
		public Entity HisEntity
		{
			get
			{
				return this.HisEntities.GetNewEntity;
			}
		}
		/// <summary>
		/// 他的实体集合
		/// </summary>
		public Entities HisEntities
		{
			get
			{
				return ClassFactory.GetEns(this.EnsEnsName) ;
			}
		}
		#endregion 

		#region 构造
		public EntityEnsName()
		{
		}
		/// <summary>
		/// class Name 
		/// </summary>
		/// <param name="_EnsEnsName">_EnsEnsName</param>
		protected EntityEnsName(string _EnsEnsName)
		{
			this.EnsEnsName = _EnsEnsName;
			this.Retrieve();
		}	
		#endregion


		#region  重写基类的方法。
		protected override bool beforeInsert()
		{
			base.beforeInsert();
			return true;
		}
		protected override bool beforeUpdate()
		{
			base.beforeUpdate();
			return true;
		}
		protected override bool beforeDelete()
		{
			base.beforeDelete();
			return true;
		}

		protected override void afterDelete()
		{
			base.afterDelete();
			return ;

		}
		protected override  void afterInsert()
		{
			base.afterInsert();
			return ;
		}
		protected override void afterUpdate()
		{
			base.afterUpdate();
			return ;
		}
		#endregion 
	}
	/// <summary>
	/// 类名实体集合
	/// </summary>
	abstract public class EntitiesEnsName : Entities
	{
		/// <summary>
		/// 实体集合
		/// </summary>
		public EntitiesEnsName()
		{
		}
	}
}
