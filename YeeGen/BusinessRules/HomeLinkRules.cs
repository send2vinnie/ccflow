using System;
using System.Data;
using System.Text.RegularExpressions;

using Tax666.SystemFramework;
using Tax666.DataEntity;
using Tax666.DataAccess;

namespace Tax666.BusinessRules
{
	/// <summary>
	/// 该类包含HomeLink系统的业务逻辑层。
	/// <remarks>
	///  完成插入、删除、更新等操作的业务逻辑校验和逻辑处理。
	/// </remarks>
	/// </summary>
	public class HomeLinkRules
	{
		public HomeLinkRules()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  根据某种规则验证数据实体HomeLinkData中的指定行。
		/// </summary>/// <param name="homeLinkDataRow">要验证的数据实体HomeLinkData的行</param>
		/// <returns>如果某field有错误，返回false</returns>
		private bool Validate(DataRow homeLinkDataRow)
		{
			bool isValid = false;
			homeLinkDataRow.ClearErrors();
			isValid = IsValidField(homeLinkDataRow, HomeLinkData.LinkName_Field,50);

			if ( !isValid )
			{
				homeLinkDataRow.RowError = HomeLinkData.INVALID_FIELDS;
			}
			return isValid;
		}

		/// <summary>
		/// 根据某种规则验证HomeLinkData中的某field。
		/// </summary>
		/// <param name="homeLinkDataRow">要验证的HomeLinkData中一行</param>
		/// <param name="fieldName">要验证的field</param>
		/// <param name="maxLen">该field的最大长度</param>
		/// <returns>如果该field不符合验证条件，返回false</returns>
		private bool IsValidField(DataRow homeLinkDataRow, String fieldName, short maxLen)
		{
			short i = (short)(homeLinkDataRow[fieldName].ToString().Trim().Length);
			if( (i<1) || (i>maxLen))
			{
				// 将该field标记为非法
				homeLinkDataRow.SetColumnError(fieldName, HomeLinkData.INVALID_FIELD);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 添加新纪录
		/// </summary>
		/// <param name="homeLinkData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertHomeLink(HomeLinkData homeLinkData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(homeLinkData != null,"HomeLinkData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(homeLinkData.Tables[HomeLinkData.HomeLink_Table].Rows.Count == 1,"HomeLinkData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = homeLinkData.Tables[HomeLinkData.HomeLink_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( HomeLinkDataAccess homeLinkDataAccess = new HomeLinkDataAccess() )
				{
					result = homeLinkDataAccess.InsertHomeLink(homeLinkData);
				}
			}
			return result;
		}

		/// <summary>
		/// 修改指定ID的友情链接的属性
		/// </summary>
		/// <param name="homeLinkData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool UpdateHomeLink(HomeLinkData homeLinkData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(homeLinkData != null,"HomeLinkData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(homeLinkData.Tables[HomeLinkData.HomeLink_Table].Rows.Count == 1,"HomeLinkData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = homeLinkData.Tables[HomeLinkData.HomeLink_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( HomeLinkDataAccess homeLinkDataAccess = new HomeLinkDataAccess() )
				{
					result = homeLinkDataAccess.UpdateHomeLink(homeLinkData);
				}
			}
			return result;
		}

		/// <summary>
		/// OptionType：VALID-有效性；AUDIT-审核；UP-上移；DOWN-下移
		/// </summary>
		/// <param name="homeLinkData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetHomeLinkInfo(HomeLinkData homeLinkData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(homeLinkData != null,"HomeLinkData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(homeLinkData.Tables[HomeLinkData.HomeLink_Table].Rows.Count == 1,"HomeLinkData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = homeLinkData.Tables[HomeLinkData.HomeLink_Table].Rows[0];

			bool result;

			using( HomeLinkDataAccess homeLinkDataAccess = new HomeLinkDataAccess() )
			{
				result = homeLinkDataAccess.SetHomeLinkInfo(homeLinkData);
			}
			return result;
		}

		/// <summary>
		/// 删除友情链接
		/// </summary>
		/// <param name="homeLinkData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelHomeLink(HomeLinkData homeLinkData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(homeLinkData != null,"HomeLinkData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(homeLinkData.Tables[HomeLinkData.HomeLink_Table].Rows.Count == 1,"HomeLinkData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = homeLinkData.Tables[HomeLinkData.HomeLink_Table].Rows[0];

			bool result;

			using( HomeLinkDataAccess homeLinkDataAccess = new HomeLinkDataAccess() )
			{
				result = homeLinkDataAccess.DelHomeLink(homeLinkData);
			}
			return result;
		}

	}
}
