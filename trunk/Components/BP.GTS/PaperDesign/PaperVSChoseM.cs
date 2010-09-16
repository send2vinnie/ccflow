using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 多项选择题设计
	/// </summary>
	public class PaperVSChoseMAttr  
	{
		#region 基本属性
		/// <summary>
		/// 试卷
		/// </summary>
		public const  string FK_Paper="FK_Paper";
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
	/// 多项选择题设计 的摘要说明。
	/// </summary>
	public class PaperVSChoseM :Entity
	{
		#region 基本属性
		/// <summary>
		///判断题
		/// </summary>
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(PaperVSChoseMAttr.FK_Chose);
			}
			set
			{
				SetValByKey(PaperVSChoseMAttr.FK_Chose,value);
			}
		}		  
		/// <summary>
		///设卷
		/// </summary>
		public string FK_Paper
		{
			get
			{
				return this.GetValStringByKey(PaperVSChoseMAttr.FK_Paper);
			}
			set
			{
				SetValByKey(PaperVSChoseMAttr.FK_Paper,value);
			}
		}
		/// <summary>
		/// 分数
		/// </summary>
		public int Cent
		{
			get
			{
				return this.GetValIntByKey(PaperVSChoseMAttr.Cent);
			}
			set
			{
				SetValByKey(PaperVSChoseMAttr.Cent,value);
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
		public PaperVSChoseM()
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
				
				Map map = new Map("GTS_PaperVSChoseM");
				map.EnDesc="多项选择题设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(PaperVSChoseMAttr.FK_Paper,"0001","试卷",new Papers(),true);
				map.AddDDLEntitiesPK(PaperVSChoseMAttr.FK_Chose,null,"多项选择题",new Choses(),true);
				map.AddTBInt(PaperVSChoseMAttr.Cent,1,"分",true,true);

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
	/// 多项选择题设计 
	/// </summary>
	public class PaperVSChoseMs : Entities
	{
		#region 构造
		/// <summary>
		/// 多项选择题设计
		/// </summary>
		public PaperVSChoseMs(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PaperVSChoseM();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
