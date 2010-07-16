
using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;

namespace BP.WF
{

	/// <summary>
	/// 开始工作基类属性
	/// </summary>
	public class PCStartWorkAttr:StartWorkAttr
	{ 
	}
	/// <summary>	 
	/// PC开始工作基类,所有开始PC工作都要从这里继承
	/// 这个工作的特点是:
	/// 1, 计算机启动工作.
	/// 2, 
	/// </summary>
	abstract public class PCStartWork:StartWork 
	{

		#region 需要子类实现的方法
		
		#endregion 		 		

		#region 构造函数
		/// <summary>
		/// 工作流程
		/// </summary>
		protected PCStartWork(){}
			 
		#endregion
		
		#region  重写基类的方法。
		 
		/// <summary>
		/// 逻辑处理
		/// </summary>
		/// <returns></returns>
		protected override bool beforeInsert()
		{			 
			base.beforeInsert();
			return true;
		}
		/// <summary>
		/// 逻辑处理
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{			
			base.beforeUpdate();
			return true;
		}
		/// <summary>
		/// 逻辑处理
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete()
		{
			base.beforeDelete();
			return true;
		}
		/// <summary>
		/// 逻辑处理
		/// </summary>
		protected override void afterDelete()
		{
			base.afterDelete();
			return;
		}
		/// <summary>
		/// 逻辑处理,在建立之后要做的工作.
		/// 1, 创建各个流程.
		/// 2, 把这个事务的信息建立到工作流程里面. 
		/// </summary>
		protected override  void afterInsert()
		{
			/*try
			{
				//把这个事务的信息建立到工作流程里面.
				WFInfo wf = new WFInfo();
				wf.WFOID =this.OID;
				wf.Insert();
			}
			catch(Exception ex)
			{
				this.Delete();
				throw new Exception("@创建流程出现错误:"+ex.Message);
			}*/
			base.afterInsert();
			return ;
		}
		/// <summary>
		/// 逻辑处理
		/// </summary>
		protected override void afterUpdate()
		{
			base.afterUpdate();
			return ;
		}
		#endregion

		#region 方法
		 
		#endregion 
		 
	}
	/// <summary>
	/// 工作流程采集信息的基类 集合
	/// </summary>
	abstract public class PCStartWorks : StartWorks
	{
		/// <summary>
		/// 调用自动产生工作流程.
		/// </summary>
		public virtual string AutoGenerWorkFlow()
		{
			return "";
		}	 

		#region 构造方法
		/// <summary>
		/// 信息采集基类
		/// </summary>
		public PCStartWorks()
		{
		}
		#endregion

		 

	
	}
}
