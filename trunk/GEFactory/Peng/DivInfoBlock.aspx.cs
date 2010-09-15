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

public partial class Peng_DivInfoBlock : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // this.ResponseWriteBlueMsg(" hello dear.... ");
        this.Pub1.DivInfoBlockBegin();
        this.Pub1.AddH1("信息块的显示调用:");
        this.Pub1.AddBR("请看代码示例 /GEFactory/Peng/DivInfoBlock.aspx");
        this.Pub1.DivInfoBlockEnd();

        this.Pub1.AddBR();

        this.Pub1.DivInfoBlockBegin();
        this.Pub1.AddH1("信息块的显示调用:");
        this.Pub1.AddBR("请看代码示例 /GEFactory/Peng/DivInfoBlock.aspx");
        this.Pub1.DivInfoBlockEnd();


        this.Pub1.DivInfoBlockBlue("您好，欢迎使用。 <br> DivInfoBlockBlue");
        this.Pub1.AddBR();


        this.Pub1.DivInfoBlockGreen("您好，欢迎使用。<br> DivInfoBlockGreen");
        this.Pub1.AddBR();

        this.Pub1.DivInfoBlockRed("您好，欢迎使用。<br> DivInfoBlockRed");
        this.Pub1.AddBR();


        this.Pub1.DivInfoBlockYellow("您好，欢迎使用。 <br> DivInfoBlockYellow");
        this.Pub1.AddBR();


        this.Pub1.DivInfoBlockBegin();
        //   this.Pub1.AddH1("在信息块里输出table.");
        BP.Port.Depts ens = new Depts();
        ens.RetrieveAll();

        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeft("Info blank 套用 table ");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("编号");
        this.Pub1.AddTDTitle("名称");
        this.Pub1.AddTREnd();
        foreach (BP.Port.Dept en in ens)
        {
            this.Pub1.AddTR();
            this.Pub1.AddTD(en.No);
            this.Pub1.AddTD(en.Name);
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTR();
        this.Pub1.AddTD("您好。");
        this.Pub1.AddTD("您好。");
        this.Pub1.AddTREnd();

        this.Pub1.AddTableEndWithBR();

        this.Pub1.DivInfoBlockEnd();


        this.Pub1.AddH4("输出-源代码");
        string msg = BP.DA.DataType.ReadTextFile(BP.SystemConfig.PathOfWebApp + "\\Peng\\DivInfoBlock.aspx.cs");
        msg = DataType.ParseText2Html(msg);
        this.Pub1.Add(msg);

 
    }
}
