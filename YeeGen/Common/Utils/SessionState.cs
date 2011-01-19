using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Tax666.Common
{
    public class SessionState
    {
        #region 从 Session 读取 键为 name 的值
        /// <summary>
        /// 从 Session 读取 键为 name 的值
        /// </summary>
        public static object Get(string name)
        {
            string appPrefix = GetAppPrefix("AppPrefix");
            return (object)HttpContext.Current.Session[appPrefix + name];
        }
        #endregion

        #region 向 Session 保存 键为 name 的， 值为 value
        /// <summary>
        /// 向 Session 保存 键为 name 的， 值为 value
        /// </summary>
        public static void Set(string name, object value)
        {
            string appPrefix = GetAppPrefix("AppPrefix");
            HttpContext.Current.Session.Add(appPrefix + name, value);
        }
        #endregion

        #region 从 Session 删除 键为 name session 项
        /// <summary>
        /// 从 Session 删除 键为 name session 项
        /// </summary>
        public static void Remove(string name)
        {
            string appPrefix = GetAppPrefix("AppPrefix");
            if (HttpContext.Current.Session[appPrefix + name] != null)
            {
                HttpContext.Current.Session.Remove(appPrefix + name);
            }
        }
        #endregion

        #region 删除所有 session 项
        /// <summary>
        /// 删除所有 session 项
        /// </summary>
        public static void RemoveAll()
        {
            HttpContext.Current.Session.RemoveAll();
        }
        #endregion

        /// <summary>
        /// 获取web.config的配置项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppPrefix(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }
}
