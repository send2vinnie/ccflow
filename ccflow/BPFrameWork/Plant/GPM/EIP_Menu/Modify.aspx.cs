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
    public partial class Modify : BP.Web.WebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    string MenuId = Request.Params["id"];
                    ShowInfo(MenuId);
                }
            }
        }

        private void ShowInfo(string MenuId)
        {
            //Lizard.GPM.BLL.EIP_Menu bll=new Lizard.GPM.BLL.EIP_Menu();
            //Lizard.GPM.Model.EIP_Menu model=bll.GetModel(MenuId);

            BP.CCOA.EIP_Menu model = new BP.CCOA.EIP_Menu();

            this.lblMenuId.Text = model.No;
            this.txtMenuNo.Text = model.MenuNo;
            this.txtMenuName.Text = model.MenuName;
            this.txtTitle.Text = model.Title;
            this.txtImg.Text = model.Img;
            this.txtUrl.Text = model.Url;
            this.txtPath.Text = model.Path;
            this.txtPid.Text = model.Pid;
            //this.chkStatus.Checked=model.Status;
        }

        public void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
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
            string MenuId = this.lblMenuId.Text;
            string MenuNo = this.txtMenuNo.Text;
            string MenuName = this.txtMenuName.Text;
            string Title = this.txtTitle.Text;
            string Img = this.txtImg.Text;
            string Url = this.txtUrl.Text;
            string Path = this.txtPath.Text;
            string Pid = this.txtPid.Text;
            bool Status = this.chkStatus.Checked;


            //Lizard.GPM.Model.EIP_Menu model=new Lizard.GPM.Model.EIP_Menu();
            BP.CCOA.EIP_Menu model = new BP.CCOA.EIP_Menu();
            model.No = MenuId;
            model.MenuNo = MenuNo;
            model.MenuName = MenuName;
            model.Title = Title;
            model.Img = Img;
            model.Url = Url;
            model.Path = Path;
            model.Pid = Pid;
            model.Status = int.Parse(Status.ToString());

            //Lizard.GPM.BLL.EIP_Menu bll=new Lizard.GPM.BLL.EIP_Menu();

            //bll.Update(model);
            model.Update();
            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
