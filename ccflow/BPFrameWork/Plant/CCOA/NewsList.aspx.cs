using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class EIP_NewsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public OA_Newss NewsList
    {
        get
        {
            BP.CCOA.OA_Newss list = new OA_Newss();
            list.RetrieveAll();

            return list;
        }
    }
}