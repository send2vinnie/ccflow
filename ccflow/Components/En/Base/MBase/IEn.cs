using System;
using BP.En;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP;
 

namespace BP.En
{
	/// <summary>
	/// 多语言实体属性
	/// </summary>
	public class IEnAttr
	{
		/// <summary>
		/// 编号
		/// </summary>
		public const string No="No";
		/// <summary>
		/// OID
		/// </summary>
		public const string OID="OID";
		/// <summary>
		/// 语言
		/// </summary>
		public const string FK_Language="FK_Language";
		

	}
	/// <summary>
	/// 多语言的实体
	/// </summary>
	[Serializable]
	abstract public class IEn : Entity
	{
		#region 基本属性
		/// <summary>
		/// OID
		/// </summary>
		public int OID
		{
			get
			{
				return this.GetValIntByKey(IEnAttr.OID) ; 
			}
			set
			{
				this.SetValByKey(IEnAttr.OID,value);				 
			}
		}
		/// <summary>
		/// 编号
		/// </summary>
		public string  No
		{
			get
			{
				return this.GetValStringByKey(IEnAttr.No) ; 
			}
			set
			{
				this.SetValByKey(IEnAttr.No,value);
			}
		}
		/// <summary>
		/// 语言
		/// </summary>
		public string  FK_Language
		{
			get
			{
				return this.GetValStringByKey(IEnAttr.FK_Language) ; 
			}
			set
			{
				this.SetValByKey(IEnAttr.FK_Language,value);
			}
		}
		
		#endregion

		#region 构造
		/// <summary>
		/// 实体
		/// </summary>
		public IEn(){}
		/// <summary>
		/// 实体
		/// </summary>
		/// <param name="OID"></param>
		public IEn(int OID)
		{
			this.OID=OID;
			this.Retrieve();
		}
		/// <summary>
		/// 实体
		/// </summary>
		/// <param name="no"></param>
		/// <param name="fk_language"></param>
		public IEn(string no, string fk_language)
		{
			this.No=no;
			this.FK_Language = fk_language;
		}
		#endregion

		#region 关于 Map

		#region 需要子类重写的方法
		/// <summary>
		/// EntityNo
		/// </summary>
		protected EntityNo _enOfA=null;
		/// <summary>
		/// EntityOIDNo
		/// </summary>
		protected EntityOIDNo _enOfM=null;
		/// <summary>
		/// 实体
		/// </summary>
		public EntityNo  EnOfA
		{
			get
			{
				if ( _enOfA==null)
					this._enOfA=(EntityNo)this.GetNewEnsOfA.GetNewEntity;
				return this._enOfA;
			}
		}
		/// <summary>
		/// HisEnOIDNo
		/// </summary>
		public EntityOIDNo EnOfM
		{
			get
			{
				if ( _enOfM==null)
					this._enOfM=(EntityOIDNo)this.GetNewEnsOfM.GetNewEntity;
				return this._enOfM;
			}
		}



		/// <summary>
		/// 子类需要实现
		/// </summary>
		public abstract EntitiesNo GetNewEnsOfA{get;}
		/// <summary>
		/// 子类需要实现
		/// </summary>
		public abstract EntitiesOIDNo GetNewEnsOfM{get;}
		/// <summary>
		/// 实体A
		/// </summary>
		public Map MapOfA
		{
			get
			{
				return this.EnOfA.EnMap;
			}
		}
		/// <summary>
		/// 实体M
		/// </summary>
		public Map MapOfM
		{
			get
			{
				return this.EnOfM.EnMap;
			}
		}
		/// <summary>
		/// 这里需要把两个 map 整合在一起。
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap==null)
				{
					Map map= new Map(this.MapOfA.EnDBUrl.DBUrlType, this.PhysicsView);
					map.Attrs = this.MapOfA.Attrs;
					foreach(Attr attr in this.MapOfM.Attrs)					
						map.Attrs.Add(attr);
                    this._enMap = map;
				}
				return this._enMap;
			}
		}
		/// <summary>
		/// 物理视图
		/// </summary>
		public string PhysicsView
		{
			get
			{
				string viewName="V_"+this.MapOfA.PhysicsTable+"_"+this.MapOfM.PhysicsTable;		
				if (BP.SystemConfig.IsDebug==false)
					return viewName ;
		 
				string sql="SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE  TABLE_NAME = '"+viewName+"'";			
				if (DBAccess.IsExits(sql)==false)
				{
					sql ="CREATE VIEW "+viewName+" AS SELECT  a.*, m.* FROM "+this.MapOfA.PhysicsTable+" a , "+this.MapOfM.PhysicsTable+" m WHERE a.No=m."+this.MapOfM.GetFieldByKey("No");
					DBAccess.RunSQL(sql);
				}
				return viewName;
			}
		}
		/// <summary>
		/// 建立视图
		/// </summary>
		private void CreateView()
		{
			//CreateViewOfMSSQL();
		}
		/// <summary>
		/// 建立 ms view 
		/// </summary>
		private void CreateViewOfMSSQL_del()
		{
			if (BP.SystemConfig.IsDebug==false)
				return ;

			string sql="SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE  TABLE_NAME = '"+this.PhysicsView+"'";			
			if (DBAccess.IsExits(sql)==false)
			{
				sql ="CREATE VIEW "+PhysicsView+" AS SELECT  a.*, m.* FROM "+this.MapOfA.PhysicsTable+" a , "+this.MapOfM.PhysicsTable+" m WHERE a.No=m.No go ";
				DBAccess.RunSQL(sql);
			}
		}
		/// <summary>
		/// 建立视图
		/// </summary>
		private void CreateViewOfOracle9i()
		{			
		}
		#endregion		 

		#endregion 
		 
		#region 基本操作
		/// <summary>
		/// 按照OID 查询.
		/// </summary>
		/// <returns></returns>
		public int RetrieveByOID()
		{
			return 0;
		}
		/// <summary>
		/// 按照OID 查询.
		/// </summary>
		/// <returns></returns>
		public int RetrieveByLanguageNo(string fk_lang, string no)
		{
			this.FK_Language=fk_lang;
			this.No=no;
			QueryObject qo= new QueryObject(this.EnOfM);			
			return  0;
		}
		/// <summary>
		/// 按主键查询，返回查询出来的个数。
		/// 如果查询出来的是多个实体，那把第一个实体给值。
		/// </summary>
		/// <returns>查询出来的个数</returns>
		public new int Retrieve()
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere("OID",this.OID);
			if (qo.DoQuery()==0)
				throw new Exception("没有这条记录"+this.OID.ToString() );
		

//		//	if (
//			if (this.OID==0 || (this.FK_Language=="" && this.No="")  )
//				return 
				
			return 0;
			/*
			int i =EnDA.Retrieve(this,SqlBuilder.Retrieve(this));
			if (i ==0)			 			 
				throw new Exception("没有["+this.EnMap.EnDesc+"  "+this.EnMap.PhysicsTable+", 类["+this.ToString()+"], 实例。PK = "+this.GetValByKey(this.PK)  );
			return i;
			*/
		}
		/// <summary>
		/// 按照主键查询，查询出来的结果不赋给当前的实体。
		/// </summary>
		/// <returns>查询出来的个数</returns>
		public new int RetrieveNotSetValues()
		{
			return 0 ;
			//return  DBAccess.RunSQLReturnTable(SqlBuilder.Retrieve(this)).Rows.Count ;            		 
		}

		#region delete 
		/// <summary>
		/// 检查完整。
		/// </summary>
		/// <returns>true/false</returns>
		private bool CheckDB()
		{
            return true;

            //CheckDatas  ens=new CheckDatas(this.EnMap.PhysicsTable);
            //if (ens.Count==0)
            //    return true;
            //foreach(CheckData en in ens)
            //{
            //    string sql="SELECT "+en.RefTBFK+" FROM  "+en.RefTBName+"   WHERE  "+en.RefTBFK+" ='"+this.GetValByKey(en.MainTBPK) +"' ";	
            //    DataTable dt= DBAccess.RunSQLReturnTable(sql); 
            //    if(dt.Rows.Count==0)
            //        continue;
            //    else				 				
            //        throw new Exception("["+this.EnDesc+"],删除期间出现错误，原因是：["+en.RefEnName+"]，与之有["+dt.Rows.Count.ToString()+"]个关联，不能删除！");
            //}
            //return true;
		}
		protected override bool beforeDelete() 
		{
			this.CheckDB();
			return true;
		}
		public new void Delete()
		{			  
			if (this.beforeDelete() == false)
				return;

			
			 
			//EnDA.Delete(this);
			this.afterDelete();
		}
		protected   override void afterDelete()  
		{
		 
			return;
		}
		#endregion

		#region insert 
		protected override bool beforeInsert()		
		{
			return true;
		}
		 
		public override void Insert()
		{
			if (this.beforeInsert() == false)
				return;
			if (this.beforeUpdateInsertAction() == false)
				return;

			//EnDA.Insert(this);
			this.afterInsert();
		}
		protected override void afterInsert() 
		{
			return;
		}
		/// <summary>
		/// 在更新与插入之后要做的工作.
		/// </summary>
		protected override void afterInsertUpdateAction() 
		{
			if (this.EnMap.HisFKEnumAttrs.Count> 0)
				this.Retrieve();
			return ;
		}
		#endregion

		 

	 
		
		 
		#endregion
	 
		 
	}	
	/// <summary>
	/// 多语言的实体集合
	/// </summary>
	[Serializable]
	public abstract class IEns : Entities
	{
		#region 构造函数
		public IEns(  string fk_language) 
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(IEnAttr.FK_Language,fk_language); 
			qo.DoQuery();
		}
		/// <summary>
		/// 构造函数
		/// </summary>
		public IEns(){}
		#endregion
	}
}
