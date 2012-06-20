using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCOA_AddressBook_AddrBook : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public OA_AddrBooks AddrBooks
    {
        get
        {
            OA_AddrBooks addrbooks = new OA_AddrBooks();
            addrbooks.RetrieveAllFromDBSource();

            return addrbooks;
        }
    }
}