
using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Sys;
//using BP.ZHZS.Base;

namespace BP.WF
{	 
	/// <summary>
	/// 节点完成任务的条件
	/// </summary>
    public class ConditionNodeAttr : ConditionAttr
    {
        /// <summary>
        /// 节点
        /// </summary>
        public const string NodeID = "NodeID";


      
    }
	/// <summary>
	/// 有节点的 节点完成任务条件
	/// </summary>
	abstract public class ConditionNode : Condition
	{
		#region 基本的属性
		#endregion 

		#region 扩展属性
		/// <summary>
		/// 他的节点
		/// </summary>
		public Node HisNode
		{
			get
			{
				return new Node(this.NodeID);
			}
		}		
		#endregion 

		
		#region 构造方法
		/// <summary>
		/// 节点完成任务条件
		/// </summary>
		public ConditionNode(){}

        public ConditionNode(int mainID)
        {
            this.NodeID = mainID;
            this.Retrieve();
        }
		/// <summary>
		/// 属性
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map(this.PhysicsTable);
                map.EnDesc = this.Desc;
                map.AddTBIntPK(ConditionNodeAttr.NodeID, 0, "MainID", true, true);
                map.AddTBInt(ConditionNodeAttr.FK_Node, 0, "节点ID", true, true);
                map.AddTBInt(ConditionNodeAttr.FK_Attr, 0, "属性", true, true);
                map.AddTBString(ConditionNodeAttr.AttrKey, null, "属性", true, true, 1, 60, 20);

                map.AddTBString(ConditionNodeAttr.FK_Operator, "=", "运算符号", true, true, 1, 60, 20);
                map.AddTBString(ConditionNodeAttr.OperatorValue, "", "要运算的值", true, true, 1, 60, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion

		#region 公共方法
		
		#endregion
	}
	/// <summary>
	/// 节点完成任务条件s
	/// 一般来说他只有一个条件.
	/// 条件与条件之间是 or  关系.
	/// 如果有多个条件,满足,任意一个条件就通过.
	/// </summary>
	abstract public class ConditionsNode :Conditions
	{
		/// <summary>
		/// nodeid
		/// </summary>
	    protected int NodeId=0;

		#region 构造
		/// <summary>
		/// 节点完成任务条件
		/// </summary>
		public ConditionsNode(){}
		/// <summary>
		/// 节点完成任务条件集合
		/// </summary>
		/// <param name="nodeID">从节点</param>		
		public ConditionsNode(int nodeID)
		{
			this.NodeId=nodeID;
			QueryObject qo = new QueryObject(this);
            qo.AddWhere(ConditionNodeAttr.NodeID, nodeID);
			qo.DoQuery();
		}
		#endregion

		#region 实现基类的方法
	 
		#endregion
	}
}
