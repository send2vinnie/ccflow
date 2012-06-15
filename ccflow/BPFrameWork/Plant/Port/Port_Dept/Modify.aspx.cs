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
    public partial class Modify : BasePage
    {
        private string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    id = Request.Params["id"];
                    BindDropDownList();
                    ShowInfo(id);
                }
            }
        }
        //public override void BindDropDownList()
        //{
        //    DataTable dt = new DataTable();
        //    IDepartment iDal = BP.EIP.DALFactory.DataAccess.CreateDepartment();
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        dt = iDal.GetParentDepartments(id);
        //        this.ddlPid.BindDataSource(dt, "Name", "No");
        //    }
        //}
        public override void BindDropDownList()
        {
            DataTable dt = new DataTable();
            IDepartment iDal = BP.EIP.DALFactory.DataAccess.CreateDepartment();
            dt = iDal.GetDT();
            this.ddlPid.BindDataSource(dt, "Name", "No");
        }

        private void ShowInfo(string No)
        {
            BP.EIP.Port_Dept model = new EIP.Port_Dept(No);
            this.lblNo.Text = model.No;
            this.txtName.Text = model.Name;
            this.txtFullName.Text = model.FullName;
            this.ddlPid.SelectedValue = model.Pid;
            this.txtStatus.Text = model.Status.ToString();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
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
            string No = this.lblNo.Text;
            string Name = this.txtName.Text;
            string FullName = this.txtFullName.Text;
            string Pid = this.ddlPid.SelectedValue;
            int Status = int.Parse(this.txtStatus.Text);

            BP.EIP.Port_Dept model = new EIP.Port_Dept(No);
            model.No = No;
            model.Name = Name;
            model.FullName = FullName;
            model.Pid = Pid;
            model.Status = Status;

            model.Update();

            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
