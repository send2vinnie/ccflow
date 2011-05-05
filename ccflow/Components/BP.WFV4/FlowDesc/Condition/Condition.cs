using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
//using BP.ZHZS.Base;

namespace BP.WF
{	
	/// <summary>
	/// 条件属性
	/// </summary>
    public class ConditionAttr
    {

        /// <summary>
        /// 属性Key
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// 属性Key
        /// </summary>
        public const string AttrKey = "AttrKey";
        /// <summary>
        /// 名称
        /// </summary>
        public const string AttrName = "AttrName";
        /// <summary>
        /// 属性
        /// </summary>
        public const string FK_Attr = "FK_Attr";
        /// <summary>
        /// 运算符号
        /// </summary>
        public const string FK_Operator = "FK_Operator";
        /// <summary>
        /// 运算的值
        /// </summary>
        public const string OperatorValue = "OperatorValue";
        /// <summary>
        /// 操作值
        /// </summary>
        public const string OperatorValueT = "OperatorValueT";
        /// <summary>
        /// Node
        /// </summary>
        public const string FK_Node = "FK_Node";
    }
	/// <summary>
	/// 条件基类
	/// </summary>
	abstract public class Condition :EntityMyPK
	{
        public string ConditionDesc
        {
            get
            {
                return "";
            }
        }
        /// <summary>
        /// 要运算的节点
        /// </summary>
        public Node HisNode
        {
            get
            {
                return new Node(this.NodeID);
            }
        }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(ConditionAttr.NodeID);
            }
            set
            {
                this.SetValByKey(ConditionAttr.NodeID, value);
            }
        }
        public int FK_Node
        {
            get
            {
                int i= this.GetValIntByKey(ConditionAttr.FK_Node);
                if (i == 0)
                    return this.NodeID;
                return i;
            }
            set
            {
                this.SetValByKey(ConditionAttr.FK_Node, value);
            }
        }
        public string FK_NodeT
        {
            get
            {
                Node nd = new Node(this.FK_Node);
                return nd.Name;
            }
        }
        /// <summary>
        /// 属性
        /// </summary>
        public int FK_Attr
        {
            get
            {
                return this.GetValIntByKey(ConditionAttr.FK_Attr);
            }
            set
            {
                this.SetValByKey(ConditionAttr.FK_Attr, value);
            }
        }
        public string FK_AttrT
        {
            get
            {
                BP.Sys.MapAttr attr = new BP.Sys.MapAttr(this.FK_Attr);
                return attr.Name;
            }
        }
		/// <summary>
		/// 在更新与插入之前要做得操作。
		/// </summary>
		/// <returns></returns>
        protected override bool beforeUpdateInsertAction()
        {
            this.RunSQL("UPDATE WF_Node SET IsCCNode=0,IsCCFlow=0");
            this.RunSQL("UPDATE WF_Node SET IsCCNode=1 WHERE NodeID IN (SELECT NodeID FROM WF_NodeCompleteCondition)");
            this.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_FlowCompleteCondition)");
            return base.beforeUpdateInsertAction();
        }

        #region 需要子类重写的方法
        /// <summary>
        /// 指定表
        /// </summary>
        protected abstract string PhysicsTable { get;}
        /// <summary>
        /// 描述
        /// </summary>
        protected abstract string Desc { get;}
        #endregion 


		#region 实现基本的方方法
	 
		/// <summary>
		/// 要运算的实体属性
		/// </summary>
		public string AttrKey
		{
			get
			{
				return this.GetValStringByKey(ConditionAttr.AttrKey);
			}
			set
			{
				this.SetValByKey(ConditionAttr.AttrKey,value);
			}
		}
        public string AttrName
        {
            get
            {
                return this.GetValStringByKey(ConditionAttr.AttrName);
            }
            set
            {
                this.SetValByKey(ConditionAttr.AttrName, value);
            }
        }
        public string OperatorValueT
        {
            get
            {
                return this.GetValStringByKey(ConditionAttr.OperatorValueT);
            }
            set
            {
                this.SetValByKey(ConditionAttr.OperatorValueT, value);
            }
        }	
		/// <summary>
		/// 运算符号
		/// </summary>
		public string FK_Operator
		{
            get
            {
                string s = this.GetValStringByKey(ConditionAttr.FK_Operator);
                if (s == null || s == "")
                    return "=";
                return s;
            }
			set
			{
				this.SetValByKey(ConditionAttr.FK_Operator,value);
			}
		}
		/// <summary>
		/// 运算值
		/// </summary>
		public object OperatorValue
		{
			get
			{
				return this.GetValStringByKey(ConditionAttr.OperatorValue);
			}
			set
			{
				this.SetValByKey(ConditionAttr.OperatorValue,value);
			}
		}
        public string OperatorValueStr
        {
            get
            {
                return this.GetValStringByKey(ConditionAttr.OperatorValue);
            }
        }
        public int OperatorValueInt
        {
            get
            {
                return this.GetValIntByKey(ConditionAttr.OperatorValue);
            }
        }
        public bool OperatorValueBool
        {
            get
            {
                return this.GetValBooleanByKey(ConditionAttr.OperatorValue);
            }
        }
        public Int64 WorkID = 0;
		#endregion 

		#region 构造方法
		/// <summary>
		/// 条件基类
		/// </summary>
		public Condition(){}
        public Condition(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }

		#endregion 

		#region 公共方法
		/// <summary>
		/// 这个条件能不能通过
		/// </summary>
		public virtual bool IsPassed
		{
			get
			{

                Work en = this.HisNode.HisWork;
                try
                {
                    en.SetValByKey("OID", this.WorkID);
                    en.Retrieve();
                }
                catch (Exception ex)
                {
                    throw new Exception("@在取得判断条件实体[" + en.EnDesc + "], 出现错误:" + ex.Message + "@错误原因是定义流程的判断条件出现错误,可能是你选择的判断条件工作类是当前工作节点的下一步工作造成,取不到该实体的实例.");
                }

				switch( this.FK_Operator.Trim() )
				{
					case "=":  // 如果是 = 
						if (en.GetValStringByKey(this.AttrKey)==this.OperatorValue.ToString())
							return true;
						else
							return false;
					 
					case ">":
						if ( en.GetValDoubleByKey(this.AttrKey) > Double.Parse(this.OperatorValue.ToString())  )
							return true;
						else
							return false;
					 
					case ">=":
						if (  en.GetValDoubleByKey(this.AttrKey) >= Double.Parse(this.OperatorValue.ToString())  )
							return true;
						else
							return false;
				 
					case "<":
						if (  en.GetValDoubleByKey(this.AttrKey) < Double.Parse(this.OperatorValue.ToString())  )
							return true;
						else
							return false;
					 
					case "<=":
						if (  en.GetValDoubleByKey(this.AttrKey) <= Double.Parse(this.OperatorValue.ToString())  )
							return true;
						else
							return false;
						 
					case "!=":
						if (  en.GetValDoubleByKey(this.AttrKey) != Double.Parse(this.OperatorValue.ToString())  )
							return true;
						else
							return false;
					case "like":
						if (  en.GetValStringByKey(this.AttrKey).IndexOf(this.OperatorValue.ToString()) == -1   )
							return false;
						else
							return true;
					default :
						throw new Exception("@没有找到操作符号..");
				}
				 
			}
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

                map.AddMyPK();

                map.AddTBInt(ConditionAttr.NodeID, 0, "MainID", true, true);
                map.AddTBInt(ConditionAttr.FK_Node, 0, "节点ID", true, true);
                map.AddTBInt(ConditionAttr.FK_Attr, 0, "属性", true, true);
                map.AddTBString(ConditionAttr.AttrKey, null, "属性", true, true, 0, 60, 20);
                map.AddTBString(ConditionAttr.AttrName, null, "中文名称", true, true, 0, 60, 20);

                map.AddTBString(ConditionAttr.FK_Operator, "=", "运算符号", true, true, 0, 60, 20);
                map.AddTBString(ConditionAttr.OperatorValue, "", "要运算的值", true, true, 0, 60, 20);
                map.AddTBString(ConditionAttr.OperatorValueT, "", "要运算的值T", true, true, 0, 60, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 
	}
	/// <summary>
	/// 条件基类s
	/// </summary>
	abstract public class Conditions :En.Base.Entities
	{
		#region public属性
		/// <summary>
		/// 在这里面的所有条件是不是都符合.
		/// </summary>
		public bool IsAllPassed
		{
            get
            {
                if (this.Count == 0)
                    throw new Exception("@没有要判断的集合.");

                foreach (Condition en in this)
                {
                    if (en.IsPassed == false)
                        return false;
                }
                return true;
            }
		}
		/// <summary>
		/// 是不是其中的一个passed. 
		/// </summary>
		public bool IsOneOfConditionPassed
		{
            get
            {
                foreach (Condition en in this)
                {
                    if (en.IsPassed == true)
                        return true;
                }
                return false;
            }
		}
		/// <summary>
		/// 取出其中一个的完成条件。. 
		/// </summary>
		public Condition GetOneOfConditionPassed
		{
			get
			{				 
				foreach(Condition en in this)
				{
					if (en.IsPassed==true)
						return en;
				}
				throw new Exception("@没有完成条件。");
			}
		}
        public int NodeID = 0;
		#endregion 

		#region 构造
		/// <summary>
		/// 条件基类
		/// </summary>
		public Conditions(){}
        public Conditions(int nodeID) 
        {
            this.NodeID = nodeID;
            this.Retrieve(ConditionAttr.NodeID, nodeID);
        }		 

		#endregion

	}
}
