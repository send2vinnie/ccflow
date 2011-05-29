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
using Ccflow.Web.Component.Workflow;
using Ccflow.Web.UI.Control.Workflow.Designer;
using WF.Resources;

namespace Ccflow.Web.UI.Control.Workflow.Setting
{
    public partial class DirectionSetting : UserControl
    {
        public void ApplyCulture()
        {

            tbCondition.Text = Text.DirectionCondition;
            tbLineType.Text = Text.LineType;
            tbDirectionName.Text = Text.DirectionName;

            btnAppay.Content = Text.Button_Apply;
            btnClose.Content = Text.Button_Cancle;
            btnSave.Content = Text.Button_OK;
            initLineType();

            if (currentDirection != null)
            {
                initSetting(currentDirection.DirectionData);
            }
        }
        Direction currentDirection;
        public void SetSetting(Direction r)
        {
            this.Visibility = Visibility.Visible;
            this.ShowDisplayAutomation();
            if (r == currentDirection)
                return;
            clearSetting();
            initSetting(r.DirectionData);
            currentDirection = r;
        }
        public void ShowDisplayAutomation()
        {
            sbDirectionSettingDisplay.Begin();
        }
        void close()
        {
            sbDirectionSettingClose.Completed += new EventHandler(sbFlowNodeSettingClose_Completed);
            sbDirectionSettingClose.Begin();
        }
        void sbFlowNodeSettingClose_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        void clearSetting()
        {
            txtDirectionName.Text = "";
            txtDirectionCondition.Text = "";
        }
        void initSetting(DirectionComponent rc)
        {
            txtDirectionName.Text = rc.DirectionName;
            txtDirectionCondition.Text = rc.DirectionCondition;

            string name = "";
            for (int i = 0; i < cbDirectionLineType.Items.Count; i++)
            {
                name = ((DirectionLineTypeItem)cbDirectionLineType.Items[i]).Name;

                if (name == rc.LineType)
                {
                    cbDirectionLineType.SelectedIndex = i;
                    break;
                }
            }
        }

        DirectionComponent getDirectionData()
        {
            DirectionComponent rc = new DirectionComponent();
            rc.DirectionName = txtDirectionName.Text;
            rc.DirectionCondition = txtDirectionCondition.Text;

            if (cbDirectionLineType.SelectedIndex >= 0)
            {
                DirectionLineTypeItem cbi = cbDirectionLineType.SelectedItem as DirectionLineTypeItem;
                if (cbi != null)
                {
                    rc.LineType = cbi.Name;
                }

            }
            return rc;
        }
        void initLineType()
        {
            List<DirectionLineTypeItem> Cus = new List<DirectionLineTypeItem>();


            Cus.Add(new DirectionLineTypeItem("Line", Text.DirectionLineType_Line));
            Cus.Add(new DirectionLineTypeItem("Polyline", Text.DirectionLineType_Polyline));


            cbDirectionLineType.ItemsSource = Cus;
        }
        public DirectionSetting()
        {
            InitializeComponent();
            initLineType();


        }
        public class DirectionLineTypeItem
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public DirectionLineTypeItem(string name, string text)
            {
                Name = name;
                Text = text;
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            currentDirection.SetDirectionData(getDirectionData());
            close();
            

        }
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            currentDirection.SetDirectionData(getDirectionData());

        }
        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            FrameworkElement element = sender as FrameworkElement;
            mousePosition = e.GetPosition(null);
            trackingMouseMove = true;
            if (null != element)
            {
                element.CaptureMouse();
                element.Cursor = Cursors.Hand;
            }

        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {



            FrameworkElement element = sender as FrameworkElement;
            trackingMouseMove = false;
            element.ReleaseMouseCapture();

            mousePosition.X = mousePosition.Y = 0;
            element.Cursor = null;


        }
        bool trackingMouseMove = false;
        Point mousePosition;
        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            element.Cursor = Cursors.Hand;
            if (trackingMouseMove)
            {
                double deltaV = e.GetPosition(null).Y - mousePosition.Y;
                double deltaH = e.GetPosition(null).X - mousePosition.X;
                double newTop = deltaV + (double)this.GetValue(Canvas.TopProperty);
                double newLeft = deltaH + (double)this.GetValue(Canvas.LeftProperty);

                double containerWidth = (double)this.Parent.GetValue(Canvas.WidthProperty);
                double containerHeight = (double)this.Parent.GetValue(Canvas.HeightProperty);
                if (newLeft + this.Width > containerWidth
                   || newTop + this.Height > containerHeight
                    || newLeft < 0
                    || newTop < 0
                    )
                {
                    //超过流程容器的范围
                }
                else
                {



                    this.SetValue(Canvas.TopProperty, newTop);
                    this.SetValue(Canvas.LeftProperty, newLeft);

                    mousePosition = e.GetPosition(null);
                }
            }

        }
    }
}
