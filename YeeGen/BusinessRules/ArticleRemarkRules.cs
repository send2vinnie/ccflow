using System;
using System.Data;
using System.Text.RegularExpressions;

using Tax666.SystemFramework;
using Tax666.DataEntity;
using Tax666.DataAccess;

namespace Tax666.BusinessRules
{
	/// <summary>
	/// 该类包含ArticleRemark系统的业务逻辑层。
	/// <remarks>
	///  完成插入、删除、更新等操作的业务逻辑校验和逻辑处理。
	/// </remarks>
	/// </summary>
	public class ArticleRemarkRules
	{
		public ArticleRemarkRules()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  根据某种规则验证数据实体ArticleRemarkData中的指定行。
		/// </summary>/// <param name="articleRemarkDataRow">要验证的数据实体ArticleRemarkData的行</param>
		/// <returns>如果某field有错误，返回false</returns>
		private bool Validate(DataRow articleRemarkDataRow)
		{
			bool isValid = false;
			articleRemarkDataRow.ClearErrors();
			isValid = IsValidField(articleRemarkDataRow, ArticleRemarkData.RemarkName_Field,50);

			if ( !isValid )
			{
				articleRemarkDataRow.RowError = ArticleRemarkData.INVALID_FIELDS;
			}
			return isValid;
		}

		/// <summary>
		/// 根据某种规则验证ArticleRemarkData中的某field。
		/// </summary>
		/// <param name="articleRemarkDataRow">要验证的ArticleRemarkData中一行</param>
		/// <param name="fieldName">要验证的field</param>
		/// <param name="maxLen">该field的最大长度</param>
		/// <returns>如果该field不符合验证条件，返回false</returns>
		private bool IsValidField(DataRow articleRemarkDataRow, String fieldName, short maxLen)
		{
			short i = (short)(articleRemarkDataRow[fieldName].ToString().Trim().Length);
			if( (i<1) || (i>maxLen))
			{
				// 将该field标记为非法
				articleRemarkDataRow.SetColumnError(fieldName, ArticleRemarkData.INVALID_FIELD);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 添加评论
		/// </summary>
		/// <param name="articleRemarkData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertArticleRemark(ArticleRemarkData articleRemarkData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleRemarkData != null,"ArticleRemarkData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleRemarkData.Tables[ArticleRemarkData.ArticleRemark_Table].Rows.Count == 1,"ArticleRemarkData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = articleRemarkData.Tables[ArticleRemarkData.ArticleRemark_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( ArticleRemarkDataAccess articleRemarkDataAccess = new ArticleRemarkDataAccess() )
				{
					result = articleRemarkDataAccess.InsertArticleRemark(articleRemarkData);
				}
			}
			return result;
		}

		/// <summary>
		/// 回复评论（只能单条）
		/// </summary>
		/// <param name="articleRemarkData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetArticleRemarkReply(ArticleRemarkData articleRemarkData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleRemarkData != null,"ArticleRemarkData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleRemarkData.Tables[ArticleRemarkData.ArticleRemark_Table].Rows.Count == 1,"ArticleRemarkData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = articleRemarkData.Tables[ArticleRemarkData.ArticleRemark_Table].Rows[0];

			bool result;

			using( ArticleRemarkDataAccess articleRemarkDataAccess = new ArticleRemarkDataAccess() )
			{
				result = articleRemarkDataAccess.SetArticleRemarkReply(articleRemarkData);
			}
			return result;
		}

		/// <summary>
		/// 删除评论
		/// </summary>
		/// <param name="articleRemarkData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticleRemark(ArticleRemarkData articleRemarkData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleRemarkData != null,"ArticleRemarkData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleRemarkData.Tables[ArticleRemarkData.ArticleRemark_Table].Rows.Count == 1,"ArticleRemarkData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = articleRemarkData.Tables[ArticleRemarkData.ArticleRemark_Table].Rows[0];

			bool result;

			using( ArticleRemarkDataAccess articleRemarkDataAccess = new ArticleRemarkDataAccess() )
			{
				result = articleRemarkDataAccess.DelArticleRemark(articleRemarkData);
			}
			return result;
		}

	}
}
