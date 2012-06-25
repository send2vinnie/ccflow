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
    public partial class Modify : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindCombobox();
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string MessageId = (Request.Params["id"]);
                    ShowInfo(MessageId);
                }
            }
        }

        private void BindCombobox()
        {
            XBindCategoryComboTool.BindCategory(XCategory.Message, this.ddlMessageType);
        }

        private void ShowInfo(string MessageId)
        {
            //BP.CCOA.OA_Message model = new BP.CCOA.OA_Message(MessageId);
            BP.CCOA.OA_Message model = new BP.CCOA.OA_Message(MessageId);
            //Lizard.OA.Model.OA_Message model=bll.GetModel(MessageId);
            this.txtMessageName.Text = model.MessageName;
            this.ddlMessageType.SelectedValue = model.MeaageType.ToString();
            this.txtCreateTime.Text = model.CreateTime.ToString();
            this.txtMessageContent.Text = model.MessageContent;
            this.ddlAccessType.Text = model.AccessType;
            this.xtxtReader.Text = XMessageTool.GetSelecedNames(MessageId, model.AccessType);
            this.hfSelects.Value = XMessageTool.GetSelectedIds(MessageId);
        }

        public void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtMessageName.Text.Trim().Length == 0)
            {
                strErr += "消息名称（标题）不能为空！\\n";
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
            string MessageId = Request.Params["id"];
            string MessageName = this.txtMessageName.Text;
            string MeaageType = this.ddlMessageType.SelectedValue.ToString();
            string Author = CurrentUser.No;
            string UpDT = XTool.Now();
            string accessType = this.ddlAccessType.Text.Trim();
            string messageContent = this.txtMessageContent.Text.Trim();

            //Lizard.OA.Model.OA_Message model = new Lizard.OA.Model.OA_Message();
            BP.CCOA.OA_Message model = new BP.CCOA.OA_Message(MessageId);
            model.No = MessageId;
            model.MessageName = MessageName;
            model.MeaageType = MeaageType;
            model.Author = Author;
            //model.CreateTime = CreateTime;
            model.UpDT = UpDT;
            model.AccessType = accessType;
            model.MessageContent = messageContent;

            model.Update();
            //BP.CCOA.OA_Message bll = new BP.CCOA.OA_Message();
            //bll.Update(model);

            ///删除接收方信息
            XMessageTool.DeleteMessgaeAuths(MessageId);

            //插入新的接收方信息
            XMessageTool.InsertMessageAuths(MessageId, this.hfSelects.Value);
         
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
