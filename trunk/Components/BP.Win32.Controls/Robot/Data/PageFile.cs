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
	/// 网页文件
	/// </summary>
    public class PageFileAttr : EntityNoNameAttr
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
        public const string FK_WebSite = "FK_WebSite";
        public const string URL = "URL";
        /// <summary>
        /// 描述
        /// </summary>
        public const string FDesc = "FDesc";
        /// <summary>
        /// PageUrl
        /// </summary>
        public const string PageUrl = "PageUrl";
        #endregion
    }
	/// <summary>
	/// 网页文件
	/// </summary>
    public class PageFile : EntityNoName
	{	
		#region 基本属性
        /// <summary>
        /// 发送次数
        /// </summary>
        public Int64 FileSize
        {
            get
            {
                return this.GetValInt64ByKey(PageFileAttr.FileSize);
            }
            set
            {
                this.SetValByKey(PageFileAttr.FileSize, value);
            }
        }
        /// <summary>
        /// PageUrl
        /// </summary>
        public string PageUrl
        {
            get
            {
                return this.GetValStringByKey(PageFileAttr.PageUrl);
            }
            set
            {
                this.SetValByKey(PageFileAttr.PageUrl, value);
            }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(PageFileAttr.RDT);
            }
            set
            {
                this.SetValByKey(PageFileAttr.RDT, value);
            }
        }
        /// <summary>
        /// 发送日期
        /// </summary>
        public string FK_WebSite
        {
            get
            {
                return this.GetValStringByKey(PageFileAttr.FK_WebSite);
            }
            set
            {
                this.SetValByKey(PageFileAttr.FK_WebSite, value);
            }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string FDesc
        {
            get
            {
                return this.GetValStringByKey(PageFileAttr.FDesc);
            }
            set
            {
                this.SetValByKey(PageFileAttr.FDesc, value);
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
		/// 网页文件
		/// </summary>		
		public PageFile()
		{
		}
		/// <summary>
		/// 网页文件
		/// </summary>
        /// <param name="no"></param>
        public PageFile(string no)
            : base(no)
        {
        }
		/// <summary>
		/// PageFileMap
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
                map.PhysicsTable = "RB_PageFile";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "文件(网页提取)";
                map.EnType = EnType.App;
                #endregion

                #region 属性

                map.AddTBStringPK(PageFileAttr.No, null, "编号URL", true, true, 1, 900, 4);
                map.AddTBString(PageFileAttr.Name, null, "名称", true, true, 0, 100, 30);
                map.AddTBString(PageFileAttr.PageUrl, null, "所在网页", true, true, 0, 500, 30);
                map.AddTBDate(PageFileAttr.RDT, "收录日期", true, true);

                map.AddTBString(PageFileAttr.FK_WebSite, null, "主机", true, true, 0, 300, 30);

                map.AddTBInt(PageFileAttr.FileSize, 0, "文件大小", true, true);
                map.AddTBStringDoc(PageFileAttr.FDesc, null, "描述", true, true);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion
	}
	/// <summary>
	/// 网页文件
	/// </summary>
    public class PageFiles : EntitiesNoName
	{
		#region 
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new PageFile();
			}
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 网页文件
		/// </summary>
		public PageFiles()
        {
        }
		#endregion

	 
	}
	
}
