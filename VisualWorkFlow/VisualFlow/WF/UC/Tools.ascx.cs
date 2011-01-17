using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Port;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_Tools : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.WF.XML.Tools tools = new BP.WF.XML.Tools();
        tools.RetrieveAll();
        string refno = this.RefNo;
        if (refno == null)
            refno = "Per";


        this.Left.DivInfoBlockBegin();

        if (WebUser.IsWap)
            this.Left.Add("<a href='Home.aspx'><img src='./Img/Home.gif' border=0/>Home</a>");

        this.Left.AddUL();

        foreach (BP.WF.XML.Tool tool in tools)
        {
            if (tool.No == refno)
                this.Left.AddLi("<b>" + tool.Name + "</b>");
            else
                this.Left.AddLi("Tools.aspx?RefNo=" + tool.No, tool.Name, "_self");
        }

        if (WebUser.No == "admin")
        {
            this.Left.AddLi("Tools.aspx?RefNo=AdminSet", this.ToE("SiteSet", "网站设置"), "_self");
        }


        this.Left.AddULEnd();
        this.Left.DivInfoBlockEnd();

    }
}
