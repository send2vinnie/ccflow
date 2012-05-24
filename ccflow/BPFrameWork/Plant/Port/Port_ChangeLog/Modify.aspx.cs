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
    public partial class Modify : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string No = Request.Params["id"];
                    ShowInfo(No);
                }
            }
        }

        private void ShowInfo(string No)
        {
            BP.EIP.Port_ChangeLog model = new EIP.Port_ChangeLog(No);
            this.lblNo.Text = model.No;
            this.txtDomain.Text = model.Domain;
            this.txtChangeDigest.Text = model.ChangeDigest;
            this.txtChangeDetail.Text = model.ChangeDetail;
            this.txtChangeType.Text = model.ChangeType.ToString();
            this.txtUpUser.Text = model.UpUser;
            this.txtUpDT.Text = model.UpDT;

        }

        public void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
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
            string No = this.lblNo.Text;
            string Domain = this.txtDomain.Text;
            string ChangeDigest = this.txtChangeDigest.Text;
            string ChangeDetail = this.txtChangeDetail.Text;
            int ChangeType = int.Parse(this.txtChangeType.Text);
            string UpUser = this.txtUpUser.Text;
            string UpDT = this.txtUpDT.Text;


            BP.EIP.Port_ChangeLog model = new EIP.Port_ChangeLog(No);
            model.No = No;
            model.Domain = Domain;
            model.ChangeDigest = ChangeDigest;
            model.ChangeDetail = ChangeDetail;
            model.ChangeType = ChangeType;
            model.UpUser = UpUser;
            model.UpDT = UpDT;

            model.Update();

            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
