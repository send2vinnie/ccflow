using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 单向选择题设计
	/// </summary>
	public class WorkVSChoseOneAttr  
	{
		#region 基本属性
		/// <summary>
		/// 试卷
		/// </summary>
		public const  string FK_Work="FK_Work";
		/// <summary>
		/// 选择题目
		/// </summary>
		public const  string FK_Chose="FK_Chose";
		/// <summary>
		/// 分数
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// 单向选择题设计 的摘要说明。
	/// </summary>
	public class WorkVSChoseOne :WorkVSBase
	{
		#region 基本属性
		/// <summary>
		///判断题
		/// </summary>
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(WorkVSChoseOneAttr.FK_Chose);
			}
			set
			{
				SetValByKey(WorkVSChoseOneAttr.FK_Chose,value);
			}
		}
		#endregion

		#region 扩展属性
		 
		#endregion		

		#region 构造函数
		/// <summary>
		/// 工作人员岗位
		/// </summary> 
		public WorkVSChoseOne()
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
				
				Map map = new Map("GTS_WorkVSChoseOne");
				map.EnDesc="单向选择题设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkVSChoseOneAttr.FK_Work,"0001","试卷",new WorkFixDesigns(),true);
				map.AddDDLEntitiesPK(WorkVSChoseOneAttr.FK_Chose,null,"单向选择题",new Choses(),true);
				map.AddTBInt(WorkVSChoseOneAttr.Cent,1,"分",true,true);

				//map.AddSearchAttr(EmpDutyAttr.FK_Emp);
				//map.AddSearchAttr(EmpDutyAttr.FK_Duty);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		#region 重载基类方法

		#endregion 
	
	}
	/// <summary>
	/// 单向选择题设计 
	/// </summary>
	public class WorkVSChoseOnes : WorkVSBases
	{
		#region 构造
		/// <summary>
		/// 单向选择题设计
		/// </summary>
		public WorkVSChoseOnes(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkVSChoseOne();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
