﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Port;
using BP.Sys;
using BP.Web.Controls;
using BP.DA;
using BP.En;

public partial class WF_MyFlowView : BP.Web.WebPage 
{
    public int WorkID
    {
        get
        {
            return int.Parse(this.Request.QueryString["WorkID"]);
        }
    }
    public int FK_Node
    {
        get
        {
            return int.Parse(this.Request.QueryString["FK_Node"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
        Work wk = nd.HisWork;
        wk.OID = this.WorkID;
        wk.RetrieveFromDBSources();

        this.UCEn1.IsReadonly = true;
        if (nd.HisFormType == FormType.FreeForm)
        {
            this.UCEn1.BindFreeFrm(wk, "ND" + wk.NodeID, true);
        }
        else
        {
            this.UCEn1.IsReadonly = true;
            this.UCEn1.BindColumn4(wk, "ND" + wk.NodeID);
        }
    }
}