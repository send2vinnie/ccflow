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
	public class InfoBulletinDataAccess : IDisposable
	{
		private SqlDataAdapter dsCommand;

		//定义存储过程所用到的参数；
		public const String BulletinID_Pram			= "@BulletinID";
		public const String BulletinTitle_Pram		= "@BulletinTitle";
		public const String BulletinDesc_Pram		= "@BulletinDesc";
		public const String AdminID_Pram			= "@AdminID";
		public const String StartTime_Pram			= "@StartTime";
		public const String EndTime_Pram			= "@EndTime";
		public const String SortOrder_Pram			= "@SortOrder";
		public const String IsAudit_Pram			= "@IsAudit";
		public const String IsAvailable_Pram		= "@IsAvailable";
		public const String OptionType_Pram			= "@OptionType";
		public const String Reason_Pram			    = "@Reason";

		private SqlCommand modifyCommand;
		private SqlCommand deleteCommand;
		private SqlCommand updateCommand;
		private SqlCommand insertCommand;

		public InfoBulletinDataAccess()
		{
			/// <summary>
			///  InfoBulletinDataAccess 构造函数，创建DataSet Command。
			///   <remarks>初始化DataSet Command对象。</remarks>
			/// </summary>
			dsCommand = new SqlDataAdapter();

			dsCommand.SelectCommand = new SqlCommand();
			dsCommand.SelectCommand.Connection = new SqlConnection(Tax666Configuration.ConnectionString);

			dsCommand.TableMappings.Add("Table",InfoBulletinData.InfoBulletin_Table);
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
		/// <returns>数据实体InfoBulletinData</returns>
		private InfoBulletinData FillInfoBulletinData(String commandText, String paramName, String paramValue)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			InfoBulletinData data = new InfoBulletinData();
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
		/// <returns>数据实体InfoBulletinData</returns>
		private InfoBulletinData FillInfoBulletinData(String commandText)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			InfoBulletinData data = new InfoBulletinData();
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
		/// <returns>数据实体InfoBulletinData</returns>
		private InfoBulletinData FillInfoBulletinData(String commandText,SqlParameter[] prams)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			InfoBulletinData data = new InfoBulletinData();
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
				insertCommand = new SqlCommand("sp_InfoBulletinInsert",new SqlConnection(Tax666Configuration.ConnectionString));
				insertCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(BulletinTitle_Pram, SqlDbType.NVarChar,100));
				sqlParams.Add(new SqlParameter(BulletinDesc_Pram, SqlDbType.NText));
				sqlParams.Add(new SqlParameter(AdminID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(StartTime_Pram, SqlDbType.DateTime));
				sqlParams.Add(new SqlParameter(EndTime_Pram, SqlDbType.DateTime));
				sqlParams.Add(new SqlParameter(IsAudit_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(Reason_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[BulletinTitle_Pram].SourceColumn	= InfoBulletinData.BulletinTitle_Field;
				sqlParams[BulletinDesc_Pram].SourceColumn	= InfoBulletinData.BulletinDesc_Field;
				sqlParams[AdminID_Pram].SourceColumn		= InfoBulletinData.AdminID_Field;
				sqlParams[StartTime_Pram].SourceColumn		= InfoBulletinData.StartTime_Field;
				sqlParams[EndTime_Pram].SourceColumn		= InfoBulletinData.EndTime_Field;
				sqlParams[IsAudit_Pram].SourceColumn		= InfoBulletinData.IsAudit_Field;

                sqlParams[Reason_Pram].SourceColumn		    = InfoBulletinData.Reason_Field;
				sqlParams[Reason_Pram].Direction		    = ParameterDirection.Output;

			}

			return insertCommand;
		}

		/// <summary>
		/// 向数据库中插入一条新记录。
		/// </summary>
		/// <param name="infoBulletinData"></param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertInfoBulletin(InfoBulletinData infoBulletinData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}

			dsCommand.InsertCommand = GetInsertCommand();
			dsCommand.Update(infoBulletinData, InfoBulletinData.InfoBulletin_Table);

			// 检查表错误以查看更新是否成功；
			if (infoBulletinData.HasErrors)
			{
				infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				infoBulletinData.AcceptChanges();
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
				updateCommand = new SqlCommand("sp_InfoBulletinUpdate",new SqlConnection(Tax666Configuration.ConnectionString));
				updateCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(BulletinID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(BulletinTitle_Pram, SqlDbType.NVarChar,100));
				sqlParams.Add(new SqlParameter(BulletinDesc_Pram, SqlDbType.NText));
				sqlParams.Add(new SqlParameter(AdminID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(StartTime_Pram, SqlDbType.DateTime));
				sqlParams.Add(new SqlParameter(EndTime_Pram, SqlDbType.DateTime));
				sqlParams.Add(new SqlParameter(IsAudit_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(Reason_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[BulletinID_Pram].SourceColumn		= InfoBulletinData.BulletinID_Field;
				sqlParams[BulletinTitle_Pram].SourceColumn	= InfoBulletinData.BulletinTitle_Field;
				sqlParams[BulletinDesc_Pram].SourceColumn	= InfoBulletinData.BulletinDesc_Field;
				sqlParams[AdminID_Pram].SourceColumn		= InfoBulletinData.AdminID_Field;
				sqlParams[StartTime_Pram].SourceColumn		= InfoBulletinData.StartTime_Field;
				sqlParams[EndTime_Pram].SourceColumn		= InfoBulletinData.EndTime_Field;
				sqlParams[IsAudit_Pram].SourceColumn		= InfoBulletinData.IsAudit_Field;
                sqlParams[Reason_Pram].SourceColumn         = InfoBulletinData.Reason_Field;
                sqlParams[Reason_Pram].Direction            = ParameterDirection.Output;
			}

			return updateCommand;
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="infoBulletinData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool UpdateInfoBulletin(InfoBulletinData infoBulletinData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetUpdateCommand();

			dsCommand.Update(infoBulletinData, InfoBulletinData.InfoBulletin_Table);

			// 检查表错误以查看更新是否成功；
			if ( infoBulletinData.HasErrors )
			{
				infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				infoBulletinData.AcceptChanges();
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
				modifyCommand = new SqlCommand("sp_InfoBulletinSet",new SqlConnection(Tax666Configuration.ConnectionString));
				modifyCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = modifyCommand.Parameters;

				sqlParams.Add(new SqlParameter(OptionType_Pram, SqlDbType.NVarChar,10));
				sqlParams.Add(new SqlParameter(BulletinID_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[OptionType_Pram].SourceColumn		= InfoBulletinData.OptionType_Field;
				sqlParams[BulletinID_Pram].SourceColumn		= InfoBulletinData.BulletinID_Field;
			}

			return modifyCommand;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="infoBulletinData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool SetInfoBulletin(InfoBulletinData infoBulletinData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetModifyCommand();

			dsCommand.Update(infoBulletinData, InfoBulletinData.InfoBulletin_Table);

			// 检查表错误以查看更新是否成功；
			if ( infoBulletinData.HasErrors )
			{
				infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				infoBulletinData.AcceptChanges();
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
				deleteCommand = new SqlCommand("sp_InfoBulletinDel",new SqlConnection(Tax666Configuration.ConnectionString));
				deleteCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(BulletinID_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[BulletinID_Pram].SourceColumn		= InfoBulletinData.BulletinID_Field;
			}

			return deleteCommand;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="infoBulletinData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelInfoBulletin(InfoBulletinData infoBulletinData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetDeleteCommand();

			dsCommand.Update(infoBulletinData, InfoBulletinData.InfoBulletin_Table);

			// 检查表错误以查看更新是否成功；
			if ( infoBulletinData.HasErrors )
			{
				infoBulletinData.Tables[InfoBulletinData.InfoBulletin_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				infoBulletinData.AcceptChanges();
				return true;
			}
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
			SqlParameter[] prams =	{
										new SqlParameter("@Where",SqlDbType.NVarChar,4000),
										new SqlParameter("@PageSize",SqlDbType.Int,4),
										new SqlParameter("@PageIndex",SqlDbType.Int,4),
										new SqlParameter("@DoCount",SqlDbType.Bit,1)
									};

			prams[0].Value = where;
			prams[1].Value = pageSize;
			prams[2].Value = pageIndex;
			prams[3].Value = doCount;

			return FillInfoBulletinData("sp_InfoBulletinGet",prams);
		}

	}
}
