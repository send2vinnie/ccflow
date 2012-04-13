using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BP.DA;

public partial class Port_NewHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    public void BindData()
    {
        string sql = "select top 5 * from OA_Article where ArticleType = 0";
        DataTable table = DBAccess.RunSQLReturnTable(sql);
        rpt1.DataSource = table;
        rpt1.DataBind();
    }

}