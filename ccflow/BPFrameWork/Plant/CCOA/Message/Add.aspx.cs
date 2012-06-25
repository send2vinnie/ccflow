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
namespace Lizard.OA.Web.OA_Message
{
    public partial class Add : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindCombobox();
            }
        }

        private void BindCombobox()
        {
            XBindCategoryComboTool.BindCategory(XCategory.Message, this.ddlMessageType);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtMessageName.Text.Trim().Length == 0)
            {
                strErr += "消息标题不能为空！\\n";
            }
            if (this.ddlAccessType.Text.Trim().Length == 0)
            {
                strErr += "发布类别不能为空！\\n";
            }
            if (this.txtMessageContent.Text.Trim().Length == 0)
            {
                strErr += "消息内容不能为空！\\n";
            }
            if (!PageValidate.IsDateTime(txtCreateTime.Text))
            {
                strErr += "发布时间格式错误！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string MessageId = Guid.NewGuid().ToString();
            string MessageName = this.txtMessageName.Text;
            string MeaageType = this.ddlMessageType.SelectedValue;
            string Author = CurrentUser.No;
            string accessType = this.ddlAccessType.Text;
            string messageContent = this.txtMessageContent.Text;

            string CreateTime = XTool.Now();
            string UpDT = XTool.Now();
            bool Status = true;
            //Lizard.OA.Model.OA_Message model=new Lizard.OA.Model.OA_Message();
            BP.CCOA.OA_Message model = new BP.CCOA.OA_Message();
            model.No = MessageId;
            model.MessageName = MessageName;
            model.MeaageType = MeaageType;
            model.Author = Author;
            model.CreateTime = CreateTime;
            model.UpDT = UpDT;
            model.Status = Status;
            model.AccessType = accessType;
            model.MessageContent = messageContent;

            model.Insert();

            //插入接收方信息
            XMessageTool.InsertMessageAuths(MessageId, this.hfSelects.Value);

            Lizard.Common.MessageBox.ShowAndRedirect(this, "发送成功！", "Manage.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage.aspx");
        }
    }
}
