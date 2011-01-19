using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Text;

using Tax666.SystemFramework;
using Tax666.DataEntity;
using Tax666.DataAccess;
using Tax666.BusinessRules;

namespace Tax666.BusinessFacade
{
	/// <summary>
	/// 该类包含Article系统的业务表现。
	/// <remarks>
	///  业务表现用于提供业务规则子系统的简单接口。
	///  该类被标记为MarshalByRefObject以支持远程处理。
	/// </remarks>
	/// </summary>
	public class ArticleFacade : MarshalByRefObject
	{
		public ArticleFacade()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  添加新文章记录
		/// </summary>
		/// <param name="articleTabID"></param>

		/// <param name="title"></param>
		/// <param name="articleHtmlDesc"></param>
		/// <param name="author"></param>
		/// <param name="sourse"></param>
		/// <param name="authorEmail"></param>
		/// <param name="authorHomePage"></param>
		/// <param name="isAudit"></param>
		/// <param name="isCommend"></param>
		/// <param name="isTop"></param>
		/// <param name="isAvailable"></param>
		/// <param name="articleID"></param>
		/// <param name="reason"></param>
		/// <returns>成功则返回true，失败则返回false</returns>
		public bool InsertArticle(
						int bigTypeID,
                        int smallTypeID,
						string title,
						string articleHtmlDesc,
						string author,
						string sourse,
						string authorEmail,
						string authorHomePage,
						int isAudit,
                        int isCommend,
                        int isTop,
						int isSubject,
                        int isAvailable,
						out int articleID,
						out int reason)
		{
			bool retVal = false;

			ArticleData articleData = new ArticleData();
			DataTable table = articleData.Tables[ArticleData.Article_Table];
			DataRow row = table.NewRow();

			row[ArticleData.BigTypeID_Field]		= bigTypeID;
            row[ArticleData.SmallTypeID_Field]      = smallTypeID;
			row[ArticleData.Title_Field]		    = title;
			row[ArticleData.ArticleHtmlDesc_Field]	= articleHtmlDesc;
			row[ArticleData.Author_Field]		    = author;
			row[ArticleData.Sourse_Field]		    = sourse;
			row[ArticleData.AuthorEmail_Field]		= authorEmail;
			row[ArticleData.AuthorHomePage_Field]	= authorHomePage;
			row[ArticleData.IsAudit_Field]		    = isAudit;
			row[ArticleData.IsCommend_Field]		= isCommend;
			row[ArticleData.IsTop_Field]		    = isTop;
			row[ArticleData.IsSubject_Field]		= isSubject;
			row[ArticleData.IsAvailable_Field]		= isAvailable;

			table.Rows.Add(row);
			retVal = (new ArticleRules()).InsertArticle(articleData);

            articleID = Int32.Parse(articleData.Tables[ArticleData.Article_Table].Rows[0][ArticleData.ArticleID_Field].ToString());
            reason = Int32.Parse(articleData.Tables[ArticleData.Article_Table].Rows[0][ArticleData.Reason_Field].ToString());

			return retVal;
		}

		/// <summary>
		/// 修改指定ID的文章属性
		/// </summary>
		/// <param name="articleData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool UpdateArticle(ArticleData articleData)
		{
			// 前置检查；
			ApplicationAssert.CheckCondition(articleData != null,"ArticleData is required", ApplicationAssert.LineNumber);

			return (new ArticleRules()).UpdateArticle(articleData);
		}

		/// <summary>
		/// OptionType : UPDATEPIC-更新图片地址；ADDNUM-更新阅读次数；TOP-固顶；COMMEND-推荐；VALID-有效；AUDIT-审核；TRAMPLE-顶一下；PEAK-踩一下；
		/// </summary>
		/// <param name="optionType"></param>
		/// <param name="articleID"></param>
		/// <param name="articlePicPath"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetArticleInfo(string optionType,int articleID,string articlePicPath)
		{
			bool retVal = false;

			ArticleData articleData = new ArticleData();
			DataRow articleDataRow = articleData.Tables[ArticleData.Article_Table].NewRow();
			articleData.Tables[ArticleData.Article_Table].Rows.Add(articleDataRow);
			articleData.AcceptChanges();

			articleDataRow[ArticleData.OptionType_Field]		= optionType;
			articleDataRow[ArticleData.ArticleID_Field]		    = articleID;
			articleDataRow[ArticleData.ArticlePicPath_Field]	= articlePicPath;

			retVal = (new ArticleRules()).SetArticleInfo(articleData);

			return retVal;
		}

		/// <summary>
		/// 将某文章移到指定的栏目下
		/// </summary>
		/// <param name="articleID"></param>
		/// <param name="articleTabID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool MoveArticleTabType(int articleID,int bigTypeID,int smallTypeID)
		{
			bool retVal = false;

			ArticleData articleData = new ArticleData();
			DataRow articleDataRow = articleData.Tables[ArticleData.Article_Table].NewRow();
			articleData.Tables[ArticleData.Article_Table].Rows.Add(articleDataRow);
			articleData.AcceptChanges();

			articleDataRow[ArticleData.ArticleID_Field]		    = articleID;
            articleDataRow[ArticleData.BigTypeID_Field]         = bigTypeID;
            articleDataRow[ArticleData.SmallTypeID_Field]       = smallTypeID;

			retVal = (new ArticleRules()).MoveArticleTabType(articleData);

			return retVal;
		}

		/// <summary>
		/// 删除指定ID的文章
		/// </summary>
		/// <param name="articleID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticle(int articleID)
		{
			bool retVal = false;

			ArticleData articleData = new ArticleData();
			DataRow articleDataRow = articleData.Tables[ArticleData.Article_Table].NewRow();
			articleData.Tables[ArticleData.Article_Table].Rows.Add(articleDataRow);
			articleData.AcceptChanges();

			articleDataRow[ArticleData.ArticleID_Field]		= articleID;

			retVal = (new ArticleRules()).DelArticle(articleData);

			return retVal;
		}

		/// <summary>
		/// ListType:1-根据ID获取记录;2-栏目热门TOP10;3-栏目最新TOP10;4-获取栏目推荐TOP10;5-获取获取图片新闻TOP10;
		/// </summary>
		/// <param name="listType"></param>
		/// <param name="articleTabID"></param>
		/// <param name="articleID"></param>
		/// <returns>数据实体ArticleData</returns>
        public ArticleData GetArticleByListType(int listType, int bigTypeID, int smallTypeID, int articleID)
		{
			using (ArticleDataAccess articleDataAccess = new ArticleDataAccess())
			{
                return articleDataAccess.GetArticleByListType(listType, bigTypeID,smallTypeID, articleID);
			}
		}

		/// <summary>
		/// 获取ID文章的相关文章列表
		/// </summary>
		/// <param name="articleID"></param>
		/// <returns>数据实体ArticleData</returns>
		public ArticleData GetArticleRelation(int articleID)
		{
			using (ArticleDataAccess articleDataAccess = new ArticleDataAccess())
			{
				return articleDataAccess.GetArticleRelation(articleID);
			}
		}

        /// <summary>
        /// 获取所有文章列表（分页）
        /// </summary>
        /// <param name="articleTabID"></param>
        /// <param name="isAvailable"></param>
        /// <param name="isAudit"></param>
        /// <param name="dateRange"></param>
        /// <param name="textKey"></param>
        /// <param name="agentID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="doCount"></param>
        /// <returns>数据实体ArticleData</returns>
        public ArticleData GetArticlesAdminAll(int bigTypeID, int smallTypeID, int isAvailable, int isAudit, string textKey, int pageSize, int pageIndex, bool doCount)
        {
            using (ArticleDataAccess articleDataAccess = new ArticleDataAccess())
            {
                return articleDataAccess.GetArticlesAdminAll(bigTypeID, smallTypeID, isAvailable, isAudit, textKey, pageSize, pageIndex, doCount);
            }
        }
		/// <summary>
		/// 统计某个栏目的文章（目录树用到）
		/// </summary>
		/// <param name="articleTabID"></param>
		/// <returns>数据实体ArticleData</returns>
		public ArticleData GetArticlesSort(int articleTabID)
		{
			using (ArticleDataAccess articleDataAccess = new ArticleDataAccess())
			{
				return articleDataAccess.GetArticlesSort(articleTabID);
			}
		}

		/// <summary>
		/// 文章前台分页显示（含搜索功能）
		/// </summary>
		/// <param name="articleTabID"></param>
		/// <param name="dateRange"></param>
		/// <param name="textKey"></param>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="doCount"></param>
		/// <returns>数据实体ArticleData</returns>
        public ArticleData GetArticleWebList(int bigTypeID, int smallTypeID, int dateRange, string textKey, int pageSize, int pageIndex, bool doCount)
		{
			using (ArticleDataAccess articleDataAccess = new ArticleDataAccess())
			{
                return articleDataAccess.GetArticleWebList(bigTypeID, smallTypeID, dateRange, textKey, pageSize, pageIndex, doCount);
			}
		}

        /// <summary>
		/// 获取文章列表（自动填充）
		/// </summary>
		/// <param name="word1"></param>
		/// <param name="word2"></param>
		/// <param name="word3"></param>
		/// <param name="word4"></param>
		/// <param name="word5"></param>
		/// <param name="doCount"></param>
		/// <returns>数据实体ArticleData</returns>
        public ArticleData GetArticleAuto(string word1, string word2, string word3, string word4, string word5, bool doCount)
        {
            using (ArticleDataAccess articleDataAccess = new ArticleDataAccess())
            {
                return articleDataAccess.GetArticleAuto(word1, word2, word3, word4, word5, doCount);
            }
        }

        /// <summary>
        /// 获取文章资讯搜索记录列表；
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="doCount"></param>
        /// <returns>数据实体ArticleData</returns>
        public ArticleData GetArticleSearch(string where, int pageSize, int pageIndex, bool doCount)
        {
            using (ArticleDataAccess articleDataAccess = new ArticleDataAccess())
            {
                return articleDataAccess.GetArticleSearch(where, pageSize, pageIndex, doCount);
            }
        }

	}
}
