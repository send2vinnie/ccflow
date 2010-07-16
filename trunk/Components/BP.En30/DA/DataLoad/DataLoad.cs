using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// 导入
	/// </summary>
	public class DataLoadAttr :EntityNoNameAttr
	{
		/// <summary>
		/// 结构描述
		/// </summary>
		public const string Ex_Stru="Ex_Stru";
		/// <summary>
		/// 类型
		/// </summary>
		public const string FK_DLType="FK_DLType";
		/// <summary>
		/// 导入之后要执行的存储过程。
		/// </summary>
		public const string DBProcedureName="DBProcedureName";
		public const string XlsDefaultTbName="XlsDefaultTbName";
		public const string  ImportClear = "ImportClear";

	}
	/// <summary>
	/// DataLoad
	/// </summary>
	public class DataLoad : EntityNoName 
	{
		 
		#region 基本属性
		/// <summary>
		/// 结构
		/// </summary>
		public  string  Ex_Stru
		{
			get
			{
			  return this.GetValStringByKey(DataLoadAttr.Ex_Stru);
			}
			set
			{
				this.SetValByKey(DataLoadAttr.Ex_Stru,value);
			}
		}
		/// <summary>
		/// 类型
		/// </summary>
		public  string  FK_DLType
		{
			get
			{
				return this.GetValStringByKey(DataLoadAttr.FK_DLType);
			}
			set
			{
				this.SetValByKey(DataLoadAttr.FK_DLType,value);
			}
		}
		/// <summary>
		/// 要调用的存储过程
		/// </summary>
		public  string  DBProcedureName
		{
			get
			{
				return this.GetValStringByKey(DataLoadAttr.DBProcedureName);
			}
			set
			{
				this.SetValByKey(DataLoadAttr.DBProcedureName,value);
			}
		}
		/// <summary>
		/// 上传数据时，默认Excel工作表表名
		/// </summary>
		public  string  XlsDefaultTbName
		{
			get
			{
				return this.GetValStringByKey(DataLoadAttr.XlsDefaultTbName);
			}
			set
			{
				this.SetValByKey(DataLoadAttr.XlsDefaultTbName,value);
			}
		}
		/// <summary>
		/// 上传数据时，默认Excel工作表表名
		/// </summary>
		public  int  ImportClear
		{
			get
			{
				return this.GetValIntByKey(DataLoadAttr.ImportClear);
			}
			set
			{
				this.SetValByKey(DataLoadAttr.ImportClear,value);
			}
		}
 
		 
		#endregion 

		#region 构造方法
		/// <summary>
		/// DataLoad
		/// </summary>
		public DataLoad(){}
		public DataLoad(string no) :base (no) {}
		 
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_DataLoad");
				map.EnDesc="装载";
				//map.PhysicsTable;
				map.AddTBStringPK(DataLoadAttr.No,null,"要装载到的物理表",true,false,3,50,20);
				map.AddTBString(DataLoadAttr.Name,null,"名称",true,false,3,50,20);
				map.AddTBString(DataLoadAttr.Ex_Stru,null,"结构描述",true,false,3,50,20);
				map.AddDDLEntities(DataLoadAttr.FK_DLType,null,"类型",new DataLoadTypes(),true);
				map.AddTBString(DataLoadAttr.DBProcedureName,null,"存储过程",true,false,3,50,20);				 
				map.AddTBString(DataLoadAttr.XlsDefaultTbName,null,"默认Excel工作表表名",true,false,0,50,30);				 
				map.AddBoolean(DataLoadAttr.ImportClear,true,"导入时是否先清空",true,true);

				this._enMap=map;
				return this._enMap; 
			}
		}		 
		#endregion 

	}
	/// <summary>
	/// 纳税人集合 
	/// </summary>
	public class DataLoads : EntitiesNoName
	{
		 
		/// <summary>
		/// DataLoads
		/// </summary>
		public DataLoads(){}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new DataLoad();
			}
		}
		public void RetrieveByFK_DLType( string fk_DataLoadType)
		{
			QueryObject qo = new QueryObject( this );
			qo.AddWhere( DataLoadAttr.FK_DLType , fk_DataLoadType);
			qo.DoQuery();
		}
	 
	}
}
