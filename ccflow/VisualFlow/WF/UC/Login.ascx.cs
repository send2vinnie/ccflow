using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.Web;
using BP.En;
using BP.DA;
using BP.WF;
using BP.Sys;
using BP.Port;
using BP;

public partial class WF_UC_Login : BP.Web.UC.UCBase3
{
    public string Lang
    {
        get
        {
            string s = this.Request.QueryString["Lang"];
            if (s == null)
                return WebUser.SysLang;
            return s;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.Browser.Cookies == false)
        {
            this.Alert("您的浏览器不支持cookies功能，无法使用改系统。");
            return;
        }

        WebUser.SysLang = this.Lang;
        Response.AddHeader("P3P", "CP=CAO PSA OUR");
        int colspan = 1;

        this.AddTable("width='500px'");
        this.AddTR();
        this.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.AddTREnd();

        this.AddTR();
        this.Add("<TD class=BigDoc align=left colspan=" + colspan + "><img src='./Img/Login.gif' > <b>" + this.ToE("Login", "系统登陆") + "</b> " + this.ToE("PlsChoseLang", "请选择系统支持的语言") + "</TD>");
        this.AddTREnd();

        this.AddTR();
        this.Add("<TD align=center >");

        BP.WF.XML.Langs langs = new BP.WF.XML.Langs();
        langs.RetrieveAll();

        foreach (BP.WF.XML.Lang lang in langs)
        {
            this.Add("<a href='Login.aspx?Lang=" + lang.No + "'>" + lang.Name + "</a> &nbsp;&nbsp;");
        }

        this.AddTDEnd();
        this.AddTREnd();

        this.AddTR();
        this.Add("<TD align=center >");

        this.AddTable("border=1px align=center ");
        //this.AddTRSum();
        //this.AddTD();
        //this.AddTD();
        //this.AddTREnd();

        this.AddTR();
        this.AddTD(this.ToE("UserName", "用户名："));

        TextBox tb = new TextBox();
        tb.ID = "TB_User";
        tb.Text = BP.Web.WebUser.No;
        tb.Columns = 20;
        //   tb.Attributes["class"] = "TB";

        this.AddTD(tb);
        this.AddTREnd();


        this.AddTR();
        this.AddTD(this.ToE("PassWord", "密&nbsp;&nbsp;码："));
        tb = new TextBox();
        tb.ID = "TB_Pass";
        tb.TextMode = TextBoxMode.Password;
        // tb.Attributes["class"] = "TB";
        tb.Columns = 22;
        this.AddTD(tb);
        this.AddTREnd();


        this.AddTRSum();
        this.AddTDBegin("colspan=3 align=center");
        Button btn = new Button();

        btn.Text = this.ToE("Login", "登 陆");

        btn.Click += new EventHandler(btn_Click);
        this.Add(btn);

        if (WebUser.No != null)
        {
            string home = "";
            if (WebUser.IsWap)
                home = "-<a href='Home.aspx'>" + this.ToE("Home", "主页") + "</a>";

            if (WebUser.IsAuthorize)
                this.Add(" - <a href=\"javascript:ExitAuth('" + WebUser.Auth + "')\" >退出授权模式[" + WebUser.Auth + "]</a>" + home);
            else
                this.Add(" - <a href='Tools.aspx?RefNo=AutoLog' >" + this.ToE("AutoLog", "授权方式登录") + "</a>" + home);
        }

        this.AddTDEnd();

        this.AddTREnd();

        this.AddTableEnd();

        this.AddBR();
        this.AddBR();


        this.AddTDEnd();
        this.AddTREnd();
        this.AddTableEnd();
    }
    public string ToWhere
    {
        get
        {
            if (this.Request.QueryString["ToWhere"] == null)
            {
                if (this.Request.RawUrl.ToLower().Contains("small"))
                    return "EmpWorksSmall.aspx";
                else
                    return "EmpWorks.aspx";
            }
            else
            {
                return this.Request.QueryString["ToWhere"];
            }
        }
    }
    void btn_Click(object sender, EventArgs e)
    {
        string user = this.GetTextBoxByID("TB_User").Text;
        string pass = this.GetTextBoxByID("TB_Pass").Text;
        try
        {
            Emp em = new Emp(user);
            if (em.CheckPass(pass))
            {
                WebUser.SignInOfGenerLang(em, this.Lang);

                if (this.Request.RawUrl.ToLower().Contains("wap"))
                    WebUser.IsWap = true;
                else
                    WebUser.IsWap = false;

                WebUser.Token = this.Session.SessionID;
                if (WebUser.IsWap)
                {
                    Response.Redirect("Home.aspx", true);
                    return;
                }
                Response.Redirect(this.ToWhere, false);
                return;
            }
            this.Alert("用户名密码错误，注意密码区分大小写，请检查是否按下了CapsLock.。");
        }
        catch (System.Exception ex)
        {
            this.Response.Write("<font color=red ><b>@用户名密码错误!@检查是否按下了CapsLock.@更详细的信息:" + ex.Message + "</b></font>");
        }
    }
}
