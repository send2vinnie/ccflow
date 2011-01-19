using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// 实体语言
	/// </summary>
	public class EnAttrLanguageAttr : EntityClassNameAttr
	{
		public const string Name="Name";
		public const string FK_Language="FK_Language";
		public const string AttrKey="AttrKey";

	}
	 
	/// <summary>
	/// EnAttrLanguages
	/// </summary>
	public class EnAttrLanguage: EntityClassName 
	{
		#region 基本属性
		public string Name
		{
			get
			{
				return this.GetValStringByKey(EnAttrLanguageAttr.Name ) ; 
			}
			set
			{
				this.SetValByKey(EnAttrLanguageAttr.Name,value) ; 
			}
		}
		public string FK_Language
		{
			get
			{
				return this.GetValStringByKey(EnAttrLanguageAttr.FK_Language ) ; 
			}
			set
			{
				this.SetValByKey(EnAttrLanguageAttr.FK_Language,value) ; 
			}
		}
		public string AttrKey
		{
			get
			{
				return this.GetValStringByKey(EnAttrLanguageAttr.AttrKey ) ; 
			}
			set
			{
				this.SetValByKey(EnAttrLanguageAttr.AttrKey,value) ; 
			}
		}
		#endregion

		#region 构造方法
		public EnAttrLanguage(){}	
		/// <summary>
		/// EnsClassName
		/// </summary>
		/// <param name="EnsClassName"></param>
		public EnAttrLanguage(string EnsClassName )
		{
			this.EnsClassName = EnsClassName;
			if (this.IsExits==false)
			{
				Entities ens =ClassFactory.GetEns(this.EnsClassName) ;
				this.Name = ens.GetNewEntity.EnDesc;
				this.Insert();
			}
			else
			{
				this.Retrieve();
			}

		}
		public override Map EnMap
		{
			get
			{ 
				if (this._enMap!=null)
					return this._enMap;
				Map map = new Map("Sys_EnAttrLanguage");
				map.EnDesc="实体属性语言";
				//map.AddDDLEntitiesPK(EnAttrLanguageAttr.EnsClassName ,null,DataType.AppString,"实体类",new SysEns(),"ClassName","Name",true);
				map.AddDDLEntitiesNoNamePK(EnAttrLanguageAttr.FK_Language,null,"语言",new Languages(),true);
				map.AddTBString(EnAttrLanguageAttr.AttrKey,null,"属性",true,false,1,20,20);
				map.AddTBString(EnAttrLanguageAttr.Name,null,"名称",true,false,0,50,50);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

	}
	
	/// <summary>
	/// 实体集合
	/// </summary>
	public class EnAttrLanguages : EntitiesClassName
	{		 
		public EnAttrLanguages(){}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new EnAttrLanguage();
			}
		}
		
	}
}
