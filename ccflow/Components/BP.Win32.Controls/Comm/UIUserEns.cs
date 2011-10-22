using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.En;

using BP.DA;
using BP.Web;
using BP.Sys ; 
using BP.DTS;

namespace BP.Win32.Comm
{
	/// <summary>
	/// UIUserEns 的摘要说明。
	/// </summary>
	public class UIUserEns : BP.Win32.PageBase
	{
		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private BP.Win32.Controls.LB lb1;
		private System.Windows.Forms.ToolBarButton Btn_Edit;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton Btn_Close;
		private System.ComponentModel.IContainer components;

		public UIUserEns()
		{
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIUserEns));
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.Btn_Edit = new System.Windows.Forms.ToolBarButton();
			this.Btn_Close = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.lb1 = new BP.Win32.Controls.LB();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.SuspendLayout();
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.Btn_Edit,
																						  this.Btn_Close});
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.ImageList = this.imageList1;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(292, 41);
			this.bpToolbar1.TabIndex = 0;
			this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
			// 
			// Btn_Edit
			// 
			this.Btn_Edit.ImageIndex = 0;
			this.Btn_Edit.Tag = "Edit";
			this.Btn_Edit.Text = "编辑选择的表";
			// 
			// Btn_Close
			// 
			this.Btn_Close.ImageIndex = 1;
			this.Btn_Close.Tag = "Close";
			this.Btn_Close.Text = "关闭";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// lb1
			// 
			this.lb1.AddAllLocation = BP.Web.Controls.AddAllLocation.None;
			this.lb1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lb1.ItemHeight = 12;
			this.lb1.Location = new System.Drawing.Point(0, 41);
			this.lb1.Name = "lb1";
			this.lb1.Size = new System.Drawing.Size(292, 232);
			this.lb1.TabIndex = 1;
			this.lb1.DoubleClick += new System.EventHandler(this.lb1_DoubleClick);
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 251);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(292, 22);
			this.statusBar1.TabIndex = 2;
			this.statusBar1.Text = "statusBar1";
			// 
			// UIUserEns
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.lb1);
			this.Controls.Add(this.bpToolbar1);
			this.Name = "UIUserEns";
			this.Text = "UIUserEns";
			this.Load += new System.EventHandler(this.UIUserEns_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void UIUserEns_Load(object sender, System.EventArgs e)
		{
			
		}

		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			 
				 
			if (e.Button.Tag.ToString()=="Close")
			{
				this.Close();
				return ;
			}
			else
			{
				ShowEns();
			}
		
		}
		public void ShowEns()
		{
			try
			{
				Entities ens = DA.ClassFactory.GetEns( this.lb1.SelectedValueString ) ;
				//SysEnsUAC uac = new SysEnsUAC(this.lb1.SelectedValueString,WebUser.No) ; 
				this.InvokUIEns(ens,this);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message) ; 
			}
		}
		/// <summary>
		/// 用户表维护
		/// </summary>
		/// <param name="type"></param>
		public void BindByEnType(EnType type)
		{
			this.Text="用户表维护";
			this.statusBar1.Text="感谢你选择ccflow软件。";
            //SysEns ens = new SysEns();
            //ens.Retrieve(type);

            //this.lb1.BindEns(ens, SysEnPowerAbleAttr.Name, SysEnPowerAbleAttr.EnsEnsName);
			this.ShowDialog();
		}

		private void lb1_DoubleClick(object sender, System.EventArgs e)
		{
			 
			this.ShowEns();
		}
	}
}
