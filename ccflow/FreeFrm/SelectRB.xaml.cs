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
using FreeFrm.FF;

namespace FreeFrm
{
    public partial class SelectRB : ChildWindow
    {
        public SelectRB()
        {
            InitializeComponent();
            this.listBox1.SelectionMode = SelectionMode.Single;
            this.listBox2.SelectionMode = SelectionMode.Single;
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
            this.listBox2.Items.Clear();

            string sql = "SELECT No,Name,CfgVal FROM Sys_EnumMain";
            FreeFrmSoapClient da = new FreeFrmSoapClient();
            da.RunSQLReturnTableAsync(sql);
            da.RunSQLReturnTableCompleted += new EventHandler<FF.RunSQLReturnTableCompletedEventArgs>(da_RunSQLReturnTableCompleted);
            this.listBox1.SelectionChanged += new SelectionChangedEventHandler(listBox1_SelectionChanged);
        }

        void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem li = e.AddedItems[0] as ListBoxItem;
            string s = li.Tag as string;
            string[] strs = s.Split('@');
            this.listBox2.Items.Clear();
            foreach (string str in strs)
            {
                if (string.IsNullOrEmpty(str))
                    continue;
                ListBoxItem dd = new ListBoxItem();
                dd.Content = str;
                this.listBox2.Items.Add(dd);
            }

            this.EditItem(li);
        }

        void da_RunSQLReturnTableCompleted(object sender, FF.RunSQLReturnTableCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ListBoxItem item = new ListBoxItem();
                item.Tag = dr["CfgVal"].ToString();
                item.Content = dr["No"] + ":" + dr["Name"];
                this.listBox1.Items.Add(item);
            }
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        
        private void Btn_New_Click(object sender, RoutedEventArgs e)
        {
            this.TB_No.Text = "";
            this.TB_Name.Text = "";
            this.tabItem2.IsSelected = true;
            this.tabItem2.Header = "新建";
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem li = this.listBox1.SelectedItem as ListBoxItem;
            this.EditItem(li);
            this.tabItem2.IsSelected = true;
        }
        public void EditItem(ListBoxItem li)
        {
            string myStr = li.Content as string;
            this.TB_No.Text = myStr.Substring(0, myStr.IndexOf(':'));
            this.TB_Name.Text = myStr.Substring(myStr.IndexOf(':') + 1);
            this.tabItem2.Header = "编辑:" + this.TB_Name.Text;

            #region 设置布局.
            if (this.grid1.ColumnDefinitions.Count == 0)
            {
                this.grid1.ColumnDefinitions.Clear();
                this.grid1.RowDefinitions.Clear();

                ColumnDefinition cd = new ColumnDefinition();
                this.grid1.ColumnDefinitions.Add(cd);
                cd = new ColumnDefinition();
                this.grid1.ColumnDefinitions.Add(cd);
                for (int i = 0; i < 20; i++)
                {
                    RowDefinition rd = new RowDefinition();
                    this.grid1.RowDefinitions.Add(rd);

                    TextBlock tbk = new TextBlock();
                    tbk.Name = "TBK_" + i;
                    tbk.SetValue(Grid.ColumnProperty, 0);
                    tbk.SetValue(Grid.RowProperty, i);
                    this.grid1.Children.Add(tbk);

                    TextBox tb = new TextBox();
                    tb.Name = "TB_" + i;
                    tb.SetValue(Grid.ColumnProperty, 1);
                    tb.SetValue(Grid.RowProperty, i);
                    this.grid1.Children.Add(tb);
                }
            }
            #endregion 设置布局.


            string s = li.Tag as string;
            string[] strs = s.Split('@');
            this.listBox2.Items.Clear();
            foreach (string str in strs)
            {
                if (string.IsNullOrEmpty(str))
                    continue;

                this.listBox2.Items.Add(str);
                string[] keyVals = str.Split('=');
                int intKey = int.Parse(keyVals[0]);

#warning 找不到里面的控件???
                foreach (Control ctl in this.grid1.Children)
                {
                    if (ctl.Name == "TB_" + intKey)
                    {
                        TextBox tb = ctl as TextBox;
                        tb.Text = keyVals[1];
                    }
                }

                //string[] keyVals = str.Split('=');
                //TextBlock tbk = new TextBlock();
                //tbk.Name = "TBK_" + keyVals[0];
                //tbk.SetValue(Grid.ColumnProperty, 0);
                //tbk.SetValue(Grid.RowProperty, int.Parse(keyVals[0]));
                //this.grid1.Children.Add(tbk);
                //TextBox tb = new TextBox();
                //tb.Name = "TB_" + keyVals[1];
                //tb.SetValue(Grid.ColumnProperty, 1);
                //tb.SetValue(Grid.RowProperty, keyVals[1]);
                //this.grid1.Children.Add(tb);
            }
        }
    }
}

