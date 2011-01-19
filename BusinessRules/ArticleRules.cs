using System;
using System.Data;
using System.Text.RegularExpressions;

using Tax666.SystemFramework;
using Tax666.DataEntity;
using Tax666.DataAccess;

namespace Tax666.BusinessRules
{
	/// <summary>
	/// 该类包含Article系统的业务逻辑层。
	/// <remarks>
	///  完成插入、删除、更新等操作的业务逻辑校验和逻辑处理。
	/// </remarks>
	/// </summary>
	public class ArticleRules
	{
		public ArticleRules()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  根据某种规则验证数据实体ArticleData中的指定行。
		/// </summary>/// <param name="articleDataRow">要验证的数据实体ArticleData的行</param>
		/// <returns>如果某field有错误，返回false</returns>
		private bool Validate(DataRow articleDataRow)
		{
			bool isValid = false;
			articleDataRow.ClearErrors();
			isValid = IsValidField(articleDataRow, ArticleData.Title_Field,160);

			if ( !isValid )
			{
				articleDataRow.RowError = ArticleData.INVALID_FIELDS;
			}
			return isValid;
		}

		/// <summary>
		/// 根据某种规则验证ArticleData中的某field。
		/// </summary>
		/// <param name="articleDataRow">要验证的ArticleData中一行</param>
		/// <param name="fieldName">要验证的field</param>
		/// <param name="maxLen">该field的最大长度</param>
		/// <returns>如果该field不符合验证条件，返回false</returns>
		private bool IsValidField(DataRow articleDataRow, String fieldName, short maxLen)
		{
			short i = (short)(articleDataRow[fieldName].ToString().Trim().Length);
			if( (i<1) || (i>maxLen))
			{
				// 将该field标记为非法
				articleDataRow.SetColumnError(fieldName, ArticleData.INVALID_FIELD);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 添加新文章记录
		/// </summary>
		/// <param name="articleData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertArticle(ArticleData articleData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleData != null,"ArticleData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleData.Tables[ArticleData.Article_Table].Rows.Count == 1,"ArticleData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = articleData.Tables[ArticleData.Article_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( ArticleDataAccess articleDataAccess = new ArticleDataAccess() )
				{
					result = articleDataAccess.InsertArticle(articleData);
				}
			}
			return result;
		}

		/// <summary>
		/// 修改指定ID的文章属性
		/// </summary>
		/// <param name="articleData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool UpdateArticle(ArticleData articleData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleData != null,"ArticleData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleData.Tables[ArticleData.Article_Table].Rows.Count == 1,"ArticleData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = articleData.Tables[ArticleData.Article_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( ArticleDataAccess articleDataAccess = new ArticleDataAccess() )
				{
					result = articleDataAccess.UpdateArticle(articleData);
				}
			}
			return result;
		}

		/// <summary>
		/// OptionType : UPDATEPIC-更新图片地址；ADDNUM-更新阅读次数；TOP-固顶；COMMEND-推荐；VALID-有效；AUDIT-审核；TRAMPLE-顶一下；PEAK-踩一下；
		/// </summary>
		/// <param name="articleData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetArticleInfo(ArticleData articleData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleData != null,"ArticleData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleData.Tables[ArticleData.Article_Table].Rows.Count == 1,"ArticleData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = articleData.Tables[ArticleData.Article_Table].Rows[0];

			bool result;

			using( ArticleDataAccess articleDataAccess = new ArticleDataAccess() )
			{
				result = articleDataAccess.SetArticleInfo(articleData);
			}
			return result;
		}

		/// <summary>
		/// 将某文章移到指定的栏目下
		/// </summary>
		/// <param name="articleData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool MoveArticleTabType(ArticleData articleData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleData != null,"ArticleData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleData.Tables[ArticleData.Article_Table].Rows.Count == 1,"ArticleData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = articleData.Tables[ArticleData.Article_Table].Rows[0];

			bool result;

			using( ArticleDataAccess articleDataAccess = new ArticleDataAccess() )
			{
				result = articleDataAccess.MoveArticleTabType(articleData);
			}
			return result;
		}

		/// <summary>
		/// 删除指定ID的文章
		/// </summary>
		/// <param name="articleData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticle(ArticleData articleData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleData != null,"ArticleData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleData.Tables[ArticleData.Article_Table].Rows.Count == 1,"ArticleData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = articleData.Tables[ArticleData.Article_Table].Rows[0];

			bool result;

			using( ArticleDataAccess articleDataAccess = new ArticleDataAccess() )
			{
				result = articleDataAccess.DelArticle(articleData);
			}
			return result;
		}

	}
}
