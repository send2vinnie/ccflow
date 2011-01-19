using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;

namespace Tax666.Common
{
    public class PageParameterSet
    {
        #region 分页每页记录数(默认10)
        /// <summary>
        /// 分页每页记录数(默认10)
        /// </summary>
        public static int PageSize
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["PageSize"] == null)
                {
                    return 10;
                }
                else
                {
                    return Convert.ToInt32(HttpContext.Current.Request.Cookies["PageSize"].Value);
                }
            }
            set
            {
                HttpContext.Current.Response.Cookies["PageSize"].Value = value.ToString();
            }
        }
        #endregion

        #region 用户在线过期时间 (分)默认30分 如果用户在当前设定的时间内没有任何操作,将会被系统自动退出
        /// <summary>
        /// 用户在线过期时间 (分)默认30分 如果用户在当前设定的时间内没有任何操作,将会被系统自动退出
        /// </summary>
        public static int OnlineMinute
        {
            get
            {
                try
                {
                    int _onlineminute = Convert.ToInt32(ConfigurationManager.AppSettings["OnlineMinute"]);
                    if (_onlineminute == 0)
                        return 10000;
                    else
                        return _onlineminute;
                }
                catch
                {
                    return 30;
                }
            }
        }
        #endregion

    }
}
