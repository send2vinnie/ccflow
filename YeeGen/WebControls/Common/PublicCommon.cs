using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Tax666.Common;
using System.IO;

namespace Tax666.WebControls
{
    /// <summary>
    /// 通用类
    /// </summary>
    public class PublicCommon
    {
        #region 获取登陆用户UserID
        /// <summary>
        /// 获取登陆用户UserID,如果未登陆为0
        /// </summary>
        public static int Get_UserID
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated ? Convert.ToInt32(HttpContext.Current.User.Identity.Name) : 0;
            }
        }
        #endregion

        #region 获取当前Cookies名称
        /// <summary>
        /// "获取当前Cookies名称
        /// </summary>
        public static string Get_CookiesName
        {
            get
            {
                return "521189_manager_vip";
            }
        }
        #endregion

        #region 获取WEBCache名称前辍
        /// <summary>
        /// 获取WEBCache名称前辍
        /// </summary>
        public static string Get_WebCacheName
        {
            get
            {
                return "521189_manager_vip";
            }
        }
        #endregion

        #region 设置页面不被缓存
        /// <summary>
        /// 设置页面不被缓存
        /// </summary>
        public static void SetPageNoCache()
        {

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.AddHeader("Pragma", "No-Cache");
        }
        #endregion

        #region 获得sessionid
        /// <summary>
        /// 获得sessionid
        /// </summary>
        public static string GetSessionID
        {
            get
            {
                return HttpContext.Current.Session.SessionID;
            }
        }
        #endregion

        #region 当前显示应用模组
        /// <summary>
        /// 显示应用模组
        /// </summary>
        //public static int ApplicationID
        //{
        //    //get
        //    //{
        //    //    try
        //    //    {
        //    //        return Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationID"]);
        //    //    }
        //    //    catch
        //    //    {
        //    //        return 0;
        //    //    }
        //    //}
        //}
        #endregion

        #region 前台设置
        /// <summary>
        /// 菜单风格 0:经典 1:流行 2:朴素
        /// </summary>
        public static int MenuStyle
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["MenuStyle"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(HttpContext.Current.Request.Cookies["MenuStyle"].Value);
                }
            }
            set
            {
                HttpContext.Current.Response.Cookies["MenuStyle"].Value = value.ToString();
            }
        }

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

        /// <summary>
        /// 表格样式(默认default)
        /// </summary>
        public static string TableSink
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["TableSink"] == null)
                {
                    return "default";
                }
                else
                {
                    return HttpContext.Current.Request.Cookies["TableSink"].Value.ToString();
                }
            }
            set
            {
                HttpContext.Current.Response.Cookies["TableSink"].Value = value;
            }
        }
        #endregion

        #region 用户在线过期时间
        /// <summary>
        /// 用户在线过期时间 (分)默认30分 如果用户在当前设定的时间内没有任何操作,将会被系统自动退出
        /// </summary>
        //public static int OnlineMinute
        //{
        //    get
        //    {
        //        try
        //        {
        //            return Convert.ToInt32(ConfigurationManager.AppSettings["OnlineMinute"]);
        //        }
        //        catch
        //        {
        //            return 30;
        //        }
        //    }
        //}
        #endregion

        #region 获得在线统计数据保存环境
        /// <summary>
        /// 获得在线统计数据保存环境
        /// </summary>
        //public static OnlineCountType GetOnlineCountType
        //{
        //    //get
        //    //{
        //    //    if (GetConfigOnlineCountType == 1)
        //    //        return OnlineCountType.DataBase;
        //    //    else
        //    //        return OnlineCountType.Cache;
        //    //}
        //}

        /// <summary>
        /// 获得配置在线统计类型
        /// </summary>
        //private static int GetConfigOnlineCountType
        //{
        //    get
        //    {
        //        return Convert.ToInt32(ConfigurationManager.AppSettings["OnlineCountType"]);
        //    }
        //}
        #endregion

        #region 获取用户Form提交字段值
        /// <summary>
        /// 获取post和get提交值
        /// </summary>
        /// <param name="InputName">字段名</param>
        /// <param name="Method">post和get</param>
        /// <param name="MaxLen">最大允许字符长度 0为不限制</param>
        /// <param name="MinLen">最小字符长度 0为不限制</param>
        /// <param name="DataType">字段数值类型 int 和str和dat不限为为空</param>
        /// <returns></returns>
        public static object sink(string InputName, MethodType Method, int MaxLen, int MinLen, DataType DataType)
        {
            HttpContext rq = HttpContext.Current;
            string TempValue = "";

            #region 获取提交字段数据TempValue
            if (Method == MethodType.Post)
            {
                if (rq.Request.Form[InputName] != null)
                {
                    TempValue = rq.Request.Form[InputName].ToString();
                }

            }
            else if (Method == MethodType.Get)
            {
                if (rq.Request.QueryString[InputName] != null)
                {
                    TempValue = rq.Request.QueryString[InputName].ToString();
                }
            }
            else
            {
                MessBox("提交数据方式不是post和get!", "?", rq);
                EventMessage.MessageBox(2, "获取数据失败", string.Format("{0}字段提交数据方式不是post和get!", InputName), Icon_Type.Error, "history.back();", UrlType.JavaScript);
            }
            #endregion

            #region 检测最大允许长度
            if (MaxLen != 0)
            {
                if (TempValue.Length > MaxLen)
                {
                    EventMessage.MessageBox(2, "输入数据格式验证失败", string.Format("{0}字段值：{1}超过系统允许长度{2}!", InputName, TempValue, MaxLen), Icon_Type.Error, "history.back();", UrlType.JavaScript);
                }
            }
            #endregion

            #region 检测最小允许长度
            if (MinLen != 0)
            {
                if (TempValue.Length < MinLen)
                {
                    EventMessage.MessageBox(2, "输入数据格式验证失败", string.Format("{0}字段值：{1}低于系统允许长度{2}!", InputName, TempValue, MinLen), Icon_Type.Error, "history.back();", UrlType.JavaScript);
                }
            }
            #endregion

            #region 检测数据类型
            if (TempValue != "")
            {

                switch (DataType)
                {
                    case DataType.Int:
                        int IntTempValue = 0;
                        if (!int.TryParse(TempValue, out IntTempValue))
                            EventMessage.MessageBox(2, "输入数据格式验证失败", string.Format("{0}字段值：{1}数据类型必需为Int型!", InputName, TempValue), Icon_Type.Error, "history.back();", UrlType.JavaScript);
                        return IntTempValue;
                    case DataType.Dat:
                        DateTime DateTempValue = DateTime.MinValue;
                        if (!DateTime.TryParse(TempValue, out DateTempValue))
                            EventMessage.MessageBox(2, "输入数据格式验证失败", string.Format("{0}字段值：{1}数据类型必需为日期型!", InputName, TempValue), Icon_Type.Error, "history.back();", UrlType.JavaScript);
                        return DateTempValue;
                    case DataType.Long:
                        long LongTempValue = long.MinValue;
                        if (!long.TryParse(TempValue, out LongTempValue))
                            EventMessage.MessageBox(2, "输入数据格式验证失败", string.Format("{0}字段值：{1}数据类型必需为Log型!", InputName, TempValue), Icon_Type.Error, "history.back();", UrlType.JavaScript);
                        return LongTempValue;
                    case DataType.Double:
                        double DoubleTempValue = double.MinValue;
                        if (!double.TryParse(TempValue, out DoubleTempValue))
                            EventMessage.MessageBox(2, "输入数据格式验证失败", string.Format("{0}字段值：{1}数据类型必需为Double型!", InputName, TempValue), Icon_Type.Error, "history.back();", UrlType.JavaScript);
                        return DoubleTempValue;
                    case DataType.CharAndNum:
                        if (!CheckRegEx(TempValue, "^[A-Za-z0-9]+$"))
                            EventMessage.MessageBox(2, "输入数据格式验证失败", string.Format("{0}字段值：{1}数据类型必需为英文或数字!", InputName, TempValue), Icon_Type.Error, "history.back();", UrlType.JavaScript);
                        return TempValue;
                    case DataType.CharAndNumAndChinese:
                        if (!CheckRegEx(TempValue, "^[A-Za-z0-9\u00A1-\u2999\u3001-\uFFFD]+$"))
                            EventMessage.MessageBox(2, "输入数据格式验证失败", string.Format("{0}字段值：{1}数据类型必需为英文或数字或中文!", InputName, TempValue), Icon_Type.Error, "history.back();", UrlType.JavaScript);
                        return TempValue;
                    case DataType.Email:
                        if (!CheckRegEx(TempValue, "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
                            EventMessage.MessageBox(2, "输入数据格式验证失败", string.Format("{0}字段值：{1}数据类型必需为邮件地址!", InputName, TempValue), Icon_Type.Error, "history.back();", UrlType.JavaScript);
                        return TempValue;
                    default:
                        return TempValue;
                }

            }
            else
            {
                switch (DataType)
                {
                    case DataType.Int:
                        return 0;
                    case DataType.Dat:
                        return null;
                    case DataType.Long:
                        return long.MinValue;
                    case DataType.Double:
                        return 0.0f;
                    default:
                        return TempValue;
                }
            }

            #endregion
        }

        #endregion

        #region js信息提示框
        /// <summary>
        /// js信息提示框
        /// </summary>
        /// <param name="Message">提示信息文字</param>
        /// <param name="ReturnUrl">返回地址</param>
        /// <param name="rq"></param>
        public static void MessBox(string Message, string ReturnUrl, HttpContext rq)
        {
            System.Text.StringBuilder msgScript = new System.Text.StringBuilder();
            msgScript.Append("<script language=JavaScript>\n");
            msgScript.Append("alert(\"" + Message + "\");\n");
            msgScript.Append("parent.location.href='" + ReturnUrl + "';\n");
            msgScript.Append("</script>\n");
            rq.Response.Write(msgScript.ToString());
            rq.Response.End();
        }

        /// <summary>
        /// 弹出Alert信息窗
        /// </summary>
        /// <param name="Message">信息内容</param>
        public static void MessBox(string Message)
        {
            System.Text.StringBuilder msgScript = new System.Text.StringBuilder();
            msgScript.Append("<script language=JavaScript>\n");
            msgScript.Append("alert(\"" + Message + "\");\n");
            msgScript.Append("</script>\n");
            HttpContext.Current.Response.Write(msgScript.ToString());
        }

        #endregion

        #region 按字符串位数补0
        /// <summary>
        /// 按字符串位数补0
        /// </summary>
        /// <param name="CharTxt">字符串</param>
        /// <param name="CharLen">字符长度</param>
        /// <returns></returns>
        public static string FillZero(string CharTxt, int CharLen)
        {
            if (CharTxt.Length < CharLen)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < CharLen - CharTxt.Length; i++)
                {
                    sb.Append("0");
                }
                sb.Append(CharTxt);
                return sb.ToString();
            }
            else
            {
                return CharTxt;
            }
        }

        #endregion

        #region 替换JS中特殊字符
        /// <summary>
        /// 将JS中的特殊字符替换
        /// </summary>
        /// <param name="str">要替换字符</param>
        /// <returns></returns>
        public static string ReplaceJs(string str)
        {

            if (str != null)
            {
                str = str.Replace("\"", "&quot;");
                str = str.Replace("(", "&#40;");
                str = str.Replace(")", "&#41;");
                str = str.Replace("%", "&#37;");
            }

            return str;

        }
        #endregion

        #region 正式表达式验证
        /// <summary>
        /// 正式表达式验证
        /// </summary>
        /// <param name="C_Value">验证字符</param>
        /// <param name="C_Str">正式表达式</param>
        /// <returns>符合true不符合false</returns>
        public static bool CheckRegEx(string C_Value, string C_Str)
        {
            Regex objAlphaPatt;
            objAlphaPatt = new Regex(C_Str, RegexOptions.Compiled);


            return objAlphaPatt.Match(C_Value).Success;
        }
        #endregion

        #region base64编码/解码
        /// <summary>
        /// 进行base64编码
        /// </summary>
        /// <param name="s">字符</param>
        /// <returns></returns>
        public static string EnBase64(string s)
        {
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(s));
        }

        /// <summary>
        /// 进行Base64解码
        /// </summary>
        /// <param name="s">字符</param>
        /// <returns></returns>
        public static string DeBase64(string s)
        {
            return System.Text.Encoding.Default.GetString(Convert.FromBase64String(s));
        }
        #endregion

        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string md5(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }

            if (code == 32)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }

            return strEncrypt;
        }
        #endregion

        #region 获得缓存类配置(命名空间/类名)
        /// <summary>
        /// 获得缓存类配置(命名空间)
        /// </summary>
        //public static string GetCachenamespace
        //{
        //    //get
        //    //{
        //    //    return ConfigurationManager.AppSettings["Cachenamespace"];
        //    //}
        //}

        /// <summary>
        /// 获得缓存类配置(类名)
        /// </summary>
        //public static string GetCacheclassName
        //{
        ////    get
        ////    {
        ////        return ConfigurationManager.AppSettings["CacheclassName"];
        ////    }
        //}
        #endregion

        #region 获取数据库连接字符串
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        //public static string GetConnString
        //{
        //    get
        //    {
        //        return OnenetConfiguration.ConnectionString;
        //    }
        //}
        #endregion

        #region 格式化字符串,符合SQL语句
        /// <summary>
        /// 格式化字符串,符合SQL语句
        /// </summary>
        /// <param name="formatStr">需要格式化的字符串</param>
        /// <returns>字符串</returns>
        public static string inSQL(string formatStr)
        {
            string rStr = formatStr;
            if (formatStr != null && formatStr != string.Empty)
            {
                rStr = rStr.Replace("'", "''");
                //rStr = rStr.Replace("\"", "\"\"");
            }
            return rStr;
        }
        /// <summary>
        /// 格式化字符串,是inSQL的反向
        /// </summary>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        public static string outSQL(string formatStr)
        {
            string rStr = formatStr;
            if (rStr != null)
            {
                rStr = rStr.Replace("''", "'");
                rStr = rStr.Replace("\"\"", "\"");
            }
            return rStr;
        }

        /// <summary>
        /// 查询SQL语句,删除一些SQL注入问题
        /// </summary>
        /// <param name="formatStr">需要格式化的字符串</param>
        /// <returns></returns>
        public static string querySQL(string formatStr)
        {
            string rStr = formatStr;
            if (rStr != null && rStr != "")
            {
                rStr = rStr.Replace("'", "");
            }
            return rStr;
        }
        #endregion

        #region 按当前日期和时间生成随机数
        /// <summary>
        /// 按当前日期和时间生成随机数
        /// </summary>
        /// <param name="Num">附加随机数长度</param>
        /// <returns></returns>
        public static string sRndNum(int Num)
        {
            string sTmp_Str = System.DateTime.Today.Year.ToString() + System.DateTime.Today.Month.ToString("00") + System.DateTime.Today.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00");
            return sTmp_Str + RndNum(Num);
        }
        #endregion

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

        #region 通过RNGCryptoServiceProvider 生成随机数 0-9
        /// <summary>
        /// 通过RNGCryptoServiceProvider 生成随机数 0-9 
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <returns></returns>
        public static string RndNumRNG(int length)
        {
            byte[] bytes = new byte[16];
            RNGCryptoServiceProvider r = new RNGCryptoServiceProvider();
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                r.GetBytes(bytes);
                sb.AppendFormat("{0}", (int)((decimal)bytes[0] / 256 * 10));
            }
            return sb.ToString();

        }
        #endregion

        #region 获得日志文件存放目录
        /// <summary>
        /// 获得日志文件存放目录
        /// </summary>
        //public static string LogDir
        //{
        //    get
        //    {
        //        string LogFilePath = WebRequests.GetPhysicsPath(ConfigurationManager.AppSettings["LogDir"]);
        //        if (!Directory.Exists(LogFilePath))
        //            Directory.CreateDirectory(LogFilePath);
        //        return LogFilePath;
        //    }
        //}
        #endregion

        #region "产生GUID"
        /// <summary>
        /// 获取一个GUID字符串
        /// </summary>
        public static string GetGUID
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
        #endregion

        #region "获取服务器IP"
        /// <summary>
        /// 获取服务器IP
        /// </summary>
        public static string GetServerIp
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"].ToString();
            }
        }
        #endregion

        #region "获取服务器操作系统"
        /// <summary>
        /// 获取服务器操作系统
        /// </summary>
        public static string GetServerOS
        {
            get
            {
                return Environment.OSVersion.VersionString;
            }
        }
        #endregion

        #region "获取服务器域名"
        /// <summary>
        /// 获取服务器域名
        /// </summary>
        public static string GetServerHost
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            }
        }
        #endregion

        #region "获取页面url"
        /// <summary>
        /// 获取当前访问页面地址
        /// </summary>
        public static string GetScriptName
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            }
        }

        /// <summary>
        /// 检测当前url是否包含指定的字符
        /// </summary>
        /// <param name="sChar">要检测的字符</param>
        /// <returns></returns>
        public static bool CheckScriptNameChar(string sChar)
        {
            bool rBool = false;
            if (GetScriptName.ToLower().LastIndexOf(sChar) >= 0)
                rBool = true;
            return rBool;
        }

        /// <summary>
        /// 获取当前页面的扩展名
        /// </summary>
        public static string GetScriptNameExt
        {
            get
            {
                return GetScriptName.Substring(GetScriptName.LastIndexOf(".") + 1);
            }
        }

        /// <summary>
        /// 获取当前访问页面地址参数
        /// </summary>
        public static string GetScriptNameQueryString
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
            }
        }

        /// <summary>
        /// 获取当前访问页面Url
        /// </summary>
        public static string GetScriptUrl
        {
            get
            {
                return GetScriptNameQueryString == "" ? GetScriptName : string.Format("{0}?{1}", GetScriptName, GetScriptNameQueryString);
            }
        }

        /// <summary>
        /// 返回当前页面目录的url
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        public static string GetHomeBaseUrl(string FileName)
        {
            string Script_Name = GetScriptName;
            return string.Format("{0}/{1}", Script_Name.Remove(Script_Name.LastIndexOf("/")), FileName);
        }

        /// <summary>
        /// 返回当前网站网址
        /// </summary>
        /// <returns></returns>
        public static string GetHomeUrl()
        {
            return HttpContext.Current.Request.Url.Authority;
        }

        /// <summary>
        /// 获取当前访问文件物理目录
        /// </summary>
        /// <returns>路径</returns>
        public static string GetScriptPath
        {
            get
            {
                string Paths = HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"].ToString();
                return Paths.Remove(Paths.LastIndexOf("\\"));
            }
        }
        #endregion

        #region Boolean状态判断并转换为是或否
        /// <summary>
        /// 状态判断
        /// </summary>
        /// <param name="ID">状态ID</param>
        /// <returns>否,是</returns>
        public static string GetStatus(int ID)
        {
            if (ID == 0)
                return "否";
            else
                return "是";
        }
        #endregion

    }

    #region 枚举定义
    /// <summary>
    /// 删除文件路径类型
    /// </summary>
    public enum DeleteFilePathType
    {
        /// <summary>
        /// 物理路径
        /// </summary>
        PhysicsPath = 1,
        /// <summary>
        /// 虚拟路径
        /// </summary>
        DummyPath = 2,
        /// <summary>
        /// 当前目录
        /// </summary>
        NowDirectoryPath = 3
    }

    /// <summary>
    /// 获取方式
    /// </summary>
    public enum MethodType
    {
        /// <summary>
        /// Post方式
        /// </summary>
        Post = 1,
        /// <summary>
        /// Get方式
        /// </summary>
        Get = 2
    }

    /// <summary>
    /// 获取数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 字符
        /// </summary>
        Str = 1,
        /// <summary>
        /// 日期
        /// </summary>
        Dat = 2,
        /// <summary>
        /// 整型
        /// </summary>
        Int = 3,
        /// <summary>
        /// 长整型
        /// </summary>
        Long = 4,
        /// <summary>
        /// 双精度小数
        /// </summary>
        Double = 5,
        /// <summary>
        /// 只限字符和数字
        /// </summary>
        CharAndNum = 6,
        /// <summary>
        /// 只限邮件地址
        /// </summary>
        Email = 7,
        /// <summary>
        /// 只限字符和数字和中文
        /// </summary>
        CharAndNumAndChinese = 8

    }

    /// <summary>
    /// 表操作方法
    /// </summary>
    public enum DataTable_Action
    {
        /// <summary>
        /// 插入
        /// </summary>
        Insert = 0,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 1,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 2
    }
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum OnlineCountType
    {
        /// <summary>
        /// 缓存
        /// </summary>
        Cache = 0,
        /// <summary>
        /// 数据库
        /// </summary>
        DataBase = 1
    }
    #endregion

}