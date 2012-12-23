﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_UC_Tools : BP.Web.UC.UCBase3
{
    public string _PageSamll = null;
    public string PageSmall
    {
        get
        {
            if (_PageSamll == null)
            {
                if (this.PageID.ToLower().Contains("small"))
                    _PageSamll = "Small";
                else
                    _PageSamll = "";
            }
            return _PageSamll;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.WF.XML.Tools tools = new BP.WF.XML.Tools();
        tools.RetrieveAll();

        if (tools.Count == 0)
            return;

        string refno = this.RefNo;
        if (refno == null)
            refno = "Per";

        this.Left.AddTable("border=0");
        this.Left.AddTR();
        if (WebUser.IsWap)
            this.Left.AddTDTitle("<a href='Home.aspx'><img src='./Img/Home.gif' border=0/>Home</a>");
        else
            this.Left.AddTDTitle("系统设置");
        this.Left.AddTREnd();

        this.Left.AddTR();
        this.Left.AddTDBigDocBegain();
        this.Left.AddUL();
        foreach (BP.WF.XML.Tool tool in tools)
        {
            if (tool.No == refno)
                this.Left.AddLi("<b>" + tool.Name + "</b>");
            else
                this.Left.AddLi("Tools" + this.PageSmall + ".aspx?RefNo=" + tool.No, tool.Name, "_self");
        }

        if (WebUser.No == "admin")
            this.Left.AddLi("Tools" + this.PageSmall + ".aspx?RefNo=AdminSet", this.ToE("SiteSet", "网站设置"), "_self");
        this.Left.AddULEnd();

        this.Left.AddTDEnd();
        this.Left.AddTREnd();
        this.Left.AddTableEnd();
    }
}
