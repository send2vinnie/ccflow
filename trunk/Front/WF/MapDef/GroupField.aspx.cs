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

        switch (this.DoType)
        {
            case "Del":
                GroupField en = new GroupField();
                en.OID = this.RefOID;
                en.Delete();
                break;
            case "DelIt":
                GroupField en1 = new GroupField();
                en1.OID = this.RefOID;
                en1.Delete();
                this.WinClose();
                return;
            case "New":
                GroupField enN = new GroupField();
                enN.EnName = this.RefNo;
                enN.RowIdx = 99;
                enN.Lab = "新建字段分组";
                enN.Insert();
                this.WinClose();
                return;
            default:
                break;
        }
 
        GroupFields ens = new GroupFields(this.RefNo);
        ens.AddEntity(new GroupField());
        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeft("字段分组");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("RowIdx");
        this.Pub1.AddTDTitle("Label");
        this.Pub1.AddTDTitle("Delete");
        this.Pub1.AddTREnd();

        foreach (GroupField en in ens)
        {
            this.Pub1.AddTR();
            TB tb = new TB();
            tb.ID = "TB_IDX_" + en.OID;
            tb.Width = new Unit(30);
            tb.Text = en.RowIdx.ToString();
            this.Pub1.AddTD(tb);

            tb = new TB();
            tb.ID = "TB_Lab_" + en.OID;
            tb.Text = en.Lab;
            tb.Columns = 40;
            this.Pub1.AddTD(tb);

            if (en.OID == 0)
                this.Pub1.AddTD("New");
            else
                this.Pub1.AddTD("<a href='GroupField.aspx?RefNo=" + this.RefNo + "&DoType=Del&RefOID=" + en.OID + "'><img src='../../../Images/Btn/Delete.gif' border=0/></a>");
            this.Pub1.AddTREnd();
        }


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
        GroupFields ens = new GroupFields(this.RefNo);
        foreach (GroupField en in ens)
        {
            en.RowIdx = this.Pub1.GetTBByID("TB_IDX_" + en.OID).TextExtInt;
            en.Lab = this.Pub1.GetTBByID("TB_Lab_" + en.OID).Text;
            en.Update();
        }
        GroupField myen = new GroupField();
        myen.RowIdx = this.Pub1.GetTBByID("TB_IDX_0").TextExtInt;
        myen.Lab = this.Pub1.GetTBByID("TB_Lab_0").Text;
        if (myen.Lab.Length > 2)
        {
            myen.EnName = this.RefNo;
            myen.Insert();
        }


        Btn btn = sender as Btn;
        if (btn.ID == "Btn_SaveAndClose")
            this.WinClose();
        else
            this.Response.Redirect("GroupField.aspx?RefNo=" + this.RefNo, true);
    }
}
