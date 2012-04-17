using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.DA;

public partial class Port_UserSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindData();
        }
    }

    public void BindData()
    {
        string sysSql = "select i.No, i.SysName, SysDescription from Port_SysInfo i order by i.SysOrder";
        System.Data.DataTable sysTable = new System.Data.DataTable();
        sysTable = DBAccess.RunSQLReturnTable(sysSql);
        this.rptSysList.DataSource = sysTable;
        this.rptSysList.DataBind();

        string userSql = "select u.SysID, u.UserName, u.Password from Port_SysUser u where u.SysUserNo = '" + BP.Web.WebUser.No + "'";
        System.Data.DataTable userTable = new System.Data.DataTable();
        userTable = DBAccess.RunSQLReturnTable(userSql);

        foreach (RepeaterItem ri in rptSysList.Items)
        {
            string id = ((HiddenField)ri.FindControl("hideSysNo")).Value;
            System.Data.DataRow[] rows = userTable.Select("SysID = '" + id + "'");
            if (rows.Length > 0)
            {
                ((TextBox)ri.FindControl("txtUserName")).Text = rows[0]["UserName"].ToString();
                ((TextBox)ri.FindControl("txtPassword")).Attributes["value"] = rows[0]["Password"].ToString();
            }
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string deleteSql = "delete from Port_SysUser where SysUserNo = '" + BP.Web.WebUser.No + "'";

        BP.CCOA.SysUser sysUser = new BP.CCOA.SysUser();

        DBAccess.RunSQL(deleteSql);

        foreach (RepeaterItem ri in rptSysList.Items)
        {
            string id = ((HiddenField)ri.FindControl("hideSysNo")).Value;
            string username = ((TextBox)ri.FindControl("txtUserName")).Text.Trim();
            string password = ((TextBox)ri.FindControl("txtPassword")).Text.Trim();

            sysUser.No = Guid.NewGuid().ToString();
            sysUser.UserName = username;
            sysUser.Password = password;
            sysUser.SysID = id;
            sysUser.SysUserNo = BP.Web.WebUser.No;

            sysUser.Insert();
        }
        this.BindData();
    }
}