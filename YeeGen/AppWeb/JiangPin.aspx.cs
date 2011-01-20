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
using BP.YG;

namespace Tax666.AppWeb
{
    public partial class JiangPin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            JiangPins ens = new JiangPins();
            ens.RetrieveAll();
            this.Pub1.AddTable("width=100%");
            this.Pub1.AddTR();
            this.Pub1.AddTDTitle("编号");
            this.Pub1.AddTDTitle("名称");
            this.Pub1.AddTDTitle("分值");
            this.Pub1.AddTREnd();

            bool is1 = false;
            foreach (BP.YG.JiangPin en in ens)
            {
                is1 = this.Pub1.AddTR(is1);

                this.Pub1.AddTD(en.No);
                this.Pub1.AddTD(en.Name);
                this.Pub1.AddTD(en.PayCent);

                this.Pub1.AddTREnd();
            }
            this.Pub1.AddTableEnd();

        }
    }
}
