using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCOA_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Channel channel = new Channel();
        channel.CheckPhysicsTable();
    }
}