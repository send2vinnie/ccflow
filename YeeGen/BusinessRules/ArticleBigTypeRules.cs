using System;
using System.Data;
using System.Text.RegularExpressions;

using Tax666.SystemFramework;
using Tax666.DataEntity;
using Tax666.DataAccess;

namespace Tax666.BusinessRules
{
	/// <summary>
	/// 该类包含ArticleBigType系统的业务逻辑层。
	/// <remarks>
	///  完成插入、删除、更新等操作的业务逻辑校验和逻辑处理。
	/// </remarks>
	/// </summary>
	public class ArticleBigTypeRules
	{
		public ArticleBigTypeRules()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  根据某种规则验证数据实体ArticleBigTypeData中的指定行。
		/// </summary>/// <param name="articleBigTypeDataRow">要验证的数据实体ArticleBigTypeData的行</param>
		/// <returns>如果某field有错误，返回false</returns>
		private bool Validate(DataRow articleBigTypeDataRow)
		{
			bool isValid = false;
			articleBigTypeDataRow.ClearErrors();
			isValid = IsValidField(articleBigTypeDataRow, ArticleBigTypeData.BigTypeName_Field,50);

			if ( !isValid )
			{
				articleBigTypeDataRow.RowError = ArticleBigTypeData.INVALID_FIELDS;
			}
			return isValid;
		}

		/// <summary>
		/// 根据某种规则验证ArticleBigTypeData中的某field。
		/// </summary>
		/// <param name="articleBigTypeDataRow">要验证的ArticleBigTypeData中一行</param>
		/// <param name="fieldName">要验证的field</param>
		/// <param name="maxLen">该field的最大长度</param>
		/// <returns>如果该field不符合验证条件，返回false</returns>
		private bool IsValidField(DataRow articleBigTypeDataRow, String fieldName, short maxLen)
		{
			short i = (short)(articleBigTypeDataRow[fieldName].ToString().Trim().Length);
			if( (i<1) || (i>maxLen))
			{
				// 将该field标记为非法
				articleBigTypeDataRow.SetColumnError(fieldName, ArticleBigTypeData.INVALID_FIELD);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 添加文章大类
		/// </summary>
		/// <param name="articleBigTypeData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertArticleBigType(ArticleBigTypeData articleBigTypeData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleBigTypeData != null,"ArticleBigTypeData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows.Count == 1,"ArticleBigTypeData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( ArticleBigTypeDataAccess articleBigTypeDataAccess = new ArticleBigTypeDataAccess() )
				{
					result = articleBigTypeDataAccess.InsertArticleBigType(articleBigTypeData);
				}
			}
			return result;
		}

		/// <summary>
		/// 更新大类
		/// </summary>
		/// <param name="articleBigTypeData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool UpdateArticleBigType(ArticleBigTypeData articleBigTypeData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleBigTypeData != null,"ArticleBigTypeData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows.Count == 1,"ArticleBigTypeData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( ArticleBigTypeDataAccess articleBigTypeDataAccess = new ArticleBigTypeDataAccess() )
				{
					result = articleBigTypeDataAccess.UpdateArticleBigType(articleBigTypeData);
				}
			}
			return result;
		}

		/// <summary>
		/// 设置大类属性
		/// </summary>
		/// <param name="articleBigTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetArticleBigTypeInfo(ArticleBigTypeData articleBigTypeData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleBigTypeData != null,"ArticleBigTypeData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows.Count == 1,"ArticleBigTypeData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows[0];

			bool result;

			using( ArticleBigTypeDataAccess articleBigTypeDataAccess = new ArticleBigTypeDataAccess() )
			{
				result = articleBigTypeDataAccess.SetArticleBigTypeInfo(articleBigTypeData);
			}
			return result;
		}

		/// <summary>
		/// 删除文章大类
		/// </summary>
		/// <param name="articleBigTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticleBigType(ArticleBigTypeData articleBigTypeData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(articleBigTypeData != null,"ArticleBigTypeData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows.Count == 1,"ArticleBigTypeData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows[0];

			bool result;

			using( ArticleBigTypeDataAccess articleBigTypeDataAccess = new ArticleBigTypeDataAccess() )
			{
				result = articleBigTypeDataAccess.DelArticleBigType(articleBigTypeData);
			}
			return result;
		}

	}
}
