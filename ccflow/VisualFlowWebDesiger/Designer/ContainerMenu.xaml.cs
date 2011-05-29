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
using WF.Resources;

namespace Ccflow.Web.UI.Control.Workflow.Designer
{
    public partial class ContainerMenu : UserControl
    {
        public ContainerMenu()
        {
            InitializeComponent();
        }
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        { 
            ShowMenu(Visibility.Collapsed);

        }
        public void ApplyCulture()
        {

            btnAddFlowNode.Content = Text.Button_AddFlowNode;
            btnAddDirection.Content = Text.Button_AddDirection;
            btnAddLabel.Content = Text.Button_AddLabel;
            btnFlowProper.Content = Text.Button_FlowProper;
            btnRunFlow.Content = Text.Button_RunFlow;
            btnFlowCheck.Content = Text.Button_FlowCheck;
            btnFlowRptDefinition.Content = Text.Button_FlowRptDefinition;
            btnShowLine.Content = Text.Button_ShowLine;
            //btnCopy.Content = Text.Menu_CopySelected;
            //btnNext.Content = Text.Button_Next;
            //btnPaste.Content = Text.Menu_Paste;
            //btnPrevious.Content = Text.Button_Previous;
            //btnDelete.Content = Text.Menu_DeleteSelected;

            //INTERACTION.Content = Text.FlowNodeType_INTERACTION;
            //AND_BRANCH.Content = Text.FlowNodeType_AND_BRANCH;
            //AND_MERGE.Content = Text.FlowNodeType_AND_MERGE;
            //AUTOMATION.Content = Text.FlowNodeType_AUTOMATION;
            //COMPLETION.Content = Text.FlowNodeType_COMPLETION;
            //DUMMY.Content = Text.FlowNodeType_DUMMY;
            //INITIAL.Content = Text.FlowNodeType_INITIAL;
            //OR_BRANCH.Content = Text.FlowNodeType_OR_BRANCH;
            //OR_MERGE.Content = Text.FlowNodeType_OR_MERGE;
            //SUBPROCESS.Content = Text.FlowNodeType_SUBPROCESS;
            //VOTE_MERGE.Content = Text.FlowNodeType_VOTE_MERGE;

            //btnAlignBottom.Content = Text.Menu_AlignBottom;
            //btnAlignLeft.Content = Text.Menu_AlignLeft;
            //btnAlignRight.Content = Text.Menu_AlignRight;
            //btnAlignTop.Content = Text.Menu_AlignTop;
            //btnAddLabel.Content = Text.Button_AddLabel;
        }
        IContainer _container;
        public IContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }
        public Point CenterPoint
        {
            get
            {
                return new Point((double)this.GetValue(Canvas.LeftProperty), (double)this.GetValue(Canvas.TopProperty));
            }
            set
            {
                this.SetValue(Canvas.TopProperty, value.Y);
                this.SetValue(Canvas.LeftProperty, value.X);
            }
        }
        void Menu_Timer(object sender, EventArgs e)
        {
            if (_menuTimer != null && _menuTimer.IsEnabled)
                _menuTimer.Stop();
            ShowMenu(Visibility.Collapsed);

        }
        System.Windows.Threading.DispatcherTimer _menuTimer;

        public void ShowMenu(Visibility visible)
        {
            if (visible == Visibility.Visible)
            {
                this.Visibility = visible;

                if (_menuTimer == null)
                {
                    _menuTimer = new System.Windows.Threading.DispatcherTimer();
                    _menuTimer.Interval = new TimeSpan(0, 0, 0, 2, 0);
                    _menuTimer.Tick += new EventHandler(Menu_Timer);
                }
                _menuTimer.Start();

                setButtonEnable();
                sbShowMenu.Begin();


            }
            else
            {
                sbCloseMenu.Completed += new EventHandler(sbCloseMenu_Completed); 
                sbCloseMenu.Begin();
            }
          

        }

        void sbCloseMenu_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed ;
        }
        void setButtonEnable()
        {
            if (_container.CurrentSelectedControlCollection != null
                   && _container.CurrentSelectedControlCollection.Count > 0)
            {
                btnCopy.IsEnabled = true;
                btnDelete.IsEnabled = true;

                if (_container.CurrentSelectedControlCollection.Count > 1)
                {
                    btnAlignTop.IsEnabled = true;
                    btnAlignBottom.IsEnabled = true;
                    btnAlignLeft.IsEnabled = true;
                    btnAlignRight.IsEnabled = true;
                }
                else
                {
                    btnAlignTop.IsEnabled = false;
                    btnAlignBottom.IsEnabled = false;
                    btnAlignLeft.IsEnabled = false;
                    btnAlignRight.IsEnabled = false;
                }
                
            }
            else
            {
                btnCopy.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnAlignTop.IsEnabled = false;
                btnAlignBottom.IsEnabled = false;
                btnAlignLeft.IsEnabled = false;
                btnAlignRight.IsEnabled = false;
            }

            if (_container.CopyElementCollectionInMemory != null
               && _container.CopyElementCollectionInMemory.Count > 0)
            {
                btnPaste.IsEnabled = true;
            }
            else
            {
                btnPaste.IsEnabled = false;
            }
            if (_container.WorkFlowXmlPreStack.Count == 0)
            {
                btnPrevious.IsEnabled = false;
            }
            else
                btnPrevious.IsEnabled = true;

            if (_container.WorkFlowXmlNextStack.Count == 0)
            {
                btnNext.IsEnabled = false;
            }
            else
                btnNext.IsEnabled = true;
        }
        private void btnAddFlowNode_Click(object sender, RoutedEventArgs e)
        {
           
            addAcitivty(FlowNodeType.INTERACTION);

        }
        private void btnAddDirection_Click(object sender, RoutedEventArgs e)
        {
            Direction r = new Direction(_container);
            r.SetValue(Canvas.ZIndexProperty, _container.NextMaxIndex);
            r.DirectionName = Text.NewDirection + _container.NextNewDirectionIndex.ToString();

            _container.AddDirection(r);
            r.SetDirectionPosition(new Point(CenterPoint.X - 20, CenterPoint.Y - 20), new Point(CenterPoint.X + 30, CenterPoint.Y + 30),null,null);
            _container.SaveChange(HistoryType.New);
            setButtonEnable();
            ShowMenu(Visibility.Collapsed);

        }
        private void btnAddLabel_Click(object sender, RoutedEventArgs e)
        {
            NodeLabel l = new NodeLabel(_container);
            l.LabelName = Text.NewLable + _container.NextNewLabelIndex.ToString();

            _container.AddLabel(l);
            l.Position = new Point(CenterPoint.X - 20, CenterPoint.Y - 20);
            _container.SaveChange(HistoryType.New);
            setButtonEnable();
            ShowMenu(Visibility.Collapsed);

        }
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            _container.CopySelectedControlToMemory(null);
            setButtonEnable();
            ShowMenu(Visibility.Collapsed);

        }
        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            _container.PastMemoryToContainer();
            setButtonEnable();
            ShowMenu(Visibility.Collapsed);
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            _container.PreviousAction();
            setButtonEnable();
            ShowMenu(Visibility.Collapsed);


        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _container.NextAction();
            setButtonEnable();
            ShowMenu(Visibility.Collapsed);


        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Browser.HtmlPage.Window.Confirm(Text.Comfirm_Delete))
            {
                _container.DeleteSeletedControl();
                _container.SaveChange(HistoryType.New);
            }
            //setButtonEnable();
            ShowMenu(Visibility.Collapsed);


        }

        void addAcitivty(FlowNodeType type)
        {
            FlowNode a = new FlowNode(_container, type);
            a.SetValue(Canvas.ZIndexProperty, _container.NextMaxIndex);
            a.FlowNodeName = Text.NewFlowNode + _container.NextNewFlowNodeIndex.ToString();
            _container.AddFlowNode(a);
            a.CenterPoint = this.CenterPoint;
            _container.SaveChange(HistoryType.New);
            setButtonEnable();

            ShowMenu(Visibility.Collapsed);
        }

        private void AddFlowNodeSubMenu_Click(object sender, RoutedEventArgs e)
        {
            FlowNodeType type =(FlowNodeType) Enum.Parse(typeof(FlowNodeType), ((HyperlinkButton)sender).Name, true);
            addAcitivty(type);
        }
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_menuTimer != null && _menuTimer.IsEnabled)
            {
                _menuTimer.Stop();
                _menuTimer = null;
            }
        } 
        private void Path_MouseEnter(object sender, MouseEventArgs e)
        {
            if (bdSubMenu.Visibility == Visibility.Collapsed)
            { 
                bdSubMenu.Visibility = Visibility.Visible;
                sbShowSubMenu.Begin();
            }
        }

        

        void sbCloseSubMenu_Completed(object sender, EventArgs e)
        {
            bdSubMenu.Visibility = Visibility.Collapsed;
           
        }
        void closeSubMenu()
        {
            if (bdSubMenu.Visibility == Visibility.Visible)
            {
                sbCloseSubMenu.Completed += new EventHandler(sbCloseSubMenu_Completed);
                sbCloseSubMenu.Begin();
            }
        }
        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            closeSubMenu();
        }

        private void HyperlinkButton_MouseEnter(object sender, MouseEventArgs e)
        {
            closeSubMenu();
        }
        private void btnTopOrderliness_Click(object sender, RoutedEventArgs e)
        {
            
                _container.AlignTop();
                _container.SaveChange(HistoryType.New); 


        }
        private void btnLeftOrderliness_Click(object sender, RoutedEventArgs e)
        {
            _container.AlignLeft();
            _container.SaveChange(HistoryType.New); 


        }
        private void btnBottomOrderliness_Click(object sender, RoutedEventArgs e)
        {

            _container.AlignBottom();
            _container.SaveChange(HistoryType.New);


        }
        private void btnRightOrderliness_Click(object sender, RoutedEventArgs e)
        {
            _container.AlignRight();
            _container.SaveChange(HistoryType.New);


        }

        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void btnFlowProper_Click(object sender, RoutedEventArgs e)
        {
            _container.SetProper("", "FlowP", _container.FlowID, "0", "0","流程属性");
            _container.IsContainerRefresh = true;
            ShowMenu(Visibility.Collapsed);
        }

        private void btnRunFlow_Click(object sender, RoutedEventArgs e)
        {
            _container.IsContainerRefresh = false;

            _container.SetProper("", "RunFlow", _container.FlowID, "0", "0","运行");
            ShowMenu(Visibility.Collapsed);
        }

        private void btnFlowCheck_Click(object sender, RoutedEventArgs e)
        {
            _container.IsContainerRefresh = false;

            _container.SetProper("", "FlowCheck", _container.FlowID, "0", "0","检查");

            ShowMenu(Visibility.Collapsed);
        }

        private void btnFlowRptDefinition_Click(object sender, RoutedEventArgs e)
        {
            _container.IsContainerRefresh = false;

            _container.SetProper("", "WFRpt", _container.FlowID, "0", "0", "流程报表定义");
            ShowMenu(Visibility.Collapsed);
        }

       
        private void btnShowLine_Click(object sender, RoutedEventArgs e)
        {

            if (_container.GridLinesContainer.Children.Count>0)
            {
                _container.GridLinesContainer.Children.Clear();
              
                
            }
            else
            {
                _container.SetGridLines();
            }
            ShowMenu(Visibility.Collapsed);
        }

        private void btnFullScreen_Click(object sender, RoutedEventArgs e)
        {
           
            Application.Current.Host.Content.IsFullScreen = !Application.Current.Host.Content.IsFullScreen;
            Application.Current.Host.Content.FullScreenChanged += new EventHandler(Content_FullScreenChanged);
        }

        void Content_FullScreenChanged(object sender, EventArgs e)
        {
            if (Application.Current.Host.Content.IsFullScreen)
            {
                btnFullScreen.Content = "退出全屏";
                
            }
            else
                btnFullScreen.Content = "全屏显示";
        }
    }
}
