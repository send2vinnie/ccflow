using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.CCOA.Enum;
using BP.CCOA.Utility;
using System.IO;
using BP.DA;

namespace BP.CCOA
{
    public class ArticleAttr : EntityNoNameAttr
    {
        public const string Id = "Id";
        public const string Title = "Title";
        public const string Description = "Description";
        public const string OwnerId = "OwnerId";
        public const string ParentId = "ParentId";
        public const string AccountId = "AccountId";
        public const string Author = "Author";
        public const string Content = "Content";
        public const string Overdue = "Overdue";
        public const string SequenceIndex = "SequenceIndex";
        public const string Source = "Source";
        public const string Keyword = "Keyword";
        public const string AllowComments = "AllowComments";
        public const string Permission = "Permission";
        public const string IsImage = "IsImage";
        public const string IsShow = "IsShow";
        public const string SubTitle = "SubTitle";
        public const string Thumbnail = "Thumbnail";
        public const string IsDeleted = "IsDeleted";
        //public const string ContentUrl="ContentUrl";
        //public const string ContentType;
        //public const string SourceId;
        //public const string SN="SN";
        //public const string ProcessState;
        //public const string ProcessDirection;
        //public const string ProcessSiteId;
        //public const string _flowxml;
        //public const string _channelname;
        //public const string _fullchannelpath;
        //public const string _channelfullurl;
        //private int? _commentcount;
        //public const string _tags;
        //public const string _modelxml;
        //public const string _enumstate;
        //public const string _industryid;
        //public const string _fromrowid;
        //public const string _fromsiteurl;
        //public const string _keyword;
        //public const string _descriptionkey;
        //public const string _videocode;
        //public const string _listkeys;
        //public const string _listkeys1;
        //public const string _listkeys2;
        //public const string _listkeys3;
        //public const string _listkeys4;
        //public const string _listkeys5;
        //public const string _ipstrategy;
        //public const string _modelname;
        //public const string _tablename;
        //public const string _modelconfig;
        //public const string _modelschema;
        //private int? _privacylevel;
        //public const string _photos;
        //private int? _clicks;
        //private int? _dayclicks;
        //private int? _yesterdayclicks;
        //private int? _weekclicks;
        //private int? _monthclicks;
        //private int? _quarterclicks;
        //private int? _yearclicks;
        //public const string _color;
        //public const string _fontweight;
        //private string _fontstyle;

        public const string Created = "Created";
        public const string Updated = "Updated";
        public const string State = "State";
    }
    /// <summary>
    /// 文章信息类
    /// </summary>
    [Serializable]
    public class Article : Entity
    {
        public string Id
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.Id);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Id, value);
            }
        }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.Title);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Title, value);
            }
        }
        /// <summary>
        /// 备注（描述）
        /// </summary>
        public string Description
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.Description);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Description, value);
            }
        }
        /// <summary>
        /// 栏目ID
        /// </summary>
        public string OwnerId
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.OwnerId);
            }
            set
            {
                this.SetValByKey(ArticleAttr.OwnerId, value);
            }
        }
        /// <summary>
        /// 父栏目ID
        /// </summary>
        public string ParentId
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.ParentId);
            }
            set
            {
                this.SetValByKey(ArticleAttr.ParentId, value);
            }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string AccountId
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.AccountId);
            }
            set
            {
                this.SetValByKey(ArticleAttr.AccountId, value);
            }
        }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.Id);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Id, value);
            }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.Content);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Content, value);
            }
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? Overdue
        {
            get
            {
                return this.GetValDateTime(ArticleAttr.Overdue);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Overdue, value);
            }
        }
        public string SequenceIndex
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.SequenceIndex);
            }
            set
            {
                this.SetValByKey(ArticleAttr.SequenceIndex, value);
            }
        }
        public string Source
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.Source);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Source, value);
            }
        }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.Keyword);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Keyword, value);
            }
        }
        /// <summary>
        /// 1—为允许评论；0—为不允许评论
        /// </summary>
        public int AllowComments
        {
            get
            {
                return this.GetValIntByKey(ArticleAttr.AllowComments);
            }
            set
            {
                this.SetValByKey(ArticleAttr.AllowComments, value);
            }
        }
        public string Permission
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.Permission);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Permission, value);
            }
        }
        /// <summary>
        /// 是否有缩略图
        /// </summary>
        public bool IsImage
        {
            get
            {
                return this.GetValBooleanByKey(ArticleAttr.IsImage);
            }
            set
            {
                this.SetValByKey(ArticleAttr.IsImage, value);
            }
        }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsShow
        {
            get
            {
                return this.GetValBooleanByKey(ArticleAttr.IsShow);
            }
            set
            {
                this.SetValByKey(ArticleAttr.IsShow, value);
            }
        }
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.SubTitle);
            }
            set
            {
                this.SetValByKey(ArticleAttr.SubTitle, value);
            }
        }
        /// <summary>
        /// 缩略图存放地址（小缩略图）
        /// </summary>
        public string Thumbnail
        {
            get
            {
                return this.GetValStringByKey(ArticleAttr.Thumbnail);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Thumbnail, value);
            }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted
        {
            get
            {
                return this.GetValBooleanByKey(ArticleAttr.IsDeleted);
            }
            set
            {
                this.SetValByKey(ArticleAttr.IsDeleted, value);
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Created
        {
            get
            {
                return this.GetValDateTime(ArticleAttr.Created);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Created, value);
            }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? Updated
        {
            get
            {
                return this.GetValDateTime(ArticleAttr.Updated);
            }
            set
            {
                this.SetValByKey(ArticleAttr.Updated, value);
            }
        }
        /// <summary>
        /// 启用与禁用：1—启用；0—禁用（停用）；2—审核中；3—过期；4—回收站
        /// </summary>
        public int State
        {
            get
            {
                return this.GetValIntByKey(ArticleAttr.State);
            }
            set
            {
                this.SetValByKey(ArticleAttr.State, value);
            }
        }

        /// <summary>
        /// 栏目地址
        /// </summary>
        public string ChannelFullUrl { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 评论总数
        /// </summary>
        public int CommentCount { get; set; }
        /// <summary>
        /// 存放扩展信息XML数据
        /// </summary>
        public string ModelXml { get; set; }

        ///// <summary>
        ///// 状态信息,本属性已过时。请不要再使用
        ///// </summary>
        [Obsolete]
        public string EnumState { get; set; }

        /// <summary>
        /// 共享来源ID
        /// </summary>
        public string FromRowID { get; set; }

        /// <summary>
        /// 共享来源文章地址
        /// </summary>
        public string FromSiteUrl { get; set; }


        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; set; }


        /// <summary>
        /// 摘要：来自Description，Content
        /// </summary>
        public string Summary
        {
            get
            {
                string s = "";
                if (string.IsNullOrEmpty(Description))
                {
                    string content = Helper.RemoveHtml(Content);
                    if (content.Length > 50)
                        s = content.Substring(0, 50) + "...";
                    else
                        s = content;
                }
                else
                    s = Description;

                return s;
            }
        }

        /// <summary>
        /// 文章内容
        /// </summary>
        //public string Content
        //{
        //    get
        //    {
        //        //return Helper.ConvertPageBreakToHtml(content);
        //        return string.Empty;
        //    }
        //    set { this.content = value; }
        //}

        /// <summary>
        /// 连接地址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 时间节点
        /// </summary>
        public string TimeNote { get; set; }

        /// <summary>
        /// 状态转化名称字符串
        /// </summary>
        public string AuditText
        {
            get
            {
                switch ((ArticleStates)State)
                {
                    case ArticleStates.Started: return "<font color=green>已发布</font>";
                    case ArticleStates.Checking: return "<font color=#aa0>审核中</font>";
                    case ArticleStates.Overdued: return "<font color=#888>已过期</font>";
                    case ArticleStates.Recycled: return "<font color=#009>已删除</font>";
                    default:
                    case ArticleStates.Stopped: return "<font color=red>已停用</font>";
                }
            }
        }

        /// <summary>
        /// 通过ID生成的url，如e6b4ed25_263c_4dc6_81f8_f7e06c214099.shtml或1008.html
        /// </summary>
        //public string FullUrl
        //{
        //    get
        //    {
        //        return GenerateArticleUrl(SN, Created, ID);
        //    }
        //}

        /// <summary>
        /// 按照变量生成文章URL
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="create"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        //public string GenerateArticleUrl(long sn, DateTime create, string id)
        //{
        //    GeneralConfigInfo si = GeneralConfigs.GetConfig();
        //    string ext = si.UrlFormat;
        //    string snRex = si.ArticleUrlGenerator;
        //    if (snRex != null && snRex.Trim().Length > 0)
        //    {
        //        if (snRex == "0")
        //            return sn.ToString() + "." + ext;
        //        else
        //            return create.ToString(snRex) + "-" + sn.ToString() + "." + ext;
        //    }
        //    else
        //        return Helper.GUIDToFormatString(id) + "." + ext;
        //}

        /// <summary>
        /// 按照变量生成文章URL
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="create"></param>
        /// <param name="id"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        //public string GenerateArticleUrl(long sn, DateTime create, string id, string modelName)
        //{
        //    GeneralConfigInfo si = GeneralConfigs.GetConfig();
        //    string ext = si.UrlFormat;
        //    if (!String.IsNullOrEmpty(modelName))
        //    {
        //        string snRex = si.ArticleUrlGenerator;
        //        if (snRex != null && snRex.Trim().Length > 0)
        //        {
        //            if (snRex == "0")
        //                return sn.ToString() + "." + ext;
        //            else
        //                return create.ToString(snRex) + "-" + sn.ToString() + "." + ext;
        //        }
        //        else
        //            return Helper.GUIDToFormatString(id) + "." + ext;
        //    }
        //    else
        //    {
        //        return Helper.GUIDToFormatString(id) + "." + ext;
        //    }

        //}

        /// <summary>
        /// 参考TypeOfArticle枚举
        /// </summary>
        public int ContentType { get; set; }

        /// <summary>
        /// 是否引用类型
        /// </summary>
        public bool IsLinkArticle { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        public string TypeText
        {
            get
            {
                switch ((TypeOfArticle)ContentType)
                {
                    case TypeOfArticle.LinkArticle:
                        return "引用文章";
                    case TypeOfArticle.ShareArticle:
                        return "共享文章";
                    case TypeOfArticle.WapArticle:
                        return "WAP文章";
                    default:
                    case TypeOfArticle.NormalArticle:
                        return "原创文章";
                }
            }
        }

        /// <summary>
        /// 模型名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 模型配置
        /// </summary>
        public string ModelConfig { get; set; }

        /// <summary>
        /// 模型数据架构
        /// </summary>
        public string ModelSchema { get; set; }

        /// <summary>
        /// 保密级别0,公开;0|公开,1|内部,2|秘密,3|机密,4|绝密
        /// </summary>
        public int PrivacyLevel { get; set; }

        /// <summary>
        /// 不同类型图片
        /// </summary>
        public string TypeIcon
        {
            get
            {
                switch ((TypeOfArticle)ContentType)
                {
                    case TypeOfArticle.LinkArticle:
                        return "/admin/images/filetype/link.gif";
                    case TypeOfArticle.ShareArticle:
                        return "/admin/images/filetype/mpg.gif";
                    default:
                    case TypeOfArticle.NormalArticle:
                        return "";
                }
            }
        }

        public string IsShowText
        {
            get
            {
                return IsShow ? "是" : "否";
            }
        }

        /// <summary>
        /// 内容URL
        /// </summary>
        public string ContentUrl { get; set; }

        /// <summary>
        /// 点击数
        /// </summary>
        public int Clicks { get; set; }

        /// <summary>
        /// 完整的标题
        /// </summary>
        public string FullTitle { get; set; }


        /// <summary>
        /// 失生前端可用的Url;
        /// </summary>
        //public string Url
        //{
        //    get
        //    {
        //        return ChannelFullUrl + FullUrl;
        //    }
        //}

        /// <summary>
        /// 大缩略图
        /// </summary>
        //public string WapImage
        //{
        //    get
        //    {
        //        return GetImageName(Thumbnail, "_W");
        //    }
        //}

        /// <summary>
        /// 文章原始图片
        /// </summary>
        //public string OriginalImage
        //{
        //    get
        //    {
        //        return GetImageName(Thumbnail, "");
        //    }
        //}

        /// <summary>
        /// 根据标签tag属性取得对应缩略图
        /// 如，Thumbnail["wap"] 为 mysource_wap.jpg
        /// </summary>
        public string TagThumbnail { get; set; }

        /// <summary>
        /// 获取标签图片名称
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        //public string GetTagThumbnail(string tag)
        //{
        //    if (string.IsNullOrEmpty(Thumbnail))
        //        return string.Empty;
        //    else
        //    {
        //        string ext = Path.GetExtension(Thumbnail);
        //        string imgName = Path.GetFileNameWithoutExtension(Thumbnail);
        //        int nameLength = ext.Length + imgName.Length;
        //        string url = Thumbnail.Substring(0, Thumbnail.Length - nameLength);
        //        return String.Format("{3}{0}_{1}{2}", imgName, tag, ext, url);
        //    }
        //}

        /// <summary>
        /// 获取图片名称
        /// </summary>
        /// <param name="thumbnailName"></param>
        /// <param name="imgType"></param>
        /// <returns></returns>
        public string GetImageName(string thumbnailName, string imgType)
        {
            string ext = Path.GetExtension(thumbnailName);
            string imgName = Path.GetFileNameWithoutExtension(thumbnailName);

            imgName = imgName.Substring(0, imgName.Length - 2);
            int nameLength = 2 + ext.Length + imgName.Length;
            string url = thumbnailName.Substring(0, thumbnailName.Length - nameLength);

            return String.Format("{3}{0}{1}{2}", imgName, imgType, ext, url);
        }

        /// <summary>
        /// 获取文章的完整url，前台调用请使用此属性，而不是FullUrl
        /// </summary>
        /// <param name="channelUrl">所属栏目url</param>
        /// <returns></returns>
        //public string GetFullUrlWithChannel(string channelUrl)
        //{
        //    if (ContentType == (int)TypeOfArticle.LinkArticle)
        //        return ContentUrl;
        //    else
        //        return String.Format("{0}{1}", channelUrl, FullUrl);
        //}

        /// <summary>
        /// 相关文章
        /// </summary>
        public List<Article> RelatedArticles { get; set; }

        /// <summary>
        /// 所属栏目的全路径名称显示：如，/新闻/图片新闻
        /// </summary>
        public string FullChannelPath { get; set; }

        /// <summary>
        /// 引用/wap类型的原文章ID
        /// </summary>
        public string SourceID { get; set; }

        /// <summary>
        /// 文章流水号
        /// </summary>
        public long SN { get; set; }

        /// <summary>
        /// IP策略
        /// </summary>
        public string IPStrategy { get; set; }

        /// <summary>
        /// 文章附件所在路径：如 /_data/2010/02/25/64a55027_062d_4f78_8c51_aeb6500fdacb/
        /// </summary>
        public string AttachmentUrlPath
        {
            get
            {
                string year = Created.Value.Year.ToString();
                string month = Created.Value.Month.ToString();
                string day = Created.Value.Day.ToString();
                string sn = Helper.GUIDToFormatString(Id);
                return string.Format("/_data/{0}/{1}/{2}/{3}", year, month, day, sn);
            }
        }

        /// <summary>
        /// 当前文章的附件列表
        /// </summary>
        //public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// 本篇文章所属站点；（用于站群搜索结果）
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 本篇文章所属站点URL；（用于站群搜索结果）
        /// </summary>
        public string SiteUrl { get; set; }

        public string Photos { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 字体重度
        /// </summary>
        public string FontWeight { get; set; }
        /// <summary>
        /// 字体样式
        /// </summary>
        public string FontStyle { get; set; }

        private string titleStyle;
        public string TitleStyle
        {
            get
            {
                if (String.IsNullOrEmpty(titleStyle))
                {
                    StringBuilder sb = new StringBuilder();
                    if (!String.IsNullOrEmpty(Color))
                    {
                        sb.AppendFormat("color:{0};", Color);
                    }
                    if (!String.IsNullOrEmpty(FontWeight))
                    {
                        sb.AppendFormat("font-weight:{0};", FontWeight);
                    }
                    if (!String.IsNullOrEmpty(FontStyle))
                    {
                        sb.AppendFormat("font-style:{0};", FontStyle);
                    }
                    titleStyle = sb.ToString();
                }
                return titleStyle;
            }
        }

        /// <summary>
        /// 日点击量
        /// </summary>
        public int DayClicks { get; set; }

        /// <summary>
        /// 昨日点击量
        /// </summary>
        public int YesterdayClicks { get; set; }

        /// <summary>
        /// 周点击量
        /// </summary>
        public int WeekClicks { get; set; }

        /// <summary>
        /// 月点击量
        /// </summary>
        public int MonthClicks { get; set; }

        /// <summary>
        /// 季点击量
        /// </summary>
        public int QuarterClicks { get; set; }

        /// <summary>
        /// 年点击量
        /// </summary>
        public int YearClicks { get; set; }

        #region
        //[NonSerialized]
        //private DataSet dataSet;
        //[NonSerialized]
        //private DataRow row;

        ///// <summary>
        ///// 访问模型中的信息
        ///// </summary>
        ///// <param name="name">字段名</param>
        ///// <returns>模型数据</returns>
        //public object this[string name]
        //{
        //    get
        //    {
        //        return Row != null && Row.Table.Columns.Contains(name) ? Row[name] : null;
        //    }
        //}

        //private DataRow Row
        //{
        //    get
        //    {
        //        if (row == null)
        //        {
        //            row = DataSet != null && DataSet.Tables.Count > 0 && DataSet.Tables[0].Rows.Count > 0 ? DataSet.Tables[0].Rows[0] : null;
        //        }
        //        return row;
        //    }
        //}

        //private DataSet DataSet
        //{
        //    get
        //    {
        //        if (dataSet == null)
        //        {
        //            dataSet = CreateDataSet();
        //            if (dataSet != null)
        //            {
        //                using (StringReader reader = new StringReader(ModelXml))
        //                {
        //                    dataSet.ReadXml(reader);
        //                }
        //            }
        //        }
        //        return dataSet;
        //    }
        //}

        //DataSet CreateDataSet()
        //{
        //    if (!String.IsNullOrEmpty(ModelSchema))
        //    {
        //        DataSet ds = new DataSet();
        //        using (StringReader reader = new StringReader(ModelSchema))
        //        {
        //            ds.ReadXmlSchema(reader);
        //        }
        //        return ds;
        //    }
        //    return null;
        //}
        #endregion


        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("OA_Article");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "文章";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(ArticleAttr.Id, "", "ID", true, false, 20, 20, 20);
                map.AddTBString(ArticleAttr.Title, null, "标题", true, true, 2, 20, 20);
                map.AddTBString(ArticleAttr.Description, null, "描述", true, true, 2, 20, 20);
                map.AddTBString(ArticleAttr.OwnerId, null, "所属栏目", true, true, 2, 20, 20);
                map.AddTBString(ArticleAttr.ParentId, null, "父栏目", true, true, 2, 20, 20);
                map.AddTBString(ArticleAttr.AccountId, null, "用户ID", true, true, 2, 20, 20);
                map.AddTBString(ArticleAttr.Author, null, "作者", true, true, 2, 20, 20);
                map.AddTBString(ArticleAttr.Content, null, "内容", true, true, 2, 20, 20);
                map.AddTBDateTime(ArticleAttr.Overdue,"过期日期", true,false);
                map.AddTBString(ArticleAttr.SequenceIndex, null, "序号", true, true, 2, 20, 20);
                map.AddTBString(ArticleAttr.Source, null, "", true, true, 2, 20, 20);
                map.AddTBString(ArticleAttr.Keyword, null, "关键字", true, true, 2, 20, 20);
                map.AddTBInt(ArticleAttr.AllowComments, 0, "是否允许评论", true, false);
                map.AddTBString(ArticleAttr.Permission, null, "权限", true, true, 2, 20, 20);
                map.AddTBInt(ArticleAttr.IsImage, 0, "是否有缩略图", true, false);
                map.AddTBInt(ArticleAttr.IsShow, 0, "是否置顶", true, false);
                map.AddTBString(ArticleAttr.SubTitle, null, "副标题", true, true, 2, 20, 20);
                map.AddTBString(ArticleAttr.Thumbnail, null, "缩略图", true, true, 2, 20, 20);
                map.AddTBString(ArticleAttr.IsDeleted, null, "是否删除", true, true, 2, 20, 20);
                map.AddTBDateTime(ArticleAttr.Created, "创建日期", true, false);
                map.AddTBDateTime(ArticleAttr.Updated, "更新日期", true, false);
                map.AddTBInt(ArticleAttr.State, 1, "状态", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
    }
}
