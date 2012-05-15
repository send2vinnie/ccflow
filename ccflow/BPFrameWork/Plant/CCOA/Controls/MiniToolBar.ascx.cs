using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CCOA_Controls_MiniToolBar : System.Web.UI.UserControl
{
    /// <summary>
    /// 标题
    /// </summary>
    //[Parameter(Title = "标题", Type = "String", DefaultValue = "")]
    public string Title = "标题";

    public string ImgScr = "../../CCOA/Images/ico/list.png";

    /// <summary>
    /// 
    /// </summary>
    public string AddUrl = "Add.aspx";
    /// <summary>
    /// 
    /// </summary>
    public string RefreshUrl = "List.aspx";
    /// <summary>
    /// 
    /// </summary>
    public string RetrunUrl = "List.aspx";
    /// <summary>
    /// 
    /// </summary>
    public string ViewUrl = "#";
    /// <summary>
    /// 
    /// </summary>
    public string EditUrl = "#";
    /// <summary>
    /// 
    /// </summary>
    public string DeleteUrl = "#";
    /// <summary>
    /// 
    /// </summary>
    public string SearchUrl = "#";


    protected void Page_Load(object sender, EventArgs e)
    {

    }
}