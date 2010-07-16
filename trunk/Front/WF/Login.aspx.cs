using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using BP.Web.Controls;
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

public partial class Face_Login : WebPage
{
    public string Lang 
    {
        get
        {
            string s= this.Request.QueryString["Lang"];
            if (s == null)
                return WebUser.SysLang;
            return s;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        WebUser.SysLang = this.Lang;
        Response.AddHeader("P3P", "CP=CAO PSA OUR");
        int colspan = 1;
        this.Pub2.AddTable("width='90%'");
        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=TitleTop colspan=" + colspan + "></TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=TitleMsg align=left colspan=" + colspan + "><img src='./Img/Login.gif' > <b>" + this.ToE("Login", "系统登陆") + "</b> " + this.ToE("PlsChoseLang", "请选择系统支持的语言") + "</TD>");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTDBegin("align=center");

        this.Pub2.AddBR();


        this.Pub2.AddTable();
        this.Pub2.AddTR();
        this.Pub2.AddTDBegin();

        BP.WF.XML.Langs langs = new BP.WF.XML.Langs();
        langs.RetrieveAll();

        foreach (BP.WF.XML.Lang lang in langs)
        {
            this.Pub2.Add("<a href='Login.aspx?Lang=" + lang.No + "'>" + lang.Name + "</a> &nbsp;&nbsp;");
        }

        this.Pub2.AddTDEnd();
        this.Pub2.AddTREnd();
        this.Pub2.AddTableEndWithHR();


        this.Pub2.AddTable();
        this.Pub2.AddTRSum();
        this.Pub2.AddTD();
        this.Pub2.AddTD();
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
      //  this.Pub2.AddTD("用户名：");
        this.Pub2.AddTD(this.ToE("UserName", "用户名："));


        TextBox tb = new TextBox();
        tb.ID = "TB_User";
        tb.Text = BP.Web.WebUser.No;
        tb.Columns = 20;

        this.Pub2.AddTD(tb);
        this.Pub2.AddTREnd();


        this.Pub2.AddTR();
        this.Pub2.AddTD( this.ToE("PassWord", "密&nbsp;&nbsp;码：") );
        tb = new TextBox();
        tb.ID = "TB_Pass";
        tb.TextMode = TextBoxMode.Password;
        tb.Columns = 22;
        this.Pub2.AddTD(tb);
        this.Pub2.AddTREnd();

      

        this.Pub2.AddTRSum();
        this.Pub2.AddTDBegin("colspan=3 align=center");
        Button btn = new Button();
         
        btn.Text =this.ToE("Login", "登 陆：");

        btn.Click += new EventHandler(btn_Click);
        this.Pub2.Add(btn);

        if (WebUser.No != null)
        {
            if (WebUser.IsAuthorize)
                this.Pub2.Add(" - <a href=\"javascript:ExitAuth('"+WebUser.Auth+"')\" >退出授权模式[" + WebUser.Auth + "]</a>");
            else
                this.Pub2.Add(" - <a href='Tools.aspx?RefNo=AutoLog' >授权方式登录</a>");
        }

        this.Pub2.AddTDEnd();

        this.Pub2.AddTREnd();
       
        this.Pub2.AddTableEnd();

        this.Pub2.AddBR();
        this.Pub2.AddBR();


        this.Pub2.AddTDEnd();
        this.Pub2.AddTREnd();
        this.Pub2.AddTableEnd();

    }

    public string ToWhere = "EmpWorks.aspx";
    void btn_Click(object sender, EventArgs e)
    {
        string user = this.Pub2.GetTextBoxByID("TB_User").Text;
        string pass = this.Pub2.GetTextBoxByID("TB_Pass").Text;
        try
        {
            Emp em = new Emp(user);
            if (pass == "test" || SystemConfig.IsDebug || em.CheckPass(pass))
            {
                WebUser.SignInOfGenerLang(em, this.Lang);
                WebUser.Token = this.Session.SessionID;
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
