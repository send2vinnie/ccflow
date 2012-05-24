using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCPort_FunctionOperate_Delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string idList = Request.QueryString["idList"];
        if (!string.IsNullOrEmpty(idList))
        {
            BP.EIP.Port_FunctionOperate Port_FunctionOperate = new BP.EIP.Port_FunctionOperate();
            XDeleteTool.DeleteByIds(Port_FunctionOperate, idList);
        }
    }
}