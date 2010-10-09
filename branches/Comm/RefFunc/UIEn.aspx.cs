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

public partial class Comm_RefFunc_En : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (this.Request.QueryString["EnsName"].Contains(".") == false)
        {
            this.Response.Redirect("SysMapEn.aspx?EnsName="+this.Request.QueryString["EnsName"]+"&PK="+this.Request["PK"],true);
            return;
        }


    }
}
