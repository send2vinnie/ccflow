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
using BP.WF;
using BP.En;
using BP.Sys;

public partial class Comm_MapDef_CopyFieldFromNode :BP.Web.WebPage
{
    public string FK_Node
    {
        get
        {
            return this.Request.QueryString["FK_Node"];
        }
    }
    public string GroupField
    {
        get
        {
            return this.Request.QueryString["GroupField"];
        }
    }
    
    public string NodeOfSelect
    {
        get
        {
            string s = this.Request.QueryString["NodeOfSelect"];
            if (s == null)
            {
                Node nd = new Node(this.FK_Node);
                int fid = int.Parse(nd.FK_Flow);
                return "ND" + fid + "01";
                //return this.FK_Node.Substring(0, this.FK_Node.Length - 1) + "01";
            }
            return s;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "字段复制";
        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        BP.WF.Nodes nds = new BP.WF.Nodes(nd.FK_Flow);
        Node sNd = new Node(this.NodeOfSelect);
        MapAttrs attrs = new MapAttrs(this.NodeOfSelect);
        MapAttrs attrsCopy = new MapAttrs(this.FK_Node);

        this.Pub2.Add("<b>选择要复制的节点与分组:</b>");
        BP.Web.Controls.DDL ddl = new BP.Web.Controls.DDL();
        ddl.ID = "DDL1";
        ddl.AutoPostBack = true;

        //ddl.Attributes["onchange"] = "javascript:Go('" + this.FK_Node + "','" + ddl.ClientID + "', this.value );";
        foreach (BP.WF.Node en in nds)
        {
            if (en.No == this.FK_Node)
                continue;
            int nodeid = nd.NodeID;
            ddl.Items.Add(new ListItem(en.Name, "ND" + en.NodeID.ToString()));
        }
        ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
        this.Pub2.Add(ddl);
        ddl.SetSelectItem(this.NodeOfSelect);

        this.Pub2.AddTable("width='400px'");
        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("字段");
        this.Pub2.AddTDTitle("描述");
        this.Pub2.AddTDTitle("类型");
        this.Pub2.AddTDTitle("显示");
        this.Pub2.AddTREnd();

        GroupFields gfs = new GroupFields(this.NodeOfSelect);
        bool isHave = false;
        foreach (GroupField gf in gfs)
        {
            this.Pub2.AddTRSum();
            CheckBox cb = new CheckBox();
            cb.ID = "CB" + gf.OID;
            cb.Text = gf.Lab;
            this.Pub2.AddTD("colspan=4", cb);
            this.Pub2.AddTREnd();
            foreach (MapAttr attr in attrs)
            {
                if (gf.OID != attr.GroupID)
                    continue;
                switch (attr.KeyOfEn)
                {
                    case BP.WF.GEStartWorkAttr.CDT:
                    case BP.WF.GEStartWorkAttr.Emps:
                    case BP.WF.GEStartWorkAttr.FID:
                    case BP.WF.GEStartWorkAttr.NodeState:
                    case BP.WF.GEStartWorkAttr.OID:
                    case BP.WF.GEStartWorkAttr.RDT:
                    case BP.WF.GEStartWorkAttr.Rec:
                    case "Sender":
                    case "FK_Dept":
                    case "FK_DeptText":
                    case "MyNum":
                    case "WFLog":
                    case "WFState":
                        continue;
                    default:
                        break;
                }
                cb = new CheckBox();
                cb.ID = attr.KeyOfEn;
                cb.Text = attr.KeyOfEn;

                if (attrsCopy.Contains(MapAttrAttr.KeyOfEn, attr.KeyOfEn))
                    cb.Enabled = false;
                else
                    cb.Enabled = true;

                isHave = true;
                this.Pub2.AddTR();
                this.Pub2.AddTD(cb);
                this.Pub2.AddTD(attr.Name);
                this.Pub2.AddTD(attr.MyDataTypeStr);
                this.Pub2.AddTD(attr.UIContralType.ToString());
                this.Pub2.AddTREnd();
            }
        }
        this.Pub2.AddTableEndWithBR();

        this.Pub2.Add("到分组:");
        gfs = new GroupFields(this.FK_Node);
        ddl = new BP.Web.Controls.DDL();
        ddl.ID = "DDL_GroupField";
        ddl.Bind(gfs, GroupFieldAttr.OID, GroupFieldAttr.Lab);
        ddl.SetSelectItem(this.GroupField);
        this.Pub2.Add(ddl);

        MapData md = new MapData(this.NodeOfSelect);

        //CheckBox cb1 = new CheckBox();
        //cb1.ID = "CB_Table";
        //cb1.Text = "复制该表单据中的表格(注意如果当前表单中已经存在，就会覆盖它。)";
        //this.Pub2.Add(cb1);

        Button btn = new Button();
        if (isHave == false)
        {
            this.Pub2.AddH3("对不起该节点下没有您要复制的字段。");
            btn.Enabled = false;
        }

        btn.ID = "Btn_OK";
        btn.Text = this.ToE("Copy", " 复制 ");
        btn.Attributes["onclick"] = " return confirm('您确定要复制选择的字段到 [" + nd.Name + "]表单中吗？');";
        btn.Click += new EventHandler(btn_Click);
        this.Pub2.Add(btn);
    }
    void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Response.Redirect("CopyFieldFromNode.aspx?FK_Node=" + this.FK_Node + "&NodeOfSelect="+this.Pub2.GetDDLByID("DDL1").SelectedItemStringVal);
    }

    void btn_Click(object sender, EventArgs e)
    {

        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        Node sNd = new Node(this.NodeOfSelect);
        BP.En.Attrs attrs = sNd.HisWork.EnMap.Attrs;
        BP.En.Attrs attrsCopy = nd.HisWork.EnMap.Attrs;

        // 开始copy 分组的节点。
        GroupFields gfs = new GroupFields(this.NodeOfSelect);
        foreach (GroupField gf in gfs)
        {
            CheckBox cb = this.Pub2.GetCBByID("CB" + gf.OID);
            if (cb.Checked == false)
                continue;


            GroupField ggggg = new GroupField();
            ggggg.Lab = gf.Lab;
            ggggg.EnName = this.FK_Node;
            ggggg.Insert();

            // copy his fields. 
            MapAttrs willCopyAttrs = new MapAttrs();
            willCopyAttrs.Retrieve(MapAttrAttr.GroupID, gf.OID);
            foreach (MapAttr attr in willCopyAttrs)
            {
                MapAttr attrNew = new MapAttr();
                if (attrNew.IsExit(MapAttrAttr.FK_MapData, this.FK_Node, MapAttrAttr.KeyOfEn, attr.KeyOfEn) == true)
                    continue;

                attrNew.Copy(attr);
                attrNew.GroupID = ggggg.OID;
                attrNew.FK_MapData = this.FK_Node;
                attrNew.InsertAsNew();
            }
        }


        int GroupField = this.Pub2.GetDDLByID("DDL_GroupField").SelectedItemIntVal;
        foreach (Attr attr in attrs)
        {
            if (this.Pub2.IsExit(attr.Key) == false)
                continue;
            CheckBox cb = this.Pub2.GetCBByID(attr.Key);
            if (cb.Checked == false)
                continue;


            BP.Sys.MapAttr ma = new BP.Sys.MapAttr();
            int i = ma.Retrieve(BP.Sys.MapAttrAttr.KeyOfEn, attr.Key,
                 BP.Sys.MapAttrAttr.FK_MapData, this.NodeOfSelect);


            BP.Sys.MapAttr ma1 = new BP.Sys.MapAttr();
            bool ishavle = ma1.IsExit(BP.Sys.MapAttrAttr.KeyOfEn, attr.Key,
                 BP.Sys.MapAttrAttr.FK_MapData, this.FK_Node);

            if (ishavle)
                continue;

            ma1.Copy(ma);

            ma1.FK_MapData = this.FK_Node;
            ma1.KeyOfEn = ma.KeyOfEn;
            ma1.Name = ma.Name;
            ma1.GroupID = GroupField;
            ma1.InsertAsNew();
        }

        if (this.Pub2.IsExit("CB_Table"))
        {
            if (this.Pub2.GetCBByID("CB_Table").Checked)
            {
                MapData md1 = new MapData(this.NodeOfSelect);
                MapData md2 = new MapData(this.FK_Node);
                //md2.CellsX = md1.CellsX;
                //md2.CellsY = md1.CellsY;
                md2.Update();

                MapAttrs ma1 = md1.GenerHisTableCells;

                // 删除历史数据。
                 
                ma1.Delete(MapAttrAttr.FK_MapData, this.FK_Node + "T");
                foreach (MapAttr attr in ma1)
                {
                    MapAttr attr2 = new MapAttr();
                    attr2.Copy(attr);
                    attr2.OID = 0;
                    attr2.GroupID = 0;
                    attr2.IDX = 0;
                    attr2.FK_MapData = this.FK_Node + "T";
                    attr2.Insert();
                }
            }
        }

        this.WinClose();


        //this.WinCloseWithMsg("复制成功");

        //this.Response.Redirect("MapDef.aspx?PK=" + this.FK_Node + "&NodeOfSelect=" + this.NodeOfSelect);
    }
}

