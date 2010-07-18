using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 作业
	/// </summary>
	public class WorkExt :SimpleNoNameFix
	{

		#region 实现基本的方方法
		/// <summary>
		/// 物理表
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "V_GTS_Work";
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "作业";
			}
		}
		#endregion 

		#region 构造方法
		public WorkExt()
		{
		}
		public WorkExt(string _No ):base(_No)
		{
		}
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class WorkExts :SimpleNoNameFixs
	{
		/// <summary>
		///  集合
		/// </summary>
		public WorkExts()
		{
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkExt();
			}
		}
	}
}
