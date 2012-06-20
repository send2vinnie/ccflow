using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCOA_ComUC_Article_Newest : BP.Web.UC.UCBase3
{
    private Articles articles;
    private Channel channel;

    public string ShowUrl { get; set; }

    public string SetUrl(string no)
    {
        return ShowUrl + "?no=" + no;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 栏目ID
    /// </summary>
    //[Parameter(Title = "栏目", Type = "Channel", Required = true)]
    public string OwnerID = String.Empty;

    /// <summary>
    /// 显示记录条数
    /// </summary>
    //[Parameter(Title = "控件每页记录", Type = "Number", DefaultValue = "10")]
    public int PageSize = 10;

    /// <summary>
    /// 标题
    /// </summary>
    //[Parameter(Title = "标题", Type = "String", DefaultValue = "")]
    public string Title = "";

    /// <summary>
    /// 日期格式
    /// </summary>
    //[Parameter(Title = "日期格式", Type = "String", DefaultValue = "[MM-dd]")]
    public string DateFormat = "[MM-dd]";

    /// <summary>
    /// 当前栏目信息
    /// </summary>
    public Channel Channel
    {
        //get
        //{
        //    if (channel == null)
        //    {
        //        ChannelHelper helper = HelperFactory.GetHelper<ChannelHelper>();
        //        channel = helper.GetChannel(OwnerID, null) ?? new Channel();
        //    }
        //    return channel;
        //}
        get { return channel; }
        set { channel = value; }
    }

    /// <summary>
    /// 文章列表
    /// </summary>
    protected Articles Articles
    {
        get
        {
            if (articles == null)
            {
                //Criteria c = new Criteria(CriteriaType.None);
                //if (IncludeChildren)
                //{
                //    c.Add(CriteriaType.Like, "ChannelFullUrl", Channel.FullUrl + "%");
                //}
                //else
                //{
                //    c.Add(CriteriaType.Equals, "OwnerID", OwnerID);
                //}
                //c.Add(CriteriaType.Equals, "State", 1);
                //if (!String.IsNullOrEmpty(Tags))
                //{
                //    c.Add(CriteriaType.Like, "Tags", "%'" + Tags + "'%");
                //}

                //Order[] os = IsShow ? new Order[] { new Order("IsShow", OrderMode.Desc), new Order("Updated", OrderMode.Desc) } : new Order[] { new Order("Updated", OrderMode.Desc) };
                //articles = Assistant.List<Article>(c, os, 0, PageSize);
                articles = new BP.CCOA.Articles();
                articles.RetrieveAll();
            }
            return articles;
        }
        set { articles = value; }
    }
}