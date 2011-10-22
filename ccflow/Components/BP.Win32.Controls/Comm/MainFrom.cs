using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BP.En;

namespace BP.Admin
{
	/// <summary>
	/// MainFrom 的摘要说明。
	/// </summary>
	public class MainFrom : BP.Win32.PageBase
	{
		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private System.Windows.Forms.ToolBarButton Btn_Exit;
		public  string bgPath ;
		private System.Windows.Forms.ToolBarButton Btn_DTS;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.ToolBarButton Btn_EnsUac;
		private System.Windows.Forms.ToolBarButton Btn_UIUserEns;
		private System.Windows.Forms.ToolBarButton Btn_UIAdminEns;
		private System.Windows.Forms.ToolBarButton Btn_ChangePass;
		private System.Windows.Forms.ToolBarButton Btn_SysOption;
		private System.Windows.Forms.ToolBarButton Btn_GenerSQL;
		private System.ComponentModel.IContainer components;

		public MainFrom()
		{
			//BP.WF.WFDTS.TransferAutoGenerWorkBreed();

			//BP.Tax.WF.DJ.KYDJInfoes

		//	BP.Tax.WF.DJ.KYDJInfoes.AutoGenerWorkBreed();
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainFrom));
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.Btn_UIUserEns = new System.Windows.Forms.ToolBarButton();
			this.Btn_UIAdminEns = new System.Windows.Forms.ToolBarButton();
			this.Btn_EnsUac = new System.Windows.Forms.ToolBarButton();
			this.Btn_DTS = new System.Windows.Forms.ToolBarButton();
			this.Btn_SysOption = new System.Windows.Forms.ToolBarButton();
			this.Btn_ChangePass = new System.Windows.Forms.ToolBarButton();
			this.Btn_Exit = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.Btn_GenerSQL = new System.Windows.Forms.ToolBarButton();
			this.SuspendLayout();
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.Btn_UIUserEns,
																						  this.Btn_UIAdminEns,
																						  this.Btn_EnsUac,
																						  this.Btn_DTS,
																						  this.Btn_SysOption,
																						  this.Btn_ChangePass,
																						  this.Btn_GenerSQL,
																						  this.Btn_Exit});
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.ImageList = this.imageList1;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(472, 41);
			this.bpToolbar1.TabIndex = 0;
			this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
			// 
			// Btn_UIUserEns
			// 
			this.Btn_UIUserEns.ImageIndex = 0;
			this.Btn_UIUserEns.Tag = "UIUserEns";
			this.Btn_UIUserEns.Text = "用户表维护";
			// 
			// Btn_UIAdminEns
			// 
			this.Btn_UIAdminEns.ImageIndex = 0;
			this.Btn_UIAdminEns.Tag = "UIAdminEns";
			this.Btn_UIAdminEns.Text = "Admin维护";
			// 
			// Btn_EnsUac
			// 
			this.Btn_EnsUac.ImageIndex = 0;
			this.Btn_EnsUac.Tag = "BP.Sys.SysEnsUACs";
			this.Btn_EnsUac.Text = "权限管理";
			// 
			// Btn_DTS
			// 
			this.Btn_DTS.ImageIndex = 1;
			this.Btn_DTS.Tag = "DTS";
			this.Btn_DTS.Text = "调度";
			// 
			// Btn_SysOption
			// 
			this.Btn_SysOption.ImageIndex = 4;
			this.Btn_SysOption.Text = "选项";
			this.Btn_SysOption.ToolTipText = "关于系统的运行的用户设置。";
			// 
			// Btn_ChangePass
			// 
			this.Btn_ChangePass.ImageIndex = 3;
			this.Btn_ChangePass.Text = "修改密码";
			// 
			// Btn_Exit
			// 
			this.Btn_Exit.ImageIndex = 2;
			this.Btn_Exit.Tag = "Exit";
			this.Btn_Exit.Text = "退出";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(0, 41);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(472, 268);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "richTextBox1";
			// 
			// Btn_GenerSQL
			// 
			this.Btn_GenerSQL.ImageIndex = 4;
			this.Btn_GenerSQL.Text = "生成SQL";
			// 
			// MainFrom
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(472, 309);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.bpToolbar1);
			this.MaximizeBox = false;
			this.Name = "MainFrom";
			this.Text = "公用组建";
			this.Load += new System.EventHandler(this.MainFrom_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void MainFrom_Load(object sender, System.EventArgs e)
		{
			this.Text = SystemConfig.SysName + "--控制台";
			//this.richTextBox1.Text="帮助;\n   此控制台用来处理,流程关系维护，数据定时导入导出以及导入导出的定时设置。\n 此公共程序许可给沂水国税使用。 2004-09-18 \n"; 
			this.richTextBox1.Text="用户须知\n\n";
			this.richTextBox1.Text+="    此控制台用来处理,流程关系维护，数据定时导入导出以及导入导出的定时设置。以及其他项目维护。\n";
			this.richTextBox1.Text+="    此控制台程序,提供给系统管理员，流程维护人员使用， 它是 b/s 程序的一个补充,解决了b/s 端效率问题。\n"; 
			this.richTextBox1.Text+="    此应用程序,有ccflow软件开发组开发，许可["+BP.SystemConfig.CustomerName+"]使用。\n\n" ; 
			this.richTextBox1.Text+="ccflow软件开发组\n";
			this.richTextBox1.Text+="    开发组主页: http://www.chichengsoft.com\n";
			this.richTextBox1.Text+="    技术支持文档:http://helper.chichengsoft.net/ E-mail: "+SystemConfig.ServiceMail+"\n";
			this.richTextBox1.Text+="    服务电话:"+SystemConfig.ServiceTel+"\n" ; 
			this.richTextBox1.Text+="    发布日期:2004年9月18号\n" ;
 
			bgPath = Application.StartupPath+"\\back.bin";
		}

		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(e.Button.Text)
			{
				case "生成SQL":
					
					//this.Information(
					break;
				case "选项":
					UAC uac = new UAC();
					uac.IsAdjunct=true;
					uac.IsDelete=true;
					uac.IsInsert=true;
					uac.IsUpdate=true;
					uac.IsView=true;
					this.InvokUIEns(new Sys.SysConfigs(),uac);
					break;
				case "修改密码":
					BP.Win32.Controls.Comm.FrmChangePass cps= new BP.Win32.Controls.Comm.FrmChangePass();
					cps.Show();					
					break;
				case "退出":
					this.Close();
					break;
				case "调度":
					this.InvokUIDTS();
					break;
				case "Admin维护":
					this.InvokUIUserEns(EnType.Admin);
					break;
				case "用户表维护":
					this.InvokUIUserEns(EnType.PowerAble);
					break;
				case "权限管理":
					Entities ens = BP.DA.ClassFactory.GetEns(e.Button.Tag.ToString())  ; 
					InvokUIEns(ens,this);
					break;
				default:
					break;
			}
		}
	}
}
