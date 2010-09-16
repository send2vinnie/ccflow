using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 选择题基类属性
	/// </summary>
	public class ChoseBaseAttr:EntityNoNameAttr
	{
		/// <summary>
		/// 选择题基类
		/// </summary>
		public const string FK_ChoseBase="FK_ChoseBase";
		/// <summary>
		/// 选择题基类类型
		/// </summary>
		public const string ChoseBaseType="ChoseBaseType";
		/// <summary>
		/// 项目个数
		/// </summary>
		public const string ItemNum="ItemNum";
	}
	/// <summary>
	/// 选择题基类
	/// </summary>
	abstract public class ChoseBase :EntityNoName
	{
		#region attrs
		public static string GenerStr(string name)
		{
			char[] chars = name.ToCharArray();
			int i=0;
			string str="";
			foreach(char c in chars)
			{
				i++;
				str+=c.ToString();
				if (i==50)
				{
					str+="\n";
					i=0;
				}
			}
			if (str.Length < 49)
				str=str.PadLeft( 50 - str.Length, ' ' ) ;
			return str;
		}
		public string NameExt
		{
			get
			{
				return ChoseBase.GenerStr(this.Name);
				
			}
		}
		#endregion
	 
		#region 实现基本的方法
		 
		 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 选择题基类
		/// </summary> 
		public ChoseBase()
		{
		}
		/// <summary>
		/// 选择题基类
		/// </summary>
		/// <param name="_No">选择题基类编号</param> 
		public ChoseBase(string _No ):base(_No)
		{
		}
		#endregion 
	 
	}
	/// <summary>
	/// 选择题基类
	/// </summary>
	abstract public class ChoseBases :EntitiesNoName
	{
		/// <summary>
		/// 选择题基类
		/// </summary>
		public ChoseBases(){}

		public int Search(string sort, string fk_emp, int sxcd)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			if (sxcd==4)
			{
				/* 要查询全部。*/
				//qo.AddWhereNotInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.ChoseOne+"'"  );
			}
			else
			{
				qo.addAnd();
				qo.AddWhereInSQL(FillBlankAttr.No,    "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.ChoseOne+"' AND SXCD="+sxcd );
			}
			return qo.DoQuery();
		}
		 
	}
}
