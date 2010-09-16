using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 填空题设计
	/// </summary>
	public class PaperVSFillBlankAttr  
	{
		#region 基本属性
		/// <summary>
		/// 试卷
		/// </summary>
		public const  string FK_Paper="FK_Paper";
		/// <summary>
		/// 填空题
		/// </summary>
		public const  string FK_FillBlank="FK_FillBlank";
		/// <summary>
		/// 分数
		/// </summary>
		public const  string Cent="Cent";
		#endregion	
	}
	/// <summary>
	/// 填空题设计 的摘要说明。
	/// </summary>
	public class PaperVSFillBlank :Entity
	{
		#region 基本属性
		/// <summary>
		///填空题
		/// </summary>
		public string FK_FillBlank
		{
			get
			{
				return this.GetValStringByKey(PaperVSFillBlankAttr.FK_FillBlank);
			}
			set
			{
				SetValByKey(PaperVSFillBlankAttr.FK_FillBlank,value);
			}
		}		  
		/// <summary>
		///设卷
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSFillBlankAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSFillBlankAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// 分数
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperVSFillBlankAttr.Cent);
			}
			set
			{
				SetValByKey(PaperVSFillBlankAttr.Cent,value);
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
		public PaperVSFillBlank()
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
				
				Map map = new Map("GTS_PaperVSFillBlank");
				map.EnDesc="填空题设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(PaperVSFillBlankAttr.FK_Paper,"0001","试卷",new Papers(),true);
				map.AddDDLEntitiesPK(PaperVSFillBlankAttr.FK_FillBlank,null,"填空题",new FillBlanks(),true);
				map.AddTBInt(PaperVSFillBlankAttr.Cent,1,"分",true,true);

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
	public class PaperVSFillBlanks : Entities
	{
		#region 构造
		/// <summary>
		/// 填空题设计
		/// </summary>
		public PaperVSFillBlanks(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperVSFillBlank();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
