using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace BP.Web.Comm.UI
{
	/// <summary>
	/// CalendarHelp 的摘要说明。
	/// </summary>
	public partial class CalendarHelp1 : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN：该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void Btn_O_Click(object sender, System.EventArgs e)
		{
			try
			{
//				string year = this.Calendar1.SelectedDate.Year.ToString();
//				string month = this.Calendar1.SelectedDate.Month.ToString();
//				string day = this.Calendar1.SelectedDate.Day.ToString();
//				string str =year+month.PadLeft(2,'0') + day.PadLeft(2,'0');
				string str = BP.DA.DataType.StringToDateStr(this.Calendar1.SelectedDate.ToShortDateString()) ;
				 
				string clientscript = "<script language='javascript'> window.returnValue = '"+str+"'; window.close(); </script>";
				this.Page.Response.Write(clientscript);
			}
			catch(System.Exception  ex)
			{
				this.Page.Response.Write(ex.Message);
			 		
			}
		}

		protected void Btn_C_Click(object sender, System.EventArgs e)
		{
			
			string clientscript = "<script language='javascript'>  window.close(); </script>";
			this.Page.Response.Write(clientscript);
		}
	}
}
