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
	public class ArticleSmallTypeDataAccess : IDisposable
	{
		private SqlDataAdapter dsCommand;

		//定义存储过程所用到的参数；
		public const String SmallTypeID_Pram			= "@SmallTypeID";
		public const String BigTypeID_Pram			    = "@BigTypeID";
		public const String SmallTypeName_Pram			= "@SmallTypeName";
		public const String SmallTypeDesc_Pram			= "@SmallTypeDesc";
		public const String IsSystem_Pram			    = "@IsSystem";
		public const String IsAvailable_Pram			= "@IsAvailable";
		public const String OptionType_Pram			    = "@OptionType";
		public const String ListType_Pram			    = "@ListType";
		public const String Reason_Pram			        = "@Reason";

		private SqlCommand deleteCommand;
		private SqlCommand insertCommand;
		private SqlCommand infoCommand;
		private SqlCommand updateCommand;

		public ArticleSmallTypeDataAccess()
		{
			/// <summary>
			///  ArticleSmallTypeDataAccess 构造函数，创建DataSet Command。
			///   <remarks>初始化DataSet Command对象。</remarks>
			/// </summary>
			dsCommand = new SqlDataAdapter();

			dsCommand.SelectCommand = new SqlCommand();
			dsCommand.SelectCommand.Connection = new SqlConnection(Tax666Configuration.ConnectionString);

			dsCommand.TableMappings.Add("Table",ArticleSmallTypeData.ArticleSmallType_Table);
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
		/// <returns>数据实体ArticleSmallTypeData</returns>
		private ArticleSmallTypeData FillArticleSmallTypeData(String commandText, String paramName, String paramValue)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			ArticleSmallTypeData data = new ArticleSmallTypeData();
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
		/// <returns>数据实体ArticleSmallTypeData</returns>
		private ArticleSmallTypeData FillArticleSmallTypeData(String commandText)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			ArticleSmallTypeData data = new ArticleSmallTypeData();
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
		/// <returns>数据实体ArticleSmallTypeData</returns>
		private ArticleSmallTypeData FillArticleSmallTypeData(String commandText,SqlParameter[] prams)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			ArticleSmallTypeData data = new ArticleSmallTypeData();
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
				insertCommand = new SqlCommand("sp_InsertArticleSmallType",new SqlConnection(Tax666Configuration.ConnectionString));
				insertCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(BigTypeID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(SmallTypeName_Pram, SqlDbType.NVarChar,50));
				sqlParams.Add(new SqlParameter(SmallTypeDesc_Pram, SqlDbType.NVarChar,200));
				sqlParams.Add(new SqlParameter(IsAvailable_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(Reason_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[BigTypeID_Pram].SourceColumn		    = ArticleSmallTypeData.BigTypeID_Field;
				sqlParams[SmallTypeName_Pram].SourceColumn		= ArticleSmallTypeData.SmallTypeName_Field;
				sqlParams[SmallTypeDesc_Pram].SourceColumn		= ArticleSmallTypeData.SmallTypeDesc_Field;
				sqlParams[IsAvailable_Pram].SourceColumn		= ArticleSmallTypeData.IsAvailable_Field;
				sqlParams[Reason_Pram].SourceColumn		        = ArticleSmallTypeData.Reason_Field;
				sqlParams[Reason_Pram].Direction		        = ParameterDirection.Output;

			}

			return insertCommand;
		}

		/// <summary>
		/// 向数据库中插入一条新记录。
		/// </summary>
		/// <param name="articleSmallTypeData"></param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertArticleSmallType(ArticleSmallTypeData articleSmallTypeData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}

			dsCommand.InsertCommand = GetInsertCommand();
			dsCommand.Update(articleSmallTypeData, ArticleSmallTypeData.ArticleSmallType_Table);

			// 检查表错误以查看更新是否成功；
			if (articleSmallTypeData.HasErrors)
			{
				articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				articleSmallTypeData.AcceptChanges();
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
				updateCommand = new SqlCommand("sp_UpdateArticleSmallType",new SqlConnection(Tax666Configuration.ConnectionString));
				updateCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(SmallTypeID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(BigTypeID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(SmallTypeName_Pram, SqlDbType.NVarChar,50));
				sqlParams.Add(new SqlParameter(SmallTypeDesc_Pram, SqlDbType.NVarChar,200));
				sqlParams.Add(new SqlParameter(IsAvailable_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(Reason_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[SmallTypeID_Pram].SourceColumn		= ArticleSmallTypeData.SmallTypeID_Field;
				sqlParams[BigTypeID_Pram].SourceColumn		    = ArticleSmallTypeData.BigTypeID_Field;
				sqlParams[SmallTypeName_Pram].SourceColumn		= ArticleSmallTypeData.SmallTypeName_Field;
				sqlParams[SmallTypeDesc_Pram].SourceColumn		= ArticleSmallTypeData.SmallTypeDesc_Field;
				sqlParams[IsAvailable_Pram].SourceColumn		= ArticleSmallTypeData.IsAvailable_Field;
				sqlParams[Reason_Pram].SourceColumn		        = ArticleSmallTypeData.Reason_Field;
                sqlParams[Reason_Pram].Direction                = ParameterDirection.Output;
			}

			return updateCommand;
		}

		/// <summary>
		/// 修改小类记录属性
		/// </summary>
		/// <param name="articleSmallTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool UpdateArticleSmallType(ArticleSmallTypeData articleSmallTypeData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetUpdateCommand();

			dsCommand.Update(articleSmallTypeData, ArticleSmallTypeData.ArticleSmallType_Table);

			// 检查表错误以查看更新是否成功；
			if ( articleSmallTypeData.HasErrors )
			{
				articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				articleSmallTypeData.AcceptChanges();
				return true;
			}
		}

		/// <summary>
		/// 为DataAdapter初始化带参数的Update command。
		/// </summary>
		/// <returns></returns>
		private SqlCommand GetInfoCommand()
		{
			if (infoCommand == null)
			{
				infoCommand = new SqlCommand("sp_SetArticleSmallTypeInfo",new SqlConnection(Tax666Configuration.ConnectionString));
				infoCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = infoCommand.Parameters;

				sqlParams.Add(new SqlParameter(OptionType_Pram, SqlDbType.NVarChar,20));
				sqlParams.Add(new SqlParameter(SmallTypeID_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[OptionType_Pram].SourceColumn		= ArticleSmallTypeData.OptionType_Field;
				sqlParams[SmallTypeID_Pram].SourceColumn	= ArticleSmallTypeData.SmallTypeID_Field;
			}

			return infoCommand;
		}

		/// <summary>
		/// 设置文章小类记录属性
		/// </summary>
		/// <param name="articleSmallTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetArticleSmallTypeInfo(ArticleSmallTypeData articleSmallTypeData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetInfoCommand();

			dsCommand.Update(articleSmallTypeData, ArticleSmallTypeData.ArticleSmallType_Table);

			// 检查表错误以查看更新是否成功；
			if ( articleSmallTypeData.HasErrors )
			{
				articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				articleSmallTypeData.AcceptChanges();
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
				deleteCommand = new SqlCommand("sp_DelArticleSmallType",new SqlConnection(Tax666Configuration.ConnectionString));
				deleteCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(SmallTypeID_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[SmallTypeID_Pram].SourceColumn		= ArticleSmallTypeData.SmallTypeID_Field;
			}

			return deleteCommand;
		}

		/// <summary>
		/// 删除文章小类
		/// </summary>
		/// <param name="articleSmallTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelArticleSmallType(ArticleSmallTypeData articleSmallTypeData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetDeleteCommand();

			dsCommand.Update(articleSmallTypeData, ArticleSmallTypeData.ArticleSmallType_Table);

			// 检查表错误以查看更新是否成功；
			if ( articleSmallTypeData.HasErrors )
			{
				articleSmallTypeData.Tables[ArticleSmallTypeData.ArticleSmallType_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				articleSmallTypeData.AcceptChanges();
				return true;
			}
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
			SqlParameter[] prams =	{
										new SqlParameter("@ListType",SqlDbType.Int,4),
										new SqlParameter("@BigTypeID",SqlDbType.Int,4),
										new SqlParameter("@SmallTypeID",SqlDbType.Int,4),
										new SqlParameter("@IsAvailable",SqlDbType.Int,4),
										new SqlParameter("@PageSize",SqlDbType.Int,4),
										new SqlParameter("@PageIndex",SqlDbType.Int,4),
										new SqlParameter("@DoCount",SqlDbType.Bit,1)
									};

			prams[0].Value = listType;
			prams[1].Value = bigTypeID;
			prams[2].Value = smallTypeID;
			prams[3].Value = isAvailable;
			prams[4].Value = pageSize;
			prams[5].Value = pageIndex;
			prams[6].Value = doCount;

			return FillArticleSmallTypeData("sp_GetArticleSmallTypeByList",prams);
		}

	}
}
