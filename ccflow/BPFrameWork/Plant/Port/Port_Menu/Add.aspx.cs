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
namespace BP.EIP.Web.Port_Menu
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
            if (this.txtMenuNo.Text.Trim().Length == 0)
            {
                strErr += "MenuNo不能为空！\\n";
            }
            if (this.txtPid.Text.Trim().Length == 0)
            {
                strErr += "Pid不能为空！\\n";
            }
            if (this.txtFK_Function.Text.Trim().Length == 0)
            {
                strErr += "FK_Function不能为空！\\n";
            }
            if (this.txtMenuName.Text.Trim().Length == 0)
            {
                strErr += "MenuName不能为空！\\n";
            }
            if (this.txtTitle.Text.Trim().Length == 0)
            {
                strErr += "Title不能为空！\\n";
            }
            if (this.txtImg.Text.Trim().Length == 0)
            {
                strErr += "Img不能为空！\\n";
            }
            if (this.txtUrl.Text.Trim().Length == 0)
            {
                strErr += "Url不能为空！\\n";
            }
            if (this.txtPath.Text.Trim().Length == 0)
            {
                strErr += "Path不能为空！\\n";
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
            string MenuNo = this.txtMenuNo.Text;
            string Pid = this.txtPid.Text;
            string FK_Function = this.txtFK_Function.Text;
            string MenuName = this.txtMenuName.Text;
            string Title = this.txtTitle.Text;
            string Img = this.txtImg.Text;
            string Url = this.txtUrl.Text;
            string Path = this.txtPath.Text;
            int Status = int.Parse(this.txtStatus.Text);

            BP.EIP.Port_Menu model = new EIP.Port_Menu();
            model.No = No;
            model.MenuNo = MenuNo;
            model.Pid = Pid;
            model.FK_Function = FK_Function;
            model.MenuName = MenuName;
            model.Title = Title;
            model.Img = Img;
            model.Url = Url;
            model.Path = Path;
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
