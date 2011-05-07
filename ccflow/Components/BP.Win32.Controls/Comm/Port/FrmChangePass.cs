using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BP.En;

using BP.Port;
namespace BP.Win32.Controls.Comm
{
	/// <summary>
	/// FrmChangePass 的摘要说明。
	/// </summary>
	public class FrmChangePass : PageBase
	{
		private BP.Win32.Controls.Lab lab1;
		private BP.Win32.Controls.TB tb1;
		private BP.Win32.Controls.TB tb2;
		private BP.Win32.Controls.Lab lab2;
		private BP.Win32.Controls.TB tb3;
		private BP.Win32.Controls.Lab lab3;
		private BP.Win32.Controls.Btn btn1;
		private BP.Win32.Controls.Btn btn2;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;

		public FrmChangePass()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmChangePass));
			this.lab1 = new BP.Win32.Controls.Lab();
			this.tb1 = new BP.Win32.Controls.TB();
			this.tb2 = new BP.Win32.Controls.TB();
			this.lab2 = new BP.Win32.Controls.Lab();
			this.tb3 = new BP.Win32.Controls.TB();
			this.lab3 = new BP.Win32.Controls.Lab();
			this.btn1 = new BP.Win32.Controls.Btn();
			this.btn2 = new BP.Win32.Controls.Btn();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// lab1
			// 
			this.lab1.Location = new System.Drawing.Point(24, 32);
			this.lab1.Name = "lab1";
			this.lab1.Size = new System.Drawing.Size(72, 23);
			this.lab1.TabIndex = 0;
			this.lab1.Text = "原密码";
			// 
			// tb1
			// 
			this.tb1.Location = new System.Drawing.Point(120, 32);
			this.tb1.Name = "tb1";
			this.tb1.PasswordChar = '*';
			this.tb1.TabIndex = 1;
			this.tb1.Text = "tb1";
			// 
			// tb2
			// 
			this.tb2.Location = new System.Drawing.Point(120, 80);
			this.tb2.Name = "tb2";
			this.tb2.PasswordChar = '*';
			this.tb2.TabIndex = 3;
			this.tb2.Text = "tb2";
			// 
			// lab2
			// 
			this.lab2.Location = new System.Drawing.Point(24, 80);
			this.lab2.Name = "lab2";
			this.lab2.Size = new System.Drawing.Size(72, 23);
			this.lab2.TabIndex = 2;
			this.lab2.Text = "新密码";
			// 
			// tb3
			// 
			this.tb3.Location = new System.Drawing.Point(120, 120);
			this.tb3.Name = "tb3";
			this.tb3.PasswordChar = '*';
			this.tb3.TabIndex = 5;
			this.tb3.Text = "tb3";
			// 
			// lab3
			// 
			this.lab3.Location = new System.Drawing.Point(24, 120);
			this.lab3.Name = "lab3";
			this.lab3.Size = new System.Drawing.Size(72, 23);
			this.lab3.TabIndex = 4;
			this.lab3.Text = "确  认";
			// 
			// btn1
			// 
			this.btn1.BtnType = BP.Web.Controls.BtnType.Confirm;
			this.btn1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn1.ImageIndex = 0;
			this.btn1.ImageList = this.imageList1;
			this.btn1.Location = new System.Drawing.Point(56, 192);
			this.btn1.Name = "btn1";
			this.btn1.TabIndex = 6;
			this.btn1.Text = "确认";
			this.btn1.Click += new System.EventHandler(this.btn1_Click);
			// 
			// btn2
			// 
			this.btn2.BtnType = BP.Web.Controls.BtnType.Cancel;
			this.btn2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn2.ImageIndex = 1;
			this.btn2.ImageList = this.imageList1;
			this.btn2.Location = new System.Drawing.Point(152, 192);
			this.btn2.Name = "btn2";
			this.btn2.TabIndex = 7;
			this.btn2.Text = "取消";
			this.btn2.Click += new System.EventHandler(this.btn2_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// FrmChangePass
			// 
			this.AcceptButton = this.btn1;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btn2;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.btn2);
			this.Controls.Add(this.btn1);
			this.Controls.Add(this.tb3);
			this.Controls.Add(this.lab3);
			this.Controls.Add(this.tb2);
			this.Controls.Add(this.lab2);
			this.Controls.Add(this.tb1);
			this.Controls.Add(this.lab1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmChangePass";
			this.Text = "修改密码";
			this.Load += new System.EventHandler(this.FrmChangePass_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FrmChangePass_Load(object sender, System.EventArgs e)
		{
		
		}

		private void btn2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btn1_Click(object sender, System.EventArgs e)
		{
			BP.Port.Emp emp = new Emp(Web.WebUser.No);
			if (emp.Pass!=this.tb1.Text)
			{
				this.ResponseWriteRedMsg("老密码不正确..");
				return;
			}
			if (this.tb2.Text!=this.tb3.Text)
			{
				this.ResponseWriteRedMsg("两次输入密码不一致.");
				return;
			}
			emp.Pass= this.tb3.Text;
			emp.Update();

			this.ResponseWriteBlueMsg("密码修改成功,请记住您的新密码.");
		}
	}
}
