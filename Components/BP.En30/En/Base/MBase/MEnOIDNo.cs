using System;
using CWAI.En;
using CWAI.En.Base ;
using System.Collections;
using CWAI.DA;
using System.Data;
using CWAI.Sys;
using CWAI;
namespace CWAI.En.Base
{
	/// <summary>
	/// 多实体
	/// </summary>
	abstract public class EnOIDNo : Entity
	{
		#region 基本属性
		/// <summary>
		/// OID
		/// </summary>
		public int OID
		{
			get
			{
				return this.GetValIntByKey(MEnAttr.OID);
			}
			set
			{
				this.SetValByKey(MEnAttr.OID,value);
			}
		}
		/// <summary>
		/// 实体编号
		/// </summary>
		public string No
		{
			get
			{
				return this.GetValStringByKey(MEnAttr.No);
			}
			set
			{
				this.SetValByKey(MEnAttr.No,value);
			}
		}
		#endregion 

		#region 构造
		public EnOIDNo()
		{}
 
		protected EnOIDNo(int _OID)
		{
			this.OID = _OID;
			this.Retrieve();
		} 
		public int RetrieveByNo()
		{
			QueryObject qo = new QueryObject(this) ; 
			qo.AddWhere(MEnAttr.No,this.No) ;
			return qo.DoQuery(); 
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
	/// 建立一个EnsOIDNo实体
	/// </summary>
	abstract public class EnsOIDNo : EntitiesOIDNo
	{
		/// <summary>
		/// 建立一个EnsOIDNo实体
		/// </summary>
		public EnsOIDNo()
		{
		}
		/// <summary>
		/// 建立一个EnsOIDNo实体
		/// </summary>
		/// <param name="no"></param>
		public EnsOIDNo(string no)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(MEnAttr.No, no);
			qo.DoQuery();
		}
	} 
}
