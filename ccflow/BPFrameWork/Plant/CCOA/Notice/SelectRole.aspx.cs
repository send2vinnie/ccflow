using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.EIP;

public partial class CCOA_Notice_SelectRole : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public Port_Stations PortStations
    {
        get
        {
            Port_Stations stations = new Port_Stations();
            stations.RetrieveAll();
            return stations;
        }
    }
}