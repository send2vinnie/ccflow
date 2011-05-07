using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.En;
using BP.Port;


namespace BP.Web
{
    /// <summary>
    /// SignInOfRe 的摘要说明。
    /// </summary>
    public partial class SignInOfRe : Page
    {
        public string RawUrl
        {
            get
            {
                return ViewState["RawUrl"] as string;
            }
            set
            {
                ViewState["RawUrl"] = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string script = "<script language=javascript>function setFocus(ctl) {if (document.forms[0][ctl] !=null )  { document.forms[0][ctl].focus(); } } setFocus('" + this.TB_Pass.ClientID + "'); </script>";
            this.RegisterStartupScript("func", script);
            if (this.Request.QueryString["Token"] != null)
            {
                HttpCookie hc1 = this.Request.Cookies["CCS"];
                if (hc1 != null)
                {
                    if (this.Request.QueryString["Token"] == hc1.Values["Token"])
                    {
                        Emp em = new Emp(this.Request.QueryString["No"]);
                        WebUser.SignInOfGenerLang(em, "CH");
                        WebUser.Token = this.Request.QueryString["Token"];
                        Response.Redirect("Home.htm", false);
                        return;
                    }
                }
            }

            if (this.IsPostBack == false)
            {
                this.TB_No.Attributes["background-image"] = "url('beer.gif')";
                HttpCookie hc = this.Request.Cookies["CCS"];
                if (hc != null)
                {
                    this.TB_No.Text = hc.Values["UserNo"];
                }
            }
            if (this.Request.QueryString["IsChangeUser"] != null)
            {

            }

            if (this.Request.Browser.MajorVersion < 6)
            {
                this.Response.Write("对不起，系统检测到您当前使用的IE版本是[" + this.Request.Browser.Version + "]，系统不能在当前的IE上正常工作。想正确的使用此系统，请升级到IE6.0，请点击<a href='../IE6.rar'>这里下载IE6.0</A>。下载后，解开压缩文件，运行 ie6setup.exe，如果有疑问请致电 [" + BP.SystemConfig.ServiceTel + "], 或者发 Mail [" + BP.SystemConfig.ServiceMail + "]。");
                this.Btn1.Enabled = false;
                this.TB_No.Enabled = false;
                this.TB_Pass.Enabled = false;
            }

            HttpCookie cookie = new HttpCookie("CCS");
        }

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN：该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }


        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void Btn1_Click(object sender, System.EventArgs e)
        {
            try
            {
                Emp em = new Emp(this.TB_No.Text);
                //		if (  this.TB_Pass.Text=="test" || SystemConfig.IsDebug || em.CheckPass(this.TB_Pass.Text) ) 
                if (SystemConfig.IsDebug || em.CheckPass(this.TB_Pass.Text))
                {
                    //if (this.Request.QueryString["IsChangeUser"]!=null)
                    /* 如果是更改用户.*/
                    if (this.Session["OID"] != null)
                    {
                        string no = WebUser.No;
                        this.Session.Clear();
                        //OnlineUserManager.ReomveUser( no );
                    }

                    WebUser.SignInOfGenerLang(em, null);
                    WebUser.Token = this.Session.SessionID;
                    string url1 = "../Home.aspx";
                    if (this.RawUrl == null)
                    {
                        Response.Redirect(url1, false);
                    }
                    else
                    {
                        string url = this.RawUrl;
                        if (url.ToLower().IndexOf("signin.aspx") > 0)
                        {
                            Response.Redirect(url1, true);
                            return;
                        }
                        Response.Redirect(url1, true);
                    }
                    return;
                }
                else
                {
                    throw new Exception("@密码错误！@检查是否按下了CapsLock");
                }
            }
            catch (System.Exception ex)
            {
                this.Response.Write("<font color=red ><b>@用户名密码错误!@检查是否按下了CapsLock.@更详细的信息:" + ex.Message + "</b></font>");

            }
        }

    }
}
