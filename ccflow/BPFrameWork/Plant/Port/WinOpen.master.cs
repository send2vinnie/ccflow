using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Port_WinOpen : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CurrentApp"] = "EIP";
        }
    }
    protected void lbtnExit_Click(object sender, EventArgs e)
    {
        BP.Web.WebUser.Exit();
        Response.Redirect("Login.aspx");
    }
}
