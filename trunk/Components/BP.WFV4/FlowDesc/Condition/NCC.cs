
using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.WF
{
	/// <summary>
	/// 节点完成任务的条件
	/// </summary>
	public class NodeCompleteConditionAttr : ConditionAttr
	{
		/// <summary>
		/// 节点
		/// </summary>
		public const string NodeID="NodeID";
	}
	/// <summary>
	/// 有节点的 节点完成任务条件
	/// </summary>
	public class NodeCompleteCondition :Condition
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
				return "节点完成工作的条件";
			}
		}
		/// <summary>
		/// 物理表
		/// </summary>
		protected override string PhysicsTable
		{
			get
			{
				return "WF_NodeCompleteCondition";
			}
		} 
		#endregion

		#region 构造方法
		/// <summary>
		/// 节点完成任务条件
		/// </summary>
		public NodeCompleteCondition(){}
        /// <summary>
        /// 节点完成任务条件
        /// </summary>
        /// <param name="mainNodeID"></param>
        public NodeCompleteCondition(string mypk) : base(mypk) { }
		#endregion
	}
	/// <summary>
	/// 节点完成任务条件s
	/// 一般来说他只有一个条件.
	/// 条件与条件之间是 or  关系.
	/// 如果有多个条件,满足,任意一个条件就通过.
	/// </summary>
	public class NodeCompleteConditions :Conditions
	{
		#region 构造
		/// <summary>
		/// 节点完成任务条件
		/// </summary>
		public NodeCompleteConditions(){}
		/// <summary>
		/// 节点完成任务条件集合
		/// </summary>
		/// <param name="nodeID">从节点</param>		
		public NodeCompleteConditions(int nodeID):base(nodeID){}
		#endregion

		#region 实现基类的方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				NodeCompleteCondition  en = new NodeCompleteCondition();
				en.NodeID=this.NodeID;
				return en;
			}
		}
		#endregion
	}
}
