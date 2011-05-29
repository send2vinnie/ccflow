using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_WorkOpt_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Pub2.AddFieldSet("流程概况");

        this.Pub2.AddFieldSetEnd();

    }
}