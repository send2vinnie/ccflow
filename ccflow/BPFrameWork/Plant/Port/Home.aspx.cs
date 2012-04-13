using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BP.DA;

public partial class Port_Home : System.Web.UI.Page
{
    public Dictionary<string, DataRow[]> DataSource { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        BP.CCOA.SysUser sysUser = new BP.CCOA.SysUser();
        sysUser.CheckPhysicsTable();
        BP.CCOA.SysInfo sysInfo = new BP.CCOA.SysInfo();
        sysInfo.CheckPhysicsTable();

        BP.CCOA.SSOModule ssom = new BP.CCOA.SSOModule();
        ssom.CheckPhysicsTable();
        BP.CCOA.SSOCustomerModule ssocm = new BP.CCOA.SSOCustomerModule();
        ssocm.CheckPhysicsTable();


        if (!IsPostBack)
        {
            this.BindSystem();
        }

    }

    public void BindSystem()
    {
        string sql = "select * from Port_SysInfo order by SysGroup, SysOrder";
        DataTable table = new DataTable();
        table = DBAccess.RunSQLReturnTable(sql);

        var groups = (from row in table.AsEnumerable()
                     select row["SysGroupName"].ToString()).Distinct().ToList();

        DataSource = new Dictionary<string, DataRow[]>();

        foreach (string groupName in groups)
        {
            DataRow[] rows = (from row in table.AsEnumerable()
                              where row["SysGroupName"].ToString() == groupName
                              select row).ToArray();

            DataSource.Add(groupName, rows);
        }
    }
    protected void lbtnExit_Click(object sender, EventArgs e)
    {
        BP.Web.WebUser.Exit();
        Response.Redirect("Login.aspx");
    }
}