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
using BP.Web;
using BP.GE;
using BP.DA;
using BP.Sys;
using BP.Port;
using BP.Web.Controls;

public partial class Peng_Menu : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.GE.MainMenuXmls xmls = new BP.GE.MainMenuXmls();
        xmls.RetrieveAll();
        this.Pub1.Menu(xmls, "Home");

        this.Pub1.AddB("单步执行");
        this.Pub1.MenuSelfBegin();
        foreach (BP.GE.MainMenuXml xml in xmls)
        {
            if (xml.No == this.PageID)
                this.Pub1.MenuSelfItem(xml.Url, xml.Name, xml.Target);
            else
                this.Pub1.MenuSelfItemS(xml.Url, xml.Name, xml.Target);
        }
        this.Pub1.MenuSelfEnd();


        this.Pub1.AddBR();
        this.Pub1.MenuRed(xmls, this.PageID);


        this.Pub1.AddBR();
        this.Pub1.MenuWin7(xmls, "Home");

        this.Pub1.AddBR();
        this.Pub1.MenuYellow(xmls, "Home");

        this.Pub1.AddH4("输出-源代码");
        string msg = BP.DA.DataType.ReadTextFile(BP.SystemConfig.PathOfWebApp + "\\Peng\\Menu.aspx.cs");
        msg = DataType.ParseText2Html(msg);
        this.Pub1.Add(msg);

        //this.Pub1.AddBR();
        //this.Pub1.MenuRed(ens, "Home");
    }
}
