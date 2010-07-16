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
using BP.Sys;
using BP.En;
using BP.Web.Controls;
using BP.DA;
using BP.Web;

public partial class WF_MapDef_GroupField : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Pub1.AddFieldSet("Group Fields");

        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("Fields");
        this.Pub1.AddTDTitle("Label");
        this.Pub1.AddTDTitle("Default IsOpen.");
        this.Pub1.AddTREnd();
        for (int i = 0; i < 5; i++)
        {


            DDL ddl = new DDL();
            ddl.ID = "DDL" + i;

                 
            this.Pub1.AddTR();
            this.Pub1.AddTD(ddl);

            TextBox tb = new TextBox();
            tb.ID = "TB"+i;

            this.Pub1.AddTD(tb);
            CheckBox cb = new CheckBox();
            cb.ID = "CB" + i;

            this.Pub1.AddTD(cb);
            this.Pub1.AddTREnd();
            
        }

        this.Pub1.AddTableEnd();


        this.Pub1.AddFieldSetEnd();

    }
}
