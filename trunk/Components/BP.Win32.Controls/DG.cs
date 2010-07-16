using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BP.Web.Controls;
using BP.DA;
using BP.En;

using BP.Web;
using BP.Sys;
using BP.Win32.Comm ; 


namespace BP.Win32.Controls
{
	
	[ToolboxBitmap(typeof(System.Windows.Forms.DataGrid))]
	public class DG : System.Windows.Forms.DataGrid
	{

		#region 构造
		/// <summary>
		/// 构造
		/// </summary>
		public DG()
		{
		
			this.CurrentCellChanged+=new EventHandler(DG_CurrentCellChanged);
			//this.CurrentCellChanged

			//this.Navigate +=new NavigateEventHandler(DG_Navigate);
			//base.BackButtonClick+=new EventHandler(DG_BackButtonClick);
			_dgColumns = new DGColumns();
			
		}
		private DGColumns _dgColumns ;
		public  DGColumns Columns
		{
			get{ return this._dgColumns;  }
			set{ this._dgColumns = value; }
		}		
		#endregion

		#region 自定义属性
		
		/// <summary>
		/// DataGride 的状态。
		/// </summary>
		public DGModel _DGModel=DGModel.None;
		/// <summary>
		/// DGModel
		/// </summary>
		public DGModel DGModel
		{
			get
			{
				return _DGModel;
			}
			set
			{
				_DGModel=value;
			}
		}
		private bool _IsReadonly=false;
		public bool IsDGReadonly
		{
			get
			{
				return _IsReadonly;
			}
			set
			{
				_IsReadonly=value;
			}
		}		 
		//private void 
		public Entities HisEns=null;

		private Entity _HisEn ;
		public Entity HisEn
		{
			get
			{
				if (_HisEn==null)
					_HisEn=this.HisEns.GetNewEntity ; 					
				return _HisEn;
			}
			set
			{
				_HisEn=value;
			}
		}
		#endregion

		#region Bind
		/// <summary>
		/// bind
		/// </summary>
		public void Bind()
		{
			this.BindEnsThisOnly(this.HisEns,this.IsDGReadonly,false);
			this.PivCell=new DataGridCell(-1,-1);
		}
		/// <summary>
		/// 仅仅处理一个ens	 
		/// </summary>
		/// <param name="ens">entity</param>
		public void BindEnsThisOnly(Entities ens, bool IsReadonly, bool IsFirstBind)
		{
			//this.UnSelectAll();
			//	this.CurrentRowIndex=0;
			//this.LastTimeRowIndex=-1;

			this.IsDGReadonly = IsReadonly;
			if (this.IsDGReadonly)
				this.ReadOnly =true;
			else
				this.ReadOnly=false;


			this.DGModel =DGModel.Ens;
			if	(ens.Count < 0 )
				ens.RetrieveAll();

			this.HisEns = ens;
			this.HisEn=ens.GetNewEntity;

			//DataSet ds = new DataSet();
			//DataTable dt = new DataTable();
 			 
			this.CurrentTable= ens.ToDataTableField();
			this.CurrentTable.TableName = this.HisEn.EnDesc;
			//ds.Tables.Add(dt);
			//this.CurrentTable.RowChanging+=new DataRowChangeEventHandler(dt_RowChanging);
			//this.CurrentTable.RowChanged+=new DataRowChangeEventHandler(CurrentTable_RowChanged);
			//this.CurrentTable.
			//			DataView dv = new DataView();
			//			dv.Table=dt;
			//			dv.AllowNew=false;
			//			dv.AllowEdit=false;
			//			dv.AllowDelete=true;
			 
			//this.IsReadonly;
			this.SetDataBinding(this.CurrentTable,"");
			//this.SetDataBinding(dv,"");

			if (IsFirstBind)
			{
				this.TableStyles.Clear();
				this.InitContextMenu();
				this.CaptionText=this.HisEn.EnDesc;
				this.InitColumn(this.HisEns);
			}
		}
		public void ReSetDataSource_del(Entities ens)
		{
			DataSet ds=EnExt.ToDataSet(ens);
			this.SetDataBinding(ds,this.HisEn.EnDesc);
		}
		 
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress (e);
			MessageBox.Show(e.KeyChar.ToString());
		}
		/// <summary>
		/// 初试化列
		/// </summary>
		public void InitColumn_del() 
		{
			this.InitColumn(this.HisEns); 

			foreach(BP.En.EnDtl dtl in this.HisEn.EnMap.DtlsAll)
			{
				Entities ens = dtl.Ens;
				Entity en = ens.GetNewEntity;
				this.InitColumn(ens);
				foreach(BP.En.EnDtl dtl1 in en.EnMap.DtlsAll)
				{
					this.InitColumn(dtl1.Ens);
				}
			}
		}
		/// <summary>
		/// 初始化列
		/// </summary> 
		public void InitColumn(Entities ens)
		{

			Entity en =ens.GetNewEntity ;
			this.DGModel = DGModel.Ens;
			Entity newEn= this.HisEns.GetNewEntity;

			// 建立一个 table style.
			DataGridTableStyle dts = new DataGridTableStyle();
			dts.MappingName=en.EnDesc;

			UAC uac = new UAC();
			if (uac.IsView==false)
				throw new Exception("您不能对["+en.EnDesc+"]有查看的权限。");

			//			if ( !(uac.IsDelete || uac.IsInsert || uac.IsUpdate) )
			//				this.ReadOnly=true;

			Attr prviewAttr= new Attr();		
			foreach(Attr attr in en.EnMap.Attrs)
			{	
				#region 首先判断他是不是 readonly
				if (this.IsDGReadonly)
				{
					if (attr.MyFieldType==FieldType.Enum
						|| attr.MyFieldType==FieldType.PKEnum 
						|| attr.MyFieldType==FieldType.FK 
						||  attr.MyFieldType==FieldType.PKFK )
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						//myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
						//myDataCol.ReadOnly = true;
						if (attr.UIContralType==UIContralType.DDL)
						{
							myDataCol.Width=0;
						}
						else
						{
							myDataCol.Width=attr.UIWidth ;
						}
						dts.GridColumnStyles.Add(myDataCol);
						continue;
					}
					else if(attr.MyDataType ==DataType.AppBoolean)
					{
						DataGridBoolColumn myDataCol = new DataGridBoolColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						myDataCol.AllowNull =false;
						myDataCol.ReadOnly = true ;
						//myDataCol.NullValue = newEn.GetValBooleanByKey(attr.Key);
						dts.GridColumnStyles.Add(myDataCol);
						continue;
					}
					else
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key; 
						//myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
						myDataCol.ReadOnly = true;
						dts.GridColumnStyles.Add(myDataCol);
						//						if (attr.UIVisible==false)
						//						  myDataCol.Width=0;
						continue;
					}					
				}
				#endregion
			 
				#region  可以编辑状态。
                if (attr.MyDataType == DataType.AppDateTime || attr.MyDataType == DataType.AppDate)
				{  /*时间类型*/
					if (attr.UIIsReadonly)
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						//myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
						myDataCol.ReadOnly =true;
						//myDataCol.Format="";
						dts.GridColumnStyles.Add(myDataCol) ;
						continue;
					}
					else
					{
						DGTimePickerColumn timePickerColumnStyle = 
							new DGTimePickerColumn();
						timePickerColumnStyle.MappingName =attr.Key;
						timePickerColumnStyle.HeaderText = attr.Desc;
						timePickerColumnStyle.Width = 100;
						timePickerColumnStyle.ReadOnly = !attr.UIIsReadonly ;
						dts.GridColumnStyles.Add(timePickerColumnStyle);
						continue;
					}
				}
				else if (attr.MyDataType==DataType.AppBoolean)
				{
					DataGridBoolColumn myDataCol = new DataGridBoolColumn();
					myDataCol.HeaderText = attr.Desc;
					myDataCol.MappingName =attr.Key;
					myDataCol.AllowNull =false;
					myDataCol.ReadOnly = !attr.UIIsReadonly;
					//myDataCol.NullValue = newEn.GetValBooleanByKey(attr.Key);
					dts.GridColumnStyles.Add(myDataCol);
					continue;
				}
				else if ( attr.UIContralType==UIContralType.DDL )
				{
					prviewAttr=attr;
					DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
					myDataCol.HeaderText = attr.Desc;
					myDataCol.MappingName =attr.Key;
					//myDataCol.NullText = newEn.GetValStringByKey(attr.Key);					
					//myDataCol.Width=10; // 隐藏掉值
					myDataCol.Width=0; // 隐藏掉值
					myDataCol.ReadOnly = attr.UIIsReadonly;
					dts.GridColumnStyles.Add(myDataCol) ;
					continue;
				}
				else if ( attr.MyFieldType==FieldType.RefText ) 
				{
					if (prviewAttr.UIIsReadonly==false)
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						//myDataCol.NullText = newEn.GetValStringByKey( myDataCol.MappingName );
						myDataCol.ReadOnly = true;
						dts.GridColumnStyles.Add(myDataCol) ;
					}
					else
					{
						DGEnsColumn myDataCol = new DGEnsColumn(prviewAttr);
						//myDataCol.HisAttr = prviewAttr;
						myDataCol.HeaderText = prviewAttr.Desc;
						myDataCol.MappingName =attr.Key ;
						myDataCol.myDDL.Enabled = true;
						//myDataCol.ReadOnly = attr.UIIsReadonly ; 
						dts.GridColumnStyles.Add(myDataCol);
						continue;
					}
				}
				else 
				{
					DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
					myDataCol.HeaderText = attr.Desc;
					myDataCol.MappingName =attr.Key; 
					//myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
					myDataCol.ReadOnly = attr.UIIsReadonly ;
					dts.GridColumnStyles.Add(myDataCol) ;
					if (attr.UIVisible==false)
					{
						myDataCol.Width=0;
					}
					continue;
				}
				//this.TableStyles.Add(myDataCol);
				#endregion 
			}
			this.TableStyles.Add(dts);			 
		}
		 
		#endregion

		#region 操作		
		public void Save123()
		{
			try
			{
				if (this.HisEns.Count==0)
					return;
				this.CurrentCell=new DataGridCell(0,0); // this.PivCell;

				if (this.IsDGReadonly)
					return;

				string errorMsg=null;
				int i=0, okNum=0,errorNum=0;
				foreach(Entity en in this.HisEns)
				{
					try
					{
						en.Save();
						okNum++;
					}
					catch(Exception ex)
					{
						errorNum++;
						errorMsg+="@在保存["+i+"]记录时间出现如下错误----- .@"+ex.Message;
					}
					i++;
				}
				if (errorMsg!=null)
					throw new Exception("共有["+okNum+"]记录更新成功,["+errorNum+"]更新失败."+errorMsg);
				 
				this.Bind();
				//PubClass.Information("共有["+okNum+"]记录更新成功");
			}
			catch(Exception ex)
			{
				PubClass.Alert(ex); 
			}
		}		
		public void Save()
		{
			try
			{
				if (this.HisEns.Count==0)
					return;

				this.CurrentCell=new DataGridCell(0,0);
				if (this.IsDGReadonly)
					return;

				string errorMsg=null;
				int i=0, okNum=0,errorNum=0;
				Entities ens  =this.CurrentEnsInDG;
				foreach(Entity en in ens)
				{
					try
					{
						en.Save();
						okNum++;
					}
					catch(Exception ex)
					{
						errorNum++;
						errorMsg+="@在保存["+i+"]记录时间出现如下错误----- .@"+ex.Message;
					}
					i++;
				}
				if (errorMsg!=null)
					throw new Exception("共有["+okNum+"]记录更新成功,["+errorNum+"]更新失败."+errorMsg);
				 
				this.HisEns  = ens;
				this.Bind(); 
				PubClass.Information("共有["+okNum+"]记录更新成功");
			}
			catch(Exception ex)
			{
				PubClass.Alert(ex); 
			}
		}		
		#endregion

		#region 重写方法
		/// <summary>
		/// 当前选择的Ens.
		/// </summary>
		public Entities CurrentSelectedEns
		{
			get
			{
				Entities ens = this.HisEns.CreateInstance();
				int num = this.HisEns.Count ; 
				for(int i=0; i< num; i++)
				{
					if (this.IsSelected(i))
						ens.AddEntity(this.GetEnByRowIndex(i));
				}
				return ens;
			}
		}
		public Entities CurrentEnsInDG
		{
			get
			{
				Entities ens = this.HisEns.CreateInstance();
				int num = this.HisEns.Count ; 
				for(int i=0; i< num; i++)
				{
					ens.AddEntity(this.GetEnByRowIndex(i));
				}
				return ens;
			}
		}
		public Entity GetEnByRowIndex(int rowIndex)
		{
			Entity en = this.HisEns.GetNewEntity;
			int i = -1;
			foreach(Attr attr in en.EnMap.Attrs)
			{
				i++;
				if (attr.MyDataType == DataType.AppBoolean)
				{
					// #warning 如何解决是 checkbox 情况.
					en.SetValByKey(attr.Key,1);
				}
				else
				{
					en.SetValByKey(attr.Key ,this[rowIndex,i] );
				}	
			}
			return en;
		}
    	/// <summary>
		/// 当前选择的Entity
		/// </summary>
		public Entity CurrentRowEn
		{
			get
			{
				if (this.CurrentRowIndex==-1)
					throw new Exception("没有选择行．．．");
				return this.GetEnByRowIndex(this.CurrentRowIndex);
			}
		}		 
		/// <summary>
		/// 获取当前工作的Table
		/// </summary>
		public DataTable CurrentTable=null;	 
		#region 关于实体的操作
		 
		private void NewEn()
		{

		}
		/// <summary>
		/// 删除全部。
		/// </summary>
		public void DeleteAll()
		{
			if (MessageBox.Show("准备删除 "+this.HisEns.Count+" 行．\t\r\t\r单击＂是＂即将永久删除这些行，您将无法撤消所做的更改．", "删除确认", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2,MessageBoxOptions.DefaultDesktopOnly)==DialogResult.No)
				return;

			try
			{
				int num = this.HisEns.Count;
				for(int i=0; i< num; i++)
				{
					//this.CurrentTable.Rows[i].Delete();
					this.HisEns[0].Delete();
					this.HisEns.RemoveAt(0);
				}
				this.HisEns.Clear();
				this.Bind();
			}
			catch(Exception ex)
			{
				this.Bind();
				PubClass.Alert(ex);	
			 
			}
		}
		/// <summary>
		/// 删除当前选择纪录.
		/// </summary>
		public void DeleteSelected()
		{
			try
			{
				Entities ens = this.CurrentSelectedEns;
				if (MessageBox.Show("准备删除 "+ens.Count+" 行．\t\r\t\r单击＂是＂即将永久删除这些行，您将无法撤消所做的更改．","删除确认", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2,MessageBoxOptions.DefaultDesktopOnly)==DialogResult.No)
					return;

				ens.Delete();

				/*
				int num = this.HisEns.Count;
				for(int i=0; i< num; i++)
				{
					if (this.IsSelected(i))
					{
						if (this.HisEns[i].IsExits)
						{
							this.HisEns[i].Delete();
							this.HisEns.RemoveAt(i);
						}
						else
							this.HisEns.RemoveAt(i);
					}
				}
				*/
				//this.Bind();
			}
			catch(Exception ex)
			{
				//this.Bind();
				PubClass.Alert(ex);				 
			}
		}		 
		#endregion
		 
		public void UnSelectAll()
		{
			
			int num = this.CurrentTable.Rows.Count ; 
			for(int i=0; i< num; i++)
			{
				this.UnSelect(i);
			}
		}
		private DataGridCell PivCell = new DataGridCell(-1,-1);
		public bool IsActive=true;
		//private int PivColumnNumber=0;
		/// <summary>
		/// 在激活这个事件时可发生。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DG_CurrentCellChanged(object sender, EventArgs e)
		{
			if (this.IsDGReadonly)
				return ;

			if ( PivCell.RowNumber==-1)
			{
				this.PivCell=this.CurrentCell;
				if (this.CurrentRowIndex > this.HisEns.Count-1)
				{
					/* 如果是新行． 近来之后就点新行．　*/
					Entity en=this.HisEns.GetNewEntity;
					int i=0;
					foreach(Attr myattr in en.EnMap.Attrs)
					{
						if (myattr.MyFieldType==FieldType.RefText)
							this[this.CurrentRowIndex,i]=en.GetValRefTextByKey(myattr.Key.Replace("Text","") );
						else
							this[this.CurrentRowIndex,i]=en.GetValByKey(myattr.Key);
						i++;
					}
					this.HisEns.AddEntity(en);
				}
				return;
			}

			if (this.PivCell.RowNumber!=this.CurrentCell.RowNumber)
			{
				/* 如果换行 */
				if (this.CurrentRowIndex > this.HisEns.Count-1)
				{
					/* 如果是新行．*/
					Entity en=this.HisEns.GetNewEntity;
					int i=0;
					foreach(Attr myattr in en.EnMap.Attrs)
					{
						if (myattr.MyFieldType==FieldType.RefText)
							this[this.CurrentRowIndex,i]=en.GetValRefTextByKey(myattr.Key.Replace("Text","") );
						else
							this[this.CurrentRowIndex,i]=en.GetValByKey(myattr.Key);

						i++;
					}
					this.HisEns.AddEntity(en);
					return;
				}
				else
				{
					/* 非换行。 */
					try
					{
						Entity en= this.GetEnByRowIndex(this.CurrentCell.RowNumber);
						en.Update();
						//en.Update();
					}
					catch(Exception ex)
					{
						throw ex;
					}

//					int i=0;
//					foreach(Entity en in this.HisEns)
//					{
//						
//						en = this.GetEnByRowIndex(i);
//						i++;
//					}
				}
 
			}

			try
			{
				// 更改 单元格的值。 
				object obj =this[PivCell.RowNumber,PivCell.ColumnNumber];
				this.GetEnByRowIndex(PivCell.RowNumber).verifyData();
			 
				// 设置当前的cell
				this.PivCell =this.CurrentCell;

			}
			catch(Exception ex)
			{
				// 如果校验不能通过， 焦点就设置为原来的焦点。
				if (this.CurrentCell.RowNumber==this.PivCell.RowNumber
					&&  this.CurrentCell.ColumnNumber==this.PivCell.ColumnNumber)
				{
					this.CurrentCell=this.PivCell; 
					this.PivCell= new DataGridCell(-1,-1);

				}
				else
				{
					PubClass.Alert(ex);
					this.CurrentCell=this.PivCell; 
					this.PivCell= new DataGridCell(-1,-1);
				}
			}
		}
		
	 

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);

			if (DGModel!=DGModel.Ens)
				return ; 
			return;
			if ( ! (e.Button==System.Windows.Forms.MouseButtons.Right || e.Button==System.Windows.Forms.MouseButtons.Left) )
			{
				/* 如果鼠标不等于左右键 ，就不处理这个事件。*/
				return ;
			}


			System.Windows.Forms.DataGrid.HitTestInfo hti;			 
				hti = this.HitTest(e.X, e.Y);
			try
			{
				CurrentRowIndex = hti.Row ; 
			}
			catch
			{

			}

			string message = "You clicked ";
			 
 
			switch (hti.Type) 
			{
				case System.Windows.Forms.DataGrid.HitTestType.None :
					this.ContextMenu=null;
					message += "the background.";
					break;
				case System.Windows.Forms.DataGrid.HitTestType.Cell :
					// 如果是在cell上。
					if (e.Button==System.Windows.Forms.MouseButtons.Right)
					{
						if (this.CurrentAttr.IsCanUseDefaultValues)
						    this.SetContextMenuItemEnable("DefaultValues",true);
//						this.ContextMenu.Show(this, new Point(e.X,e.Y));
					}
					message += "cell at row " + hti.Row + ", col " + hti.Column;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader :
				{
					//this.CurrentCell.RowNumber =hti.Column ;
					this.ContextMenu=null;
					message += "the column header for column " + hti.Column;
					break;
				}
				case System.Windows.Forms.DataGrid.HitTestType.RowHeader :
					// 在row header 上，不让使用获取或设置默认值．
					if (e.Button==System.Windows.Forms.MouseButtons.Right)
					{					
						this.SetContextMenuItemEnable("DefaultValues",false);
						this.ContextMenu.Show(this, new Point(e.X,e.Y)) ;					  
					}
					message += "the row header for row " + hti.Row;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.ColumnResize :
					this.ContextMenu=null;
					message += "the column resizer for column " + hti.Column;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.RowResize :
					this.ContextMenu=null;
					message += "the row resizer for row " + hti.Row;
					break;
				case System.Windows.Forms.DataGrid.HitTestType.Caption :
					this.ContextMenu=null;
					message += "the caption";
					break;
				case System.Windows.Forms.DataGrid.HitTestType.ParentRows :
					this.ContextMenu=null;
					 
					message += "the parent row";
					break;
			}

		//	MessageBox.Show(message) ; 


			if (e.Button==System.Windows.Forms.MouseButtons.Right)
			{
				/* 在右键按下时间 */

				//				CMenu menu= new CMenu();
				//				this.ContextMenu = menu;
				//				menu.Popup+=new EventHandler(menu_Popup);
				//				this.ContextMenu = menu;
				

			}
			else if (e.Button==System.Windows.Forms.MouseButtons.Left)
			{
				/*在左键按下时间*/

			}	
		}
		 
		/// <summary>
		/// 初始化cell右键菜单
		/// </summary>
		private void InitContextMenu()
		{
			/* 如果是在右键上 
						 * 1，加入 获取或者设置默认值。
						 * 2，加入 删除行。
						 * 3，如果当前的属性是fk.加入编辑xxx. 打开后判断权限的问题，为了提高工作效率。
						 * */
			ContextMenu cm = new ContextMenu();
			this.ContextMenu= cm;

			BPMenuItem midel = new BPMenuItem();
			midel.Click +=new EventHandler(ContextMenu_Click); // 加入事件
			midel.Text="删除行";
			midel.Enabled=true;
			midel.Tag="Delete";
			midel.Visible=true;
			midel.ShowShortcut=true;
			midel.Shortcut = Shortcut.CtrlD; // shen
			this.ContextMenu.MenuItems.Add(midel);

			BPMenuItem miDefaultValues = new BPMenuItem();
			miDefaultValues.Click +=new EventHandler(ContextMenu_Click); // 加入事件
			miDefaultValues.Text="获取或设置默认值";			 
			miDefaultValues.Enabled=true; 
			miDefaultValues.Tag="DefaultValues";
			miDefaultValues.ShowShortcut=true;
			miDefaultValues.Shortcut = Shortcut.CtrlH;
			this.ContextMenu.MenuItems.Add(miDefaultValues);

			BPMenuItem miCard = new BPMenuItem();
			miCard.Click +=new EventHandler(ContextMenu_Click); // 加入事件
			miCard.Text="信息编辑";
			miCard.Enabled=true;
			miCard.Tag="Card";
			miCard.ShowShortcut=true;
			miCard.Shortcut = Shortcut.CtrlO;
			this.ContextMenu.MenuItems.Add(miCard);

			BPMenuItem miNew = new BPMenuItem();
			miNew.Click +=new EventHandler(ContextMenu_Click); // 加入事件
			miNew.Text="新建";
			miNew.Enabled=true;
			miNew.Tag="New";
			miNew.ShowShortcut=true;
			miNew.Shortcut = Shortcut.CtrlN;

			this.ContextMenu.MenuItems.Add(miNew);

			#region 加入他的明细			
			EnDtls enDtls= this.HisEn.EnMap.Dtls;
			if ( enDtls.Count > 0 )
			{								
				foreach(EnDtl enDtl in enDtls)
				{	 
					BPMenuItem miDtl = new BPMenuItem();
					miDtl.Click +=new EventHandler(ContextMenu_Click); // 加入事件
					miDtl.Text=enDtl.Desc;
					miDtl.Enabled=true;
                    miDtl.Tag = enDtl.EnsName;
					miDtl.ShowShortcut=true;
					miDtl.Shortcut=Shortcut.CtrlR ;
					this.ContextMenu.MenuItems.Add(miDtl);
				}
			}
			#endregion

			#region 加入一对多的实体编辑
			AttrsOfOneVSM oneVsM= this.HisEn.EnMap.AttrsOfOneVSM;
			if ( oneVsM.Count > 0 )
			{
				
				foreach(AttrOfOneVSM vsM in oneVsM)
				{
					BPMenuItem miVsM = new BPMenuItem();
					miVsM.Click +=new EventHandler(ContextMenu_Click); // 加入事件
					miVsM.Text=vsM.Desc;
					miVsM.Enabled=true;
					miVsM.Tag=vsM.EnsOfMM.ToString() ;
					miVsM.ShowShortcut=true;
					miVsM.Shortcut=Shortcut.CtrlR ;
					this.ContextMenu.MenuItems.Add(miVsM);
				}
			}
			#endregion

			 


		}
		/// <summary>
		/// 右键菜单，事件处理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ContextMenu_Click(object sender, EventArgs e)
		{
			BPMenuItem item = (BPMenuItem)sender; 
			switch(item.Text)
			{
				case "新建":
					NewEn();
					break;
				case "删除行":
					DeleteSelected();
					break;
				case "卡片":
					this.Card();
					break;
				case "DefaultValues":
					this.InvokeDefaultValues();
					break;
				default:
					UIEnsRelation uir =new UIEnsRelation();
					uir.BindEn(this.CurrentRowEn,this.IsDGReadonly) ;
					uir.ShowDialog();					
					return ;
			}
		}

		public Attr CurrentAttr
		{
			get
			{
				return this.HisEn.EnMap.Attrs[this.CurrentCell.ColumnNumber] ;
			}
		}
		public Attr PivAttr
		{
			get
			{
				return this.HisEn.EnMap.Attrs[this.PivCell.ColumnNumber] ;
			}
		}
		public void InvokeDefaultValues()
		{
			BP.Win32.Controls.UIWinDefaultValues ui = new UIWinDefaultValues(this.HisEn, this.CurrentAttr.Key );
		}
		public void Card()
		{
			BP.Win32.Comm.En en = new BP.Win32.Comm.En() ; 
			en.Bind2(this.CurrentRowEn );
			en.Show();

			return ;
		}
		/// <summary>
		/// 设置一个菜单是否可以使用
		/// </summary>
		/// <param name="Tag">名称</param>
		/// <param name="enable">true/false</param>
		public void SetContextMenuItemEnable(string Tag, bool enable)
		{
			try
			{
				if ( this.ContextMenu.MenuItems==null)
					this.InitContextMenu();
			}
			catch
			{
				this.InitContextMenu();
				this.SetContextMenuItemEnable(Tag,enable) ; 
			}
				
			foreach(BPMenuItem menu in this.ContextMenu.MenuItems)
			{
				if (menu.Tag == Tag)
				{
					menu.Enabled = enable;
					return;
				}
			}
		} 
		/// <summary>
		/// 单击事件
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);
			
		}
		/// <summary>
		/// 双击事件
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick (e);
			this.Card();
		}	
		

		#endregion

//		/// <summary>
//		/// 短名称
//		/// </summary>
//		private string CaptionTextOfShortName
//		{
//			get
//			{
//				if (this.DataMember.IndexOf( "." )==-1)
//					return this.DataMember;
//				int i =this.DataMember.LastIndexOf( "." ) ;
//				return this.DataMember.Substring(i+1  );			 
//			}
//		}
		 
		 

		#region 取到 PK val。
		/// <summary>
		///  查找当前的选择的 OID ， 适用于对OID 作 key 的。情况。
		/// </summary>
		public int CurrendSelectedOID
		{
			get
			{
				return int.Parse(this.CurrendSelectedNo);
			}
		}
		/// <summary>
		///  查找当前的选择的 No ， 适用于No 作 key 的情况。
		/// </summary>
		public string CurrendSelectedNo
		{
			get
			{				
				if (this.CurrentCell.RowNumber < 0  )
					throw new Exception("@没有选择行。");
				throw new Exception("还没有实现。");
			}
		}
		#endregion

	 
	}
}
