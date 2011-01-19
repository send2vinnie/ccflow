using System;
using System.Data;
using System.Runtime.Serialization;

namespace Tax666.DataEntity
{
	///<summary>
	/// 一个可序列化的并包含(数据表名称)信息的Dataset。 
	///该类用于定义RegionsData的结构。序列化构造器允许RegionsData类型的对象被远程序列化。 
	///</summary>
	
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute]

	public class RegionsData : DataSet
	{
		///<value>用于表示Regions_Table表的常量。</value>
		public const String Regions_Table		    = "Regions";

		//表中字段的常量定义：字段对应数据实体常量；

		public const String RegionId_Field		    = "RegionId";
		public const String AreaId_Field		    = "AreaId";
		public const String ParentId_Field		    = "ParentId";
		public const String RegionName_Field		= "RegionName";
		public const String DisplaySequence_Field	= "DisplaySequence";
		public const String Path_Field		        = "Path";
		public const String Depth_Field		        = "Depth";

		///默认常量定义(记录统计数)；
		public const String TOTALCOUNT_FIELD		= "TotalCount";

		///错误信息定义；
		///<value>用于表示RegionsData中行错误“有一个非法字段”的常量。</value>
		public const String INVALID_FIELD		    = "Invalid Field";
		///<value>用于表示RegionsData中行错误“有一些非法字段”的常量。</value>
		public const String INVALID_FIELDS		    = "Invalid Fields";

		///<summary>
		///用以支持序列化的构造函数 
		///<remarks>支持序列化的构造函数</remarks> 
		///<param name="info">用来读取的序列化对象</param> 
		///<param name="context">调用该方法者的信息</param> 
		///</summary>
		private RegionsData(SerializationInfo info, StreamingContext context) : base(info, context){}

		///<summary>
		///    RegionsData的构造函数 
		///<remarks>通过构造表结构初始化RegionsData实例</remarks> 
		///</summary> 
		public RegionsData()
		{
			//在Dataset中创建表Regions_Table
			BuildDataTables();
		}

		///<summary>
		///创建表：Regions_Table
		///</summary> 
		private void BuildDataTables()
		{
			DataTable table = new DataTable(Regions_Table);
			DataColumnCollection Columns = table.Columns;

			DataColumn Column = Columns.Add(RegionId_Field, typeof(System.Int32));
			Column.AllowDBNull = false;
			Column.AutoIncrement = true;

			Columns.Add(AreaId_Field, typeof(System.Int32));
			Columns.Add(ParentId_Field, typeof(System.Int32));
			Columns.Add(RegionName_Field, typeof(System.String));
			Columns.Add(DisplaySequence_Field, typeof(System.Int32));
			Columns.Add(Path_Field, typeof(System.String));
			Columns.Add(Depth_Field, typeof(System.Int32));

			Columns.Add(TOTALCOUNT_FIELD,typeof(System.Int32));

			this.Tables.Add(table);
		}

	}
}
