using System;
using System.Collections;
using BP.DA;

namespace BP.En.Base
{
	/// <summary>
	/// Dict Attr 
	/// </summary>
	public class DictAttr : EntityOIDAttr
	{
		public const string No="No";
		public const string Name="Name";
	}
	/// <summary>
	/// DictEntity 的摘要说明。
	/// </summary>
	abstract public class Dict : EntityOID
	{ 

		#region 与编号有关的逻辑操作(这个属性只与dict EntityNo, 基类有关系。)
		/// <summary>
		/// beforeInsert
		/// </summary>
		/// <returns></returns>
		protected override bool beforeInsert()
		{
//			if (this.No=="" || this.No==null)
//				throw new Exception("@编号不允许为空。") ; 
			base.beforeInsert();
			if (this.EnMap.IsAllowRepeatNo==true)
				return true;
			string No = this.GetValStringByKey("No") ; 
			string sql ="SELECT "+this.EnMap.GetFieldByKey("No")+" FROM "+this.EnMap.PhysicsTable + " WHERE "+this.EnMap.GetFieldByKey("No")+ "='"+No+"'";
			if (DBAccess.RunSQLReturnTable(sql).Rows.Count!=0)			 
				throw new Exception("@["+this.EnMap.EnDesc+" , "+this.EnMap.PhysicsTable+"] 编号["+No+"]重复。");
			if (this.EnMap.IsCheckNoLength)
			{
				if (this.No.Length!=int.Parse(this.EnMap.CodeStruct))
					throw new Exception("@ ["+this.EnMap.EnDesc+"]编号["+this.No+"]错误，长度不符合系统要求，必须是["+int.Parse(this.EnMap.CodeStruct).ToString()+"]位，而现在有长度是["+this.No.Length.ToString()+"]位。");
			}
			return true; 
		}
		#endregion 
	
		protected Dict(){}
		/// <summary>
		/// 调用base 的方法。
		/// </summary>
		/// <param name="oid"></param>
		protected Dict(int oid) : base(oid){}
		/// <summary>
		/// 根据No 建立一个实例。
		/// </summary>
		/// <param name="_no">编号</param>
		protected Dict(string _no )
		{
			this.No=_no;
			if (this.RetrieveByNo()==0)
				throw new Exception("@没有找到编号为["+this.No+"], ["+this.EnDesc+"]的实例.");
				 
		}
		#region 提供的属性
		/// <summary>
		/// No
		/// </summary>
		public virtual string No
		{
			get
			{
				return this.GetValStringByKey(DictAttr.No);
			}
			set
			{
				this.SetValByKey(DictAttr.No,value);
			}
		}
		/// <summary>
		/// Name
		/// </summary>
		public string Name
		{
			get
			{
				return this.GetValStringByKey(DictAttr.Name);
			}
			set
			{
				this.SetValByKey(DictAttr.Name,value);
			}
		}
		#endregion

		#region 提供的查询方法
		/// <summary>
		/// 按No 查询。
		/// </summary>
		/// <returns></returns>
		public int RetrieveByNo() 
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DictAttr.No,this.No);
			int i = qo.DoQuery();
			if (i==0)
			  return 0 ; //throw new Exception("@没有找到编号为["+this.No+"]的实例.");
			else
			  return i;
			 
		}	
		/// <summary>
		///  按Name 查询。
		/// </summary>
		/// <returns></returns>	
		public int RetrieveByName()
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DictAttr.Name, this.Name);
			return qo.DoQuery();
		}
		#endregion
}
	abstract public class Dicts : EntitiesOID
	{
		public Dicts()
		{		
		}
	}
}
