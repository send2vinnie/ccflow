using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.Sys;
using BP.WF;
using BP.Web;
public partial class WF_Admin_WFRptDo : WebPage
{
    public new string DoType
    {
        get
        {
            return this.Request.QueryString["DoType"];
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
        switch (this.DoType)
        {
            case "Up":
                RptAttr attrU = new RptAttr(this.MyPK);
                attrU.DoUp();
                this.WinClose();
                break;
            case "Down":
                RptAttr attrD = new RptAttr(this.MyPK);
                attrD.DoDown();
                this.WinClose();
                break;
            case "Del":
                RptAttr attr = new RptAttr();
                attr.MyPK = this.MyPK;
                attr.RetrieveFromDBSources();
                attr.Delete();

                WFRpt rpt = new WFRpt(attr.FK_Rpt);
                rpt.DoGenerView();
                this.WinClose();
                break;
            default:
                break;
        }
    }
}
