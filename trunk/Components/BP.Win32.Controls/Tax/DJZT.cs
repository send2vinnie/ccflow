using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
 

namespace BP.Tax
{
	/// <summary>
	/// DSTaxpayer 的摘要说明。DSDJZT
	/// </summary>
	public class DJZT : SimpleNoNameFix
	{
		#region 实现基本的方方法
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Tax_DJZT";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "登记状态";		
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// dengji
		/// </summary>
		public DJZT(){}
		
		/// <summary>
		/// state 
		/// </summary>
		/// <param name="_No">No</param>
		public DJZT(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// 纳税人集合DSDJZTs
	/// </summary>
	public class DJZTs :SimpleNoNameFixs
	{
		/// <summary>
		/// state
		/// </summary>
		public DJZTs(){}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new DJZT();
			}
		}
	}
}
