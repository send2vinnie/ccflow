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
using Liquid;
using WF.Controls;
using WF.DataServiceReference;
using WF.Resources;

namespace Ccflow.Web.UI.Control.Workflow.Designer
{
    public partial class ContainerMenu : UserControl
    {
        private FlowNode flowNode = null;

        public ContainerMenu()
        {
            InitializeComponent();
        }
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            MuContentMenu.Hide();

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
                // 调整x,y 值 ，以防止菜单被遮盖住
                var x = value.X;
                var y = value.Y;
                var menuHeight = 400;
                var menuWidth = 170;
                var hostWidth = Application.Current.Host.Content.ActualWidth - 250;
                var hostHeight = Application.Current.Host.Content.ActualHeight;
                if (x + menuWidth > hostWidth)
                {
                    x = x - (x + menuWidth - hostWidth);
                }
                if (y + menuHeight > hostHeight)
                {
                    y = y - (y + menuHeight - hostHeight);
                }
                this.SetValue(Canvas.TopProperty, y);
                this.SetValue(Canvas.LeftProperty, x);
            }
        }
        
         
        public void ShowMenu()
        {
            
            this.Visibility = Visibility.Visible;
            MuContentMenu.Show();
        }
        
        private void menuAddNode_Click()
        {
            addAcitivty(FlowNodeType.INTERACTION);

        }
        private void menuAddLine_Click()
        {
            Direction r = new Direction(_container);
            r.SetValue(Canvas.ZIndexProperty, _container.NextMaxIndex);
            r.DirectionName = Text.NewDirection + _container.NextNewDirectionIndex.ToString();

            _container.AddDirection(r);
            r.SetDirectionPosition(new Point(CenterPoint.X - 20, CenterPoint.Y - 20), new Point(CenterPoint.X + 30, CenterPoint.Y + 30),null,null);
            _container.SaveChange(HistoryType.New);
 
        }
        private void menuAddLabel_Click()
        {
            NodeLabel l = new NodeLabel(_container);
            l.LabelName = Text.NewLable + _container.NextNewLabelIndex.ToString();

            _container.AddLabel(l);
            l.Position = new Point(CenterPoint.X - 20, CenterPoint.Y - 20);
            _container.SaveChange(HistoryType.New);
           
        }
        
        private void menuDelete_Click()
        {
            if (System.Windows.Browser.HtmlPage.Window.Confirm(Text.Comfirm_Delete))
            {
                _container.DeleteSeletedControl();
                _container.SaveChange(HistoryType.New);
            }

        }

        void addAcitivty(FlowNodeType type)
        {
            flowNode = new FlowNode(_container, type);
            flowNode.SetValue(Canvas.ZIndexProperty, _container.NextMaxIndex);
            flowNode.FlowNodeName = Text.NewFlowNode + _container.NextNewFlowNodeIndex.ToString();
            flowNode.CenterPoint = this.CenterPoint;

            _container._Service.DoNewNodeAsync(_container.FlowID, 10, 10, flowNode.FlowNodeName, true);
            _container._Service.DoNewNodeCompleted += _Service_DoNewNodeCompleted;

        }

        void _Service_DoNewNodeCompleted(object sender, DoNewNodeCompletedEventArgs e)
        {
            flowNode.FlowNodeID = e.Result.ToString();
            _container.AddFlowNode(flowNode);
            _container.SaveChange(HistoryType.New);

            _container._Service.DoNewNodeCompleted -= _Service_DoNewNodeCompleted;

        }

        private void AddFlowNodeSubMenu_Click(object sender, RoutedEventArgs e)
        {
            FlowNodeType type =(FlowNodeType) Enum.Parse(typeof(FlowNodeType), ((HyperlinkButton)sender).Name, true);
            addAcitivty(type);
        }
         
  
        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            MuContentMenu.Hide();
        }
       

        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void menuFlowProperty_Click()
        {
            _container.SetProper("", "FlowP", _container.FlowID, "0", "0","流程属性");
            _container.IsContainerRefresh = true;
        }

        private void menuRunFlow_Click()
        {
            _container.IsContainerRefresh = false;

            _container.SetProper("", "RunFlow", _container.FlowID, "0", "0","运行");
        }

        private void menuFlowCheck_Click()
        {
            _container.IsContainerRefresh = false;

            _container.SetProper("", "FlowCheck", _container.FlowID, "0", "0","检查");

         }

        private void menuFlowDefination_Click()
        {
            _container.IsContainerRefresh = false;

            _container.SetProper("", "WFRpt", _container.FlowID, "0", "0", "流程报表定义");
         }

       
        private void menuDisplayHideGrid_Click()
        {

            if (_container.GridLinesContainer.Children.Count>0)
            {
                _container.GridLinesContainer.Children.Clear();
                
            }
            else
            {
                _container.SetGridLines();
            }
        }

        private void menuFullScreen_Click()
        {
           Application.Current.Host.Content.IsFullScreen = !Application.Current.Host.Content.IsFullScreen;
        }

       

        private void Menu_ItemSelected(object sender, MenuEventArgs e)
        {
            if (e.Tag == null)
            {
                return;
            }

            switch (e.Tag.ToString())
            {
                case "menuFullScreen":
                    menuFullScreen_Click();
                    break;
                case "menuAddNode":
                    menuAddNode_Click();
                    break;
                case "menuAddLine":
                    menuAddLine_Click();
                    break;
                case "menuAddLabel":
                    menuAddLabel_Click();
                    break;
                case "menuFlowPropertity":
                    menuFlowProperty_Click();
                    break;
                case "menuRunFlow":
                    menuRunFlow_Click();
                    break;
                case "menuCheckFlow":
                    menuFlowCheck_Click();
                    break;
                case "menuFlowDefination":
                    menuFlowDefination_Click();
                    break;
                case "menuDelete":
                    menuDelete_Click();
                    break;
                case "MenuDisplayHideGrid":
                    menuDisplayHideGrid_Click();
                    break;
            }

            MuContentMenu.Hide();
        }
    }
}
