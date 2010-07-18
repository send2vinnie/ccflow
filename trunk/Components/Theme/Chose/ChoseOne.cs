using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 单项选择题属性
	/// </summary>
	public class ChoseOneAttr:EntityNoNameAttr
	{
		/// <summary>
		/// 单项选择题
		/// </summary>
		public const string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// 单项选择题类型
		/// </summary>
		public const string ChoseOneType="ChoseOneType";
		/// <summary>
		/// 项目个数
		/// </summary>
		public const string ItemNum="ItemNum";
	}
	/// <summary>
	/// 单项选择题
	/// </summary>
	public class ChoseOne :ChoseBase
	{
		#region 扩展属性
		public string NameHtml
		{
			get
			{
				return "<b><font color=blue  >"+this.Name+"</font> </b>";
			}
		}
		
		public ChoseItems HisChoseItems
		{
			get
			{
				return  new ChoseItems(this.No);
			}
		}
		public ChoseItems HisChoseItemsOfRight
		{
			get
			{
				ChoseItems cts=  new ChoseItems();
				cts.RetrieveRightItems(this.No);
				return cts;
			}
		}
		/// <summary>
		/// 单项选择题类型
		/// </summary>
		public int ChoseOneType
		{
			get
			{
				return this.GetValIntByKey(ChoseOneAttr.ChoseOneType);
			}
			set
			{
				this.SetValByKey(ChoseOneAttr.ChoseOneType,value);
			}
		}
		 
		 
		#endregion
	 
		#region 实现基本的方法
		/// <summary>
		/// 权限控制
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
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("V_GTS_ChoseOne");
				map.EnDesc="单项选择题";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(ChoseOneAttr.No,null,"编号",true,true,0,50,20);
				map.AddTBString(ChoseOneAttr.Name,null,"名称",true,false,0,100,100);


				map.AddDDLEntities(ChoseOneAttr.FK_ThemeSort,"0001","单项选择题类型",new ThemeSorts(),true);


				//map.AddDtl(new ChoseOneItems(),ChoseOneItemAttr.FK_Chose);
				map.AddSearchAttr( ChoseOneAttr.FK_ThemeSort);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 单项选择题
		/// </summary> 
		public ChoseOne()
		{
		}
		/// <summary>
		/// 单项选择题
		/// </summary>
		/// <param name="_No">单项选择题编号</param> 
		public ChoseOne(string _No ):base(_No)
		{
		}
		#endregion 

		#region 逻辑处理
		protected override bool beforeUpdate()
		{
			int i = DBAccess.RunSQLReturnValInt("select count(*) from GTS_ChoseOneItem where FK_Chose='"+this.No+"' and isOk=1 ");
			if (i>1)
			{
				this.ChoseOneType=1;
				/*多项单项选择题 */
			}
			else if (i==1) 
			{
				this.ChoseOneType = 0;
			}

			return base.beforeUpdate ();
		}
		/// <summary>
		/// 自动生成4各选择项目。
		/// </summary> 
		protected override void afterInsert()
		{
			// 自动生成4各选择项目。
			this.AutoGenerItems();
		
			base.afterInsert ();
		}

		/// <summary>
		/// 自动生成Item
		/// </summary>
		/// <param name="themeNo"></param>
		public void AutoGenerItems()
		{
 
		}
		#endregion

	}
	/// <summary>
	/// 单项选择题 集合
	/// </summary>
	public class ChoseOnes :ChoseBases
	{
		/// <summary>
		/// 
		/// </summary>
		public ChoseOnes(string themesort)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(ChoseOneAttr.FK_ThemeSort,themesort);
			qo.DoQuery();
		}
		
		/// <summary>
		/// 单项选择题
		/// </summary>
		public ChoseOnes(){}
		/// <summary>
		/// 单项选择题
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ChoseOne();
			}
		}
	}
}
