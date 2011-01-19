using System;

namespace BP.DA
{
	/// <summary>
	/// DTSXml 的摘要说明。
	/// </summary>
	public class DTSXml:BP.XML.XmlEn
	{
		public DTSXml()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public override BP.XML.XmlEns GetNewEntities
		{
			get
			{
				return new DTSXmls();
			}
		}
		public string Name
		{
			get
			{
				return this.GetValStringByKey("Name");
			}
		}
		public DBUrlType DBUrlFrom
		{
			get
			{
				switch( this.GetValStringByKey("DBUrlFrom") )
				{
					case "AppCenterDSN":
						return DBUrlType.AppCenterDSN;
					case "DBAccessOfMSSQL2000":
						return DBUrlType.DBAccessOfMSSQL2000;
					case "DBAccessOfODBC":
						return DBUrlType.DBAccessOfODBC;
					case "DBAccessOfOLE":
						return DBUrlType.DBAccessOfOLE;
					case "DBAccessOfOracle9i":
						return DBUrlType.DBAccessOfOracle9i;
					case "DBAccessOfOracle9i1":
						return DBUrlType.DBAccessOfOracle9i1;
					default:
						throw new Exception("error DBUrlFrom ");
				}
			}
		}
		public DBUrlType DBUrlTo
		{
			get
			{
				
				switch( this.GetValStringByKey("DBUrlTo") )
				{
					case "AppCenterDSN":
						return DBUrlType.AppCenterDSN;
					case "DBAccessOfMSSQL2000":
						return DBUrlType.DBAccessOfMSSQL2000;
					case "DBAccessOfODBC":
						return DBUrlType.DBAccessOfODBC;
					case "DBAccessOfOLE":
						return DBUrlType.DBAccessOfOLE;
					case "DBAccessOfOracle9i":
						return DBUrlType.DBAccessOfOracle9i;
					case "DBAccessOfOracle9i1":
						return DBUrlType.DBAccessOfOracle9i1;
					default:
						throw new Exception("error DBUrlTo ");
				}
			}
		}
		public string SQL
		{
			get
			{
				return this.GetValStringByKey("SQL");
			}
		}
		public string FromTable
		{
			get
			{
				return this.GetValStringByKey("FromTable");
			}
		}
		public string ToTable
		{
			get
			{
				return this.GetValStringByKey("ToTable");
			}
		}
		public string PKs
		{
			get
			{
				return this.GetValStringByKey("PKs");
			}
		}

	}
	public class DTSXmls:BP.XML.XmlEns
	{
		public DTSXmls()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public override string File
		{
			get
			{
				return SystemConfig.PathOfXML+"//SQL//DTS.xml";
			}
		}
		public override string TableName
		{
			get
			{
				return "Item";
			}
		}
		public override BP.XML.XmlEn GetNewEntity
		{
			get
			{
				return new DTSXml();
			}
		}
		public override BP.En.Entities RefEns
		{
			get
			{
				return null;
			}
		}
	}
}
