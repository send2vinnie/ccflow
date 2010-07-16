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
using BP.En;
using BP.Web;
using BP.Web.Controls;
using BP.Web.UC;
public partial class WF_MapDef_EditFromTable : BP.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MapData md = new MapData(this.MyPK);
        md.RetrieveFromDBSources();

        
        this.Pub1.AddFieldSet("Edit cells");
        this.Pub1.Add("<Table width='100%'>");
        this.Pub1.AddTR();

        this.Pub1.AddTD("Field From");
        DDL ddl = new DDL();
        ddl.ID = "DDL_F";
        ddl.AutoPostBack = true;
        ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
        ddl.Items.Add(new ListItem("Disable.",""));
        

        Attrs attrs =  md.GenerHisMap().Attrs;
        foreach (Attr attr in attrs)
        {
            if (attr.IsRefAttr || attr.UIVisible == false)
                continue;

            ddl.Items.Add(new ListItem(attr.Desc, attr.Key));
        }
        ddl.SetSelectItem(md.CellsFrom);
        this.Pub1.AddTD(ddl);

        this.Pub1.AddTD("Columns");
        ddl =new DDL();
        ddl.ID="DDL_X";
        ddl.BindNumFromTo(1,20);
        ddl.SetSelectItem( md.CellsX.ToString().PadLeft(2,'0') );

        ddl.AutoPostBack = true;
        ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);

        this.Pub1.AddTD(ddl);



        this.Pub1.AddTD("Rows");
        ddl = new DDL();
        ddl.ID = "DDL_Y";
        ddl.BindNumFromTo(1, 20);
        ddl.SetSelectItem(md.CellsY.ToString().PadLeft(2, '0'));
        ddl.AutoPostBack = true;
        ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
        this.Pub1.AddTD(ddl);

        //Button btn = new Button();
        //btn.ID = "Btn_Save";
        //btn.Text =this.ToE("Save","");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();

        if (md.CellsFrom == null)
        {
            this.Pub1.AddFieldSetEnd();
            return;
        }

        this.Pub1.Add("<Table width='100%'>");
        //this.Pub1.AddTable();
        MapAttrs attrs1 = md.GenerHisTableCells; 
        this.Pub1.AddTR();
        foreach (MapAttr attr in attrs1)
        {
            if (attr.UIVisible==false)
                continue;

            this.Pub1.AddTDTitle("<a href=\"javascript:Edit('"+this.MyPK+"T','"+attr.OID+"','"+attr.MyDataType+"');\" >" + attr.Name+"</a>");
        }
        this.Pub1.AddTREnd();


        for (int y = 0; y < md.CellsY; y++)
        {
            this.Pub1.AddTR();
            int idx = 0;
            foreach (MapAttr attr in attrs1)
            {
                if ( attr.UIVisible==false)
                    continue;

                TB tb = new TB();
                tb.ID = "s" + idx++ + y + attr.KeyOfEn;
                tb.CssClass = "TB";
                tb.Attributes["Width"] = attr.UIWidth.ToString();
                this.Pub1.AddTD(tb);
            }
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();


        this.Pub1.AddFieldSetEnd();
    }

    void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        MapData md = new MapData(this.MyPK);
        md.CellsFrom = this.Pub1.GetDDLByID("DDL_F").SelectedItemStringVal;
        md.CellsX = this.Pub1.GetDDLByID("DDL_X").SelectedItemIntVal;
        md.CellsY = this.Pub1.GetDDLByID("DDL_Y").SelectedItemIntVal;

        MapAttrs attrs = new MapAttrs(md.No + "T");
        for (int i = attrs.Count ; i < md.CellsX; i++)
        {
            MapAttr attr = new MapAttr();
            attr.FK_MapData = this.MyPK + "T";
            attr.KeyOfEn = "F" + i.ToString();
            attr.Name = "Lab" + i;
            attr.MyDataType = BP.DA.DataType.AppString;
            attr.UIContralType = UIContralType.TB;
            attr.LGType = FieldTypeS.Normal;
            attr.UIVisible = true;
            attr.UIIsEnable = true;
            attr.UIWidth = 30;
            attr.UIIsLine = true;
            attr.MinLen = 0;
            attr.MaxLen = 600;
            attr.IDX = i;
            attr.Insert();
            attrs = new MapAttrs(md.No + "T");
        }

       int idx = 0;
        foreach (MapAttr attr in attrs)
        {
            idx++;
            if (idx > md.CellsX)
                attr.UIVisible = false;
            else
                attr.UIVisible = true;
            attr.Update();
        }
        md.Update();
        this.Response.Redirect("EditCells.aspx?MyPK="+this.MyPK, true);
    }
}
