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
namespace Lizard.OA.Web.OA_Notice
{
    public partial class Modify : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string NoticeId = Request.Params["id"];
                    ShowInfo(NoticeId);
                }
            }
        }

        private void ShowInfo(string NoticeId)
        {
            BP.CCOA.OA_Notice model = new BP.CCOA.OA_Notice(NoticeId);
            this.txtNoticeTitle.Text = model.NoticeTitle;
            this.txtNoticeSubTitle.Text = model.NoticeSubTitle;
            this.txtNoticeType.Text = model.NoticeType;
            this.txtNoticeContent.Text = model.NoticeContent;
            this.ddlAccessType.Text = model.AccessType;
            this.txtSelected.Text = XNoticeTool.GetSelecedNames(NoticeId, model.AccessType);
            this.txtSelectedIds.Value = XNoticeTool.GetSelectedIds(NoticeId);
        }

        public void btnSave_Click(object sender, EventArgs e)
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
            if (this.txtNoticeType.Text.Trim().Length == 0)
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

            string NoticeId = Request.Params["id"];
            string NoticeTitle = this.txtNoticeTitle.Text;
            string NoticeSubTitle = this.txtNoticeSubTitle.Text;
            string NoticeType = this.txtNoticeType.Text;
            string NoticeContent = this.txtNoticeContent.Text;
            string accessType = this.ddlAccessType.Text.Trim();

            BP.CCOA.OA_Notice model = new BP.CCOA.OA_Notice(NoticeId);
            model.NoticeTitle = NoticeTitle;
            model.NoticeSubTitle = NoticeSubTitle;
            model.NoticeType = NoticeType;
            model.AccessType = accessType;
            model.NoticeContent = NoticeContent;
            model.Author = CurrentUser.No;
            model.UpDT = DateTime.Now;
            model.UpUser = BP.Web.WebUser.No;

            model.Update();

            ///删除接收方信息
            XNoticeTool.DeleteNoticeAuths(NoticeId);

            //插入新的接收方信息
            XNoticeTool.InsertNoticeAuths(NoticeId, this.txtSelectedIds.Value);

            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
