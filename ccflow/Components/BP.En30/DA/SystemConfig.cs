#region Copyright
//------------------------------------------------------------------------------
// <copyright file="ConfigReaders.cs" company="BP">
//     
//      Copyright (c) 2002 Microsoft Corporation.  All rights reserved.
//     
//      BP ZHZS Team
//      Purpose: config system: finds config files, loads config factories,
//               filters out relevant config file sections
//      Date: Oct 14, 2003
//      Author: peng zhou (pengzhoucn@hotmail.com) 
//      http://www.BP.com.cn
//
// </copyright>                                                                
//------------------------------------------------------------------------------
#endregion

using System;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Data;
using System.IO;

namespace BP
{	 
	/// <summary>
	/// 用户编号(用来区分那家用户在使用它)
	/// </summary>
    public class CustomerNoList
    {

        #region 地税局
        /// <summary>
        /// 青海地税
        /// </summary>
        public const string QHDS = "QHDS";
        /// <summary>
        /// 海南地税
        /// </summary>
        public const string HNDS = "HNDS";
        /// <summary>
        /// 时代智囊
        /// </summary>
        public const string CCS = "CCS";
        /// <summary>
        /// 莒南县地方税务局
        /// </summary>
        public const string DS371327 = "DS371327";
        /// <summary>
        /// 蒙阴县地税局
        /// </summary>
        public const string DS371328 = "DS371328";
        /// <summary>
        /// 费县地税局
        /// </summary>
        public const string DS371325 = "DS371325";
        /// <summary>
        /// 罗庄区地税局罗庄征收局
        /// </summary>
        public const string DS371311 = "DS371311";
        /// <summary>
        /// 经济开发区分局
        /// </summary>
        public const string DS371307 = "DS371307";
        /// <summary>
        /// 苍山县地方税务局
        /// </summary>
        public const string DS371324 = "DS371324";
        /// <summary>
        /// 沂南县地方税务局
        /// </summary>
        public const string DS371321 = "DS371321";
        /// <summary>
        /// 市开发区分局
        /// </summary>
        public const string DS371306 = "DS371306";
        /// <summary>
        /// 临沭县地方税务局
        /// </summary>
        public const string DS371329 = "371329";
        /// <summary>
        /// 平邑县地税局
        /// </summary>
        public const string DS371326 = "DS371326";
        /// <summary>
        /// 兰山区地方税务局
        /// </summary>
        public const string DS371301 = "DS371301";
        /// <summary>
        /// 河东区地方税务局
        /// </summary>
        public const string DS371312 = "DS371312";
        /// <summary>
        /// DS371323
        /// </summary>
        public const string DS371323 = "DS371323";
        /// <summary>
        /// 廊坊地税
        /// </summary>
        public const string DS1310 = "DS1310";
        /// <summary>
        /// 
        /// </summary>
        public const string DS5109 = "DS5109";
        #endregion

        /// <summary>
        /// 湖北地税
        /// </summary>
        public const string HBDS = "HBDS";
        /// <summary>
        /// 宣城地税
        /// </summary>
        public const string XCDS = "XCDS";
        /// <summary>
        /// 沂水国税
        /// </summary>
        public const string YSGS = "YSGS";
        /// <summary>
        /// 沂水地税
        /// </summary>
        public const string YSDS = "YSDS";
        /// <summary>
        /// 泰安
        /// </summary>
        public const string TA = "TA";
        /// <summary>
        /// binbin
        /// </summary>
        public const string XinBin = "XinBin";
        /// <summary>
        /// 沂水网通
        /// </summary>
        public const string YSNet = "YSNet";
        /// <summary>
        /// 临沂地税
        /// </summary>
        public const string LYTax = "LYTax";
        /// <summary>
        /// 海南文昌
        /// </summary>
        public const string HNWC = "HNWC";
    }
	public class ConfigKeyEns
	{
		/// <summary>
		/// 在新建之前是否插入
		/// </summary>
		public const string IsInsertBeforeNew="IsInsertBeforeNew";
		/// <summary>
		/// 是否显示导入
		/// </summary>
		public const string IsShowDataIn="IsShowDataIn";
	}
	/// <summary>
	/// 系统编号(用系统编号来区分是什么样的系统)
	/// </summary>
	public class SysNoList
	{
        public const string SG = "SG";
        /// <summary>
        /// CDH
        /// </summary>
        public const string CDH = "CDH";
		/// <summary>
		/// 通用考试系统
		/// </summary>
		public const string GTS="GTS";
		/// <summary>
		/// 资产管理
		/// </summary>
		public const string AM="AM";
		/// <summary>
		/// 沂水国税
		/// </summary>
		public const string GS="GS";
		/// <summary>
		/// 地税流程
		/// </summary>
		public const string WF="WF";
		/// <summary>
		/// 执法系统
		/// </summary>
		public const string ZF="ZF";
		/// <summary>
		/// 房地产
		/// </summary>
		public const string FDC="FDC";
		/// <summary>
		/// 数据门户
		/// </summary>
		public const string DP="DP";
		/// <summary>
		/// PING GU
		/// </summary>
		public const string PG="PG";
		/// <summary>
		/// 办公自动化
		/// </summary>
		public const string OA="OA";
		/// <summary>
		/// 服务器
		/// </summary>
		public const string CTISRV="CTISRV";
		/// <summary>
		/// 客户
		/// </summary>
		public const string CTIClient="CTIClient";
		/// <summary>
		/// CT
		/// </summary>
		public const string CT="CT";
		/// <summary>
		/// 网上申报
		/// </summary>
		public const string TP="TP";
		/// <summary>
		/// KM
		/// </summary>
		public const string KM="KM";
		/// <summary>
		/// 公用组件
		/// </summary>
		public const string PubComponents="PubComponents";
		/// <summary>
		/// 调度
		/// </summary>
		public const string DTS="DTS";
        /// <summary>
        /// JGLicence
        /// </summary>
        public const string JGLicence = "JGLicence";

        public const string CaiShuiRen = "CaiShuiRen";
        public const string YaoCai = "YaoCai";


        public const string Volunteer = "Volunteer";
        public const string EduAdmin = "EduAdmin";
	}
    public class ConfigEns
    {
        public const string IsShowRefFunc = "IsShowRefFunc";
        public const string DefaultSelectedAttrs = "DefaultSelectedAttrs";
        public const string ShowTextLen = "ShowTextLen";
        /// <summary>
        /// IsShowSearchKey
        /// </summary>
        public const string IsShowSearchKey = "IsShowSearchKey";
        /// <summary>
        /// 上传文件路径 它在ConfigEns.xml
        /// </summary>
        public const string PathOfUploadFile = "PathOfUploadFile";
        /// <summary>
        /// 默认搜索项
        /// </summary>
        public const string DefSearchAttr = "DefSearchAttr";
    }
	/// <summary>
	/// 系统配值
	/// </summary>
    public class SystemConfig
    {


        #region clear cash date .
        public static void ReadConfigFile(string cfgFile)
        {

            try
            {
                BP.DA.DBAccess.HisConnOfOLEs.Clear();
            }
            catch
            {
            }
            try
            {
                BP.DA.DBAccess.HisConnOfOras.Clear();
            }
            catch
            {
            }
            try
            {
                BP.DA.DBAccess.HisConnOfSQLs.Clear();
            }
            catch
            {
            }

            try
            {

                BP.DA.ClassFactory._BPAssemblies = null;
            }
            catch
            {
            }

            try
            {
                BP.DA.ClassFactory.Htable_Ens.Clear();
            }
            catch
            {
            }

            try
            {
                BP.DA.ClassFactory.Htable_XmlEn.Clear();
            }
            catch
            {
            }

            try
            {
                BP.DA.ClassFactory.Htable_XmlEns.Clear();
            }
            catch
            {
            }
            try
            {
                BP.SystemConfig.CS_AppSettings.Clear();
            }
            catch
            {
            }
        #endregion

            #region 加载 Web.Config 文件配置

            if (File.Exists(cfgFile) == false)
                throw new Exception("文件不存在 [" + cfgFile + "]");


            string _RefConfigPath = cfgFile;
            StreamReader read = new StreamReader(cfgFile);
            string firstline = read.ReadLine();
            string cfg = read.ReadToEnd();
            read.Close();

            int start = cfg.ToLower().IndexOf("<appsettings>");
            int end = cfg.ToLower().IndexOf("</appsettings>");

            cfg = cfg.Substring(start, end - start + "</appsettings".Length + 1);

            string tempFile = "Web.config.xml";

            StreamWriter write = new StreamWriter(tempFile);
            write.WriteLine(firstline);
            write.Write(cfg);
            write.Flush();
            write.Close();

            DataSet dscfg = new DataSet("cfg");
            dscfg.ReadXml(tempFile);


            //    BP.SystemConfig.CS_AppSettings = new System.Collections.Specialized.NameValueCollection();
            BP.SystemConfig.CS_DBConnctionDic.Clear();
            foreach (DataRow row in dscfg.Tables["add"].Rows)
            {
                BP.SystemConfig.CS_AppSettings.Add(row["key"].ToString().Trim(), row["value"].ToString().Trim());
            }
            #endregion
        }

        #region 关于开发商的信息
        public static string Ver
        {
            get
            {
                try
                {
                    return AppSettings["Ver"];
                }
                catch
                {
                    return "1.0.0";
                }
            }
        }
        public static string TouchWay
        {
            get
            {
                try
                {
                    return AppSettings["TouchWay"];
                }
                catch
                {
                    return SystemConfig.CustomerTel + " 地址:" + SystemConfig.CustomerAddr;
                }
            }
        }
        public static string CityNo
        {
            get
            {
                return AppSettings["CityNo"] as string;
            }
        }

        public static string CopyRight
        {
            get
            {
                try
                {
                    return AppSettings["CopyRight"];
                }
                catch
                {
                    return "版权所有@" + CustomerName;
                }
            }
        }


        /// <summary>
        /// 开发商quan 称（）		 
        /// </summary>
        public static string DeveloperName
        {
            get { return AppSettings["DeveloperName"]; }
        }
        /// <summary>
        /// 开发商简称		 
        /// </summary>
        public static string DeveloperShortName
        {
            get { return AppSettings["DeveloperShortName"]; }
        }
        /// <summary>		 
        /// 开发商电话。
        /// </summary>
        public static string DeveloperTel
        {
            get { return AppSettings["DeveloperTel"]; }
        }
        /// <summary>		
        /// 开发商的地址。
        /// </summary>
        public static string DeveloperAddr
        {
            get { return AppSettings["DeveloperAddr"]; }
        }
        #endregion

        #region 用户配置信息
        /// <summary>
        /// 系统语言（）
        /// 对多语言的系统有效。
        /// </summary>
        public static string SysLanguage
        {
            get
            {
                string s = AppSettings["SysLanguage"];
                if (s == null)
                    s = "CH";
                return s;
            }
        }


        #endregion

        #region 逻辑处理
        /// <summary>
        /// 封装了AppSettings
        /// </summary>		
        private static NameValueCollection _CS_AppSettings;
        public static NameValueCollection CS_AppSettings
        {
            get
            {
                if (_CS_AppSettings == null)
                    _CS_AppSettings = new NameValueCollection();
                return _CS_AppSettings;
            }
            set
            {
                _CS_AppSettings = value;
            }
        }

        /// <summary>
        /// 封装了AppSettings
        /// </summary>
        public static NameValueCollection AppSettings
        {
            get
            {
                if (SystemConfig.IsBSsystem)
                {
                    //return System.Configuration.ConfigurationSettings.AppSettings;
                    return System.Configuration.ConfigurationManager.AppSettings;

                    //System.Configuration.ConfigurationSettings.GetConfig
                    // return System.Configuration.con.ConfigurationSettings.AppSettings;

                }
                else
                {
                    return CS_AppSettings;
                }
            }
        }
        static SystemConfig()
        {
            CS_DBConnctionDic = new Hashtable();
        }
        /// <summary>
        /// 应用程序路径
        /// </summary>
        public static string PhysicalApplicationPath
        {
            get
            {
                if (SystemConfig.IsBSsystem && HttpContext.Current != null)
                    return HttpContext.Current.Request.PhysicalApplicationPath;
                else
                    return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }
        /// <summary>
        /// 文件放置的路径
        /// </summary>
        public static string PathOfUsersFiles
        {
            get
            {
                return "/Data/Files/";
            }
        }
        /// <summary>
        /// 临时文件路径
        /// </summary>
        public static string PathOfTemp
        {
            get
            {
                return PathOfWebApp + "\\Temp\\";
            }
        }
        public static string PathOfWorkDir
        {
            get
            {
                if (BP.SystemConfig.IsBSsystem)
                {
                    string path1 = HttpContext.Current.Request.PhysicalApplicationPath + "\\..\\";
                    System.IO.DirectoryInfo info1 = new DirectoryInfo(path1);
                    return info1.FullName;
                }
                else
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\..\\";
                    System.IO.DirectoryInfo info = new DirectoryInfo(path);
                    return info.FullName;
                }
            }
        }
        public static string PathOfData
        {
            get
            {
                return PathOfWebApp + "\\Data\\";
            }
        }
        public static string PathOfDataUser
        {
            get
            {
                return PathOfWebApp + "\\DataUser\\";
            }
        }
        /// <summary>
        /// XmlFilePath
        /// </summary>
        public static string PathOfXML
        {
            get
            {
                return PathOfWebApp + "\\Data\\XML\\";
            }
        }
        public static string PathOfAppUpdate
        {
            get
            {
                return PathOfWebApp + "\\Data\\AppUpdate\\";
            }
        }
        public static string PathOfCyclostyleFile
        {
            get
            {
                return PathOfWebApp + "\\DataUser\\CyclostyleFile\\";
            }
        }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public static string AppName
        {
            get
            {
                return System.Web.HttpContext.Current.Request.ApplicationPath.Replace("/", "");
            }
        }

        /// <summary>
        /// WebApp Path.
        /// </summary>
        public static string PathOfWebApp
        {
            get
            {
                if (SystemConfig.IsBSsystem)
                {
                    return System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
                }
                else
                {
                    if (SystemConfig.SysNo == "FTA")
                        return AppDomain.CurrentDomain.BaseDirectory;
                    else
                        return AppDomain.CurrentDomain.BaseDirectory + "..\\..\\";
                }
            }
        }
        #endregion

        #region
        public static bool IsBSsystem_Test = true;

        /// <summary>
        /// 是不是BS系统结构。
        /// </summary>
        private static bool _IsBSsystem = true;
        /// <summary>
        /// 是不是BS系统结构。
        /// </summary>
        public static bool IsBSsystem
        {
            get
            {
                // return true;
                return SystemConfig._IsBSsystem;
            }
            set
            {
                SystemConfig._IsBSsystem = value;
            }
        }
        public static bool IsCSsystem
        {
            get
            {
                return !SystemConfig._IsBSsystem;
            }
        }
        #endregion

        #region 系统配置信息
        /// <summary>
        /// 执行清空
        /// </summary>
        public static void DoClearCash()
        {
            HttpRuntime.UnloadAppDomain();
            BP.DA.Cash.Map_Cash.Clear();
            BP.DA.Cash.SQL_Cash.Clear();
            BP.DA.Cash.EnsData_Cash.Clear();
            BP.DA.Cash.EnsData_Cash_Ext.Clear();
            BP.DA.Cash.BS_Cash.Clear();
            BP.DA.Cash.Bill_Cash.Clear();
            try
            {
                System.Web.HttpContext.Current.Session.Clear();
                System.Web.HttpContext.Current.Application.Clear();
            }
            catch
            {
            }
        }
        /// <summary>
        /// 系统编号
        /// </summary>		 
        public static string SysNo
        {
            get { return AppSettings["SysNo"]; }
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        public static string SysName
        {
            get
            {

                if (SystemConfig.SysNo == SysNoList.PG)
                {
                    switch (SystemConfig.CustomerNo)
                    {
                        case "DS1310":
                            return "廊坊市地税局 - 纳税评估管理系统";
                        case "DS5115_":
                            return "宜宾市地税局 - 纳税评估管理系统";
                        case "DS5109":
                            return "遂宁市地税局 - 纳税评估管理系统";
                        case "DS3713":
                            return "临沂市地税局 - 纳税评估管理系统";
                        default:
                            return "未授权软件-没有数据安全的保证";
                    }
                }
                else
                {
                    return AppSettings["SysName"];
                }
            }
        }

        public static string OrderWay
        {
            get
            {
                return AppSettings["OrderWay"];
            }
        }

        public static int PageSize
        {
            get
            {
                try
                {
                    return int.Parse(AppSettings["PageSize"]);
                }
                catch
                {
                    return 99999;
                }
            }
        }

        public static int MaxDDLNum
        {
            get
            {
                try
                {
                    return int.Parse(AppSettings["MaxDDLNum"]);
                }
                catch
                {
                    return 50;
                }
            }
        }

        public static int PageSpan
        {
            get
            {
                try
                {
                    return int.Parse(AppSettings["PageSpan"]);
                }
                catch
                {
                    return 20;
                }
            }
        }

        /// <summary>
        ///  到的路径.PageOfAfterAuthorizeLogin
        /// </summary>
        public static string PageOfAfterAuthorizeLogin
        {
            get { return System.Web.HttpContext.Current.Request.ApplicationPath + "/" + AppSettings["PageOfAfterAuthorizeLogin"]; }
        }
        /// <summary>
        /// 丢失session 到的路径.
        /// </summary>
        public static string PageOfLostSession
        {
            get { return System.Web.HttpContext.Current.Request.ApplicationPath + "/" + AppSettings["PageOfLostSession"]; }
        }
        /// <summary>
        /// BBS
        /// </summary>
        public static string PageOfBBS
        {
            get { return AppSettings["PageOfBBS"]; }
        }
        public static string PathOfFDB
        {
            get
            {
                string s = SystemConfig.AppSettings["FDB"];
                if (s == "" || s == null)
                    return PathOfWebApp + "\\Data\\FDB\\";
                return s;
            }
        }
        /// <summary>
        /// 日志路径
        /// </summary>
        public static string PathOfLog
        {
            get { return PathOfWebApp + "\\DataUser\\Log\\"; }
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        public static int TopNum
        {
            get
            {
                try
                {
                    return int.Parse(AppSettings["TopNum"]);
                }
                catch
                {
                    return 99999;
                }
            }
        }
        /// <summary>
        /// 服务电话
        /// </summary>
        public static string ServiceTel
        {
            get { return AppSettings["ServiceTel"]; }
        }
        /// <summary>
        /// 服务E-mail
        /// </summary>
        public static string ServiceMail
        {
            get { return AppSettings["ServiceMail"]; }
        }
        /// <summary>
        /// 第3方软件
        /// </summary>
        public static string ThirdPartySoftWareKey
        {
            get
            {
                return AppSettings["ThirdPartySoftWareKey"];
            }
        }
        /// <summary>
        /// 第3方软件db类型．
        /// </summary>
        public static DA.DBType ThirdPartySoftDBType
        {
            get
            {
                if (AppSettings["ThirdPartySoftDBType"] == "Oracle")
                {
                    return DA.DBType.Oracle9i;
                }
                else if (AppSettings["ThirdPartySoftDBType"] == "Sybase")
                {
                    return DA.DBType.Sybase;
                }
                else if (AppSettings["ThirdPartySoftDBType"] == "SQL2000")
                {
                    return DA.DBType.SQL2000;
                }
                else
                {
                    throw new Exception("ThirdPartySoftDBType error ");
                }
            }
        }
        /// <summary>
        /// 是否 debug 状态
        /// </summary>
        public static bool IsDebug
        {
            get
            {
                if (AppSettings["IsDebug"] == "1")
                    return true;
                else
                    return false;
            }
        }
        public static bool IsUnit
        {
            get
            {
                if (AppSettings["IsUnit"] == "1")
                    return true;
                else
                    return false;
            }
        }
        
        public static bool IsOpenSQLCheck
        {
            get
            {
                if (AppSettings["IsOpenSQLCheck"] == "0")
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 是不是多系统工作。
        /// </summary>
        public static bool IsMultiSys
        {
            get
            {
                if (AppSettings["IsMultiSys"] == "1")
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 是不是多线程工作。
        /// </summary>
        public static bool IsMultiThread_del
        {
            get
            {
                if (AppSettings["IsMultiThread"] == "1")
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 是不是多语言版本
        /// </summary>
        public static bool IsMultiLanguageSys
        {
            get
            {
                if (AppSettings["IsMultiLanguageSys"] == "1")
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region 处理临时缓存
        /// <summary>
        /// 放在 Temp 中的cash 多少时间失效。
        /// 0, 表示永久不失效。
        /// </summary>
        private static int CashFail
        {
            get
            {
                try
                {
                    return int.Parse(AppSettings["CashFail"]);
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 当前的 TempCash 是否失效了
        /// </summary>
        public static bool IsTempCashFail
        {
            get
            {
                if (SystemConfig.CashFail == 0)
                    return false;

                if (_CashFailDateTime == null)
                {
                    _CashFailDateTime = DateTime.Now;
                    return true;
                }
                else
                {
                    TimeSpan ts = DateTime.Now - _CashFailDateTime;
                    if (ts.Minutes >= SystemConfig.CashFail)
                    {
                        _CashFailDateTime = DateTime.Now;
                        return true;
                    }
                    return false;
                }
            }
        }
        public static DateTime _CashFailDateTime;
        #endregion

        #region 客户配置信息
        /// <summary>
        /// 客户编号
        /// </summary>
        public static string CustomerNo
        {
            get
            {
                return AppSettings["CustomerNo"];
            }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public static string CustomerName
        {
            get
            {
                return AppSettings["CustomerName"];
            }
        }
        public static string CustomerURL
        {
            get
            {
                return AppSettings["CustomerURL"];
            }
        }
        /// <summary>
        /// 客户简称
        /// </summary>
        public static string CustomerShortName
        {
            get
            {
                return AppSettings["CustomerShortName"];
            }
        }
        /// <summary>
        /// 客户地址
        /// </summary>
        public static string CustomerAddr
        {
            get
            {
                return AppSettings["CustomerAddr"];
            }
        }
        /// <summary>
        /// 客户电话
        /// </summary>
        public static string CustomerTel
        {
            get
            {
                return AppSettings["CustomerTel"];
            }
        }
        #endregion

        /// <summary>
        ///取得配置 NestedNamesSection 内的相应 key 的内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static NameValueCollection GetConfig(string key)
        {
            //Hashtable ht = (Hashtable)ConfigurationSettings.GetConfig("NestedNamesSection");

            Hashtable ht = (Hashtable)System.Configuration.ConfigurationManager.GetSection("NestedNamesSection");
            //Hashtable ht = (Hashtable)System.Configuration.ConfigurationManager.GetSection("NestedNamesSection");

            return (NameValueCollection)ht[key];
        }
        public static string GetValByKey(string key, string isNullas)
        {
            string s = AppSettings[key];
            if (s == null)
                s = isNullas;
            return s;
        }
        public static bool GetValByKeyBoolen(string key, bool isNullas)
        {
            string s = AppSettings[key];
            if (s == null)
                return isNullas;

            if (s == "1")
                return true;
            else
                return false;
        }
        public static int GetValByKeyInt(string key, int isNullas)
        {
            string s = AppSettings[key];
            if (s == null)
                return isNullas;

            return int.Parse(s);
        }
        public static string GetConfigXmlKeyVal(string key)
        {
            try
            {
                DataSet ds = new DataSet("dss");
                ds.ReadXml(BP.SystemConfig.PathOfXML + "\\KeyVal.xml");
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Key"].ToString() == key)
                        return dr["Val"].ToString();
                }
                throw new Exception("在您利用GetXmlConfig 取值出现错误，没有找到key= " + key + ", 请检查 /data/Xml/KeyVal.xml. ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetConfigXmlNode(string fk_Breed, string enName, string key)
        {
            try
            {
                string file = BP.SystemConfig.PathOfXML + "\\Node\\" + fk_Breed + ".xml";
                DataSet ds = new DataSet("dss");
                try
                {
                    ds.ReadXml(file);
                }
                catch
                {
                    return null;
                }
                DataTable dt = ds.Tables[0];
                if (dt.Columns.Contains(key) == false)
                    throw new Exception(file + "配置错误，您没有按照格式配置，它不包含标记 " + key);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["NodeEnName"].ToString() == enName)
                    {
                        if (dr[key].Equals(DBNull.Value))
                            return null;
                        else
                            return dr[key].ToString();

                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取xml中的配置信息
        /// GroupTitle, ShowTextLen, DefaultSelectedAttrs, TimeSpan
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ensName"></param>
        /// <returns></returns>
        public static string GetConfigXmlEns(string key, string ensName)
        {
            try
            {
                DataTable dt = BP.DA.Cash.GetObj("TConfigEns", BP.DA.Depositary.Application) as DataTable;
                if (dt == null)
                {
                    DataSet ds = new DataSet("dss");
                    ds.ReadXml(BP.SystemConfig.PathOfXML + "\\Ens\\ConfigEns.xml");
                    dt = ds.Tables[0];
                    BP.DA.Cash.AddObj("TConfigEns", BP.DA.Depositary.Application, dt);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Key"].ToString() == key && dr["For"].ToString() == ensName)
                        return dr["Val"].ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetConfigXmlSQL(string key)
        {
            try
            {
                DataSet ds = new DataSet("dss");
                ds.ReadXml(BP.SystemConfig.PathOfXML + "\\SQL\\" + BP.SystemConfig.ThirdPartySoftWareKey + ".xml");
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["No"].ToString() == key)
                        return dr["SQL"].ToString();
                }
                throw new Exception("在您利用 GetXmlConfig 取值出现错误，没有找到key= " + key + ", 请检查 /Data/XML/" + SystemConfig.ThirdPartySoftWareKey + ".xml. ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetConfigXmlSQLApp(string key)
        {
            try
            {
                DataSet ds = new DataSet("dss");
                ds.ReadXml(BP.SystemConfig.PathOfXML + "\\SQL\\App.xml");
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["No"].ToString() == key)
                        return dr["SQL"].ToString();
                }
                throw new Exception("在您利用 GetXmlConfig 取值出现错误，没有找到key= " + key + ", 请检查 /Data/XML/App.xml. ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetConfigXmlSQL(string key, string replaceKey, string replaceVal)
        {
            return GetConfigXmlSQL(key).Replace(replaceKey, replaceVal);
        }
        public static string GetConfigXmlSQL(string key, string replaceKey1, string replaceVal1, string replaceKey2, string replaceVal2)
        {
            return GetConfigXmlSQL(key).Replace(replaceKey1, replaceVal1).Replace(replaceKey2, replaceVal2);
        }

        #region dsn
        public static string HelpUrl
        {
            get
            {
                string url = AppSettings["HelpUrl"] as string;
                if (url == null || url == "")
                    url = "./Helper/Helper.htm";
                return url;
            }
        }
        public static string AppCenterDSN
        {
            get
            {
                string dsn = AppSettings["AppCenterDSN"];
                if (dsn.IndexOf("@Pass") != -1)
                {
                    dsn = dsn.Replace("@Pass", "helloccs");
                    AppSettings["AppCenterDSN"] = dsn;
                }

                if (dsn.IndexOf("@Path") != -1)
                {
                    dsn = dsn.Replace("@Path", SystemConfig.PathOfWebApp);
                    AppSettings["AppCenterDSN"] = dsn;
                }
                return dsn;
            }
            set
            {
                AppSettings["AppCenterDSN"] = value;
            }
        }

        public static string DBAccessOfOracle9i
        {
            get
            {
                return AppSettings["DBAccessOfOracle9i"];
            }
        }
        public static string DBAccessOfOracle9i1
        {
            get
            {
                return AppSettings["DBAccessOfOracle9i1"];
            }
        }
        public static string DBAccessOfMSSQL2000
        {
            get
            {
                return AppSettings["DBAccessOfMSSQL2000"];
            }
        }
        public static string DBAccessOfOLE
        {
            get
            {
                string dsn = AppSettings["DBAccessOfOLE"];
                if (dsn.IndexOf("@Pass") != -1)
                    dsn = dsn.Replace("@Pass", "helloccs");

                if (dsn.IndexOf("@Path") != -1)
                    dsn = dsn.Replace("@Path", SystemConfig.PathOfWebApp);
                return dsn;

            }
        }
        public static string DBAccessOfODBC
        {
            get
            {
                return AppSettings["DBAccessOfODBC"];
            }
        }
        #endregion

        /// <summary>
        /// 获取主应用程序的数据库类型．
        /// </summary>
        public static BP.DA.DBType AppCenterDBType
        {
            get
            {
                switch (AppSettings["AppCenterDBType"])
                {
                    case "MSSQL2000":
                    case "MSSQL":
                        return BP.DA.DBType.SQL2000;
                        break;
                    case "Access":
                        return BP.DA.DBType.Access;
                        break;
                    case "Oracle":
                    default:
                        return BP.DA.DBType.Oracle9i;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string AppCenterDBVarStr
        {
            get
            {
                switch (SystemConfig.AppCenterDBType)
                {
                    case BP.DA.DBType.Oracle9i:
                        return ":";
                    default:
                        return "@";
                }
            }
        }

        public static string AppCenterDBSubstringStr
        {
            get
            {
                switch (SystemConfig.AppCenterDBType)
                {
                    case BP.DA.DBType.Oracle9i:
                        return "substr";
                    case BP.DA.DBType.SQL2000:
                        return "substring";
                    case BP.DA.DBType.Access:
                        return "Mid";
                    default:
                        return "substring";
                }
            }
        }


        public static string AppCenterDBAddStringStr
        {
            get
            {
                switch (SystemConfig.AppCenterDBType)
                {
                    case BP.DA.DBType.Oracle9i:
                        return "||";
                    default:
                        return "+";
                }
            }
        }
        public static readonly Hashtable CS_DBConnctionDic;
    }
}
