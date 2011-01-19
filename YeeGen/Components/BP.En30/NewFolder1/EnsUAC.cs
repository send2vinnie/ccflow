using System;
using System.Collections; 
using CWAI.En.Base;
//using CWAI.ZHZS.Base;

namespace CWAI.DA
{
	/// <summary>
	/// DSTaxpayer 的摘要说明。
	/// </summary>
	public class EnsUAC :SimpleNoName
	{
		#region 实现基本的方方法
		protected override string  PhysicsTable
		{
			get
			{
				return "Sys_DBSimpleNoNames";
			}
		}
		protected override string  Desc
		{
			get
			{
				return "Sys_DBSimpleNoNames";				
			}
		}
		#endregion 

		#region 构造方法
		public EnsUAC(){}
		/// <summary>
		/// 税务编号
		/// </summary>
		/// <param name="_No"></param>
		public EnsUAC(string _No ): base(_No){}
		#endregion 
	}
	/// <summary>
	/// 纳税人集合
	/// </summary>
	public class EnsUACs :SimpleNoNames
	{
		public EnsUACs(){}
		/// <summary>
		/// 得到它的 Entity.
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new EnsUAC();
			}
			 
		}
		 
	}
}
