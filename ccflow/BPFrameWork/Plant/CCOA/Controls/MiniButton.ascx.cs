using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MiniButton : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string Iconcls { get; set; }

    public bool Plain { get; set; }

    public string Url { get; set; }

    public string Text { get; set; }
}