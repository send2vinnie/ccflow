using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

using Tax666.SystemFramework;
using Tax666.DataEntity;

namespace Tax666.DataAccess
{
	/// <summary>
	/// 定义用来管理对用户对象在数据库中的（Selects，Inserts，Updates,Delete）操作的数据访问层。
	/// <remarks>
	///  在这个类中,带参数集的Sql Command命令对象和DataSet作为单一数据表的数据集应用于所有的Select,Insert和Update业务中；
	///  继承基类IDisposable,关闭数据库连接时,该类从内存中被释放。
	/// </remarks>
	/// </summary>
	public class ArticleDataAccess : IDisposable
	{
		private SqlDataAdapter dsCommand;

		//定义存储过程所用到的参数；
		public const String ArticleID_Pram			= "@ArticleID";
        public const String BigTypeID_Pram          = "@BigTypeID";
        public const String SmallTypeID_Pram        = "@SmallTypeID";
		public const String Title_Pram			    = "@Title";
		public const String ArticleHtmlDesc_Pram	= "@ArticleHtmlDesc";
		public const String ArticlePicPath_Pram		= "@ArticlePicPath";
		public const String Author_Pram			    = "@Author";
		public const String Sourse_Pram			    = "@Sourse";
		public const String AuthorEmail_Pram		= "@AuthorEmail";
		public const String AuthorHomePage_Pram		= "@AuthorHomePage";
		public const String CreateTime_Pram			= "@CreateTime";
		public const String IsCommend_Pram			= "@IsCommend";
		public const String IsTop_Pram			    = "@IsTop";
		public const String ReadNum_Pram			= "@ReadNum";
		public const String TrampleNum_Pram			= "@TrampleNum";
		public const String PeakNum_Pram			= "@PeakNum";
		public const String IsAudit_Pram			= "@IsAudit";
        public const String IsSubject_Pram          = "@IsSubject";
		public const String IsAvailable_Pram		= "@IsAvailable";
		public const String OptionType_Pram			= "@OptionType";
		public const String ListType_Pram			= "@ListType";
		public const String Reason_Pram			    = "@Reason";

		private SqlCommand deleteCommand;
		private SqlCommand insertCommand;
		private SqlCommand setCommand;
		private SqlCommand updateCommand;
		private SqlCommand moveCommand;

		public ArticleDataAccess()
		{
			/// <summary>
			///  ArticleDataAccess 构造函数，创建DataSet Command。
			///   <remarks>初始化DataSet Command对象。</remarks>
			/// </summary>
			dsCommand = new SqlDataAdapter();

			dsCommand.SelectCommand = new SqlCommand();
			dsCommand.SelectCommand.Connection = new SqlConnection(Tax666Configuration.ConnectionString);

			dsCommand.TableMappings.Add("Table",ArticleData.Article_Table);
		}

		/// <summary>
		/// 释放对象资源。
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(true);
		}

		/// <summary>
		/// 释放该对象的实例化变量。
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if(! disposing)
				return;

			if (dsCommand != null)
			{
				if(dsCommand.SelectCommand != null)
				{
					if(dsCommand.SelectCommand.Connection != null)
					{
						dsCommand.SelectCommand.Connection.Dispose();
					}
					dsCommand.SelectCommand.Dispose();
				}
				dsCommand.Dispose();
				dsCommand = null;
			}
		}

		/// <summary>
		///返回包含指定查找内容的数据实体。
		/// </summary>
		/// <param name="commandText">sql存储过程名称</param>
		/// <param name="paramName">sql参数名称</param>
		/// <param name="paramValue">sql参数值</param>
		/// <returns>数据实体ArticleData</returns>
		private ArticleData FillArticleData(String commandText, String paramName, String paramValue)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			ArticleData data = new ArticleData();
			SqlCommand command = dsCommand.SelectCommand;

			command.CommandText = commandText;
			command.CommandType = CommandType.StoredProcedure;
			SqlParameter param = new SqlParameter(paramName, SqlDbType.NVarChar, 255);
			param.Value = paramValue;
			command.Parameters.Add(param);
			dsCommand.Fill(data);

			return data;
		}

		/// <summary>
		/// 产生一个包含所有记录的DataSet。
		/// </summary>
		/// <param name="commandText">sql存储过程名称、SQL命令；</param>
		/// <returns>数据实体ArticleData</returns>
		private ArticleData FillArticleData(String commandText)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			ArticleData data = new ArticleData();
			SqlCommand command = dsCommand.SelectCommand;

			command.CommandText = commandText;
			command.CommandType = CommandType.StoredProcedure;
			dsCommand.Fill(data);

			return data;
		}

		/// <summary>
		/// 产生一个包含满足参数指定条件的数据实体。
		/// </summary>
		/// <param name="commandText">sql存储过程名称</param>
		/// <param name="prams">序列化的参数组</param>
		/// <param name="paramValue">sql参数值</param>
		/// <returns>数据实体ArticleData</returns>
		private ArticleData FillArticleData(String commandText,SqlParameter[] prams)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			ArticleData data = new ArticleData();
			SqlCommand command = dsCommand.SelectCommand;

			command.CommandText = commandText;
			command.CommandType = CommandType.StoredProcedure;

			if (prams != null)
			{
				foreach (SqlParameter parameter in prams)
					command.Parameters.Add(parameter);
			}
			dsCommand.Fill(data);

			return data;
		}

		/// <summary>
		/// 为DataAdapter初始化带参数的Insert command。
		/// </summary>
		/// <returns></returns>
		private SqlCommand GetInsertCommand()
		{
			if (insertCommand == null)
			{
				insertCommand = new SqlCommand("sp_InsertArticle",new SqlConnection(Tax666Configuration.ConnectionString));
				insertCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(BigTypeID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(SmallTypeID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(Title_Pram, SqlDbType.NVarChar,160));
				sqlParams.Add(new SqlParameter(ArticleHtmlDesc_Pram, SqlDbType.NText));
				sqlParams.Add(new SqlParameter(Author_Pram, SqlDbType.NVarChar,50));
				sqlParams.Add(new SqlParameter(Sourse_Pram, SqlDbType.NVarChar,100));
				sqlParams.Add(new SqlParameter(AuthorEmail_Pram, SqlDbType.NVarChar,200));
				sqlParams.Add(new SqlParameter(AuthorHomePage_Pram, SqlDbType.NVarChar,200));
				sqlParams.Add(new SqlParameter(IsAudit_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(IsCommend_Pram, SqlDbType.Bit));
				sqlParams.Add(new SqlParameter(IsTop_Pram, SqlDbType.Bit));
                sqlParams.Add(new SqlParameter(IsSubject_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(IsAvailable_Pram, SqlDbType.Bit));
				sqlParams.Add(new SqlParameter(ArticleID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(Reason_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
                sqlParams[BigTypeID_Pram].SourceColumn          = ArticleData.BigTypeID_Field;
                sqlParams[SmallTypeID_Pram].SourceColumn        = ArticleData.SmallTypeID_Field;
                sqlParams[IsSubject_Pram].SourceColumn          = ArticleData.IsSubject_Field;
				sqlParams[Title_Pram].SourceColumn		        = ArticleData.Title_Field;
				sqlParams[ArticleHtmlDesc_Pram].SourceColumn	= ArticleData.ArticleHtmlDesc_Field;
				sqlParams[Author_Pram].SourceColumn		        = ArticleData.Author_Field;
				sqlParams[Sourse_Pram].SourceColumn		        = ArticleData.Sourse_Field;
				sqlParams[AuthorEmail_Pram].SourceColumn		= ArticleData.AuthorEmail_Field;
				sqlParams[AuthorHomePage_Pram].SourceColumn		= ArticleData.AuthorHomePage_Field;
				sqlParams[IsAudit_Pram].SourceColumn		    = ArticleData.IsAudit_Field;
				sqlParams[IsCommend_Pram].SourceColumn		    = ArticleData.IsCommend_Field;
				sqlParams[IsTop_Pram].SourceColumn		        = ArticleData.IsTop_Field;
				sqlParams[IsAvailable_Pram].SourceColumn		= ArticleData.IsAvailable_Field;

				sqlParams[ArticleID_Pram].SourceColumn		    = ArticleData.ArticleID_Field;
				sqlParams[ArticleID_Pram].Direction		        = ParameterDirection.Output;

				sqlParams[Reason_Pram].SourceColumn		        = ArticleData.Reason_Field;
				sqlParams[Reason_Pram].Direction		        = ParameterDirection.Output;

			}

			return insertCommand;
		}

		/// <summary>
		/// 向数据库中插入一条新记录。
		/// </summary>
		/// <param name="articleData"></param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertArticle(ArticleData articleData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}

			dsCommand.InsertCommand = GetInsertCommand();
			dsCommand.Update(articleData, ArticleData.Article_Table);

			// 检查表错误以查看更新是否成功；
			if (articleData.HasErrors)
			{
				articleData.Tables[ArticleData.Article_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				articleData.AcceptChanges();
				return true;
			}
		}

		/// <summary>
		/// 为DataAdapter初始化带参数的Update command。
		/// </summary>
		/// <returns></returns>
		private SqlCommand GetUpdateCommand()
		{
			if (updateCommand == null)
			{
				updateCommand = new SqlCommand("sp_UpdateArticle",new SqlConnection(Tax666Configuration.ConnectionString));
				updateCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(ArticleID_Pram, SqlDbType.Int));
                sqlParams.Add(new SqlParameter(BigTypeID_Pram, SqlDbType.Int));
                sqlParams.Add(new SqlParameter(SmallTypeID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(Title_Pram, SqlDbType.NVarChar,160));
				sqlParams.Add(new SqlParameter(ArticleHtmlDesc_Pram, SqlDbType.NText));
				sqlParams.Add(new SqlParameter(Author_Pram, SqlDbType.NVarChar,50));
				sqlParams.Add(new SqlParameter(Sourse_Pram, SqlDbType.NVarChar,100));
				sqlParams.Add(new SqlParameter(AuthorEmail_Pram, SqlDbType.NVarChar,200));
				sqlParams.Add(new SqlParameter(AuthorHomePage_Pram, SqlDbType.NVarChar,200));
				sqlParams.Add(new SqlParameter(IsCommend_Pram, SqlDbType.Bit));
				sqlParams.Add(new SqlParameter(IsTop_Pram, SqlDbType.Bit));
                sqlParams.Add(new SqlParameter(IsSubject_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(IsAvailable_Pram, SqlDbType.Bit));
				sqlParams.Add(new SqlParameter(Reason_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[ArticleID_Pram].SourceColumn		    = ArticleData.ArticleID_Field;
                sqlParams[BigTypeID_Pram].SourceColumn          = ArticleData.BigTypeID_Field;
                sqlParams[SmallTypeID_Pram].SourceColumn        = ArticleData.SmallTypeID_Field;
				sqlParams[Title_Pram].SourceColumn		        = ArticleData.Title_Field;
				sqlParams[ArticleHtmlDesc_Pram].SourceColumn    = ArticleData.ArticleHtmlDesc_Field;
				sqlParams[Author_Pram].SourceColumn		        = ArticleData.Author_Field;
				sqlParams[Sourse_Pram].SourceColumn		        = ArticleData.Sourse_Field;
				sqlParams[AuthorEmail_Pram].SourceColumn		= ArticleData.AuthorEmail_Field;
				sqlParams[AuthorHomePage_Pram].SourceColumn		= ArticleData.AuthorHomePage_Field;
				sqlParams[IsCommend_Pram].SourceColumn		    = ArticleData.IsCommend_Field;
				sqlParams[IsTop_Pram].SourceColumn		        = ArticleData.IsTop_Field;
                sqlParams[IsSubject_Pram].SourceColumn          = ArticleData.IsSubject_Field;
				sqlParams[IsAvailable_Pram].SourceColumn		= ArticleData.IsAvailable_Field;

				sqlParams[Reason_Pram].SourceColumn		        = ArticleData.Reason_Field;
                sqlParams[Reason_Pram].Direction                = ParameterDirection.Output;
			}

			return updateCommand;
		}

		/// <summary>
		/// 修改指定ID的文章属性
		/// </summary>
		/// <param name="articleData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool UpdateArticle(ArticleData articleData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetUpdateCommand();

			dsCommand.Update(articleData, ArticleData.Article_Table);

			// 检查表错误以查看更新是否成功；
			if ( articleData.HasErrors )
			{
				articleData.Tables[ArticleData.Article_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				articleData.AcceptChanges();
				return true;
			}
		}

		/// <summary>
		/// 为DataAdapter初始化带参数的Update command。
		/// </summary>
		/// <returns></returns>
		private SqlCommand GetSetCommand()
		{
			if (setCommand == null)
			{
				setCommand = new SqlCommand("sp_SetArticleInfo",new SqlConnection(Tax666Configuration.ConnectionString));
				setCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = setCommand.Parameters;

				sqlParams.Add(new SqlParameter(OptionType_Pram, SqlDbType.NVarChar,10));
				sqlParams.Add(new SqlParameter(ArticleID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(ArticlePicPath_Pram, SqlDbType.NVarChar,200));

				// 定义数据集中表的参数对应；
				sqlParams[OptionType_Pram].SourceColumn		    = ArticleData.OptionType_Field;
				sqlParams[ArticleID_Pram].SourceColumn		    = ArticleData.ArticleID_Field;
				sqlParams[ArticlePicPath_Pram].SourceColumn		= ArticleData.ArticlePicPath_Field;
			}

			return setCommand;
		}

		/// <summary>
		/// OptionType : UPDATEPIC-更新图片地址；ADDNUM-更新阅读次数；TOP-固顶；COMMEND-推荐；VALID-有效；AUDIT-审核；TRAMPLE-顶一下；PEAK-踩一下；
		/// </summary>
		/// <param name="articleData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetArticleInfo(ArticleData articleData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetSetCommand();

			dsCommand.Update(articleData, ArticleData.Article_Table);

			// 检查表错误以查看更新是否成功；
			if ( articleData.HasErrors )
			{
				articleData.Tables[ArticleData.Article_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				articleData.AcceptChanges();
				return true;
			}
		}
        /// <summary>
        /// 获取所有文章列表（分页）
        /// </summary>
        /// <param name="articleTabID"></param>
        /// <param name="isAvailable"></param>
        /// <param name="isAudit"></param>
        /// <param name="dateRange"></param>
        /// <param name="textKey"></param>
        /// <param name="agentID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="doCount"></param>
        /// <returns>数据实体ArticleData</returns>
        public ArticleData GetArticlesAdminAll(int bigTypeID, int smallTypeID, int isAvailable, int isAudit, string textKey, int pageSize, int pageIndex, bool doCount)
        {
            SqlParameter[] prams =	{										
										new SqlParameter("@BigTypeID",SqlDbType.Int,4),
										new SqlParameter("@SmallTypeID",SqlDbType.Int,4),
										new SqlParameter("@IsAvailable",SqlDbType.Int,4),
										new SqlParameter("@IsAudit",SqlDbType.Int,4),
										new SqlParameter("@TextKey",SqlDbType.NVarChar,50),
										new SqlParameter("@PageSize",SqlDbType.Int,4),
										new SqlParameter("@PageIndex",SqlDbType.Int,4),
										new SqlParameter("@DoCount",SqlDbType.Bit,1)
									};

            prams[0].Value = bigTypeID;
            prams[1].Value = smallTypeID;
            prams[2].Value = isAvailable;
            prams[3].Value = isAudit;
            prams[4].Value = textKey;
            prams[5].Value = pageSize;
            prams[6].Value = pageIndex;
            prams[7].Value = doCount;

            return FillArticleData("sp_GetArticlesAdminAll", prams);
        }
		/// <summary>
		/// 为DataAdapter初始化带参数的Update command。
		/// </summary>
		/// <returns></returns>
		private SqlCommand GetMoveCommand()
		{
			if (moveCommand == null)
			{
				moveCommand = new SqlCommand("sp_MoveArticleTabType",new SqlConnection(Tax666Configuration.ConnectionString));
				moveCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = moveCommand.Parameters;

				sqlParams.Add(new SqlParameter(ArticleID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(BigTypeID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(SmallTypeID_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[ArticleID_Pram].SourceColumn		    = ArticleData.ArticleID_Field;
                sqlParams[BigTypeID_Pram].SourceColumn          = ArticleData.BigTypeID_Field;
                sqlParams[SmallTypeID_Pram].SourceColumn        = ArticleData.SmallTypeID_Field;
			}

			return moveCommand;
		}

		/// <summary>
		/// 将某文章移到指定的栏目下
		/// </summary>
		/// <param name="articleData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool MoveArticleTabType(ArticleData articleData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetMoveCommand();

			dsCommand.Update(articleData, ArticleData.Article_Table);

			// 检查表错误以查看更新是否成功；
			if ( articleData.HasErrors )
			{
				articleData.Tables[ArticleData.Article_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				articleData.AcceptChanges();
				return true;
			}
		}

		/// <summary>
		/// 为DataAdapter初始化带参数的Update command。
		/// </summary>
		/// <returns></returns>
		private SqlCommand GetDeleteCommand()
		{
			if (deleteCommand == null)
			{
				deleteCommand = new SqlCommand("sp_DelArticle",new SqlConnection(Tax666Configuration.ConnectionString));
				deleteCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(ArticleID_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[ArticleID_Pram].SourceColumn		= ArticleData.ArticleID_Field;
			}

			return deleteCommand;
		}

		/// <summary>
		/// 删除指定ID的文章
		/// </summary>
		/// <param name="articleData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticle(ArticleData articleData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetDeleteCommand();

			dsCommand.Update(articleData, ArticleData.Article_Table);

			// 检查表错误以查看更新是否成功；
			if ( articleData.HasErrors )
			{
				articleData.Tables[ArticleData.Article_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				articleData.AcceptChanges();
				return true;
			}
		}

		/// <summary>
		/// ListType:1-根据ID获取记录;2-栏目热门TOP10;3-栏目最新TOP10;4-获取栏目推荐TOP10;5-获取获取图片新闻TOP10;
		/// </summary>
		/// <param name="listType"></param>
		/// <param name="articleTabID"></param>
		/// <param name="articleID"></param>
		/// <returns>数据实体ArticleData</returns>
        public ArticleData GetArticleByListType(int listType, int bigTypeID, int smallTypeID, int articleID)
		{
			SqlParameter[] prams =	{
										new SqlParameter("@ListType",SqlDbType.Int,4),
										new SqlParameter("@BigTypeID",SqlDbType.Int,4),
										new SqlParameter("@SmallTypeID",SqlDbType.Int,4),
										new SqlParameter("@ArticleID",SqlDbType.Int,4)
									};

			prams[0].Value = listType;
            prams[1].Value = bigTypeID;
            prams[2].Value = smallTypeID;
			prams[3].Value = articleID;

			return FillArticleData("sp_GetArticleByListType",prams);
		}

		/// <summary>
		/// 获取ID文章的相关文章列表
		/// </summary>
		/// <param name="articleID"></param>
		/// <returns>数据实体ArticleData</returns>
        public ArticleData GetArticleRelation(int articleID)
        {
            SqlParameter[] prams =	{
										new SqlParameter("@ArticleID",SqlDbType.Int,4)
									};

            prams[0].Value = articleID;

            return FillArticleData("sp_GetArticleRelation", prams);
        }

		/// <summary>
		/// 统计某个栏目的文章（目录树用到）
		/// </summary>
		/// <param name="articleTabID"></param>
		/// <returns>数据实体ArticleData</returns>
		public ArticleData GetArticlesSort(int articleTabID)
		{
			SqlParameter[] prams =	{
										new SqlParameter("@ArticleTabID",SqlDbType.Int,4)
									};

			prams[0].Value = articleTabID;

			return FillArticleData("sp_GetArticlesSort",prams);
		}

		/// <summary>
		/// 文章前台分页显示（含搜索功能）
		/// </summary>
		/// <param name="articleTabID"></param>
		/// <param name="dateRange"></param>
		/// <param name="textKey"></param>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="doCount"></param>
		/// <returns>数据实体ArticleData</returns>
        public ArticleData GetArticleWebList(int bigTypeID, int smallTypeID, int dateRange, string textKey, int pageSize, int pageIndex, bool doCount)
		{
			SqlParameter[] prams =	{
										new SqlParameter("@BigTypeID",SqlDbType.Int,4),
										new SqlParameter("@SmallTypeID",SqlDbType.Int,4),
										new SqlParameter("@DateRange",SqlDbType.Int,4),
										new SqlParameter("@TextKey",SqlDbType.NVarChar,50),
										new SqlParameter("@PageSize",SqlDbType.Int,4),
										new SqlParameter("@PageIndex",SqlDbType.Int,4),
										new SqlParameter("@DoCount",SqlDbType.Bit,1)
									};

            prams[0].Value = bigTypeID;
            prams[1].Value = smallTypeID;
			prams[2].Value = dateRange;
			prams[3].Value = textKey;
			prams[4].Value = pageSize;
			prams[5].Value = pageIndex;
			prams[6].Value = doCount;

			return FillArticleData("sp_GetArticleWebList",prams);
		}

        /// <summary>
		/// 获取文章列表（自动填充）
		/// </summary>
		/// <param name="word1"></param>
		/// <param name="word2"></param>
		/// <param name="word3"></param>
		/// <param name="word4"></param>
		/// <param name="word5"></param>
		/// <param name="doCount"></param>
		/// <returns>数据实体ArticleData</returns>
		public ArticleData GetArticleAuto(string word1, string word2, string word3, string word4, string word5,bool doCount)
		{
			SqlParameter[] prams =	{
										new SqlParameter("@Word1",SqlDbType.NVarChar,15),
										new SqlParameter("@Word2",SqlDbType.NVarChar,15),
										new SqlParameter("@Word3",SqlDbType.NVarChar,15),
										new SqlParameter("@Word4",SqlDbType.NVarChar,15),
										new SqlParameter("@Word5",SqlDbType.NVarChar,15),
										new SqlParameter("@DoCount",SqlDbType.Bit,1)
									};

			prams[0].Value = word1;
			prams[1].Value = word2;
			prams[2].Value = word3;
			prams[3].Value = word4;
			prams[4].Value = word5;
			prams[5].Value = doCount;

			return FillArticleData("sp_GetArticleAuto",prams);
		}

        /// <summary>
        /// 获取文章资讯搜索记录列表；
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="doCount"></param>
        /// <returns>数据实体ArticleData</returns>
        public ArticleData GetArticleSearch(string where, int pageSize, int pageIndex, bool doCount)
        {
            SqlParameter[] prams =	{
										new SqlParameter("@Where",SqlDbType.NVarChar,2000),
										new SqlParameter("@PageSize",SqlDbType.Int,4),
										new SqlParameter("@PageIndex",SqlDbType.Int,4),
										new SqlParameter("@DoCount",SqlDbType.Bit,1)
									};

            prams[0].Value = where;
            prams[1].Value = pageSize;
            prams[2].Value = pageIndex;
            prams[3].Value = doCount;

            return FillArticleData("sp_GetArticleSearch", prams);
        }

	}
}
