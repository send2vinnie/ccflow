using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.Port; 
using BP.En;

namespace BP.WF
{
	/// <summary>
	/// 消息
	/// </summary>
	public class CHOfNodeAttr 
	{
		#region 基本属性
		/// <summary>
		/// 部门
		/// </summary>
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// 县局
		/// </summary>
		public const  string FK_XJ="FK_XJ";

		/// <summary>
		/// 流程
		/// </summary>
		public const  string FK_Flow="FK_Flow";
		/// <summary>
		/// 年月
		/// </summary>
		public const  string FK_NY="FK_NY";
		/// <summary>
		/// 特殊条件字段
		/// </summary>
		public const  string Spec="Spec";
		/// <summary>
		/// 季度
		/// </summary>
		public const  string FK_AP_del="FK_AP";
		/// <summary>
		/// empid
		/// </summary>
		public const  string EmpID="EmpID";

		/// <summary>
		/// 工作ID
		/// </summary>
		public const  string WorkID="WorkID";
		/// <summary>
		/// 考核的节点
		/// </summary>
		public const  string FK_Node="FK_Node";
		/// <summary>
		/// 节点状态
		/// </summary>
		public const  string NodeState="NodeState";
		 
		/// <summary>
		/// 执行员
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// 考核的工作岗位
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		/// <summary>
		/// 工作的发起时间
		/// </summary>
		public const  string RDT="RDT";		 	 
		/// <summary>
		/// 完成日期
		/// </summary>
		public const  string CDT="CDT";
		/// <summary>
		/// 应该完成日期
		/// </summary>
		public const  string SDT="SDT";
		/// <summary>
		/// 完成工作奖励分
		/// </summary>
		public const  string NodeSwinkCent="NodeSwinkCent";

		/// <summary>
		/// 需要天数（限期）
		/// </summary>
		public const  string NodeDeductDays="NodeDeductDays";

		/// <summary>
		/// 扣分率（分/天）
		/// </summary>
		public const  string NodeDeductCent="NodeDeductCent";
		/// <summary>
		/// 最高扣分
		/// </summary>
		public const  string NodeMaxDeductCent="NodeMaxDeductCent";

		/// <summary>
		/// 实际得分
		/// </summary>
		public const  string Cent="Cent";
		/// <summary>
		/// 加分
		/// </summary>
		public const  string CentOfAdd="CentOfAdd";
		/// <summary>
		/// 扣分
		/// </summary>
		public const  string CentOfCut="CentOfCut";
		/// <summary>
		/// 质量考核得分
		/// </summary>
		public const  string CentOfQU="CentOfQU";

		/// <summary>
		/// 追究人次
		/// </summary>
		public const  string ZJRC="ZJRC";
		/// <summary>
		/// SpanDays
		/// </summary>
		public const  string SpanDays="SpanDays";
		/// <summary>
		/// Note
		/// </summary>
		public const  string Note="Note";
        /// <summary>
        /// 参与人员
        /// </summary>
		public const  string Emps="Emps";
		#endregion
	}
	/// <summary>
	/// 消息
	/// </summary>
	public class CHOfNode : Entity
	{
		#region 基本属性
		/// <summary>
		/// 追究人次
		/// </summary>
		public float ZJRC
		{
			get
			{
				return this.GetValIntByKey(CHOfNodeAttr.ZJRC);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.ZJRC,value);
			}
		}

		public float CentOfAdd
		{
			get
			{
				return this.GetValFloatByKey(CHOfNodeAttr.CentOfAdd);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.CentOfAdd,value);
			}
		}
        public Int64 WorkID
		{
			get
			{
				return this.GetValInt64ByKey(CHOfNodeAttr.WorkID);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.WorkID,value);
			}
		}
		public int FK_Node
		{
			get
			{
				return this.GetValIntByKey(CHOfNodeAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_Node,value);
			}
		}
		public NodeState NodeState
		{
			get
			{
				return (NodeState)this.GetValIntByKey(CHOfNodeAttr.NodeState);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.NodeState,(int)value);
			}
		}
		public float CentOfCut
		{
			get
			{
				return this.GetValFloatByKey(CHOfNodeAttr.CentOfCut);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.CentOfCut,value);
			}
		}
		public float Cent
		{
			get
			{
				return this.GetValFloatByKey(CHOfNodeAttr.Cent);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.Cent,value);
			}
		}
		public float NodeDeductCent
		{
			get
			{
				return this.GetValFloatByKey(CHOfNodeAttr.NodeDeductCent);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.NodeDeductCent,value);
			}
		}
		public int SpanDays
		{
			get
			{
				return this.GetValIntByKey(CHOfNodeAttr.SpanDays);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.SpanDays,value);
			}
		}
		public string  Emps
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.Emps);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.Emps,value);
			}
		}
		public string  Spec
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.Spec);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.Spec,value);
			}
		}
		 

		public string  RDT
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.RDT);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.RDT,value);
			}
		}
		public string  SDT
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.SDT);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.SDT,value);
			}
		}
      
		 
		public string  FK_Dept
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.FK_Dept);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_Dept,value);
			}
		}
		public string  FK_Station
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.FK_Station);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_Station,value);
			}
		}
		public string  FK_StationText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.FK_Station);
			}
		}
		public string  FK_DeptText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.FK_Dept);
			}
		}
		public string  FK_NY
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.FK_NY);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_NY,value);
			}
		}
		
		public string  CDT
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.CDT);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.CDT,value);
			}
		}
		public string  FK_Flow
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.FK_Flow);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_Flow,value);
			}
		}
		public string  FK_FlowText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.FK_Flow);
			}
		}
		public string  FK_NodeText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.FK_Node);
			}
		}
	
		public string  FK_Emp
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.FK_Emp);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.FK_Emp,value);
			}
		}
		public string  FK_EmpText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.FK_Emp);
			}
		}
		public string  NodeStateText
		{
			get
			{
				return this.GetValRefTextByKey(CHOfNodeAttr.NodeState);
			}
		}
		public string  Note
		{
			get
			{
				return this.GetValStringByKey(CHOfNodeAttr.Note);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.Note,value);
			}
		}
		 
		#endregion 

		#region 构造函数
		/// <summary>
		/// 消息
		/// </summary>
		public CHOfNode(){}
		/// <summary>
		/// 
		/// </summary>
		public CHOfNode(int oid)
		{
			string sql="SELECT WorkID,FK_Node  FROM WF_CHOfNode WHERE WorkID+FK_Node="+oid;
			DataTable dt  = DBAccess.RunSQLReturnTable(sql);
			if (dt.Rows.Count==0)
				throw new Exception("error CHOfNode oid= "+oid);

			this.WorkID =int.Parse( dt.Rows[0][0].ToString()) ;
			this.FK_Node=int.Parse( dt.Rows[0][1].ToString()) ;
			this.Retrieve();

		}
		public CHOfNode(int workid, int FK_Node,string fk_emp)
		{
			this.WorkID=workid;
			this.FK_Node=FK_Node;
            this.FK_Emp = fk_emp;
			this.Retrieve();
		}
        /// <summary>
        /// 是否我处理的
        /// </summary>
        public bool IsMyDeal
        {
            get
            {
                return this.GetValBooleanByKey("IsMyDeal");
            }
            set
            {
                this.SetValByKey("IsMyDeal", value);
            }
        }

		public int NodeSwinkCent
		{
			get
			{
				return this.GetValIntByKey(CHOfNodeAttr.NodeSwinkCent);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.NodeSwinkCent,value);
			}
		}
		public float NodeMaxDeductCent
		{
			get
			{
				return this.GetValFloatByKey(CHOfNodeAttr.NodeMaxDeductCent);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.NodeMaxDeductCent,value);
			}
		}

		public int NodeDeductDays
		{
			get
			{
				return this.GetValIntByKey(CHOfNodeAttr.NodeDeductDays);
			}
			set
			{
				this.SetValByKey(CHOfNodeAttr.NodeDeductDays,value);
			}
		}

		/// <summary>
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;
               // Map map = new Map("WF_CHOfNode");
                 Map map = new Map("WF_CHOfNode");

                map.EnDesc = "工作";
                map.EnType = EnType.App;

                map.AddDDLEntities(CHOfNodeAttr.FK_Flow, null, "流程", new Flows(), false);

                map.AddTBIntPK(CHOfNodeAttr.WorkID, 0, "WorkID", false, true);
                map.AddDDLEntitiesPK(CHOfNodeAttr.FK_Node, 0, DataType.AppInt, "节点", new Nodes(), NodeAttr.NodeID, NodeAttr.Name, false);
                map.AddDDLEntitiesPK(CHOfNodeAttr.FK_Emp, BP.Web.WebUser.No, "人员", new BP.Port.Emps(), false);

                map.AddDDLSysEnum(CHOfNodeAttr.NodeState, 0, "节点状态", true, false);

                map.AddDDLEntities(CHOfNodeAttr.FK_Station, BP.Web.WebUser.FK_Station, "岗位", new Stations(), false);
                map.AddDDLEntities(CHOfNodeAttr.FK_Dept, null, "部门", new Depts(), false);
                map.AddDDLEntities(CHOfNodeAttr.FK_Dept, BP.Web.WebUser.FK_Dept, "部门", new Depts(), false);
                //map.AddDDLEntities(CHOfNodeAttr.FK_XJ,BP.Web.WebUser.FK_DeptOfXJ,"县局",new BP.Port.XJs(),false);
                map.AddDDLEntities(CHOfNodeAttr.FK_NY, DataType.CurrentYearMonth, "年月", new BP.Pub.NYs(), true);

                map.AddTBDateTime(CHOfNodeAttr.RDT, "接受时间", true, true);
                map.AddTBDateTime(CHOfNodeAttr.CDT, "实际完成时间", true, true);
                map.AddTBDateTime(CHOfNodeAttr.SDT, "应完成日期", true, true);

                map.AddTBInt(CHOfNodeAttr.SpanDays, 0, "相隔天", false, false);
                map.AddTBInt(CHOfNodeAttr.NodeDeductDays, 0, "限期", true, true);
                map.AddTBFloat(CHOfNodeAttr.NodeDeductCent, 0, "扣分率（分/天）", true, true);
                map.AddTBFloat(CHOfNodeAttr.NodeMaxDeductCent, 0, "最高扣分", true, true);

                map.AddTBFloat(CHOfNodeAttr.NodeSwinkCent, 0, "完成工作奖励分", false, true);

                map.AddTBFloat(CHOfNodeAttr.CentOfAdd, 0, "加分", true, true);
                map.AddTBFloat(CHOfNodeAttr.CentOfCut, 0, "扣分", true, true);

                // map.AddTBFloat(CHOfNodeAttr.CentOfQU, 0, "质量考核得分", false, false);

                map.AddTBFloat(CHOfNodeAttr.Cent, 0, "得分", false, false);
                map.AddTBString(CHOfNodeAttr.Spec, null, "特殊条件字段", false, true, 0, 200, 10);
                map.AddTBString(CHOfNodeAttr.Emps, null, "Emps", false, true, 0, 500, 10);
                map.AddTBString(CHOfNodeAttr.Note, "无", "扣分原因", false, true, 0, 2000, 20);

                map.AddTBInt("ZJRC", 0, "追究人次", false, false);
                map.AddTBInt("IsMyDeal", 1, "是否我处理的", false, false);

                //添加查询条件
                //map.AttrsOfSearch.AddFromTo("日期",CHOfNodeAttr.RDT,DateTime.Now.AddDays(-7).ToString(DataType.SysDataFormat) , DataType.CurrentData,8);
                map.AddSearchAttr(CHOfNodeAttr.FK_Dept);
                //map.AddSearchAttr(CHOfNodeAttr.FK_Flow);
                map.AddSearchAttr(CHOfNodeAttr.FK_NY);
                //map.AddSearchAttr(CHOfNodeAttr.FK_Dept);
                //map.AddSearchAttr(CHOfNodeAttr.FK_Station);
                map.AddSearchAttr(CHOfNodeAttr.FK_Emp);
                //map.AddSearchAttr(CHOfNodeAttr.FK_Node);
                map.AddSearchAttr(CHOfNodeAttr.NodeState);

                //右键添加工作报告功能
                //				RefMethod rm = new RefMethod();
                //				rm.Title="本节点";
                //				rm.ClassMethodName=this.ToString()+".DoShowWorkRpt()";
                //				rm.Icon="/Images/Btn/Rpt.gif";
                //				rm.Width=0;
                //				rm.Height=0;
                //				rm.HisAttrs=null;
                //				rm.Target=null;
                //				map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
		}
		//右键添加工作报告功能链接
        public string DoShowWorkRpt()
        {
            PubClass.WinOpen("../../" + SystemConfig.AppName + "/WF/WFRpt.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID);
            return null;
        }
		#endregion

		#region 重载基类方法
        protected override bool beforeUpdateInsertAction()
        {
            if (this.CDT == "无")
                this.NodeState = NodeState.Init;
            else
                this.NodeState = NodeState.Complete;

            return base.beforeUpdateInsertAction();
        }
		#endregion	
	}
	/// <summary>
	/// 消息s BP.Port.FK.CHOfNodes
	/// </summary>
	public class CHOfNodes : Entities
	{
		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CHOfNode();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public CHOfNodes(){}
		#endregion
	}
	
}
