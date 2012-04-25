
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;
using System.Web;
using System.Data;
using BP.En;
using BP.DA;
using System.Configuration;
using BP.Port;

namespace BP.Web
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        SysAdmin,
        /// <summary>
        /// 应用管理员
        /// </summary>
        AppAdmin,
        /// <summary>
        /// 普通用户
        /// </summary>
        User
    }
    /// <summary>
    /// User 的摘要说明。
    /// </summary>
    public class WebUser
    {
        public static void DeleteTempFileOfMy()
        {
            //PubClass.ResponseWriteScript("");
            HttpCookie hc = System.Web.HttpContext.Current.Request.Cookies["CCS"];

            if (hc == null)
                return;
            string usr = hc.Values["No"];
            string[] strs = System.IO.Directory.GetFileSystemEntries(SystemConfig.PathOfTemp);
            foreach (string str in strs)
            {
                if (str.IndexOf(usr) == -1)
                    continue;

                try
                {
                    System.IO.File.Delete(str);
                }
                catch
                {
                }
            }
        }
        public static void DeleteTempFileOfAll()
        {
            string[] strs = System.IO.Directory.GetFileSystemEntries(SystemConfig.PathOfTemp);
            foreach (string str in strs)
            {
                try
                {
                    System.IO.File.Delete(str);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 密码解密
        /// </summary>
        /// <param name="pass">用户输入密码</param>
        /// <returns>解密后的密码</returns>
        public static string ParsePass(string pass)
        {
            if (pass == "")
                return "";

            string str = "";
            char[] mychars = pass.ToCharArray();
            int i = 0;
            foreach (char c in mychars)
            {
                i++;

                //step 1 
                long A = Convert.ToInt64(c) * 2;

                // step 2
                long B = A - i * i;

                // step 3 
                long C = 0;
                if (B > 196)
                    C = 196;
                else
                    C = B;

                str = str + Convert.ToChar(C).ToString();
            }
            return str;
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="em"></param>
        public static void SignInOfGener(Emp em)
        {
            SignInOfGener(em, "CH", null,true,false);
        }
      
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="em"></param>
        /// <param name="isRememberMe"></param>
        public static void SignInOfGener(Emp em, bool isRememberMe)
        {
            SignInOfGener(em, "CH", null, isRememberMe, false);
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="em"></param>
        /// <param name="auth"></param>
        public static void SignInOfGenerAuth(Emp em, string auth)
        {
            SignInOfGener(em, "CH", auth, true, false);
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="em"></param>
        /// <param name="lang"></param>
        public static void SignInOfGenerLang(Emp em, string lang, bool isRememberMe)
        {
            SignInOfGener(em, lang, null, isRememberMe, false);
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="em"></param>
        /// <param name="lang"></param>
        public static void SignInOfGenerLang(Emp em, string lang)
        {
            SignInOfGener(em, lang, null, true,false);
        }
        public static void SignInOfGener(Emp em, string lang)
        {
            SignInOfGener(em, lang, em.No, true,false);
        }
        public static void SignInOfGener(Emp em, string lang, string auth, bool isRememberMe)
        {
            SignInOfGener(em, lang, auth, isRememberMe, false);
        }
        /// <summary>
        /// 通用的登录
        /// </summary>
        /// <param name="em">人员</param>
        /// <param name="lang">语言</param>
        /// <param name="auth">授权人</param>
        /// <param name="isRememberMe">是否记录cookies</param>
        /// <param name="IsRecSID">是否记录SID</param>
        public static void SignInOfGener(Emp em, string lang, string auth, bool isRememberMe,bool IsRecSID)
        {
            if (System.Web.HttpContext.Current == null)
                SystemConfig.IsBSsystem = false;
            else
                SystemConfig.IsBSsystem = true;

            if (BP.SystemConfig.IsBSsystem && System.Web.HttpContext.Current.Session != null)
            {
                System.Web.HttpContext.Current.Session.Clear();
            }

            if (em.No == "admin")
            {
                if (em.FK_Dept.Length != 2)
                    throw new Exception("@您没有把admin设置成最高的部门，这样会造成数据查询权限的错误，当然部门编号是("+em.FK_Dept+")");
            }

            WebUser.Auth = auth;
            WebUser.No = em.No;
            WebUser.Name = em.Name;
            WebUser.FK_Dept = em.FK_Dept;
            WebUser.FK_DeptName = em.FK_DeptText;
            WebUser.AppUserType = "Gener";
            WebUser.HisDepts = null;
            WebUser.HisStations = null;

            if (SystemConfig.IsUnit)
            {
                WebUser.FK_Unit = em.FK_Unit;
                WebUser.FK_UnitName = em.FK_UnitText;
            }

            if (lang == null)
                lang = WebUser.LangOfcookie;

            if (IsRecSID)
            {
                /*如果记录sid*/
                string sid1 = DateTime.Now.ToString("MMddHHmmss");
                DBAccess.RunSQL("UPDATE Port_Emp SET SID='" + sid1 + "' WHERE No='" + WebUser.No + "'");
                WebUser.SID = sid1;
            }

            WebUser.SysLang = lang;
            if (BP.SystemConfig.IsBSsystem)
            {
                System.Web.HttpContext.Current.Response.Cookies.Clear();
                HttpCookie cookie = new HttpCookie("CCS");
                cookie.Expires = DateTime.Now.AddMonths(10);
                cookie.Values.Add("No", em.No);
                cookie.Values.Add("Name", HttpUtility.UrlEncode(em.Name));

                if (isRememberMe)
                    cookie.Values.Add("IsRememberMe", "1");
                else
                    cookie.Values.Add("IsRememberMe", "0");

                cookie.Values.Add("FK_Dept", em.FK_Dept);
                cookie.Values.Add("FK_DeptName", HttpUtility.UrlEncode(em.FK_DeptText));

                cookie.Values.Add("Token", System.Web.HttpContext.Current.Session.SessionID);
                cookie.Values.Add("Lang", lang);

                string  isEnableStyle=SystemConfig.AppSettings["IsEnableStyle"];
                if (isEnableStyle == "1")
                {
                    try
                    {
                        string sql = "SELECT Style FROM WF_Emp WHERE No='" + WebUser.No + "' ";
                        int val = DBAccess.RunSQLReturnValInt(sql, 0);
                        cookie.Values.Add("Style", val.ToString());
                        WebUser.Style = val.ToString();
                    }
                    catch
                    {
                    }
                }

                if (SystemConfig.IsUnit)
                {
                    cookie.Values.Add("FK_Unit", em.FK_Unit);
                    cookie.Values.Add("FK_UnitText", em.FK_UnitText);
                }

                if (auth == null)
                    auth = "";
                cookie.Values.Add("Auth", auth); //授权人.
                System.Web.HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
        public static void SignInOfGTSUser(Emp em)
        {
            WebUser.FontSize = "12px";
            WebUser.No = em.No;
            WebUser.Name = em.Name;
            WebUser.AppUserType = "Tax";
            WebUser.FK_Dept = em.FK_Dept; //.FK_Dept;
            WebUser.FK_DeptName = em.FK_DeptText; //// BP.DA.DBAccess.RunSQLReturnVal("select addr from Port_Emp WHERE no='" + em.No + "'").ToString();
            //   WebUser.FK_DeptName = BP.DA.DBAccess.RunSQLReturnVal("select addr from Port_Emp WHERE no='" + em.No + "'").ToString();

            WebUser.SetSessionByKey("MyFileExt", BP.DA.DBAccess.RunSQLReturnVal("select myfileext from Port_Emp WHERE no='" + em.No + "'").ToString());

            if (System.Web.HttpContext.Current == null)
                SystemConfig.IsBSsystem = false;
            else
                SystemConfig.IsBSsystem = true;

            //WebUser.HisBPUser = em;
        }
        /// <summary>
        /// 用户类型
        /// </summary>
        public static UserType HisUserType
        {
            get
            {
                if (WebUser.No == "admin")
                    return BP.Web.UserType.SysAdmin;
                if (WebUser.No.IndexOf("8888") != -1)
                    return BP.Web.UserType.AppAdmin;
                return BP.Web.UserType.User;
            }
        }

        #region 静态方法
        /// <summary>
        /// 通过key,取出session.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="isNullAsVal">如果是Null, 返回的值.</param>
        /// <returns></returns>
        public static string GetSessionByKey(string key, string isNullAsVal)
        {
            if (IsBSMode)
            {
                string str = System.Web.HttpContext.Current.Session[key] as string;
                if (string.IsNullOrEmpty(str))
                    str = isNullAsVal;
                return str;
            }
            else
            {
                if (BP.Port.Win.Current.Session[key] == null || BP.Port.Win.Current.Session[key].ToString() == "")
                {
                    BP.Port.Win.Current.Session[key] = isNullAsVal;
                    return isNullAsVal;
                }
                else
                    return (string)BP.Port.Win.Current.Session[key];
            }
        }
        public static object GetObjByKey(string key)
        {
            if (IsBSMode)
            {
                return System.Web.HttpContext.Current.Session[key];
            }
            else
            {
                return BP.Port.Win.Current.Session[key];
            }
        }
        #endregion

        /// <summary>
        /// FontSize
        /// </summary>
        public static string FontSize
        {
            get
            {
                return GetSessionByKey("FontSize", "12px");
            }
            set
            {
                SetSessionByKey("FontSize", value);
            }
        }
        /// <summary>
        /// 是不是b/s 工作模式。
        /// </summary>
        protected static bool IsBSMode
        {
            get
            {
                if (System.Web.HttpContext.Current == null)
                    return false;
                else
                    return true;
            }
        }
        public static object GetSessionByKey(string key, Object defaultObjVal)
        {
            if (IsBSMode)
            {
                if (System.Web.HttpContext.Current.Session[key] == null)
                    return defaultObjVal;
                else
                    return System.Web.HttpContext.Current.Session[key];
            }
            else
            {
                if (BP.Port.Win.Current.Session[key] == null)
                    return defaultObjVal;
                else
                    return BP.Port.Win.Current.Session[key];
            }
        }
        public static void SetSessionByKey(string key, object val)
        {
            if (IsBSMode)
                System.Web.HttpContext.Current.Session[key] = val;
            else
                BP.Port.Win.Current.SetSession(key, val);
        }

        public static void Exit()
        {
            if (IsBSMode)
            {
                string token = WebUser.Token;
                System.Web.HttpContext.Current.Response.Cookies.Clear();
                System.Web.HttpContext.Current.Request.Cookies.Clear();
               // return;

                HttpCookie cookie = new HttpCookie("CCS", string.Empty);
                cookie.Expires = DateTime.Now.AddMinutes(1);
                cookie.Values.Add("No", string.Empty);
                cookie.Values.Add("Name", string.Empty);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                WebUser.Token = token;
            }
            else
            {
                try
                {
                    string token = WebUser.Token;
                    System.Web.HttpContext.Current.Response.Cookies.Clear();
                    System.Web.HttpContext.Current.Request.Cookies.Clear();
                    // return;

                    HttpCookie cookie = new HttpCookie("CCS", string.Empty);
                    cookie.Expires = DateTime.Now.AddMinutes(1);
                    cookie.Values.Add("No", string.Empty);
                    cookie.Values.Add("Name", string.Empty);
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                    WebUser.Token = token;
                }
                catch
                {
                }


                BP.Port.Win.Current.Session.Clear();


            }
        }
        /// <summary>
        /// 授权人
        /// </summary>
        public static string Auth
        {
            get
            {
                return GetSessionByKey("Auth", null);
            }
            set
            {
                if (value == "")
                    SetSessionByKey("Auth", null);
                else
                    SetSessionByKey("Auth", value);
            }
        }
        public static string FK_DeptName
        {
            get
            {
                return GetSessionByKey("FK_DeptName", null);
            }
            set
            {
                SetSessionByKey("FK_DeptName", value);
            }
        }
        public static string FK_UnitName
        {
            get
            {
                return GetSessionByKey("FK_UnitName", null);
            }
            set
            {
                SetSessionByKey("FK_UnitName", value);
            }
        }
        public static string SysLang
        {
            get
            {
                return "CH";
                string no = GetSessionByKey("Lang", null);
                if (no == null || no == "")
                {
                    if (IsBSMode)
                    {
                        HttpCookie hc1 = System.Web.HttpContext.Current.Request.Cookies["CCS"];
                        if (hc1 == null)
                            return "CH";
                        SetSessionByKey("Lang", hc1.Values["Lang"]);
                    }
                    else
                    {
                        return "CH";
                    }
                    return GetSessionByKey("Lang", "CH");
                }
                else
                {
                    return no;
                }
            }
            set
            {
                SetSessionByKey("Lang", value);
            }
        }

        public static string GetValFromSessionOrCookies(string sessionkey)
        {
            if (WebUser.No == null)
                throw new Exception("@登陆时间太长。");

            string s = GetSessionByKey(sessionkey, null);
            if (string.IsNullOrEmpty(s) == false)
                return s;

            if (BP.SystemConfig.IsBSsystem==false)
                return null;

            string key = "CCS";
            HttpCookie hc = System.Web.HttpContext.Current.Request.Cookies[key];
            if (hc == null)
                throw new Exception("@您登陆信息已经超时，或者您的IE不支持记录cookies.");

            if (hc.Values["No"] != null)
            {
                WebUser.No = hc["No"];
                WebUser.FK_Dept = hc["FK_Dept"];
                WebUser.Auth = hc["Auth"];
                WebUser.FK_DeptName = HttpUtility.UrlDecode(hc["FK_DeptName"]);

                if (BP.SystemConfig.IsUnit)
                {
                    WebUser.FK_Unit = HttpUtility.UrlDecode(hc["FK_Unit"]);
                    WebUser.FK_UnitName = HttpUtility.UrlDecode(hc["FK_UnitName"]);
                }

                string name = BP.DA.DBAccess.RunSQLReturnStringIsNull("SELECT Name, FK_Dept FROM Port_Emp WHERE No='" + HttpUtility.UrlDecode(hc["No"] + "'"), "null");
                if (name == "null")
                    throw new Exception("@您需要重新登陆。");

                WebUser.SetSessionByKey("Name", name);
                return hc.Values[sessionkey];
            }
            throw new Exception("@登陆时间太长或者是您的浏览器不支持cookies.");
        }
        /// <summary>
        /// FK_Dept
        /// </summary>
        public static string FK_Dept
        {
            get
            {
                string s = GetSessionByKey("FK_Dept", null);
                if (string.IsNullOrEmpty(s))
                {
                    s =WebUser.GetValFromSessionOrCookies("FK_Dept");
                    if (s == null)
                    {
                        s = DBAccess.RunSQLReturnStringIsNull("SELECT FK_Dept FROM Port_Emp WHERE No='"+WebUser.No+"'",null);
                        if (string.IsNullOrEmpty(s))
                            throw new Exception("@人员("+WebUser.No+")没有设置部门。");
                        SetSessionByKey("FK_Dept", s);
                    }
                }
                return s;
            }
            set
            {
                SetSessionByKey("FK_Dept", value);
            }
        }
        public static string FK_Station
        {
            get
            {
                return GetSessionByKey("FK_Station", null);
            }
            set
            {
                SetSessionByKey("FK_Station", value);
            }
        }
        /// <summary>
        /// 检查权限控制
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static bool CheckSID(string sid)
        {
            string mysid = DBAccess.RunSQLReturnStringIsNull("SELECT SID FROM PORT_EMP WHERE No='" + Web.WebUser.No + "'", null);
            if (sid == mysid)
                return true;
            else
                return false;
        }
        public static string FK_Unit
        {
            get
            {
                string s = GetSessionByKey("FK_Unit", null);
                if (string.IsNullOrEmpty(s))
                {
                    s = WebUser.GetValFromSessionOrCookies("FK_Unit");
                }
                return s;
            }
            set
            {
                SetSessionByKey("FK_Unit", value);
            }
        }
        /// <summary>
        /// 用户类型 Tax, Taxpayer . 
        /// DS , GS , GongShang 
        /// </summary>
        public static string AppUserType
        {
            get
            {
                return GetSessionByKey("UserType", "DS");
            }
            set
            {
                SetSessionByKey("UserType", value);
            }
        }
        public static string NoOfRel
        {
            get
            {
                return GetSessionByKey("No", null);
            }
        }
        public static string NoOfSessionID
        {
            get
            {
                string s= GetSessionByKey("No",null);
                if (s == null)
                    return System.Web.HttpContext.Current.Session.SessionID;
                return s;
            }
        }
        public static string LangOfcookie
        {
            get
            {
                return "CH";

                string key = "CCS";
                HttpCookie hc1 = System.Web.HttpContext.Current.Request.Cookies[key];
                if (hc1 == null)
                    return null;

                if (hc1.Values["No"] != null)
                    return hc1.Values["Lang"];
                return SystemConfig.SysLanguage;
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public static string No
        {
            get
            {
                string no = GetSessionByKey("No", null);
                if (no == null || no == "")
                {
                    if (IsBSMode == false)
                        return "admin";

                    //if (IsBSMode == false)
                    //    throw new Exception("d no ddddddddd");

                    string key = "CCS";
                    HttpCookie hc = System.Web.HttpContext.Current.Request.Cookies[key];
                    if (hc == null)
                        return null;

                    if (hc.Values["No"] != null)
                    {
                        WebUser.No = hc["No"];
                        WebUser.FK_Dept = hc["FK_Dept"];
                        WebUser.Auth = hc["Auth"];
                        WebUser.FK_DeptName = HttpUtility.UrlDecode(hc["FK_DeptName"]);

                        if (BP.SystemConfig.IsUnit)
                        {
                            WebUser.FK_Unit = HttpUtility.UrlDecode(hc["FK_Unit"]);
                            WebUser.FK_UnitName = HttpUtility.UrlDecode(hc["FK_UnitName"]);
                        }

                        string name = BP.DA.DBAccess.RunSQLReturnStringIsNull("SELECT Name, FK_Dept FROM Port_Emp WHERE No='" + HttpUtility.UrlDecode(hc["No"] + "'"), "null");
                        if (name == "null")
                            return null;

                        WebUser.SetSessionByKey("Name", name);
                        return hc.Values["No"];
                    }
                    throw new Exception("@err-001 登陆信息丢失。");
                }
                return no;
            }
            set
            {
                SetSessionByKey("No", value);
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public static string Name
        {
            get
            {
                return GetSessionByKey("Name", BP.Web.WebUser.No);
            }
            set
            {
                SetSessionByKey("Name", value);
            }
        }
        public static string Tag
        {
            get
            {
                return GetSessionByKey("Tag", "null");
            }
            set
            {
                SetSessionByKey("Tag", value);
            }
        }
        /// <summary>
        /// 令牌
        /// </summary>
        public static string Token
        {
            get
            {
                return GetSessionByKey("token", "null");
            }
            set
            {
                SetSessionByKey("token", value);
            }
        }
        public static string Style
        {
            get
            {
               return GetSessionByKey("Style", "0");
            }
            set
            {
                SetSessionByKey("Style", value);
            }
        }
        /// <summary>
        /// 当前工作人员实体
        /// </summary>
        public static Emp HisEmp
        {
            get
            {
                return new Emp(WebUser.No);
            }
        }
        public static Station HisStation
        {
            get
            {
                return new Station(WebUser.FK_Station);
            }
        }
        public static Stations HisStations
        {
            get
            {
                object obj = null;
                obj = GetSessionByKey("HisSts", obj);
                if (obj == null)
                {
                    Stations sts = new Stations();
                    QueryObject qo = new QueryObject(sts);
                    qo.AddWhereInSQL("No", "SELECT FK_Station FROM Port_EmpStation WHERE FK_Emp='" + WebUser.No + "'");
                    qo.DoQuery();
                    SetSessionByKey("HisSts", sts);
                    return sts;
                }
                return obj as Stations;
            }
            set
            {
                SetSessionByKey("HisSts", value);
            }
        }
        public static Depts HisDepts
        {
            get
            {
                object obj = null;
                obj = GetSessionByKey("HisDepts", obj);
                if (obj == null)
                {
                    Depts sts = WebUser.HisEmp.HisDepts;
                    SetSessionByKey("HisDepts", sts);
                    return sts;
                }
                return obj as Depts;
            }
            set
            {
                SetSessionByKey("HisDepts", value);
            }
        }
        /// <summary>
        /// 最后登录日期
        /// </summary>
        public static string LastEnterDate
        {
            get
            {
                return (string)GetSessionByKey("LastEnterDate", false);
            }
            set
            {
                SetSessionByKey("LastEnterDate", value);
            }
        }
        /// <summary>
        /// SID
        /// </summary>
        public static string SID
        {
            get
            {
                return (string)GetSessionByKey("SID", null);
            }
            set
            {
                SetSessionByKey("SID", value);
            }
        }
        /// <summary>
        /// 是否是授权状态
        /// </summary> 
        public static bool IsAuthorize
        {
            get
            {
                if (Auth ==null || Auth=="" )
                    return false;
                return true;
            }
        }
        /// <summary>
        /// 使用授权人ID
        /// </summary>
        public static string AuthorizerEmpID
        {
            get
            {
                return (string)GetSessionByKey("AuthorizerEmpID", false);

            }
            set
            {
                SetSessionByKey("AuthorizerEmpID", value);

            }
        }
        /// <summary>
        /// IsWap
        /// </summary>
        public static bool IsWap
        {
            get
            {
                if (BP.SystemConfig.IsBSsystem == false)
                    return false;

                int s = (int)GetSessionByKey("IsWap", 9);
                if (s == 9)
                {
                    bool b = System.Web.HttpContext.Current.Request.RawUrl.ToLower().Contains("wap");
                    IsWap = b;
                    return b;
                }
                if (s == 1)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    SetSessionByKey("IsWap", 1);
                else
                    SetSessionByKey("IsWap", 0);
            }
        }

        #region 功能


        #endregion 功能

        #region 部门权限
        /// <summary>
        /// 部门权限
        /// </summary>
        public static Depts HisDeptsOfPower
        {
            get
            {
                EmpDepts eds = new EmpDepts();
                return eds.GetHisDepts(WebUser.No);

                //				if (WebUser._HisDeptsOfPower==null)
                //				{
                //					EmpDepts eds = new EmpDepts();
                //					_HisDeptsOfPower=eds.GetHisDepts(WebUser.No);					
                //				}
                //				return _HisDeptsOfPower;
            }
        }
        #endregion 部门权限

        #region 工作人员权限
        /// <summary>
        /// 工作人员权限
        /// </summary>
        //private static 县局 _His县局OfPower=null;
        /// <summary>
        /// 工作人员权限
        /// </summary>
        public static Emps HisEmpsOfPower
        {
            get
            {
                Paras ens = new Paras("p", WebUser.FK_Dept);

                string sql = "";
                switch (DBAccess.AppCenterDBType)
                {
                    case DBType.Oracle9i:
                        sql = "select EmpID as OID,  Name||' '||No as Text from pub_emp WHERE fk_dept like :p%'";
                        break;
                    case DBType.SQL2000:
                    case DBType.Access:
                    case DBType.DB2:
                        sql = "select EmpID as OID, No+' '+Name as Text from pub_emp WHERE fk_dept like :p%'";
                        break;
                    default:
                        break;
                }
                DataTable dt = DBAccess.RunSQLReturnTable(sql);
                Emps emps = new Emps();
                foreach (DataRow dr in dt.Rows)
                {
                    emps.AddEntity(new Emp(dr[0].ToString()));
                }
                return emps;

                //				if (WebUser._His县局OfPower==null)
                //				{
                //					string sql="select EmpID as OID, No+' '+Name as Text from pub_emp WHERE fk_dept like '"+WebUser.FK_Dept+"%'" ;
                //					DataTable dt = DBAccess.RunSQLReturnTable(sql); 
                //					县局 emps = new 县局();
                //					foreach(DataRow dr in dt.Rows)
                //					{
                //						emps.AddEntity( new Emp(int.Parse(dr[0].ToString()))) ; 
                //					}
                //					_His县局OfPower=emps;
                //				}
                //				return _His县局OfPower;
            }
        }
        #endregion 工作人员权限



        /// <summary>
        /// 当前共享的用户
        /// </summary>
        public static string ShareUserNo
        {
            get
            {
                return GetSessionByKey("ShareUserNo", Web.WebUser.No);
            }
            set
            {
                Emp emp = new Emp(value);
                SetSessionByKey("ShareUserNo", emp.No);
                SetSessionByKey("ShareUserName", emp.Name);
            }
        }
        /// <summary>
        /// 当前共享用户名称
        /// </summary>
        public static string ShareUserName
        {
            get
            {
                return GetSessionByKey("ShareUserName", Web.WebUser.Name);
            }
        }
        /// <summary>
        /// (OA 共享信息用到 )是否是当前的用户
        /// </summary>
        public static bool IsCurrUser
        {
            get
            {
                if (Web.WebUser.No == ShareUserNo)
                    return true;
                else
                    return false;
            }
        }

    }
}
