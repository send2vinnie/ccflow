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
using FreeFrm.FF;
using Silverlight;

namespace FreeFrm
{
    public partial class CopyFrm : ChildWindow
    {
        public CopyFrm()
        {
            InitializeComponent();
            string sql = "SELECT NodeID ,Name, Step FROM WF_Node WHERE FK_Flow='" + Glo.FK_Flow + "'";
            FreeFrmSoapClient da = new FF.FreeFrmSoapClient();
            da.RunSQLReturnTableAsync(sql);
            da.RunSQLReturnTableCompleted += new EventHandler<RunSQLReturnTableCompletedEventArgs>(da_RunSQLReturnTableCompleted);
        }
        void da_RunSQLReturnTableCompleted(object sender, RunSQLReturnTableCompletedEventArgs e)
        {
            this.listBox1.Items.Clear();
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ListBoxItem lb = new ListBoxItem();
                lb.Content = "Step:" + dr["Step"] + " " + dr["Name"].ToString();
                lb.Tag = "ND" + dr["NodeID"];
                if (Glo.FK_MapData == lb.Tag.ToString())
                    continue;
                this.listBox1.Items.Add(lb);
            }
            this.listBox1.SelectedIndex = 0;
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("您确定要执行吗？", "Note", MessageBoxButton.OKCancel) 
                == MessageBoxResult.Cancel)
                return;

            ListBoxItem lb = this.listBox1.SelectedItem as ListBoxItem;
            FreeFrmSoapClient da = new FreeFrmSoapClient();
            da.CopyFrmAsync(lb.Tag.ToString(), Glo.FK_MapData);
            da.CopyFrmCompleted += new EventHandler<CopyFrmCompletedEventArgs>(da_CopyFrmCompleted);
        }
        void da_CopyFrmCompleted(object sender, CopyFrmCompletedEventArgs e)
        {
            MessageBox.Show(e.Result);
            this.DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

