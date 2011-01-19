using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.IO;
using System.Runtime.Remoting;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Profile;
using System.Web.Security;
using Tax666.SystemFramework;
using Tax666.Common;

namespace Tax666.AppWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ApplicationConfiguration.OnApplicationStart(Context.Server.MapPath(Context.Request.ApplicationPath));
            string configPath = Path.Combine(Context.Server.MapPath(Context.Request.ApplicationPath), "remotingclient.cfg");
            if (File.Exists(configPath))
                RemotingConfiguration.Configure(configPath, false);

            Application["user_sessions"] = 0;
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (Request.Path.IndexOf('\\') >= 0 || Path.GetFullPath(Request.PhysicalPath) != Request.PhysicalPath)
                new Terminator().ThrowError("对不起，您访问的页面不存在！请检查您输入的地址是否正确。");
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            //会话计数；
            Application.Lock();
            Application["user_sessions"] = (int)Application["user_sessions"] + 1;
            Application.UnLock();
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            Application.Lock();
            Application["user_sessions"] = (int)Application["user_sessions"] - 1;
            Application.UnLock();

            // Session中的键名称常量
            const String KEY_CACHEUSER = "Cache:User:";			            //基本用户的Session-UserInfo；
            const String KEY_CACHEADMINUSER = "Cache:AdminUser:";			//后台管理用户Session-AdminUserInfo;

            DataSet UserInfo = (DataSet)(Session[KEY_CACHEUSER]);
            DataSet AdminUserInfo = (DataSet)(Session[KEY_CACHEADMINUSER]);
        }

        #region 全局的属性或方法
        /// <summary>
        /// 返回系统安装后的Web根目录虚拟根路径(/Tax666 )。
        /// </summary>
        /// <remarks>
        /// 在ASPX、ASCX中的调用：
        /// 1、引用： %@ Import Namespace="Tax666.AppWeb" %＞
        /// 2、＜%=Global.WebPath%＞/Images/Logo1.gif
        /// </remarks>
        public static string WebPath
        {
            get
            {
                if (!HttpContext.Current.Request.Url.IsDefaultPort)
                {
                    return @"http://" + string.Format("{0}:{1}", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port.ToString());
                }
                else
                {
                    return @"http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
                }
            }
        }

        /// <summary>
        /// 添加代理商管理页面的页头内容
        /// </summary>
        /// <returns></returns>
        public static string GetHeadInfo()
        {
            PageBase page = new PageBase();
            string header = page.AdminHeaderInfo();

            if (header != "" || header != string.Empty)
                return header;
            else
                return "";
        }

        /// <summary>
        /// 获取CSS文件的路径；
        /// </summary>
        /// <param name="cssName"></param>
        /// <returns></returns>
        public static string GetCSSPathInfo(string cssName)
        {
            PageBase page = new PageBase();
            string header = page.GetCSSPath(cssName);

            if (header != "" || header != string.Empty)
                return header;
            else
                return "";
        }

        /// <summary>
        /// 添加Page页面的头部信息
        /// </summary>
        /// <param name="secondTitle">副标题</param>
        /// <returns></returns>
        public static string GetHeadInfo(string secondTitle)
        {
            PageBase page = new PageBase();
            string header = page.PageHeaderInfo(secondTitle);

            if (header != "" || header != string.Empty)
                return header;
            else
                return "";
        }

        /// <summary>
        /// 添加用户管理中心 Page页面的头部信息
        /// </summary>
        /// <param name="secondTitle"></param>
        /// <returns></returns>
        public static string GetUserHeadInfo(string secondTitle)
        {
            PageBase page = new PageBase();
            string header = page.PageAccountHeader(secondTitle);

            if (header != "" || header != string.Empty)
                return header;
            else
                return "";
        }

        #endregion
    }
}