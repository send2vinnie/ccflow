﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Web;
using BP.Sys;

public partial class WF_WorkOpt_UnPassDtl : WebPage
{
    public int WorkID
    {
        get
        {
            return int.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    public int FK_Node
    {
        get
        {
            return int.Parse(this.Request.QueryString["FK_Node"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        this.Pub2.AddFieldSet("未审核的数据");

        this.Pub2.Add("无");

        this.Pub2.AddFieldSetEnd();
    }
}
