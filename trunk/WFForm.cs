using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BP.Win.WF
{
	/// <summary>
	/// WFForm 的摘要说明。
	/// </summary>
	public class WFForm : System.Windows.Forms.Form
	{
        public string ToE(string no,string chVal)
        {
           // return no;
            return BP.Sys.Language.GetValByUserLang(no, chVal);
        }
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WFForm()
		{
			InitializeComponent();
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
            this.SuspendLayout();
            // 
            // WFForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(633, 440);
            this.Name = "WFForm";
            this.ShowInTaskbar = false;
            this.Text = "WFForm";
            this.ResumeLayout(false);

		}
		#endregion
	}
}
