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
    public int FK_Node
    {
        get
        {
            return int.Parse(this.Request.QueryString["NodeID"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "设置:节点事件接口";
        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeftTX(this.ToE("NodeAction", "节点事件接口") +" - <a href=http://ccflow.org target=_blank >Help</a>");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle("事件");
        this.Pub1.AddTDTitle("执行内容");
        this.Pub1.AddTDTitle("内容");
        this.Pub1.AddTDTitle("成功提示");
        this.Pub1.AddTDTitle("错误提示");
        this.Pub1.AddTREnd();

        NDEvents ndevs = new NDEvents();
        ndevs.Retrieve(NDEventAttr.FK_Node, this.FK_Node);

        EventLists xmls = new EventLists();
        xmls.RetrieveAll();
        int i = 1;
        bool is1 = false;
        foreach (BP.WF.XML.EventList xml in xmls)
        {
            NDEvent nde = ndevs.GetEntityByKey(NDEventAttr.FK_Event, xml.No) as NDEvent;
            if (nde == null)
                nde = new NDEvent();

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
            ddl.SetSelectItem( (int)nde.HisDoType );
            this.Pub1.AddTD(ddl);

            tb = new TextBox();
            tb.ID = "TB_MsgOK_" + xml.No;
            tb.Columns = 20;
            this.Pub1.AddTD(tb);

            tb = new TextBox();
            tb.ID = "TB_MsgErr_" + xml.No;
            tb.Columns = 20;
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
        NDEvents ndevs = new NDEvents();
        ndevs.Retrieve(NDEventAttr.FK_Node, this.FK_Node);

        EventLists xmls = new EventLists();
        xmls.RetrieveAll();
        int i = 1;
        foreach (EventList xml in xmls)
        {
            NDEvent nde = ndevs.GetEntityByKey(NDEventAttr.FK_Event, xml.No) as NDEvent;
            if (nde == null)
                nde = new NDEvent();
            string doc = this.Pub1.GetTextBoxByID("TB_Doc_" + xml.No).Text.Trim();
            if (doc == "" || doc == null)
            {
                if (nde.MyPK.Length > 3)
                    nde.Delete();
                continue;
            }

            nde.MyPK = this.FK_Node + "_" + xml.No;
            nde.DoDoc = doc;
            nde.FK_Event = xml.No;
            nde.FK_Node = this.FK_Node;
            nde.HisDoType = (EventDoType)this.Pub1.GetDDLByID("DDL_EventDoType_" + xml.No).SelectedItemIntVal;//.Trim();
            nde.MsgOK = this.Pub1.GetTextBoxByID("TB_MsgOK_" + xml.No).Text;
            nde.MsgError = this.Pub1.GetTextBoxByID("TB_MsgErr_" + xml.No).Text;
            nde.Save();
            
        }
    }
}