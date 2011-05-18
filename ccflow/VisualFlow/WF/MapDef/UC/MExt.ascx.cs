﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.Sys;

public partial class WF_MapDef_UC_MExt : BP.Web.UC.UCBase3
{
    public string FK_MapData
    {
        get
        {
            return this.Request.QueryString["FK_MapData"];
        }
    }
    public string ExtType
    {
        get
        {
            string s = this.Request.QueryString["ExtType"];
            if (s == "")
                s = null;
            return s;
        }
    }
    public string Lab = null;
    /// <summary>
    /// BindLeft
    /// </summary>
    public void BindLeft()
    {
        MapExtXmls fss = new MapExtXmls();
        fss.RetrieveAll();

        this.Pub1.AddFieldSet("表单扩展设置-<a href='MapExt.aspx?FK_MapData=" + this.FK_MapData + "'>帮助</a>");
        this.Pub1.AddUL();
        foreach (MapExtXml fs in fss)
        {
            if (this.ExtType == fs.No)
            {
                this.Lab = fs.Name;
                this.Pub1.AddLiB("MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + fs.No, fs.Name);
            }
            else
                this.Pub1.AddLi("MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + fs.No, fs.Name);
        }
        this.Pub1.AddULEnd();
        this.Pub1.AddFieldSetEnd();  
    }
    public void EditAutoFullDtl()
    {
        this.Pub2.AddFieldSet("<a href='?ExtType=" + this.ExtType + "&MyPK=" + this.MyPK + "&FK_MapData=" + this.FK_MapData + "'>返回</a> -设置自动填充明细表");
        MapExt myme = new MapExt(this.MyPK);
        MapDtls dtls = new MapDtls(myme.FK_MapData);
        string[] strs = myme.Tag1.Split('$');

        this.Pub2.AddTable("border=0  width='300px' ");
        bool is1 = false;
        foreach (MapDtl dtl in dtls)
        {
            is1 = this.AddTR(is1);
            TextBox tb = new TextBox();
            tb.ID = "TB_" + dtl.No;
            tb.Columns = 50;
            tb.TextMode = TextBoxMode.MultiLine;

            foreach (string s in strs)
            {
                if (s == null)
                    continue;

                if (s.Contains(dtl.No + ":") == false)
                    continue;

                string[] ss = s.Split(':');
                tb.Text = ss[1];
            }

            this.Pub2.AddTDBegin();
            this.Pub2.AddB("&nbsp;&nbsp;" + dtl.Name + "-明细表");
            this.Pub2.AddBR();
            this.Pub2.Add(tb);
            this.Pub2.AddTDEnd();
            this.Pub2.AddTREnd();
        }
        this.Pub2.AddTableEnd();

        Button mybtn = new Button();
        mybtn.ID = "Btn_Save";
        mybtn.Text = "保存";
        mybtn.Click += new EventHandler(mybtn_SaveAutoFullDtl_Click);
        this.Pub2.Add(mybtn);

        mybtn = new Button();
        mybtn.ID = "Btn_Cancel";
        mybtn.Text = "取消";
        mybtn.Click += new EventHandler(mybtn_SaveAutoFullDtl_Click);
        this.Pub2.Add(mybtn);
        this.Pub2.AddFieldSetEnd();
    }

    public void EditAutoJL()
    {
        this.Pub2.AddFieldSet("<a href='?ExtType=" + this.ExtType + "&MyPK=" + this.MyPK + "&FK_MapData=" + this.FK_MapData + "'>返回</a> -设置级连菜单");
        MapExt myme = new MapExt(this.MyPK);
        MapAttrs attrs = new MapAttrs(myme.FK_MapData);
        string[] strs = myme.Tag.Split('$');

        this.Pub2.AddTable("border=0 width='300px' ");
        bool is1 = false;
        foreach (MapAttr attr in attrs)
        {
            if (attr.LGType == FieldTypeS.Normal)
                continue;
            if (attr.UIIsEnable == false)
                continue;

            is1 = this.AddTR(is1);
            TextBox tb = new TextBox();
            tb.ID = "TB_" + attr.KeyOfEn;
            tb.Columns = 50;
            tb.Rows = 2;
            tb.TextMode = TextBoxMode.MultiLine;

            foreach (string s in strs)
            {
                if (s == null)
                    continue;

                if (s.Contains(attr.KeyOfEn + ":") == false)
                    continue;

                string[] ss = s.Split(':');
                tb.Text = ss[1];
            }
            this.Pub2.AddTDBegin();
            this.Pub2.AddB("&nbsp;&nbsp;" + attr.Name + "-字段");
            this.Pub2.AddBR();
            this.Pub2.Add(tb);
            this.Pub2.AddTDEnd();
            this.Pub2.AddTREnd();
        }

        this.Pub2.AddTableEnd();

        Button mybtn = new Button();
        mybtn.ID = "Btn_Save";
        mybtn.Text = "保存";
        mybtn.Click += new EventHandler(mybtn_SaveAutoFullJilian_Click);
        this.Pub2.Add(mybtn);

        mybtn = new Button();
        mybtn.ID = "Btn_Cancel";
        mybtn.Text = "取消";
        mybtn.Click += new EventHandler(mybtn_SaveAutoFullJilian_Click);
        this.Pub2.Add(mybtn);
        this.Pub2.AddFieldSetEnd();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "表单扩展设置";
        switch (this.DoType)
        {
            case "Del":
                MapExt mm = new MapExt();
                mm.MyPK = this.MyPK;
                mm.Delete();
                this.Response.Redirect("MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType, true);
                return;
            case "EditAutoJL":
            default:
                break;
        }

        this.BindLeft();
        if (this.ExtType == null)
        {
            this.Pub2.AddFieldSet("帮助");
            this.Pub2.Add(BP.DA.DataType.ReadTextFile2Html(BP.SystemConfig.PathOfData + "\\HelpDesc\\MapExt_" + BP.Web.WebUser.SysLang + ".txt"));
            this.Pub2.AddFieldSetEnd();
            return;
        }

        MapExts mes = new MapExts();
        switch (this.ExtType)
        {
            case MapExtXmlList.ActiveDDL: //联动菜单.
                if (this.MyPK != null || this.DoType == "New")
                {
                    Edit_ActiveDDL();
                    return;
                }
                mes.Retrieve(MapExtAttr.ExtType, this.ExtType,
                    MapExtAttr.FK_MapData, this.FK_MapData);
                this.MapExtList(mes);
                break;
            case MapExtXmlList.AutoFull: //自动完成.
                if (this.DoType == "EditAutoJL")
                {
                    this.EditAutoJL();
                    return;
                }

                if (this.DoType == "EditAutoFullDtl")
                {
                    this.EditAutoFullDtl();
                    return;
                }
                

                if (this.MyPK != null || this.DoType == "New")
                {
                    Edit_AutoFull();
                    return;
                }
                mes.Retrieve(MapExtAttr.ExtType, this.ExtType,
                    MapExtAttr.FK_MapData, this.FK_MapData);
                this.MapExtList(mes);
                break;
            case MapExtXmlList.InputCheck: //输入检查.
                if (this.MyPK != null || this.DoType == "New")
                {
                    Edit_InputCheck();
                    return;
                }
                mes.Retrieve(MapExtAttr.ExtType, this.ExtType,
                    MapExtAttr.FK_MapData, this.FK_MapData);
                this.MapExtList(mes);
                break;
            case MapExtXmlList.PopVal: //联动菜单.
                if (this.MyPK != null || this.DoType == "New")
                {
                    Edit_PopVal();
                    return;
                }
                mes.Retrieve(MapExtAttr.ExtType, this.ExtType,
                    MapExtAttr.FK_MapData, this.FK_MapData);
                this.MapExtList(mes);
                break;
            default:
                break;
        }
    }
    void mybtn_SaveAutoFullDtl_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn.ID.Contains("Cancel"))
        {
            this.Response.Redirect("MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType + "&MyPK=" + this.MyPK, true);
            return;
        }

        MapExt myme = new MapExt(this.MyPK);
        MapDtls dtls = new MapDtls(myme.FK_MapData);
        string info = "";
        foreach (MapDtl dtl in dtls)
        {
            TextBox tb = this.Pub2.GetTextBoxByID("TB_" + dtl.No);
            if (tb.Text.Trim() == "")
                continue;

            try
            {
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(tb.Text);
            }
            catch (Exception ex)
            {
                this.Alert("SQL ERROR: " + ex.Message);
                return;
            }
            info += "$" + dtl.No + ":" + tb.Text;
        }

        myme.Tag1 = info;
        myme.Update();
        this.Response.Redirect("MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType + "&MyPK=" + this.MyPK, true);
    }

    void mybtn_SaveAutoFullJilian_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn.ID.Contains("Cancel"))
        {
            this.Response.Redirect("MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType + "&MyPK=" + this.MyPK, true);
            return;
        }


        MapExt myme = new MapExt(this.MyPK);

        MapAttrs attrs = new MapAttrs(myme.FK_MapData);
        string info = "";
        foreach (MapAttr attr in attrs)
        {
            if (attr.LGType == FieldTypeS.Normal)
                continue;

            if (attr.UIIsEnable == false)
                continue;

            TextBox tb = this.Pub2.GetTextBoxByID("TB_" + attr.KeyOfEn);
            if (tb.Text.Trim() == "")
                continue;

            try
            {
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(tb.Text);
            }
            catch (Exception ex)
            {
                this.Alert("SQL ERROR: " + ex.Message);
                return;
            }
            info += "$" + attr.KeyOfEn + ":" + tb.Text;
        }

        myme.Tag = info;
        myme.Update();
        this.Response.Redirect("MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType + "&MyPK=" + this.MyPK, true);
    }

    public void Edit_PopVal()
    {
        MapExt me = null;
        if (this.MyPK == null)
        {
            me = new MapExt();
            this.Pub2.AddFieldSet("新建:" + this.Lab);
        }
        else
        {
            me = new MapExt(this.MyPK);
            this.Pub2.AddFieldSet("编辑:" + this.Lab);
        }

        me.FK_MapData = this.FK_MapData;

        this.Pub2.AddTable("border=0");
        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("项目");
        this.Pub2.AddTDTitle("采集");
        this.Pub2.AddTDTitle("说明");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTD("作用字段");
        BP.Web.Controls.DDL ddl = new BP.Web.Controls.DDL();
        ddl.ID = "DDL_Oper";
        MapAttrs attrs = new MapAttrs(this.FK_MapData);
        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            if (attr.UIIsEnable == false)
                continue;

            if (attr.UIContralType == UIContralType.TB)
            {
                ddl.Items.Add(new ListItem(attr.KeyOfEn + " - " + attr.Name, attr.KeyOfEn));
                continue;
            }
        }
        ddl.SetSelectItem(me.AttrOfOper);
        this.Pub2.AddTD(ddl);
        this.Pub2.AddTD("处理pop窗体的字段.");
        this.Pub2.AddTREnd();


        this.Pub2.AddTR();
        this.Pub2.AddTD("URL");
        TextBox tb = new TextBox();
        tb.ID = "TB_Doc";
        tb.Text = me.Doc;
        tb.Columns = 50;
        this.Pub2.AddTD("colspan=2", tb);
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTD("colspan=3", "请输入一个弹出窗口的url,当操作员关闭后返回值就会被放在里面.");
        this.Pub2.AddTREnd();


        this.Pub2.AddTRSum();
        Button btn = new Button();
        btn.ID = "BtnSave";
        btn.Text = "Save";
        btn.Click += new EventHandler(btn_SaveInputCheck_Click);
        this.Pub2.AddTD("colspan=3", btn);
        this.Pub2.AddTREnd();
        this.Pub2.AddTableEnd();
        this.Pub2.AddFieldSetEnd();
    }
    public void Edit_InputCheck()
    {
        MapExt me = null;
        if (this.MyPK == null)
        {
            me = new MapExt();
            this.Pub2.AddFieldSet("新建:" + this.Lab);
        }
        else
        {
            me = new MapExt(this.MyPK);
            this.Pub2.AddFieldSet("编辑:" + this.Lab);
        }
        me.FK_MapData = this.FK_MapData;

        this.Pub2.AddTable("border=0  width='300px' ");
        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("项目");
        this.Pub2.AddTDTitle("采集");
        this.Pub2.AddTDTitle("说明");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTD("验证字段:");
        BP.Web.Controls.DDL ddl = new BP.Web.Controls.DDL();
        ddl.ID = "DDL_Oper";
        MapAttrs attrs = new MapAttrs(this.FK_MapData);
        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            if (attr.UIIsEnable == false)
                continue;

            if (attr.UIContralType == UIContralType.TB)
            {
                ddl.Items.Add(new ListItem(attr.KeyOfEn + " - " + attr.Name, attr.KeyOfEn));
                continue;
            }
        }
        ddl.SetSelectItem(me.AttrOfOper);
        this.Pub2.AddTD(ddl);
        this.Pub2.AddTD("");
        this.Pub2.AddTREnd();


        this.Pub2.AddTR();
        this.Pub2.AddTD("验证方式:");
        ddl = new BP.Web.Controls.DDL();
        ddl.ID = "DDL_CheckWay";
        InputCheckXmls xmls = new InputCheckXmls();
        xmls.RetrieveAll();
        foreach (InputCheckXml xml in xmls)
        {
            ddl.Items.Add(new ListItem(xml.Name, xml.No));
        }

        ddl.Items.Add(new ListItem("*****自己验证方式*******", "all"));

        ddl.SetSelectItem(me.AttrOfOper);
        this.Pub2.AddTD(ddl);
        this.Pub2.AddTD("");
        this.Pub2.AddTREnd();


        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("colspan=3", "处理内容");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        TextBox tb = new TextBox();
        tb.ID = "TB_Doc";
        tb.Text = me.Doc;
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Rows = 5;
        tb.Columns = 40;
        this.Pub2.AddTD("colspan=3", tb);
        this.Pub2.AddTREnd();

        this.Pub2.AddTRSum();
        Button btn = new Button();
        btn.ID = "BtnSave";
        btn.Text = "Save";
        btn.Click += new EventHandler(btn_SaveInputCheck_Click);
        this.Pub2.AddTD("colspan=3", btn);
        this.Pub2.AddTREnd();
        this.Pub2.AddTableEnd();
        this.Pub2.AddFieldSetEnd();
    }
    public void Edit_AutoFull()
    {
        MapExt me = null;
        if (this.MyPK == null)
        {
            me = new MapExt();
            this.Pub2.AddFieldSet("新建:" + this.Lab);
        }
        else
        {
            me = new MapExt(this.MyPK);
            this.Pub2.AddFieldSet("编辑:" + this.Lab);
        }
        me.FK_MapData = this.FK_MapData;

        this.Pub2.AddTable("border=0  width='300px' ");
        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("项目");
        this.Pub2.AddTDTitle("采集");
        this.Pub2.AddTDTitle("说明");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTD("输入文本框");
        BP.Web.Controls.DDL ddl = new BP.Web.Controls.DDL();
        ddl.ID = "DDL_Oper";
        MapAttrs attrs = new MapAttrs(this.FK_MapData);
        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            if (attr.UIIsEnable == false)
                continue;

            if (attr.UIContralType == UIContralType.TB)
            {
                ddl.Items.Add(new ListItem(attr.KeyOfEn + " - " + attr.Name, attr.KeyOfEn));
                continue;
            }
        }
        ddl.SetSelectItem(me.AttrOfOper);
        this.Pub2.AddTD(ddl);
        this.Pub2.AddTD("输入项");
        this.Pub2.AddTREnd();


        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("colspan=3", "处理内容");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        TextBox tb = new TextBox();
        tb.ID = "TB_Doc";
        tb.Text = me.Doc;
        tb.TextMode = TextBoxMode.MultiLine;
        tb.Rows = 5;
        tb.Columns = 40;
        this.Pub2.AddTD("colspan=3", tb);
        this.Pub2.AddTREnd();

        this.Pub2.AddTRSum();
        Button btn = new Button();
        btn.ID = "BtnSave";
        btn.Text = this.ToE("Save", "保存");
        btn.Click += new EventHandler(btn_SaveAutoFull_Click);
        this.Pub2.AddTD("colspan=2", btn);

        if (this.MyPK == null)
            this.Pub2.AddTD();
        else
            this.Pub2.AddTD("<a href=\"MapExt.aspx?MyPK=" + this.MyPK + "&FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType + "&DoType=EditAutoJL\" >级连下拉框</a>-<a href=\"MapExt.aspx?MyPK=" + this.MyPK + "&FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType + "&DoType=EditAutoFullDtl\" >填充明细表</a>");

        this.Pub2.AddTREnd();
        this.Pub2.AddTableEnd();
        this.Pub2.AddFieldSetEnd();
    }

    public void Edit_ActiveDDL()
    {
        MapExt me = null;
        if (this.MyPK == null)
        {
            me = new MapExt();
            this.Pub2.AddFieldSet("新建:" + this.Lab);
        }
        else
        {
            me = new MapExt(this.MyPK);
            this.Pub2.AddFieldSet("编辑:" + this.Lab);
        }
        me.FK_MapData = this.FK_MapData;

        this.Pub2.AddTable("border=0  width='300px' ");
        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("项目");
        this.Pub2.AddTDTitle("采集");
        this.Pub2.AddTDTitle("说明");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTD("主菜单");
        BP.Web.Controls.DDL ddl = new BP.Web.Controls.DDL();
        ddl.ID = "DDL_Oper";
        MapAttrs attrs = new MapAttrs(this.FK_MapData);
        int num = 0;
        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            if (attr.UIIsEnable == false)
                continue;

            if (attr.UIContralType == UIContralType.DDL)
            {
                num++;
                ddl.Items.Add(new ListItem(attr.KeyOfEn + " - " + attr.Name, attr.KeyOfEn));
                continue;
            }
        }
        ddl.SetSelectItem(me.AttrOfOper);

        this.Pub2.AddTD(ddl);
        this.Pub2.AddTD("输入项");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTD("联动项");
        ddl = new BP.Web.Controls.DDL();
        ddl.ID = "DDL_Attr";
        foreach (MapAttr attr in attrs)
        {
            if (attr.UIVisible == false)
                continue;

            if (attr.UIIsEnable == false)
                continue;

            if (attr.UIContralType != UIContralType.DDL)
                continue;

            ddl.Items.Add(new ListItem(attr.KeyOfEn + " - " + attr.Name, attr.KeyOfEn));
        }
        ddl.SetSelectItem(me.AttrsOfActive);
        this.Pub2.AddTD(ddl);
        this.Pub2.AddTD("要实现联动效果的菜单");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTDTitle("colspan=3", "联动方式");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.Add("<TD class=BigDoc width='100%' colspan=3>");
        RadioButton rb = new RadioButton();
        rb.Text = "通过sql获取联动";
        rb.GroupName = "sdr";
        rb.ID = "RB_0";
        if (me.DoWay == 0)
            rb.Checked = true;

        this.Pub2.AddFieldSet(rb);
        this.Pub2.Add("在下面文本框中输入一个SQL,具有编号，标签列，用来绑定下从动下拉框。");
        this.Pub2.Add("比如: SELECT No,Name FROM CN_City WHERE No LIKE '@FK_SF%' ");
        this.Pub2.AddBR();
        TextBox tb = new TextBox();
        tb.ID = "TB_Doc";
        tb.Text = me.Doc;
        tb.Columns = 70;
        this.Pub2.Add( tb);
        this.Pub2.AddFieldSetEnd();

        rb = new RadioButton();
        rb.Text = "通过编码标识获取";
        rb.GroupName = "sdr";
        rb.ID = "RB_1";
        if (me.DoWay == 1)
            rb.Checked = true;

        this.Pub2.AddFieldSet(rb);
        this.Pub2.Add("主菜单是编号的是从动菜单编号的前几位，不必联动内容。");
        this.Pub2.Add("比如: 主下拉框是省份，联动菜单是城市。");
        this.Pub2.AddFieldSetEnd();

        this.Pub2.Add("</TD>");
        this.Pub2.AddTREnd();

        //this.Pub2.AddTR();
        //this.Pub2.AddTDTitle("colspan=3", "处理内容");
        //this.Pub2.AddTREnd();

        //this.Pub2.AddTR();
        //TextBox tb = new TextBox();
        //tb.ID = "TB_Doc";
        //tb.Text = me.Doc;
        //tb.TextMode = TextBoxMode.MultiLine;
        //tb.Rows = 5;
        //tb.Columns = 40;
        //this.Pub2.AddTD("colspan=3", tb);
        //this.Pub2.AddTREnd();


        this.Pub2.AddTRSum();
        Button btn = new Button();
        btn.ID = "BtnSave";
        btn.Text = "Save";
        btn.Click += new EventHandler(btn_SaveJiLian_Click);
        this.Pub2.AddTD("colspan=3", btn);
        this.Pub2.AddTREnd();
        this.Pub2.AddTableEnd();

        this.Pub2.AddFieldSetEnd();
    }
    void btn_SaveJiLian_Click(object sender, EventArgs e)
    {
        MapExt me = new MapExt();
        me.MyPK = this.MyPK;
        if (me.MyPK.Length > 2)
            me.RetrieveFromDBSources();
        me = (MapExt)this.Pub2.Copy(me);
        me.ExtType = this.ExtType;
        me.Doc = this.Pub2.GetTextBoxByID("TB_Doc").Text;
        me.AttrOfOper = this.Pub2.GetDDLByID("DDL_Oper").SelectedItemStringVal;
        me.AttrsOfActive = this.Pub2.GetDDLByID("DDL_Attr").SelectedItemStringVal;
        if (me.AttrsOfActive == me.AttrOfOper)
        {
            this.Alert("两个项目不能相同.");
            return;
        }
        if (this.Pub2.GetRadioButtonByID("RB_1").Checked)
            me.DoWay = 1;
        else
            me.DoWay = 0;

        me.FK_MapData = this.FK_MapData;
        try
        {
            me.MyPK = this.FK_MapData + "_" + me.ExtType + "_" + me.AttrOfOper + "_" + me.AttrsOfActive;

            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(me.Doc);
            if (dt.Columns.Contains("Name") == false || dt.Columns.Contains("No") == false)
                throw new Exception("在您的sql表达式里，必须有No,Name 还两个列。");
            me.Save();
        }
        catch (Exception ex)
        {
            this.Alert(ex.Message);
            return;
        }
        this.Response.Redirect("MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType, true);
    }
    void btn_SaveInputCheck_Click(object sender, EventArgs e)
    {
        MapExt me = new MapExt();
        me.MyPK = this.MyPK;
        if (me.MyPK.Length > 2)
            me.RetrieveFromDBSources();
        me = (MapExt)this.Pub2.Copy(me);
        me.ExtType = this.ExtType;
        me.Doc = this.Pub2.GetTextBoxByID("TB_Doc").Text;
        me.AttrOfOper = this.Pub2.GetDDLByID("DDL_Oper").SelectedItemStringVal;
        me.FK_MapData = this.FK_MapData;
        me.MyPK = this.FK_MapData + "_" + me.ExtType + "_" + me.AttrOfOper;
        me.Save();
        this.Response.Redirect("MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType, true);
    }
    
    void btn_SaveAutoFull_Click(object sender, EventArgs e)
    {
        MapExt me = new MapExt();
        me.MyPK = this.MyPK;
        if (me.MyPK.Length > 2)
            me.RetrieveFromDBSources();
        me = (MapExt)this.Pub2.Copy(me);
        me.ExtType = this.ExtType;
        me.Doc = this.Pub2.GetTextBoxByID("TB_Doc").Text;
        me.AttrOfOper = this.Pub2.GetDDLByID("DDL_Oper").SelectedItemStringVal;
        me.FK_MapData = this.FK_MapData;
        me.MyPK = this.FK_MapData + "_" + me.ExtType + "_" + me.AttrOfOper;

        try
        {
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(me.Doc);
            if (dt.Columns.Contains("Name") == false || dt.Columns.Contains("No") == false)
                throw new Exception("在您的sql表达式里，必须有No,Name 还两个列。");

            MapAttrs attrs = new MapAttrs(this.FK_MapData);
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName == "No" || dc.ColumnName == "Name")
                    continue;

                if (attrs.Contains(MapAttrAttr.KeyOfEn, dc.ColumnName) == false)
                    throw new Exception("@系统没有找到您要匹配的列(" + dc.ColumnName + ")");
            }
            me.Save();
        }
        catch (Exception ex)
        {
            this.Alert(ex.Message);
            return;
        }
        this.Response.Redirect("MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType, true);
    }
    public void MapExtList(MapExts ens)
    {
        this.Pub2.AddFieldSet("<a href='MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType + "&DoType=New' >新建:" + this.Lab + "</a>");
        this.Pub2.AddTable("border=0");
        this.Pub2.AddTR();
        this.Pub2.AddTDTitle(this.ToE("Sort", "类型"));
        this.Pub2.AddTDTitle(this.ToE("Desc", "描述"));
        this.Pub2.AddTDTitle(this.ToE("Oper", "删除"));
        this.Pub2.AddTREnd();
        foreach (MapExt en in ens)
        {
            this.Pub2.AddTR();
            this.Pub2.AddTD(en.ExtType);
            this.Pub2.AddTDA("MapExt.aspx?FK_MapData=" + this.FK_MapData + "&ExtType=" + this.ExtType + "&MyPK=" + en.MyPK, en.ExtDesc);
            this.Pub2.AddTD("<a href=\"javascript:DoDel('" + en.MyPK + "','" + this.FK_MapData + "','" + this.ExtType + "');\" >删除</a>");
            this.Pub2.AddTREnd();
        }
        this.Pub2.AddTableEnd();
        this.Pub2.AddFieldSetEnd();
    }

}