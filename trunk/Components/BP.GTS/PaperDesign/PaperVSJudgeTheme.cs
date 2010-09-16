using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 判断题设计
	/// </summary>
	public class PaperVSJudgeThemeAttr  
	{
		#region 基本属性
		/// <summary>
		/// 试卷
		/// </summary>
		public const  string FK_Paper="FK_Paper";
		/// <summary>
		/// 判断题目
		/// </summary>
		public const  string FK_JudgeTheme="FK_JudgeTheme";
		/// <summary>
		/// 分数
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// 判断题设计 的摘要说明。
	/// </summary>
	public class PaperVSJudgeTheme :Entity
	{
		#region 基本属性
		/// <summary>
		///判断题
		/// </summary>
		public string FK_JudgeTheme
		{
			get
			{
				return this.GetValStringByKey(PaperVSJudgeThemeAttr.FK_JudgeTheme);
			}
			set
			{
				SetValByKey(PaperVSJudgeThemeAttr.FK_JudgeTheme,value);
			}
		}		  
		/// <summary>
		///设卷
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSJudgeThemeAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSJudgeThemeAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// 分数
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperVSJudgeThemeAttr.Cent);
			}
			set
			{
				SetValByKey(PaperVSJudgeThemeAttr.Cent,value);
			}
		}		 
		#endregion

		#region 扩展属性
		 
		#endregion		

		#region 构造函数
		/// <summary>
		/// HisUAC
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
				return uc;
			}
		}
		/// <summary>
		/// 工作人员岗位
		/// </summary> 
		public PaperVSJudgeTheme()
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
				
				Map map = new Map("GTS_PaperVSJudgeTheme");
				map.EnDesc="判断题设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(PaperVSJudgeThemeAttr.FK_Paper,"0001","试卷",new Papers(),true);
				map.AddDDLEntitiesPK(PaperVSJudgeThemeAttr.FK_JudgeTheme,null,"判断题",new JudgeThemes(),true);
				map.AddTBInt(PaperVSJudgeThemeAttr.Cent,1,"分",true,true);

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
	public class PaperVSJudgeThemes : Entities
	{
		#region 构造
		/// <summary>
		/// 判断题设计
		/// </summary>
		public PaperVSJudgeThemes(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperVSJudgeTheme();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
