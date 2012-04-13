using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CCOA_Controls_XToolBar : System.Web.UI.UserControl
{
    /// <summary>
    /// 标题
    /// </summary>
    //[Parameter(Title = "标题", Type = "String", DefaultValue = "")]
    public string Title = "";

    public string ImgScr = "../../CCOA/Images/ico/list.png";

    /// <summary>
    /// 
    /// </summary>
    public string AddUrl = "#";
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