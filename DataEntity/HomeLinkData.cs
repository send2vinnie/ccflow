using System;
using System.Data;
using System.Runtime.Serialization;

namespace Tax666.DataEntity
{
	///<summary>
	/// 一个可序列化的并包含(数据表名称)信息的Dataset。 
	///该类用于定义HomeLinkData的结构。序列化构造器允许HomeLinkData类型的对象被远程序列化。 
	///</summary>
	
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute]

	public class HomeLinkData : DataSet
	{
		///<value>用于表示HomeLink_Table表的常量。</value>
		public const String HomeLink_Table		= "HomeLink";

		//表中字段的常量定义：字段对应数据实体常量；

		public const String LinkID_Field		= "LinkID";
		public const String LinkName_Field		= "LinkName";
		public const String LinkUrl_Field		= "LinkUrl";
		public const String HomeDesc_Field		= "HomeDesc";
		public const String LogoPath_Field		= "LogoPath";
		public const String LinkTypeID_Field	= "LinkTypeID";
		public const String LinkMode_Field		= "LinkMode";
		public const String SortOrder_Field		= "SortOrder";
		public const String IsAudit_Field		= "IsAudit";
		public const String CreateTime_Field	= "CreateTime";
		public const String IsAvailable_Field	= "IsAvailable";
        public const String Reason_Field        = "Reason";
        public const String OptionType_Field    = "OptionType";
        public const String ListType_Field      = "ListType";

		///默认常量定义(记录统计数)；
		public const String TOTALCOUNT_FIELD	= "TotalCount";

		///错误信息定义；
		///<value>用于表示HomeLinkData中行错误“有一个非法字段”的常量。</value>
		public const String INVALID_FIELD		= "Invalid Field";
		///<value>用于表示HomeLinkData中行错误“有一些非法字段”的常量。</value>
		public const String INVALID_FIELDS		= "Invalid Fields";

		///<summary>
		///用以支持序列化的构造函数 
		///<remarks>支持序列化的构造函数</remarks> 
		///<param name="info">用来读取的序列化对象</param> 
		///<param name="context">调用该方法者的信息</param> 
		///</summary>
		private HomeLinkData(SerializationInfo info, StreamingContext context) : base(info, context){}

		///<summary>
		///    HomeLinkData的构造函数 
		///<remarks>通过构造表结构初始化HomeLinkData实例</remarks> 
		///</summary> 
		public HomeLinkData()
		{
			//在Dataset中创建表HomeLink_Table
			BuildDataTables();
		}

		///<summary>
		///创建表：HomeLink_Table
		///</summary> 
		private void BuildDataTables()
		{
			DataTable table = new DataTable(HomeLink_Table);
			DataColumnCollection Columns = table.Columns;

			DataColumn Column = Columns.Add(LinkID_Field, typeof(System.Int32));
			Column.AllowDBNull = false;
			Column.AutoIncrement = true;

			Columns.Add(LinkName_Field, typeof(System.String));
			Columns.Add(LinkUrl_Field, typeof(System.String));
			Columns.Add(HomeDesc_Field, typeof(System.String));
			Columns.Add(LogoPath_Field, typeof(System.String));
			Columns.Add(LinkTypeID_Field, typeof(System.Int32));
			Columns.Add(LinkMode_Field, typeof(System.Int32));
			Columns.Add(SortOrder_Field, typeof(System.Int32));
			Columns.Add(IsAudit_Field, typeof(System.Boolean));
			Columns.Add(CreateTime_Field, typeof(System.DateTime));
			Columns.Add(IsAvailable_Field, typeof(System.Boolean));
            Columns.Add(OptionType_Field, typeof(System.String));
            Columns.Add(ListType_Field, typeof(System.Int32));
            Columns.Add(Reason_Field, typeof(System.Int32));

			Columns.Add(TOTALCOUNT_FIELD,typeof(System.Int32));

			this.Tables.Add(table);
		}

	}
}
