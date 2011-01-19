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
using Tax666.BusinessFacade;
using Tax666.DataEntity;

namespace Tax666.AppWeb.UserControls
{
    public partial class ArticleSmallType : ModuleBase
    {
        private int m_BigTypeID = 0;
        private int m_Valid = 2;

        protected int m_RecordCount;
        protected int m_PageCount;
        protected int m_CurrentPageIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnDel.Attributes.Add("onclick", "return OptionRec('CheckAll','VALID','ArticleSmallType1_btnDel');");
            this.btnDelete.Attributes.Add("onclick", "return OptionRec('CheckAll','DELETE','ArticleSmallType1_btnDelete');");
            this.btnSystem.Attributes.Add("onclick", "return OptionRec('CheckAll','STATUS','SamllTypeManage1_btnSystem');");

            if (Request.Params["valid"] == null)
                m_Valid = 2;
            else
                m_Valid = int.Parse(Request.Params["valid"].ToString());

            if (Request.Params["bigid"] == null)
                m_BigTypeID = 0;
            else
                m_BigTypeID = int.Parse(Request.Params["bigid"].ToString());

            if (!Page.IsPostBack)
            {
                //保持下拉列表中的值；
                ArticleCommon.BindArticleBigTypeList(this.dplBigType, true);
                foreach (ListItem item in this.dplBigType.Items)
                {
                    if (item.Value == m_BigTypeID.ToString())
                        item.Selected = true;
                }

                foreach (ListItem item in this.dplvalid.Items)
                {
                    if (item.Value == m_Valid.ToString())
                        item.Selected = true;
                }

                //获取分页的数据；
                ArticleSmallTypeData RecCountSet = (new ArticleSmallTypeFacade()).GetArticleSmallTypeByList(3, m_BigTypeID, 0, m_Valid, 0, 0, true);
                this.pagesTrade.RecordCount = Int32.Parse(RecCountSet.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows[0][ArticleSmallTypeData.TOTALCOUNT_FIELD].ToString());
                this.BindArticleSallTypeDataList();
            }
        }

        private void BindArticleSallTypeDataList()
        {
            ArticleSmallTypeData smallData = (new ArticleSmallTypeFacade()).GetArticleSmallTypeByList(3, m_BigTypeID, 0, m_Valid, pagesTrade.PageSize, pagesTrade.CurrentPageIndex, false);

            if (smallData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Count == 0)
            {
                this.tabNoRec.Visible = true;
                this.btnDel.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnSystem.Enabled = false;
            }
            else
            {
                this.tabNoRec.Visible = false;
                this.btnDel.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnSystem.Enabled = true;
            }

            this.listRecord.DataSource = smallData;
            this.listRecord.DataKeyField = ArticleSmallTypeData.SmallTypeID_Field;
            this.listRecord.DataBind();

            //动态设置用户自定义文本内容			
            m_RecordCount = pagesTrade.RecordCount;
            m_PageCount = pagesTrade.PageCount;
            m_CurrentPageIndex = pagesTrade.CurrentPageIndex;
        }
        protected void pagesTrade_PageChanged(object sender, EventArgs e)
        {
            BindArticleSallTypeDataList();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new ArticleSmallTypeFacade()).SetArticleSmallTypeInfo("VALID", chkedID);
                }
            }
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=1");
        }

        protected void btnSystem_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new ArticleSmallTypeFacade()).SetArticleSmallTypeInfo("SYSTEM", chkedID);
                }
            }
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=1");
        }

        protected void dplBigType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valid = this.dplvalid.SelectedValue;
            string bigtypeid = this.dplBigType.SelectedValue;
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=1&valid=" + valid + "&bigid=" + bigtypeid);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new ArticleSmallTypeFacade()).DelArticleSmallType(chkedID);
                }
            }
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=1");
        }

        protected void btnAddTab_Click(object sender, EventArgs e)
        {
            string bigtypeid = this.dplBigType.SelectedValue;
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleSmallTypeEdit.aspx?bigid=" + bigtypeid);
        }

    }
}