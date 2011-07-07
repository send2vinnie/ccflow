using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.En;
using BP.DA;
using BP.Sys;

public partial class WF_Frm : System.Web.UI.Page
{
    public string FK_MapData
    {
        get
        {
            string s = this.Request.QueryString["FK_MapData"];
            if (s == null)
                return "ND101";
            return s;
        }
    }
    public int WorkID
    {
        get
        {
            try
            {
                return int.Parse(this.Request.QueryString["WorkID"]);
            }
            catch
            {
                return 0;
            }
        }
    }
    public bool IsReadonly
    {
        get
        {
            if (this.Request.QueryString["IsReadonly"] == "1")
                return true;
            return false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        MapData md = new MapData(this.FK_MapData);
        GEEntity en = md.HisGEEn;
        en.SetValByKey("OID", this.WorkID);
        this.UCEn1.BindFreeFrm(en, this.FK_MapData,this.IsReadonly);
        this.Button1.Enabled = !this.IsReadonly;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}