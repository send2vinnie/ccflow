using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Tax666.Common;
using Tax666.DataEntity;
using Tax666.BusinessFacade;
using Tax666.WebControls;
using Tax666.SystemFramework;

namespace Tax666.AppWeb.Manager.AticleManage
{
    public partial class HomeLinkEdit : PageBase
    {
        private int m_LinkID;
        private RecordOption OptionType;
        private int  m_Reason;
        private bool m_LogoPathNull = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["linkid"] != null)
            {
                //修改状态；
                OptionType = RecordOption.ModifyMode;
                m_LinkID = Int32.Parse(Request.Params["linkid"]);

                this.trUpFile.Visible = false;
                this.trResPath.Visible = true;

                HomeLinkData links;
                links = (new HomeLinkFacade()).GetHomeLinkByListType(1, 0, m_LinkID);
                DataRow row = links.Tables[HomeLinkData.HomeLink_Table].Rows[0];

                if (row[HomeLinkData.LogoPath_Field].ToString() == "" || row[HomeLinkData.LogoPath_Field].ToString() == string.Empty)
                {
                    this.lblResPath.Text = "无Logo图片！";
                    this.chkChanageRes.Text = "添加Logo图片";
                    m_LogoPathNull = true;
                }
                else
                {
                    this.lblResPath.Text = row[HomeLinkData.LogoPath_Field].ToString();
                    this.chkChanageRes.Text = "更换Logo图片";
                    m_LogoPathNull = false;
                }

                if (!Page.IsPostBack)
                {
                    this.BindLinkType(1);

                    //填充获取的值到相应的控件中；
                    int linkTypeID = Int32.Parse(row[HomeLinkData.LinkTypeID_Field].ToString());
                    //根据类别确定其值在下拉列表中的位置；
                    foreach (ListItem item in this.dplHomeType.Items)
                    {
                        if (item.Value == linkTypeID.ToString())
                            item.Selected = true;
                    }
                    this.txtName.Text = row[HomeLinkData.LinkName_Field].ToString();
                    this.txtDesc.Text = row[HomeLinkData.HomeDesc_Field].ToString();
                    this.txtUrl.Text = row[HomeLinkData.LinkUrl_Field].ToString();
                    int linkType = Int32.Parse(row[HomeLinkData.LinkMode_Field].ToString());
                    if (linkType == 1) this.radLink1.Checked = true;
                    if (linkType == 2) this.radLink2.Checked = true;
                }
            }
            else
            {
                //添加状态；
                OptionType = RecordOption.AddMode;

                this.trUpFile.Visible = true;
                this.trResPath.Visible = false;

                if (!Page.IsPostBack)
                {
                    this.BindLinkType(1);
                }
            }
        }
        private void BindLinkType(int valid)
        {
            HomeLinkTypeData typeData = (new HomeLinkTypeFacade()).GetHomeLinkTypeByListType(2, 0, valid);
            this.dplHomeType.DataSource = typeData;
            this.dplHomeType.DataTextField = HomeLinkTypeData.TypeName_Field;
            this.dplHomeType.DataValueField = HomeLinkTypeData.LinkTypeID_Field;
            this.dplHomeType.DataBind();
        }

        /// <summary>
        /// 添加/修改友情链接记录。
        /// </summary>
        /// <returns></returns>
        private bool SaveHomeLink()
        {
            bool retVal = false;

            foreach (IValidator val in Page.Validators)
            {
                val.Validate();
            }

            //if (AgentUserInfo == null)
            //    retVal = false;
            //else
            //{
                if (Page.IsValid)
                {
                    int linkType = 1;
                    if (this.radLink1.Checked) linkType = 1;
                    if (this.radLink2.Checked) linkType = 2;

                    txtName.Text = txtName.Text.Trim();
                    txtUrl.Text = txtUrl.Text.Trim();

                    //根据OptionType的值，将记录保存或更新到数据库中；
                    switch (this.OptionType)
                    {
                        case RecordOption.AddMode:
                            //上传文件操作；
                            HttpPostedFile PostedFile = Request.Files[0];
                            if (PostedFile.FileName != "" || PostedFile.FileName != String.Empty)
                            {
                                //上传图片文件(大小限制：2MB)；
                                string[] upFileRet = FileOption.UpLoadImgFile(PostedFile, 2000, Tax666Configuration.AdLinkPath, true, 88, 31);

                                if (Int32.Parse(upFileRet[0]) == 1)
                                {
                                    //添加记录操作；
                                    retVal = (new HomeLinkFacade()).InsertHomeLink(
                                        txtName.Text.Trim(),
                                        txtUrl.Text.Trim(),
                                        this.txtDesc.Text.Trim(),
                                        upFileRet[5],
                                        Int32.Parse(this.dplHomeType.SelectedValue),
                                        linkType,
                                        out m_Reason);
                                }
                            }
                            else
                            {
                                //添加记录操作；
                                retVal = (new HomeLinkFacade()).InsertHomeLink(
                                    txtName.Text,
                                    txtUrl.Text,
                                    this.txtDesc.Text,
                                    "",
                                    Int32.Parse(this.dplHomeType.SelectedValue),
                                    linkType,
                                    out m_Reason);
                            }

                            break;

                        case RecordOption.ModifyMode:
                            string[] upFileRet1 = null;
                            if (this.chkChanageRes.Checked)
                            {
                                //上传新文件，如果m_LogoPathNull ＝ false，删除旧文件；
                                //上传文件操作；
                                HttpPostedFile PostedFile1 = Request.Files[0];
                                if (PostedFile1.FileName != "" || PostedFile1.FileName != String.Empty)
                                {
                                    //上传图片文件(大小限制：2MB)；
                                    upFileRet1 = FileOption.UpLoadImgFile(PostedFile1, 2000, Tax666Configuration.AdLinkPath, true, 180, 60);

                                    if (Int32.Parse(upFileRet1[0]) == 1)
                                    {
                                        if (m_LogoPathNull == false)
                                            FileOption.DelFile(this.lblResPath.Text);
                                    }
                                }
                                //保存修改的记录；
                                HomeLinkData linkData = new HomeLinkData();
                                DataRow row = linkData.Tables[HomeLinkData.HomeLink_Table].NewRow();

                                row[HomeLinkData.LinkName_Field] = txtName.Text;
                                row[HomeLinkData.LinkUrl_Field] = txtUrl.Text;
                                row[HomeLinkData.HomeDesc_Field] = txtDesc.Text.Trim();
                                row[HomeLinkData.LinkTypeID_Field] = Int32.Parse(this.dplHomeType.SelectedValue);
                                if (upFileRet1 == null)
                                {
                                    if (m_LogoPathNull)
                                        row[HomeLinkData.LogoPath_Field] = "";
                                    else
                                        row[HomeLinkData.LogoPath_Field] = this.lblResPath.Text;
                                }
                                else
                                {
                                    row[HomeLinkData.LogoPath_Field] = upFileRet1[5];
                                    this.lblResPath.Text = upFileRet1[5];
                                }

                                row[HomeLinkData.LinkMode_Field] = linkType;

                                //将该行添加到数据集DataSet中；
                                linkData.Tables[HomeLinkData.HomeLink_Table].Rows.Add(row);
                                linkData.AcceptChanges();
                                row[HomeLinkData.LinkID_Field] = m_LinkID;

                                retVal = (new HomeLinkFacade()).UpdateHomeLink(linkData);
                                m_Reason =Convert.ToInt32( row[HomeLinkData.Reason_Field].ToString());
                            }
                            else
                            {
                                //保存修改的记录；
                                HomeLinkData linkData = new HomeLinkData();
                                DataRow row = linkData.Tables[HomeLinkData.HomeLink_Table].NewRow();

                                row[HomeLinkData.LinkName_Field] = txtName.Text.Trim();
                                row[HomeLinkData.LinkUrl_Field] = txtUrl.Text.Trim();
                                row[HomeLinkData.HomeDesc_Field] = txtDesc.Text.Trim();
                                row[HomeLinkData.LinkTypeID_Field] = Int32.Parse(this.dplHomeType.SelectedValue);

                                if (m_LogoPathNull)
                                    row[HomeLinkData.LogoPath_Field] = "";
                                else
                                    row[HomeLinkData.LogoPath_Field] = this.lblResPath.Text;

                                row[HomeLinkData.LinkMode_Field] = linkType;

                                //将该行添加到数据集DataSet中；
                                linkData.Tables[HomeLinkData.HomeLink_Table].Rows.Add(row);
                                linkData.AcceptChanges();
                                row[HomeLinkData.LinkID_Field] = m_LinkID;

                                retVal = (new HomeLinkFacade()).UpdateHomeLink(linkData);
                                m_Reason =Convert.ToInt32(row[HomeLinkData.Reason_Field].ToString());
                            }
                            break;
                //  }
                }
            }
            return retVal;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            this.SaveHomeLink();
            if (m_Reason == 1)
                Response.Redirect(UrlBase + "Manager/AticleManage/HomeLinkManage.aspx");
            else
                EventMessage.MessageBox(1, "添加网站友情链接失败", "添加网站友情链接失败！可能原因：①已存在相同应用名称；②网络或数据库繁忙。请稍后重试！", Tax666.WebControls.Icon_Type.Error, PublicCommon.GetHomeBaseUrl("HomeLinkManage.aspx"));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlBase + "Manager/AticleManage/HomeLinkManage.aspx");
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

