using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Web;
using BP.En;
using BP.DA;
using BP.DA;


public partial class WF_Admin_FlowDB : System.Web.UI.Page
{
    public string FK_Flow
    {
        get
        {
            string s = this.Request.QueryString["FK_Flow"];
            if (string.IsNullOrEmpty(s) )
                s = "200";
            return s;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
}