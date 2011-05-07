using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Do : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (this.Request["DoType"].ToString())
        {
            case "":
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string GenerInsallSql()
    {
        return null;
    }
}