using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;


namespace BP.Rpt.Doc
{
	public class RefLinkAttr : EntityNoNameAttr
	{
		/// <summary>
		/// 类名称
		/// </summary>
		public const  string ClassName="ClassName";	
		/// <summary>
		/// 连接
		/// </summary>
		public const  string Url="Url";
		/// <summary>
		/// 目标
		/// </summary>
		public const  string Target="Target";
		public const  string Note="Note";
	}

	/// <summary>
	/// 相关功能链接
	/// </summary>
	public class RefLink:EntityNoName 
	{		
		#region 属性

		/// <summary>
		/// 要连接到的地方。
		/// </summary>
		public string Url
		{
			get
			{
				return this.GetValStringByKey(RefLinkAttr.Url);
			}
			set
			{
				SetValByKey(RefLinkAttr.Url,value);
			}
		}
		/// <summary>
		/// 组合的HTMl字符串。
		/// </summary>
		/// <param name="className"></param>
		/// <param name="val"></param>
		/// <returns></returns>
		public string HtmlName(string className, string val)
		{	 
			return "<a href='"+System.Web.HttpContext.Current.Request.ApplicationPath+this.Url+"?"+className+"="+val+"' target='"+this.Target+"' >"+this.Name+"</a>";
		}
		/// <summary>
		/// 连接的目标
		/// </summary>
		public string Target
		{
			get
			{
				return this.GetValStringByKey(RefLinkAttr.Target);
			}
			set
			{
				SetValByKey(RefLinkAttr.Target,value);
			}
		}
		/// <summary>
		/// 备注，用来显示给用户看。
		/// </summary>
		public string Note
		{
			get
			{
				return this.GetValStringByKey(RefLinkAttr.Note);
			}
			set
			{
				SetValByKey(RefLinkAttr.Note,value);
			}
		}
	    /// <summary>
	    /// 类名称
	    /// </summary>		 
		public string ClassName
		{
			get
			{
				return  this.GetValStringByKey(RefLinkAttr.ClassName)   ; 
			}
		}
		#endregion 

		public RefLink(){}
	
		
		public RefLink(string _No ) : base(_No){}	
		/// <summary>
		/// 重写基类方法
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_RptRefLink");
				map.EnDesc="Port_RefLink";
				map.AddAttr(RefLinkAttr.No, "", DataType.AppString,true,"编号");
				map.AddAttr(RefLinkAttr.Name,null,DataType.AppString,false,"名称");				
				map.AddAttr(RefLinkAttr.ClassName,null,DataType.AppString ,true,"类名称");
				map.AddAttr(RefLinkAttr.Url,null,DataType.AppString,false,"url");
				map.AddAttr(RefLinkAttr.Target,null,DataType.AppString,false,"Target");
				map.AddAttr(RefLinkAttr.Note,null,DataType.AppString,false,"备注");				
				this._enMap=map;
				return this._enMap; 
			}
		}
	
	}
	/// <summary>
	/// 相关功能链接 集合
	/// </summary>
	public class RefLinks :EntitiesNoName 
	{
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			get
			{
				return "相关功能实体";
			}
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new RefLink();
			}
		}	
		public RefLinks(){}
		/// <summary>
		/// 按照className ，生成它的与之相关联的实体集合。
		/// </summary>
		/// <param name="className"></param>
		public RefLinks(string className)
		{
			QueryObject qo = new QueryObject(this) ; 
			qo.AddWhere(RefLinkAttr.ClassName,className) ; 
			qo.DoQuery();			
		}
	}
}
