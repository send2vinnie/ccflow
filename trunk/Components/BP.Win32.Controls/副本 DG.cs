using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BP.Web.Controls;
using BP.DA;
using BP.En;
using BP.En.Base;
using BP.Web;
using BP.Sys;
using BP.Win32.Comm ; 


namespace BP.Win32.Controls
{
	/// <summary>
	/// 当前的DataGride 状态。
	/// </summary>
	public enum DGModel
	{
		/// <summary>
		/// none
		/// </summary>
		None,
		/// <summary>
		/// 编辑Ens
		/// </summary>
		Ens,
		/// <summary>
		/// en
		/// </summary>
		En
	}
	[ToolboxBitmap(typeof(System.Windows.Forms.DataGrid))]
	public class DG : System.Windows.Forms.DataGrid
	{
		#region 构造
		/// <summary>
		/// 构造
		/// </summary>
		public DG()
		{
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
		public bool IsReadonly
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
		private Entities _HisEns ;
		 
		public Entities HisEns
		{
			get
			{
				return _HisEns;
			}
			set
			{
				_HisEns=value;
			}
		}		 
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
		/// 仅仅处理一个ens	 
		/// </summary>
		/// <param name="ens">entity</param>
		public void BindEnsThisOnly(Entities ens, bool IsReadonly, bool IsFirstBind)
		{
			//this.UnSelectAll();
		//	this.CurrentRowIndex=0;
			//this.LastTimeRowIndex=-1;

			this.IsReadonly = IsReadonly;
			this.ReadOnly =false;		 


			this.DGModel =DGModel.Ens;
			this.HisEns = ens;

			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
 
			if (ens.Count > 0)
			{
				dt= ens.ToDataTable();
			}
			else
			{
				dt= ens.RetrieveAllToTable();
			}

			
			dt.TableName = this.HisEn.EnDesc;
			ds.Tables.Add(dt);
			 
			this.SetDataBinding( ds,dt.TableName);
			if (IsFirstBind)
			{
				this.TableStyles.Clear();
			}
  
			#region 加入要重写的事件。
			if (IsFirstBind)
			{
				this.CurrentCellChanged+=new EventHandler(DG_CurrentCellChanged);
				//this.ParentChanged+=new EventHandler(DG_ParentChanged);
				//this.Navigate+=new NavigateEventHandler(DG_Navigate);
				this.KeyPress+=new KeyPressEventHandler(DG_KeyPress);
			}
			#endregion

			if (IsFirstBind)
			{
				this.InitContextMenu();
				this.CaptionText=this.HisEn.EnDesc;
				this.InitColumn(this.HisEns);
			}
			return ;
		}
		public void ReSetDataSource(Entities ens)
		{
			DataSet ds=EnExt.ToDataSet(ens);
			this.SetDataBinding(ds,this.HisEn.EnDesc);

		}
		/// <summary>
		/// thisOnly
		/// </summary>
		/// <param name="ens">ens</param>
		/// <param name="thisOnly">是不是现实单个</param>
		public void BindEns(Entities ens )
		{
			this.HisEns=ens;			 
			/* 如果只是这一个ens */

			this.SetDataBinding(EnExt.ToDataSet(ens),this.HisEn.EnDesc);


//			this.DataSource =EnExt.ToDataSet(ens);
//			this.DataMember=this.HisEn.EnDesc;		 

			#region 加入要重写的事件。
			this.CurrentCellChanged+=new EventHandler(DG_CurrentCellChanged);
			this.ParentChanged+=new EventHandler(DG_ParentChanged);
			//this.Navigate+=new NavigateEventHandler(DG_Navigate);
			this.KeyPress+=new KeyPressEventHandler(DG_KeyPress);			
			#endregion

			this.CaptionText = this.HisEn.EnDesc ; 
			this.InitColumn();
			// 初试化菜单．
			this.InitContextMenu();
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
		public void InitColumn() 
		{
			this.InitColumn(this.HisEns); 

			foreach(BP.En.Base.EnDtl dtl in this.HisEn.EnMap.DtlsAll)
			{
				Entities ens = dtl.Ens;
				Entity en = ens.GetNewEntity;
				this.InitColumn(ens);
				foreach(BP.En.Base.EnDtl dtl1 in en.EnMap.DtlsAll)
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
			Entity newEn= this.HisEns.GetNewEntity ;

			// 建立一个 table style.
			DataGridTableStyle dts = new DataGridTableStyle();
			dts.MappingName=en.EnDesc; 

			SysEnsUAC uac = new SysEnsUAC();

			if (uac.IsView==false)
				//throw new Exception("您不能对["+en.EnDesc+"]有查看的权限。");

			if ( !(uac.IsDelete || uac.IsInsert || uac.IsUpdate))
				this.ReadOnly=true;		 

			Attr prviewAttr= new Attr();		
			foreach(Attr attr in en.EnMap.Attrs)
			{	
				#region 首先判断他是不是 readonly
				if (this.IsReadonly)
				{
					if (attr.MyFieldType==FieldType.Enum
						|| attr.MyFieldType==FieldType.PKEnum 
						|| attr.MyFieldType==FieldType.FK 
						||  attr.MyFieldType==FieldType.PKFK )
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
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
						myDataCol.NullValue = newEn.GetValBooleanByKey(attr.Key);
						dts.GridColumnStyles.Add(myDataCol);
						continue;
					}
					else
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key; 
						myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
						myDataCol.ReadOnly = true;
						dts.GridColumnStyles.Add(myDataCol);
//						if (attr.UIVisible==false)
//						  myDataCol.Width=0;
						continue;
					}

					
				}

				#endregion
			 
				#region  可以编辑状态。
				if (attr.MyDataType==DataType.AppDatetime || attr.MyDataType==DataType.AppDate )
				{  /*时间类型*/
					if (attr.UIIsReadonly)
					{
						DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
						myDataCol.HeaderText = attr.Desc;
						myDataCol.MappingName =attr.Key;
						myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
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
					myDataCol.NullValue = newEn.GetValBooleanByKey(attr.Key);
					dts.GridColumnStyles.Add(myDataCol);
					continue;
				}
				else if ( attr.UIContralType==UIContralType.DDL )
				{
					prviewAttr=attr;
					DataGridTextBoxColumn myDataCol = new DataGridTextBoxColumn();
					myDataCol.HeaderText = attr.Desc;
					myDataCol.MappingName =attr.Key;
					myDataCol.NullText = newEn.GetValStringByKey(attr.Key);					
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
					myDataCol.NullText = newEn.GetValStringByKey(attr.Key);
					myDataCol.ReadOnly = attr.UIIsReadonly ;
					dts.GridColumnStyles.Add(myDataCol) ;
					continue;
				}
				//this.TableStyles.Add(myDataCol);
				#endregion 
			}
			this.TableStyles.Add(dts);			 
		}
		 
		#endregion

		#region 操作
		public void SaveAllEns()
		{
			try
			{
				if (this.IsReadonly)
					return;
				Entities ens = this.CurrentDataEns;
				ens.Update();
				MessageBox.Show("共有["+ens.Count+"]记录更新成功.");
			}
			catch(Exception ex)
			{
				PubClass.Alert(ex);				 
			}
		}
		#endregion

		#region 重写方法
		/// <summary>
		/// OnCurrentCellChanged
		/// </summary>
		/// <param name="e"></param>
		protected override void OnCurrentCellChanged(EventArgs e)
		{
			base.OnCurrentCellChanged (e);			  
			if (this.DGModel !=DGModel.Ens) 
				return;
			return; 
		}
//		/// <summary>
//		/// 在导航按钮返回时间触发的事件
//		/// </summary>
//		/// <param name="sender"></param>
//		/// <param name="e"></param>
//		private void DG_BackButtonClick(object sender, EventArgs e)
//		{
//			LastTimeRowIndex = 0 ;
//		}
		public Entities CurrentSelectedEns
		{
			get
			{
				Entities ens = this.HisEns.CreateInstance();


				int num = this.CurrentTable.Rows.Count ; 
//				for(int i=0; i< num; i++)
//				{
//					if (this.SetDataBindingi);
//				}
// 

				

				//foreach(


				 
			}
		}
		/// <summary>
		/// 当前选择的Entity
		/// </summary>
		public Entity CurrentRowEn
		{
			get
			{
				if (this.CurrentRowIndex==-1)
					throw new Exception("没有选择行．");
 
				Entity en =this.CurrentEns.GetNewEntity ;
				 
				int i = -1;					 
				foreach(Attr attr in en.EnMap.Attrs)
				{
					i++;					
					if (attr.MyDataType == DataType.AppBoolean)
					{
						// #warning 如何解决是 checkbox 情况.
						en.SetValByKey(attr.Key , 1 );
					}
					else
					{
						en.SetValByKey(attr.Key , this[this.CurrentRowIndex,i]);
					}
				}
				return en;
			}
		}
		/// <summary>
		/// 更新全部的Ens
		/// </summary>
		public void UpdateAll()
		{
			Entities ens =this.CurrentDataEns;		 
			foreach(Entity en in ens)
			{
				en.Update();
			}
			MessageBox.Show("["+ens.Count+"]更新成功");
		}		
		/// <summary>
		/// 
		/// </summary>
		public Entities CurrentDataEns
		{
			get
			{
				Entities ens =this.CurrentEns;
				ens.Clear();
				int currRowIndex=this.CurrentCell.RowNumber ;				 
				int rowNum = this.CurrentTable.Rows.Count;
				for(int i=0; i< rowNum;i++)
				{
					this.CurrentRowIndex=i;
					ens.AddEntity(this.CurrentRowEn); 
				}
				return ens;
			}
		}
		/// <summary>
		/// 获取当前工作的Table
		/// </summary>
		public DataTable CurrentTable
		{
			get
			{
				return this.HisDataSet.Tables[this.HisEn.EnDesc] ; 
			}
		}
		/// <summary>
		/// 数据源
		/// </summary>
		public DataSet HisDataSet
		{
			get
			{
				return (DataSet)this.DataSource ; 
			}
		}

		#region 关于实体的操作
		/// <summary>
		/// 保存实体， 在换行时间保存实体。
		/// </summary>
		private void SaveEn()
		{
			//判断实体有没有变化。
			if (this.IsReadonly)
				return ;
			// 当前的实体更新。
			this.CurrentRowEn.Update();
			return ;
		}
		private void NewEn()
		{

		}
		/// <summary>
		/// 删除当前纪录.
		/// </summary>
		public void DeleteAll()
		{
			try
			{
				Entities ens = this.CurrentDataEns;
				ens.Delete();
				MessageBox.Show("共有["+ens.Count+"]记录删除成功.");
			}
			catch(Exception ex)
			{
				PubClass.Alert(ex);				 
			}
		}
		public void DeleteSelectedEns()
		{
			//			if (MessageBox.Show("将要删除第["+this.CurrentRowIndex+"]行数据，要删除吗？","删除确认", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2,MessageBoxOptions.RightAlign)==DialogResult.No)
			//				return ;
			this.CurrentSelectedEns.Delete();

		}
		/// <summary>
		/// 删除实体
		/// </summary>
		public void DeleteCurrentRowEn()
		{
			if (this.CurrentRowIndex == -1)
			{
				MessageBox.Show("没有选中行。");
				return ;
			}

			if (MessageBox.Show("将要删除第["+this.CurrentRowIndex+"]行数据，要删除吗？","删除确认", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2,MessageBoxOptions.RightAlign)==DialogResult.No)
				return ;

			try
			{
				this.CurrentRowEn.Delete();
			}
			catch(Exception ex)
			{
				MessageBox.Show("删除出现下列问题"+ex.Message);
			}
			DataSet ds = (DataSet)this.DataSource ;
			ds.Tables[this.DataMember].Rows[this.CurrentCell.RowNumber].Delete();
			return ;
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
		/// <summary>
		/// 在激活这个事件时可发生。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DG_CurrentCellChanged(object sender, EventArgs e)
		{
			if (this.DGModel!=DGModel.Ens)
				return ;  
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
					miDtl.Tag=enDtl.ClassName ;
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
			switch(item.Tag)
			{
				case "New":
					NewEn();
					break;
				case "Delete":
					DeleteCurrentRowEn();
					break;
				case "Card":
					this.Card();
					break;
				case "DefaultValues":
					this.InvokeDefaultValues();
					break;
				default:
					UIEnsRelation uir =new UIEnsRelation();
					uir.BindEn(this.CurrentRowEn,this.IsReadonly) ;
					uir.ShowDialog();					
					return ;
			}
		}

		public Attr CurrentAttr
		{
			get
			{
				return this.CurrentEns.GetNewEntity.EnMap.Attrs[this.CurrentCell.ColumnNumber] ;
			}
		}
		public void InvokeDefaultValues()
		{
			BP.Win32.Controls.UIWinDefaultValues ui = new UIWinDefaultValues(this.CurrentEns.GetNewEntity, this.CurrentAttr.Key );
		}
		public void Card()
		{
			UIEn ui = new UIEn();
			ui.SetData(this.CurrentRowEn,this.IsReadonly);
			ui.ShowDialog();
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

			if (DGModel==DGModel.Ens)
			{
				if (this.CurrentRowIndex ==-1)
					return ;
				
				this.Card();
				//if (this.CurrentCell==null)
				//	return;
				//UIWinDefaultValues win = new UIWinDefaultValues(this.HisEn, this.HisEn.EnMap.Attrs[this.CurrentCell.ColumnNumber].Key) ;
			}
			
		}	
		
		//protected override void 

		#endregion
		/// <summary>
		/// 短名称
		/// </summary>
		private string CaptionTextOfShortName
		{
			get
			{
				if (this.DataMember.IndexOf( "." )==-1)
					return this.DataMember;
				int i =this.DataMember.LastIndexOf( "." ) ;
				return this.DataMember.Substring(i+1  );			 
			}
		}		
		/// <summary>
		/// 获得当前编辑的
		/// </summary>
		private Entities _CurrentEns=null;
		/// <summary>
		/// 获得当前编辑的Ens
		/// </summary>
		private Entities CurrentEns
		{
			get
			{
				if (_CurrentEns==null)
				{
					if (this.HisEn.EnDesc==this.CaptionTextOfShortName)
					{
						_CurrentEns= this.HisEns ;
						return _CurrentEns;
					}
					foreach(EnDtl en in this.HisEn.EnMap.Dtls)
					{
						Entity entity = en.Ens.GetNewEntity ; 
						if (entity.EnDesc==this.CaptionTextOfShortName)
						{
							_CurrentEns= en.Ens;
							return _CurrentEns ; 
							
						}
						else
						{
							foreach(EnDtl dtl in entity.EnMap.Dtls)
							{
								if (dtl.Ens.GetNewEntity.EnDesc==this.CaptionTextOfShortName)
								{
									_CurrentEns= dtl.Ens ;
									return _CurrentEns ; 
								}								
							}
						}
					}

					foreach(AttrOfOneVSM en in this.HisEn.EnMap.AttrsOfOneVSM)
					{
						if (en.EnsOfMM.GetNewEntity.EnDesc==this.CaptionTextOfShortName)
						{
							_CurrentEns = en.EnsOfMM;
							return _CurrentEns;
						}
					}
				}
				else
				{
					return _CurrentEns;
				}

				throw new Exception("没有找到当前工作的Ens");
			}
		}
//		private void DG_Navigate(object sender, NavigateEventArgs ne)
//		{
//			LastTimeRowIndex = 0 ;
//			this.CaptionText = this.DataMember;		 
//		}

		private void DG_ParentChanged(object sender, EventArgs e)
		{
			MessageBox.Show("DG_ParentChanged: sender  peng, "+sender.ToString()+" sss " + e.ToString() ) ; 
		}

		private void DG_KeyPress(object sender, KeyPressEventArgs e)
		{
			MessageBox.Show(e.KeyChar.ToString()) ;
		}

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
