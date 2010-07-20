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
using BP.Sys;
using BP.En;
using BP.Web.Controls;
using BP.DA;
using BP.Web;

public partial class WF_MapDef_GroupField : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MapData md = new MapData(this.RefNo);

        //string[] titles = md.histi


        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeft("字段分组");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("RowIdx");
        this.Pub1.AddTDTitle("Label");
        this.Pub1.AddTDTitle("Default IsOpen.");
        this.Pub1.AddTREnd();



        for (int i = 0; i < 5; i++)
        {

            TextBox tbIdx = new TextBox();
            tbIdx.ID = "TB_IDX_" + i;
            tbIdx.Width = new Unit(30);

                 
            this.Pub1.AddTR();
            this.Pub1.AddTD(tbIdx);

            TextBox tb = new TextBox();
            tb.ID = "TB"+i;

            this.Pub1.AddTD(tb);
          
            this.Pub1.AddTD("<a href=''><img src='../../../Images/Btn/Delete.gif' border=0/></a>");
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTableEnd();



    }
}
