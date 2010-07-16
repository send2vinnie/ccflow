using System;
using System.Data;
using System.IO;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.RB
{
	/// <summary>
	/// 网站外连接
	/// </summary>
    public class WebSiteLinkAttr : EntityNoNameAttr
    {
        #region 基本属性
        /// <summary>
        /// RDT
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 发送时间
        /// </summary>
        public const string FileSize = "FileSize";
        public const string HostName = "HostName";
        public const string URL = "URL";
        /// <summary>
        /// 描述
        /// </summary>
        public const string FDesc = "FDesc";
        /// <summary>
        /// 
        /// </summary>
        public const string PageUrl = "PageUrl";
        #endregion
    }
	/// <summary>
	/// 网站外连接
	/// </summary>
    public class WebSiteLink : EntityNoName
	{	
		#region 基本属性
        /// <summary>
        /// 发送日期
        /// </summary>
        public string HostName
        {
            get
            {
                return this.GetValStringByKey(WebSiteLinkAttr.HostName);
            }
            set
            {
                this.SetValByKey(WebSiteLinkAttr.HostName, value);
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
		/// 网站外连接
		/// </summary>		
		public WebSiteLink()
		{
		}
		/// <summary>
		/// 网站外连接
		/// </summary>
        /// <param name="no"></param>
        public WebSiteLink(string no)
            : base(no)
        {
        }
		/// <summary>
		/// WebSiteLinkMap
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
                map.PhysicsTable = "RB_WebSiteLink";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "外部连接";
                map.EnType = EnType.App;
                #endregion

                #region 属性
                map.AddTBStringPK(WebSiteLinkAttr.No, null, "编号", true, true, 1, 200, 4);
                map.AddTBString(WebSiteLinkAttr.Name, null, "名称", true, true, 0, 900, 4);

                map.AddTBString(WebSiteLinkAttr.HostName, null, "HostName", true, true, 0, 900, 4);
                #endregion


                 

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion

       
	}
	/// <summary>
	/// 网站外连接
	/// </summary>
    public class WebSiteLinks : EntitiesNoName
	{
		#region 
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WebSiteLink();
			}
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 网站外连接
		/// </summary>
		public WebSiteLinks()
        {
        }
		#endregion

	 
	}
	
}
