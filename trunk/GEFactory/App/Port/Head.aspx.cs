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
using BP.Web;

namespace BP.Web.App
{
	/// <summary>
	/// Home 的摘要说明。
	/// </summary>
	public partial class Head : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			string path = this.Request.ApplicationPath;
			string str="<b>";
			if (this.Session["UserNo"]==null)
			{
				str+="<br><br><center><img  src='users.gif' border='0'   ><a href='"+SystemConfig.PageOfLostSession+"' target='mainfrm' >登陆时间太长需要您重新登录</a><img src='../../Images/Btn/Refurbish.gif'><a href='Head.aspx'  border=0 >刷新</a></center>";
				//this.Response.Write(str+"</b>");
				return;
			}

			//
			//			str="<p align=right ><b>";
			//			str+="<img  src='Home.gif'          border='0'   ><a href='Wel.aspx' target='mainfrm' ><b>主页</b></a>";
			//			str+="<img  src='OnlineUser.gif'    border='0'   ><a href='"+path+"/Comm/Port/OnlineUsers.aspx' target='_OnlineUserWin' >在线("+OnlineUserManager.GetUserCount()+")</a>";
			//			str+="<img  src='Help.gif'          border='0'   ><a href='Helper.htm' target='_blank' >帮助</a>";
			//			str+="<img  src='Personal.gif'      border='0'   ><a href='"+path+"/Comm/Port/Personal.aspx' target='mainfrm' >工具</a>";
			//			str+="<img  src='ChangeUser.gif'    border='0'   ><a href='SignIn.aspx?IsChangeUser=1' target='mainfrm' >更改用户("+WebUser.Name+")</a>";
			//
			//			str+="<br>";
			//          str+="<br>";
			// 
			//	str+="<img  src='"+path+"/App/Paper/ChoseOne.gif'         border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=ChoseOne' target='mainfrm' >单选题</a>";
			//	str+="<img  src='"+path+"/App/Paper/ChoseM.gif'           border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=ChoseM' target='mainfrm' >多选题</a>";
			//	str+="<img  src='"+path+"/App/Paper/FillBlank.gif'        border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=FillBlank' target='mainfrm' >填空题</a>";
			//	str+="<img  src='"+path+"/App/Paper/JudgeTheme.gif'       border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=JudgeTheme' target='mainfrm' >判断题</a>";
			//	str+="<img  src='"+path+"/App/Paper/EssayQuestion.gif'       border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=EssayQuestion' target='mainfrm' >问答题</a>";
			//	str+="<img  src='"+path+"/App/Paper/RC.gif'       border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=RC' target='mainfrm' >阅读理解</a>";
			//	str+="<img  src='"+path+"/App/Paper/RC.gif'       border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=RC' target='mainfrm' >阅读理解</a>";
			//	str+="<img  src='"+path+"/App/Paper/RC.gif'       border='0'   ><a href='"+path+"/App/Paper/Exercise.aspx?ThemeType=RC' target='mainfrm' >阅读理解</a>";
			//	str+="</b></p>";
			//	this.Response.Write(str);

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
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
