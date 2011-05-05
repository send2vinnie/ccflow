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
	/// 条件属性
	/// </summary>
	public class DirectionConditionAttr:ConditionAttr
	{
		/// <summary>
		/// 节点
		/// </summary>
		public const string NodeID="NodeID";
		/// <summary>
		/// 要转到的节点
		/// </summary>
		public const string ToNodeID="ToNodeID";
		/// <summary>
		/// 组Key
		/// </summary>
		public const string Groupkey="Groupkey";		
	}
	/// <summary>
	/// 节点方向条件
	/// </summary>
	public class DirectionCondition :Condition
	{
		#region 逻辑处理
        protected override string PhysicsTable
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
        protected override string Desc
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
		/// <summary>
		/// 在更新与插入之前要做得操作。
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdateInsertAction()
		{
			return base.beforeUpdateInsertAction ();
		}
		#endregion 

		#region 基本属性
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey("MyPK");
            }
            set
            {
                this.SetValByKey("MyPK", value);
            }
        }
		/// <summary>
		/// 节点信息
		/// </summary>
		public int NodeID
		{
			get
			{
			   return this.GetValIntByKey(DirectionConditionAttr.NodeID);
			}
			set
			{
				this.SetValByKey(DirectionConditionAttr.NodeID,value);
			}
		}
		/// <summary>
		/// 要转向的节点
		/// </summary>
		public int ToNodeID
		{
			get
			{
				return this.GetValIntByKey(DirectionConditionAttr.ToNodeID);
			}
			set
			{
				this.SetValByKey(DirectionConditionAttr.ToNodeID,value);
			}
		}
		#endregion 

		#region 实现基本的方方法
		 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 节点方向条件
		/// </summary>
		public DirectionCondition()
		{
		}
		/// <summary>
		/// 节点方向条件
		/// </summary>
		/// <param name="nodeID">节点信息</param>
		/// <param name="toNodeID">要转向的节点</param>		 
		public DirectionCondition(int nodeID, int toNodeID)
		{
            this.NodeID = nodeID;
            this.ToNodeID = toNodeID;
            this.MyPK = nodeID + "_" + toNodeID;
            this.Retrieve();
		}
		/// <summary>
		/// 属性
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("WF_DirectionCondition");
				map.EnDesc="转向条件";

                map.AddMyPK();
				map.AddTBInt(DirectionConditionAttr.NodeID,0,"节点",true,true);
				map.AddTBInt(DirectionConditionAttr.ToNodeID,0,"转向节点",true,true);


                map.AddTBInt(DirectionConditionAttr.FK_Node, 0, "节点", true, true);
                map.AddTBInt(DirectionConditionAttr.FK_Attr, 0, "属性OID", true, true);
				map.AddTBString(DirectionConditionAttr.AttrKey,null,"属性值",true,true,0,60,20);
				map.AddTBString(DirectionConditionAttr.FK_Operator,null,"运算符号",true,true,0,60,20); 
				map.AddTBString(DirectionConditionAttr.OperatorValue,null,"要运算的值",true,true,0,60,20);

				this._enMap=map;
				return this._enMap;
			}
		}
        protected override bool beforeUpdate()
        {
            this.MyPK = this.NodeID + "_" + this.ToNodeID;
            return base.beforeUpdate();
        }
        protected override bool beforeInsert()
        {
            this.MyPK = this.NodeID + "_" + this.ToNodeID;
            return base.beforeInsert();
        }
		#endregion
	 
	}
	/// <summary>
	/// 节点方向条件s
	/// </summary>
	public class DirectionConditions :Conditions
	{
		#region 基本属性
		private int  fromNode=0;
		private int  toNode=0;
		#endregion 

		#region 构造
		/// <summary>
		/// 节点方向条件
		/// </summary>
        public DirectionConditions() { }
		/// <summary>
		/// 节点方向条件
		/// </summary>
		/// <param name="fromNode">从节点</param>
		/// <param name="toNode">到节点</param>
		/// <param name="operatorPK">操作的PK</param>
        public DirectionConditions(int fromNode, int toNode)
        {
            this.fromNode = fromNode;
            this.toNode = toNode;

            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DirectionConditionAttr.NodeID, fromNode);
            qo.addAnd();
            qo.AddWhere(DirectionConditionAttr.ToNodeID, toNode);
            qo.DoQuery();
        }
		#endregion

		#region 重写基类的方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				DirectionCondition en = new DirectionCondition();
				en.ToNodeID=this.toNode ;
				en.NodeID = this.fromNode;
                en.MyPK = this.toNode + "_" + this.fromNode;
				return en;
			}
		}
		#endregion
	}
}
