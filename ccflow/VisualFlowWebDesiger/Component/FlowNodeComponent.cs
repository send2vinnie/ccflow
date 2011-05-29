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

namespace Ccflow.Web.Component.Workflow 
{
    public class FlowNodeComponent
    {

        string _subFlow;
        public string SubFlow
        {
            get
            {
                return _subFlow;
            }
            set
            {
                _subFlow = value;
            }
        }

        string _repeatDirection = "Horizontal";
        public string RepeatDirection
        {
            get
            {
                return _repeatDirection;
            }
            set
            {
                _repeatDirection = value;
            }
        }

        string flowID;
        public string FlowID
        {
            get
            {
                if (string.IsNullOrEmpty(flowID))
                {
                    flowID = Guid.NewGuid().ToString();
                }
                return flowID;
            }
            set
            {
               flowID = value;
            }

        }
         string flowNodeID;

        public string FlowNodeID
        {
            get
            {
                return flowNodeID;
            }
            set
            {
                flowNodeID = value;
            }
        } 

         string flowNodeName;

        public string FlowNodeName
        {
            get
            {
                return flowNodeName;
            }
            set
            {
                flowNodeName = value;
            }
        } 
         string flowNodeType; 
        public string FlowNodeType
        {
            get
            {
                return flowNodeType;
            }
            set
            {
                flowNodeType = value;
            }
        }
    }
}
