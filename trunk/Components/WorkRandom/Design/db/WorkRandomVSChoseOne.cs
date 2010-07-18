using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.GTS
{
	
	/// <summary>
	/// 单向选择题设计
	/// </summary>
	public class WorkRandomVSChoseOneAttr  
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
	public class WorkRandomVSChoseOne :WorkVSBase
	{
		#region 基本属性
		/// <summary>
		///判断题
		/// </summary>
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSChoseOneAttr.FK_Chose);
			}
			set
			{
				SetValByKey(WorkRandomVSChoseOneAttr.FK_Chose,value);
			}
		}
		#endregion

		#region 扩展属性
		 
		#endregion		

		#region 构造函数
		/// <summary>
		/// 工作人员岗位
		/// </summary> 
		public WorkRandomVSChoseOne()
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
				
				Map map = new Map("GTS_WorkRandomVSChoseOne");
				map.EnDesc="单向选择题设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkRandomVSChoseOneAttr.FK_Work,"0001","试卷",new WorkFixDesigns(),true);
				map.AddDDLEntitiesPK(WorkRandomVSChoseOneAttr.FK_Chose,null,"单向选择题",new Choses(),true);
				map.AddTBInt(WorkRandomVSChoseOneAttr.Cent,1,"分",true,true);

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
	public class WorkRandomVSChoseOnes : WorkVSBases
	{
		#region 构造
		/// <summary>
		/// 单向选择题设计
		/// </summary>
		public WorkRandomVSChoseOnes(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomVSChoseOne();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
