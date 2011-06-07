using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BP.En;

namespace FreeFrm
{
    /// <summary>
    /// 类型
    /// </summary>
    public enum TBType
    {
        String,
        Int,
        Float,
        Money,
        DateTime,
        Date
    }
    public class BPTextBox : System.Windows.Controls.TextBox
    {
        public double X = 0;
        public double Y=0;
        //public double X
        //{
        //    get
        //    {
        //        return (double)this.GetValue(Canvas.LeftProperty);
        //    }
        //}
        //public double Y
        //{
        //    get
        //    {
        //        return (double)this.GetValue(Canvas.TopProperty);
        //    }
        //}
        /// <summary>
        /// 类型
        /// </summary>
        public TBType HisTBType = TBType.String;
        public string HisDataType
        {
            get
            {
                switch (this.HisTBType)
                {
                    case TBType.Float:
                        return DataType.AppFloat;
                    case TBType.Money:
                        return DataType.AppMoney;
                    case TBType.Int:
                        return DataType.AppInt;
                    case TBType.String:
                    default:
                        return DataType.AppString;
                }
            }
        }
        /// <summary>
        /// BPTextBox
        /// </summary>
        public BPTextBox()
        {
            this.Name = "TB" + DateTime.Now.ToString("yyMMddhhmmss");
        }
        public BPTextBox(TBType ty)
        {
            this.Name = "TB" + DateTime.Now.ToString("yyMMddhhmmss");
            this.HisTBType = ty;
            this.InitType();
        }
        public void InitType()
        {
            switch (this.HisTBType)
            {
                case TBType.String:
                    this.Width = 100;
                    this.Height = 23;
                    break;
                case TBType.Money:
                    this.Width = 100;
                    this.Height = 23;
                    this.Text = "0.00";
                    this.TextAlignment = System.Windows.TextAlignment.Right;
                    break;
                case TBType.Int:
                    this.Width = 100;
                    this.Height = 23;
                    this.Text = "0";
                    this.TextAlignment = System.Windows.TextAlignment.Right;
                    break;
                case TBType.Float:
                    this.Width = 100;
                    this.Height = 23;
                    this.Text = "0";
                    this.TextAlignment = System.Windows.TextAlignment.Right;
                    break;
                default:
                    break;
            }
        }
        protected override void OnDrop(DragEventArgs e)
        {
            MessageBox.Show(e.ToString());
            base.OnDrop(e);
        }

        #region 焦点事件
        //protected override void OnGotFocus(RoutedEventArgs e)
        //{
        //    this.BorderBrush.Opacity = 4;
        //    base.OnGotFocus(e);
        //}
        //protected override void OnLostFocus(RoutedEventArgs e)
        //{
        //    this.BorderBrush.Opacity = 0.5;
        //    base.OnLostFocus(e);
        //}
        #endregion 焦点事件

        #region 移动事件
        bool trackingMouseMove = false;
        Point mousePosition;
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            mousePosition = e.GetPosition(null);
            trackingMouseMove = true;
            base.OnMouseLeftButtonDown(e);
        }
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.X = (double)this.GetValue(Canvas.LeftProperty);
            this.Y = (double)this.GetValue(Canvas.TopProperty);

            trackingMouseMove = false;
            base.OnMouseLeftButtonUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
          //  FrameworkElement element = sender as FrameworkElement;
            //if (trackingMouseMove)
            //{
            //    double moveH = e.GetPosition(null).Y - mousePosition.Y;
            //    double moveW = e.GetPosition(null).X - mousePosition.X;

            //    double newTop = moveH + (double)this.GetValue(Canvas.TopProperty);
            //    double newLeft = moveW + (double)this.GetValue(Canvas.LeftProperty);
            //    this.SetValue(Canvas.TopProperty, newTop);
            //    this.SetValue(Canvas.LeftProperty, newLeft);
            //    mousePosition = e.GetPosition(null);
            //}

            this.X = (double)this.GetValue(Canvas.LeftProperty);
            this.Y = (double)this.GetValue(Canvas.TopProperty);

            base.OnMouseMove(e);
            trackingMouseMove = false;
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.V)
                {
                    base.OnKeyUp(e);
                    return;
                }
                this.DoCopy();

            }
            base.OnKeyUp(e);
        }
        bool isCopy = false;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.Handled = true;
            double x = (double)this.GetValue(Canvas.LeftProperty);
            double y = (double)this.GetValue(Canvas.TopProperty);

            switch (e.Key)
            {
                case Key.Up:
                    if (Keyboard.Modifiers == ModifierKeys.Shift && this.HisDataType ==DataType.AppString )
                    {
                        if (this.Height > 5)
                            this.Height = this.Height - 1;
                    } 
                    else
                    {
                        this.SetValue(Canvas.TopProperty, y - 1);
                    }
                    break;
                case Key.Down:
                    if (Keyboard.Modifiers == ModifierKeys.Shift && this.HisDataType == DataType.AppString)
                    {
                        if (this.Height > 5)
                            this.Height = this.Height + 1;
                    }
                    else
                    {
                        this.SetValue(Canvas.TopProperty, y + 1);
                    }
                    break;
                case Key.Left:
                    if (Keyboard.Modifiers == ModifierKeys.Shift  )
                    {
                        if (this.Width > 5)
                            this.Width = this.Width - 1;
                    }
                    else
                    {
                        this.SetValue(Canvas.LeftProperty, x - 1);
                    }
                    break;
                case Key.Right:
                    if (Keyboard.Modifiers == ModifierKeys.Shift)
                    {
                        this.Width = this.Width + 1;
                    }
                    else
                    {
                        this.SetValue(Canvas.LeftProperty, x + 1);
                    }
                    break;
                case Key.Delete:
                    if (this.Name.Contains("TB") == false)
                    {
                        if (MessageBox.Show("您确定要删除吗？",
                            "删除提示", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                            return;
                    }
                    Canvas c = this.Parent as Canvas;
                    c.Children.Remove(this);
                    return;
                case Key.C:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                        isCopy = true;
                    break;
                case Key.V:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        this.DoCopy();
                    }
                    break;
                //case Key.Add:
                //    this.Width = this.Width + 1;
                //    break;
                //case Key.Subtract:
                //    this.Width = this.Width - 1;
                //    break;
                default:
                    break;
            }

            this.X  = (double)this.GetValue(Canvas.LeftProperty);
            this.Y  = (double)this.GetValue(Canvas.TopProperty);

            base.OnKeyDown(e);
        }
        public void DoCopy()
        {
            BPTextBox tb = new BPTextBox(this.HisTBType);
            tb.Cursor = Cursors.Hand;
            tb.HisTBType = this.HisTBType;
            tb.InitType();
            tb.Text = this.Text;
            tb.Width = this.Width;
            tb.Height = this.Height;

            tb.SetValue(Canvas.LeftProperty, (double)this.GetValue(Canvas.LeftProperty) + 15);
            tb.SetValue(Canvas.TopProperty, (double)this.GetValue(Canvas.TopProperty) + 15);
            Canvas s1c = this.Parent as Canvas;
            try
            {
                s1c.Children.Add(tb);
            }
            catch
            {
                s1c.Children.Remove(tb);
            }
        }
        #endregion 移动事件
    }
}
