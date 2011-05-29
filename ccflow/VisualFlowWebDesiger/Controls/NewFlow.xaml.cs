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

namespace WF.Controls
{
    public partial class NewFlow : ChildWindow
    {
        static EndpointAddress address = new EndpointAddress("http://localhost/VisualFlow/Service.svc");
        //new Uri(Application.Current.Host.Source, "/Flow/(S(f3j3qsoyuqidwhf0inzuk20z))/DataService.svc"));
        WebServiceSoapClient _service = new WebServiceSoapClient();//new BasicHttpBinding(), address);
        public string No{get;set;}
        public string FK_FlowSort { get; set; }
        public NewFlow()
        {
            InitializeComponent();
        }

  public NewFlow(string FK_FlowSort) :this()
        {
            this.FK_FlowSort = FK_FlowSort;
          
        }
    
       
      
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
         //_service.GetDataAsync(1);
            _service.DoAsync("NewFlow", FK_FlowSort, true);
            _service.DoCompleted += new EventHandler<DoCompletedEventArgs>(_service_DoCompleted);
      //  _service.Do2Completed += new EventHandler<Do2CompletedEventArgs>(_service_Do2Completed);
            this.DialogResult = true;

        }

        void _service_DoCompleted(object sender, DoCompletedEventArgs e)
        {
            this.No = e.Result;
        }

      

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

