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
using System.IO.IsolatedStorage;
using System.Xml.Linq;
using System.IO;

namespace Ccflow.Web.UI.Control.Workflow.Setting
{
    public partial class FlowNodeSetting : UserControl
    {
        public void ApplyCulture()
        {
            btSubFlow.Text = Text.SubFlow;
            tbFlowNodeName.Text = Text.FlowNodeName;
            tbFlowNodeType.Text = Text.FlowNodeType;
            btnAppay.Content = Text.Button_Apply;
            btnClose.Content = Text.Button_Cancle;
            btnSave.Content = Text.Button_OK;
            tbMergePictureRepeatDirection.Text = Text.RepeatDirection;
            initFlowNodeList();
            initMergePictureRepeatDirection();

            if (currentFlowNode != null)
            {

                initSetting(currentFlowNode.FlowNodeData);
            }
        }
        FlowNode currentFlowNode;
        public void SetSetting(FlowNode a)
        {
            this.Visibility = Visibility.Visible;
            this.ShowDisplayAutomation();
            if (a == currentFlowNode)
                return;
            clearSetting();
            initSetting(a.FlowNodeData);
            currentFlowNode = a;
        }
        void clearSetting()
        {
            txtFlowNodeName.Text = "";
            cbFlowNodeType.SelectedIndex = -1;
        }
        void initSetting(FlowNodeComponent ac)
        {
            txtFlowNodeName.Text = ac.FlowNodeName;
            string name = "";
            for (int i = 0; i < cbFlowNodeType.Items.Count; i++)
            {
                name = ((FlowNodeTypeItem)cbFlowNodeType.Items[i]).Name;

                if (name == ac.FlowNodeType)
                {
                    cbFlowNodeType.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < cbMergePictureRepeatDirection.Items.Count; i++)
            {
                name = ((RepeatDirectionItem)cbMergePictureRepeatDirection.Items[i]).Name;

                if (name == ac.RepeatDirection)
                {
                    cbMergePictureRepeatDirection.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < cbSubFlowList.Items.Count; i++)
            {
                name = ((WorkflowListItem)cbSubFlowList.Items[i]).ID;

                if (name == ac.SubFlow)
                {
                    cbSubFlowList.SelectedIndex = i;
                    break;
                }
            }

            FlowNodeType t = (FlowNodeType)Enum.Parse(typeof(FlowNodeType), ac.FlowNodeType, true);
            if (t == FlowNodeType.OR_MERGE
                || t == FlowNodeType.AND_MERGE
                || t == FlowNodeType.VOTE_MERGE)
            {
                tbMergePictureRepeatDirection.Visibility = Visibility.Visible;
                cbMergePictureRepeatDirection.Visibility = Visibility.Visible;
            }
            else
            {
                tbMergePictureRepeatDirection.Visibility = Visibility.Collapsed;
                cbMergePictureRepeatDirection.Visibility = Visibility.Collapsed;
            }


            if (t == FlowNodeType.SUBPROCESS)
            {
                btSubFlow.Visibility = Visibility.Visible;
                cbSubFlowList.Visibility = Visibility.Visible;
            }
            else
            {
                btSubFlow.Visibility = Visibility.Collapsed;
                cbSubFlowList.Visibility = Visibility.Collapsed;
            }

        }
        void initFlowNodeList()
        {
            List<FlowNodeTypeItem> Cus = new List<FlowNodeTypeItem>();
            Cus.Add(new FlowNodeTypeItem("INTERACTION", Text.FlowNodeType_INTERACTION));
            Cus.Add(new FlowNodeTypeItem("AND_BRANCH", Text.FlowNodeType_AND_BRANCH));
            Cus.Add(new FlowNodeTypeItem("OR_BRANCH", Text.FlowNodeType_OR_BRANCH));
            Cus.Add(new FlowNodeTypeItem("AND_MERGE", Text.FlowNodeType_AND_MERGE));
            Cus.Add(new FlowNodeTypeItem("OR_MERGE", Text.FlowNodeType_OR_MERGE));
            Cus.Add(new FlowNodeTypeItem("VOTE_MERGE", Text.FlowNodeType_VOTE_MERGE));
            Cus.Add(new FlowNodeTypeItem("AUTOMATION", Text.FlowNodeType_AUTOMATION));
            Cus.Add(new FlowNodeTypeItem("INITIAL", Text.FlowNodeType_INITIAL));
            Cus.Add(new FlowNodeTypeItem("COMPLETION", Text.FlowNodeType_COMPLETION));
            // Cus.Add(new FlowNodeTypeItem("DUMMY", Text.FlowNodeType_DUMMY));
            Cus.Add(new FlowNodeTypeItem("SUBPROCESS", Text.FlowNodeType_SUBPROCESS));
            cbFlowNodeType.ItemsSource = Cus;
        }
        public FlowNodeSetting()
        {
            InitializeComponent();
            initFlowNodeList();
            initMergePictureRepeatDirection();
            try
            {
                initSubflowList();
            }
            catch { }

        }
        public class RepeatDirectionItem
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public RepeatDirectionItem()
            {
            }
            public RepeatDirectionItem(string name, string text)
            {
                Name = name;
                Text = text;
            }
        }
        void initMergePictureRepeatDirection()
        {
            List<RepeatDirectionItem> Cus = new List<RepeatDirectionItem>();

            Cus.Add(new RepeatDirectionItem("Horizontal", Text.RepeatDirection_Horizontal));
            Cus.Add(new RepeatDirectionItem("Vertical", Text.RepeatDirection_Vertical));
            cbMergePictureRepeatDirection.ItemsSource = Cus;
            cbMergePictureRepeatDirection.SelectedIndex = 0;
        }
        bool workflowListIsCreated = false;
        void initSubflowList()
        {
            if (workflowListIsCreated)
                return;
            workflowListIsCreated = true;

            System.ServiceModel.BasicHttpBinding bind = new System.ServiceModel.BasicHttpBinding();
            System.ServiceModel.EndpointAddress endpoint = new System.ServiceModel.EndpointAddress(
                new Uri(System.Windows.Browser.HtmlPage.Document.DocumentUri, "services/workflow.asmx"), null);

         


        }

   
        public class WorkflowListItem
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public WorkflowListItem()
            {
            }
            public WorkflowListItem(string name, string id)
            {
                Name = name;
                ID = id;
            }
        }
   

        public class FlowNodeTypeItem
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public FlowNodeTypeItem(string name, string text)
            {
                Name = name;
                Text = text;
            }
        }
        FlowNodeComponent getFlowNodeData()
        {
            FlowNodeComponent ac = new FlowNodeComponent();
            ac.FlowNodeName = txtFlowNodeName.Text;
            if (cbFlowNodeType.SelectedIndex >= 0)
            {
                FlowNodeTypeItem cbi = cbFlowNodeType.SelectedItem as FlowNodeTypeItem;
                if (cbi != null)
                {
                    ac.FlowNodeType = cbi.Name;
                }

            }
            if (cbMergePictureRepeatDirection.SelectedIndex >= 0)
            {
                RepeatDirectionItem cbi = cbMergePictureRepeatDirection.SelectedItem as RepeatDirectionItem;
                if (cbi != null)
                {
                    ac.RepeatDirection = cbi.Name;
                }

            }
            if (cbSubFlowList.SelectedIndex >= 0)
            {
                WorkflowListItem cbi = cbSubFlowList.SelectedItem as WorkflowListItem;
                if (cbi != null)
                {
                    ac.SubFlow = cbi.ID;
                }

            }
            return ac;
        }
        public void ShowDisplayAutomation()
        {
            sbFlowNodeSettingDisplay.Begin();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

            close();
        }
        void close()
        {
            sbFlowNodeSettingClose.Completed += new EventHandler(sbFlowNodeSettingClose_Completed);
            sbFlowNodeSettingClose.Begin();
        }
        void sbFlowNodeSettingClose_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            currentFlowNode.SetFlowNodeData(getFlowNodeData());
            close();


        }
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            currentFlowNode.SetFlowNodeData(getFlowNodeData());

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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFlowNodeType.SelectedItem != null)
            {
                FlowNodeType t = (FlowNodeType)Enum.Parse(typeof(FlowNodeType), ((FlowNodeTypeItem)cbFlowNodeType.SelectedItem).Name, true);
                if (t == FlowNodeType.OR_MERGE
                    || t == FlowNodeType.AND_MERGE
                    || t == FlowNodeType.VOTE_MERGE)
                {
                    tbMergePictureRepeatDirection.Visibility = Visibility.Visible;
                    cbMergePictureRepeatDirection.Visibility = Visibility.Visible;
                }
                else
                {
                    tbMergePictureRepeatDirection.Visibility = Visibility.Collapsed;
                    cbMergePictureRepeatDirection.Visibility = Visibility.Collapsed;
                }

                if (t == FlowNodeType.SUBPROCESS)
                {
                    btSubFlow.Visibility = Visibility.Visible;
                    cbSubFlowList.Visibility = Visibility.Visible;
                }
                else
                {
                    btSubFlow.Visibility = Visibility.Collapsed;
                    cbSubFlowList.Visibility = Visibility.Collapsed;
                }
            }
        }

    }
}
