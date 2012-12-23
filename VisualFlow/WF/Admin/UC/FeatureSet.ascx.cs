﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF.XML;
using BP.WF;
using BP.En;

public partial class WF_Admin_UC_FeatureSet : BP.Web.UC.UCBase3
{
    public int FK_Node
    {
        get
        {
            return int.Parse(this.Request.QueryString["FK_Node"]);
        }
    }
    public string FK_Flow
    {
        get
        {
            return this.Request.QueryString["FK_Flow"];
        }
    }
    public void BindLeft()
    {
        FeatureSets fss = new FeatureSets();
        fss.RetrieveAll();

        this.Pub1.AddFieldSet("流程特性");
        this.Pub1.AddUL();
        foreach (FeatureSet fs in fss)
        {
            if (this.DoType == fs.No)
            {
                this.Pub1.AddLiB("FeatureSetUI.aspx?FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&DoType=" + fs.No, fs.Name);
                this.Lab = fs.Name;
            }
            else
                this.Pub1.AddLi("FeatureSetUI.aspx?FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&DoType=" + fs.No, fs.Name);
        }
        this.Pub1.AddULEnd();

        this.Pub1.AddFieldSetEnd(); // ("流程特性");
    }
    public string Lab = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = this.ToE("DoFeatureSet", "特性集");
        this.BindLeft();

        if (this.DoType == null)
        {
            this.Pub2.AddFieldSet("帮助", " 特性集就是整个流程节点中的特定属性批量的修改。");
            return;
        }

        this.Pub2.AddFieldSet("编辑:" + this.Lab);

        this.Pub2.AddTable("border=0");
        this.Pub2.AddTR();
        switch (this.DoType)
        {
            case "Base":
                this.Pub2.AddTDTitle("步骤");
                this.Pub2.AddTDTitle("节点名称");
                this.Pub2.AddTDTitle("是否可以退回");
                this.Pub2.AddTDTitle("是否可删除");
                this.Pub2.AddTDTitle("是否可转发");
                this.Pub2.AddTDTitle("允许分配工作否?");
                this.Pub2.AddTDTitle("是否可以查看工作报告?");
                this.Pub2.AddTDTitle("是否是保密步骤?");
                break;
            case "FormType":
                this.Pub2.AddTDTitle("步骤");
                this.Pub2.AddTDTitle("节点名称");
                this.Pub2.AddTDTitle("类型");
                this.Pub2.AddTDTitle("URL");
                break;
            default:
                 this.Pub2.AddTDTitle("步骤");
                this.Pub2.AddTDTitle("节点名称");
                this.Pub2.AddTDTitle(this.Lab);
                break;
        }
        this.Pub2.AddTREnd();

        BP.WF.Ext.NodeOs nds = new BP.WF.Ext.NodeOs();
        nds.Retrieve("FK_Flow", this.FK_Flow);

        BP.WF.Ext.NodeO mynd =new BP.WF.Ext.NodeO();

        Attr attr = null;
        try
        {
            attr = mynd.EnMap.GetAttrByKey(this.DoType);
        }
        catch
        {
        }

        foreach (BP.WF.Ext.NodeO nd in nds)
        {
            if (this.FK_Node == nd.NodeID)
                this.Pub2.AddTR1();
            else
                this.Pub2.AddTR();

            switch (this.DoType)
            {
                case "Base":
                    break;
                case "FormType":
                     this.Pub2.AddTDIdx(nd.Step);
                    this.Pub2.AddTD(nd.Name);
                    BP.Web.Controls.DDL ddlFormType = new BP.Web.Controls.DDL();
                    ddlFormType.ID = "DDL_" + nd.NodeID;
                    ddlFormType.BindSysEnum(NodeAttr.FormType, nd.GetValIntByKey(NodeAttr.FormType));
                    this.Pub2.AddTD(ddlFormType);

                    TextBox mytbURL = new TextBox();
                    mytbURL.ID = "TB_" + nd.NodeID;
                    mytbURL.Text = nd.GetValStringByKey(NodeAttr.FormUrl);
                    mytbURL.Columns = 50;
                    this.Pub2.AddTD(mytbURL);
                    break;
                default:
                    this.Pub2.AddTDIdx(nd.Step);
                    this.Pub2.AddTD(nd.Name);
                    switch (attr.UIContralType)
                    {
                        case UIContralType.TB:
                            TextBox mytb = new TextBox();
                            mytb.ID = "TB_" + nd.NodeID;
                            mytb.Text = nd.GetValStringByKey(this.DoType);
                            mytb.Columns = 50;
                            this.Pub2.AddTD(mytb);
                            break;
                        case UIContralType.CheckBok:
                            CheckBox mycb = new CheckBox();
                            mycb.ID = "CB_" + nd.NodeID;
                            mycb.Text = attr.Desc;
                            mycb.Checked = nd.GetValBooleanByKey(this.DoType);
                            this.Pub2.AddTD(mycb);
                            break;
                        case UIContralType.DDL:
                             BP.Web.Controls.DDL ddl =new BP.Web.Controls.DDL();
                             ddl.ID = "DDL_" + nd.NodeID;
                             ddl.BindSysEnum(attr.UIBindKey,  nd.GetValIntByKey(this.DoType));
                             this.Pub2.AddTD(ddl);
                            break;
                        default:
                            break;
                    }
                    break;
            }
            this.Pub2.AddTREnd();
        }
        this.Pub2.AddTableEndWithHR();
        Button btn = new Button();
        btn.ID = "Btn_Save";
        btn.Text = " Save ";
        btn.CssClass = "Btn";
        btn.Click += new EventHandler(btn_Click);
        this.Pub2.Add(btn);
        this.Pub2.AddFieldSetEnd();
    }

    void btn_Click(object sender, EventArgs e)
    {
        BP.WF.Ext.NodeOs nds = new BP.WF.Ext.NodeOs();
        nds.Retrieve("FK_Flow", this.FK_Flow);
        BP.WF.Ext.NodeO mynd = new BP.WF.Ext.NodeO();
        Attr attr = null;
        try
        {
            attr = mynd.EnMap.GetAttrByKey(this.DoType);
        }
        catch
        {
        }

        foreach (BP.WF.Ext.NodeO nd in nds)
        {
            switch (this.DoType)
            {
                case "Base":
                    break;
                case "FormType":
                    nd.SetValByKey(NodeAttr.FormType, this.Pub2.GetDDLByID("DDL_" + nd.NodeID).SelectedItemStringVal);
                    nd.SetValByKey(NodeAttr.FormUrl, this.Pub2.GetTextBoxByID("TB_" + nd.NodeID).Text);
                    nd.Update();
                    break;
                default:
                    switch (attr.UIContralType)
                    {
                        case UIContralType.TB:
                            nd.SetValByKey(this.DoType, this.Pub2.GetTextBoxByID("TB_" + nd.NodeID).Text);
                            break;
                        case UIContralType.CheckBok:
                            nd.SetValByKey(this.DoType, this.Pub2.GetCBByID("CB_" + nd.NodeID).Checked);
                            break;
                        case UIContralType.DDL:
                            nd.SetValByKey(this.DoType, this.Pub2.GetDDLByID("DDL_" + nd.NodeID).SelectedItemStringVal);
                            break;
                        default:
                            break;
                    }
                    nd.Update();
                    break;
            }
        }
    }
}

