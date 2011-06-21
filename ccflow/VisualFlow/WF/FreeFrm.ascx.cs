using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Web;
using BP.En;
using BP.Web.Controls;
using BP.Sys;

public partial class WF_FreeFrm : BP.Web.UC.UCBase3
{
    public string FK_MapData
    {
        get
        {
            return "ND"+this.NodeID;
        }
    }
    public int NodeID
    {
        get
        {
            return 410;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       // this.Init();

        BP.PubClass.InitFrm(this.FK_MapData);

        BP.WF.Node nd = new BP.WF.Node(this.NodeID);
        Entity en = nd.HisWork;
        en.SetValByKey("OID", 100);
        en.RetrieveFromDBSources();
        this.UCEn1.BindFreeFrm(en, FK_MapData);

    }

    public void Init()
    {

        BP.PubClass.InitFrm(this.FK_MapData);


        //开始画左右边的竖线.
    }
}