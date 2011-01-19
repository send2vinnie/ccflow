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
	/// 该类包含HomeLinkType系统的业务表现。
	/// <remarks>
	///  业务表现用于提供业务规则子系统的简单接口。
	///  该类被标记为MarshalByRefObject以支持远程处理。
	/// </remarks>
	/// </summary>
	public class HomeLinkTypeFacade : MarshalByRefObject
	{
		public HomeLinkTypeFacade()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  添加友情链接的类型
		/// </summary>
		/// <param name="typeName"></param>
		/// <param name="typeDesc"></param>
		/// <param name="isAvailable"></param>
		/// <param name="reason"></param>
		/// <returns>成功则返回true，失败则返回false</returns>
		public bool InsertHomeLinkType(
						string typeName,
						string typeDesc,
						int isAvailable,
						out int reason)
		{
			bool retVal = false;

			HomeLinkTypeData homeLinkTypeData = new HomeLinkTypeData();
			DataTable table = homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table];
			DataRow row = table.NewRow();

			row[HomeLinkTypeData.TypeName_Field]		= typeName;
			row[HomeLinkTypeData.TypeDesc_Field]		= typeDesc;
			row[HomeLinkTypeData.IsAvailable_Field]		= isAvailable;

			table.Rows.Add(row);
			retVal = (new HomeLinkTypeRules()).InsertHomeLinkType(homeLinkTypeData);

			reason = Convert.ToInt32(homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].Rows[0][HomeLinkTypeData.Reason_Field].ToString());

			return retVal;
		}

		/// <summary>
		/// 修改指定记录
		/// </summary>
		/// <param name="homeLinkTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool UpdateHomeLinkType(HomeLinkTypeData homeLinkTypeData)
		{
			// 前置检查；
			ApplicationAssert.CheckCondition(homeLinkTypeData != null,"HomeLinkTypeData is required", ApplicationAssert.LineNumber);

			return (new HomeLinkTypeRules()).UpdateHomeLinkType(homeLinkTypeData);
		}

		/// <summary>
		/// 删除友情链接类型
		/// </summary>
		/// <param name="linkTypeID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelHomeLinkType(int linkTypeID)
		{
			bool retVal = false;

			HomeLinkTypeData homeLinkTypeData = new HomeLinkTypeData();
			DataRow homeLinkTypeDataRow = homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].NewRow();
			homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].Rows.Add(homeLinkTypeDataRow);
			homeLinkTypeData.AcceptChanges();

			homeLinkTypeDataRow[HomeLinkTypeData.LinkTypeID_Field]		= linkTypeID;

			retVal = (new HomeLinkTypeRules()).DelHomeLinkType(homeLinkTypeData);

			return retVal;
		}

		/// <summary>
		/// ListType:1-ID获取记录；2-获取全部记录(IsAvailable=2全部，否则0、1为有效或无效)
		/// </summary>
		/// <param name="listType"></param>
		/// <param name="linkTypeID"></param>
		/// <param name="isAvailable"></param>
		/// <returns>数据实体HomeLinkTypeData</returns>
		public HomeLinkTypeData GetHomeLinkTypeByListType(int listType,int linkTypeID,int isAvailable)
		{
			using (HomeLinkTypeDataAccess homeLinkTypeDataAccess = new HomeLinkTypeDataAccess())
			{
				return homeLinkTypeDataAccess.GetHomeLinkTypeByListType(listType,linkTypeID,isAvailable);
			}
		}

	}
}
