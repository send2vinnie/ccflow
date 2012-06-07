using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.EIP.Enum;

public partial class CCOA_Login : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string userName = txtUser.Text.Trim();
        string password = txtPass.Text.Trim();
        BP.EIP.Interface.IUser dal = BP.EIP.DALFactory.DataAccess.CreateUser();
        if (!dal.ExistsName(userName))
        {
            lblMsg.Text = "用户不存在！";
            return;
        }
        BP.EIP.Enum.StatusCode statusCode;
        string statusMessage = string.Empty;
        this.CurrentUser = dal.UserLogOn(userName, password, out statusCode, out statusMessage);
        if (statusCode == StatusCode.Success)
        {
            Response.Redirect("Home.aspx");
        }
        else
        {
            lblMsg.Text = statusMessage;
        }
    }
    //protected void btnReset_Click(object sender, EventArgs e)
    //{
    //    this.txtUser.Text = string.Empty;
    //    this.txtPass.Text = string.Empty;
    //    this.txtUser.Focus();
    //}
}