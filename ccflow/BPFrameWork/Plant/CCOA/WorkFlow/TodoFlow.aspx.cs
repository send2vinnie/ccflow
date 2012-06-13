using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BP.Web;

public partial class CCOA_WorkFlow_TodoFlow : System.Web.UI.Page
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
        DataTable dt = BP.WF.Dev2Interface.DB_GenerEmpWorksOfDataTable(WebUser.No);
        //this.Pub1.AddTD("<a href=\"MyFlow" + this.PageSmall + ".aspx?FK_Flow=" + dr["FK_Flow"] + "&FK_Node=" + dr["NodeID"] + "&FID=" + dr["FID"] + "&WorkID=" + dr["WorkID"] + "\" >" + dr["Title"].ToString());
        gvData.DataSource = dt;
        gvData.DataBind();
    }

    protected void lbtnWorkChart_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = sender as LinkButton;
        Label FK_Flow = lbtn.FindControl("lbFK_Flow") as Label;
        Label FID = lbtn.FindControl("lbFID") as Label;
        string url = string.Format("../../../../ccFlow/WF/Chart.aspx?WorkID={0}&FK_Flow={1}&FID={2}", lbtn.CommandArgument, FK_Flow.Text, FID.Text);
        //Response.Redirect(url);
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "", "window.open('" + url + "');", true);
    }

    string sql = string.Empty;

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    LinkButton workid = e.Row.FindControl("lbtnWorkChart") as LinkButton;
        //    sql = string.Format("select * from V_SHOW where OID =  '{0}'", workid.CommandArgument);
        //    DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
        //    Label prjno = e.Row.FindControl("lbPrjNo") as Label;
        //    Label prjname = e.Row.FindControl("lbPrjName") as Label;
        //    if (dt.Rows.Count > 0)
        //    {
        //        prjno.Text = dt.Rows[0]["OID"].ToString();
        //        prjname.Text = dt.Rows[0]["Name"].ToString();
        //    }
        //}
    }
    protected void lbtnTitle_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = sender as LinkButton;
        Label No = lbtn.FindControl("lbNo") as Label;
        Label FK_Flow = lbtn.FindControl("lbFK_Flow") as Label;
        Label FID = lbtn.FindControl("lbFID") as Label;
        string url = string.Format("../../../../ccFlow/WF/MyFlowSmall.aspx?FK_Node={0}&FK_Flow={1}&FID={2}&WorkID={3}", No.Text, FK_Flow.Text, FID.Text, lbtn.CommandArgument);
        //Response.Redirect(url);
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "", "window.open('" + url + "');", true);
    }
}