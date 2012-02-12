using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 把流程运行到最后的节点上去，并且结束流程。
        string file = @"C:\aa\开票流程2.xls";
        string info = BP.WF.Glo.LoadFlowDataWithToSpecEndNode(file);
        this.Response.Write(info);
        return;

        // 把流程运行到指定的节点上去，并且不结束流程。
        string file1 = @"C:\aa\开票流程1.xls";
        string info1 = BP.WF.Glo.LoadFlowDataWithToSpecNode(file1);
        this.Response.Write(info1);
        return;
 
    }
}