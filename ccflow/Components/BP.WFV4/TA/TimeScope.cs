using System;
using System.Collections;
using BP.DA;
using BP.En;


namespace BP.TA
{
	/// <summary>
	/// 时间段
	/// </summary>
	public class TimeScope :SimpleNoNameFix
	{
		#region 实现基本的方法
		/// <summary>
		/// PhysicsTable
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "TA_TimeScope";
			}
		}
		/// <summary>
		/// Desc
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "时间段";
			}
		}
		#endregion 

		#region 构造方法
		 
		/// <summary>
		/// 时间段
		/// </summary>
		public TimeScope(){}
		 
		/// <summary>
		/// 时间段
		/// </summary>
		/// <param name="_No">编号</param>
		public TimeScope(string _No ): base(_No){}
		#endregion 
	}
	 
	/// <summary>
	/// 时间段s
	/// </summary>
	public class TimeScopes :SimpleNoNameFixs
	{
		#region 构造
		/// <summary>
		/// 时间段s
		/// </summary>
		public TimeScopes(){}
		/// <summary>
		/// 时间段
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TimeScope();
			}
		}
		#endregion
	}
}
