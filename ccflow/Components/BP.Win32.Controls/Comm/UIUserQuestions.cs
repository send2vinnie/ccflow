using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BP.Win32.Controls
{
	/// <summary>
	/// UIUserQuestions 的摘要说明。
	/// </summary>
	public class UIUserQuestions : System.Windows.Forms.Form
	{
		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UIUserQuestions()
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
			this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// bpToolbar1
			// 
			this.bpToolbar1.DropDownArrows = true;
			this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
			this.bpToolbar1.Name = "bpToolbar1";
			this.bpToolbar1.ShowToolTips = true;
			this.bpToolbar1.Size = new System.Drawing.Size(292, 42);
			this.bpToolbar1.TabIndex = 0;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(0, 42);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(292, 231);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "richTextBox1";
			// 
			// UIUserQuestions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.bpToolbar1);
			this.Name = "UIUserQuestions";
			this.Text = "UIUserQuestions";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
