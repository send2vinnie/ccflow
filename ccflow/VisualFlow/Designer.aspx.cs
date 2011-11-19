using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Port;
using BP.DA;

public partial class Designer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (DBAccess.IsExitsObject("WF_FlowSort") == false)
            {
                this.Response.Redirect("./WF/admin/DBInstall.aspx", true);
                return;
            }

            string sql = "SELECT count(*) FROM CN_City ";
            if (BP.DA.DBAccess.RunSQLReturnValInt(sql) == 0)
                throw new Exception("@系统需要升级，");

            Emp emp = new Emp();
            emp.No = "admin";
            if (emp.RetrieveFromDBSources() == 1)
            {
                BP.Web.WebUser.SignInOfGener(emp, true);
            }
            else
            {
                throw new Exception("admin 用户丢失，请注意大小写。");
            }
        }
        catch (Exception ex)
        {
            this.Response.Write(ex.Message + "<br>@<a href='./WF/admin/DBInstall.aspx' >点这里到系统升级界面。</a>");
            return;
        }
    }
}