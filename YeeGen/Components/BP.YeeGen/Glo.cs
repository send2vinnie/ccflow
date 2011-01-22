using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net.Mail;
using BP.DA;

namespace BP.YG
{
    public class Glo
    {
        #region 公共操作方法
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailAddr"></param>
        /// <param name="title"></param>
        /// <param name="doc"></param>
        public static void SendMail(string mailAddr, string title, string doc)
        {
            System.Net.Mail.MailMessage myEmail = new System.Net.Mail.MailMessage();
            myEmail.From = new MailAddress("ccflow.cn@gmail.com", "ccflow", System.Text.Encoding.UTF8);
            // myEmail.From = new MailAddress("pengzhou86@gmail.com", "public", System.Text.Encoding.UTF8);

            myEmail.To.Add(mailAddr);
            myEmail.Subject = title;
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码


            myEmail.Body = doc;
            myEmail.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
            myEmail.IsBodyHtml = true;//是否是HTML邮件

            myEmail.Priority = MailPriority.High;//邮件优先级

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("ccflow.cn@gmail.com", "www.ccflow.cn");

            //上述写你的GMail邮箱和密码
            client.Port = 587;//Gmail使用的端口
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;//经过ssl加密
            object userState = myEmail;
            try
            {
                //简单一点儿可以client.Send(msg);
                // MessageBox.Show("发送成功");
                client.SendAsync(myEmail, userState);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }
        }
        #endregion

        public static string PathFDBWordFile
        {
            get
            {
                return "D:\\CaiShui800\\CaiShui\\FDB\\EnFile\\BP.BK.Word\\";
            }
        }
        #region 分值操作

        /// <summary>
        /// 是否交易了
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="type"></param>
        /// <param name="refObj"></param>
        /// <returns></returns>
        public static bool IsTrade(string type, string refObj, string userNo)
        {
            if (BP.DA.DBAccess.RunSQLReturnValInt("select count(cent) from YG_CBuess where YG_CBuess.Fk_Member='" + userNo + "' and fk_cbuess='" + type + "' and refobj='" + refObj + "'") >= 1)
                return true;
            return false;
        }
        public static bool IsTrade(string type, object refObj)
        {
            return IsTrade(type, refObj.ToString(), BP.YG.Glo.MemberNo);
        }
        /// <summary>
        /// 分值操作
        /// </summary>
        /// <param name="fK_type"></param>
        /// <param name="refobj"></param>
        /// <param name="note"></param>
        /// <param name="cent"></param>
        public static void Trade(string fK_type, object refobj, string note, int cent)
        {

            if (BP.YG.Glo.MemberNo == null)
                throw new Exception("您的登陆时间太长，或者没有登陆，交易无法执行。");

            CBuessType type = new CBuessType(fK_type);
            CBuess cb = new CBuess();
            cb.Cent = type.Cent + cent;
            cb.Note = note;
            cb.FK_Member = Glo.MemberNo;
            cb.FK_CBuess = fK_type;
            cb.RefObj = refobj.ToString();
            cb.RDT = DataType.CurrentDataTime;
            cb.Insert();
        }
        /// <summary>
        /// 初始化分
        /// </summary>
        /// <param name="flag">操作类型</param>
        /// <param name="note">备注</param>
        public static void Trade(string fK_type, object refobj, string note)
        {
            CBuessType type = new CBuessType(fK_type);
            CBuess cb = new CBuess();
            cb.Cent = type.Cent;
            cb.Note = note;
            cb.FK_Member = Glo.MemberNo;
            cb.FK_CBuess = fK_type;
            cb.RefObj = refobj.ToString();
            cb.RDT = DataType.CurrentDataTime;
            cb.Insert();
            Member c = new Member(Glo.MemberNo);
            c.Cent = c.Cent + type.Cent;
            c.Update();
        }
        public static void TradeAdmin(string fk_c, string fK_type, object refobj, string note)
        {
            CBuessType type = new CBuessType(fK_type);
            CBuess cb = new CBuess();
            cb.Cent = type.Cent;
            cb.Note = note;
            cb.FK_Member = fk_c;
            cb.FK_CBuess = fK_type;
            cb.RefObj = refobj.ToString();
            cb.RDT = DataType.CurrentDataTime;

            cb.Insert();
            Member c = new Member(Glo.MemberNo);
            c.Cent = c.Cent + type.Cent;
            c.Update();
        }
        public static void Trade(string fK_type, object refobj)
        {
            Glo.Trade(fK_type, refobj, "");
        }
        #endregion

        #region 基本属性
        public static string PathOfApp
        {
            get
            {
                return System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
            }
        }
        public static string PathData
        {
            get
            {
                return Glo.PathOfApp + "\\Data\\";
            }
        }
        public static string PathTemp
        {
            get
            {
                return Glo.PathOfApp + "\\Temp\\";
            }
        }
        public static string PathFDB
        {
            get
            {
                return "D:\\CaiShui800\\FDB\\";
            }
        }
        public static string PathFDBSource
        {
            get
            {
                return "D:\\CaiShui800\\FDB\\FDBSource\\";
            }
        }
        public static string PathFDBDoc
        {
            get
            {
                return "D:\\CaiShui800\\FDB\\FDBDoc\\";
            }
        }
        public static string PathFDBShareFile
        {
            get
            {
                return "D:\\CaiShui800\\FDB\\ShareFile\\";
            }
        }
        public static string PathFDBPostFile
        {
            get
            {
                return "D:\\CaiShui800\\FDB\\PostFile\\";
            }
        }
        public static string PathFDBFAQFile
        {
            get
            {
                return "D:\\CaiShui800\\FDB\\FAQFile\\";
            }
        }
        /// <summary>
        /// 专家
        /// </summary>
        public static string PathFDBZJT
        {
            get
            {
                return Glo.PathFDB + "ZJT\\";
            }
        }
        public static string PathFDBCAlbum
        {
            get
            {
                return Glo.PathFDB + "CAlbum\\";
            }
        }
        public static string PathFDBFileUser
        {
            get
            {
                return Glo.PathFDB + "FileUser\\";
            }
        }
        /// <summary>
        /// PathFDB_HR_User
        /// </summary>
        public static string PathFDB_HR_User
        {
            get
            {
                return Glo.PathFDB + "HR\\User\\";
            }
        }
        public static string PathFDB_HR_YPage
        {
            get
            {
                return Glo.PathFDB + "YPage\\";
            }
        }
        /// <summary>
        /// PathFDBBureau_EmpPhoto
        /// </summary>
        public static string PathFDBBureau_EmpPhoto
        {
            get
            {
                return Glo.PathFDB + "Bureau\\" + Glo.BureauNo + "\\EmpPhoto\\";
            }
        }
        public static string PathBureauTemplate
        {
            get
            {
                return Glo.PathData + "TemplateFile\\";
            }
        }
        public static string PathFDBBureau_ZT
        {
            get
            {
                return Glo.PathFDB + "Bureau\\" + Glo.BureauNo + "\\ZT\\";
            }
        }
        public static string PathFDBBureau
        {
            get
            {
                return Glo.PathFDB + "Bureau\\" + Glo.BureauNo + "\\";
            }
        }
        public static string PathFDBBureau_Model
        {
            get
            {
                return Glo.PathFDB + "Bureau\\" + Glo.BureauNo + "\\Model\\";
            }
        }
        public static string PathFDBBureau_News
        {
            get
            {
                return Glo.PathFDB + "Bureau\\" + Glo.BureauNo + "\\News\\";
            }
        }

        public static string PathFDBBureau_Down
        {
            get
            {
                return Glo.PathFDB + "Bureau\\" + Glo.BureauNo + "\\Down\\";
            }
        }
        public static string PathFDBBureau_Placard
        {
            get
            {
                return Glo.PathFDB + "Bureau\\" + Glo.BureauNo + "\\Placard\\";
            }
        }
        public static string PathFDBBureau_XJDW
        {
            get
            {
                return Glo.PathFDB + "Bureau\\" + Glo.BureauNo + "\\XJDW\\";
            }
        }
        #endregion

        #region hxcsr models
        public static string CurrModel
        {
            get
            {
                string url = System.Web.HttpContext.Current.Request.RawUrl;
                string url1 = url.Substring(url.IndexOf(".aspx"));
                url1 = url.Replace(url1, "");
                url1 = url1.Substring(url1.LastIndexOf("/") + 1);
                return url1;
            }
        }
       
        #endregion

        #region 属性
        public static Member Member
        {
            get
            {
                return new Member(Glo.MemberNo);
            }
        }
        public static string SearchKey
        {
            get
            {
                object val = System.Web.HttpContext.Current.Session["SearchKey"];
                if (val == null)
                    return null;
                else
                    return val.ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["SearchKey"] = value;
            }
        }
        public static string FK_SF
        {
            get
            {
                object val = System.Web.HttpContext.Current.Session["FK_SF"];
                if (val == null)
                    return null;
                else
                    return val.ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["FK_SF"] = value;
            }
        }
        public static string GetRefObj(string key)
        {
            object val = System.Web.HttpContext.Current.Session[key];
            if (val == null)
                return null;
            else
                return val.ToString();
        }
        public static void SetRefObj(string key, object val)
        {
            System.Web.HttpContext.Current.Session[key] = val;
        }
        public static string FK_City
        {
            get
            {
                object val = System.Web.HttpContext.Current.Session["FK_City"];
                if (val == null)
                    return null;
                else
                    return val.ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["FK_City"] = value;
            }
        }
        public static string FK_Area
        {
            get
            {
                object val = System.Web.HttpContext.Current.Session["FK_Area"];
                if (val == null)
                    return null;
                else
                    return val.ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["FK_Area"] = value;
            }
        }


        public static string SearchTypes
        {
            get
            {
                object val = System.Web.HttpContext.Current.Session["SearchTypes"];
                if (val == null)
                    return "'Doc'";
                else
                    return val.ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["SearchTypes"] = value;
            }
        }
        public static bool IsLogin
        {
            get
            {
                if (Glo.MemberNo == null)
                    return false;
                return true;
            }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public static string MemberNo
        {
            get
            {
                string val = System.Web.HttpContext.Current.Session["MemberNo"] as string;
                if (val == null)
                {
                    HttpCookie hc1 = System.Web.HttpContext.Current.Request.Cookies["caishui800"];
                    if (hc1 == null)
                        return null;

                    string userNo = hc1.Values["UserNo"];
                    if (userNo == null)
                        return null;

                    string isonline = hc1.Values["Online"];
                    if (isonline == "0" || isonline == null)
                        return null;


                    Member c = new Member(userNo);
                    Glo.Signin(c, true);
                    return c.No;
                }
                else
                    return val.ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["MemberNo"] = value;
            }
        }
        public static void DoExit()
        {
            System.Web.HttpContext.Current.Session.Clear();
            HttpCookie ck = System.Web.HttpContext.Current.Request.Cookies["caishui800"];
            ck.Values["Online"] = "0";
            System.Web.HttpContext.Current.Response.Cookies.Add(ck);

            //ck.Domain="caishui114.com";
            //.Values["Online"] = "0";
            //System.Web.HttpContext.Current.Request.Cookies["caishui800"].Domain  = "";
            //System.Web.HttpContext.Current.Request.Cookies["caishui800"].Expires = DateTime.Now.AddHours(-24);

            //HttpCookieMyCo = HttpContext.Current.Request.Cookies["caishui800"];
            //if (HttpContext.Current.Request.ServerVariables["Http_Host"].IndexOf("abc.com") >= 0)
            //{
            //    MyCo.Domain = "abc.com";
            //}
            //MyCo.Expires = DateTime.Now.AddHours(-24);
            //Response.Cookies.Add(MyCo);
        }
        /// <summary>
        /// Token
        /// </summary>
        public static string Token
        {
            get
            {
                string val = System.Web.HttpContext.Current.Session["Token"].ToString();
                if (val == null)
                {
                    //val= System.Web.HttpContext.Current.Session["Token"];
                }
                return val;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string LostSessionMsg(string url)
        {
            string msg = "";
            msg += "&nbsp;&nbsp;<a href='/RegUser.aspx?WhereGo=" + url + "' >注册(20秒内完成)</a>]或[<a href='/Login.aspx?WhereGo=" + url + "' >登陆</a>]，在您登陆或注册完毕后系统会自动转到此页面上来。";
            msg += "<BR><BR>";
            msg += "&nbsp;&nbsp;关于本网站：";
            msg += "&nbsp;&nbsp;<BR>1、本网站以积分制,保持可持续的、良性的发展。";
            msg += "&nbsp;&nbsp;<BR>2、获取积分的方法途径是回答别人的问题，共享自己的文件发表文章等等。";
            msg += "&nbsp;&nbsp;<BR>3、在您帮助别人的同时，您会获得本网站的积分回报。";
            msg += "&nbsp;&nbsp;<BR>4、有了这些积分您就可以浏览更多的文件资源和获取更多的其它网友的帮助。";
            msg += "&nbsp;&nbsp;<BR><BR>感谢您支持caishui800.cn";
            return msg;
        }
        public static void AutoLogin()
        {
            if (BP.YG.Glo.MemberNo == null)
            {

                HttpCookie hc = System.Web.HttpContext.Current.Request.Cookies["caishui800"];
                if (hc != null)
                {
                    string isauto = hc.Values["IsRememberMe"];
                    if (isauto == "0")
                        return;

                    string no = hc.Values["UserNo"];
                    BP.YG.Member c = new Member(no);
                    Glo.Signin(c, true);
                }
            }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public static string MemberName
        {
            get
            {
                object val = System.Web.HttpContext.Current.Session["MemberName"];
                if (val == null)
                    return null;
                else
                    return val.ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["MemberName"] = value;
            }
        }
        /// <summary>
        /// 消息
        /// </summary>
        public static string Msg
        {
            get
            {
                object val = System.Web.HttpContext.Current.Session["Msg"];
                if (val == null)
                    return null;
                else
                    return val.ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["Msg"] = value;
            }
        }

        public static string MsgOfReLogin
        {
            get
            {
                object val = System.Web.HttpContext.Current.Session["MsgOfReLogin"];
                if (val == null)
                    return null;
                else
                    return val.ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["MsgOfReLogin"] = value;
            }
        }
        public static string GoWhere
        {
            get
            {
                object val = System.Web.HttpContext.Current.Session["GoWhere"];
                if (val == null)
                    return null;
                else
                    return val.ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["GoWhere"] = value;
            }
        }
        public static string MemberICON
        {
            get
            {
                object val = System.Web.HttpContext.Current.Session["ICON"];
                if (val == null)
                    return null;
                else
                    return val.ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["ICON"] = value;
            }
        }
        /// <summary>
        /// 当前部门类型
        /// </summary>
        public static string CurrBureauType
        {
            get
            {
                return null;
                //if (Glo.BureauNo.ToLower() == "hxcsr" || Glo.BureauNo.ToLower() == "CaiShui")
                //    return BP.YG.BureauType.HXCSR;
                //else
                //    return BP.YG.BureauType.caishui800;
            }
        }
        #endregion

        #region 共用方法
        public static string GenerMemberStr(string usr)
        {
            return "<a href='/Space/Home.aspx?FK_Member=" + usr + "' target=_blank >" + usr + "</a>";
        }
        public static string GenerMemberStr(string usr, string lab)
        {
            return "<a href='/Space/Home.aspx?FK_Member=" + usr + "' target=_blank >" + lab + "</a>";
        }
        /// <summary>
        /// 给用户加分
        /// </summary>
        /// <param name="cent"></param>
        public static void MemberAddCent_del(int cent)
        {
            //Glo.MemberAddCent(Glo.MemberNo,cent);
        }
        /// <summary>
        /// 给用户加分
        /// </summary>
        /// <param name="no"></param>
        /// <param name="cent"></param>
        public static void MemberAddCent_del(string no, int cent)
        {
            Member c = new Member(no);
            c.Cent = c.Cent + cent;
            c.Update();
            //DBAccess.RunSQL("UPDATE YG_Member SET CENT");
        }
        public static void Signin(Member c, bool isRememberMe)
        {
            Glo.MemberNo = c.No;
            Glo.MemberName = c.Name;

            try
            {
                Glo.FK_Area = c.FK_Area;
                Glo.FK_SF = c.FK_Area.Substring(0, 2);
                Glo.FK_City = c.FK_Area.Substring(0, 4);
            }
            catch
            {

            }


            HttpCookie cookie = new HttpCookie("caishui800");
            cookie.Expires = DateTime.Now.AddMonths(10);
            cookie.Values.Add("UserNo", c.No);
            cookie.Values.Add("UserName", c.Name);
            if (isRememberMe)
                cookie.Values.Add("IsRememberMe", "1");
            else
                cookie.Values.Add("IsRememberMe", "0");

            cookie.Values.Add("Online", "1");
            try
            {
                Glo.Trade(CBuessType.CZ_Login, System.DateTime.Now.ToString("yy-MM-dd HH"), "用户登陆");
            }
            catch
            {
            }

            System.Web.HttpContext.Current.Response.AppendCookie(cookie);
        }
        #endregion

        #region 共用方法
        public static bool MemberAddFriend(string f)
        {
            Member b = Glo.Member;
            if (b.Friends.IndexOf("@" + f) == -1)
            {
                b.Friends = b.Friends + "@" + f;
                b.Update();
            }
            return true;
        }
        public static void MemberRemoveFriend(string f)
        {
            Member b = Glo.Member;
            if (b.Friends.IndexOf("@" + f) == -1)
            {
            }
            else
            {
                b.Friends = b.Friends.Replace("@" + f, "");
                b.Update();
            }
        }
        public static void HomeModelOfAdd(string fk_model)
        {
            //Bureau b = Glo.CurrBureau;
            //if (b.HomeModels.IndexOf("@" + fk_model) == -1)
            //{
            //    b.HomeModels = b.HomeModels + "@" + fk_model;
            //    b.Update();
            //}

        }
        public static void HomeModelOfRemove(string fk_model)
        {
            //Bureau b = Glo.CurrBureau;
            //if (b.HomeModels.IndexOf("@" + fk_model) == -1)
            //{
            //}
            //else
            //{
            //    b.HomeModels = b.HomeModels.Replace("@" + fk_model, "");
            //    b.Update();
            //}
        }
        #endregion


        //public static Bureau CurrBureau
        //{
        //    get
        //    {
        //        return new Bureau(Glo.BureauNo);
        //    }
        //}
        public static string BureauNo
        {
            get
            {
                return "hxcsr";

                /*

                string B = System.Web.HttpContext.Current.Request.QueryString["B"];
                if (B == null)
                {
                    B = System.Web.HttpContext.Current.Items["B"] as string;
                    if (B == null)
                    {
                        string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                        try
                        {
                            url = url.ToLower();
                            if (url.IndexOf("url.aspx") != -1)
                                return "hxcsr";
                            else
                                throw new Exception("@没有传递 B ");


                            url = url.Substring(0, url.IndexOf(".caishui800"));
                            url = url.Substring(url.LastIndexOf(".") + 1);
                            if (url == "www" || url == "http://www")
                                return "hxcsr";

                            B = url;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message + url);
                        }
                        System.Web.HttpContext.Current.Items["B"] = B;
                    }
                }
                return B;
                 
                 */

            }
        }
        public static int Style
        {
            get
            {
                return 1;
                //return Glo.CurrBureau.Style;
            }
        }
        public static int PageSize
        {
            get
            {
                // return 10;
                return BP.SystemConfig.PageSize;
            }
        }
        public static string BureauName
        {
            get
            {
                return "易根网";
              //  return Glo.CurrBureau.Name;
            }
        }
        //public static bool IsAdmin
        //{
        //    get
        //    {
               
        //        if (Glo.BureauAdmins.IndexOf(Glo.MemberNo) != -1)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
        //public static string BureauAdmins
        //{
        //    get
        //    {
        //        string msg = "";
        //        string[] strs = Glo.CurrBureau.Admins.Split(',');
        //        foreach (string str in strs)
        //        {
        //            if (str == null || str == "")
        //                continue;

        //            msg += "<a href='/Space/Home.aspx?FK_Member=" + str + "' target=_blank >" + str + "</a><BR>";
        //        }
        //        return msg;
        //    }
        //}
        //public static string BureauTitle
        //{
        //    get
        //    {
        //        return "/FDB/Bureau/" + Glo.BureauNo + "/Title.gif";
        //    }
        //}
        /// <summary>
        /// 当前汇率 购买
        /// </summary>
        public static int HLOfBuy
        {
            get
            {
                return 100;
            }
        }
        /// <summary>
        /// 兑换汇率
        /// </summary>
        public static int HLOfDH
        {
            get
            {
                return 150;
            }
        }
    }
}
