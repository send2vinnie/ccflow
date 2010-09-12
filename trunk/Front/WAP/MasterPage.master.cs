using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.Web;
using BP.DA;
using BP.WF;
using BP.En;

public partial class WAP_MasterPage : System.Web.UI.MasterPage
{
 
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AddHeader("P3P", "CP=CAO PSA OUR");
        return;


        //BP.WF.XML.ToolBars ens = new BP.WF.XML.ToolBars();
        //ens.RetrieveAll();

        //this.Top.Add("<div align=center ><Table><TR>");
        //this.Top.Add("<TD align=right nowarp=true ><a href='Tools.aspx' >您好:" + BP.Web.WebUser.No + "</a>&nbsp;</TD>");
        //string dotype = "";
        //foreach (BP.WF.XML.ToolBar en in ens)
        //{
        //    if (en.No == this.DoType)
        //    {
        //        this.Top.Add("<TD nowrap=true><img src='" + en.Img + "' border='0' ><b>" + en.Name + "</b></TD>");
        //    }
        //    else
        //    {
        //        this.Top.Add("<TD nowrap=true><a href='" + en.Url + "' target='_self' title='" + en.Title + "' ><img src='" + en.Img + "' border='0' >" + en.Name + "</a></TD>");
        //    }
        //}

        //this.Top.Add("<TD width='20%' ></TD>");
        //this.Top.Add("</TR>");
        //this.Top.Add("</Table></div><hr width='80%'>");
    }
}
