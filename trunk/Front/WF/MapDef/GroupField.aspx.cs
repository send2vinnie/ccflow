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
    public string FromGroupField
    {
        get
        {
            return this.Request.QueryString["FromGroupField"];
        }
    }
    public new string RefNo
    {
        get
        {
            string s = this.Request.QueryString["RefNo"];
            if (s == null)
                return "t";
            else
                return s;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "字段分组";
        if (this.RefOID == 0)
        {
            GroupFields mygfs = new GroupFields(this.RefNo);
            GroupField gf1 = new GroupField();
            gf1.Idx = mygfs.Count;
            gf1.Lab = "新建字段分组";
            gf1.EnName = this.RefNo;
            gf1.Insert();
            this.Response.Redirect("GroupField.aspx?RefNo=" + this.RefNo + "&RefOID=" + gf1.OID, true);
            return;
        }
        
        #region edit operation
        GroupField en = new GroupField(this.RefOID);
        this.Pub1.AddBR();
        this.Pub1.Add("<Table border=0 >");
        this.Pub1.AddTR();

        this.Pub1.AddTD("分组名称");

        TB tb = new TB();
        tb.ID = "TB_Lab_" + en.OID;
        tb.Text = en.Lab;
        tb.Columns = 50;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("<a href=\"javascript:Del('" + this.RefNo + "','" + this.RefOID + "');\"  ><img src='../../../Images/Btn/Delete.gif' border=0/></a>");
        this.Pub1.AddTREnd();

        this.Pub1.AddTRSum();
        this.Pub1.Add("<TD align=center colspan=3>");
        Btn btn = new Btn();
        btn.Text = this.ToE("Save", "保存");
        btn.ID = "Btn_Save";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);
        btn = new Btn();
        btn.Text = this.ToE("SaveAndClose", "保存并关闭");
        btn.ID = "Btn_SaveAndClose";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);


        btn = new Btn();
        btn.Text = this.ToE("NewField", "新建字段");
        btn.ID = "Btn_NewField";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);


        btn = new Btn();
        btn.Text = this.ToE("CopyField", "复制字段");
        btn.ID = "Btn_CopyField";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);


        btn = new Btn();
        btn.Text = this.ToE("Delete", "删除");
        btn.ID = "Btn_Delete";
        btn.Click += new EventHandler(btn_del_Click);
        btn.Attributes["onclick"] = "return window.confirm('您确定要删除吗？');";

        this.Pub1.Add(btn);

        this.Pub1.Add("</TD>");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
        #endregion

    }

    void btnC_Click(object sender, EventArgs e)
    {
        BP.WF.Node mynd = new BP.WF.Node(this.RefNo);
        BP.WF.Nodes nds = new BP.WF.Nodes(mynd.FK_Flow);
        foreach (BP.WF.Node nd in nds)
        {
            if ("ND" + nd.NodeID == this.RefNo)
                continue;

            GroupFields gfs = new GroupFields("ND" + nd.NodeID);
            foreach (GroupField gf in gfs)
            {
                string id = "CB_" + gf.OID;
                if (this.Pub1.GetCBByID(id).Checked == false)
                    continue;

                MapAttrs attrs = new MapAttrs();
                attrs.Retrieve(MapAttrAttr.GroupID, gf.OID);
                if (attrs.Count == 0)
                    continue;

            }
        }
    }
    void btn_del_Click(object sender, EventArgs e)
    {
        MapAttrs attrs = new MapAttrs();
        attrs.Retrieve(MapAttrAttr.FK_MapData, this.RefNo, MapAttrAttr.GroupID, this.RefOID, MapAttrAttr.UIIsEnable, "1");
        bool isHaveIt = false;
        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible = true)
            {
                isHaveIt = true;
            }
        }
        if (isHaveIt)
        {
            this.WinCloseWithMsg("删除错误: 该分组下有 " + attrs.Count + " 个字段，您需要将它们移除，您才能删除它。");
            return;
        }

        MapDtls dtls = new MapDtls();
        dtls.Retrieve(MapDtlAttr.GroupID, this.RefOID);
        if (dtls.Count > 0)
        {
            this.WinCloseWithMsg("删除错误: 该分组下有 " + dtls.Count + " 个表格，您需要将它们移除，您才能删除它。");
            return;
        }

        GroupField gf = new GroupField(this.RefOID);
        gf.Delete();
        this.WinCloseWithMsg("删除成功。");
    }

    void btn_Click(object sender, EventArgs e)
    {

        GroupField en = new GroupField(this.RefOID);
        en.Lab = this.Pub1.GetTBByID("TB_Lab_" + en.OID).Text;
        en.Update();

        Btn btn = sender as Btn;
        switch (btn.ID)
        {
            case "Btn_SaveAndClose":
                this.WinClose();
                break;
            case "Btn_NewField":
                this.Session["GroupField"] = this.RefOID;
                this.Response.Redirect("Do.aspx?DoType=AddF&MyPK=" + this.RefNo + "&GroupField=" + this.RefOID, true);
                break;
            case "Btn_CopyField":
                this.Response.Redirect("CopyFieldFromNode.aspx?FK_Node=" + this.RefNo + "&GroupField=" + this.RefOID, true);
                break;
            default:
                this.Response.Redirect("GroupField.aspx?RefNo=" + this.RefNo + "&RefOID=" + this.RefOID, true);
                break;
        }
    }
}
