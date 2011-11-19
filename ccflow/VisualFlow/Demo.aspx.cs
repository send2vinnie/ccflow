using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;

public partial class _Demo : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Flow.DoLoadFlowTemplate("02", @"D:\ccflow\VisualFlow\Temp\标准制修订.xml");
        return;
        

        FlowSort fl = new FlowSort();
        fl.No = "09";
        fl.Name = "adccc";
        fl.Insert();
        fl.Delete();
    }
}