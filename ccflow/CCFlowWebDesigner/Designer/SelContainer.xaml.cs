using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.IO;
using Ccflow.Web.UI.Control.Workflow.Designer;
using FluxJpeg.Core;
using WF.Resources;
using WF.WS;
using Silverlight;
using System.Windows.Browser;

namespace WF.Designer
{
    /// <summary>
    /// 主要用来显示流程轨迹
    /// </summary>
    public partial class SelContainer : UserControl, IContainer
    {
        #region Constructs

        public SelContainer()
        {
            InitializeComponent();
            cnsDesignerContainer.Width = Application.Current.Host.Content.ActualWidth;
            cnsDesignerContainer.Height = Application.Current.Host.Content.ActualHeight;
            Application.Current.Host.Content.Resized += new EventHandler(Content_Resized);

            SetGridLines();

            _doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
            _doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, SystemConst.DoubleClickTime);
            _doubleClickTimer.Tick += new EventHandler(DoubleClick_Timer);

        }

        public SelContainer(string fk_flow, string workid)
            : this()
        {
            FK_Flow = fk_flow;
            WorkID = workid;
            _Service.GetDTOfWorkListAsync(FK_Flow, WorkID);
            _Service.GetDTOfWorkListCompleted += _Service_GetDTOfWorkListCompleted;
        }
        #endregion

        #region Properties

        public bool IsMouseSelecting
        {
            get { return (temproaryEllipse != null); }
        }

        public WSDesignerSoapClient _Service
        {
            get { return _service; }
            set { _service = value; }
        }

        public string FK_Flow { get; set; }

        public string WorkID { get; set; }

        public bool IsNeedSave { get; set; }

        /// <summary>
        /// 当前是否有节点在编辑状态
        /// </summary>
        public bool IsSomeChildEditing { get; set; }

        public PageEditType EditType
        {
            get
            {
                if (editType == PageEditType.None)
                {
                    editType = PageEditType.Add;
                }
                return editType;
            }
            set { editType = value; }
        }


        public int NodeID;

        public List<FlowNode> flowNodeCollections;

        public List<FlowNode> FlowNodeCollections
        {
            get
            {
                if (flowNodeCollections == null)
                {
                    flowNodeCollections = new List<FlowNode>();
                }
                return flowNodeCollections;
            }
        }

        public List<Direction> directionCollections;

        public List<Direction> DirectionCollections
        {
            get
            {
                if (directionCollections == null)
                {
                    directionCollections = new List<Direction>();
                }
                return directionCollections;
            }
        }

        public List<NodeLabel> lableCollections;

        public List<NodeLabel> LableCollections
        {
            get
            {
                if (lableCollections == null)
                {
                    lableCollections = new List<NodeLabel>();
                }
                return lableCollections;
            }
        }

        private int nextMaxIndex = 0;

        public int NextMaxIndex
        {
            get
            {
                nextMaxIndex++;
                return nextMaxIndex;
            }
        }

        public double Left
        {
            get { return 230; }
        }

        public double Top
        {
            get { return 40; }
        }

        public string FlowID { get; set; }

        public Double ContainerWidth
        {
            get { return cnsDesignerContainer.Width; }
            set { cnsDesignerContainer.Width = value; }
        }

        public Double ContainerHeight
        {
            get { return cnsDesignerContainer.Height; }
            set { cnsDesignerContainer.Height = value; }
        }

        public int NextNewFlowNodeIndex
        {
            get { return 0; }
        }

        public int NextNewDirectionIndex
        {
            get { return 0; }
        }

        public int NextNewLabelIndex
        {
            get { return 0; }
        }

        public Double ScrollViewerHorizontalOffset
        {
            get { return svContainer.HorizontalOffset; }
            set { svContainer.ScrollToHorizontalOffset(value); }
        }

        public Double ScrollViewerVerticalOffset
        {
            get { return svContainer.VerticalOffset; }
            set { svContainer.ScrollToVerticalOffset(value); }
        }

        public Canvas GridLinesContainer
        {
            get
            {
                if (_gridLinesContainer == null)
                {
                    Canvas temCan = new Canvas();
                    temCan.Name = "canGridLinesContainer";
                    cnsDesignerContainer.Children.Add(temCan);
                    _gridLinesContainer = temCan;
                }
                return _gridLinesContainer;
            }
        }

        private List<Control> copyElementCollectionInMemory;

        public List<Control> CopyElementCollectionInMemory
        {
            get
            {
                if (copyElementCollectionInMemory == null)
                    copyElementCollectionInMemory = new List<System.Windows.Controls.Control>();
                return copyElementCollectionInMemory;
            }
            set { copyElementCollectionInMemory = value; }
        }

        private bool mouseIsInContainer = false;

        public bool MouseIsInContainer
        {
            get { return mouseIsInContainer; }
            set { mouseIsInContainer = value; }
        }

        public Direction CurrentTemporaryDirection { get; set; }

        private List<System.Windows.Controls.Control> _currentSelectedControlCollection;

        public List<System.Windows.Controls.Control> CurrentSelectedControlCollection
        {
            get
            {
                if (_currentSelectedControlCollection == null)
                    _currentSelectedControlCollection = new List<System.Windows.Controls.Control>();
                return _currentSelectedControlCollection;
            }
        }

        //bool ctrlKeyIsPress;
        public bool CtrlKeyIsPress
        {
            get
            {
                return (Keyboard.Modifiers == ModifierKeys.Control);
                //return ctrlKeyIsPress;
            }
        }

        public bool IsContainerRefresh { get; set; }

        #endregion

        #region Variables

        private WSDesignerSoapClient _service = Glo.GetDesignerServiceInstance();
        private PageEditType editType = PageEditType.None;
        private Point mousePosition;
        private bool trackingMouseMove = false;
        private System.Windows.Threading.DispatcherTimer _doubleClickTimer;
        private Canvas _gridLinesContainer;
        private Rectangle temproaryEllipse;
        private DataSet workListDataSet;


        #endregion
        
        #region 加载流程相关
        private void getFlow(string flowID)
        {
            this.FlowID = flowID;
            _Service.RunSQLReturnTableAsync(
                "select nodeid,Name,X,Y,nodepostype,HisToNDs from wf_node where fk_flow=" + flowID, false);
            _Service.RunSQLReturnTableCompleted += _service_RunSQLReturnTableCompleted;
        }

        private void _service_RunSQLReturnTableCompleted(object sender, RunSQLReturnTableCompletedEventArgs e)
        {
            var ds = new DataSet();
            ds.FromXml(e.Result);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                FlowNode flowNode = null;
                if (dr["nodepostype"].ToString() == "0")
                {
                    flowNode = new FlowNode((IContainer)this, FlowNodeType.INITIAL);
                }
                else if (dr["nodepostype"].ToString() == "2")
                {
                    flowNode = new FlowNode((IContainer)this, FlowNodeType.COMPLETION);
                }
                else
                {
                    flowNode = new FlowNode((IContainer)this, FlowNodeType.INTERACTION);
                }
                flowNode.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
                flowNode.FlowID = FlowID;
                flowNode.FlowNodeID = dr["nodeid"].ToString();
                flowNode.FlowNodeName = dr["Name"].ToString();
                double x = double.Parse(dr["X"]);
                double y = double.Parse(dr["Y"]);
                if (x < 50)
                    x = 50;
                if (x > 1190)
                {
                    x = 1190;
                }
                if (y < 30)
                    y = 30;
                if (y > 770)
                    y = 770;
                flowNode.CenterPoint = new Point(x, y);
                AddFlowNode(flowNode);
            }
            _Service.GetLablesAsync(FlowID);
            _Service.GetLablesCompleted += _service_GetLablesCompleted;

            _Service.GetDirectionAsync(FlowID);
            _Service.GetDirectionCompleted += _service_GetDirectionCompleted;

            SaveChange(HistoryType.New);
            _Service.RunSQLReturnTableCompleted -= _service_RunSQLReturnTableCompleted;
        }

        void _Service_GetDTOfWorkListCompleted(object sender, WF.WS.GetDTOfWorkListCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                return;
            }
            try
            {
                workListDataSet = new DataSet();
                workListDataSet.FromXml(e.Result);

                getFlow(FK_Flow);

            }
            catch (Exception exception)
            {
                MessageBox.Show("在取得轨迹信息时出错，错误信息为：\n"  + exception.Message);
            }
        }

        private void _service_GetDirectionCompleted(object sender, GetDirectionCompletedEventArgs e)
        {
            var ds = new DataSet();
            ds.FromXml(e.Result);

            foreach (FlowNode bfn in FlowNodeCollections)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (bfn.FlowNodeID == dr["Node"].ToString())
                    {
                        foreach (FlowNode efn in FlowNodeCollections)
                        {
                            if (efn.FlowNodeID == dr["ToNode"].ToString())
                            {
                                var d = new Direction((IContainer)this);
                                d.FlowID = FlowID;
                                d.BeginFlowNode = bfn;
                                d.EndFlowNode = efn;
                                AddDirection(d);
                            }
                        }
                    }
                }
            }
            
            _Service.GetDirectionCompleted -= _service_GetDirectionCompleted;
        }

        private void _service_GetLablesCompleted(object sender, GetLablesCompletedEventArgs e)
        {
            var ds = new DataSet();
            ds.FromXml(e.Result);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var nodeLabel = new NodeLabel((IContainer)this);
                nodeLabel.LabelName = dr["Name"].ToString();
                nodeLabel.Position = new Point(double.Parse(dr["X"].ToString()), double.Parse(dr["Y"].ToString()));
                nodeLabel.LableID = dr["MyPK"].ToString();

                AddLabel(nodeLabel);
            }
            _Service.GetLablesCompleted -= _service_GetLablesCompleted;
        }

        public void AddDirection(Direction r)
        {
            r.Worklist(workListDataSet);
            if (!cnsDesignerContainer.Children.Contains(r))
            {
                cnsDesignerContainer.Children.Add(r);
                r.Container = this;

            }
            if (!DirectionCollections.Contains(r))
            {
                DirectionCollections.Add(r);
            }
        }

        public void AddLabel(NodeLabel l)
        {
            if (!cnsDesignerContainer.Children.Contains(l))
            {
                cnsDesignerContainer.Children.Add(l);
            }
            if (!LableCollections.Contains(l))
            {
                LableCollections.Add(l);
            }
        }

        public void AddFlowNode(FlowNode a)
        {
            if (!cnsDesignerContainer.Children.Contains(a))
            {
                cnsDesignerContainer.Children.Add(a);

                a.Container = this;
                a.FlowID = FlowID;
                if (a.Type != FlowNodeType.STATIONODE)
                {
                    a.Worklist(workListDataSet);
                }
            }
            if (!FlowNodeCollections.Contains(a))
            {
                FlowNodeCollections.Add(a);
            }
        }
        
        #endregion

        #region 菜单相关
        public void ShowFlowNodeContentMenu(FlowNode a, object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        public void ShowLabelContentMenu(NodeLabel l, object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        public void ShowDirectionContentMenu(Direction r, object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// 双击节点后要做的事件
        /// </summary>
        /// <param name="a">节点</param>
        public void ShowFlowNodeSetting(FlowNode a)
        {
            ///todo 周朋说这里要弹出一个窗口。不过url还没定
        }
        #endregion

        private void Content_Resized(object sender, EventArgs e)
        {
            cnsDesignerContainer.Width = Application.Current.Host.Content.ActualWidth;
            cnsDesignerContainer.Height = Application.Current.Host.Content.ActualHeight;
            SetGridLines();
        }


        public void SetGridLines()
        {
            GridLinesContainer.Children.Clear();
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(255, 160, 160, 160);
            //  brush.Color = Color.FromArgb(255, 255, 255, 255);
            double thickness = 0.3;
            double top = 0;
            double left = 0;

            double width = cnsDesignerContainer.Width;
            double height = cnsDesignerContainer.Height;

            double stepLength = 40;

            double x, y;
            x = left + stepLength;
            y = top;

            while (x < width + left)
            {
                Line line = new Line();
                line.X1 = x;
                line.Y1 = y;
                line.X2 = x;
                line.Y2 = y + height;


                line.Stroke = brush;
                line.StrokeThickness = thickness;
                line.Stretch = Stretch.Fill;
                GridLinesContainer.Children.Add(line);
                x += stepLength;
            }


            x = left;
            y = top + stepLength;

            while (y < height + top)
            {
                Line line = new Line();
                line.X1 = x;
                line.Y1 = y;
                line.X2 = x + width;
                line.Y2 = y;


                line.Stroke = brush;
                line.Stretch = Stretch.Fill;
                line.StrokeThickness = thickness;
                GridLinesContainer.Children.Add(line);
                y += stepLength;
            }
        }

        public bool Contains(UIElement uie)
        {
            return cnsDesignerContainer.Children.Contains(uie);
        }

        public CheckResult CheckSave()
        {
            CheckResult cr = new CheckResult();
            cr.IsPass = true;
            return cr;
        }

        public void ShowMessage(string message)
        {
           
        }

        public void SaveChange(HistoryType action)
        {
            
        }

        private void DoubleClick_Timer(object sender, EventArgs e)
        {
            _doubleClickTimer.Stop();
        }


        #region 鼠标操作相关
        public void AddSelectedControl(System.Windows.Controls.Control uc)
        {
            if (!CurrentSelectedControlCollection.Contains(uc))
                CurrentSelectedControlCollection.Add(uc);
        }

        public void RemoveSelectedControl(System.Windows.Controls.Control uc)
        {
            if (CurrentSelectedControlCollection.Contains(uc))
                CurrentSelectedControlCollection.Remove(uc);
        }

        public void SetWorkFlowElementSelected(System.Windows.Controls.Control uc, bool isSelected)
        {
            if (isSelected)
                AddSelectedControl(uc);
            else
                RemoveSelectedControl(uc);
            if (!CtrlKeyIsPress)
                ClearSelectFlowElement(uc);
        }

        public void ClearSelectFlowElement(System.Windows.Controls.Control uc)
        {
            if (CurrentSelectedControlCollection == null || CurrentSelectedControlCollection.Count == 0)
                return;

            int count = CurrentSelectedControlCollection.Count;
            for (int i = 0; i < count; i++)
            {
                ((IElement)CurrentSelectedControlCollection[i]).IsSelectd = false;
            }
            CurrentSelectedControlCollection.Clear();
            if (uc != null)
            {
                ((IElement)uc).IsSelectd = true;
                AddSelectedControl(uc);
            }
            mouseIsInContainer = true;
        }

        public void MoveControlCollectionByDisplacement(double x, double y, UserControl uc)
        {
            if (CurrentSelectedControlCollection == null || CurrentSelectedControlCollection.Count == 0)
                return;

            FlowNode selectedFlowNode = null;
            Direction selectedDirection = null;
            NodeLabel selectedLabel = null;
            if (uc is FlowNode)
                selectedFlowNode = uc as FlowNode;

            if (uc is Direction)
                selectedDirection = uc as Direction;
            if (uc is NodeLabel)
                selectedLabel = uc as NodeLabel;

            FlowNode a = null;
            Direction r = null;
            NodeLabel l = null;
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {
                if (CurrentSelectedControlCollection[i] is FlowNode)
                {
                    a = CurrentSelectedControlCollection[i] as FlowNode;
                    if (a == selectedFlowNode)
                        continue;
                    a.SetPositionByDisplacement(x, y);
                }
                if (CurrentSelectedControlCollection[i] is NodeLabel)
                {
                    l = CurrentSelectedControlCollection[i] as NodeLabel;
                    if (l == selectedLabel)
                        continue;
                    l.SetPositionByDisplacement(x, y);
                }
                if (CurrentSelectedControlCollection[i] is Direction)
                {
                    r = CurrentSelectedControlCollection[i] as Direction;
                    if (r == selectedDirection)
                        continue;
                    r.SetPositionByDisplacement(x, y);
                }
            }

            //for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            //{

            //    if (CurrentSelectedControlCollection[i] is Direction)
            //    {
            //        r = CurrentSelectedControlCollection[i] as Direction;
            //        if (r == selectedDirection)
            //            continue;
            //        r.SetPositionByDisplacement(x, y); 
            //    }
            //}
        }

        private void Container_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_doubleClickTimer.IsEnabled)
            {
                _doubleClickTimer.Stop();
                if (!string.IsNullOrEmpty(FlowID))
                {
                    //FlowNode a = new FlowNode((IContainer)this, FlowNodeType.INTERACTION);
                    //a.FlowNodeName = Text.NewFlowNode + NextNewFlowNodeIndex.ToString();
                    //Point p = e.GetPosition(this);

                    //a.CenterPoint = new Point(p.X - this.Left, p.Y - this.Top);
                    //a.IsSelectd = true;
                    //this.AddFlowNode(a);
                    //_Service.DoNewNodeAsync(FlowID, (int)a.CenterPoint.X, (int)a.CenterPoint.Y);
                }
            }
            else
            {
                _doubleClickTimer.Start();

                ClearSelectFlowElement(null);

                FrameworkElement element = sender as FrameworkElement;
                mousePosition = e.GetPosition(element);
                trackingMouseMove = true;
            }
        }

        private void Container_MouseMove(object sender, MouseEventArgs e)
        {
            if (trackingMouseMove)
            {
                FrameworkElement element = sender as FrameworkElement;
                Point beginPoint = mousePosition;
                Point endPoint = e.GetPosition(element);

                if (temproaryEllipse == null)
                {
                    temproaryEllipse = new Rectangle();


                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Color.FromArgb(255, 234, 213, 2);
                    temproaryEllipse.Fill = brush;
                    temproaryEllipse.Opacity = 0.2;

                    brush = new SolidColorBrush();
                    brush.Color = Color.FromArgb(255, 0, 0, 0);
                    temproaryEllipse.Stroke = brush;
                    temproaryEllipse.StrokeMiterLimit = 2.0;

                    cnsDesignerContainer.Children.Add(temproaryEllipse);
                }

                if (endPoint.X >= beginPoint.X)
                {
                    if (endPoint.Y >= beginPoint.Y)
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, beginPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, beginPoint.X);
                    }
                    else
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, endPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, beginPoint.X);
                    }
                }
                else
                {
                    if (endPoint.Y >= beginPoint.Y)
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, beginPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, endPoint.X);
                    }
                    else
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, endPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, endPoint.X);
                    }
                }


                temproaryEllipse.Width = Math.Abs(endPoint.X - beginPoint.X);
                temproaryEllipse.Height = Math.Abs(endPoint.Y - beginPoint.Y);
            }
            else
            {
                if (CurrentTemporaryDirection != null)
                {
                    CurrentTemporaryDirection.CaptureMouse();
                    Point currentPoint = e.GetPosition(CurrentTemporaryDirection);
                    CurrentTemporaryDirection.EndPointPosition = currentPoint;

                    if (CurrentTemporaryDirection.BeginFlowNode != null)
                    {
                        CurrentTemporaryDirection.BeginPointPosition =
                            CurrentTemporaryDirection.GetResetPoint(currentPoint,
                                                                    CurrentTemporaryDirection.BeginFlowNode.CenterPoint,
                                                                    CurrentTemporaryDirection.BeginFlowNode,
                                                                    DirectionMoveType.Begin);
                    }
                }
            }
        }

        private void Container_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trackingMouseMove = false;
            if (CurrentTemporaryDirection != null)
            {
                CurrentTemporaryDirection.SimulateDirectionPointMouseLeftButtonUpEvent(DirectionMoveType.End,
                                                                                       CurrentTemporaryDirection, e);
                if (CurrentTemporaryDirection.EndFlowNode == null)
                {
                    CurrentTemporaryDirection.BeginFlowNode.CanShowMenu = false;

                    CurrentTemporaryDirection.CanShowMenu = false;
                    // this.RemoveDirection(CurrentTemporaryDirection);
                    CurrentTemporaryDirection.Delete();
                }
                else
                {
                    CurrentTemporaryDirection.CanShowMenu = false;
                    CurrentTemporaryDirection.BeginFlowNode.CanShowMenu = false;
                    CurrentTemporaryDirection.EndFlowNode.CanShowMenu = true;
                    CurrentTemporaryDirection.IsTemporaryDirection = false;
                    CurrentTemporaryDirection.IsSelectd = false;
                    RemoveSelectedControl(CurrentTemporaryDirection);
                    SaveChange(HistoryType.New);
                }
                CurrentTemporaryDirection.ReleaseMouseCapture();
                CurrentTemporaryDirection = null;
            }

            FrameworkElement element = sender as FrameworkElement;
            mousePosition = e.GetPosition(element);
            if (temproaryEllipse != null)
            {
                double width = temproaryEllipse.Width;
                double height = temproaryEllipse.Height;

                if (width > 10 && height > 10)
                {
                    Point p = new Point();
                    p.X = (double)temproaryEllipse.GetValue(Canvas.LeftProperty);
                    p.Y = (double)temproaryEllipse.GetValue(Canvas.TopProperty);

                    FlowNode a = null;
                    Direction r = null;
                    NodeLabel l = null;
                    foreach (UIElement uie in cnsDesignerContainer.Children)
                    {
                        if (uie is FlowNode)
                        {
                            a = uie as FlowNode;
                            if (p.X < a.CenterPoint.X && a.CenterPoint.X < p.X + width
                                && p.Y < a.CenterPoint.Y && a.CenterPoint.Y < p.Y + height)
                            {
                                AddSelectedControl(a);
                                a.IsSelectd = true;
                            }
                        }
                        if (uie is NodeLabel)
                        {
                            l = uie as NodeLabel;
                            if (p.X < l.Position.X && l.Position.X < p.X + width
                                && p.Y < l.Position.Y && l.Position.Y < p.Y + height)
                            {
                                AddSelectedControl(a);
                                l.IsSelectd = true;
                            }
                        }
                        if (uie is Direction)
                        {
                            r = uie as Direction;

                            Point ruleBeginPointPosition = r.BeginPointPosition;
                            Point ruleEndPointPosition = r.EndPointPosition;

                            if (p.X < ruleBeginPointPosition.X && ruleBeginPointPosition.X < p.X + width
                                && p.Y < ruleBeginPointPosition.Y && ruleBeginPointPosition.Y < p.Y + height
                                &&
                                p.X < ruleEndPointPosition.X && ruleEndPointPosition.X < p.X + width
                                && p.Y < ruleEndPointPosition.Y && ruleEndPointPosition.Y < p.Y + height
                                )
                            {
                                AddSelectedControl(r);
                                r.IsSelectd = true;
                            }
                        }
                    }
                }
                cnsDesignerContainer.Children.Remove(temproaryEllipse);
                temproaryEllipse = null;
            }
        }

        private void Container_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseIsInContainer = true;
        }

        private void Container_MouseLeave(object sender, MouseEventArgs e)
        {
            mouseIsInContainer = false;
        }

        private void cnsDesignerContainer_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mouseIsInContainer)
            {
                e.GetPosition(this);
            }
            e.Handled = true;
        }
        
        #endregion
       
        #region IContainer 成员，但是不需要实现

        public void SetProper(string lang, string dotype, string fk_flow, string node1, string node2, string title)
        {
            throw new NotImplementedException();
        }
        public void CopySelectedControlToMemory(System.Windows.Controls.Control currentControl)
        {
            throw new NotImplementedException();
        }

        public void UpdateSelectedControlToMemory(System.Windows.Controls.Control currentControl)
        {
            throw new NotImplementedException();
        }

        public void UpDateSelectedNode(System.Windows.Controls.Control currentControl)
        {
            throw new NotImplementedException();
        }

        public void DeleteSeletedControl()
        {
            throw new NotImplementedException();
        }

        public void RemoveFlowNode(FlowNode a)
        {
            throw new NotImplementedException();
        }

        public void RemoveLabel(NodeLabel l)
        {
            throw new NotImplementedException();
        }

        public void ShowDirectionSetting(Direction r)
        {
            throw new NotImplementedException();
        }

        public void PreviousAction()
        {
            throw new NotImplementedException();
        }

        public void NextAction()
        {
            throw new NotImplementedException();
        }
        public void RemoveDirection(Direction r)
        {
            throw new NotImplementedException();
        }

        public void LoadFromXmlString(string xml)
        {
            throw new NotImplementedException();
        }

        public string ToXmlString()
        {
            throw new NotImplementedException();
        }

        public void PastMemoryToContainer()
        {
            throw new NotImplementedException();
        }

        public void AddLabel(int x, int y)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}