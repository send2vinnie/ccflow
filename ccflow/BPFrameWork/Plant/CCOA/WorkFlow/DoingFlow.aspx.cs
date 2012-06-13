using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BP.WF;
using BP.Web;

public partial class CCOA_WorkFlow_DoingFlow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindList();
        if (WebUser.No == "admin")
        {
            gvData.Columns[11].Visible = true;
        }
    }

    public void BindList()
    {
        if (WebUser.No == "admin")
        {
            string sql = "SELECT a.WorkID FROM WF_GenerWorkFlow A, WF_GenerWorkerlist B WHERE A.WorkID=B.WorkID AND B.IsEnable=1 AND B.IsPass=1 ";
            GenerWorkFlows gwfs = new GenerWorkFlows();
            gwfs.RetrieveInSQL(GenerWorkFlowAttr.WorkID, "(" + sql + ")");
            gvData.DataSource = gwfs;
            gvData.DataBind();
            gvData.Columns[10].Visible = true;
        }
        else
        {
            GenerWorkFlows dt = BP.WF.Dev2Interface.DB_GenerRuningOfEntities(WebUser.No);
            gvData.DataSource = dt;
            gvData.DataBind();
        }
    }

    protected void lbtnWorkChart_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = sender as LinkButton;
        //Label WorkID = lbtn.FindControl("lbWorkID") as Label;
        Label FK_Flow = lbtn.FindControl("lbFK_Flow") as Label;
        Label FID = lbtn.FindControl("lbFID") as Label;
        string url = string.Format("../../../../ccFlow/WF/Chart.aspx?WorkID={0}&FK_Flow={1}&FID={2}", lbtn.CommandArgument, FK_Flow.Text, FID.Text);
        //Response.Redirect(url);
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "", "window.open('" + url + "');", true);
    }

    string sql = string.Empty;

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //LinkButton workid = e.Row.FindControl("lbtnWorkChart") as LinkButton;
            //sql = string.Format("select * from V_Prj where oid = '{0}'", workid.CommandArgument);
            //DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            //Label prjno = e.Row.FindControl("lbPrjNo") as Label;
            //Label prjname = e.Row.FindControl("lbPrjName") as Label;
            //if (dt.Rows.Count > 0)
            //{
            //    prjno.Text = dt.Rows[0]["PrjNo"].ToString();
            //    prjname.Text = dt.Rows[0]["PrjName"].ToString();
            //}
        }
    }
    protected void lbtnTitle_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = sender as LinkButton;
        Label FK_Flow = lbtn.FindControl("lbFK_Flow") as Label;
        Label FID = lbtn.FindControl("lbFID") as Label;
        string workflow = FK_Flow.Text;
        string fId = FID.Text;
        string workId = lbtn.CommandArgument;
        string url = string.Format("../../../../ccFlow/WF/MyFlowView.aspx?FK_Flow={0}&FID={1}&WorkID={2}",
            workflow, fId, workId);
        //Response.Redirect(url);
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "", "window.open('" + url + "');", true);
    }
    protected void lblDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = sender as LinkButton;
        Label lb = lbtn.FindControl("lbFK_Flow") as Label;
        string msg = BP.WF.Dev2Interface.Flow_DoDeleteFlow(lb.Text, long.Parse(lbtn.CommandArgument));
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "", "alert('" + msg + "');", true);
    }
}