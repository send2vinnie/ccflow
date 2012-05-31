using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lizard.Tools;

public partial class CCOA_AjaxMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.EIP.Port_Menu dal = new BP.EIP.Port_Menu();
        var ds = dal.GetJsonList("");

        string jsonData = JsonHelper.DataTable2Json(ds.Tables[0]);

        Response.Write(jsonData);
    }
}