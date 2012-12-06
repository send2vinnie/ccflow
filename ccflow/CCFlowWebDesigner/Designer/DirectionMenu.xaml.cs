﻿using System;
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
using BP;

namespace BP
{
    public partial class DirectionMenu : UserControl
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
                // 调整x,y 值 ，以防止菜单被遮盖住
                var x = value.X;
                var y = value.Y;
                var menuHeight = 100;
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
        public void ApplyCulture()
        {
           

        }
        public DirectionMenu()
        {
            InitializeComponent();

        }
        Direction relatedDirection;
        public Direction RelatedDirection
        {
            get
            {
                return relatedDirection;
            }
            set
            {
                relatedDirection = value;
            }
        }

        private void deleteDirection()
        {
            if (relatedDirection != null)
            {
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
                relatedDirection.Delete();
                _container.SaveChange(HistoryType.New);
                _container.IsNeedSave = true;

            }
        }
      
        bool isMultiControlSelect = false;

        public void ShowMenu()
        {
            if (RelatedDirection.BeginFlowNode == null && RelatedDirection.EndFlowNode == null)
            {
                this.MuContentMenu.SetEnabledStatus("menuSetDirectionCondition", false);
            }
            else
            {
                this.MuContentMenu.SetEnabledStatus("menuSetDirectionCondition", true);
            }
            isMultiControlSelect = false;

            if (_container.CurrentSelectedControlCollection != null
                && _container.CurrentSelectedControlCollection.Count > 0
                )
            {
                if (!_container.CurrentSelectedControlCollection.Contains(relatedDirection))
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

            this.Visibility = Visibility.Visible;

            MuContentMenu.Show();
        }
       
        private void showDirectionSetting()
        {
            this.Visibility = Visibility.Collapsed;
            _container.ShowDirectionSetting(relatedDirection);
            _container.IsContainerRefresh = false;

        }
        private void Menu_ItemSelected(object sender, MenuEventArgs e)
        {
            if (e.Tag == null)
            {
                return;
            }
            switch (e.Tag.ToString())
            {
                case "menuDeleteDirection":
                    deleteDirection();
                    break;
                case "menuSetDirectionCondition":
                  //  _Service.GetRelativeUrlAsync("", "Dir", FlowID, r.BeginFlowNode.FlowNodeID, r.EndFlowNode.FlowNodeID, true);
                    showDirectionSetting();
                    break;
            }
            MuContentMenu.Hide();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
