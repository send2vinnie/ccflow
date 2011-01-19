using System;

namespace BP.En.MBase
{
	/// <summary>
	/// 单实体 。
	/// </summary>
	abstract public class EnNo:EntityNo
	{
		#region 构造涵数
		/// <summary>
		/// EnNo
		/// </summary>
		public EnNo(){}
		/// <summary>
		/// EnNo
		/// </summary>
		/// <param name="_no">_no</param>
		public EnNo(string _no)  :base(_no){}		
		#endregion		 
	}
	/// <summary>
	/// 单实体集合。
	/// </summary>
	abstract public class EnsNo : Entities
	{
		public EnsNo(){}
	}
}
