using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class EIP_NoticeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public OA_Notices NoticeList
    {
        get
        {
            BP.CCOA.OA_Notices list = new OA_Notices();
            list.RetrieveAll();

            return list;
        }
    }
}