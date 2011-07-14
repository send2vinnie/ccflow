using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
//using BP.ZHZS.Base;

namespace BP.WF
{
    /// <summary>
    /// 流程岗位属性属性	  
    /// </summary>
    public class NodeReturnAttr
    {
        /// <summary>
        /// 节点
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 工作节点
        /// </summary>
        public const string ReturnN = "ReturnN";
    }
    /// <summary>
    /// 流程岗位属性
    /// 节点的工作节点有两部分组成.	 
    /// 记录了从一个节点到其他的多个节点.
    /// 也记录了到这个节点的其他的节点.
    /// </summary>
    public class NodeReturn : EntityMM
    {
        #region 基本属性
        /// <summary>
        /// HisUAC
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        ///节点
        /// </summary>
        public int ReturnN
        {
            get
            {
                return this.GetValIntByKey(NodeReturnAttr.ReturnN);
            }
            set
            {
                this.SetValByKey(NodeReturnAttr.ReturnN, value);
            }
        }
        /// <summary>
        /// 工作流程
        /// </summary>
        public string FK_Node
        {
            get
            {
                return this.GetValStringByKey(NodeReturnAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(NodeReturnAttr.FK_Node, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 流程岗位属性
        /// </summary>
        public NodeReturn() { }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_NodeReturn");
                map.EnDesc = "可退回的节点";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(NodeReturnAttr.FK_Node, null, "流程编号", true, true, 1, 20, 20);
                map.AddDDLEntitiesPK(NodeReturnAttr.ReturnN, null, "节点", new NodeExts(), true);
                this._enMap = map;
                
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 可退回的节点
    /// </summary>
    public class NodeReturns : EntitiesMM
    {
        /// <summary>
        /// 他的工作节点
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (NodeReturn ns in this)
                {
                    ens.AddEntity(new Node(ns.ReturnN));
                }
                return ens;
            }
        }
        /// <summary>
        /// 可退回的节点
        /// </summary>
        public NodeReturns() { }
        /// <summary>
        /// 可退回的节点
        /// </summary>
        /// <param name="NodeID">节点ID</param>
        public NodeReturns(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.FK_Node, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// 可退回的节点
        /// </summary>
        /// <param name="NodeNo">NodeNo </param>
        public NodeReturns(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.ReturnN, NodeNo);
            qo.DoQuery();
        }
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeReturn();
            }
        }
        /// <summary>
        /// 可退回的节点s
        /// </summary>
        /// <param name="sts">可退回的节点</param>
        /// <returns></returns>
        public Nodes GetHisNodes(Nodes sts)
        {
            Nodes nds = new Nodes();
            Nodes tmp = new Nodes();
            foreach (Node st in sts)
            {
                tmp = this.GetHisNodes(st.No);
                foreach (Node nd in tmp)
                {
                    if (nds.Contains(nd))
                        continue;
                    nds.AddEntity(nd);
                }
            }
            return nds;
        }
        /// <summary>
        /// 可退回的节点
        /// </summary>
        /// <param name="NodeNo">工作节点编号</param>
        /// <returns>节点s</returns>
        public Nodes GetHisNodes(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.ReturnN, NodeNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeReturn en in this)
            {
                ens.AddEntity(new Node(en.FK_Node));
            }
            return ens;
        }
        /// <summary>
        /// 转向此节点的集合的Nodes
        /// </summary>
        /// <param name="nodeID">此节点的ID</param>
        /// <returns>转向此节点的集合的Nodes (FromNodes)</returns> 
        public Nodes GetHisNodes(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeReturnAttr.FK_Node, nodeID);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeReturn en in this)
            {
                ens.AddEntity(new Node(en.ReturnN));
            }
            return ens;
        }
    }
}
