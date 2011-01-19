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
	/// 该类包含ArticleKeywords系统的业务表现。
	/// <remarks>
	///  业务表现用于提供业务规则子系统的简单接口。
	///  该类被标记为MarshalByRefObject以支持远程处理。
	/// </remarks>
	/// </summary>
	public class ArticleKeywordsFacade : MarshalByRefObject
	{
		public ArticleKeywordsFacade()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  添加文章关键字
		/// </summary>
		/// <param name="articleID"></param>
		/// <param name="keyword"></param>
		/// <returns>成功则返回true，失败则返回false</returns>
		public bool InsertArticleKeywords(
						int articleID,
						string keyword)
		{
			bool retVal = false;

			ArticleKeywordsData articleKeywordsData = new ArticleKeywordsData();
			DataTable table = articleKeywordsData.Tables[ArticleKeywordsData.ArticleKeywords_Table];
			DataRow row = table.NewRow();

			row[ArticleKeywordsData.ArticleID_Field]	= articleID;
			row[ArticleKeywordsData.Keyword_Field]		= keyword;

			table.Rows.Add(row);
			retVal = (new ArticleKeywordsRules()).InsertArticleKeywords(articleKeywordsData);

			return retVal;
		}

		/// <summary>
		/// 删除指定ID的文章关键字
		/// </summary>
		/// <param name="articleID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticleKeywords(int articleID)
		{
			bool retVal = false;

			ArticleKeywordsData articleKeywordsData = new ArticleKeywordsData();
			DataRow articleKeywordsDataRow = articleKeywordsData.Tables[ArticleKeywordsData.ArticleKeywords_Table].NewRow();
			articleKeywordsData.Tables[ArticleKeywordsData.ArticleKeywords_Table].Rows.Add(articleKeywordsDataRow);
			articleKeywordsData.AcceptChanges();

			articleKeywordsDataRow[ArticleKeywordsData.ArticleID_Field]		= articleID;

			retVal = (new ArticleKeywordsRules()).DelArticleKeywords(articleKeywordsData);

			return retVal;
		}

        /// <summary>
        /// 根据文章ID获取所有关键字；
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public ArticleKeywordsData GetArticleKeywordsByID(int articleID)
        {
            using (ArticleKeywordsDataAccess articleKeywordsDataAccess = new ArticleKeywordsDataAccess())
            {
                return articleKeywordsDataAccess.GetArticleKeywordsByID(articleID);
            }
        }

	}
}
