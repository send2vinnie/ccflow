using System;
using System.Data;
using System.Runtime.Serialization;

namespace Tax666.DataEntity
{
	///<summary>
	/// 一个可序列化的并包含(数据表名称)信息的Dataset。 
	///该类用于定义ArticleData的结构。序列化构造器允许ArticleData类型的对象被远程序列化。 
	///</summary>
	
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute]

	public class ArticleData : DataSet
	{
		///<value>用于表示Article_Table表的常量。</value>
		public const String Article_Table		    = "Article";

		//表中字段的常量定义：字段对应数据实体常量；

		public const String ArticleID_Field		    = "ArticleID";
        public const String BigTypeID_Field         = "BigTypeID";
        public const String SmallTypeID_Field       = "SmallTypeID";
		public const String Title_Field		        = "Title";
		public const String ArticleHtmlDesc_Field	= "ArticleHtmlDesc";
		public const String ArticlePicPath_Field	= "ArticlePicPath";
		public const String Author_Field		    = "Author";
		public const String Sourse_Field		    = "Sourse";
		public const String AuthorEmail_Field		= "AuthorEmail";
		public const String AuthorHomePage_Field	= "AuthorHomePage";
		public const String CreateTime_Field		= "CreateTime";
		public const String IsCommend_Field		    = "IsCommend";
		public const String IsTop_Field		        = "IsTop";
		public const String ReadNum_Field		    = "ReadNum";
		public const String TrampleNum_Field		= "TrampleNum";
		public const String PeakNum_Field		    = "PeakNum";
		public const String IsAudit_Field		    = "IsAudit";
        public const String IsSubject_Field         = "IsSubject";
		public const String IsAvailable_Field		= "IsAvailable";
		public const String OptionType_Field		= "OptionType";
		public const String ListType_Field		    = "ListType";
		public const String Reason_Field		    = "Reason";

		///默认常量定义(记录统计数)；
		public const String TOTALCOUNT_FIELD		= "TotalCount";

		///错误信息定义；
		///<value>用于表示ArticleData中行错误“有一个非法字段”的常量。</value>
		public const String INVALID_FIELD		    = "Invalid Field";
		///<value>用于表示ArticleData中行错误“有一些非法字段”的常量。</value>
		public const String INVALID_FIELDS		    = "Invalid Fields";

		///<summary>
		///用以支持序列化的构造函数 
		///<remarks>支持序列化的构造函数</remarks> 
		///<param name="info">用来读取的序列化对象</param> 
		///<param name="context">调用该方法者的信息</param> 
		///</summary>
		private ArticleData(SerializationInfo info, StreamingContext context) : base(info, context){}

		///<summary>
		///    ArticleData的构造函数 
		///<remarks>通过构造表结构初始化ArticleData实例</remarks> 
		///</summary> 
		public ArticleData()
		{
			//在Dataset中创建表Article_Table
			BuildDataTables();
		}

		///<summary>
		///创建表：Article_Table
		///</summary> 
		private void BuildDataTables()
		{
			DataTable table = new DataTable(Article_Table);
			DataColumnCollection Columns = table.Columns;

			DataColumn Column = Columns.Add(ArticleID_Field, typeof(System.Int32));
			Column.AllowDBNull = false;
			Column.AutoIncrement = true;

            Columns.Add(BigTypeID_Field, typeof(System.Int32));
            Columns.Add(SmallTypeID_Field, typeof(System.Int32));
			Columns.Add(Title_Field, typeof(System.String));
			Columns.Add(ArticleHtmlDesc_Field, typeof(System.String));
			Columns.Add(ArticlePicPath_Field, typeof(System.String));
			Columns.Add(Author_Field, typeof(System.String));
			Columns.Add(Sourse_Field, typeof(System.String));
			Columns.Add(AuthorEmail_Field, typeof(System.String));
			Columns.Add(AuthorHomePage_Field, typeof(System.String));
			Columns.Add(CreateTime_Field, typeof(System.DateTime));
			Columns.Add(IsCommend_Field, typeof(System.Boolean));
			Columns.Add(IsTop_Field, typeof(System.Boolean));
			Columns.Add(ReadNum_Field, typeof(System.Int32));
			Columns.Add(TrampleNum_Field, typeof(System.Int32));
			Columns.Add(PeakNum_Field, typeof(System.Int32));
			Columns.Add(IsAudit_Field, typeof(System.Boolean));
            Columns.Add(IsSubject_Field, typeof(System.Int32));
			Columns.Add(IsAvailable_Field, typeof(System.Boolean));
			Columns.Add(OptionType_Field, typeof(System.String));
			Columns.Add(ListType_Field, typeof(System.Int32));
			Columns.Add(Reason_Field, typeof(System.Int32));

			Columns.Add(TOTALCOUNT_FIELD,typeof(System.Int32));

			this.Tables.Add(table);
		}

	}
}
