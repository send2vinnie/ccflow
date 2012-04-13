using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;
using BP.Port;

public partial class CCOA_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Channel channel = new Channel();
            channel.CheckPhysicsTable();

            Article article = new Article();
            article.CheckPhysicsTable();

            ArticleType type = new ArticleType();
            type.CheckPhysicsTable();

            EmpInfo empinfo = new EmpInfo();
            empinfo.CheckPhysicsTable();

            AddrBook ab = new AddrBook();
            ab.CheckPhysicsTable();

            AddrBookDept abd = new AddrBookDept();
            abd.CheckPhysicsTable();
        }

    }
}