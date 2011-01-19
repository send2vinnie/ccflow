using System;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using System.Xml;
using System.Collections.Specialized;

namespace Tax666.SystemFramework
{
	/// <summary>
	/// 从config文件中读取配置信息，处理与应用程序运行本身有关的基本配置信息。
	/// <remarks>
	///  解析Web项目中的web.config配置文件中的“Tax666Configuration”节下的配置设置。
	///  通过调用ApplicationConfiguration类中的ReadSetting()方法来读取名称/值对集合。
	/// </remarks>
	/// </summary>
	public class Tax666Configuration : IConfigurationSectionHandler
    {
        #region 私有变量：常量定义对应到Web.config的相关节
        //Tax666Configuration节中的所有项的常量。
		//说明：如果要在该配置节中定义其它项(如定义上传路径等)，请在该处定义其常量，并在该文件的相关地方赋值。
		private const String WEB_ENABLEPAGECACHE                    = "Tax666.Web.EnablePageCache";
		private const String WEB_PAGECACHEEXPIRESINSECONDS          = "Tax666.Web.PageCacheExpiresInSeconds";
		private const String DATAACCESS_CONNECTIONSTRING            = "Tax666.DataAccess.ConnectionString";
		private const String WEB_ENABLESSL                          = "Tax666.Web.EnableSsl";

        //私有变量：文件上传路径设置
        //图片路径。包括：NewsImages Gallery HelpImages UserImages
        private const String UPLOAD_IMAGEPATH                       = "Tax666.upload.ImagePath";
        private const String UPLOAD_FLASHPATH                       = "Tax666.upload.FlashPath";
        //广告所用到的图片、Flash、视频等
        private const String UPLOAD_ADLINKPATH                      = "Tax666.upload.AdLinkPath";
        private const String UPLOAD_IMPORTDATA                      = "Tax666.upload.ImportData";
        private const String UPLOAD_DOWNLOADPATH                    = "Tax666.upload.DownloadPath";
        #endregion

        #region 私有变量：常量值的默认设置
        // 所有默认设置的常量值
		private const bool   WEB_ENABLEPAGECACHE_DEFAULT            = true;
		private const int    WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT  = 7200;
		//如果要更改数据库字符串连接，请在此处修改；
		private const String DATAACCESS_CONNECTIONSTRING_DEFAULT    = "server=.;database=Tax666;uid=sa;pwd=sa";
		private const bool   WEB_ENABLESSL_DEFAULT                  = false;

        private const String UPLOAD_IMAGEPATH_DEFAULT               = "uploads/ImagePath/";
        private const String UPLOAD_FLASHPATH_DEFAULT               = "uploads/FlashPath/";
        private const String UPLOAD_ADLINKPATH_DEFAULT              = "uploads/AdLinkPath/";
        private const String UPLOAD_IMPORTDATA_DEFAULT              = "uploads/ImportData/";
        private const String UPLOAD_DOWNLOADPATH_DEFAULT            = "uploads/DownloadPath/";
        #endregion

        #region 私有变量：静态成员变量
        // 静态成员变量。它们包含了Web.config中的应用程序设置，或者默认值。
		private static String dbConnectionString;
		private static bool   enablePageCache;
		private static int    pageCacheExpiresInSeconds;
		private static bool   enableSsl;

        private static String imagePath;
        private static String flashPath;
        private static String adLinkPath;
        private static String importData;
        private static String downloadPath;
        #endregion

        #region 构造函数
        ///<summary>
		/// 构造函数。
		///</summary>
		public Tax666Configuration()
		{
			//
			// 构造函数部分；
			//
        }
        #endregion

        #region 在应用程序开始从Web.config文件中初始化设置之前由ASP.NET调用
        /// <summary>
		/// 在应用程序开始从Web.config文件中初始化设置之前由ASP.NET调用。
		/// </summary>
		///<remarks>
		/// Create()函数使用NameValueSectionHandler基类生成了一个XML的hashtable，该hashtable用来存储现有的设置。；
		///</remarks>
		/// <param name="parent">该对象用于处理Web.config文件中同名的父节点。</param>
		/// <param name="configContext">配置的内容</param>
		/// <param name="section">读取的配置节点</param>
		/// <returns>配置输出对象</returns>
		public Object Create(Object parent, object configContext, XmlNode section)
		{
			NameValueCollection settings;
			try
			{
				NameValueSectionHandler baseHandler = new NameValueSectionHandler();
				settings = (NameValueCollection)baseHandler.Create(parent, configContext, section);
			}
			catch
			{
				settings = null;
			}

			if ( settings == null )
			{
				dbConnectionString          = DATAACCESS_CONNECTIONSTRING_DEFAULT;
				pageCacheExpiresInSeconds   = WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT;
				enablePageCache             = WEB_ENABLEPAGECACHE_DEFAULT;
				enableSsl                   = WEB_ENABLESSL_DEFAULT;

                imagePath                   = UPLOAD_IMAGEPATH_DEFAULT;
                flashPath                   = UPLOAD_FLASHPATH_DEFAULT;
                adLinkPath                  = UPLOAD_ADLINKPATH_DEFAULT;
                importData                  = UPLOAD_IMPORTDATA_DEFAULT;
                downloadPath                = UPLOAD_DOWNLOADPATH_DEFAULT;
			}
			else
			{
				dbConnectionString          = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_CONNECTIONSTRING, DATAACCESS_CONNECTIONSTRING_DEFAULT);
				pageCacheExpiresInSeconds   = ApplicationConfiguration.ReadSetting(settings, WEB_PAGECACHEEXPIRESINSECONDS, WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT);
				enablePageCache             = ApplicationConfiguration.ReadSetting(settings, WEB_ENABLEPAGECACHE, WEB_ENABLEPAGECACHE_DEFAULT);
				enableSsl                   = ApplicationConfiguration.ReadSetting(settings, WEB_ENABLESSL, WEB_ENABLESSL_DEFAULT);

                imagePath                   = ApplicationConfiguration.ReadSetting(settings, UPLOAD_IMAGEPATH, UPLOAD_IMAGEPATH_DEFAULT);
                flashPath                   = ApplicationConfiguration.ReadSetting(settings, UPLOAD_FLASHPATH, UPLOAD_FLASHPATH_DEFAULT);
                adLinkPath                  = ApplicationConfiguration.ReadSetting(settings, UPLOAD_ADLINKPATH, UPLOAD_ADLINKPATH_DEFAULT);
                importData                  = ApplicationConfiguration.ReadSetting(settings, UPLOAD_IMPORTDATA, UPLOAD_IMPORTDATA_DEFAULT);
                downloadPath                = ApplicationConfiguration.ReadSetting(settings, UPLOAD_DOWNLOADPATH, UPLOAD_DOWNLOADPATH_DEFAULT);
			}

			return settings;
        }
        #endregion

        #region 属性
        /// <value>
		///  属性: 应用程序页面缓存的设置(只读类型)
		/// </value>
		/// <remarks>返回值：true - 页面可以缓存</remarks>
		public static bool EnablePageCache
		{
			get
			{
				return enablePageCache;
			}
		}

		/// <value>
		///  属性: 页面缓存超时的设置(只读类型)
		/// </value>
		/// <remarks>返回值：seconds</remarks>
		public static int PageCacheExpiresInSeconds
		{
			get
			{
				return pageCacheExpiresInSeconds;
			}
		}

		/// <value>
		///  属性: 数据库连接字符串设置(只读类型)
		/// </value>
		/// <remarks>返回值：数据库连接字符串</remarks>
		public static String ConnectionString
		{
			get
			{
				return dbConnectionString;
			}
		}

		/// <value>
		///  属性: SSL加密传输设置(只读类型)
		/// </value>
		/// <remarks>返回值：True: SSL加密方式传输</remarks>
		public static bool EnableSsl
		{
			get
			{
				return enableSsl;
			}
        }

        /// <summary>
        /// 图片路径。包括：NewsImages Gallery HelpImages UserImages
        /// </summary>
        public static string ImagePath
        {
            get { return imagePath; }
        }

        /// <summary>
        /// Flash上传路径设置
        /// </summary>
        public static String FlashPath
        {
            get { return flashPath; }
        }

        /// <summary>
        /// 广告所用到的图片、Flash、视频等
        /// </summary>
        public static String AdLinkPath
        {
            get { return adLinkPath; }
        }

        /// <summary>
        /// 数据导入、导出的路径
        /// </summary>
        public static string ImportData
        {
            get { return importData; }
        }

        /// <summary>
        /// 资源下载路径
        /// </summary>
        public static string DownloadPath
        {
            get { return downloadPath; }
        }
        #endregion

    }
}
