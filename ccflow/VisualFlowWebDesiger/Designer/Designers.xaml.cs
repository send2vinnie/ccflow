using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using WF.DataServiceReference;
using Silverlight;
using WF.Controls;
using Liquid;
using WF.Resources;
using System.Windows.Browser;
using WF.Designer;
using System.IO;
namespace Ccflow.Web.UI.Control.Workflow.Designer
{
    /// <summary>
    /// 设计器主页面
    /// </summary>
    public partial class Designers : UserControl
    {
        #region 全局变量
          TreeNode firstNodeByFlow = new TreeNode();

        DataSet dsFlows = new DataSet();
        System.Windows.Threading.DispatcherTimer _doubleClickTimer;
        string flowSortID = "";
        string FlowTempleteUrl = "";
        int windowWidth;
        int windowHeight;
        /// <summary>
        /// 子窗口标题
        /// </summary>
        string title;
        #endregion

        #region 属性
        WebServiceSoapClient _service = new WebServiceSoapClient();//new BasicHttpBinding(), address);

   public WebServiceSoapClient _Service
        {
            get { return _service; }
            set { _service = value; }
        }

        public string FlowID
        {
            get;
            set;
        }

        private Container NewContainer
        {
            get
            {
                Container c = (Container)tbDesigner.SelectedContent;
                return c;
            }

        }
        public bool IsRefresh { get; set; }
        #endregion

     /// <summary>
     /// 构造方法
     /// </summary>
        public Designers()
        {
            InitializeComponent();
            this.deptTree.Designer = this;
            this.stationTree.Designer = this;

            _Service.GetFlowSortAsync();
            _Service.GetFlowSortCompleted += new EventHandler<GetFlowSortCompletedEventArgs>(_service_GetFlowSortCompleted);
            _Service.GetFlowsCompleted += new EventHandler<GetFlowsCompletedEventArgs>(_Service_GetFlowsCompleted);
        }
        #region 方法
        /// <summary>
        /// 语言设置
        /// </summary>
        void applyContainerCulture()
        {
            btnAddFlowNode.Content = Text.Button_AddFlowNode;
            //  btnCreatePicture.Content = Text.Button_CreatePicture;
            btnAddDirection.Content = Text.Button_AddDirection;
            btnAddLabel.Content = Text.Button_AddLabel;
            btnNewFlow.Content = Text.Button_NewFlow;
            btnDesignerTable.Content = Text.Button_DesignerTable;
            btnCheck.Content = Text.Button_FlowCheck;
            btnSave.Content = Text.Button_Save;
            btnRun.Content = Text.Button_RunFlow;
            btnEdit.Content = Text.Button_Edit;
            btnDel.Content = Text.Button_DelFlow;
            btnCreatTemplate.Content = Text.Button_CreatTemplate;
            btnAddTemplate.Content = Text.Button_AddTemplate;
            //tbShowGridLines.Text = Text.Menu_ShowGridLines;
         

        }
        /// <summary>
        /// 语言设置
        /// </summary>
        public void ApplyCulture()
        {

            applyContainerCulture();

            //siFlowNodeSetting.ApplyCulture();
            //siDirectionSetting.ApplyCulture();
        }
        /// <summary>
        /// 打开工作流
        /// </summary>
        /// <param name="flowid">工作流ID</param>
        /// <param name="title">工作流名称</param>
        private void OpenFlow(string flowid, string title)
        {
            foreach (TabItem t in tbDesigner.Items)
            {
                Container ct = t.Content as Container;
                if (ct.FlowID == flowid)
                {
                    tbDesigner.SelectedItem = t;
                    return;

                }
            }
            Container c = new Container();
            c.Designer = this;
            c.FlowID = flowid;
            c.getFlows();
            TabItemEx ti = new TabItemEx();

        //    ti.MouseEnter += new MouseEventHandler(ti_MouseEnter);
            ti.Content = c;
            Button btn = new Button();
            btn.Opacity = 0.2;
            btn.Content = "╳";
            btn.MinHeight = 15;
            btn.MinWidth = 17;
            btn.Click += new RoutedEventHandler(btn_Click);
            btn.ClickMode = ClickMode.Release;
            TextBlock tbx = new TextBlock();
            tbx.Text = title;
            tbx.Width = title.Length;
            Canvas cs = new Canvas();
            cs.Children.Add(btn);
            cs.Children.Add(tbx);
              cs.Height = 20;
            ti.Title = title;
            ti.Width = tbx.ActualWidth + btn.ActualWidth + 30;
            ti.Header = cs;
            btn.DataContext = ti;
            btn.SetValue(Canvas.LeftProperty, ti.Width - btn.ActualWidth - 20);
            btn.SetValue(Canvas.TopProperty, 0.0);
            tbx.SetValue(Canvas.TopProperty, btn.ActualHeight);
            tbx.SetValue(Canvas.LeftProperty, 0.0);
            ti.SetValue(TabControl.WidthProperty, tbx.ActualWidth + btn.ActualWidth + 40);

            ti.VerticalAlignment = VerticalAlignment.Top;
            ti.VerticalContentAlignment = VerticalAlignment.Top;

            tbDesigner.Items.Add(ti);
            tbDesigner.SelectedItem = ti;

        }

        //void ti_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    this.tbDesigner.SelectedItem = sender as TabItemEx;
        //}

       
        /// <summary>
        /// 删除工作流
        /// </summary>
        public void DeleteFlow(string flowid)
        { if (System.Windows.Browser.HtmlPage.Window.Confirm(Text.DeleteFlow))
                {

                   

                        _Service.DoAsync("DelFlow", flowid,true);


                        foreach (TabItem t in tbDesigner.Items)
                        {
                            Container ct = t.Content as Container;
                            if (ct.FlowID == NewContainer.FlowID)
                            {
                                tbDesigner.Items.Remove(t);
                                break;

                            }
                        }
            _Service.GetFlowSortAsync();
            }
            
        }
        /// <summary>
        /// 删除工作流类别
        /// </summary>
        /// <param name="flowsortid">工作流类别ID</param>
        public void DeleteFlowSort(string flowsortid)
        {
            if (System.Windows.Browser.HtmlPage.Window.Confirm(Text.DeleteFlowSort))
            {
                if (System.Windows.Browser.HtmlPage.Window.Confirm(Text.DeleteFlowSort))
                {
                    _Service.DoAsync("DelFlowSort", flowsortid,true);
                    _Service.GetFlowSortAsync();
                }
            }

        }

        /// <summary>
        /// 新建工作流
        /// </summary>
        public void NewFlow()
        {





            if (TvwFlow.Selected != null)
            {
                flowSortID = TvwFlow.Selected.ID;
                _Service.DoAsync("NewFlow", TvwFlow.Selected.ID,true);


            }
            else
            {
                _Service.DoAsync("NewFlow", null,true);
            } _Service.DoCompleted += new EventHandler<DoCompletedEventArgs>(_service_DoCompleted);
            _Service.GetFlowSortAsync();

        }

        /// <summary>
        /// 设置打开网页窗口的属性
        /// </summary>
        /// <param name="lang">语言</param>
        /// <param name="dotype">窗口类型</param>
        /// <param name="fk_flow">工作流ID</param>
        /// <param name="node1">结点1</param>
        /// <param name="node2">结点2</param>
        public void SetProper(string lang, string dotype, string fk_flow, string node1, string node2)
        {
            _Service.WinOpenAsync(lang, dotype, fk_flow, node1, node2,true);
            _Service.WinOpenCompleted += new EventHandler<WF.DataServiceReference.WinOpenCompletedEventArgs>(_Service_WinOpenCompleted);

        }
        public void WinOpen(string url, string title, int h,int w)
        {
            WinOpen(url, title);
        }
        /// <summary>
        /// 弹出网页窗口
        /// </summary>
        /// <param name="url">网页地址</param>
        public void WinOpen(string url,string title)
        {
            //WinOpens wo = new WinOpens(title);
            //if (IsRefresh)
            //{
            //    wo = new WinOpens(this, title);

            //}

            //wo.GetRichContent(url);

            //wo.Show();
            //htmlHost.Width = windowWidth;
            //htmlHost.Height = windowHeight;
            //htmlHost.TargetUrl = url;

            //MessageBody.Visibility = Visibility.Visible;
       
            //ShowContainerCover();
            HtmlPage.Window.Eval( "window.showModalDialog('" + url + "',window,'dialogHeight:600px;dialogWidth:800px;center:Yes;help:No;scroll:auto;resizable:No;status:No;');");
                      //  result = OpenModalDialog('../Hrm/PFrmZG.aspx?org_No=' + orgNo, obj, '500', '400');  //打开模态子窗体,并获取返回值

            if (IsRefresh)
            {
                this._Service.GetFlowSortAsync();
                if (this.tbDesigner.SelectedItem != null)
                {
                 //   Container c = (Container)this.tbDesigner.SelectedContent;
                  
                    
                    //this.tbDesigner.Items.Remove(te);
                    NewContainer.cleareContainer();
                    NewContainer.getFlows();
                   // te.DataContext = c;
                }
               
            }
           

          //  HtmlPage.Window.Eval("window.open ('" + url + "', 'newwindow', 'height=600, width=800, top=200, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=yes,location=no, status=no')");
        }
        

        #endregion
        #region 事件
     
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DoubleClick_Timer(object sender, EventArgs e)
        {
            _doubleClickTimer.Stop();
        }

        /// <summary>
        /// 获取工作流类型事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _service_GetFlowSortCompleted(object sender, GetFlowSortCompletedEventArgs e)
        {
  //TvwFlow.Clear();
  //          firstNodeByFlow = new TreeNode();
  //          TreeNode RootNode = new TreeNode();
  //          RootNode.ID = "Root";
  //          RootNode.Title = "流程类型";
  //          RootNode.Name = "流程类型";
  //          RootNode.isRoot = true;
  //          TvwFlow.Nodes.Add(RootNode);


          
           


  //          DataSet ds = new DataSet();
  //          ds.FromXml(e.Result);
  //          foreach (DataRow dr in ds.Tables[0].Rows)
  //          {

  //              TreeNode node = new TreeNode();
  //              node.Title = dr["Name"].ToString();
  //              node.ID = dr["No"].ToString();
  //              if (node.Title == "公文类" || node.Title == "办公类")
  //                  continue;
  //              node.IsFlowSort = true;
  //              firstNodeByFlow.Nodes.Add(node);

  //              RootNode.Nodes.Add(node);
  //          }

  //          RootNode.Expand();
  //          _Service.GetFlowsAsync();


            firstNodeByFlow = new TreeNode();
            TvwFlow.Clear();



            DataSet ds = new DataSet();
            ds.FromXml(e.Result);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                TreeNode node = new TreeNode();
                node.Title = dr["Name"].ToString();
                node.ID = dr["No"].ToString();
                node.IsFlowSort = true;
                firstNodeByFlow.Nodes.Add(node);

                TvwFlow.Nodes.Add(node);
            }


            _Service.GetFlowsAsync();
        }
        /// <summary>
        /// 获取工作流事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _Service_GetFlowsCompleted(object sender, GetFlowsCompletedEventArgs e)
        {
            dsFlows = new DataSet();

            dsFlows.FromXml(e.Result);
            foreach (DataRow d in dsFlows.Tables[0].Rows)
            {

                TreeNode node = new TreeNode();
             
                node.Title = d["Name"].ToString();

                node.ID = d["FK_FlowSort"].ToString();
                node.Name = d["No"].ToString();
                node.IsFlowSort = false;
                if (NewContainer != null)
                {
                    if (NewContainer.FlowID == node.Name) 
                    {
                        

                        TabItemEx te = this.tbDesigner.SelectedItem as TabItemEx;
                        te.Title = node.Title;
                        Canvas cs = te.Header as Canvas;
                        TextBlock tbx = cs.Children[1] as TextBlock;
                        tbx.Text = node.Title;
                    }

                }
                foreach (TreeNode ne in firstNodeByFlow.Nodes)
                {
                    try
                    {

                        if (ne.ID == d["FK_FlowSort"].ToString())
                        {
                            ne.Nodes.Add(node);
                            ne.IsFlowSort = true;

                        }

                    }
                    catch { }

                }


            }
            try
            {
                TvwFlow.Get(flowSortID).Expand();
            }
            catch { }
            try {
                TvwFlow.Get(FlowID).IsSelected = true;
            
            }
            catch { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuFlowTree_ItemSelected(object sender, MenuEventArgs e)
        {
            if (e.Tag == null)
            {
                return;
            }


            switch (e.Tag.ToString())
            {
                case "OpenFlow":

             flowSortID=       TvwFlow.Selected.ID;
                    OpenFlow(TvwFlow.Selected.Name, TvwFlow.Selected.Title);
                    break;
                case "NewFlow":

                    NewFlow();

                    break;
                case "NewFlowSort":

                    //NewFlowSort ns = new NewFlowSort(this);
                    //ns.Show();
                    title = "修改流程类别";
                    IsRefresh = true;
                    SetProper("", "EditFlowSort", null, null,  null);
                  

                    break;
                case "Delete":
                    TreeNode DeleteFlowNode = TvwFlow.Selected as TreeNode;
                    flowSortID=TvwFlow.Selected.ID;
                 
                   
                    if (!DeleteFlowNode.IsFlowSort)
                    {
                        DeleteFlow(TvwFlow.Selected.Name);
                      foreach (TabItem t in tbDesigner.Items)
                       {
                        Container ct = t.Content as Container;
                        if (ct.FlowID == TvwFlow.Selected.Name)
                        {
                            tbDesigner.Items.Remove(t);
                            break;

                        }
                       }
                    }
                    else
                    {
                        DeleteFlowSort(DeleteFlowNode.ID);
                    }
                    break;
                case "Refresh":
                    _Service.GetFlowSortAsync();
                    break;
                case "Edit":
                  
                  title = "修改流程类别";
                    IsRefresh = true;
                    SetProper("", "EditFlowSort", TvwFlow.Selected.ID, null, null);
                    break;
                    
            }

            MuFlowTree.Hide();
        }
        /// <summary>
        /// 键盘按键按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        /// <summary>
        /// 键盘按键释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {

        }
        /// <summary>
        /// 在工作流树空白处按下鼠标左键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CvsFlowTree_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MuFlowTree.Hide();
        }
        /// <summary>
        /// 工作流树被选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvwFlow_SelectionChanged(object sender, TreeEventArgs e)
        {
            MuFlowTree.Hide();
        }
        /// <summary>
        /// 右击工作流树事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvwFlow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(TvwFlow);
            TreeNode node = TvwFlow.GetNodeAtPoint(position) as TreeNode;

            if (node != null)
            {
                Point MuFlowTreePos = TvwFlow.TransformToVisual(TvwFlow).Transform(position);
                node.IsSelected = true;
                TvwFlow.ClearSelected();
                TvwFlow.SetSelected(node);

                MuFlowTree.SetValue(Canvas.LeftProperty, MuFlowTreePos.X);
                MuFlowTree.SetValue(Canvas.TopProperty, MuFlowTreePos.Y);
                MuFlowTree.Show();
                if (node.isRoot)
                {
                    MuFlowTree.Get("OpenFlow").IsEnabled = false;
                    MuFlowTree.Get("NewFlow").IsEnabled = false;
                    MuFlowTree.Get("Delete").IsEnabled = false;
                    MuFlowTree.Get("Edit").IsEnabled = false;

                }
                else
                {
                    MuFlowTree.Get("OpenFlow").IsEnabled = true;
                    MuFlowTree.Get("NewFlow").IsEnabled = true;
                    MuFlowTree.Get("Delete").IsEnabled = true;
                    MuFlowTree.Get("Edit").IsEnabled = true;
                }
                if (node.IsFlowSort)
                {
                   
                    MuFlowTree.Get("OpenFlow").IsEnabled = false;
                    MuFlowTree.Get("Edit").IsEnabled = true;
                }
                else
                { MuFlowTree.Get("Edit").IsEnabled = false;
                    MuFlowTree.Get("OpenFlow").IsEnabled = true;
                }
                
            }

            e.Handled = true;
        }
        /// <summary>
        /// 双击工作流树事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvwFlow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_doubleClickTimer.IsEnabled)
            {
                _doubleClickTimer.Stop();

                OpenFlow(TvwFlow.Selected.Name, TvwFlow.Selected.Title);
            }
            else
            {
                _doubleClickTimer.Start();

            }
        }
      /// <summary>
      /// 新建工作流事件
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void btnNewFlow_Click(object sender, RoutedEventArgs e)
        {

            NewFlow();
        }
/// <summary>
///添别节点事件
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void AddFlowNode_Click(object sender, RoutedEventArgs e)
        {
            if (NewContainer != null)
            {
                NewContainer.AddFlowNode();
                NewContainer.IsSave = true;
            }
        }
        /// <summary>
        /// 添加连线事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDirection_Click(object sender, RoutedEventArgs e)
        {
            if (NewContainer != null)
            {
                NewContainer.AddDirection();
                NewContainer.IsSave = true;
            }
        }
        /// <summary>
        /// 添加标签事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLabel_Click(object sender, RoutedEventArgs e)
        {
            {
                NewContainer.AddLabel();
                NewContainer.IsSave = true;
            }
        }
        /// <summary>
        /// 保存工作流事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (NewContainer != null)
                NewContainer.Save();
            NewContainer.IsSave = false;
        }
        /// <summary>
        /// 报表设计事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDesignerTable_Click(object sender, RoutedEventArgs e)
        {
            if (NewContainer != null)
                NewContainer.btnDesignerTable();
        }
        /// <summary>
        /// 检查事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (NewContainer != null)
                NewContainer.btnCheck();

        }
        /// <summary>
        /// 运行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            if (NewContainer != null)
                NewContainer.btnRun();
        }
       /// <summary>
       /// 编辑事件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try{
             if (NewContainer != null)
                NewContainer.Edit();
            }
            catch { }
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (NewContainer != null)
            {
                this.DeleteFlow(NewContainer.FlowID);
               
            }

        }
        /// <summary>
        /// 模板事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatMode_Click(object sender, RoutedEventArgs e)
        {
           
            if (NewContainer != null)
            {
                _Service.FlowTemplete_GenerAsync(NewContainer.FlowID,true);
                _service.FlowTemplete_GenerCompleted += new EventHandler<FlowTemplete_GenerCompletedEventArgs>(_service_FlowTemplete_GenerCompleted);
            }
        }

        void _service_FlowTemplete_GenerCompleted(object sender, FlowTemplete_GenerCompletedEventArgs e)
        {
            
            try
            {
                FlowTempleteUrl = e.Result;
                MessageBox.Show("生成成功！");
            }
            catch { MessageBox.Show("生成失败！"); }
            _service.FlowTemplete_GenerCompleted -= new EventHandler<FlowTemplete_GenerCompletedEventArgs>(_service_FlowTemplete_GenerCompleted);

        }
        /// <summary>
        /// 增加模板事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMode_Click(object sender, RoutedEventArgs e)
        {
            FileUpLoad fu = new FileUpLoad();
            fu.Show();
        }

      
        /// <summary>
        /// 关闭选项卡事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Click(object sender, RoutedEventArgs e)
        {
          Button btn= sender as Button;

          TabItemEx te = btn.DataContext as TabItemEx;
          if (te != null) 
            this.tbDesigner.Items.Remove(te);
           
            //if (NewContainer != null)
            //{
            //    if (NewContainer.IsSave)
            //    {
            //        if (System.Windows.Browser.HtmlPage.Window.Confirm(Text.IsSave))
            //        {
            //            if (NewContainer.Save())
            //            {
            //                this.tbDesigner.Items.Remove(this.tbDesigner.SelectedItem);
            //            }



            //        }
            //        else { this.tbDesigner.Items.Remove(this.tbDesigner.SelectedItem); }
            //    }
            //    else
            //    {
            //        this.tbDesigner.Items.Remove(this.tbDesigner.SelectedItem);

            //    }


            //}
        }
        /// <summary>
        /// 工作流选项卡事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
           private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// 执行新建工作流事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
           void _service_DoCompleted(object sender, DoCompletedEventArgs e)
           {
               string[] flow = e.Result.Split(';');
               FlowID = flow[0];
               _Service.DoCompleted -= new EventHandler<DoCompletedEventArgs>(_service_DoCompleted);

               OpenFlow(flow[0], flow[1]);
               if (TvwFlow.Selected == null)
               {
                   title = "流程属性";
                   SetProper("", "FlowP", FlowID, "0", "0");
               }

           }
        /// <summary>
        /// 弹出网页窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
           void _Service_WinOpenCompleted(object sender, WF.DataServiceReference.WinOpenCompletedEventArgs e)
           {
               string suburl = HtmlPage.Document.DocumentUri.ToString();
               string url = suburl.Substring(0, suburl.LastIndexOf('/'));
               WinOpen(url + e.Result,title);

               _Service.WinOpenCompleted -= new EventHandler<WF.DataServiceReference.WinOpenCompletedEventArgs>(_Service_WinOpenCompleted);

           }
       
        #endregion





       

           private void UserControl_Loaded(object sender, RoutedEventArgs e)
           {

               _doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
               _doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
               _doubleClickTimer.Tick += new EventHandler(DoubleClick_Timer);
               ApplyCulture();
               try
               {
                   LayoutRoot.Height = TbcFDS.Height = Application.Current.Host.Content.ActualHeight;
                   TvwFlow.Height = Application.Current.Host.Content.ActualHeight - 35;
                   tbDesigner.Height = Application.Current.Host.Content.ActualHeight - 35;
                   tbDesigner.Width = Application.Current.Host.Content.ActualWidth - 227;
               }
               catch { }
               Application.Current.Host.Content.FullScreenChanged += new EventHandler(Content_FullScreenChanged);
              
           }

           void Content_FullScreenChanged(object sender, EventArgs e)
           {
               _doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
               _doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
               _doubleClickTimer.Tick += new EventHandler(DoubleClick_Timer);
               ApplyCulture();
               try
               {
                   LayoutRoot.Height = TbcFDS.Height = Application.Current.Host.Content.ActualHeight;
                   TvwFlow.Height = Application.Current.Host.Content.ActualHeight - 35;
                   tbDesigner.Height = Application.Current.Host.Content.ActualHeight - 35;
                   tbDesigner.Width = Application.Current.Host.Content.ActualWidth - 227;
               }
               catch { }
           }

           private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
           {

               MuFlowTree.Hide();
           }

           private void TbcFDS_MouseLeave(object sender, MouseEventArgs e)
           {
               MuFlowTree.Hide();
           }

         

        
      
       
     


    
     
    }
}
