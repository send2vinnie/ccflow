using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace BP.Win.Controls
{
	/// <summary>
	/// DG_DDL 的摘要说明。
	/// </summary>
	public class DG_DDLColumn : DataGridColumnStyle
	{
		public DG_DDLColumn()
		{
			this._ddl=new DG_DDL();
			this._ddl.Visible=false;
			this._ddl.Leave+=new System.EventHandler(LeaveDDL);
			this._ddl.SelectedIndexChanged+=new System.EventHandler(DDLIndexChanged);
			this._ddl.SelectionChangeCommitted+=new System.EventHandler(DDLStartEditing);
		}
		public DG_DDLColumn(System.Data.DataTable dropTable ,string val ,string text)
		{
			this._ddl=new DG_DDL();
			this._ddl.BindDataDataTable(dropTable ,val ,text);//(DataTable)
			this._ddl.Visible=false;
			this._ddl.Leave+=new System.EventHandler(LeaveDDL);
			this._ddl.SelectedIndexChanged+=new System.EventHandler(DDLIndexChanged);
			this._ddl.SelectionChangeCommitted+=new System.EventHandler(DDLStartEditing);
		}

		#region 属性

		private DG_DDL _ddl =null ;
		public  DG_DDL DDL
		{
			get {return this._ddl;}
			set { this._ddl = value;}
		}
		private int xMargin = 2;
		private int yMargin = 1;
		private bool InEdit= false;

		private CurrencyManager _source = null;
		private int _rowNum;
		private bool _saveText=false;//ccb 0430
		public  bool IsSaveText
		{
			get
			{
				return this._saveText ;
			}
			set
			{
				this._saveText = value;
			}
		}

		private EventHandlerDDLValueChanged _ddlValueChanged;
		public event EventHandlerDDLValueChanged  DDLValueChanged
		{
			add
			{
				_ddlValueChanged += value;
			}
			remove
			{
				_ddlValueChanged -= value;
			}
		}

		#endregion

		#region 方法

		private void HideComboBox()
		{
			if( this._ddl.Focused )
			{
				this.DataGridTableStyle.DataGrid.Focus();
			}
			_ddl.Visible = false;
		}
		public void EndEdit()
		{
			InEdit = false;
			Invalidate();
		}

		private void LeaveDDL(object sender, EventArgs e)
		{
			if(this.InEdit)
			{
				object Value= DBNull.Value;
				if(!this._saveText)   //
				{
					if(this._ddl.SelectedValue!=null)
						Value = this._ddl.SelectedValue;
				}
				else if(this._ddl.Text.Trim()!="")
				{
					Value = this._ddl.Text;//ccb 0430
				}
				if(!Value.Equals(DBNull.Value) && string.Compare(Value.ToString(),this.NullText,true)==0 )
				{
					Value = DBNull.Value;//System.Convert.DBNull; 
				}
				this.SetColumnValueAtRow(_source, _rowNum ,Value);

				InEdit = false;
				Invalidate();
			}
			_ddl.Hide();
		}
		private void DDLIndexChanged(object sender, EventArgs e)
		{
			if(_ddlValueChanged!=null)
				_ddlValueChanged(_rowNum , this._ddl.SelectedValue); 	
		}
		private void DDLStartEditing(object sender, EventArgs e)
		{
			this.InEdit = true;
			base.ColumnStartedEditing((Control) sender);
		}

		public void SetDDLDataSource( object ddl_DataSource)//ccb0508+
		{
			this._ddl.BindData( ddl_DataSource );
		}

		#endregion

		#region 重写1

		protected override int GetMinimumHeight()
		{
			return this._ddl.PreferredHeight + yMargin;
		}
		
		protected override Size GetPreferredSize(
			Graphics g,
			object Value
			)
		{
			string text=this.NullText;
			if(!Value.Equals(DBNull.Value))
				text=this._ddl.GetDisplayText(Value);
			
			Size Extents = Size.Ceiling(g.MeasureString(text, this.DataGridTableStyle.DataGrid.Font));
			Extents.Width += xMargin * 2 + 1 ;
			Extents.Height += yMargin;
			return Extents;
		}
		protected override void Abort(int rowNum)
		{
			this.HideComboBox();
			this.EndEdit();
		}
		
		protected override bool Commit( CurrencyManager dataSource,int rowNum)
		{
			this.HideComboBox();
			if(!this.InEdit)
				return true;
//			try
//			{
				object Value= DBNull.Value;
				if(!this._saveText)   //
				{
					if(this._ddl.SelectedValue!=null)
						Value = this._ddl.SelectedValue;
				}
				else if(this._ddl.Text.Trim()!="")
				{
					Value = this._ddl.Text;//ccb 0430
				}
				if(!Value.Equals(DBNull.Value) && string.Compare(Value.ToString(),this.NullText,true)==0 )
				{
					Value = DBNull.Value;//System.Convert.DBNull; 
				}
				this.SetColumnValueAtRow( dataSource, rowNum, Value);
//			}
//			catch
//			{
//				return false;
//			}
			this.EndEdit();
			return true;
		}
	
		protected override void Edit(CurrencyManager source ,int rowNum,Rectangle bounds,bool readOnly,string instantText,bool cellIsVisible)
		{
			Rectangle OriginalBounds = bounds;
			_rowNum = rowNum;
			_source = source;
			if(readOnly)
				return;
	
			if(cellIsVisible)
			{
				bounds.Offset(xMargin, yMargin);
				bounds.Width -= xMargin * 2;
				bounds.Height -= yMargin;
				_ddl.Bounds = bounds;
				_ddl.Visible = true;
			}
			else
			{
				_ddl.Bounds = OriginalBounds;
				_ddl.Visible = false;
			}
			
			_ddl.SelectedValue = GetColumnValueAtRow(source, rowNum);
			if(_ddl.SelectedValue==null)
				_ddl.SelectedIndex=-1;
			_ddl.RightToLeft = this.DataGridTableStyle.DataGrid.RightToLeft;
			//			Combo.Focus();
			if(_ddl.Visible)
			{
				DataGridTableStyle.DataGrid.Invalidate(OriginalBounds);
			}

			InEdit = true;
		}
	
	
		protected override int GetPreferredHeight(Graphics g,object Value)
		{
			int NewLineIndex  = 0;
			int NewLines = 0;
			string text=this.NullText;
			if(!Value.Equals(DBNull.Value))
				text=this._ddl.GetDisplayText(Value);
			do
			{//NewLineIndex = ValueString.IndexOf("r\n", NewLineIndex + 1);//ValueString为0时,超范围
				NewLineIndex = text.IndexOf("r\n", NewLineIndex );//ccb0619
				NewLines += 1;
			}while(NewLineIndex != -1);
			return FontHeight * NewLines + yMargin;
		}
		protected override void ConcedeFocus()
		{
			this.HideComboBox();
		
		}
	
		protected override void Paint(
			Graphics g,
			Rectangle bounds,
			CurrencyManager source,
			int rowNum
			)
		{
			
			Paint(g, bounds, source, rowNum, false);

		}
		protected override void Paint(
			Graphics g,
			Rectangle bounds,
			CurrencyManager source,
			int rowNum,
			bool alignToRight
			)
		{
			
			Brush BackBrush = new SolidBrush(this.DataGridTableStyle.BackColor);
			g.FillRectangle(BackBrush,bounds);
			bounds.Offset(this.xMargin,this.yMargin);
			
			string text=this.NullText;
			object Value = GetColumnValueAtRow(source, rowNum);
			if(!Value.Equals(DBNull.Value))
				text=this._ddl.GetDisplayText(Value);

			PaintText(g, bounds, text, alignToRight);


		}
		
		protected override void Paint(
			Graphics g,
			Rectangle bounds,
			CurrencyManager source,
			int rowNum,
			Brush backBrush,
			Brush foreBrush,
			bool alignToRight
			)
		{
			Brush BackBrush = new SolidBrush(this.DataGridTableStyle.BackColor);
			g.FillRectangle(BackBrush,bounds);
			bounds.Offset(this.xMargin,this.yMargin);

			
			string text=this.NullText;
			object Value = GetColumnValueAtRow(source, rowNum);
			if(!Value.Equals(DBNull.Value))
				text=this._ddl.GetDisplayText(Value);

			//string Text = this._ddl.GetDisplayText(GetColumnValueAtRow(source, rowNum));
//			if(this._saveText)                                        //
//				text = GetColumnValueAtRow(source, rowNum).ToString();//ccb 0430
			PaintText(g, bounds, text, backBrush, foreBrush, alignToRight);

		}
	
		private void PaintText(Graphics g ,Rectangle Bounds,string Text,bool AlignToRight)
		{
			Brush BackBrush = new SolidBrush(this.DataGridTableStyle.BackColor);
			Brush ForeBrush= new SolidBrush(this.DataGridTableStyle.ForeColor);
			PaintText(g, Bounds, Text, BackBrush, ForeBrush, AlignToRight);
		}
		private void PaintText(Graphics g , Rectangle TextBounds, string Text, Brush BackBrush,Brush ForeBrush,bool AlignToRight)
		{	
			Rectangle Rect = TextBounds;
			RectangleF RectF  = Rect; 
			StringFormat Format = new StringFormat();
			if(AlignToRight)
			{
				Format.FormatFlags = StringFormatFlags.DirectionRightToLeft;
			}
			switch(this.Alignment)
			{
				case HorizontalAlignment.Left:
					Format.Alignment = StringAlignment.Near;
					break;
				case HorizontalAlignment.Right:
					Format.Alignment = StringAlignment.Far;
					break;
				case HorizontalAlignment.Center:
					Format.Alignment = StringAlignment.Center;
					break;
			}
			Format.FormatFlags =Format.FormatFlags;
			Format.FormatFlags =StringFormatFlags.NoWrap;
			g.FillRectangle(BackBrush, Rect);
			Rect.Offset(0, yMargin);
			Rect.Height -= yMargin;
			g.DrawString(Text, this.DataGridTableStyle.DataGrid.Font, ForeBrush, RectF, Format);
			Format.Dispose();
		}
		#endregion

		#region 重写2
		protected override void SetDataGridInColumn(
			DataGrid value
			)
		{
			base.SetDataGridInColumn(value);
			this._ddl.SetDataGrid(value);
		}
		protected override void SetDataGrid(System.Windows.Forms.DataGrid value)
		{
			base.SetDataGrid(value);
			this._ddl.SetDataGrid(value);
		}
		#endregion


	}


}
