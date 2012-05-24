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
namespace BP.EIP.Web.Port_Station
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
            BP.EIP.Port_Station model = new EIP.Port_Station(No);
            this.lblNo.Text = model.No;
            this.txtName.Text = model.Name;
            this.txtStaGrade.Text = model.StaGrade.ToString();
            this.txtDescription.Text = model.Description;
            this.txtStatus.Text = model.Status.ToString();

        }

        public void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtName.Text.Trim().Length == 0)
            {
                strErr += "名称不能为空！\\n";
            }
            if (!PageValidate.IsNumber(txtStaGrade.Text))
            {
                strErr += "类型格式错误！\\n";
            }
            if (this.txtDescription.Text.Trim().Length == 0)
            {
                strErr += "Description不能为空！\\n";
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
            int StaGrade = int.Parse(this.txtStaGrade.Text);
            string Description = this.txtDescription.Text;
            int Status = int.Parse(this.txtStatus.Text);


            BP.EIP.Port_Station model = new EIP.Port_Station(No);
            model.No = No;
            model.Name = Name;
            model.StaGrade = StaGrade;
            model.Description = Description;
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
