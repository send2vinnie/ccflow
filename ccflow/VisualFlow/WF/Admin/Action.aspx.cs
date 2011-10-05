using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.WF.XML;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

public partial class WF_Admin_Action : WebPage
{
    public string FK_MapData
    {
        get
        {
            return "ND"+this.Request.QueryString["NodeID"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "设置:节点事件接口";
        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeft(this.ToE("NodeAction", "节点事件接口") + " - <a href=http://ccflow.org target=_blank >Help</a>");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle("事件");
        this.Pub1.AddTDTitle("执行内容");
        this.Pub1.AddTDTitle("事件类型");
        this.Pub1.AddTDTitle("成功提示");
        this.Pub1.AddTDTitle("错误提示");
        this.Pub1.AddTREnd();

        FrmEvents ndevs = new FrmEvents();
        ndevs.Retrieve(FrmEventAttr.FK_MapData, this.FK_MapData);

        EventLists xmls = new EventLists();
        xmls.RetrieveAll();
        int i = 1;
        bool is1 = false;
        foreach (BP.WF.XML.EventList xml in xmls)
        {
            FrmEvent nde = ndevs.GetEntityByKey(FrmEventAttr.FK_Event, xml.No) as FrmEvent;
            if (nde == null)
                nde = new FrmEvent();

            is1 = this.Pub1.AddTR(is1);
            this.Pub1.AddTDIdx(i++);
            this.Pub1.AddTD(xml.Name);
            TextBox tb = new TextBox();
            tb.ID = "TB_Doc_" + xml.No;
            tb.Columns = 40;
            tb.Text = nde.DoDoc;

            this.Pub1.AddTD(tb);
            DDL ddl = new DDL();
            ddl.BindSysEnum("EventDoType");
            ddl.ID = "DDL_EventDoType_" + xml.No;
            ddl.SetSelectItem((int)nde.HisDoType);
            this.Pub1.AddTD(ddl);

            tb = new TextBox();
            tb.ID = "TB_MsgOK_" + xml.No;
            tb.Columns = 20;
            tb.Text = nde.MsgOKString;
            this.Pub1.AddTD(tb);

            tb = new TextBox();
            tb.ID = "TB_MsgErr_" + xml.No;
            tb.Columns = 20;
            tb.Text = nde.MsgErrorString;
            this.Pub1.AddTD(tb);
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEndWithHR();
        Button btn = new Button();
        this.Pub1.Add(btn);
        btn.ID = "Btn_Save";
        btn.Text = "Save";
        btn.Click += new EventHandler(btn_Click);
    }

    void btn_Click(object sender, EventArgs e)
    {
        FrmEvents ndevs = new FrmEvents();
        ndevs.Retrieve(FrmEventAttr.FK_MapData, this.FK_MapData);

        EventLists xmls = new EventLists();
        xmls.RetrieveAll();
        int i = 1;
        foreach (EventList xml in xmls)
        {
            FrmEvent nde = ndevs.GetEntityByKey(FrmEventAttr.FK_Event, xml.No) as FrmEvent;
            if (nde == null)
                nde = new FrmEvent();
            string doc = this.Pub1.GetTextBoxByID("TB_Doc_" + xml.No).Text.Trim();
            if (doc == "" || doc == null)
            {
                if (nde.MyPK.Length > 3)
                    nde.Delete();
                continue;
            }

            nde.MyPK =   this.FK_MapData + "_" + xml.No;
            nde.DoDoc = doc;
            nde.FK_Event = xml.No;
            nde.FK_MapData = this.FK_MapData;
            nde.HisDoType = (EventDoType)this.Pub1.GetDDLByID("DDL_EventDoType_" + xml.No).SelectedItemIntVal;//.Trim();
            nde.MsgOKString = this.Pub1.GetTextBoxByID("TB_MsgOK_" + xml.No).Text;
            nde.MsgErrorString = this.Pub1.GetTextBoxByID("TB_MsgErr_" + xml.No).Text;
            nde.Save();
            
        }
    }
}