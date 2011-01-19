using System;
using System.Data;
using System.Text.RegularExpressions;

using Tax666.SystemFramework;
using Tax666.DataEntity;
using Tax666.DataAccess;

namespace Tax666.BusinessRules
{
	/// <summary>
	/// 该类包含ArticleKeywords系统的业务逻辑层。
	/// <remarks>
	///  完成插入、删除、更新等操作的业务逻辑校验和逻辑处理。
	/// </remarks>
	/// </summary>
	public class ArticleKeywordsRules
	{
		public ArticleKeywordsRules()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  根据某种规则验证数据实体ArticleKeywordsData中的指定行。
		/// </summary>/// <param name="articleKeywordsDataRow">要验证的数据实体ArticleKeywordsData的行</param>
		/// <returns>如果某field有错误，返回false</returns>
		private bool Validate(DataRow articleKeywordsDataRow)
		{
			bool isValid = false;
			articleKeywordsDataRow.ClearErrors();
			isValid = IsValidField(articleKeywordsDataRow, ArticleKeywordsData.Keyword_Field,50);

			if ( !isValid )
			{
				articleKeywordsDataRow.RowError = ArticleKeywordsData.INVALID_FIELDS;
			}
			return isValid;
		}

		/// <summary>
		/// 根据某种规则验证ArticleKeywordsData中的某field。
		/// </summary>
		/// <param name="articleKeywordsDataRow">要验证的ArticleKeywordsData中一行</param>
		/// <param name="fieldName">要验证的field</param>
		/// <param name="maxLen">该field的最大长度</param>
		/// <returns>如果该field不符合验证条件，返回false</returns>
		private bool IsValidField(DataRow articleKeywordsDataRow, String fieldName, short maxLen)
		{
			short i = (short)(articleKeywordsDataRow[fieldName].ToString().Trim().Length);
			if( (i<1) || (i>maxLen))
			{
				// 将该field标记为非法
				articleKeywordsDataRow.SetColumnError(fieldName, ArticleKeywordsData.INVALID_FIELD);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 添加文章关键字
		/// </summary>
		/// <param name="articleKeywordsData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertArticleKeywords(ArticleKeywordsData articleKeywordsData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleKeywordsData != null,"ArticleKeywordsData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleKeywordsData.Tables[ArticleKeywordsData.ArticleKeywords_Table].Rows.Count == 1,"ArticleKeywordsData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = articleKeywordsData.Tables[ArticleKeywordsData.ArticleKeywords_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( ArticleKeywordsDataAccess articleKeywordsDataAccess = new ArticleKeywordsDataAccess() )
				{
					result = articleKeywordsDataAccess.InsertArticleKeywords(articleKeywordsData);
				}
			}
			return result;
		}

		/// <summary>
		/// 删除指定ID的文章关键字
		/// </summary>
		/// <param name="articleKeywordsData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticleKeywords(ArticleKeywordsData articleKeywordsData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleKeywordsData != null,"ArticleKeywordsData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleKeywordsData.Tables[ArticleKeywordsData.ArticleKeywords_Table].Rows.Count == 1,"ArticleKeywordsData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = articleKeywordsData.Tables[ArticleKeywordsData.ArticleKeywords_Table].Rows[0];

			bool result;

			using( ArticleKeywordsDataAccess articleKeywordsDataAccess = new ArticleKeywordsDataAccess() )
			{
				result = articleKeywordsDataAccess.DelArticleKeywords(articleKeywordsData);
			}
			return result;
		}

	}
}
