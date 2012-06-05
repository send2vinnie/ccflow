using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CCOA_Controls_Bottom : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private string _onlinenum;
    public string OnLineNum
    {
        get
        {
            return string.IsNullOrEmpty(_onlinenum) ? "0" : _onlinenum;
        }
        set { _onlinenum = value; }
    }
}