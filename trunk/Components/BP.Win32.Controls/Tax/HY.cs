using System;
using System.Collections;
using BP.DA;
using BP.En.Base;

namespace BP.Tax
{
	/// <summary>
	/// 行业
	/// </summary>
	public class HY :SimpleNoNameFix
	{
		#region 实现基本的方方法		 
		public override string  PhysicsTable
		{
			get
			{
				return "Tax_HY";
			}
		}	 
		public override string  Desc
		{
			get
			{
				return "行业";			
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 行业
		/// </summary> 
		public HY(){}
		/// <summary>
		/// 行业
		/// </summary>
		/// <param name="_No"></param>
		public HY(string _No ): base(_No){}
		#endregion 
	}
	 
	public class HYs :SimpleNoNameFixs
	{
		/// <summary>
		/// 行业集合
		/// </summary>
		public HYs(){}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new HY();
			}
		}
	}
}
