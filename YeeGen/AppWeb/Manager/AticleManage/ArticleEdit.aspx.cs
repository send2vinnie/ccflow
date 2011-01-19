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
using Tax666.Common;
using Tax666.DataEntity;
using Tax666.BusinessFacade;
using Tax666.SystemFramework;
using Tax666.WebControls;

namespace Tax666.AppWeb.Manager.AticleManage
{
    public partial class ArticleEdit : PageBase
    {
        protected int m_ArticleID;
        protected string m_ImagePath;
        private RecordOption OptionType;
        private int  m_Reason;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnOK.Attributes.Add("onclick", "return inputcheck();");

            if (Request.Params["articleid"] != null)
            {
                //修改状态；
                OptionType = RecordOption.ModifyMode;
                m_ArticleID = Int32.Parse(Request.Params["articleid"]);
                if (!Page.IsPostBack)
                {
                    ArticleData articleData = (new ArticleFacade()).GetArticleByListType(1, 0, 0, m_ArticleID);
                    if (articleData.Tables[ArticleData.Article_Table].Rows.Count > 0)
                    {
                        DataRow row = articleData.Tables[ArticleData.Article_Table].Rows[0];

                        //绑定栏目列表；
                        ArticleCommon.BindArticleTypeList(this.dplModuleType, false, 2);

                        string dropValue = row[ArticleData.BigTypeID_Field].ToString() + "|" + row[ArticleData.SmallTypeID_Field].ToString();
                        foreach (ListItem item in this.dplModuleType.Items)
                        {
                            if (item.Value == dropValue)
                                item.Selected = true;
                        }

                        txtTitle.Value = row[ArticleData.Title_Field].ToString();
                        Editor1.Text = row[ArticleData.ArticleHtmlDesc_Field].ToString();
                        Author.Value = row[ArticleData.Author_Field].ToString();
                        txtSourse.Text = row[ArticleData.Sourse_Field].ToString();
                        txtEmail.Text = row[ArticleData.AuthorEmail_Field].ToString();
                        txtHomepage.Text = row[ArticleData.AuthorHomePage_Field].ToString();
                        this.txtSEOKey.Value = ArticleCommon.GetAricleKeys(m_ArticleID);

                        //图片的路径及文件处理；					
                        if (row[ArticleData.ArticlePicPath_Field].ToString() == "" || row[ArticleData.ArticlePicPath_Field].ToString() == string.Empty)
                        {
                            this.trUpFile.Visible = true;
                            this.trResPath.Visible = false;
                            m_ImagePath = "主题图片不存在";
                        }
                        else
                        {
                            this.trUpFile.Visible = false;
                            this.trResPath.Visible = true;

                            m_ImagePath = row[ArticleData.ArticlePicPath_Field].ToString().Replace("OriginImages", "ZoomImages").Replace("_Ori", "_Res");
                        }

                        //有效；
                        if (Convert.ToBoolean(row[ArticleData.IsAvailable_Field]))
                            this.chkValid.Checked = true;
                        else
                            this.chkValid.Checked = false;
                        //置顶；
                        if (Convert.ToBoolean(row[ArticleData.IsTop_Field]))
                            this.chkTop.Checked = true;
                        else
                            this.chkTop.Checked = false;
                        //推荐；
                        if (Convert.ToBoolean(row[ArticleData.IsCommend_Field]))
                            this.chkIsCommend.Checked = true;
                        else
                            this.chkIsCommend.Checked = false;

                    }
                    else
                        new Terminator().ThrowError("该文章已经不存在，无法对其进行编辑操作！");

                }
            }
            else
            {
                //添加状态；
                OptionType = RecordOption.AddMode;

                if (!Page.IsPostBack)
                {
                    this.trUpFile.Visible = true;
                    this.trResPath.Visible = false;

                    //绑定栏目列表；
                    ArticleCommon.BindArticleTypeList(this.dplModuleType, false, 2);
                }
            }
        }

        /// <summary>
        /// 保存文章记录。
        /// </summary>
        private int  SaveArticle()
        {
            bool retVal = false;
            int isTop = 0;
            int isCommend = 0;
            int isAudit = 0;
            int isAvailable = 1;

            //获取页面中的值；
            string[] dropValue = this.dplModuleType.SelectedValue.Split(new char[] { '|' });
            int bigTypeID = int.Parse(dropValue[0]);
            int smallTypeID = int.Parse(dropValue[1]);
            if (bigTypeID == 0)
                new Terminator().ThrowError("对不起，目前无文章资讯分类，请先添加或选择资讯栏目后再添加文章！");
            if (smallTypeID == 0)
                new Terminator().ThrowError("该文章没有选择文章小类，请返回重新选择文章所属类别！");

            if (this.chkTop.Checked)
                isTop = 1;

            if (this.chkIsCommend.Checked)
                isCommend = 1;

            if (this.chkValid.Checked)
                isAvailable = 1;
            else
                isAvailable = 0;

            string title = this.txtTitle.Value.Trim();
            string articleKeys = this.txtSEOKey.Value.Trim();
            string author = this.Author.Value.Trim();
            string source = this.txtSourse.Text.Trim();
            string email = this.txtEmail.Text.Trim();
            string homepage = this.txtHomepage.Text.Trim();

            //根据OptionType的值，将新供求信息保存或更新到数据库中；
            switch (this.OptionType)
            {
                case RecordOption.AddMode:
                    //添加记录；
                    retVal = (new ArticleFacade()).InsertArticle(
                        bigTypeID,
                        smallTypeID,
                        title,
                        Editor1.Text.Trim(),
                        author,
                        source,
                        email,
                        homepage,
                        isAudit,
                        isCommend,
                        isTop,
                        isAvailable,1,
                        out m_ArticleID,
                        out m_Reason);

                    if (retVal)
                        ArticleCommon.InsertArticleKeys(m_ArticleID, articleKeys);

                    break;
                case RecordOption.ModifyMode:
                    ArticleData articleData = new ArticleData();
                    DataRow row = articleData.Tables[ArticleData.Article_Table].NewRow();

                   // row[ArticleData.AgentID_Field] = ParentAgentID;
                    row[ArticleData.Title_Field] = title;
                    row[ArticleData.BigTypeID_Field] = bigTypeID;
                    row[ArticleData.SmallTypeID_Field] = smallTypeID;
                    row[ArticleData.ArticleHtmlDesc_Field] = this.Editor1.Text.Trim();
                    row[ArticleData.Author_Field] = author;
                    row[ArticleData.Sourse_Field] = source;
                    row[ArticleData.AuthorEmail_Field] = email;
                    row[ArticleData.AuthorHomePage_Field] = homepage;
                    row[ArticleData.IsCommend_Field] = isCommend;
                    row[ArticleData.IsTop_Field] = isTop;
                    row[ArticleData.IsAvailable_Field] = isAvailable;

                    //将该行添加到数据集DataSet中；
                    articleData.Tables[ArticleData.Article_Table].Rows.Add(row);
                    articleData.AcceptChanges();
                    row[ArticleData.ArticleID_Field] = m_ArticleID;

                    retVal = (new ArticleFacade()).UpdateArticle(articleData);
                    m_Reason =Convert.ToInt32(row[ArticleData.Reason_Field].ToString());

                    if (retVal)
                        ArticleCommon.InsertArticleKeys(m_ArticleID, articleKeys);

                    break;
            }

            if (m_Reason == 1)
            {
                if (Request.Files["filepath"] != null)
                {
                    //上传文件操作；
                    HttpPostedFile PostedFile = Request.Files[0];
                    if (PostedFile.FileName != "" || PostedFile.FileName != String.Empty)
                    {
                        //上传图片文件(大小限制：2MB)；
                        string[] upFileRet = FileOption.UpLoadImgFile(PostedFile, 2048, Tax666Configuration.ImagePath, true, 160, 120);

                        if (Int32.Parse(upFileRet[0]) == 1)
                        {
                            string m_Picpath = upFileRet[4];
                            (new ArticleFacade()).SetArticleInfo("UPDATEPIC", m_ArticleID, m_Picpath);
                        }
                    }
                }
            }

            return m_Reason;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (SaveArticle() == 1)
            {
                string typeStr = Server.UrlEncode(this.dplModuleType.SelectedValue);
                Response.Redirect(UrlBase + "Manager/AticleManage/ArticleManage.aspx?typereq=" + typeStr);
            }
            else
                EventMessage.MessageBox(1, "添加或修改资讯文章失败", "添加或修改资讯文章失败！可能原因：①已存在相同文章标题名称；②网络或数据库繁忙。请稍后重试！", Tax666.WebControls.Icon_Type.Error, PublicCommon.GetHomeBaseUrl("ArticleManage.aspx"));
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleEdit.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleManage.aspx");
        }

        protected void chkChanageRes_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChanageRes.Checked)
                this.trUpFile.Visible = true;
            else
                this.trUpFile.Visible = false;
        }

    }
}
