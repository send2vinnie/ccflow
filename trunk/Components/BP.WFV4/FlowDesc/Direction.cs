using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.WF
{
	/// <summary>
	/// 节点方向属性	  
	/// </summary>
	public class DirectionAttr
	{
		/// <summary>
		/// 节点
		/// </summary>
		public const string Node="Node";
		/// <summary>
		/// 转向的节点
		/// </summary>
		public const string ToNode="ToNode";
	}
	/// <summary>
	/// 节点方向
	/// 节点的方向有两部分组成.
	/// 1, Node.
	/// 2, toNode.
	/// 记录了从一个节点到其他的多个节点.
	/// 也记录了到这个节点的其他的节点.
	/// </summary>
	public class Direction :Entity
	{
		#region 基本属性
		/// <summary>
		///节点
		/// </summary>
        public int Node
        {
            get
            {
                return this.GetValIntByKey(DirectionAttr.Node);
            }
            set
            {
                this.SetValByKey(DirectionAttr.Node, value);
            }
        }
		/// <summary>
		/// 转向的节点
		/// </summary>
		public int  ToNode
		{
			get
			{
				return this.GetValIntByKey(DirectionAttr.ToNode);
			}
			set
			{
				this.SetValByKey(DirectionAttr.ToNode,value);
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 节点方向
		/// </summary>
		public Direction(){}
		/// <summary>
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("WF_Direction");				 
				map.EnDesc="节点方向信息";
				map.AddTBIntPK( DirectionAttr.Node,0,"FromNode",false,true);
				map.AddTBIntPK( DirectionAttr.ToNode,0,"ToNode",false,true);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

	}
	 /// <summary>
	 /// 节点方向
	 /// </summary>
	public class Directions :En.EntitiesNoName
	{
		/// <summary>
		/// 节点方向
		/// </summary>
		public Directions(){}
		/// <summary>
		/// 节点方向
		/// </summary>
		/// <param name="NodeID">节点ID</param>
		public Directions(int NodeID)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DirectionAttr.Node,NodeID);
		    qo.DoQuery();			
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Direction();
			}
		}
		/// <summary>
		/// 此节点的转向方向集合
		/// </summary>
		/// <param name="nodeID">此节点的ID</param>
		/// <param name="isLifecyle">是不是判断在节点的生存期内</param>		 
		/// <returns>转向方向集合(ToNodes)</returns> 
		public Nodes GetHisToNodes(int nodeID, bool isLifecyle)
		{
			Nodes nds = new Nodes();
			QueryObject qo = new QueryObject(nds);
			qo.AddWhereInSQL(NodeAttr.NodeID,"SELECT ToNode FROM WF_Direction WHERE Node="+nodeID );
			qo.DoQuery();
			return nds;

			/*
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DirectionAttr.Node,nodeID);
			qo.DoQuery();

			Nodes ens = new Nodes();
			foreach(Direction en in this)
			{
				Node nd = new Node(en.ToNode);
				//				if (isLifecyle==false)
				//				{
				ens.AddEntity(nd);
				//				}					 
				//				else
				//				{
				//					if (nd.IsInLifeCycle)
				//						ens.AddEntity(nd);
				//				}
			}
			return ens;
			*/
		}
		/// <summary>
		/// 转向此节点的集合的Nodes
		/// </summary>
		/// <param name="nodeID">此节点的ID</param>
		/// <returns>转向此节点的集合的Nodes (FromNodes)</returns> 
		public Nodes GetHisFromNodes(int nodeID)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DirectionAttr.ToNode,nodeID);
			qo.DoQuery();
			Nodes ens = new Nodes();
			foreach(Direction en in this)
			{
				ens.AddEntity( new Node(en.Node) ) ;
			}
			return ens;
		}
		 
	}
}
