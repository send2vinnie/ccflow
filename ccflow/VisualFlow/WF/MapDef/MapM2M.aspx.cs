using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Sys;
using BP.En;
using BP.Web.Controls;
using BP.DA;
using BP.Web;

public partial class WF_MapDef_MapM2M : WebPage
{
    #region 属性
    public new string DoType
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
    public string FK_MapM2M
    {
        get
        {
            return this.Request.QueryString["FK_MapM2M"];
        }
    }
    #endregion 属性

    protected void Page_Load(object sender, EventArgs e)
    {
        MapData md = new MapData(this.FK_MapData);
        this.Title = md.Name + " - " + this.ToE("DesignFrame", "设计多选");
        switch (this.DoType)
        {
            case "Edit":
                MapM2M dtl = new MapM2M();
                if (this.FK_MapM2M == null)
                {
                    dtl.NoOfObj = "Frm";
                    dtl.FK_MapData = this.FK_MapData;
                }
                else
                {
                    dtl.MyPK = this.FK_MapM2M;
                    dtl.Retrieve();
                }
                BindEdit(md, dtl);
                break;
            default:
            case "New":
                int num = BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM Sys_MapM2M WHERE FK_MapData='" + this.FK_MapData + "'") + 1;
                MapM2M dtl1 = new MapM2M();
                dtl1.Name = this.ToE("DtlFrame", "多选") + num;
                dtl1.FK_MapData = this.FK_MapData;
                dtl1.NoOfObj = "M2M" + num;
                BindEdit(md, dtl1);
                break;
        }
    }
    void btn_Save_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        try
        {
            switch (this.DoType)
            {
                default:
                case "New":
                    MapM2M dtlN = new MapM2M();
                    dtlN = (MapM2M)this.Pub1.Copy(dtlN);
                    if (this.DoType == "New")
                    {
                        if (dtlN.IsExits)
                        {
                            this.Alert(this.ToE("Exits", "已存在编号：") + dtlN.NoOfObj);
                            return;
                        }
                    }
                    dtlN.FK_MapData = this.FK_MapData;
                    dtlN.GroupID = 0;
                    dtlN.RowIdx = 0;
                    GroupFields gfs1 = new GroupFields(this.FK_MapData);
                    if (gfs1.Count == 1)
                    {
                        GroupField gf = (GroupField)gfs1[0];
                        dtlN.GroupID = gf.OID;
                    }
                    else
                    {
                        dtlN.GroupID = this.Pub1.GetDDLByID("DDL_GroupField").SelectedItemIntVal;
                    }
                    dtlN.Insert();
                    if (btn.ID.Contains("AndClose"))
                    {
                        this.WinClose();
                        return;
                    }
                    this.Response.Redirect("MapM2M.aspx?DoType=Edit&FK_MapM2M=" + dtlN.MyPK + "&FK_MapData=" + this.FK_MapData, true);
                    break;
                case "Edit":
                    MapM2M dtl = new MapM2M(this.FK_MapM2M);
                    dtl = (MapM2M)this.Pub1.Copy(dtl);
                    if (this.DoType == "New")
                    {
                        if (dtl.IsExits)
                        {
                            this.Alert(this.ToE("Exits", "已存在编号：") + dtl.MyPK);
                            return;
                        }
                    }
                    dtl.FK_MapData = this.FK_MapData;
                    dtl.IsAutoSize = this.Pub1.GetRadioBtnByID("RB_IsAutoSize_1").Checked;

                    GroupFields gfs = new GroupFields(dtl.FK_MapData);
                    dtl.GroupID = this.Pub1.GetDDLByID("DDL_GroupField").SelectedItemIntVal;

                    if (this.DoType == "New")
                        dtl.Insert();
                    else
                        dtl.Update();

                    if (btn.ID.Contains("AndC"))
                    {
                        this.WinClose();
                        return;
                    }
                    this.Response.Redirect("MapM2M.aspx?DoType=Edit&FK_MapM2M=" + dtl.MyPK + "&FK_MapData=" + this.FK_MapData, true);
                    break;
            }
        }
        catch (Exception ex)
        {
            this.Alert(ex.Message);
        }
    }
    void btn_Del_Click(object sender, EventArgs e)
    {
        try
        {
            MapM2M dtl = new MapM2M();
            dtl.MyPK = this.FK_MapM2M;
            dtl.Delete();
            this.WinClose();
        }
        catch (Exception ex)
        {
            this.Alert(ex.Message);
        }
    }
    
    void btn_Go_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("MapM2MDe.aspx?DoType=Edit&FK_MapData=" + this.FK_MapData + "&FK_MapM2M=" + this.FK_MapM2M, true);
    }
    public void BindEdit(MapData md, MapM2M dtl)
    {
        this.Pub1.AddTable();
        //  this.Pub1.AddCaptionLeftTX("<a href='MapDef.aspx?MyPK=" + md.No + "'>" + this.ToE("Back", "返回") + ":" + md.Name + "</a> -  " + this.ToE("DtlTable", "明细表") + ":（" + dtl.Name + "）");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("ID");
        this.Pub1.AddTDTitle(this.ToE("Item", "项目"));
        this.Pub1.AddTDTitle(this.ToE("Gather", "采集"));
        this.Pub1.AddTDTitle(this.ToE("Note", "备注"));
        this.Pub1.AddTREnd();

        int idx = 1;
        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("No", "编号"));
        TB tb = new TB();
        tb.ID = "TB_NoOfObj";
        tb.Text = dtl.NoOfObj;
        if (this.DoType == "Edit")
            tb.Enabled = false;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("Desc", "描述"));
        tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = dtl.Name;
        tb.Columns = 50;
        this.Pub1.AddTD("colspan=2", tb);
        this.Pub1.AddTREnd();
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("Desc", "主体数据源")+"<font color=red>*</font>");
        tb = new TB();
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Rows = 3;
        tb.ID = "TB_DBOfObjs";
        tb.Text = dtl.DBOfObjs;
        tb.Columns = 50;

        this.Pub1.AddTD("colspan=2", tb);
        this.Pub1.AddTREnd();
        this.Pub1.AddTREnd();


        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("Desc", "分组数据源"));
        tb = new TB();
        tb.ID = "TB_DBOfGroups";
        tb.Text = dtl.DBOfGroups;
        tb.Columns = 50;
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Rows = 3;
        this.Pub1.AddTD("colspan=2", tb);
        this.Pub1.AddTREnd();
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("Width", "框架宽度"));
        tb = new TB();
        tb.ID = "TB_W";
        tb.Text = dtl.W.ToString();
        tb.ShowType = TBType.TB;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("Height", "框架高度"));
        tb = new TB();
        tb.ID = "TB_H";
        tb.ShowType = TBType.TB;
        tb.Text = dtl.H.ToString();
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("Cols", "记录呈现列数"));
        tb = new TB();
        tb.ID = "TB_Cols";
        tb.ShowType = TBType.TB;
        tb.Text = dtl.Cols.ToString();
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTDBegin("colspan=3");

        RadioBtn rb = new RadioBtn();
        rb.Text = "指定框架宽度高度";
        rb.ID = "RB_IsAutoSize_0";
        rb.GroupName = "s";
        if (dtl.IsAutoSize)
            rb.Checked = false;
        else
            rb.Checked = true;
        this.Pub1.Add(rb);

        rb = new RadioBtn();
        rb.Text = "让框架自适应大小";
        rb.ID = "RB_IsAutoSize_1";
        rb.GroupName = "s";

        if (dtl.IsAutoSize)
            rb.Checked = true;
        else
            rb.Checked = false;

        this.Pub1.Add(rb);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("Power", "权限"));
        this.Pub1.AddTDBegin("colspan=2");

        CheckBox cb = new CheckBox();
        cb.Checked = dtl.IsDelete;
        cb.Text = "是否可以删除?";
        cb.ID = "CB_IsDelete";
        this.Pub1.Add(cb);

        cb = new CheckBox();
        cb.Checked = dtl.IsDelete;
        cb.Text = "是否可以增加?";
        cb.ID = "CB_IsInsert";
        this.Pub1.Add(cb);

        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();


        GroupFields gfs = new GroupFields(md.No);
        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("ShowInGroup", "显示在分组"));
        DDL ddl = new DDL();
        ddl.ID = "DDL_GroupField";
        ddl.BindEntities(gfs, GroupFieldAttr.OID, GroupFieldAttr.Lab, false, AddAllLocation.None);
        ddl.SetSelectItem(dtl.GroupID);
        this.Pub1.AddTD("colspan=2", ddl);
        this.Pub1.AddTREnd();

        this.Pub1.AddTRSum();
        this.Pub1.AddTDBegin("colspan=4 align=center");

        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " " + this.ToE("Save", "保存") + " ";
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_SaveAndClose";
        btn.Text = " " + this.ToE("SaveAndClose", "保存并关闭") + " ";
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);

        if (this.FK_MapM2M != null)
        {
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

        this.Pub1.AddFieldSet("SQL事例");

        this.Pub1.Add("主体数据源:");
        this.Pub1.AddBR("SELECT No,Name,FK_Dept FROM Port_Emp ");

        this.Pub1.AddBR();
        this.Pub1.Add("分组数据源:");
        this.Pub1.AddBR("SELECT No,Name FROM Port_Dept ");
        this.Pub1.AddFieldSetEnd();
    }
}