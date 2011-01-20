using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace BP.YG.WebUI.Info
{
    public partial class Dtl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int oid = int.Parse( this.Request.QueryString["OID"]);
            BP.YG.Infos ens = new BP.YG.Infos();
            BP.En.QueryObject qo = new BP.En.QueryObject(ens);
            qo.AddWhere(BP.YG.InfoAttr.FK_Item, "0101");
            qo.addAnd(); 
            qo.AddWhere(BP.YG.InfoAttr.ShareType, "3");
            qo.DoQuery();
            foreach (BP.YG.Info myen in ens)
            {
                this.Response.Write(myen.Title);
            }

            BP.YG.Info en = new BP.YG.Info();
            en.OID = oid;
            en.Retrieve();

            this.Response.Write(en.Title);

        }
    }
}
