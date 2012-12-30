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
using BP.WF;
using BP.En;
using BP.Port;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

public partial class WF_Admin_listen : WebPage
{
    public int FK_Node
    {
        get
        {
            return int.Parse( this.Request.QueryString["FK_Node"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.DoType)
        {
            case "New":
                this.BindNew();
                break;
            default:
                this.BindList();
                break;
        }
    }
    public void BindNew()
    {
        Listen li = new Listen();
        if (this.RefOID != 0)
        {
            li.OID = this.RefOID;
            li.Retrieve();
        }

        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        this.Pub1.AddTable();
        this.Pub1.AddCaptionLeft("设置收听：" + nd.Name + "- <a href='Listen.aspx?FK_Node=" + this.FK_Node + "' >返回列表</a>");

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("选择您要收听的节点（可以选择多个）");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDBegin();
        BP.WF.Nodes nds = new BP.WF.Nodes(nd.FK_Flow);
        foreach (BP.WF.Node en in nds)
        {
            if (en.NodeID == this.FK_Node)
                continue;

            CheckBox cb = new CheckBox();
            cb.Text ="步骤："+en.Step+" - "+  en.Name;
            cb.ID = "CB_" + en.NodeID;

            cb.Checked = li.Nodes.Contains("@" + en.NodeID);
            this.Pub1.Add(cb);
            this.Pub1.AddBR();
        }
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("设置标题(最大长度不超过250个字符，可以包含字段变量变量以@开头)");
        this.Pub1.AddTREnd();
      
        this.Pub1.AddTR();
        TextBox tb = new TextBox();
        tb.ID = "TB_Title";
        tb.Columns = 70;
        tb.Text = li.Title;
        this.Pub1.AddTD(tb);
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("例如：您发起的工作@Title已经被@WebUser.Name处理。");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDTitle("内容信息(长度不限制，可以包含字段变量变量以@开头)");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTDBegin();
        tb = new TextBox();
        tb.TextMode = TextBoxMode.MultiLine;
        tb.ID = "TB_Doc";
        tb.Columns = 70;
        tb.Rows = 8;
        tb.Text = li.Doc;
        this.Pub1.Add(tb);
        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("例如：处理时间@RDT，您可以登陆系统查看处理的详细信息，特此通知。");
        this.Pub1.AddTREnd();

        this.Pub1.AddTR();
        this.Pub1.AddTD("<b>特别说明：</b>消息以什么样的渠道(短信，邮件)发送出去，是以用户设置的 “信息提示”来确定的。");
        this.Pub1.AddTREnd();


        this.Pub1.AddTRSum();
        this.Pub1.AddTDBegin();
        Button btn = new Button();
        btn.Text = this.ToE("Save", "保存");
        btn.ID = "Save";
        btn.CssClass = "Btn";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.Text = this.ToE("SaveAndNew", "保存并新建");
        btn.ID = "New";

        btn.CssClass = "Btn";
        btn.Click += new EventHandler(btn_Click);
        this.Pub1.Add(btn);

        btn = new Button();
        btn.Text = this.ToE("Del", "删除") ;
        btn.CssClass = "Btn";
        btn.Attributes["onclick"] = " return confirm('" + this.ToE("AYS", "您确认吗？") + "');";
        btn.Click += new EventHandler(btn_Del_Click);
        if (this.RefOID == 0)
            btn.Enabled = false;
        this.Pub1.Add(btn);

        this.Pub1.AddTDEnd();
        this.Pub1.AddTREnd();
        this.Pub1.AddTableEnd();
    }

    void btn_Click(object sender, EventArgs e)
    {
        Listen li = new Listen();
        if (this.RefOID != 0)
        {
            li.OID = this.RefOID;
            li.Retrieve();
        }
        li = this.Pub1.Copy(li) as Listen;
        li.OID = this.RefOID;

        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        BP.WF.Nodes nds = new BP.WF.Nodes(nd.FK_Flow);
        string strs = "";
        foreach (BP.WF.Node en in nds)
        {
            if (en.NodeID == this.FK_Node)
                continue;

            CheckBox cb = this.Pub1.GetCBByID("CB_" + en.NodeID);
            if (cb.Checked)
                strs += "@" + en.NodeID;
        }

        li.Nodes = strs;
        li.FK_Node = this.FK_Node;

        if (li.OID == 0)
            li.Insert();
        else
            li.Update();

        Button btn = (Button)sender;
        if (btn.ID == "Save")
            this.Response.Redirect("Listen.aspx?FK_Node=" + this.FK_Node + "&DoType=New&RefOID=" + li.OID, true);
        else
            this.Response.Redirect("Listen.aspx?FK_Node=" + this.FK_Node + "&DoType=New&RefOID=0", true);
    }
    void btn_Del_Click(object sender, EventArgs e)
    {
        Listen li = new Listen();
        if (this.RefOID != 0)
        {
            li.OID = this.RefOID;
            li.Delete();
        }
        this.Response.Redirect("Listen.aspx?FK_Node=" + this.FK_Node + "&DoType=New&RefOID=0", true);
    }
    public void BindList()
    {
        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);

        Listens ens = new Listens();
        ens.Retrieve(ListenAttr.FK_Node, this.FK_Node);

        if (ens.Count == 0)
        {
            this.Response.Redirect("Listen.aspx?FK_Node=" + this.FK_Node + "&DoType=New", true);
            return;
        }

        this.Pub1.AddTable("width=80%");
        this.Pub1.AddCaptionLeft("设置收听：" + nd.Name + "- <a href='Listen.aspx?FK_Node=" + this.FK_Node + "&DoType=New' >" + this.ToE("New", "新建") + "</a>");
        this.Pub1.AddTR();
        this.Pub1.AddTDTitle(this.ToE("CurrNode", "当前节点"));
        this.Pub1.AddTDTitle(this.ToE("CurrNode", "收听节点"));
        this.Pub1.AddTDTitle(this.ToE("Oper", "操作"));
        this.Pub1.AddTREnd();
        foreach (Listen en in ens)
        {
            this.Pub1.AddTR();
            this.Pub1.AddTD(nd.Name);
            this.Pub1.AddTD(en.Nodes);
            // this.Pub1.AddTD(en.Title);
            //this.Pub1.AddTD(en.Doc);
            this.Pub1.AddTD("<a href='Listen.aspx?FK_Node=" + this.FK_Node + "&DoType=New&RefOID=" + en.OID + "'>" + this.ToE("Edit", "删除-编辑") + "</a>");
            this.Pub1.AddTREnd();
        }
        this.Pub1.AddTableEnd();
    }
}
