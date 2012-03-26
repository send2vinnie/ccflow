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
	/// UIRunDTS ��ժҪ˵����
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
				this.BPToolBar1.AddLab("ss","����:"+dts.Name );
				this.BPToolBar1.AddBtn(NamesOfBtn.DTS);
				this.BPToolBar1.AddBtn(NamesOfBtn.Close);				
			}
			this.BPToolBar1.ButtonClick+=new EventHandler(BPToolBar1_ButtonClick);
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
							this.Label1.Text="�������У���Ⱥ򡣡�����";
							this.Label2.Text="����ʱ��: "+DateTime.Now.ToString("HH-mm-ss");
							DTS.SysDTS dts = new BP.DTS.SysDTS(this.DTSNo);
							dts.Run();
                            this.Label2.Text += "����ʱ��: " + DateTime.Now.ToString("HH-mm-ss");
                            this.Label1.Text = "ִ����ϣ�����";
						}
						catch(Exception ex)
						{
							throw new Exception("@�ֹ�ִ�е��ȳ��������쳣"+ex.Message);
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
