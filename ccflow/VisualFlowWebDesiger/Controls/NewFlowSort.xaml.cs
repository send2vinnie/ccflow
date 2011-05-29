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
using System.ServiceModel;
using WF.DataServiceReference;
using Ccflow.Web.UI.Control.Workflow.Designer;

namespace WF.Controls
{
    public partial class NewFlowSort : ChildWindow
    {
     
        public string No { get; set; }
        public string FK_FlowSort { get; set; }
        public NewFlowSort(Designers contaniner)
            : this()
        {
           
            this._container = contaniner;
        }
        public NewFlowSort() 
        {  InitializeComponent();
        
        }
        Designers _container;
        public Designers Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Container._Service.DoAsync("NewFlowSort", txtFlowNodeName.Text, true);
            Container._Service.DoCompleted += new EventHandler<DoCompletedEventArgs>(_service_DoCompleted);
            Container._Service.GetFlowSortAsync();
           
            this.DialogResult = true;
        }

        void _service_DoCompleted(object sender, DoCompletedEventArgs e)
        {
          this.No=  e.Result;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

