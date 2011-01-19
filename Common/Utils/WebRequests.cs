using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;

namespace Tax666.Common
{
    public class WebRequests
    {
        #region 页面请求（POST、GET）和获取请求值
        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// 获取页面提交的参数值，相当于 Request.Form
        /// </summary>
        public static string Post(string name)
        {
            string value = HttpContext.Current.Request.Form[name];
            return value == null ? string.Empty : value.Trim();
        }

        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// 获取页面地址的参数值，相当于 Request.QueryString
        /// </summary>
        public static string Get(string name)
        {
            string value = HttpContext.Current.Request.QueryString[name];
            return value == null ? string.Empty : value.Trim();
        }

        /// <summary>
        /// 获取页面地址的参数值并检查安全性，相当于 Request.QueryString
        /// chkType 有 CheckGetEnum.Int， CheckGetEnum.Safety两种类型，
        /// CheckGetEnum.Int 保证参数是数字型
        /// CheckGetEnum.Safety 保证提交的参数值没有操作数据库的语句
        /// </summary>
        public static string Get(string name, CheckGetEnum chkType)
        {
            string value = Get(name);
            bool isPass = false;
            switch (chkType)
            {
                default:
                    isPass = true;
                    break;
                case CheckGetEnum.Int:
                    {
                        try
                        {
                            int.Parse(value);
                            isPass = RegExp.IsNumeric(value);
                        }
                        catch
                        {
                            isPass = false;
                        }
                        break;
                    }
                case CheckGetEnum.Safety:
                    isPass = RegExp.IsSafety(value);
                    break;
            }
            if (!isPass)
            {
                new Terminator().Throw("地址栏中参数“" + name + "”的值不符合要求或具有潜在威胁，请不要手动修改URL。");
                return string.Empty;
            }
            return value;
        }

        #endregion

        #region 获取表单或URL传值
        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.Form[strName];
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.QueryString[strName];
        }

        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            return WebUtility.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            if ("".Equals(GetQueryString(strName)))
            {
                return GetFormString(strName);
            }
            else
            {
                return GetQueryString(strName);
            }
        }
        #endregion

        #region 获得当前应用程序的路径
        /// <summary>
        /// 获得当前绝对路径
        /// 如：根目录“~/”的返回值为“G:\Tax666\AppWeb\”；
        /// WebRequests.GetMapPath("~/TestDel.aspx")返回值：G:\Tax666\AppWeb\TestDel.aspx 
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        /// <summary>
        /// 得到当前URL完整的地址;
        /// 格式：http://Tax666:8080/；
        /// </summary>
        /// <returns></returns>
        public static string GetWebUrl()
        {
            string webUrl = string.Empty;

            if (!HttpContext.Current.Request.Url.IsDefaultPort)
            {
                webUrl = @"http://" + string.Format("{0}:{1}", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port.ToString()) + "/";
            }
            else
            {
                webUrl = @"http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/";
            }

            return webUrl;
        }

        /// <summary>
        /// 返回系统安装后的Web根目录虚拟根路径(/zmsoft )。
        /// </summary>
        /// <remarks>
        public static string WebPath
        {
            get
            {
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                if (applicationPath == "/")
                {
                    return string.Empty;
                }
                else
                {
                    return applicationPath;
                }
            }
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// 格式：http://192.168.1.119:8080/TestDel.aspx
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 获得物理路径
        /// </summary>
        /// <param name="path">路径 格式：Configfile\WebSite.config（在Web.config的appSettings配置节中获取）</param>
        /// <returns>物理路径</returns>
        public static string GetPhysicsPath(string path)
        {
            string AppDir = System.AppDomain.CurrentDomain.BaseDirectory;
            if (path.IndexOf(":") < 0)
            {
                string str = path.Replace("..\\", "");
                if (str != path)
                {
                    int Num = (path.Length - str.Length) / ("..\\").Length + 1;
                    for (int i = 0; i < Num; i++)
                    {
                        AppDir = AppDir.Substring(0, AppDir.LastIndexOf("\\"));
                    }
                    str = "\\" + str;

                }
                path = AppDir + str;
            }
            return path;
        }
        #endregion

        #region 获取页面url
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
                string Script_Name = GetScriptName;
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

        #region 获得当前页面客户端的IP及IP转长整形
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.UserHostAddress;

            if (null == result || result == String.Empty || !WebUtility.IsIP(result))
                return "0.0.0.0";

            return result;
        }

        /// <summary>
        /// IP 地址字符串形式转换成长整型
        /// </summary>
        public static long Ip2Int(string ip)
        {
            if (!RegExp.IsIp(ip))
            {
                return -1;
            }
            string[] arr = ip.Split('.');
            long lng = long.Parse(arr[0]) * 16777216;
            lng += int.Parse(arr[1]) * 65536;
            lng += int.Parse(arr[2]) * 256;
            lng += int.Parse(arr[3]);
            return lng;
        }
        #endregion

        #region 获取请求的地址信息
        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL(不包括主机头)</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }
        #endregion

        #region 判断url路径 是否为根目录
        /// <summary>
        /// 判断url路径 是否为根目录
        /// Req.Url.GetLeftPart(UriPartial.Authority); 在根目录情况下，返回http://localhost;
        /// 如果是在虚拟目录下（虚拟目录设置为web）则返回的是http://localhost/web
        /// </summary>
        /// <returns></returns>
        public static string GetRootUrl()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            HttpRequest Req;
            if (HttpCurrent != null)
            {
                Req = HttpCurrent.Request;

                string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);

                if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                    //直接安装在 Web 站点   
                    AppPath = UrlAuthority;
                else
                    //安装在虚拟子目录下   
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }
        #endregion

        #region 获取当前Cookies名称的前缀
        /// <summary>
        /// 获取当前Cookies名称
        /// </summary>
        public static string Get_CookiesPrefix
        {
            get
            {
                return ConfigurationManager.AppSettings["AppPrefix"];
            }
        }
        #endregion

        #region 产生GUID
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

    }
}
