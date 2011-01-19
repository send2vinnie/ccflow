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
	/// 该类包含ArticleBigType系统的业务表现。
	/// <remarks>
	///  业务表现用于提供业务规则子系统的简单接口。
	///  该类被标记为MarshalByRefObject以支持远程处理。
	/// </remarks>
	/// </summary>
	public class ArticleBigTypeFacade : MarshalByRefObject
	{
		public ArticleBigTypeFacade()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  添加文章大类
		/// </summary>
		/// <param name="bigTypeName"></param>
		/// <param name="bigTypeDesc"></param>

		/// <param name="reason"></param>
		/// <returns>成功则返回true，失败则返回false</returns>
		public bool InsertArticleBigType(
						string bigTypeName,
						string bigTypeDesc,
                        out int reason)
		{
			bool retVal = false;

			ArticleBigTypeData articleBigTypeData = new ArticleBigTypeData();
			DataTable table = articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table];
			DataRow row = table.NewRow();

			row[ArticleBigTypeData.BigTypeName_Field]	= bigTypeName;
			row[ArticleBigTypeData.BigTypeDesc_Field]	= bigTypeDesc;

			table.Rows.Add(row);
			retVal = (new ArticleBigTypeRules()).InsertArticleBigType(articleBigTypeData);

			reason = Int32.Parse(articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows[0][ArticleBigTypeData.Reason_Field].ToString());

			return retVal;
		}

		/// <summary>
		/// 更新大类
		/// </summary>
		/// <param name="articleBigTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool UpdateArticleBigType(ArticleBigTypeData articleBigTypeData)
		{
			// 前置检查；
			ApplicationAssert.CheckCondition(articleBigTypeData != null,"ArticleBigTypeData is required", ApplicationAssert.LineNumber);

			return (new ArticleBigTypeRules()).UpdateArticleBigType(articleBigTypeData);
		}

		/// <summary>
		/// 设置大类属性
		/// </summary>
		/// <param name="bigTypeID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetArticleBigTypeInfo(int bigTypeID)
		{
			bool retVal = false;

			ArticleBigTypeData articleBigTypeData = new ArticleBigTypeData();
			DataRow articleBigTypeDataRow = articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].NewRow();
			articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows.Add(articleBigTypeDataRow);
			articleBigTypeData.AcceptChanges();

			articleBigTypeDataRow[ArticleBigTypeData.BigTypeID_Field]		= bigTypeID;

			retVal = (new ArticleBigTypeRules()).SetArticleBigTypeInfo(articleBigTypeData);

			return retVal;
		}

		/// <summary>
		/// 删除文章大类
		/// </summary>
		/// <param name="bigTypeID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticleBigType(int bigTypeID)
		{
			bool retVal = false;

			ArticleBigTypeData articleBigTypeData = new ArticleBigTypeData();
			DataRow articleBigTypeDataRow = articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].NewRow();
			articleBigTypeData.Tables[ArticleBigTypeData.ArticleBigType_Table].Rows.Add(articleBigTypeDataRow);
			articleBigTypeData.AcceptChanges();

			articleBigTypeDataRow[ArticleBigTypeData.BigTypeID_Field]		= bigTypeID;

			retVal = (new ArticleBigTypeRules()).DelArticleBigType(articleBigTypeData);

			return retVal;
		}

		/// <summary>
		/// 获取文章大类
		/// </summary>
		/// <param name="listType"></param>

		/// <param name="bigTypeID"></param>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="doCount"></param>
		/// <returns>数据实体ArticleBigTypeData</returns>
		public ArticleBigTypeData GetArticleBigTypeByListType(int listType,int bigTypeID,int pageSize,int pageIndex,bool doCount)
		{
			using (ArticleBigTypeDataAccess articleBigTypeDataAccess = new ArticleBigTypeDataAccess())
			{
				return articleBigTypeDataAccess.GetArticleBigTypeByListType(listType,bigTypeID,pageSize,pageIndex,doCount);
			}
		}

	}
}
