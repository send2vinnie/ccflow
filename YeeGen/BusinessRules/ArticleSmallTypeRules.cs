using System;
using System.Data;
using System.Text.RegularExpressions;

using Tax666.SystemFramework;
using Tax666.DataEntity;
using Tax666.DataAccess;

namespace Tax666.BusinessRules
{
	/// <summary>
	/// 该类包含ArticleSmallType系统的业务逻辑层。
	/// <remarks>
	///  完成插入、删除、更新等操作的业务逻辑校验和逻辑处理。
	/// </remarks>
	/// </summary>
	public class ArticleSmallTypeRules
	{
		public ArticleSmallTypeRules()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  根据某种规则验证数据实体ArticleSmallTypeData中的指定行。
		/// </summary>/// <param name="articleSmallTypeDataRow">要验证的数据实体ArticleSmallTypeData的行</param>
		/// <returns>如果某field有错误，返回false</returns>
		private bool Validate(DataRow articleSmallTypeDataRow)
		{
			bool isValid = false;
			articleSmallTypeDataRow.ClearErrors();
			isValid = IsValidField(articleSmallTypeDataRow, ArticleSmallTypeData.SmallTypeName_Field,50);

			if ( !isValid )
			{
				articleSmallTypeDataRow.RowError = ArticleSmallTypeData.INVALID_FIELDS;
			}
			return isValid;
		}

		/// <summary>
		/// 根据某种规则验证ArticleSmallTypeData中的某field。
		/// </summary>
		/// <param name="articleSmallTypeDataRow">要验证的ArticleSmallTypeData中一行</param>
		/// <param name="fieldName">要验证的field</param>
		/// <param name="maxLen">该field的最大长度</param>
		/// <returns>如果该field不符合验证条件，返回false</returns>
		private bool IsValidField(DataRow articleSmallTypeDataRow, String fieldName, short maxLen)
		{
			short i = (short)(articleSmallTypeDataRow[fieldName].ToString().Trim().Length);
			if( (i<1) || (i>maxLen))
			{
				// 将该field标记为非法
				articleSmallTypeDataRow.SetColumnError(fieldName, ArticleSmallTypeData.INVALID_FIELD);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 添加文章小类记录
		/// </summary>
		/// <param name="articleSmallTypeData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertArticleSmallType(ArticleSmallTypeData articleSmallTypeData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleSmallTypeData != null,"ArticleSmallTypeData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Count == 1,"ArticleSmallTypeData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( ArticleSmallTypeDataAccess articleSmallTypeDataAccess = new ArticleSmallTypeDataAccess() )
				{
					result = articleSmallTypeDataAccess.InsertArticleSmallType(articleSmallTypeData);
				}
			}
			return result;
		}

		/// <summary>
		/// 修改小类记录属性
		/// </summary>
		/// <param name="articleSmallTypeData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool UpdateArticleSmallType(ArticleSmallTypeData articleSmallTypeData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleSmallTypeData != null,"ArticleSmallTypeData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Count == 1,"ArticleSmallTypeData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( ArticleSmallTypeDataAccess articleSmallTypeDataAccess = new ArticleSmallTypeDataAccess() )
				{
					result = articleSmallTypeDataAccess.UpdateArticleSmallType(articleSmallTypeData);
				}
			}
			return result;
		}

		/// <summary>
		/// 设置文章小类记录属性
		/// </summary>
		/// <param name="articleSmallTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetArticleSmallTypeInfo(ArticleSmallTypeData articleSmallTypeData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleSmallTypeData != null,"ArticleSmallTypeData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Count == 1,"ArticleSmallTypeData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows[0];

			bool result;

			using( ArticleSmallTypeDataAccess articleSmallTypeDataAccess = new ArticleSmallTypeDataAccess() )
			{
				result = articleSmallTypeDataAccess.SetArticleSmallTypeInfo(articleSmallTypeData);
			}
			return result;
		}

		/// <summary>
		/// 删除文章小类
		/// </summary>
		/// <param name="articleSmallTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticleSmallType(ArticleSmallTypeData articleSmallTypeData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleSmallTypeData != null,"ArticleSmallTypeData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Count == 1,"ArticleSmallTypeData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows[0];

			bool result;

			using( ArticleSmallTypeDataAccess articleSmallTypeDataAccess = new ArticleSmallTypeDataAccess() )
			{
				result = articleSmallTypeDataAccess.DelArticleSmallType(articleSmallTypeData);
			}
			return result;
		}

	}
}
