using System;
using System.Web;
using System.Data;
using BP.En;
using BP.DA;
using System.Configuration;
using BP.Port;

namespace BP.Web
{
	/// <summary>
	/// User 的摘要说明。
	/// </summary>
	public class GTSUser:WebUser
	{
         
		/// <summary>
		/// 考试结束时间
		/// </summary>
		public static string EndExamTime
		{
			get
			{				
				return GetSessionByKey("EndExamTime", DataType.CurrentDataTime);
			}
			set
			{
				SetSessionByKey("EndExamTime",value);
			}
		}
		/// <summary>
		/// 当前考试的
		/// </summary>
		public static string FK_Paper
		{
			get
			{				
				return GetSessionByKey("FK_Paper", "-1");
			}
			set
			{
				SetSessionByKey("FK_Paper",value);
			}
		}
        public static string MyFileExt
        {
            get
            {
                return GetSessionByKey("MyFileExt", "jpg");
            }
            set
            {
                SetSessionByKey("MyFileExt", value);
            }
        }
		/// <summary>
		/// 是否超出了考试的时间。
		/// </summary>
        public static int TimeLeft
        {
            get
            {
                return DataType.GetSpanMinute((string)GetSessionByKey("EndExamTime", DataType.CurrentDataTime));
            }
        }
        public static bool IsInTestTime
        {
            get
            {
                DateTime dt = DateTime.Now;
                string dtStr = dt.ToString("yyyy-MM-dd");
                string sql = "SELECT No FROM GTS_PaperFix WHERE   (ValidTimeFrom LIKE '" + dtStr + "%') ";

                DataTable table = DBAccess.RunSQLReturnTable(sql);
                if (table.Rows.Count == 0)
                    return false;

                BP.GTS.PaperFixs pfs = new BP.GTS.PaperFixs();
                pfs.RetrieveInSQL("No", sql);


                string msg = "如下考试在进行或者将要进行：<hr>";
                foreach (BP.GTS.PaperFix pf in pfs)
                {
                    msg += "<br>" + pf.Name + " 有效时间从：" + pf.ValidTimeFrom + " 到 " + pf.ValidTimeTo;
                }
                msg += "<hr>当前时间：" + DataType.CurrentDataTimess + "  现在<a href='/GTS/App/Paper/Link.aspx?IsExam=1'>进入考试任务</a>。";

                throw new Exception(msg);
            }
        }
	}
}
