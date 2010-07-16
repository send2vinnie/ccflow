using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.RB
{
	/// <summary>
	/// 网页搜索
	/// </summary>
    public class PageTagAttr : EntityNoNameAttr
    {
        #region 基本属性
        /// <summary>
        /// Docs
        /// </summary>
        public const string Docs = "Docs";
        /// <summary>
        /// FK_Tag
        /// </summary>
        public const string FK_Tag = "FK_Tag";
        /// <summary>
        /// FK_TagText
        /// </summary>
        public const string FK_TagText = "FK_TagText";
        /// <summary>
        /// URL
        /// </summary>
        public const string URL = "URL";
        /// <summary>
        /// RDT
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 
        /// </summary>
        public const string PageSize = "PageSize";

        /// <summary>
        /// Emails
        /// </summary>
        public const string Emails = "Emails";
        /// <summary>
        /// Files
        /// </summary>
        public const string Files = "Files";
        /// <summary>
        /// 主机
        /// </summary>
        public const string HostName = "HostName";
        /// <summary>
        /// 主机名称
        /// </summary>
        public const string HostNameText = "HostNameText";
        #endregion
    }
	/// <summary>
	/// 网页搜索
	/// </summary>
    public class PageTag : EntityNoName
    {
        #region 基本属性
        public int FK_Tag
        {
            get
            {
                return this.GetValIntByKey(PageTagAttr.FK_Tag);
            }
            set
            {
                this.SetValByKey(PageTagAttr.FK_Tag, value);
            }
        }
        public string FK_TagText
        {
            get
            {
                return this.GetValStringByKey(PageTagAttr.FK_TagText);
            }
            set
            {
                this.SetValByKey(PageTagAttr.FK_TagText, value);
            }
        }
        
      
        public string Files
        {
            get
            {
                return this.GetValStringByKey(PageTagAttr.Files);
            }
            set
            {
                this.SetValByKey(PageTagAttr.Files, value);
            }
        }
        public string Emails
        {
            get
            {
                return this.GetValStringByKey(PageTagAttr.Emails);
            }
            set
            {
                this.SetValByKey(PageTagAttr.Emails, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(PageTagAttr.RDT);
            }
            set
            {
                this.SetValByKey(PageTagAttr.RDT, value);
            }
        }
        /// <summary>
        /// 网页搜索标题
        /// </summary>
        public string Docs
        {
            get
            {
                return this.GetValStringByKey(PageTagAttr.Docs);
            }
            set
            {
                this.SetValByKey(PageTagAttr.Docs, value);
                this.PageSize = value.Length;
            }
        }
        public string URL
        {
            get
            {
                return this.GetValStringByKey(PageTagAttr.URL);
            }
            set
            {
                this.SetValByKey(PageTagAttr.URL, value);
            }
        }
        /// <summary>
        /// 来自网站
        /// </summary>
        public string HostName
        {
            get
            {
                return this.GetValStringByKey(PageTagAttr.HostName);
            }
            set
            {
                this.SetValByKey(PageTagAttr.HostName, value);
            }
        }
        public string HostNameText
        {
            get
            {
                return this.GetValStringByKey(PageTagAttr.HostNameText);
            }
            set
            {
                this.SetValByKey(PageTagAttr.HostNameText, value);
            }
        }
        public int PageSize
        {
            get
            {
                return this.GetValIntByKey(PageTagAttr.PageSize);
            }
            set
            {
                this.SetValByKey(PageTagAttr.PageSize, value);
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
        /// 网页搜索
        /// </summary>		
        public PageTag()
        {
        }
        /// <summary>
        /// 网页搜索
        /// </summary>
        /// <param name="no"></param>
        public PageTag(string no)
            : base(no)
        {
        }
        /// <summary>
        /// PageTagMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();

                #region 基本属性
                map.PhysicsTable = "RB_PageTag";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "网页";
                map.EnType = EnType.App;
                #endregion

                #region 基本属性
                map.AddTBStringPK(PageTagAttr.No, null, "编号", true, true, 1, 500, 4);
                map.AddTBString(PageTagAttr.Name, null, "网页名称", true, true, 0, 4000, 30);
                map.AddTBString(PageTagAttr.URL, null, "URL", true, true, 0, 500, 30);

                map.AddTBInt(PageTagAttr.FK_Tag, 0, "FK_Tag", true, true);
                map.AddTBString(PageTagAttr.FK_TagText, null, "标签名称", true, true, 0, 500, 30);

                map.AddTBString(PageTagAttr.HostName, null, "网站", true, true, 0, 500, 30);
                map.AddTBString(PageTagAttr.HostNameText, null, "网站名称", true, true, 0, 500, 30);

                map.AddTBStringDoc(PageTagAttr.Docs, null, "Docs", true, false);
                map.AddTBInt(PageTagAttr.PageSize, 0, "PageSize", true, true);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 网页搜索
	/// </summary>
    public class PageTags : Entities
    {
        #region  GetNewEntity
        /// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PageTag();
			}
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 网页搜索
		/// </summary>
		public PageTags(){}


        public PageTags(string hostNmae) 
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(PageTagAttr.HostName, hostNmae);
            qo.DoQuery();

        }
		#endregion

	 
	}
	
}
