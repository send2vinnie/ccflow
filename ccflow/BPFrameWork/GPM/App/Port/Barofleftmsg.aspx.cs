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

namespace BP.Web.PG.App.Port
{
	/// <summary>
	/// BarOfLeftMsg ��ժҪ˵����
	/// </summary>
	public partial class BarOfLeftMsg : System.Web.UI.Page
	{
        protected void Page_Load(object sender, System.EventArgs e)
        {
            return;
            string path = this.Request.ApplicationPath;
            string str = "";
            if (WebUser.No == null)
            {
                str = "<img  src='OnlineUser.gif' border='0'   ><font color='#000000' size='1' ><a href=\"SignInOfRe.aspx?IsChangeUser=1\" target='mainfrm' >��½ʱ��̫����Ҫ�����µ�¼</a></font>";
            }
            else
            {
                str = "<a href=\"SignInOfRe.aspx\"  target='mainfrm' ><img src='Img/OnlineUser.gif'  border='0' /><font color='#000000' size='1' >&nbsp;�л�" + WebUser.Name + "</a>"; //"<a href=\"javascript: WinOpen('"+path+"/Comm/Port/OnlineUsers.aspx','online' ) ; \" >����("+OnlineUserManager.GetUserCount()+")</font></a>";
            }
            this.Response.Write(str );
            return;
        }

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
