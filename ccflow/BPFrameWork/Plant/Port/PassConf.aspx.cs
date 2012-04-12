using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;

public partial class Port_PassConf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string no = BP.Web.WebUser.No;
        BP.Port.Emp emp = new BP.Port.Emp(no);
        if (emp != null)
        {
            if (emp.Pass != txtCurPassword.Text.Trim())
            {
                ScriptAlert("输入密码错误！");
                return;
            }
            if (txtNewPassword.Text.Trim() != txtReNewPassword.Text.Trim())
            {
                ScriptAlert("两次输入的密码不同！");
                return;
            }
            emp.Pass = txtNewPassword.Text.Trim();
            emp.Update();
        }
    }

    public void ScriptAlert(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msg + "')", true);
    }
}