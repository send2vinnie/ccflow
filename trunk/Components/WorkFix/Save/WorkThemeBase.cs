using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	 
	/// <summary>
	/// 作业
	/// </summary>
	public class WorkThemeBaseAttr
	{
		/// <summary>
		/// 作业
		/// </summary>
		public const string FK_Work="FK_Work";
		/// <summary>
		/// 学生
		/// </summary>
		public const string FK_Emp="FK_Emp";

	}
	/// <summary>
	/// 考试题存储基类
	/// </summary>
	abstract public class WorkThemeBase :Entity
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
		public WorkThemeBase()
		{
		}
		#endregion 

		#region 考试题存储基类
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(WorkThemeBaseAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(WorkThemeBaseAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// Work
		/// </summary>
		public string FK_Work
		{
			get
			{
				return this.GetValStringByKey(WorkThemeBaseAttr.FK_Work);
			}
			set
			{
				this.SetValByKey(WorkThemeBaseAttr.FK_Work,value);
			}
		}
		#endregion
	}
	/// <summary>
	///   
	/// </summary>
	abstract public class WorkThemeBases :EntitiesOID
	{
		/// <summary>
		/// WorkThemeBases
		/// </summary>
		public WorkThemeBases(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkThemeBases(string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkThemeBaseAttr.FK_Emp,empNo);
			qo.DoQuery();
		}
		 
	}
}
