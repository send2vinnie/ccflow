using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GTS
{
	/// <summary>
	/// 学习
	/// </summary>
	public class StudyAttr  
	{
		#region 基本属性
		/// <summary>
		/// 学生
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// 类型
		/// </summary>
		public const  string FK_ThemeType="FK_ThemeType";
		/// <summary>
		/// 类别
		/// </summary>
		public const  string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// 分数
		/// </summary>
		public const  string FK_Theme="FK_Theme";
		/// <summary>
		/// 熟悉程度
		/// </summary>
		public const  string SXCD="SXCD";
		public const  string Docs="Docs";

		#endregion	
	}
	/// <summary>
	/// 学习 的摘要说明
	/// </summary>
	public class Study :Entity
	{
		#region 基本属性
		/// <summary>
		///阅读题
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(StudyAttr.FK_Emp);
			}
			set
			{
				SetValByKey(StudyAttr.FK_Emp,value);
			}
		}
		public string Docs
		{
			get
			{
				return this.GetValStringByKey(StudyAttr.Docs);
			}
			set
			{
				SetValByKey(StudyAttr.Docs,value);
			}
		}
		/// <summary>
		///设卷
		/// </summary>
		public string FK_ThemeType
		{
			get
			{
				return this.GetValStringByKey(StudyAttr.FK_ThemeType);
			}
			set
			{
				SetValByKey(StudyAttr.FK_ThemeType,value);
			}
		}
		public string FK_ThemeSort
		{
			get
			{
				return this.GetValStringByKey(StudyAttr.FK_ThemeSort);
			}
			set
			{
				SetValByKey(StudyAttr.FK_ThemeSort,value);
			}
		}
		/// <summary>
		/// FK_Theme
		/// </summary>
		public string FK_Theme
		{
			get
			{
				return this.GetValStringByKey(StudyAttr.FK_Theme);
			}
			set
			{
				SetValByKey(StudyAttr.FK_Theme,value);
			}
		}
		/// <summary>
		/// 熟悉程度
		/// </summary>
		public int SXCD
		{
			get
			{
				return this.GetValIntByKey(StudyAttr.SXCD);
			}
			set
			{
				SetValByKey(StudyAttr.SXCD,value);
			}
		}
		public string SXCDText
		{
			get
			{
				return this.GetValRefTextByKey(StudyAttr.SXCD);
			}
		}
		#endregion

		#region 构造函数
		public Study()
		{

		}
		/// <summary>
		/// 阅读题分数设计
		/// </summary> 
		public Study(string fk_emp, string fk_themtype, string fk_theme, string themSort)
		{
			this.FK_Emp = fk_emp;
			this.FK_ThemeType=fk_themtype;
			this.FK_Theme=fk_theme;
			this.FK_ThemeSort=themSort;
//			if (themSort==null || themSort=="undefined" )
//			{
//				throw new  Exception("error themSort ");
//			}
			try
			{
				this.Retrieve();
			}
			catch
			{
				this.Insert();
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
				
				Map map = new Map("GTS_Study");
				map.EnDesc="学习";	
				map.EnType=EnType.Dtl;
				map.AddDDLEntitiesPK(StudyAttr.FK_Emp,Web.WebUser.No,"学生",new Emps(),false);
				map.AddDDLEntitiesPK(StudyAttr.FK_ThemeType,"ChoseOne","类型",new ThemeTypes(),false);
				map.AddDDLEntities(StudyAttr.FK_ThemeSort,"01","类别",new ThemeSorts(),false);
				map.AddTBStringPK(StudyAttr.FK_Theme,null,"题目编号",true,true,0,90,30);
				map.AddDDLSysEnum(StudyAttr.SXCD,1,"熟悉程度",true,false);
				map.AddTBStringDoc(StudyAttr.Docs,null,"备注",true,true);

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
	public class Studys : Entities
	{
		#region 构造
		/// <summary>
		/// 学习
		/// </summary>
		public Studys(){}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_themetype"></param>
		/// <param name="fk_emp"></param>
		public Studys(string fk_themetype, string fk_emp, int sxcd)
		{
			if (sxcd==4)
				return;

			QueryObject qo = new QueryObject(this);
			qo.AddWhere(StudyAttr.FK_ThemeType, fk_themetype);
			qo.addAnd();
			qo.AddWhere(StudyAttr.FK_Emp, fk_emp);
			qo.addAnd();
			qo.AddWhere(StudyAttr.SXCD, sxcd);
			qo.DoQuery();

		}

		#endregion

		#region 方法
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Study();
			}
		}	
		#endregion 

		#region 查询方法
		 
		#endregion
	}
	
}
