using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.Sys;
using BP.En;
using BP.En;
using BP.Web;
using BP.Web.UC;

public partial class Comm_MapDef_SFTableEditData : BP.Web.WebPage
{
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public string IDX
    {
        get
        {
            return this.Request.QueryString["IDX"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["EnPK"] != null)
        {
            GENoName en = new GENoName(this.RefNo, "");
            en.No = this.Request.QueryString["EnPK"];
            en.Delete();
        }

        this.Title = "编辑表数据";
        this.BindSFTable();
    }
    public void BindSFTable()
    {
        SFTable sf = new SFTable(this.RefNo);
        this.Ucsys1.AddTable();
        this.Ucsys1.AddCaptionLeftTX("编辑:" + sf.Name);
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDTitle("编号");
        this.Ucsys1.AddTDTitle("名称");
        this.Ucsys1.AddTDTitle("操作");
        this.Ucsys1.AddTREnd();

        GENoNames ens = new GENoNames(sf.No, sf.Name);
        QueryObject qo = new QueryObject(ens);
        this.Ucsys2.BindPageIdx(qo.GetCOUNT(), 10, this.PageIdx, "SFTableEditData.aspx?RefNo=" + this.RefNo);
        qo.DoQuery("No", 10, this.PageIdx, false);

        foreach (GENoName en in ens)
        {
            this.Ucsys1.AddTR();
            this.Ucsys1.AddTDDesc(en.No);
            TextBox tb = new TextBox();
            tb.ID = "TB_" + en.No;
            tb.Text = en.Name;
            this.Ucsys1.AddTD(tb);
            this.Ucsys1.AddTD("<a href=\"javascript:Del('"+this.RefNo+"','"+this.PageIdx+"','"+en.No+"')\" >删除</a>");
            this.Ucsys1.AddTREnd();
        }

        GENoName newen = new GENoName(sf.No, sf.Name);
        this.Ucsys1.AddTR();
        this.Ucsys1.AddTDDesc("新记录");
        TextBox tb1 = new TextBox();
        tb1.ID = "TB_Name";
        tb1.Text = newen.Name;
        this.Ucsys1.AddTD(tb1);
        Button btn = new Button();
        btn.Text = "保存";
        btn.Click += new EventHandler(btn_Click);
        this.Ucsys1.AddTD(btn);
        this.Ucsys1.AddTREnd();
        this.Ucsys1.AddTableEnd();

        //this.Ucsys3.AddTable();
        //this.Ucsys3.AddTRSum();
        //this.Ucsys3.AddTD("编号");
        //this.Ucsys3.AddTD("名称");
        //this.Ucsys3.AddTD("");
        //this.Ucsys3.AddTREnd();

        //GENoName newen = new GENoName(sf.No, sf.Name);
        //this.Ucsys3.AddTRSum();
        //this.Ucsys3.AddTD(newen.GenerNewNo);
        //TextBox tbn = new TextBox();
        //tbn.ID = "TB_Name";

        //this.Ucsys3.AddTD(tbn);
        //Button btn = new Button();
        //btn.Text = "增加";
        //btn.Click += new EventHandler(btn_Click);
        //this.Ucsys3.AddTD(btn);
        //this.Ucsys3.AddTREnd();
        //this.Ucsys3.AddTableEnd();
    }

    void btn_Click(object sender, EventArgs e)
    {
        //批量保存数据。
        GENoNames ens = new GENoNames(this.RefNo, "sdsd");
        QueryObject qo = new QueryObject(ens);
        qo.DoQuery("No", 10, this.PageIdx, false);
        foreach (GENoName myen in ens)
        {
            string no = myen.No;
            string name1 = this.Ucsys1.GetTextBoxByID("TB_" + myen.No).Text;
            if (name1 == "")
                continue;
            BP.DA.DBAccess.RunSQL("update " + this.RefNo + " set Name='" + name1 + "' WHERE no='" + no + "'");
        }


        BP.En.GENoName en = new GENoName(this.RefNo, "sd");
        string name = this.Ucsys1.GetTextBoxByID("TB_Name").Text.Trim();
        if (name.Length > 0)
        {
            en.Name = name;
            en.No = en.GenerNewNo;
            en.Insert();
            this.Response.Redirect("SFTableEditData.aspx?RefNo=" + this.RefNo + "&PageIdx="+this.PageIdx, true);
        }
    }
}
