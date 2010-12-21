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
using BP.WF.Port;

public partial class OA_Do : BP.Web.WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        switch (this.DoType)
        {
            case "GotoFtp":
                //string ftp=""
                WFEmp emp = new WFEmp(BP.Web.WebUser.No);
                this.Response.Redirect(emp.FtpUrl,true);
                break;
            default:
                break;
        }
    }
}
