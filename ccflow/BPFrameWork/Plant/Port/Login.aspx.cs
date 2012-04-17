using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Port;
using BP.Web;

public partial class Port_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Emp emp = new Emp("admin");
        //if (emp.Pass.CompareTo)
        
        WebUser.SignInOfGener(emp);
    }
}