using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
namespace BP.Win.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.Form))]
	public class FCheckedList : System.Windows.Forms.Form
	{
		private BP.Win.Controls.CLB baseCheckedListBox1;
		private BP.Win.Controls.Btn btnOk;
		private BP.Win.Controls.Btn btnCancel;
		private BP.Win.Controls.Btn btnUp;
		private BP.Win.Controls.Btn btnDown;
		private BP.Win.Controls.Btn btnAll;
		private BP.Win.Controls.Btn btnNot;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FCheckedList()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
		protected string _checks = "";
		public    string Checks
		{
			get
			{
				return _checks;
			}
		}
		public void BindData(DataTable tb)
		{
			this.baseCheckedListBox1.Items.Clear();
			for(int i=0;i<tb.Columns.Count;i++)
			{
				this.baseCheckedListBox1.Items.Add(tb.Columns[i].ColumnName);
			}
		}
		public void SetChecked(string checks)
		{
		}
		public DialogResult FrmShow()
		{
			return this.ShowDialog();
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
			this.baseCheckedListBox1 = new BP.Win.Controls.CLB();
			this.btnOk = new BP.Win.Controls.Btn();
			this.btnCancel = new BP.Win.Controls.Btn();
			this.btnUp = new BP.Win.Controls.Btn();
			this.btnDown = new BP.Win.Controls.Btn();
			this.btnAll = new BP.Win.Controls.Btn();
			this.btnNot = new BP.Win.Controls.Btn();
			this.SuspendLayout();
			// 
			// baseCheckedListBox1
			// 
			this.baseCheckedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.baseCheckedListBox1.Location = new System.Drawing.Point(0, 0);
			this.baseCheckedListBox1.Name = "baseCheckedListBox1";
			this.baseCheckedListBox1.Size = new System.Drawing.Size(256, 260);
			this.baseCheckedListBox1.TabIndex = 0;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(264, 208);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(72, 22);
			this.btnOk.TabIndex = 5;
			this.btnOk.Text = "确定";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(264, 232);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 22);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnUp
			// 
			this.btnUp.Location = new System.Drawing.Point(264, 8);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(72, 22);
			this.btnUp.TabIndex = 1;
			this.btnUp.Text = "上移";
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnDown
			// 
			this.btnDown.Location = new System.Drawing.Point(264, 32);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(72, 22);
			this.btnDown.TabIndex = 2;
			this.btnDown.Text = "下移";
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnAll
			// 
			this.btnAll.Location = new System.Drawing.Point(264, 64);
			this.btnAll.Name = "btnAll";
			this.btnAll.Size = new System.Drawing.Size(72, 22);
			this.btnAll.TabIndex = 3;
			this.btnAll.Text = "全选";
			this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
			// 
			// btnNot
			// 
			this.btnNot.Location = new System.Drawing.Point(264, 88);
			this.btnNot.Name = "btnNot";
			this.btnNot.Size = new System.Drawing.Size(72, 22);
			this.btnNot.TabIndex = 4;
			this.btnNot.Text = "反选";
			this.btnNot.Click += new System.EventHandler(this.btnNot_Click);
			// 
			// F_CheckedList
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(344, 261);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.baseCheckedListBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnAll);
			this.Controls.Add(this.btnNot);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "F_CheckedList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "列表";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			_checks = "";
			this.DialogResult=DialogResult.Cancel;
			this.Close();
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			_checks = this.baseCheckedListBox1.GetCheckToStr();
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void btnUp_Click(object sender, System.EventArgs e)
		{
			this.baseCheckedListBox1.Up();
		}

		private void btnDown_Click(object sender, System.EventArgs e)
		{
			this.baseCheckedListBox1.Down();
		}

		private void btnAll_Click(object sender, System.EventArgs e)
		{
			this.baseCheckedListBox1.SetAllChecked(true);
		}

		private void btnNot_Click(object sender, System.EventArgs e)
		{
			this.baseCheckedListBox1.Not();
		}
	}
}
