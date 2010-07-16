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
using BP.Web.Controls;
using BP.DA;
using BP.Web;

public partial class Comm_MapDef_MapDtl : WebPage
{
    #region 属性
    public string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public string FK_MapData
    {
        get
        {
            return this.Request.QueryString["FK_MapData"];
        }
    }
    public string FK_MapDtl
    {
        get
        {
            return this.Request.QueryString["FK_MapDtl"];
        }
    }
    #endregion 属性

    protected void Page_Load(object sender, EventArgs e)
    {
        MapData md = new MapData(this.FK_MapData);
        this.Title = md.Name + " - " + this.ToE("DesignDtl", "设计明细");
        switch (this.DoType)
        {
            case "DtlList":
                BindDtlList(md);
                break;
            case "New":
            case "Edit":
                MapDtl dtl = new MapDtl();
                if (this.FK_MapDtl == null)
                {
                    dtl.No = this.FK_MapData + "Dtl";
                }
                else
                {
                    dtl.No = this.FK_MapDtl;
                    dtl.Retrieve();
                }
                BindEdit(md, dtl);
                break;
            default:
                throw new Exception("er" + this.DoType);
        }
    }
    public void BindDtlList(MapData md)
    {
        MapDtls dtls = new MapDtls(md.No);
        if (dtls.Count == 0)
        {
            this.Response.Redirect("MapDtl.aspx?DoType=New&FK_MapData=" + this.FK_MapData + "&sd=sd", true);
            return;
        }

        if (dtls.Count == 1)
        {
            MapDtl d = (MapDtl)dtls[0];
            this.Response.Redirect("MapDtl.aspx?DoType=Edit&FK_MapData=" + this.FK_MapData + "&FK_MapDtl=" + d.No, true);
            return;
        }

        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeftTX("<a href='MapDef.aspx?MyPK=" + this.MyPK + "'>" + this.ToE("Back", "返回") + ":" + md.Name + "</a> - <a href='MapDtl.aspx?DoType=New&FK_MapData=" + this.FK_MapData + "&sd=sd'><img src='../../Images/Btn/New.gif' border=0/>" + this.ToE("New", "新建") + "</a>");

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("IDX");
        this.Pub1.AddTDTitle(this.ToE("No", "编号"));
        this.Pub1.AddTDTitle(this.ToE("Name", "名称"));
        this.Pub1.AddTDTitle(this.ToE("PTable", "物理表"));
        this.Pub1.AddTDTitle(this.ToE("Oper", "操作"));
        this.Pub1.AddTREnd();

        TB tb = new TB();
        int i = 0;
        foreach (MapDtl dtl in dtls)
        {
            i++;
            this.Pub1.AddTR();
            this.Pub1.AddTDIdx(i);

            tb = new TB();
            tb.ID = "TB_No_" + dtl.No;
            tb.Text = dtl.No;
            this.Pub1.AddTD(tb);

            tb = new TB();
            tb.ID = "TB_Name_" + dtl.No;
            tb.Text = dtl.Name;
            this.Pub1.AddTD(tb);

            tb = new TB();
            tb.ID = "TB_PTable_" + dtl.No;
            tb.Text = dtl.PTable;
            this.Pub1.AddTD(tb);

            this.Pub1.AddTD("<a href='MapDtl.aspx?FK_MapData=" + this.FK_MapData + "&DoType=Edit&FK_MapDtl=" + dtl.No + "'>" + this.ToE("Edit", "编辑") + "</a>|<a href=''>" + this.ToE("Design", "设计") + "</a>");
            this.Pub1.AddTREnd();
        }

        //this.Pub1.AddTRSum();
        //this.Pub1.AddTD("新建");

        //tb = new TB();
        //tb.ID = "TB_No";
        //this.Pub1.AddTD(tb);

        //tb = new TB();
        //tb.ID = "TB_Name";
        //this.Pub1.AddTD(tb);

        //tb = new TB();
        //tb.ID = "TB_PTable";
        //this.Pub1.AddTD(tb);

        //this.Pub1.AddTD( );
        //this.Pub1.AddTREnd();

        this.Pub1.AddTRSum();
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = this.ToE("Save", "保存");
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.AddTD("colspan=5", btn);
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }

    void btn_Click(object sender, EventArgs e)
    {
        try
        {
            switch (this.DoType)
            {
                case "New":
                case "Edit":
                    MapDtl dtl = new MapDtl();
                    dtl = (MapDtl)this.Pub1.Copy(dtl);
                     
                    if (this.DoType == "New")
                    {
                        if (dtl.IsExits)
                        {
                            this.Alert(this.ToE("Exits", "已存在编号：") + dtl.No);
                            return;
                        }
                    }
                    dtl.FK_MapData = this.FK_MapData;
                    if (this.DoType == "New")
                        dtl.Insert();
                    else
                        dtl.Update();

                    this.Response.Redirect("MapDtl.aspx?DoType=Edit&FK_MapDtl=" + dtl.No+"&FK_MapData="+this.FK_MapData, true);
                    break;
                default:
                    break;
            }
        }
        catch(Exception ex)
        {
            this.Alert(ex.Message);
        }
    }
    void btn_Del_Click(object sender, EventArgs e)
    {
        try
        {
            MapDtl dtl = new MapDtl();
            dtl.No = this.FK_MapDtl;
            dtl.Delete();
            this.Response.Redirect("MapDtl.aspx?DoType=DtlList&FK_MapData=" + this.FK_MapData, true);
        }
        catch(Exception ex)
        {
            this.Alert(ex.Message);
        }
    }
    void btn_Go_Click(object sender, EventArgs e)
    {
        MapDtl dtl = new MapDtl(this.FK_MapDtl);
        dtl.IntMapAttrs();
        this.Response.Redirect("MapDtlDe.aspx?DoType=Edit&FK_MapData=" + this.FK_MapData + "&FK_MapDtl=" + this.FK_MapDtl, true);
    }

    public void BindEdit(MapData md,MapDtl dtl)
    {
        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeftTX("<a href='MapDef.aspx?MyPK=" + md.No + "'>" + this.ToE("Back", "返回") + ":" + md.Name + "</a> -  " + this.ToE("DtlTable", "明细表") + ":（" + dtl.Name + "）");

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle(this.ToE("Item", "项目"));
        this.Pub1.AddTDTitle(this.ToE("Gather", "采集"));
        this.Pub1.AddTDTitle(this.ToE("Note","备注") );
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("PTable", "物理表名"));
        TB tb = new TB();
        tb.ID = "TB_PTable";
        tb.Text = dtl.PTable ;

        //if (this.DoType == "Edit")
        //    tb.Enabled = false;

        this.Pub1.AddTD(tb);
        this.Pub1.AddTDBigDoc(this.FK_MapData + "Dtl");
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("TableEName", "表英文名称"));
        tb = new TB();
        tb.ID = "TB_No";
        tb.Text = dtl.No;
        if (this.DoType == "Edit")
            tb.Enabled = false;

        this.Pub1.AddTD(tb);
        this.Pub1.AddTDBigDoc( this.FK_MapData + "Dtl");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("TableName", "表中文名称"));
        tb = new TB();
        tb.ID = "TB_Name";
        tb.Text =dtl.Name;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("XX " + this.ToE("Dtl", "明细表"));
        this.Pub1.AddTREnd();



        this.Pub1.AddTR();
        this.Pub1.AddTD(this.ToE("TableName", "操作权限"));
        DDL ddl = new DDL();
        ddl.BindSysEnum(MapDtlAttr.DtlOpenType, (int)dtl.DtlOpenType);
        ddl.ID = "DDL_DtlOpenType";
        this.Pub1.AddTD(ddl);
        this.Pub1.AddTD("用于明细表的权限控制");
        this.Pub1.AddTREnd();

        //this.Pub1.AddTR();
        //this.Pub1.AddTD( this.ToE("PTable") );
        // tb = new TB();
        //tb.ID = "TB_PTable";
        //tb.Text = dtl.PTable ;
        //this.Pub1.AddTD(tb);
        //this.Pub1.AddTDBigDoc(this.ToE("PTableD") );
        //this.Pub1.AddTREnd();


        this.Pub1.AddTRSum();
        this.Pub1.AddTDBegin("colspan=3 align=center");

        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " "+this.ToE("Save","保存")+" ";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);

        if (this.FK_MapDtl != null)
        {
            btn = new Button();
            btn.ID = "Btn_D";
            btn.Text = this.ToE("DesignSheet", "设计表单"); // "设计表单";
            btn.Click += new EventHandler(btn_Go_Click);
            this.Pub1.Add(btn);

            btn = new Button();
            btn.ID = "Btn_Del";
            btn.Text = this.ToE("Del", "删除"); // "删除";
            btn.Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";
            btn.Click += new EventHandler(btn_Del_Click);
            this.Pub1.Add(btn);
        }
        this.Pub1.AddTDEnd();

        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
  
}
