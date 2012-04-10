using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XControls.Base;
using System.Data;
using BP.CCOA;

public partial class CCOA_News_uc_NoticeGrid : BP.Web.UC.UCBase3
{
    public int PageSize { get; set; }
    public bool DisplayOperatingColumn { get; set; }
    //Contract m_Dal = new Contract();
    protected void Page_Load(object sender, EventArgs e)
    {
        //m_Dal.Pid = Request.QueryString["pid"];

        //PageCounter.OnPagerChanged += new control_PageCounter.RefreshEventHandler(PageCounter_OnPagerChanged);
        if (!IsPostBack)
        {
            int count = 0;
            //Attachment a = new Attachment();
            //DataTable dt = m_Dal.SelectAll(1, PageSize == 0 ? 10 : PageSize, out count);
            //PageCounter.InitControl(this.PageSize, count);
            //this.Bind(dt);
            //this.BindSearch();
            //this.grid.Columns[11].Visible = this.DisplayOperatingColumn;
        }
    }


    public Articles Articles
    {
        get
        {
            Articles articles = new Articles();
            articles.RetrieveAll();
            return articles;      
        }
    }


    private void BindSearch()
    {
        this.xSearch.AddItem(new ListItem("合同编号", "F_ID"));
        this.xSearch.AddItem(new ListItem("合同名称", "F_NAME"));
        //this.xSearch.AddItem(new ListItem("所属工程", "F_ENGINEERING_NAME"));
        this.xSearch.AddItem(new ListItem("合同类别", "F_CATEGORY"));
        this.xSearch.AddItem(new ListItem("对方单位", "F_CONSTRUCTION_ORGANIZATION"));
    }

    //void PageCounter_OnPagerChanged(object sender, CurrentPageEventArgs e)
    //{
    //    DataTable dt = m_Dal.SelectAll(e.currentPage, e.pageSize);
    //    Bind(dt);
    //}

    //public void Bind(DataTable dt)
    //{
    //    this.grid.DataSource = dt.DefaultView;
    //    this.grid.DataBind();
    //}
    protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string no = (e.Row.FindControl("lblNo") as Label).Text;
            e.Row.Attributes.Add("onclick", "setid('" + no + "');");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //FilterInfo filter = new FilterInfo(xSearch.FilterType, xSearch.FilterValue)
        //{
        //    StartDate = xSearch.StartDate,
        //    EndDate = xSearch.EndDate
        //};
        //int count = 0;
        //var dt = m_Dal.SelectByPara(grid.PageIndex + 1, grid.PageSize, filter, out count);
        //Bind(dt);
    }

    public void Rebind()
    {
        //int count = 0;
        //DataTable dt = m_Dal.SelectAll(1, PageSize == 0 ? 10 : PageSize, out count);
        //PageCounter.InitControl(this.PageSize, count);
        //this.Bind(dt);
    }

    //public string SelectedItemValue
    //{
    //    get
    //    {
    //        string selectedValue = string.Empty;
    //        if (this.grid.SelectedRow != null)
    //        {
    //            selectedValue = (this.grid.SelectedRow.FindControl("lblNo") as Label).Text.Trim();
    //        }
    //        return selectedValue;
    //    }
    //}
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        string pid = ((XLinkButton)sender).CommandArgument.ToString();
        Response.Redirect("htgl_xzht.aspx?type=htgl_xzht&pid=" + pid + "&editmodel=edit");
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        //string no = ((XLinkButton)sender).CommandArgument.ToString();
        //DAL.Contract dal = new Contract();
        //dal.Delete(no);
        //Utility.ScriptAlert(this, "删除成功！");
        //Rebind();
    }
}