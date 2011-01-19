using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// 年月日
	/// </summary>
	public class NYR :SimpleNoNameFix
	{
		#region 实现基本的方方法
		/// <summary>
		/// 物理表
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_NYR";
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public override string  Desc
		{
			get
			{
                return this.ToE("NYR", "日期"); // "日期";
			}
		}
		#endregion 

		#region 构造方法
		 
		public NYR(){}
		 
		public NYR(string _No ): base(_No){}
		
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class NYRs :SimpleNoNameFixs
	{
		/// <summary>
		/// 年月日集合
		/// </summary>
		public NYRs()
		{
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new NY();
			}
		}
		 
		 

	}
}
