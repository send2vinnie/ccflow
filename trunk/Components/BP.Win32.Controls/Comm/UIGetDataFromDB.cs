using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BP.Win32.Controls.Comm
{
	public class UIGetDataFromDB : BP.Win32.PageBase
	{
		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private BP.Win32.Controls.RB rb4;
		private BP.Win32.Controls.RB rb5;
		private BP.Win32.Controls.RB rb3;
		private BP.Win32.Controls.RB rb1;
		private BP.Win32.Controls.RB rb2;
		private BP.Win32.Controls.GB gb1;
		private BP.Win32.Controls.TB tb1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.ComponentModel.IContainer components = null;

		public UIGetDataFromDB()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.rb4 = new BP.Win32.Controls.RB();
			this.rb5 = new BP.Win32.Controls.RB();
			this.rb3 = new BP.Win32.Controls.RB();
			this.rb1 = new BP.Win32.Controls.RB();
			this.rb2 = new BP.Win32.Controls.RB();
			this.gb1 = new BP.Win32.Controls.GB();
			this.tb1 = new BP.Win32.Controls.TB();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.gb1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(472, 42);
			this.bpToolbar1.TabIndex = 0;
			// 
			// rb4
			// 
			this.rb4.Location = new System.Drawing.Point(24, 152);
			this.rb4.Name = "rb4";
			this.rb4.TabIndex = 5;
			this.rb4.Text = "OLE联接";
			// 
			// rb5
			// 
			this.rb5.Location = new System.Drawing.Point(24, 192);
			this.rb5.Name = "rb5";
			this.rb5.TabIndex = 6;
			this.rb5.Text = "ODBC联接";
			// 
			// rb3
			// 
			this.rb3.Location = new System.Drawing.Point(24, 128);
			this.rb3.Name = "rb3";
			this.rb3.TabIndex = 4;
			this.rb3.Text = "SQLServer联接";
			// 
			// rb1
			// 
			this.rb1.Location = new System.Drawing.Point(24, 40);
			this.rb1.Name = "rb1";
			this.rb1.Size = new System.Drawing.Size(152, 24);
			this.rb1.TabIndex = 2;
			this.rb1.Text = "应用程序数据库";
			// 
			// rb2
			// 
			this.rb2.Location = new System.Drawing.Point(16, 88);
			this.rb2.Name = "rb2";
			this.rb2.TabIndex = 3;
			this.rb2.Text = "Oracle联接";
			// 
			// gb1
			// 
			this.gb1.Controls.Add(this.rb4);
			this.gb1.Controls.Add(this.rb5);
			this.gb1.Controls.Add(this.rb3);
			this.gb1.Controls.Add(this.rb1);
			this.gb1.Controls.Add(this.rb2);
			this.gb1.Location = new System.Drawing.Point(0, 64);
			this.gb1.Name = "gb1";
			this.gb1.Size = new System.Drawing.Size(184, 280);
			this.gb1.TabIndex = 7;
			this.gb1.TabStop = false;
			this.gb1.Text = "选择数据源";
			// 
			// tb1
			// 
			this.tb1.Location = new System.Drawing.Point(216, 112);
			this.tb1.Multiline = true;
			this.tb1.Name = "tb1";
			this.tb1.Size = new System.Drawing.Size(216, 216);
			this.tb1.TabIndex = 8;
			this.tb1.Text = "tb1\r\n\r\ndsfsadf\r\n\r\ndsfasd";
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 439);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(472, 22);
			this.statusBar1.TabIndex = 9;
			this.statusBar1.Text = "statusBar1";
			// 
			// UIGetDataFromDB
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(472, 461);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.tb1);
			this.Controls.Add(this.gb1);
			this.Controls.Add(this.bpToolbar1);
			this.Name = "UIGetDataFromDB";
			this.Load += new System.EventHandler(this.UIGetDataFromDB_Load);
			this.gb1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void UIGetDataFromDB_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}

