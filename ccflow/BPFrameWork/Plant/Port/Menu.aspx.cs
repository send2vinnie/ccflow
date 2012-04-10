using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Port;
using BP.Port.Xml;

public partial class Port_Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.Port.Xml.Models xmls = new BP.Port.Xml.Models();
        xmls.RetrieveAll();

        foreach (Model xml in xmls)
        {
            var url = xml.Url;
            var img = xml.Img;
        }
    }
}