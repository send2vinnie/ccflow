using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// 学习
	/// </summary>
	public class ThemeSortAttr  
	{
		#region 基本属性
		/// <summary>
		/// 学生
		/// </summary>
		public const  string No="No";
		/// <summary>
		/// 类型
		/// </summary>
		public const  string Name="Name";
		 
		#endregion	
	}
	/// <summary>
	/// 学习 的摘要说明
	/// </summary>
	public class ThemeSort :EntityNoName
	{
		#region 构造函数
		public ThemeSort()
		{

		}
		public ThemeSort(string no ):base(no)
		{

		}
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
				//return base.HisUAC;
			}
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

				Map map = new Map("GTS_ThemeSort");
				map.EnDesc="学习";	
				map.CodeStruct ="2" ;
				map.IsAllowRepeatNo=false;
				map.IsAutoGenerNo=true;
				map.DepositaryOfEntity=Depositary.Application;
				map.DepositaryOfMap=Depositary.Application;
				map.EnType=EnType.App;
				
				map.AddTBStringPK(SimpleNoNameFixAttr.No,null,"编号",true,true,1,20,4);
				map.AddTBString(SimpleNoNameFixAttr.Name,null,"名称",true,false,2,60,200);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		#region 重载基类方法
		#endregion 
	
	}
	/// <summary>
	/// 学习 
	/// </summary>
	public class ThemeSorts : EntitiesNoName
	{
		#region 构造
		/// <summary>
		/// 学习
		/// </summary>
		public ThemeSorts(){}

		 
		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ThemeSort();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
