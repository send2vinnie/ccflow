using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Sys;
using BP.Win32.Controls;
using BP.Port;
using BP.Web; 
using BP.Pub;
using BP.Web.Controls;

namespace BP.Win32.Comm
{
	/// <summary>
	/// UIEns 的摘要说明。
	/// </summary>
	public class UIEns :  BP.Win32.PageBase
	{
		private BP.Win32.Controls.BPToolbar bpToolbar1;
		private BP.Win32.Controls.DG dg1;
		public System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.ComponentModel.IContainer components; 
		public System.Windows.Forms.StatusBarPanel sta1;
		private System.Windows.Forms.ToolBarButton Btn_New;
		private System.Windows.Forms.ToolBarButton Btn_Xml;
        private System.Windows.Forms.ToolBarButton Btn_Refurbish;
		private System.Windows.Forms.ToolBarButton Btn_Edit1;
		private System.Windows.Forms.ToolBarButton Btn_Save1;
		private System.Windows.Forms.ToolBarButton Btn_Delete;
		private System.Windows.Forms.ToolBarButton Btn_Execl;
		private System.Windows.Forms.ToolBarButton Btn_Close;
		private System.Windows.Forms.ToolBarButton Btn_Income;
		private System.Windows.Forms.ToolBarButton Btn_DeleteAll;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private GB gb1;	
		private System.Windows.Forms.StatusBarPanel sta3;

		private void InitToolBar()
		{
			try
			{
				//this.bpToolbar1.Buttons.Clear();
			}
			catch
			{

			}

			#region tool1
			//			this.bpToolbar1.AddBtn(NamesOfBtn.Card);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.New);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.Save);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.SaveAndNew);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.SaveAndClose);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.Delete);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.Adjunct);
			//			this.bpToolbar1.AddBtn(NamesOfBtn.Close);
			#endregion
		}
		public void BindEns(Entities ens, UAC uac,bool IsFirstBind)
		{
			if (IsFirstBind==false)
			{
				this.dg1.DataSource=null;
				this.dg1.DataMember=null;
			}

			this.HisSysEnsUAC = uac;

			this.HisEn =ens.GetNewEntity;
			this.HisEns= ens ;
			this.Text  = this.HisEn.EnDesc;

			#region  设置权限控制
			// 设置权限控制。
			foreach (ToolBarButton btn in this.bpToolbar1.Buttons)
			{
				switch( btn.Text )
				{
					case "删除":
					case "清除":
						btn.Enabled=uac.IsDelete;
						continue;
					case "编辑":
						btn.Enabled=uac.IsDelete;
						continue;
					case "新建":
						btn.Enabled=uac.IsInsert;
						continue;
					case "保存":
						btn.Enabled = uac.IsUpdate ;
						continue;
					default:
						continue;
				}
			}
			#endregion
		
			this.AddSearchControl();
			// this.InitToolBar();			
			this.dg1.BindEnsThisOnly(this.HisEns,!uac.IsUpdate,true);

			btn_SearchClick(null,null);
		}

		/// <summary>
		/// 加载实体 
		/// </summary>
		/// <param name="ens"></param>
		public void BindEns_del(Entities ens, bool IsReadonly,bool IsFirstBind)
		{
			UAC uac = new UAC();
			uac.IsUpdate = !IsReadonly ;
			
			this.HisEn =ens.GetNewEntity;
			this.HisEns= ens ;
			this.Text  = "感谢您选择"+SystemConfig.DeveloperShortName+","+this.HisEn.EnDesc ;
			this.AddSearchControl();			
			// this.InitToolBar();
			this.dg1.BindEnsThisOnly(this.HisEns,IsReadonly,IsFirstBind);
		}

		#region 构造函数
		public UIEns()
		{
			InitializeComponent();			

		}
	 
		#endregion 构造函数

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			this.sta3.Text = "当前时间:" + System.DateTime.Now.ToString("yyyy年M月d日,HH:mm");
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

		#region 自动增加控件
		/// <summary>
		/// 加入查询功能。
		/// </summary>
		public void AddSearchControl()
		{
			
			#region 加入查询功能
			//			this.gb1.Controls.Clear();

		 

			int width=100;
			int y=10;
			int x=1;
			int hight=22;
			// lab提示文本
			Lab lab = new Lab();
			lab.Text="输入关键字:";
			lab.Name="Lab_Search";		 
			lab.Location =new Point( x+5+10,y) ; 
			lab.Size= new Size(80,hight-10);
			lab.Visible=true;
			this.gb1.Controls.Add(lab);

			// TB式查询条件			
			TB tb = new TB();			
			//tb.Text="输入关键字:";
			tb.Name="TB_Search";
			x+=100;
			tb.Location = new Point( x,y );
			tb.Size= new Size(width,hight);
			tb.Visible=true;
			this.gb1.Controls.Add(tb);

			//int OffsetH = 0; //偏移量

			#region 添加所有DLL式查询条件
			Map map =this.HisEn.EnMap;
			foreach(Attr attr in map.SearchAttrs)
			{
				if (attr.MyFieldType==FieldType.RefText)				 
					continue;
				 
				DDL ddl = new DDL();
				ddl.AddAllLocation = BP.Web.Controls.AddAllLocation.TopAndEnd ;
				if (attr.MyFieldType==FieldType.PKFK || attr.MyFieldType==FieldType.FK) 
				{
					 
					Entities myens =DA.ClassFactory.GetEns(attr.UIBindKey) ; 
					myens.RetrieveAll();
					ddl.BindEns( myens,attr.UIRefKeyText , attr.UIRefKeyValue );
				
				}
				else if (attr.MyFieldType==FieldType.PKEnum || attr.MyFieldType==FieldType.Enum ) 
				{
					SysEnums enums = new SysEnums(attr.UIBindKey) ;				
					ddl.BindEns(enums);
				}
				else
				{
					throw new Exception("@属性"+attr.Key+" 不是枚举类型，也不是外键 不能作为查询属性。");
				}
				x += 100;
				ddl.Location = new Point(x ,y ) ; 
				ddl.Size= new Size(width,hight);
				ddl.Visible=true;
				ddl.Name="DDL_"+attr.Key;
				this.gb1.Controls.Add(ddl);
			}
			#endregion 循环所有DLL式查询条件

			// 查询按钮 
			Btn btn = new Btn();
			x += 100;
			btn.Location =  new Point( x, y);
			btn.Size=new Size(width,hight);
			btn.Name="Btn_Search";
			btn.Text="查询(&F)";
			btn.Click+=new EventHandler(btn_SearchClick);
			this.gb1.Controls.Add(btn);

			this.gb1.Width=x+100;
			this.gb1.Text="查询";

			#endregion
		}

		/// <summary>
		/// 自动创建控件
		/// </summary>
		public void AutoAddControl_old()
		{
			#region 加入查询功能
			//			this.gb1.Controls.Clear();

			// lab提示文本			
			Lab lab = new Lab();			
			lab.Text="输入检索条件：";
			lab.Name="lab_search";		 
			lab.Location =new Point( 45,52 )  ; 
			lab.Size= new Size(100,22);
			lab.Visible=true;
			this.Controls.Add(lab);

			// TB式查询条件			
			TB tb = new TB();			
			tb.Text="输入关键字";
			tb.Name="TB_Search";			 
			tb.Location = new Point( 145,50 ) ; 
			tb.Size= new Size(100,22);
			tb.Visible=true;
			this.Controls.Add(tb);

			int OffsetH = 0; //偏移量

			#region 添加所有DLL式查询条件
			Map map =this.HisEn.EnMap;
			foreach(Attr attr in map.SearchAttrs)
			{
				if (attr.MyFieldType==FieldType.RefText)				 
					continue;
				 
				DDL ddl = new DDL();
				ddl.AddAllLocation = BP.Web.Controls.AddAllLocation.TopAndEnd ;
				if (attr.MyFieldType==FieldType.PKFK || attr.MyFieldType==FieldType.FK) 
				{
					Entities myens =DA.ClassFactory.GetEns(attr.UIBindKey) ; 
					myens.RetrieveAll();
					ddl.BindEns( myens,attr.UIRefKeyText , attr.UIRefKeyValue );
				}
				else if (attr.MyFieldType==FieldType.PKEnum || attr.MyFieldType==FieldType.Enum ) 
				{
					SysEnums enums = new SysEnums(attr.UIBindKey) ;				
					ddl.BindEns(enums);
				}
				else
				{
					throw new Exception("@属性"+attr.Key+" 不是枚举类型，也不是外键 不能作为查询属性。");
				}			
				
				ddl.Location = new Point(OffsetH+250,50 ) ; 
				ddl.Size= new Size(100,22);
				ddl.Visible=true;
				ddl.Name="DDL_"+attr.Key;
				this.Controls.Add(ddl);
				OffsetH += 100;
			}
			#endregion 循环所有DLL式查询条件
			// 查询按钮 
			Btn btn = new Btn();
			btn.Location =  new Point( OffsetH+250, 50 );
			btn.Size=new Size(60,22);
			btn.Name="Btn_Search";			
			btn.Text="查询(&F)";
			btn.Click+=new EventHandler(btn_SearchClick);
			this.Controls.Add(btn);

			#endregion
		}
		#endregion 自动增加控件

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIEns));
            this.bpToolbar1 = new BP.Win32.Controls.BPToolbar();
            this.Btn_New = new System.Windows.Forms.ToolBarButton();
            this.Btn_Edit1 = new System.Windows.Forms.ToolBarButton();
            this.Btn_Save1 = new System.Windows.Forms.ToolBarButton();
            this.Btn_Delete = new System.Windows.Forms.ToolBarButton();
            this.Btn_DeleteAll = new System.Windows.Forms.ToolBarButton();
            this.Btn_Refurbish = new System.Windows.Forms.ToolBarButton();
            this.Btn_Execl = new System.Windows.Forms.ToolBarButton();
            this.Btn_Xml = new System.Windows.Forms.ToolBarButton();
            this.Btn_Income = new System.Windows.Forms.ToolBarButton();
            this.Btn_Close = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dg1 = new BP.Win32.Controls.DG();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.sta1 = new System.Windows.Forms.StatusBarPanel();
            this.sta3 = new System.Windows.Forms.StatusBarPanel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gb1 = new BP.Win32.Controls.GB();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sta1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sta3)).BeginInit();
            this.SuspendLayout();
            // 
            // bpToolbar1
            // 
            this.bpToolbar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.Btn_New,
            this.Btn_Edit1,
            this.Btn_Save1,
            this.Btn_Delete,
            this.Btn_DeleteAll,
            this.Btn_Refurbish,
            this.Btn_Execl,
            this.Btn_Xml,
            this.Btn_Income,
            this.Btn_Close});
            this.bpToolbar1.DropDownArrows = true;
            this.bpToolbar1.ImageList = this.imageList1;
            this.bpToolbar1.Location = new System.Drawing.Point(0, 0);
            this.bpToolbar1.Name = "bpToolbar1";
            this.bpToolbar1.ShowToolTips = true;
            this.bpToolbar1.Size = new System.Drawing.Size(687, 28);
            this.bpToolbar1.TabIndex = 0;
            this.bpToolbar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
            this.bpToolbar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.bpToolbar1_ButtonClick);
            // 
            // Btn_New
            // 
            this.Btn_New.ImageIndex = 0;
            this.Btn_New.Name = "Btn_New";
            this.Btn_New.Tag = "New";
            this.Btn_New.Text = "新建";
            // 
            // Btn_Edit1
            // 
            this.Btn_Edit1.ImageIndex = 1;
            this.Btn_Edit1.Name = "Btn_Edit1";
            this.Btn_Edit1.Tag = "Edit";
            this.Btn_Edit1.Text = "编辑";
            this.Btn_Edit1.ToolTipText = "选择一个实体打开编辑它。";
            // 
            // Btn_Save1
            // 
            this.Btn_Save1.ImageIndex = 2;
            this.Btn_Save1.Name = "Btn_Save1";
            this.Btn_Save1.Tag = "Save";
            this.Btn_Save1.Text = "保存";
            this.Btn_Save1.ToolTipText = "保存全部数据。";
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.ImageIndex = 3;
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Tag = "Delete";
            this.Btn_Delete.Text = "删除";
            this.Btn_Delete.ToolTipText = "删除当前选择的行。";
            // 
            // Btn_DeleteAll
            // 
            this.Btn_DeleteAll.ImageIndex = 8;
            this.Btn_DeleteAll.Name = "Btn_DeleteAll";
            this.Btn_DeleteAll.Text = "清除";
            this.Btn_DeleteAll.ToolTipText = "清除当前查询出来的纪录";
            // 
            // Btn_Refurbish
            // 
            this.Btn_Refurbish.ImageIndex = 4;
            this.Btn_Refurbish.Name = "Btn_Refurbish";
            this.Btn_Refurbish.Text = "刷新";
            this.Btn_Refurbish.ToolTipText = "重新读取数据";
            // 
            // Btn_Execl
            // 
            this.Btn_Execl.ImageIndex = 5;
            this.Btn_Execl.Name = "Btn_Execl";
            this.Btn_Execl.Tag = "Execl";
            this.Btn_Execl.Text = "导出";
            // 
            // Btn_Xml
            // 
            this.Btn_Xml.ImageIndex = 5;
            this.Btn_Xml.Name = "Btn_Xml";
            this.Btn_Xml.Tag = "XML";
            this.Btn_Xml.Text = "导出到Xml";
            this.Btn_Xml.Visible = false;
            // 
            // Btn_Income
            // 
            this.Btn_Income.ImageIndex = 6;
            this.Btn_Income.Name = "Btn_Income";
            this.Btn_Income.Text = "导入";
            this.Btn_Income.ToolTipText = "导入";
            // 
            // Btn_Close
            // 
            this.Btn_Close.ImageIndex = 7;
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Tag = "Close";
            this.Btn_Close.Text = "关闭";
            this.Btn_Close.ToolTipText = "关闭按钮。";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            // 
            // dg1
            // 
            this.dg1.AlternatingBackColor = System.Drawing.Color.Blue;
            this.dg1.DataMember = "";
            this.dg1.DGModel = BP.Win32.Controls.DGModel.None;
            this.dg1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dg1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
            this.dg1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dg1.IsDGReadonly = false;
            this.dg1.Location = new System.Drawing.Point(0, 71);
            this.dg1.Name = "dg1";
            this.dg1.ReadOnly = true;
            this.dg1.RowHeaderWidth = 0;
            this.dg1.Size = new System.Drawing.Size(687, 351);
            this.dg1.TabIndex = 1;
            this.dg1.Navigate += new System.Windows.Forms.NavigateEventHandler(this.dg1_Navigate);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 422);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sta1,
            this.sta3});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(687, 22);
            this.statusBar1.TabIndex = 2;
            this.statusBar1.Text = "statusBar1";
            // 
            // sta1
            // 
            this.sta1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.sta1.Name = "sta1";
            this.sta1.Text = "当前选择功能";
            this.sta1.Width = 607;
            // 
            // sta3
            // 
            this.sta3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sta3.Name = "sta3";
            this.sta3.Text = "当前时间";
            this.sta3.Width = 64;
            // 
            // gb1
            // 
            this.gb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb1.Location = new System.Drawing.Point(0, 28);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(687, 43);
            this.gb1.TabIndex = 3;
            this.gb1.TabStop = false;
            this.gb1.Text = "gb1";
            // 
            // UIEns
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(687, 444);
            this.Controls.Add(this.gb1);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.bpToolbar1);
            this.Name = "UIEns";
            this.Text = "UIEns";
            this.Resize += new System.EventHandler(this.UIEns_Resize);
            this.Load += new System.EventHandler(this.UIEns_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sta1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sta3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		#region 变量

		/// <summary>
		/// 当前的工作的Ens
		/// </summary>
		private UAC _SysEnsUAC=null;
		/// <summary>
		/// 当前的工作的Ens
		/// </summary>
		public UAC HisSysEnsUAC
		{
			get
			{
				if (_SysEnsUAC==null)
					throw new Exception("没有给hisEns变量赋值");
				return _SysEnsUAC;
			}
			set
			{
				_SysEnsUAC=value;
			}
		}
		/// <summary>
		/// 当前的工作的Ens
		/// </summary>
		private Entities _HisEns=null;
		/// <summary>
		/// 当前的工作的Ens
		/// </summary>
		public new Entities HisEns
		{
			get
			{
				if (_HisEns==null)
					throw new Exception("没有给hisEns变量赋值");
				return _HisEns;
			}
			set
			{
				_HisEns=value;
			}
		}
		#endregion

		#region Edit
		 
		 
		public void Btn_New1()
		{
			En ui =new En() ;
			ui.Bind2(this.dg1.HisEns.GetNewEntity );
			ui.ShowDialog();

//			UIEn ui =new UIEn() ;
//			ui.SetData(this.dg1.HisEns.GetNewEntity,false);
//			ui.ShowDialog();
		}
		#endregion

		#region 公用方法
		/// <summary>
		/// 获得控件DDL
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private BP.Win32.Controls.DDL GetDDLByKey(string key)
		{
			return (DDL)this.GetCtlByKey(key);
		}
        /// <summary>
        /// 获得控件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
		public Control GetCtlByKey(string key)
		{
			foreach(Control ctl in this.Controls)
			{
				if (ctl.Name==key)
				{
					return  ctl;
				}
			}
			throw new Exception("没有找到name="+key+"的控件.");
		}

		
		#endregion

		/// <summary>
		/// 设置数据窗体的滚动条
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UIEns_Resize(object sender, System.EventArgs e)
		{
			this.dg1.Width = this.Width-50;
			this.dg1.Height = this.Height-150;
		}
		#region 事件

        public void BtnRefurbish()
        {
            UAC en = this.HisEn.HisUAC;
            this.BindEns(this.HisEns, en, false);
        }

		private void bpToolbar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			string msg="";
            try
            {
                switch (e.Button.Text)
                {
                    case "刷新":
                        this.dg1.Bind();
                        break;
                    case "导入":
                        UILoadData ui = new UILoadData();
                        ui.HisEn = this.HisEn;
                        ui.HisEns = this.HisEns;
                        ui.ShowDialog();
                        break;
                    case "编辑":
                        this.dg1.Card();
                        break;
                    case "保存":
                        this.dg1.Save();
                        this.sta1.Text = "保存成功...";
                        break;
                    case "关闭":
                        this.Close();
                        break;
                    case "新建":
                        this.Btn_New1();
                        break;
                    case "删除":
                        this.dg1.DeleteSelected();
                        this.btn_SearchClick(null, null);
                        this.Activate();
                        break;
                    case "清除":
                        this.dg1.DeleteAll();
                        break;
                    case "导出到Excel":
                    case "导出":
                        this.saveFileDialog1.Title = "南京税通软件--请选择文件存放的位置.";
                        this.saveFileDialog1.Filter = "txt files (*.txt)|*.txt| 电子表格(*.xls)|*.xls";
                        this.saveFileDialog1.FileName = this.HisEn.EnDesc;
                        this.saveFileDialog1.DefaultExt = ".xls";
                        this.saveFileDialog1.ShowDialog();
                        string file = this.saveFileDialog1.FileName;
                        if (file.Trim().Length == 0)
                            return;
                        this.ExportDGToExcel(this.dg1, file);
                        break;
                    case "导出到Xml":
                        this.ExportToXml(this.dg1.HisEns);
                        break;
                    default:
                        throw new Exception("Error tag" + e.Button.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                this.ResponseWriteRedMsg(ex);
                //this.Alert(msg+ex.Message);
            }
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_SearchClick(object sender, EventArgs e)
		{
			QueryObject qo =new QueryObject(this.HisEns);
			TB tb = (TB)this.gb1.GetTBByKey("TB_Search");//获得TB（名称为TB_Search）控件
			string keyVal=tb.Text.Trim();
			Attrs attrs = this.HisEn.EnMap.Attrs;
		
			string pk=this.HisEn.PK ; 
			// TB式查询条件
            if (keyVal.Length > 0)
            {
                qo.addLeftBracket();
                qo.AddWhere(pk, " LIKE ", "%" + keyVal + "%");
                foreach (Attr en in attrs)
                {
                    if (en.MyFieldType == FieldType.RefText || en.MyFieldType == FieldType.Enum)
                        continue;
                    if (en.UIContralType == UIContralType.DDL || en.UIContralType == UIContralType.CheckBok)
                        continue;
                    if (en.Key == pk)
                        continue;

                    qo.addOr();
                    qo.AddWhere(en.Key, " LIKE ", "%" + keyVal + "%");
                }
                qo.addRightBracket();
            }
            else
            {
                qo.addLeftBracket();
                qo.AddWhere("abc", "all");
                qo.addRightBracket();
            }

			// 属性。
			Attrs searchAttrs = this.HisEn.EnMap.SearchAttrs;
            foreach (Attr attr in searchAttrs)
            {
                if (attr.MyFieldType == FieldType.RefText)
                    continue;
                qo.addAnd();
                qo.addLeftBracket();
                qo.AddWhere(attr.Key, this.gb1.GetDDLByKey("DDL_" + attr.Key).SelectedValue.ToString());
                qo.addRightBracket();
            }
			qo.DoQuery();

			this.dg1.BindEnsThisOnly(this.HisEns,false,false);

			/*
			//
			 DataTable dt= qo.DoQueryToTable();
			 dt.TableName = this.HisEn.EnDesc ;
			DataSet ds = new DataSet();
			ds.Tables.Add(dt);
			this.dg1.SetDataBinding(ds,dt.TableName) ; 
			if(dt.Rows.Count==0)
			{
				System.Windows.Forms.MessageBox.Show("没有符合当前条件的记录！","查询结果");
			}
			*/
			
			//this.dg1.ReSetDataSource(ens);
		}
		#endregion 事件

		private void UIEns_Load(object sender, System.EventArgs e)
		{
			
		}

        private void dg1_Navigate(object sender, NavigateEventArgs ne)
        {

        }
	}
}
