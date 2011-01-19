using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;

namespace Tax666.Common
{
    public class CommonUtil
    {
        #region 生成0-9随机数
        /// <summary>
        /// 生成0-9随机数
        /// </summary>
        /// <param name="VcodeNum">生成长度</param>
        /// <returns></returns>
        public static string RndNum(int VcodeNum)
        {
            StringBuilder sb = new StringBuilder(VcodeNum);
            Random rand = new Random();
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                int t = rand.Next(9);
                sb.AppendFormat("{0}", t);
            }
            return sb.ToString();

        }
        #endregion

        #region 获得日志文件存放目录
        /// <summary>
        /// 获得日志文件存放目录
        /// </summary>
        public static string LogDir
        {
            get
            {
                string LogFilePath = WebRequests.GetPhysicsPath(ConfigurationManager.AppSettings["LogDir"]);
                if (!Directory.Exists(LogFilePath))
                    Directory.CreateDirectory(LogFilePath);
                return LogFilePath;
            }
        }
        #endregion

        #region 获得缓存类配置(命名空间)
        /// <summary>
        /// 获得缓存类配置(命名空间)
        /// </summary>
        public static string GetCachenamespace
        {
            get
            {
                return ConfigurationManager.AppSettings["Cachenamespace"];
            }
        }
        #endregion

        #region 获得缓存类配置(类名)
        /// <summary>
        /// 获得缓存类配置(类名)
        /// </summary>
        public static string GetCacheclassName
        {
            get
            {
                return ConfigurationManager.AppSettings["CacheclassName"];
            }
        }
        #endregion 

    }
}
