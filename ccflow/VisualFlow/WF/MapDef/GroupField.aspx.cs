﻿using System;
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
    void btn_Check_Click(object sender, EventArgs e)
    {
        string sta = this.Pub1.GetTBByID("TB_Sta").Text.Trim();
        if (sta.Length == 0)
        {
            this.Alert("审核岗位不能为空");
            return;
        }

        string Prx = this.Pub1.GetTBByID("TB_Prx").Text.Trim();
        if (Prx.Length == 0)
        {
            Prx = chs2py.convert(sta);
        }

        MapAttr attr = new MapAttr();
        int i = attr.Retrieve(MapAttrAttr.FK_MapData, this.RefNo, MapAttrAttr.KeyOfEn, Prx + "_Note");
        i += attr.Retrieve(MapAttrAttr.FK_MapData, this.RefNo, MapAttrAttr.KeyOfEn, Prx + "_Checker");
        i += attr.Retrieve(MapAttrAttr.FK_MapData, this.RefNo, MapAttrAttr.KeyOfEn, Prx + "_RDT");

        if (i > 0)
        {
           
            this.Alert("前缀已经使用：" + Prx + " ， 请确认您是否增加了这个审核分组或者，请您更换其他的前缀。");
            return;
        }

        GroupField gf = new GroupField();
        gf.Lab = sta;
        gf.EnName = this.RefNo;
        gf.Insert();

        attr = new MapAttr();
        attr.FK_MapData = this.RefNo;
        attr.KeyOfEn = Prx + "_Note";
        attr.Name = this.ToE("CheckNote", "审核意见");
        attr.MyDataType = DataType.AppString;
        attr.UIContralType = UIContralType.TB;
        attr.UIIsEnable = true;
        attr.UIIsLine = true;
        attr.MaxLen = 4000;
        attr.GroupID = gf.OID;
        attr.UIHeight = 23 * 3;
        attr.IDX = 1;
        attr.Insert();

        attr = new MapAttr();
        attr.FK_MapData = this.RefNo;
        attr.KeyOfEn = Prx + "_Checker";
        attr.Name = this.ToE("Checker", "审核人");// "审核人";
        attr.MyDataType = DataType.AppString;
        attr.UIContralType = UIContralType.TB;
        attr.MaxLen = 50;
        attr.MinLen = 0;
        attr.UIIsEnable = true;
        attr.UIIsLine = false;
        attr.DefVal = "@WebUser.Name";
        attr.UIIsEnable = false;
        attr.GroupID = gf.OID;
        attr.IsSigan = true;
        attr.IDX = 2;
        attr.Insert();

        attr = new MapAttr();
        attr.FK_MapData = this.RefNo;
        attr.KeyOfEn = Prx + "_RDT";
        attr.Name = this.ToE("CheckDate", "审核日期"); // "审核日期";
        attr.MyDataType = DataType.AppDateTime;
        attr.UIContralType = UIContralType.TB;
        attr.UIIsEnable = true;
        attr.UIIsLine = false;
        attr.DefVal = "@RDT";
        attr.UIIsEnable = false;
        attr.GroupID = gf.OID;
        attr.IDX = 3;
        attr.Insert();
        this.WinCloseWithMsg(this.ToE("SaveOK", "保存成功")); // "增加成功，您可以调整它的位置与修改字段的标签。"
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = this.ToE("FieldGroup", "字段分组");

        switch (this.DoType)
        {
            case "FunList":
                this.Pub1.AddFieldSet(this.ToE("FieldGroupGuide", "字段分组向导"));
                this.Pub1.AddBR();

                this.Pub1.AddUL();
                this.Pub1.AddLi("GroupField.aspx?DoType=NewGroup&RefNo=" + this.RefNo,
                     "<b>"+this.ToE("NewGFBlank", "新建空白字段分组") + "</b><br><font color=green>" + this.ToE("NewGFBlankD", "系统会让您输入审核的信息，并创建审核分组。") + "</font>");

                this.Pub1.AddLi("GroupField.aspx?DoType=NewCheckGroup&RefNo=" + this.RefNo,
                    "<b>" + this.ToE("NewGFCheck", "新建审核分组") + "</b><br><font color=green>" + this.ToE("NewGFCheckD", "系统会让您输入审核的信息，并创建审核分组。") + "</font>");
                //TB tb = new TB();
                this.Pub1.AddULEnd();
                this.Pub1.AddFieldSetEnd();
                return;
            case "NewCheckGroup":
                this.Pub1.AddFieldSet("<a href=GroupField.aspx?DoType=FunList&RefNo=" + this.RefNo + " >" + this.ToE("FieldGroupGuide", "字段分组向导") + "</a> - " + this.ToE("NewGFCheck", "审核分组"));
                TB tbc = new TB();
                tbc.ID = "TB_Sta";
                this.Pub1.Add(this.ToE("CheckSta", "审核岗位") + "<font color=red>*</font>");
                this.Pub1.Add(tbc);
                this.Pub1.AddBR("<font color=green>" + this.ToE("CheckStaD", "比如:分局长审核、科长审核、总经理审核。") + "</font>");
                this.Pub1.AddBR();

                tbc = new TB();
                tbc.ID = "TB_Prx";
                this.Pub1.Add(this.ToE("FieldFix", "字段前缀") + ":");
                this.Pub1.Add(tbc);
                this.Pub1.AddBR("<font color=green>" + this.ToE("FieldFixD", "用于自动创建字段，请输入英文字母或者字母数字组合。系统自动依据您的输入产生字段。如：XXX_Note，审核意见。XXX_RDT审核时间。XXX_Checker审核人，为空系统自动用拼音表示。") + "</font>");
                this.Pub1.AddBR();
                this.Pub1.AddHR();
                Btn btnc = new Btn();
                btnc.Click += new EventHandler(btn_Check_Click);
                btnc.Text = this.ToE("Save", "保存");

                this.Pub1.Add(btnc);
                this.Pub1.AddFieldSetEnd();
                return;
            case "NewGroup":
                GroupFields mygfs = new GroupFields(this.RefNo);
                GroupField gf1 = new GroupField();
                gf1.Idx = mygfs.Count;
                gf1.Lab = this.ToE("NewGroupField", "新建字段分组"); // "新建字段分组";
                gf1.EnName = this.RefNo;
                gf1.Insert();
                this.Response.Redirect("GroupField.aspx?RefNo=" + this.RefNo + "&RefOID=" + gf1.OID, true);
                return;
            default:
                break;
        }
        
        #region edit operation
        GroupField en = new GroupField(this.RefOID);
        this.Pub1.AddBR();
        this.Pub1.Add("<Table border=0 >");
        this.Pub1.AddTR();

        this.Pub1.AddTD(this.ToE("Name", "分组名称") );

        TB tb = new TB();
        tb.ID = "TB_Lab_" + en.OID;
        tb.Text = en.Lab;
        tb.Columns = 50;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTREnd();

        this.Pub1.AddTRSum();
        this.Pub1.Add("<TD align=center colspan=2>");
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

        //btn = new Btn();
        //btn.Text = this.ToE("CopyField", "复制字段");
        //btn.ID = "Btn_CopyField";
        //btn.Click += new EventHandler(btn_Click);
        //this.Pub1.Add(btn);


        btn = new Btn();
        btn.Text = this.ToE("Delete", "删除");
        btn.ID = "Btn_Delete";
        btn.Click += new EventHandler(btn_del_Click);
        btn.Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";

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
        attrs.Retrieve(MapAttrAttr.GroupID, this.RefOID);
        foreach (MapAttr attr in attrs)
        {
            if (attr.HisEditType != EditType.Edit)
                continue;
            if (attr.KeyOfEn == "FID")
                continue;

            attr.Delete();
        }

        MapDtls dtls = new MapDtls();
        dtls.Retrieve(MapDtlAttr.GroupID, this.RefOID);
        foreach (MapDtl dtl in dtls)
            dtl.Delete();

        GroupField gf = new GroupField(this.RefOID);
        gf.Delete();
        this.WinClose();// ("删除成功。");
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
