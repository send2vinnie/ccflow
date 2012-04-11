using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Port_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.CCOA.SysUser sysUser = new BP.CCOA.SysUser();
        sysUser.CheckPhysicsTable();
        BP.CCOA.SysInfo sysInfo = new BP.CCOA.SysInfo();
        sysInfo.CheckPhysicsTable();

    }
}