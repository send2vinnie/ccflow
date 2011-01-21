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

public partial class GE_FDB_PartDirTab : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.GE.FDBDirs en1s = new FDBDirs();
        en1s.Retrieve(BP.GE.FDBDirAttr.Grade, 1);

        BP.GE.FDBDirs dtls = new FDBDirs();
        dtls.RetrieveAll();

        foreach (BP.GE.FDBDir en1 in en1s)
        {
            string msg = "";
            foreach (BP.GE.FDBDir dtl in dtls)
            {
                if (dtl.Grade == 1)
                    continue;

                if (dtl.No.Substring(0, 2) != en1.No)
                    continue;

                msg += " <a href='FDBDir.aspx?FK_Dir=" + dtl.No + "'>" + dtl.Name + "</a> ";
            }

          //  this.GeTab1.Tabs.Add(en1.Name, msg);
        }
    }
}
