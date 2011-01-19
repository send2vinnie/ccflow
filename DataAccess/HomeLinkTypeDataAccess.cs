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
	public class HomeLinkTypeDataAccess : IDisposable
	{
		private SqlDataAdapter dsCommand;

		//定义存储过程所用到的参数；
		public const String LinkTypeID_Pram			= "@LinkTypeID";
		public const String TypeName_Pram			= "@TypeName";
		public const String TypeDesc_Pram			= "@TypeDesc";
		public const String IsAvailable_Pram		= "@IsAvailable";
		public const String OptionType_Pram			= "@OptionType";
		public const String ListType_Pram			= "@ListType";
		public const String Reason_Pram			    = "@Reason";

		private SqlCommand deleteCommand;
		private SqlCommand insertCommand;
		private SqlCommand updateCommand;

		public HomeLinkTypeDataAccess()
		{
			/// <summary>
			///  HomeLinkTypeDataAccess 构造函数，创建DataSet Command。
			///   <remarks>初始化DataSet Command对象。</remarks>
			/// </summary>
			dsCommand = new SqlDataAdapter();

			dsCommand.SelectCommand = new SqlCommand();
			dsCommand.SelectCommand.Connection = new SqlConnection(Tax666Configuration.ConnectionString);

			dsCommand.TableMappings.Add("Table",HomeLinkTypeData.HomeLinkType_Table);
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
		/// <returns>数据实体HomeLinkTypeData</returns>
		private HomeLinkTypeData FillHomeLinkTypeData(String commandText, String paramName, String paramValue)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			HomeLinkTypeData data = new HomeLinkTypeData();
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
		/// <returns>数据实体HomeLinkTypeData</returns>
		private HomeLinkTypeData FillHomeLinkTypeData(String commandText)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			HomeLinkTypeData data = new HomeLinkTypeData();
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
		/// <returns>数据实体HomeLinkTypeData</returns>
		private HomeLinkTypeData FillHomeLinkTypeData(String commandText,SqlParameter[] prams)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			HomeLinkTypeData data = new HomeLinkTypeData();
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
				insertCommand = new SqlCommand("sp_InsertHomeLinkType",new SqlConnection(Tax666Configuration.ConnectionString));
				insertCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(TypeName_Pram, SqlDbType.NVarChar,50));
				sqlParams.Add(new SqlParameter(TypeDesc_Pram, SqlDbType.NVarChar,200));
				sqlParams.Add(new SqlParameter(IsAvailable_Pram, SqlDbType.Bit));
				sqlParams.Add(new SqlParameter(Reason_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[TypeName_Pram].SourceColumn		= HomeLinkTypeData.TypeName_Field;
				sqlParams[TypeDesc_Pram].SourceColumn		= HomeLinkTypeData.TypeDesc_Field;
				sqlParams[IsAvailable_Pram].SourceColumn	= HomeLinkTypeData.IsAvailable_Field;

				sqlParams[Reason_Pram].SourceColumn		    = HomeLinkTypeData.Reason_Field;
				sqlParams[Reason_Pram].Direction		    = ParameterDirection.Output;

			}

			return insertCommand;
		}

		/// <summary>
		/// 向数据库中插入一条新记录。
		/// </summary>
		/// <param name="homeLinkTypeData"></param>
		/// <returns>添加成功：true；失败：false。</returns>
		public bool InsertHomeLinkType(HomeLinkTypeData homeLinkTypeData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}

			dsCommand.InsertCommand = GetInsertCommand();
			dsCommand.Update(homeLinkTypeData, HomeLinkTypeData.HomeLinkType_Table);

			// 检查表错误以查看更新是否成功；
			if (homeLinkTypeData.HasErrors)
			{
				homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				homeLinkTypeData.AcceptChanges();
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
				updateCommand = new SqlCommand("sp_UpdateHomeLinkType",new SqlConnection(Tax666Configuration.ConnectionString));
				updateCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(LinkTypeID_Pram, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(TypeName_Pram, SqlDbType.NVarChar,50));
				sqlParams.Add(new SqlParameter(TypeDesc_Pram, SqlDbType.NVarChar,200));
				sqlParams.Add(new SqlParameter(IsAvailable_Pram, SqlDbType.Bit));
				sqlParams.Add(new SqlParameter(Reason_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[LinkTypeID_Pram].SourceColumn		= HomeLinkTypeData.LinkTypeID_Field;
				sqlParams[TypeName_Pram].SourceColumn		= HomeLinkTypeData.TypeName_Field;
				sqlParams[TypeDesc_Pram].SourceColumn		= HomeLinkTypeData.TypeDesc_Field;
				sqlParams[IsAvailable_Pram].SourceColumn	= HomeLinkTypeData.IsAvailable_Field;

				sqlParams[Reason_Pram].SourceColumn		    = HomeLinkTypeData.Reason_Field;
                sqlParams[Reason_Pram].Direction            = ParameterDirection.Output;
			}

			return updateCommand;
		}

		/// <summary>
		/// 修改指定记录
		/// </summary>
		/// <param name="homeLinkTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool UpdateHomeLinkType(HomeLinkTypeData homeLinkTypeData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetUpdateCommand();

			dsCommand.Update(homeLinkTypeData, HomeLinkTypeData.HomeLinkType_Table);

			// 检查表错误以查看更新是否成功；
			if ( homeLinkTypeData.HasErrors )
			{
				homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				homeLinkTypeData.AcceptChanges();
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
				deleteCommand = new SqlCommand("sp_DelHomeLinkType",new SqlConnection(Tax666Configuration.ConnectionString));
				deleteCommand.CommandType = CommandType.StoredProcedure;

				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(LinkTypeID_Pram, SqlDbType.Int));

				// 定义数据集中表的参数对应；
				sqlParams[LinkTypeID_Pram].SourceColumn		= HomeLinkTypeData.LinkTypeID_Field;
			}

			return deleteCommand;
		}

		/// <summary>
		/// 删除友情链接类型
		/// </summary>
		/// <param name="homeLinkTypeData"></param>
		/// <returns>成功返回true；失败返回false。</returns>
		public bool DelHomeLinkType(HomeLinkTypeData homeLinkTypeData)
		{
			if ( dsCommand == null )
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			}
			//
			// 得到command并更新数据库；
			//
			dsCommand.UpdateCommand = GetDeleteCommand();

			dsCommand.Update(homeLinkTypeData, HomeLinkTypeData.HomeLinkType_Table);

			// 检查表错误以查看更新是否成功；
			if ( homeLinkTypeData.HasErrors )
			{
				homeLinkTypeData.Tables[HomeLinkTypeData.HomeLinkType_Table].GetErrors()[0].ClearErrors();
				return false;
			}
			else
			{
				homeLinkTypeData.AcceptChanges();
				return true;
			}
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
			SqlParameter[] prams =	{
										new SqlParameter("@ListType",SqlDbType.Int,4),
										new SqlParameter("@LinkTypeID",SqlDbType.Int,4),
										new SqlParameter("@IsAvailable",SqlDbType.Int,4)
									};

			prams[0].Value = listType;
			prams[1].Value = linkTypeID;
			prams[2].Value = isAvailable;

			return FillHomeLinkTypeData("sp_GetHomeLinkTypeByListType",prams);
		}

	}
}
