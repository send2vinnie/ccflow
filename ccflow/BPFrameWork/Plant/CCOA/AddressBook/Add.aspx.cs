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
namespace Lizard.OA.Web.OA_AddrBook
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtAddrBookId.Text.Trim().Length == 0)
            {
                strErr += "主键Id不能为空！\\n";
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                strErr += "姓名不能为空！\\n";
            }
            if (this.txtNickName.Text.Trim().Length == 0)
            {
                strErr += "NickName不能为空！\\n";
            }
            //if (!PageValidate.IsDateTime(txtBirthday.Text))
            //{
            //    strErr += "生日格式错误！\\n";
            //}
            if (this.txtEmail.Text.Trim().Length == 0)
            {
                strErr += "电子邮件不能为空！\\n";
            }
            if (this.txtMobile.Text.Trim().Length == 0)
            {
                strErr += "手机不能为空！\\n";
            }
            if (this.txtQQ.Text.Trim().Length == 0)
            {
                strErr += "QQ不能为空！\\n";
            }
            if (this.txtWorkUnit.Text.Trim().Length == 0)
            {
                strErr += "工作单位不能为空！\\n";
            }
            if (this.txtWorkPhone.Text.Trim().Length == 0)
            {
                strErr += "工作电话不能为空！\\n";
            }
            if (this.txtWorkAddress.Text.Trim().Length == 0)
            {
                strErr += "工作地址不能为空！\\n";
            }
            if (this.txtHomePhone.Text.Trim().Length == 0)
            {
                strErr += "家庭电话不能为空！\\n";
            }
            if (this.txtHomeAddress.Text.Trim().Length == 0)
            {
                strErr += "家庭地址不能为空！\\n";
            }
            if (!PageValidate.IsNumber(txtGrouping.Text))
            {
                strErr += "分组格式错误！\\n";
            }
            if (this.txtRemarks.Text.Trim().Length == 0)
            {
                strErr += "Remarks不能为空！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string AddrBookId = this.txtAddrBookId.Text;
            string Name = this.txtName.Text;
            string NickName = this.txtNickName.Text;
            bool Sex = this.chkSex.Checked;
            //DateTime Birthday = DateTime.Parse(this.txtBirthday.Text);
            string Email = this.txtEmail.Text;
            string Mobile = this.txtMobile.Text;
            string QQ = this.txtQQ.Text;
            string WorkUnit = this.txtWorkUnit.Text;
            string WorkPhone = this.txtWorkPhone.Text;
            string WorkAddress = this.txtWorkAddress.Text;
            string HomePhone = this.txtHomePhone.Text;
            string HomeAddress = this.txtHomeAddress.Text;
            int Grouping = int.Parse(this.txtGrouping.Text);
            bool Status = this.chkStatus.Checked;
            string Remarks = this.txtRemarks.Text;

            //Lizard.OA.Model.OA_AddrBook model = new Lizard.OA.Model.OA_AddrBook();
            BP.CCOA.OA_AddrBook model = new BP.CCOA.OA_AddrBook();

            model.No = AddrBookId;
            model.Name = Name;
            model.NickName = NickName;
            model.Sex = Sex ? 1 : 0;
            //model.Birthday = Birthday;
            model.Email = Email;
            model.Mobile = Mobile;
            model.QQ = QQ;
            model.WorkUnit = WorkUnit;
            model.WorkPhone = WorkPhone;
            model.WorkAddress = WorkAddress;
            model.HomePhone = HomePhone;
            model.HomeAddress = HomeAddress;
            model.Grouping = Grouping;
            model.Status = Status ? 1 : 0;
            model.Remarks = Remarks;

            model.Insert();
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "add.aspx");

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
