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
namespace Lizard.OA.Web.OA_News
{
    public partial class List : BasePage
    {
        BP.CCOA.OA_News bll = new BP.CCOA.OA_News();

        private int m_PageIndex = 1;

        private int m_PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString());

        string[] columns = { 
                   OA_NewsAttr.Author,
                   OA_NewsAttr.UpUser,
                   OA_NewsAttr.NewsTitle,
                   OA_NewsAttr.NewsSubTitle,
                   OA_NewsAttr.NewsType
                   };
        BP.CCOA.OA_News OA_News = new BP.CCOA.OA_News();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int rowsCount = this.GetQueryRowsCount();
                this.XPager1.InitControl(this.m_PageSize, rowsCount);

                //this.MiniToolBar1.AddClickButton("icon-addfolder", "增加（弹窗）", "add()");
                BindData();
            }
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
            return XQueryTool.GetRowCount<BP.CCOA.OA_News>(OA_News, columns, searchValue);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
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
            DataTable OA_NewsTable = XQueryTool.Query<BP.CCOA.OA_News>(OA_News, columns, searchValue,
                m_PageIndex, m_PageSize, null);

            gridView.DataSource = OA_NewsTable;
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

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl("DeleteThis");
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
                    if (gridView.DataKeys[i].Value != null)
                    {
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        #endregion
    }
}
