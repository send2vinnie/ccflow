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

namespace BP.Picture
{
    public partial class FenHeLiuNode : UserControl,IFlowNodePicture
    {
        public FenHeLiuNode()
        {
            InitializeComponent();
        }
        public new double Opacity
        {
            set { picRect.Opacity = value; }
            get { return picRect.Opacity; }
        }
        public  double PictureWidth
        {
            set { picRect.Width = value; }
            get { return picRect.Width; }
        }
        public  double PictureHeight
        {
            set { picRect.Height = value; }
            get { return picRect.Height; }
        }
        public new Brush Background
        {
            set { picRect.Fill = value; }
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
