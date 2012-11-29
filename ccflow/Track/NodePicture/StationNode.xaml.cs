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

namespace BP.Picture
{
    public partial class StationNode : UserControl,IFlowNodePicture
    {
        public StationNode()
        {
            InitializeComponent();
        }

        #region IFlowNodePicture 成员


        public new double Opacity
        {
            set
            {
                picSTATION.Opacity = value;
            }
            get { return picSTATION.Opacity; }
        }
        public double PictureWidth
        {
            set
            {
                picSTATION.Width = value - 4;
                //   eliBorder.Width = value - 2;
            }
            get { return picSTATION.Width + 4; }
        }
        public double PictureHeight
        {
            set
            {
                picSTATION.Height = value - 4;
                //  eliBorder.Height = value - 2;
            }
            get { return picSTATION.Height + 4; }
        }
        public new Brush Background
        {
            set
            {
                picSTATION.Background = value;
            }
            get { return picSTATION.Background; }
        }
        public Visibility PictureVisibility
        {
            set
            {

                this.Visibility = value;
            }
            get
            {

                return this.Visibility;
            }
        }
        public void ResetInitColor()
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(255, 144, 117, 92);
            picSTATION.Background = brush;
          
         
        }

        public void SetWarningColor()
        {
            picSTATION.Background = SystemConst.ColorConst.WarningColor;
        }
        public void SetSelectedColor()
        {
            picSTATION.Background = SystemConst.ColorConst.SelectedColor;


        }
        public PointCollection ThisPointCollection
        {
            get { return null; }
        }
        #endregion


        public string StationMessage
        {
            set
            {
                tbMessage.Text = value;


            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void picSTATION_MouseMove(object sender, MouseEventArgs e)
        {
            double x = Canvas.GetLeft(picSTATION);

            double y = Canvas.GetTop(picSTATION);
            double bx = x + picSTATION.ActualWidth-5;
            double by = y + picSTATION.ActualHeight-20;
            if ((e.GetPosition(picSTATION).X < bx && e.GetPosition(picSTATION).X > bx - 6) && (e.GetPosition(picSTATION).Y < bx && e.GetPosition(picSTATION).Y > by - 6))
            { picSTATION.Cursor = Cursors.SizeNWSE; }
            else
            {
                picSTATION.Cursor = null;
            }
        //  picSTATION
           
        }
      
    }
}
