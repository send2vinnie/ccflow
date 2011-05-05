using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;
//using BP.ZHZS.DS;


namespace BP.WF
{

	/// <summary>
	/// 节点类型
	/// </summary>
	public enum FlowCheckNodeType
	{
		/// <summary>
		/// 标准类型
		/// </summary>
		GECheckStand,
		/// <summary>
		/// 数量审核
		/// </summary>
		NumCheck
	}
	 
	/// <summary>
	/// 这里存放每个节点的信息.	 
	/// </summary>
	public class FlowCheckNode 
	{
		/// <summary>
		/// wsss
		/// </summary>
		public string EnsName
		{
			get
			{
                if (FlowCheckNodeType == FlowCheckNodeType.GECheckStand)
                    return "BP.WF.GECheckStands";
                else
                    return "BP.WF.NumChecks";
			}
		}
        public string EnName
        {
            get
            {
                if (FlowCheckNodeType == FlowCheckNodeType.GECheckStand)
                    return "BP.WF.GECheckStand";
                else
                    return "BP.WF.NumCheck";
            }
        }
		/// <summary>
		/// 节点ID
		/// </summary>
		public int NodeID;
		/// <summary>
		/// 名称
		/// </summary>
		public string Name;
		/// <summary>
		/// ddd
		/// </summary>
		public FlowCheckNodeType FlowCheckNodeType=FlowCheckNodeType.GECheckStand;
	}
	/// <summary>
	/// 节点集合
	/// </summary>
	public class FlowCheckNodes : System.Collections.CollectionBase
	{

		#region 构造方法
		/// <summary>
		/// 节点集合
		/// </summary>
		public FlowCheckNodes()
		{		
		}		 
		#endregion

		#region 
		/// <summary>
		/// 根据位置取得数据
		/// </summary>
		public FlowCheckNode this[int index]
		{
			get 
			{
				return (FlowCheckNode)this.InnerList[index];
			}
		}
		/// <summary>
		/// 将对象添加到集合尾处，如果对象已经存在，则不添加
		/// </summary>
		/// <param name="flowNode">要添加的对象</param>
		/// <returns>返回添加到的地方</returns>
		public virtual int Add(FlowCheckNode flowNode)
		{
			return this.InnerList.Add(flowNode);
		}
		/// <summary>
		/// 增加一个属性
		/// </summary>
		/// <param name="NodeID">节点ID</param>
		/// <param name="NodeName">节点名称</param>
		/// <param name="type">FlowCheckNodeType</param>
		public void  Add( int NodeID,string NodeName, FlowCheckNodeType type )
		{
			FlowCheckNode fcn = new FlowCheckNode();
			fcn.Name=NodeName;
			fcn.NodeID=NodeID;
			fcn.FlowCheckNodeType = type;
			this.Add(fcn);
		}					
		#endregion 
	}
	
}
