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
using BP.DA;
using BP.WF;
using BP.En;

public partial class WAP_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (WebUser.No == null)
        {
            this.Response.Redirect("Login.aspx", true);
            return;
        }

        this.Title = "您好：" + WebUser.No + "," + WebUser.Name;

        BP.WF.XML.ToolBars ens = new BP.WF.XML.ToolBars();
        ens.RetrieveAll();

        this.Top.Add("Hi:" + WebUser.No + "," + WebUser.Name + "-<a href='DoWap.aspx?DoType=Out'>注销</a>");

        this.Top.AddUL();

        foreach (BP.WF.XML.ToolBar en in ens)
        {
            this.Top.AddLi("<a href='" + en.Url + "' target='_self' title='" + en.Title + "' ><img src='" + en.Img + "' border='0' >" + en.Name + "</a>");
        }
        this.Top.AddULEnd();
        return;

        this.Top.Add("<div align=center><Table width='100%' >");
        this.Top.Add("<TR>");
        this.Top.Add("<TD nowarp=true ><a href='Tools.aspx' >您好:" + BP.Web.WebUser.No + "</a>&nbsp;</TD>");
        this.Top.Add("</TR>");
        string dotype = "";
        foreach (BP.WF.XML.ToolBar en in ens)
        {
            this.Top.Add("<TR>");
            this.Top.Add("<TD nowrap=true><a href='" + en.Url + "' target='_self' title='" + en.Title + "' ><img src='" + en.Img + "' border='0' >" + en.Name + "</a></TD>");
            this.Top.Add("</TR>");
        }
        this.Top.Add("</Table></div>");

    }
}
