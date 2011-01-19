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
	/// 该类包含Regions系统的业务表现。
	/// <remarks>
	///  业务表现用于提供业务规则子系统的简单接口。
	///  该类被标记为MarshalByRefObject以支持远程处理。
	/// </remarks>
	/// </summary>
	public class RegionsFacade : MarshalByRefObject
	{
		public RegionsFacade()
		{
			//
			// 构造函数部分。
			//
		}

		/// <summary>
		/// 获取省市地县的联动菜单
		/// </summary>
		/// <param name="depth"></param>
		/// <param name="regionId"></param>
		/// <returns>数据实体RegionsData</returns>
		public RegionsData GetRegionsList(int depth,int regionId)
		{
			using (RegionsDataAccess regionsDataAccess = new RegionsDataAccess())
			{
				return regionsDataAccess.GetRegionsList(depth,regionId);
			}
		}

	}
}
