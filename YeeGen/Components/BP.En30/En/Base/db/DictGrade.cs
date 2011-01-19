using System;
using System.Collections;

namespace BP.En.Base
{
	/// <summary>
	/// 属性
	/// </summary>
	public class DictGradeAttr:DictAttr
	{
		/// <summary>
		/// 级别
		/// </summary>
		public const string Grade="Grade";
		/// <summary>
		/// 是不是明晰
		/// </summary>
		public const string IsDetal="IsDetal";

	}
	/// <summary>
	/// GradeDictEntity 的摘要说明。
	/// </summary>
	abstract public class DictGrade:Dict
	{
		public DictGrade()
		{			
		}
		public DictGrade(int oid)  : base(oid){}
		
		/// <summary>
		/// 级别
		/// </summary>
		public int Grade
		{
			get
			{
				return this.GetValIntByKey(DictGradeAttr.Grade);
			}
			set
			{
				this.SetValByKey(DictGradeAttr.Grade,value);
			}
		}
		/// <summary>
		/// 是不是明晰
		/// </summary>
		public bool IsDetal
		{
			get
			{
				return this.GetValBooleanByKey(DictGradeAttr.IsDetal);
			}
			set
			{
				this.SetValByKey(DictGradeAttr.IsDetal,value);
			}

		}

	}
	abstract public class DictGrades: Dicts
	{
		public DictGrades()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
