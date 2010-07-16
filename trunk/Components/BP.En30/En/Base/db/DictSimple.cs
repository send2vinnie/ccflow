using System;
using System.Collections;

namespace BP.En.Base
{
	/// <summary>
	/// DictSimple 的摘要说明。
	/// 简单编码类 Key val 
	/// </summary>
	abstract public class DictSimple : EntityOID
	{
		protected DictSimple() 
		{
		}
		protected DictSimple(int oid) : base(oid){}

		#region 提供的属性
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

		#region 实现基类的方法
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

}
