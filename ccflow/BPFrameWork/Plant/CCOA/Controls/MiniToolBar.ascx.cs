using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

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

    public bool CanAdd = true;

    public string IsDeleteUrlHaveParamerter = "false";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InitToolBar();
        }
    }

    private void InitToolBar()
    {
        //AddLinkButton("icon-reload", "返回", RetrunUrl);
        if (ShowAddBtn)
        {
            AddLinkButton("icon-addfolder", "增加", AddUrl, CanAdd.ToString().ToLower());
        }
        if (ShowDelBtn)
        {
            AddClickButton("icon-remove", "删除", "getSelectedIdList()");
        }
        //AddClickButton("icon-addfolder", "增加（弹窗）", "add()");
        //AddSeparator();
        //AddLinkButton("icon-reload", "刷新", RefreshUrl);
        //AddLinkButton("icon-download", "下载", AddUrl);
    }

    private bool showAddBtn = true;
    public bool ShowAddBtn
    {
        get
        {
            return showAddBtn;
        }
        set
        {
            showAddBtn = value;
        }
    }
    private bool showDelBtn = true;
    public bool ShowDelBtn
    {
        get
        {
            return showDelBtn;
        }
        set
        {
            showDelBtn = value;
        }
    }

    public void AddLinkButton(string icon, string name, string url)
    {
        StringBuilder sbrHtml = new StringBuilder();
        sbrHtml.Append(this.ButtonContainers.InnerHtml);
        sbrHtml.AppendFormat("<a class='mini-button' iconcls='{0}' plain='true' href='{1}'>{2}</a> ", new string[] { icon, url, name });

        this.ButtonContainers.InnerHtml = sbrHtml.ToString();
    }

    public void AddClickButton(string icon, string name, string clickevent)
    {
        StringBuilder sbrHtml = new StringBuilder();
        sbrHtml.Append(this.ButtonContainers.InnerHtml);
        sbrHtml.AppendFormat("<a class='mini-button' iconcls='{0}' plain='true' onclick='{1}'>{2}</a> ",
            new string[] { icon, clickevent, name });

        this.ButtonContainers.InnerHtml = sbrHtml.ToString();
    }
    public void AddLinkButton(string icon, string name, string url, string isEnable)
    {
        StringBuilder sbrHtml = new StringBuilder();
        sbrHtml.Append(this.ButtonContainers.InnerHtml);
        sbrHtml.AppendFormat("<a class='mini-button' iconcls='{0}' plain='true' href='{1}' enabled='{3}'>{2}</a> ",
            new string[] { icon, url, name, isEnable });

        this.ButtonContainers.InnerHtml = sbrHtml.ToString();
    }

    public void AddSeparator()
    {
        StringBuilder sbrHtml = new StringBuilder();
        sbrHtml.Append(this.ButtonContainers.InnerHtml);
        sbrHtml.Append("<span class='separator'></span>");

        this.ButtonContainers.InnerHtml = sbrHtml.ToString();
    }
}