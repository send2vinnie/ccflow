using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 填空题设计
	/// </summary>
	public class WorkVSFillBlankAttr:WorkVSBaseAttr
	{
		#region 基本属性
		/// <summary>
		/// 填空题
		/// </summary>
		public const  string FK_FillBlank="FK_FillBlank";
		#endregion	
	}
	/// <summary>
	/// 填空题设计 的摘要说明。
	/// </summary>
	public class WorkVSFillBlank :WorkVSBase
	{
		#region 基本属性
		/// <summary>
		///填空题
		/// </summary>
		public string FK_FillBlank
		{
			get
			{
				return this.GetValStringByKey(WorkVSFillBlankAttr.FK_FillBlank);
			}
			set
			{
				SetValByKey(WorkVSFillBlankAttr.FK_FillBlank,value);
			}
		}		  
		#endregion

		#region 扩展属性
		 
		#endregion		

		#region 构造函数
		/// <summary>
		/// 工作人员岗位
		/// </summary> 
		public WorkVSFillBlank()
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
				
				Map map = new Map("GTS_WorkVSFillBlank");
				map.EnDesc="填空题设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkVSFillBlankAttr.FK_Work,"0001","试卷", new WorkFixDesigns(),true);
				map.AddDDLEntitiesPK(WorkVSFillBlankAttr.FK_FillBlank,null,"填空题",new FillBlanks(),true);
				map.AddTBInt(WorkVSFillBlankAttr.Cent,1,"分",true,true);

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
	/// 填空题设计 
	/// </summary>
	public class WorkVSFillBlanks : WorkVSBases
	{
		#region 构造
		/// <summary>
		/// 填空题设计
		/// </summary>
		public WorkVSFillBlanks(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkVSFillBlank();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
