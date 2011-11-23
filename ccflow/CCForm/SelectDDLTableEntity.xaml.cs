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

namespace CCForm
{
    public partial class SelectDDLTableEntity : ChildWindow
    {
        public SelectDDLTableEntity()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.OKButton.Content.ToString() == "创建")
            {

                FF.CCFormSoapClient fc = Glo.GetCCFormSoapClientServiceInstance();
                fc.DoTypeAsync("CreateTable", this.TB_EnName.Text, this.TB_CHName.Text, null, null, null, null);
                fc.DoTypeCompleted += new EventHandler<FF.DoTypeCompletedEventArgs>(fc_DoTypeCompleted);
                return;
            }

            FF.CCFormSoapClient ff = Glo.GetCCFormSoapClientServiceInstance();
            ff.RunSQLsAsync("UPDATE Sys_SFTable SET NAME='" + this.TB_CHName.Text + "' WHERE NO='" + this.TB_EnName.Text + "'");
            ff.RunSQLsCompleted += new EventHandler<FF.RunSQLsCompletedEventArgs>(ff_RunSQLsCompleted);
        }

        void ff_RunSQLsCompleted(object sender, FF.RunSQLsCompletedEventArgs e)
        {
            /*开始执行保存数据.*/
            MessageBox.Show("表或视图数据保存功能未实现，您可以打开数据库直接修改表或视图 " + this.TB_EnName.Text + "。");
            this.DialogResult = true;
        }

        void fc_DoTypeCompleted(object sender, FF.DoTypeCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                MessageBox.Show(e.Result);
                return;
            }

            this.TB_EnName.IsEnabled = false;
            this.tabItem2.IsEnabled = false;
            this.OKButton.Content = "保存";
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

