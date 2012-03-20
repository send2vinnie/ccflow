using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DevDemo_Demo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        BP.Sys.FrmLine line = new BP.Sys.FrmLine();
        line.X1 = 11;

        line.Insert();

    }
}