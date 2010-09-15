using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.XML;


public partial class App_Port_BarOfTop : System.Web.UI.Page
{
    public void Bind()
    {
        BP.Sys.Xml.ShortKeys sks = new BP.Sys.Xml.ShortKeys();
        sks.RetrieveAll();

        this.UCSys1.Add("<div id='top_nav'>");
        foreach (BP.Sys.Xml.ShortKey sk in sks)
        {
            //this.UCSys1.Add("<a href='" + sk.URL + "' target='left' ><img src='" + sk.Img + "' border=0 />" + sk.Name + "</a>");
            this.UCSys1.Add("<a href='" + sk.URL + "' target='" + sk.Target + "' ><img src='./Style/img/" + sk.No + ".gif' border=0 />" + sk.Name + "</a>");
        }
        this.UCSys1.AddDivEnd();
    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            this.Bind();
        }
        catch
        {
            this.Bind();
        }
        return;

        this.UCSys1.Add("<a href='../Home.aspx' target='mainfrm' ><img src='../Images/Home.gif' border=0 />主页</a>");
        this.UCSys1.Add("<a href='../List.aspx?StateOfHD=1' target='mainfrm' ><img src='../Images/Home.gif' border=0 />定税审核</a>");
        this.UCSys1.Add("<a href='../../Comm/Port/ChangeSystem.aspx' target='mainfrm' ><img src='../../Images/System/Power.gif' border=0 />切换系统</a>");
        return;

    }
}
