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
using BP.EIP.Interface;
namespace BP.EIP.Web.Port_Dept
{
    public partial class Add : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindDropDownList();
            }
        }

        public override void BindDropDownList()
        {
            DataTable dt = new DataTable();
            IDepartment iDal = BP.EIP.DALFactory.DataAccess.CreateDepartment();
            dt = iDal.GetDT();
            this.ddlPid.BindDataSource(dt, "Name", "No");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtCode.Text.Trim().Length == 0)
            {
                strErr += "编码不能为空！\\n";
            }
            if (this.txtName.Text.Trim().Length == 0)
            {
                strErr += "名称不能为空！\\n";
            }
            if (this.txtFullName.Text.Trim().Length == 0)
            {
                strErr += "FullName不能为空！\\n";
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
            string Code = this.txtCode.Text;
            string Name = this.txtName.Text;
            string FullName = this.txtFullName.Text;
            string Pid = this.ddlPid.SelectedValue;
            int Status = int.Parse(this.txtStatus.Text);

            BP.EIP.Port_Dept model = new EIP.Port_Dept();
            //BP.EIP.Model.Port_Dept model = new BP.EIP.Model.Port_Dept();
            model.No = Guid.NewGuid().ToString();
            model.Code = Code;
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
