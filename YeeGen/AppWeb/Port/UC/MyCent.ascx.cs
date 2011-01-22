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
    public partial class MyCent : BP.Web.UC.UCBase3
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CBuesss cbs = new CBuesss();
            cbs.Retrieve(CBuessAttr.FK_Member, Glo.MemberNo);

            this.AddFieldSet("积分明细");

            this.AddTable();
            this.AddTR();
            this.AddTDTitle("日期");
            this.AddTDTitle("业务");
            this.AddTDTitle("积分");
            this.AddTDTitle("备注");
            this.AddTREnd();

            bool is1 = false;
            foreach (CBuess en in cbs)
            {
                is1 = this.AddTR(is1);
                this.AddTD(en.RDT);
                this.AddTD(en.FK_CBuess);
                this.AddTD(en.Cent);
                this.AddTD(en.Note);
                this.AddTREnd();
            }
            this.AddTableEnd();

            this.AddFieldSetEnd();
        }
    }
}