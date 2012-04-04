﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BP.En;
using Microsoft.Expression.Interactivity;
using Microsoft.Expression.Interactivity.Layout;
namespace CCForm
{
    public class BPEle : System.Windows.Controls.TextBox
    {
        public string EleType = null;
        public string EleName = null;
        public string EleID = null;
        public string Tag1 = null;
        public string Tag2 = null;
        public string Tag3 = null;
        public string Tag4 = null;
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            this.Text = "按住边框拖动位置,Shift+方向键改变大小."+this.Name;
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            this.Text = "";
            base.OnMouseLeave(e);
        }

        #region 处理选中.
        public string Desc = null;
        private bool _IsSelected = false;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                if (value == true)
                {
                    Thickness d = new Thickness(1);
                    this.BorderThickness = d;
                    this.BorderBrush = new SolidColorBrush(Colors.Blue);
                }
                else
                {
                    Thickness d1 = new Thickness(0.5);
                    this.BorderThickness = d1;
                    this.BorderBrush = new SolidColorBrush(Colors.Black);
                }
            }
        }
        public void SetUnSelectedState()
        {
            if (this.IsSelected)
                this.IsSelected = false;
            else
                this.IsSelected = true;
        }
        #endregion 处理选中.

        public double X = 0;
        public double Y = 0;
        public string KeyName = null;
        /// <summary>
        /// BPEle
        /// </summary>
        public BPEle()
        {
            this.IsReadOnly = true;
            this.Width = 200;
            this.Height = 150;
            ImageBrush ib = new ImageBrush();
            BitmapImage png = new BitmapImage(new Uri("/CCForm;component/Img/FrmEleDef.png", UriKind.Relative));
            ib.ImageSource = png;
            this.Background = ib;

            this.HisPng = png;
            this.TextWrapping = System.Windows.TextWrapping.Wrap;
        }
        public BitmapImage HisPng = null;

        #region 移动事件
        public bool IsCanReSize
        {
            get
            {
                return true;
            }
        }
        public bool IsCanDel
        {
            get
            {
                return true;
            }
        }
        public double MoveStep
        {
            get
            {
                if (Keyboard.Modifiers == ModifierKeys.Shift)
                    return 1;
                if (Keyboard.Modifiers == ModifierKeys.Control)
                    return 2;
                return 0;
            }
        }
        bool isCopy = false;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.Handled = true;
            this.X = (double)this.GetValue(Canvas.LeftProperty);
            this.Y = (double)this.GetValue(Canvas.TopProperty);
            switch (e.Key)
            {
                case Key.Up:
                case Key.W:
                    if (this.MoveStep != 0)
                    {
                        if (this.IsCanReSize == false)
                        {
                            MessageBox.Show("此控件不允许改变大小");
                            return;
                        }
                        if (this.Height > 18)
                            this.Height = this.Height - this.MoveStep;
                    }
                    else
                    {
                        this.SetValue(Canvas.TopProperty, this.Y - 1);
                    }
                    break;
                case Key.Down:
                case Key.S:
                    if (this.MoveStep != 0)
                    {
                        if (this.IsCanReSize == false)
                        {
                            MessageBox.Show("此控件不允许改变大小");
                            return;
                        }
                        //   if (this.Height > 23)
                        this.Height = this.Height + this.MoveStep;
                    }
                    else
                    {
                        this.SetValue(Canvas.TopProperty, this.Y + 1);
                    }
                    break;
                case Key.Left:
                case Key.A:
                    if (this.MoveStep != 0)
                    {
                        if (this.IsCanReSize == false)
                        {
                            MessageBox.Show("此控件不允许改变大小");
                            return;
                        }
                        if (this.Width > 8)
                            this.Width = this.Width - this.MoveStep;
                    }
                    else
                    {
                        this.SetValue(Canvas.LeftProperty, this.X - 1);
                    }
                    break;
                case Key.Right:
                case Key.D:
                    if (Keyboard.Modifiers == ModifierKeys.Shift)
                    {
                        if (this.IsCanReSize == false)
                        {
                            MessageBox.Show("此控件不允许改变大小");
                            return;
                        }
                        this.Width = this.Width + 1;
                    }
                    else
                    {
                        this.SetValue(Canvas.LeftProperty, this.X + 1);
                    }
                    break;
                case Key.Delete:
                    if (this.IsCanDel == false)
                    {
                        MessageBox.Show("该字段[" + this.Name + "]不可删除!", "提示", MessageBoxButton.OK);
                        return;
                    }
                    Canvas c = this.Parent as Canvas;
                    c.Children.Remove(this);
                    return;
                default:
                    break;
            }

            this.X = (double)this.GetValue(Canvas.LeftProperty);
            this.Y = (double)this.GetValue(Canvas.TopProperty);

            base.OnKeyDown(e);
        }
        public void DoCopy()
        {
            return;
        }
        #endregion 移动事件
    }
}
