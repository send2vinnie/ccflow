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
using BP.OA;
using BP.WF;
using BP.Sys;
using BP.Port;
using BP;

public partial class WF_UC_Link : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Links ens = new Links();
        ens.RetrieveAll();

        this.DivInfoBlockBegin();
        if (WebUser.IsWap)
            this.Add("<img src='./Img/Home.gif' ><a href='Home.aspx' >Home</a>");

        this.AddUL();
        foreach (Link en in ens)
        {
            this.AddLi(en.Url, "<b>" + en.Name + "</b>&nbsp;&nbsp;<font color=green>" + en.Url + "</font><br>" + en.Note, en.Target);
        }
        this.AddULEnd();
        this.AddBR();
        this.AddBR();
        this.AddBR();
        this.AddBR();
        this.AddBR();
        this.DivInfoBlockEnd();
    }
}
