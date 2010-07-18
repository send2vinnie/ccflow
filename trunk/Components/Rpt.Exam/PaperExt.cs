using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 试卷
	/// </summary>
	public class PaperExt :SimpleNoNameFix
	{

		#region 实现基本的方方法
		/// <summary>
		/// 物理表
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "V_GTS_Paper";
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "试卷";
			}
		}
		#endregion 

		#region 构造方法
		public PaperExt()
		{
		}
		public PaperExt(string _No ):base(_No)
		{
		}
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class PaperExts :SimpleNoNameFixs
	{
		/// <summary>
		///  集合
		/// </summary>
		public PaperExts()
		{
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperExt();
			}
		}
	}
}
