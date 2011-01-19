using System;
using System.Collections;
using BP.DA;
using BP.En;


namespace BP.Tax
{
	/// <summary>
	/// 银行
	/// </summary>
	public class PubYearOfHistory :SimpleNoNameFix
	{
		#region 实现基本的方法
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_YearOfHistory";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "历史年度";
			}
		}
		#endregion 

		#region 构造方法
		 
		public PubYearOfHistory(){}
		 
		public PubYearOfHistory(string _No ): base(_No){}
		#endregion 
	}
	 
	/// <summary>
	/// 银行s
	/// </summary>
	public class PubYearOfHistorys :SimpleNoNameFixs
	{
		#region 构造
		/// <summary>
		/// 银行s
		/// </summary>
		public PubYearOfHistorys(){}
		/// <summary>
		/// 银行
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PubYearOfHistory();
			}
		}
		#endregion
	}
}
