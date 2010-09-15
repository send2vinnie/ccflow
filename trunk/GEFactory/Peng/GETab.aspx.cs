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
using BP.GE; 
using BP.En;
using BP.DA;
using BP.Web;

public partial class Peng_GETab : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GeTab1.Items.Add("how ", " this is how tab");
        this.GeTab1.Items.Add("are ", " this is are tab");
        this.GeTab1.Items.Add("you ", " this is you tab");
        this.GeTab1.Items.Add("? ",   " this is ? tab");
        
        this.Pub1.AddH4("输出-源代码");
        string msg = BP.DA.DataType.ReadTextFile(BP.SystemConfig.PathOfWebApp + "\\Peng\\"+this.PageID+".aspx.cs");
        msg = DataType.ParseText2Html(msg);
        this.Pub1.Add(msg);
    }
}
