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
    public partial class DocList : BP.Web.UC.UCBase3
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Docs ens = new Docs();
            ens.Retrieve(DocAttr.FK_Member, Glo.MemberNo);

            this.AddFieldSet("日志列表");
            this.AddUL();
            foreach (BP.YG.Doc en in ens)
            {
                this.AddLi("DocReader.aspx?OID=" + en.OID, en.Name + "(" + en.ReadTimes + ")");
            }
            this.AddULEnd();
            this.AddFieldSetEnd();
        }
    }
}