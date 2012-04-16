
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
using BP.En;
using BP.DA;
using BP.Port;
using System.Xml;
using System.IO ; 

namespace BP.Web
{
	/// <summary>
	/// SignIn ��ժҪ˵����
	/// </summary>
	public partial class SignInPG: Page
	{
		//protected BP.Web.Controls.BPCheckBox BPCheckBox1;

		public string RawUrl
		{
			get
			{
				return ViewState["RawUrl"] as string ; 
			}
			set
			{
				ViewState["RawUrl"]=value;
			}
		}
		public bool IsTurnTo
		{
			get
			{
				if (this.Request.QueryString["IsTurnTo"]==null)
					return false;
				else
					return true;
			}
		}
//		public void BeforeLoad()
//		{
//
//		}
	
		public void Page_Load(object sender, System.EventArgs e)
		{
            string userNo = this.Request.QueryString["UserNo"];
            if (userNo != null && userNo.Length > 1)
            {
                string sid = this.Request.QueryString["SID"];

                string mysid = DBAccess.RunSQLReturnStringIsNull("SELECT SID FROM PORT_EMP WHERE No='" + Web.WebUser.No + "'", null);
                if (sid == mysid)
                {
                    Response.Redirect("Home.aspx", false);
                }
                else
                {
                    Response.Redirect("Home.aspx", false);
                }
                return;
            }


			string script="<script language=javascript>function setFocus(ctl) {if (document.forms[0][ctl] !=null )  { document.forms[0][ctl].focus(); } } setFocus('"+this.TB_Pass.ClientID+"'); </script>";
			this.RegisterStartupScript("func", script);
			if (this.Request.QueryString["Token"] !=null )
			{
				HttpCookie hc1 = this.Request.Cookies["CCS"];
				if (hc1!=null)
				{
					if (this.Request.QueryString["Token"]==hc1.Values["Token"] )
					{
						Emp em = new Emp(this.Request.QueryString["No"]);
						WebUser.SignInOfGener(em,true);
						WebUser.Token = this.Request.QueryString["Token"];
						Response.Redirect("Home.aspx",false);
						return;
					}
				}
			}

			if (this.IsPostBack==false)
			{
				this.TB_No.Attributes["background-image"]="url('beer.gif')"; 
				HttpCookie hc = this.Request.Cookies["CCS"];
				if (hc!=null)
				{

              //      Response.Redirect("Home.aspx", false);
					this.TB_No.Text=hc.Values["UserNo"];
					//this.Lab1.Text=hc.Values["UserName"];
					//this.TB_Pass.Text=hc.Values["Pass"];
				}
			}
			if (this.Request.QueryString["IsChangeUser"]!=null)
			{
				this.RawUrl=this.Request.RawUrl;
			}			 
			if (this.Request.Browser.MajorVersion < 6 )	
			{
				this.Response.Write("�Բ���ϵͳ��⵽����ǰʹ�õ�IE�汾��["+this.Request.Browser.Version+"]��ϵͳ�����ڵ�ǰ��IE����������������ȷ��ʹ�ô�ϵͳ����������IE6.0������<a href='../IE6.rar'>��������IE6.0</A>�����غ󣬽⿪ѹ���ļ������� ie6setup.exe��������������µ� ["+BP.SystemConfig.ServiceTel+"], ���߷� Mail ["+BP.SystemConfig.ServiceMail+"]��");
				this.Btn1.Enabled=false;
				this.TB_No.Enabled=false;
				this.TB_Pass.Enabled=false;
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
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		public void Btn1_Click(object sender, System.EventArgs e)
		{
			try
			{
				 Emp em = new Emp(this.TB_No.Text);
				if ( em.Pass.Trim().Equals(this.TB_Pass.Text.Trim() ) || this.TB_Pass.Text.ToLower().Trim()== BP.SystemConfig.AppSettings["GenerPass"] || SystemConfig.IsDebug ) 
				{
					//OnlineUserManager.AddUser(em,"ss",em.FK_DeptText);
                    if (this.Request.QueryString["IsChangeUser"] != null)
                    {
                        /* ����Ǹ����û�.*/
                        if (this.Session["OID"] != null)
                        {
                            string oid = WebUser.No;
                            this.Session.Clear();
                           // OnlineUserManager.ReomveUser(oid);
                        }
                    }

					HttpCookie cookie=new HttpCookie("CCS");
					cookie.Expires = DateTime.Now.AddMonths(10);
					cookie.Values.Add( "UserNo",em.No);
					cookie.Values.Add( "UserName",em.Name);
					cookie.Values.Add( "Token",this.Page.Session.SessionID );

					Response.AppendCookie( cookie );
					if (this.Session["UserNo"]!=null)
					{
						string oid= this.Session["UserNo"].ToString() ;
					}

					WebUser.SignInOfGener(em,true);
					WebUser.Token=this.Session.SessionID;

                    if (this.RawUrl == null)
                    {
                        Response.Redirect("Home.aspx", false);
                    }
                    else
                    {
                        string url = this.RawUrl;
                        if (url.ToLower().IndexOf("signin.aspx") > 0)
                        {
                            Response.Redirect("../Paper/ExamLink.aspx", true);
                            return;
                        }
                        Response.Redirect(url, true);
                    }
					return;
				}
				else
				{
					throw new Exception("������󣡼���Ƿ�����CapsLock ��");
				}
			}
			catch(System.Exception ex)
			{
				this.Response.Write("<font color=red ><b>@�û����������!@����Ƿ�����CapsLock.@����ϸ����Ϣ:"+ex.Message+"</b></font>");
				 
			}
		}
		 
	}
}
