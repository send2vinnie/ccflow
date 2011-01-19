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
using BP.Web.Port.Xml;

namespace Tax666.AppWeb.Manager
{
    public partial class LeftMenu : PageBase
    {
        public int menuid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            BP.Port.Emp emp = new BP.Port.Emp("admin");
            BP.Web.WebUser.SignInOfGener(emp);


            BP.Web.Port.Xml.MenuLefts ens = new BP.Web.Port.Xml.MenuLefts();
            ens.RetrieveAll();

            this.Pub1.AddTable();
            foreach (MenuLeft en in ens)
            {
                this.Pub1.AddTR();
                if (en.Url.Length > 3)
                    this.Pub1.AddTDA(en.Url, en.Name, "mainframe");
                else
                    this.Pub1.AddTD(en.Name);

                this.Pub1.AddTREnd();
            }
            this.Pub1.AddTableEnd();
        }
    }
}
