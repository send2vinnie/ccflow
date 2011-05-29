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
using System.Windows.Browser;

namespace Ccflow.Web.UI.Control.Workflow.Designer
{
    public partial class FlowNodeMenu : UserControl
    {
        IContainer _container;
        public FlowNodeMenu()
        {
            InitializeComponent(); 
        }
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
        FlowNode relatedFlowNode;
        public FlowNode RelatedFlowNode
        {
            get
            {
                return relatedFlowNode;
            }
            set
            {
                relatedFlowNode = value;
            }
        }
        private void deleteFlowNode(object sender, RoutedEventArgs e)
        {
            if (relatedFlowNode != null)
            {
                if (System.Windows.Browser.HtmlPage.Window.Confirm(Text.Comfirm_Delete))
                {
                    this.Visibility = Visibility.Collapsed;
                   
                        IElement iel;
                        foreach (System.Windows.Controls.Control c in _container.CurrentSelectedControlCollection)
                        {
                        
                            iel = c as IElement;
                            if (iel != null)
                            {
                                iel.Delete();
                            }
                        }

                    
                  
                    relatedFlowNode.Delete();
                  
                  _container.SaveChange(HistoryType.New);
                   

                }
                    

            }
        }
       public void ApplyCulture()
        {
            //btnShowFlowNodeSetting.Content = Text.Menu_ModifyFlowNode;
            //btnCopy.Content = Text.Menu_CopyFlowNode;
            //btnDelete.Content = Text.Menu_DeleteFlowNode;
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
        System.Windows.Threading.DispatcherTimer _menuTimer;
        bool isMultiControlSelect = false;
        void Menu_Timer(object sender, EventArgs e)
        {
            if (_menuTimer != null && _menuTimer.IsEnabled)
                _menuTimer.Stop();
            ShowMenu(Visibility.Collapsed);

        }
        public void ShowMenu(Visibility visible)
        { 



            isMultiControlSelect = false;
            if (visible == Visibility.Visible)
            {
                if (_menuTimer == null)
                {
                    _menuTimer = new System.Windows.Threading.DispatcherTimer();
                    _menuTimer.Interval = new TimeSpan(0, 0, 0, 2, 0);
                    _menuTimer.Tick += new EventHandler(Menu_Timer);
                }
                _menuTimer.Start();


                if (_container.CurrentSelectedControlCollection != null
                    && _container.CurrentSelectedControlCollection.Count > 0
                    )
                {
                    if (!_container.CurrentSelectedControlCollection.Contains(relatedFlowNode))
                    {
                        _container.ClearSelectFlowElement(null);
                        //btnShowFlowNodeSetting.IsEnabled = true;
                        isMultiControlSelect = false;

                    }
                    else
                    {
                        //btnShowFlowNodeSetting.IsEnabled = false;
                        isMultiControlSelect = true;

                    }
                }
                else
                {
                    btnShowFlowNodeSetting.IsEnabled = true;
                    isMultiControlSelect = false;
                }
                if (isMultiControlSelect)
                {
                    btnDelete.Content = Text.Menu_DeleteSelected; ;
                    btnCopy.Content = Text.Menu_CopySelected; ;

                }
                else
                {
                    btnDelete.Content = Text.Menu_DeleteFlowNode;
                    btnCopy.Content = Text.Menu_CopyFlowNode;

                }
                this.Visibility = visible;
                sbShowMenu.Begin();


            }
            else
            {
                if (this.Visibility != visible)
                {
                    sbCloseMenu.Completed += new EventHandler(sbCloseMenu_Completed);
                    sbCloseMenu.Begin();
                }
            }
            try
            {

                if (RelatedFlowNode.Type == FlowNodeType.INITIAL)
                {
                    this.btnDelete.IsEnabled = false;
                }
                else
                {
                    this.btnDelete.IsEnabled = true;
                }
            }
            catch { }
        }
        void sbCloseMenu_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void showFlowNodeSetting(object sender, RoutedEventArgs e) 
        {
            this.Visibility = Visibility.Collapsed;
            _container.ShowFlowNodeSetting(relatedFlowNode);

        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        { 
            ShowMenu(Visibility.Collapsed);

        }
        private void ShowSubFlow(object sender, RoutedEventArgs e)
        {
        }
        private void copyFlowNode(object sender, RoutedEventArgs e)
        {
            if (isMultiControlSelect)
            {
                _container.CopySelectedControlToMemory(null);
            }
            else
            {
                _container.CopySelectedControlToMemory(relatedFlowNode);

            }
            this.Visibility = Visibility.Collapsed;

        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_menuTimer != null && _menuTimer.IsEnabled)
            {
                _menuTimer.Stop();
                _menuTimer = null;
            }
        }

        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }


        //private void SetProper(string lang, string dotype, string fk_flow, string node1, string node2)
        //{
        //    _container._Service.WinOpenAsync(lang, dotype, fk_flow, node1, node2);
        //    _container._Service.WinOpenCompleted += new EventHandler<WF.DataServiceReference.WinOpenCompletedEventArgs>(_Service_WinOpenCompleted);
        //}
        //void _Service_WinOpenCompleted(object sender, WF.DataServiceReference.WinOpenCompletedEventArgs e)
        //{
        //    string suburl=HtmlPage.Document.DocumentUri.ToString();
        //    string url = suburl.Substring(0, suburl.LastIndexOf('/'));
        //    _container.WinOpen(url+e.Result);

        //    _container._Service.WinOpenCompleted -= new EventHandler<WF.DataServiceReference.WinOpenCompletedEventArgs>(_Service_WinOpenCompleted);
        
        //}
        private void btnNodeProper_Click(object sender, RoutedEventArgs e)
        {
            _container.IsContainerRefresh = true;
            _container.SetProper("", "NodeP", _container.FlowID, RelatedFlowNode.FlowNodeID, "0","结点属性");
        }

       

        private void btnNodeTable_Click(object sender, RoutedEventArgs e)
        {
            _container.IsContainerRefresh = false;
            _container.SetProper("", "MapDef", _container.FlowID, RelatedFlowNode.FlowNodeID, "0", "报表设计");
          
        }

        private void btnNodeStation_Click(object sender, RoutedEventArgs e)
        {
            _container.IsContainerRefresh = false;
           
            _container.SetProper("", "StaDef", _container.FlowID, RelatedFlowNode.FlowNodeID, "0", "节点岗位");

        }

        private void btnFlowConditions_Click(object sender, RoutedEventArgs e)
        {
            _container.IsContainerRefresh = false;

            _container.SetProper("", "MapDef", _container.FlowID, RelatedFlowNode.FlowNodeID, "0", "定义");

        }

        private void btnFlowProper_Click(object sender, RoutedEventArgs e)
        {    _container.IsContainerRefresh = true;
            _container.SetProper("", "FlowP", _container.FlowID, RelatedFlowNode.FlowNodeID, "0","流程属性");
        

        }
     

    }

}