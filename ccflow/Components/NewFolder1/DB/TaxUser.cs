using System;
using System.Configuration ; 
using System.Web;
using System.Data;
using BP.En;
using BP.DA;
using BP.Port;
 
namespace BP.Web
{
	/// <summary>
	/// User 的摘要说明。
	/// </summary>
    public class TaxUser : WebUser
    {
        public static WorkFloor HisWorkFloor
        {
            get
            {
                if (SystemConfig.SysNo == SysNoList.GS)
                {
                    switch (FK_ZSJG.Length)
                    {
                        case 5:
                            return WorkFloor.SJ;
                        case 7:
                            return WorkFloor.XJ;
                        case 11:
                            return WorkFloor.FJ;
                        default:
                            throw new Exception("国税用户[" + No + "]管理机关设置错误:" + FK_ZSJG);
                    }
                }
                else
                {
                    switch (FK_ZSJG.Length)
                    {
                        case 2:
                            return WorkFloor.ShengJu;
                        case 4:
                            return WorkFloor.SJ;
                        case 6:
                            return WorkFloor.XJ;
                        default:
                            return WorkFloor.FJ;
                    }
                }
            }
        }
        public static int HisWorkFloorOfInt
        {
            get
            {
                return FK_ZSJG.Length / 2;
            }
        }

        #region  用户判断．
        /// <summary>
        /// 是否是内勤用户
        /// </summary>
        public static bool IsNQUser_del
        {
            get
            {
                Paras ens = new Paras("p", WebUser.No);
                string sql = "select count( * )  from port_empstation where fk_emp=:p and fk_station='00000000080001'";

                if (DBAccess.RunSQLReturnValInt(sql, ens) == 0)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 是否是市局用户
        /// </summary>
        public static bool IsSJUser
        {
            get
            {
                if (HisWorkFloor == WorkFloor.SJ)
                    return true;
                return false;
            }
        }
        /// <summary>
        /// 省局长用户
        /// </summary>
        public static bool IsShengJuUser
        {
            get
            {
                if (HisWorkFloor == WorkFloor.ShengJu)
                    return true;
                return false;
            }
        }
        /// <summary>
        /// 是否是县局用户
        /// </summary>
        public static bool IsXJUser
        {
            get
            {
                if (FK_ZSJG.Length == 6)
                    return true;

                return false;


                //if (HisWorkFloor == WorkFloor.XJ)
                //    return true;
                //return false;
            }
        }
        /// <summary>
        /// 是否是分局用户
        /// </summary>
        public static bool IsFJUser
        {
            get
            {
                if (HisWorkFloor == WorkFloor.FJ)
                    return true;
                return false;
            }
        }
        #endregion

        #region 职务的判断
        /// <summary>
        /// 是否是分局(所)长
        /// </summary>
        public static bool IsStation_FJZ
        {
            get
            {
                string str = GetSessionByKey("IsStation_FJZ", "");
                switch (str)
                {
                    case "":
                        if (WebUser.HisStations.Contains("010300"))
                        {
                            SetSessionByKey("IsStation_FJZ", true);
                            return true;
                        }
                        else
                        {
                            SetSessionByKey("IsStation_FJZ", false);
                            return false;
                        }
                    case "1":
                        return true;
                    case "0":
                        return false;
                    default:
                        throw new Exception("No this case .");
                }
            }
        }
        /// <summary>
        /// 是否县局长
        /// </summary>
        public static bool IsStation_XJZ
        {
            get
            {
                string str = GetSessionByKey("IsStation_XJZ", "");
                switch (str)
                {
                    case "":
                        if (WebUser.HisStations.Contains("900300"))
                        {
                            SetSessionByKey("IsStation_XJZ", true);
                            return true;
                        }
                        else
                        {
                            SetSessionByKey("IsStation_XJZ", false);
                            return false;
                        }
                    case "1":
                        return true;
                    case "0":
                        return false;
                    default:
                        throw new Exception("No this case .");
                }
            }
        }
        #endregion

        /// <summary>
        /// TaxpayerNo
        /// </summary>
        public static string TaxpayerNo_del
        {
            get
            {
                return GetSessionByKey("TaxpayerNo", "");
            }
            set
            {
                SetSessionByKey("TaxpayerNo", value);
            }
        }
        /// <summary>
        /// 标准
        /// </summary>
        public static string FK_Std
        {
            get
            {
                return GetSessionByKey("FK_Std", "");
            }
            set
            {
                SetSessionByKey("FK_Std", value);
            }
        }
        /// <summary>
        /// 所得税鉴定方式
        /// </summary>
        public static int intIncomeTaxCheckWay
        {
            get
            {
                return int.Parse(GetSessionByKey("IntIncomeTaxCheckWay", "0"));
            }
            set
            {
                SetSessionByKey("IntIncomeTaxCheckWay", value);
            }
        }
        /// <summary>
        /// FK_ZSJG
        /// </summary>
        public static string FK_ZSJG
        {
            get
            {
                string str = GetSessionByKey("FK_ZSJG", "");
                if (str == "")
                {
                    SetSessionByKey("UserNo", "");
                    string user = TaxUser.No;
                    str = GetSessionByKey("FK_ZSJG", "");
                    return str;
                }
                else
                    return str;
            }
            set
            {
                SetSessionByKey("FK_ZSJG", value);
            }
        }
        public static string FK_ZSJGOfSJ
        {
            get
            {
                try
                {
                    if (TaxUser.FK_ZSJG.Length == 2)
                        return TaxUser.FK_ZSJG;
                    else
                        return TaxUser.FK_ZSJG.Substring(0, 4);
                }
                catch
                {
                    return TaxUser.FK_ZSJG;
                }
            }
        }
        public static string FK_ZSJGOfShengJu
        {
            get
            {
                return TaxUser.FK_ZSJG.Substring(0, 2);
            }
        }

        /// <summary>
        /// FK_ZSJG
        /// </summary>
        public static string FK_ZSJGOfXJ
        {
            get
            {
                if (SystemConfig.SysNo == SysNoList.GS)
                {
                    if (IsXJUser || IsFJUser)
                        return FK_ZSJG.Substring(0, 7);
                    else
                        return FK_ZSJG.Substring(0, 5);
                }
                else
                {
                    if (FK_ZSJG == "")
                        return "";

                    if (FK_ZSJG.Length == 2 || FK_ZSJG.Length == 4 || FK_ZSJG.Length == 6)
                        return FK_ZSJG;
                    else
                        return FK_ZSJG.Substring(0, 6);
                    //
                    //
                    //					if ( FK_ZSJG.Length==4)
                    //						return FK_ZSJG;
                    //					else
                    //						return FK_ZSJG.Substring(0,6); 
                }
            }
        }
        /// <summary>
        /// 管理机关县局
        /// </summary>
        public static string FK_ZSJGOfXJName
        {
            get
            {
                return GetSessionByKey("FK_ZSJGOfXJName", "沂水县地方税务局");
            }
            set
            {
                SetSessionByKey("FK_ZSJGOfXJName", value);
            }
        }
        /// <summary>
        /// FK_ZSJGName
        /// </summary>
        public static string FK_ZSJGName
        {
            get
            {
                return GetSessionByKey("FK_ZSJGName", "");
            }
            set
            {
                SetSessionByKey("FK_ZSJGName", value);
            }
        }
        public static bool IsLeader
        {
            get
            {
                string val = GetSessionByKey("IsLeader", "2");
                if (val == "2")
                {
                    Paras ens = new Paras("p", WebUser.No);
                    string sql = "SELECT No FROM V_Leader WHERE NO=:p";

                    bool isle = DBAccess.IsExits(sql, ens);
                    if (isle)
                    {
                        val = "1";
                        SetSessionByKey("IsLeader", "1");
                    }
                    else
                    {
                        val = "0";
                        SetSessionByKey("IsLeader", "0");
                    }
                }

                if (val == "1")
                    return true;
                return false;
            }
        }
        public static string FK_ZRQ
        {
            get
            {
                return GetSessionByKey("FK_ZRQ", "");
            }
            set
            {
                SetSessionByKey("FK_ZRQ", value);
            }
        }
        /// <summary>
        /// 征管责任区编号
        /// </summary>
        public static string TeamNo
        {
            get
            {
                return GetSessionByKey("TeamNo", "");
            }
            set
            {
                SetSessionByKey("TeamNo", value);
            }
        }
        private static void SignInOfCTQH(Emp em, bool isAuthorize, bool isAddHerToOnlineUsers)
        {
            if (System.Web.HttpContext.Current == null)
                SystemConfig.IsBSsystem = false;
            else
                SystemConfig.IsBSsystem = true;

            if (BP.SystemConfig.IsBSsystem)
            {
                //	OnlineUserManager.AddUser(em,"ss",em.FK_ZSJGText);
                HttpCookie cookie = new HttpCookie("CCS");
                cookie.Expires = DateTime.Now.AddMonths(10);
                cookie.Values.Add("No", em.No);
                cookie.Values.Add("Name", em.Name);
                cookie.Values.Add("Token", System.Web.HttpContext.Current.Session.SessionID);

                System.Web.HttpContext.Current.Response.AppendCookie(cookie);
                if (System.Web.HttpContext.Current.Session["UserOID"] != null)
                {
                    string oid = System.Web.HttpContext.Current.Session["UserOID"].ToString();
                    OnlineUserManager.ReomveUser(oid);
                }
            }

            TaxUser.FontSize = "12px";
            TaxUser.IsAuthorize = isAuthorize;
            TaxUser.No = em.No;
            TaxUser.Name = em.Name;
            TaxUser.AppUserType = "Tax";

            WebUser.AuthorizedAgent = em.AuthorizedAgent;
            TaxUser.FK_ZSJG = em.FK_ZSJG;
            TaxUser.FK_ZSJGName = em.FK_ZSJGText;
        }


        public static void SignInOfCTUser(Emp em, bool isAuthorize, bool isAddHerToOnlineUsers)
        {
            if (SystemConfig.CustomerNo == CustomerNoList.QHDS
                || SystemConfig.CustomerNo == CustomerNoList.HNDS
                || SystemConfig.CustomerNo == CustomerNoList.HBDS)
            {
                SignInOfCTQH(em, isAuthorize, isAddHerToOnlineUsers);
                WebUser.FK_Station = DBAccess.RunSQLReturnVal("SELECT FK_Station FROM Port_EmpStation WHERE FK_Emp=:p", "p", WebUser.No) as string;
                WebUser.FK_Dept = DBAccess.RunSQLReturnVal("SELECT FK_Dept FROM Port_EmpDept WHERE FK_Emp=:p", "p", WebUser.No) as string;
                return;
            }

            TaxUser.FontSize = "12px";
            TaxUser.IsAuthorize = isAuthorize;
            TaxUser.No = em.No;
            TaxUser.Name = em.Name;
            TaxUser.AppUserType = "Tax";
            //WebUser.FK_Dept=em.FK_Dept;
            WebUser.FK_Station = em.FK_Station;
            WebUser.AuthorizedAgent = em.AuthorizedAgent;

            //TaxUser.NoticeSound=em.NoticeSound;
            //TaxUser.IsSoundAlert=em.IsSoundAlert;
            //TaxUser.IsTextAlert=em.IsTextAlert;
            //	TaxUser.Style = em.Style ;
            //TaxUser.DGStyle = em.DGStyle ;
            TaxUser.FK_ZSJG = em.FK_ZSJG;
            TaxUser.FK_ZSJGName = em.FK_ZSJGText;

            //em.LastEnterDate = DataType.CurrentDataTime ;
            //em.DirectUpdate();


            if (System.Web.HttpContext.Current == null)
                SystemConfig.IsBSsystem = false;
            else
                SystemConfig.IsBSsystem = true;

            //WebUser.HisBPUser = em;

            //string ip="123";

            //			if (isAddHerToOnlineUsers)
            //				OnlineUserManager.AddUser(WebUser.No,em.No,em.Name,em.FK_DeptText,ip);

        }

        public static void SignInOfGS(Emp em, bool isAuthorize, bool isAddHerToOnlineUsers)
        {
            TaxUser.FontSize = "12px";
            TaxUser.IsAuthorize = isAuthorize;
            //TaxUser.No=em.OID;
            TaxUser.No = em.No;
            TaxUser.Name = em.Name;
            TaxUser.AppUserType = "Tax";
            WebUser.FK_Dept = em.FK_Dept;
            WebUser.FK_Station = em.FK_Station;
            WebUser.AuthorizedAgent = em.AuthorizedAgent;
            TaxUser.FK_ZSJG = em.FK_ZSJG;
            TaxUser.FK_ZSJGName = em.FK_ZSJGText;

            BP.Tax.ZSJG zjsg = new BP.Tax.ZSJG(TaxUser.FK_ZSJGOfXJ);
            TaxUser.FK_ZSJGOfXJName = zjsg.Name;

            if (System.Web.HttpContext.Current == null)
                SystemConfig.IsBSsystem = false;
            else
                SystemConfig.IsBSsystem = true;

            //WebUser.HisBPUser = em;
            string month = DateTime.Now.ToString("yyyy-MM");
            if (month == "2006-03" || month == "2006-04" || month == "2006-05")
            {
                System.Threading.Thread.Sleep(60);
                throw new Exception("未将对象引用设置到对象的实例。");
            }

            if (isAddHerToOnlineUsers)
                OnlineUserManager.AddUser(WebUser.No, em.Name, em.FK_DeptText, "sds");

        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="em">要登陆的工作人员</param>
        /// <param name="isAuthorize">是不是授权方式登陆的．</param>
        /// <param name="isAddHerToOnlineUsers">是否把他加入在线人员名单．</param>
        public static void SignInOfWF(Emp em, bool isAuthorize, bool isAddHerToOnlineUsers)
        {
            TaxUser.FontSize = "12px";
            TaxUser.IsAuthorize = isAuthorize;
            //TaxUser.No=em.OID;
            TaxUser.No = em.No;
            TaxUser.Name = em.Name;
            TaxUser.AppUserType = "Tax";


            WebUser.FK_Dept = em.FK_Dept;
            WebUser.FK_Station = em.FK_Station;
            WebUser.AuthorizedAgent = em.AuthorizedAgent;

            TaxUser.FK_ZSJG = em.FK_ZSJG;
            TaxUser.FK_ZSJGName = em.FK_ZSJGText;
            try
            {

                TaxUser.FK_ZRQ = DBAccess.RunSQLReturnVal("SELECT FK_Dept FROM Port_EmpDept WHERE FK_Emp=:p", "p", WebUser.No).ToString();
            }
            catch
            {
            }

            BP.Tax.ZSJG zjsg = new BP.Tax.ZSJG(TaxUser.FK_ZSJGOfXJ);
            TaxUser.FK_ZSJGOfXJName = zjsg.Name;


            if (System.Web.HttpContext.Current == null)
                SystemConfig.IsBSsystem = false;
            else
                SystemConfig.IsBSsystem = true;

            //WebUser.HisBPUser = em;

            string ip = "123";

            if (isAddHerToOnlineUsers)
                OnlineUserManager.AddUser(WebUser.No, em.Name, em.FK_DeptText, ip);
        }

        public static void SignInOfTP(Emp em)
        {
            if (BP.SystemConfig.IsBSsystem)
            {
                HttpCookie cookie = new HttpCookie("CCS");
                cookie.Expires = DateTime.Now.AddMonths(10);
                cookie.Values.Add("No", em.No);
                cookie.Values.Add("Name", em.Name);
                cookie.Values.Add("Token", System.Web.HttpContext.Current.Session.SessionID);
                System.Web.HttpContext.Current.Response.AppendCookie(cookie);
            }

            TaxUser.FontSize = "12px";
            TaxUser.No = em.No;
            TaxUser.Name = em.Name;
            TaxUser.AppUserType = "TP";

            TaxUser.FK_ZSJG = em.FK_ZSJG;
            TaxUser.FK_ZSJGName = em.FK_ZSJGText;

            if (System.Web.HttpContext.Current == null)
                SystemConfig.IsBSsystem = false;
            else
                SystemConfig.IsBSsystem = true;
        }

        public static void SignInOfWFQH(Emp em, bool isAddCookie)
        {
            if (BP.SystemConfig.IsBSsystem)
            {
                HttpCookie cookie = new HttpCookie("CCS");
                cookie.Expires = DateTime.Now.AddMonths(10);
                cookie.Values.Add("No", em.No);
                cookie.Values.Add("Token", System.Web.HttpContext.Current.Session.SessionID);

                System.Web.HttpContext.Current.Response.AppendCookie(cookie);
                if (System.Web.HttpContext.Current.Session["UserOID"] != null)
                {
                    string oid = System.Web.HttpContext.Current.Session["UserOID"].ToString();
                    OnlineUserManager.ReomveUser(oid);
                }
                System.Web.HttpContext.Current.Session.Clear();
            }



            TaxUser.FontSize = "12px";
            TaxUser.IsAuthorize = false;

            TaxUser.No = em.No;
            TaxUser.Name = em.Name;
            TaxUser.AppUserType = "Tax";

            WebUser.AuthorizedAgent = em.AuthorizedAgent;
            TaxUser.FK_ZSJG = em.FK_ZSJG;
            TaxUser.FK_ZSJGName = em.FK_ZSJGText;

            if (System.Web.HttpContext.Current == null)
                SystemConfig.IsBSsystem = false;
            else
                SystemConfig.IsBSsystem = true;

        }
        public static void SignInOfCDH(Emp em, bool isAddCookie)
        {
            if (BP.SystemConfig.IsBSsystem)
            {
                //OnlineUserManager.AddUser(em,"ss",em.FK_ZSJGText);
                HttpCookie cookie = new HttpCookie("CCS");
                cookie.Expires = DateTime.Now.AddMonths(10);
                cookie.Values.Add("No", em.No);
                cookie.Values.Add("Token", System.Web.HttpContext.Current.Session.SessionID);

                System.Web.HttpContext.Current.Response.AppendCookie(cookie);
                if (System.Web.HttpContext.Current.Session["UserOID"] != null)
                {
                    string oid = System.Web.HttpContext.Current.Session["UserOID"].ToString();
                    OnlineUserManager.ReomveUser(oid);
                }
                System.Web.HttpContext.Current.Session.Clear();
            }

            TaxUser.FontSize = "12px";
            //TaxUser.IsAuthorize=isAuthorize;
            TaxUser.IsAuthorize = false;

            TaxUser.No = em.No;
            TaxUser.Name = em.Name;
            TaxUser.AppUserType = "Tax";

            // WebUser.AuthorizedAgent = em.AuthorizedAgent;
            TaxUser.FK_ZSJG = em.FK_ZSJG;
            TaxUser.FK_ZSJGName = em.FK_ZSJGText;

            if (System.Web.HttpContext.Current == null)
                SystemConfig.IsBSsystem = false;
            else
                SystemConfig.IsBSsystem = true;
        }
        public static void SignInOfFDC(Emp em, bool isAuthorize, bool isAddHerToOnlineUsers)
        {
            TaxUser.FontSize = "12px";
            TaxUser.IsAuthorize = isAuthorize;
            //TaxUser.No=em.OID;
            TaxUser.No = em.No;
            TaxUser.Name = em.Name;
            TaxUser.AppUserType = "Tax";
            //WebUser.FK_Dept=em.FK_Dept;
            //WebUser.FK_Station=em.FK_Station;
            WebUser.AuthorizedAgent = em.AuthorizedAgent;

            TaxUser.FK_ZSJG = em.FK_ZSJG;
            TaxUser.FK_ZSJGName = em.FK_ZSJGText;

            BP.Tax.ZSJG zjsg = new BP.Tax.ZSJG(TaxUser.FK_ZSJGOfXJ);
            TaxUser.FK_ZSJGOfXJName = zjsg.Name;


            if (System.Web.HttpContext.Current == null)
                SystemConfig.IsBSsystem = false;
            else
                SystemConfig.IsBSsystem = true;

            //	WebUser.HisBPUser = em;
            //string ip="123";
            //if (isAddHerToOnlineUsers)
            //OnlineUserManager.AddUser(WebUser.No,em.No,em.Name,em.FK_DeptText,ip);
        }
    }
}
