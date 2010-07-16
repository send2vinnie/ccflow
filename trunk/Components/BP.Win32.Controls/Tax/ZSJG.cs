using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
//using BP.ZHZS.Base;

namespace BP.Tax
{
	/// <summary>
	///属于国税的经济性质
	/// </summary>
	public class JJXZ :SimpleNoNameFix
	{
		#region 实现基本的方方法
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Tax_JJXZ";
			}
		}
		 
		public override string  Desc
		{
			get
			{
				return "经济性质";
			}
		}
		#endregion 

		#region 构造方法
		 
		public JJXZ(){}
		 
		public JJXZ(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// 属于国税的经济性质
	/// </summary>
	public class JJXZs :SimpleNoNameFixs
	{
		 
		public JJXZs(){}
		 
		public override Entity GetNewEntity
		{
			get
			{
				return new JJXZ();
			}
		}
	}
}
