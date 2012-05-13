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

public partial class WF_WFRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fk_flow = this.Request.QueryString["FK_Flow"];
        string fk_node = this.Request.QueryString["FK_Node"];
        string workid = this.Request.QueryString["WorkID"];
        this.Response.Redirect("./WorkOpt/OneWork/Track.aspx?FK_Flow=" + fk_flow + "&FK_Node=" + fk_node + "&WorkID=" + workid, true);
    }
}
