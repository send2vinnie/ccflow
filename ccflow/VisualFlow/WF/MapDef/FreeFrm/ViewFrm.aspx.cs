using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WF_MapDef_FreeFrm_ViewFrm : System.Web.UI.Page
{
    public string FK_MapData
    {
        get
        {
            return this.Request.QueryString["FK_MapData"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int nodeid= int.Parse(this.FK_MapData.Replace("ND",""));
        BP.WF.Node nd =new BP.WF.Node(nodeid);
        BP.WF.Work work =new BP.WF.GEStartWork(nd.NodeID);
        this.UCEn1.BindFreeFrm(work, this.FK_MapData);
    }
}