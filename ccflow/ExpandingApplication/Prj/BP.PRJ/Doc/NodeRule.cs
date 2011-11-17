using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
using BP.WF;

namespace BP.PRJ
{
    /// <summary>
    /// 流程岗位属性属性	  
    /// </summary>
    public class NodeRuleAttr
    {
        /// <summary>
        /// 节点
        /// </summary>
        public const string FK_Rule = "FK_Rule";
        /// <summary>
        /// 工作节点
        /// </summary>
        public const string FK_Node = "FK_Node";
    }
    /// <summary>
    /// 流程岗位属性
    /// 节点的工作节点有两部分组成.	 
    /// 记录了从一个节点到其他的多个节点.
    /// 也记录了到这个节点的其他的节点.
    /// </summary>
    public class NodeRule : EntityMM
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
        public string FK_Node
        {
            get
            {
                return this.GetValStringByKey(NodeRuleAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(NodeRuleAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// 工作流程
        /// </summary>
        public string FK_Rule
        {
            get
            {
                return this.GetValStringByKey(NodeRuleAttr.FK_Rule);
            }
            set
            {
                this.SetValByKey(NodeRuleAttr.FK_Rule, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 流程岗位属性
        /// </summary>
        public NodeRule() { }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Prj_NodeRule");
                map.EnDesc = "上传规则";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(NodeRuleAttr.FK_Rule, null, "上传规则", true, true, 1, 20, 20);
                map.AddTBStringPK(NodeRuleAttr.FK_Node, null, "节点", true, true, 1, 20, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 流程抄送节点
    /// </summary>
    public class NodeRules : EntitiesMM
    {
        /// <summary>
        /// 他的工作节点
        /// </summary>
        public Rules HisNodes
        {
            get
            {
                Rules ens = new Rules();
                foreach (NodeRule ns in this)
                {
                    ens.AddEntity(new Rule(ns.FK_Node));
                }
                return ens;
            }
        }
        /// <summary>
        /// 流程抄送节点
        /// </summary>
        public NodeRules() { }
        /// <summary>
        /// 流程抄送节点
        /// </summary>
        /// <param name="NodeID">节点ID</param>
        public NodeRules(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeRuleAttr.FK_Rule, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// 流程抄送节点
        /// </summary>
        /// <param name="NodeNo">NodeNo </param>
        public NodeRules(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeRuleAttr.FK_Node, NodeNo);
            qo.DoQuery();
        }
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeRule();
            }
        }
        /// <summary>
        /// 流程抄送节点
        /// </summary>
        /// <param name="NodeNo">工作节点编号</param>
        /// <returns>节点s</returns>
        public Nodes GetHisNodes(string NodeNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeRuleAttr.FK_Node, NodeNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeRule en in this)
            {
                ens.AddEntity(new Node(en.FK_Rule));
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
            qo.AddWhere(NodeRuleAttr.FK_Rule, nodeID);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeRule en in this)
                ens.AddEntity(new Node(en.FK_Node));

            return ens;
        }
    }
}
