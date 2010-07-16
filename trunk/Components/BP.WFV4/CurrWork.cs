
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
	/// 当前工作 属性
	/// </summary>
	public class CurrWorkAttr  
	{
		#region 基本属性
		/// <summary>
		/// 工作节点
		/// </summary>
		public const  string WorkID="WorkID";
        /// <summary>
        /// MyPK
        /// </summary>
        public const string MyPK = "MyPK";
		/// <summary>
		/// 处罚单据编号
		/// </summary>
		public const  string FK_Node="FK_Node";
		/// <summary>
		/// 流程
		/// </summary>
		public const  string FK_Flow="FK_Flow";
		/// <summary>
		/// 征管软件是不是罚款
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// 应该完成时间
		/// </summary>
		public const  string SDT="SDT";
		/// <summary>
		/// 记录日期
		/// </summary>
		public const  string RDT="RDT";

		/// <summary>
		/// 警告日期
		/// </summary>
		public const  string DTOfWarning="DTOfWarning";
		/// <summary>
		/// 当前日期
		/// </summary>
		public const  string CurrDate="CurrDate";
		public const  string FK_SJ="FK_SJ";
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// 预期天
		/// </summary>
		public const  string OutDays="OutDays";
		/// <summary>
		/// 工作时间状态
		/// </summary>
		public const  string WorkTimeState="WorkTimeState";
        public const string WorkFloor = "WorkFloor";
        public const string FID = "FID";

        

		#endregion
	}
	/// <summary>
	/// 当前工作
	/// </summary>
	public class CurrWork :Entity
	{
		#region 基本属性
		public string FK_EmpText
		{
			get
			{
				return this.GetValRefTextByKey( CurrWorkAttr.FK_Emp);
			}
		}
		public string FK_NodeText
		{
			get
			{
				return this.GetValRefTextByKey( CurrWorkAttr.FK_Node);
			}
		}
		public string FK_FlowText
		{
			get
			{
				return this.GetValRefTextByKey( CurrWorkAttr.FK_Flow);
			}
		}
		public int OutDays
		{
			get
			{
				return this.GetValIntByKey( CurrWorkAttr.OutDays);
			}
		}
        public int FID
        {
            get
            {
                return this.GetValIntByKey(CurrWorkAttr.FID);
            }
        }
        public string MyPK
        {
            get
            {
                return this.GetValStringByKey(CurrWorkAttr.MyPK);
            }
        }
		public string WorkTimeStateText
		{
			get
			{
				return this.GetValRefTextByKey( CurrWorkAttr.WorkTimeState);
			}
		}
		public int WorkTimeState
		{
			get
			{
				return this.GetValIntByKey(CurrWorkAttr.WorkTimeState);
			}
		}

		/// <summary>
		/// WorkID
		/// </summary>
		public string WorkID
		{
			get
			{
				return this.GetValStringByKey(CurrWorkAttr.WorkID);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.WorkID,value);
			}
		}
		/// <summary>
		/// Node
		/// </summary>
		public int FK_Node
		{
			get
			{
				return this.GetValIntByKey(CurrWorkAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.FK_Node,value);
			}
		}
		/// <summary>
		/// 工作人员
		/// </summary>
		public Emp HisEmp
		{
			get
			{
				return new Emp(this.FK_Emp);
			}
		}
		public string CurrDate
		{
			get
			{
                return DataType.CurrentData;
				//return this.GetValStringByKey(CurrWorkAttr.CurrDate);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.CurrDate,value);
			}
		}
		
		/// <summary>
		/// 应该完成日期
		/// </summary>
		public string SDT
		{
			get
			{
				return this.GetValStringByKey(CurrWorkAttr.SDT);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.SDT,value);
			}
		}
		public string DTOfWarning
		{
			get
			{
				return this.GetValStringByKey(CurrWorkAttr.DTOfWarning);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.DTOfWarning,value);
			}
		}
		public string RDT
		{
			get
			{
				return this.GetValStringByKey(CurrWorkAttr.RDT);
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.RDT,value);
			}
		}
		/// <summary>
		/// 工作人员
		/// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(CurrWorkAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(CurrWorkAttr.FK_Emp, value);
            }
        }
		/// <summary>
		/// FK_Flow
		/// </summary>		 
		public string  FK_Flow
		{
			get
			{
				return this.GetValStringByKey(CurrWorkAttr.FK_Flow );
			}
			set
			{
				this.SetValByKey(CurrWorkAttr.FK_Flow,value);
			}
		}
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(CurrWorkAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(CurrWorkAttr.FK_Dept, value);
            }
        }
		/// <summary>
		/// 节点
		/// </summary>
		public Node HisNode
		{
			get
			{
				return new Node(this.FK_Node);
			}
		}
		#endregion

		#region 构造函数
		/// <summary>
		/// CurrWork
		/// </summary>
		public CurrWork()
		{
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

                Map map = new Map("V_WF_CURRWROKS");
                map.EnDesc = "当前工作";
                map.EnType = EnType.App;
                map.DepositaryOfEntity = Depositary.None;

                map.AddDDLEntities(CurrWorkAttr.FK_Dept, null, "所属部门", new Depts(), false);
                map.AddDDLEntities(CurrWorkAttr.FK_Emp, Web.WebUser.No, "工作人员", new Emps(), false);

                map.AddDDLEntities(CurrWorkAttr.FK_Flow, null, "流程", new Flows(), false);
                map.AddDDLEntities(CurrWorkAttr.FK_Node,null, "停留节点", new NodeExts(), false);

                map.AddTBDate(CurrWorkAttr.RDT, "记录日期", false, false);
                map.AddTBDate(CurrWorkAttr.DTOfWarning, "警告日期", false, false);
                map.AddTBDate(CurrWorkAttr.SDT, "应完成日期", false, false);

             //   map.AddTBDate(CurrWorkAttr.CurrDate, "当前日期", false, false);
                map.AddTBInt(CurrWorkAttr.OutDays, 0, "逾期天", false, false);

                map.AddTBInt(CurrWorkAttr.FID, 0, "FID", false, false);

                map.AddDDLSysEnum(CurrWorkAttr.WorkTimeState, 0, "状态", true, false);

                map.AddTBInt("MyNum", 1, "工作个数", true, false);
                map.AddTBString(CurrWorkAttr.WorkID, "0", "工作ID", true, true, 0, 100, 0);


                map.AddTBStringPK(CurrWorkAttr.MyPK, "0", "MyPK", false, false, 0, 500, 0);
                map.AddDDLSysEnum(CurrWorkAttr.WorkFloor, 0, "级次", true, false);


                map.AddSearchAttr(CurrWorkAttr.FK_Dept);
                map.AddSearchAttr(CurrWorkAttr.FK_Emp);
                map.AddSearchAttr(CurrWorkAttr.WorkTimeState);
                map.AddSearchAttr(CurrWorkAttr.WorkFloor);


                map.IsShowSearchKey = false;



                RefMethod rm = new RefMethod();
                rm.Title = "详细信息";
                rm.ClassMethodName = this.ToString() + ".DoWarningDtl()";
                //rm.Icon="../../WFQH/Images/System/Details.ico";
                rm.Width = 0;
                rm.Height = 0;
                rm.HisAttrs = null;
                rm.Target = null;
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "工作报告");  //"工作报告";
                rm.ClassMethodName = this.ToString() + ".DoShowWorkRpt()";
                //rm.Icon="../../WFQH/Images/System/workinfo.gif";
                rm.Width = 0;
                rm.Height = 0;
                rm.HisAttrs = null;
                rm.Target = null;
                map.AddRefMethod(rm);

                /*
                rm = new RefMethod();
                rm.Title="执行";
                rm.ClassMethodName=this.ToString()+".DoWork()";
                rm.Icon="../../WFQH/Images/System/Exe.gif";
                rm.Width=0;
                rm.Height=0;
                rm.HisAttrs=null;
                rm.Target=null;
                map.AddRefMethod(rm);
                */

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

		public string DoWarningDtl()
		{
			//if (this.WorkID.ToString().Length >7)
			//return "此工作虚拟流程无法执行此操作。";
			PubClass.WinOpen("../ZF/Warning.aspx?FK_Emp="+this.FK_Emp+"&WorkID="+this.WorkID+"&WorkProgress="+this.WorkTimeState );
			return null;
		}
        public string DoShowWorkRpt()
        {
            if (this.WorkID.ToString().Length > 10)
                return "此工作虚拟流程无法执行此操作。";


            if (this.FK_Dept.Contains("99"))
                return "稽查流程不能显示工作报告。";

            PubClass.WinOpen("../../" + SystemConfig.AppName + "/WF/WFRpt.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID+"&FID="+this.FID);
            return null;
        }

		public string DoWork()
		{
            if (this.WorkID.ToString().Length > 10 )
				return "此工作虚拟流程无法执行此操作。";

            if (this.FK_Dept.Contains("99"))
                return "稽查流程不能操作。";

            PubClass.WinOpen("../../" + SystemConfig.AppName + "/WF/MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID);
			return null;
		}
	}
	/// <summary>
	/// 当前工作
	/// </summary>
	public class CurrWorks: Entities
	{
		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CurrWork();
			}
		}
		/// <summary>
		/// CurrWork
		/// </summary>
		public CurrWorks(){} 		 
		#endregion
	}
	
}
