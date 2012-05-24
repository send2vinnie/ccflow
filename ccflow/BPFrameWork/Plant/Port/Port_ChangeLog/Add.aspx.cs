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
namespace BP.EIP.Web.Port_ChangeLog
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtNo.Text.Trim().Length == 0)
            {
                strErr += "No不能为空！\\n";
            }
            if (this.txtDomain.Text.Trim().Length == 0)
            {
                strErr += "Domain不能为空！\\n";
            }
            if (this.txtChangeDigest.Text.Trim().Length == 0)
            {
                strErr += "ChangeDigest不能为空！\\n";
            }
            if (this.txtChangeDetail.Text.Trim().Length == 0)
            {
                strErr += "ChangeDetail不能为空！\\n";
            }
            if (!PageValidate.IsNumber(txtChangeType.Text))
            {
                strErr += "ChangeType格式错误！\\n";
            }
            if (this.txtUpUser.Text.Trim().Length == 0)
            {
                strErr += "UpUser不能为空！\\n";
            }
            if (this.txtUpDT.Text.Trim().Length == 0)
            {
                strErr += "UpDT不能为空！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string No = this.txtNo.Text;
            string Domain = this.txtDomain.Text;
            string ChangeDigest = this.txtChangeDigest.Text;
            string ChangeDetail = this.txtChangeDetail.Text;
            int ChangeType = int.Parse(this.txtChangeType.Text);
            string UpUser = this.txtUpUser.Text;
            string UpDT = this.txtUpDT.Text;

            BP.EIP.Port_ChangeLog model = new EIP.Port_ChangeLog();
            model.No = No;
            model.Domain = Domain;
            model.ChangeDigest = ChangeDigest;
            model.ChangeDetail = ChangeDetail;
            model.ChangeType = ChangeType;
            model.UpUser = UpUser;
            model.UpDT = UpDT;

            model.Insert();

            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "add.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
