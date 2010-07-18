using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 多项选择题设计
	/// </summary>
	public class WorkVSBaseAttr  
	{
		#region 基本属性
		/// <summary>
		/// 作业
		/// </summary>
		public const  string FK_Work="FK_Work";
		/// <summary>
		/// 分数
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// 多项选择题设计 的摘要说明。
	/// </summary>
	abstract public class WorkVSBase :Entity
	{
		#region 基本属性
		/// <summary>
		///设卷
		/// </summary>
		public string FK_Work
		{
			get
			{
				return this.GetValStringByKey(WorkVSBaseAttr.FK_Work);
			}
			set
			{
				SetValByKey(WorkVSBaseAttr.FK_Work,value);
			}
		}
		/// <summary>
		/// 分数
		/// </summary>
		public decimal Cent
		{
			get
			{
				return this.GetValDecimalByKey(WorkVSBaseAttr.Cent);
			}
			set
			{
				SetValByKey(WorkVSBaseAttr.Cent,value);
			}
		}
		#endregion

		 

		#region 构造函数
		/// <summary>
		/// 工作人员岗位
		/// </summary> 
		public WorkVSBase()
		{
		}
		#endregion

		#region 重载基类方法
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForAppAdmin();
				return uac;
				//return base.HisUAC;
			}
		}


		#endregion 
	
	}
	/// <summary>
	/// 多项选择题设计 
	/// </summary>
	abstract public class WorkVSBases : Entities
	{
		#region 构造
		/// <summary>
		/// 多项选择题设计
		/// </summary>
		public WorkVSBases(){}
		#endregion

		#region 查询方法
		#endregion
	}
	
}
