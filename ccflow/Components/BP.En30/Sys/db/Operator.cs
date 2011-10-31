using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.Sys
{	 
	/// <summary>
	/// 操做符号
	/// </summary>
	public class Operator :SimpleNoNameFix
	{
		#region 实现基本的方方法
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Sys_Operator";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "操做符号";				
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
        /// 操做符号
		/// </summary> 
		public Operator(){}		 
		/// <summary>
		/// 操做符号
		/// </summary>
		/// <param name="_No"></param>
		public Operator(string _No ): base(_No){}
		#endregion 
	}
	 /// <summary>
	 /// 操做符号s
	 /// </summary>
	public class Operators :SimpleNoNameFixs
	{
		/// <summary>
		/// 操做符号
		/// </summary>
		public Operators(){}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Operator();
			}
		}
	}
}
