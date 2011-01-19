using System;
using BP.En.Base;
using BP.DA;

namespace BP.Sys
{
	
	public class LanguageAttr : EntityNoNameAttr
	{	 
		/// <summary>
		/// 标记
		/// </summary>
		public const string  Flag="Flag";
	}
	public class Language : EntityNoName
	{
		public string Flag
		{
			get
			{
				return  this.GetValStringByKey(LanguageAttr.Flag);
			}
			set
			{
				this.SetValByKey(LanguageAttr.Flag,value);
			}
		}	
		/// <summary>
		/// 
		/// </summary>
		public Language()
		{
		}
		public Language(string no) :base(no){}
		/// <summary>
		/// 重写基类的方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_Language");
				map.EnDesc="语言";
				map.AddTBStringPK(LanguageAttr.No,"00","编号",true,true,1,2,2);
				map.AddTBString(LanguageAttr.Name,"00","语言名称",true,true,1,15,2);
 
				this._enMap=map;
				return this._enMap;
			}
		}
	}
	public class Languages : EntitiesNoName
	{
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Language();
			}
		}		
	}
	 
	
	
}
