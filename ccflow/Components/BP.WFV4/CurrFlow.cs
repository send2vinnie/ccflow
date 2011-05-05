
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
	/// 当前流程 属性
	/// </summary>
    public class CurrFlowAttr
    {
        #region 基本属性
        /// <summary>
        /// 工作节点
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 当前节点
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 流程
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 征管软件是不是罚款
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// Title
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// 企业编码
        /// </summary>
        public const string FK_Taxpayer = "FK_Taxpayer";
        /// <summary>
        /// 纳税人名称
        /// </summary>
        public const string TaxpayerName = "TaxpayerName";
        /// <summary>
        /// 参与节点
        /// </summary>
        public const string FK_NodeOfJion = "FK_NodeOfJion";
        /// <summary>
        /// 参与人
        /// </summary>
        public const string FK_EmpOfJion = "FK_EmpOfJion";
        /// <summary>
        /// 接受时间
        /// </summary>
        public const string DTOfAccept = "DTOfAccept";
        /// <summary>
        /// 产与人的 部门.
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        public const string FK_SJ = "FK_SJ";
        public const string RDT = "RDT";
        #endregion
    }
	/// <summary>
	/// 当前流程
	/// </summary>
	public class CurrFlow :Entity
	{
		public string FK_Flow
		{
			get
			{
				return this.GetValStringByKey(CurrFlowAttr.FK_Flow);
			}
		}
		public int WorkID
		{
			get
			{
				return this.GetValIntByKey(CurrFlowAttr.WorkID);
			}
		}

		#region 构造函数
		/// <summary>
		/// CurrFlow
		/// </summary>
		public CurrFlow()
		{
		}
		/// <summary>
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null)
					return this._enMap;
				Map map = new Map("V_WF_CurrFlow");
				map.EnDesc="当前流程";
				map.EnType=EnType.View;

				map.AddTBIntPK(CurrFlowAttr.WorkID,0,"工作ID",false,false);
				map.AddTBString(CurrFlowAttr.Title,"","标题",true,false,0,10,10);
				map.AddTBString(CurrFlowAttr.FK_Taxpayer,"","税务管理码",true,false,0,50,10);
				map.AddTBString(CurrFlowAttr.TaxpayerName,"","纳税人名称",true,false,0,200,10);
				map.AddDDLEntities( CurrFlowAttr.Rec, Web.WebUser.No,"发起人", new Emps(),false);
				map.AddTBDate(CurrFlowAttr.RDT,"发起日期",false,false);
				map.AddDDLEntities( CurrFlowAttr.FK_Flow,null, "流程", new Flows(),false);
				map.AddDDLEntities( CurrFlowAttr.FK_Node,null, "停留节点", new NodeExts(),false);

				map.AddDDLEntitiesPK( CurrFlowAttr.FK_NodeOfJion,null, "工作节点", new NodeExts(),false);
				map.AddDDLEntitiesPK( CurrFlowAttr.FK_EmpOfJion,null, "参与人", new Emps(),false);
				map.AddTBDate(CurrFlowAttr.DTOfAccept,"接受日期",true,false);

				map.AddDDLEntities( CurrFlowAttr.FK_Dept,null, "部门", new Depts(),false);
				map.AddTBInt("MyNum",1, "个数",false,false);

				map.AddSearchAttr(CurrFlowAttr.FK_Dept);
				map.AddSearchAttr(CurrFlowAttr.FK_Flow);
				map.AddSearchAttr(CurrFlowAttr.FK_EmpOfJion);
				map.IsShowSearchKey=false;



				//右键添加工作报告功能
				RefMethod rm = new RefMethod();
                rm.Title = this.ToE("WorkRpt", "工作报告"); ; // "工作报告";
				rm.ClassMethodName=this.ToString()+".DoShowWorkRpt()";
				rm.Icon="/Images/Btn/Rpt.gif";
				rm.Width=0;
				rm.Height=0;
				rm.HisAttrs=null;
				rm.Target=null;
				map.AddRefMethod(rm);


				//右键添加执行工作功能
				rm = new RefMethod();
				rm.Title="执行工作";
				rm.ClassMethodName=this.ToString()+".DoWork()";
				rm.Icon="/Images/Btn/Do.gif";
				rm.Width=0;
				rm.Height=0;
				rm.HisAttrs=null;
				rm.Target=null;
				map.AddRefMethod(rm);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		/// <summary>
		/// 右键添加工作报告链接
		/// </summary>
		/// <returns></returns>
		public string DoShowWorkRpt()
		{
			PubClass.WinOpen("../../WFQH/WF/WFRpt.aspx?FK_Flow="+this.FK_Flow+"&WorkID="+this.WorkID );
			return null;
		}
		
		/// <summary>
		/// 右键添加执行工作链接
		/// </summary>
		/// <returns></returns>
		public string DoWork()
		{
            PubClass.WinOpen("../../Face/MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID);
			return null;
		}
	}
	/// <summary>
	/// 当前流程
	/// </summary>
	public class CurrFlows: Entities
	{
		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CurrFlow();
			}
		}
		/// <summary>
		/// CurrFlow
		/// </summary>
		public CurrFlows(){} 		 
		#endregion
	}
	
}
