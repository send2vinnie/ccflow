using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BP.CTI
{
	/// <summary>
	/// FrmTest 的摘要说明。
	/// </summary>
	public class FrmTest : BP.Win32.PageBase
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox textBox_Tel;
		private System.Windows.Forms.TextBox textBox_CH;
		private System.Windows.Forms.TextBox textBox_Doc;
		private System.Windows.Forms.TextBox textBox_JE;
		private System.ComponentModel.IContainer components;

		public FrmTest()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmTest));
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_Tel = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_CH = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_Doc = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.button3 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.textBox_JE = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "被叫号码";
			// 
			// textBox_Tel
			// 
			this.textBox_Tel.Location = new System.Drawing.Point(64, 24);
			this.textBox_Tel.Name = "textBox_Tel";
			this.textBox_Tel.Size = new System.Drawing.Size(144, 21);
			this.textBox_Tel.TabIndex = 1;
			this.textBox_Tel.Text = "13864938800";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(216, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 24);
			this.label2.TabIndex = 2;
			this.label2.Text = "通道号:";
			// 
			// textBox_CH
			// 
			this.textBox_CH.Location = new System.Drawing.Point(264, 24);
			this.textBox_CH.Name = "textBox_CH";
			this.textBox_CH.Size = new System.Drawing.Size(40, 21);
			this.textBox_CH.TabIndex = 3;
			this.textBox_CH.Text = "0";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "播放内容:";
			// 
			// textBox_Doc
			// 
			this.textBox_Doc.Location = new System.Drawing.Point(64, 56);
			this.textBox_Doc.Name = "textBox_Doc";
			this.textBox_Doc.Size = new System.Drawing.Size(424, 21);
			this.textBox_Doc.TabIndex = 5;
			this.textBox_Doc.Text = "jn_yqsb.wav";
			// 
			// button1
			// 
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.ImageIndex = 0;
			this.button1.ImageList = this.imageList1;
			this.button1.Location = new System.Drawing.Point(192, 264);
			this.button1.Name = "button1";
			this.button1.TabIndex = 6;
			this.button1.Text = "呼出";
			this.button1.Click += new System.EventHandler(this.but_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// button3
			// 
			this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button3.ImageIndex = 1;
			this.button3.ImageList = this.imageList1;
			this.button3.Location = new System.Drawing.Point(320, 264);
			this.button3.Name = "button3";
			this.button3.TabIndex = 8;
			this.button3.Text = "关闭";
			this.button3.Click += new System.EventHandler(this.but_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Desktop;
			this.panel1.Location = new System.Drawing.Point(0, 88);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(488, 8);
			this.panel1.TabIndex = 9;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(120, 104);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(368, 136);
			this.richTextBox1.TabIndex = 10;
			this.richTextBox1.Text = "richTextBox1";
			// 
			// textBox_JE
			// 
			this.textBox_JE.Location = new System.Drawing.Point(360, 24);
			this.textBox_JE.Name = "textBox_JE";
			this.textBox_JE.Size = new System.Drawing.Size(128, 21);
			this.textBox_JE.TabIndex = 12;
			this.textBox_JE.Text = "123.00";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(312, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "金额";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 104);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 136);
			this.pictureBox1.TabIndex = 13;
			this.pictureBox1.TabStop = false;
			// 
			// FrmTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(488, 301);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.textBox_JE);
			this.Controls.Add(this.textBox_Doc);
			this.Controls.Add(this.textBox_CH);
			this.Controls.Add(this.textBox_Tel);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "FrmTest";
			this.Text = "语音文件测试";
			this.Click += new System.EventHandler(this.but_Click);
			this.Load += new System.EventHandler(this.FrmTest_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FrmTest_Load(object sender, System.EventArgs e)
		{
			

			this.Text ="语音测试";
			//this.richTextBox1.Text="帮助;\n   此控制台用来处理,流程关系维护，数据定时导入导出以及导入导出的定时设置。\n 此公共程序许可给沂水国税使用。 2004-09-18 \n"; 
			this.richTextBox1.Text="帮助:\n";
			this.richTextBox1.Text+="1)您需要把语音文件放入指定的目录里面。\n";
			this.richTextBox1.Text+="2)单个文件定义直接输入文件名，例如:CB.TW \n";
			this.richTextBox1.Text+="3)多个文件定义文件名称之间需要用半角的逗号隔开比如 Welcome.TW,Thank.TW \n";
			this.richTextBox1.Text+="4)如果播放内容中用金额,月份变量,请用@JE@表示金额, @YF@表示变量, 例如: Hello.TW,@YF@,Month.TW,HF.TW,@JE@,Thanks.TW 。播放内容为，您好xx月份，金额为xx元，谢谢。\n";
		}

		private void but_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Windows.Forms.Button btn = (Button)sender;
				switch(btn.Text)
				{
					case "呼出":
						BP.CTI.App.CallList cl = new BP.CTI.App.CallList();
						cl.Tel =this.textBox_Tel.Text ; 
						cl.JE=float.Parse(this.textBox_JE.Text);
						cl.DoCalling(this.textBox_Doc.Text);
						Card.Test(int.Parse(this.textBox_CH.Text), cl );
						break;
					case "开始":
						break;
					case "关闭":
						this.Close();
						break;  
					default:
						throw new Exception("error :"+btn.Text);
				}
			}
			catch(Exception ex)
			{
				this.Warning(ex);
			} 
		}
	}
}
