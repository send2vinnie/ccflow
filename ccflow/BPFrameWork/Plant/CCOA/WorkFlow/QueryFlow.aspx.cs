using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CCOA_WorkFlow_QueryFlow : System.Web.UI.Page
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
        //int count = 0;
        //DataTable dt = new DAL.Flow().QueryEndFlow(WebUser.No, 1, PageCounter.PageSize, out count);
        //PageCounter.InitControl(this.PageCounter.PageSize, count);
        //gvData.DataSource = dt;
        //gvData.DataBind();
    }
    //void PageCounter_OnPagerChanged(object sender, CurrentPageEventArgs e)
    //{

    //    DataTable dt = new DAL.Flow().QueryEndFlow(WebUser.No, e.currentPage, PageCounter.PageSize);
    //    gvData.DataSource = dt;
    //    gvData.DataBind();
    //}

    protected void lbtnWorkChart_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = sender as LinkButton;
        //Label WorkID = lbtn.FindControl("lbWorkID") as Label;
        Label FK_Flow = lbtn.FindControl("lbFK_Flow") as Label;
        Label FID = lbtn.FindControl("lbFID") as Label;
        string url = string.Format("../../Flow/WF/Chart.aspx?WorkID={0}&FK_Flow={1}&FID={2}", lbtn.CommandArgument, FK_Flow.Text, FID.Text);
        //Response.Redirect(url);
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "", "window.open('" + url + "');", true);
    }

    string sql = string.Empty;

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton workid = e.Row.FindControl("lbtnWorkChart") as LinkButton;
            sql = string.Format("select * from V_Prj where oid = '{0}'", workid.CommandArgument);
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            Label prjno = e.Row.FindControl("lbPrjNo") as Label;
            Label prjname = e.Row.FindControl("lbPrjName") as Label;
            if (dt.Rows.Count > 0)
            {
                prjno.Text = dt.Rows[0]["PrjNo"].ToString();
                prjname.Text = dt.Rows[0]["PrjName"].ToString();
            }
        }
    }

    protected void lbtnTitle_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = sender as LinkButton;
        Label FK_Flow = lbtn.FindControl("lbFK_Flow") as Label;
        Label FID = lbtn.FindControl("lbFID") as Label;
        string url = string.Format("../../Flow/WF/MyFlowView.aspx?FK_Flow={0}&FID={1}&WorkID={2}&FK_NODE={3}", FK_Flow.Text, FID.Text, lbtn.CommandArgument, int.Parse(FK_Flow.Text) + "99");
        //Response.Redirect(url);
        ScriptManager.RegisterStartupScript(this, Page.GetType(), "", "window.open('" + url + "');", true);
    }
}