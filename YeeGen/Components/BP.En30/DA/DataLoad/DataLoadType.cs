using System;
using System.Collections;
using BP.DA;
using BP.En;
 

namespace BP.Sys
{	 
	/// <summary>
	/// 装载类型
	/// </summary>
	public class DataLoadType :SimpleNoNameFix
	{
		#region 实现基本的方方法
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Sys_DataLoadType";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "装载类型";				
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 装载类型
		/// </summary> 
		public DataLoadType(){}
		/// <summary>
		/// 装载类型
		/// </summary>
		/// <param name="_No"></param>
		public DataLoadType(string _No ): base(_No){}
		#endregion 
	}
	 /// <summary>
	 /// 装载类型s
	 /// </summary>
	public class DataLoadTypes :SimpleNoNameFixs
	{
		/// <summary>
		/// 操做符号
		/// </summary>
		public DataLoadTypes(){}
		/// <summary>
		/// 装载类型 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new DataLoadType();
			}
		}
	}
}
