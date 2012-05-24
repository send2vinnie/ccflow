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
namespace BP.EIP.Web.Port_Dept
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
            if (this.txtName.Text.Trim().Length == 0)
            {
                strErr += "名称不能为空！\\n";
            }
            if (this.txtFullName.Text.Trim().Length == 0)
            {
                strErr += "FullName不能为空！\\n";
            }
            if (this.txtPid.Text.Trim().Length == 0)
            {
                strErr += "Pid不能为空！\\n";
            }
            if (!PageValidate.IsNumber(txtStatus.Text))
            {
                strErr += "Status格式错误！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string No = this.txtNo.Text;
            string Name = this.txtName.Text;
            string FullName = this.txtFullName.Text;
            string Pid = this.txtPid.Text;
            int Status = int.Parse(this.txtStatus.Text);

            BP.EIP.Port_Dept model = new EIP.Port_Dept();
            //BP.EIP.Model.Port_Dept model = new BP.EIP.Model.Port_Dept();
            model.No = No;
            model.Name = Name;
            model.FullName = FullName;
            model.Pid = Pid;
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
