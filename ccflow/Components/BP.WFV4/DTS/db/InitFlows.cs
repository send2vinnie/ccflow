using System;
using BP.DTS;
using BP.DA;


namespace BP.WF.DTS
{


	/// <summary>
	/// 流程统计
	/// </summary>
	public class InitFlows:DataIOEn2
	{
		/// <summary>
		/// 流程统计
		/// </summary>
		public InitFlows()
		{
			this.HisDoType=DoType.UnName;
			this.Title="流程汇总(用于流程统计分析)";
			this.Note="用于流程的统计分析";
			this.HisRunTimeType=RunTimeType.UnName;
			this.HisUserType = BP.Web.UserType.SysAdmin ; 
			this.FromDBUrl=DBUrlType.AppCenterDSN;
			this.ToDBUrl=DBUrlType.AppCenterDSN;
		}
		/// <summary>
		/// 流程统计
		/// </summary>
		public override void Do()
		{
			WFDTS.InitFlows(DataType.CurrentYear+"-01-01 00:00");
		 
		}
	}	 
}
