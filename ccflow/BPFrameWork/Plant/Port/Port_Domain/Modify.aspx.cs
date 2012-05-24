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
namespace BP.EIP.Web.Port_Domain
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
            BP.EIP.Port_Domain model = new EIP.Port_Domain(No);
            this.lblNo.Text = model.No;
            this.txtParentId.Text = model.ParentId;
            this.txtDomainName.Text = model.DomainName;
            this.txtDescription.Text = model.Description;

        }

        public void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtParentId.Text.Trim().Length == 0)
            {
                strErr += "ParentId不能为空！\\n";
            }
            if (this.txtDomainName.Text.Trim().Length == 0)
            {
                strErr += "DomainName不能为空！\\n";
            }
            if (this.txtDescription.Text.Trim().Length == 0)
            {
                strErr += "Description不能为空！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string No = this.lblNo.Text;
            string ParentId = this.txtParentId.Text;
            string DomainName = this.txtDomainName.Text;
            string Description = this.txtDescription.Text;


            BP.EIP.Port_Domain model = new EIP.Port_Domain(No);
            model.No = No;
            model.ParentId = ParentId;
            model.DomainName = DomainName;
            model.Description = Description;

            model.Update();
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
