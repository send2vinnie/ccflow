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
using Silverlight;
using WF.Controls;
using BP;
using WF;
using WF.WS;
namespace WF.Frm
{
    public class FlowForm
    {
        public string No { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string PTable { get; set; }
        public string DataBaseName { get; set; }
        public string Type { get; set; }
        public string IsReadOnly { get; set; }
        public string IsPrintable { get; set; }
    }
    public partial class FrmLib : ChildWindow
    {
        List<FlowForm> list = new List<FlowForm>();
        public FrmLib()
        {
            InitializeComponent();

            var client = Glo.GetDesignerServiceInstance();
            var sql = "select No,Name,PTable from Sys_MapData";
            client.RunSQLReturnTableCompleted += new EventHandler<RunSQLReturnTableCompletedEventArgs>(client_RunSQLReturnTableCompleted);
            client.RunSQLReturnTableAsync(sql, true);
        }

        void client_RunSQLReturnTableCompleted(object sender, RunSQLReturnTableCompletedEventArgs e)
        {
            var ds = new DataSet();
            ds.FromXml(e.Result);
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                var flowForm = new FlowForm
                {
                    DataBaseName = string.Empty,
                    No = dataRow["No"].ToString(),
                    Name = dataRow["Name"].ToString(),
                    PTable = dataRow["PTable"].ToString(),
                    //Type = formatFormType(dataRow["FormType"]),
                    //URL = dataRow["URL"]
                };
                list.Add(flowForm);
            }
            this.Grid1.ItemsSource = list;

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}

