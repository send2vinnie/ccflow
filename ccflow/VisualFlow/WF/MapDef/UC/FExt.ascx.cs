using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.Sys;

public partial class WF_MapDef_UC_FExt : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.AddFieldSet("处理内容");

        this.AddFieldSetEnd();

    }
}