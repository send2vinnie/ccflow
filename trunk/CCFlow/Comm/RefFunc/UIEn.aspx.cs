﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Comm_RefFunc_UIEn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string enName = this.Request.QueryString["EnName"];
        if (enName == null || enName == "")
            enName = this.Request.QueryString["EnsName"];

        if (enName.Contains(".") == false)
        {
            this.Response.Redirect("SysMapEn.aspx?EnsName=" + enName + "&PK=" + this.Request["PK"], true);
            return;
        }
    }
}