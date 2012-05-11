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
namespace Lizard.GPM.Web.EIP_Menu
{
    public partial class Add : BP.Web.WebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtMenuId.Text.Trim().Length == 0)
            {
                strErr += "MenuId不能为空！\\n";
            }
            if (this.txtMenuNo.Text.Trim().Length == 0)
            {
                strErr += "MenuNo不能为空！\\n";
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
            if (this.txtPid.Text.Trim().Length == 0)
            {
                strErr += "Pid不能为空！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string MenuId = this.txtMenuId.Text;
            string MenuNo = this.txtMenuNo.Text;
            string MenuName = this.txtMenuName.Text;
            string Title = this.txtTitle.Text;
            string Img = this.txtImg.Text;
            string Url = this.txtUrl.Text;
            string Path = this.txtPath.Text;
            string Pid = this.txtPid.Text;
            bool Status = this.chkStatus.Checked;

            //Lizard.GPM.Model.EIP_Menu model = new Lizard.GPM.Model.EIP_Menu();
            BP.CCOA.EIP_Menu model = new BP.CCOA.EIP_Menu();

            model.No = MenuId;
            model.MenuNo = MenuNo;
            model.MenuName = MenuName;
            model.Title = Title;
            model.Img = Img;
            model.Url = Url;
            model.Path = Path;
            model.Pid = Pid;
            model.Status = Status;

            //Lizard.GPM.BLL.EIP_Menu bll = new Lizard.GPM.BLL.EIP_Menu();
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
