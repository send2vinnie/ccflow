using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.GE;
public partial class AboutUs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        this.Title = "ccflow 领军人物";

        InfoList3s ens = new InfoList3s();
        ens.RetrieveAll();
        this.Pub1.AddTable("width=100%");
        bool is1 = false;
        foreach (InfoList3 en in ens)
        {
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTD("valign=top align=left ", "<img src='./CCS/Data/BP.GE.InfoList3/" + en.No + "." + en.MyFileExt + "' width='180px' height='150px' border=0/>");
            this.Pub1.AddTDBigDoc("valign=top align=left", "<b>" + en.Name + "</b><br>" + en.DocHtml);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
    }
}