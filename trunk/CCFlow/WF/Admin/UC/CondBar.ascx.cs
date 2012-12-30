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
using BP.WF.XML;
using BP.Port;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.Web;

public partial class WF_Admin_UC_CondBar :BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["CondType"] != "2")
            return;

        BP.WF.XML.CondTypeXmls xmls = new CondTypeXmls();
        xmls.RetrieveAll();

        this.Add("<div id='tabsJ' style='height=30px;' >");
        this.AddUL();
        foreach (CondTypeXml item in xmls)
        {
            if (item.No == this.PageID)
                this.Add("<li><a href=#><span><b>" + item.Name + "</b></span></a></li>");
            else
                this.AddLi("<a href='" + item.No + ".aspx?CondType=" + this.Request["CondType"] + "&FK_Flow=" + this.Request["FK_Flow"] + "&FK_MainNode=" + this.Request["FK_MainNode"] + "&FK_Node=0&FK_Attr=" + this.Request["FK_Attr"] + "&DirType=" + this.Request["DirType"] + "&ToNodeID=" + this.Request["ToNodeID"] + "'><span>" + item.Name + "</span></a>");
        }
        this.AddULEnd();
        this.AddDivEnd();
    }
}
