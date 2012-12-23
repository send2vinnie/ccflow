namespace BP.Web.WF.Port
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BP.Web;

	/// <summary>
	///		Navigation ��ժҪ˵����
	/// </summary>
	public partial  class Navigation : System.Web.UI.UserControl
	{
		const string SkinName = "default";

		protected void Page_Load(object sender, System.EventArgs e)
		{
 		
			string path = this.Request.ApplicationPath;

			if (this.Session["UserNo"]==null)
			{
                //OnlineUsersMenu.NavigateUrl =  SystemConfig.PageOfLostSession;			
                //OnlineUsersMenu.Text = "<img  src='users.gif' border='0'  width='30' height='26' >";									
                //OnlineUsersMenu.Text += "��½ʱ��̫����Ҫ�����µ�¼";
                //OnlineUsersMenu.Target = "mainfrm";
				return;
			}

            //OnlineUsersMenu.NavigateUrl = path + "/Comm/Port/OnlineUsers.aspx";			
            //OnlineUsersMenu.Text = "<img  src='Users.gif'  border='0'  width='30' height='26' >";									
            //OnlineUsersMenu.Text += "����("+OnlineUserManager.GetUserCount()+")";
            //OnlineUsersMenu.Target = "_OnlineUserWin";

			NewMess.Visible=false;
			/*
			NewMess.NavigateUrl = path + "/WF/Port/UIWFMsg.aspx";
			NewMess.Text = "<img src=\"" + path + "/images/WF/Title/newmess.gif" + "\" border=\"0\" width='28' height='13'' >";
			NewMess.Text += "��Ϣ("+MsgsManager.GetMsgsCountByEmpID(WebUser.OID)+")";
			NewMess.Target = "_MsgWin";
			*/

			HomeMenu.NavigateUrl = "Wel.aspx";
			HomeMenu.Text = "<img src='Home.gif' border='0' width='28' height='15' >";
			HomeMenu.Text += "��ҳ";
			HomeMenu.Target = "mainfrm";
 
			/*

			MyMsgs.NavigateUrl =path+"/Comm/Port/Msg/MyMsgs.aspx";
			MyMsgs.Text = "<img src='newmess.gif' border='0' width='28' height='15' >";
			MyMsgs.Text += "�ҵ���Ϣ("+Sys.SysMsg.MyMsgNum+")";
			MyMsgs.Target = "mainfrm";
			*/

			 
			MyMsgs.NavigateUrl =path+"/GTS/Paper/ExamLink.aspx";
			MyMsgs.Text = "<img src='newmess.gif' border='0' width='28' height='15' >";
			MyMsgs.Text += "���߿���";
			MyMsgs.Target = "mainfrm";
			

			HelpMenu.NavigateUrl="Helper.htm";
			HelpMenu.Text = "<img src='Help.gif' border=\"0\" >";
			HelpMenu.Text += "����";
			HelpMenu.Target = "_blank";

			PersonalMenu.NavigateUrl = path + "/Comm/Sys/UserTools.aspx";
			PersonalMenu.Text = "<img src='Personal.gif' border='0'  >";
			PersonalMenu.Text += "����";
			PersonalMenu.Target = "mainfrm";


			if (BP.SystemConfig.IsMultiSys )
			{
				ChangeSys.NavigateUrl = path+"/Comm/Port/ChangeSystem.aspx";
				ChangeSys.Text = "<img src='ChangeUser.gif' border='0' >";
				ChangeSys.Text += "�л�ϵͳ";
				ChangeSys.Target = "mainfrm";
			}

			ChangeUser.NavigateUrl = "SignIn.aspx?IsChangeUser=1";
			ChangeUser.Text = "<img src='ChangeUser.gif' border='0' >";
			ChangeUser.Text += "�����û�["+WebUser.Name+"]";
			ChangeUser.Target = "mainfrm";

			AuthorizedAgent.NavigateUrl = path+"/Comm/Port/OnlineUsers.aspx";
			AuthorizedAgent.Text = "<img src='AuthorizedAgent.gif' border='0' >";
			AuthorizedAgent.Text += "��Ȩ";
			AuthorizedAgent.Target = "mainfrm";

			LoginWithA.NavigateUrl = path+"/Comm/Port/AuthorizeLogin.aspx";
			LoginWithA.Text = "<img src='LoginWithAgree.gif' border=\"0\" >";
			LoginWithA.Text += "��Ȩ��ʽ��½";
			LoginWithA.Target = "mainfrm";
			
			BackToMySession.NavigateUrl = path+"/Comm/Port/BackToMySession.aspx";
			BackToMySession.Text = "<img src='BackToMySession.gif' border='0' >";
			BackToMySession.Text += "�˳���Ȩ��½";
			BackToMySession.Target = "mainfrm";

			if (WebUser.IsAuthorize)
			{
				BackToMySession.Visible=true;
				LoginWithA.Visible=false;
				AuthorizedAgent.Visible=false;
			}
			else
			{
				BackToMySession.Visible=false;
				LoginWithA.Visible=true;
				AuthorizedAgent.Visible=true;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN���õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		�����֧������ķ��� - ��Ҫʹ��
		///		����༭���޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		/// <summary>
		/// �˳�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogoutMenu_DataBinding(object sender, System.EventArgs e)
		{

			try
			{
				//BP.WF.MsgsManager.ClearMsgsByEmpID(WebUser.OID) ;
				WebUser.Exit();
				//this.url=this.Request.QueryString["url"];
				this.Response.Write("���ڣ����Ѿ���ȫ���˳���ϵͳ��ллʹ�ã��ټ�!");

			}
			catch(System.Exception ex)
			{
				this.Response.Write(ex.Message) ; 
			}		
		}
	}
}
