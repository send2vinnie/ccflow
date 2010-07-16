using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using BP.DTS;

using BP.CTI;
using BP.CTI.App;

namespace BP.CTI
{
	/// <summary>
	/// FrmSRV 的摘要说明。
	/// </summary>
	public class FrmSRV :  BP.Win32.PageBase
	{
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btn_SrvPause;
		private System.Windows.Forms.Button btn_SrvStart;
		private System.Windows.Forms.Button btn_SrvStop;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btn_DBOut;
		private System.Windows.Forms.Button btn_DBContinue;
		private System.Windows.Forms.Button Btn_Hand;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.StatusBarPanel statusBarPanel3;
		private System.ComponentModel.IContainer components;

		public FrmSRV()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;


			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmSRV));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btn_SrvPause = new System.Windows.Forms.Button();
			this.btn_SrvStart = new System.Windows.Forms.Button();
			this.btn_SrvStop = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel3 = new System.Windows.Forms.StatusBarPanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.Btn_Hand = new System.Windows.Forms.Button();
			this.btn_DBOut = new System.Windows.Forms.Button();
			this.btn_DBContinue = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btn_SrvPause);
			this.groupBox1.Controls.Add(this.btn_SrvStart);
			this.groupBox1.Controls.Add(this.btn_SrvStop);
			this.groupBox1.Location = new System.Drawing.Point(60, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(152, 152);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "服务运行控制";
			// 
			// btn_SrvPause
			// 
			this.btn_SrvPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_SrvPause.ImageIndex = 9;
			this.btn_SrvPause.ImageList = this.imageList1;
			this.btn_SrvPause.Location = new System.Drawing.Point(32, 64);
			this.btn_SrvPause.Name = "btn_SrvPause";
			this.btn_SrvPause.TabIndex = 2;
			this.btn_SrvPause.Text = "暂停";
			this.btn_SrvPause.Click += new System.EventHandler(this.btn_Click);
			// 
			// btn_SrvStart
			// 
			this.btn_SrvStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_SrvStart.ImageIndex = 8;
			this.btn_SrvStart.ImageList = this.imageList1;
			this.btn_SrvStart.Location = new System.Drawing.Point(32, 32);
			this.btn_SrvStart.Name = "btn_SrvStart";
			this.btn_SrvStart.TabIndex = 0;
			this.btn_SrvStart.Text = "开始";
			this.btn_SrvStart.Click += new System.EventHandler(this.btn_Click);
			// 
			// btn_SrvStop
			// 
			this.btn_SrvStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_SrvStop.ImageIndex = 12;
			this.btn_SrvStop.ImageList = this.imageList1;
			this.btn_SrvStop.Location = new System.Drawing.Point(32, 96);
			this.btn_SrvStop.Name = "btn_SrvStop";
			this.btn_SrvStop.TabIndex = 7;
			this.btn_SrvStop.Text = "停止";
			this.btn_SrvStop.Click += new System.EventHandler(this.btn_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(24, 192);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(152, 24);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "在系统启动时自动运行";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.ImageIndex = 7;
			this.button1.ImageList = this.imageList1;
			this.button1.Location = new System.Drawing.Point(216, 224);
			this.button1.Name = "button1";
			this.button1.TabIndex = 4;
			this.button1.Text = "确定";
			this.button1.Click += new System.EventHandler(this.btn_Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.ImageIndex = 6;
			this.button2.ImageList = this.imageList1;
			this.button2.Location = new System.Drawing.Point(304, 224);
			this.button2.Name = "button2";
			this.button2.TabIndex = 5;
			this.button2.Text = "关闭";
			this.button2.Click += new System.EventHandler(this.btn_Click);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 60000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Text = "CTI服务控制器";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDown);
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 255);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.statusBarPanel1,
																						  this.statusBarPanel2,
																						  this.statusBarPanel3});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(408, 22);
			this.statusBar1.TabIndex = 6;
			this.statusBar1.Text = "statusBar1";
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.Text = "statusBarPanel1";
			this.statusBarPanel1.Width = 110;
			// 
			// statusBarPanel2
			// 
			this.statusBarPanel2.Text = "statusBarPanel2";
			this.statusBarPanel2.Width = 150;
			// 
			// statusBarPanel3
			// 
			this.statusBarPanel3.Text = "statusBarPanel3";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.Btn_Hand);
			this.groupBox2.Controls.Add(this.btn_DBOut);
			this.groupBox2.Controls.Add(this.btn_DBContinue);
			this.groupBox2.Location = new System.Drawing.Point(224, 32);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(144, 152);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "数据源工作模式控制";
			// 
			// Btn_Hand
			// 
			this.Btn_Hand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Btn_Hand.ImageIndex = 0;
			this.Btn_Hand.ImageList = this.imageList1;
			this.Btn_Hand.Location = new System.Drawing.Point(32, 104);
			this.Btn_Hand.Name = "Btn_Hand";
			this.Btn_Hand.TabIndex = 2;
			this.Btn_Hand.Text = "手动";
			this.Btn_Hand.Click += new System.EventHandler(this.btn_Click);
			// 
			// btn_DBOut
			// 
			this.btn_DBOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_DBOut.ImageIndex = 11;
			this.btn_DBOut.ImageList = this.imageList1;
			this.btn_DBOut.Location = new System.Drawing.Point(32, 72);
			this.btn_DBOut.Name = "btn_DBOut";
			this.btn_DBOut.TabIndex = 1;
			this.btn_DBOut.Text = "脱机";
			this.btn_DBOut.Click += new System.EventHandler(this.btn_Click);
			// 
			// btn_DBContinue
			// 
			this.btn_DBContinue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_DBContinue.ImageIndex = 10;
			this.btn_DBContinue.ImageList = this.imageList1;
			this.btn_DBContinue.Location = new System.Drawing.Point(32, 40);
			this.btn_DBContinue.Name = "btn_DBContinue";
			this.btn_DBContinue.TabIndex = 0;
			this.btn_DBContinue.Text = "联机";
			this.btn_DBContinue.Click += new System.EventHandler(this.btn_Click);
			// 
			// button3
			// 
			this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button3.ImageIndex = 1;
			this.button3.ImageList = this.imageList1;
			this.button3.Location = new System.Drawing.Point(120, 224);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(88, 23);
			this.button3.TabIndex = 8;
			this.button3.Text = "客户端";
			this.button3.Click += new System.EventHandler(this.btn_Click);
			// 
			// FrmSRV
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(408, 277);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmSRV";
			this.Text = "CTI服务控制器";
			this.Load += new System.EventHandler(this.FrmSRV_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel3)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
 
		/// <summary>
		/// 从新设置环境变量.
		/// </summary>
		public void ReSetEnv()
		{
			Sys.SysConfig sc = new Sys.SysConfig("DBWorkState");
			if (sc.Val=="1")
			{
				this.btn_DBContinue.Enabled=false;
				this.btn_DBOut.Enabled=true;
				this.statusBar1.Panels[2].Text="调度在执行,时间"+DateTime.Now.ToString("HH:mm");
				this.IsRunDTS=true;
			}
			else
			{
				this.btn_DBContinue.Enabled=true;
				this.btn_DBOut.Enabled=false;
				this.statusBar1.Panels[2].Text="调度停止,时间"+DateTime.Now.ToString("HH:mm");
				this.IsRunDTS=false;
			}

			this._SysDTSs=null; 
			Card.ResetParas(); 
		}
		public void SetState()
		{
			this.statusBar1.Panels[1].Text="连接"+Card.GetCHsCount(CHState.Connect)+"接通"+Card.GetCHsCount(CHState.Play)+"空闲"+Card.GetCHsCount(CHState.HangUp);
			switch(Card.HisCardWorkState)
			{
				case CardWorkState.Runing:
					this.btn_SrvStart.Enabled=false;
					this.btn_SrvPause.Enabled=true;
					this.btn_SrvStop.Enabled=true;
					this.statusBar1.Panels[0].Text="服务在运行。";
					break;
				case CardWorkState.Pause:
					this.btn_SrvStart.Enabled=true;
					this.btn_SrvPause.Enabled=false;
					this.btn_SrvStop.Enabled=true;
					this.statusBar1.Panels[0].Text="服务暂停。";
					break;
				case CardWorkState.Stop:
					this.btn_SrvStart.Enabled=true;
					this.btn_SrvPause.Enabled=false;
					this.btn_SrvStop.Enabled=false;
					this.statusBar1.Panels[0].Text="服务停止。";
					break;
			}
		}
		public void DoCall()
		{
			Card.HisCardWorkState=CardWorkState.Runing;
			Card.DoCall();
			this.SetState();
		}
		private void btn_Click(object sender, System.EventArgs e)
		{						
			try
			{
				System.Windows.Forms.Button btn = (Button)sender;
				switch(btn.Text)
				{
					case "开始":
						Card.HisCardWorkState=CardWorkState.Runing;					
						break;
					case "暂停":
						Card.HisCardWorkState=CardWorkState.Pause;					 
						break;
					case "停止":
						Card.HisCardWorkState=CardWorkState.Stop;						 
						break;
					case "联机":
						Sys.SysConfig sc = new Sys.SysConfig("DBWorkState");
						sc.Val="1";
						sc.Name="数据源工作状态";
						sc.Note="(联机1脱机0)";
						sc.Update();
						this.btn_DBContinue.Enabled=false;
						this.btn_DBOut.Enabled=true;
						break;
					case "脱机":
						Sys.SysConfig conf = new Sys.SysConfig("DBWorkState");
						conf.Val="0";						
						conf.Name="数据源工作状态";
						conf.Note="(联机1脱机0)";
						conf.Update();
						this.btn_DBContinue.Enabled=true;
						this.btn_DBOut.Enabled=false;
						break;
					case "手动":
						if (Card.HisCardWorkState==CardWorkState.Runing)
						{
							throw new Exception("请停止呼叫服务在运行它。");
						}
						else
						{
							TelDTS dts = new TelDTS();
							dts.Do();
						}
						this.Information("手工执行完毕..");
						break;
					case "客户端":
						BP.CTI.FrmClient fc = new FrmClient();
						fc.Show();
						break;
					case "确定":
						this.Information("系统放入了图标栏目上,您通过双击图标就可以显示.");

						this.ShowInTaskbar=false;
						this.notifyIcon1.Icon=this.Icon;
						this.notifyIcon1.Visible=true;
						this.WindowState=FormWindowState.Minimized;

						break;
					case "关闭":
						if (Card.HisCardWorkState!=CardWorkState.Stop)						 
							throw new Exception("呼叫工作必须停止才能关闭。");

						if (this.Question("您确实要停止呼叫服务并退出系统吗?")==false)
							return;

						this.thisThread.Abort();
						Card.Disable();

						this.notifyIcon1.Icon=this.Icon;
						this.ShowInTaskbar=false;
						
						this.Close();
						break;
					default:
						throw new Exception("error "+btn.Text);
				}
				this.ReSetEnv();
				this.SetState();

			}
			catch(Exception ex)
			{
				this.ResponseWriteRedMsg(ex);
			}

		}
		/// <summary>
		/// 
		/// </summary>
		public System.Threading.Thread thisThread = null;

		private void FrmSRV_Load(object sender, System.EventArgs e)
		{
			ThreadStart ts =new ThreadStart( Card.Run )  ; 
			thisThread = new Thread(ts);
			thisThread.Start();
			this.timer1.Start();
			this.ReSetEnv();
			this.SetState();
		}

		private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
				this.WindowState = FormWindowState.Normal;
			this.Activate();
			this.ShowInTaskbar = true ;
			this.Show();
		}

		 
		private int dtsRunNum=0;
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			/* 设置板卡运行变量，以便相应远程的控制。 */
			this.SetState();
			if (this.IsRunDTS)
			{
				this.AllSysDTSs.Run();				
			}

			dtsRunNum++;
			if (dtsRunNum>4)
			{
				dtsRunNum=0;
				this.ReSetEnv();
			}
		}
		private bool IsRunDTS=false;
		private SysDTSs _SysDTSs=null;
		private SysDTSs AllSysDTSs
		{
			get
			{
				if (this._SysDTSs==null)
				{
					_SysDTSs= new SysDTSs();
					_SysDTSs.RetrieveAll();
				}
				return _SysDTSs;				 
			}
		}
		


		private void notifyIcon1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
				this.WindowState = FormWindowState.Normal;
			this.Activate();			 
		}

		private void timer2_Tick(object sender, System.EventArgs e)
		{
			this.ReSetEnv(); // 设置环境变量
		}
	}
}
