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
    public partial class Add : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtNO.Text.Trim().Length == 0)
            {
                strErr += "NO不能为空！\\n";
            }
            if (this.txtEMPNO.Text.Trim().Length == 0)
            {
                strErr += "EMPNO不能为空！\\n";
            }
            if (!PageValidate.IsDecimal(txtAGE.Text))
            {
                strErr += "AGE格式错误！\\n";
            }
            if (this.txtIDCARD.Text.Trim().Length == 0)
            {
                strErr += "IDCARD不能为空！\\n";
            }
            if (this.txtPHONE.Text.Trim().Length == 0)
            {
                strErr += "PHONE不能为空！\\n";
            }
            if (this.txtEMAIL.Text.Trim().Length == 0)
            {
                strErr += "EMAIL不能为空！\\n";
            }
            if (this.txtUPUSER.Text.Trim().Length == 0)
            {
                strErr += "UPUSER不能为空！\\n";
            }
            if (this.txtFK_DEPT.Text.Trim().Length == 0)
            {
                strErr += "FK_DEPT不能为空！\\n";
            }
            if (this.txtEMPNAME.Text.Trim().Length == 0)
            {
                strErr += "EMPNAME不能为空！\\n";
            }
            if (!PageValidate.IsDecimal(txtSEX.Text))
            {
                strErr += "SEX格式错误！\\n";
            }
            if (this.txtBIRTHDAY.Text.Trim().Length == 0)
            {
                strErr += "BIRTHDAY不能为空！\\n";
            }
            if (this.txtADDRESS.Text.Trim().Length == 0)
            {
                strErr += "ADDRESS不能为空！\\n";
            }
            if (this.txtCREATEDATE.Text.Trim().Length == 0)
            {
                strErr += "CREATEDATE不能为空！\\n";
            }
            if (this.txtUPDT.Text.Trim().Length == 0)
            {
                strErr += "UPDT不能为空！\\n";
            }
            if (!PageValidate.IsDecimal(txtSTATUS.Text))
            {
                strErr += "STATUS格式错误！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string NO = this.txtNO.Text;
            string EMPNO = this.txtEMPNO.Text;
            int AGE = int.Parse(this.txtAGE.Text);
            string IDCARD = this.txtIDCARD.Text;
            string PHONE = this.txtPHONE.Text;
            string EMAIL = this.txtEMAIL.Text;
            string UPUSER = this.txtUPUSER.Text;
            string FK_DEPT = this.txtFK_DEPT.Text;
            string EMPNAME = this.txtEMPNAME.Text;
            int SEX = int.Parse(this.txtSEX.Text);
            string BIRTHDAY = this.txtBIRTHDAY.Text;
            string ADDRESS = this.txtADDRESS.Text;
            string CREATEDATE = this.txtCREATEDATE.Text;
            string UPDT = this.txtUPDT.Text;
            int STATUS = int.Parse(this.txtSTATUS.Text);

            //BP.EIP.Model.PORT_STAFF model = new BP.EIP.Model.PORT_STAFF();
            BP.EIP.Port_Staff model = new Port_Staff();
            model.No = Guid.NewGuid().ToString();
            model.EmpNo = EMPNO;
            model.Age = AGE;
            model.IDCard = IDCARD;
            model.Phone = PHONE;
            model.Email = EMAIL;
            model.UpUser = CurrentUser.No;
            model.Fk_Dept = FK_DEPT;
            model.EmpName = EMPNAME;
            model.Sex = SEX;
            model.Birthday = BIRTHDAY;
            model.Address = ADDRESS;
            model.CreateDate = XTool.Now();
            model.UpDT = XTool.Now();
            model.Status = STATUS;

            //BP.EIP.BLL.PORT_STAFF bll = new BP.EIP.BLL.PORT_STAFF();
            //bll.Add(model);
            model.Insert();
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "add.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
