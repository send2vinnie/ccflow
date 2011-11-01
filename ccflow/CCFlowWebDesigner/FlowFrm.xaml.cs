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

namespace WF
{
    public partial class FlowFrm : ChildWindow
    {
        public int FK_Node = 0;
        public string FK_Flow = "";
        public FlowFrm()
        {
            InitializeComponent();
        }
        protected override void OnOpened()
        {
            WS.WSDesignerSoapClient da = new WS.WSDesignerSoapClient();
            string sql = "SELECT * FROM Sys_MapData WHERE FK_Flow='"+this.FK_Flow+"'";
            sql += "@SELECT * FROM Sys_FrmNode  WHERE FK_Node='"+this.FK_Node+"'";
            da.RunSQLReturnTableSAsync(sql.Split('@'));
            da.RunSQLReturnTableSCompleted += new EventHandler<WS.RunSQLReturnTableSCompletedEventArgs>(da_RunSQLReturnTableSCompleted);
            base.OnOpened();
        }
        void da_RunSQLReturnTableSCompleted(object sender, WS.RunSQLReturnTableSCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);
             

        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

