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
    public partial class Modify : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string MessageId = (Request.Params["id"]);
                    ShowInfo(MessageId);
                }
            }
        }

        private void ShowInfo(string MessageId)
        {
            //BP.CCOA.OA_Message model = new BP.CCOA.OA_Message(MessageId);
            BP.CCOA.OA_Message model = new BP.CCOA.OA_Message(MessageId);
            //Lizard.OA.Model.OA_Message model=bll.GetModel(MessageId);
            this.lblMessageId.Text = model.No.ToString();
            this.txtMessageName.Text = model.MessageName;
            this.txtMeaageType.Text = model.MeaageType.ToString();
            this.txtAuthor.Text = model.Author.ToString();
            this.txtCreateTime.Text = model.CreateTime.ToString();
            this.txtUpDT.Text = model.UpDT.ToString();
            this.chkStatus.Checked = model.Status;

        }

        public void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtMessageName.Text.Trim().Length == 0)
            {
                strErr += "消息名称（标题）不能为空！\\n";
            }
            if (!PageValidate.IsNumber(txtMeaageType.Text))
            {
                strErr += "消息类型格式错误！\\n";
            }
            if (!PageValidate.IsNumber(txtAuthor.Text))
            {
                strErr += "发布人格式错误！\\n";
            }
            if (!PageValidate.IsDateTime(txtCreateTime.Text))
            {
                strErr += "发布时间格式错误！\\n";
            }
            if (!PageValidate.IsDateTime(txtUpDT.Text))
            {
                strErr += "最后更新时间格式错误！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string MessageId = this.lblMessageId.Text;
            string MessageName = this.txtMessageName.Text;
            string MeaageType = this.txtMeaageType.Text;
            string Author = this.txtAuthor.Text;
            DateTime CreateTime = DateTime.Now;
            DateTime UpDT = DateTime.Parse(this.txtUpDT.Text);
            bool Status = this.chkStatus.Checked;


            //Lizard.OA.Model.OA_Message model = new Lizard.OA.Model.OA_Message();
            BP.CCOA.OA_Message model = new BP.CCOA.OA_Message(MessageId);
            model.No = MessageId;
            model.MessageName = MessageName;
            model.MeaageType = MeaageType;
            model.Author = Author;
            //model.CreateTime = CreateTime;
            model.UpDT = UpDT;
            model.Status = Status;

            model.Update();
            //BP.CCOA.OA_Message bll = new BP.CCOA.OA_Message();
            //bll.Update(model);
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
