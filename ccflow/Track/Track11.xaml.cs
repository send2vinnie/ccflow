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
using BP;
using WF.Resources;
using WF.WS;
using Silverlight;
using System.Windows.Browser;

namespace BP
{
    /// <summary>
    /// 主要用来显示流程轨迹
    /// </summary>
    public partial class Track : UserControl, IContainer
    {
        #region Constructs
        public void SaveChange(BP.HistoryType ty)
        {
        }
        public Track()
        {
            InitializeComponent();
            Application.Current.Host.Content.Resized += new EventHandler(Content_Resized);
            _doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
            _doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, SystemConst.DoubleClickTime);
            _doubleClickTimer.Tick += new EventHandler(DoubleClick_Timer);
        }
        /// <summary>
        /// 轨迹图
        /// </summary>
        /// <param name="fk_flow"></param>
        /// <param name="workid"></param>
        public Track(string fk_flow, string workid)
            : this()
        {
            this.FK_Flow = fk_flow;
            this.WorkID = workid;
            if (string.IsNullOrEmpty(workid) == false)
            {
                //如果传递过来workid 说明要显示轨图./
                WSDesignerSoapClient ws = BP.Glo.GetDesignerServiceInstance();
                ws.GetDTOfWorkListAsync(this.FK_Flow, workid);
                ws.GetDTOfWorkListCompleted += new EventHandler<GetDTOfWorkListCompletedEventArgs>(ws_GetDTOfWorkListCompleted);
            }
            else
            {
                this.GenerFlowChart(this.FK_Flow);
            }
        }

        void ws_GetDTOfWorkListCompleted(object sender, GetDTOfWorkListCompletedEventArgs e)
        {
            dsWorkFlow = new DataSet();
            dsWorkFlow.FromXml(e.Result);
            this.GenerFlowChart(this.FK_Flow);
        }
        #endregion

        #region 属性
        public bool IsMouseSelecting
        {
            get { return (temproaryEllipse != null); }
        }
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
        /// <summary>
        /// 方向
        /// </summary>
        public List<Direction> directionCollections;
        /// <summary>
        /// 方向
        /// </summary>
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
        /// <summary>
        /// 标签
        /// </summary>
        public List<NodeLabel> lableCollections;
        /// <summary>
        /// 标签
        /// </summary>
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

        /// <summary>
        /// 流程编号
        /// </summary>
        public string FK_Flow { get; set; }
        public Double ContainerWidth
        {
            get { return paint.Width; }
            set { paint.Width = value; }
        }

        public Double ContainerHeight
        {
            get { return paint.Height; }
            set { paint.Height = value; }
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
                    paint.Children.Add(temCan);
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
        public bool CtrlKeyIsPress
        {
            get
            {
                return (Keyboard.Modifiers == ModifierKeys.Control);
            }
        }
        public bool IsContainerRefresh { get; set; }
        #endregion

        #region 变量
        private PageEditType editType = PageEditType.None;
        private Point mousePosition;
        private bool trackingMouseMove = false;
        private System.Windows.Threading.DispatcherTimer _doubleClickTimer;
        private Canvas _gridLinesContainer;
        private Rectangle temproaryEllipse;
        private DataSet dsWorkFlow;
        private double DesignerHeight = 50;
        private double DesignerWdith = 50;
        #endregion 变量

        #region 加载流程相关
        private void GenerFlowChart(string fk_flow)
        {
            this.FK_Flow = fk_flow;
            string sqls = "";
            sqls += "SELECT NodeID,Name,X,Y,NodePosType,RunModel,HisToNDs FROM WF_Node WHERE FK_Flow='" + fk_flow + "'";
            sqls += "@SELECT MyPK,Name,FK_Flow,X,Y FROM WF_LabNote WHERE FK_Flow='" + fk_flow + "'";
            sqls += "@SELECT Node,ToNode FROM WF_Direction ";
            WSDesignerSoapClient ws = BP.Glo.GetDesignerServiceInstance();
            ws.RunSQLReturnTableSAsync(sqls);
            ws.RunSQLReturnTableSCompleted += new EventHandler<RunSQLReturnTableSCompletedEventArgs>(GenerFlowChart_RunSQLReturnTableSCompleted);
        }
        void GenerFlowChart_RunSQLReturnTableSCompleted(object sender, RunSQLReturnTableSCompletedEventArgs e)
        {
            var ds = new DataSet();
            ds.FromXml(e.Result);

            #region 画流程节点。
            DataTable dtNode = ds.Tables[0];
            foreach (DataRow dr in dtNode.Rows)
            {
                FlowNode flowNode = null;
                if (dr["NodePosType"].ToString() == "0") /*开始节点*/
                    flowNode = new FlowNode((IContainer)this, FlowNodeType.INITIAL);
                else if (dr["NodePosType"].ToString() == "2") /*结束节点*/
                    flowNode = new FlowNode((IContainer)this, FlowNodeType.COMPLETION);
                else /*中间节点*/
                    flowNode = new FlowNode((IContainer)this, FlowNodeType.INTERACTION);

                flowNode.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
                flowNode.FK_Flow = FK_Flow;
                flowNode.FlowNodeID = dr["NodeID"].ToString();
                flowNode.FlowNodeName = dr["Name"].ToString();
                double x = double.Parse(dr["X"]);
                double y = double.Parse(dr["Y"]);
                if (x < 50)
                    x = 50;
                if (x > 1190)
                    x = 1190;
                if (y < 30)
                    y = 30;
                if (y > 770)
                    y = 770;

                flowNode.CenterPoint = new Point(x, y);

                // 永远使设计器的宽和高为节点的最大值　
                if (y > DesignerHeight)
                    DesignerHeight = y;

                if (x > DesignerWdith)
                    DesignerWdith = x;

                AddFlowNode(flowNode);
            }
            #endregion 画流程节点。

            #region 生成标签.
            DataTable dtLabel = ds.Tables[1];
            foreach (DataRow dr in dtLabel.Rows)
            {
                var nodeLabel = new NodeLabel((IContainer)this);
                nodeLabel.LabelName = dr["Name"].ToString();
                nodeLabel.Position = new Point(double.Parse(dr["X"].ToString()), double.Parse(dr["Y"].ToString()));
                nodeLabel.LableID = dr["MyPK"].ToString();
                this.AddLabel(nodeLabel);
            }
            #endregion 生成标签.

            #region 生成方向.
            DataTable dtDir = ds.Tables[2];
            foreach (FlowNode bfn in FlowNodeCollections)
            {
                foreach (DataRow dr in dtDir.Rows)
                {
                    if (bfn.FlowNodeID == dr["Node"].ToString())
                    {
                        foreach (FlowNode efn in FlowNodeCollections)
                        {
                            if (efn.FlowNodeID == dr["ToNode"].ToString())
                            {
                                var d = new Direction((IContainer)this);
                                d.FK_Flow = FK_Flow;
                                d.BeginFlowNode = bfn;
                                d.EndFlowNode = efn;
                                AddDirection(d);
                            }
                        }
                    }
                }
            }
            #endregion 生成方向.

          //  SaveChange(HistoryType.New);
            Content_Resized(null, null);
        }
        /// <summary>
        /// 增加方向
        /// </summary>
        /// <param name="r"></param>
        public void AddDirection(Direction r)
        {
            r.Worklist(dsWorkFlow);
            if (paint.Children.Contains(r) == false)
            {
                paint.Children.Add(r);
                r.Container = this;
            }
            if (DirectionCollections.Contains(r) == false)
                DirectionCollections.Add(r);
        }
        /// <summary>
        /// 增加标签
        /// </summary>
        /// <param name="l"></param>
        public void AddLabel(NodeLabel l)
        {
            if (!paint.Children.Contains(l))
                paint.Children.Add(l);

            if (!LableCollections.Contains(l))
                LableCollections.Add(l);
        }
        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="a"></param>
        public void AddFlowNode(FlowNode a)
        {
            if (paint.Children.Contains(a)==false)
            {
                paint.Children.Add(a);

                a.Container = this;
                a.FK_Flow = FK_Flow;

                if (a.Type != FlowNodeType.STATIONODE)
                {
                    a.Worklist(dsWorkFlow);
                }
            }

            if (FlowNodeCollections.Contains(a) == false)
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
            MessageBox.Show("在施工中.");
            ///todo 周朋说这里要弹出一个窗口。不过url还没定
        }
        #endregion

        /// <summary>
        /// Content改变时重设设计器的大小， 设计器的大小取Content及最大节点值中的最大值 
        /// 20为假设的滚动条宽度。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Content_Resized(object sender, EventArgs e)
        {
            var contentHeight = Application.Current.Host.Content.ActualHeight - 20;
            var contentWidth = Application.Current.Host.Content.ActualWidth - 20;
            paint.Width = DesignerWdith > contentWidth
                                             ? DesignerWdith
                                             : contentWidth;
            paint.Height = DesignerHeight > contentHeight
                                              ? DesignerHeight
                                              : contentHeight;
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

            double width = paint.Width;
            double height = paint.Height;

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
            return paint.Children.Contains(uie);
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


        /// <summary>
        /// 屏蔽SL默认的右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}