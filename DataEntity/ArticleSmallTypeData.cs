using System;
using System.Data;
using System.Runtime.Serialization;

namespace Tax666.DataEntity
{
	///<summary>
	/// 一个可序列化的并包含(数据表名称)信息的Dataset。 
	///该类用于定义ArticleSmallTypeData的结构。序列化构造器允许ArticleSmallTypeData类型的对象被远程序列化。 
	///</summary>
	
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute]

	public class ArticleSmallTypeData : DataSet
	{
		///<value>用于表示ArticleSmallType_Table表的常量。</value>
		public const String ArticleSmallType_Table	= "ArticleSmallType";

		//表中字段的常量定义：字段对应数据实体常量；

		public const String SmallTypeID_Field		= "SmallTypeID";
		public const String BigTypeID_Field		    = "BigTypeID";
		public const String SmallTypeName_Field		= "SmallTypeName";
		public const String SmallTypeDesc_Field		= "SmallTypeDesc";
		public const String IsSystem_Field		    = "IsSystem";
		public const String IsAvailable_Field		= "IsAvailable";
		public const String OptionType_Field		= "OptionType";
		public const String ListType_Field		    = "ListType";
		public const String Reason_Field		    = "Reason";

		///默认常量定义(记录统计数)；
		public const String TOTALCOUNT_FIELD		= "TotalCount";

		///错误信息定义；
		///<value>用于表示ArticleSmallTypeData中行错误“有一个非法字段”的常量。</value>
		public const String INVALID_FIELD		= "Invalid Field";
		///<value>用于表示ArticleSmallTypeData中行错误“有一些非法字段”的常量。</value>
		public const String INVALID_FIELDS		= "Invalid Fields";

		///<summary>
		///用以支持序列化的构造函数 
		///<remarks>支持序列化的构造函数</remarks> 
		///<param name="info">用来读取的序列化对象</param> 
		///<param name="context">调用该方法者的信息</param> 
		///</summary>
		private ArticleSmallTypeData(SerializationInfo info, StreamingContext context) : base(info, context){}

		///<summary>
		///    ArticleSmallTypeData的构造函数 
		///<remarks>通过构造表结构初始化ArticleSmallTypeData实例</remarks> 
		///</summary> 
		public ArticleSmallTypeData()
		{
			//在Dataset中创建表ArticleSmallType_Table
			BuildDataTables();
		}

		///<summary>
		///创建表：ArticleSmallType_Table
		///</summary> 
		private void BuildDataTables()
		{
			DataTable table = new DataTable(ArticleSmallType_Table);
			DataColumnCollection Columns = table.Columns;

			DataColumn Column = Columns.Add(SmallTypeID_Field, typeof(System.Int32));
			Column.AllowDBNull = false;
			Column.AutoIncrement = true;

			Columns.Add(BigTypeID_Field, typeof(System.Int32));
			Columns.Add(SmallTypeName_Field, typeof(System.String));
			Columns.Add(SmallTypeDesc_Field, typeof(System.String));
			Columns.Add(IsSystem_Field, typeof(System.Boolean));
			Columns.Add(IsAvailable_Field, typeof(System.Boolean));
			Columns.Add(OptionType_Field, typeof(System.String));
			Columns.Add(ListType_Field, typeof(System.Int32));
			Columns.Add(Reason_Field, typeof(System.Int32));

			Columns.Add(TOTALCOUNT_FIELD,typeof(System.Int32));

			this.Tables.Add(table);
		}

	}
}
