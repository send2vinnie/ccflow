using System;
using System.Data;
using System.Runtime.Serialization;

namespace Tax666.DataEntity
{
	///<summary>
	/// 一个可序列化的并包含(数据表名称)信息的Dataset。 
	///该类用于定义ArticleRemarkData的结构。序列化构造器允许ArticleRemarkData类型的对象被远程序列化。 
	///</summary>
	
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute]

	public class ArticleRemarkData : DataSet
	{
		///<value>用于表示ArticleRemark_Table表的常量。</value>
		public const String ArticleRemark_Table		= "ArticleRemark";

		//表中字段的常量定义：字段对应数据实体常量；

		public const String RemarkID_Field		    = "RemarkID";
		public const String ArticleID_Field		    = "ArticleID";
		public const String RemarkName_Field		= "RemarkName";
		public const String RemarkDesc_Field		= "RemarkDesc";
		public const String RemarkTime_Field		= "RemarkTime";
		public const String ReplyName_Field		    = "ReplyName";
		public const String RemarkReply_Field		= "RemarkReply";
		public const String ReplyTime_Field		    = "ReplyTime";
		public const String IsReply_Field		    = "IsReply";

		///默认常量定义(记录统计数)；
		public const String TOTALCOUNT_FIELD		= "TotalCount";

		///错误信息定义；
		///<value>用于表示ArticleRemarkData中行错误“有一个非法字段”的常量。</value>
		public const String INVALID_FIELD		= "Invalid Field";
		///<value>用于表示ArticleRemarkData中行错误“有一些非法字段”的常量。</value>
		public const String INVALID_FIELDS		= "Invalid Fields";

		///<summary>
		///用以支持序列化的构造函数 
		///<remarks>支持序列化的构造函数</remarks> 
		///<param name="info">用来读取的序列化对象</param> 
		///<param name="context">调用该方法者的信息</param> 
		///</summary>
		private ArticleRemarkData(SerializationInfo info, StreamingContext context) : base(info, context){}

		///<summary>
		///    ArticleRemarkData的构造函数 
		///<remarks>通过构造表结构初始化ArticleRemarkData实例</remarks> 
		///</summary> 
		public ArticleRemarkData()
		{
			//在Dataset中创建表ArticleRemark_Table
			BuildDataTables();
		}

		///<summary>
		///创建表：ArticleRemark_Table
		///</summary> 
		private void BuildDataTables()
		{
			DataTable table = new DataTable(ArticleRemark_Table);
			DataColumnCollection Columns = table.Columns;

			DataColumn Column = Columns.Add(RemarkID_Field, typeof(System.Int32));
			Column.AllowDBNull = false;
			Column.AutoIncrement = true;

			Columns.Add(ArticleID_Field, typeof(System.Int32));
			Columns.Add(RemarkName_Field, typeof(System.String));
			Columns.Add(RemarkDesc_Field, typeof(System.String));
			Columns.Add(RemarkTime_Field, typeof(System.DateTime));
			Columns.Add(ReplyName_Field, typeof(System.String));
			Columns.Add(RemarkReply_Field, typeof(System.String));
			Columns.Add(ReplyTime_Field, typeof(System.DateTime));
			Columns.Add(IsReply_Field, typeof(System.Boolean));

			Columns.Add(TOTALCOUNT_FIELD,typeof(System.Int32));

			this.Tables.Add(table);
		}

	}
}
