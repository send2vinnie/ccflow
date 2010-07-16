using System;
using System.Collections;
using BP.DA;
using BP.En;


namespace BP.Tax
{
	/// <summary>
	/// 银行
	/// </summary>
	public class PubYearOfFuture :SimpleNoNameFix
	{
		#region 实现基本的方法
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_YearOfFuture";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "未来年度";
			}
		}
		#endregion 

		#region 构造方法
		 
		public PubYearOfFuture(){}
		 
		public PubYearOfFuture(string _No ): base(_No){}
		#endregion 
	}
	 
	/// <summary>
	/// 银行s
	/// </summary>
	public class PubYearOfFutures :SimpleNoNameFixs
	{
		#region 构造
		/// <summary>
		/// 银行s
		/// </summary>
		public PubYearOfFutures(){}
		/// <summary>
		/// 银行
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PubYearOfFuture();
			}
		}
		#endregion
	}
}
