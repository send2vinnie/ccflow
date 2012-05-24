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
namespace BP.EIP.Web.Port_Emp
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
            if (this.txtPass.Text.Trim().Length == 0)
            {
                strErr += "密码不能为空！\\n";
            }
            if (this.txtFK_Dept.Text.Trim().Length == 0)
            {
                strErr += "部门, 外键:对应物理表:Po不能为空！\\n";
            }
            if (this.txtPID.Text.Trim().Length == 0)
            {
                strErr += "PID不能为空！\\n";
            }
            if (this.txtPIN.Text.Trim().Length == 0)
            {
                strErr += "PIN不能为空！\\n";
            }
            if (this.txtKeyPass.Text.Trim().Length == 0)
            {
                strErr += "KeyPass不能为空！\\n";
            }
            if (this.txtIsUSBKEY.Text.Trim().Length == 0)
            {
                strErr += "IsUSBKEY不能为空！\\n";
            }
            if (this.txtFK_Emp.Text.Trim().Length == 0)
            {
                strErr += "FK_Emp不能为空！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string No = this.txtNo.Text;
            string Name = this.txtName.Text;
            string Pass = this.txtPass.Text;
            string FK_Dept = this.txtFK_Dept.Text;
            string PID = this.txtPID.Text;
            string PIN = this.txtPIN.Text;
            string KeyPass = this.txtKeyPass.Text;
            string IsUSBKEY = this.txtIsUSBKEY.Text;
            string FK_Emp = this.txtFK_Emp.Text;
            int Status = this.chkStatus.Checked ? 1 : 0;

            BP.EIP.Port_Emp model = new EIP.Port_Emp();
            model.No = No;
            model.Name = Name;
            model.Pass = Pass;
            model.FK_Dept = FK_Dept;
            model.PID = PID;
            model.PIN = PIN;
            model.KeyPass = KeyPass;
            model.IsUSBKEY = IsUSBKEY;
            model.FK_Emp = FK_Emp;
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
