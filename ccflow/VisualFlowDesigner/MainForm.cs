using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using BP.En;
using BP.DA;
using BP.Port;
using BP.Win.WF;
using BP.WF;
using BP.Win.Controls;


namespace BP.WF.Design
{
	//public class MainForm : BP.Win32.PageBase
    public class MainForm : BP.Win32.PageBase
	{
		#region 控件
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ImageList imageList2;
		private BP.Win32.Controls.BPToolbar tBar1;
		private BP.Win.Controls.Pan pan1;
		private System.Windows.Forms.Splitter splitter1;
        public BP.Win.WF.WFToolBar wfToolBar1;
		public System.Windows.Forms.StatusBarPanel sBarPanelUser;
		public System.Windows.Forms.StatusBarPanel sBarPanelRight;
		public System.Windows.Forms.StatusBarPanel sBarPanelLoadTime;
		private BP.Win.Controls.Tree tree1;
		private System.Windows.Forms.ContextMenu NodeCMenu;
		private System.Windows.Forms.MenuItem M_Ref;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem M_Edit;
		private BP.Win.Controls.SBar sBar1;
        private System.Windows.Forms.ImageList imageList3;
        public System.Windows.Forms.StatusBarPanel sBarVersion;
		private System.Windows.Forms.ToolBarButton Btn_Exit;
		private System.Windows.Forms.ToolBarButton Btn_Open;
		private System.Windows.Forms.ToolBarButton Btn_Save;
        private System.Windows.Forms.ToolBarButton Btn_SaveAll;
        private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.Windows.Forms.ToolBarButton Btn_DTS;
        private MenuItem M_NewFlowSort;
        private MenuItem M_Del;
        private MenuItem M_NewFlow;
        private MenuItem M_NewFlowByTemplate;
        private MenuItem menuItem6;
        private ImageList imageList4;
        private ToolBarButton toolBarButton1;
        private ToolBarButton Btn_New;
        private ToolBarButton Btn_DeptEdit;
        private ToolBarButton Btn_StationEdit;
        private ToolBarButton Btn_EmpEdit;
        private ToolBarButton Btn_OperVideo;
        private MenuItem miReLoad;
        private MenuItem menuItem26;
        private MenuItem miExit;
        private MenuItem menuItem18;
        private MenuItem miCascade;
        private MenuItem miHorizontal;
        private MenuItem miVertical;
        private MenuItem menuItem9;
        private MenuItem miChangeBack;
        private MenuItem miDeleteBack;
        private MenuItem menuItem11;
        private MenuItem miCloseCurrent;
        private MenuItem miCloseAll;
        private MenuItem menuItem4;
        private MenuItem Btn_OpeVideo;
        private MenuItem Btn_CCS;
        private MenuItem menuItem16;
        private MenuItem menuItem1;
        private MenuItem Btn_About;
        private MenuItem menuItem14;
        private SaveFileDialog saveFileDialog1;
        private ToolBarButton toolBarButtonSpt;
        private ToolBarButton toolBarButton_Save;
        private ToolBarButton toolBarButton_Template;
        private ToolBarButton toolBarButton3;
        private OpenFileDialog openFileDialog1;
		private System.ComponentModel.IContainer components;
		#endregion 控件

		#region 初始化
		public MainForm()
		{
        
			InitializeComponent();
			bgPath = Application.StartupPath+"\\back.bin";

            BP.SystemConfig.IsBSsystem_Test = false;
            BP.SystemConfig.IsBSsystem = false;
            SystemConfig.IsBSsystem = false;
		}
		public  readonly string bgPath ;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (System.IO.File.Exists(bgPath))
                try
                {
                    System.Drawing.Image img = Bitmap.FromFile(bgPath);
                    this.BackgroundImage = img;
                }
                catch
                {
                    MessageBox.Show("文件格式错误，该文件不是正确的图像文件", "提示");
                }

            //this.fileSystemWatcher1.Path = Path.GetDirectoryName(Global.AppConfigPath);
            //this.fileSystemWatcher1.Filter = Path.GetFileName(Global.AppConfigPath);

            //this.fileSystemWatcher2.Path = Path.GetDirectoryName(Global.RefConfigPath);
            //this.fileSystemWatcher2.Filter = Path.GetFileName(Global.RefConfigPath);

            this.Cursor = Cursors.WaitCursor;
            this.sBarPanelLoadTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd,HH:mm");
            this.sBarVersion.Text =  " ver " + Global.VersionDate;

            FlowSorts fss = new FlowSorts();
            fss.RetrieveAll();

            TreeNode top = new TreeNode(this.ToE("WorkFlow", "工作流"), 0, 2);
            this.tree1.Nodes.Add(top);
            try
            {
                this.BindTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }
        public void BindTree()
        {
            this.tree1.Nodes.Clear();

            TreeNode top = new TreeNode(this.ToE("FlowSort","流程类别"), 0, 2);
            this.tree1.Nodes.Add(top);


            FlowSorts sorts = new FlowSorts();
            try
            {
                sorts.RetrieveAllFromDBSource();
            }
            catch
            {
                sorts.RetrieveAllFromDBSource();
            }

            sorts.FlodInCash();


            foreach (FlowSort sort in sorts)
            {
                TreeNode tn = new TreeNode();
                tn.Text = sort.Name;
                tn.Tag = sort.No;
                if (sort.Name == "公文类" )
                    continue;

                top.Nodes.Add(tn);

                Flows fls = new Flows(sort.No);
                foreach (Flow fl in fls)
                {
                    TreeNode flowTn = new TreeNode();
                    flowTn.Tag = fl;
                    flowTn.Text = fl.Name;
                    //   flowTn.StateImageIndex = 2;
                    // flowTn.SelectedImageIndex = 3;
                    tn.Nodes.Add(flowTn);
                }
            }

           //this.tree1.AddNodes(tree, top, "No", "Name", 1, 2, true);

            foreach (TreeNode tn in this.tree1.Nodes)
            {
                tn.ExpandAll();
            }

            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
        }
		#endregion 初始化
		
		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            if (disposing)
            {
                if (components != null)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.tBar1 = new BP.Win32.Controls.BPToolbar();
            this.Btn_New = new System.Windows.Forms.ToolBarButton();
            this.Btn_Save = new System.Windows.Forms.ToolBarButton();
            this.Btn_Open = new System.Windows.Forms.ToolBarButton();
            this.Btn_SaveAll = new System.Windows.Forms.ToolBarButton();
            this.Btn_DeptEdit = new System.Windows.Forms.ToolBarButton();
            this.Btn_StationEdit = new System.Windows.Forms.ToolBarButton();
            this.Btn_EmpEdit = new System.Windows.Forms.ToolBarButton();
            this.Btn_OperVideo = new System.Windows.Forms.ToolBarButton();
            this.Btn_Exit = new System.Windows.Forms.ToolBarButton();
            this.pan1 = new BP.Win.Controls.Pan();
            this.tree1 = new BP.Win.Controls.Tree();
            this.NodeCMenu = new System.Windows.Forms.ContextMenu();
            this.M_NewFlowSort = new System.Windows.Forms.MenuItem();
            this.M_NewFlow = new System.Windows.Forms.MenuItem();
            this.M_NewFlowByTemplate = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.M_Edit = new System.Windows.Forms.MenuItem();
            this.M_Del = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.M_Ref = new System.Windows.Forms.MenuItem();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.sBar1 = new BP.Win.Controls.SBar();
            this.sBarPanelUser = new System.Windows.Forms.StatusBarPanel();
            this.sBarPanelRight = new System.Windows.Forms.StatusBarPanel();
            this.sBarPanelLoadTime = new System.Windows.Forms.StatusBarPanel();
            this.sBarVersion = new System.Windows.Forms.StatusBarPanel();
            this.imageList4 = new System.Windows.Forms.ImageList(this.components);
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.miReLoad = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.miCascade = new System.Windows.Forms.MenuItem();
            this.miHorizontal = new System.Windows.Forms.MenuItem();
            this.miVertical = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.miChangeBack = new System.Windows.Forms.MenuItem();
            this.miDeleteBack = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.miCloseCurrent = new System.Windows.Forms.MenuItem();
            this.miCloseAll = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.Btn_OpeVideo = new System.Windows.Forms.MenuItem();
            this.Btn_CCS = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.Btn_About = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolBarButtonSpt = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton_Template = new System.Windows.Forms.ToolBarButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.wfToolBar1 = new BP.Win.WF.WFToolBar();
            this.pan1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelLoadTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarVersion)).BeginInit();
            this.SuspendLayout();
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
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            this.imageList1.Images.SetKeyName(12, "asf.gif");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            this.imageList2.Images.SetKeyName(3, "");
            // 
            // tBar1
            // 
            this.tBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tBar1.BackColor = System.Drawing.SystemColors.Control;
            this.tBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.Btn_New,
            this.Btn_Save,
            this.Btn_Open,
            this.Btn_SaveAll,
            this.Btn_DeptEdit,
            this.Btn_StationEdit,
            this.Btn_EmpEdit,
            this.Btn_OperVideo,
            this.Btn_Exit});
            this.tBar1.DropDownArrows = true;
            this.tBar1.ImageList = this.imageList1;
            this.tBar1.Location = new System.Drawing.Point(0, 0);
            this.tBar1.Name = "tBar1";
            this.tBar1.ShowToolTips = true;
            this.tBar1.Size = new System.Drawing.Size(962, 30);
            this.tBar1.TabIndex = 1;
            this.tBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
            this.tBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tBar1_ButtonClick);
            // 
            // Btn_New
            // 
            this.Btn_New.ImageIndex = 0;
            this.Btn_New.Name = "Btn_New";
            this.Btn_New.Text = "新建流程";
            // 
            // Btn_Save
            // 
            this.Btn_Save.ImageIndex = 2;
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Text = "保存";
            // 
            // Btn_Open
            // 
            this.Btn_Open.ImageIndex = 1;
            this.Btn_Open.Name = "Btn_Open";
            this.Btn_Open.Text = "打开";
            this.Btn_Open.Visible = false;
            // 
            // Btn_SaveAll
            // 
            this.Btn_SaveAll.ImageIndex = 3;
            this.Btn_SaveAll.Name = "Btn_SaveAll";
            this.Btn_SaveAll.Text = "全部保存";
            this.Btn_SaveAll.Visible = false;
            // 
            // Btn_DeptEdit
            // 
            this.Btn_DeptEdit.ImageIndex = 11;
            this.Btn_DeptEdit.Name = "Btn_DeptEdit";
            this.Btn_DeptEdit.Text = "部门维护";
            // 
            // Btn_StationEdit
            // 
            this.Btn_StationEdit.ImageIndex = 6;
            this.Btn_StationEdit.Name = "Btn_StationEdit";
            this.Btn_StationEdit.Text = "岗位维护";
            // 
            // Btn_EmpEdit
            // 
            this.Btn_EmpEdit.ImageIndex = 10;
            this.Btn_EmpEdit.Name = "Btn_EmpEdit";
            this.Btn_EmpEdit.Text = "工作人员维护";
            // 
            // Btn_OperVideo
            // 
            this.Btn_OperVideo.ImageIndex = 12;
            this.Btn_OperVideo.Name = "Btn_OperVideo";
            this.Btn_OperVideo.Text = "录像帮助";
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.ImageIndex = 8;
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Text = "退出";
            // 
            // pan1
            // 
            this.pan1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pan1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pan1.Controls.Add(this.tree1);
            this.pan1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pan1.Location = new System.Drawing.Point(0, 30);
            this.pan1.Name = "pan1";
            this.pan1.Size = new System.Drawing.Size(200, 397);
            this.pan1.TabIndex = 2;
            // 
            // tree1
            // 
            this.tree1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tree1.ContextMenu = this.NodeCMenu;
            this.tree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree1.HideSelection = false;
            this.tree1.HotTracking = true;
            this.tree1.ImageIndex = 0;
            this.tree1.ImageList = this.imageList3;
            this.tree1.Location = new System.Drawing.Point(0, 0);
            this.tree1.Name = "tree1";
            this.tree1.ReadOnly = false;
            this.tree1.SelectedImageIndex = 2;
            this.tree1.Size = new System.Drawing.Size(196, 393);
            this.tree1.TabIndex = 0;
            this.tree1.FirstExpand += new BP.Win.Controls.OnFirstExpandEventHandler(this.tree1_FirstExpand);
            this.tree1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree1_AfterSelect);
            this.tree1.DoubleClick += new System.EventHandler(this.tree1_DoubleClick);
            // 
            // NodeCMenu
            // 
            this.NodeCMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.M_NewFlowSort,
            this.M_NewFlow,
            this.M_NewFlowByTemplate,
            this.menuItem6,
            this.M_Edit,
            this.M_Del,
            this.menuItem3,
            this.M_Ref});
            // 
            // M_NewFlowSort
            // 
            this.M_NewFlowSort.Index = 0;
            this.M_NewFlowSort.Text = "新建流程类别";
            this.M_NewFlowSort.Click += new System.EventHandler(this.DoAddFlowSort_Click);
            this.M_NewFlowSort.Popup += new System.EventHandler(this.M_NewFlowSort_Popup);
            // 
            // M_NewFlow
            // 
            this.M_NewFlow.Index = 1;
            this.M_NewFlow.Text = "新建流程";
            this.M_NewFlow.Click += new System.EventHandler(this.DoAddFlow_Click);
            // 
            // M_NewFlowByTemplate
            // 
            this.M_NewFlowByTemplate.Index = 2;
            this.M_NewFlowByTemplate.Text = "导入流程模板(从本机)";
            this.M_NewFlowByTemplate.Click += new System.EventHandler(this.DoAddFlowByTemplate_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 3;
            this.menuItem6.Text = "-";
            // 
            // M_Edit
            // 
            this.M_Edit.DefaultItem = true;
            this.M_Edit.Index = 4;
            this.M_Edit.Text = "编辑";
            this.M_Edit.Click += new System.EventHandler(this.miTreeNodeAttr_Click);
            // 
            // M_Del
            // 
            this.M_Del.Index = 5;
            this.M_Del.Text = "删除";
            this.M_Del.Click += new System.EventHandler(this.DoDeleteFlow_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 6;
            this.menuItem3.Text = "-";
            // 
            // M_Ref
            // 
            this.M_Ref.Index = 7;
            this.M_Ref.Text = "刷新";
            this.M_Ref.Click += new System.EventHandler(this.DoRefreshNodes_Click);
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "");
            this.imageList3.Images.SetKeyName(1, "");
            this.imageList3.Images.SetKeyName(2, "");
            this.imageList3.Images.SetKeyName(3, "WF.ICO");
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 30);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 397);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // menuItem20
            // 
            this.menuItem20.Index = -1;
            this.menuItem20.Text = "部门";
            this.menuItem20.Click += new System.EventHandler(this.menuItem_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = -1;
            this.menuItem22.Text = "";
            // 
            // sBar1
            // 
            this.sBar1.Location = new System.Drawing.Point(0, 427);
            this.sBar1.Name = "sBar1";
            this.sBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sBarPanelUser,
            this.sBarPanelRight,
            this.sBarPanelLoadTime,
            this.sBarVersion});
            this.sBar1.ShowPanels = true;
            this.sBar1.Size = new System.Drawing.Size(962, 21);
            this.sBar1.TabIndex = 9;
            this.sBar1.Text = "sBar1";
            // 
            // sBarPanelUser
            // 
            this.sBarPanelUser.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.sBarPanelUser.Name = "sBarPanelUser";
            this.sBarPanelUser.Text = "用户";
            this.sBarPanelUser.Width = 860;
            // 
            // sBarPanelRight
            // 
            this.sBarPanelRight.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sBarPanelRight.Name = "sBarPanelRight";
            this.sBarPanelRight.Width = 10;
            // 
            // sBarPanelLoadTime
            // 
            this.sBarPanelLoadTime.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sBarPanelLoadTime.Name = "sBarPanelLoadTime";
            this.sBarPanelLoadTime.Width = 10;
            // 
            // sBarVersion
            // 
            this.sBarVersion.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sBarVersion.Name = "sBarVersion";
            this.sBarVersion.Text = "Ver 4.0 ";
            this.sBarVersion.Width = 58;
            // 
            // imageList4
            // 
            this.imageList4.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList4.ImageStream")));
            this.imageList4.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList4.Images.SetKeyName(0, "Delete.gif");
            this.imageList4.Images.SetKeyName(1, "Edit.gif");
            this.imageList4.Images.SetKeyName(2, "New.gif");
            this.imageList4.Images.SetKeyName(3, "New.gif");
            this.imageList4.Images.SetKeyName(4, "Refurbish.gif");
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.ImageIndex = 4;
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // miReLoad
            // 
            this.miReLoad.Index = 0;
            this.miReLoad.Text = "";
            // 
            // menuItem26
            // 
            this.menuItem26.Index = 1;
            this.menuItem26.Text = "";
            // 
            // miExit
            // 
            this.miExit.Index = 2;
            this.miExit.Text = "";
            // 
            // menuItem18
            // 
            this.menuItem18.Index = -1;
            this.menuItem18.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miReLoad,
            this.menuItem26,
            this.miExit});
            this.menuItem18.Text = "System";
            // 
            // miCascade
            // 
            this.miCascade.Index = 0;
            this.miCascade.Text = "WindowsCascade";
            this.miCascade.Click += new System.EventHandler(this.miCascade_Click);
            // 
            // miHorizontal
            // 
            this.miHorizontal.Index = 1;
            this.miHorizontal.Text = "HXPP";
            this.miHorizontal.Click += new System.EventHandler(this.miHorizontal_Click);
            // 
            // miVertical
            // 
            this.miVertical.Index = 2;
            this.miVertical.Text = "ZXPP";
            this.miVertical.Click += new System.EventHandler(this.miVertical_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 3;
            this.menuItem9.Text = "-";
            // 
            // miChangeBack
            // 
            this.miChangeBack.Index = 4;
            this.miChangeBack.Text = "";
            // 
            // miDeleteBack
            // 
            this.miDeleteBack.Index = 5;
            this.miDeleteBack.Text = "";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 6;
            this.menuItem11.Text = "-";
            // 
            // miCloseCurrent
            // 
            this.miCloseCurrent.Index = 7;
            this.miCloseCurrent.Text = "CloseCurrWindows";
            this.miCloseCurrent.Click += new System.EventHandler(this.miCloseCurrent_Click);
            // 
            // miCloseAll
            // 
            this.miCloseAll.Index = 8;
            this.miCloseAll.Text = "CloseAllWindows";
            this.miCloseAll.Click += new System.EventHandler(this.miCloseAll_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = -1;
            this.menuItem4.MdiList = true;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miCascade,
            this.miHorizontal,
            this.miVertical,
            this.menuItem9,
            this.miChangeBack,
            this.miDeleteBack,
            this.menuItem11,
            this.miCloseCurrent,
            this.miCloseAll});
            this.menuItem4.Text = "Windows";
            // 
            // Btn_OpeVideo
            // 
            this.Btn_OpeVideo.Index = 0;
            this.Btn_OpeVideo.Text = "OperVideo";
            this.Btn_OpeVideo.Click += new System.EventHandler(this.Menu_Do_Click);
            // 
            // Btn_CCS
            // 
            this.Btn_CCS.Index = -1;
            this.Btn_CCS.Text = "";
            // 
            // menuItem16
            // 
            this.menuItem16.Index = -1;
            this.menuItem16.Text = "";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = -1;
            this.menuItem1.Text = "";
            // 
            // Btn_About
            // 
            this.Btn_About.Index = 1;
            this.Btn_About.Text = "About";
            this.Btn_About.Click += new System.EventHandler(this.Menu_Do_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = -1;
            this.menuItem14.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Btn_OpeVideo,
            this.Btn_About});
            this.menuItem14.Text = "Help";
            // 
            // toolBarButtonSpt
            // 
            this.toolBarButtonSpt.Name = "toolBarButtonSpt";
            this.toolBarButtonSpt.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.toolBarButton3.Text = "保存";
            // 
            // toolBarButton_Template
            // 
            this.toolBarButton_Template.Name = "toolBarButton_Template";
            this.toolBarButton_Template.Text = "载入模板";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // wfToolBar1
            // 
            this.wfToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.wfToolBar1.AutoSize = false;
            this.wfToolBar1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.wfToolBar1.ButtonsCount = 4;
            this.wfToolBar1.ButtonSize = new System.Drawing.Size(22, 22);
            this.wfToolBar1.Divider = false;
            this.wfToolBar1.DropDownArrows = true;
            this.wfToolBar1.ImageList = this.imageList2;
            this.wfToolBar1.Location = new System.Drawing.Point(203, 30);
            this.wfToolBar1.Name = "wfToolBar1";
            this.wfToolBar1.ShowToolTips = true;
            this.wfToolBar1.Size = new System.Drawing.Size(759, 39);
            this.wfToolBar1.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(962, 448);
            this.Controls.Add(this.wfToolBar1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pan1);
            this.Controls.Add(this.tBar1);
            this.Controls.Add(this.sBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pan1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarPanelLoadTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sBarVersion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region 事件处理
		private void miExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
        private void tree1_FirstExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            try
            {
                if (e.Node.Tag != null)
                {
                    e.Node.Nodes.Clear();
                    string no = e.Node.Tag.ToString();
                    Flows fs = new Flows(no);
                    this.tree1.AddNodes(fs, e.Node, "", "Name", 1, 2, false);
                }
                else //根节点
                {
                    this.BindTree();
                }
            }
            catch { }
        }

        private void tree1_DoubleClick(object sender, System.EventArgs e)
        {
            BP.Win.WF.Global.ToolIndex = 0;
            this.OpenFlow();
        }

		private void SaveAll()
		{
			foreach(Form f in this.MdiChildren)
			{
				FrmDesigner ch = f as FrmDesigner;
				if(f!=null)
				{
					try
					{
						ch.Save();
					}
					catch(Exception ex)
					{
						MessageBox.Show(ex.Message );
						ch.Activate();
					}
				}
			}
		}
		private bool SaveAndCloseAll()
		{
			bool all = true;
            foreach (Form f in this.MdiChildren)
            {
                FrmDesigner ch = f as FrmDesigner;
                if (f != null)
                {
                    try
                    {
                        ch.Save();
                        ch.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(f.Text + "：\n" + ex.Message, "保存时发生错误！");
                        ch.Activate();
                        all = false;
                        break;
                    }
                }
            }
			return all;
		}
		public void Do(string text)
		{
            switch (text)
            {
                case "新建流程":
                case "新建":
                case "New":
                case "NewFlow":
                    DoAddFlow_Click(null, null);
                     
                    break;
                case "操作员":
                case "人员维护":
                case "用户维护":
                case "用户":
                case "Emp":
                case "EmpEdit":
                    BP.WF.Global.DoEdit(new BP.Port.Emps());
                    break;
                case "岗位维护":
                case "StationEdit":
                    BP.WF.Global.DoEdit(new BP.WF.Port.Stations());
                    break;
                case "部门":
                case "部门维护":
                case "DeptEdit":
                    BP.WF.Global.DoEdit(new BP.WF.Port.Depts());
                    return;
                case "公用组件":
                    BP.Admin.MainFrom mf = new BP.Admin.MainFrom();
                    mf.MdiParent = this;
                    mf.Show();
                    break;
                case "Open": // op
                    break;
                case "Open1": // save
                    if (this.tree1.SelectedNode != null)
                        this.tree1.SelectedNode.Expand();
                    this.OpenFlow();
                    break;
                case "保存": //saveall
                case "保存流程": //saveall
                case "Save":
                case "SaveFlow":
                    FrmDesigner design = this.ActiveMdiChild as FrmDesigner;
                    if (design != null)
                        design.Save();
                    break;
                case "全部保存"://save alll
                case "SaveAll":
                    this.SaveAll();
                    break;
                case "工作流属性": // work para
                case "FlowProperty":
                    FrmDesigner p = this.ActiveMdiChild as FrmDesigner;
                    if (p != null)
                    {
                        p.HisWinFlow.ShowFlowAttr();
                    }
                    break;
                case "退出":
                case "Exit":
                    if (this.Question(this.ToE("ExitQuestion","您确定要退出吗？")))
                        this.Close();
                    break;
                case "About":
                case "关于":
                case "访问驰骋软件":
                case "CCS":
                    BP.WF.Global.DoIE(SystemConfig.GetValByKey("AboutUrl", "http://ccflow.cn"));
                    break;
                case "操作录像":
                case "OperVideo":
                case "Oper Video":
                    BP.WF.Global.DoIE(SystemConfig.GetValByKey("AboutUrl", "http://ccflow.cn/ftp/flow/demo/"));
                    break;
                default:
                    MessageBox.Show(text);
                    break;
            }
		}
        private void tBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string id = e.Button.Name.Replace("Btn_", "");
            //this.Do(e.Button.Text);
            this.Do(id);
            this.Cursor = Cursors.Default;
            Application.DoEvents();
        }
        private void RefreshsBa_de()
        {
            this.sBarPanelUser.Text = Global.User;
            this.sBarPanelRight.Text = Global.Right;
            if (msgshow < 1)
                this.sBarVersion.Text = "";
            else
                msgshow--;
        }
		private int  msgshow=5;
		private bool lock_check = false;
        /// <summary>
        /// 刷新节点。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoRefreshNodes_Click(object sender, System.EventArgs e)
        {
            TreeViewCancelEventArgs te = new TreeViewCancelEventArgs(this.tree1.SelectedNode, false, TreeViewAction.ByMouse);
            this.tree1_FirstExpand(this.tree1, te);
        }
		private void miTreeNodeAttr_Click(object sender, System.EventArgs e)
		{
			if(this.tree1.SelectedNodeLevel ==-1)
				return;

			if( this.tree1.SelectedNode.Tag != null)
				this.ShowAttr();
		}
        public void OpenFlow()
        {
            if (this.tree1.SelectedNodeLevel != 2)
                return;

            Flow flow = this.tree1.SelectedNode.Tag as Flow;
            OpenFlow(flow);

        }
        public void OpenFlow(Flow flow)
        {
            if (flow.IsExits == false)
            {
                this.BindTree();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            if (this.FindChildForm(flow.No) == null)
            {
                Nodes nds = new Nodes();
                nds.Retrieve(NodeAttr.FK_Flow, flow.No);
                flow.HisNodes = nds;

                FrmDesigner design = new FrmDesigner();
                design.Name = flow.No;
                design.Text = flow.Name;
                if (flow.HisNodes.Count == 0)
                {
                    if (this.Question("系统没有找到该流程的节点的数据，您是否要注册它吗？"))
                        this.RegFlow(flow);
                }
                design.BindData(flow);
                ShowChild(design);
            }
            this.Cursor = Cursors.Default;
        }
		public void ShowAttr()
		{
			if( this.tree1.SelectedNodeLevel ==2)
			{
                //MessageBox.Show("请在流程");

                //Flow fl = this.tree1.SelectedNode.Tag as Flow;
                //BP.WF.Global.DoEdit(fl);
                //fl.RetrieveFromDBSources();

                //this.HisWinFlow.HisFlow = fl;
                //if (this.Text != fl.Name)
                //    this.Text = fl.Name;

                //try
                //{
                //    Control[] ctl = this.MdiParent.Controls.Find("tree1", true);
                //    BP.Win.Controls.Tree t = (BP.Win.Controls.Tree)ctl[0];
                //    t.SelectedNode.Text = fl.Name;
                //    t.Tag = fl;
                //}
                //catch
                //{
                //}
                 
                //FrmAttr fotm = new FrmAttr();
                //if(fotm.ShowAttr( "工作流属性 ["+fs.Name +"]",  fs ,false )==DialogResult.OK)
                //    this.tree1.SelectedNode.Text = fs.Name;
			}
			else if( this.tree1.SelectedNodeLevel ==1)
			{
				FlowSort fs = new FlowSort( this.tree1.SelectedNode.Tag.ToString() );
				FrmAttr fotm = new FrmAttr();
				if(fotm.ShowAttr( "工作流类别属性 ["+fs.Name +"]",  fs ,false )==DialogResult.OK)
					this.tree1.SelectedNode.Text = fs.Name;
			}
		}
		#endregion 事件处理
	
		#region ChildForm
		public Form FindChildForm(string name)
		{
			foreach(Form f in this.MdiChildren)
			{
				if(f.Name == name)
				{
					f.Activate();
					return f;
				}
			}
			return null;
		}
		/// <summary>
		/// 显示一个流程设计框
		/// </summary>
		/// <param name="child">要显示的from</param>
        public void ShowChild(Form child)
        {
            child.MdiParent = this; // 指定父窗体是本窗体
            child.StartPosition = FormStartPosition.CenterParent; // 指定位置。
            child.Show();
        }

		private void miCascade_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.Cascade);
		}
		
		private void miHorizontal_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void miVertical_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileVertical);
		}
	  
        private void miCloseCurrent_Click(object sender, System.EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                this.ActiveMdiChild.Close();
        }
		private void miCloseAll_Click(object sender, System.EventArgs e)
		{
			this.SaveAndCloseAll();
		}
		 
		#endregion 

		#region 系统维护
        private void miReLoad_Click(object sender, System.EventArgs e)
        {
            bool flag = true;
            if (this.MdiChildren.Length > 0)
            {
                flag = MSG.ShowQuestion("将关闭所有窗口！是否继续(Y/N)？", "重新登陆") == DialogResult.Yes;
            }
            if (flag)
            {
                this.miCloseAll_Click(this, e);
            }
        }
		#endregion 系统维护

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            this.Text = SystemConfig.SysName;
        }

		private void menuItem_Click(object sender, System.EventArgs e)
		{
			MenuItem item = (MenuItem)sender;
			this.Do(item.Text);
		}
        private void DoAddFlowSort_Click(object sender, EventArgs e)
        {
            FlowSort fs = new FlowSort();
            fs.No = fs.GenerNewNo;
            fs.Name = this.ToE("FlowSort", "流程类别") + fs.No;
            fs.Insert();

            FrmAttr fotm = new FrmAttr();
            if (fotm.ShowAttr("编辑", fs, false) == DialogResult.OK)
            {
            }
            this.BindTree();
        }

        private void DoAddFlowByTemplate_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Multiselect = true;
            DialogResult dg = this.openFileDialog1.ShowDialog();
            if (dg != DialogResult.OK)
                return;

            string[] paths = this.openFileDialog1.FileNames;
            if (paths == null)
                return;

            string msg = "";
            Flow myflow = new Flow();
            foreach (string path in paths)
            {
                try
                {
                    myflow = new Flow();
                    if (this.tree1.SelectedNodeLevel == 2)
                    {
                        Flow fs = this.tree1.SelectedNode.Tag as Flow;
                        myflow.FK_FlowSort = fs.FK_FlowSort;
                    }
                    else if (this.tree1.SelectedNodeLevel == 1)
                    {
                        FlowSort fs = new FlowSort(this.tree1.SelectedNode.Tag.ToString());
                        myflow.FK_FlowSort = fs.No;
                    }

                    //  myflow.DoNewFlow();
                    myflow = Flow.DoLoadFlowTemplate(myflow.FK_FlowSort, path);
                    try
                    {
                        this.tree1.SelectedNode.Text = myflow.Name;
                    }
                    catch
                    {
                    }
                }
                catch (Exception ex)
                {
                    msg += "@" + ex.Message;
                }
            }
            if (msg != "")
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.BindTree();
            this.OpenFlow(myflow);
        }
        private void DoAddFlow_Click(object sender, EventArgs e)
        {
            if (this.tree1.SelectedNodeLevel <= 0)
            {
                MessageBox.Show("请选择流程类别或新建一个流程类别！");
                return;
            }

            Flow myflow = new Flow();
            if (this.tree1.SelectedNodeLevel == 2)
            {
                Flow fs = this.tree1.SelectedNode.Tag as Flow;
                myflow.FK_FlowSort = fs.FK_FlowSort;
            }
            else if (this.tree1.SelectedNodeLevel == 1)
            {
                FlowSort fs = new FlowSort(this.tree1.SelectedNode.Tag.ToString());
                myflow.FK_FlowSort = fs.No;
            }

            myflow.DoNewFlow();
            BP.WF.Global.DoEdit(myflow);
            myflow.RetrieveFromDBSources();
            this.tree1.SelectedNode.Tag = myflow;
            this.BindTree();
            try
            {
                this.tree1.SelectedNode.Text = myflow.Name;
            }
            catch
            {
            }
            this.OpenFlow(myflow);
        }
        /// <summary>
        /// 注册流程
        /// </summary>
        /// <param name="fs"></param>
        public void RegFlow(Flow fs)
        {
            this.Cursor = Cursors.WaitCursor;
            string msg = Node.RegFlow(fs);
            this.Cursor = Cursors.Default;
            MessageBox.Show(msg, "流程注册信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefFlow()
        {
            Flow fs = this.tree1.SelectedNode.Tag as Flow;

            if (Web.WebUser.No != "admin")
                throw new Exception("您没有此权限！！！");

            this.Cursor = Cursors.WaitCursor;
            string str = "InitGlobalWorks";
            this.Cursor = Cursors.Default;
        }
        private void DoDeleteFlow_Click(object sender, EventArgs e)
        {
            if (this.tree1.SelectedNode.Text == "公文类" || this.tree1.SelectedNode.Text == "办公类")
            {
                MessageBox.Show("此类别不能删除！");
                return;
            }
            if (this.tree1.SelectedNodeLevel == -1)
                return;

            if (this.tree1.SelectedNode.Tag == null)
                return;

            Flow flow = this.tree1.SelectedNode.Tag as Flow;
            if (flow != null)
            {
                if (BP.Win32.PubClass.Question(this.ToE("AYS", "您确定吗？")) == false)
                    return;

                if (BP.Win32.PubClass.Question(this.ToE("AYS", "您确定吗？")) == false)
                    return;
                flow.DoDelete();
            }

            string fk_sort = this.tree1.SelectedNode.Tag as string;
            if (fk_sort != null)
            {

                FlowSort sort = new FlowSort(fk_sort);
                if (BP.Win32.PubClass.Question(this.ToE("AYS", "您确定吗？")) == false)
                    return;

                Flows fls = new Flows(sort.No);
                if (fls.Count > 0)
                {
                    BP.Win32.PubClass.Alert(this.ToE("DelSortErr", "该类别下面有流程您不能删除它"));
                    return;
                }
                sort.Delete(); 
            }
            this.BindTree();
        }
        private void Menu_Do_Click(object sender, EventArgs e)
        {
            MenuItem it = sender as MenuItem;
            this.Do(it.Text);
        }

        private void M_NewFlowSort_Popup(object sender, EventArgs e)
        {
            //if (this.tree1.SelectedNodeLevel == 2)
            //    this.M_Edit.Visible = false;
            //else
            //    this.M_Edit.Visible = true;
        }

        private void tree1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tree1.SelectedNodeLevel == 2)
            {
                this.M_Edit.Visible = false;
                this.M_Del.Text = "删除-流程";
            }
            else
            {
                this.M_Del.Text = "删除-流程类别";
                this.M_Edit.Visible = true;
                this.M_Edit.Text = "编辑类别";
            }
            this.M_NewFlowByTemplate.Visible = false;
        }
	}
}
