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

public partial class Comm_GE_Pict_ImgRun : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Picts ens = new Picts();
        ens.Retrieve(PictAttr.PictSta, (int)PictSta.Hot);
       // this.AddMsgOfInfo("宣转图片", "");
    }
}
