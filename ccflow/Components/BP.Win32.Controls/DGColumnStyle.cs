
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using BP.En ; 
using BP.En;
using BP;
using BP.DA;

namespace BP.Win32.Controls
{
	/// <summary>
	/// 编辑DG列
	/// </summary>
	public class DGEnsColumn:DataGridTextBoxColumn
	{
		//private DateTimePicker myDateTimePicker = new DateTimePicker();

		#region self 
		private Attr _HisAttr=null;
		public Attr HisAttr1
		{
			get
			{
				return _HisAttr;
			}
			set
			{
				_HisAttr=value;
			}
		}
		private Attr _HisRefAttr=null;
		public Attr HisRefAttr
		{
			get
			{
				return _HisRefAttr;
			}
			set
			{
				_HisRefAttr=value;
			}
		}
		#endregion

		private  BP.Win32.Controls.DDL  _myDDL=null;
		/// <summary>
		/// 创建一个ddl
		/// </summary>
		public BP.Win32.Controls.DDL myDDL 
		{
			get
			{
				if (_myDDL==null)
				{
					try
					{
						_myDDL = new DDL();
						_myDDL.Name=this.HisRefAttr.Key ;

						if (this.HisRefAttr.MyFieldType ==FieldType.Enum || this.HisRefAttr.MyFieldType ==FieldType.PKEnum)
						{
							Sys.SysEnums ens = new BP.Sys.SysEnums(HisRefAttr.UIBindKey) ; 
							_myDDL.BindEns(ens) ;
						}
						else
						{
							Entities ens =ClassFactory.GetEns(this.HisRefAttr.UIBindKey);
							ens.RetrieveAll();
							_myDDL.BindEns(ens,this.HisRefAttr.UIRefKeyText,this.HisRefAttr.UIRefKeyValue);

						}
					}
					catch(Exception ex)
					{
						throw new Exception("error 11"+ex.Message);
					}
				}
				return _myDDL ; 
			}
		}
		/// <summary>
		/// 能不能编辑
		/// </summary>
		private bool isEditing;	 
		 
		/// <summary>
		/// 在New 时间visible =false ; 
		/// </summary>
		public DGEnsColumn():base() 
		{
			//myDateTimePicker.Visible = false;
			myDDL.Visible=false;
			 
		}
		public DGEnsColumn(Attr refAttr):base() 
		{
			this.HisRefAttr =refAttr;
			//myDateTimePicker.Visible = false;
			myDDL.Visible=false;
		}

		#region 方法
		/// <summary>
		/// 中断
		/// </summary>
		/// <param name="rowNum"></param>
		protected override void Abort(int rowNum)
		{
			try
			{
				isEditing = false;
				// ValueChanged  变化后．
				//myDateTimePicker.ValueChanged -= new EventHandler(TimePickerValueChanged);
				myDDL.SelectedIndexChanged  -= new EventHandler(DDLValueChanged) ;
				Invalidate(); // 积类的方法．
			}
			catch(Exception ex)
			{
				throw new Exception("error 22"+ex.Message);
			}
		}
		/// <summary>
		/// 委托
		/// </summary>
		/// <param name="dataSource"></param>
		/// <param name="rowNum"></param>
		/// <returns></returns>
		protected override bool Commit(CurrencyManager dataSource, int rowNum) 
		{
			try
			{
				//myDateTimePicker.Bounds = Rectangle.Empty;
				myDDL.Bounds = Rectangle.Empty;
				//myDateTimePicker.ValueChanged -= new EventHandler(TimePickerValueChanged);
				myDDL.SelectedValueChanged  -= new EventHandler(DDLValueChanged);
				if (!isEditing)
					return true;
				isEditing = false;
				//			try 
				//			{				 
				//				DateTime value = myDateTimePicker.Value;
				//				SetColumnValueAtRow(dataSource, rowNum, value);
				//			} 
				//			catch (Exception) 
				//			{
				//				Abort(rowNum);
				//				return false;
				//			}
				//SetColumnValueAtRow(dataSource, rowNum, myDDL.SelectedValue );
				SetColumnValueAtRow(dataSource, rowNum, myDDL.SelectedListItem.Text );

				//	SetColumnValueAtRow(dataSource, rowNum-1, myDDL.SelectedListItem.Value );
 
				Invalidate();
				return true;
			}
			catch(Exception ex)
			{
				throw new Exception("error 33"+ex.Message);
			}
		}	 
		 
		protected override void Edit(CurrencyManager source, int rowNum,Rectangle bounds, bool readOnly,string instantText, bool cellIsVisible) 
		{
			try
			{
				// 当编辑时间找到要编辑cell的值。
				string str ;
				str = GetColumnValueAtRow(source, rowNum).ToString();

				if (str=="")
				{
					if (this.HisRefAttr.MyFieldType==FieldType.Enum
						|| this.HisRefAttr.MyFieldType==FieldType.PKEnum)
					{
						BP.Sys.SysEnum en = new BP.Sys.SysEnum(this.HisRefAttr.UIBindKey,int.Parse(this.HisRefAttr.DefaultVal.ToString()));
						str=en.Lab;
					}
					else
					{

						if ( this.HisRefAttr.DefaultVal.ToString()=="" )
						{
							/* 如果没有默认值 */
							Entities ens = ClassFactory.GetEns(this.HisRefAttr.UIBindKey) ;
							if (ens.RetrieveAll(1)==1)
								str=(string)ens[0].GetValByKey(this.HisRefAttr.UIRefKeyText);
						}
						else
						{
							/* 如果有默认值的情况。 */
							Entity en = ClassFactory.GetEns(this.HisRefAttr.UIBindKey).GetNewEntity ;
							en.PKVal =this.HisRefAttr.DefaultVal ; 
							en.Retrieve();
							str= (string)en.GetValByKey( this.HisRefAttr.UIRefKeyText);
						}
					}

					this.SetColumnValueAtRow(source,rowNum,str);
				}
					
				//MessageBox.Show("GetColumnValueAtRow  ss :"+str);

				if (cellIsVisible) 
				{
					myDDL.Bounds = new Rectangle
						(bounds.X + 2, bounds.Y + 2, 
						bounds.Width - 4, bounds.Height - 4);
					myDDL.SelectedText=str;
					//myDDL.SetSelectedText( str ) ;					
					myDDL.Visible = true;
					myDDL.SelectedValueChanged += 
						new EventHandler(DDLValueChanged);
				}
				else
				{
					myDDL.SetSelectedText(str);
					myDDL.Visible = false;
				}

				if (myDDL.Visible)
					DataGridTableStyle.DataGrid.Invalidate(bounds);
			}
			catch(Exception ex)
			{
				//myDDL.Visible = false;
				MessageBox.Show(ex.Message,"Edit error2222");
			}
		}
		/// <summary>
		/// 大小
		/// </summary>
		/// <param name="g"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		protected override Size GetPreferredSize(Graphics g, object value) 
		{
			//return new Size(100, myDateTimePicker.PreferredHeight + 4);
			return new Size(100, myDDL.PreferredHeight + 4);
		}
		/// <summary>
		/// 最小的高度
		/// </summary>
		/// <returns></returns>
		protected override int GetMinimumHeight() 
		{
			//return myDateTimePicker.PreferredHeight + 4;
			return myDDL.PreferredHeight + 4;
		}
		/// <summary>
		/// 最小的高度
		/// </summary>
		/// <param name="g"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		protected override int GetPreferredHeight(Graphics g, object value) 
		{
			//return myDateTimePicker.PreferredHeight + 4;
			return myDDL.PreferredHeight + 4;
		}
		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum) 
		{
			Paint(g, bounds, source, rowNum, false);
		}
		protected override void Paint(Graphics g, Rectangle bounds,	CurrencyManager source, int rowNum,	bool alignToRight) 
		{
			Paint(
				g,bounds, 
				source, 
				rowNum, 
				Brushes.Red, 
				Brushes.Blue, 
				alignToRight);
		}
		protected override void Paint(Graphics g, Rectangle bounds,	CurrencyManager source, int rowNum,	Brush backBrush, Brush foreBrush,bool alignToRight) 
		{
			try
			{
				string date= GetColumnValueAtRow(source, rowNum).ToString();	
				//string date="2004-09-01"; 
				Rectangle rect = bounds;

				g.FillRectangle(backBrush,rect);

				rect.Offset(0, 2);
				rect.Height -= 2;
				//g.DrawString(date.ToString("d"), this.DataGridTableStyle.DataGrid.Font, foreBrush, rect);
				g.DrawString(date , this.DataGridTableStyle.DataGrid.Font, foreBrush, rect);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message," 44");
			}
		}

		protected override void SetDataGridInColumn(DataGrid value) 
		{
			try
			{
			 
				base.SetDataGridInColumn(value);
				if (myDDL.Parent != null) 
				{
					myDDL.Parent.Controls.Remove (myDDL);
				}
				if (value != null) 
				{
					value.Controls.Add(myDDL);
				}

			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message," 55");
			}
			 
		}
		/// <summary>
		/// TimePickerValueChanged
		/// </summary>
		/// <param name="sender">myDateTimePicker</param>
		/// <param name="e"></param>
		private void DDLValueChanged(object sender, EventArgs e) 
		{
			try
			{
				this.isEditing = true;
				base.ColumnStartedEditing(myDDL);
				 
				int ColumnNumber = this.DataGridTableStyle.DataGrid.CurrentCell.ColumnNumber ;
				int RowNumber = this.DataGridTableStyle.DataGrid.CurrentCell.RowNumber;

				//this.SetColumnValueAtRow(this.DataGridTableStyle.DataGrid,
				this.DataGridTableStyle.DataGrid[RowNumber,ColumnNumber]=myDDL.SelectedText ;

				// 在ddl 变化后设置 对应的文本。
				this.DataGridTableStyle.DataGrid[RowNumber, ColumnNumber-1 ]=myDDL.SelectedValue;
 
				//this.SetColumnValueAtRow(this.DataGridTableStyle.DataGrid,
				//this.DataGridTableStyle.DataGrid[RowNumber,ColumnNumber]=myDDL.SelectedListItem.Text ;

				// 在ddl 变化后设置 对应的文本。
				//this.DataGridTableStyle.DataGrid[RowNumber, ColumnNumber-1 ]=myDDL.SelectedListItem.Value ;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message,"999-66");
			}
			 
		}
		#endregion
	}
  
	// This example shows how to create your own column style that
	// hosts a control, in this case, a DateTimePicker.
	/// <summary>
	/// 日期column . 
	/// </summary>
	public class DGTimePickerColumn : DataGridTextBoxColumn
	{
		// create a new datatimepicker . 
		private DateTimePicker myDateTimePicker = new DateTimePicker();
		// The isEditing field tracks whether or not the user is
		// editing data with the hosted control.
		/// <summary>
		/// 是不是编辑
		/// </summary>
		private bool isEditing;
		/// <summary>
		/// 在New 时间visible =false ; 
		/// </summary>
		public DGTimePickerColumn():base() 
		{
			myDateTimePicker.Visible = false;
		}
		/// <summary>
		/// 中断
		/// </summary>
		/// <param name="rowNum"></param>
		protected override void Abort(int rowNum)
		{
			isEditing = false;
			// ValueChanged  变化后．
			myDateTimePicker.ValueChanged -= new EventHandler(TimePickerValueChanged);
			Invalidate(); // 积类的方法．
		}
		/// <summary>
		/// 委托
		/// </summary>
		/// <param name="dataSource"></param>
		/// <param name="rowNum"></param>
		/// <returns></returns>
		protected override bool Commit(CurrencyManager dataSource, int rowNum) 
		{
		 
			myDateTimePicker.Bounds = Rectangle.Empty;
			myDateTimePicker.ValueChanged -= new EventHandler(TimePickerValueChanged);
			if (!isEditing)
				return true;
			isEditing = false;
			try 
			{
				DateTime value = myDateTimePicker.Value;
				SetColumnValueAtRow(dataSource, rowNum, value);
			} 
			catch (Exception) 
			{
				Abort(rowNum);
				return false;
			}
			Invalidate();
			return true;
			 
		}
		/// <summary>
		/// 编辑
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <param name="bounds"></param>
		/// <param name="readOnly"></param>
		/// <param name="instantText"></param>
		/// <param name="cellIsVisible"></param>
		protected override void Edit(CurrencyManager source, int rowNum,Rectangle bounds, bool readOnly,string instantText, bool cellIsVisible) 
		{
			try
			{
				DateTime value ;
				try
				{
					value = (DateTime) 
						GetColumnValueAtRow(source, rowNum);					 
				}
				catch
				{
					value=DateTime.Now;
				}

				
				if (cellIsVisible) 
				{
					myDateTimePicker.Bounds = new Rectangle
						(bounds.X + 2, bounds.Y + 2, 
						bounds.Width - 4, bounds.Height - 4);
					myDateTimePicker.Value = value;
					myDateTimePicker.Visible = true;
					myDateTimePicker.ValueChanged += 
						new EventHandler(TimePickerValueChanged);
				} 
				else 
				{
					myDateTimePicker.Value = value;
					myDateTimePicker.Visible = false;
				}

				if (myDateTimePicker.Visible)
					DataGridTableStyle.DataGrid.Invalidate(bounds);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message,"99999");
			}
		}
		/// <summary>
		/// 大小
		/// </summary>
		/// <param name="g"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		protected override Size GetPreferredSize(Graphics g, object value) 
		{
			return new Size(100, myDateTimePicker.PreferredHeight + 4);
		}
		/// <summary>
		/// 最小的高度
		/// </summary>
		/// <returns></returns>
		protected override int GetMinimumHeight() 
		{
			return myDateTimePicker.PreferredHeight + 4;
		}
		/// <summary>
		/// 最小的高度
		/// </summary>
		/// <param name="g"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		protected override int GetPreferredHeight(Graphics g, object value) 
		{
			return myDateTimePicker.PreferredHeight + 4;
		}
		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum) 
		{
			Paint(g, bounds, source, rowNum, false);
		}
		protected override void Paint(Graphics g, Rectangle bounds,	CurrencyManager source, int rowNum,	bool alignToRight) 
		{
			Paint(
				g,bounds, 
				source, 
				rowNum, 
				Brushes.Red, 
				Brushes.Blue, 
				alignToRight);
		}
		protected override void Paint(Graphics g, Rectangle bounds,	CurrencyManager source, int rowNum,	Brush backBrush, Brush foreBrush,bool alignToRight) 
		{
			 
			DateTime date ;
			try
			{

				date = (DateTime) GetColumnValueAtRow(source, rowNum);
			}
			catch
			{
				date=DateTime.Now;
			}
			Rectangle rect = bounds;
			g.FillRectangle(backBrush,rect);
			rect.Offset(0, 2);
			rect.Height -= 2;
			g.DrawString(date.ToString(DataType.SysDataTimeFormat), 
				this.DataGridTableStyle.DataGrid.Font, 
				foreBrush, rect);

			 
		}
		protected override void SetDataGridInColumn(DataGrid value) 
		{
			 
			base.SetDataGridInColumn(value);
			if (myDateTimePicker.Parent != null) 
			{
				myDateTimePicker.Parent.Controls.Remove (myDateTimePicker);
			}
			if (value != null) 
			{
				value.Controls.Add(myDateTimePicker);
			}
			 
		}
		/// <summary>
		/// TimePickerValueChanged
		/// </summary>
		/// <param name="sender">myDateTimePicker</param>
		/// <param name="e"></param>
		private void TimePickerValueChanged(object sender, EventArgs e) 
		{
			this.isEditing = true;
			base.ColumnStartedEditing(myDateTimePicker);
			 
		}
	}	 
}