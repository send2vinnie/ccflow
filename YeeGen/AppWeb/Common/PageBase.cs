using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using Tax666.SystemFramework;
using Tax666.Common;

namespace Tax666.AppWeb
{
    /// <summary>
    ///	所有aspx页面的基类。
    ///	<remarks>
    ///	该类继承自System.Web.UI.Page。
    ///	</remarks>
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        #region 私有变量定义
        /// <summary>
        /// 当前页面是否被POST请求
        /// </summary>
        protected internal bool ispost;

        /// <summary>
        /// 当前页面是否被GET请求
        /// </summary>
        protected internal bool isget;

        /// <summary>
        /// 网站基本配置信息；
        /// </summary>
        protected internal SiteConfigInfo config;

        /// <summary>
        /// 错误日志常量；
        /// </summary>
        private const String UNHANDLED_EXCEPTION = "Unhandled Exception:";

        /// <summary>
        /// Session中登录普通用户的键名称常量。
        /// </summary>
        private const String KEY_CACHEUSER = "Cache:User:";

        /// <summary>
        /// Session中后台管理用户Sessions。
        /// </summary>
        private const String KEY_CACHEADMINUSER = "Cache:AdminUser:";

        #endregion

        #region 构造函数
        public PageBase()
        {
            config = SiteConfigs.GetConfig();

            //禁止页面缓冲
            if (config.Nocacheheaders == 1)
            {
                System.Web.HttpContext.Current.Response.BufferOutput = false;
                System.Web.HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                System.Web.HttpContext.Current.Response.Expires = 0;
                System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
                System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            }

            //是否授权使用该系统；
            if (config.SiteEnabled == 0)
            {
                new Terminator().ThrowError("网站未授权无法正常使用，请与客服联系！");
            }

            // 如果IP允许访问列表有设置则进行判断
            //if (config.Ipaccess.Trim() != "")
            //{
            //    string[] regctrl = WebUtility.SplitString(config.Ipaccess, "\r\n");
            //    if (!WebUtility.InIPArray(WebUtility.GetIP(), regctrl))
            //        new Terminator().Throw("抱歉，系统设置了IP访问列表限制，您无法访问本网站！");
            //}

            // 如果IP禁止访问列表有设置则进行判断
            //if (config.Ipdenyaccess.Trim() != "")
            //{
            //    string[] regctrl = WebUtility.SplitString(config.Ipdenyaccess, "\n");
            //    if (WebUtility.InIPArray(WebUtility.GetIP(), regctrl))
            //        new Terminator().Throw("抱歉，您的IP已被禁止访问本系统！");
            //}

            ispost = WebRequests.IsPost();
            isget = WebRequests.IsGet();
        }
        #endregion

        #region 添加Web Page页面的头部信息
        /// <summary>
        /// 添加Page页面的头部信息；
        /// </summary>
        /// <param name="pageTitle"></param>
        /// <returns></returns>
        public string PageHeaderInfo(string pageTitle) 
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html;\" />\r\n");

            if (config.Webtitle != "")
                sb.AppendFormat("<title>{0} - {1}</title>\r\n", pageTitle, config.Webtitle);
            else
                sb.AppendFormat("<title>{0} - 天道管理、税务咨询高端服务门户</title>\r\n", pageTitle);

            if (config.Seokeywords != "")
                sb.AppendFormat("<meta name=\"keywords\" content=\"{0}\" />\r\n", WebUtility.RemoveHtml(config.Seokeywords).Replace("\"", " "));
            if (config.Seodescription != "")
                sb.AppendFormat("<meta name=\"description\" content=\"{0}\" />\r\n", WebUtility.RemoveHtml(config.Seodescription).Replace("\"", " "));

            sb.Append("<link rel=\"Shortcut Icon\" href=\"www.zmsoft.net/favicon.ico\"  type=\"image/x-icon\" />\r\n");
            sb.Append("<link rel=\"Bookmark\" href=\"/favicon.ico\" />\r\n");
            sb.Append("<meta http-equiv=\"x-ua-compatible\" content=\"ie=7\" />\r\n");  //解决IE6、IE7、IE8样式不兼容问题

            string defaultThemepath = "Default";
            if (config.ThemeID > 0 && config.Themepath != "")
                defaultThemepath = config.Themepath;
            sb.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}App_Themes/{1}/default.css\" />\r\n", UrlBase, defaultThemepath);

            //添加JavaScript属性；
            sb.AppendFormat("<script  language=\"javascript\" type=\"text/javascript\" src=\"{0}JScript/jquery-1.3.2.min.js\"></script>\r\n", UrlBase);
            sb.AppendFormat("<script  language=\"javascript\" type=\"text/javascript\" src=\"{0}JScript/default.js\"></script>\r\n", UrlBase);
            return sb.ToString();

        }

        /// <summary>
        /// 会员中心页面头部信息
        /// </summary>
        /// <param name="pageTitle"></param>
        /// <returns></returns>
        public string PageAccountHeader(string pageTitle)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html;\" />\r\n");

            if (config.Webtitle != "")
                sb.AppendFormat("<title>{0} - {1}</title>\r\n", pageTitle, config.Webtitle);
            else
                sb.AppendFormat("<title>{0} - 天道管理、税务咨询高端服务门户</title>\r\n", pageTitle);

            if (config.Seokeywords != "")
                sb.AppendFormat("<meta name=\"keywords\" content=\"{0}\" />\r\n", WebUtility.RemoveHtml(config.Seokeywords).Replace("\"", " "));
            if (config.Seodescription != "")
                sb.AppendFormat("<meta name=\"description\" content=\"{0}\" />\r\n", WebUtility.RemoveHtml(config.Seodescription).Replace("\"", " "));

            sb.Append("<link rel=\"Shortcut Icon\" href=\"www.zmsoft.net/favicon.ico\"  type=\"image/x-icon\" />\r\n");
            sb.Append("<link rel=\"Bookmark\" href=\"/favicon.ico\" />\r\n");
            sb.Append("<meta http-equiv=\"x-ua-compatible\" content=\"ie=7\" />\r\n");  //解决IE6、IE7、IE8样式不兼容问题

            string defaultThemepath = "Default";
            if (config.ThemeID > 0 && config.Themepath != "")
                defaultThemepath = config.Themepath;
            sb.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}App_Themes/{1}/account.css\" />\r\n", UrlBase, defaultThemepath);

            //添加JavaScript属性；
            sb.AppendFormat("<script  language=\"javascript\" type=\"text/javascript\" src=\"{0}JScript/jquery-1.3.2.min.js\"></script>\r\n", UrlBase);
            sb.AppendFormat("<script  language=\"javascript\" type=\"text/javascript\" src=\"{0}JScript/default.js\"></script>\r\n", UrlBase);
            return sb.ToString();

        }

        /// <summary>
        /// 添加管理页面的页头内容
        /// </summary>
        /// <returns></returns>
        public string AdminHeaderInfo()
        {
            StringBuilder sb = new StringBuilder();

            if (config.Webtitle != "")
                sb.Append("<title>" + config.Webtitle + " - 天道管理、税务咨询高端服务门户</title>\r\n");
            else
                sb.Append("<title>天道管理、税务咨询高端服务门户</title>\r\n");

            if (config.Seokeywords != "")
                sb.Append("<meta name=\"keywords\" content=\"" + WebUtility.RemoveHtml(config.Seokeywords).Replace("\"", " ") + "\" />\r\n");
            if (config.Seodescription != "")
                sb.Append("<meta name=\"description\" content=\"" + WebUtility.RemoveHtml(config.Seodescription).Replace("\"", " ") + "\" />\r\n");

            sb.Append("<link rel=\"Shortcut Icon\" href=\"/favicon.ico\" />\r\n");
            sb.Append("<link rel=\"Bookmark\" href=\"/favicon.ico\" />\r\n");
            sb.Append("<meta http-equiv=\"x-ua-compatible\" content=\"ie=7\" />\r\n");  //解决IE6、IE7、IE8样式不兼容问题

            sb.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + UrlBase + "App_Themes/Manager/style.css\"/>\r\n");

            //添加JavaScript属性；
            sb.Append("<script  language=\"javascript\" type=\"text/javascript\" src=\"" + UrlBase + "Manager/JScript/admin.js\"></script>\r\n");
            sb.Append("<script  language=\"javascript\" type=\"text/javascript\" src=\"" + UrlBase + "Manager/JScript/checkform.js\"></script>\r\n");

            return sb.ToString();
        }
        #endregion

        #region 普通用户和管理员用户相关属性
        /// <value>
        /// 普通用户的登录Session信息：UserInfo属性
        /// </value>
        public DataSet UserInfo
        {
            get
            {
                try
                {
                    return (DataSet)(Session[KEY_CACHEUSER]);
                }
                catch
                {
                    return (null);
                }
            }
            set
            {
                if (null == value)
                {
                    Session.Remove(KEY_CACHEUSER);
                    Session.Abandon();
                }
                else
                {
                    Session[KEY_CACHEUSER] = value;
                }
            }
        }

        /// <summary>
        /// 后台管理管理员登录的Sessions；
        /// </summary>
        public DataSet AdminUserInfo
        {
            get
            {
                try
                {
                    return (DataSet)(Session[KEY_CACHEADMINUSER]);
                }
                catch
                {
                    return (null);
                }
            }
            set
            {
                if (null == value)
                {
                    Session.Remove(KEY_CACHEADMINUSER);
                    Session.Abandon();
                }
                else
                {
                    Session[KEY_CACHEADMINUSER] = value;
                }
            }
        }
        #endregion

        #region 处理在显示页面时可能遇到的错误
        /// <summary>
        ///	处理在显示页面时可能遇到的错误。
        /// </summary>
        /// <param name="e">包含event数据的EventArgs</param>
        protected override void OnError(EventArgs e)
        {
            ApplicationLog.WriteError(ApplicationLog.FormatException(Server.GetLastError(), UNHANDLED_EXCEPTION));
            base.OnError(e);
        }
        #endregion

        #region 获取CSS文件的路径
        /// <summary>
        /// 获取CSS文件的路径；
        /// </summary>
        /// <param name="cssName"></param>
        /// <returns></returns>
        public string GetCSSPath(string cssName)
        {
            StringBuilder sb = new StringBuilder();
            //string defaultThemepath = "Default";
            //if (config.ThemeID > 0 && config.Themepath != "")
            //    defaultThemepath = config.Themepath;

            ////此处判断文件是否合法；
            //string extName = cssName.Substring(cssName.Length-2,3);

            sb.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}{1}\"/>\r\n",UrlBase,cssName);
            return sb.ToString();
        }
        #endregion

        #region 操作员用户身份校验
        /// <summary>
        /// 操作员用户身份校验
        /// </summary>
        public void PageValidCheck()
        {
            if (AdminUserInfo == null)
            {
                //跳转到登录页面；
                HttpContext.Current.Response.Redirect(string.Format("{0}Manager/AdminLogin.aspx", UrlBase), true);
            }
        }
        #endregion

        #region 属性：网站程序所在的虚拟根目录路径
        /// <summary>
        ///	网站程序所在的虚拟根目录路径
        /// 格式：http://Tax666:8080/；
        /// </summary>
        public String UrlBase
        {
            get
            {
                return WebRequests.GetWebUrl();
            }
        }
        #endregion

    }
}
