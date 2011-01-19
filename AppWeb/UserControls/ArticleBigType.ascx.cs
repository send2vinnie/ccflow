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
using Tax666.DataEntity;
using Tax666.BusinessFacade;

namespace Tax666.AppWeb.UserControls
{
    public partial class ArticleBigType : ModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnDel1.Attributes.Add("onclick", "return OptionRec('CheckAll','STATUS','ArticleBigType1_btnDel1');");
            this.btnDelete1.Attributes.Add("onclick", "return OptionRec('CheckAll','DELETE','ArticleBigType1_btnDelete1');");

            if (!Page.IsPostBack)
            {
                this.BindArticleTypeList();
            }
        }

        /// <summary>
        /// 绑定文章大类列表记录；
        /// </summary>
        private void BindArticleTypeList()
        {
            ArticleBigTypeData bigData = (new ArticleBigTypeFacade()).GetArticleBigTypeByListType(2, 0, 0, 0, true);
            DataTable table = bigData.Tables[ArticleBigTypeData.ArticleBigType_Table];

            if (table.Rows.Count == 0)
            {
                this.btnDel1.Enabled = false;
                this.tabNoRec.Visible = true;
                this.btnDelete1.Enabled = false;
            }
            else
            {
                this.btnDel1.Enabled = true;
                this.btnDelete1.Enabled = true;
                this.tabNoRec.Visible = false;
            }

            this.listRecord1.DataSource = table;
            this.listRecord1.DataKeyField = ArticleBigTypeData.BigTypeID_Field;
            this.listRecord1.DataBind();
        }

        protected void btnDel1_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord1.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord1.DataKeys[thisItem.ItemIndex].ToString());
                    (new ArticleBigTypeFacade()).SetArticleBigTypeInfo(chkedID);
                }
            }
            //this.BindArticleTypeList();
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=0");
        }

        protected void btnDelete1_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord1.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord1.DataKeys[thisItem.ItemIndex].ToString());
                    (new ArticleBigTypeFacade()).DelArticleBigType(chkedID);
                }
            }
            //this.BindArticleTypeList();
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=0");
        }

        protected void btnAddBigType_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleBigTypeEdit.aspx");
        }
    }
}