using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// 填空题attr
	/// </summary>
	public class FillBlankAttr:EntityNoNameAttr
	{
		/// <summary>
		/// 类型
		/// </summary>
		public const string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// 空格数量
		/// </summary>
		public const string BlankNum="BlankNum";
	}
	/// <summary>
	/// 填空题
	/// </summary>
	public class FillBlank :ChoseBase
	{
		#region attr
		public new string NameExt
		{
			get
			{
				return ChoseBase.GenerStr(this.Name)+"\n\n\n";
				
			}
		}
		/// <summary>
		/// 空格数量
		/// </summary>
		public int BlankNum
		{
			get
			{
				return this.GetValIntByKey(FillBlankAttr.BlankNum);
			}
			set
			{
				this.SetValByKey(FillBlankAttr.BlankNum,value);
			}
		}
		#endregion

	 
		#region 实现基本的方法
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
				
				Map map = new Map("GTS_FillBlank");
				map.EnDesc="填空题";
				map.CodeStruct="5";
				//map.EnType= EnType.Admin;

				map.AddTBStringPK(FillBlankAttr.No,null,"编号",true,true,0,50,20);
				map.AddTBString(FillBlankAttr.Name,null,"内容",true,false,0,5000,600);
				map.AddDDLEntities(FillBlankAttr.FK_ThemeSort,"0001","填空题类型",new ThemeSorts(),true);
				map.AddTBInt(FillBlankAttr.BlankNum,0,"空格数",true,true);
 
				map.AddSearchAttr( FillBlankAttr.FK_ThemeSort);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region 构造方法
		protected override void afterDelete()
		{
			this.DeleteHisRefEns();
			base.afterDelete ();
		}
		/// <summary>
		/// 填空题
		/// </summary> 
		public FillBlank()
		{
		}
		/// <summary>
		/// 填空题
		/// </summary>
		/// <param name="_No">征收机关编号</param> 
		public FillBlank(string _No ):base(_No)
		{
		}
		#endregion 

		#region 逻辑处理
		public string[] HisKeys
		{
			get
			{
				string[] keys= new string[this.BlankNum];
				string name=this.Name ; 
				 

				int i=0;
				while( name.IndexOf("(")!=-1)
				{
					name = name.Substring( name.IndexOf("(") +1  );
					string answer = name.Substring( 0 ,  name.IndexOf(")")   ); // 答案。
					name=name.Substring( answer.Length);
					keys[i]=answer;
					i++;
				}
				
				return keys;
			}
		}
		protected override bool beforeUpdate()
		{
		 
 
			string name=this.Name ; 
			name=name.Replace("（","(");
			name=name.Replace("）",")");
			name=name.Replace(" ","");
			this.Name=name;

			if (name.IndexOf("(")==-1)
			{
				throw new Exception("题目格式错误：您没有按照正确的格式输入(abc)答案部分，括号应半角书写。");
			}

			int i=0;
			while( name.IndexOf("(")!=-1)
			{
				/*  */
				name = name.Substring( name.IndexOf("(") +1  );

				string answer = name.Substring( 0 ,  name.IndexOf(")")   ); // 答案。

				name=name.Substring( answer.Length);

				i++;

			}
			this.BlankNum = i;

			//string[] docs = name.Split("(") ;

			



			return base.beforeUpdate ();
		}

 

		#endregion

	 
	}
	/// <summary>
	/// 国税征收机关
	/// </summary>
	public class FillBlanks :EntitiesNoName
	{
		public FillBlanks(string sort)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			qo.DoQuery();
		}
		public int Search(string sort, string fk_emp, int sxcd)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			qo.addAnd();
			if (sxcd==4)
			{
				qo.AddWhereNotInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.FillBlank+"'"  );
			}
			else
			{
				qo.AddWhereInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.FillBlank+"' AND SXCD="+sxcd );
			}
			return qo.DoQuery();
		}

		/// <summary>
		/// FillBlanks
		/// </summary>
		public FillBlanks(){}
		/// <summary>
		/// FillBlank
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new FillBlank();
			}
		}
	}
}
