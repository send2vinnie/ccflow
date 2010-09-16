using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 选择题考试attr
	/// </summary>
	public class PaperExamThemeBaseAttr:EntityOIDAttr
	{
		/// <summary>
		/// 试卷
		/// </summary>
		public const string FK_Paper="FK_Paper";
		/// <summary>
		/// 学生
		/// </summary>
		public const string FK_Emp="FK_Emp";

	}
	/// <summary>
	/// 考试题存储基类
	/// </summary>
	abstract public class PaperExamThemeBase :Entity
	{
		#region 实现基本的方法
		/// <summary>
		/// uac
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
				return uc;
			}
		}
 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 考试题存储基类
		/// </summary> 
		public PaperExamThemeBase()
		{
		}

		 
		 
		#endregion 

		#region 考试题存储基类
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(PaperExamThemeBaseAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(PaperExamThemeBaseAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// paper
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperExamThemeBaseAttr.FK_Paper);
			}
			set
			{
				this.SetValByKey(PaperExamThemeBaseAttr.FK_Paper,value);
			}
		}
		#endregion
	}
	/// <summary>
	///  选择题考试
	/// </summary>
	abstract public class PaperExamThemeBases :EntitiesOID
	{
		/// <summary>
		/// PaperExamThemeBases
		/// </summary>
		public PaperExamThemeBases(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public PaperExamThemeBases(string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(PaperExamThemeBaseAttr.FK_Emp,empNo);
			qo.DoQuery();
		}
		 
	}
}
