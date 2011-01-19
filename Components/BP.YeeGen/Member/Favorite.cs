using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.YG
{
	/// <summary>
	/// 收藏夹
	/// </summary>
	public class FavoriteAttr: EntityOIDAttr
	{
		#region 基本属性
		/// <summary>
		/// 文件名称
		/// </summary>
		public const  string FK_Type="FK_Type";
		/// <summary>
		/// 对应的主标题
		/// </summary>
		public const  string RefObj="RefObj";
		/// <summary>
		/// 标题
		/// </summary>
		public const  string Title="Title";
		/// <summary>
		/// 隶属
		/// </summary>
		public const string FK_Member="FK_Member";
		#endregion
	}
	/// <summary>
	/// 收藏夹
	/// </summary>
	public class Favorite :Entity
	{	
		#region 基本属性
		public const string TypeOfShareFile="SF";
		public const string TypeOfShareFileFDB="FDB";
		public const string TypeOfNews="News";
		public const string TypeOfPost="BBS";
		public const string TypeOfFAQ="FAQ";
        public const string TypeOfWord = "BK";
        public const string TypeOfLink = "Link";
        public const string TypeOfYPage = "YP";

		#endregion 


		#region 基本属性
		public string FK_Type
		{
			get
			{
				return this.GetValStringByKey(FavoriteAttr.FK_Type);
			}
			set
			{
				this.SetValByKey(FavoriteAttr.FK_Type,value);
			}
		}
		public string RefObj
		{
			get
			{
				return this.GetValStringByKey(FavoriteAttr.RefObj);
			}
			set
			{
				this.SetValByKey(FavoriteAttr.RefObj,value);
			}
		}
		public string Title
		{
			get
			{
				return this.GetValStringByKey(FavoriteAttr.Title);
			}
			set
			{
				this.SetValByKey(FavoriteAttr.Title,value);
			}
		}
		public string FK_Member
		{
			get
			{
				return this.GetValStringByKey(FavoriteAttr.FK_Member);
			}
			set
			{
				this.SetValByKey(FavoriteAttr.FK_Member,value);
			}
		}
		#endregion 

		#region 构造函数
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
		}
		/// <summary>
		/// 收藏夹
		/// </summary>		
        public Favorite()
        {
        }
		/// <summary>
		/// FavoriteMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map();

				#region 基本属性 
				map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ; 
				map.PhysicsTable="YG_Favorite";  
				map.AdjunctType = AdjunctType.AllType ;  
				map.DepositaryOfMap=Depositary.Application; 
				map.DepositaryOfEntity=Depositary.None; 
				map.IsAllowRepeatNo=false;
				map.IsCheckNoLength=false;
				map.EnDesc="收藏夹";
				map.EnType=EnType.App;
				#endregion

				#region 属性 

				map.AddTBStringPK(FavoriteAttr.FK_Type,Favorite.TypeOfShareFile,"类型",true,false,0,50,200);
				map.AddTBStringPK(FavoriteAttr.FK_Member,null,"人员",true,false,0,50,200);
				map.AddTBStringPK(FavoriteAttr.RefObj,Favorite.TypeOfShareFile,"RefObj",true,false,0,50,200);

				map.AddTBString(FavoriteAttr.Title,Favorite.TypeOfShareFile,"标题",true,false,0,500,200);
				#endregion

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion
	}
	/// <summary>
	/// 收藏夹
	/// </summary>
	public class Favorites : Entities
	{
		#region 收藏夹
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Favorite();
			}
		}
		#endregion 

		#region 构造方法
		public Favorites(){}
		public Favorites(string fk_custmor)
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere(FavoriteAttr.FK_Member, fk_custmor);
			qo.addOrderByDesc("RefObj");
			qo.DoQuery();
		}
		#endregion
	}
	
}
