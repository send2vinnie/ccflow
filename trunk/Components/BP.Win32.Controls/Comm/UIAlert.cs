using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BP.Win32.Controls
{
	/// <summary>
	/// UIAlert 的摘要说明。
	/// </summary>
	public class UIAlert : System.Windows.Forms.Form
	{
		private BP.Win32.Controls.RTB rtb1;
		private BP.Win32.Controls.Lab lab1;
		private BP.Win32.Controls.Btn btn1;
		private BP.Win32.Controls.Btn btn2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panel1;
		private System.ComponentModel.IContainer components;

		public UIAlert()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UIAlert));
			this.rtb1 = new BP.Win32.Controls.RTB();
			this.lab1 = new BP.Win32.Controls.Lab();
			this.btn1 = new BP.Win32.Controls.Btn();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btn2 = new BP.Win32.Controls.Btn();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// rtb1
			// 
			this.rtb1.Dock = System.Windows.Forms.DockStyle.Top;
			this.rtb1.Location = new System.Drawing.Point(0, 0);
			this.rtb1.Name = "rtb1";
			this.rtb1.Size = new System.Drawing.Size(456, 120);
			this.rtb1.TabIndex = 0;
			this.rtb1.Text = "rtb1";
			// 
			// lab1
			// 
			this.lab1.BackColor = System.Drawing.Color.LightGray;
			this.lab1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lab1.Dock = System.Windows.Forms.DockStyle.Top;
			this.lab1.Location = new System.Drawing.Point(0, 120);
			this.lab1.Name = "lab1";
			this.lab1.Size = new System.Drawing.Size(456, 104);
			this.lab1.TabIndex = 1;
			this.lab1.Text = "lab1";
			// 
			// btn1
			// 
			this.btn1.BtnType = BP.Web.Controls.BtnType.Confirm;
			this.btn1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn1.ImageIndex = 0;
			this.btn1.ImageList = this.imageList1;
			this.btn1.Location = new System.Drawing.Point(152, 248);
			this.btn1.Name = "btn1";
			this.btn1.Size = new System.Drawing.Size(160, 23);
			this.btn1.TabIndex = 2;
			this.btn1.Text = "发送此信息给开发商";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btn2
			// 
			this.btn2.BtnType = BP.Web.Controls.BtnType.Confirm;
			this.btn2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn2.ImageIndex = 1;
			this.btn2.ImageList = this.imageList1;
			this.btn2.Location = new System.Drawing.Point(344, 248);
			this.btn2.Name = "btn2";
			this.btn2.TabIndex = 3;
			this.btn2.Text = "关闭";
			this.btn2.Click += new System.EventHandler(this.btn2_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Desktop;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.ForeColor = System.Drawing.Color.Yellow;
			this.panel1.Location = new System.Drawing.Point(0, 224);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(456, 8);
			this.panel1.TabIndex = 4;
			// 
			// UIAlert
			// 
			this.AcceptButton = this.btn1;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btn2;
			this.ClientSize = new System.Drawing.Size(456, 277);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btn2);
			this.Controls.Add(this.btn1);
			this.Controls.Add(this.lab1);
			this.Controls.Add(this.rtb1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UIAlert";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "提示信息";
			this.ResumeLayout(false);

		}
		#endregion

		private void UIAlert_Load(object sender, System.EventArgs e)
		{
		
		}
		public void Show(string msg)
		{
			this.lab1.Text= "\n1)您可以用匿名实名的方式把以上信息发送给我们．\n";
			this.lab1.Text+="2)您的问题能够帮助我们提高服务质量，改善我们的软件，找到潜在的问题．\n";
			this.lab1.Text+="3)给您的工作带来不便表示抱歉．\n";
			this.lab1.Text+="4)服务电话"+BP.SystemConfig.ServiceTel+"，Email:"+BP.SystemConfig.ServiceMail+"．\n";
			//this.lab1.Text+="5)如果您需要回访，请把您的联系方式写到问题内容里面。．\n";

			this.rtb1.Text = msg.Replace("@","\n@") ; 
			 
		}

		private void btn2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		 
 
	}
}
