using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lizard.Tools;
using System.Data;

public partial class CCOA_AjaxMenu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string app = string.Empty;
        if (Session["CurrentApp"] != null)
        {
            app = Session["CurrentApp"].ToString();
        }
        BP.CCOA.EIP_Menu dal = new BP.CCOA.EIP_Menu();
        DataSet ds = new DataSet();
        if (app != string.Empty)
        {
            ds = dal.GetJsonList(" APPNAME ='" + app + "'");
        }
        else
        {
            ds = dal.GetJsonList("");
        }

        string jsonData = JsonHelper.DataTable2Json(ds.Tables[0]);
        jsonData = jsonData.Replace("iconcls", "iconCls");

        Response.Write(jsonData);
    }
}