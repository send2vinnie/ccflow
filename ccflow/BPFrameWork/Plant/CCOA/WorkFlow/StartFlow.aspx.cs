using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.En;
using BP.WF;
using BP.Web;

public partial class CCOA_WorkFlow_StartFlow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
        }
    }
    public void BindList()
    {
        Flows dt = Dev2Interface.DB_GenerCanStartFlowsOfEntities(WebUser.No);
        gvData.DataSource = dt;
        gvData.DataBind();
    }

    protected void lbtnOpen_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = sender as LinkButton;
        Label No = lbtn.FindControl("lbNo") as Label;
        string url = string.Format("../../Flow/WF/Chart.aspx?FK_Flow={0}&DoType=Chart", No.Text);
        ScriptManager.RegisterStartupScript(
            this, Page.GetType(), "", "window.open('" + url + "');", true);
    }
    protected void lbtnName_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = sender as LinkButton;
        Label No = lbtn.FindControl("lbNo") as Label;
        string url = string.Empty;
        url = string.Format("../../../../ccFlow/WF/MyFlowSmall.aspx?FK_Node={0}&FK_Flow={1}&IsDeleteDraft=1", No.Text + "01", No.Text);
        ScriptManager.RegisterStartupScript(
            this, Page.GetType(), "", "window.open('" + url + "');", true);
    }
}