using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCPort_ChangeLog_Delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string idList = Request.QueryString["idList"];
        if (!string.IsNullOrEmpty(idList))
        {
            BP.EIP.Port_ChangeLog Port_ChangeLog = new BP.EIP.Port_ChangeLog();
            XDeleteTool.DeleteByIds(Port_ChangeLog, idList);
        }
    }
}