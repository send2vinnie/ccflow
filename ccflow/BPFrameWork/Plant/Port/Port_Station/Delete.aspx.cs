using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCPort_Station_Delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string idList = Request.QueryString["idList"];
        if (!string.IsNullOrEmpty(idList))
        {
            BP.EIP.Port_Station Port_Station = new BP.EIP.Port_Station();
            XDeleteTool.DeleteByIds(Port_Station, idList);
        }
    }
}