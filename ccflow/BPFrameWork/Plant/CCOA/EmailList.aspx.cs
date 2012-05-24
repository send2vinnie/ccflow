using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class EIP_EmailList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public OA_Emails EmailList
    {
        get
        {
            BP.CCOA.OA_Emails list = new OA_Emails();
            list.RetrieveAll();

            return list;
        }
    }
}