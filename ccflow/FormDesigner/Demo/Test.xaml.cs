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
    public partial class Test : UserControl
    {
        bool trackingMouseMove = false;
        Point mousePosition;
        public Test()
        {
            InitializeComponent();
            button1.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(Button_MouseLeftButtonDown), true);
            button1.AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(Button_MouseLeftButtonUp), true);
        }
        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mousePosition = e.GetPosition(null);
            trackingMouseMove = true;
        }
        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trackingMouseMove = false;
        }
        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (trackingMouseMove)
            {
                double moveH = e.GetPosition(null).Y - mousePosition.Y; double moveW = e.GetPosition(null).X - mousePosition.X;
                double newTop = moveH + (double)element.GetValue(Canvas.TopProperty);
                double newLeft = moveW + (double)element.GetValue(Canvas.LeftProperty);
                element.SetValue(Canvas.TopProperty, newTop); element.SetValue(Canvas.LeftProperty, newLeft);
                mousePosition = e.GetPosition(null);
            }
        }
        private void button2_MouseEnter(object sender, MouseEventArgs e)
        {
            button2.Content = "leave me";
        }

        private void button2_MouseLeave(object sender, MouseEventArgs e)
        {
            button2.Content = "enter me";
        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

            Image img = new Image();

        }
    }
}