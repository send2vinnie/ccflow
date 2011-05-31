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

namespace Demo
{
    public partial class AddEnumGuide : ChildWindow
    {
        public AddEnumGuide()
        {
            InitializeComponent();
            this.Visibility = System.Windows.Visibility.Visible;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}

