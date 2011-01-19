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
using Tax666.WebControls;

namespace Tax666.AppWeb.Manager.AticleManage
{
    public partial class ArticleSmallTypeEdit : PageBase
    {
        private int m_SmallTypeID;
        private RecordOption OptionType;
        private int  m_Reason;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnOK.Attributes.Add("onclick", "return inputcheckvalid();");

            if (Request.Params["smallid"] == null)
            {
                //添加状态；
                int bigTypeID = 0;
                if (Request.Params["bigid"] == null)
                    bigTypeID = 0;
                else
                    bigTypeID = int.Parse(Request.Params["bigid"].ToString());

                OptionType = RecordOption.AddMode;
                if (!Page.IsPostBack)
                {
                    int smallTypeNum = ArticleCommon.BindArticleBigTypeList(this.dplBigType, false);
                    if (smallTypeNum > 0)
                    {
                        this.btnOK.Enabled = true;
                        this.btnNew.Enabled = true;
                        if (bigTypeID > 0)
                        {
                            foreach (ListItem item in this.dplBigType.Items)
                            {
                                if (item.Value == bigTypeID.ToString())
                                    item.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        this.btnOK.Enabled = false;
                        this.btnNew.Enabled = false;
                    }
                }
            }
            else
            {
                //修改状态；
                OptionType = RecordOption.ModifyMode;
                m_SmallTypeID = Int32.Parse(Request.Params["smallid"].ToString());

                if (!Page.IsPostBack)
                {
                    ArticleCommon.BindArticleBigTypeList(this.dplBigType, false);

                    //获取值进行页面填充；
                    ArticleSmallTypeData smallData = (new ArticleSmallTypeFacade()).GetArticleSmallTypeByList(1, 0, m_SmallTypeID, 2, 0, 0, true);
                    if (smallData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Count > 0)
                    {
                        DataRow row = smallData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows[0];

                        this.txtsmalltypename.Value = row[ArticleSmallTypeData.SmallTypeName_Field].ToString();
                        this.txttypedesc.Value = WebUtility.htmlOutputText(row[ArticleSmallTypeData.SmallTypeDesc_Field].ToString());
                        int bigTypeID = int.Parse(row[ArticleSmallTypeData.BigTypeID_Field].ToString());

                        foreach (ListItem item in this.dplBigType.Items)
                        {
                            if (item.Value == bigTypeID.ToString())
                                item.Selected = true;
                        }

                        if ((bool)row[ArticleSmallTypeData.IsAvailable_Field])
                            this.radValid.Checked = true;
                        else
                            this.radInvalid.Checked = true;
                    }
                    else
                        Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=1");
                }
            }
        }

        /// <summary>
        /// 保存商品小类记录；
        /// </summary>
        /// <returns></returns>
        private void SaveArticleSmallType()
        {
            bool retVal = false;
            //获取页面中的值；
            string tabName = this.txtsmalltypename.Value.Trim();
            string tabContent = WebUtility.htmlInputText(this.txttypedesc.Value.Trim());
            int isAvailable = 1;
            if (this.radValid.Checked)
                isAvailable = 1;
            else if (this.radInvalid.Checked)
                isAvailable = 0;
            else
                isAvailable = 1; // 如果没有，强迫产生一个isAvailable字段的默认值;
            int bigtypeid = int.Parse(this.dplBigType.SelectedValue);

            //根据OptionType的值，将新记录保存或更新到数据库中；
            switch (this.OptionType)
            {
                case RecordOption.AddMode:
                    retVal = (new ArticleSmallTypeFacade()).InsertArticleSmallType(
                        bigtypeid,
                        tabName,
                        tabContent,
                        isAvailable,
                        out m_Reason);
                    break;

                case RecordOption.ModifyMode:
                    //修改分类的属性；
                    ArticleSmallTypeData smallData = new ArticleSmallTypeData();
                    DataRow row = smallData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].NewRow();

                    row[ArticleSmallTypeData.BigTypeID_Field] = bigtypeid;
                    row[ArticleSmallTypeData.SmallTypeName_Field] = tabName;
                    row[ArticleSmallTypeData.SmallTypeDesc_Field] = tabContent;
                    row[ArticleSmallTypeData.IsAvailable_Field] = isAvailable;

                    //将该行添加到数据集DataSet中；
                    smallData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Add(row);
                    smallData.AcceptChanges();
                    row[ArticleSmallTypeData.SmallTypeID_Field] = m_SmallTypeID;

                    retVal = (new ArticleSmallTypeFacade()).UpdateArticleSmallType(smallData);
                    m_Reason =Convert.ToInt32( row[ArticleSmallTypeData.Reason_Field].ToString());
                    break;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            int bigtypeid = int.Parse(this.dplBigType.SelectedValue);
            this.SaveArticleSmallType();
            if (m_Reason == 1)
                Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=1&bigid=" + bigtypeid);
            else
                EventMessage.MessageBox(1, "添加/修改文章小类记录失败", "添加/修改文章小类记录失败！可能原因：①已存在相同小类名称；②网络或数据库繁忙。请稍后重试！", Tax666.WebControls.Icon_Type.Error, PublicCommon.GetHomeBaseUrl("ArticleTypeManage.aspx?type=1&bigid=" + bigtypeid));
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            string bigtypeid = this.dplBigType.SelectedValue;
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleSmallTypeEdit.aspx?bigid=" + bigtypeid);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=1");
        }

    }
}
