using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Browser;
using System.Text;
using Demo.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using Microsoft.Expression.Interactivity;
using Microsoft.Expression.Interactivity.Layout;
using System.Windows.Media.Imaging; 

namespace Demo
{
    public partial class MainPage : UserControl
    {
        #region 全局变量
        bool b = false;//在主面板上判断当前鼠标的状态是否按下
        bool be = false;//在绿点上判断当前鼠标的状态是否按下
        bool bl = false;//判断LABEL当前鼠标的状态是否按下
        bool btxt = false;
        public string FK_MapData = "ND501"; // 
        string selectType =  Tools.Mouse;//当前工具选择类型 hand line1 line2 label txt cannel
        Line l;//当前线
        TextBlock tb;//当前标签
        TextBox txt;//当前文本框
        Ellipse e1;//选中线后出现的绿点1
        Ellipse e2;//选中线后出现的绿点2
        Ellipse eCurrent;//选中的绿点
        Grid g = new Grid();//遮罩图层
        private DateTime _lastTime;//获取双击label的时间间隔

        //删除框
        StackPanel spDel = new StackPanel();
        DropShadowEffect dse = new DropShadowEffect();
        TextBlock tbDel = new TextBlock();
        TextBlock tbDelLine = new TextBlock();
        TextBlock tbDelCannel = new TextBlock();
        UIElement ui;//要删除的对象
        #endregion

        public MainPage()
        {
            InitializeComponent();

            #region 构造
            spDel.Width = 80;
            spDel.Height = 26;
            spDel.Background = new SolidColorBrush(Colors.Brown);
            spDel.Opacity = 0.6;
            spDel.Orientation = Orientation.Horizontal;
            dse.Color = Colors.Gray;
            spDel.Effect = dse;
            tbDel.Text = "   删除 ";
            tbDel.Margin = new Thickness(0, 5, 0, 0);
            tbDelLine.Margin = new Thickness(0, 5, 0, 0);
            tbDelCannel.Margin = new Thickness(0, 5, 0, 0);
            tbDel.Foreground = new SolidColorBrush(Colors.White);
            tbDelLine.Foreground = tbDel.Foreground;
            tbDelCannel.Foreground = tbDel.Foreground;
            tbDel.Cursor = Cursors.Hand;
            tbDelLine.Text = "|";
            tbDelCannel.Text = " 取消 ";
            tbDelCannel.Cursor = Cursors.Hand;

            spDel.Children.Add(tbDel);
            spDel.Children.Add(tbDelLine);
            spDel.Children.Add(tbDelCannel);

            tbDel.MouseLeftButtonDown += new MouseButtonEventHandler(tbDelete_MouseLeftButtonDown);
            tbDelCannel.MouseLeftButtonDown += new MouseButtonEventHandler(tbDelete_MouseLeftButtonDown);

            this.SetSelectedTool( Tools.Mouse );
            e1 = new Ellipse();
            e1.Tag = "e1";
            e1.Cursor = Cursors.Hand;
            e1.MouseLeftButtonDown += new MouseButtonEventHandler(e_MouseLeftButtonDown);
            e1.Width = 9;
            e1.Height = 9;
            e1.Fill = new SolidColorBrush(Colors.Green);

            e2 = new Ellipse();
            e2.Tag = "e2";
            e2.Cursor = Cursors.Hand;
            e2.MouseLeftButtonDown += new MouseButtonEventHandler(e_MouseLeftButtonDown);
            e2.Width = 9;
            e2.Height = 9;
            e2.Fill = new SolidColorBrush(Colors.Green);
            #endregion
        }

        #region 拖拽线上的绿点时间集合
        void e_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            be = true;
            eCurrent = sender as Ellipse;
            eCurrent.Fill = new SolidColorBrush(Colors.Red);
        }
        #endregion

        //鼠标单击主面板事件
        private void canvasMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            b = true;
            delPoint();

            switch (selectType)
            {
                case Tools.Mouse:
                    return;
                case Tools.Line:  // 线.
                    l = new Line();
                    l.Cursor = Cursors.Hand;
                    l.StrokeThickness = 2;
                    l.Stroke = new SolidColorBrush(Colors.Black);
                    l.X1 = l.X2 = e.GetPosition(this.canvasMain).X;
                    l.Y1 = l.Y2 = e.GetPosition(this.canvasMain).Y;
                    this.canvasMain.Children.Add(l);
                    this.SetSelectedTool(Tools.Mouse);
                    break;
                case Tools.Label: /* 标签。 */
                    BPLabel lab = new BPLabel();
                      lab.FK_MapData = this.FK_MapData;
                      lab.Width = 100;
                      lab.Height = 23;
                      lab.Cursor = Cursors.Hand;
                      lab.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                      lab.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                      this.canvasMain.Children.Add(lab);

                     MouseDragElementBehavior DragBehavior = new MouseDragElementBehavior();
                     Interaction.GetBehaviors(lab).Add(DragBehavior);

                    this.SetSelectedTool(Tools.Mouse);
                    break;
                case Tools.TextBox:  // 文本框。
                    BPTextBox mytb = new BPTextBox();
                    mytb.FK_MapData = this.FK_MapData;
                    mytb.Width = 100;
                    mytb.Height = 23;
                    mytb.Cursor = Cursors.Hand;
                    mytb.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    mytb.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                    this.canvasMain.Children.Add(mytb);
                    this.SetSelectedTool(Tools.Mouse);
                    break;
                case Tools.DDL:  // 文本框。
                    BPDDL myddl = new BPDDL();
                    myddl.FK_MapData = this.FK_MapData;
                    myddl.Width = 100;
                    myddl.Height = 23;
                    myddl.Cursor = Cursors.Hand;
                    myddl.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    myddl.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                    myddl.Items.Add("ComboBox");
                    myddl.SelectedIndex = 0;
                      MouseDragElementBehavior mymde = new MouseDragElementBehavior();
                     Interaction.GetBehaviors(myddl).Add(mymde);
                    this.canvasMain.Children.Add(myddl);

                    this.SetSelectedTool(Tools.Mouse);
                    break;
                case Tools.CheckBox:
                    BPCheckBox cb = new BPCheckBox();
                    cb.FK_MapData = this.FK_MapData;
                    cb.Cursor = Cursors.Hand;
                    cb.Content = "new checkbox";
                    cb.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    cb.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                    this.canvasMain.Children.Add(cb);

                     //MouseDragElementBehavior add = new MouseDragElementBehavior();
                     //Interaction.GetBehaviors(cb).Add(add);

                    this.SetSelectedTool(Tools.Mouse);
                    break;
                case Tools.RBS:  // 复选框。
                    AddEnumGuide ae = new AddEnumGuide();
                    ae.Visibility = System.Windows.Visibility.Visible;
                    this.LayoutRoot.Children.Add(ae);
                    int addX = 0;
                    int addY = 0;
                    string gName ="s"+ DateTime.Now.ToString("hhmmss");
                    for (int i = 0; i < 3; i++)
                    {
                        BPRadioBtn rb = new BPRadioBtn();
                        rb.Content = "New RB " + i;
                        rb.Name = "RB_" + i + gName;
                        rb.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X + addX);
                        rb.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y + addY);
                        rb.Cursor = Cursors.Hand;
                        rb.GroupName = gName; ;
                        this.canvasMain.Children.Add(rb);
                        addX += 15;
                        addY += 15;
                        //MouseDragElementBehavior addd = new MouseDragElementBehavior();
                        //Interaction.GetBehaviors(rb).Add(addd);
                    }
                    this.SetSelectedTool(Tools.Mouse);
                   // this.caEnum.Visibility = System.Windows.Visibility.Visible;
                    //this.gVisable.Visibility = Visibility.Visible;
                    break;
                case Tools.Img: 
                    Image img = new Image();
                    //img.Source =new BitmapImage(new Uri("D:\\ccflow\\VisualFlow\\DataUser\\LogBig.png", UriKind.Relative));  //= @"\\Img\\LogBig.png";
                    BitmapImage png =new BitmapImage(new Uri("/Demo;component/Img/LogBig.png", UriKind.Relative));  //= @"\\Img\\LogBig.png";
                    img.Source =png; //=new BitmapImage(new Uri("/Demo;component/Img/LogBig.png", UriKind.Relative));  //= @"\\Img\\LogBig.png";

                    //img.Width = png.PixelWidth;
                    //img.Height = png.PixelHeight;

                    img.Width = 200;
                    img.Height = 200;

                    img.Cursor = Cursors.Hand;
                    img.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    img.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);

                    MouseDragElementBehavior mde = new MouseDragElementBehavior();
                    Interaction.GetBehaviors(img).Add(mde);

                    this.canvasMain.Children.Add(img);
                    this.SetSelectedTool(Tools.Mouse);
                    break;
                default:
                    MessageBox.Show("功能未完成:" + selectType, "请期待", MessageBoxButton.OK);
                    break;
            }

            #region 线二
            //if (selectType == "line2")
            //{
            //    l = new Line();
            //    l.Cursor = Cursors.Hand;
            //    l.StrokeThickness = 2;
            //    l.Stroke = new SolidColorBrush(Colors.Black);
            //    l.X1 = l.X2 = e.GetPosition(this.canvasMain).X;
            //    l.Y1 = l.Y2 = e.GetPosition(this.canvasMain).Y;
            //    this.canvasMain.Children.Add(l);

            //    l.MouseLeftButtonDown += (s, a) =>
            //    {
            //        if (selectType == "hand")
            //        {
            //            l = s as Line;
            //            a.Handled = true;
            //            if (!canvasMain.Children.Contains(e1) && !canvasMain.Children.Contains(e2))
            //            {
            //                e1.SetValue(Canvas.LeftProperty, l.X1 - 4);
            //                e1.SetValue(Canvas.TopProperty, l.Y1 - 4);
            //                e2.SetValue(Canvas.LeftProperty, l.X2 - 4);
            //                e2.SetValue(Canvas.TopProperty, l.Y2 - 4);
            //                this.canvasMain.Children.Add(e1);
            //                this.canvasMain.Children.Add(e2);
            //            }
            //        }
            //    };
            //    l.MouseRightButtonDown += (s, a) =>
            //        {
            //            a.Handled = true;
            //            if (selectType == "hand")
            //            {
            //                if (!this.canvasMain.Children.Contains(spDel))
            //                {
            //                    this.canvasMain.Children.Add(spDel);
            //                    spDel.SetValue(Canvas.LeftProperty, a.GetPosition(this.canvasMain).X);
            //                    spDel.SetValue(Canvas.TopProperty, a.GetPosition(this.canvasMain).Y);
            //                    ui = s as Line;
            //                }
            //            }
            //        };
            //}
            #endregion
        }

        //鼠标松开主面板事件
        private void canvasMain_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            b = false;
            be = false;
            bl = false;
            btxt = false;
            this.SetSelectedTool(Tools.Mouse);
            if (eCurrent != null)
                eCurrent.Fill = new SolidColorBrush(Colors.Green);
        }

        //鼠标在主面板上移动事件
        private void canvasMain_MouseMove(object sender, MouseEventArgs e)
        {
            #region 画线线
            if (b)
            {
                #region 线一
                if (selectType == "line1")
                {
                    l.X2 = e.GetPosition(this.canvasMain).X;
                    l.Y2 = e.GetPosition(this.canvasMain).Y;
                }
                #endregion

                #region 线二
                else if (selectType == "line2")
                {
                    double x = e.GetPosition(this.canvasMain).X - l.X1;
                    double y = e.GetPosition(this.canvasMain).Y - l.Y1;

                    if (Math.Abs(x) > Math.Abs(y))
                    {
                        l.X2 = e.GetPosition(this.canvasMain).X;
                        l.Y2 = l.Y1;
                    }
                    else
                    {
                        l.X2 = l.X1;
                        l.Y2 = e.GetPosition(this.canvasMain).Y;
                    }
                }
                #endregion
            }
            #endregion

            if (selectType == Tools.Mouse)
            {
                #region 改变线的长度
                if (be)
                {
                    if (eCurrent.Tag.ToString() == "e1")
                    {
                        double x = e.GetPosition(this.canvasMain).X - l.X2;
                        double y = e.GetPosition(this.canvasMain).Y - l.Y2;
                        if (Math.Abs(x) > Math.Abs(y))
                        {
                            l.X1 = e.GetPosition(this.canvasMain).X;
                            l.Y1 = l.Y2;
                            eCurrent.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X - 4);
                            eCurrent.SetValue(Canvas.TopProperty, l.Y2 - 4);
                        }
                        else
                        {
                            l.X1 = l.X2;
                            l.Y1 = e.GetPosition(this.canvasMain).Y;
                            eCurrent.SetValue(Canvas.LeftProperty, l.X2 - 4);
                            eCurrent.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y - 4);
                        }
                    }
                    else //if (eCurrent.Tag.ToString() == "e2")
                    {
                        double x = e.GetPosition(this.canvasMain).X - l.X1;
                        double y = e.GetPosition(this.canvasMain).Y - l.Y1;
                        if (Math.Abs(x) > Math.Abs(y))
                        {
                            l.X2 = e.GetPosition(this.canvasMain).X;
                            l.Y2 = l.Y1;
                            eCurrent.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X - 4);
                            eCurrent.SetValue(Canvas.TopProperty, l.Y1 - 4);
                        }
                        else
                        {
                            l.X2 = l.X1;
                            l.Y2 = e.GetPosition(this.canvasMain).Y;
                            eCurrent.SetValue(Canvas.LeftProperty, l.X1 - 4);
                            eCurrent.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y - 4);
                        }
                    }
                }
                #endregion

                #region 拖拉label
                if (bl)
                {
                    tb.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    tb.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                }
                #endregion

                #region 拖拉txt
                if (btxt)
                {
                    txt.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    txt.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                }
                #endregion
            }
        }

        //工具选择触发事件
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            string id = tb.Name.Replace("Btn_", "");
            selectType = id;

            switch (id)
            {
                case Tools.RBS:
                    break;
                default:
                    break;
            }

 

            this.Btn_Mouse.Foreground = new SolidColorBrush(Colors.White);
            this.Btn_Line.Foreground = new SolidColorBrush(Colors.White);
            this.Btn_Dtl.Foreground = new SolidColorBrush(Colors.White);
            this.Btn_Img.Foreground = new SolidColorBrush(Colors.White);

            this.Btn_Label.Foreground = new SolidColorBrush(Colors.White);
            this.Btn_CheckBox.Foreground = new SolidColorBrush(Colors.White);
            this.Btn_DDL.Foreground = new SolidColorBrush(Colors.White);
            this.Btn_RBS.Foreground = new SolidColorBrush(Colors.White);
            this.Btn_TextBox.Foreground = new SolidColorBrush(Colors.White);
            this.Btn_M2M.Foreground = new SolidColorBrush(Colors.White);

            //设置按钮状态。
            this.SetSelectedTool(id);
        }
        /// <summary>
        /// 设置选择的tools.
        /// </summary>
        /// <param name="id"></param>
        public void SetSelectedTool(string id)
        {
            this.selectType = id;
            switch (id)
            {
                case Demo.Tools.Mouse:
                    this.Btn_Mouse.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Demo.Tools.Dtl:
                    this.Btn_Dtl.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Demo.Tools.Img:
                    this.Btn_Img.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Demo.Tools.Label:
                    this.Btn_Label.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Demo.Tools.Line:
                    this.Btn_Line.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Demo.Tools.RBS: 
                    this.Btn_RBS.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Demo.Tools.DDL:
                    this.Btn_DDL.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Demo.Tools.CheckBox:
                    this.Btn_CheckBox.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Demo.Tools.M2M:
                    this.Btn_M2M.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Demo.Tools.TextBox:
                    this.Btn_TextBox.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                default:
                    MessageBox.Show("****** 功能未完成:" + selectType);
                    this.SetSelectedTool(Tools.Mouse);
                    break;
            }
        }

        //标签赋值 X 事件
        private void tbClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvasWin.Visibility = Visibility.Collapsed;
            gVisable.Visibility = Visibility.Collapsed;
        }

        //标签赋值确定事件
        private void tbSure_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tb.Text = txtLabel.Text;
            tb.FontSize = double.Parse(cbSize.SelectionBoxItem.ToString());
            if (cbWight.IsChecked == true)
                tb.FontWeight = FontWeights.Bold;
            else
                tb.FontWeight = FontWeights.Normal;
            canvasWin.Visibility = Visibility.Collapsed;
            gVisable.Visibility = Visibility.Collapsed;
        }

        //文本框长度赋值事件
        private void tbSureTxt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (txtWidth.Text == "" || txtHeight.Text == "")
            {
                MessageBox.Show("请输入要设置的长度！");
                return;
            }

            double i = 0;
            double j = 0;
            if (!double.TryParse(txtWidth.Text, out i))
            {
                MessageBox.Show("请输入整型数字！");
                return;
            }
            if (!double.TryParse(txtHeight.Text, out j))
            {
                MessageBox.Show("请输入整型数字！");
                return;
            }
            if (i > 1000 || j > 500)
            {
                MessageBox.Show("请输入1000之内的数字！");
                return;
            }
            txt.Width = double.Parse(txtWidth.Text);
            txt.Height = double.Parse(txtHeight.Text);

            canvasWinTxt.Visibility = Visibility.Collapsed;
            gVisable.Visibility = Visibility.Collapsed;

        }
        //文本框 X 事件
        private void tbCloseTxt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvasWinTxt.Visibility = Visibility.Collapsed;
            gVisable.Visibility = Visibility.Collapsed;
        }

        //删除操作
        private void tbDelete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            if (tb.Text.Trim() == "删除")
            {
                if (this.canvasMain.Children.Contains(ui))
                    this.canvasMain.Children.Remove(ui);
            }
            if (this.canvasMain.Children.Contains(spDel))
                this.canvasMain.Children.Remove(spDel);
        }

        #region 私有方法

     
        //删除主面板上线上的点
        private void delPoint()
        {
            if (canvasMain.Children.Contains(e1))
                this.canvasMain.Children.Remove(e1);


            if (canvasMain.Children.Contains(e2))
                this.canvasMain.Children.Remove(e2);
        }
        #endregion

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    int il = 0;
        //    int ll = 0;
        //    foreach (UIElement u in canvasMain.Children)
        //    {
        //        if (u is Line)
        //        {
        //            il++;
        //        }
        //        else if (u is TextBlock)
        //        {
        //            ll++;
        //        }
        //    }

        //    MessageBox.Show(il.ToString() + "---------" + ll.ToString());
        //    //Test.SYN_ALTERFILER
        //}


        private void tbTool_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            tb.Opacity = 1;
        }

        private void tbTool_MouseMove(object sender, MouseEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            tb.Opacity = 0.6;
        }
       
        private void Btn_Enum_Cancel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvasWinTxt.Visibility = Visibility.Collapsed;
            gVisable.Visibility = Visibility.Collapsed;
        }

        private void Btn_Enum_OK_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //canvasWinTxt.Visibility = Visibility.Collapsed;
            //gVisable.Visibility = Visibility.Collapsed;
            AddEnum ae = new AddEnum();
            ae.Visibility= System.Windows.Visibility.Visible;
            this.LayoutRoot.Children.Add(ae);
            ae.Visibility = System.Windows.Visibility.Collapsed;
            MessageBox.Show("ddd here.");

        }
    }
}
