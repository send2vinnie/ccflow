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
    public partial class ArticleBigTypeEdit : PageBase
    {
        private int m_BigTypeID;
        private RecordOption OptionType;
        private int  m_Reason;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnOK.Attributes.Add("onclick", "return inputcheckvalid();");

            if (Request.Params["bigid"] == null)
            {
                //添加状态；
                OptionType = RecordOption.AddMode;
            }
            else
            {
                //修改状态；
                OptionType = RecordOption.ModifyMode;
                m_BigTypeID = Int32.Parse(Request.Params["bigid"].ToString());

                if (!Page.IsPostBack)
                {
                    //获取值进行页面填充；
                    ArticleBigTypeData bigData = (new ArticleBigTypeFacade()).GetArticleBigTypeByListType(1, m_BigTypeID, 0, 0, true);
                    if (bigData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows.Count > 0)
                    {
                        DataRow row = bigData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows[0];

                        this.txtbigtypename.Value = row[ArticleBigTypeData.BigTypeName_Field].ToString();
                        this.txttypedesc.Value = WebUtility.htmlOutputText(row[ArticleBigTypeData.BigTypeDesc_Field].ToString());
                    }
                    else
                        Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=0");
                }
            }
        }

        /// <summary>
        /// 保存商品类别；
        /// </summary>
        /// <returns></returns>
        private void SaveArticleBigType()
        {
            bool retVal = false;
            //获取页面中的值；
            string tabName = this.txtbigtypename.Value.Trim();
            string tabContent = WebUtility.htmlInputText(this.txttypedesc.Value.Trim());

            //根据OptionType的值，将新记录保存或更新到数据库中；
            switch (this.OptionType)
            {
                case RecordOption.AddMode:
                    retVal = (new ArticleBigTypeFacade()).InsertArticleBigType(
                        tabName,
                        tabContent,
                        out m_Reason);
                    break;

                case RecordOption.ModifyMode:
                    //修改分类的属性；
                    ArticleBigTypeData bigData = new ArticleBigTypeData();
                    DataRow row = bigData.Tables[ArticleBigTypeData.ArticleBigType_Table].NewRow();

                    row[ArticleBigTypeData.BigTypeName_Field] = tabName;
                    row[ArticleBigTypeData.BigTypeDesc_Field] = tabContent;

                    //将该行添加到数据集DataSet中；
                    bigData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows.Add(row);
                    bigData.AcceptChanges();
                    row[ArticleBigTypeData.BigTypeID_Field] = m_BigTypeID;

                    retVal = (new ArticleBigTypeFacade()).UpdateArticleBigType(bigData);
                    m_Reason =Convert.ToInt32(row[ArticleBigTypeData.Reason_Field].ToString());
                    break;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            this.SaveArticleBigType();
            if (m_Reason == 1)
                Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=0");
            else
                EventMessage.MessageBox(1, "添加/修改文章大类记录失败", "添加/修改文章大类记录失败！可能原因：①已存在相同大类名称；②网络或数据库繁忙。请稍后重试！", Tax666.WebControls.Icon_Type.Error, PublicCommon.GetHomeBaseUrl("ArticleTypeManage.aspx?type=0"));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleTypeManage.aspx?type=0");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlBase + "Manager/AticleManage/ArticleBigTypeEdit.aspx");
        }

    }
}
