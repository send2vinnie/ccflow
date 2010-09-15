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
using BP.GE;

public partial class Comm_GE_Gener_ImgLink : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string app = this.Request.ApplicationPath;
        ImgLinks ens = new ImgLinks();
        ens.RetrieveAll();
        this.AddTable("width='100%'");
        foreach (ImgLink en in ens)
        {
            this.AddTR();
            this.AddTD("<a href='" + en.Url + "' target=" + en.Target + " ><img src='" + app + "/Data/BP.GE.ImgLink/" + en.No + "." + en.MyFileExt + "' width=100% height=90 /></a>");
            this.AddTREnd();
        }
        this.AddTableEnd();
    }
}
