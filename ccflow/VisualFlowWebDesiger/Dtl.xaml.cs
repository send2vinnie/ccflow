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
using System.IO.IsolatedStorage;
using System.Xml.Linq;
using System.IO;
using Silverlight;
using WF.DataServiceReference;

namespace WF
{
    public partial class Dtl : UserControl
    {
        WebServiceSoapClient _service = new WebServiceSoapClient();//new BasicHttpBinding(), address);
        public Dtl()
        {
            InitializeComponent();
            BindIt();
        }
        public void BindIt()
        {
            _service.GetNameAsync("sdsd");
            _service.GetNameCompleted += new System.EventHandler<GetNameCompletedEventArgs>(_service_GetNameCompleted);

            //_service.RunSQLReturnTablePengAsync("SELECT * FROM Sys_MapAttr FK_MapData='" + Glo.FK_MapData+"'");
            //_service.RunSQLReturnTablePengCompleted += new EventHandler<RunSQLReturnTablePengCompletedEventArgs>(_service_RunSQLReturnTableCompleted);
        }

        void _service_GetNameCompleted(object sender, GetNameCompletedEventArgs e)
        {
            MessageBox.Show(e.Result);
        }

        //void _service_RunSQLReturnTableCompleted(object sender, RunSQLReturnTablePengCompletedEventArgs e)
        //{
        //    DataSet ds = new DataSet();
        //    ds.FromXml(e.Result);

        //    MessageBox.Show(e.Result);
        //}
    }
}
