using System;
using System.Data;
using System.Text.RegularExpressions;

using Tax666.SystemFramework;
using Tax666.DataEntity;
using Tax666.DataAccess;

namespace Tax666.BusinessRules
{
	/// <summary>
	/// 该类包含InfoBulletin系统的业务逻辑层。
	/// <remarks>
	///  完成插入、删除、更新等操作的业务逻辑校验和逻辑处理。
	/// </remarks>
	/// </summary>
	public class InfoBulletinRules
	{
		public InfoBulletinRules()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  根据某种规则验证数据实体InfoBulletinData中的指定行。
		/// </summary>/// <param name="infoBulletinDataRow">要验证的数据实体InfoBulletinData的行</param>
		/// <returns>如果某field有错误，返回false</returns>
		private bool Validate(DataRow infoBulletinDataRow)
		{
			bool isValid = false;
			infoBulletinDataRow.ClearErrors();
			isValid = IsValidField(infoBulletinDataRow, InfoBulletinData.BulletinTitle_Field,100);

			if ( !isValid )
			{
				infoBulletinDataRow.RowError = InfoBulletinData.INVALID_FIELDS;
			}
			return isValid;
		}

		/// <summary>
		/// 根据某种规则验证InfoBulletinData中的某field。
		/// </summary>
		/// <param name="infoBulletinDataRow">要验证的InfoBulletinData中一行</param>
		/// <param name="fieldName">要验证的field</param>
		/// <param name="maxLen">该field的最大长度</param>
		/// <returns>如果该field不符合验证条件，返回false</returns>
		private bool IsValidField(DataRow infoBulletinDataRow, String fieldName, short maxLen)
		{
			short i = (short)(infoBulletinDataRow[fieldName].ToString().Trim().Length);
			if( (i<1) || (i>maxLen))
			{
				// 将该field标记为非法
				infoBulletinDataRow.SetColumnError(fieldName, InfoBulletinData.INVALID_FIELD);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 添加公告信息
		/// </summary>
		/// <param name="infoBulletinData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertInfoBulletin(InfoBulletinData infoBulletinData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(infoBulletinData != null,"InfoBulletinData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows.Count == 1,"InfoBulletinData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( InfoBulletinDataAccess infoBulletinDataAccess = new InfoBulletinDataAccess() )
				{
					result = infoBulletinDataAccess.InsertInfoBulletin(infoBulletinData);
				}
			}
			return result;
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="infoBulletinData">返回插入的信息数据，如果其中有field有错，就将它们分别的标识出来</param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool UpdateInfoBulletin(InfoBulletinData infoBulletinData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(infoBulletinData != null,"InfoBulletinData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows.Count == 1,"InfoBulletinData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			//获取一行；
			DataRow row = infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows[0];

			//核心校验；
			bool result = Validate(row);

			// 没有错误，执行插入或修改操作；
			if(result)
			{
				using( InfoBulletinDataAccess infoBulletinDataAccess = new InfoBulletinDataAccess() )
				{
					result = infoBulletinDataAccess.UpdateInfoBulletin(infoBulletinData);
				}
			}
			return result;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="infoBulletinData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetInfoBulletin(InfoBulletinData infoBulletinData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(infoBulletinData != null,"InfoBulletinData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows.Count == 1,"InfoBulletinData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows[0];

			bool result;

			using( InfoBulletinDataAccess infoBulletinDataAccess = new InfoBulletinDataAccess() )
			{
				result = infoBulletinDataAccess.SetInfoBulletin(infoBulletinData);
			}
			return result;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="infoBulletinData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelInfoBulletin(InfoBulletinData infoBulletinData)
		{
			// 有效性校验；
			ApplicationAssert.CheckCondition(infoBulletinData != null,"InfoBulletinData Parameter cannot be null",ApplicationAssert.LineNumber);
			ApplicationAssert.CheckCondition(infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows.Count == 1,"InfoBulletinData Parameter can only contain 1 row",ApplicationAssert.LineNumber);

			DataRow row = infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows[0];

			bool result;

			using( InfoBulletinDataAccess infoBulletinDataAccess = new InfoBulletinDataAccess() )
			{
				result = infoBulletinDataAccess.DelInfoBulletin(infoBulletinData);
			}
			return result;
		}

	}
}
