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
	public class HomeLinkDataAccess : IDisposable
	{
		private SqlDataAdapter dsCommand;

		//定义存储过程所用到的参数；
		public const String LinkID_Pram			    = "@LinkID";
		public const String LinkName_Pram			= "@LinkName";
		public const String LinkUrl_Pram			= "@LinkUrl";
		public const String HomeDesc_Pram			= "@HomeDesc";
		public const String LogoPath_Pram			= "@LogoPath";
		public const String LinkTypeID_Pram			= "@LinkTypeID";
		public const String LinkMode_Pram			= "@LinkMode";
		public const String SortOrder_Pram			= "@SortOrder";
		public const String IsAudit_Pram			= "@IsAudit";
		public const String CreateTime_Pram			= "@CreateTime";
		public const String IsAvailable_Pram		= "@IsAvailable";
        public const String OptionType_Pram         = "@OptionType";
        public const String ListType_Pram           = "@ListType";
        public const String Reason_Pram             = "@Reason";

		private SqlCommand deleteCommand;
		private SqlCommand insertCommand;
		private SqlCommand modifyCommand;
		private SqlCommand updateCommand;

		public HomeLinkDataAccess()
		{
			/// <summary>
			///  HomeLinkDataAccess 构造函数，创建DataSet Command。
			///   <remarks>初始化DataSet Command对象。</remarks>
			/// </summary>
			dsCommand = new SqlDataAdapter();

			dsCommand.SelectCommand = new SqlCommand();
			dsCommand.SelectCommand.Connection = new SqlConnection(Tax666Configuration.ConnectionString);

			dsCommand.TableMappings.Add("Table",HomeLinkData.HomeLink_Table);
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
		/// <returns>数据实体HomeLinkData</returns>
		private HomeLinkData FillHomeLinkData(String commandText, String paramName, String paramValue)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			HomeLinkData data = new HomeLinkData();
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
		/// <returns>数据实体HomeLinkData</returns>
		private HomeLinkData FillHomeLinkData(String commandText)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			HomeLinkData data = new HomeLinkData();
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
		/// <returns>数据实体HomeLinkData</returns>
		private HomeLinkData FillHomeLinkData(String commandText,SqlParameter[] prams)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			HomeLinkData data = new HomeLinkData();
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
				insertCommand = new SqlCommand("sp_InsertHomeLink",new SqlConnection(Tax666Configuration.ConnectionString));
				insertCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(LinkName_Pram, SqlDbType.NVarChar,50));
				sqlParams.Add(new SqlParameter(LinkUrl_Pram, SqlDbType.NVarChar,200));
				sqlParams.Add(new SqlParameter(HomeDesc_Pram, SqlDbType.NVarChar,500));
				sqlParams.Add(new SqlParameter(LogoPath_Pram, SqlDbType.NVarChar,100));
				sqlParams.Add(new SqlParameter(LinkTypeID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(LinkMode_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(Reason_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[LinkName_Pram].SourceColumn		= HomeLinkData.LinkName_Field;
				sqlParams[LinkUrl_Pram].SourceColumn		= HomeLinkData.LinkUrl_Field;
				sqlParams[HomeDesc_Pram].SourceColumn		= HomeLinkData.HomeDesc_Field;
				sqlParams[LogoPath_Pram].SourceColumn		= HomeLinkData.LogoPath_Field;
				sqlParams[LinkTypeID_Pram].SourceColumn		= HomeLinkData.LinkTypeID_Field;
				sqlParams[LinkMode_Pram].SourceColumn		= HomeLinkData.LinkMode_Field;

				sqlParams[Reason_Pram].SourceColumn		    = HomeLinkData.Reason_Field;
				sqlParams[Reason_Pram].Direction		    = ParameterDirection.Output;

			}

			return insertCommand;
		}

		/// <summary>
		/// 向数据库中插入一条新记录。
		/// </summary>
		/// <param name="homeLinkData"></param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertHomeLink(HomeLinkData homeLinkData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}

			dsCommand.InsertCommand = GetInsertCommand();
			dsCommand.Update(homeLinkData, HomeLinkData.HomeLink_Table);

			// 检查表错误以查看更新是否成功；
			if (homeLinkData.HasErrors)
			{
				homeLinkData.Tables[HomeLinkData.HomeLink_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				homeLinkData.AcceptChanges();
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
				updateCommand = new SqlCommand("sp_UpdateHomeLink",new SqlConnection(Tax666Configuration.ConnectionString));
				updateCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(LinkID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(LinkName_Pram, SqlDbType.NVarChar,50));
				sqlParams.Add(new SqlParameter(LinkUrl_Pram, SqlDbType.NVarChar,200));
				sqlParams.Add(new SqlParameter(HomeDesc_Pram, SqlDbType.NVarChar,500));
				sqlParams.Add(new SqlParameter(LogoPath_Pram, SqlDbType.NVarChar,100));
				sqlParams.Add(new SqlParameter(LinkTypeID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(LinkMode_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(Reason_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[LinkID_Pram].SourceColumn		    = HomeLinkData.LinkID_Field;
				sqlParams[LinkName_Pram].SourceColumn		= HomeLinkData.LinkName_Field;
				sqlParams[LinkUrl_Pram].SourceColumn		= HomeLinkData.LinkUrl_Field;
				sqlParams[HomeDesc_Pram].SourceColumn		= HomeLinkData.HomeDesc_Field;
				sqlParams[LogoPath_Pram].SourceColumn		= HomeLinkData.LogoPath_Field;
				sqlParams[LinkTypeID_Pram].SourceColumn		= HomeLinkData.LinkTypeID_Field;
				sqlParams[LinkMode_Pram].SourceColumn		= HomeLinkData.LinkMode_Field;

				sqlParams[Reason_Pram].SourceColumn		    = HomeLinkData.Reason_Field;
                sqlParams[Reason_Pram].Direction            = ParameterDirection.Output;
			}

			return updateCommand;
		}

		/// <summary>
		/// 修改指定ID的友情链接的属性
		/// </summary>
		/// <param name="homeLinkData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool UpdateHomeLink(HomeLinkData homeLinkData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetUpdateCommand();

			dsCommand.Update(homeLinkData, HomeLinkData.HomeLink_Table);

			// 检查表错误以查看更新是否成功；
			if ( homeLinkData.HasErrors )
			{
				homeLinkData.Tables[HomeLinkData.HomeLink_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				homeLinkData.AcceptChanges();
				return true;
			}
		}

		/// <summary>
		/// 为DataAdapter初始化带参数的Update command。
		/// </summary>
		/// <returns></returns>
		private SqlCommand GetModifyCommand()
		{
			if (modifyCommand == null)
			{
				modifyCommand = new SqlCommand("sp_SetHomeLinkInfo",new SqlConnection(Tax666Configuration.ConnectionString));
				modifyCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = modifyCommand.Parameters;

				sqlParams.Add(new SqlParameter(OptionType_Pram, SqlDbType.NVarChar,20));
				sqlParams.Add(new SqlParameter(LinkID_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[OptionType_Pram].SourceColumn		= HomeLinkData.OptionType_Field;
				sqlParams[LinkID_Pram].SourceColumn		    = HomeLinkData.LinkID_Field;
			}

			return modifyCommand;
		}

		/// <summary>
		/// OptionType：VALID-有效性；AUDIT-审核；UP-上移；DOWN-下移
		/// </summary>
		/// <param name="homeLinkData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetHomeLinkInfo(HomeLinkData homeLinkData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetModifyCommand();

			dsCommand.Update(homeLinkData, HomeLinkData.HomeLink_Table);

			// 检查表错误以查看更新是否成功；
			if ( homeLinkData.HasErrors )
			{
				homeLinkData.Tables[HomeLinkData.HomeLink_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				homeLinkData.AcceptChanges();
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
				deleteCommand = new SqlCommand("sp_DelHomeLink",new SqlConnection(Tax666Configuration.ConnectionString));
				deleteCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(LinkID_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[LinkID_Pram].SourceColumn		= HomeLinkData.LinkID_Field;
			}

			return deleteCommand;
		}

		/// <summary>
		/// 删除友情链接
		/// </summary>
		/// <param name="homeLinkData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelHomeLink(HomeLinkData homeLinkData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetDeleteCommand();

			dsCommand.Update(homeLinkData, HomeLinkData.HomeLink_Table);

			// 检查表错误以查看更新是否成功；
			if ( homeLinkData.HasErrors )
			{
				homeLinkData.Tables[HomeLinkData.HomeLink_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				homeLinkData.AcceptChanges();
				return true;
			}
		}

		/// <summary>
		/// 获取所有链接记录列表
		/// </summary>
		/// <param name="agentID"></param>
		/// <param name="linkTypeID"></param>
		/// <param name="isAudit"></param>
		/// <param name="isAvailable"></param>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="doCount"></param>
		/// <returns>数据实体HomeLinkData</returns>
		public HomeLinkData GetHomeLinkAll(int linkTypeID,int isAudit,int isAvailable,int pageSize,int pageIndex,bool doCount)
		{
			SqlParameter[] prams =	{
										new SqlParameter("@LinkTypeID",SqlDbType.Int,4),
										new SqlParameter("@IsAudit",SqlDbType.Int,4),
										new SqlParameter("@IsAvailable",SqlDbType.Int,4),
										new SqlParameter("@PageSize",SqlDbType.Int,4),
										new SqlParameter("@PageIndex",SqlDbType.Int,4),
										new SqlParameter("@DoCount",SqlDbType.Bit,1)
									};

			prams[0].Value = linkTypeID;
			prams[1].Value = isAudit;
			prams[2].Value = isAvailable;
			prams[3].Value = pageSize;
			prams[4].Value = pageIndex;
			prams[5].Value = doCount;

			return FillHomeLinkData("sp_GetHomeLinkAll",prams);
		}

		/// <summary>
		/// ListType：1-ID；2-文字；3-LOGO；4-文字和Logo
		/// </summary>
		/// <param name="listType"></param>
		/// <param name="agentID"></param>
		/// <param name="linkID"></param>
		/// <returns>数据实体HomeLinkData</returns>
		public HomeLinkData GetHomeLinkByListType(int listType,int linkTypeID,int linkID)
		{
			SqlParameter[] prams =	{
										new SqlParameter("@ListType",SqlDbType.Int,4),
										new SqlParameter("@LinkTypeID",SqlDbType.Int,4),
										new SqlParameter("@LinkID",SqlDbType.Int,4)
									};

			prams[0].Value = listType;
            prams[1].Value = linkTypeID;
			prams[2].Value = linkID;

			return FillHomeLinkData("sp_GetHomeLinkByListType",prams);
		}

	}
}
