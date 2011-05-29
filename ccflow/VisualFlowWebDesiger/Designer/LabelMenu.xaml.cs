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
    public partial class LabelMenu : UserControl
    {
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
        public void ApplyCulture()
        {
            btnCopy.Content = Text.Menu_CopyLabel;
            btnDelete.Content = Text.Menu_DeleteLabel;
            btnUpdate.Content = Text.UpdateLable;

        }
        public LabelMenu()
        {
            InitializeComponent();

        }

        private void deleteLabel(object sender, RoutedEventArgs e)
        {

            //if (System.Windows.Browser.HtmlPage.Window.Confirm(Text.Comfirm_Delete))
            //{
                this.Visibility = Visibility.Collapsed;
                if (isMultiControlSelect && _container.CurrentSelectedControlCollection != null
                    && _container.CurrentSelectedControlCollection.Count > 0)
                {
                    IElement iel;
                    foreach (System.Windows.Controls.Control c in _container.CurrentSelectedControlCollection)
                    {
                        iel = c as IElement;
                        if (iel != null)
                        {
                            iel.Delete();
                        }
                    }
                }
                relatedLabel.Delete();
                _container.SaveChange(HistoryType.New);


            //}
        }
        System.Windows.Threading.DispatcherTimer _menuTimer;
        void Menu_Timer(object sender, EventArgs e)
        {
            if (_menuTimer != null && _menuTimer.IsEnabled)
                _menuTimer.Stop();
            ShowMenu(Visibility.Collapsed);

        }
        bool isMultiControlSelect = false;
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
                    if (!_container.CurrentSelectedControlCollection.Contains(relatedLabel))
                    {
                        _container.ClearSelectFlowElement(null);
                        isMultiControlSelect = false;

                    }
                    else
                    {
                        isMultiControlSelect = true;

                    }
                }
                else
                {
                    isMultiControlSelect = false;
                }
                if (isMultiControlSelect)
                {
                    btnDelete.Content = Text.Menu_DeleteLabel;
                    btnCopy.Content = Text.Menu_CopySelected;
                    btnUpdate.Content = Text.UpdateLable;
                }
                else
                {
                    btnDelete.Content = Text.Menu_DeleteLabel;
                    btnCopy.Content = Text.Menu_CopyLabel;
                    btnUpdate.Content = Text.UpdateLable;

                }
                this.Visibility = visible;

                sbShowMenu.Begin();


            }
            else
            {
                sbCloseMenu.Completed += new EventHandler(sbCloseMenu_Completed);
                sbCloseMenu.Begin();
            }
        }
        NodeLabel relatedLabel;
        public NodeLabel RelatedLabel
        {
            get
            {
                return relatedLabel;
            }
            set
            {
                relatedLabel = value;
            }
        }
        void sbCloseMenu_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }


        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            ShowMenu(Visibility.Collapsed);

        }
        private void copyLabel(object sender, RoutedEventArgs e)
        {
            if (isMultiControlSelect)
            {
                _container.CopySelectedControlToMemory(null);
            }
            else
            {
                _container.CopySelectedControlToMemory(relatedLabel);

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

        private void UpdateLabel(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            _container.UpdateSelectedControlToMemory(relatedLabel);
            
            _container.SaveChange(HistoryType.New);
        }
    }
}
