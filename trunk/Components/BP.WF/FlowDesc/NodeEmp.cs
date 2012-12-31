using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.WF.Port;
//using BP.ZHZS.Base;

namespace BP.WF
{
	/// <summary>
	/// 节点到人员属性
	/// </summary>
	public class NodeEmpAttr
	{
		/// <summary>
		/// 节点
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// 到人员
		/// </summary>
		public const string FK_Emp="FK_Emp";
	}
	/// <summary>
	/// 节点到人员
	/// 节点的到人员有两部分组成.	 
	/// 记录了从一个节点到其他的多个节点.
	/// 也记录了到这个节点的其他的节点.
	/// </summary>
	public class NodeEmp :EntityMM
	{
		#region 基本属性
		/// <summary>
		///节点
		/// </summary>
		public int  FK_Node
		{
			get
			{
				return this.GetValIntByKey(NodeEmpAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(NodeEmpAttr.FK_Node,value);
			}
		}
		/// <summary>
		/// 到人员
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(NodeEmpAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(NodeEmpAttr.FK_Emp,value);
			}
		}
        public string FK_EmpT
        {
            get
            {
                return this.GetValRefTextByKey(NodeEmpAttr.FK_Emp);
            }
        }
		#endregion 

		#region 构造方法
		/// <summary>
		/// 节点到人员
		/// </summary>
		public NodeEmp(){}
		/// <summary>
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_NodeEmp");
                map.EnDesc = "节点人员";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.AddDDLEntitiesPK(NodeEmpAttr.FK_Node, null, "到人员", new NodeExts(), true);
                map.AddDDLEntitiesPK(NodeEmpAttr.FK_Emp, null, "到人员", new Emps(), true);

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion
	}
	/// <summary>
	/// 节点到人员
	/// </summary>
    public class NodeEmps : EntitiesMM
    {
        /// <summary>
        /// 他的到人员
        /// </summary>
        public Emps HisEmps
        {
            get
            {
                Emps ens = new Emps();
                foreach (NodeEmp ns in this)
                {
                    ens.AddEntity(new Emp(ns.FK_Emp));
                }
                return ens;
            }
        }
        /// <summary>
        /// 他的工作节点
        /// </summary>
        public Nodes HisNodes
        {
            get
            {
                Nodes ens = new Nodes();
                foreach (NodeEmp ns in this)
                {
                    ens.AddEntity(new Node(ns.FK_Node));
                }
                return ens;

            }
        }
        /// <summary>
        /// 节点到人员
        /// </summary>
        public NodeEmps() { }
        /// <summary>
        /// 节点到人员
        /// </summary>
        /// <param name="NodeID">节点ID</param>
        public NodeEmps(int NodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeEmpAttr.FK_Node, NodeID);
            qo.DoQuery();
        }
        /// <summary>
        /// 节点到人员
        /// </summary>
        /// <param name="EmpNo">EmpNo </param>
        public NodeEmps(string EmpNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeEmpAttr.FK_Emp, EmpNo);
            qo.DoQuery();
        }
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NodeEmp();
            }
        }
        /// <summary>
        /// 取到一个到人员集合能够访问到的节点s
        /// </summary>
        /// <param name="sts">到人员集合</param>
        /// <returns></returns>
        public Nodes GetHisNodes(Emps sts)
        {
            Nodes nds = new Nodes();
            Nodes tmp = new Nodes();
            foreach (Emp st in sts)
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
        /// 到人员对应的节点
        /// </summary>
        /// <param name="EmpNo">到人员编号</param>
        /// <returns>节点s</returns>
        public Nodes GetHisNodes(string EmpNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeEmpAttr.FK_Emp, EmpNo);
            qo.DoQuery();

            Nodes ens = new Nodes();
            foreach (NodeEmp en in this)
            {
                ens.AddEntity(new Node(en.FK_Node));
            }
            return ens;
        }
        /// <summary>
        /// 转向此节点的集合的 Nodes
        /// </summary>
        /// <param name="nodeID">此节点的ID</param>
        /// <returns>转向此节点的集合的Nodes (FromNodes)</returns> 
        public Emps GetHisEmps(int nodeID)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NodeEmpAttr.FK_Node, nodeID);
            qo.DoQuery();

            Emps ens = new Emps();
            foreach (NodeEmp en in this)
            {
                ens.AddEntity(new Emp(en.FK_Emp));
            }
            return ens;
        }
    }
}
