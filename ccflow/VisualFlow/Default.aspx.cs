using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Web;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.RawUrl.ToLower().Contains("wap"))
        {
            this.Response.Redirect("./WAP/", true);
            return;
        }
        this.Response.Redirect("./WF/Login.aspx", true);
        //    this.Response.Redirect("Designer.aspx", true);
        return;
    }
}
