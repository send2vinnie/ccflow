using System;
using System.Data;
using System.Runtime.Serialization;

namespace Tax666.DataEntity
{
	///<summary>
	/// 一个可序列化的并包含(数据表名称)信息的Dataset。 
	///该类用于定义ArticleBigTypeData的结构。序列化构造器允许ArticleBigTypeData类型的对象被远程序列化。 
	///</summary>
	
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute]

	public class ArticleBigTypeData : DataSet
	{
		///<value>用于表示ArticleBigType_Table表的常量。</value>
		public const String ArticleBigType_Table	= "ArticleBigType";

		//表中字段的常量定义：字段对应数据实体常量；

		public const String BigTypeID_Field		    = "BigTypeID";
		public const String BigTypeName_Field		= "BigTypeName";
		public const String BigTypeDesc_Field		= "BigTypeDesc";
		public const String IsSystem_Field		    = "IsSystem";
		public const String Reason_Field		    = "Reason";
		public const String ListType_Field		    = "ListType";

		///默认常量定义(记录统计数)；
		public const String TOTALCOUNT_FIELD		= "TotalCount";

		///错误信息定义；
		///<value>用于表示ArticleBigTypeData中行错误“有一个非法字段”的常量。</value>
		public const String INVALID_FIELD		= "Invalid Field";
		///<value>用于表示ArticleBigTypeData中行错误“有一些非法字段”的常量。</value>
		public const String INVALID_FIELDS		= "Invalid Fields";

		///<summary>
		///用以支持序列化的构造函数 
		///<remarks>支持序列化的构造函数</remarks> 
		///<param name="info">用来读取的序列化对象</param> 
		///<param name="context">调用该方法者的信息</param> 
		///</summary>
		private ArticleBigTypeData(SerializationInfo info, StreamingContext context) : base(info, context){}

		///<summary>
		///    ArticleBigTypeData的构造函数 
		///<remarks>通过构造表结构初始化ArticleBigTypeData实例</remarks> 
		///</summary> 
		public ArticleBigTypeData()
		{
			//在Dataset中创建表ArticleBigType_Table
			BuildDataTables();
		}

		///<summary>
		///创建表：ArticleBigType_Table
		///</summary> 
		private void BuildDataTables()
		{
			DataTable table = new DataTable(ArticleBigType_Table);
			DataColumnCollection Columns = table.Columns;

			DataColumn Column = Columns.Add(BigTypeID_Field, typeof(System.Int32));
			Column.AllowDBNull = false;
			Column.AutoIncrement = true;

			Columns.Add(BigTypeName_Field, typeof(System.String));
			Columns.Add(BigTypeDesc_Field, typeof(System.String));
			Columns.Add(IsSystem_Field, typeof(System.Boolean));
            Columns.Add(Reason_Field, typeof(System.Int32));
			Columns.Add(ListType_Field, typeof(System.Int32));

			Columns.Add(TOTALCOUNT_FIELD,typeof(System.Int32));

			this.Tables.Add(table);
		}

	}
}
