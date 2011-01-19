using System;
using BP.En;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP;
 

namespace BP.En
{
	/// <summary>
	/// 多语言实体属性
	/// </summary>
	public class IEnUrlAttr:IEnAttr
	{
		/// <summary>
		/// URL
		/// </summary>
		public const string Url="Url";		
		/// <summary>
		/// Target
		/// </summary>
		public const string Target="Target";	
	}
	/// <summary>
	/// 多语言的实体
	/// </summary>
	[Serializable]
	abstract public class IEnUrl : IEn
	{
		#region 基本属性
		/// <summary>
		/// 编号
		/// </summary>
		public string  Url
		{
			get
			{
				return this.GetValStringByKey(IEnUrlAttr.Url) ; 
			}
			set
			{
				this.SetValByKey(IEnUrlAttr.Url,value);
			}
		}
		/// <summary>
		/// 语言
		/// </summary>
		public string  Target
		{
			get
			{
				return this.GetValStringByKey(IEnUrlAttr.Target) ; 
			}
			set
			{
				this.SetValByKey(IEnUrlAttr.Target,value);
			}
		}
		#endregion		
 
		#region 构造
		protected IEnUrl(){}
		protected IEnUrl(int oid) : base(oid){}
		protected IEnUrl(string no, string langugae ) : base(no,langugae){}
		#endregion
			
		
	}
	/// <summary>
	/// 多语言的实体集合
	/// </summary>
	[Serializable]
	abstract public  class IEnsUrl : IEns
	{
		#region 构造函数
		public IEnsUrl( string fk_language) :base( fk_language){}
		/// <summary>
		/// 构造函数
		/// </summary>
		public IEnsUrl(){}
		#endregion
	}

}
