using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

namespace Tax666.Common
{
    /// <summary>
    /// 网站基本设置管理类
    /// </summary>
    public class SiteConfigFileManager : DefaultConfigFileManager
    {
        private static SiteConfigInfo m_configinfo;

        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime m_fileoldchange;

        /// <summary>
        /// 配置文件所在路径
        /// </summary>
        public static string filename = null;

        /// <summary>
        /// 构造函数:初始化文件修改时间和对象实例
        /// </summary>
        static SiteConfigFileManager()
        {
            m_fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);

            try
            {
                m_configinfo = (SiteConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(SiteConfigInfo));
            }
            catch
            {
                if (File.Exists(ConfigFilePath))
                {
                    m_configinfo = (SiteConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(SiteConfigInfo));
                }
            }
        }

        #region 属性

        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public new static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = (SiteConfigInfo)value; }
        }

        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        public new static string ConfigFilePath
        {
            get
            {
                if (filename == null)
                {
                    filename = WebRequests.GetPhysicsPath(ConfigurationManager.AppSettings["CustomConfigFile"]);
                }
                return filename;
            }
        }

        #endregion

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static SiteConfigInfo LoadConfig()
        {

            try
            {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            catch
            {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            return ConfigInfo as SiteConfigInfo;
        }


        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }
    }
}
