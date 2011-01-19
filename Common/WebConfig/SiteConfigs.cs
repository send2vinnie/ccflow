using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Tax666.Common.Utils;

namespace Tax666.Common
{
    /// <summary>
    /// 网站基本设置类
    /// </summary>
    public class SiteConfigs
    {
        #region 私有字段

        private static object lockHelper = new object();
        private static System.Timers.Timer siteConfigTimer = new System.Timers.Timer(15000);
        private static SiteConfigInfo m_configinfo;

        #endregion

        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static SiteConfigs() 
        {
            m_configinfo = SiteConfigFileManager.LoadConfig();

            siteConfigTimer.AutoReset = true;
            siteConfigTimer.Enabled = true;
            siteConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            siteConfigTimer.Start();
        }

        public static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) 
        {
            ResetConfig();
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            m_configinfo = SiteConfigFileManager.LoadConfig();
        }

        public static SiteConfigInfo GetConfig()
        {
            return m_configinfo;
        }

        /// <summary>
        /// 获得设置项信息
        /// </summary>
        /// <returns>设置项</returns>
        public static bool SetIpDenyAccess(string denyipaccess)
        {
            bool result;

            lock (lockHelper)
            {
                try
                {
                    SiteConfigInfo configInfo = SiteConfigs.GetConfig();
                    configInfo.Ipdenyaccess = configInfo.Ipdenyaccess + "\n" + denyipaccess;
                    SiteConfigs.Serialiaze(configInfo, WebRequests.GetPhysicsPath(ConfigurationManager.AppSettings["CustomConfigFile"]));
                    result = true;
                }
                catch
                {
                    return false;
                }

            }
            return result;
        }

        #region Helper

        /// <summary>
        /// 序列化配置信息为XML
        /// </summary>
        /// <param name="configinfo">配置信息</param>
        /// <param name="configFilePath">配置文件完整路径</param>
        public static SiteConfigInfo Serialiaze(SiteConfigInfo configinfo, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(configinfo, configFilePath);
            }
            return configinfo;
        }


        public static SiteConfigInfo Deserialize(string configFilePath)
        {
            return (SiteConfigInfo)SerializationHelper.Load(typeof(SiteConfigInfo), configFilePath);
        }

        #endregion
    }
}
