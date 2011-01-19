using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// 连接实体。
	/// </summary>
	public class EntityNoNameUrlAttr : EntityNoNameAttr
	{
		public const string Url="Url";
		public const string Target="Target";
	}
	/// <summary>
	/// NoEntity 的摘要说明。
	/// </summary>
	abstract public class EntityNoNameUrl : EntityNoName
	{
		/// <summary>
		/// 连接实体
		/// </summary>
		public EntityNoNameUrl()
		{
		}
		protected EntityNoNameUrl(string _No) : base(_No){}		 
		/// <summary>
		/// URL
		/// </summary>
		public string Url
		{
			get
			{
				return this.GetValStringByKey(EntityNoNameUrlAttr.Url);
			}
			set
			{
				this.SetValByKey(EntityNoNameUrlAttr.Url,value);
			}
		}	
		/// <summary>
		/// 目标
		/// </summary>
		public string Target
		{
			get
			{
				return this.GetValStringByKey(EntityNoNameUrlAttr.Target);
			}
			set
			{
				this.SetValByKey(EntityNoNameUrlAttr.Target,value);
			}
		}	

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
	abstract public class EntitiesNoNameUrl : EntitiesNoName
	{
		public EntitiesNoNameUrl()
		{
		}
	 
	}
}
