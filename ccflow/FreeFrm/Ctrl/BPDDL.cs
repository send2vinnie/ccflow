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
    public class BPDDL : System.Windows.Controls.ComboBox
    {
        #region bind Enum
        public string UIBindKey = "";
        public string HisLGType = LGType.Enum;
        public string HisDataType
        {
            get
            {
                if (this.HisLGType == LGType.Enum)
                    return DataType.AppInt;
                else
                    return DataType.AppString;
            }
        }
        /// <summary>
        /// BPDDL
        /// </summary>
        public BPDDL()
        {
            this.Name = "DDL" + DateTime.Now.ToString("yyMMddhhmmss");
        }
        #endregion bind Enum

        #region bind Enum
        /// <summary>
        /// enumID
        /// </summary>
        /// <param name="enumID"></param>
        public void BindEnum(string enumID)
        {
            this.UIBindKey = enumID;
            this.HisLGType = LGType.Enum;
            string sql = "SELECT IntKey as No, Lab as Name FROM Sys_Enum WHERE EnumKey='" + enumID + "'";
            this.BindSQL(sql);
        }
        /// <summary>
        /// ensName
        /// </summary>
        /// <param name="ensName"></param>
        public void BindEns(string ensName)
        {
            this.UIBindKey = ensName;
            this.HisLGType = LGType.FK;
            FF.FreeFrmSoapClient da = new FF.FreeFrmSoapClient();
            da.RequestSFTableAsync(ensName);
            da.RequestSFTableCompleted += new EventHandler<FF.RequestSFTableCompletedEventArgs>(da_RequestSFTableCompleted);
        }
        void da_RequestSFTableCompleted(object sender, FF.RequestSFTableCompletedEventArgs e)
        {
            Silverlight.DataSet ds = new Silverlight.DataSet();
            ds.FromXml(e.Result);
            this.Items.Clear();
            foreach (Silverlight.DataRow dr in ds.Tables[0].Rows)
            {
                ListBoxItem li = new ListBoxItem();
                li.Tag = dr["No"];
                li.Content = dr["Name"];
                this.Items.Add(li);
            }
            if (this.Items.Count != 0)
                this.SelectedIndex = 0;
        }
        #endregion bing Enum

        #region bind table
        public void BindSQL(string sql)
        {
            
            FF.FreeFrmSoapClient da = new FF.FreeFrmSoapClient();
            da.RunSQLReturnTableAsync(sql);
            da.RunSQLReturnTableCompleted += new EventHandler<FF.RunSQLReturnTableCompletedEventArgs>(da_RunSQLReturnTableCompleted);
        }
        void da_RunSQLReturnTableCompleted(object sender, FF.RunSQLReturnTableCompletedEventArgs e)
        {
            Silverlight.DataSet ds = new Silverlight.DataSet();
            ds.FromXml(e.Result);

            this.Items.Clear();
            foreach (Silverlight.DataRow dr in ds.Tables[0].Rows)
            {
                this.Items.Add(dr["No"] + ":" + dr["Name"]);
            }

            if (this.Items.Count != 0)
                this.SelectedIndex = 0;
        }
        #endregion bing Enum

        #region 焦点事件
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            this.BorderBrush.Opacity = 4;
            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            this.BorderBrush.Opacity = 0.5;
            base.OnLostFocus(e);
        }
        #endregion 焦点事件

        #region 移动事件
        //protected override void OnDrop(DragEventArgs e)
        //{
        //      trackingMouseMove = true;
        //    base.OnDrop(e);
        //}
        //bool trackingMouseMove = false;
        //Point mousePosition;
        //protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    mousePosition = e.GetPosition(null);
        //    trackingMouseMove = true;
        //    base.OnMouseLeftButtonDown(e);
        //}
        //protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        //{
        //    trackingMouseMove = false;
        //    base.OnMouseLeftButtonUp(e);
        //}
        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    //FrameworkElement element = sender as FrameworkElement;
        //    if (trackingMouseMove)
        //    {
        //        double moveH = e.GetPosition(null).Y - mousePosition.Y;
        //        double moveW = e.GetPosition(null).X - mousePosition.X;
        //        double newTop = moveH + (double)this.GetValue(Canvas.TopProperty);
        //        double newLeft = moveW + (double)this.GetValue(Canvas.LeftProperty);
        //        this.SetValue(Canvas.TopProperty, newTop);
        //        this.SetValue(Canvas.LeftProperty, newLeft);
        //        mousePosition = e.GetPosition(null);
        //    }
        //    base.OnMouseMove(e);
        //}
        bool isCopy = false;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.Handled = true;
            // 获取 textBox 对象的相对于 Canvas 的 x坐标 和 y坐标
            double x = (double)this.GetValue(Canvas.LeftProperty);
            double y = (double)this.GetValue(Canvas.TopProperty);
            

            // KeyEventArgs.Key - 与事件相关的键盘的按键 [System.Windows.Input.Key枚举]
            switch (e.Key)
            {
                // 按 Up 键后 textBox 对象向 上 移动 1 个像素
                // Up 键所对应的 e.PlatformKeyCode == 38 
                // 当获得的 e.Key == Key.Unknown 时，可以使用 e.PlatformKeyCode 来确定用户所按的键
               
                case Key.Up:
                    this.SetValue(Canvas.TopProperty, y - 1);
                    break;

                case Key.Down:
                    this.SetValue(Canvas.TopProperty, y + 1);
                    break;

                case Key.Left:
                    if (Keyboard.Modifiers == ModifierKeys.Shift)
                    {
                        if (this.Width > 10)
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
                    if (this.Name.Contains("_blank_") == false)
                    {
                        if (MessageBox.Show("您确定要删除吗？",
                            "删除提示", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                            return;
                    }
                    Canvas c = this.Parent as Canvas;
                    c.Children.Remove(this);
                    break;
                case Key.C:
                    break;
                case Key.V:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        BPDDL ctl = new BPDDL();
                        ctl.Cursor = Cursors.Hand;
                        ctl.SetValue(Canvas.LeftProperty, (double)this.GetValue(Canvas.LeftProperty) + 15);
                        ctl.SetValue(Canvas.TopProperty, (double)this.GetValue(Canvas.TopProperty) + 15);
                        ctl.Items.Add("ComboBox");
                        ctl.SelectedIndex = 0;

                        Canvas s1c = this.Parent as Canvas;
                        try
                        {
                            s1c.Children.Add(ctl);
                        }
                        catch
                        {
                            s1c.Children.Remove(ctl);
                        }
                    }
                    break;
                default:
                    break;
            }
            base.OnKeyDown(e);
        }
        #endregion 移动事件
    }
}
