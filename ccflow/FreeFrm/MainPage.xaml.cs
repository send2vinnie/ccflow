using System;
using System.Collections.Generic;
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
using System.Windows.Interactivity;
using Microsoft.Expression.Interactivity;
using Microsoft.Expression.Interactivity.Layout;
using System.Windows.Media.Imaging;
using Silverlight;
using BP.En;

namespace FreeFrm
{
    public partial class MainPage : UserControl
    {

#warning 没有实现的功能
        /**
         * 1, 圈选.
         * 2, 选择的效果.
         * 3, 拖拉复制 . 
         * 4, DDL 不能拖动移动。
         * 5, label  键盘事件。
         * */

        #region zhoupeng add 全局变量
        public SelectTB winSelectTB = new SelectTB();
        public SelectDDLTable winSelectDDL = new SelectDDLTable();
        public SelectRB winSelectRB = new SelectRB();
        public double X = 0;
        public double Y = 0;
        public bool IsRB = false;
        #endregion 全局变量

        #region 全局变量
        bool isMouseDown = false; //在主面板上判断当前鼠标的状态是否按下.
        bool be = false;//在绿点上判断当前鼠标的状态是否按下.
        bool bl = false;//判断LABEL当前鼠标的状态是否按下
        bool btxt = false;
        string selectType = Tools.Mouse;//当前工具选择类型 hand line1 line2 label txt cannel

        BPCheckBox currCB; //当前 label
        BPRadioBtn currRB; //当前 label
        Label currCheckBoxLabel; //当前 label
        BPLabel  currLab; //当前 label
        BPLink currLink;  //当前 linke
        Line currLine;  //当前 Line
        Image currImg;   //当前Img
        BPTextBox currTB;    //当前textbox
        BPDatePicker currDP;    //当前textbox
        BPDDL currDDL;   //当前标签

        Ellipse e1;//选中线后出现的绿点1
        Ellipse e2;//选中线后出现的绿点2
        Ellipse eCurrent;//选中的绿点
        Grid g = new Grid();//遮罩图层
        private DateTime _lastTime;//获取双击label的时间间隔

        UIElement ui;//要删除的对象
        #endregion

        void WindowDilag_Closed(object sender, EventArgs e)
        {
            ChildWindow c = sender as ChildWindow;
            if (c.DialogResult == false)
                return;

            if (c.Name == "winSelectTB")
            {
                TBType tp = TBType.String;
                if (winSelectTB.RB_String.IsChecked == true)
                    tp = TBType.String;
                if (winSelectTB.RB_Money.IsChecked == true)
                    tp = TBType.Money;
                if (winSelectTB.RB_Int.IsChecked == true)
                    tp = TBType.Int;
                if (winSelectTB.RB_Float.IsChecked == true)
                    tp = TBType.Float;

                if (winSelectTB.RB_DataTime.IsChecked == true)
                    tp = TBType.DateTime;
                if (winSelectTB.RB_Data.IsChecked == true)
                    tp = TBType.Date;

                if (tp == TBType.DateTime || tp == TBType.Date)
                {
                    BPDatePicker dp = new BPDatePicker();
                    //   dp.Name = "DP"+date;
                    dp.Cursor = Cursors.Hand;
                    dp.SetValue(Canvas.LeftProperty, X);
                    dp.SetValue(Canvas.TopProperty, Y);
                    MouseDragElementBehavior mymdeEdp = new MouseDragElementBehavior();
                    Interaction.GetBehaviors(dp).Add(mymdeEdp);
                    this.canvasMain.Children.Add(dp);

                    dp.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                    dp.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                }
                else
                {
                    BPTextBox mytb = new BPTextBox(tp);
                    mytb.Cursor = Cursors.Hand;
                    mytb.SetValue(Canvas.LeftProperty, X);
                    mytb.SetValue(Canvas.TopProperty, Y);
                    this.canvasMain.Children.Add(mytb);
                    MouseDragElementBehavior mymdeEdp = new MouseDragElementBehavior();
                    Interaction.GetBehaviors(mytb).Add(mymdeEdp);

                    mytb.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                    mytb.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                }
            }
            if (c.Name == "winSelectDDL")
            {
                if (this.winSelectDDL.listBox1.SelectedIndex < 0)
                    return;

                ListBoxItem mylbi = this.winSelectDDL.listBox1.SelectedItem as ListBoxItem;
                string enKey = mylbi.Content.ToString();
                enKey = enKey.Substring(0, enKey.IndexOf(':'));

                BPDDL myddl = new BPDDL();
                myddl.Width = 100;
                myddl.Height = 23;
                myddl.Cursor = Cursors.Hand;
                myddl.SetValue(Canvas.LeftProperty, X);
                myddl.SetValue(Canvas.TopProperty, Y);
                myddl.BindEns(enKey);
                this.canvasMain.Children.Add(myddl);
                MouseDragElementBehavior mymdeE = new MouseDragElementBehavior();
                Interaction.GetBehaviors(myddl).Add(mymdeE);

                myddl.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                myddl.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);

            }
            if (c.Name == "winSelectRB")
            {
                if (this.winSelectRB.listBox1.SelectedIndex < 0)
                    return;

                ListBoxItem lbi = this.winSelectRB.listBox1.SelectedItem as ListBoxItem;
                string enumKey = lbi.Content.ToString();
                enumKey = enumKey.Substring(0, enumKey.IndexOf(':'));

                string cfgKeys = lbi.Tag as string;
                string[] strs = cfgKeys.Split('@');
                if (IsRB)
                {
                    int addX = 0;
                    int addY = 0;
                    string gName = "RB" + DateTime.Now.ToString("yyMMddhhmmss");
                    foreach (string str in strs)
                    {
                        if (string.IsNullOrEmpty(str))
                            continue;

                        string[] mykey = str.Split('=');
                        BPRadioBtn rb = new BPRadioBtn();
                        rb.Content = mykey[1];
                        rb.Tag = mykey[0];
                        rb.Name = gName + "_" + mykey[0];
                        rb.UIBindKey = enumKey;
                        rb.SetValue(Canvas.LeftProperty, X + addX);
                        rb.SetValue(Canvas.TopProperty, Y + addY);
                        rb.Cursor = Cursors.Hand;
                        rb.GroupName = gName;
                        this.canvasMain.Children.Add(rb);
                        addY += 16;

                        rb.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                        rb.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);

                    }
                }
                else
                {
                    /* 如果是ddl.*/
                    BPDDL myddlEnum = new BPDDL();
                    myddlEnum.Width = 100;
                    myddlEnum.Height = 23;
                    myddlEnum.Cursor = Cursors.Hand;
                    myddlEnum.SetValue(Canvas.LeftProperty, X);
                    myddlEnum.SetValue(Canvas.TopProperty, Y);
                    myddlEnum.BindEnum(enumKey);
                    MouseDragElementBehavior mymdeEE = new MouseDragElementBehavior();
                    Interaction.GetBehaviors(myddlEnum).Add(mymdeEE);
                    this.canvasMain.Children.Add(myddlEnum);

                    myddlEnum.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                    myddlEnum.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                }
            }
        }
        StackPanel rightMenu = new StackPanel();
        public MainPage()
        {

            InitializeComponent();

            winSelectTB.Name = "winSelectTB";
            winSelectDDL.Name = "winSelectDDL";
            winSelectRB.Name = "winSelectRB";
            winSelectTB.Closed += new EventHandler(WindowDilag_Closed);
            winSelectDDL.Closed += new EventHandler(WindowDilag_Closed);
            winSelectRB.Closed += new EventHandler(WindowDilag_Closed);

            #region 构造


            #region 右键菜单
            DropShadowEffect dse = new DropShadowEffect();
            TextBlock rightMenu_Btn_Del = new TextBlock();
            rightMenu_Btn_Del.Name = "rightMenu_Btn_Del";

            TextBlock rightMenu_Btn_Spt = new TextBlock();
            rightMenu_Btn_Spt.Name = "rightMenu_Btn_Spt";

            TextBlock rightMenu_Btn_Cannel = new TextBlock();
            rightMenu_Btn_Cannel.Name = "rightMenu_Btn_Cannel";

            TextBlock rightMenu_Btn_Spt1 = new TextBlock();
            rightMenu_Btn_Spt1.Name = "rightMenu_Btn_Spt1";

            TextBlock rightMenu_Btn_Edit = new TextBlock();
            rightMenu_Btn_Edit.Name = "rightMenu_Btn_Edit";

            rightMenu.Width = 150;
            rightMenu.Height = 26;
            rightMenu.Background = new SolidColorBrush(Colors.Brown);
            rightMenu.Opacity = 0.6;
            rightMenu.Orientation = Orientation.Horizontal;
            dse.Color = Colors.Gray;
            rightMenu.Effect = dse;
            rightMenu_Btn_Del.Text = " Delete";

            rightMenu_Btn_Del.Margin = new Thickness(0, 5, 0, 0);
            rightMenu_Btn_Cannel.Margin = new Thickness(0, 5, 0, 0);
            rightMenu_Btn_Edit.Margin = new Thickness(0, 5, 0, 0);

            rightMenu_Btn_Del.Foreground = new SolidColorBrush(Colors.White);
            rightMenu_Btn_Del.Cursor = Cursors.Hand;
            rightMenu_Btn_Spt.Text = " | ";
            rightMenu_Btn_Cannel.Text = "Cancel";
            rightMenu_Btn_Cannel.Foreground = new SolidColorBrush(Colors.White);
            rightMenu_Btn_Cannel.Cursor = Cursors.Hand;
            rightMenu_Btn_Spt1.Text = " | ";
            rightMenu_Btn_Edit.Text = "Edit";
            rightMenu_Btn_Edit.Foreground = new SolidColorBrush(Colors.White);
            rightMenu_Btn_Edit.Cursor = Cursors.Hand;

            //rightMenu_Btn_Spt1.Text = " | ";
            //rightMenu_Btn_Edit.Text = "Edit CheckBox";
            //rightMenu_Btn_Edit.Foreground = new SolidColorBrush(Colors.White);
            //rightMenu_Btn_Edit.Cursor = Cursors.Hand;

            rightMenu.Children.Add(rightMenu_Btn_Del);
            rightMenu.Children.Add(rightMenu_Btn_Spt);
            rightMenu.Children.Add(rightMenu_Btn_Cannel);
            rightMenu.Children.Add(rightMenu_Btn_Spt1);
            rightMenu.Children.Add(rightMenu_Btn_Edit);

            rightMenu_Btn_Del.MouseLeftButtonDown += new MouseButtonEventHandler(UI_rightMenu_Btn_MouseLeftButtonDown);
            rightMenu_Btn_Cannel.MouseLeftButtonDown += new MouseButtonEventHandler(UI_rightMenu_Btn_MouseLeftButtonDown);
            rightMenu_Btn_Edit.MouseLeftButtonDown += new MouseButtonEventHandler(UI_rightMenu_Btn_MouseLeftButtonDown);
            rightMenu.Visibility = System.Windows.Visibility.Collapsed;
            this.canvasMain.Children.Add(rightMenu);
            #endregion 右键菜单


            this.SetSelectedTool(Tools.Mouse);
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


            this.BindFrm();
        }
        public DataSet FrmDS = null;
        public void BindFrm()
        {
            this.canvasMain.Children.Clear();
            FF.FreeFrmSoapClient da = new FF.FreeFrmSoapClient();
            da.GenerFrmAsync(Glo.FK_MapData);
           // da.GenerFrmAsync();
            da.GenerFrmCompleted += new EventHandler<FF.GenerFrmCompletedEventArgs>(da_BindFrmCompleted);
        }
        void da_BindFrmCompleted(object sender, FF.GenerFrmCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);
            this.FrmDS = ds;
            foreach (DataTable dt in ds.Tables)
            {
                switch (dt.TableName)
                {
                    case "Sys_FrmLine":
                        foreach (DataRow dr in dt.Rows)
                        {
                            Line line = new Line();
                            line.Name = dr["MyPK"];
                            line.X1 = double.Parse(dr["X1"]);
                            line.X2 = double.Parse(dr["X2"]);
                            line.Y1 = double.Parse(dr["Y1"]);
                            line.Y2 = double.Parse(dr["Y2"]);
                            line.StrokeThickness = double.Parse(dr["BorderWidth"]);
                            line.Cursor = Cursors.Hand;

                            string color=dr["BorderColor"];
                            if (string.IsNullOrEmpty(color))
                                color = "#000000";

                            ColorConverter cc = new ColorConverter();
                            line.Stroke = (SolidColorBrush)cc.Convert(color, null, null, null);

                            this.canvasMain.Children.Add(line);

                            MouseDragElementBehavior mdeLine = new MouseDragElementBehavior();
                            Interaction.GetBehaviors(line).Add(mdeLine);

                            line.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                            line.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                        }
                        continue;
                    case "Sys_FrmLab":
                        foreach (DataRow dr in dt.Rows)
                        {
                            BPLabel lab = new BPLabel();
                            lab.Name = dr["MyPK"];
                            lab.Content = dr["Text"];
                            lab.FontSize = double.Parse(dr["FontSize"]);
                            lab.Cursor = Cursors.Hand;
                            lab.SetValue(Canvas.LeftProperty, double.Parse(dr["X"]));
                            lab.SetValue(Canvas.TopProperty, double.Parse(dr["Y"]));

                            string color = dr["FontColor"];
                            if (string.IsNullOrEmpty(color))
                                color = "#000000";
                            ColorConverter cc = new ColorConverter();
                            lab.Foreground = (SolidColorBrush)cc.Convert(color, null, null, null);


                            this.canvasMain.Children.Add(lab);

                            MouseDragElementBehavior DragBehavior = new MouseDragElementBehavior();
                            Interaction.GetBehaviors(lab).Add(DragBehavior);

                            lab.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                            lab.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                        }
                        continue;
                    case "Sys_FrmLink":
                        foreach (DataRow dr in dt.Rows)
                        {
                            BPLink link = new BPLink();
                            link.Name = dr["MyPK"];
                            link.Content = dr["Text"];
                            link.FontSize = double.Parse(dr["FontSize"]);
                            link.Cursor = Cursors.Hand;
                            link.SetValue(Canvas.LeftProperty, double.Parse(dr["X"]));
                            link.SetValue(Canvas.TopProperty, double.Parse(dr["Y"]));

                            string color = dr["FontColor"];
                            if (string.IsNullOrEmpty(color))
                                color = "#000000";
                            ColorConverter cc = new ColorConverter();
                            link.Foreground = (SolidColorBrush)cc.Convert(color, null, null, null);

                            Label cbLab = new Label();
                            cbLab.Name = "LinkLab" + link.Name;
                            cbLab.Content = dr["Text"];
                            link.Content = cbLab;
                            this.canvasMain.Children.Add(link);

                            cbLab.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                            link.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);

                            //MouseDragElementBehavior DragBehavior = new MouseDragElementBehavior();
                            //Interaction.GetBehaviors(lab).Add(DragBehavior);
                        }
                        continue;
                    case "Sys_FrmImg":
                        foreach (DataRow dr in dt.Rows)
                        {
                            Image img = new Image();
                            img.Name = dr["MyPK"];

                            img.Width = double.Parse(dr["W"].ToString());
                            img.Height = double.Parse(dr["H"].ToString());

                            img.Cursor = Cursors.Hand;
                            img.SetValue(Canvas.LeftProperty, double.Parse(dr["X"].ToString()));
                            img.SetValue(Canvas.TopProperty, double.Parse(dr["Y"].ToString()));

                            BitmapImage png = new BitmapImage(new Uri(dr["URL"], UriKind.Relative));  //= @"\\Img\\LogBig.png";
                            img.Source = png;
                            MouseDragElementBehavior mde = new MouseDragElementBehavior();
                            Interaction.GetBehaviors(img).Add(mde);
                            this.canvasMain.Children.Add(img);


                            img.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                            img.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                        }
                        continue;
                    case "Sys_FrmRB":
                        DataTable dtRB = ds.Tables["Sys_FrmRB"];
                        foreach (DataRow dr in dtRB.Rows)
                        {
                            BPRadioBtn btn = new BPRadioBtn();
                            btn.Name = dr["MyPK"];
                            btn.GroupName = dr["KeyOfEn"];
                            btn.Content = dr["Lab"];
                            btn.UIBindKey = dr["EnumKey"];
                            btn.SetValue(Canvas.LeftProperty, double.Parse(dr["X"].ToString()));
                            btn.SetValue(Canvas.TopProperty, double.Parse(dr["Y"].ToString()));

                            MouseDragElementBehavior mde = new MouseDragElementBehavior();
                            Interaction.GetBehaviors(btn).Add(mde);
                            this.canvasMain.Children.Add(btn);


                            btn.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                            btn.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);

                        }
                        continue;
                    case "Sys_MapAttr":
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["UIVisible"] == "0")
                                continue;
                            string myPk = dr["MyPK"];
                            string FK_MapData = dr["FK_MapData"];
                            string keyOfEn = dr["KeyOfEn"];
                            string name = dr["Name"];
                            string defVal = dr["DefVal"];
                            string UIContralType = dr["UIContralType"];
                            string MyDataType = dr["MyDataType"];
                            string lgType = dr["LGType"];
                            double X = double.Parse(dr["X"]);
                            double Y = double.Parse(dr["Y"]);
                            if (X == 0)
                                X = 100;
                            if (Y == 0)
                                Y = 100;
                         
                            string UIBindKey = dr["UIBindKey"];
                            switch (UIContralType)
                            {
                                case CtrlType.TextBox:
                                    if (MyDataType == DataType.AppDate || MyDataType == DataType.AppDateTime)
                                    {
                                        BPDatePicker dp = new BPDatePicker();
                                        dp.Name = keyOfEn;
                                        dp.SetValue(Canvas.LeftProperty, X);
                                        dp.SetValue(Canvas.TopProperty, Y);
                                        this.canvasMain.Children.Add(dp);
                                        dp.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                                        dp.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                                    }
                                    else
                                    {
                                        TBType tp = TBType.String;
                                        switch (MyDataType)
                                        {
                                            case DataType.AppInt:
                                                tp = TBType.Int;
                                                break;
                                            case DataType.AppFloat:
                                            case DataType.AppDouble:
                                                tp = TBType.Float;
                                                break;
                                            case DataType.AppMoney:
                                                tp = TBType.Money;
                                                break;
                                            case DataType.AppString:
                                            default:
                                                tp = TBType.String;
                                                break;
                                        }
                                        BPTextBox tb = new BPTextBox(tp);
                                        tb.Name = keyOfEn;
                                        tb.SetValue(Canvas.LeftProperty, X);
                                        tb.SetValue(Canvas.TopProperty, Y);
                                        tb.Width = double.Parse(dr["UIWidth"]);

                                        this.canvasMain.Children.Add(tb);
                                        MouseDragElementBehavior mymdee = new MouseDragElementBehavior();
                                        Interaction.GetBehaviors(tb).Add(mymdee);

                                        tb.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                                        tb.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);

                                    }
                                    break;
                                case CtrlType.DDL:
                                    BPDDL ddl = new BPDDL();
                                    ddl.Name = keyOfEn;
                                    ddl.HisLGType = lgType;
                                    ddl.Width = double.Parse(dr["UIWidth"]);
                                    ddl.UIBindKey = UIBindKey;
                                    ddl.HisLGType = lgType;
                                    if (lgType == LGType.Enum)
                                    {
                                        ddl.BindEnum(UIBindKey);
                                    }
                                    else
                                    {
                                        ddl.BindEns(UIBindKey);
                                    }
                                    ddl.SetValue(Canvas.LeftProperty, X);
                                    ddl.SetValue(Canvas.TopProperty, Y);

                                    MouseDragElementBehavior mde = new MouseDragElementBehavior();
                                    Interaction.GetBehaviors(ddl).Add(mde);
                                    this.canvasMain.Children.Add(ddl);

                                    ddl.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                                    ddl.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                                    break;
                                case CtrlType.CheckBox:
                                    BPCheckBox cb = new BPCheckBox();
                                    cb.Name = keyOfEn;
                                    cb.Content = name;

                                    Label cbLab = new Label();
                                    cbLab.Name = "CBLab" + cb.Name;
                                    cbLab.Content = name;
                                    cbLab.Tag = keyOfEn;
                                    cb.Content = cbLab;

                                    if (defVal == "1")
                                        cb.IsChecked = true;
                                    else
                                        cb.IsChecked = false;

                                    cb.SetValue(Canvas.LeftProperty, X);
                                    cb.SetValue(Canvas.TopProperty, Y);

                                    MouseDragElementBehavior mdeCB = new MouseDragElementBehavior();
                                    Interaction.GetBehaviors(cb).Add(mdeCB);
                                    this.canvasMain.Children.Add(cb);

                                    cbLab.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                                    cb.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                                    break;
                                case CtrlType.RB:
                                    break;
                                default:
                                    break;
                            }
                        }
                        continue;
                    default:
                        break;
                }
            }
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
            this.rightMenu.Visibility = System.Windows.Visibility.Collapsed;
            isMouseDown = true;
            delPoint();

            switch (selectType)
            {
                case Tools.Mouse:
                    return;
                case Tools.Line:  // 线.
                    currLine = new Line();
                    currLine.Name = "Line" + DateTime.Now.ToString("yyMMhhddhhss");
                    currLine.Cursor = Cursors.Hand;
                    currLine.StrokeThickness = 2;
                    currLine.Stroke = new SolidColorBrush(Colors.Black);
                    currLine.X1 = currLine.X2 = e.GetPosition(this.canvasMain).X;
                    currLine.Y1 = currLine.Y2 = e.GetPosition(this.canvasMain).Y;
                    this.canvasMain.Children.Add(currLine);
                    MouseDragElementBehavior myLineMDE = new MouseDragElementBehavior();
                    Interaction.GetBehaviors(currLine).Add(myLineMDE);
                    currLine.MouseLeftButtonDown += new MouseButtonEventHandler(line_MouseLeftButtonDown);
                    currLine.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                    this.SetSelectedTool(Tools.Line);
                    break;
                case Tools.Label: /* 标签。 */
                    BPLabel lab = new BPLabel();
                    lab.Width = 100;
                    lab.Height = 23;
                    lab.Cursor = Cursors.Hand;
                    lab.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    lab.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                    this.canvasMain.Children.Add(lab);
                    MouseDragElementBehavior DragBehavior = new MouseDragElementBehavior();
                    Interaction.GetBehaviors(lab).Add(DragBehavior);
                    this.SetSelectedTool(Tools.Mouse);

                    lab.MouseLeftButtonDown += new MouseButtonEventHandler(UIElement_Click);
                    lab.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                    break;
                case Tools.Link: /* Link。 */
                    BPLink link = new BPLink();
                    link.Width = 100;
                    link.Height = 23;
                    link.Cursor = Cursors.Hand;
                    link.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    link.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);

                    Label lab1 = new Label();
                    lab1.Name = "Lab" + link.Name;
                    lab1.Content = link.Content;
                    link.Content = lab1;
                    this.canvasMain.Children.Add(link);

                    MouseDragElementBehavior mylinkE = new MouseDragElementBehavior();
                    Interaction.GetBehaviors(link).Add(mylinkE);

                    link.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
                    this.SetSelectedTool(Tools.Mouse);
                    break;
                case Tools.TextBox:  // 文本框。
                    this.winSelectTB.Show();
                    X = e.GetPosition(this.canvasMain).X;
                    Y = e.GetPosition(this.canvasMain).Y;
                    this.SetSelectedTool(Tools.Mouse);

                    break;
                case Tools.DDLTable:  // DDL。
                    this.winSelectDDL.Show();
                    X = e.GetPosition(this.canvasMain).X;
                    Y = e.GetPosition(this.canvasMain).Y;
                    this.SetSelectedTool(Tools.Mouse);
                    break;
                case Tools.CheckBox:
                    BPCheckBox cb = new BPCheckBox();
                    cb.Cursor = Cursors.Hand;
                    cb.Content = "New checkbox";
                    cb.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    cb.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);

                     Label cblab = new Label();
                     cblab.Name = "Lab" + cb.Name;
                     cblab.Content = cb.Content;
                     cb.Content = cblab;

                    this.canvasMain.Children.Add(cb);
                    //MouseDragElementBehavior add = new MouseDragElementBehavior();
                    //Interaction.GetBehaviors(cb).Add(add);
                    this.SetSelectedTool(Tools.Mouse);

                    cb.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);

                    break;
                case Tools.DDLEnum:  // DDL。
                    this.winSelectRB.Show();
                    this.IsRB = false;
                    X = e.GetPosition(this.canvasMain).X;
                    Y = e.GetPosition(this.canvasMain).Y;
                    this.SetSelectedTool(Tools.Mouse);
                    break;
                case Tools.RBS:  // 复选框。
                    this.winSelectRB.Show();
                    this.IsRB = true;
                    X = e.GetPosition(this.canvasMain).X;
                    Y = e.GetPosition(this.canvasMain).Y;
                    this.SetSelectedTool(Tools.Mouse);
                    break;
                case Tools.Img:
                    Image img = new Image();
                    //img.Source =new BitmapImage(new Uri("D:\\ccflow\\VisualFlow\\DataUser\\LogBig.png", UriKind.Relative));  //= @"\\Img\\LogBig.png";
                    BitmapImage png = new BitmapImage(new Uri("/FreeFrm;component/Img/LogBig.png", UriKind.Relative));  //= @"\\Img\\LogBig.png";
                    img.Source = png; //=new BitmapImage(new Uri("/BP;component/Img/LogBig.png", UriKind.Relative));  //= @"\\Img\\LogBig.png";
                    //img.Width = png.PixelWidth;
                    //img.Height = png.PixelHeight;
                    img.Name = "Img" + DateTime.Now.ToString("yyMMddhhmmss");
                    img.Width = 200;
                    img.Height = 200;
                    img.Cursor = Cursors.Hand;
                    img.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                    img.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);

                    MouseDragElementBehavior mde = new MouseDragElementBehavior();
                    Interaction.GetBehaviors(img).Add(mde);
                    this.canvasMain.Children.Add(img);
                    this.SetSelectedTool(Tools.Mouse);
                    img.MouseRightButtonDown += new MouseButtonEventHandler(UIElement_MouseRightButtonDown);
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
            //                if (!this.canvasMain.Children.Contains(rightMenu))
            //                {
            //                    this.canvasMain.Children.Add(rightMenu);
            //                    rightMenu.SetValue(Canvas.LeftProperty, a.GetPosition(this.canvasMain).X);
            //                    rightMenu.SetValue(Canvas.TopProperty, a.GetPosition(this.canvasMain).Y);
            //                    ui = s as Line;
            //                }
            //            }
            //        };
            //}
            #endregion
        }
        void link_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            bl = true;
            BPLink link = sender as BPLink;
            this.currLink = link;
            if ((DateTime.Now.Subtract(_lastTime).TotalMilliseconds) < 300)
            {
                this.linkWin.Visibility = Visibility.Visible;
                this.gVisable.Visibility = Visibility.Visible;
                this.linkWin_Label.Text = this.currLink.Content.ToString();
                foreach (ComboBoxItem cbi in this.linkWin_cbSize.Items)
                {
                    if (cbi.Content.ToString() == currLink.FontSize.ToString())
                        cbi.IsSelected = true;
                }

                if (currLink.FontWeight == FontWeights.Bold)
                {
                    this.linkWin_CBWight.IsChecked = true;
                }
                else
                {
                    this.linkWin_CBWight.IsChecked = false;
                }

                this.linkWin.SetValue(Canvas.TopProperty, link.GetValue(Canvas.TopProperty));
                this.linkWin.SetValue(Canvas.LeftProperty, link.GetValue(Canvas.LeftProperty));
            }
            else
            {
                this.linkWin.Visibility = Visibility.Collapsed;
                this.gVisable.Visibility = Visibility.Collapsed;
            }
            _lastTime = DateTime.Now;
        }
        public bool IsRightMenu = false;
        /// <summary>
        /// 点画布上的元素时发生, 根据元素的类型处理响应的操作。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UIElement_Click(object sender, MouseButtonEventArgs e)
        {
            this.ui = sender as UIElement;
            //   MessageBox.Show(sender.ToString());

            #region 处理 Line
            Line line = sender as Line;
            if (line != null)
            {
                this.currLine = line;
                this.line_MouseLeftButtonDown(line, e);
                return;
            }
            #endregion 处理 Line

            #region 处理标签
            BPLabel lab = sender as BPLabel;
            if (lab != null)
            {
                this.currLab = lab;
                if ((DateTime.Now.Subtract(_lastTime).TotalMilliseconds) < 300 || IsRightMenu == true)
                {
                    IsRightMenu = false;
                    this.labWin.Visibility = Visibility.Visible;
                    this.gVisable.Visibility = Visibility.Visible;
                    this.labWin_Label.Text = this.currLab.Content.ToString();
                    Glo.BindComboBoxFontSize(this.labWin_cbSize, this.currLab.FontSize);
                    SolidColorBrush d = (SolidColorBrush)currLab.Foreground;
                    Glo.BindComboBoxColors(this.labWin_cbColor, d.Color.ToString());
                    if (currLab.FontWeight == FontWeights.Bold)
                    {
                        this.labWin_CBWight.IsChecked = true;
                    }
                    else
                    {
                        this.labWin_CBWight.IsChecked = false;
                    }

                    this.gVisable.Visibility = Visibility.Visible;
                    this.labWin.Visibility = Visibility.Visible;
                    this.labWin.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).X);
                    this.labWin.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).Y);
                }
                _lastTime = DateTime.Now;
                return;
            }
            #endregion 处理标签

            #region 处理　Link 
            BPLink link = sender as BPLink;
            if (link != null)
            {
                this.currLink = link;
                if ((DateTime.Now.Subtract(_lastTime).TotalMilliseconds) < 300 || IsRightMenu == true)
                {
                    IsRightMenu = false;
                    this.linkWin.Visibility = Visibility.Visible;
                    this.gVisable.Visibility = Visibility.Visible;
                    this.linkWin_Label.Text = this.currLink.Content.ToString();
                    Glo.BindComboBoxFontSize(this.linkWin_cbSize, this.currLink.FontSize);
                    SolidColorBrush d = (SolidColorBrush)currLink.Foreground;
                    Glo.BindComboBoxColors(this.linkWin_cbColor, d.Color.ToString());
                    if (currLink.FontWeight == FontWeights.Bold)
                    {
                        this.linkWin_CBWight.IsChecked = true;
                    }
                    else
                    {
                        this.linkWin_CBWight.IsChecked = false;
                    }
                    this.linkWin.SetValue(Canvas.TopProperty, currLink.GetValue(Canvas.TopProperty));
                    this.linkWin.SetValue(Canvas.LeftProperty, currLink.GetValue(Canvas.LeftProperty));
                }
                else
                {
                    this.linkWin.Visibility = Visibility.Collapsed;
                    this.gVisable.Visibility = Visibility.Collapsed;
                }
                _lastTime = DateTime.Now;
                return;
            }
            #endregion 处理标签

            #region 处理　textbox . 
            string host = "http://127.0.0.1/Flow/WF/MapDef/Do.aspx?DoType=FreeFrm";
            BPTextBox tb = sender as BPTextBox;
            if (tb != null)
            {
                this.currTB = tb;
                if ((DateTime.Now.Subtract(_lastTime).TotalMilliseconds) < 300 || IsRightMenu == true)
                {
                    IsRightMenu = false;
                    /* Edit 事件.   */
                    string url = host + "&FK_MapData=" + Glo.FK_MapData + "&MyPK=" + Glo.FK_MapData + "_" + tb.Name + "&DataType=" + tb.HisDataType + "&GroupField=0&LGType=" + LGType.Normal + "&KeyOfEn=" + tb.Name + "&UIContralType=" + CtrlType.TextBox;
                    HtmlPage.Window.Eval("window.showModalDialog('" + url + "',window,'dialogHeight:300px;dialogWidth:500px;center:Yes;help:No;scroll:auto;resizable:No;status:No;');");
                }
                _lastTime = DateTime.Now;
                return;
            }
            #endregion 处理 textbox . 

            #region 处理　BPDatePicker .
            BPDatePicker dp = sender as BPDatePicker;
            if (dp != null)
            {
                this.currDP = dp;
                if ((DateTime.Now.Subtract(_lastTime).TotalMilliseconds) < 300 || IsRightMenu == true)
                {
                    IsRightMenu = false;
                    /* Edit 事件.   */
                    string url = host + "&FK_MapData=" + Glo.FK_MapData + "&MyPK=" + Glo.FK_MapData + "_" + dp.Name + "&DataType=" + dp.HisDateType + "&GroupField=0&LGType=" + LGType.Normal + "&KeyOfEn=" + dp.Name + "&UIContralType=" + CtrlType.TextBox;
                    HtmlPage.Window.Eval("window.showModalDialog('" + url + "',window,'dialogHeight:300px;dialogWidth:500px;center:Yes;help:No;scroll:auto;resizable:No;status:No;');");
                }
                _lastTime = DateTime.Now;
                return;
            }
            #endregion 处理 textbox . 

            #region 处理　BPDDL .
            BPDDL ddl = sender as BPDDL;
            if (ddl != null)
            {
                this.currDDL = ddl;
                if ((DateTime.Now.Subtract(_lastTime).TotalMilliseconds) < 300 || IsRightMenu == true)
                {
                    IsRightMenu = false;
                    /* Edit 事件. */
                    string url = host + "&FK_MapData=" + Glo.FK_MapData + "&MyPK=" + Glo.FK_MapData + "_" + this.currDDL.Name + "&DataType=" + ddl.HisDataType + "&GroupField=0&LGType=" + ddl.HisLGType + "&KeyOfEn=" + ddl.Name + "&UIBindKey=" + ddl.UIBindKey + "&UIContralType=" + CtrlType.DDL;
                    //MessageBox.Show(url);
                    HtmlPage.Window.Eval("window.showModalDialog('" + url + "',window,'dialogHeight:300px;dialogWidth:500px;center:Yes;help:No;scroll:auto;resizable:No;status:No;');");
                }
                _lastTime = DateTime.Now;
                return;
            }
            #endregion 处理 BPDDL .

            #region 处理　checkBox .
            Label cbLab = sender as Label;
            if (cbLab != null)
            {
                this.currCheckBoxLabel = cbLab;
                //this.currLink = link;
                if ((DateTime.Now.Subtract(_lastTime).TotalMilliseconds) < 300 || IsRightMenu == true)
                {
                    IsRightMenu = false;
                    this.checkBoxWin.Visibility = Visibility.Visible;
                    this.gVisable.Visibility = Visibility.Visible;

                    this.checkBoxWin_Text.Text = cbLab.Content.ToString();

                    this.checkBoxWin.SetValue(Canvas.TopProperty, currCheckBoxLabel.GetValue(Canvas.TopProperty));
                    this.checkBoxWin.SetValue(Canvas.LeftProperty, currCheckBoxLabel.GetValue(Canvas.LeftProperty));
                }
                else
                {
                    this.linkWin.Visibility = Visibility.Collapsed;
                    this.gVisable.Visibility = Visibility.Collapsed;
                }
                _lastTime = DateTime.Now;
                return;
            }
            #endregion 处理 checkBox .

            #region 处理　RadioBtn .
            BPRadioBtn rb = sender as BPRadioBtn;
            if (rb != null)
            {
                this.currRB = rb;
                if ((DateTime.Now.Subtract(_lastTime).TotalMilliseconds) < 300 || IsRightMenu == true)
                {
                    IsRightMenu = false;
                    /* Edit 事件. */
                    string url = host + "&FK_MapData=" + Glo.FK_MapData + "&MyPK=" + Glo.FK_MapData + "_" + this.currRB.GroupName + "&DataType=" + DataType.AppInt + "&GroupField=0&LGType=" + LGType.Enum + "&KeyOfEn=" + currRB.GroupName + "&UIBindKey=" + currRB.UIBindKey + "&UIContralType=" + CtrlType.RB;
                    HtmlPage.Window.Eval("window.showModalDialog('" + url + "',window,'dialogHeight:300px;dialogWidth:500px;center:Yes;help:No;scroll:auto;resizable:No;status:No;');");
                }
                _lastTime = DateTime.Now;
                return;
            }
            #endregion 处理 RadioBtn .

        }
        /// <summary>
        /// 显示菜单。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UIElement_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (this.canvasMain.Children.Contains(rightMenu) == false)
                this.canvasMain.Children.Add(rightMenu);

            this.rightMenu.Visibility = System.Windows.Visibility.Visible;
            rightMenu.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
            rightMenu.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
            ui = sender as UIElement;
        }
        void line_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Line line = sender as Line;
            if (selectType == Tools.Mouse)
            {
                e.Handled = true;
                if (!canvasMain.Children.Contains(e1) && !canvasMain.Children.Contains(e2))
                {
                    e1.SetValue(Canvas.LeftProperty, line.X1 - 4);
                    e1.SetValue(Canvas.TopProperty, line.Y1 - 4);

                    e2.SetValue(Canvas.LeftProperty, line.X2 - 4);
                    e2.SetValue(Canvas.TopProperty, line.Y2 - 4);

                    this.canvasMain.Children.Add(e1);
                    this.canvasMain.Children.Add(e2);

                    //Interaction.GetBehaviors(e1).Add(myLineMDE);
                    //Interaction.GetBehaviors(e2).Add(myLineMDE);
                }
                else
                {
                    e1.SetValue(Canvas.LeftProperty, line.X1 - 4);
                    e1.SetValue(Canvas.TopProperty, line.Y1 - 4);

                    e2.SetValue(Canvas.LeftProperty, line.X2 - 4);
                    e2.SetValue(Canvas.TopProperty, line.Y2 - 4);
                }
            }

            if ((DateTime.Now.Subtract(_lastTime).TotalMilliseconds) < 300)
            {
                this.lineWin.Visibility = Visibility.Visible;
                this.gVisable.Visibility = Visibility.Visible;

                Glo.BindComboBoxLineBorder(this.lineWin_cbSize, currLine.StrokeThickness);

                SolidColorBrush sc = (SolidColorBrush)currLine.Stroke;
                Glo.BindComboBoxColors(this.lineWin_cbColor, sc.Color.ToString() );

                this.lineWin.SetValue(Canvas.TopProperty, line.GetValue(Canvas.TopProperty));
                this.lineWin.SetValue(Canvas.LeftProperty, line.GetValue(Canvas.LeftProperty));
            }
            else
            {
                e1.SetValue(Canvas.LeftProperty, line.X1 - 4);
                e1.SetValue(Canvas.TopProperty, line.Y1 - 4);

                e2.SetValue(Canvas.LeftProperty, line.X2 - 4);
                e2.SetValue(Canvas.TopProperty, line.Y2 - 4);

                //this.e1.SetValue(Canvas.TopProperty, currLine.X1);
                //this.e1.SetValue(Canvas.LeftProperty, currLine.Y1);
                //this.e2.SetValue(Canvas.TopProperty, currLine.X2);
                //this.e2.SetValue(Canvas.LeftProperty, currLine.Y2);
            }
            _lastTime = DateTime.Now;
        }

        //鼠标松开主面板事件
        private void canvasMain_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
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
            if (isMouseDown)
            {
                #region 线一
                if (this.selectType == Tools.Line)
                {
                    currLine.X2 = e.GetPosition(this.canvasMain).X;
                    currLine.Y2 = e.GetPosition(this.canvasMain).Y;
                }
                #endregion

                //#region 线二
                //else if (selectType == "line2")
                //{
                //    double x = e.GetPosition(this.canvasMain).X - l.X1;
                //    double y = e.GetPosition(this.canvasMain).Y - l.Y1;

                //    if (Math.Abs(x) > Math.Abs(y))
                //    {
                //        l.X2 = e.GetPosition(this.canvasMain).X;
                //        l.Y2 = l.Y1;
                //    }
                //    else
                //    {
                //        l.X2 = l.X1;
                //        l.Y2 = e.GetPosition(this.canvasMain).Y;
                //    }
                //}
                //#endregion
            }
            #endregion

            if (selectType == Tools.Mouse)
            {
                #region 改变线的长度
                if (be)
                {
                    if (eCurrent.Tag.ToString() == "e1")
                    {
                        double x = e.GetPosition(this.canvasMain).X - currLine.X2;
                        double y = e.GetPosition(this.canvasMain).Y - currLine.Y2;
                        if (Math.Abs(x) > Math.Abs(y))
                        {
                            currLine.X1 = e.GetPosition(this.canvasMain).X;
                            currLine.Y1 = currLine.Y2;
                            eCurrent.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X - 4);
                            eCurrent.SetValue(Canvas.TopProperty, currLine.Y2 - 4);
                        }
                        else
                        {
                            currLine.X1 = currLine.X2;
                            currLine.Y1 = e.GetPosition(this.canvasMain).Y;
                            eCurrent.SetValue(Canvas.LeftProperty, currLine.X2 - 4);
                            eCurrent.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y - 4);
                        }
                    }
                    else //if (eCurrent.Tag.ToString() == "e2")
                    {
                        double x = e.GetPosition(this.canvasMain).X - currLine.X1;
                        double y = e.GetPosition(this.canvasMain).Y - currLine.Y1;
                        if (Math.Abs(x) > Math.Abs(y))
                        {
                            currLine.X2 = e.GetPosition(this.canvasMain).X;
                            currLine.Y2 = currLine.Y1;
                            eCurrent.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X - 4);
                            eCurrent.SetValue(Canvas.TopProperty, currLine.Y1 - 4);
                        }
                        else
                        {
                            currLine.X2 = currLine.X1;
                            currLine.Y2 = e.GetPosition(this.canvasMain).Y;
                            eCurrent.SetValue(Canvas.LeftProperty, currLine.X1 - 4);
                            eCurrent.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y - 4);
                        }
                    }
                }
                #endregion

                //#region 拖拉label
                //if (bl)
                //{
                //    tb.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                //    tb.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                //}
                //#endregion

                //#region 拖拉txt
                //if (btxt)
                //{
                //    txt.SetValue(Canvas.LeftProperty, e.GetPosition(this.canvasMain).X);
                //    txt.SetValue(Canvas.TopProperty, e.GetPosition(this.canvasMain).Y);
                //}
                //#endregion
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

            this.Btn_Link.Foreground = new SolidColorBrush(Colors.White);

            this.Btn_Label.Foreground = new SolidColorBrush(Colors.White);
            this.Btn_CheckBox.Foreground = new SolidColorBrush(Colors.White);
            //  this.Btn_DDLEnum.Foreground = new SolidColorBrush(Colors.White);
            this.Btn_DDLEnum.Foreground = new SolidColorBrush(Colors.White);
            this.Btn_DDLTable.Foreground = new SolidColorBrush(Colors.White);

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
                case Tools.Mouse:
                    this.Btn_Mouse.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Tools.Link:
                    this.Btn_Link.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Tools.Dtl:
                    this.Btn_Dtl.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Tools.Img:
                    this.Btn_Img.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Tools.Label:
                    this.Btn_Label.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Tools.Line:
                    this.Btn_Line.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Tools.RBS:
                    this.Btn_RBS.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Tools.DDLTable:
                    this.Btn_DDLTable.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Tools.DDLEnum:
                    this.Btn_DDLEnum.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Tools.CheckBox:
                    this.Btn_CheckBox.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Tools.M2M:
                    this.Btn_M2M.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                case Tools.TextBox:
                    this.Btn_TextBox.Foreground = new SolidColorBrush(Colors.Orange);
                    break;
                default:
                    MessageBox.Show("****** 功能未完成:" + selectType);
                    this.SetSelectedTool(Tools.Mouse);
                    break;
            }
        }

       
        //文本框长度赋值事件
        private void tbSureTxt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (txtWidth.Text == "" || txtHeight.Text == "")
            //{
            //    MessageBox.Show("请输入要设置的长度！");
            //    return;
            //}

            //double i = 0;
            //double j = 0;
            //if (!double.TryParse(txtWidth.Text, out i))
            //{
            //    MessageBox.Show("请输入整型数字！");
            //    return;
            //}
            //if (!double.TryParse(txtHeight.Text, out j))
            //{
            //    MessageBox.Show("请输入整型数字！");
            //    return;
            //}
            //if (i > 1000 || j > 500)
            //{
            //    MessageBox.Show("请输入1000之内的数字！");
            //    return;
            //}
            //txt.Width = double.Parse(txtWidth.Text);
            //txt.Height = double.Parse(txtHeight.Text);

            //canvasWinTxt.Visibility = Visibility.Collapsed;
            //gVisable.Visibility = Visibility.Collapsed;

        }
        //文本框 X 事件
        private void tbCloseTxt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //canvasWinTxt.Visibility = Visibility.Collapsed;
            //gVisable.Visibility = Visibility.Collapsed;
        }
        private void DeleteCurrSelectUI()
        {
            BPRadioBtn rb = ui as BPRadioBtn;
            if (rb != null)
            {
                rb.DoDeleteIt();
                return;
            }
            if (this.canvasMain.Children.Contains(ui))
                this.canvasMain.Children.Remove(ui);
        }

        //删除操作
        private void UI_rightMenu_Btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            switch (tb.Name)
            {
                case "rightMenu_Btn_Del":
                    this.DeleteCurrSelectUI();
                    break;
                case "rightMenu_Btn_Cannel":
                    break;
                case "rightMenu_Btn_Edit":
                    Line line = ui as Line;
                    if (line != null)
                    {
                        this.IsRightMenu = true;
                        line_MouseLeftButtonDown(line, e);
                        return;
                    }
                    BPLabel lab = ui as BPLabel;
                    if (lab != null)
                    {
                        this.IsRightMenu = true;
                        UIElement_Click(lab, e);
                    }

                    BPLink link = ui as BPLink;
                    if (link != null)
                    {
                        this.IsRightMenu = true;
                        UIElement_Click(link, e);
                    }

                    BPTextBox textb = ui as BPTextBox;
                    if (textb != null)
                    {
                        this.IsRightMenu = true;
                        UIElement_Click(textb, e);
                    }

                    BPDDL ddl = ui as BPDDL;
                    if (ddl != null)
                    {
                        this.IsRightMenu = true;
                        UIElement_Click(ddl, e);
                    }

                    BPCheckBox cb = ui as BPCheckBox;
                    if (cb != null)
                    {
                        this.currCB = cb;
                        this.IsRightMenu = true;
                        UIElement_Click(cb, e);
                    }

                    BPRadioBtn rb = ui as BPRadioBtn;
                    if (rb != null)
                    {
                        this.currRB = rb;
                        this.IsRightMenu = true;
                        UIElement_Click(rb, e);
                    }


                    Image img = ui as Image;
                    if (img != null)
                    {
                        this.IsRightMenu = true;
                        UIElement_Click(img, e);
                    }
                    break;
                default:
                    break;
            }
            this.rightMenu.Visibility = System.Windows.Visibility.Collapsed;
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
            //canvasWinTxt.Visibility = Visibility.Collapsed;
            //gVisable.Visibility = Visibility.Collapsed;
        }

        //private void Btn_Enum_OK_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    //canvasWinTxt.Visibility = Visibility.Collapsed;
        //    //gVisable.Visibility = Visibility.Collapsed;
        //    AddEnumGuide ae = new AddEnumGuide();
        //    ae.Visibility = System.Windows.Visibility.Visible;
        //    this.LayoutRoot.Children.Add(ae);
        //    ae.Visibility = System.Windows.Visibility.Collapsed;
        //    MessageBox.Show("ddd here.");
        //}

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            #region line
            DataTable lineDT = new DataTable();
            lineDT.TableName = "Sys_FrmLine";
            lineDT.Columns.Add(new DataColumn("MyPK", typeof(string)));
            lineDT.Columns.Add(new DataColumn("FK_MapData", typeof(string)));
            lineDT.Columns.Add(new DataColumn("X1", typeof(double)));
            lineDT.Columns.Add(new DataColumn("Y1", typeof(double)));

            lineDT.Columns.Add(new DataColumn("X2", typeof(double)));
            lineDT.Columns.Add(new DataColumn("Y2", typeof(double)));

            lineDT.Columns.Add(new DataColumn("BorderWidth", typeof(string)));
            lineDT.Columns.Add(new DataColumn("BorderColor", typeof(string)));
            lineDT.Columns.Add(new DataColumn("BorderStyle", typeof(string)));
            #endregion line

            #region label
            DataTable labDT = new DataTable();
            labDT.TableName = "Sys_FrmLab";
            labDT.Columns.Add(new DataColumn("MyPK", typeof(string)));
            labDT.Columns.Add(new DataColumn("FK_MapData", typeof(string)));
            labDT.Columns.Add(new DataColumn("X", typeof(double)));
            labDT.Columns.Add(new DataColumn("Y", typeof(double)));
            labDT.Columns.Add(new DataColumn("Text", typeof(string)));

            labDT.Columns.Add(new DataColumn("FontColor", typeof(string)));
            labDT.Columns.Add(new DataColumn("FontName", typeof(string)));
            labDT.Columns.Add(new DataColumn("FontStyle", typeof(string)));
            labDT.Columns.Add(new DataColumn("FontSize", typeof(int)));
            #endregion label

            #region Link
            DataTable linkDT = new DataTable();
            linkDT.TableName = "Sys_FrmLink";
            linkDT.Columns.Add(new DataColumn("MyPK", typeof(string)));
            linkDT.Columns.Add(new DataColumn("FK_MapData", typeof(string)));
            linkDT.Columns.Add(new DataColumn("X", typeof(double)));
            linkDT.Columns.Add(new DataColumn("Y", typeof(double)));
            linkDT.Columns.Add(new DataColumn("Text", typeof(string)));
            linkDT.Columns.Add(new DataColumn("Url", typeof(string)));
            linkDT.Columns.Add(new DataColumn("Target", typeof(string)));

            linkDT.Columns.Add(new DataColumn("FontColor", typeof(string)));
            linkDT.Columns.Add(new DataColumn("FontName", typeof(string)));
            linkDT.Columns.Add(new DataColumn("FontStyle", typeof(string)));
            linkDT.Columns.Add(new DataColumn("FontSize", typeof(int)));
            #endregion label

            #region img
            DataTable imgDT = new DataTable();
            imgDT.TableName = "Sys_FrmImg";
            imgDT.Columns.Add(new DataColumn("MyPK", typeof(string)));
            imgDT.Columns.Add(new DataColumn("FK_MapData", typeof(string)));
            imgDT.Columns.Add(new DataColumn("X", typeof(double)));
            imgDT.Columns.Add(new DataColumn("Y", typeof(double)));
            imgDT.Columns.Add(new DataColumn("W", typeof(double)));
            imgDT.Columns.Add(new DataColumn("H", typeof(double)));
            imgDT.Columns.Add(new DataColumn("URL", typeof(string)));
            #endregion img

            #region mapAttrDT
            DataTable mapAttrDT = new DataTable();
            mapAttrDT.TableName = "Sys_MapAttr";
            mapAttrDT.Columns.Add(new DataColumn("MyPK", typeof(string)));
            mapAttrDT.Columns.Add(new DataColumn("FK_MapData", typeof(string)));
            mapAttrDT.Columns.Add(new DataColumn("KeyOfEn", typeof(string)));
            mapAttrDT.Columns.Add(new DataColumn("UIContralType", typeof(string)));
            mapAttrDT.Columns.Add(new DataColumn("MyDataType", typeof(string)));
            mapAttrDT.Columns.Add(new DataColumn("LGType", typeof(string)));

            mapAttrDT.Columns.Add(new DataColumn("UIWidth", typeof(double)));
          //  mapAttrDT.Columns.Add(new DataColumn("UIHeight", typeof(double)));

            mapAttrDT.Columns.Add(new DataColumn("UIBindKey", typeof(string)));
            mapAttrDT.Columns.Add(new DataColumn("UIRefKey", typeof(string)));
            mapAttrDT.Columns.Add(new DataColumn("UIRefKeyText", typeof(string)));
            mapAttrDT.Columns.Add(new DataColumn("UIVisible", typeof(string)));
            mapAttrDT.Columns.Add(new DataColumn("X", typeof(double)));
            mapAttrDT.Columns.Add(new DataColumn("Y", typeof(double)));
            #endregion mapAttrDT

            #region frmRBDT
            DataTable frmRBDT = new DataTable();
            frmRBDT.TableName = "Sys_FrmRB";
            frmRBDT.Columns.Add(new DataColumn("MyPK", typeof(string)));
            frmRBDT.Columns.Add(new DataColumn("FK_MapData", typeof(string)));
            frmRBDT.Columns.Add(new DataColumn("KeyOfEn", typeof(string)));
            frmRBDT.Columns.Add(new DataColumn("EnumKey", typeof(string)));
            frmRBDT.Columns.Add(new DataColumn("IntKey", typeof(int)));
            frmRBDT.Columns.Add(new DataColumn("Lab", typeof(string)));
            frmRBDT.Columns.Add(new DataColumn("X", typeof(double)));
            frmRBDT.Columns.Add(new DataColumn("Y", typeof(double)));
            #endregion frmRBDT

            #region 把数据给值.
            foreach (UIElement ctl in this.canvasMain.Children)
            {
                double dX = Canvas.GetLeft(ctl);
                double dY = Canvas.GetTop(ctl);

                #region line.
                Line line = ctl as Line;
                if (line != null)
                {
                    DataRow drline = lineDT.NewRow();
                    drline["MyPK"] = line.Name;
                    drline["FK_MapData"] = Glo.FK_MapData;
                    drline["X1"] = line.X1.ToString("0.00");
                    drline["X2"] = line.X2.ToString("0.00");
                    drline["Y1"] = line.Y1.ToString("0.00");
                    drline["Y2"] = line.Y2.ToString("0.00");
                    drline["BorderWidth"] = line.StrokeThickness.ToString("0.00");
                    SolidColorBrush d=(SolidColorBrush)line.Stroke;
                    drline["BorderColor"] =d.Color.ToString();
                    lineDT.Rows.Add(drline);
                    continue;
                }
                #endregion line.

                #region lab.
                BPLabel lab = ctl as BPLabel;
                if (lab != null)
                {
                    DataRow drLab = labDT.NewRow();
                    drLab["MyPK"] = lab.Name;
                    drLab["Text"] = lab.Content.ToString();
                    drLab["FK_MapData"] = Glo.FK_MapData;

                    drLab["X"] = dX.ToString("0.00");
                    drLab["Y"] = dY.ToString("0.00");

                    // drLab["FontColor"] = lab.GetValue( lapp ).ToString();
#warning 如何获取字体颜色 ? . 
                    
                    SolidColorBrush d = (SolidColorBrush)lab.Foreground;
                    drLab["FontColor"] = d.Color.ToString();

                    drLab["FontName"] = lab.FontFamily.ToString();
                    drLab["FontStyle"] = lab.FontStyle.ToString();
                    drLab["FontSize"] = lab.FontSize.ToString();
                    //   drLab["UIWidth"] = lab.Width.ToString();
                    labDT.Rows.Add(drLab);
                    continue;
                }
                #endregion lab.

                #region BPLink.
                BPLink link = ctl as BPLink;
                if (link != null)
                {
                    DataRow drLink = linkDT.NewRow();

                    drLink["MyPK"] = link.Name;

                    Label mylab = link.Content as Label;
                    drLink["Text"] =mylab.Content.ToString();
                    drLink["FK_MapData"] = Glo.FK_MapData;

                    drLink["X"] = dX.ToString("0.00");
                    drLink["Y"] = dY.ToString("0.00");

                    SolidColorBrush d = (SolidColorBrush)link.Foreground;
                    drLink["FontColor"] = d.Color.ToString();

                    drLink["FontName"] = link.FontFamily.ToString();
                    drLink["FontStyle"] = link.FontStyle.ToString();
                    drLink["FontSize"] = link.FontSize.ToString();
                    linkDT.Rows.Add(drLink);
                    continue;
                }
                #endregion BPLink.

                #region img.
                Image img = ctl as Image;
                if (img != null)
                {
                    //double dX = Canvas.GetLeft(ctl);
                    //double dY =   Canvas.GetTop(ctl);

                    DataRow drImg = imgDT.NewRow();

                    drImg["MyPK"] = img.Name;
                    drImg["FK_MapData"] = Glo.FK_MapData;


                    MouseDragElementBehavior me = Interaction.GetBehaviors(ctl)[0] as MouseDragElementBehavior;
                    double x = me.X - 138.40;
                    double y = me.Y - 32.40;
                    if (x <= 0)
                        x = 0;
                    if (y == 0)
                        y = 0;
                    if (y.ToString() == "NaN")
                    {
                        x = Canvas.GetLeft(ctl);
                        y = Canvas.GetTop(ctl);
                    }
                    drImg["X"] = x.ToString("0.00"); // Canvas.GetLeft(ctl).ToString("0.00");
                    drImg["Y"] = y.ToString("0.00"); // Canvas.GetTop(ctl).ToString("0.00");

                    drImg["W"] = img.Width.ToString("0.00");
                    drImg["H"] = img.Height.ToString("0.00");

                    BitmapImage png = img.Source as BitmapImage;
                    drImg["URL"] = png.UriSource.ToString();
                    imgDT.Rows.Add(drImg);
                    continue;
                }
                #endregion lab.

                #region TextBox
                BPTextBox tb = ctl as BPTextBox;
                if (tb != null)
                {
                    DataRow mapAttrDR = mapAttrDT.NewRow();
                    mapAttrDR["MyPK"] = Glo.FK_MapData + "_" + tb.Name;
                    mapAttrDR["FK_MapData"] = Glo.FK_MapData;
                    mapAttrDR["KeyOfEn"] = tb.Name;

                    mapAttrDR["UIContralType"] = CtrlType.TextBox;
                    mapAttrDR["MyDataType"] = tb.HisDataType;

                    mapAttrDR["UIWidth"] = tb.Width.ToString("0.00");
                    //mapAttrDR["UIHeight"] = tb.Height.ToString("0.00");

                    mapAttrDR["LGType"] = LGType.Normal;


                    MouseDragElementBehavior me = Interaction.GetBehaviors(ctl)[0] as MouseDragElementBehavior;
                    double x=me.X - 138.40;
                    double y= me.Y - 32.40;
                    if (x <= 0)
                        x = 0;
                    if (y == 0)
                        y = 0;

                    if (y.ToString() == "NaN")
                    {
                        x = Canvas.GetLeft(ctl);
                        y = Canvas.GetTop(ctl);
                    }

                    mapAttrDR["X"] = x.ToString("0.00"); // Canvas.GetLeft(ctl).ToString("0.00");
                    mapAttrDR["Y"] = y.ToString("0.00"); // Canvas.GetTop(ctl).ToString("0.00");

                    //element.GetValue(Canvas.LeftProperty);
                    //element.GetValue(Canvas.TopProperty);

                    mapAttrDR["UIVisible"] = "1";
                    mapAttrDR["UIWidth"] = tb.Width.ToString();
                    mapAttrDT.Rows.Add(mapAttrDR);
                    continue;
                }
                #endregion TextBox

                #region datapicker
                BPDatePicker dp = ctl as BPDatePicker;
                if (dp != null)
                {
                    DataRow mapAttrDR = mapAttrDT.NewRow();
                    mapAttrDR["MyPK"] = Glo.FK_MapData + "_" + dp.Name;
                    mapAttrDR["FK_MapData"] = Glo.FK_MapData;
                    mapAttrDR["KeyOfEn"] = dp.Name;

                    mapAttrDR["UIContralType"] = CtrlType.TextBox;
                    mapAttrDR["MyDataType"] = dp.HisDateType;
                    mapAttrDR["LGType"] = LGType.Normal;

                    mapAttrDR["X"] = dX.ToString("0.00");
                    mapAttrDR["Y"] = dY.ToString("0.00");

                    mapAttrDR["UIVisible"] = "1";
                    mapAttrDR["UIWidth"] = "20";
                    mapAttrDT.Rows.Add(mapAttrDR);
                    continue;
                }
                #endregion TextBox

                #region BPDDL
                BPDDL ddl = ctl as BPDDL;
                if (ddl != null)
                {
                    DataRow mapAttrDR = mapAttrDT.NewRow();
                    mapAttrDR["MyPK"] = Glo.FK_MapData + "_" + ddl.Name;
                    mapAttrDR["FK_MapData"] = Glo.FK_MapData;
                    mapAttrDR["KeyOfEn"] = ddl.Name;

                    mapAttrDR["UIContralType"] = CtrlType.DDL;
                    mapAttrDR["MyDataType"] = DataType.AppInt;
                    mapAttrDR["LGType"] = ddl.HisLGType;

                    mapAttrDR["UIWidth"] = ddl.Width.ToString("0.00");
                   // mapAttrDR["UIHeight"] = "0";  
                    // ddl.Height.ToString("0.00");


                    mapAttrDR["X"] = dX.ToString("0.00");
                    mapAttrDR["Y"] = dY.ToString("0.00");

                    mapAttrDR["UIBindKey"] = ddl.UIBindKey;
                    mapAttrDR["UIRefKey"] = "No";
                    mapAttrDR["UIRefKeyText"] = "Name";
                    mapAttrDR["UIVisible"] = "1";
                    mapAttrDR["UIWidth"] = ddl.Width.ToString();
                    mapAttrDT.Rows.Add(mapAttrDR);
                    continue;
                }
                #endregion BPDDL

                #region CheckBox
                BPCheckBox cb = ctl as BPCheckBox;
                if (cb != null)
                {
                    DataRow mapAttrDR = mapAttrDT.NewRow();
                    mapAttrDR["MyPK"] = Glo.FK_MapData + "_" + cb.Name;
                    mapAttrDR["FK_MapData"] = Glo.FK_MapData;
                    mapAttrDR["KeyOfEn"] = cb.Name;
                    mapAttrDR["UIContralType"] = CtrlType.CheckBox;
                    mapAttrDR["LGType"] = LGType.Normal;
                    mapAttrDR["X"] = dX.ToString("0.00");
                    mapAttrDR["Y"] = dY.ToString("0.00");
                    mapAttrDR["UIVisible"] = "1";
                    mapAttrDR["UIWidth"] = "0"; // cb.Width.ToString();
                    mapAttrDR["UIBindKey"] = "";
                    mapAttrDR["UIRefKey"] = "";
                    mapAttrDR["UIRefKeyText"] = "";
                    mapAttrDT.Rows.Add(mapAttrDR);
                    continue;
                }
                #endregion CheckBox

                #region BPRadioBtn
                BPRadioBtn rb = ctl as BPRadioBtn;
                if (rb != null)
                {
                    DataRow mapAttrRB = frmRBDT.NewRow();
                    mapAttrRB["MyPK"] = rb.Name;
                    mapAttrRB["FK_MapData"] = Glo.FK_MapData;
                    mapAttrRB["KeyOfEn"] = rb.GroupName;
                    mapAttrRB["IntKey"] = rb.Tag as string;
                    mapAttrRB["Lab"] = rb.Content as string;
                    mapAttrRB["EnumKey"] = rb.UIBindKey;
                    mapAttrRB["X"] = dX.ToString("0.00");
                    mapAttrRB["Y"] = dY.ToString("0.00");

                    frmRBDT.Rows.Add(mapAttrRB);
                    continue;
                }
                #endregion BPRadioBtn
            }
            #endregion 把数据给值.

            #region 处理 RB 枚举值
            string keys = "";
            foreach (DataRow dr in frmRBDT.Rows)
            {
                string keyOfEn = dr["KeyOfEn"];
                if (keys.Contains("@" + keyOfEn + "@"))
                {
                    continue;
                }
                else
                {
                    keys += "@" + keyOfEn + "@";
                }

                string enumKey = dr["EnumKey"];
                DataRow mapAttrDR = mapAttrDT.NewRow();
                mapAttrDR["MyPK"] = Glo.FK_MapData + "_" + keyOfEn;
                mapAttrDR["FK_MapData"] = Glo.FK_MapData;
                mapAttrDR["KeyOfEn"] = keyOfEn;

                mapAttrDR["UIContralType"] = CtrlType.RB;
                mapAttrDR["MyDataType"] = DataType.AppInt;
                mapAttrDR["LGType"] = LGType.Enum;

                mapAttrDR["X"] = "0";
                mapAttrDR["Y"] = "0";

                mapAttrDR["UIBindKey"] = enumKey;
                mapAttrDR["UIRefKey"] = "No";
                mapAttrDR["UIRefKeyText"] = "Name";
                mapAttrDR["UIVisible"] = "1";
                mapAttrDR["UIWidth"] = "30";
                mapAttrDT.Rows.Add(mapAttrDR);
            }
            #endregion 处理 RB 枚举值

            DataSet myds = new DataSet();
            myds.Tables.Add(labDT);
            myds.Tables.Add(linkDT);
            myds.Tables.Add(imgDT);
            myds.Tables.Add(mapAttrDT);
            myds.Tables.Add(frmRBDT);
            myds.Tables.Add(lineDT);


            #region 求出已经删除的集合.
            string sqls = "";
            foreach (DataTable ysdt in this.FrmDS.Tables)
            {
                DataTable newDt = myds.Tables[ysdt.TableName];
                #region 求pK
                string pk = "";
                foreach (DataColumn dc in ysdt.Columns)
                {
                    switch (dc.ColumnName)
                    {
                        case "MyPK":
                            pk = "MyPK";
                            break;
                        case "No":
                            pk = "No";
                            break;
                        case "OID":
                            pk = "OID";
                            break;
                        default:
                            break;
                    }
                }
                #endregion 求pK

                foreach (DataRow dr in ysdt.Rows)
                {
                    string pkVal = dr[pk].ToString();
                    bool isHave = false;
                    foreach (DataRow newDr in newDt.Rows)
                    {
                        if (newDr[pk].ToString() == pkVal)
                        {
                            isHave = true;
                            break;
                        }
                    }
                    if (isHave == false)
                        sqls += "@DELETE " + ysdt.TableName + " WHERE " + pk + "='" + pkVal + "'";
                }
            }
            #endregion

            #region save checkbox label.
            foreach (UIElement ctl in this.canvasMain.Children)
            {
                BPCheckBox cb = ctl as BPCheckBox;
                if (cb == null)
                    continue;

                Label mylab = cb.Content as Label;
                sqls += "@UPDATE Sys_MapAttr SET Name='" + mylab.Content + "' WHERE MyPK='" + Glo.FK_MapData + "_" + cb.Name + "'";
            }
            #endregion save checkbox label.

            FF.FreeFrmSoapClient da = new FF.FreeFrmSoapClient();
            da.SaveFrmAsync(myds.ToXml(true, false), sqls);
            da.SaveFrmCompleted += new EventHandler<FF.SaveFrmCompletedEventArgs>(da_SaveFrmCompleted);

           // this.BindFrm();
        }
        void da_RunSQLsCompleted(object sender, FF.RunSQLsCompletedEventArgs e)
        {
            MessageBox.Show(e.Result.ToString(), "保存信息", MessageBoxButton.OK);
        }
        void da_SaveFrmCompleted(object sender, FF.SaveFrmCompletedEventArgs e)
        {
            MessageBox.Show(e.Result,"保存信息", MessageBoxButton.OK);
        }

        private void canvasMain_KeyDown(object sender, KeyEventArgs e)
        {

            return;
            UIElement ctl = (UIElement)sender;
            MessageBox.Show(e.Key.ToString());
            //MessageBox.Show(ctl.ToString());
            //e.Handled = true;

            // 获取 textBox 对象的相对于 Canvas 的 x坐标 和 y坐标
            double x = (double)this.GetValue(Canvas.LeftProperty);
            double y = (double)this.GetValue(Canvas.TopProperty);

            // KeyEventArgs.Key - 与事件相关的键盘的按键 [System.Windows.Input.Key枚举]
            switch (e.Key)
            {
                // 按 Up 键后 textBox 对象向 上 移动 1 个像素
                // Up 键所对应的 e.PlatformKeyCode == 38 
                // 当获得的 e.Key == Key.Unknown 时，可以使用 e.PlatformKeyCode 来确定用户所按的键
                case Key.Delete:
                    if (MessageBox.Show("您确定要删除吗？",
                        "删除提示", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                        return;
                    this.DeleteCurrSelectUI();
                    break;
                case Key.Up:
                    this.SetValue(Canvas.TopProperty, y - 1);
                    break;

                // 按 Down 键后 textBox 对象向 下 移动 1 个像素
                // Down 键所对应的 e.PlatformKeyCode == 40
                case Key.Down:
                    this.SetValue(Canvas.TopProperty, y + 1);
                    break;

                // 按 Left 键后 textBox 对象向 左 移动 1 个像素
                // Left 键所对应的 e.PlatformKeyCode == 37
                case Key.Left:
                    this.SetValue(Canvas.LeftProperty, x - 1);
                    break;

                // 按 Right 键后 textBox 对象向 右 移动 1 个像素
                // Right 键所对应的 e.PlatformKeyCode == 39 
                case Key.Right:
                    this.SetValue(Canvas.LeftProperty, x + 1);
                    break;
                case Key.C:
                    break;
                case Key.V:
                    //if (Keyboard.Modifiers == ModifierKeys.Control)
                    //{
                    //    BPCheckBox tb = new BPCheckBox();
                    //    tb.Cursor = Cursors.Hand;
                    //    tb.Content = "New Checkbox";

                    //    tb.SetValue(Canvas.LeftProperty, (double)this.GetValue(Canvas.LeftProperty) + 15);
                    //    tb.SetValue(Canvas.TopProperty, (double)this.GetValue(Canvas.TopProperty) + 15);
                    //    Canvas s1c = this.Parent as Canvas;
                    //    try
                    //    {
                    //        s1c.Children.Add(tb);
                    //    }
                    //    catch
                    //    {
                    //        s1c.Children.Remove(tb);
                    //    }
                    //}
                    break;
                default:
                    break;
            }
            base.OnKeyDown(e);
        }
        private void UI_WinCanvas_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.labWin.Visibility = Visibility.Collapsed;
            this.lineWin.Visibility = Visibility.Collapsed;
            this.linkWin.Visibility = Visibility.Collapsed;
            this.checkBoxWin.Visibility = Visibility.Collapsed;
            this.gVisable.Visibility = Visibility.Collapsed;

            Button btn = sender as Button;
            if (btn == null)
                return;

            if (btn.Name.Contains("lab"))
            {
                if (btn != null && btn.Name == "labWin_OK")
                {
                    currLab.Content = this.labWin_Label.Text;
                    currLab.FontSize = double.Parse(this.labWin_cbSize.SelectionBoxItem.ToString());
                    if (this.labWin_CBWight.IsChecked == true)
                        currLab.FontWeight = FontWeights.Bold;
                    else
                        currLab.FontWeight = FontWeights.Normal;

                    ComboBoxItem cbi = this.labWin_cbColor.SelectedItem as ComboBoxItem;
                    string color = cbi.Content.ToString();
                    currLab.Foreground = new SolidColorBrush(Glo.PreaseColor(color));
                }
            }
            if (btn.Name.Contains("line"))
            {
                if (btn != null && btn.Name == "lineWin_OK")
                {
                    int size = this.lineWin_cbSize.SelectedIndex + 1;
                    currLine.StrokeThickness = double.Parse(size.ToString());
                    ComboBoxItem cbi = this.lineWin_cbColor.SelectedItem as ComboBoxItem;
                    currLine.Stroke = new SolidColorBrush(Glo.PreaseColor(cbi.Content.ToString()));
                    // currLine.Visibility = System.Windows.Visibility.Visible;
                    // currLine.Width = double.Parse(size.ToString());
                    //currLine.LayoutUpdated
                    //currLine.re
                }
            }

            if (btn.Name.Contains("checkBox"))
            {
                if (btn != null && btn.Name == "checkBoxWin_OK")
                    this.currCheckBoxLabel.Content = this.checkBoxWin_Text.Text;
            }
            
            if (btn.Name.Contains("link"))
            {

                if (btn != null && btn.Name == "linkWin_OK")
                {
                    currLink.Content = this.linkWin_Label.Text;
                    currLink.FontSize = double.Parse(this.linkWin_cbSize.SelectionBoxItem.ToString());
                    if (this.linkWin_CBWight.IsChecked == true)
                        currLink.FontWeight = FontWeights.Bold;
                    else
                        currLink.FontWeight = FontWeights.Normal;

                    ComboBoxItem cbi = this.linkWin_cbColor.SelectedItem as ComboBoxItem;
                    currLink.Foreground = new SolidColorBrush(Glo.PreaseColor(cbi.Content.ToString()));
                }
                //this.labWin.Visibility = Visibility.Collapsed;
                //this.gVisable.Visibility = Visibility.Collapsed;
                //if (btn != null && btn.Name == "linkWin_OK")
                //{
                //    int size = this.linkWin_cbSize.SelectedIndex + 1;
                //    currLink.FontSize = double.Parse(size.ToString());
                //    ComboBoxItem cbi = this.lineWin_cbColor.SelectedItem as ComboBoxItem;
                //    currLink.Foreground = new SolidColorBrush(Glo.PreaseColor(cbi.Content.ToString()));
                //}
            }
        }
       

        private void canvasMain_KeyUp(object sender, KeyEventArgs e)
        {
            this.canvasMain_KeyDown(sender, e);
        }


        //private void labWin_Cancel_Click(object sender, RoutedEventArgs e)
        //{
        //}
    }
}
