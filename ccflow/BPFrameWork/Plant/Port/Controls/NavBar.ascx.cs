using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Port_Controls_NavBar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //_menuurl = "~/DataUser/navbarmenu.txt";
        }
    }

    private string _menuurl;
    public string MenuUrl
    {
        get
        {
            return Page.ResolveUrl(_menuurl); ;
        }
        set
        {
            _menuurl = value;
        }
    }
}