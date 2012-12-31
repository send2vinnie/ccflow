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
        /// �ϴ��ļ�·�� ����ConfigEns.xml
        /// </summary>
        public const string PathOfUploadFile = "PathOfUploadFile";
        /// <summary>
        /// Ĭ��������
        /// </summary>
        public const string DefSearchAttr = "DefSearchAttr";
    }
    /// <summary>
    /// ϵͳ��ֵ
    /// </summary>
    public class SystemConfig
    {
        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>
        /// <param name="cfgFile"></param>
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

            #region ���� Web.Config �ļ�����

            if (File.Exists(cfgFile) == false)
                throw new Exception("�ļ������� [" + cfgFile + "]");


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

        #region ���ڿ����̵���Ϣ
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
                    return SystemConfig.CustomerTel + " ��ַ:" + SystemConfig.CustomerAddr;
                }
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
                    return "��Ȩ����@" + CustomerName;
                }
            }
        }
        public static string CompanyID
        {
            get
            {
                string s = AppSettings["CompanyID"];
                if (string.IsNullOrEmpty(s))
                    return "CCFlow";
                return s;
            }
        }
        /// <summary>
        /// ������ȫ��		 
        /// </summary>
        public static string DeveloperName
        {
            get { return AppSettings["DeveloperName"]; }
        }
        /// <summary>
        /// �����̼��
        /// </summary>
        public static string DeveloperShortName
        {
            get { return AppSettings["DeveloperShortName"]; }
        }
        /// <summary>		 
        /// �����̵绰��
        /// </summary>
        public static string DeveloperTel
        {
            get { return AppSettings["DeveloperTel"]; }
        }
        /// <summary>		
        /// �����̵ĵ�ַ��
        /// </summary>
        public static string DeveloperAddr
        {
            get { return AppSettings["DeveloperAddr"]; }
        }
        #endregion

        #region �û�������Ϣ
        /// <summary>
        /// ϵͳ���ԣ���
        /// �Զ����Ե�ϵͳ��Ч��
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

        #region �߼�����
        /// <summary>
        /// ��װ��AppSettings
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
        /// ��װ��AppSettings
        /// </summary>
        public static NameValueCollection AppSettings
        {
            get
            {
                if (SystemConfig.IsBSsystem)
                {
                    return System.Configuration.ConfigurationManager.AppSettings;

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
        /// Ӧ�ó���·��
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
        /// �ļ����õ�·��
        /// </summary>
        public static string PathOfUsersFiles
        {
            get
            {
                return "/Data/Files/";
            }
        }
        /// <summary>
        /// ��ʱ�ļ�·��
        /// </summary>
        public static string PathOfTemp
        {
            get
            {
                return PathOfWebApp + SystemConfig.AppSettings["DataDirPath"] + "\\Temp\\";
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
        public static string PathOfFDB
        {
            get
            {
                string s = SystemConfig.AppSettings["FDB"];
                if (s == "" || s == null)
                    return PathOfWebApp + "\\DataUser\\FDB\\";
                return s;
            }
        }
        /// <summary>
        /// �����ļ�
        /// </summary>
        public static string PathOfData
        {
            get
            {
                return PathOfWebApp + SystemConfig.AppSettings["DataDirPath"] + "\\Data\\";
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
                return PathOfWebApp + SystemConfig.AppSettings["DataDirPath"] + "\\Data\\XML\\";
            }
        }
        public static string PathOfAppUpdate
        {
            get
            {
                return PathOfWebApp + SystemConfig.AppSettings["DataDirPath"] + "\\Data\\AppUpdate\\";
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
        /// Ӧ�ó�������
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

        #region ��ͬ������
        public static bool IsBSsystem_Test = true;
        /// <summary>
        /// �ǲ���BSϵͳ�ṹ��
        /// </summary>
        private static bool _IsBSsystem = true;
        /// <summary>
        /// �ǲ���BSϵͳ�ṹ��
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

        #region ϵͳ������Ϣ
        /// <summary>
        /// ִ�����
        /// </summary>
        public static void DoClearCash()
        {
           // HttpRuntime.UnloadAppDomain();
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
        /// ϵͳ���
        /// </summary>		 
        public static string SysNo
        {
            get { return AppSettings["SysNo"]; }
        }

        /// <summary>
        /// ϵͳ����
        /// </summary>
        public static string SysName
        {
            get
            {
                string s = AppSettings["SysName"];
                if (s == null)
                    s = "����web.config������SysName���ơ�";
                return s;
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
        ///  ����·��.PageOfAfterAuthorizeLogin
        /// </summary>
        public static string PageOfAfterAuthorizeLogin
        {
            get { return System.Web.HttpContext.Current.Request.ApplicationPath + "" + AppSettings["PageOfAfterAuthorizeLogin"]; }
        }
        /// <summary>
        /// ��ʧsession ����·��.
        /// </summary>
        public static string PageOfLostSession
        {
            get { return System.Web.HttpContext.Current.Request.ApplicationPath + "" + AppSettings["PageOfLostSession"]; }
        }
        /// <summary>
        /// ��־·��
        /// </summary>
        public static string PathOfLog
        {
            get { return PathOfWebApp + "\\DataUser\\Log\\"; }
        }

        /// <summary>
        /// ϵͳ����
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
        /// ����绰
        /// </summary>
        public static string ServiceTel
        {
            get { return AppSettings["ServiceTel"]; }
        }
        /// <summary>
        /// ����E-mail
        /// </summary>
        public static string ServiceMail
        {
            get { return AppSettings["ServiceMail"]; }
        }
        /// <summary>
        /// ��3�����
        /// </summary>
        public static string ThirdPartySoftWareKey
        {
            get
            {
                return AppSettings["ThirdPartySoftWareKey"];
            }
        }
        /// <summary>
        /// �Ƿ� debug ״̬
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
        /// �ǲ��Ƕ�ϵͳ������
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
        /// �ǲ��Ƕ��̹߳�����
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
        /// �ǲ��Ƕ����԰汾
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

        #region ������ʱ����
        /// <summary>
        /// ���� Temp �е�cash ����ʱ��ʧЧ��
        /// 0, ��ʾ���ò�ʧЧ��
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
        /// ��ǰ�� TempCash �Ƿ�ʧЧ��
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

        #region �ͻ�������Ϣ
        /// <summary>
        /// �ͻ����
        /// </summary>
        public static string CustomerNo
        {
            get
            {
                return AppSettings["CustomerNo"];
            }
        }
        /// <summary>
        /// �ͻ�����
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
        /// �ͻ����
        /// </summary>
        public static string CustomerShortName
        {
            get
            {
                return AppSettings["CustomerShortName"];
            }
        }
        /// <summary>
        /// �ͻ���ַ
        /// </summary>
        public static string CustomerAddr
        {
            get
            {
                return AppSettings["CustomerAddr"];
            }
        }
        /// <summary>
        /// �ͻ��绰
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
        ///ȡ������ NestedNamesSection �ڵ���Ӧ key ������
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static NameValueCollection GetConfig(string key)
        {
            Hashtable ht = (Hashtable)System.Configuration.ConfigurationManager.GetSection("NestedNamesSection");
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
                throw new Exception("��������GetXmlConfig ȡֵ���ִ���û���ҵ�key= " + key + ", ���� /data/Xml/KeyVal.xml. ");
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
                    throw new Exception(file + "���ô�����û�а��ո�ʽ���ã������������ " + key);
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
        /// ��ȡxml�е�������Ϣ
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
                throw new Exception("�������� GetXmlConfig ȡֵ���ִ���û���ҵ�key= " + key + ", ���� /Data/XML/" + SystemConfig.ThirdPartySoftWareKey + ".xml. ");
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
                throw new Exception("�������� GetXmlConfig ȡֵ���ִ���û���ҵ�key= " + key + ", ���� /Data/XML/App.xml. ");
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
        /// ��ȡ��Ӧ�ó�������ݿ����ͣ�
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
                    case "Oracle":
                        return BP.DA.DBType.Oracle9i;
                    case "MySQL":
                        return BP.DA.DBType.MySQL;
                    case "Access":
                        return BP.DA.DBType.Access;
                    case "Informix":
                        return BP.DA.DBType.Informix;
                    default:
                        return BP.DA.DBType.Oracle9i;
                }
            }
        }
        /// <summary>
        /// ��ȡ��ͬ���͵����ݿ�������
        /// </summary>
        public static string AppCenterDBVarStr
        {
            get
            {
                switch (SystemConfig.AppCenterDBType)
                {
                    case BP.DA.DBType.Oracle9i:
                        return ":";
                    case BP.DA.DBType.Informix:
                        return "?";
                    default:
                        return "@";
                }
            }
        }
        /// <summary>
        /// ��ȡ��ͬ���͵�substring��������д
        /// </summary>
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
                    case BP.DA.DBType.Informix:
                        return "MySubString";
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
                    case BP.DA.DBType.MySQL:
                    case BP.DA.DBType.Informix:
                        return "||";
                    default:
                        return "+";
                }
            }
        }
        public static readonly Hashtable CS_DBConnctionDic;
    }
}
