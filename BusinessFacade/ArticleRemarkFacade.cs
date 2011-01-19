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
	/// 该类包含ArticleRemark系统的业务表现。
	/// <remarks>
	///  业务表现用于提供业务规则子系统的简单接口。
	///  该类被标记为MarshalByRefObject以支持远程处理。
	/// </remarks>
	/// </summary>
	public class ArticleRemarkFacade : MarshalByRefObject
	{
		public ArticleRemarkFacade()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		///  添加评论
		/// </summary>
		/// <param name="articleID"></param>
		/// <param name="remarkName"></param>
		/// <param name="remarkDesc"></param>
		/// <returns>成功则返回true，失败则返回false</returns>
		public bool InsertArticleRemark(
						int articleID,
						string remarkName,
						string remarkDesc)
		{
			bool retVal = false;

			ArticleRemarkData articleRemarkData = new ArticleRemarkData();
			DataTable table = articleRemarkData.Tables[ArticleRemarkData.ArticleRemark_Table];
			DataRow row = table.NewRow();

			row[ArticleRemarkData.ArticleID_Field]		= articleID;
			row[ArticleRemarkData.RemarkName_Field]		= remarkName;
			row[ArticleRemarkData.RemarkDesc_Field]		= remarkDesc;

			table.Rows.Add(row);
			retVal = (new ArticleRemarkRules()).InsertArticleRemark(articleRemarkData);

			return retVal;
		}

		/// <summary>
		/// 回复评论（只能单条）
		/// </summary>
		/// <param name="remarkID"></param>
		/// <param name="replyName"></param>
		/// <param name="remarkReply"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetArticleRemarkReply(int remarkID,string replyName,string remarkReply)
		{
			bool retVal = false;

			ArticleRemarkData articleRemarkData = new ArticleRemarkData();
			DataRow articleRemarkDataRow = articleRemarkData.Tables[ArticleRemarkData.ArticleRemark_Table].NewRow();
			articleRemarkData.Tables[ArticleRemarkData.ArticleRemark_Table].Rows.Add(articleRemarkDataRow);
			articleRemarkData.AcceptChanges();

			articleRemarkDataRow[ArticleRemarkData.RemarkID_Field]		= remarkID;
			articleRemarkDataRow[ArticleRemarkData.ReplyName_Field]		= replyName;
			articleRemarkDataRow[ArticleRemarkData.RemarkReply_Field]	= remarkReply;

			retVal = (new ArticleRemarkRules()).SetArticleRemarkReply(articleRemarkData);

			return retVal;
		}

		/// <summary>
		/// 删除评论
		/// </summary>
		/// <param name="remarkID"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticleRemark(int remarkID)
		{
			bool retVal = false;

			ArticleRemarkData articleRemarkData = new ArticleRemarkData();
			DataRow articleRemarkDataRow = articleRemarkData.Tables[ArticleRemarkData.ArticleRemark_Table].NewRow();
			articleRemarkData.Tables[ArticleRemarkData.ArticleRemark_Table].Rows.Add(articleRemarkDataRow);
			articleRemarkData.AcceptChanges();

			articleRemarkDataRow[ArticleRemarkData.RemarkID_Field]		= remarkID;

			retVal = (new ArticleRemarkRules()).DelArticleRemark(articleRemarkData);

			return retVal;
		}

		/// <summary>
		/// 获取评论列表
		/// </summary>
		/// <param name="where"></param>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="doCount"></param>
		/// <returns>数据实体ArticleRemarkData</returns>
		public ArticleRemarkData GetArticleRemark(string where,int pageSize,int pageIndex,bool doCount)
		{
			using (ArticleRemarkDataAccess articleRemarkDataAccess = new ArticleRemarkDataAccess())
			{
				return articleRemarkDataAccess.GetArticleRemark(where,pageSize,pageIndex,doCount);
			}
		}

	}
}
