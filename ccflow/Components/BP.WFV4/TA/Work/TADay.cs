using System;
using BP.DA;
using BP.En;
using BP.Web;


namespace BP.TA
{
	/// <summary>
	/// TADay 的摘要说明。
	/// </summary>
	public class TADay
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public TADay(string data)
		{
			this.MyDateTime=DataType.ParseSysDate2DateTime(data);

			this.MyWorks= new Works(WebUser.No,this.MyDateTime.ToString("yyyy-MM-dd") );
			this.MyReWorks= new ReWorks(WebUser.No,this.MyDateTime.ToString("yyyy-MM-dd") );
			this.MyReturnWorks= new ReturnWorks(WebUser.No,this.MyDateTime.ToString("yyyy-MM-dd") );
			this.MyTaLogs= new TaLogs(WebUser.No,this.MyDateTime.ToString("yyyy-MM-dd") );
			this.MyTasks= new Tasks(WebUser.No,this.MyDateTime.ToString("yyyy-MM-dd") );
		}
		public DateTime MyDateTime;
		public BP.TA.Works MyWorks=null;
		public ReWorks MyReWorks=null;
		public ReturnWorks MyReturnWorks=null;
		public TaLogs MyTaLogs=null;
		public Tasks MyTasks=null;

		public string Week
		{
			get
			{
				return DataType.GetWeek((int)this.MyDateTime.DayOfWeek);
			}
		}
	}
}
