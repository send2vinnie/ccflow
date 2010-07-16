using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	/// <summary>
	/// QueryDialogBase 的摘要说明。
	/// </summary>
	public class QueryDialogBase : System.Windows.Forms.Form
	{
		protected BP.Win.Controls.TB tb1;
		private BP.Win.Controls.Btn btnAdd;
		private BP.Win.Controls.Btn btnSub;
		private BP.Win.Controls.Btn btnClose;
		private BP.Win.Controls.Btn btnClear;
		protected BP.Win.Controls.DDL ddlAndOr;
		protected BP.Win.Controls.DDL ddl_Col;
		protected BP.Win.Controls.DDL ddl_Compare;
		protected BP.Win.Controls.DDL ddl_Val;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public QueryDialogBase()
		{
			InitializeComponent();

			this.ddlAndOr.SelectedIndex = 0;
			this.ddl_Compare.FillItemsByItemsType( ItemsType.Compare );
			this.ddl_Val.DropDownStyle = ComboBoxStyle.Simple;
		}

		public DDL DDLCol
		{
			get
			{
				return this.ddl_Col;
			}
		}
		public void BindCols( DataTable tb)
		{
			this.ddl_Col.BindDataTableSchema( tb );
		}

		private string selectSql = "";
		public string SelectSql 
		{
			get
			{
				return selectSql;
			}
			set
			{
				this.selectSql = value;
			}
		}
		public string WhereSql 
		{
			get
			{
				string sql = this.tb1.Lines[0].Trim();
				for(int i=1;i<this.tb1.Lines.Length;i++)
				{
					sql += " "+this.tb1.Lines[i].Trim();
				}
				return sql;
			}
		}


		

		protected virtual string GetSqlCondition()
		{
			string sql = "";
			if( this.DDLCol.SelectedValue!=null )
				sql = this.DDLCol.SelectedValue.ToString();
			else
				sql = this.DDLCol.Text;
			sql += this.ddl_Compare.SelectedValue.ToString();
			if( this.ddl_Compare.SelectedValue.ToString().Trim()=="like")
				sql +="'%"+ this.ddl_Val.Text + "%'";
			else
				sql +="'"+ this.ddl_Val.Text + "'";
			return sql;
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
			this.ddlAndOr = new BP.Win.Controls.DDL();
			this.ddl_Col = new BP.Win.Controls.DDL();
			this.ddl_Compare = new BP.Win.Controls.DDL();
			this.ddl_Val = new BP.Win.Controls.DDL();
			this.tb1 = new BP.Win.Controls.TB();
			this.btnAdd = new BP.Win.Controls.Btn();
			this.btnSub = new BP.Win.Controls.Btn();
			this.btnClose = new BP.Win.Controls.Btn();
			this.btnClear = new BP.Win.Controls.Btn();
			this.SuspendLayout();
			// 
			// ddlAndOr
			// 
			this.ddlAndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlAndOr.DropDownWidth = 200;
			this.ddlAndOr.Items.AddRange(new object[] {
														  "而且",
														  "或者"});
			this.ddlAndOr.ItemsType = BP.Win.Controls.ItemsType.None;
			this.ddlAndOr.Location = new System.Drawing.Point(0, 8);
			this.ddlAndOr.MaxDropDownItems = 20;
			this.ddlAndOr.Name = "ddlAndOr";
			this.ddlAndOr.Size = new System.Drawing.Size(56, 20);
			this.ddlAndOr.TabIndex = 0;
			// 
			// ddl_Col
			// 
			this.ddl_Col.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ddl_Col.DropDownWidth = 200;
			this.ddl_Col.ItemsType = BP.Win.Controls.ItemsType.None;
			this.ddl_Col.Location = new System.Drawing.Point(56, 8);
			this.ddl_Col.MaxDropDownItems = 20;
			this.ddl_Col.Name = "ddl_Col";
			this.ddl_Col.Size = new System.Drawing.Size(120, 20);
			this.ddl_Col.TabIndex = 1;
			this.ddl_Col.Text = "ddl2";
			// 
			// ddl_Compare
			// 
			this.ddl_Compare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ddl_Compare.DropDownWidth = 200;
			this.ddl_Compare.ItemsType = BP.Win.Controls.ItemsType.None;
			this.ddl_Compare.Location = new System.Drawing.Point(176, 8);
			this.ddl_Compare.MaxDropDownItems = 20;
			this.ddl_Compare.Name = "ddl_Compare";
			this.ddl_Compare.Size = new System.Drawing.Size(56, 20);
			this.ddl_Compare.TabIndex = 2;
			this.ddl_Compare.Text = "ddl3";
			// 
			// ddl_Val
			// 
			this.ddl_Val.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ddl_Val.DropDownWidth = 200;
			this.ddl_Val.ItemsType = BP.Win.Controls.ItemsType.None;
			this.ddl_Val.Location = new System.Drawing.Point(232, 8);
			this.ddl_Val.MaxDropDownItems = 20;
			this.ddl_Val.Name = "ddl_Val";
			this.ddl_Val.Size = new System.Drawing.Size(104, 20);
			this.ddl_Val.TabIndex = 3;
			// 
			// tb1
			// 
			this.tb1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tb1.Location = new System.Drawing.Point(0, 32);
			this.tb1.Multiline = true;
			this.tb1.Name = "tb1";
			this.tb1.Size = new System.Drawing.Size(400, 152);
			this.tb1.TabIndex = 6;
			this.tb1.Text = "";
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(344, 8);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(56, 22);
			this.btnAdd.TabIndex = 5;
			this.btnAdd.Text = "添加(&A)";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnSub
			// 
			this.btnSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSub.Location = new System.Drawing.Point(280, 192);
			this.btnSub.Name = "btnSub";
			this.btnSub.Size = new System.Drawing.Size(56, 22);
			this.btnSub.TabIndex = 7;
			this.btnSub.Text = "确定(&S)";
			this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(344, 192);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(56, 22);
			this.btnClose.TabIndex = 8;
			this.btnClose.Text = "关闭(&C)";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClear.Location = new System.Drawing.Point(0, 192);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(56, 22);
			this.btnClear.TabIndex = 10;
			this.btnClear.Text = "清空(&L)";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// QueryDialogBase
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(400, 221);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnSub);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.tb1);
			this.Controls.Add(this.ddl_Compare);
			this.Controls.Add(this.ddl_Col);
			this.Controls.Add(this.ddlAndOr);
			this.Controls.Add(this.ddl_Val);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "QueryDialogBase";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "查询";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			this.tb1.Text = "";
		}

		private void btnSub_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			string sql = this.GetSqlCondition();

			if( this.tb1.Text.Trim()!="" )
			{
				if( this.ddlAndOr.SelectedIndex ==0)
				{
					sql =" and " + sql;
				}
				else
					sql =" or " + sql;
			}
			this.tb1.Text += sql +"\r\n";
		}

	}
}
