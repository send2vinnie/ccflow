using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.GE;


public partial class Case : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "BPM系统成功案例";

        InfoList2s ens = new InfoList2s();
        ens.RetrieveAll();
        this.Pub1.AddTable("width=100%");
        bool is1 = false;
        foreach (InfoList2 en in ens)
        {
            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTD("valign=top","<img src='./CCS/Data/BP.GE.InfoList2/" + en.No + "." + en.MyFileExt + "' width='90px' height='90px' border=0/>");
            this.Pub1.AddTDBigDoc("valign=top align=left", "<b>"+en.Name+"</b><br>"+en.DocHtml);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();


    }
}