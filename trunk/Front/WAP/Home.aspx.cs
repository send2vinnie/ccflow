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



        string sql = "SELECT COUNT(*) AS Num FROM WF_EmpWorks WHERE FK_Emp='" + BP.Web.WebUser.No + "'";
        int num = BP.DA.DBAccess.RunSQLReturnValInt(sql);
        string msg = "待办";
        if (num != 0)
        {
            msg = "<div id='blink'>待办-" + num + "</div>";
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


        this.Top.AddTable();
        this.Top.AddCaptionLeft("<img src='./Img/Home.gif' border=0/>  Hi:" + WebUser.No + "," + WebUser.Name + "-<a href='DoWap.aspx?DoType=Out'>注销</a>");

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
