using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	/// <summary>
	/// FrmDGEditBase 的摘要说明。
	/// </summary>
	public class FrmDGEditBase : Form
	{
		private BP.Win.Controls.DG dg1;
		private BP.Win.Controls.Btn btnOpen;
		private BP.Win.Controls.Btn btnDel;
		private BP.Win.Controls.Btn btnClose;
		private BP.Win.Controls.Btn btnSave;
		private BP.Win.Controls.Btn btnClear;
		private BP.Win.Controls.DDL ddl1;
		private System.Windows.Forms.ToolTip toolTip1;
		private BP.Win.Controls.Btn btnFind;
		private System.ComponentModel.IContainer components;

		public FrmDGEditBase()
		{
			InitializeComponent();
		}

		#region Windows 窗体设计器生成的代码
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
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.dg1 = new BP.Win.Controls.DG();
			this.btnOpen = new BP.Win.Controls.Btn();
			this.btnDel = new BP.Win.Controls.Btn();
			this.btnClose = new BP.Win.Controls.Btn();
			this.btnSave = new BP.Win.Controls.Btn();
			this.btnClear = new BP.Win.Controls.Btn();
			this.ddl1 = new BP.Win.Controls.DDL();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.btnFind = new BP.Win.Controls.Btn();
			((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
			this.SuspendLayout();
			// 
			// dg1
			// 
			this.dg1.AllowDelete = true;
			this.dg1.AllowEdit = true;
			this.dg1.AllowNew = true;
			this.dg1.AlternatingBackColor = System.Drawing.Color.LightGray;
			this.dg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dg1.BackColor = System.Drawing.Color.DarkGray;
			this.dg1.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dg1.CaptionBackColor = System.Drawing.Color.Silver;
			this.dg1.CaptionFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dg1.CaptionForeColor = System.Drawing.Color.Black;
			this.dg1.CaptionText = "记录数";
			this.dg1.DataMember = "";
			this.dg1.FKDataSource = null;
			this.dg1.ForeColor = System.Drawing.Color.Black;
			this.dg1.GridLineColor = System.Drawing.Color.Black;
			this.dg1.HeaderBackColor = System.Drawing.Color.Silver;
			this.dg1.HeaderForeColor = System.Drawing.Color.Black;
			this.dg1.LinkColor = System.Drawing.Color.Navy;
			this.dg1.Location = new System.Drawing.Point(0, 32);
			this.dg1.Name = "dg1";
			this.dg1.ParentRowsBackColor = System.Drawing.Color.White;
			this.dg1.ParentRowsForeColor = System.Drawing.Color.Black;
			this.dg1.SelectionBackColor = System.Drawing.Color.Navy;
			this.dg1.SelectionForeColor = System.Drawing.Color.White;
			this.dg1.SelectSQL = "";
			this.dg1.Size = new System.Drawing.Size(560, 368);
			this.dg1.TabIndex = 7;
			this.dg1.DoubleClick += new System.EventHandler(this.dg1_DoubleClick);
			// 
			// btnOpen
			// 
			this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpen.Location = new System.Drawing.Point(232, 7);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(48, 22);
			this.btnOpen.TabIndex = 1;
			this.btnOpen.Text = "刷新";
			this.toolTip1.SetToolTip(this.btnOpen, "从数据库中重新加载");
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnDel
			// 
			this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDel.Location = new System.Drawing.Point(344, 7);
			this.btnDel.Name = "btnDel";
			this.btnDel.Size = new System.Drawing.Size(48, 22);
			this.btnDel.TabIndex = 3;
			this.btnDel.Text = "删除";
			this.toolTip1.SetToolTip(this.btnDel, "删除当前选择的记录");
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(512, 7);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(48, 22);
			this.btnClose.TabIndex = 6;
			this.btnClose.Text = "关闭";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(288, 7);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(48, 22);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "保存";
			this.toolTip1.SetToolTip(this.btnSave, "保存当前表格中所有数据");
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.Location = new System.Drawing.Point(400, 7);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(48, 22);
			this.btnClear.TabIndex = 4;
			this.btnClear.Text = "清除";
			this.toolTip1.SetToolTip(this.btnClear, "删除当前表格中所有数据");
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// ddl1
			// 
			this.ddl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ddl1.CurrentEditIndex = -1;
			this.ddl1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddl1.DropDownWidth = 300;
			this.ddl1.ItemsType = BP.Win.Controls.ItemsType.None;
			this.ddl1.Location = new System.Drawing.Point(0, 8);
			this.ddl1.MaxDropDownItems = 20;
			this.ddl1.Name = "ddl1";
			this.ddl1.Size = new System.Drawing.Size(224, 20);
			this.ddl1.TabIndex = 0;
			this.ddl1.SelectedIndexChanged += new System.EventHandler(this.ddl1_SelectedIndexChanged);
			// 
			// btnFind
			// 
			this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFind.Location = new System.Drawing.Point(456, 7);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(48, 22);
			this.btnFind.TabIndex = 5;
			this.btnFind.Text = "查找";
			this.toolTip1.SetToolTip(this.btnFind, "删除当前表格中所有数据");
			this.btnFind.Visible = false;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// FrmDGEditBase
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(560, 397);
			this.Controls.Add(this.dg1);
			this.Controls.Add(this.ddl1);
			this.Controls.Add(this.btnDel);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnFind);
			this.Name = "FrmDGEditBase";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DG直接数据维护";
			((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		protected string selectSQL ="";
		public  string SelectSQL
		{
			get{ return this.selectSQL;}
			set{ this.selectSQL =value;}
		}
		protected object Conn =null;
		public void RefreshData()
		{
			this.dg1.BindData( this.selectSQL ,this.Conn );
		}

		public bool AllowFind
		{
			get
			{
				return this.btnFind.Visible;
			}
			set
			{
				this.btnFind.Visible = value;
			}
		}
		public bool AllowAdd
		{
			get
			{
				return this.dg1.AllowNew;
			}
			set
			{
				this.dg1.AllowNew = value;
			}
		}
		public bool AllowDel
		{
			get
			{
				return this.dg1.AllowDelete;
			}
			set
			{
				this.dg1.AllowDelete = value;
				this.btnClear.Enabled = value;
				this.btnDel.Enabled = value;
			}
		}
		public bool AllowUpd
		{
			get
			{
				return this.dg1.AllowEdit;
			}
			set
			{
				this.dg1.AllowEdit = value;
				this.btnClear.Enabled = value;
				this.btnDel.Enabled = value;
				this.btnSave.Enabled = value;
			}
		}
		
		public void BindData( string title,string select , object conn)
		{
			this.Text = title;
			this.ddl1.DropDownStyle = ComboBoxStyle.Simple;
			this.ddl1.Text = title;
			this.ddl1.Enabled = false;

			this.selectSQL = select ;
			this.Conn = conn;
			this.RefreshData();
		}
		public DDL List
		{
			get
			{
				return this.ddl1;
			}
		}
		public DG DataGrid
		{
			get
			{
				return this.dg1;
			}
		}
		public  DataRowView DDLCurrentRow
		{
			get{ return this.ddl1.CurrentRow;}
		}
		public void BindConn( string formText, object conn)
		{
			this.Text = formText;
			this.Conn = conn;
		}
		
		private void btnOpen_Click(object sender, System.EventArgs e)
		{
			this.RefreshData();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			this.dg1.Save();
		}

		private void btnDel_Click(object sender, System.EventArgs e)
		{
			this.dg1.Delete();
		}
		private void btnClear_Click(object sender, System.EventArgs e)
		{
			this.dg1.Clear();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			if(this.DataGrid.DVSource==null)
				return;
			QueryDialogBase qdb = new QueryDialogBase();
			qdb.BindCols( this.DataGrid.DVSource.Table );
			int wh = this.selectSQL.ToLower().IndexOf("where");
			if( wh !=-1)
				qdb.SelectSql = this.selectSQL.Substring( 0 ,wh );
			else
				qdb.SelectSql = this.selectSQL;
			if( qdb.ShowDialog()==DialogResult.OK )
			{
				this.selectSQL = qdb.SelectSql + " where " +qdb.WhereSql;
				this.RefreshData();
			}
		}

		private void ddl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			AfterDDL_SelectedIndexChanged();
		}
		protected virtual void AfterDDL_SelectedIndexChanged()
		{
			if( this.ddl1.SelectedValue==null )
				return;

			DataRowView row = this.DDLCurrentRow;
			if(row==null)
				return;

			string sql = row["SqlText"].ToString().Trim();
			if( sql=="" )
			{
				string msg ="";
				foreach(DataColumn col in row.Row.Table.Columns)
				{
					msg += col.ColumnName+"["+row[col.ColumnName]+"],";
				}
				msg =msg.Trim(',');
				MessageBox.Show("SqlText内容不能为空！\r\n"+msg ,"加载失败！");
				return;
			}
			if(row["SqlType"].ToString().Trim()=="0")
			{
				sql = "select * from "+sql;
			}
			string allowAddDelUpd = row["AllowAddDelUpd"].ToString().Trim();
			if(allowAddDelUpd.Length>=3)
			{
				if(allowAddDelUpd[2]=='0')
					this.AllowUpd = false;
				else
					this.AllowUpd = true;

				if(allowAddDelUpd[1]=='0')
					this.AllowDel = false;
				else
					this.AllowDel = true;

				if(allowAddDelUpd[0]=='0')
					this.AllowAdd = false;
				else
					this.AllowAdd = true;
			}
			else
			{
				this.AllowAdd = true;
				this.AllowDel = true;
				this.AllowUpd = true;
			}

			this.SelectSQL = sql;
			this.RefreshData();
		}
		private void dg1_DoubleClick(object sender, System.EventArgs e)
		{
			AfterDG_DoubleClick();
		}
		protected virtual void AfterDG_DoubleClick()
		{
		}
	}
}
