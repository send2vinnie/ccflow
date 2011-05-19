using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SDKFlows_Do : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        switch (this.Request.QueryString["DoType"])
        {
            case "DelInfo":
                this.Response.Write("执行成功。");
                break;
            default:
                break;
        }
    }
}