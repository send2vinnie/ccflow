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
using BP;

namespace BP.Picture
{
    public partial class HeLiuNode : UserControl, IFlowNodePicture
    {
        MergePictureRepeatDirection _repeatDirection = MergePictureRepeatDirection.None;
        public MergePictureRepeatDirection RepeatDirection
        {
            get
            {
                if (_repeatDirection == MergePictureRepeatDirection.None)
                {
                    if (picRect.Width > picRect.Height)
                        _repeatDirection = MergePictureRepeatDirection.Horizontal;
                    else
                        _repeatDirection = MergePictureRepeatDirection.Vertical;
                }

                return _repeatDirection;
            }
            set
            {
                _repeatDirection = value;
                if (_repeatDirection == MergePictureRepeatDirection.Vertical)
                {
                    picRect.Height = 60.0;
                    picRect.Width = 20.0;
                }
                else
                {
                    picRect.Height = 20.0;
                    picRect.Width = 60.0;
                }
            }
        }
        public HeLiuNode()
        {
            InitializeComponent();
        }
        public new double Opacity
        {
            set { picRect.Opacity = value; }
            get { return picRect.Opacity; }
        }
        public double PictureWidth
        {
            set { picRect.Width = value; }
            get { return picRect.Width; }
        }
        public double PictureHeight
        {
            set { picRect.Height = value; }
            get { return picRect.Height; }
        }
        public new Brush Background
        {
            set
            {
                picRect.Fill = value;
            }
            get { return picRect.Fill; }
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
            brush.Color = Colors.White;
            picRect.Fill = brush;
            brush = new SolidColorBrush();
            brush.Color = Colors.Green;
            picRect.Stroke = brush;
        }
        public void SetWarningColor()
        {
            picRect.Fill = SystemConst.ColorConst.WarningColor;

        }
        public void SetSelectedColor()
        {
            picRect.Fill = SystemConst.ColorConst.SelectedColor;

        }

        public PointCollection ThisPointCollection
        {
            get { return null; }
        }
    }
}
