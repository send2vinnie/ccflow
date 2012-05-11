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
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
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
            if (!PageValidate.IsDateTime(txtUpDT.Text))
            {
                strErr += "最后更新时间格式错误！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string MessageId = this.txtMessageId.Text;
            string MessageName = this.txtMessageName.Text;
            string MeaageType = this.txtMeaageType.Text;
            string Author = this.txtAuthor.Text;
            DateTime CreateTime = DateTime.Now;
            DateTime UpDT = DateTime.Now;
            bool Status = this.chkStatus.Checked;

            //Lizard.OA.Model.OA_Message model=new Lizard.OA.Model.OA_Message();
            BP.CCOA.OA_Message model = new BP.CCOA.OA_Message();
            model.No = MessageId;
            model.MessageName = MessageName;
            model.MeaageType = MeaageType;
            model.Author = Author;
            model.CreateTime = CreateTime;
            model.UpDT = UpDT;
            model.Status = Status;

            model.Insert();

            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "add.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
