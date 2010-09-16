using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 会计期间
	/// </summary>
	public class ThemeType :SimpleNoNameFix
	{
		/// <summary>
		/// 单项选择题
		/// </summary>
		public const string ChoseOne="ChoseOne";
		/// <summary>
		/// 多项选择题
		/// </summary>
		public const string ChoseM="ChoseM";
		/// <summary>
		/// 判断题
		/// </summary>
		public const string JudgeTheme="JudgeTheme";
		/// <summary>
		/// 填空题
		/// </summary>
		public const string FillBlank="FillBlank";
		/// <summary>
		/// 问答题
		/// </summary>
		public const string EssayQuestion="EssayQuestion";
		/// <summary>
		/// 阅读理解题目
		/// </summary>
		public const string RC="RC";


		#region 实现基本的方方法
		/// <summary>
		/// 物理表
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "GTS_ThemeType";
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public override string  Desc
		{
			get
			{
				return "题目类型";
			}
		}
		#endregion 

		#region 构造方法
		public ThemeType()
		{
		}
		public ThemeType(string _No ):base(_No)
		{
		}
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class ThemeTypes :SimpleNoNameFixs
	{
		/// <summary>
		/// 会计期间集合
		/// </summary>
		public ThemeTypes()
		{
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ThemeType();
			}
		}
	}
}
