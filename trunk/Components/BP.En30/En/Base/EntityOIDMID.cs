using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// EntityOIDMIDAttr
	/// </summary>
	public class EntityOIDMIDAttr:EntityOIDAttr
	{
		/// <summary>
		/// 标记ID
		/// </summary>
		public const string MID="MID";

	}
	/// <summary>
	/// 用于OID MID 明晰实体继承
	/// </summary>
	abstract public class EntityOIDMID : EntityOID
	{		 
		#region 构造
		/// <summary>
		/// 构造
		/// </summary>
		protected EntityOIDMID(){}
		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="oid">OID</param>
		protected EntityOIDMID(int oid):base(oid){}
		#endregion 

		#region 属性方法
		/// <summary>
		/// 标记ID
		/// </summary>
		public int MID 
		{
			get
			{			 
				return this.GetValIntByKey(EntityOIDMIDAttr.MID);			 
			} 
			set
			{			 
				this.SetValByKey(EntityOIDMIDAttr.MID,value);			 
			} 
		}
		/// <summary>
		/// 返回查询出来的个数
		/// </summary>
		/// <param name="mid">mid</param>
		/// <returns>返回查询出来的个数</returns>
		public int RetrieveByMID(int mid)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere("MID",mid );
			return qo.DoQuery();			 
		}
		#endregion
		 
	}
	/// <summary>
	/// 用于OID MID 明晰实体继承
	/// </summary>
	abstract public class EntitiesOIDMID : EntitiesOID
	{
		#region 构造
		/// <summary>
		/// 构造
		/// </summary>
		public EntitiesOIDMID()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 构造
		/// </summary>
		public EntitiesOIDMID(int mid)
		{
			this.RetrieveByMID(mid);			
		}
		#endregion

		#region 查询方法
		/// <summary>
		/// 按照MID查询，返回查询出来的个数，并把查询结果放入实体集合内。
		/// </summary>
		/// <param name="mid">MID</param>
		/// <returns>返回查询出来的个数</returns>
		public int RetrieveByMID(int mid)
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere("MID",mid);
			return qo.DoQuery();
		}
		#endregion
	}
}
