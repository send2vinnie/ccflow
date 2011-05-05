
using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Tax;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// 管理机关开始工作节点属性
	/// </summary>
	public class StartWorkOfZSJGAttr: StartWorkAttr
	{
		/// <summary>
		/// 管理机关编号
		/// </summary>
		public const string FK_Dept="FK_Dept";	 
	 
	}
	/// <summary>	 
	/// 管理机关开始节点,具有管理机关的属性的开始节点.
	/// </summary>
	abstract public class StartWorkOfZSJG : StartWork 
	{		 
		#region 基本属性
		/// <summary>
		/// 管理机关
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey(StartWorkOfZSJGAttr.FK_Dept);				
			}
			set
			{
				this.SetValByKey(StartWorkOfZSJGAttr.FK_Dept,value);
			}
		}
		/// <summary>
		/// 管理机关名称
		/// </summary>
		public string FK_DeptName
		{
			get
			{
				return this.GetValRefTextByKey(StartWorkOfZSJGAttr.FK_Dept);				
			}
		}
		#endregion		  		

		#region 扩展属性
		/// <summary>
		/// 纳税人
		/// </summary>
		public ZSJG HisZSJG
		{
			get
			{
				return new ZSJG(this.FK_Dept);
			}
		}		 
		#endregion 

		#region 构造函数
		/// <summary>
		/// 工作流程
		/// </summary>
		protected StartWorkOfZSJG()
		{
		}
			 
		#endregion

		#region 重写
		/// <summary>
		/// 在插入于更新前找到纳税人对应的部门.
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdateInsertAction()
		{
			return base.beforeUpdateInsertAction();
		}
		/// <summary>
		/// 更新之前的操作
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			return base.beforeUpdate();
		}
		#endregion

	}
	/// <summary>
	/// 工作流程采集信息的基类 集合
	/// </summary>
	abstract public class StartWorkOfZSJGs : StartWorks
	{
		#region 构造方法
		/// <summary>
		/// 信息采集基类
		/// </summary>
		public StartWorkOfZSJGs()
		{
		}
		#endregion		 
	}
}
