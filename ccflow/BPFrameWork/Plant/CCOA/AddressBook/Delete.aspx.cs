using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCOA_AddrBook_Delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string idList = Request.QueryString["idList"];
        if (!string.IsNullOrEmpty(idList))
        {
            BP.CCOA.OA_AddrBook OA_AddrBook = new OA_AddrBook();
            XDeleteTool.DeleteByIds(OA_AddrBook, idList);
        }
    }
}