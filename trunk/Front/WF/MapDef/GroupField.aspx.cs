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
        this.Title = "编辑字段分组";

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

        switch (this.DoType)
        {
            case "Del":
                MapAttrs attrs = new MapAttrs();
                attrs.Retrieve(MapAttrAttr.FK_MapData, this.RefNo, MapAttrAttr.GroupID, this.RefOID);
                bool isHaveIt = false;
                foreach (MapAttr attr in attrs)
                {
                    if (attr.UIVisible = true)
                    {
                        attr.GroupID = 0 ;
                        attr.Update();
                        isHaveIt = true;
                    }
                }
                if (isHaveIt)
                {
                    this.Response.Redirect(this.Request.RawUrl, true);
                    return;
                }

                if (attrs.Count >= 1)
                {
                    this.Pub1.AddMsgInfo("删除错误:", "该分组下有 " + attrs.Count + " 个字段，您需要将它们移除，您才能删除它。");
                    return;
                }

                GroupField endel = new GroupField();
                endel.OID = this.RefOID;
                endel.Delete();
                this.WinClose();
                return;
            case "DelIt":
                GroupField en1 = new GroupField();
                en1.OID = this.RefOID;
                en1.Delete();
                this.WinClose();
                return;
            default:
                break;
        }

        GroupField en = new GroupField(this.RefOID);

        this.Pub1.AddBR();
        this.Pub1.Add("<Table border=0 >");
        this.Pub1.AddTR();
      
        this.Pub1.AddTD("分组名称");

        TB  tb = new TB();
        tb.ID = "TB_Lab_" + en.OID;
        tb.Text = en.Lab;
        tb.Columns = 50;
        this.Pub1.AddTD(tb);
            this.Pub1.AddTD("<a href='GroupField.aspx?RefNo=" + this.RefNo + "&DoType=Del&RefOID=" + en.OID + "'><img src='../../../Images/Btn/Delete.gif' border=0/></a>");
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

        this.Pub1.Add("</TD>");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }

    void btn_Click(object sender, EventArgs e)
    {
        GroupField en = new GroupField(this.RefOID);
        en.Lab = this.Pub1.GetTBByID("TB_Lab_" + en.OID).Text;
        en.Update();


        Btn btn = sender as Btn;
        if (btn.ID == "Btn_SaveAndClose")
            this.WinClose();
        else
            this.Response.Redirect("GroupField.aspx?RefNo=" + this.RefNo + "&RefOID=" + this.RefOID, true);
    }
}
