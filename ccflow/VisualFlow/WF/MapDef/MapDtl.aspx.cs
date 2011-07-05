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
using BP.Web.Controls;
using BP.DA;
using BP.Web;

public partial class Comm_MapDef_MapDtl : WebPage
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
                int num = BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM Sys_MapDtl WHERE FK_MapData='" + this.FK_MapData + "'") + 1;
                MapDtl dtl1 = new MapDtl();
                dtl1.Name = this.ToE("DtlTable", "明细表") + num;
                dtl1.No = this.FK_MapData + "Dtl" + num;
                dtl1.PTable = this.FK_MapData + "Dtl" + num;
                BindEdit(md, dtl1);
                break;
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
                throw new Exception("er:" + this.DoType);
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
            this.Pub1.AddTD(dtl.No);
            this.Pub1.AddTD(dtl.Name);
            this.Pub1.AddTD(dtl.PTable);
            this.Pub1.AddTD("<a href='MapDtl.aspx?FK_MapData=" + this.FK_MapData + "&DoType=Edit&FK_MapDtl=" + dtl.No + "'>" + this.ToE("Edit", "编辑") + "</a>");
            this.Pub1.AddTREnd();
            continue;

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

            this.Pub1.AddTD("<a href='MapDtl.aspx?FK_MapData=" + this.FK_MapData + "&DoType=Edit&FK_MapDtl=" + dtl.No + "'>" + this.ToE("Edit", "编辑") + "</a>");
            this.Pub1.AddTREnd();
        }

        //this.Pub1.AddTRSum();
        //Button btn = new Button();
        //btn.ID = "Btn_Save";
        //btn.Text = this.ToE("Save", "保存");
        //btn.Click += new EventHandler(btn_Click);
        //this.Pub1.AddTD("colspan=5", btn);
        //this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
    void btn_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        try
        {
            switch (this.DoType)
            {
                case "New":
                    MapDtl dtlN = new MapDtl();
                    dtlN = (MapDtl)this.Pub1.Copy(dtlN);
                    if (this.DoType == "New")
                    {
                        if (dtlN.IsExits)
                        {
                            this.Alert(this.ToE("Exits", "已存在编号：") + dtlN.No);
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
                    this.Response.Redirect("MapDtl.aspx?DoType=Edit&FK_MapDtl=" + dtlN.No + "&FK_MapData=" + this.FK_MapData, true);
                    break;
                case "Edit":
                    MapDtl dtl = new MapDtl(this.FK_MapDtl);
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
                    GroupFields gfs = new GroupFields(dtl.FK_MapData);
                    if (gfs.Count > 1)
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
                    this.Response.Redirect("MapDtl.aspx?DoType=Edit&FK_MapDtl=" + dtl.No + "&FK_MapData=" + this.FK_MapData, true);
                    break;
                default:
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
            MapDtl dtl = new MapDtl();
            dtl.No = this.FK_MapDtl;
            dtl.Delete();
            this.WinClose();
            //this.Response.Redirect("MapDtl.aspx?DoType=DtlList&FK_MapData=" + this.FK_MapData, true);
        }
        catch(Exception ex)
        {
            this.Alert(ex.Message);
        }
    }
    void btn_MapExt_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("MapExt.aspx?DoType=New&FK_MapData=" + this.FK_MapDtl, true);
     }
    
    void btn_New_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("MapDtl.aspx?DoType=New&FK_MapData=" + this.FK_MapData, true);
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
      //  this.Pub1.AddCaptionLeftTX("<a href='MapDef.aspx?MyPK=" + md.No + "'>" + this.ToE("Back", "返回") + ":" + md.Name + "</a> -  " + this.ToE("DtlTable", "明细表") + ":（" + dtl.Name + "）");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("ID");
        this.Pub1.AddTDTitle(this.ToE("Item", "项目"));
        this.Pub1.AddTDTitle(this.ToE("Gather", "采集"));
        this.Pub1.AddTDTitle(this.ToE("Note","备注") );
        this.Pub1.AddTREnd();

        int idx = 1;
        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("TableEName", "表英文名称"));
        TB tb = new TB();
        tb.ID = "TB_No";
        tb.Text = dtl.No;
        if (this.DoType == "Edit")
            tb.Enabled = false;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        //this.Pub1.AddTD("英文名称全局唯一");
        this.Pub1.AddTREnd();


        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("TableName", "表中文名称"));
        tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = dtl.Name;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("XX " + this.ToE("Dtl", "明细表"));
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("PTable", "物理表名"));
        tb = new TB();
        tb.ID = "TB_PTable";
        tb.Text = dtl.PTable;

        this.Pub1.AddTD(tb);
        this.Pub1.AddTD();
        //this.Pub1.AddTD("存储数据的物理表名称");
      //  this.Pub1.AddTD("存储数据的物理表名称");
        this.Pub1.AddTREnd();


        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("TableName", "操作权限"));
        DDL ddl = new DDL();
        ddl.BindSysEnum(MapDtlAttr.DtlOpenType, (int)dtl.DtlOpenType);
        ddl.ID = "DDL_DtlOpenType";
        this.Pub1.AddTD(ddl);
        this.Pub1.AddTD();
       // this.Pub1.AddTD("用于明细表的权限控制");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD("");
        CheckBox cb = new CheckBox();
        cb.ID = "CB_IsUpdate";
        cb.Text = this.ToE("IsUpdateR", "是否可以修改行"); // "是否可以修改行";
        cb.Checked = dtl.IsUpdate;
        this.Pub1.AddTD(cb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();


        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD("");
        cb = new CheckBox();
        cb.ID = "CB_IsInsert";
        cb.Text = this.ToE("IsInsertR", "是否可以新增行"); // "是否可以新增行";
        cb.Checked = dtl.IsInsert;
        this.Pub1.AddTD(cb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD("");
        cb = new CheckBox();
        cb.ID = "CB_IsDelete";
        cb.Text = this.ToE("IsDeleteR", "是否可以删除行"); // "是否可以删除行";
        cb.Checked = dtl.IsDelete;
        this.Pub1.AddTD(cb);
        this.Pub1.AddTD();
        this.Pub1.AddTREnd();


        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD("");
        cb =new CheckBox();
        cb.ID = "CB_IsShowIdx";
        cb.Text = this.ToE("IsShowIdx", "是否显示序号列"); //"是否显示序号列";
        cb.Checked = dtl.IsShowIdx;
        this.Pub1.AddTD("colspan=2", cb);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD("");
        cb = new CheckBox();
        cb.ID = "CB_IsShowSum";
        cb.Text = this.ToE("IsShowSum", "是否合计行");// "是否合计行";
        cb.Checked = dtl.IsShowSum;
        this.Pub1.AddTD("colspan=2", cb);
        this.Pub1.AddTREnd();


        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD("");
        cb = new CheckBox();
        cb.ID = "CB_IsEnablePass";
        cb.Text = this.ToE("IsEnablePass", "是否起用审核字段？");// "是否合计行";
        cb.Checked = dtl.IsEnablePass;
        this.Pub1.AddTD("colspan=2", cb);
        this.Pub1.AddTREnd();


        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("RowsOfList", "初始化行数"));
        tb = new TB();
        tb.ID = "TB_RowsOfList";
        tb.Attributes["class"] = "TBNum";
        tb.TextExtInt = dtl.RowsOfList;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("WhenOverSize", "越出处理"));
        ddl = new DDL();
        ddl.ID = "DDL_WhenOverSize";
        ddl.BindSysEnum(MapDtlAttr.WhenOverSize, (int)dtl.HisWhenOverSize);
        this.Pub1.AddTD(ddl);
       // this.Pub1.AddTD("行数显示方式");
        this.Pub1.AddTD(); // ("行数显示方式");
        this.Pub1.AddTREnd();

        GroupFields gfs = new GroupFields(md.No);
        if (gfs.Count > 1)
        {
            this.Pub1.AddTR1();
            this.Pub1.AddTDIdx(idx++);
            this.Pub1.AddTD( this.ToE("ShowInGroup","显示在分组") );
            ddl = new DDL();
            ddl.ID = "DDL_GroupField";
            ddl.BindEntities(gfs, GroupFieldAttr.OID, GroupFieldAttr.Lab, false, AddAllLocation.None);
            ddl.SetSelectItem(dtl.GroupID);
            this.Pub1.AddTD("colspan=2",ddl);
            this.Pub1.AddTREnd();
        }
        if (gfs.Count > 1)
            this.Pub1.AddTR();
        else
            this.Pub1.AddTR1();

        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD();
        cb = new CheckBox();
        cb.ID = "CB_IsCopyNDData";
        cb.Text =this.ToE("IsCopyNDData","是允许从上一个节点Copy数据");
        cb.Checked = dtl.IsCopyNDData;
        this.Pub1.AddTD("colspan=2 nowarp=true ", cb);
        this.Pub1.AddTREnd();


        //#region 字段分组
        //this.Pub1.AddTR();
        //this.Pub1.AddTD("字段分组");
        //DDL ddlGroup = new DDL();
        //ddlGroup.ID = "DDL_GroupID";
        //GroupFields gfs = new GroupFields(this.FK_MapData);
        //ddlGroup.Bind(gfs, GroupFieldAttr.OID, GroupFieldAttr.Lab);
        //if (mapAttr.GroupID == 0)
        //    mapAttr.GroupID = this.GroupField;

        //ddlGroup.SetSelectItem(mapAttr.GroupID);
        //this.Pub1.AddTD(ddlGroup);
        //this.Pub1.AddTD("修改隶属分组");
        //this.Pub1.AddTREnd();
        //#endregion 字段分组


        //this.Pub1.AddTR();
        //this.Pub1.AddTD();
        //cb = new CheckBox();
        //cb.ID = "CB_IsReadonly";
        //cb.Text = "所有的列不可编辑时它Readonly.（系统自动计算）";
        //cb.Enabled = false;
        //cb.Checked = dtl.IsReadonly;
        //this.Pub1.AddTD("colspan=2 nowarp=true ", cb);
        //this.Pub1.AddTREnd();

        this.Pub1.AddTRSum();
        this.Pub1.AddTD("");
        this.Pub1.AddTDBegin("colspan=3 align=center");

        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " "+this.ToE("Save","保存")+" ";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_SaveAndClose";
        btn.Text = " " + this.ToE("SaveAndClose", "保存并关闭") + " ";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);

        if (this.FK_MapDtl != null)
        {
            //btn = new Button();
            //btn.ID = "Btn_D";
            //btn.Text = this.ToE("DesignSheet", "设计表单"); // "设计表单";
            //btn.Click += new EventHandler(btn_Go_Click);
            //this.Pub1.Add(btn);

            btn = new Button();
            btn.ID = "Btn_Del";
            btn.Text = this.ToE("Del", "删除"); // "删除";
            btn.Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";
            btn.Click += new EventHandler(btn_Del_Click);
            this.Pub1.Add(btn);


            btn = new Button();
            btn.ID = "Btn_New";
            btn.Text = this.ToE("New", "新建"); // "删除";
            btn.Click += new EventHandler(btn_New_Click);
            this.Pub1.Add(btn);

            btn = new Button();
            btn.ID = "Btn_MapExt";
            btn.Text = this.ToE("MapExt", "扩展设置"); // "删除";
             
            btn.Click += new EventHandler(btn_MapExt_Click);
            this.Pub1.Add(btn);
        }
     
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }
  
}
