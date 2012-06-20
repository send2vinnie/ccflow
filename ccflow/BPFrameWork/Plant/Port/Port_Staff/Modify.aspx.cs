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
namespace BP.EIP.Web.PORT_STAFF
{
    public partial class Modify : BasePage
    {
        protected void PAge_Load(object sender, EventArgs e)
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
            BP.EIP.Port_Staff model = new Port_Staff(No);
            this.txtNo.Text = No;
            this.txtEmpNo.Text = model.EmpNo;
            this.txtAge.Text = model.Age.ToString();
            this.txtIDCard.Text = model.IDCard;
            this.txtPhone.Text = model.Phone;
            this.txtEmail.Text = model.Email;
            this.ddlDept.SelectedValue = model.Fk_Dept;
            this.txtEmpName.Text = model.EmpName;
            this.chklstSex.SelectedValue = model.Sex.ToString();
            this.txtBirthday.Text = model.Birthday;
            this.txtAddress.Text = model.Address;
        }

        public void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtEmpNo.Text.Trim().Length == 0)
            {
                strErr += "EmpNo不能为空！\\n";
            }
            if (!PageValidate.IsDecimal(txtAge.Text))
            {
                strErr += "Age格式错误！\\n";
            }
            if (this.txtIDCard.Text.Trim().Length == 0)
            {
                strErr += "IDCard不能为空！\\n";
            }
            if (this.txtPhone.Text.Trim().Length == 0)
            {
                strErr += "Phone不能为空！\\n";
            }
            if (this.txtEmail.Text.Trim().Length == 0)
            {
                strErr += "Email不能为空！\\n";
            }
            if (this.txtEmpName.Text.Trim().Length == 0)
            {
                strErr += "EmpName不能为空！\\n";
            }
            if (this.txtBirthday.Text.Trim().Length == 0)
            {
                strErr += "Birthday不能为空！\\n";
            }
            if (this.txtAddress.Text.Trim().Length == 0)
            {
                strErr += "Address不能为空！\\n";
            }
         
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string NO = this.txtNo.Text;
            string EmpNo = this.txtEmpNo.Text;
            int Age = int.Parse(this.txtAge.Text);
            string IDCard = this.txtIDCard.Text;
            string Phone = this.txtPhone.Text;
            string Email = this.txtEmail.Text;
            string UpUser = this.CurrentUser.No;
            string Fk_Dept = this.ddlDept.SelectedValue;
            string EmpName = this.txtEmpName.Text;
            int Sex = int.Parse(chklstSex.SelectedValue);
            string Birthday = this.txtBirthday.Text;
            string Address = this.txtAddress.Text;

            BP.EIP.Port_Staff model = new BP.EIP.Port_Staff(NO);
            model.EmpNo = EmpNo;
            model.Age = Age;
            model.IDCard = IDCard;
            model.Phone = Phone;
            model.Email = Email;
            model.UpUser = UpUser;
            model.Fk_Dept = Fk_Dept;
            model.EmpName = EmpName;
            model.Sex = Sex;
            model.Birthday = Birthday;
            model.Address = Address;
            model.UpDT = XTool.Now();

            model.Update();
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
