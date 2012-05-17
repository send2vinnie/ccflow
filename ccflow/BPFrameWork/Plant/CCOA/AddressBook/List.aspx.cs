using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Lizard.Common;
using System.Drawing;
using LTP.Accounts.Bus;
using BP.DA;
using BP.CCOA;
namespace Lizard.OA.Web.OA_AddrBook
{
    public partial class List : BasePage
    {
        private int m_PageIndex = 1;

        private int m_PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());


        private BP.CCOA.OA_AddrBook OA_AddrBook = new BP.CCOA.OA_AddrBook();

        string[] columns = {
        		                OA_AddrBookAttr.Mobile,
        		                OA_AddrBookAttr.WorkPhone,
        		                OA_AddrBookAttr.HomePhone,
        		                OA_AddrBookAttr.Name,
        		                OA_AddrBookAttr.NickName,
        		                OA_AddrBookAttr.Email,
        		                OA_AddrBookAttr.QQ,
        		                OA_AddrBookAttr.WorkUnit,
        		                OA_AddrBookAttr.WorkAddress,
        		                OA_AddrBookAttr.HomeAddress,
        		                OA_AddrBookAttr.Remarks,
        		                };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int rowsCount = this.GetQueryRowsCount();
                this.XPager1.InitControl(m_PageSize, rowsCount);

                BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void XPager1_PagerChanged(object sender, CurrentPageEventArgs e)
        {
            m_PageIndex = e.pageSize;
            m_PageIndex = e.currentPage;
            this.BindData();
        }

        private int GetQueryRowsCount()
        {
            string searchValue = Request.QueryString["searchvalue"];
            return XQueryTool.GetRowCount(this.OA_AddrBook, this.columns, searchValue);
        }

        #region gridView

        public void BindData()
        {
            #region
            //if (!Context.User.Identity.IsAuthenticated)
            //{
            //    return;
            //}
            //AccountsPrincipal user = new AccountsPrincipal(Context.User.Identity.Name);
            //if (user.HasPermissionID(PermId_Modify))
            //{
            //    gridView.Columns[6].Visible = true;
            //}
            //if (user.HasPermissionID(PermId_Delete))
            //{
            //    gridView.Columns[7].Visible = true;
            //}
            #endregion
            string searchValue = Request.QueryString["searchvalue"];
            DataTable OA_AddrBookTable = XQueryTool.Query<BP.CCOA.OA_AddrBook>(this.OA_AddrBook, columns, searchValue, m_PageIndex, m_PageSize, null);

            gridView.DataSource = OA_AddrBookTable;
            gridView.DataBind();

        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            BindData();
        }
        protected void gridView_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.Cells[0].Text = "<input id='Checkbox2' type='checkbox' onclick='CheckAll()'/><label></label>";
            }
        }
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
                linkbtnDel.Attributes.Add("onclick", "return confirm(\"你确认要删除吗\")");

                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[1].Text = obj1.ToString();
                //}

            }
        }

        #endregion
    }
}
