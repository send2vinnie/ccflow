using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BP.Web;
using BP.En;
using BP.DA;
using BP.GE;

public partial class GE_FDB_FDBDir : BP.Web.UC.UCBase3
{
    public string FK_Dir
    {
        get
        {
            return this.Request.QueryString["FK_Dir"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Title样式
        string groupTitle = BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s","GroupTitleCSS");

        this.Pub2.AddTable();
        this.Pub2.AddTR();
        this.Pub2.AddTD("class='" + groupTitle + "'", "资源列表");
        this.Pub2.AddTREnd();

        this.Pub2.AddTR();
        this.Pub2.AddTDBegin("height='500px'");
        this.Pub2.AddIframe("FDBDtl.aspx?FK_Dir=" + this.FK_Dir);
        this.Pub2.AddTDEnd();
        this.Pub2.AddTREnd();

        this.Pub2.AddTableEnd();

    }
}
