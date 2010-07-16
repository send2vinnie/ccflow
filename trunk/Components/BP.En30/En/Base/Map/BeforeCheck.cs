using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;


namespace BP.En
{
	public class CheckDataAttr
	{
		public const string MainEnName="MainEnName";
		public const string MainTBName="MainTBName";
		public const string MainTBPK="MainTBPK";

		public const string RefEnName="RefEnName";
		public const string RefTBName="RefTBName";
		public const string RefTBFK="RefTBFK";
	}
	
	public class CheckData:Entity
	{
		#region 属性
		/// <summary>
		/// 主表名称
		/// </summary>
		public string MainEnName
		{
			get
			{
				return this.GetValStringByKey(CheckDataAttr.MainEnName );
			}
			set
			{
				this.SetValByKey(CheckDataAttr.MainEnName,value);
			}
		}
		/// <summary>
		/// 主表名称
		/// </summary>
		public string MainTBName
		{
			get
			{
				return this.GetValStringByKey(CheckDataAttr.MainTBName );
			}
			set
			{
				this.SetValByKey(CheckDataAttr.MainTBName,value);
			}
		}
		/// <summary>
		/// 主表字段
		/// </summary>
		public string MainTBPK
		{
			get
			{
				return this.GetValStringByKey(CheckDataAttr.MainTBPK);
			}
			set
			{
				this.SetValByKey(CheckDataAttr.MainTBPK,value);
			}
		}
		/// <summary>
		/// 从表名称
		/// </summary>
		public string RefEnName
		{
			get
			{
				return this.GetValStringByKey(CheckDataAttr.RefEnName);
			}
			set
			{
				this.SetValByKey(CheckDataAttr.RefEnName,value);
			}
		}

		/// <summary>
		/// 从表名称
		/// </summary>
		public string RefTBName
		{
			get
			{
				return this.GetValStringByKey(CheckDataAttr.RefTBName);
			}
			set
			{
				this.SetValByKey(CheckDataAttr.RefTBName,value);
			}
		}
		/// <summary>
		/// 从表字段
		/// </summary>
		public string RefTBFK
		{
			get
			{
				return this.GetValStringByKey(CheckDataAttr.RefTBFK);
			}
			set
			{
				this.SetValByKey(CheckDataAttr.RefTBFK,value);
			}
		}
		 
		#endregion 

		#region 构造方法
		public CheckData(){}

		public CheckData(string _MainTBName,string _RefTBName)
		{
			this.MainTBName =_MainTBName;
			this.RefTBName=_RefTBName;
			this.Retrieve();	  
		}
   
		public override Map EnMap
		{
			get
			{
				if(this._enMap!=null) 
					return this._enMap;
                
				Map map=new Map("Sys_Check");
				map.EnDesc="删除更新之前作检查";

				map.AddTBStringPK(CheckDataAttr.MainEnName,null,"主表名称",true,false,1,100,10);
				map.AddTBStringPK(CheckDataAttr.MainTBName,null,"主表名称",true,false,1,100,10);
				map.AddTBString(CheckDataAttr.MainTBPK,null,"主表字段",true,false,1,100,10);
				map.AddTBStringPK(CheckDataAttr.RefEnName,null,"主表名称",true,false,1,100,10);
				map.AddTBStringPK(CheckDataAttr.RefTBName,null,"主表名称",true,false,1,100,10);

				map.AddTBString(CheckDataAttr.RefTBFK,null,"从表字段",true,false,1,100,10);

				 
				this._enMap=map;
				return this._enMap;
			}
		}
        public override Entities GetNewEntities
        {
            get { return new CheckDatas(); }
        }
		#endregion 

//		#region 重写基类方法
//		public bool Check()
//		{
//			BP.ZHZS.BeforeChecks ens=new BeforeChecks(this.PhysicsTable);
//			DataTable tb1=ens.ToDataTable();		
//			if(ens.Count==0)
//				return true;
//			else
//			{
//				foreach( DataRow dr in tb1.Rows)
//				{
//					string table1 = dr["tbName1"].ToString() ;
//					string table2=dr["tbName2"].ToString();
//					BeforeCheck en=new BeforeCheck(table1,table2);
//					for(i=1;i<=ens.Count;i++)
//					{
//						string sql="select * from '"+table1+"','"+table2+"' where '"+table2.tbField2+"'='"+table1.tbField1+"' ";	
//						DataTable dt= DBAccess.RunSQLReturnTable(sql); 
//						if(dt.Rows==0)
//							return true;
//						else
//						{
//							return false;
//							throw new Exception("@其他表正用着这个数据，不能删除！");
//						}
//					}
//				}
//			}
//		}
//
//		protected override bool beforeUpdate()
//		{
//			this.Check();
//			base.beforeUpdate();
//			return true;
//		}
//		protected override bool beforeDelete()
//		{
//			this.Check();
//			base.beforeDelete();
//			return true;
//		}	
//
//		//		protected override bool beforeDelete()
//		//		{
//		//		
//		//			}
//		//
//		//			base.beforeInsert();
//		//			return true;
//		//		}
//
// 
//
//		#endregion 
	}

	public class CheckDatas:Entities
	{
		
		public override Entity GetNewEntity
		{
			get
			{
				return new CheckData();
			}
		}
		public CheckDatas(){}
		/// <summary>
		/// 根据一个Main Table . 
		/// </summary>
		/// <param name="MainTbName"></param>
		public CheckDatas(string MainTbName)
		{
			QueryObject qo=new QueryObject(this); 
			qo.AddWhere(CheckDataAttr.MainTBName,MainTbName);
			qo.DoQuery();
		}
	}
}
