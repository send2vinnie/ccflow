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
    ///	所有ascx控件页面的基类,UserControl用户控件的过渡类,同时是所有页面的基类,所有的页面都继承该类.
    ///	<remarks>
    ///	该类继承自System.Web.UI.UserControl。
    ///	</remarks>
    /// </summary>
    public class ModuleBase : System.Web.UI.UserControl
    {
        #region 常量定义
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

        #region 属性
        /// <summary>
        ///	网站程序所在的虚拟根目录路径
        /// 格式：http://192.168.8.170:8008/Tax666/ 或 http://192.168.8.170:8008/；
        /// </summary>
        public String UrlBase
        {
            get
            {
                return WebRequests.GetWebUrl();
            }
        }

        /// <value>
        /// UserInfo属性用于get或者set已登录的用户的相关数据。
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
        #endregion

        #region 属性：后台管理管理员登录的Sessions
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

    }
}