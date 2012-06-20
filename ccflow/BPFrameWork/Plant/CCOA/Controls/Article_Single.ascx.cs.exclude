using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CCOA;

public partial class CCOA_Controls_Article_Single : BP.Web.UC.UCBase3
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 文章业务助手
    /// </summary>
    //protected ArticleHelper ArticleHelper
    //{
    //    get { return HelperFactory.GetHelper<ArticleHelper>(); }
    //}

    /// <summary>
    /// 栏目业务助手
    /// </summary>
    //protected ChannelHelper ChannelHelper
    //{
    //    get { return HelperFactory.GetHelper<ChannelHelper>(); }
    //}

    /// <summary>
    /// 当前文章
    /// </summary>
    private Article thisArticle;
    /// <summary>
    /// 相关文章
    /// </summary>
    private List<Article> relevantArticles;

    /// <summary>
    /// 上一篇
    /// </summary>
    private Article previousArticle;

    /// <summary>
    ///下一篇 
    /// </summary>
    private Article nextArticle;

    /// <summary>
    /// 相关文章条数
    /// </summary>
    //[Parameter(Title = "相关文章条数", Type = "Number", DefaultValue = "3")]
    public int PageSize = 3;

    /// <summary>
    /// 标题长度
    /// </summary>
    //[Parameter(Title = "标题长度", Type = "Number", DefaultValue = "30")]
    public int TitleLength = 30;

    /// <summary>
    /// 日期格式
    /// </summary>
    //[Parameter(Title = "日期格式", Type = "String", DefaultValue = "[MM-dd]")]
    public string DateFormat = "[MM-dd]";
 
    /// <summary>
    /// 文章ID
    /// </summary>
    public string ArticleID
    {
        get
        {
            //return ArticleHelper.GetArticleIDFromURL();
            return string.Empty;
        }
    }

    /// <summary>
    /// 获得当前栏目下的第一篇文章
    /// </summary>
    /// <returns></returns>
    protected Article GetThisArticle()
    {
        //string id = ChannelHelper.GetChannelIDFromURL();
        //Channel ch = ChannelHelper.GetChannel(id, null);

        //Criteria c = new Criteria(CriteriaType.Equals, "ChannelFullUrl", ch.FullUrl);
        //c.Add(CriteriaType.Equals, "State", 1);
        //Order[] os = new Order[] { new Order("Updated", OrderMode.Desc) };
        //List<Article> aList = Assistant.List<Article>(c, os, 0, 1);
        //if (aList != null && aList.Count > 0)
        //{
        //    return aList[0];
        //}
        //else
        //{
            return new Article();
        //}
    }

    /// <summary>
    /// 当前文章
    /// </summary>
    protected Article ThisArticle
    {
        get
        {
            //if (thisArticle == null)
            //{
            //    if (!We7Helper.IsEmptyID(ArticleID))
            //    {
            //        Criteria c = new Criteria(CriteriaType.Equals, "ID", ArticleID);
            //        c.Add(CriteriaType.Equals, "State", 1);
            //        Order[] os = new Order[] { new Order("Updated", OrderMode.Desc) };
            //        List<Article> aList = Assistant.List<Article>(c, os, 0, 1);
            //        if (aList != null && aList.Count > 0)
            //        {
            //            thisArticle = aList[0];
            //        }
            //    }
            //    else
            //    {
            //        thisArticle = GetThisArticle();
            //    }
            //}
            return thisArticle ?? new Article(); ;
        }
        set
        {
            thisArticle = value;
        }
    }

    /// <summary>
    /// 上一篇
    /// </summary>
    protected Article PreviousArticle
    {
        get
        {
            //if (previousArticle == null)
            //{
            //    Criteria c = new Criteria(CriteriaType.None);
            //    c.Add(CriteriaType.MoreThan, "Updated", ThisArticle.Updated);
            //    c.Add(CriteriaType.Equals, "State", 1);
            //    Order[] os = new Order[] { new Order("Updated", OrderMode.Asc) };
            //    List<Article> aList = Assistant.List<Article>(c, os, 0, 1);
            //    if (aList != null && aList.Count > 0)
            //    {
            //        previousArticle = aList[0];
            //    }
            //}
            return previousArticle;
        }
    }

    /// <summary>
    /// 下一篇
    /// </summary>
    protected Article NextArticle
    {
        get
        {
            //if (nextArticle == null)
            //{
            //    Criteria c = new Criteria(CriteriaType.None);
            //    c.Add(CriteriaType.LessThan, "Updated", ThisArticle.Updated);
            //    c.Add(CriteriaType.Equals, "State", 1);
            //    Order[] os = new Order[] { new Order("Updated", OrderMode.Desc) };
            //    List<Article> aList = Assistant.List<Article>(c, os, 0, 1);
            //    if (aList != null && aList.Count > 0)
            //    {
            //        nextArticle = aList[0];
            //    }
            //}
            return nextArticle;
        }
    }

    /// <summary>
    /// 相关文章
    /// </summary>
    protected List<Article> RelevantArticles
    {
        get
        {
            //if (relevantArticles == null)
            //{
            //    if (!We7Helper.IsEmptyID(ArticleID))
            //    {
            //        Criteria c = new Criteria(CriteriaType.None);
            //        c.Add(CriteriaType.Equals, "OwnerID", ThisArticle.OwnerID);
            //        c.Add(CriteriaType.Equals, "State", 1);
            //        Order[] os = new Order[] { new Order("Updated", OrderMode.Desc) };
            //        List<Article> aList = Assistant.List<Article>(c, os, 0, PageSize);
            //        if (aList != null && aList.Count > 0)
            //        {
            //            relevantArticles = aList;
            //        }
            //    }
            //}
            return relevantArticles;
        }
    }
}