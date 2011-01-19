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
using BP.YG;

namespace Tax666.AppWeb
{
    public partial class ZJ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ZJs ens = new ZJs();
            ens.RetrieveAll();
            this.Pub1.AddTable("width=100%");
            bool is1 = false;
            foreach (BP.YG.ZJ en in ens)
            {
                is1 = this.Pub1.AddTR(is1);
                this.Pub1.AddTD("valign=top", "<img src='./Data/BP.YG.ZJs/" + en.OID + "." + en.MyFileExt + "' width='90px' height='90px' border=0/>");
                this.Pub1.AddTDBigDoc("valign=top align=left", "<b>" + en.Name + "</b><br>" + en.DocS);
                this.Pub1.AddTREnd();
            }
            this.Pub1.AddTableEnd();
        }
    }
}
