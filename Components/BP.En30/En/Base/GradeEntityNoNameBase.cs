
using System;
using BP.DA;
using System.Data;
using System.Collections ;
using BP.En;
 

namespace BP.En
{
	#region 字典基类的属性类
	/// <summary>
	/// 字典基类属性
	/// </summary>
	public class GradeEntityNoNameBaseAttr : EntityNoNameAttr
	{
		/// <summary>
		/// 级数
		/// </summary>
		public const string Grade = "Grade";
		/// <summary>
		/// 是否明细
		/// </summary>
		public const string IsDtl = "IsDtl";
	}
	#endregion

	#region 分级字典的基类
	/// <summary>
	/// 分级字典的基类
	/// </summary>
	public abstract class GradeEntityNoNameBase : EntityNoName
	{
		#region 常用方法
		/// <summary>
		/// 产生同级编号
		/// </summary>
		/// <returns></returns>
		public string GenerSameGradeNo()
		{
			string field=this.EnMap.GetAttrByKey("No").Field ;
			int startPos=this.GetNoLengthByGrade(this.Grade-1)+1;
			

			string sql=null;
			switch(SystemConfig.AppCenterDBType)
			{
				case DBType.SQL2000:
					sql="SELECT CONVERT(INT, MAX( SUBSTRING("+field+","+startPos.ToString()+","+this.CodeStueOfThisGrade+"))  )+1 AS No FROM "+this.EnMap.PhysicsTable+" WHERE len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade) +" AND "+field+" like '"+this.No+"%' AND len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade)  ;
					break;
				case DBType.Access:
					sql="SELECT CONVERT(INT, MAX( SUBSTRING("+field+","+startPos.ToString()+","+this.CodeStueOfThisGrade+"))  )+1 AS No FROM "+this.EnMap.PhysicsTable+" WHERE len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade) +" AND "+field+" like '"+this.No+"%' AND len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade)  ;
					break;
				case DBType.Oracle9i:
					sql="SELECT   MAX( SUBSTRING("+field+","+startPos.ToString()+","+this.CodeStueOfThisGrade+"))   +1 AS No FROM "+this.EnMap.PhysicsTable+" WHERE len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade) +" AND "+field+" like '"+this.No+"%' AND len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade)  ;
					break;
				default:
					throw new Exception("error11 ");
			}

			 
		 
			DataTable dt = new DataTable();
			switch (this.EnMap.EnDBUrl.DBUrlType )
			{
				case DBUrlType.AppCenterDSN:
					dt=DBAccess.RunSQLReturnTable(sql);
					break;
				case DBUrlType.DBAccessOfMSSQL2000:
					dt=DBAccessOfMSSQL2000.RunSQLReturnTable(sql);
					break;
				case DBUrlType.DBAccessOfOracle9i:
					dt=DBAccessOfOracle9i.RunSQLReturnTable(sql);
					break;
				default:
					throw new Exception("@sys error ");						
			}
			  
			string str="1";
			if (dt.Rows.Count!=0)
				str=dt.Rows[0][0].ToString();
			if ( str.Length > int.Parse(this.CodeStueOfThisGrade) )
				throw new Exception("@当前的编号需要超出了编号规则定义的范围。生成新的编号失败。");
	
			str=this.NoOfParent + str.PadLeft(this.NoOfThisGrade.Length,'0') ;
			return str;
		}
		/// <summary>
		/// 产生孩子编号
		/// </summary>
		/// <returns>孩子编号</returns>
		public string GenerChildGradeNo(string thisNo)
		{
			string field=this.EnMap.GetAttrByKey("No").Field ;
			int startPos=this.GetNoLengthByGrade(this.Grade)+1;
			string sql ="SELECT CONVERT(INT, MAX( SUBSTRING("+field+","+startPos.ToString()+","+this.CodeStueOfChildGrade+"))  )+1 AS No FROM "+this.EnMap.PhysicsTable+" WHERE len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade+1) +" AND "+field+" like '"+this.No+"%' AND len( rtrim(ltrim("+field+")) )="+this.GetNoLengthByGrade(this.Grade+1)  ;
			DataTable dt = DBAccess.RunSQLReturnTable(sql) ; 
			string str="1";
			if (dt.Rows.Count!=0)
				str=dt.Rows[0][0].ToString();

			if ( str.Length > int.Parse(this.CodeStueOfThisGrade) )
				throw new Exception("@生成的分级编号["+str+"]超出了编号规则定义的["+CodeStueOfThisGrade+"]范围。生成新的编号失败。"+sql);
	
			str= thisNo + str.PadLeft(this.NoOfThisGrade.Length,'0') ;

			return str;
		}
		/// <summary>
		/// 生成一个新的子类实体。
		/// </summary>
		/// <returns></returns>
		public GradeEntityNoNameBase NewChildEntity()
		{

			// 判断还能不能增加下级别
			if (this.No.Length >=this.NoLengthOfMax)
				throw new Exception("@此级别是最高级别，您不能在增加下级别。");

			int grade=this.Grade;
			string thisNo=this.No;
			string newChildNo= this.GenerChildGradeNo( thisNo);
			this.ResetDefaultVal();
			this.Grade=grade+1 ;
		    this.No =newChildNo; 
			this.IsDtl=true;			
			return this;

		}
		/// <summary>
		/// 通过一个编号判断他的级别。
		/// </summary>
		/// <param name="No">要判断的编号</param>
		/// <returns>级别</returns>
		public int GetGradeByNo(string checkNo)
		{
			char[] stru=this.EnMap.CodeStruct.ToCharArray() ; 
			
			int i = 0;
			int len=0;
			foreach(char c in stru)
			{
				i++;
				len=len+int.Parse(c.ToString() ) ; 
				if (len==checkNo.Length)
					return i ;				
			}
			throw new Exception("@您在判断["+this.EnDesc+"]中，判断的编号["+checkNo+"]，不合法。");

			
		}
		/// <summary>
		/// 按照级别取到编号的长度
		/// </summary>
		/// <param name="grade">级别</param>
		/// <returns>length</returns>
		public int GetNoLengthByGrade(int grade)
		{
			string str = this.EnMap.CodeStruct.Substring(0,grade); 
			char[] stru=str.ToCharArray();		
			 
			int len=0;
			foreach(char c in stru)
			{
				 
				len=len+int.Parse(c.ToString() ) ; 
				 		
			}
			return len;			
		}
		#endregion 

		#region 构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		protected GradeEntityNoNameBase()
		{
		}
		protected GradeEntityNoNameBase(string _No ) : base(_No){}

		#endregion 

		#region 基本属性
		/// <summary>
		/// 出当前最大的级别．
		/// </summary>
		public int GradeMax
		{
			get
			{
				return this.EnMap.CodeStruct.Length;
			}
		}
 		/// <summary>
		/// 级数
		/// </summary>
		public int Grade
		{
			get
			{
				//return this.No.Length/2;
			    return 	this.GetValIntByKey(GradeEntityNoNameBaseAttr.Grade) ; 				 
			}
			set
			{
				this.SetValByKey(GradeEntityNoNameBaseAttr.Grade,value) ; 
			}
		}
		/// <summary>
		/// 是否明细
		/// </summary>
		public bool IsDtl
		{
			get
			{
				return 	this.GetValBooleanByKey(GradeEntityNoNameBaseAttr.IsDtl) ; 				 
			}
			set
			{
				this.SetValByKey(GradeEntityNoNameBaseAttr.IsDtl,value) ; 
			}
		}
		
		#endregion

		#region 扩充属性
		/// <summary>
		/// 当前级别的编号
		/// </summary>
		public string NoOfThisGrade
		{
			get
			{
				if (this.Grade==1)
					return this.No;

				return this.No.Substring(this.NoLengthOfParent);
			}
		}
		/// <summary>
		/// 当前级别的编码。
		/// </summary>
		public string CodeStueOfThisGrade
		{
			get
			{
				if (this.Grade > this.EnMap.CodeStruct.Length)
					throw new Exception("@["+this.EnDesc+"]定义的编码["+this.EnMap.CodeStruct+"]结构太短.");
				return this.EnMap.CodeStruct.Substring(this.Grade-1,1);
			}
		}
		public string CodeStueOfChildGrade
		{
			get
			{
				if (this.Grade+1 > this.EnMap.CodeStruct.Length)
					throw new Exception("@["+this.EnDesc+"]定义的编码["+this.EnMap.CodeStruct+"]结构太短.");
				return this.EnMap.CodeStruct.Substring(this.Grade,1);
			}
		}
		/// <summary>
		/// 当前级别的上机编码。
		/// </summary>
		public string CodeStueOfParent
		{
			get
			{
				if (this.Grade==1)
					throw new Exception("@当前是最大级别，不能取到上级编码。"); 
				return this.EnMap.CodeStruct.Substring(this.Grade-1,1);
			}
		}
		/// <summary>
		/// 上级别的编码长度。
		/// </summary>
		public int NoLengthOfParent
		{
			get
			{
				if (this.Grade==1)
					throw new Exception("@当前是最大级别，不能取到上级编码的长度。"); 
				return this.No.Length - int.Parse(this.CodeStueOfThisGrade) ; 
			}
		}
		/// <summary>
		/// 上级编号
		/// </summary>
		public string NoOfParent
		{
			get
			{
				return this.No.Substring(0,NoLengthOfParent);
			}
		}
		public int NoLengthOfMax
		{
			get
			{
				int i = 0 ;
				char[] strs=this.EnMap.CodeStruct.ToCharArray() ; 
				foreach(char s in strs)
				{
					i= i+int.Parse(s.ToString());
				}
				return i ;
			}
		}
		/// <summary>
		/// 上级的No
		/// </summary>
		public string ParentNo_del
		{
			get 
			{
				if (this.Grade==1)
					throw new Exception("@此节点是最高级别。");

				int parentPos = this.No.Length - int.Parse(this.EnMap.CodeStruct.Substring(this.Grade - 1,1));
				string _parentNo = this.No.Substring(0,parentPos);
				return _parentNo;				 
			}
		}
		
		#endregion

		/// <summary>
		/// 取到他的父节点
		/// </summary>
		public  GradeEntityNoNameBase Parent
		{
			get
			{
				GradeEntityNoNameBase en = (GradeEntityNoNameBase)this.CreateInstance();
				en.No = this.NoOfParent ;
				en.Retrieve();
				return en;
			}
		}
		/// <summary>
		/// 取出全部的孩子节点.
		/// </summary>
		/// <returns></returns>
		public GradeEntitiesNoNameBase GetChildren()
		{
			GradeEntitiesNoNameBase ens = (GradeEntitiesNoNameBase)this.GetNewEntities;
			QueryObject qo = new QueryObject(ens) ;
			qo.AddWhere(GradeEntityNoNameBaseAttr.Grade," > ",this.Grade );
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, "like", this.No +"%"); 
			qo.DoQuery();
			return ens ;
		}
		
		/// <summary>
		/// 通过编号、级数取孩子结点
		/// </summary> 
		public GradeEntitiesNoNameBase GetChildren(int grade)
		{
			GradeEntitiesNoNameBase ens = (GradeEntitiesNoNameBase)this.GetNewEntities;
			QueryObject qo = new QueryObject(ens) ;
			qo.AddWhere(GradeEntityNoNameBaseAttr.Grade," = ",grade );
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, "like", this.No+"%"); 
			qo.DoQuery();
			return ens ;
		}
		/// <summary>
		/// 取出下级别的节点.
		/// </summary> 
		public GradeEntitiesNoNameBase GetChildrenOfNextGrade()
		{
			return GetChildren(this.Grade+1);			 
		}
		/// <summary>
		/// 取出下级别的节点
		/// </summary>
		/// <param name="isForDtl">是否明细</param>
		/// <returns></returns>
		public GradeEntitiesNoNameBase GetChildrenOfNextGrade(bool isForDtl)
		{
			return GetChildren(this.Grade+1,isForDtl);
		}
		/// <summary>
		/// 通过编号、级数、是否明细取孩子结点
		/// </summary> 
		public GradeEntitiesNoNameBase GetChildren(int grade,bool isDtl)
		{
			GradeEntitiesNoNameBase ens = (GradeEntitiesNoNameBase)this.GetNewEntities;
			QueryObject qo = new QueryObject(ens) ;
			qo.AddWhere(GradeEntityNoNameBaseAttr.Grade," = ",grade );
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, "like", this.No +"%"); 
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.IsDtl, isDtl);
			qo.DoQuery();
			return ens ;
		}
		/// <summary>
		/// 修改前的处理
		/// </summary>
        protected override bool beforeUpdate()
        {
            this.Grade = this.No.Length / 2;
            return true;
        }
		/// <summary>
		/// 插入前的处理
		/// </summary>
		protected override bool beforeInsert()
		{
			//return base.beforeInsert();
			if (!base.beforeInsert())
				return false;
			if (this.EnMap.IsCheckNoLength==false)
				return true;
			//判断其上级是否存在
			if( Grade != 1 )
			{
				GradeEntityNoNameBase en =Parent;
				if (en.IsDtl)
				{
					en.IsDtl=false;
					en.Update();
				}
			}
			this.IsDtl=true;
			return true;
		}
		/// <summary>
		/// 删除前的处理
		/// </summary>
		protected override bool beforeDelete()
		{
			//return base.beforeInsert();
			if (!base.beforeDelete())
				return false;
			if (this.GetChildren().Count > 0)
				throw new Exception("@实体是非明细，不能删除。");
			return true;
		}
		/// <summary>
		/// 删除后处理, 判断他的父级.如果是明细就设置.他.
		/// </summary>
		/// <returns></returns>
		protected override void afterDelete()
		{
			base.beforeInsert();
			return ;

			/*
			base.afterDelete();
			GradeEntityNoNameBase  parent = this.Parent;
			if ( parent.GetChildren().Count > 0 )
			{
				parent.IsDtl=true;
				parent.Update();
			}
			*/
		}
	}
	#endregion

	#region 分级字典集合的基类
	/// <summary>
	/// 分级的字典集合基类
	/// </summary>
	public abstract class GradeEntitiesNoNameBase : EntitiesNoName
	{		 
		#region 在集合内根据级别取出实体。
		/// <summary>
		/// 
		/// </summary>
		/// <param name="grade"></param>
		/// <returns></returns>
		public GradeEntitiesNoNameBase GetEntitiesByGrade(int grade)
		{
			return null;	 
		}
		#endregion
		
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="parntNo">编号</param>
		public GradeEntitiesNoNameBase RetrieveByParnt(string parntNo)
		{
			GradeEntityNoNameBase en = (GradeEntityNoNameBase)this.GetNewEntity;
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, " like ",parntNo+"%");
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.Grade, en.GetGradeByNo(parntNo)+1 );
			qo.DoQuery();
			return this; 
		}
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="parntNo">编号</param>
		public GradeEntitiesNoNameBase RetrieveByParnt(string parntNo, bool IsJustFo )
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, " like ",parntNo+"%");
			qo.addAnd();
			qo.AddWhere(GradeEntityNoNameBaseAttr.No, " like ",parntNo+"%");

			qo.DoQuery();
			return this; 
		}

		#region 查询
		/// <summary>
		/// 查询出所有的非明细值且指定级数
		/// </summary>
		/// <returns></returns>
		public void QueryGradeEntitiesNoNameBase(int grade)
		{
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.IsDetail),false);
//			qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.Grade),grade);
//			qo.AddOrder(DictBaseAttr.No,true);
//			retrieveBy(qo);
		}
		/// <summary>
		/// 查询出指定级的所有数据
		/// </summary>
		/// <returns></returns>
		public void RetrieveByGrade(int grade)
		{
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.Grade),grade);
//			qo.AddOrder(DictBaseAttr.No,true);
//
//			retrieveBy(qo);
		}

		#endregion
		
		#region Count 统计
		/// <summary>
		/// 统计该级的所有记录数
		/// </summary>
		/// <param name="grade">级数</param>
		/// <returns>记录数</returns>
		public int CountBy(int grade)
		{	
			return CountBy("",grade,false);
		}
		/// <summary>
		/// 统计该编号的所有下级
		/// </summary>
		/// <param name="gradeNo">编号</param>
		/// <returns>记录数</returns>
		public int CountBy(string gradeNo)
		{	
			return CountBy(gradeNo,0,false);
		}
		/// <summary>
		/// 统计该编号下的指定级数的记录数
		/// </summary>
		/// <param name="gradeNo">编号</param>
		/// <param name="grade">级数</param>
		/// <returns>记录数</returns>
		public int CountBy(string gradeNo,int grade)
		{	
			return CountBy(gradeNo,grade,false);
		}
		/// <summary>
		/// 统计对象的记录数
		/// </summary>
		/// <param name="gradeNo">上级编号，如果为空则统计所有</param>
		/// <param name="grade">本级级数，如果小于等于0则统计所有</param>
		/// <param name="detailFlag">是否只统计明细记录</param>
		/// <returns>记录数</returns>
		public int CountBy(string gradeNo,int grade,bool detailFlag)
		{	
//			QueryObject qo = this.GetQueryObject();
//			qo.AddSelect(new SqlString("1",new CountModifier()));
//
//			//如果上级编号不空，则只统计该编号下的记录
//			if(!Pansoft.ArguIEntCheck.CheckEmptyString(gradeNo))
//			{
//				SubStrModifier modifier = new SubStrModifier();
//				modifier.Count = gradeNo.Length;
//				qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.No,modifier),gradeNo);
//			}
//			//如果级数不空，则只统计该级的记录
//			if (grade > 0)
//			{
//				qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.Grade),grade);
//			}
//			//只统计明细记录
//			if (detailFlag)
//			{
//				qo.AddWhere(new SqlField(GradeEntityNoNameBaseAttr.IsDetail),true);
//			}
//			object result = DataAccess.Query(qo,0);
//			if(result==DBNull.Value)
//				return 0;
//			else
//				return (int)result;

			return 0 ;
		}
		#endregion
	}
	#endregion
}