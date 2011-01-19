using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.En
{
	/// <summary>
	/// EntityOIDNoAttr
	/// </summary>
	public class EntityOIDNoAttr : EntityOIDAttr 
	{	
		/// <summary>
		/// 编号
		/// </summary>
		public const string No="No";		
	}
	
	abstract public class EntityOIDNo : EntityOID
	{
		#region 基本属性
		/// <summary>
		/// 实体编号
		/// </summary>
		public string No
		{
			get
			{
				return this.GetValStringByKey(EntityOIDNoAttr.No);
			}
			set
			{
				this.SetValByKey(EntityOIDNoAttr.No,value);
			}
		}
		#endregion 

		#region 构造
		public EntityOIDNo()
		{}
		protected EntityOIDNo(string _No)
		{
			this.No = _No  ; 
			QueryObject qo = new QueryObject(this) ; 
			qo.AddWhere(EntityOIDNoAttr.No , this.No);
			if (qo.DoQuery()==0) 
			{
				throw new Exception("@没有编号["+this.No+"]["+this.EnDesc+"]这个实体");
			}
		}		
		protected EntityOIDNo(int _OID) : base(_OID){}		
 
		public int RetrieveByNo()
		{
			QueryObject qo = new QueryObject(this) ; 
			qo.AddWhere(EntityOIDNoAttr.No,this.No) ;
			return qo.DoQuery(); 
		}
		#endregion 	

		#region  重写基类的方法。
		#endregion

	}
	abstract public class EntitiesOIDNo : EntitiesOID
	{	 
		public EntitiesOIDNo(){}
	}
}
