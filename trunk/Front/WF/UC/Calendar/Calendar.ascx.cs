//===========================================================================
// 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分修改的。
// 类名已更改，且类已修改为从文件“App_Code\Migrated\ta\Stub_calendar_ascx_cs.cs”的抽象基类 
// 继承。
// 在运行时，此项允许您的 Web 应用程序中的其他类使用该抽象基类绑定和访问 
// 代码隐藏页。
// 关联的内容页“ta\calendar.ascx”也已修改，以引用新的类名。
// 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
namespace BP.Web.Comm.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BP.TA;
	using BP.En;
	using BP.DA;
	using BP.Web;
	using BP.Web.Controls;
	using BP.Web.Comm;
	/// <summary>
	///		Calendar 的摘要说明。
	/// </summary>
	public partial class Calendar : BP.Web.UC.UCBase
	{
		private void ViewToDayAddStr(DateTime dt, string html)
		{
			this.Add("<TR>");
			this.Add("<TD  class='TimeList' valign=top ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+dt.ToString("yyyy-MM-dd")+"&RefTime="+dt.ToString("hh_mm")+"')\"  >&nbsp;&nbsp;&nbsp;&nbsp;"+dt.ToString(":mm")+"</TD>");
			if (dt.Hour >=8 && dt.Hour<=18)
			{
				this.Add("<TD  valign=top class='TimeActive'    >"+html+"</TD>");
			}
			else
			{
				this.Add("<TD  valign=top class='TimeNoActive'  >"+html+"</TD>");
			}
			this.AddTREnd();
		}
		/// <summary>
		/// 查看一天的事件
		/// </summary>
		/// <param name="date">要查看的天</param>
//		public void MyDay(DateTime dt )
		 public void MyDay(DateTime dt )
		{
			string date=dt.ToString( "yyyy-MM-dd" );
			this.Controls.Clear();
			//this.ViewState.Clear();
			this.Controls.Clear();
			this.Attributes["Height"]="100%";
			int time=7;
			this.Add("<TABLE border=1 width='100%' >");
			this.Add("<TR><TD width=70%  valign=top  >");
			this.Add("<Table border=1   width='100%'  style='border-collapse: collapse' bordercolor='#111111' valign=top bgcolor=InfoBackground >");

			Works wks = new Works(WebUser.No, date);
			WorkDtls dtls = new WorkDtls(WebUser.No, date);
			ReWorks rewks = new ReWorks(WebUser.No, date);

			ReturnWorks rtwks = new ReturnWorks(WebUser.No, date);
			TaLogs tls = new TaLogs(WebUser.No, date);
			Tasks tks = new Tasks(WebUser.No, date);
			
			string cdID=this.Page.FindControl("WebMenu1").ClientID;
			string timeStr="";
			for(int h=6;h<=24;h++)
			{
				timeStr= h.ToString();
				timeStr=timeStr.PadLeft(2,'0');
				string timebarcss="TimeNoActive";

				if (h>=8 && h<=18)
				{
					timebarcss="TimeActive";
				}

				/*如果没有要加入的事情。*/				
				this.Add("<TR>");
				this.Add("<TD nowrap=true width='1%' onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+timeStr+":00' );\" class='TimeList' valign=top ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+timeStr+"_00' , 'log' )\" >&nbsp;&nbsp;"+timeStr+":00</TD>");
				this.Add("<TD onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+timeStr+":00' );\" class='"+timebarcss+"' ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+timeStr+"_00' , 'log' )\" >&nbsp;</TD>");
				this.AddTREnd();

				this.Add("<TR>");
				this.Add("<TD onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+timeStr+":30' );\" class='TimeList' valign=top ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+timeStr+"_30' , 'log' )\" >&nbsp;&nbsp;&nbsp;&nbsp;:30</TD>");
				this.Add("<TD onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+timeStr+":30' );\" class='"+timebarcss+"' ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+timeStr+"_30' , 'log' )\" >&nbsp;</TD>");
				this.AddTREnd();

				// 加入我的工作。
				bool isHave=false;
				foreach(Work tn in wks)
				{
					/* 如果我是任务的下达人 */
					if (tn.DTOfStart.IndexOf(timeStr+":")==-1 )
						continue;
					isHave=true;
					this.ViewToDayAddStr( tn.DTOfStart_S ,  tn.WorkStateOpStr);
				}

				// 加入我的回复与回复给我的信息。
				foreach(ReWork tn in rewks)
				{
					if (WebUser.No==tn.Executer )
					{
						/*如果我是任务的回复人 */
						if (tn.DTOfActive.IndexOf(timeStr+":")==-1)
							continue;

						isHave=true;
						//this.ViewToDayAddStr( tn.DTOfActive_S , tn.MyReWorkStateOpStr );
						this.ViewToDayAddStr( tn.DTOfActive_S , "" );

					}
					else if(WebUser.No==tn.Sender )
					{
						if (tn.DTOfActive.IndexOf(timeStr+":")==-1)
							continue;

						/* 如果我是任务的发送人 */
						isHave=true;
						//this.ViewToDayAddStr( tn.DTOfActive_S ,   tn.MyReWorkStateOpStr  );
						this.ViewToDayAddStr( tn.DTOfActive_S ,   ""  );

					}
				}

				// 加入我的退回与退回给我的信息。
				foreach(ReturnWork tn in rtwks)
				{
					if (WebUser.No==tn.Executer )
					{
						/*如果我是任务的回复人 */
						if (tn.DTOfActive.IndexOf(timeStr+":")==-1)
							continue;

						isHave=true;
						//this.ViewToDayAddStr( tn.DTOfActive_S , tn.MyReturnNodeStateOpStr);
						this.ViewToDayAddStr( tn.DTOfActive_S , "");

					}
					else if(WebUser.No==tn.Sender )
					{
						/* 如果我是任务的下达人。 */
						if (tn.DTOfActive.IndexOf(timeStr+":")==-1)
							continue;

						isHave=true;
						//this.ViewToDayAddStr( tn.DTOfActive_S, tn.MyReturnNodeStateOpStr );
						this.ViewToDayAddStr( tn.DTOfActive_S, "" );

					}
				}

				// 加入我的日志, 放在最后边。
				foreach(TaLog tn in tls)
				{
					if (tn.LogTime.IndexOf(timeStr+":")==-1)
						continue;
					isHave=true;
					this.ViewToDayAddStr( tn.LogDatetime , tn.MyLogOpStr );
				}

				// 加入我的计划, 放在最后边。
				foreach(Task tn in tks )
				{
					if (tn.InfactEndDate.IndexOf(timeStr+":")==-1)
						continue;

					isHave=true;
					this.ViewToDayAddStr( tn.InfactEndDate_S , tn.NameExt );
				}
			}
			this.Add("</TABLE>");  // end gener day .
			this.Add("</TD>");
			this.Add("<TD width=30% >"); //begin gener

			this.Add( this.DataPanelMonth_Small(dt)) ; // 增加小月份

			/*
			// 增加计划模块
			this.Add("<TABLE   style='border-collapse: collapse' bordercolor='#111111' bgcolor=InfoBackground >");
			this.Add("<TR bgcolor=ActiveBorder >");
			this.Add("<TD>");
			//			CheckBox cb = new CheckBox();
			//			cb.ID="CB_Task";
			//			this.Add(cb);
			this.Add("</TD>");

			this.Add("<TD width=100% >");
			TB tb = new TB();
			tb.Text="单击此处增加新任务";
			tb.ID="TB_Task";
			tb.ClassName="TBTask";
			tb.Attributes["style"]="WIDTH: 100%;";
			tb.AutoPostBack=true;
			tb.TextChanged+=new EventHandler(tb_TextChanged);
			this.Add(tb);
			this.Add("</TD>");


			//			this.Add("<TD>");
			//			Btn btn = new Btn();
			//			btn.Text="保存";
			//			btn.ID="Btn_Task";
			//			this.Add(btn);
			//			this.Add("</TD>");
			this.AddTREnd();

			Tasks pls = new Tasks(WebUser.No);
			foreach(Task pl in pls)
			{
				this.Add("<TR>");
				this.Add("<TD>");
				CheckBox cb = new CheckBox();
				cb.ID="CB_Task_"+pl.OID;
				cb.AutoPostBack=true;
				cb.CheckedChanged+=new EventHandler(cb_CheckedChanged);
				if (pl.HisTaskSta==TaskStatus.Complete)
					cb.Checked=true;

				this.Add(cb);
				this.Add("</TD>");

				this.Add("<TD>");
				tb = new TB();
				tb.Text=pl.Name;
				tb.ID="TB_Task_"+pl.OID;
				
				tb.Attributes["style"]="";
				tb.AutoPostBack=true;
				tb.TextChanged+=new EventHandler(tb_TextChanged);

				if (pl.HisTaskSta==TaskStatus.Complete)
					tb.Attributes["style"]+="WIDTH: 100%;text-decoration: line-through;border: 1px solid #FFFFFF";
				else
					tb.Attributes["style"]="WIDTH: 100%;border: 1px solid #FFFFFF";

				this.Add(tb);
				this.Add("</TD>");
				this.AddTREnd();
			}
			
			this.Add("</TABLE>");
			*/
			 
			this.Add("</TD><TR>");			  
			this.Add("</TABLE>");
		}


//		public void MyWeek(DateTime dt)
		 public void MyWeek(DateTime dt)
		{
			this.Controls.Clear();

			System.DayOfWeek dw=  dt.DayOfWeek;
			switch(dw)
			{
				case System.DayOfWeek.Sunday:
					break;
				case System.DayOfWeek.Monday:
					dt=dt.AddDays(-1);
					break;
				case System.DayOfWeek.Tuesday:
					dt=dt.AddDays(-2);
					break;
				case System.DayOfWeek.Wednesday:
					dt=dt.AddDays(-3);
					break;
				case System.DayOfWeek.Thursday:
					dt=dt.AddDays(-4);
					break;
				case System.DayOfWeek.Friday:
					dt=dt.AddDays(-5);
					break;
				case System.DayOfWeek.Saturday:
					dt=dt.AddDays(-6);
					break;
			}

			//string newStr= "新建:<a href=\"javascript:OpenWork('"+date+"')\" ><img src='./Images/MyWork.gif' border=0 />工作</a><a href=\"javascript:WinOpen( 'CycleEvent.aspx?RefDate="+date+"')\" ><img src='./images/Event.gif' border=0 />定期事件</a><a href='MyMonth.aspx?RefDate="+date+"' ><img src='./images/Calendar.gif' border=0 />日历面板</a><a href='./Comm/UIEns.aspx?ClassName=BP.TA.Tasks' ><img src='./images/Task.gif' border=0 />我的任务</a>" ;


			this.Controls.Clear();
			this.Attributes["Height"]="100%";
			int time=7;
			this.Add("<TABLE  class='WeekTable' border=0 >");
			this.Add("<TR  class='CTitle'  ><TD width=1% >时间</TD><TD ondblclick=\"CelldblClick('"+dt.ToString("yyyy-MM-dd")+"','0');\" >"+dt.ToString("M月d日")+"&nbsp;周日</TD><TD ondblclick=\"CelldblClick('"+dt.AddDays(1).ToString("yyyy-MM-dd")+"','0');\" >"+dt.AddDays(1).ToString("M月d日")+"&nbsp;周一</TD><TD ondblclick=\"CelldblClick('"+dt.AddDays(2).ToString("yyyy-MM-dd")+"','0');\"  >"+dt.AddDays(2).ToString("M月d日")+"&nbsp;周二</TD><TD ondblclick=\"window.location.href='Calendar.aspx?CalendarType=1&RefDate="+dt.AddDays(3).ToString("yyyy-MM-dd")+"';\" >"+dt.AddDays(3).ToString("M月d日")+"&nbsp;周三</TD><TD  ondblclick=\"window.location.href='Calendar.aspx?CalendarType=0&RefDate="+dt.AddDays(4).ToString("yyyy-MM-dd")+"';\">"+dt.AddDays(4).ToString("M月d日")+"&nbsp;周四</TD><TD ondblclick=\"window.location.href='Calendar.aspx?CalendarType=0&RefDate="+dt.AddDays(5).ToString("yyyy-MM-dd")+"';\">"+dt.AddDays(5).ToString("M月d日")+"&nbsp;周五</TD><TD  ondblclick=\"window.location.href='Calendar.aspx?CalendarType=0&RefDate="+dt.AddDays(6).ToString("yyyy-MM-dd")+"';\">"+dt.AddDays(6).ToString("M月d日")+"&nbsp;周六</TD></TR>");
 
			this.Add("<TR><TD >"+ this.MyWeek_TimeBar(dt) +"</TD><TD >"+ this.MyWeek_Day(dt) +"</TD><TD >"+ this.MyWeek_Day( dt.AddDays(1) ) +"</TD><TD >"+ this.MyWeek_Day( dt.AddDays(2) ) +"</TD><TD >"+ this.MyWeek_Day( dt.AddDays(3) ) +"</TD><TD >"+ this.MyWeek_Day( dt.AddDays(4) ) +"</TD><TD >"+ this.MyWeek_Day( dt.AddDays(5) ) +"</TD><TD >"+ this.MyWeek_Day( dt.AddDays(6) ) +"</TD></TR>");
			
			this.Add("</TABLE>");
		}
//		public string MyWeek_Day_Time(string strs)
		 public string MyWeek_Day_Time(string strs)
		{
			return "&nbsp;";
			//return null;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
//		public string MyWeek_Day(DateTime dt)
		 public string MyWeek_Day(DateTime dt)
		{
			string date=dt.ToString("yyyy-MM-dd");
		//	TADay td = new TADay(date); 
			string cdID=this.Page.FindControl("WebMenu1").ClientID;
			string hh="";
			string html="<Table  border='1' width=100% cellpadding='0' cellspacing='0' style='border-collapse: collapse' bordercolor='#111111' >";
			for(int h=6;h<=24;h++)
			{
				hh= h.ToString();
				hh=hh.PadLeft(2,'0');
				string timebarcss="TimeNoActive";
				if (h>=8 && h<=18)
				{
					timebarcss="TimeActive";
				}

				/*如果没有要加入的事情。*/				
				html+="<TR >";
				//html+="<TD width=1% onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+timeStr+":00' );\" class='TimeList' valign=top ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+timeStr+"_00' , 'log' )\" >&nbsp;&nbsp;"+timeStr+":00</TD>";
				html+="<TD onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+hh+":00' );\" class='"+timebarcss+"' ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+hh+"_00' , 'log' )\" >"+this.MyWeek_Day_Time( hh+":00" )+"</TD>";
				html+="</TR>";

				html+="<TR>";
				//html+="<TD width=1% onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+timeStr+":30' );\" class='TimeList' valign=top ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+timeStr+"_30' , 'log' )\" >&nbsp;&nbsp;&nbsp;&nbsp;:30</TD>";
				html+="<TD onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+hh+":30' );\" class='"+timebarcss+"' ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+hh+"_30' , 'log' )\" >"+this.MyWeek_Day_Time( hh+":30" )+"</TD>";
				html+="</TR>";
			}
			html+="</TABLE>";
			return html;
		}
//		public string MyWeek_TimeBar(DateTime dt)
		 public string MyWeek_TimeBar(DateTime dt)
		{
			string date=dt.ToString("yyyy-MM-dd");
			string cdID=this.Page.FindControl("WebMenu1").ClientID;
			string timeStr="";
			string html="<Table   border='1' width=100% cellpadding='0' cellspacing='0' style='border-collapse: collapse' bordercolor='#111111' >";
			for(int h=6;h<=24;h++)
			{
				timeStr= h.ToString();
				timeStr=timeStr.PadLeft(2,'0');
				string timebarcss="TimeNoActive";
				if (h>=8 && h<=18)
				{
					timebarcss="TimeActive";
				}

				/*如果没有要加入的事情。*/
				
				html+="<TR>";
				html+="<TD width=1% onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+timeStr+":00' );\" class='TimeList' valign=top ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+timeStr+"_00' , 'log' )\" >"+timeStr+":00</TD>";
				//html+="<TD onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+timeStr+":00' );\" class='"+timebarcss+"' ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+timeStr+"_00' , 'log' )\" >&nbsp;</TD>";
				html+="</TR>";

				html+="<TR>";
				html+="<TD width=1% onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+timeStr+":30' );\" class='TimeList' valign=top ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+timeStr+"_30' , 'log' )\" >&nbsp;&nbsp;:30</TD>";
				//html+="<TD onmousedown=\"javascript:OnDGMousedown( '"+cdID+"', '"+date+"' , '"+timeStr+":30' );\" class='"+timebarcss+"' ondblclick=\"javascript:WinOpen( 'Log.aspx?RefDate="+date+"&RefTime="+timeStr+"_30' , 'log' )\" >&nbsp;</TD>";
				html+="</TR>";
			}
			html+="</TABLE>";
			return html;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{

		}
        public string FK_NY
        {
            get
            {
                return ViewState["FK_NY"] as string;
            }
            set
            {
                this.ViewState["FK_NY"] = value;
            }
        }

//		public void MyMonth(DateTime dt)
        public void MyMonth(DateTime dt)
        {
            this.Controls.Clear();
            string year = dt.Year.ToString();
            string month = dt.Month.ToString();
            string day = dt.Day.ToString();
            string ny = dt.ToString("yyyy-MM");
           // this.FK_NY = ny;
            string today = DataType.CurrentData;

            TaLogs tes = new TaLogs(BP.OA.TAUser.ShareUserNo, ny);
            CycleEvents ces = new CycleEvents(BP.OA.TAUser.ShareUserNo, ny);
            Works wks = new Works(BP.OA.TAUser.ShareUserNo, ny);
            WorkDtls dtls = new WorkDtls(BP.OA.TAUser.ShareUserNo, ny);


            ReWorks res = new ReWorks();
            ReturnWorks rets = new ReturnWorks();

            //ReWorks res = new ReWorks(BP.OA.TAUser.ShareUserNo, ny);
            //ReturnWorks rets = new ReturnWorks(BP.OA.TAUser.ShareUserNo, ny);


            // 一个月份的第一天
            DateTime firstDay = DataType.ParseSysDate2DateTime(year + "-" + month.PadLeft(2, '0') + "-01");

            switch (firstDay.DayOfWeek)
            {
                case DayOfWeek.Sunday: // 0
                    break;
                case DayOfWeek.Monday: // 1
                    firstDay = firstDay.AddDays(-1);
                    break;
                case DayOfWeek.Tuesday: // 2
                    firstDay = firstDay.AddDays(-2);
                    break;
                case DayOfWeek.Wednesday: // 3
                    firstDay = firstDay.AddDays(-3);
                    break;
                case DayOfWeek.Thursday: // 4
                    firstDay = firstDay.AddDays(-4);
                    break;
                case DayOfWeek.Friday: // 5
                    firstDay = firstDay.AddDays(-5);
                    break;
                case DayOfWeek.Saturday: // 6
                    firstDay = firstDay.AddDays(-6);
                    break;
            } // 经过运算后，就不可能是本月的天。

            this.Add("<Table border=1 class='CTable' alt='双击日期出现当天的内容' style='border-collapse: collapse' bordercolor='#111111' valign=top  >");
            this.Add("<TR class='CTitle' >");
            this.Add(" <TD></TD>");
            this.Add(" <TD class='CWeekTD' >星期日</TD>");
            this.Add(" <TD class='CWeekTD' >星期一</TD>");
            this.Add(" <TD class='CWeekTD' >星期二</TD>");
            this.Add(" <TD class='CWeekTD' >星期三</TD>");
            this.Add(" <TD class='CWeekTD' >星期四</TD>");
            this.Add(" <TD class='CWeekTD' >星期五</TD>");
            this.Add(" <TD class='CWeekTD' >星期六</TD>");
            this.AddTREnd();

            string dtFormat = "yyyy年MM月dd日";

            string currDate = DataType.CurrentData;

            int i = 0;
            int week = 0;
            int Mday = 0;
            string cellDate = "";
            while (i != 35)
            {
                i++;
               // cellDate = firstDay.ToString(DataType.SysDataFormat);
                 cellDate = firstDay.ToString(DataType.SysDataFormat);

                //DataType.ParseSysDate2DateTime
                Mday = firstDay.Day;
                switch (firstDay.DayOfWeek)
                {
                    case DayOfWeek.Sunday: // 0
                        this.Add("<TR valign=top >");
                        week++;
                        this.Add(" <TD class='Week' valign=top ><br><br><a href='Calendar.aspx?CalendarType=2&RefDate=" + cellDate + "'>第" + week + "周</a><br><br></TD>");
                        if (today == cellDate)
                            this.Add(" <TD  onmousedown=\"javascript:OnDGMousedown( 'WebMenu1', '" + cellDate + "');\" class='ToDay'  valign=top ondblclick=\"javascript:window.location.href='Calendar.aspx?CalendarType=1&RefDate=" + cellDate + "'\" >" + MyMonthGetCell(firstDay, tes, ces, wks, dtls, res, rets) + "</TD>");
                        else
                            if (firstDay.ToString("yyyy-MM") == ny)
                            {
                                this.Add(" <TD onmousedown=\"javascript:OnDGMousedown( 'WebMenu1', '" + cellDate + "');\" class='WeekEnd' valign=top ondblclick=\"javascript:window.location.href='Calendar.aspx?CalendarType=1&RefDate=" + cellDate + "'\" >" + MyMonthGetCell(firstDay, tes, ces, wks, dtls, res, rets) + "</TD>");
                            }
                            else
                            {
                                this.Add(" <TD  onmousedown=\"javascript:OnDGMousedown( 'WebMenu1', '" + cellDate + "');\" class='UnMonthDay' valign=top ondblclick=\"javascript:window.location.href='Calendar.aspx?CalendarType=1&RefDate=" + cellDate + "'\" >" + MyMonthGetCell(firstDay, tes, ces, wks, dtls, res, rets) + "</TD>");
                            }
                        break;
                    case DayOfWeek.Saturday: // 1
                        if (DataType.CurrentData == firstDay.ToString(DataType.SysDataFormat))
                            this.Add(" <TD onmousedown=\"javascript:OnDGMousedown( 'WebMenu1', '" + cellDate + "');\" class='ToDay' valign=top  ondblclick=\"javascript:window.location.href='Calendar.aspx?CalendarType=1&RefDate=" + cellDate + "'\" >" + MyMonthGetCell(firstDay, tes, ces, wks, dtls, res, rets) + "</TD>");
                        else
                            if (firstDay.ToString("yyyy-MM") == ny)
                            {
                                this.Add(" <TD onmousedown=\"javascript:OnDGMousedown( 'WebMenu1', '" + cellDate + "');\" class='WeekEnd' valign=top ondblclick=\"javascript:window.location.href='Calendar.aspx?CalendarType=1&RefDate=" + cellDate + "'\" >" + MyMonthGetCell(firstDay, tes, ces, wks, dtls, res, rets) + "</TD>");
                            }
                            else
                            {
                                this.Add(" <TD  onmousedown=\"javascript:OnDGMousedown( 'WebMenu1', '" + cellDate + "');\" class='UnMonthDay' valign=top ondblclick=\"javascript:window.location.href='Calendar.aspx?CalendarType=1&RefDate=" + cellDate + "'\" >" + MyMonthGetCell(firstDay, tes, ces, wks, dtls, res, rets) + "</TD>");
                            }

                        this.AddTREnd();
                        break;
                    default:
                        if (currDate == firstDay.ToString(DataType.SysDataFormat))
                            this.Add(" <TD title='" + firstDay.ToString(dtFormat) + "' onmousedown=\"javascript:OnDGMousedown( 'WebMenu1', '" + cellDate + "');\" class='ToDay' nowrap=false valign=top ondblclick=\"javascript:window.location.href='Calendar.aspx?CalendarType=1&RefDate=" + cellDate + "'\" >" + MyMonthGetCell(firstDay, tes, ces, wks, dtls, res, rets) + "</TD>");
                        else
                            if (firstDay.ToString("yyyy-MM") == ny )
                            {
                                this.Add(" <TD  onmousedown=\"javascript:OnDGMousedown( 'WebMenu1', '" + cellDate + "');\" class='TimeActive' valign=top ondblclick=\"javascript:window.location.href='Calendar.aspx?CalendarType=1&RefDate=" + cellDate + "'\" >" + MyMonthGetCell(firstDay, tes, ces, wks, dtls, res, rets) + "</TD>");
                            }
                            else
                            {
                                this.Add(" <TD  onmousedown=\"javascript:OnDGMousedown( 'WebMenu1', '" + cellDate + "');\" class='UnMonthDay' valign=top ondblclick=\"javascript:window.location.href='Calendar.aspx?CalendarType=1&RefDate=" + cellDate + "'\" >" + MyMonthGetCell(firstDay, tes, ces, wks, dtls, res, rets) + "</TD>");
                            }
                        break;
                }
                firstDay = firstDay.AddDays(1);
            }
            this.AddTableEnd();

            this.AddMsgOfInfo( dt.ToString("yyyy年MM月")+"事务统计信息：日志:" + tes.Count + "条,循环事件:" + ces.Count + "条,接受工作:" + dtls.Count + "件。<b>提示:在日期上有右键操作。</b>", null);
            //this.BindMonthNaviat(dt);
            //this.DataPanelAddEvent(dt);
        }
//		public void BindMonthNaviat(DateTime dt)
		 public void BindMonthNaviat(DateTime dt)
		{
			//return;

			int pYear=dt.Year-1;
			string str="<a href='Calendar.aspx?CalendarType=1&RefDate="+pYear+dt.ToString("-MM-dd")+"' ><img src='./Images/Arrowhead_Previous_S.gif' border=0 />"+dt.Year.ToString()+"</a>";
			pYear=dt.Year+1;
			str+="<a href='Calendar.aspx?CalendarType=1&RefDate="+pYear+dt.ToString("-MM-dd")+"' ><img src='./Images/Arrowhead_Next_S.gif' border=0 />"+dt.Year.ToString()+"</a>";

			pYear=dt.Year;
			for(int i =0;i <=12; i++)
				str+="<a href='Calendar.aspx?CalendarType=1&RefDate="+pYear+"-"+ i.ToString().PadLeft(2,'0') +"-01' >"+i.ToString().PadLeft(2,'0')+"</a>";

			this.AddTable("width=100%");
			this.AddTR();
			//this.AddTD( str );
			this.AddTD( "sdsdsd" );

			this.AddTREnd();
			this.AddTableEnd();
		}

        private string MyMonthGetCell(DateTime dt, TaLogs tes, CycleEvents ces, Works wks, WorkDtls dtls, ReWorks rns, ReturnWorks rets)
        {
            string date = dt.ToString("yyyy-MM-dd");
            //string str= "<a href='OpenCell.aspx?RefDate="+date+"' ><b>"+dt.Day+"</b></a>&nbsp;新建<a onclick=\"javascript:WinOpen('Work.aspx?RefDate="+date+"')\" >任务</a>,<a onclick=\"javascript:WinOpen('Log.aspx?EventDate="+date+"')\" >日志</a>,<a onclick=\"javascript:WinOpen('CycleEvent.aspx?RefDate="+date+"')\" >定期事件</a>" ;
            //string str= "<a href='Calendar.aspx?CalendarType=1&RefDate="+date+"' ><b>"+dt.Day+"</b></a> " ;
            string str = "<a href='Calendar.aspx?CalendarType=1&RefDate=" + date + "' ><b>" + dt.ToString("dd") + "</b></a> ";

            #region  工作日志
            foreach (TaLog te in tes)
            {
                if (te.LogDate.IndexOf(date) == -1)
                    continue;
                str += "<br>" + te.LogTime + "<a href=\"javascript:OpenLog('" + te.OID + "') \" title=\"持续:" + te.FK_TimeScopeText + "\" >" + te.TitleHtml + "</a>";
            }
            #endregion

            #region  加入定期事件
            foreach (CycleEvent ce in ces)
            {
                if (ce.NoEndDate == false)
                {
                    /* 有结束日期 */
                    if (dt > ce.EndDate_S)
                    {
                        /* 面板日期 > 结束日期 */
                        continue;
                    }
                }
                switch (ce.MyCycleWay)
                {
                    case CycleWay.ByWeek:
                        int week = (int)dt.DayOfWeek;
                        if (ce.Weeks == week)
                        {
                            /*如果指定的周内有它。 */
                            str += "<br>" + ce.TitleHtml + "</a>";
                        }
                        break;
                    case CycleWay.ByMonth:
                        int day = (int)dt.Day;
                        if (ce.Days == day)
                        {
                            /*如果指定的天内有它。 */
                            str += "<br>" + ce.TitleHtml + "</a>";
                        }
                        break;
                    case CycleWay.ByYear:
                        int month = (int)dt.Month;
                        if (ce.Monthes == month)
                            continue;
                        int day1 = (int)dt.Day;
                        if (ce.Days == day1)
                        {
                            /*如果指定的天内有它。 */
                            str += "<br>" + ce.TitleHtml + "</a>";
                        }
                        break;
                }
            }
            #endregion

            #region 加入我的工作。
            foreach (Work wk in wks)
            {
                if (wk.WorkState == WorkState.None)
                    continue;

                if (wk.Title.Trim().Length == 0)
                    continue;

                if (wk.DTOfStart.IndexOf(date) == -1)
                    continue;

                str += "<BR>" + wk.RDT.Substring(11);
                string url = "Work.aspx?RefOID=" + wk.OID + "&ActionType=" + (int)BP.Web.TA.ActionType.ViewWork + "&WorkState=" + (int)wk.WorkState;
                str += "<a href=\"javascript:WinOpen('" + url + "','" + (int)BP.Web.TA.ActionType.ViewWork + "')\" title='我发起的工作' ><img src='" + wk.EnMap.Icon + "' border=0 >" + wk.Title + "</a>";
            }
            #endregion

            #region 我的工作。
            foreach (WorkDtl dtl in dtls)
            {
                if (dtl.DTOfActive.IndexOf(date) == -1)
                    continue;

                str += "<BR>" + dtl.DTOfSend;
                string url = "Work.aspx?RefOID=" + dtl.OID + "&ActionType=" + (int)BP.Web.TA.ActionType.ViewWorkDtl;
                str += "<a href=\"javascript:WinOpen('" + url + "','workdtl' )\"  title='我接受的工作' >" + dtl.WorkDtlStateImg + dtl.HisWork.Title + "</a>";
                //this.Add("<TR class='TR'  ondblclick=\"javascript:window.location.href='Work.aspx?RefOID="+dtl.OID+"&ActionType="+(int)ActionType.ViewWorkDtl+"&FK_From="+datafrom+"&FK_To="+datato+"&WorkDtlState="+state+"'\" onmouseover='TROver(this)' onmouseout='TROut(this)' >");
                continue;
            }
            #endregion


            #region 加入我回复的与回复给我的信息
            foreach (ReWork tn in rns)
            {

                if (tn.DTOfActive.IndexOf(date) == -1)
                    continue;
                if (tn.ReWorkState == ReWorkState.None)
                    continue;

                if (WebUser.No == tn.Sender)
                {
                    /* 如果我是接受人, 我是工作的下达人。 */
                    // str+="<br><img src='./Images/"+tn.ReWorkState.ToString()+".ico' /><a onclick=\"javascript:WinOpen('Work.aspx?RefOID="+tn.OID+"&ActionType="+(int)BP.Web.TA.ActionType.ViewReWork+ "') \" >"+tn.Title+"</a>";
                    switch (tn.ReWorkState)
                    {
                        case ReWorkState.None:
                            //str+="<BR>"+tn.ReWorkState;
                            break;
                        case ReWorkState.Ratify:
                            //str+="<BR>"+tn.ReWorkState;
                            break;
                        case ReWorkState.UnRatify: // 就不让其显示
                            //str+="<br><img src='./Images/"+icon+"' />"+tn.ExecuterText +"<a onclick=\"javascript:WinOpen('Work.aspx?RefOID="+tn.OID+"&ActionType="+(int)BP.Web.TA.ActionType.ViewReWork+ "') \" >"+tn.Title+"</a>";
                            break;
                        default:
                            continue;
                    }
                }
                else if (WebUser.No == tn.Executer)
                {
                    /*如果我是执行人 */

                    //str+="<BR>"+tn.MyReWorkStateOpStr;
                    /*我是发送人 */
                    switch (tn.ReWorkState)
                    {
                        case ReWorkState.Ratify: //认可的工作.
                            str += "<br><img src='./Images/" + tn.ReWorkState + ".gif' />" + tn.ExecuterText + "<a onclick=\"javascript:WinOpen('Work.aspx?RefOID=" + tn.OID + "&ActionType=" + (int)BP.Web.TA.ActionType.ViewReWork + "') \" >" + tn.Title + "</a>";
                            //str+="<BR>"+tn.ReWorkState;
                            break;
                        case ReWorkState.UnRatify:
                            str += "<br><img src='./Images/" + tn.ReWorkState + ".gif' />" + tn.ExecuterText + "<a onclick=\"javascript:WinOpen('Work.aspx?RefOID=" + tn.OID + "&ActionType=" + (int)BP.Web.TA.ActionType.ViewReWork + "') \" >" + tn.Title + "</a>";
                            //str+="<BR>"+tn.ReWorkState;
                            break;
                        case ReWorkState.Sending:
                            //str+="<br><img src='./Images/"+tn.ReWorkState+".ico' />"+tn.ExecuterText +"<a onclick=\"javascript:WinOpen('Work.aspx?RefOID="+tn.OID+"&ActionType="+(int)BP.Web.TA.ActionType.ViewReWork+ "') \" >"+tn.Title+"</a>";
                            break;
                        default:
                            throw new Exception("no such case11 " + tn.ReWorkState);
                    }
                }
            }
            #endregion
            return str;
        }
//		public string DataPanelMonth_Small(DateTime dt)
		 public string DataPanelMonth_Small(DateTime dt)
		{
			string html="";
			string year=dt.Year.ToString();
			string month=dt.Month.ToString();
			string day=dt.Day.ToString();
			string ny=dt.ToString("yyyy-MM");
			string today=DataType.CurrentData;
			string selectedDay=dt.ToString("yyyy-MM-dd");

			// 一个月份的第一天
			DateTime firstDay=DataType.ParseSysDate2DateTime(year+"-"+month.PadLeft(2,'0')+"-01");
			switch(firstDay.DayOfWeek)
			{
				case DayOfWeek.Sunday: // 0
					break;						 
				case DayOfWeek.Monday: // 1
					firstDay=firstDay.AddDays(-1);
					break;
				case DayOfWeek.Tuesday: // 2
					firstDay=firstDay.AddDays(-2);
					break;
				case DayOfWeek.Wednesday: // 3
					firstDay=firstDay.AddDays(-3);
					break;
				case DayOfWeek.Thursday: // 4
					firstDay=firstDay.AddDays(-4);
					break;
				case DayOfWeek.Friday: // 5
					firstDay=firstDay.AddDays(-5);
					break;
				case DayOfWeek.Saturday: // 6
					firstDay=firstDay.AddDays(-6);
					break;
			} // 经过运算后，就不可能是本月的天。
			 
			html+="<Table border=1 width=100% height=100% class='CTable' alt='双击日期出现当天的内容' style='border-collapse: collapse' bordercolor='#111111' valign=top  >";

			html+="<TR class='CNYTitle' align=center >";
			html+=" <TD align=center colspan=7><a href='Calendar.aspx?RefDate="+dt.AddMonths(-1).ToString("yyyy-MM-dd")+"&CalendarType=1'  ><img src='./Images/Arrowhead_Previous_S.gif' border=0 /></a>"+year+"年"+month+"月<a href='Calendar.aspx?CalendarType=1&RefDate="+dt.AddMonths(+1).ToString("yyyy-MM-dd")+"'  ><img src='./Images/Arrowhead_Next_S.gif' border=0 /></a></TD>";
			html+="</TR>";

			html+="<TR class='CTitle' >";
			//html+=" <TD></TD>";
			html+=" <TD>日</TD>";
			html+=" <TD>一</TD>";
			html+=" <TD>二</TD>";
			html+=" <TD>三</TD>";
			html+=" <TD>四</TD>";
			html+=" <TD>五</TD>";
			html+=" <TD>六</TD>";
			html+="</TR>";

			int i=0;
			int week=0;
			int Mday=0;
			string cellDate="";
			while( i!=35 )
			{
				i++;
				cellDate=firstDay.ToString(DataType.SysDataFormat);
				//DataType.ParseSysDate2DateTime
				Mday=firstDay.Day;
				switch(firstDay.DayOfWeek)
				{
					case DayOfWeek.Sunday: // 0
						html+="<TR valign=center >";
						week++;
						//html+=" <TD class='Week' valign=top ><br><br>"+week+"<br><br></TD>";	
						if ( today == cellDate)
							html+=" <TD class='ToDay'   onclick=\"javascript:CelldblClick('"+cellDate+"' , '1')\" >"+Mday+"</TD>";
						else
						{
							if (selectedDay==cellDate)
							{
								html+=" <TD class='SelectedDay' onclick=\"javascript:CelldblClick('"+cellDate+"' , '1' )\" >"+Mday+"</TD>";
							}
							else
							{
								html+=" <TD  class='WeekEnd' onclick=\"javascript:CelldblClick('"+cellDate+"' , '1' )\" >"+Mday+"</TD>";
							}
						}
						break;
					case DayOfWeek.Saturday: // 1
						if (today==firstDay.ToString(DataType.SysDataFormat) )
							html+=" <TD class='ToDay'  onclick=\"javascript:CelldblClick('"+cellDate+"' , '1' )\" >"+Mday+"</TD>";
						else
						{
							if (selectedDay==cellDate)
							{
								html+=" <TD class='SelectedDay' onclick=\"javascript:CelldblClick('"+cellDate+"' , '1' )\" >"+Mday+"</TD>";
							}
							else
							{
								html+=" <TD class='WeekEnd' onclick=\"javascript:CelldblClick('"+cellDate+"' , '1' )\" >"+Mday+"</TD>";
							}
						}
						html+="</TR>";
						break;
					default:
						if (today==firstDay.ToString(DataType.SysDataFormat) )
							html+=" <TD class='ToDay' nowrap=false valign=center onclick=\"javascript:CelldblClick('"+cellDate+"' , '1' )\" >"+Mday+"</TD>";
						else
						{
							if (selectedDay==cellDate)
							{
								html+=" <TD class='SelectedDay' onclick=\"javascript:CelldblClick('"+cellDate+"')\" , '1'>"+Mday+"</TD>";
							}
							else
							{
								html+=" <TD   onclick=\"javascript:CelldblClick('"+cellDate+"' , '1' )\" >"+Mday+"</TD>";
							}
						}
						break;
				}
				firstDay=firstDay.AddDays(1);
			}
			html+="</Table>";
			return html; 
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
