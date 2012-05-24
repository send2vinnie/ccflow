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
namespace BP.EIP.Web.Port_FunctionOperate
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
            BP.EIP.Port_FunctionOperate model = new EIP.Port_FunctionOperate(No);
            this.lblNo.Text = model.No;
            this.txtFK_Function.Text = model.FK_Function;
            this.txtOperateName.Text = model.OperateName;
            this.txtOperateDesc.Text = model.OperateDesc;
            this.txtControl_Name.Text = model.Control_Name;

        }

        public void btnSave_Click(object sender, EventArgs e)
        {

            string strErr = "";
            if (this.txtFK_Function.Text.Trim().Length == 0)
            {
                strErr += "所属功能ID不能为空！\\n";
            }
            if (this.txtOperateName.Text.Trim().Length == 0)
            {
                strErr += "操作名称不能为空！\\n";
            }
            if (this.txtOperateDesc.Text.Trim().Length == 0)
            {
                strErr += "功能描述不能为空！\\n";
            }
            if (this.txtControl_Name.Text.Trim().Length == 0)
            {
                strErr += "控件名称不能为空！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            string No = this.lblNo.Text;
            string FK_Function = this.txtFK_Function.Text;
            string OperateName = this.txtOperateName.Text;
            string OperateDesc = this.txtOperateDesc.Text;
            string Control_Name = this.txtControl_Name.Text;


            BP.EIP.Port_FunctionOperate model = new EIP.Port_FunctionOperate();

            model.No = No;
            model.FK_Function = FK_Function;
            model.OperateName = OperateName;
            model.OperateDesc = OperateDesc;
            model.Control_Name = Control_Name;
            model.Update();

            Lizard.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "list.aspx");

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
