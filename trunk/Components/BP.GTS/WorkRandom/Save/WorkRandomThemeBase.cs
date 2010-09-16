using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	 
	/// <summary>
	/// 作业
	/// </summary>
	public class WorkRandomThemeBaseAttr
	{
		/// <summary>
		/// 作业
		/// </summary>
		public const string FK_WorkRandom="FK_WorkRandom";
		/// <summary>
		/// 学生
		/// </summary>
		public const string FK_Emp="FK_Emp";

	}
	/// <summary>
	/// 考试题存储基类
	/// </summary>
	abstract public class WorkRandomThemeBase :Entity
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
		public WorkRandomThemeBase()
		{
		}
		#endregion 

		#region 考试题存储基类
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(WorkRandomThemeBaseAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(WorkRandomThemeBaseAttr.FK_Emp,value);
			}
		}
		/// <summary>
		/// Work
		/// </summary>
		public string FK_WorkRandom
		{
			get
			{
				return this.GetValStringByKey(WorkRandomThemeBaseAttr.FK_WorkRandom);
			}
			set
			{
				this.SetValByKey(WorkRandomThemeBaseAttr.FK_WorkRandom,value);
			}
		}
		#endregion
	}
	/// <summary>
	///   
	/// </summary>
	abstract public class WorkRandomThemeBases :EntitiesOID
	{
		/// <summary>
		/// WorkRandomThemeBases
		/// </summary>
		public WorkRandomThemeBases(){}
		/// <summary>
		/// empid
		/// </summary>
		/// <param name="empid"></param>
		public WorkRandomThemeBases(string empNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(WorkRandomThemeBaseAttr.FK_Emp,empNo);
			qo.DoQuery();
		}
		 
	}
}
