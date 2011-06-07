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

namespace FreeFrm
{
    public partial class SelectDDLTable : ChildWindow
    {
        public SelectDDLTable()
        {
            InitializeComponent();
        }
        protected override void OnOpened()
        {
            if (this.listBox1.Items.Count == 0)
            {
                this.BindData();
                this.tabItem1.IsSelected = true;
                this.tabItem2.Header = "新建";
            }
            base.OnOpened();
        }
        public void BindData()
        {
            this.listBox1.Items.Clear();

            string sql = "SELECT No,Name,TableDesc FROM Sys_SFTable";
            FF.FreeFrmSoapClient da = new FF.FreeFrmSoapClient();
            da.RunSQLReturnTableAsync(sql);
            da.RunSQLReturnTableCompleted += new EventHandler<FF.RunSQLReturnTableCompletedEventArgs>(da_RunSQLReturnTableCompleted);
            //  this.listBox1.SelectionChanged += new SelectionChangedEventHandler(listBox1_SelectionChanged);
        }
        void da_RunSQLReturnTableCompleted(object sender, FF.RunSQLReturnTableCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ListBoxItem item = new ListBoxItem();
                item.Tag = dr["No"].ToString();
                item.Content = dr["No"] + ":" + dr["Name"];
                this.listBox1.Items.Add(item);
            }
        }
        void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ListBoxItem li = e.AddedItems[0] as ListBoxItem;
            //string s = li.Tag as string;
            //string[] strs = s.Split('@');
            //this.listBox2.Items.Clear();
            //foreach (string str in strs)
            //{
            //    if (string.IsNullOrEmpty(str))
            //        continue;
            //    ListBoxItem dd = new ListBoxItem();
            //    dd.Content = str;
            //    this.listBox2.Items.Add(dd);
            //}
            //this.EditItem(li);
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

