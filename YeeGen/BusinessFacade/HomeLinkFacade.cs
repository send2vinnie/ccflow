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
	/// 该类包含HomeLink系统的业务表现。
	/// <remarks>
	///  业务表现用于提供业务规则子系统的简单接口。
	///  该类被标记为MarshalByRefObject以支持远程处理。
	/// </remarks>
	/// </summary>
	public class HomeLinkFacade : MarshalByRefObject
	{
		public HomeLinkFacade()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  添加新纪录
		/// </summary>
		/// <param name="linkName"></param>
		/// <param name="linkUrl"></param>
		/// <param name="homeDesc"></param>
		/// <param name="logoPath"></param>
		/// <param name="linkTypeID"></param>
		/// <param name="linkMode"></param>
		/// <param name="reason"></param>
		/// <returns>成功则返回true，失败则返回false</returns>
		public bool InsertHomeLink(
						string linkName,
						string linkUrl,
						string homeDesc,
						string logoPath,
						int linkTypeID,
						int linkMode,
						out int reason)
		{
			bool retVal = false;

			HomeLinkData homeLinkData = new HomeLinkData();
			DataTable table = homeLinkData.Tables[HomeLinkData.HomeLink_Table];
			DataRow row = table.NewRow();

			row[HomeLinkData.LinkName_Field]		= linkName;
			row[HomeLinkData.LinkUrl_Field]		    = linkUrl;
			row[HomeLinkData.HomeDesc_Field]		= homeDesc;
			row[HomeLinkData.LogoPath_Field]		= logoPath;
			row[HomeLinkData.LinkTypeID_Field]		= linkTypeID;
			row[HomeLinkData.LinkMode_Field]		= linkMode;

			table.Rows.Add(row);
			retVal = (new HomeLinkRules()).InsertHomeLink(homeLinkData);

			reason = Convert.ToInt32(homeLinkData.Tables[HomeLinkData.HomeLink_Table].Rows[0][HomeLinkData.Reason_Field].ToString());

			return retVal;
		}

		/// <summary>
		/// 修改指定ID的友情链接的属性
		/// </summary>
		/// <param name="homeLinkData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool UpdateHomeLink(HomeLinkData homeLinkData)
		{
			// 前置检查；
			ApplicationAssert.CheckCondition(homeLinkData != null,"HomeLinkData is required", ApplicationAssert.LineNumber);

			return (new HomeLinkRules()).UpdateHomeLink(homeLinkData);
		}

		/// <summary>
		/// OptionType：VALID-有效性；AUDIT-审核；UP-上移；DOWN-下移
		/// </summary>
		/// <param name="optionType"></param>
		/// <param name="linkID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetHomeLinkInfo(string optionType,int linkID)
		{
			bool retVal = false;

			HomeLinkData homeLinkData = new HomeLinkData();
			DataRow homeLinkDataRow = homeLinkData.Tables[HomeLinkData.HomeLink_Table].NewRow();
			homeLinkData.Tables[HomeLinkData.HomeLink_Table].Rows.Add(homeLinkDataRow);
			homeLinkData.AcceptChanges();

			homeLinkDataRow[HomeLinkData.OptionType_Field]		= optionType;
			homeLinkDataRow[HomeLinkData.LinkID_Field]		    = linkID;

			retVal = (new HomeLinkRules()).SetHomeLinkInfo(homeLinkData);

			return retVal;
		}

		/// <summary>
		/// 删除友情链接
		/// </summary>
		/// <param name="linkID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelHomeLink(int linkID)
		{
			bool retVal = false;

			HomeLinkData homeLinkData = new HomeLinkData();
			DataRow homeLinkDataRow = homeLinkData.Tables[HomeLinkData.HomeLink_Table].NewRow();
			homeLinkData.Tables[HomeLinkData.HomeLink_Table].Rows.Add(homeLinkDataRow);
			homeLinkData.AcceptChanges();

			homeLinkDataRow[HomeLinkData.LinkID_Field]		= linkID;

			retVal = (new HomeLinkRules()).DelHomeLink(homeLinkData);

			return retVal;
		}

		/// <summary>
		/// 获取所有链接记录列表
		/// </summary>
		/// <param name="linkTypeID"></param>
		/// <param name="isAudit"></param>
		/// <param name="isAvailable"></param>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="doCount"></param>
		/// <returns>数据实体HomeLinkData</returns>
		public HomeLinkData GetHomeLinkAll(int linkTypeID,int isAudit,int isAvailable,int pageSize,int pageIndex,bool doCount)
		{
			using (HomeLinkDataAccess homeLinkDataAccess = new HomeLinkDataAccess())
			{
				return homeLinkDataAccess.GetHomeLinkAll(linkTypeID,isAudit,isAvailable,pageSize,pageIndex,doCount);
			}
		}

		/// <summary>
		/// ListType：1-ID；2-文字；3-LOGO；4-文字和Logo
		/// </summary>
		/// <param name="listType"></param>
		/// <param name="agentID"></param>
		/// <param name="linkID"></param>
		/// <returns>数据实体HomeLinkData</returns>
        public HomeLinkData GetHomeLinkByListType(int listType, int linkTypeID, int linkID)
		{
			using (HomeLinkDataAccess homeLinkDataAccess = new HomeLinkDataAccess())
			{
                return homeLinkDataAccess.GetHomeLinkByListType(listType, linkTypeID, linkID);
			}
		}

	}
}
