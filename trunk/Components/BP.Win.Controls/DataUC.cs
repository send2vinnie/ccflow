using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CWAI.Win.Controls
{
	/// <summary>
	/// DataUC 的摘要说明。
	/// </summary>
	public class DataUC : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataUC()
		{
			InitializeComponent();
		}

		#region 绑定  初始化设置
		
		public virtual void BindData( SqlConnection con , string sql)
		{
			this.BindTable = new DT("tb");
			this.BindTable.BindData(con, sql);

			this.BindData( BindTable );
		}
		public virtual void BindData( DT table)
		{
			this.BindTable = table ;

			Point locLab = new Point(10,14);
			Point locTB = new Point(160,10);

			this.Controls.Clear();
			foreach(DataColumn att in table.Columns )
			{
				Lab lab = new Lab();
				lab.Name = "lab" + att.ColumnName ;
				lab.Text = att.ColumnName + "("+att.ColumnMapping+")";
				lab.Location = locLab;
				this.Controls.Add( lab );
				
				if( att.ColumnName.IndexOf("FK_")!=-1)
				{
					DDL ddl = new DDL();
					ddl.Name = att.ColumnName ;
					ddl.Location = locTB ;
					ddl.Size = new Size( 200,22);

					ddl.DataBindings.Add("Text" ,table.DefaultView ,att.ColumnName );
					this.Controls.Add( ddl );
				}
				else
				{
					TB tb = new TB();
					tb.Name = att.ColumnName ;
					tb.Size = new Size( 200,22);
					tb.Location = locTB;

					tb.DataBindings.Add("Text" ,table.DefaultView ,att.ColumnName );
					this.Controls.Add( tb );
				}

				locLab.Offset( 0 , lab.Height + 8 );
				locTB.Offset( 0 , lab.Height + 8 );

			}

			if( this.Height < locTB.Y)
				this.Height = locTB.Y;

			if( this.Width < locTB.X + 216)
				this.Width = locTB.X + 216;

		}

		#endregion 

		#region 组件设计器生成的代码
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
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	

		public DT BindTable;
		public BindingManagerBase BindContext
		{
			get
			{
				return this.BindingContext[this.BindTable.DefaultView];
			}
		}

		public virtual bool IsChanged()
		{
			try
			{
				this.BindContext.EndCurrentEdit();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				this.BindContext.CancelCurrentEdit();
				return false;
			}
	
			if(this.BindTable.GetChanges() != null)
				return true;
			return false;
		}
		public virtual bool Save()
		{
			if(this.IsChanged())
			{
				this.BindTable.Save();
			}
			return true;
		}
	
	}
}
