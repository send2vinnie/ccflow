using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using BP.En;

using BP.DA;
using BP.Web;
using BP.Sys;
using BP.DTS;

namespace BP.DTS
{
	/// <summary>
	/// HomeForm 的摘要说明。
	/// </summary>
	public class FrmDTS: BP.Win32.PageBase 
	{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton Btn_New;
		private System.Windows.Forms.ToolBarButton Btn_Save;
		private System.Windows.Forms.ToolBarButton Btn_Delete;
		private System.Windows.Forms.ToolBarButton Btn_Exit;
		private BP.Win32.Controls.DG dg1;
		private System.IO.FileSystemWatcher fileSystemWatcher2;
		private System.IO.FileSystemWatcher fileSystemWatcher1;
		private System.Windows.Forms.ToolBarButton Btn_Invok;
		private System.Windows.Forms.StatusBar statusBar1;

//		private System.Windows.Forms.NotifyIcon notifyIcon1;

		#region 变量
		//private bool _configChange1 = false;
		//private bool _configChange2 = false;
		//private int  msgshow=5;
		#endregion

		/// <summary>
		/// Btn_Start
		/// </summary>
		private System.Windows.Forms.ToolBarButton Btn_Start ;
		/// <summary>
		/// IconStop
		/// </summary>
		private System.Windows.Forms.ToolBarButton Btn_Stop;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton Btn_Edit;
		private System.Windows.Forms.ToolBarButton Btn_Refurbish;
		private System.Windows.Forms.Timer timer1;
		
		private System.ComponentModel.IContainer components;

		public FrmDTS()
		{
//			this.fileSystemWatcher1.Path="D:\\WebApp\\Components\\DTS\\" ;
////			this.fileSystemWatcher1.Filter="*.txt";
//			this.fileSystemWatcher1.Filter="HomeForm.cs";

			
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
//			this.IconStop = (Icon)this.notifyIcon1.Icon.Clone();

			this.Text=SystemConfig.SysName+" - 调度";
			BP.DTS.SysDTSs.InitDataIOEns();
			this.statusBar1.Text="请您选择要执行的纪录，运行执行按钮。";

			
			this.BindData(true);
			this.Btn_Stop.Enabled=false;
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
		private void BindData(bool IsFirstBind)
		{
			SysDTSs ens = new SysDTSs();
			ens.RetrieveAll();
			//this.dg1.ReSetDataSource(ens) ; 
			this.dg1.BindEnsThisOnly(ens,false,IsFirstBind );

		}
//		private readonly Icon IconStop;
		//private  Icon IconStop=null;

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

			//this.notifyIcon1.Dispose();

			Log.DefaultLogWriteLine(LogType.Info,"=================================>>调度关闭。。。");

		}


		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			
			//			this.fileSystemWatcher1.Path = Path.GetDirectoryName(Global.AppConfigPath);
			//			this.fileSystemWatcher1.Filter = Path.GetFileName(Global.AppConfigPath);
			//			this.fileSystemWatcher2.Path = Path.GetDirectoryName(Global.RefConfigPath);
			//			this.fileSystemWatcher2.Filter = Path.GetFileName(Global.RefConfigPath);

			//#if DEBUG
			//			TimerEnableFalse();
			//			
			//#else
			//		this.TimerStart();
			//		
			//#endif 

		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmDTS));
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.Btn_New = new System.Windows.Forms.ToolBarButton();
			this.Btn_Edit = new System.Windows.Forms.ToolBarButton();
			this.Btn_Save = new System.Windows.Forms.ToolBarButton();
			this.Btn_Refurbish = new System.Windows.Forms.ToolBarButton();
			this.Btn_Delete = new System.Windows.Forms.ToolBarButton();
			this.Btn_Start = new System.Windows.Forms.ToolBarButton();
			this.Btn_Stop = new System.Windows.Forms.ToolBarButton();
			this.Btn_Invok = new System.Windows.Forms.ToolBarButton();
			this.Btn_Exit = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.dg1 = new BP.Win32.Controls.DG();
			this.fileSystemWatcher2 = new System.IO.FileSystemWatcher();
			this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
			this.SuspendLayout();
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.Btn_New,
																						this.Btn_Edit,
																						this.Btn_Save,
																						this.Btn_Refurbish,
																						this.Btn_Delete,
																						this.Btn_Start,
																						this.Btn_Stop,
																						this.Btn_Invok,
																						this.Btn_Exit});
			this.toolBar1.ButtonSize = new System.Drawing.Size(55, 41);
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(608, 47);
			this.toolBar1.TabIndex = 0;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// Btn_New
			// 
			this.Btn_New.ImageIndex = 6;
			this.Btn_New.Tag = "New";
			this.Btn_New.Text = "新建";
			// 
			// Btn_Edit
			// 
			this.Btn_Edit.ImageIndex = 1;
			this.Btn_Edit.Tag = "Edit";
			this.Btn_Edit.Text = "编辑";
			this.Btn_Edit.ToolTipText = "编辑选择的调度。";
			// 
			// Btn_Save
			// 
			this.Btn_Save.ImageIndex = 4;
			this.Btn_Save.Tag = "Save";
			this.Btn_Save.Text = "保存";
			// 
			// Btn_Refurbish
			// 
			this.Btn_Refurbish.ImageIndex = 14;
			this.Btn_Refurbish.Tag = "Refurbish";
			this.Btn_Refurbish.Text = "刷新";
			// 
			// Btn_Delete
			// 
			this.Btn_Delete.ImageIndex = 8;
			this.Btn_Delete.Tag = "Delete";
			this.Btn_Delete.Text = "删除";
			// 
			// Btn_Start
			// 
			this.Btn_Start.ImageIndex = 9;
			this.Btn_Start.Tag = "Start";
			this.Btn_Start.Text = "开始";
			// 
			// Btn_Stop
			// 
			this.Btn_Stop.ImageIndex = 7;
			this.Btn_Stop.Tag = "Stop";
			this.Btn_Stop.Text = "停止";
			// 
			// Btn_Invok
			// 
			this.Btn_Invok.ImageIndex = 10;
			this.Btn_Invok.Tag = "Invok";
			this.Btn_Invok.Text = "手工调用";
			// 
			// Btn_Exit
			// 
			this.Btn_Exit.ImageIndex = 0;
			this.Btn_Exit.Tag = "Exit";
			this.Btn_Exit.Text = "退出";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// dg1
			// 
			this.dg1.DataMember = "";
			this.dg1.DGModel = BP.Win32.Controls.DGModel.None;
			this.dg1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dg1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dg1.IsDGReadonly = false;
			this.dg1.Location = new System.Drawing.Point(0, 47);
			this.dg1.Name = "dg1";
			this.dg1.Size = new System.Drawing.Size(608, 294);
			this.dg1.TabIndex = 1;
			// 
			// fileSystemWatcher2
			// 
			this.fileSystemWatcher2.EnableRaisingEvents = true;
			this.fileSystemWatcher2.NotifyFilter = System.IO.NotifyFilters.LastWrite;
			this.fileSystemWatcher2.SynchronizingObject = this;
			this.fileSystemWatcher2.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
			// 
			// fileSystemWatcher1
			// 
			this.fileSystemWatcher1.EnableRaisingEvents = true;
			this.fileSystemWatcher1.NotifyFilter = System.IO.NotifyFilters.LastWrite;
			this.fileSystemWatcher1.SynchronizingObject = this;
			this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 319);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(608, 22);
			this.statusBar1.TabIndex = 2;
			this.statusBar1.Text = "statusBar1";
			// 
			// timer1
			// 
			this.timer1.Interval = 60000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// FrmDTS
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(608, 341);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.dg1);
			this.Controls.Add(this.toolBar1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmDTS";
			this.Text = "调度2004-08-18";
			this.Resize += new System.EventHandler(this.FrmDTS_Resize);
			this.Load += new System.EventHandler(this.FrmDTS_Load);
			((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void Refurbish()
		{
			//this.BindData();
		}
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{ 
			//			int i = 0 ;
			try
			{	
				switch(e.Button.Text )
				{
					case "刷新":
						//	this.BindData();
						SysDTSs ens = new SysDTSs();
						ens.RetrieveAll();
						this.allDtss=ens;
						//this.dg1.BindEnsThisOnly(ens,false,true);
						this.dg1.HisEns=ens;
						this.dg1.Bind();
						//this.dg1.ReSetDataSource(ens) ; 
						break;
					case "开始": // 开始
						TimerStart();
						break;
					case "停止": // 停止
						TimerStop();
						break;
					case "删除": //删除。
						this.dg1.DeleteSelected();
						break;
					case "新建": // 新建
						this.InvokUIEn( new SysDTS(),false);
						break;
					case "编辑": // 编辑
						this.InvokUIEn( this.dg1.CurrentRowEn ,false);
						break;
					case "保存": // 保存
						this.dg1.Save();
						break;
					case "手工调用":  //调用。
						BP.DTS.SysDTS  dts =(BP.DTS.SysDTS)this.dg1.CurrentRowEn;
						DateTime dt1 =DateTime.Now;
						this.statusBar1.Text="开始执行:"+dts.Name +"，执行时间："+ DataType.CurrentDataTimess ;
						dts.Execute();
						TimeSpan span = DateTime.Now -  dt1;
						this.statusBar1.Text+="执行完毕! 结束时间："+DataType.CurrentDataTimess+" 历时："+span.Seconds;
						break;
					case "退出" ://退出
						this.Close();
						break;
					default:
						throw new Exception(e.Button.Text+"error");
				}
			}
			catch(Exception ex)  //未将对象引用设置到对象的实例
			{
				this.ResponseWriteRedMsg(ex.Message);
				//MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// 启动计时器
		/// </summary>
		private void TimerStart()
		{
			//this.notifyIcon1.Icon = this.Icon;
			this.timer1.Start();
			this.Btn_Stop.Enabled=true;
			this.Btn_Start.Enabled=false;
			//this.ShowInTaskBarEx
			/*
			this.miStop.Enabled = true;
			this.miStop2.Enabled = true;
			this.miStart.Enabled = false;
			this.miStart2.Enabled = false;
			*/
		}
		/// <summary>
		/// 停止计时器
		/// </summary>
		private void TimerStop()
		{
			//this.notifyIcon1.Icon = this.Icon;   
			this.timer1.Stop();
			this.Btn_Stop.Enabled=false;
			this.Btn_Start.Enabled=true;
		}
		/// <summary>
		/// 计时器Enabled
		/// </summary>
		private void TimerEnableFalse()
		{
			//this.notifyIcon1.Icon = this.IconStop;
			this.timer1.Stop();
		}

		private void lb1_DoubleClick(object sender, System.EventArgs e)
		{
			//	Entities ens = ClassFactory.GetEns(this.lb1.SelectedValue.ToString());
			//	ens.RetrieveAll();
			//	this.dg1.BindEns(ens,false);
			//  this.dg1.bin
		}
        private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.ShowInTaskbar = true;
            this.Show();
        }

		private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
		{
//			if(sender.Equals(this.fileSystemWatcher1))
//				this._configChange1 = true;
//			else
//				this._configChange2 = true;
		}
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			RefreshsBar();
			this.allDtss.Run();
		}
		 
		/// <summary>
		/// 当前系统中全部的DTS
		/// </summary>
		private SysDTSs _allDtss=null;
		/// <summary>
		/// 当前系统中全部的DTS
		/// </summary>
		public SysDTSs allDtss
		{
			get
			{
				if (_allDtss==null)
				{
					_allDtss= new SysDTSs();
					_allDtss.RetrieveAll();
				}
				return _allDtss;
			}
			set
			{
				_allDtss=value;
			}
		}
		 
		/// <summary>
		/// 重新加载配置(刷新)
		/// </summary>
		private void RefreshsBar()
		{
			//this.BindData(false);
			this.statusBar1.Text= "当前时间:" + System.DateTime.Now.ToString("yyyy年M月d日,HH:mm");
			return ;

//
//			if( this._configChange1 || this._configChange2)
//			{
//				this.msgshow = 5;
//				string msg = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm")+", 配置文件";
//				if(this._configChange1)
//					msg += "["+this.fileSystemWatcher1.Path+"\\"+fileSystemWatcher1.Filter+"]";
//				if(this._configChange2)
//					msg += "["+this.fileSystemWatcher2.Path+"\\"+fileSystemWatcher2.Filter+"]";
//				msg += "已更改！";
//				this.statusBar1.Text+= "配置已更改！" ;
//				if(Global.LoadConfig())
//				{
//					_configChange1 = false;
//					_configChange2 = false;
//					msg+="加载新配置成功！";
//					msg += " 重新加载配置成功！";
//				}
//				else
//				{
//					this.statusBar1.Text +="加载新配置失败！";
//					msg += " 重新加载配置失败！";
//					this.msgshow = 1440;//24小时
//				}
//				msg += " 登陆用户："+Global.User +"["+Global.UserID+"]";
//				Log.DefaultLogWriteLine(LogType.Info,msg);
//			}
//			else
//			{
//				if(msgshow<1)
//				{
//					//this.sBarVersion.Text = "";
//				}
//				else
//				{
//
//					msgshow--;
//				}
//			}
		}
		

		private void FrmDTS_Load(object sender, System.EventArgs e)
		{
			//this.notifyIcon1.Icon = this.Icon;
			//this.ShowInTaskbar=true;
			this.TimerStart();

			Log.DefaultLogWriteLine(LogType.Info,"=================================>>调度开始运行。。。");
			//把所有没有初始化进去的.
		}

		private void FrmDTS_MinimumSizeChanged(object sender, System.EventArgs e)
		{
			//this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
			//this.notifyIcon1.
			//this.ShowInTaskbar=false;
			//this.notifyIcon1.Icon = this.Icon;
			//this.ShowInTaskbar=false;
			//this.ResponseWriteBlueMsg_SaveOK();
		
		}

		private void notifyIcon1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
		
//			this.Visible = true;
//			this.WindowState = FormWindowState.Normal;
//			this.notifyIcon1.Visible = false;

		}

		private void notifyIcon1_Click(object sender, System.EventArgs e)
		{
			this.Visible = true;
			this.WindowState = FormWindowState.Normal;
			//this.notifyIcon1.Visible = false;
		}

		private void FrmDTS_Resize(object sender, System.EventArgs e)
		{
//			if (this.WindowState == FormWindowState.Minimized) 
//			{
//				this.notifyIcon1.Icon = this.Icon;
//				this.ShowInTaskbar=true;
//				this.Visible = false;
//				 
//			}
//			else
//			{
//				this.notifyIcon1.Icon = this.Icon;
//				this.ShowInTaskbar=true;
//				this.Visible = true;
//			}


		//	this.notifyIcon1.Icon = this.Icon;
			this.ShowInTaskbar=true;

		}

	 

		
		
	}
}
