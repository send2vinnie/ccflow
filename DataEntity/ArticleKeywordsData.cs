using System;
using System.Data;
using System.Runtime.Serialization;

namespace Tax666.DataEntity
{
	///<summary>
	/// 一个可序列化的并包含(数据表名称)信息的Dataset。 
	///该类用于定义ArticleKeywordsData的结构。序列化构造器允许ArticleKeywordsData类型的对象被远程序列化。 
	///</summary>
	
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute]

	public class ArticleKeywordsData : DataSet
	{
		///<value>用于表示ArticleKeywords_Table表的常量。</value>
		public const String ArticleKeywords_Table	= "ArticleKeywords";

		//表中字段的常量定义：字段对应数据实体常量；

		public const String ArticleID_Field		    = "ArticleID";
		public const String Keyword_Field		    = "Keyword";

		///默认常量定义(记录统计数)；
		public const String TOTALCOUNT_FIELD		= "TotalCount";

		///错误信息定义；
		///<value>用于表示ArticleKeywordsData中行错误“有一个非法字段”的常量。</value>
		public const String INVALID_FIELD		= "Invalid Field";
		///<value>用于表示ArticleKeywordsData中行错误“有一些非法字段”的常量。</value>
		public const String INVALID_FIELDS		= "Invalid Fields";

		///<summary>
		///用以支持序列化的构造函数 
		///<remarks>支持序列化的构造函数</remarks> 
		///<param name="info">用来读取的序列化对象</param> 
		///<param name="context">调用该方法者的信息</param> 
		///</summary>
		private ArticleKeywordsData(SerializationInfo info, StreamingContext context) : base(info, context){}

		///<summary>
		///    ArticleKeywordsData的构造函数 
		///<remarks>通过构造表结构初始化ArticleKeywordsData实例</remarks> 
		///</summary> 
		public ArticleKeywordsData()
		{
			//在Dataset中创建表ArticleKeywords_Table
			BuildDataTables();
		}

		///<summary>
		///创建表：ArticleKeywords_Table
		///</summary> 
		private void BuildDataTables()
		{
			DataTable table = new DataTable(ArticleKeywords_Table);
			DataColumnCollection Columns = table.Columns;

			Columns.Add(ArticleID_Field, typeof(System.Int32));
			Columns.Add(Keyword_Field, typeof(System.String));

			Columns.Add(TOTALCOUNT_FIELD,typeof(System.Int32));

			this.Tables.Add(table);
		}

	}
}
