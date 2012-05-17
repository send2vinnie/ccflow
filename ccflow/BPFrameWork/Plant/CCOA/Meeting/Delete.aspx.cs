using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCOA_Meeting_Delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string idList = Request.QueryString["idList"];
        if (!string.IsNullOrEmpty(idList))
        {
            BP.CCOA.OA_Meeting OA_Meeting = new OA_Meeting();
            XDeleteTool.DeleteByIds(OA_Meeting, idList);
        }
    }
}