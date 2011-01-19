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
	/// 该类包含ArticleSmallType系统的业务表现。
	/// <remarks>
	///  业务表现用于提供业务规则子系统的简单接口。
	///  该类被标记为MarshalByRefObject以支持远程处理。
	/// </remarks>
	/// </summary>
	public class ArticleSmallTypeFacade : MarshalByRefObject
	{
		public ArticleSmallTypeFacade()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  添加文章小类记录
		/// </summary>
		/// <param name="bigTypeID"></param>
		/// <param name="smallTypeName"></param>
		/// <param name="smallTypeDesc"></param>

		/// <param name="isAvailable"></param>
		/// <param name="reason"></param>
		/// <returns>成功则返回true，失败则返回false</returns>
		public bool InsertArticleSmallType(
						int bigTypeID,
						string smallTypeName,
						string smallTypeDesc,
						int isAvailable,
                        out int reason)
		{
			bool retVal = false;

			ArticleSmallTypeData articleSmallTypeData = new ArticleSmallTypeData();
			DataTable table = articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table];
			DataRow row = table.NewRow();

			row[ArticleSmallTypeData.BigTypeID_Field]		= bigTypeID;
			row[ArticleSmallTypeData.SmallTypeName_Field]	= smallTypeName;
			row[ArticleSmallTypeData.SmallTypeDesc_Field]	= smallTypeDesc;
			row[ArticleSmallTypeData.IsAvailable_Field]		= isAvailable;

			table.Rows.Add(row);
			retVal = (new ArticleSmallTypeRules()).InsertArticleSmallType(articleSmallTypeData);

			reason = Int32.Parse(articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows[0][ArticleSmallTypeData.Reason_Field].ToString());

			return retVal;
		}

		/// <summary>
		/// 修改小类记录属性
		/// </summary>
		/// <param name="articleSmallTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool UpdateArticleSmallType(ArticleSmallTypeData articleSmallTypeData)
		{
			// 前置检查；
			ApplicationAssert.CheckCondition(articleSmallTypeData != null,"ArticleSmallTypeData is required", ApplicationAssert.LineNumber);

			return (new ArticleSmallTypeRules()).UpdateArticleSmallType(articleSmallTypeData);
		}

		/// <summary>
		/// 设置文章小类记录属性
		/// </summary>
		/// <param name="optionType"></param>
		/// <param name="smallTypeID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetArticleSmallTypeInfo(string optionType,int smallTypeID)
		{
			bool retVal = false;

			ArticleSmallTypeData articleSmallTypeData = new ArticleSmallTypeData();
			DataRow articleSmallTypeDataRow = articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].NewRow();
			articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Add(articleSmallTypeDataRow);
			articleSmallTypeData.AcceptChanges();

			articleSmallTypeDataRow[ArticleSmallTypeData.OptionType_Field]		= optionType;
			articleSmallTypeDataRow[ArticleSmallTypeData.SmallTypeID_Field]		= smallTypeID;

			retVal = (new ArticleSmallTypeRules()).SetArticleSmallTypeInfo(articleSmallTypeData);

			return retVal;
		}

		/// <summary>
		/// 删除文章小类
		/// </summary>
		/// <param name="smallTypeID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticleSmallType(int smallTypeID)
		{
			bool retVal = false;

			ArticleSmallTypeData articleSmallTypeData = new ArticleSmallTypeData();
			DataRow articleSmallTypeDataRow = articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].NewRow();
			articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].Rows.Add(articleSmallTypeDataRow);
			articleSmallTypeData.AcceptChanges();

			articleSmallTypeDataRow[ArticleSmallTypeData.SmallTypeID_Field]		= smallTypeID;

			retVal = (new ArticleSmallTypeRules()).DelArticleSmallType(articleSmallTypeData);

			return retVal;
		}

		/// <summary>
		/// 获取文章小类记录列表
		/// </summary>
		/// <param name="listType"></param>

		/// <param name="bigTypeID"></param>
		/// <param name="smallTypeID"></param>
		/// <param name="isAvailable"></param>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="doCount"></param>
		/// <returns>数据实体ArticleSmallTypeData</returns>
		public ArticleSmallTypeData GetArticleSmallTypeByList(int listType,int bigTypeID,int smallTypeID,int isAvailable,int pageSize,int pageIndex,bool doCount)
		{
			using (ArticleSmallTypeDataAccess articleSmallTypeDataAccess = new ArticleSmallTypeDataAccess())
			{
				return articleSmallTypeDataAccess.GetArticleSmallTypeByList(listType,bigTypeID,smallTypeID,isAvailable,pageSize,pageIndex,doCount);
			}
		}

	}
}
