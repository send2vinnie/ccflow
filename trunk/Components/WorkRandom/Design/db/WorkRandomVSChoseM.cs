using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.GTS
{
	
	/// <summary>
	/// 多项选择题设计
	/// </summary>
	public class WorkRandomVSChoseMAttr : WorkVSBaseAttr
	{
		#region 基本属性
		/// <summary>
		/// 选择题目
		/// </summary>
		public const  string FK_Chose="FK_Chose";
		#endregion	
	}
	/// <summary>
	/// 多项选择题设计 的摘要说明。
	/// </summary>
	public class WorkRandomVSChoseM :WorkVSBase
	{
		#region 基本属性
		/// <summary>
		///判断题
		/// </summary>
		public string FK_Chose
		{
			get
			{
				return this.GetValStringByKey(WorkRandomVSChoseMAttr.FK_Chose);
			}
			set
			{
				SetValByKey(WorkRandomVSChoseMAttr.FK_Chose,value);
			}
		}
		 
		#endregion

		#region 扩展属性
		 
		#endregion		

		#region 构造函数
		/// <summary>
		/// 工作人员岗位
		/// </summary> 
		public WorkRandomVSChoseM()
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
				
				Map map = new Map("GTS_WorkRandomVSChoseM");
				map.EnDesc="多项选择题设计";	
				map.EnType=EnType.Dot2Dot;
		 
				map.AddDDLEntitiesPK(WorkRandomVSChoseMAttr.FK_Work,"0001","作业",new WorkFixDesigns(),true);
				map.AddDDLEntitiesPK(WorkRandomVSChoseMAttr.FK_Chose,null,"多项选择题",new Choses(),true);
				map.AddTBInt(WorkRandomVSChoseMAttr.Cent,1,"分",true,true);

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
	public class WorkRandomVSChoseMs : WorkVSBases
	{
		#region 构造
		/// <summary>
		/// 多项选择题设计
		/// </summary>
		public WorkRandomVSChoseMs(){}
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WorkRandomVSChoseM();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
