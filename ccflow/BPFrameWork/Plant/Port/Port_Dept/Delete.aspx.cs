using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.EIP;
using BP.CCOA;

public partial class CCPort_Dept_Delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string idList = Request.QueryString["idList"];
        if (!string.IsNullOrEmpty(idList))
        {
            Port_Dept Port_Dept = new Port_Dept();
            XDeleteTool.DeleteByIds(Port_Dept, idList);
        }
    }
}