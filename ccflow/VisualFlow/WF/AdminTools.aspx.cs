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
using BP.WF.XML;

public partial class WF_AdminTools : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AdminMenus ens = new AdminMenus();
        ens.RetrieveAll();
        string no = "";
        foreach (AdminMenu en in ens)
        {
            if (en.No.Length == 2)
            {
                if (no == "")
                {
                    this.Pub2.AddFieldSet(en.Name);
                    this.Pub2.AddUL();
                    continue;
                }
                else
                {
                    this.Pub2.AddULEnd();
                    this.Pub2.AddFieldSetEnd();
                    this.Pub2.AddFieldSet(en.Name);
                    this.Pub2.AddUL();
                    continue;
                }
            }
            this.Pub2.AddLi("<a href=\"javascript:WinOpen('" + en.Url + "')\">" + en.Name + "</a>");
            no = en.No;
        }
    }
}
