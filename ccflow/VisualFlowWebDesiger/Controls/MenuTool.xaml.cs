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
using Ccflow.Web.UI.Control.Workflow.Designer;

namespace WF.Controls
{
    public partial class MenuTool : UserControl
    {
        public MenuTool()
        {
            InitializeComponent();
        }

        private void btnFlow_Click(object sender, RoutedEventArgs e)
        {
            flowlist.IsOpen = true;
           
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
         //   flowlist.IsOpen = false;
        }

        private void btnNewFlow_Click(object sender, RoutedEventArgs e)
        {
            Designers ds = this.Parent as Designers;
            ds.NewFlow();
        }

        private void AddFlowNode_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddDirection_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddLabel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDesignerTable_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreatMode_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddMode_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
