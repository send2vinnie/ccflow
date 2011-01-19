using System;
using BP.En;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP;
 

namespace BP.En
{
	/// <summary>
	/// 多语言实体属性
	/// </summary>
	public class IEnGradeAttr:IEnAttr
	{
		/// <summary>
		/// 是否明细
		/// </summary>
		public const string IsDetail = "IsDetail";
		/// <summary>
		/// Grade
		/// </summary>
		public const string Grade="Grade";		
	}
	/// <summary>
	/// 多语言的实体
	/// </summary>
	[Serializable]
	abstract public class IEnGrade : IEn
	{
		#region 基本属性
		/// <summary>
		/// 编号
		/// </summary>
		public int Grade
		{
			get
			{
				return this.GetValIntByKey(IEnGradeAttr.Grade) ; 
			}
			set
			{
				this.SetValByKey(IEnGradeAttr.Grade,value);
			}
		}
		/// <summary>
		/// 是否明细
		/// </summary>
		public bool  IsDetail
		{
			get
			{
				return this.GetValBooleanByKey(IEnGradeAttr.IsDetail);
			}
			set
			{
				this.SetValByKey(IEnGradeAttr.IsDetail,value);
			}
		}
		#endregion		
 
		#region 构造
		protected IEnGrade(){}
		protected IEnGrade(int oid) : base(oid){}
		protected IEnGrade(string no, string langugae ) : base(no,langugae){}
		#endregion
			
		
	}
	/// <summary>
	/// 多语言的实体集合
	/// </summary>
	[Serializable]
	abstract public  class IEnsGrade : IEns
	{
		#region 构造函数
		public IEnsGrade( string fk_language) :base( fk_language){}
		/// <summary>
		/// 构造函数
		/// </summary>
		public IEnsGrade(){}
		#endregion
	}

}
