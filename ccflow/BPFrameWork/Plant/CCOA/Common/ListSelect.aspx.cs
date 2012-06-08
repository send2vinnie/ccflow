using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CCOA_Common_ListSelect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"];
        switch (type)
        {
            case "dept":
                Url = "LoadDeptTree.aspx";
                break;
            case "role":
                Url = "LoadRoleTree.aspx";
                break;
            case "emp":
                Url = "LoadPeopleTree.aspx";
                break;
            default:
                Url = "LoadDeptTree.aspx";
                break;
        }
    }
    public string Url { get; set; }
}