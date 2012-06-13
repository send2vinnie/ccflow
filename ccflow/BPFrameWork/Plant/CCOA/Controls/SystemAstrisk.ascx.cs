using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BP.Web;

public partial class CCOA_Controls_SystemAstrisk : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable dt = BP.WF.Dev2Interface.DB_GenerEmpWorksOfDataTable(WebUser.No);
            TodoWorkCount = dt.Rows.Count.ToString();
        }
    }

    public string TodoWorkCount { get; set; }
}