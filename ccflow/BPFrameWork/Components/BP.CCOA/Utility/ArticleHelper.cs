using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Text.RegularExpressions;
using BP.CCOA.Enum;

namespace BP.CCOA.Utility
{
    public class ArticleHelper
    {

        #region 基本操作：插入、删除、更新、获取

        /// <summary>
        /// 根据条件获取文章数
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        //public int QueryArtilceCount(Criteria c)
        //{
        //    return Assistant.Count<Article>(c);
        //}

        /// <summary>
        /// 根据ID删除一篇文章
        /// </summary>
        /// <param name="id">文章ID</param>
        public void DeleteArticle(string id)
        {
            //删除文章
            //Article a = new Article();
            //a.ID = id;
            //Assistant.Delete(a);
            ////删除相关文章
            //DeleteRelatedArticles(id);
            //Criteria ch = new Criteria(CriteriaType.Equals, "ArticleID", id);
            ////删除引用

            ////删除附件
            //List<Attachment> atts = Assistant.List<Attachment>(ch, null);
            //foreach (Attachment att in atts)
            //{
            //    string file = HttpContext.Current.Server.MapPath(att.FilePath + "/" + att.FileName);
            //    if (File.Exists(file))
            //        File.Delete(file);
            //    Assistant.Delete(att);
            //}
            ////清除wap相关

            ////删除文章标签

            ////删除评论
            //List<Comments> coms = Assistant.List<Comments>(ch, null);
            //foreach (Comments c in coms)
            //{
            //    Assistant.Delete(c);
            //}
        }

        /// <summary>
        /// 获取一篇文章
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <param name="fields">返回的字段集合</param>
        /// <returns></returns>
        public Article GetArticle(string id, string[] fields)
        {
            Article a = new Article();
            //Criteria c = new Criteria(CriteriaType.Equals, "ID", id);
            //List<Article> aList = Assistant.List<Article>(c, null, 0, 0, fields);
            //if (aList != null && aList.Count > 0)
            //{
            //    a = aList[0];
            //}
            return a;
        }

        private static readonly string ArticleKeyID = "$ArticleID{0}";
        /// <summary>
        /// 获取一篇文章（使用了缓存）
        /// </summary>
        /// <param name="ArticleID">文章ID</param>
        /// <returns></returns>
        public Article GetArticle(string ArticleID)
        {
            if (ArticleID != null && ArticleID != string.Empty)
            {
                HttpContext Context = HttpContext.Current;
                string key = string.Format(ArticleKeyID, ArticleID);
                Article article = (Article)Context.Items[key];//内存
                if (article == null)
                {
                    article = (Article)Context.Cache[key];//缓存
                    //if (article == null)
                    //{
                    //    if (ArticleID != null && ArticleID != string.Empty)
                    //    {
                    //        //读取数据库
                    //        Order[] o = new Order[] { new Order("ID") };
                    //        Criteria c = new Criteria(CriteriaType.Equals, "ID", ArticleID);
                    //        List<Article> articles = Assistant.List<Article>(c, o);
                    //        if (articles.Count > 0)
                    //        {
                    //            article = articles[0];
                    //        }
                    //    }

                    //    if (article != null)
                    //    {
                    //        CacherCache(key, Context, article, CacheTime.Short);
                    //    }
                    //}

                    if (Context.Items[key] == null)
                    {
                        Context.Items.Remove(key);
                        Context.Items.Add(key, article);
                    }
                }
                return article;
            }
            else
                return null;

        }

        /// <summary>
        /// 通过ID获取文章标题
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <returns></returns>
        public string GetArticleName(string id)
        {
            //Criteria c = new Criteria(CriteriaType.Equals, "ID", id);
            //List<Article> articles = Assistant.List<Article>(c, null);
            //if (articles.Count > 0)
            //    return articles[0].Title;
            //else
            //    return "";

            Article article = new Article();
            article.RetrieveByNo(id);
            if (article != null)
            {
                return article.Title;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 通过SN获取栏目ID
        /// </summary>
        /// <param name="sn">栏目SN</param>
        /// <returns></returns>
        public string GetArticleIDBySN(string sn)
        {
            //Criteria c = new Criteria(CriteriaType.Equals, "SN", sn);
            //List<Article> articles = Assistant.List<Article>(c, null);
            //if (articles.Count > 0)
            //    return articles[0].ID;
            //else
            //    return "";

            Article article = new Article();
            article.RetrieveByAttr("SN", sn);
            if (article != null)
            {
                return article.Title;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 添加一篇文章
        /// </summary>
        /// <param name="a">一篇文章对象</param>
        /// <returns></returns>
        public Article AddArticles(Article a)
        {
            //if (a.Created == DateTime.MinValue)
            //    a.Created = DateTime.Now;
            //if (a.Updated == DateTime.MinValue)
            //    a.Updated = a.Created;
            //if (string.IsNullOrEmpty(a.ID))
            //    a.ID = We7Helper.CreateNewID();
            //a.SN = CreateArticleSN();
            //a.Clicks = 0;
            //a.CommentCount = 0;
            //if (String.IsNullOrEmpty(a.ModelName))
            //{
            //    a.ModelName = Constants.ArticleModelName;
            //}
            //Assistant.Insert(a);
            return a;
        }

        /// <summary>
        /// 添加一篇文章
        /// </summary>
        /// <param name="a">一篇文章对象</param>
        public void AddArticle(Article a)
        {
            //if (a.Created == DateTime.MinValue)
            //    a.Created = DateTime.Now;
            //if (a.Updated == DateTime.MinValue)
            //    a.Updated = a.Created;
            //if (string.IsNullOrEmpty(a.ID))
            //    a.ID = We7Helper.CreateNewID();
            //a.SN = CreateArticleSN();
            //if (String.IsNullOrEmpty(a.ModelName))
            //{
            //    a.ModelName = Constants.ArticleModelName;
            //}
            //a.Clicks = 0;
            //a.CommentCount = 0;
            //Assistant.Insert(a);
        }

        /// <summary>
        /// 创建文章的sn
        /// </summary>
        /// <returns></returns>
        //public long CreateArticleSN()
        //{
        //CreateSNHelper helper = new CreateSNHelper();
        //helper.Assistant = Assistant;
        //return helper.SnBase;
        //}

        /// <summary>
        /// 更新一篇文章记录
        /// </summary>
        /// <param name="a">一篇文章记录</param>
        /// <param name="fields">需要更新的字段</param>
        public void UpdateArticle(Article a, string[] fields)
        {
            //清除缓存
            HttpContext Context = HttpContext.Current;
            string key = string.Format(ArticleKeyID, a.No);
            Context.Cache.Remove(key);
            Context.Items.Remove(key);

            //a.Updated = DateTime.Now;
            //Assistant.Update(a, fields);
        }

        /// <summary>
        /// 查找某个站点共享过来的文章
        /// </summary>
        /// <param name="ownerID">栏目ID</param>
        /// <param name="sourceID">站点ID</param>
        /// <returns></returns>
        public Article GetArticleBySource(string ownerID, string sourceID)
        {
            //Criteria c = new Criteria(CriteriaType.Equals, "SourceID", sourceID);
            //if (ownerID != null)
            //    c.Add(CriteriaType.Equals, "OwnerID", ownerID);
            //Order[] orders = new Order[] { new Order("Updated", OrderMode.Desc) };
            //List<Article> articles = Assistant.List<Article>(c, orders);
            //if (articles.Count > 0)
            //    return articles[0];
            //else
            return null;
        }

        /// <summary>
        /// 更新一篇文章的流转状态
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <param name="ProcessState">流转状态</param>
        /// <param name="state">文章状态</param>
        public void UpdateArticleProcess(string id, string ProcessState, ArticleStates state)
        {
            //Article a = GetArticle(id);
            //a.ProcessState = ProcessState;
            //a.State = (int)state;
            //a.Updated = DateTime.Now;
            //Assistant.Update(a, new string[] { "Updated", "ProcessState", "State" });
        }

        /// <summary>
        /// 通过Url取得文章列表
        /// </summary>
        /// <param name="url">用来查询的Url</param>
        /// <param name="from">起始记录</param>
        /// <param name="count">查询条数</param>
        /// <param name="fields">查询的字段</param>
        /// <param name="OnlyInUser">是否是只查询当前用户的文章</param>
        /// <returns>文章列表</returns>
        public List<Article> GetArticlesByUrl(string url, int from, int count, string[] fields, bool OnlyInUser)
        {
            //Criteria c = new Criteria(CriteriaType.Like, "ChannelFullUrl", url);
            //if (OnlyInUser)
            //    c.Add(CriteriaType.Equals, "State", 1);

            //Order[] orders = new Order[] { new Order("Updated", OrderMode.Desc) };
            //return Assistant.List<Article>(c, orders, from, count, fields);

            List<Article> lst = new List<Article>();

            Articles articles = new Articles();
            articles.RetrieveByAttr("ChannelFullUrl", url);

            foreach (Article item in articles)
            {
                lst.Add(item);
            }

            return lst;
        }

        #endregion


        #region 特殊方法
        /// <summary>
        /// 按照配置文件加水印到图片
        /// </summary>
        /// <param name="ImageConfig">图片配置类</param>
        /// <param name="thumbnailFile">加水印后文件</param>
        /// <param name="originalFilePath">原文件</param>
        //public static void AddWatermarkToImage(GeneralConfigInfo ImageConfig, string thumbnailFile, string originalFilePath)
        //{
        //    if (ImageConfig.WaterMarkStatus != 0)
        //    {
        //        string waterparkedFile = ImageUtils.GenerateWatermarkFile(originalFilePath);
        //        System.Drawing.Image img = System.Drawing.Image.FromFile(thumbnailFile);
        //        System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
        //        img.Dispose();
        //        img = null;

        //        if (ImageConfig.WaterMarkType == 1 && File.Exists(We7Utils.GetMapPath(ImageConfig.WaterMarkPicfile)))
        //        {
        //            ImageUtils.AddImageSignPic(bmp, waterparkedFile, We7Utils.GetMapPath(ImageConfig.WaterMarkPicfile), ImageConfig.WaterMarkStatus, ImageConfig.AttachImageQuality, ImageConfig.WaterMarkTransparency);
        //        }
        //        else
        //        {
        //            string watermarkText = ImageConfig.WaterMarkText;
        //            //watermarkText = ImageConfig.Watermarktext.Replace("{1}", ImageConfig.Forumtitle);
        //            //watermarkText = watermarkText.Replace("{2}", "http://" + DNTRequest.GetCurrentFullHost() + "/");
        //            //watermarkText = watermarkText.Replace("{3}", Utils.GetDate());
        //            //watermarkText = watermarkText.Replace("{4}", Utils.GetTime());
        //            ImageUtils.AddImageSignText(bmp, waterparkedFile, watermarkText, ImageConfig.WaterMarkStatus, ImageConfig.AttachImageQuality, ImageConfig.WaterMarkFontName, ImageConfig.WaterMarkFontSize);
        //        }

        //        bmp.Dispose();
        //        bmp = null;

        //        if (File.Exists(waterparkedFile))
        //        {
        //            System.IO.File.Delete(thumbnailFile);
        //            System.IO.File.Copy(waterparkedFile, thumbnailFile);
        //            System.IO.File.Delete(waterparkedFile);
        //        }
        //    }
        //}
        /// <summary>
        /// 创建并写入Config文件
        /// </summary>
        /// <param name="path"></param>
        public void Write(string path)
        {
            string str = "<?xml version=\"" + "1.0\"?>" +
"<GeneralConfigInfo xmlns:xsi=\"" + "http://www.w3.org/2001/XMLSchema-instance\"" + " xmlns:xsd=\"" + "http://www.w3.org/2001/XMLSchema\"" + ">" +
"<SiteTitle>We7</SiteTitle>" +
"<IcpInfo />" +
"<RewriteUrl />" +
"<UrlExtName>.aspx</UrlExtName>" +
"<PostInterval>0</PostInterval>" +
"<WaterMarkStatus>3</WaterMarkStatus>" +
"<WaterMarkType>0</WaterMarkType>" +
"<WaterMarkTransparency>5</WaterMarkTransparency>" +
"<WaterMarkText>We7.cn</WaterMarkText>" +
"<WaterMarkPic>watermark.gif</WaterMarkPic>" +
"<WaterMarkFontName>Tahoma</WaterMarkFontName>" +
"<WaterMarkFontSize>12</WaterMarkFontSize>" +
"<AttachImageQuality>80</AttachImageQuality>" +
"<OverdueDateTime>365</OverdueDateTime>" +
"<ADVisbleToSite>0</ADVisbleToSite>" +
"</GeneralConfigInfo>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(str);
            doc.Save(path);
        }

        #endregion

        #region 静态URL
        private static readonly string ArticleKeyName = "$ArticleID{0}";

        /// <summary>
        /// 通过URL取得文章ID号
        /// </summary>
        /// <returns></returns>
        public string GetArticleIDFromURL()
        {
            HttpContext Context = HttpContext.Current;
            if (Context.Request["aid"] != null)
                return Context.Request["aid"];
            else
            {
                return GetArticleIDFromURL(Context.Request.RawUrl);
            }
        }

        /// <summary>
        /// 通过URL取得文章ID号
        /// </summary>
        /// <returns></returns>
        public string GetArticleIDFromURL(string url)
        {
            string id = GetArticleIDOrSNFromUrl(url);
            //if (We7Helper.IsNumber(id))
            return GetArticleIDBySN(id);
            //return id;
        }

        /// <summary>
        /// 从url获取文章id或者SN
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetArticleIDOrSNFromUrl(string path)
        {
            //GeneralConfigInfo si = GeneralConfigs.GetConfig();
            //if (si == null) return "";
            //string ext = si.UrlFormat;
            string ext = "yyyy-MM-dd";
            if (ext == null || ext.Length == 0) ext = "html";

            if (path.LastIndexOf("?") > -1)
            {
                if (path.ToLower().IndexOf("article=") > -1)
                    path = path.Substring(path.ToLower().IndexOf("article=") + 8);
                else
                    path = path.Remove(path.LastIndexOf("?"));
            }

            string mathstr = @"/(\w|\s|(-)|(_))+\." + ext + "$";
            if (path.ToLower().EndsWith("default." + ext))
                path = path.Remove(path.Length - 12);
            if (path.ToLower().EndsWith("index." + ext))
                path = path.Remove(path.Length - 10);

            if (Regex.IsMatch(path, mathstr))
            {
                int lastSlash = path.LastIndexOf("/");
                if (lastSlash > -1)
                {
                    path = path.Remove(0, lastSlash + 1);
                }

                int lastDot = path.LastIndexOf(".");
                if (lastDot > -1)
                {
                    path = path.Remove(lastDot, path.Length - lastDot);
                }

                //if (We7Helper.IsGUID(We7Helper.FormatToGUID(path)))
                //    path = We7Helper.FormatToGUID(path);
                //else
                //{
                //    int lastSub = path.LastIndexOf("-");
                //    if (lastSub > -1)
                //    {
                //        path = path.Remove(0, lastSub + 1);
                //    }

                //    if (!We7Helper.IsNumber(path))
                //        path = "";
                //}

                return path;
            }
            else
                return string.Empty;

        }


        #endregion

        #region 数据移植或数据采集后的数据批量更新

        /// <summary>
        /// 数据移植之后把原有数据建立索引
        /// 禁用（停用）；审核中；过期；回收站的数据部不应该写到索引表里面
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state">信息状态，1—启用；0—禁用（停用）；2—审核中；3—过期；4—回收站</param>
        /// <returns></returns>
        //public List<string> GetDataBystate(int state)
        //{
        //    Criteria c = new Criteria(CriteriaType.Equals, "State", state);
        //    List<Article> articleList = Assistant.List<Article>(c, null);
        //    List<string> stringList = new List<string>();
        //    foreach (Article a in articleList)
        //    {
        //        stringList.Add(a.ID);
        //    }
        //    return stringList;
        //}

        /// <summary>
        /// 用于数据采集来的数据进行批量更新SN
        /// </summary>
        /// <returns></returns>
        public int UpdateSN()
        {
            int count = 0;
            //long sn = 0;
            //Criteria c = new Criteria(CriteriaType.Equals, "SN", 0);
            //List<Article> articleList = Assistant.List<Article>(c, null);
            //if (articleList != null && articleList.Count > 0)
            //{
            //    foreach (Article a in articleList)
            //    {
            //        if (count == 0)
            //        {
            //            sn = CreateArticleSN();
            //        }
            //        a.SN = sn;
            //        UpdateArticle(a, new string[] { "SN" });
            //        sn++;
            //        count++;
            //    }
            //}
            return count;
        }

        #endregion

        #region

        bool CheckModel(string modelname)
        {
            if (!String.IsNullOrEmpty(modelname))
            {
                modelname = modelname.ToLower();
                List<string> list = new List<string>() { "article", "system.article" };
                return !list.Contains(modelname.ToLower());
            }
            return false;
        }

        //void ExtendCriteria(Criteria c, string oid)
        //{
        //    string parameters, modelname;
        //    modelname = ChannelHelper.GetModelName(oid, out parameters);
        //    if (CheckModel(modelname))
        //    {
        //        c.Add(CriteriaType.Equals, "ModelName", modelname);
        //        if (!String.IsNullOrEmpty(parameters))
        //        {
        //            CriteriaExpressionHelper.Execute(c, parameters);
        //        }
        //    }
        //}
        #endregion

        #region 根据查询实体得到Artilce
        /// <summary>
        /// 参数查询得到数据总条数
        /// </summary>
        /// <param name="queryEntity">查询实体</param>
        /// <returns>数据总条数</returns>
        //public int QueryArtilceModelCountByParameter(QueryEntity queryEntity)
        //{
        //    if (queryEntity != null)
        //    {
        //        Criteria c = new Criteria(CriteriaType.Equals, "ModelName", queryEntity.ModelName);
        //        List<QueryParam> queryPanamList = queryEntity.QueryParams;
        //        for (int i = 0; i < queryPanamList.Count; i++)
        //        {
        //            QueryParam qp = queryPanamList[i];
        //            if (qp.CriteriaType == CriteriaType.Like)
        //            {
        //                qp.ColumnValue = string.Format("%{0}%", qp.ColumnValue);
        //            }
        //            c.Add(qp.CriteriaType, qp.ColumnKey, qp.ColumnValue);
        //        }
        //        return Assistant.Count<Article>(c);
        //    }
        //    return 0;
        //}

        /// <summary>
        /// 查询文章集合
        /// </summary>
        /// <param name="queryEntity">查询参数实体</param>
        /// <param name="orders">排序规则实体数组</param>
        /// <param name="from">开始条数</param>
        /// <param name="count">每页条数</param>
        /// <param name="fields">节点数组</param>
        /// <returns>文章实体泛型集合</returns>
        //public List<Article> QueryArticles(QueryEntity queryEntity, int from, int count, string[] fields)
        //{

        //    if (queryEntity != null)
        //    {
        //        Criteria c = GetCriteriaByQueryEntity(queryEntity);
        //        return Assistant.List<Article>(c, queryEntity.Orders, from, count, fields);


        //    }
        //    else
        //    {
        //        return new List<Article>();
        //    }
        //}

        #endregion

        #region IObjectClickHelper实现
        /// <summary>
        /// 更新指定对象的点击量报表
        /// </summary>
        /// <param name="modelName">模块名称</param>
        /// <param name="id">文章ID</param>
        /// <param name="dictClickReport">点击量报表</param>
        public void UpdateClicks(string modelName, string id, Dictionary<string, int> dictClickReport)
        {
            //ArticleHelper helper = HelperFactory.GetHelper<ArticleHelper>();
            //Article targetObject = helper.GetArticle(id);
            //if (targetObject != null)
            //{
            //    targetObject.DayClicks = dictClickReport["日点击量"];
            //    targetObject.YesterdayClicks = dictClickReport["昨日点击量"];
            //    targetObject.WeekClicks = dictClickReport["周点击量"];
            //    targetObject.MonthClicks = dictClickReport["月点击量"];
            //    targetObject.QuarterClicks = dictClickReport["季点击量"];
            //    targetObject.YearClicks = dictClickReport["年点击量"];
            //    targetObject.Clicks = dictClickReport["总点击量"];

            //    Assistant.Update(targetObject, new string[] { "DayClicks", "YesterdayClicks", "WeekClicks", "MonthClicks", "QuarterClicks", "YearClicks", "Clicks" });
            //}
        }
        #endregion
    }
    /// <summary>
    /// 创建序列号SN处理类    /// </summary>
    [Serializable]
    public class CreateSNHelper
    {
        static object lockHelper = new object();//互斥锁

        private static long snBase = 0;
        private static long AppSN = 0;

        //public long SnBase
        //{
        //    get
        //    {
        //        lock (lockHelper)
        //        {
        //            Criteria c = new Criteria(CriteriaType.Equals, "SN", ++AppSN);
        //            long totalAll = Assistant.Count<Article>(c);
        //            if (totalAll > 0)
        //            {
        //                List<Article> articles = Assistant.List<Article>(null, new Order[] { new Order("SN", OrderMode.Desc) }, 0, 1);
        //                AppSN = articles[0].SN + 1;
        //            }
        //            return AppSN;
        //        }
        //    }
        //}

        //private ObjectAssistant assistant;
        ///// <summary>
        ///// 当前Helper的数据访问对象
        ///// </summary>
        //public ObjectAssistant Assistant
        //{
        //    get { return assistant; }
        //    set { assistant = value; }
        //}

        private static readonly string ArticleKeyID = "ArticleID{0}";

        /// <summary>
        /// 更新一篇文章记录
        /// </summary>
        /// <param name="a">一篇文章记录</param>
        /// <param name="fields">需要更新的字段</param>
        //void UpdateArticle(Article a, string[] fields)
        //{
        //    try
        //    {
        //        //清除缓存
        //        HttpContext Context = HttpContext.Current;
        //        string key = string.Format(ArticleKeyID, a.ID);
        //        Context.Cache.Remove(key);
        //        Context.Items.Remove(key);
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    Assistant.Update(a, fields);
        //}

    }

    //public class CriteriaExpressionHelper
    //{
    //    static List<CriteriaExpression> expList = new List<CriteriaExpression>();
    //    static CriteriaExpressionHelper()
    //    {
    //        expList.Add(new LikeExpression());
    //    }

    //    public static void Execute(Criteria c, string expr)
    //    {
    //        foreach (CriteriaExpression exp in expList)
    //        {
    //            exp.Execute(c, expr);
    //        }
    //    }
    //}

    //public interface CriteriaExpression
    //{
    //    void Execute(Criteria c, string expr);
    //}

    //public class LikeExpression : CriteriaExpression
    //{
    //    Regex regex = new Regex(@"like\((?<field>\S*),(?<value>\S*)\)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    //    public void Execute(Criteria c, string expr)
    //    {
    //        StringReader reader = new StringReader(expr);
    //        string s = null;
    //        while (!String.IsNullOrEmpty(s = reader.ReadLine()))
    //        {
    //            s = s.Trim();
    //            Match m = regex.Match(s);
    //            if (m != null && m.Success)
    //            {
    //                c.Add(CriteriaType.Like, m.Groups["field"].Value, m.Groups["value"].Value);
    //            }
    //        }
    //    }
    //}
}
