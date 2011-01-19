using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Security;
using System.Security.Cryptography;
using System.Configuration;

namespace Tax666.Common
{
    /// <summary>
    /// 字符串操作静态类
    /// </summary>
    public class StringUtil
    {
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

        #region 移除Html标记
        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }
        #endregion

        #region 过滤HTML中的不安全标签
        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }
        #endregion

        #region 检测是否有Sql危险字符
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
        #endregion

        #region 检测是否有危险的可能用于链接的字符串
        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }
        #endregion

        #region 对表 表单内容进行转换HTML操作
        /// <summary>
        /// 功能:对表 表单内容进行转换HTML操作,
        /// </summary>
        /// <param name="s">html字符串</param>
        /// <returns></returns>
        public static string HtmlCode(string s)
        {
            string str = "";
            str = s.Replace(">", "&gt;");
            str = s.Replace("<", "&lt;");
            str = s.Replace(" ", "&nbsp;");
            str = s.Replace("\n", "<br />");
            str = s.Replace("\r", "<br />");
            str = s.Replace("\r\n", "<br />");

            return str;
        }

        /// <summary>
        /// 功能:对表 表单内容进行转换HTML操作,
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CodeHtml(string s)
        {
            string str = "";
            str = s.Replace("&gt;", ">");
            str = s.Replace("&lt;", "<");
            str = s.Replace("&nbsp;", " ");
            str = s.Replace("<br />", "\n");
            str = s.Replace("<br />", "\r");
            str = s.Replace("<br />", "\r\n");

            return str;
        }
        #endregion

        #region 过滤xss攻击脚本
        /// <summary>     
        /// 过滤xss攻击脚本     
        /// </summary>     
        /// <param name="input">传入字符串</param>     
        /// <returns>过滤后的字符串</returns>     
        public static string FilterXSS(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;

            // CR(0a) ，LF(0b) ，TAB(9) 除外，过滤掉所有的不打印出来字符.     
            // 目的防止这样形式的入侵 ＜java\0script＞     
            // 注意：\n, \r,  \t 可能需要单独处理，因为可能会要用到     
            string ret = System.Text.RegularExpressions.Regex.Replace(
                html, "([\x00-\x08][\x0b-\x0c][\x0e-\x20])", string.Empty);

            ret = ret.Replace("\t", "");  //(补充，过滤TAB空格，那也是危险的XSS字符)

            //替换所有可能的16和10进制构建的恶意代码     
            //<IMG SRC=&#X40&#X61&#X76&#X61&#X73&#X63&#X72&#X69&#X70&#X74&#X3A&#X61&_#X6C&#X65&#X72&#X74&#X28&#X27&#X58&#X53&#X53&#X27&#X29>            
            ret = System.Text.RegularExpressions.Regex.Replace(ret, @"(&#[x|X]?\d+);?", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //过滤Javascript事件触发的恶意代码   
            string[] keywords = {
                                    "javascript", "vbscript", "expression", "applet", "meta", 
                                    "xml", "blink", "script", "embed", "object", 
                                    "iframe", "frame", "frameset", "ilayer", "layer", "bgsound", "title", "base",   
                                    "onabort", "onactivate", "onafterprint", "onafterupdate", "onbeforeactivate", 
                                    "onbeforecopy", "onbeforecut", "onbeforedeactivate", "onbeforeeditfocus", "onbeforepaste", 
                                    "onbeforeprint", "onbeforeunload", "onbeforeupdate", "onblur", "onbounce", "oncellchange", 
                                    "onchange", "onclick", "oncontextmenu", "oncontrolselect", "oncopy", "oncut", "ondataavailable", 
                                    "ondatasetchanged", "ondatasetcomplete", "ondblclick", "ondeactivate", "ondrag", "ondragend",
                                    "ondragenter", "ondragleave", "ondragover", "ondragstart", "ondrop", "onerror", "onerrorupdate", 
                                    "onfilterchange", "onfinish", "onfocus", "onfocusin", "onfocusout", "onhelp", "onkeydown", 
                                    "onkeypress", "onkeyup", "onlayoutcomplete", "onload", "onlosecapture", "onmousedown", 
                                    "onmouseenter", "onmouseleave", "onmousemove", "onmouseout", "onmouseover", "onmouseup", 
                                    "onmousewheel", "onmove", "onmoveend", "onmovestart", "onpaste", "onpropertychange", 
                                    "onreadystatechange", "onreset", "onresize", "onresizeend", "onresizestart", 
                                    "onrowenter", "onrowexit", "onrowsdelete", "onrowsinserted", "onscroll", "onselect", 
                                    "onselectionchange", "onselectstart", "onstart", "onstop", "onsubmit", "onunload"};

            bool found = true;
            while (found)
            {
                string retBefore = ret;
                for (int i = 0; i < keywords.Length; i++)
                {
                    //string pattern = "/"; (补允, 正则前台加/过滤不到)
                    string pattern = "";
                    for (int j = 0; j < keywords[i].Length; j++)
                    {
                        if (j > 0)
                            pattern = string.Concat(pattern, '(', "(&#[x|X]0{0,8}([9][a][b]);?)?", "|(&#0{0,8}([9][10][13]);?)?",
                                ")?");
                        pattern = string.Concat(pattern, keywords[i][j]);
                    }
                    string replacement = string.Concat(keywords[i].Substring(0, 2), "＜x＞", keywords[i].Substring(2));
                    ret = System.Text.RegularExpressions.Regex.Replace(ret, pattern, replacement, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    if (ret == retBefore)
                        found = false;
                }

            }

            return ret;
        }
        #endregion

        #region 用正则表达式对字符串进行验证
        /// <summary>
        /// 用正则表达式对字符串进行验证
        /// </summary>
        /// <param name="str">被验证的字符串</param>
        /// <param name="i">序号:1,正整数和0;2,非负数;3,0到99之间的整数;4,中文字符;5,密码至少6个字符，可使用字母、数字及下划线！</param>
        /// <returns>返回出错语句,如为""则验正通过</returns>
        public static string InputValidate(string str, int i)
        {
            if (i == 0)
            {
                return "";
            }
            string exp = "", tip = "";
            switch (i)
            {
                case 1:
                    exp = @"^\d+$";  //正整数和0
                    tip = "只能输入正整数和0";
                    break;
                case 2:
                    exp = @"^\d+(\.\d+)?$"; //非负数
                    tip = "只能输入非负数";
                    break;
                case 3:
                    exp = @"^(\d|[1-9]\d)$";  //0到99之间的整数
                    tip = "只能输入0到99之间的整数";
                    break;
                case 4:
                    exp = @"[\u4e00-\u9fa5]"; //中文字符
                    tip = "只能输入中文字符";
                    break;
                case 5:
                    exp = @"^\w{6,}$";
                    tip = "密码至少6个字符，可使用字母、数字及下划线！";
                    break;
                default:
                    exp = "";
                    tip = "";
                    break;


            }

            if (Regex.IsMatch(str, exp))
            {
                return "";
            }
            else
            {
                return tip;
            }
        }
        #endregion

        #region 判断是否是数字
        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>全数字返回True,若有某个值不为数字返回False</returns>
        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 字符串直接md5加密
        /// <summary>
        /// 字符串直接md5加密
        /// </summary>
        /// <param name="str">需加密的字符串</param>
        /// <returns>md5加密后的密码</returns>
        public static string StringTomd5(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str,"md5").ToLower();

        }
        #endregion

        #region 字符串转换小写后md5加密
        /// <summary>
        /// 字符串转换小写后md5加密
        /// </summary>
        /// <param name="psw">密码明码</param>
        /// <returns>md5加密后的密码</returns>
        public static string stringTomd5(string str)
        {
            str = str.ToLower();
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str,"md5").ToLower();

        }
        #endregion

        #region 字符串直接SHA1加密
        /// <summary>
        /// 字符串直接SHA1加密
        /// </summary>
        /// <param name="str">需加密的字符串</param>
        /// <returns>SHA1加密后的密码</returns>
        public static string StringToSHA1(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1").ToLower();

        }
        #endregion

        #region 字符串转换小写后SHA1加密
        /// <summary>
        /// 字符串转换小写后SHA1加密
        /// </summary>
        /// <param name="psw">密码明码</param>
        /// <returns>SHA1加密后的密码</returns>
        public static string stringToSHA1(string str)
        {
            str = str.ToLower();
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");

        }
        #endregion

        #region 默认密钥向量 DES加密解密用
        /// <summary>
        /// 默认密钥向量
        /// </summary>
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        #endregion

        #region DES加密字符串，需设置加密密钥,要求为8位
        /// <summary>
        /// DES加密字符串，需设置加密密钥,要求为8位
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }
        #endregion

        #region DES解密字符串,需加密时的密钥,要求为8位
        /// <summary>
        /// DES解密字符串,需加密时的密钥,要求为8位
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        #endregion

        #region "按当前日期和时间生成随机数"
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

        #region "通过RNGCryptoServiceProvider 生成随机数 0-9"
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

        #region "转换编码"
        /// <summary>
        /// 转换编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encode(string str)
        {
            if (str == null)
            {
                return "";
            }
            else
            {

                return System.Web.HttpUtility.UrlEncode(Encoding.GetEncoding(54936).GetBytes(str));
            }
        }
        #endregion

        #region "按字符串位数补0"
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

        #region "正式表达式验证"
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

        #region "检测是否为有效邮件地址格式"
        /// <summary>
        /// 检测是否为有效邮件地址格式
        /// </summary>
        /// <param name="strIn">输入邮件地址</param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion

        #region 返回字符串在字符串中出现的次数
        /// <summary>
        /// 返回字符串在字符串中出现的次数
        /// </summary>
        /// <param name="Char">要检测出现的字符</param>
        /// <param name="String">要检测的字符串</param>
        /// <returns>出现次数</returns>
        public static int GetCharInStringCount(string Char, string String)
        {
            string str = String.Replace(Char, "");
            return (String.Length - str.Length) / Char.Length;

        }
        #endregion

        #region 全角半角转换
        /// <summary>
        /// 转全角的函数(SBC case)
        /// javascript: onblur="javascript:this.value=this.value.replace(/，/ig,',');"
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary> 转半角的函数(DBC case) </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        
        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="SBCCase"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] c = SBCCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
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
                return "Tax666_manager";
            }
        }
        #endregion

        #region "获取登陆用户UserID"
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

        #region "获取当前Cookies名称"
        /// <summary>
        /// "获取当前Cookies名称
        /// </summary>
        public static string Get_CookiesName
        {
            get
            {
                return "Tax666_manager";
            }
        }
        #endregion

        #region "获取用户IP地址"
        /// <summary>
        /// 获取用户IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress() 
        {
            string user_IP = string.Empty;
            user_IP = System.Web.HttpContext.Current.Request.UserHostAddress;
            return user_IP;
        }
        #endregion

        #region "用户在线时间"
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

        #region "获得在线统计数据保存环境"
        /// <summary>
        /// 获得在线统计数据保存环境
        /// </summary>
        public static OnlineCountType GetOnlineCountType
        {
            get
            {
                if (GetConfigOnlineCountType == 1)
                    return OnlineCountType.DataBase;
                else
                    return OnlineCountType.Cache;
            }
        }

        /// <summary>
        /// 获得配置在线统计类型
        /// </summary>
        private static int GetConfigOnlineCountType
        {
            get
            {
                int rInt = 0;
                try
                {
                    rInt = Convert.ToInt32(ConfigurationManager.AppSettings["OnlineCountType"]);
                }
                catch (Exception)
                {
                   // FileTxtLogs.WriteLog(ex.ToString());
                }
                return rInt;
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
        /// 获得页面文件名和参数名
        /// </summary>
        public static string GetScriptNameUrl
        {
            get
            {
                string Script_Name = StringUtil.GetScriptName;
                Script_Name = Script_Name.Substring(Script_Name.LastIndexOf("/") + 1);
                Script_Name += "?" + GetScriptNameQueryString;
                return Script_Name;
            }
        }

        /// <summary>
        /// 获取当前访问页面Url
        /// </summary>
        public static string GetScriptUrl
        {
            get
            {
                return StringUtil.GetScriptNameQueryString == "" ? StringUtil.GetScriptName : string.Format("{0}?{1}", StringUtil.GetScriptName, StringUtil.GetScriptNameQueryString);
            }
        }

        /// <summary>
        /// 返回当前页面目录的url
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        public static string GetHomeBaseUrl(string FileName)
        {
            string Script_Name = StringUtil.GetScriptName;
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

        #region "数据库设置"

        /// <summary>
        /// 获取数据库类型
        /// </summary>
        public static string GetDBType
        {
            get
            {
                return ConfigurationManager.AppSettings["DBType"];
            }
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        public static string GetConnString
        {
            get
            {
                return ConfigurationManager.AppSettings[GetDBType];
            }
        }
        #endregion

        #region "获得sessionid"
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

        #region "格式化日期24小时制为字符串如:2008/12/12 21:22:33"
        /// <summary>
        /// 格式化日期24小时制为字符串如:2008/12/12 21:22:33
        /// </summary>
        /// <param name="d">日期</param>
        /// <returns>字符</returns>
        public static string FormatDateToString(DateTime d)
        {
            return d.ToString("yyyy/MM/dd HH:mm:ss");
        }
        #endregion

        #region 获取指定长度的字符串
        /// <summary>
        /// 获取指定长度的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetStrLength(string str, int length)
        {
            int i = 0, j = 0;
            foreach (char chr in str)
            {
                if ((int)chr > 127)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }
                if (i > length)
                {
                    str = str.Substring(0, j);
                    break;
                }
                j++;
            }
            return str;
        }
        #endregion

        /// <summary>
        /// 控制DataGrid,DataList等中字符串的长度;
        /// 在aspx页面调用的方法：
        /// SmartVOD.Components.StringUtil.ControlStrLength("需控制的字符串",限制长度);
        /// 一个汉字为两个字符；
        /// </summary>
        /// <param name="str">需要控制的字符串</param>
        /// <param name="length">最大长度值</param>
        /// <returns></returns>
        public static string ControlStrLength(string str, int length)
        {
            int i = 0, j = 0;
            foreach (char chr in str)
            {
                if ((int)chr > 127)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }
                if (i > length)
                {
                    str = str.Substring(0, j) + "…";
                    break;
                }
                j++;
            }
            return str;
        }
    }
}
