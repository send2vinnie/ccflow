﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using BP.Web.Controls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.Sys;
using BP.En;
using BP.Web;
using BP.Web.UC;
using BP.DA;

public partial class Comm_MapDef_EditF : BP.Web.WebPage
{
    /// <summary>
    /// GroupField
    /// </summary>
    public int GroupField
    {
        get
        {
            string  s= this.Request.QueryString["GroupField"];
            if (s == "" || s==null)
                return 0;
            return int.Parse( s);
        }
    }
    /// <summary>
    /// 执行类型
    /// </summary>
    public new string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
        }
    }
    public int FType
    {
        get
        {
            return int.Parse(this.Request.QueryString["FType"]);
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
        if (this.MyPK == null)
            throw new Exception("Mypk==null");

        this.Title = this.ToE("EditField", "编辑字段");

        switch (this.DoType)
        {
            case "Add":
                this.Add();
                break;
            case "Edit":
                MapAttr attr = new MapAttr(this.RefNo);
                attr.MyDataType = this.FType;
                switch (attr.MyDataType)
                {
                    case BP.DA.DataType.AppString:
                        this.EditString(attr);
                        break;
                    case BP.DA.DataType.AppDateTime:
                    case BP.DA.DataType.AppDate:
                    case BP.DA.DataType.AppInt:
                    case BP.DA.DataType.AppFloat:
                    case BP.DA.DataType.AppMoney:
                        this.EditInt(attr);
                        break;
                    case BP.DA.DataType.AppBoolean:
                        this.EditBool(attr);
                        break;
                    default:
                        throw new Exception("为考虑的类型" + this.FType);
                }
                break;
            default:
                break;
        }
    }
    public void Add()
    {
        MapAttr attr = new MapAttr();
        attr.MyDataType = this.FType;
        attr.FK_MapData = this.MyPK;
        attr.UIIsEnable = true;
        switch (this.FType)
        {
            case DataType.AppString:
                this.EditString(attr);
                break;
            case DataType.AppInt:
            case DataType.AppDateTime:
            case DataType.AppDate:
            case DataType.AppFloat:
            case DataType.AppMoney:
                this.EditInt(attr);
                break;
            case DataType.AppBoolean:
                this.EditBool(attr);
                break;
            default:
                break;
        }
    }
    int idx = 1;
    public void EditBeforeAdd(MapAttr mapAttr)
    {
        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("ID");
        this.Pub1.AddTDTitle(this.ToE("Item","项目") );
        this.Pub1.AddTDTitle(this.ToE("Input","采集") );
        this.Pub1.AddTDTitle(this.ToE("Note","备注"));
        this.Pub1.AddTREnd();

        if (mapAttr.IsTableAttr)
        {
            /* if here is table attr, It's will let use can change data type. */
            this.Pub1.AddTR();
            this.Pub1.AddTDIdx(idx++);
            this.Pub1.AddTD("改变数据类型");
            DDL ddlType = new DDL();
            ddlType.ID = "DDL_DTType";
            BP.Sys.XML.SysDataTypes xmls = new BP.Sys.XML.SysDataTypes();
            xmls.RetrieveAll();
            ddlType.Bind(xmls, "No", "Name");
            ddlType.SetSelectItem(mapAttr.MyDataTypeS);

            ddlType.AutoPostBack = true;
            ddlType.SelectedIndexChanged += new EventHandler(ddlType_SelectedIndexChanged);

            this.Pub1.AddTD(ddlType);
            this.Pub1.AddTD("");
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("FEName", "字段英文名称"));
        TB tb = new TB();
        tb.ID = "TB_KeyOfEn";
        tb.Text = mapAttr.KeyOfEn;
        if (this.RefNo != null)
            tb.Enabled = false;

       tb.Attributes["onkeyup"] = "return IsDigit(this);";
         

        this.Pub1.AddTD(tb);
        this.Pub1.AddTD(this.ToE("FENameD", "字母或者字母数字组合"));
        this.Pub1.AddTREnd();


        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("FLabel", "字段中文名称"));
        tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = mapAttr.Name;
        tb.Attributes["width"] = "100%";

        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("DefaultVal", "默认值"));
        tb = new TB();
        tb.ID = "TB_DefVal";
        tb.Text = mapAttr.DefValReal;

       // tb.Width = 20;

        switch (this.FType)
        {
            case BP.DA.DataType.AppDouble:
            case BP.DA.DataType.AppInt:
            case BP.DA.DataType.AppFloat:
                this.Pub1.AddTDNum(tb);
                tb.ShowType = TBType.Num;
                tb.Text = mapAttr.DefVal;
                if (tb.Text == "")
                    tb.Text = "0";
                break;
            case BP.DA.DataType.AppMoney:
            case BP.DA.DataType.AppRate:
                this.Pub1.AddTDNum(tb);
                tb.ShowType = TBType.Moneny;
                break;
            default:
                this.Pub1.AddTD(tb);
                break;
        }

        tb.ShowType = mapAttr.HisTBType;
        switch (this.FType)
        {
            case DataType.AppDateTime:
            case DataType.AppDate:
                CheckBox cb = new CheckBox();
                cb.Text = this.ToE("DefSysData", "默认系统当前日期");
                cb.ID = "CB_DefVal";
                if (mapAttr.DefValReal == "@RDT")
                    cb.Checked = true;
                else
                    cb.Checked = false;
                cb.AutoPostBack = true;
                cb.CheckedChanged += new EventHandler(cb_CheckedChanged_rdt);
                this.Pub1.AddTD(cb);
                break;
            case DataType.AppString:
                DDL ddl = new DDL();
                ddl.AutoPostBack = true;

                BP.WF.XML.DefVals vals = new BP.WF.XML.DefVals();
                vals.Retrieve("Lang", WebUser.SysLang);
                foreach (BP.WF.XML.DefVal def in vals)
                    ddl.Items.Add(new ListItem(def.Name, def.Val));

                //ddl.Items.Add(new ListItem("选择系统约定默认值", ""));
                //ddl.Items.Add(new ListItem("操作员编号", "@WebUser.No"));
                //ddl.Items.Add(new ListItem("操作员名称", "@WebUser.Name"));
                //ddl.Items.Add(new ListItem("操作员部门编号", "@WebUser.FK_Dept"));
                //ddl.Items.Add(new ListItem("操作员部门名称", "@WebUser.FK_DeptName"));

                //ddl.Items.Add(new ListItem("当前日期-1", "@yyyy年mm月dd日"));
                //ddl.Items.Add(new ListItem("当前日期-2", "@yy年mm月dd日"));

                //ddl.Items.Add(new ListItem("当前年度", "@FK_ND"));
                //ddl.Items.Add(new ListItem("当前月份", "@FK_YF"));

                ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged_DefVal);
                ddl.SetSelectItem(mapAttr.DefValReal);
                ddl.ID = "DDL_SelectDefVal";
                this.Pub1.AddTD(ddl);
                break;
            default:
                this.Pub1.AddTD("&nbsp;");
                break;
        }
        this.Pub1.AddTREnd();

        RadioButton rb = new RadioButton();
        if (MapData.IsEditDtlModel == false)
        {
            //this.Pub1.AddTR();
            //this.Pub1.AddTD("界面上是否可见");
            //this.Pub1.Add("<TD>");
            //rb = new RadioButton();
            //rb.ID = "RB_UIVisible_0";
            //rb.Text = "不 可 见";
            //rb.GroupName = "s1";
            //if (mapAttr.UIVisible)
            //    rb.Checked = false;
            //else
            //    rb.Checked = true;
            //this.Pub1.Add(rb);

            //rb = new RadioButton();
            //rb.ID = "RB_UIVisible_1";
            //rb.Text = "可见 ";
            //rb.GroupName = "s1";

            //if (mapAttr.UIVisible)
            //    rb.Checked = true;
            //else
            //    rb.Checked = false;
            //this.Pub1.Add(rb);
            //this.Pub1.Add("</TD>");
            //this.Pub1.AddTD("控制是否显示在页面上");
            //this.Pub1.AddTREnd();
        }

        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("IsCanEdit", "是否可编辑"));
        this.Pub1.Add("<TD>");

        rb = new RadioButton();
        rb.ID = "RB_UIIsEnable_0";
        rb.Text = this.ToE("UnCanEdit", "不可编辑");
        rb.GroupName = "s";
        rb.Checked = !mapAttr.UIIsEnable;

        this.Pub1.Add(rb);
        rb = new RadioButton();
        rb.ID = "RB_UIIsEnable_1";
        rb.Text = this.ToE("CanEdit", "可编辑");
        rb.GroupName = "s";
        rb.Checked = mapAttr.UIIsEnable;

        this.Pub1.Add(rb);
        this.Pub1.Add("</TD>");
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();


        #region 是否可单独行显示
        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("CtlShowWay", "呈现方式")); //呈现方式;
        this.Pub1.AddTDBegin();
        rb = new RadioButton();
        rb.ID = "RB_UIIsLine_0";
        rb.Text = this.ToE("Row2", "两列显示"); // 两行
        rb.GroupName = "sa";
        if (mapAttr.UIIsLine)
            rb.Checked = false;
        else
            rb.Checked = true;

        this.Pub1.Add(rb);
        rb = new RadioButton();
        rb.ID = "RB_UIIsLine_1";
        rb.Text = this.ToE("Row1", "整行显示"); // "一行";
        rb.GroupName = "sa";

        if (mapAttr.UIIsLine)
            rb.Checked = true;
        else
            rb.Checked = false;

        this.Pub1.Add(rb);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTD("对傻瓜表单有效");


       // this.Pub1.AddTD("控制该它在表单的显示方式");
        this.Pub1.AddTREnd();
        #endregion 是否可编辑


        #region 是否可界面可见
        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("IsView", "是否界面可见")); //是否界面可见
        this.Pub1.AddTDBegin();
        rb = new RadioButton();
        rb.ID = "RB_UIVisible_0";
        rb.Text = this.ToE("IsView0", "不可见"); // 界面不可见
        rb.GroupName = "sa3";
        if (mapAttr.UIVisible)
            rb.Checked = false;
        else
            rb.Checked = true;

        this.Pub1.Add(rb);
        if (mapAttr.IsTableAttr)
            rb.Enabled = false;

        rb = new RadioButton();
        rb.ID = "RB_UIVisible_1";
        rb.Text = this.ToE("IsView1", "界面可见"); // 界面可见;
        rb.GroupName = "sa3";

        if (mapAttr.UIVisible)
            rb.Checked = true;
        else
            rb.Checked = false;

        if (mapAttr.IsTableAttr)
            rb.Enabled = false;

        this.Pub1.Add(rb);
        this.Pub1.AddTDEnd();

        this.Pub1.AddTD("不可见者为隐藏字段.");
     //   this.Pub1.AddTD("控制该它在表单的界面里是否可见");
        this.Pub1.AddTREnd();
        #endregion 是否可界面可见

    }

    void ddl_SelectedIndexChanged_DefVal(object sender, EventArgs e)
    {
        this.Pub1.GetTBByID("TB_DefVal").Text = this.Pub1.GetDDLByID("DDL_SelectDefVal").SelectedItemStringVal;
    }

    void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        MapAttr attr = new MapAttr(this.RefNo);
        attr.MyDataTypeS = this.Pub1.GetDDLByID("DDL_DTType").SelectedItemStringVal;
        attr.Update();
        this.Response.Redirect("EditF.aspx?DoType=" + this.DoType + "&MyPK=" + this.MyPK + "&RefNo=" + this.RefNo + "&FType=" + attr.MyDataType + "&GroupField=" + this.GroupField, true);
        // this.Response.Redirect(this.Request.RawUrl, true);
    }
    public void EditBeforeEnd(MapAttr mapAttr)
    {
        #region 字段分组
        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("FieldGroup", "字段分组"));
        DDL ddlGroup = new DDL();
        ddlGroup.ID = "DDL_GroupID";
        GroupFields gfs = new GroupFields(mapAttr.FK_MapData);
        ddlGroup.Bind(gfs, GroupFieldAttr.OID, GroupFieldAttr.Lab);
        if (mapAttr.GroupID == 0)
            mapAttr.GroupID = this.GroupField;

        ddlGroup.SetSelectItem(mapAttr.GroupID);

        this.Pub1.AddTD("colspan=3", ddlGroup);
        this.Pub1.AddTREnd();
        #endregion 字段分组

        #region 是否是数字签名字段
        if (mapAttr.UIIsEnable == false && mapAttr.MyDataType == DataType.AppString && mapAttr.LGType == FieldTypeS.Normal)
        {
            this.Pub1.AddTR();
            this.Pub1.AddTDIdx(idx++);
            CheckBox cb = new CheckBox();
            cb.ID = "CB_IsSigan";
            cb.Text = "是否是数字签名字段";
            cb.Checked = mapAttr.IsSigan;

            this.Pub1.AddTD("colspan=3", cb);
            this.Pub1.AddTREnd();
        }
        #endregion 字段分组

        this.Pub1.AddTRSum();
        this.Pub1.Add("<TD colspan=4 align=center>");
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " " + this.ToE("Save", "保存") + " ";
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_SaveAndClose";
        btn.Text = this.ToE("SaveAndClose", "保存并关闭"); // "保存并关闭";
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_SaveAndNew";
        btn.Text = this.ToE("SaveAndNew", "保存新建"); // "保存新建";
        btn.Click += new EventHandler(btn_Save_Click);
        this.Pub1.Add(btn);

        if (this.RefNo != null)
        {
            btn = new Button();
            btn.ID = "Btn_AutoFull";
            btn.Text = this.ToE("AutoFull", "扩展设置");
            btn.Attributes["onclick"] = "javascript:WinOpen('AutoFull.aspx?RefNo=" + this.RefNo + "&FK_MapData=" + mapAttr.FK_MapData + "',''); return false;";
            this.Pub1.Add(btn);

            if (mapAttr.HisEditType == EditType.Edit)
            {
                btn = new Button();
                btn.ID = "Btn_Del";
                btn.Text = this.ToE("Del", "删除");
                btn.Click += new EventHandler(btn_Save_Click);
                btn.Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";
                this.Pub1.Add(btn);
            }
        }

        string url = "Do.aspx?DoType=AddF&MyPK=" + mapAttr.FK_MapData + "&IDX=" + mapAttr.IDX;
        btn = new Button();
        btn.ID = "Btn_New";
        btn.Text = this.ToE("New", "新建");
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.ID = "Btn_Back";
        btn.Text = this.ToE("Back", "返回");
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);

        // this.Pub1.Add("<input type=button  value='" + + "' onclick=\"window.location.href='" + url + "'\" >");
        // this.Pub1.Add("<a href='" + url + "'><img src='../../Images/Btn/New.gif' border=0>" + this.ToE("New", "新建") + "</a></TD>");
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEndWithBR();
    }
    public void btn_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        switch (btn.ID)
        {
            case "Btn_New":
                MapAttr mapAttr = new MapAttr(this.RefNo);
                string url = "Do.aspx?DoType=AddF&MyPK=" + mapAttr.FK_MapData + "&IDX=" + mapAttr.IDX + "&GroupField = " + this.GroupField;
                this.Response.Redirect(url, true);
                return;
            case "Btn_Back":
                string url1 = "Do.aspx?DoType=AddF&MyPK=" + this.MyPK + "&GroupField = " + this.GroupField;
                this.Response.Redirect(url1, true);
                return;
            default:
                break;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapAttr"></param>
    public void EditString(MapAttr mapAttr)
    {
        this.EditBeforeAdd(mapAttr);
        TB tb = new TB();
        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("MinLen", "最小长度"));
        tb = new TB();
        tb.ID = "TB_MinLen";
        tb.CssClass = "TBNum";
        tb.Text = mapAttr.MinLen.ToString();
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("MaxLen", "最大长度"));
        tb = new TB();
        tb.ID = "TB_MaxLen";
        tb.CssClass = "TBNum";
        tb.Text = mapAttr.MaxLen.ToString();

        //DDL cb = new DDL();
        //cb.ID = "DDL_TBType";
        //cb.Items.Add(new ListItem("单行文本框", "0"));
        //cb.Items.Add(new ListItem("多行文本框", "1"));
        //cb.Items.Add(new ListItem("Sina编辑框", "2"));
        //cb.Items.Add(new ListItem("FCKEditer编辑框", "3"));

        this.Pub1.AddTD(tb);

        DDL ddlBig = new DDL();
        ddlBig.ID = "DDL_TBModel";
        ddlBig.BindSysEnum("TBModel", mapAttr.TBModel);
        ddlBig.AutoPostBack = true;
        ddlBig.SelectedIndexChanged += new EventHandler(ddlBig_SelectedIndexChanged);
        this.Pub1.AddTD(ddlBig);

        //CheckBox cb = new CheckBox();
        //cb.CheckedChanged += new EventHandler(cb_CheckedChanged);
        //cb.ID = "CB_IsM";
        //cb.Text = this.ToE("IsBigDoc", "是否大块文本(对傻瓜表单有效)");
        //cb.AutoPostBack = true;
        //if (mapAttr.MaxLen >= 3000)
        //{
        //    cb.Checked = true;
        //    tb.Enabled = false;
        //}
        //else
        //{
        //    cb.Checked = false;
        //    tb.Enabled = true;
        //}
        //this.Pub1.AddTD(cb);
        //if (mapAttr.IsTableAttr)
        //    cb.Enabled = false;

        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("TBWidth", "文本框宽度"));
        tb = new TB();
        tb.ID = "TB_UIWidth";
        tb.CssClass = "TBNum";
        tb.Text = mapAttr.UIWidth.ToString();
        this.Pub1.AddTD(tb);
        this.Pub1.AddTDB(this.ToE("ForDtl", "对明细表有效"));
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("TBHeight", "高度"));
        tb = new TB();
        tb.ID = "TB_UIHeight";
        tb.CssClass = "TBNum";
        tb.Text = mapAttr.UIHeight.ToString();
        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();
        this.EditBeforeEnd(mapAttr);
    }

    void ddlBig_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDL ddl = this.Pub1.GetDDLByID("DDL_TBModel");
        if (ddl.SelectedItemIntVal != 0)
        {
            this.Pub1.GetTBByID("TB_MaxLen").Enabled = false;
            this.Pub1.GetTBByID("TB_MaxLen").Text = "4000";
            this.Pub1.GetTBByID("TB_UIHeight").Text = "90";
        }
        else
        {
            this.Pub1.GetTBByID("TB_MaxLen").Enabled = true;
            this.Pub1.GetTBByID("TB_MaxLen").Text = "50";
        }
    }
    void cb_CheckedChanged_rdt(object sender, EventArgs e)
    {
        CheckBox cb = this.Pub1.GetCBByID("CB_DefVal");
        if (cb.Checked)
        {
            this.Pub1.GetTBByID("TB_DefVal").Text = "@RDT";
        }
        else
        {
            this.Pub1.GetTBByID("TB_DefVal").Text = "";
        }
    }
    void cb_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = this.Pub1.GetCBByID("CB_IsM");
        if (cb.Checked)
        {
            this.Pub1.GetTBByID("TB_MaxLen").Enabled = false;
            this.Pub1.GetTBByID("TB_MaxLen").Text = "4000";
            this.Pub1.GetTBByID("TB_UIHeight").Text = "90";
        }
        else
        {
            this.Pub1.GetTBByID("TB_MaxLen").Enabled = true;
            this.Pub1.GetTBByID("TB_MaxLen").Text = "50";
        }
    }
    public void EditInt(MapAttr mapAttr)
    {
        this.EditBeforeAdd(mapAttr);
        this.EditBeforeEnd(mapAttr);
    }
    public void EditBool(MapAttr mapAttr)
    {
        this.Pub1.AddTable();
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("ID"); // 项目
        this.Pub1.AddTDTitle("项目"); // 项目
        this.Pub1.AddTDTitle("采集");   // 值
        this.Pub1.AddTDTitle("描述"); // 描述
        this.Pub1.AddTREnd();

        if (mapAttr.IsTableAttr)
        {
            /* if here is table attr, It's will let use can change data type. */
            this.Pub1.AddTR();
            this.Pub1.AddTDIdx(idx++);
            this.Pub1.AddTD("改变数据类型");
            DDL ddlType = new DDL();
            ddlType.ID = "DDL_DTType";
            BP.Sys.XML.SysDataTypes xmls = new BP.Sys.XML.SysDataTypes();
            xmls.RetrieveAll();
            ddlType.Bind(xmls, "No", "Name");
            ddlType.SetSelectItem(mapAttr.MyDataTypeS);

            ddlType.AutoPostBack = true;
            ddlType.SelectedIndexChanged += new EventHandler(ddlType_SelectedIndexChanged);

            this.Pub1.AddTD(ddlType);
            this.Pub1.AddTD("");
            this.Pub1.AddTREnd();
        }

        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("FEName", "字段英文名称"));
        TB tb = new TB();
        tb.ID = "TB_KeyOfEn";
        tb.Text = mapAttr.KeyOfEn;

        if (this.RefNo != null)
            tb.Enabled = false;

        this.Pub1.AddTD(tb);
        this.Pub1.AddTD(this.ToE("FENameD", "字母或者字母数字组合"));
        this.Pub1.AddTREnd();

        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("DefaultVal", "默认值"));
        CheckBox cb = new CheckBox();
        cb.ID = "CB_DefVal";
        cb.Text = this.ToE("PChose", "请选择");
        cb.Checked = mapAttr.DefValOfBool;

        this.Pub1.AddTD(cb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("FLabel", "字段中文名称"));
        tb = new TB();
        tb.ID = "TB_Name";
        tb.Text = mapAttr.Name;
        tb.Attributes["width"] = "100%";

        this.Pub1.AddTD(tb);
        this.Pub1.AddTD("");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("IsEdit", "是否可编辑"));
        this.Pub1.Add("<TD>");

        RadioButton rb = new RadioButton();
        rb.ID = "RB_UIIsEnable_0";
        rb.Text = this.ToE("UnCanEdit", "不可编辑");
        rb.GroupName = "s";         
        rb.Checked = !mapAttr.UIIsEnable;
        this.Pub1.Add(rb);


        rb = new RadioButton();
        rb.ID = "RB_UIIsEnable_1";
        rb.Text = this.ToE("CanEdit", "可编辑");
        rb.GroupName = "s";
        rb.Checked = mapAttr.UIIsEnable;
        this.Pub1.Add(rb);

        this.Pub1.Add("</TD>");
        this.Pub1.AddTD();
        // this.Pub1.AddTD(this.ToE("IsReadonly", "是否只读"));

        this.Pub1.AddTREnd();

        #region 是否可单独行显示
        this.Pub1.AddTR();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD( this.ToE("CtlShowWay", "呈现方式") ); //呈现方式;
        this.Pub1.AddTDBegin();
        rb = new RadioButton();
        rb.ID = "RB_UIIsLine_0";
        rb.Text = this.ToE("Row2", "两列显示"); // 两行
        rb.GroupName = "sa";
        if (mapAttr.UIIsLine)
            rb.Checked = false;
        else
            rb.Checked = true;

        this.Pub1.Add(rb);
        rb = new RadioButton();
        rb.ID = "RB_UIIsLine_1";
        rb.Text = this.ToE("Row1", "整行显示"); // "一行";
        rb.GroupName = "sa";

        if (mapAttr.UIIsLine)
            rb.Checked = true;
        else
            rb.Checked = false;

        this.Pub1.Add(rb);
        this.Pub1.AddTDEnd();

        this.Pub1.AddTD("对傻瓜表单有效");

        //this.Pub1.AddTD("控制该它在表单的显示方式");
        this.Pub1.AddTREnd();
        #endregion 是否可编辑

        #region 是否可界面可见
        this.Pub1.AddTR1();
        this.Pub1.AddTDIdx(idx++);
        this.Pub1.AddTD(this.ToE("IsView", "是否界面可见")); //是否界面可见
        this.Pub1.AddTDBegin();
        rb = new RadioButton();
        rb.ID = "RB_UIVisible_0";
        rb.Text = this.ToE("IsView0", "不可见"); // 界面不可见
        rb.GroupName = "sa3";
        if (mapAttr.UIVisible)
            rb.Checked = false;
        else
            rb.Checked = true;

        this.Pub1.Add(rb);
        rb = new RadioButton();
        rb.ID = "RB_UIVisible_1";
        rb.Text = this.ToE("IsView1", "界面可见"); // 界面可见;
        rb.GroupName = "sa3";

        if (mapAttr.UIVisible)
            rb.Checked = true;
        else
            rb.Checked = false;

        this.Pub1.Add(rb);
        this.Pub1.AddTDEnd();

        this.Pub1.AddTD("不可见者为隐藏字段.");
        //this.Pub1.AddTD("控制该它在表单的界面里是否可见");
        this.Pub1.AddTREnd();
        #endregion 是否可界面可见

        this.EditBeforeEnd(mapAttr);
    }
    public void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = sender as Button;
            switch (btn.ID)
            {
                case "Btn_Del":
                    this.Response.Redirect("Do.aspx?DoType=Del&MyPK=" + this.MyPK + "&RefNo=" + this.RefNo + "&GroupField = " + this.GroupField, true);
                    return;
                default:
                    break;
            }

            MapAttr attr = new MapAttr();
            if (this.RefNo != null)
            {
                attr.MyPK = this.RefNo;
                try
                {
                    attr.Retrieve();
                }
                catch
                {
                    attr.CheckPhysicsTable();
                    attr.Retrieve();
                }

                attr = (MapAttr)this.Pub1.Copy(attr);
                attr.GroupID = this.Pub1.GetDDLByID("DDL_GroupID").SelectedItemIntVal;
                if (attr.UIIsEnable == false && attr.MyDataType == DataType.AppString)
                {
                    try
                    {
                        attr.IsSigan = this.Pub1.GetCBByID("CB_IsSigan").Checked;
                    }
                    catch
                    {
                    }
                }

                switch (this.FType)
                {
                    case DataType.AppBoolean:
                        attr.MyDataType = BP.DA.DataType.AppBoolean;
                        attr.DefValOfBool = this.Pub1.GetCBByID("CB_DefVal").Checked;
                        break;
                    case DataType.AppDateTime:
                    case DataType.AppDate:
                        attr.DefValReal = this.Pub1.GetTBByID("TB_DefVal").Text;
                        //if (this.Pub1.GetCBByID("CB_DefVal").Checked)
                        //    attr.DefValReal = "1";
                        //else
                        //    attr.DefValReal = "0";
                        break;
                    case DataType.AppString:
                        attr.UIBindKey = this.Pub1.GetDDLByID("DDL_TBModel").SelectedItemStringVal;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                attr = (MapAttr)this.Pub1.Copy(attr);
                attr.GroupID = this.Pub1.GetDDLByID("DDL_GroupID").SelectedItemIntVal;
                MapAttrs attrS = new MapAttrs(this.MyPK);
                int idx = 0;
                foreach (MapAttr en in attrS)
                {
                    idx++;
                    en.IDX = idx;
                    en.Update();
                    if (en.KeyOfEn == attr.KeyOfEn)
                        throw new Exception(this.ToE("FExits", "字段已经存在") + " Key=" + attr.KeyOfEn);
                }
                if (this.IDX == null || this.IDX == "")
                    attr.IDX = 0;
                else
                    attr.IDX = int.Parse(this.IDX) - 1;

                attr.MyDataType = this.FType;
                switch (this.FType)
                {
                    case DataType.AppBoolean:
                        attr.MyDataType = BP.DA.DataType.AppBoolean;
                        attr.UIContralType = UIContralType.CheckBok;
                        attr.DefValOfBool = this.Pub1.GetCBByID("CB_DefVal").Checked;
                        break;
                    case DataType.AppString:
                        attr.UIBindKey = this.Pub1.GetDDLByID("DDL_TBModel").SelectedItemStringVal;
                        break;
                    default:
                        break;
                }
            }

            Response.Buffer = true;
            attr.FK_MapData = this.MyPK;
            attr.MyPK = this.RefNo;
            attr.Save();
            switch (btn.ID)
            {
                case "Btn_SaveAndClose":
                    this.WinClose();
                    return;
                case "Btn_SaveAndNew":
                    this.Response.Redirect("Do.aspx?DoType=AddF&MyPK=" + this.MyPK + "&IDX=" + this.IDX + "&GroupField=" + attr.GroupID, true);
                    return;
                default:
                    break;
            }
            this.Response.Redirect("EditF.aspx?DoType=Edit&MyPK=" + this.MyPK + "&RefNo=" + attr.MyPK + "&FType=" + this.FType + "&GroupField=" + attr.GroupID, true);
        }
        catch (Exception ex)
        {
            this.Alert(ex.Message);
        }
    }
    public string GetCaption
    {

        get
        {
            if (this.DoType == "Add")
                return this.ToE("GuideNewField", "增加新字段向导") + " - <a href='Do.aspx?DoType=ChoseFType&GroupField=" + this.GroupField + "' >" + this.ToE("ChoseType", "返回类型选择") + "</a> - " + this.ToE("Edit", "编辑");
            else
                return "<a href='Do.aspx?DoType=ChoseFType&MyPK=" + this.MyPK + "&RefNo=" + this.RefNo + "&GroupField=" + this.GroupField + "'>" + this.ToE("ChoseType", "返回类型选择") + "</a> - " + this.ToE("Edit", "编辑");
        }
    }
}
