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
using System.Collections.Generic;
using Ccflow.Web.Component.Workflow;
using WF.WS;

namespace Ccflow.Web.UI.Control.Workflow.Designer
{
    public interface IContainer
    {
        void ShowMessage(string message);
        List<FlowNode> FlowNodeCollections { get; }
        List<Direction> DirectionCollections { get; }
        List<NodeLabel> LableCollections { get; }
        string FlowID { get; set; }
        string FK_Flow { get; }
        string WorkID { get; }

        void AddFlowNode(FlowNode a);
        void RemoveFlowNode(FlowNode a);
        void RemoveLabel(NodeLabel l);
        void AddDirection(Direction r);
        void AddLabel(NodeLabel l);
        void AddLabel(int x, int y);
        void RemoveDirection(Direction r);
        int NextMaxIndex { get; }
        string ToXmlString();
        void LoadFromXmlString(string xmlString);
        PageEditType EditType { get; set; }

        void ShowFlowNodeSetting(FlowNode ac);
        void ShowDirectionSetting(Direction rc);
        Direction CurrentTemporaryDirection { get; set; }
        List<System.Windows.Controls.Control> CurrentSelectedControlCollection { get; }
        void AddSelectedControl(System.Windows.Controls.Control uc);
        void RemoveSelectedControl(System.Windows.Controls.Control uc);
        void SetWorkFlowElementSelected(System.Windows.Controls.Control uc, bool isSelect);
        void MoveControlCollectionByDisplacement(double x, double y, UserControl uc);
        bool CtrlKeyIsPress { get; }
        double ContainerWidth { get; set; }
        double ContainerHeight { get; set; }
        double ScrollViewerHorizontalOffset { get; set; }
        double ScrollViewerVerticalOffset { get; set; }
        void ShowFlowNodeContentMenu(FlowNode a, object sender, MouseButtonEventArgs e);
        void ShowLabelContentMenu(NodeLabel l, object sender, MouseButtonEventArgs e);
        void ShowDirectionContentMenu(Direction r, object sender, MouseButtonEventArgs e);
        void ClearSelectFlowElement(System.Windows.Controls.Control uc);
        void SaveChange(HistoryType action);
        int NextNewFlowNodeIndex { get; }
        int NextNewDirectionIndex { get; }
        int NextNewLabelIndex { get; }
        void CopySelectedControlToMemory(System.Windows.Controls.Control currentControl);
        void UpdateSelectedControlToMemory(System.Windows.Controls.Control currentControl);
        void UpDateSelectedNode(System.Windows.Controls.Control currentControl);
        void PastMemoryToContainer();
        void PreviousAction();
        void NextAction();
        List<System.Windows.Controls.Control> CopyElementCollectionInMemory { get; set; }
        Stack<string> WorkFlowXmlPreStack { get; }
        Stack<string> WorkFlowXmlNextStack { get; }

        void DeleteSeletedControl();
        bool IsMouseSelecting { get; }
        CheckResult CheckSave();


        bool Contains(UIElement uiel);
        bool IsContainerRefresh { get; set; }
        bool MouseIsInContainer { get; set; }
        void AlignBottom();
        void AlignRight();
        void AlignTop();
        void AlignLeft();
        WSDesignerSoapClient _Service { get; set; }
        void WinOpen(string url, string title);
        void SetProper(string lang, string dotype, string fk_flow, string node1, string node2, string title);
        void NewFlow();
        void SetGridLines();
        Canvas GridLinesContainer { get; }
        bool IsNeedSave { get; set; }
        bool IsSomeChildEditing { get; set; }
    }
}