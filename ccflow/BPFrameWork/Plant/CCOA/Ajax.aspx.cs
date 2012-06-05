using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lizard.Tools;

public partial class EIP_Ajax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BP.CCOA.EIP_LayoutDetail dal = new BP.CCOA.EIP_LayoutDetail();
        var ds = dal.GetJsonList("");

        string jsonData = JsonHelper.DataTable2Json(ds.Tables[0]);
        jsonData = jsonData.Replace("showcollapsebutton", "showCollapseButton");

        Response.Write(jsonData);
    }

    public string Save()
    {
        return "save successful";
    }
}