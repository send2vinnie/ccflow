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
using System.IO;
using WF.DataServiceReference;
using Silverlight;
using System.Collections;
namespace WF.Controls
{
    public partial class FileUpLoad : ChildWindow
    {
        WebServiceSoapClient _service = new WebServiceSoapClient();
        public FileUpLoad()
        {
            InitializeComponent();
            _service.GetFlowSortAsync();
            _service.GetFlowSortCompleted += _service_GetFlowSortCompleted;
        }

        void _service_GetFlowSortCompleted(object sender, GetFlowSortCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);

            IList list = ds.Tables[0].GetBindableData(new Connector());

            if(list.Count > 0 )
            {
                cbxFlowSort.ItemsSource = list;
                cbxFlowSort.DisplayMemberPath = "Name";

                cbxFlowSort.SelectedIndex = 0;
            }

            _service.GetFlowSortCompleted -= _service_GetFlowSortCompleted;

        }
        OpenFileDialog dialog = new OpenFileDialog();
        private byte[] buffer;
        FileInfo file;
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

            if (buffer == null || buffer.Length <= 0 || file == null || cbxFlowSort.SelectedIndex == -1)
            {
                MessageBox.Show("请选择上传模板或模板类型", "提示", MessageBoxButton.OK);
                return;
            }

            //调用服务上传
            _service.UploadfileAsync(buffer, file.Name);
            _service.UploadfileCompleted += new EventHandler<UploadfileCompletedEventArgs>(_Service_UploadfileCompleted);

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void BtnUpLoad_Click(object sender, RoutedEventArgs e)
        {

            dialog.Filter = "Xml Files (.xml)|*.xml|All Files (*.*)|*.*";

            if (dialog.ShowDialog().Value)
            {
                // 选择上传的文件
                file = dialog.File;
                Stream stream = file.OpenRead();
                stream.Position = 0;
                buffer = new byte[stream.Length + 1];
                //将文件读入字节数组
                stream.Read(buffer, 0, buffer.Length);

                tbxFileName.Text = dialog.File.Name;
            }
            else
            {
                MessageBox.Show("请选择文件！","提示", MessageBoxButton.OK);
            }
        }

        void _Service_UploadfileCompleted(object sender, UploadfileCompletedEventArgs e)
        {
            _service.FlowTemplete_LoadAsync((cbxFlowSort.SelectedItem as BindableObject).GetValue("No"), e.Result, true);
            MessageBox.Show("上传成功！");
        }
    }
}

