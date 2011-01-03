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

public partial class WAP_Home : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (WebUser.No == null)
        {
            this.Response.Redirect("Login.aspx", true);
            return;
        }

        this.Title = "Hi: " + WebUser.No + "," + WebUser.Name;

        BP.WF.XML.ToolBars ens = new BP.WF.XML.ToolBars();
        ens.RetrieveAll();

        string sql = "SELECT COUNT(*) AS Num FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'";
        int num = BP.DA.DBAccess.RunSQLReturnValInt(sql);
        string msg =  this.ToE("PendingWork", "待办");
        if (num != 0)
        {
            msg = "<div id='blink'>" + this.ToE("PendingWork", "待办") + "-" + num + "</div>";
            string script = "";
            script += "<script language=javascript>";
            script += "function changeColor(){";
            script += " var color='#f00|#0f0|#00f|#880|#808|#088|yellow|green|blue|gray';";
            script += " color=color.split('|'); ";
            script += " document.getElementById('blink').style.color=color[parseInt(Math.random() * color.length)] ";
            script += " }";
            script += " setInterval('changeColor()',200);";
            script += "</script> ";
            this.RegisterClientScriptBlock("s", script);
        }

        this.Top.AddFieldSet("<img src='./Img/Home.gif' border=0/>  Hi:" + WebUser.No + "," + WebUser.Name + "-<a href='DoWap.aspx?DoType=Out'>" + this.ToE("LogOut", "注销") + "</a>");

        this.Top.Add("<table border=0 width=100% >");

        bool isTR = true;
        foreach (BP.WF.XML.ToolBar en in ens)
        {
            if (isTR)
                this.Top.AddTR();

            if (en.No == "EmpWorks")
                this.Top.AddTDBigDoc("class=BigDoc align=center", "<a href='" + en.Url + "' target='_self' title='" + en.Title + "' ><img src='" + en.Img + "' border='0' ><br>" + msg + "</a>");
            else
                this.Top.AddTDBigDoc("class=BigDoc align=center", "<a href='" + en.Url + "' target='_self' title='" + en.Title + "' ><img src='" + en.Img + "' border='0' ><br>" + en.Name + "</a>");

            if (isTR == false)
                this.Top.AddTREnd();
            isTR = !isTR;
        }
        this.Top.AddTableEnd();
        return;

         
    }
}
