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
using BP.Port;
using BP.YG;
using BP.DA;

namespace BP.YG.WebUI.Port.UC
{
    public partial class DocReader : BP.Web.UC.UCBase3
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Doc doc = new Doc(int.Parse(this.Request.QueryString["OID"]));
            this.AddH3(doc.Name);
            this.AddHR();
            this.Add(doc.DocHtml);

            //     doc.ReadTimes = doc.ReadTimes + 1;
            doc.Update("ReadTimes", doc.ReadTimes + 1);
        }
    }
}