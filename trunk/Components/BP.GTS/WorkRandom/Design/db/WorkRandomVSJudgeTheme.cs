using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.GTS
{
	
	/// <summary>
	/// 判断题设计
	/// </summary>
	public class WorkRandomVSJudgeThemeAttr  :WorkVSBaseAttr
	{
		#region 基本属性
		/// <summary>
		/// 判断题目
		/// </summary>
		public const  string FK_JudgeTheme="FK_JudgeTheme";
		#endregion	
	}
	/// <summary>
	/// 判断题设计 的摘要说明。
	/// </summary>
	public class WorkRandomVSJudgeTheme :WorkVSBase
	{
		#region 基本属性
		/// <summary>
		///判断题
		/// </summary>
		public string FK_JudgeTheme
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSJudgeThemeAttr.FK_JudgeTheme);
			}
			set
			{
				SetValByKey(WorkRandomVSJudgeThemeAttr.FK_JudgeTheme,value);
			}
		}		  
		 
		#endregion

		#region 扩展属性
		 
		#endregion		

		#region 构造函数
		/// <summary>
		/// 工作人员岗位
		/// </summary> 
		public WorkRandomVSJudgeTheme()
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
				
				Map map = new Map("GTS_WorkRandomVSJudgeTheme");
				map.EnDesc="判断题设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkRandomVSJudgeThemeAttr.FK_Work,"0001","试卷",new WorkFixDesigns(),true);
				map.AddDDLEntitiesPK(WorkRandomVSJudgeThemeAttr.FK_JudgeTheme,null,"判断题",new JudgeThemes(),true);
				map.AddTBInt(WorkRandomVSJudgeThemeAttr.Cent,1,"分",true,true);

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
	/// 判断题设计 
	/// </summary>
	public class WorkRandomVSJudgeThemes : WorkVSBases
	{
		#region 构造
		/// <summary>
		/// 判断题设计
		/// </summary>
		public WorkRandomVSJudgeThemes(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomVSJudgeTheme();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
