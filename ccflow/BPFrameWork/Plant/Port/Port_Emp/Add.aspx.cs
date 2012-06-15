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
using BP.EIP.Enum;
namespace BP.EIP.Web.Port_Emp
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDropDownList();
            }
        }

        private void BindDropDownList()
        {
            BP.EIP.Port_Depts list = new Port_Depts();
            list.RetrieveAll();
            this.ddlFK_Dept.DataSource = list;
            this.ddlFK_Dept.DataTextField = "Name";
            this.ddlFK_Dept.DataValueField = "No";
            this.ddlFK_Dept.DataBind();

            BP.EIP.Port_Staffs staffs = new Port_Staffs();
            staffs.RetrieveAll();
            this.ddlEmp.DataSource = staffs;
            this.ddlEmp.DataTextField = "Name";
            this.ddlEmp.DataValueField = "No";
            this.ddlEmp.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtNo.Text.Trim().Length == 0)
            {
                strErr += "用户名不能为空！\\n";
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                strErr += "昵称不能为空！\\n";
            }
            if (this.txtPass.Text.Trim().Length == 0)
            {
                strErr += "密码不能为空！\\n";
            }
            //if (this.txtPID.Text.Trim().Length == 0)
            //{
            //    strErr += "PID不能为空！\\n";
            //}
            //if (this.txtPIN.Text.Trim().Length == 0)
            //{
            //    strErr += "PIN不能为空！\\n";
            //}
            if (this.txtKeyPass.Text.Trim().Length == 0)
            {
                strErr += "KeyPass不能为空！\\n";
            }
            //if (this.txtFK_Emp.Text.Trim().Length == 0)
            //{
            //    strErr += "FK_Emp不能为空！\\n";
            //}

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            //string No = Guid.NewGuid().ToString();
            string No = this.txtNo.Text;
            string Name = this.txtName.Text;
            string Pass = this.txtPass.Text;
            string FK_Dept = this.ddlFK_Dept.SelectedValue;
            string PID = this.txtPID.Text;
            string PIN = this.txtPIN.Text;
            string KeyPass = this.txtKeyPass.Text;
            string IsUSBKEY = this.chkIsUSBKEY.Checked ? "1" : "0";
            string FK_Emp = this.ddlEmp.SelectedValue;
            //int Status = this.chkStatus.Checked ? 1 : 0;

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
            model.Status = (int)AuditStatus.Passed;

            model.Insert();
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "add.aspx");

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
