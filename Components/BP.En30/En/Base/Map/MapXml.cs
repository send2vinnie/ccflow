using System;
using BP.XML;

namespace BP.En.Base
{
	/// <summary>
	/// MapXml 的摘要说明。
	/// </summary>
	public class Base:BP.XML.XmlEn
	{
		public Base()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
	public class Bases:BP.XML.XmlEns
	{
		public Bases()
		{

		}
		public override string File
		{
			get
			{
				return null;
			}
		}
		public override Entities RefEns
		{
			get
			{
				return null;
			}
		}
		public override string TableName
		{
			get
			{
				return null;
			}
		}
	}
}
