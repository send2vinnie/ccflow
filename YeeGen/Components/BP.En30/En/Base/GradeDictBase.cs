
using System;
 

namespace BP.En.Base
{
	#region 字典基类的属性类
	/// <summary>
	/// 字典基类属性
	/// </summary>
	public class GradeDictBaseAttr : DictAttr
	{
		/// <summary>
		/// 级数
		/// </summary>
		public const string Grade = "Grade";
		/// <summary>
		/// 是否明细
		/// </summary>
		public const string IsDetail = "IsDetail";
	}
	#endregion

	#region 分级字典的基类
	/// <summary>
	/// 分级字典的基类
	/// </summary>
	public abstract class GradeDictBase : Dict
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		protected GradeDictBase()
		{
		}
		/// <summary>
		/// 创建对象，如果OID小于等于0则自动产生一个OID；如果大于0则查询该对象，如果不存在则创建一个OID为0的空对象
		/// </summary>
		/// <param name="oid"></param>
		protected GradeDictBase(int oid) : base(oid){}
		protected GradeDictBase(string _No ) : base(_No){}

		#region 属性
		/// <summary>
		/// 编号
		/// </summary>
		public override string No
		{
			get
			{
				 return this.GetValStringByKey(GradeDictBaseAttr.No);
				 
			}
			set
			{
				this.SetValByKey(GradeDictBaseAttr.No,value); 
//				setValue(GradeDictBaseAttr.Grade,this.GetGrade());
			}
		}
		/// <summary>
		/// 级数
		/// </summary>
		public int Grade
		{
			get
			{
				//return int.Parse(getValue(GradeDictBaseAttr.Grade));
				return 0 ;
			}
			set
			{
				///setValue(GradeDictBaseAttr.Grade,value);
			}
		}
		/// <summary>
		/// 是否明细
		/// </summary>
		public bool IsDetail
		{
			get
			{
				///return bool.Parse(getValue(GradeDictBaseAttr.IsDetail));
				return true;
			}
			set
			{
				//setValue(GradeDictBaseAttr.IsDetail,value);
			}
		}
		private string _parentNo;
		/// <summary>
		/// 通过编号取级数
		/// </summary>
		/// <returns></returns>
		private int GetGrade()
		{
			int leng=this.No.Length,i=0;
			do 
			{
				leng = leng - int.Parse(this.EnMap.CodeStruct.Substring(i,1));
				i++;
			}while(leng!=0);
			return i;
			 
		}
		/// <summary>
		/// 上级的No
		/// </summary>
		public string ParentNo
		{
			get 
			{
				int parentPos = this.No.Length - int.Parse(this.EnMap.CodeStruct.Substring(this.Grade - 1,1));
				_parentNo = this.No.Substring(0,parentPos);
				return _parentNo;
				 
			}
		}
		#endregion

		/// <summary>
		/// 取得本级的最大编号（没找到返回空串）
		/// </summary>
		/// <returns>最大编号</returns>
		public string GetGradeMaxNo()
		{
			string maxNo = "";
			if(this.Grade>0)
			{
				QueryObject qo = new QueryObject(this);
//				qo.AddSelect(new SqlField(DictBaseAttr.No,new MaxModifier()));
//				qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),this.Grade);
//				DictBase dict = (DictBase)CreateInstance();
//				maxNo = DataAccess.Query(qo,"").ToString();
			}
			return maxNo;
		}
		/// <summary>
		/// 通过编号取父级对象数据
		/// </summary>
		/// <returns></returns>
		public GradeDictBase GetParent()
		{

//			GradeDictBase dict = (GradeDictBase)CreateInstance();
//			dict = (GradeDictBase)dict.QueryByNo( this.ParentNo );
			//return dict;
			return null;
			 
		}
		/// <summary>
		/// 通过编号取孩子结点
		/// </summary>
		/// <returns></returns>
		public GradeDictBases GetChildren()
		{
//			SubStrModifier subModifier = new SubStrModifier();
//			subModifier.Place = 1;
//			subModifier.Count = this.No.Length;
//			GradeDictBases dicts = (GradeDictBases)NewCollection();
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),">",this.Grade);
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.No,subModifier),this.No);
//
//			qo.AddOrder(GradeDictBaseAttr.No,true);
//
//			DataAccess.Query(qo,dicts);
			//return dicts;
			return null;

		}
		
		/// <summary>
		/// 通过编号、级数取孩子结点
		/// </summary> 
		public GradeDictBases GetChildren(int grade)
		{
//			SubStrModifier subModifier = new SubStrModifier();
//			subModifier.Place = 1;
//			subModifier.Count = this.No.Length;
//			GradeDictBases dicts = (GradeDictBases)NewCollection();
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.No,subModifier),this.No);
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),grade);
//
//			qo.AddOrder(GradeDictBaseAttr.No,true);
//
//			DataAccess.Query(qo,dicts);
			//return dicts;
			return null;

		}

		/// <summary>
		/// 通过是否明细取孩子结点
		/// </summary> 
		public GradeDictBases GetChildren(bool isDtl)
		{
////			SubStrModifier subModifier = new SubStrModifier();
////			subModifier.Place = 1;
////			subModifier.Count = this.No.Length;
////			GradeDictBases dicts = (GradeDictBases)NewCollection();
////			QueryObject qo = GetQueryObject();
////			qo.AddWhere(new SqlField(GradeDictBaseAttr.No,subModifier),this.No);
////			qo.AddWhere(new SqlField(GradeDictBaseAttr.IsDetail),isDtl);
////
////			qo.AddOrder(GradeDictBaseAttr.No,true);
////
////			DataAccess.Query(qo,dicts);
//			return dicts;
			return null;
		}
		/// <summary>
		/// 通过编号、级数、是否明细取孩子结点
		/// </summary> 
		public GradeDictBases GetChildren(int grade,bool isDtl)
		{
//			SubStrModifier subModifier = new SubStrModifier();
//			subModifier.Place = 1;
//			subModifier.Count = this.No.Length;
//			GradeDictBases dicts = (GradeDictBases)NewCollection();
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.No,subModifier),this.No);
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),grade);
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.IsDetail),isDtl);
//
//			qo.AddOrder(GradeDictBaseAttr.No,true);
//
//			DataAccess.Query(qo,dicts);
//			return dicts;
			return null;

		}

		/// <summary>
		/// 修改前的处理
		/// </summary>
		protected override bool beforeUpdate()
		{
//			if (!base.beforeUpdate())
//				return false;
//
//			//创建一个新对象
//			GradeDictBase olddict = (GradeDictBase)CreateInstance();
//			//创建一个集合
//			GradeDictBases dicts = (GradeDictBases)NewCollection();
//			//判断是否是明细
//			if( this.IsDetail == false )
//			{   
//				olddict = (GradeDictBase)olddict.QueryByOID( this.OID );
//				if( this.No !=olddict.No )
//				{
//					dicts = olddict.GetChildren();
//					for( int i=0;i<dicts.Count;i++ )
//					{
//						int pos = this.No.Length;
//
//						((GradeDictBase)dicts[i]).No = this.No + ((GradeDictBase)dicts[i]).No.Substring(pos);
//					}
//					dicts.Update();
//				}
//			}
			return true;	
		}	
		
		/// <summary>
		/// 插入前的处理
		/// </summary>
		protected override bool beforeInsert()
		{
//			if (!base.beforeInsert())
//				return false;
//
//			//判断其上级是否存在
//			if( Grade != 1 )
//			{
//				GradeDictBase dict = this.GetParent();
//				if( dict == null )
//				{
//					throw new PanException("@L01000001",this.ParentNo);
//				}
//				dict.IsDetail = false;
//				dict.Update();
//			}
//			this.IsDetail=true;

			return true;
		}
		/// <summary>
		/// 删除前的处理
		/// </summary>
		protected override bool beforeDelete()
		{   
//			if (!base.beforeDelete())
//				return false;
//
//			//检查是否存在此对象，如果有则看其是否是明细
//			if ( this.QueryByOID( this.OID )==null|| this.IsDetail ==false )
//			{    
//				throw new PanException("@L01000002");
//			}
			return true;
		}
		/// <summary>
		/// 删除后处理
		/// </summary>
		/// <returns></returns>
		protected override void afterDelete()
		{
			base.afterDelete();

			GradeDictBase dict = this.GetParent();

			if (dict == null)
				return;

			GradeDictBases gdbs = dict.GetChildren();
			if(gdbs == null || gdbs.Count <= 0)
			{
				dict.IsDetail = true;
				dict.Update();
			}
		}		

//		/// <summary>
//		/// 删除前判断是否已经被使用 add by bluesky
//		/// </summary>
//		/// <returns></returns>
//		public override bool beforeDelete()
//		{
//			return  beforeDelete();
//		}
	}
	#endregion

	#region 分级字典集合的基类
	/// <summary>
	/// 分级的字典集合基类
	/// </summary>
	public abstract class GradeDictBases : Dicts
	{
		
		
		#region 查询
		/// <summary>
		/// 查询出所有的非明细值
		/// </summary>
		/// <returns></returns>
		public void RetrieveNotDetail()
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GradeDictBaseAttr.IsDetail,false);
			qo.addOrderBy(GradeDictBaseAttr.No );
			qo.DoQuery();
			 
		}
		/// <summary>
		/// 查询出所有的非明细值且指定级数
		/// </summary>
		/// <returns></returns>
		public void RetrieveNotDetail(int grade)
		{
//			QueryObject qo = GetQueryObject();
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.IsDetail),false);
//			qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),grade);
//			qo.AddOrder(DictBaseAttr.No,true);
//			retrieveBy(qo);
		}

		 
		/// <summary>
		/// 查询出指定级的所有数据
		/// </summary>
		/// <param name="no">指定的父节点编号</param>
		public void RetrieveChildrens(string no)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(GradeDictBaseAttr.No, " like ", no+"%" );
			qo.DoQuery();
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
//				qo.AddWhere(new SqlField(GradeDictBaseAttr.No,modifier),gradeNo);
//			}
//			//如果级数不空，则只统计该级的记录
//			if (grade > 0)
//			{
//				qo.AddWhere(new SqlField(GradeDictBaseAttr.Grade),grade);
//			}
//			//只统计明细记录
//			if (detailFlag)
//			{
//				qo.AddWhere(new SqlField(GradeDictBaseAttr.IsDetail),true);
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