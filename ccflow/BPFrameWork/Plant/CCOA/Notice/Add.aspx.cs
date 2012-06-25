using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Lizard.Common;
using LTP.Accounts.Bus;
using System.IO;
namespace Lizard.OA.Web.OA_Notice
{
    public partial class Add : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDropDownList();
            }
        }

        private void BindDropDownList()
        {
            XBindCategoryComboTool.BindCategory(XCategory.Notice, this.ddlNoticeType);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtNoticeTitle.Text.Trim().Length == 0)
            {
                strErr += "通告标题不能为空！\\n";
            }
            if (this.txtNoticeSubTitle.Text.Trim().Length == 0)
            {
                strErr += "副标题不能为空！\\n";
            }
            if (this.ddlNoticeType.Text.Trim().Length == 0)
            {
                strErr += "通告类型不能为空！\\n";
            }
            if (this.ddlAccessType.Text.Trim().Length == 0)
            {
                strErr += "发布类别不能为空！\\n";
            }
            if (this.txtNoticeContent.Text.Trim().Length == 0)
            {
                strErr += "通告内容不能为空！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string NoticeId = Guid.NewGuid().ToString();
            string NoticeTitle = this.txtNoticeTitle.Text;
            string NoticeSubTitle = this.txtNoticeSubTitle.Text;
            string NoticeType = this.ddlNoticeType.SelectedValue.ToString();
            string accessType = this.ddlAccessType.Text.Trim();
            string NoticeContent = this.txtNoticeContent.Text;
            string Author = CurrentUser.No;
            DateTime CreateTime = DateTime.Now;

            //Lizard.OA.Model.OA_Notice model = new Lizard.OA.Model.OA_Notice();
            BP.CCOA.OA_Notice model = new BP.CCOA.OA_Notice();
            model.No = NoticeId;
            model.NoticeTitle = NoticeTitle;
            model.NoticeSubTitle = NoticeSubTitle;
            model.NoticeType = NoticeType;
            model.AccessType = accessType;
            model.NoticeContent = NoticeContent;
            model.Author = Author;
            model.CreateTime = XTool.Now();
            model.Clicks = 0;
            model.IsRead = 0;
            model.UpDT = XTool.Now();
            model.UpUser = BP.Web.WebUser.No;
            model.Status = 1;

            model.Insert();

            XNoticeTool.InsertNoticeAuths(NoticeId, this.txtSelectedIds.Value);

            //保存附件
            if (this.FileUpload1.HasFile)
            {
                string fileName = this.FileUpload1.FileName;
                string saveFilePath = XAttachmentTool.UploadFile(this.FileUpload1, Request.PhysicalApplicationPath, this, Server.HtmlEncode(fileName));
                string attachNo = string.Empty;
                XAttachmentTool.InsertAttachment(fileName, fileName, saveFilePath, CurrentUser.No, out attachNo);

                //插入公告附件表
                BP.CCOA.OA_NoticeAttach noticeAttach = new BP.CCOA.OA_NoticeAttach();
                noticeAttach.No = Guid.NewGuid().ToString();
                noticeAttach.Notice_Id = NoticeId;
                noticeAttach.Accachment_Id = attachNo;
                noticeAttach.Insert();
            }

            //BP.CCOA.OA_Notice bll = new BP.CCOA.OA_Notice();
            //bll.Add(model);
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "MyNotice.aspx");

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyNotice.aspx");
        }
    }
}
