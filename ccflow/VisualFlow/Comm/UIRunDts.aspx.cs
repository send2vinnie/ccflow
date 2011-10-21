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
using BP.Web.Controls ;

namespace BP.Web.WF.Comm
{
	/// <summary>
	/// UIRunDTS 的摘要说明。
	/// </summary>
	public partial class UIRunDTS : WebPage
	{

		public string DTSNo
		{
			get
			{
				return this.Request.QueryString["PK"];
			}
		}	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (this.IsPostBack==false)
			{
				DTS.SysDTS dts = new BP.DTS.SysDTS(this.DTSNo);
				this.BPToolBar1.AddLab("ss","调度:"+dts.Name );
				this.BPToolBar1.AddBtn(NamesOfBtn.DTS);
				this.BPToolBar1.AddBtn(NamesOfBtn.Close);				
			}
			this.BPToolBar1.ButtonClick+=new EventHandler(BPToolBar1_ButtonClick);
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

		private void BPToolBar1_ButtonClick(object sender, EventArgs e)
		{
			try
			{
				BP.Web.Controls.ToolbarBtn btn =(ToolbarBtn)sender;

				switch(btn.ID)
				{
					case NamesOfBtn.DTS:
						try
						{
							this.Label1.Text="正在运行，请等候。。。。";
							this.Label2.Text="运行时间: "+DateTime.Now.ToString("HH-mm-ss");
							DTS.SysDTS dts = new BP.DTS.SysDTS(this.DTSNo);
							dts.Run();
                            this.Label2.Text += "结束时间: " + DateTime.Now.ToString("HH-mm-ss");
                            this.Label1.Text = "执行完毕！！！";
						}
						catch(Exception ex)
						{
							throw new Exception("@手工执行调度出现下列异常"+ex.Message);
						}


						break;
					case NamesOfBtn.Close:
						this.WinClose();
						break;
				}
			}
			catch(Exception ex)
			{
				this.Alert(ex.Message) ; 
			}

		}
	}
}
