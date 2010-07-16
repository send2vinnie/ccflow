using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
namespace BP.Win.Controls
{
	[ToolboxBitmap(typeof(System.Windows.Forms.DataGrid))]
	public class DG: System.Windows.Forms.DataGrid
	{
		public DG()
		{

			_dgColumns = new DGColumns();
		}
		private DGColumns _dgColumns ;
		public  DGColumns Columns
		{
			get{ return this._dgColumns;  }
			set{ this._dgColumns = value; }
		}
		public void SetDataSource( DataTable source)
		{
			this.TableStyles.Clear();
			this.Columns.Clear();
			DataGridTableStyle tb = new DataGridTableStyle();

			for(int i=0 ; i< source.Columns.Count ;i++)
			{
				DGColumnInfo colinfo = new DGColumnInfo(source.Columns[i].ColumnName);
				
				if( source.Columns[i].ColumnName.IndexOf("FK_")!=-1)
				{
					DG_DDLColumn col = new DG_DDLColumn();
					col.SetDDLDataSource( source );
					colinfo.ColumnType = DGColumnType.DDL;
					col.MappingName = source.Columns[i].ColumnName;
					col.HeaderText =  source.Columns[i].ColumnName;
					tb.GridColumnStyles.Add( col);
				}
				else
				{
					DataGridTextBoxColumn col = new DataGridTextBoxColumn();
					colinfo.ColumnType = DGColumnType.TB;
					col.MappingName = source.Columns[i].ColumnName;
					col.HeaderText =  source.Columns[i].ColumnName;
					tb.GridColumnStyles.Add( col);
				}
				

				this.Columns.Add( colinfo );
			}
			
			this.TableStyles.Add(tb);
			this.TableStyles[0].MappingName = source.TableName;

			this.DataSource = source; 
		}
		private EventHandlerSelectedChanged _selectedChanged;
		public event EventHandlerSelectedChanged  SelectedChanged
		{
			add
			{
				_selectedChanged += value;
			}
			remove
			{
				_selectedChanged -= value;
			}
		}
		protected virtual void OnSelectedChanged( EventArgs e)
		{
			if(this._selectedChanged != null)
			{
				_selectedChanged( this , this.CurrentRowIndex);
			}
		}
		private int _SelectedHandler = -1;
		protected override void OnClick(EventArgs e)
		{
			base.OnClick( e);
			if( this._SelectedHandler != this.CurrentRowIndex
				&& this.IsSelected(this.CurrentRowIndex))
			{
				this.OnSelectedChanged( e);
				this._SelectedHandler = this.CurrentRowIndex ;
			}
		}
		protected override void OnCurrentCellChanged(EventArgs e)
		{
			this._SelectedHandler = -1;
			base.OnCurrentCellChanged (e);
		}

		public void AutoGenerateColumns()
		{
			this.TableStyles.Clear();
			DataGridTableStyle stl = new DataGridTableStyle( this.ListManager );

			stl.MappingName = this.DVSource.Table.TableName;
			this.TableStyles.Add( stl );
			this.ReSetColorStyle();
		}

		public void AutoGenerateCols()//为以后版本改进预留
		{
		}
		public void ReSetColorStyle()
		{
			if(TableStyles.Count>0)
			{
				this.TableStyles[0].BackColor=this.BackColor;
				this.TableStyles[0].AlternatingBackColor=this.AlternatingBackColor;
				this.TableStyles[0].GridLineColor=this.GridLineColor;
			}
		}

		#region 数据绑定 
		private DataSet fkDataSource=null;
		public  DataSet FKDataSource
		{
			get{ return this.fkDataSource;}
			set{ this.fkDataSource = value;}
		}
		protected object dataConn=null;
		protected object dataAdapter=null;
		public OleDbDataAdapter  AdaOle
		{
			get{return this.dataAdapter as OleDbDataAdapter;}
		}
		public SqlDataAdapter    AdaSql
		{
			get{return this.dataAdapter as SqlDataAdapter;}
		}
		public OracleDataAdapter AdaOra
		{
			get{return this.dataAdapter as OracleDataAdapter;}
		}

		private DataTable         DBTable=null;
		public  DataView DVSource
		{
			get
			{
				return this.DataSource as DataView;
			}
		}		
		
		
		public bool Save()
		{
			if( this.DVSource == null)
				return false;
			try
			{
				this.BindingContext[this.DVSource].EndCurrentEdit();
			}
			catch(Exception ex)
			{
				if( MSG.ShowQuestion( "数据填写有误！是否取消编辑（Y/N）？\n"+ex.Message ,"错误！")==DialogResult.Yes)
				{
					this.BindingContext[this.DVSource].CancelCurrentEdit();
				}
				return false;
			}

			try
			{
				if( AdaOle!=null )
				{
					AdaOle.Update( this.DVSource.Table );
				}
				else if( AdaOra!=null )
				{
					AdaOra.Update( this.DVSource.Table );
				}
				else if( AdaSql!=null )
				{
					AdaSql.Update( this.DVSource.Table );
				}
				RefreshCaptionText();
				return true;
			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message ,"保存失败！");
				return false;
			}
		}
		public void RefreshCaptionText()
		{
			this.CaptionText = "行数："+DBTable.Rows.Count;
		}
		public bool Delete()
		{
			if( this.DVSource == null)
				return false;
			try
			{
				this.BindingContext[ this.DataSource].EndCurrentEdit();
				this.BindingContext[ this.DataSource].RemoveAt(this.BindingContext[ this.DataSource].Position);
				RefreshCaptionText();
				return true;
			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message ,"删除失败！");
				this.BindingContext[ this.DataSource].CancelCurrentEdit();
				return false;
			}
		}
		public bool Clear()
		{
			if( this.DVSource == null)
				return false;
			if(this.BindingContext[ this.DataSource].Count==0)
				return true;
			try
			{
				this.BindingContext[ this.DataSource].EndCurrentEdit();
				while( this.BindingContext[ this.DataSource].Count> 0)
					this.BindingContext[ this.DataSource].RemoveAt(this.BindingContext[ this.DataSource].Count-1);
				RefreshCaptionText();
				return true;
			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message ,"操作失败！");
				this.BindingContext[ this.DataSource].CancelCurrentEdit();
				return false;
			}
		}


		public bool BindData( string select , OleDbConnection conn)
		{
			OleDbDataAdapter adaOle = new OleDbDataAdapter(select,conn);
			this.dataAdapter = adaOle;
			DBTable = new DataTable( "tb");
			try
			{
				adaOle.Fill( DBTable);
			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message +"\n"+this.selectSQL ,"操作失败！");
				return false;
			}

			if(this.AllowEdit)
			{
				OleDbCommandBuilder cb = new OleDbCommandBuilder( adaOle );
				try
				{
					adaOle.InsertCommand = cb.GetInsertCommand();
					adaOle.UpdateCommand = cb.GetUpdateCommand();
					adaOle.DeleteCommand = cb.GetDeleteCommand();
				}
				catch
				{
					MessageBox.Show( "不能从所给SQL生成自动更新！\r\n如果查询对象是单一的表， 请检查该表是否有主键！\r\n["+select+"]","提示！");
					this.AllowNew = false;
					this.AllowEdit = false;
					this.AllowDelete = false;
				}
			}
			else
			{
				this.AllowNew = false;
				this.AllowEdit = false;
				this.AllowDelete = false;
			}
			this.selectSQL = select;
			this.dataConn = conn;
			this.DataSource = DBTable.DefaultView;
			this.ReSetRight();
			this.RefreshCaptionText();
			return true;
		}
		public bool BindData( string select , SqlConnection conn)
		{
			SqlDataAdapter adaSql = new SqlDataAdapter(select,conn);
			this.dataAdapter = adaSql;
			DBTable = new DataTable( "tb");
			try
			{
				adaSql.Fill( DBTable);
			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message +"\n"+this.selectSQL ,"操作失败！");
				return false;
			}

			if(this.AllowEdit)
			{
				SqlCommandBuilder cb = new SqlCommandBuilder( adaSql );
				try
				{
					adaSql.InsertCommand = cb.GetInsertCommand();
					adaSql.UpdateCommand = cb.GetUpdateCommand();
					adaSql.DeleteCommand = cb.GetDeleteCommand();
				}
				catch
				{
					MessageBox.Show( "不能从所给SQL生成自动更新！\r\n如果查询对象是单一的表， 请检查该表是否有主键！\r\n["+select+"]","提示！");
					this.AllowNew = false;
					this.AllowEdit = false;
					this.AllowDelete = false;
				}
			}
			else
			{
				this.AllowNew = false;
				this.AllowEdit = false;
				this.AllowDelete = false;
			}
			this.selectSQL = select;
			this.dataConn = conn;
			this.DataSource = DBTable.DefaultView;
			this.ReSetRight();
			this.RefreshCaptionText();
			return true;
		}
		public bool BindData( string select , OracleConnection conn)
		{
			OracleDataAdapter adaOra = new OracleDataAdapter(select,conn);
			this.dataAdapter = adaOra;
			DBTable = new DataTable( "tb");
			try
			{
				adaOra.Fill( DBTable);
			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message +"\n"+this.selectSQL ,"操作失败！");
				return false;
			}

			if(this.AllowEdit)
			{
				OracleCommandBuilder cb = new OracleCommandBuilder( adaOra );
				try
				{
					adaOra.InsertCommand = cb.GetInsertCommand();
					adaOra.UpdateCommand = cb.GetUpdateCommand();
					adaOra.DeleteCommand = cb.GetDeleteCommand();
				}
				catch
				{
					MessageBox.Show( "不能从所给SQL生成自动更新！\r\n如果查询对象是单一的表， 请检查该表是否有主键！\r\n["+select+"]","提示！");
					this.AllowNew = false;
					this.AllowEdit = false;
					this.AllowDelete = false;
				}
			}
			else
			{
				this.AllowNew = false;
				this.AllowEdit = false;
				this.AllowDelete = false;
			}
			this.selectSQL = select;
			this.dataConn = conn;
			this.DataSource = DBTable.DefaultView;
			this.ReSetRight();
			this.RefreshCaptionText();
			return true;
		}
		public bool BindData( string select , object conn)
		{
			if( conn is OleDbConnection)
			{
				return this.BindData( select ,(OleDbConnection)conn );
			}
			else if( conn is SqlConnection)
			{
				return this.BindData( select ,(SqlConnection)conn );
			}
			else if( conn is OracleConnection)
			{
				return this.BindData( select ,(OracleConnection)conn );
			}
			else
			{
				throw new Exception("数据库连接失败！"+conn);
			}
		}
		protected void ReSetRight()
		{
			DBTable.DefaultView.AllowNew = this.AllowNew ;
			DBTable.DefaultView.AllowEdit = this.AllowEdit ;
			DBTable.DefaultView.AllowDelete = this.AllowDelete ;
		}
		public void RefreshData()
		{
			if(this.dataConn!=null&&this.SelectSQL!="")
			{
				this.BindData(this.selectSQL ,this.dataConn);
			}
		}
		

		private string selectSQL ="";
		public  string SelectSQL
		{
			get
			{
				return selectSQL;
			}
			set
			{
				this.selectSQL = value;
			}
		}
		private bool _allowNew = true;
		public  bool AllowNew
		{
			get
			{
				return _allowNew;
			}
			set
			{
				_allowNew = value;
				if( DVSource !=null)
					DVSource.AllowNew = value;
			}
		}
		private bool _allowEdit = true;
		public  bool AllowEdit
		{
			get
			{
				return _allowEdit;
			}
			set
			{
				_allowEdit = value;
				if( DVSource !=null)
					DVSource.AllowEdit = value;
			}
		}
		private bool _allowDelete = true;
		public  bool AllowDelete
		{
			get
			{
				return _allowDelete;
			}
			set
			{
				_allowDelete = value;
				if( DVSource !=null)
					DVSource.AllowDelete = value;
			}
		}		
		public void EndCurrentEditDataSource()
		{
			if(this.DataSource!=null)
				this.BindingContext[this.DataSource].EndCurrentEdit();
		}
		public DataRowView CurrentRow
		{
			get
			{
				if(this.DVSource!=null)
					return this.BindingContext[this.DVSource].Current as DataRowView;
				return null;
			}
		}
		
		#endregion 数据绑定 

	}
	public delegate void EventHandlerSelectedChanged(  object sender ,int rowindex);

}
