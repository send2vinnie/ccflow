using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Port;
using BP.Web;

public partial class Port_Controls_log : System.Web.UI.UserControl
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
       
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string user = txtName.Text.Trim();
        string pass = txtPass.Text;
        try
        {
            Emp em = new Emp();
            em.No = user;
            if (em.RetrieveFromDBSources() == 0)
            {
                ScriptManager.RegisterStartupScript(Page,typeof(Page), "", "alert('用户名或密码错误，注意两者区分大小写，请检查是否按下了CapsLock。');",true);
                return;
            }
            if (em.CheckPass(pass))
            {
                if ("1".Equals(em.IsUSBKEY))
                {
                    string script = string.Format("Check('{0}','{1}','{2}');", em.PID, em.PIN, em.KeyPass);
                    ScriptManager.RegisterStartupScript(Page,typeof(Page), "", script,true);
                    return;
                }
                WebUser.SignInOfGenerLang(em, this.Lang);

                if (this.Request.RawUrl.ToLower().Contains("wap"))
                    WebUser.IsWap = true;
                else
                    WebUser.IsWap = false;

                WebUser.Token = this.Session.SessionID;
                if (WebUser.IsWap)
                {
                    Response.Redirect("NewHome.aspx", true);
                    return;
                }
                Response.Redirect(this.ToWhere, false);
                return;
            }
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "", "alert('用户名或密码错误，注意两者区分大小写，请检查是否按下了CapsLock。');",true);
            //this.Alert("用户名或密码错误，注意两者区分大小写，请检查是否按下了CapsLock。");
        }
        catch (System.Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "", "alert('用户名或密码错误，注意两者区分大小写，请检查是否按下了CapsLock。@更详细的信息:" + ex.Message + "');",true);
           // this.Response.Write("<font color=red ><b>@用户名密码错误!@检查是否按下了CapsLock.@更详细的信息:" + ex.Message + "</b></font>");
        }
    }
    public string ToWhere
    {
        get
        {
            if (this.Request.QueryString["ToWhere"] == null)
            {
                //if (this.Request.RawUrl.ToLower().Contains("small"))
                //    return "EmpWorksSmall.aspx";
                //else
                //    return "EmpWorks.aspx";
                return "Home.aspx";
            }
            else
            {
                return this.Request.QueryString["ToWhere"];
            }
        }
    }
    public void btn1_Click(object sender, EventArgs e)
    {
        string user = txtName.Text.Trim();
        string pass = txtPass.Text;
        try
        {
            Emp em = new Emp();
            em.No = user;
            if (em.RetrieveFromDBSources() == 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "", "alert('用户名或密码错误，注意两者区分大小写，请检查是否按下了CapsLock。');", true);
                return;
            }
            WebUser.SignInOfGenerLang(em, this.Lang);

            if (this.Request.RawUrl.ToLower().Contains("wap"))
                WebUser.IsWap = true;
            else
                WebUser.IsWap = false;

            WebUser.Token = this.Session.SessionID;
            if (WebUser.IsWap)
            {
                Response.Redirect("NewHome.aspx", true);
                return;
            }
            Response.Redirect(this.ToWhere, false);
            return;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "", "alert('用户名或密码错误，注意两者区分大小写，请检查是否按下了CapsLock。@更详细的信息:" + ex.Message + "');", true);
        }
    }
}