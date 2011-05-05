using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.WF
{
    public enum ConnDataFrom
    {
        /// <summary>
        /// 表单数据
        /// </summary>
        Form,
        /// <summary>
        /// 岗位数据
        /// </summary>
        Stas,
        /// <summary>
        /// Depts
        /// </summary>
        Depts
    }
    public enum ConnJudgeWay
    {
        /// <summary>
        /// 或者的关系
        /// </summary>
        ByOr,
        /// <summary>
        /// 并且的关系
        /// </summary>
        ByAnd
    }
	/// <summary>
	/// 条件属性
	/// </summary>
    public class CondAttr
    {
        /// <summary>
        /// 数据来源
        /// </summary>
        public const string DataFrom = "DataFrom";
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
        /// <summary>
        /// 条件类型
        /// </summary>
        public const string CondType = "CondType";
        /// <summary>
        /// 流程
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 对方向条件有效
        /// </summary>
        public const string ToNodeID = "ToNodeID";
        public const string ConnJudgeWay = "ConnJudgeWay";
        public const string MyPOID = "MyPOID";
    }
    /// <summary>
    /// 条件类型
    /// </summary>
    public enum CondType
    {
        /// <summary>
        /// 节点完成条件
        /// </summary>
        Node,
        /// <summary>
        /// 流程条件
        /// </summary>
        Flow,
        /// <summary>
        /// 方向条件
        /// </summary>
        Dir,
        /// <summary>
        /// 分流规则设置
        /// </summary>
        FLRole
    }
	/// <summary>
	/// 条件基类
	/// </summary>
	public class Cond :EntityMyPK
	{
        public ConnDataFrom HisDataFrom
        {
            get
            {
                return (ConnDataFrom)this.GetValIntByKey(CondAttr.DataFrom);
            }
            set
            {
                this.SetValByKey(CondAttr.DataFrom, (int)value);
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(CondAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(CondAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// 条件类型
        /// </summary>
        public CondType HisCondType
        {
            get
            {
                return (CondType)this.GetValIntByKey(CondAttr.CondType);
            }
            set
            {
                this.SetValByKey(CondAttr.CondType, (int)value);
            }
        }
        /// <summary>
        /// 条件计算方式
        /// </summary>
        public string HisConnJudgeWayT
        {
            get
            {
                return this.GetValRefTextByKey(CondAttr.ConnJudgeWay);
            }
        }
        /// <summary>
        /// 条件判断方式
        /// </summary>
        public ConnJudgeWay HisConnJudgeWay
        {
            get
            {
                return (ConnJudgeWay)this.GetValIntByKey(CondAttr.ConnJudgeWay);
            }
            set
            {
                this.SetValByKey(CondAttr.ConnJudgeWay, (int)value);
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
        public int MyPOID
        {
            get
            {
                return this.GetValIntByKey(CondAttr.MyPOID);
            }
            set
            {
                this.SetValByKey(CondAttr.MyPOID, value);
            }
        }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(CondAttr.NodeID);
            }
            set
            {
                this.SetValByKey(CondAttr.NodeID, value);
            }
        }
        public int FK_Node
        {
            get
            {
                int i= this.GetValIntByKey(CondAttr.FK_Node);
                if (i == 0)
                    return this.NodeID;
                return i;
            }
            set
            {
                this.SetValByKey(CondAttr.FK_Node, value);
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
        /// 对方向条件有效
        /// </summary>
        public int ToNodeID
        {
            get
            {
                return this.GetValIntByKey(CondAttr.ToNodeID);
            }
            set
            {
                this.SetValByKey(CondAttr.ToNodeID, value);
            }
        }
       
		/// <summary>
		/// 在更新与插入之前要做得操作。
		/// </summary>
		/// <returns></returns>
        protected override bool beforeUpdateInsertAction()
        {
            this.RunSQL("UPDATE WF_Node SET IsCCNode=0,IsCCFlow=0");
            this.RunSQL("UPDATE WF_Node SET IsCCNode=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond WHERE CondType=" + (int)CondType.Node + ")");
            this.RunSQL("UPDATE WF_Node SET IsCCFlow=1 WHERE NodeID IN (SELECT NodeID FROM WF_Cond WHERE CondType=" + (int)CondType.Flow + ")");

            this.MyPOID = BP.DA.DBAccess.GenerOID();
            return base.beforeUpdateInsertAction();
        }

		#region 实现基本的方方法
        /// <summary>
        /// 属性
        /// </summary>
        public string FK_Attr
        {
            get
            {
                return this.GetValStringByKey(CondAttr.FK_Attr);
            }
            set
            {
                if (value == null)
                    throw new Exception("FK_Attr不能设置为null");

                value = value.Trim();

                this.SetValByKey(CondAttr.FK_Attr, value);

                BP.Sys.MapAttr attr = new BP.Sys.MapAttr(value);
                this.SetValByKey(CondAttr.AttrKey, attr.KeyOfEn);
                this.SetValByKey(CondAttr.AttrName, attr.Name);
            }
        }
		/// <summary>
		/// 要运算的实体属性
		/// </summary>
		public string AttrKey
		{
			get
			{
				return this.GetValStringByKey(CondAttr.AttrKey);
			}
		}
        public string AttrName
        {
            get
            {
                return this.GetValStringByKey(CondAttr.AttrName);
            }
        }
        public string OperatorValueT
        {
            get
            {
                return this.GetValStringByKey(CondAttr.OperatorValueT);
            }
            set
            {
                this.SetValByKey(CondAttr.OperatorValueT, value);
            }
        }
		/// <summary>
		/// 运算符号
		/// </summary>
        public string FK_Operator
        {
            get
            {
                string s = this.GetValStringByKey(CondAttr.FK_Operator);
                if (s == null || s == "")
                    return "=";
                return s;
            }
            set
            {
                this.SetValByKey(CondAttr.FK_Operator, value);
            }
        }
		/// <summary>
		/// 运算值
		/// </summary>
        public object OperatorValue
        {
            get
            {
                return this.GetValStringByKey(CondAttr.OperatorValue);
            }
            set
            {
                this.SetValByKey(CondAttr.OperatorValue, value as string);
            }
        }
        public string OperatorValueStr
        {
            get
            {
                return this.GetValStringByKey(CondAttr.OperatorValue);
            }
        }
        public int OperatorValueInt
        {
            get
            {
                return this.GetValIntByKey(CondAttr.OperatorValue);
            }
        }
        public bool OperatorValueBool
        {
            get
            {
                return this.GetValBooleanByKey(CondAttr.OperatorValue);
            }
        }
        public Int64 WorkID = 0;
        public string MsgOfCond = "";
		#endregion 

		#region 构造方法
		/// <summary>
		/// 条件基类
		/// </summary>
		public Cond(){}
        public Cond(string mypk)
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
                Node nd = new Node(this.FK_Node);
                Work en = nd.HisWork;
                try
                {
                    en.SetValByKey("OID", this.WorkID);
                    en.Retrieve();
                }
                catch (Exception ex)
                {
                    throw new Exception("@在取得判断条件实体[" + nd.EnDesc + "], 出现错误:" + ex.Message + "@错误原因是定义流程的判断条件出现错误,可能是你选择的判断条件工作类是当前工作节点的下一步工作造成,取不到该实体的实例.");
                }


                if (this.HisDataFrom == ConnDataFrom.Stas)
                {
                    string strs = this.OperatorValue.ToString();
                    BP.Port.EmpStations sts = new BP.Port.EmpStations();
                    sts.Retrieve("FK_Emp", en.Rec);
                    string strs1 = "";
                    foreach (BP.Port.EmpStation st in sts)
                    {
                        if (strs.Contains("@" + st.FK_Station))
                        {
                            this.MsgOfCond = "@以岗位判断方向，条件为true：岗位集合" + strs + "，操作员(" + en.Rec + ")岗位:" + st.FK_Station + st.FK_StationT;
                            return true;
                        }
                        strs1 += st.FK_Station + "-" + st.FK_StationT;
                    }

                    this.MsgOfCond = "@以岗位判断方向，条件为false：岗位集合" + strs + "，操作员(" + en.Rec + ")岗位:" + strs1;
                    return false;
                }
                
              
                try
                {
                    if (en.EnMap.Attrs.Contains(this.AttrKey) == false)
                        throw new Exception("判断条件方向出现错误：实体：" + nd.EnDesc + " 属性" + this.AttrKey + "已经不存在.");

                    this.MsgOfCond = "@以表单值判断方向，值 "+en.EnDesc+"."+this.AttrKey+" (" + en.GetValStringByKey(this.AttrKey) + ") 操作符:(" + this.FK_Operator + ") 判断值:(" + this.OperatorValue.ToString()+")";

                    switch (this.FK_Operator.Trim().ToLower())
                    {
                        case "=":  // 如果是 = 
                            if (en.GetValStringByKey(this.AttrKey) == this.OperatorValue.ToString())
                                return true;
                            else
                                return false;

                        case ">":
                            if (en.GetValDoubleByKey(this.AttrKey) > Double.Parse(this.OperatorValue.ToString()))
                                return true;
                            else
                                return false;

                        case ">=":
                            if (en.GetValDoubleByKey(this.AttrKey) >= Double.Parse(this.OperatorValue.ToString()))
                                return true;
                            else
                                return false;

                        case "<":
                            if (en.GetValDoubleByKey(this.AttrKey) < Double.Parse(this.OperatorValue.ToString()))
                                return true;
                            else
                                return false;

                        case "<=":
                            if (en.GetValDoubleByKey(this.AttrKey) <= Double.Parse(this.OperatorValue.ToString()))
                                return true;
                            else
                                return false;

                        case "!=":
                            if (en.GetValDoubleByKey(this.AttrKey) != Double.Parse(this.OperatorValue.ToString()))
                                return true;
                            else
                                return false;
                        case "like":
                            if (en.GetValStringByKey(this.AttrKey).IndexOf(this.OperatorValue.ToString()) == -1)
                                return false;
                            else
                                return true;
                        default:
                            throw new Exception("@没有找到操作符号..");
                    }

                }
                catch (Exception ex)
                {
                    Node nd23 = new Node(this.NodeID);
                    throw new Exception("@判断条件:Node=[" + this.NodeID+"," +nd23.EnDesc+ "], 出现错误。@" + ex.Message +"  。有可能您设置了非法的条件判断方式。");
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
                Map map = new Map("WF_Cond");
                map.EnDesc = "流程条件";

                map.AddMyPK();

                map.AddTBInt(CondAttr.CondType, 0, "条件类型", true, true);
            //map.AddDDLSysEnum(CondAttr.CondType, 0, "条件类型", true, false, CondAttr.CondType,"@0=节点完成条件@1=流程完成条件@2=方向条件");

                map.AddTBInt(CondAttr.DataFrom, 0, "条件数据来源0表单,1岗位(对方向条件有效)", true, true);

                map.AddTBString(CondAttr.FK_Flow, null, "流程", true, true, 0, 60, 20);

                map.AddTBInt(CondAttr.NodeID, 0, "发生的事件", true, true);
                map.AddTBInt(CondAttr.FK_Node, 0, "节点ID", true, true);

              //  map.AddTBInt(CondAttr.FK_Attr, 0, "属性ID", true, true);

              map.AddTBString(CondAttr.FK_Attr, null, "属性", true, true, 0, 80, 20);

                map.AddTBString(CondAttr.AttrKey, null, "属性", true, true, 0, 60, 20);
                map.AddTBString(CondAttr.AttrName, null, "中文名称", true, true, 0, 60, 20);

                map.AddTBString(CondAttr.FK_Operator, "=", "运算符号", true, true, 0, 60, 20);
                map.AddTBString(CondAttr.OperatorValue, "", "要运算的值", true, true, 0, 60, 20);
                map.AddTBString(CondAttr.OperatorValueT, "", "要运算的值T", true, true, 0, 60, 20);

                map.AddTBInt(CondAttr.ToNodeID, 0, "ToNodeID（对方向条件有效）", true, true);

                map.AddDDLSysEnum(CondAttr.ConnJudgeWay, 0, "条件关系", true, false, CondAttr.ConnJudgeWay, "@0=or@1=and");
              //  map.AddTBInt(CondAttr.ConnJudgeWay, 0, "条件关系", true, true);


                map.AddTBInt(CondAttr.MyPOID, 0, "MyPOID", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 
	}
	/// <summary>
	/// 条件基类s
	/// </summary>
	public class Conds :En.Entities
	{
		#region public属性
        public override Entity GetNewEntity
        {
            get {  return new Cond(); }
        }
		/// <summary>
		/// 在这里面的所有条件是不是都符合.
		/// </summary>
		public bool IsAllPassed
		{
            get
            {
                if (this.Count == 0)
                    throw new Exception("@没有要判断的集合.");

                foreach (Cond en in this)
                {
                    if (en.IsPassed == false)
                        return false;
                }
                return true;
            }
		}
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsPass
        {
            get
            {
                if (this.Count == 1)
                    if (this.IsOneOfCondPassed)
                        return true;
                    else
                        return false;

                // 先判断  or and. 的关系。
                foreach (Cond en in this)
                {
                    if (en.HisConnJudgeWay == ConnJudgeWay.ByOr)
                        if (en.IsPassed == true)
                            return true;

                    if (en.HisConnJudgeWay == ConnJudgeWay.ByAnd)
                        if (en.IsPassed == false)
                            return false;
                }
                return true;
            }
        }
        public string MsgOfDesc
        {
            get
            {
                string msg = "";
                foreach (Cond c in this)
                {
                    msg += "@" + c.MsgOfCond;
                }
                return msg;
            }
        }
		/// <summary>
		/// 是不是其中的一个passed. 
		/// </summary>
		public bool IsOneOfCondPassed
		{
            get
            {
                foreach (Cond en in this)
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
		public Cond GetOneOfCondPassed
		{
			get
			{				 
				foreach(Cond en in this)
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
		public Conds()
        {
        }
        /// <summary>
        /// 条件基类
        /// </summary>
        public Conds(string fk_flow)
        {
            this.Retrieve(CondAttr.FK_Flow, fk_flow);
        }
        /// <summary>
        /// 条件
        /// </summary>
        /// <param name="ct">类型</param>
        /// <param name="nodeID">节点</param>
        public Conds(CondType ct, int nodeID, Int64 workid) 
        {
            this.NodeID = nodeID;
            this.Retrieve(CondAttr.NodeID, nodeID, CondAttr.CondType, (int)ct);

            foreach (Cond en in this)
                en.WorkID = workid;
        }
       
        public string ConditionDesc
        {
            get
            {
                return "";
            }
        }
		#endregion

	}
}
