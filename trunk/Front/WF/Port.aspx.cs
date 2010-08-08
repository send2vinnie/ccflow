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
using BP.WF;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Sys;


namespace BP.Web.Port
{
	/// <summary>
	/// Port 的摘要说明。
	/// </summary>
	public partial class Port : System.Web.UI.Page
	{
		#region 必须传递参数
		public string DoWhat
		{
			get
			{
				return this.Request.QueryString["DoWhat"];
			}
		}
		public string UserNo
		{
			get
			{
				return this.Request.QueryString["UserNo"];
			}
		}
		public string RequestKey
		{
			get
			{
				return this.Request.QueryString["Key"];
			}
		}
		#endregion

		#region  可选择的参数
		public string Taxpayer
		{
			get
			{
				if ( this.Request.QueryString["Taxpayer"]==null) 
					throw new Exception("纳税人编号没有传递过来。");

				return this.Request.QueryString["Taxpayer"];
			}
		}
		public string FK_Flow
		{
			get
			{
				return this.Request.QueryString["FK_Flow"];
			}
		}
       
        public string SID
        {
            get
            {
                return this.Request.QueryString["SID"];
            }
        }
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
            Response.AddHeader("P3P", "CP=CAO PSA OUR");

            if (this.UserNo != null && this.SID != null)
            {
                string sql = "select sid  from Port_Emp WHERE no='" + this.UserNo + "'";
                string sid = BP.DA.DBAccess.RunSQLReturnVal(sql).ToString();
                if (sid != this.SID)
                {
                    this.Response.Write("非法的访问，请与管理员联系。");
                    //this.UCSys1.AddMsgOfWarning("错误：", "非法的访问，请与管理员联系。");
                    return;
                }
                else
                {
                    Emp em = new Emp(this.UserNo);
                    WebUser.Token = this.Session.SessionID;
                    WebUser.SignInOfGenerLang(em, SystemConfig.SysLanguage);
                }
                this.Response.Redirect("EmpWorks.aspx", true);
                return;
            }

            foreach (string str in this.Request.QueryString)
            {
                string val = this.Request.QueryString[str];
                if (val.IndexOf('@') != -1)
                    throw new Exception("您没有能参数: [ " + str + " ," + val + " ] 给值 ，URL 将不能被执行。");
            }

			if (this.IsPostBack==false)
			{
				this.IsCanLogin();
				BP.Port.Emp emp = new BP.Port.Emp(this.UserNo) ; 
				BP.Web.WebUser.SignInOfGener(emp); //开始执行登陆。
				switch( this.DoWhat )
				{
					case DoWhatList.RequestCJ: // 请求调用流程
						this.Response.Redirect("../ZF/CJ.htm",true);
						break;
					case DoWhatList.RequsetMyCT: // 请求调用流程
						this.Response.Redirect("../../CT/CT/Port/Home.htm",true);
						break;
					case DoWhatList.RequsetMyZF: // 请求调用流程
						this.Response.Redirect("../../ZF/ZF/Port/Home.htm",true);
						break;
					case DoWhatList.RequestStart: // 请求调用流程业务受理。
						this.Response.Redirect("Start.aspx",true);
						break;
					case DoWhatList.RequestMyWork: // 请求调用流程 我的工作。
						this.Response.Redirect("MyWork.aspx",true);
						break;
					case DoWhatList.RequestHome: // 请求调用流程
						this.Response.Redirect("./Port/Home.htm",true);
						break;
					case DoWhatList.RequestMyFlow: // 请求调用流程
						if (this.FK_Flow==null)
							throw new Exception("没有指定流程编号");
						this.Response.Redirect("MyFlow.aspx?FK_Flow="+this.FK_Flow,true);
						break;
					case DoWhatList.Login:
						this.Response.Redirect("./Port/Home.htm",true);
						break;
					case DoWhatList.FlowSearch: // 流程查询。
						this.Response.Redirect("../Comm/PanelEns.aspx?EnsName=BP.WF.CHOfFlows&FK_Flow="+this.FK_Flow,true);
						break;	
					case DoWhatList.KYDJ:
//						BP.Port.WF.DJ.ND1 kydj= new BP.Port.WF.DJ.ND1();
//						if (kydj.IsExit("FK_Taxpayer",this.Taxpayer)==true)
//						{
//							this.ShowMsg( "流程已经启动." );
//							WebUser.Exit();
//							return;
//						}
//						kydj.FK_Taxpayer=this.Taxpayer;
//						kydj.Rec=WebUser.No;
//						kydj.RDT=DataType.CurrentDataTime;
//						kydj.CDT=DataType.CurrentDataTime;
//						kydj.Insert();
//
//						WorkNode wn = new WorkNode(kydj,new Node( 11001 ));
//						string msg=wn.AfterNodeSave(true,false);
//						WebUser.Exit();
//						this.ShowMsg(msg); 
						break;
					default:
						this.ToErrorPage("没有约定的标记:DoWhat="+this.DoWhat );
						break;
				}
			}
		}
		public void ShowMsg(string msg)
		{
			this.Response.Write(msg);
		}
		public bool IsCanLogin()
		{
			return true;

			if (this.RequestKey!=this.GetKey())
			{
				if (SystemConfig.IsDebug)
					return true;
					//throw new Exception("钥匙无效，不能执行您你请求。key="+this.GetKey() );
				else
					throw new Exception("钥匙无效，不能执行您你请求。" );
			}
			return true;
		}
		public string GetKey()
		{
			int a=int.Parse( DateTime.Now.ToString("hhMMdd") );
			decimal b=decimal.Parse( this.UserNo );
			
			decimal c =  a+b ;
			return c.ToString("0");
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

		public   void ToMsgPage(string mess)
		{		
			System.Web.HttpContext.Current.Session["info"]=mess;
			System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Port/InfoPage.aspx",true);
			return;
		}
		/// <summary>
		/// 切换到信息也面。
		/// </summary>
		/// <param name="mess"></param>
		public   void ToErrorPage(string mess)
		{		
			System.Web.HttpContext.Current.Session["info"]=mess;
			System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Comm/Port/InfoPage.aspx");
			return;

		}
	}
}
