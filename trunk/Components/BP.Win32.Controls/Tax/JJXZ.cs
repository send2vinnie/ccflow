using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
//using BP.ZHZS.Base;

namespace BP.Tax
{
	/// <summary>
	///属于国税的征收机关
	/// </summary>
	public class ZSJG :SimpleNoNameFix
	{
		#region 实现基本的方方法
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Tax_ZSJG";
			}
		}
		 
		public override string  Desc
		{
			get
			{
				return "征收机关";
			}
		}
		#endregion 

		#region 构造方法
		 
		public ZSJG(){}
		 
		public ZSJG(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// 属于国税的征收机关
	/// </summary>
	public class ZSJGs :SimpleNoNameFixs
	{
		 
		public ZSJGs(){}
		 
		public override Entity GetNewEntity
		{
			get
			{
				return new ZSJG();
			}
		}
	}
}
