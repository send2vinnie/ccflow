
using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;
using BP.Sys;

namespace BP.WF
{	 
	/// <summary>
	/// 节点完成任务的条件
	/// </summary>
	public class GlobalCompleteConditionAttr : ConditionAttr
	{
		/// <summary>
		/// 流程No
		/// </summary>
		public const string NodeID="NodeID";
	}
	/// <summary>
	/// 有节点的 节点完成任务条件
	/// </summary>
	public class GlobalCompleteCondition :Condition
	{
		#region 基本属性
		/// <summary>
		/// gongu
		/// </summary>
		public string NodeID
		{
			get
			{
				return this.GetValStringByKey(GlobalCompleteConditionAttr.NodeID);
			}
			set
			{
				this.SetValByKey(GlobalCompleteConditionAttr.NodeID,value);
			}   
		}		 
		#endregion 

		#region 扩展属性
		/// <summary>
		/// 工作流程
		/// </summary>
		private Flow _HisFlow=null;
		/// <summary>
		/// 工作流程
		/// </summary>
		public Flow HisFlow
		{
			get
			{
				if (this._HisFlow==null)
					this._HisFlow =  new Flow(this.NodeID);

				return this._HisFlow ; 
			}
		}		 
		#endregion 

		#region 实现基类的方法
	    
		#endregion

		#region 构造方法
		/// <summary>
		/// 全局工作流程完成工作的条件
		/// </summary>
		public GlobalCompleteCondition(){}
		/// <summary>
		/// 属性
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("WF_GlobalCompleteCondition");
				map.EnDesc="全局工作流程完成工作的条件";
                map.AddTBIntPK(GlobalCompleteConditionAttr.NodeID,0, "当前点", true, true);
				map.AddTBString(GlobalCompleteConditionAttr.AttrKey,null,"属性",true,true,1,60,20);
                map.AddTBString(GlobalCompleteConditionAttr.FK_Operator, "=", "运算符号", true, true, 1, 60, 20);
				map.AddTBString(GlobalCompleteConditionAttr.OperatorValue,"","要运算的值",true,true,1,60,20);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		#region 公共方法
		
		#endregion
	}
	/// <summary>
	/// 全局工作流程完成工作的条件s
	/// 一般来说他只有一个条件.
	/// 条件与条件之间是 or  关系.
	/// 如果有多个条件,满足,任意一个条件就通过.
	/// </summary>
	public class GlobalCompleteConditions :Conditions
	{
		#region 构造
		/// <summary>
		/// 全局工作流程完成工作的条件
		/// </summary>
		public GlobalCompleteConditions(){}
		/// <summary>
		/// 全局工作流程完成工作的条件集合
		/// </summary>
		/// <param name="flowNo">flowNo</param>
		/// <param name="workFolwID">workFolwID</param>
		public GlobalCompleteConditions(string flowNo, int workFolwID)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GlobalCompleteConditionAttr.NodeID,flowNo);
			qo.DoQuery();
			foreach(GlobalCompleteCondition en in this)
			{
                en.WorkID = workFolwID;
			}
		}
		#endregion

		#region 实现基类的方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new GlobalCompleteCondition();
			}
		}
		#endregion
	}
}
