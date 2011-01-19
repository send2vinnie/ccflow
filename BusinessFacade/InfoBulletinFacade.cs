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
	/// 该类包含InfoBulletin系统的业务表现。
	/// <remarks>
	///  业务表现用于提供业务规则子系统的简单接口。
	///  该类被标记为MarshalByRefObject以支持远程处理。
	/// </remarks>
	/// </summary>
	public class InfoBulletinFacade : MarshalByRefObject
	{
		public InfoBulletinFacade()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  添加公告信息
		/// </summary>
		/// <param name="bulletinTitle"></param>
		/// <param name="bulletinDesc"></param>
		/// <param name="adminID"></param>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <param name="isAudit"></param>
		/// <param name="reason"></param>
		/// <returns>成功则返回true，失败则返回false</returns>
		public bool InsertInfoBulletin(
						string bulletinTitle,
						string bulletinDesc,
						int adminID,
						DateTime startTime,
						DateTime endTime,
						int isAudit,
						out int  reason)
		{
			bool retVal = false;

			InfoBulletinData infoBulletinData = new InfoBulletinData();
			DataTable table = infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table];
			DataRow row = table.NewRow();

			row[InfoBulletinData.BulletinTitle_Field]		= bulletinTitle;
			row[InfoBulletinData.BulletinDesc_Field]		= bulletinDesc;
			row[InfoBulletinData.AdminID_Field]		        = adminID;
			row[InfoBulletinData.StartTime_Field]		    = startTime;
			row[InfoBulletinData.EndTime_Field]		        = endTime;
			row[InfoBulletinData.IsAudit_Field]		        = isAudit;

			table.Rows.Add(row);
			retVal = (new InfoBulletinRules()).InsertInfoBulletin(infoBulletinData);

             reason= Int32.Parse(infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows[0][InfoBulletinData.Reason_Field].ToString());
            //reason = 0;
			return retVal;
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="infoBulletinData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool UpdateInfoBulletin(InfoBulletinData infoBulletinData)
		{
			// 前置检查；
			ApplicationAssert.CheckCondition(infoBulletinData != null,"InfoBulletinData is required", ApplicationAssert.LineNumber);

			return (new InfoBulletinRules()).UpdateInfoBulletin(infoBulletinData);
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="optionType"></param>
		/// <param name="bulletinID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetInfoBulletin(string optionType,int bulletinID)
		{
			bool retVal = false;

			InfoBulletinData infoBulletinData = new InfoBulletinData();
			DataRow infoBulletinDataRow = infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].NewRow();
			infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows.Add(infoBulletinDataRow);
			infoBulletinData.AcceptChanges();

			infoBulletinDataRow[InfoBulletinData.OptionType_Field]		= optionType;
			infoBulletinDataRow[InfoBulletinData.BulletinID_Field]		= bulletinID;

			retVal = (new InfoBulletinRules()).SetInfoBulletin(infoBulletinData);

			return retVal;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="bulletinID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelInfoBulletin(int bulletinID)
		{
			bool retVal = false;

			InfoBulletinData infoBulletinData = new InfoBulletinData();
			DataRow infoBulletinDataRow = infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].NewRow();
			infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].Rows.Add(infoBulletinDataRow);
			infoBulletinData.AcceptChanges();

			infoBulletinDataRow[InfoBulletinData.BulletinID_Field]		= bulletinID;

			retVal = (new InfoBulletinRules()).DelInfoBulletin(infoBulletinData);

			return retVal;
		}

		/// <summary>
		/// 查询公告信息
		/// </summary>
		/// <param name="where"></param>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="doCount"></param>
		/// <returns>数据实体InfoBulletinData</returns>
		public InfoBulletinData GetInfoBulletin(string where,int pageSize,int pageIndex,bool doCount)
		{
			using (InfoBulletinDataAccess infoBulletinDataAccess = new InfoBulletinDataAccess())
			{
				return infoBulletinDataAccess.GetInfoBulletin(where,pageSize,pageIndex,doCount);
			}
		}

	}
}
