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
using WF.WS;

namespace Ccflow.Web.UI.Control.Workflow.Designer
{
    public enum MergePictureRepeatDirection
    {
        Vertical = 0,
        Horizontal,
        None
    }

    public enum FlowNodeType
    {
        INITIAL,
        INTERACTION,
        COMPLETION,
        AND_MERGE,
        AND_BRANCH ,
        STATIONODE,
        AUTOMATION,
        DUMMY,
        OR_BRANCH,
        OR_MERGE,
        SUBPROCESS,
        VOTE_MERGE,
        
    }
    
    public enum WorkFlowElementType
    {
        FlowNode = 0,
        Direction,
        Label
    }

    public enum PageEditType
    {
        Add = 0,
        Modify,
        None
    }

    public enum DirectionLineType
    {
        Line = 0,
        Polyline
    }

    public enum HistoryType
    {
        New,
        Next,
        Previous
    } ;

    public class CheckResult
    {
        private bool isPass = true;

        public bool IsPass
        {
            get { return isPass; }
            set { isPass = value; }
        }

        private string message = "";

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }

    public class UserStation
    {
        private bool isPass = true;

        public bool IsPass
        {
            get { return isPass; }
            set { isPass = value; }
        }

        private bool isSel = true;

        public bool IsSel
        {
            get { return isSel; }
            set { isSel = value; }
        }

        private string message = "";

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }

    public interface IElement
    {
        CheckResult CheckSave();
        UserStation Station();
        string ToXmlString();
        void LoadFromXmlString(string xmlString);
        void ShowMessage(string message);
        WorkFlowElementType ElementType { get; }

        PageEditType EditType { get; set; }

        bool IsSelectd { get; set; }
        IContainer Container { get; set; }
        void Delete();
        void UpperZIndex();
        bool IsDeleted { get; }
        void Zoom(double zoomDeep);
        void worklist();
        void Edit();
        
    }
}