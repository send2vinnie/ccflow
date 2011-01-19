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
using Tax666.Common;

namespace Tax666.AppWeb.Manager.AticleManage
{
    public partial class ArticleManage : PageBase
    {
        private int m_Valid = 2;
        private int m_Audit = 2;
        private int m_BigTypeID = 0;
        private int m_SmallTypeID = 0;
        private string m_TextKey = "ALL";

        protected int m_RecordCount;
        protected int m_PageCount;
        protected int m_CurrentPageIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSearch.Attributes.Add("onclick", "return checktextinput();");
            this.btnDel.Attributes.Add("onclick", "return OptionRec('CheckAll','VALID','btnDel');");
            this.btnDelete.Attributes.Add("onclick", "return OptionRec('CheckAll','DELETE','btnDelete');");
            this.btnAudit.Attributes.Add("onclick", "return OptionRec('CheckAll','AUDIT','btnAudit');");
            this.btnTop.Attributes.Add("onclick", "return OptionRec('CheckAll','STATUS','btnTop');");
            this.btnCommend.Attributes.Add("onclick", "return OptionRec('CheckAll','STATUS','btnCommend');");
            this.btnMove.Attributes.Add("onclick", "return OptionRec('CheckAll','UPDATE','btnMove');");

            string requestStr = "0|0";
            if (Request.Params["typereq"] == null || Request.Params["typereq"] == "")
            {
                m_BigTypeID = 0;
                m_SmallTypeID = 0;
            }
            else
            {
                requestStr = Server.UrlDecode(Request.Params["typereq"]);
                //拆分字符串并取出值；
                string[] reqValue = requestStr.Split(new char[] { '|' });
                m_BigTypeID = int.Parse(reqValue[0]);
                m_SmallTypeID = int.Parse(reqValue[1]);
            }

            if (Request.Params["valid"] == null)
                m_Valid = 2;
            else
                m_Valid = int.Parse(Request.Params["valid"].ToString());

            if (Request.Params["audit"] == null)
                m_Audit = 2;
            else
                m_Audit = int.Parse(Request.Params["audit"].ToString());

            if (Request.Params["key"] == null)
                m_TextKey = "ALL";
            else if (Request.Params["key"] == "" || Request.Params["key"] == string.Empty)
                m_TextKey = "ALL";
            else
                m_TextKey = Server.UrlDecode(Request.Params["key"].ToString());

            //页面按钮执行权限判断(只有平台拥有者才具有的权限)；
            //m_AgentID = int.Parse(AgentUserInfo.Tables[UserAgentData.UserAgent_Table].Rows[0][UserAgentData.AgentID_Field].ToString());
            //if (m_AgentID == ParentAgentID)
            //{
               this.btnDelete.Enabled = true;
                this.btnAudit.Enabled = true;
                this.btnDel.Enabled = true;
            //}
            //else
            //{
                this.btnDelete.Enabled = false;
                this.btnAudit.Enabled = false;
                this.btnDel.Enabled = false;
          //  }

            if (!Page.IsPostBack)
            {
                //保持下拉列表中的值；
                ArticleCommon.BindArticleTypeList(this.dplType, true, 2);
                ArticleCommon.BindArticleTypeList(this.dplType1, false, 2);

                foreach (ListItem item in this.dplType.Items)
                {
                    if (item.Value == requestStr.ToString())
                        item.Selected = true;
                }

                foreach (ListItem item in this.dplAudit.Items)
                {
                    if (item.Value == m_Audit.ToString())
                        item.Selected = true;
                }

                foreach (ListItem item in this.dplvalid.Items)
                {
                    if (item.Value == m_Valid.ToString())
                        item.Selected = true;
                }

                //获取分页的数据；
                ArticleData RecCountSet = (new ArticleFacade()).GetArticlesAdminAll(m_BigTypeID, m_SmallTypeID, m_Valid, m_Audit, m_TextKey, 0, 0, true);
                this.pagesTrade.RecordCount = Int32.Parse(RecCountSet.Tables[ArticleData.Article_Table].Rows[0][ArticleData.TOTALCOUNT_FIELD].ToString());
                this.BindArticleList();
            }
        }

        /// <summary>
        /// 绑定Article的DataList列表。
        /// </summary>
        private void BindArticleList()
        {
            ArticleData data1;
            data1 = (new ArticleFacade()).GetArticlesAdminAll(m_BigTypeID, m_SmallTypeID, m_Valid, m_Audit, m_TextKey, pagesTrade.PageSize, pagesTrade.CurrentPageIndex, false);

            if (data1.Tables[ArticleData.Article_Table].Rows.Count > 0)
            {
                this.tabNoRec.Visible = false;

                //if (m_AgentID == ParentAgentID)
                //{
                   this.btnDelete.Enabled = true;
                    this.btnAudit.Enabled = true;
                    this.btnDel.Enabled = true;
                    this.btnMove.Enabled = true;
                //}
                //else
                //{
                    //this.btnDelete.Enabled = false;
                    //this.btnAudit.Enabled = false;
                    //this.btnDel.Enabled = false;
                    //this.btnMove.Enabled = false;
               // }
            }
            else
            {
                this.tabNoRec.Visible = true;

                this.btnDelete.Enabled = false;
                this.btnAudit.Enabled = false;
                this.btnCommend.Enabled = false;
                this.btnDel.Enabled = false;
                this.btnMove.Enabled = false;
                this.btnTop.Enabled = false;
            }

            this.listRecord.DataSource = data1;
            this.listRecord.DataKeyField = ArticleData.ArticleID_Field;
            this.listRecord.DataBind();

            //动态设置用户自定义文本内容			
            m_RecordCount = pagesTrade.RecordCount;
            m_PageCount = pagesTrade.PageCount;
            m_CurrentPageIndex = pagesTrade.CurrentPageIndex;
        }

        protected void pagesTrade_PageChanged(object sender, EventArgs e)
        {
            this.BindArticleList();
        }

        protected void dplType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string typeStr = Server.UrlEncode(this.dplType.SelectedValue);
            string validvalue = this.dplvalid.SelectedValue;
            string auditvalue = this.dplAudit.SelectedValue;
            string searchtxt = Server.UrlEncode(this.txtKey.Value.Trim());

            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleManage.aspx?typereq=" + typeStr + "&valid=" + validvalue + "&audit=" + auditvalue + "&key=" + searchtxt);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleEdit.aspx");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            //有效和无效设置操作；
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new ArticleFacade()).SetArticleInfo("VALID", chkedID, "");
                }
            }
            this.BindArticleList();
        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new ArticleFacade()).SetArticleInfo("AUDIT", chkedID, "");
                }
            }
            this.BindArticleList();
        }

        protected void btnTop_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new ArticleFacade()).SetArticleInfo("TOP", chkedID, "");
                }
            }
            this.BindArticleList();
        }

        protected void btnCommend_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new ArticleFacade()).SetArticleInfo("COMMEND", chkedID, "");
                }
            }
            this.BindArticleList();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem thisItem in listRecord.Items)
            {
                if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                {
                    int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                    (new ArticleFacade()).DelArticle(chkedID);
                }
            }
            this.BindArticleList();
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            string[] dropList = this.dplType1.SelectedValue.Split(new char[] { '|' });
            int bigClass = int.Parse(dropList[0]);
            int smallClass = int.Parse(dropList[1]);

            if (smallClass == 0)
                new Terminator().ThrowError("不能将您选择的文章移到某个大类中，只能移动到某个小类中。请选择目标小类！");
            else
            {
                foreach (DataGridItem thisItem in listRecord.Items)
                {
                    if (((CheckBox)thisItem.Cells[0].Controls[1]).Checked)
                    {
                        int chkedID = Int32.Parse(listRecord.DataKeys[thisItem.ItemIndex].ToString());
                        (new ArticleFacade()).MoveArticleTabType(chkedID, bigClass, smallClass);
                    }
                }
                this.BindArticleList();
            }
        }

    }
}
