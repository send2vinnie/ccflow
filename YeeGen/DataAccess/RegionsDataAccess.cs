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
	public class RegionsDataAccess : IDisposable
	{
		private SqlDataAdapter dsCommand;

		//定义存储过程所用到的参数；
		public const String RegionId_Pram			= "@RegionId";
		public const String AreaId_Pram			    = "@AreaId";
		public const String ParentId_Pram			= "@ParentId";
		public const String RegionName_Pram			= "@RegionName";
		public const String DisplaySequence_Pram	= "@DisplaySequence";
		public const String Path_Pram			    = "@Path";
		public const String Depth_Pram			    = "@Depth";


		public RegionsDataAccess()
		{
			/// <summary>
			///  RegionsDataAccess 构造函数，创建DataSet Command。
			///   <remarks>初始化DataSet Command对象。</remarks>
			/// </summary>
			dsCommand = new SqlDataAdapter();

			dsCommand.SelectCommand = new SqlCommand();
			dsCommand.SelectCommand.Connection = new SqlConnection(Tax666Configuration.ConnectionString);

			dsCommand.TableMappings.Add("Table",RegionsData.Regions_Table);
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
		/// <returns>数据实体RegionsData</returns>
		private RegionsData FillRegionsData(String commandText, String paramName, String paramValue)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			RegionsData data = new RegionsData();
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
		/// <returns>数据实体RegionsData</returns>
		private RegionsData FillRegionsData(String commandText)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			RegionsData data = new RegionsData();
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
		/// <returns>数据实体RegionsData</returns>
		private RegionsData FillRegionsData(String commandText,SqlParameter[] prams)
		{
			if (dsCommand == null)
			{
				throw new System.ObjectDisposedException(GetType().FullName);
			}
			RegionsData data = new RegionsData();
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
		/// 获取省市地县的联动菜单
		/// </summary>
		/// <param name="depth"></param>
		/// <param name="regionId"></param>
		/// <returns>数据实体RegionsData</returns>
		public RegionsData GetRegionsList(int depth,int regionId)
		{
			SqlParameter[] prams =	{
										new SqlParameter("@Depth",SqlDbType.Int,4),
										new SqlParameter("@RegionId",SqlDbType.Int,4)
									};

			prams[0].Value = depth;
			prams[1].Value = regionId;

			return FillRegionsData("sp_GetRegionsList",prams);
		}

	}
}
