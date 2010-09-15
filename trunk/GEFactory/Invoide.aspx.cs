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
using BP.KG;
using BP.En;
using BP.DA;

public partial class Invoide : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Invoice en = new Invoice();
        en.CheckPhysicsTable();

        en.No = "0002";
        en.Name = "hi dear3333";
        en.Insert();

        PayDtl dtl = new PayDtl(); // en = new Invoice();
        dtl.FK_Invoice = "0002";
        dtl.JE = 90000;
        dtl.Insert();
    //    dtl.



    }
}
