using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;
namespace BP.YG
{
	/// <summary>
	/// 资讯
	/// </summary>
	public class InfoAttr
	{
		#region 基本属性
        public const string Title = "Title";
		public const string ReadTimes="ReadTimes";
        public const string Doc = "Doc";
        public const string RDT = "RDT";
        public const string ShareType = "ShareType";
        public const string Author = "Author";

        public const string FK_Item = "FK_Item";
        public const string FK_Model = "FK_Model";

        #endregion
    }
	/// <summary>
	/// 资讯
	/// </summary>
	public class Info :EntityOID
	{	
		#region 基本属性
        /// <summary>
        /// 阅读次数
        /// </summary>
		public int ReadTimes
		{
			get
			{
				return this.GetValIntByKey(InfoAttr.ReadTimes);
			}
			set
			{
				this.SetValByKey(InfoAttr.ReadTimes,value);
			}
		}
        public string FK_Item
        {
            get
            {
                return this.GetValStringByKey(InfoAttr.FK_Item);
            }
            set
            {
                this.SetValByKey(InfoAttr.FK_Item, value);
            }
        }
        public string FK_Model
        {
            get
            {
                return this.GetValStringByKey(InfoAttr.FK_Model);
            }
            set
            {
                this.SetValByKey(InfoAttr.FK_Model, value);
            }
        }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get
            {
                return this.GetValStringByKey(InfoAttr.Author);
            }
            set
            {
                this.SetValByKey(InfoAttr.Author, value);
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
		{
			get
			{
				return this.GetValStringByKey(InfoAttr.Title);
			}
			set
			{
                this.SetValByKey(InfoAttr.Title, value);
			}
		}
        /// <summary>
        /// 内容
        /// </summary>
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(InfoAttr.Doc);
            }
            set
            {
                this.SetValByKey(InfoAttr.Doc, value);
            }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(InfoAttr.RDT);
            }
            set
            {
                this.SetValByKey(InfoAttr.RDT, value);
            }
        }
		#endregion 

		#region 构造函数
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenAll();
				return uac;
			}
		}
		/// <summary>
		/// 资讯
		/// </summary>		
		public Info()
		{
		}
		/// <summary>
		/// InfoMap
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map();

                #region 基本属性
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "YG_Info";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "资讯";
                map.EnType = EnType.App;

                map.AddTBIntPKOID();

                map.AddTBString(InfoAttr.Title, null, "标题", true, false, 0, 300, 200, true);
                map.AddTBStringDoc();// (InfoAttr.Doc, null, "介绍", true, false, true);

                map.AddDDLSysEnum(InfoAttr.ShareType, 0, "查看权限", true, true, InfoAttr.ShareType, "@0=任何人@1=普通会员@2=白金会员@3=黄金会员@4=钻石会员");
                map.AddTBString(InfoAttr.Author, null, "作者", true, false, 0, 50, 200);
                map.AddTBInt(InfoAttr.ReadTimes, 0, "阅读次数", true, false);
                map.AddTBDate(InfoAttr.RDT, null, "发布日期", true, true);

                map.AddDDLEntities(InfoAttr.FK_Item, null, "明细", new InfoItems(), true);
                map.AddDDLEntities(InfoAttr.FK_Model, "01", "模块", new InfoModels(), false);

                #endregion

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion

		#region 方法
		/// <summary>
		/// 
		/// </summary>
		public void UpdateReadTimes()
		{
		}
		protected override bool beforeUpdateInsertAction()
		{
            InfoItem it = new InfoItem(this.FK_Item);
            this.FK_Model = it.FK_Model;

            this.RDT = DataType.CurrentData;
			return base.beforeUpdateInsertAction ();
		}
		#endregion 
	}
	/// <summary>
	/// 资讯
	/// </summary>
    public class Infos : EntitiesOID
	{
		#region Entity
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Info();
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 资讯
		/// </summary>
		public Infos()
		{
		}
		#endregion

        public int Search(int stat, string fk_tp)
        {
            QueryObject qo = new QueryObject(this);
            return qo.DoQuery();
        }
	}
	
}
