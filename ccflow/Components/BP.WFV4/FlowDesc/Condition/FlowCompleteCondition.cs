
using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.WF
{	 
	/// <summary>
	/// 流程完成流程的条件
	/// </summary>
	public class FlowCompleteConditionAttr : ConditionAttr
	{
		/// <summary>
		/// 节点
		/// </summary>
		public const string NodeID="NodeID";
	}
	/// <summary>
	/// 有节点的 节点完成任务条件
	/// </summary>
	public class FlowCompleteCondition :Condition
	{
		#region 扩展属性
		 
		#endregion 

		#region 实现基类的方法
		/// <summary>
		/// 描述
		/// </summary>
		protected override string Desc
		{
			get
			{
				return "流程完成条件";
			}
		}
		/// <summary>
		/// 物理表
		/// </summary>
		protected override string PhysicsTable
		{
			get
			{
			   
				return "WF_FlowCompleteCondition";
			}
		} 
		#endregion

		#region 构造方法
		/// <summary>
		/// 节点完成任务条件
		/// </summary>
		public FlowCompleteCondition(){}
	    
		 
		#endregion

		
	}
	/// <summary>
	/// 流程完成流程的条件
	/// </summary>
	/// <summary>
	/// 节点完成流程条件s
	/// 一般来说他只有一个条件.
	/// 条件与条件之间是 or  关系.
	/// 如果有多个条件,满足,任意一个条件就通过.
	/// </summary>
	public class FlowCompleteConditions :Conditions
	{
		#region 构造
		/// <summary>
		/// 节点完成流程条件
		/// </summary>
		public FlowCompleteConditions(){}
		/// <summary>
		/// 节点完成流程条件集合
		/// </summary>
		/// <param name="nodeID">从节点</param>	
		public FlowCompleteConditions(int nodeID ): base(nodeID){}		 
		#endregion

		#region 实现基类的方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				FlowCompleteCondition  en = new FlowCompleteCondition();
				en.NodeID=this.NodeID;
				return en;
			}			 
		}
		#endregion
	}
}
