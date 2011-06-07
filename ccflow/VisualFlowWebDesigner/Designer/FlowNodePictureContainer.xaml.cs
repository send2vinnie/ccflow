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
using Ccflow.Web.UI.Control.Workflow.Designer.Picture;
using Ccflow.Web.Component.Workflow;
using WF.Resources;
using System.ComponentModel;

namespace Ccflow.Web.UI.Control.Workflow.Designer
{
    public delegate void ColseAnimationCompletedDelegate(object sender, EventArgs e);

    public partial class FlowNodePictureContainer : UserControl, INotifyPropertyChanged
    {
        public double ContainerWidth
        {
            set
            {

                gridContainer.Width = value;
            }
            get
            {
                return gridContainer.Width;
            }
        }
        public double ContainerHeight
        {
            set
            {

                gridContainer.Height = value;
            }
            get
            {
                return gridContainer.Height;
            }
        }

       public  double PictureWidth
        {
            get
            {
                return (( IFlowNodePicture) currentPic).PictureWidth;
            }
            set
            {
                ((IFlowNodePicture)currentPic).PictureWidth = value;
            }
        }
       public double PictureHeight
       {
           get
           {
               return ((IFlowNodePicture)currentPic).PictureHeight;
           }
           set
           {
               ((IFlowNodePicture)currentPic).PictureHeight = value;
           }
       }

   public    UserControl currentPic;
       public FlowNodePictureContainer()
        {
            InitializeComponent();
        }
        public new SolidColorBrush Background
        {
            set
            {
                ((IFlowNodePicture)currentPic).Background = value;  
                 
            }
        }
        FlowNodeType type;
        public FlowNodeType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;


                picAUTOMATION.PictureVisibility = Visibility.Collapsed;
                picBegin.PictureVisibility = Visibility.Collapsed;
                picBRANCH.PictureVisibility = Visibility.Collapsed;
                picEnd.PictureVisibility = Visibility.Collapsed;
                picINTERACTION.PictureVisibility = Visibility.Collapsed;
                picMERGE.PictureVisibility = Visibility.Collapsed;
                picSTATION.PictureVisibility = Visibility.Collapsed;

                if (type == FlowNodeType.INTERACTION)
                {
                    currentPic = picINTERACTION;

                }
                else if (type == FlowNodeType.COMPLETION)
                {
                    currentPic = picEnd;


                }
                else if (type == FlowNodeType.INITIAL)
                {
                    currentPic = picBegin;


                }
                else if (type == FlowNodeType.AUTOMATION
                     || type == FlowNodeType.DUMMY
                    || type== FlowNodeType.SUBPROCESS)
                {
                    currentPic = picAUTOMATION ;


                }
                else if (type == FlowNodeType.AND_BRANCH
                    || type == FlowNodeType.OR_BRANCH)
                {
                    currentPic = picBRANCH;


                }
                else if (type == FlowNodeType.AND_MERGE
                    || type == FlowNodeType.OR_MERGE
                    || type == FlowNodeType.VOTE_MERGE)
                {
                    currentPic = picMERGE ;


                }
                else if (type == FlowNodeType.STATIONODE)
                {
                    currentPic = picSTATION;
                }
                ((IFlowNodePicture)currentPic).PictureVisibility = Visibility.Visible;
                 
            }
        }

     public   string NodeName
        {
             get{ return txtFlowNodeName.Text; }
            set {
                RaisePropertyChanging(new PropertyChangedEventArgs("NodeName"));
                txtFlowNodeName.Text = value;
                tbNodeName.Text = value; }
        }
        public void SetSelectedColor()
        {
            ((IFlowNodePicture)currentPic).SetSelectedColor();

        }
        public void SetWarningColor()
        {
            ((IFlowNodePicture)currentPic).SetWarningColor();

        }
        public void ResetInitColor()
        {
            ((IFlowNodePicture)currentPic).ResetInitColor();
        }
        public Point GetPointOfIntersection(Point beginPoint, Point endPoint, DirectionMoveType type)
        {

            return new Point(0,0);
        }
        public PointCollection ThisPointCollection
        {
            get
            {
                return ((IFlowNodePicture)currentPic).ThisPointCollection;
            }
        }
        MergePictureRepeatDirection _repeatDirection;
        public MergePictureRepeatDirection RepeatDirection
        {
            get
            {
                if (Type == FlowNodeType.OR_MERGE
                    || Type == FlowNodeType.AND_MERGE
                    || Type == FlowNodeType.VOTE_MERGE)
                {

                    _repeatDirection=((MergeFlowNode)currentPic).RepeatDirection;
                }
                return _repeatDirection;
            }
            set
            { 
                _repeatDirection = value;
                if (Type == FlowNodeType.OR_MERGE
                    ||  Type == FlowNodeType.AND_MERGE
                    ||  Type == FlowNodeType.VOTE_MERGE)
                {

                    ((MergeFlowNode)currentPic).RepeatDirection = _repeatDirection;
                }
            }
        }

        private void tbNodeName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtFlowNodeName.Text = tbNodeName.Text;
        }

        private void tbNodeName_LostFocus(object sender, RoutedEventArgs e)
        {
            tbNodeName.Visibility = Visibility.Collapsed;
            txtFlowNodeName.Visibility = Visibility.Visible;
            
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        public event EventHandler<PropertyChangedEventArgs> PropertyChanging;
        protected virtual void RaisePropertyChanging(string prop)
        {
            RaisePropertyChanging(new PropertyChangedEventArgs(prop));
        }
        protected virtual void RaisePropertyChanging(PropertyChangedEventArgs e)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, e);
        }

        #endregion
    }
}
