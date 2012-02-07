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
using Ccflow.Web.UI.Control.Workflow.Designer;
using WF.WS;
using Silverlight;
using System.Collections;
using BP;
namespace WF.Controls
{
    public partial class FrmNewFlow : ChildWindow
    {
        #region Private Variables
        WSDesignerSoapClient _service = Glo.GetDesignerServiceInstance();
        private LoadingWindow loadingWindow = new LoadingWindow();
        OpenFileDialog dialog = new OpenFileDialog();
        private byte[] buffer;
        FileInfo file; 
        #endregion

        #region Events
        public event EventHandler<FlowTemplete_LoadCompletedEventArgs> FlowTempleteLoadCompeletedEventHandler;
        
        #endregion

        #region Properties
        /// <summary>
        /// 当前设计器的实例
        /// </summary>
        public MainPage CurrentDesinger { get; set; }
        /// <summary>
        /// 默认流程类别
        /// </summary>
        public String CurrentFlowSortName { get; set; }
        #endregion

        #region Constructs
        public FrmNewFlow()
        {
            InitializeComponent();
            _service.DoAsync("GetFlows", string.Empty, true);
            _service.DoCompleted += _service_GetFlowsCompleted;
        } 
        #endregion


        #region Private Methods
		private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = tabControl.SelectedItem as TabItem;
            if (null == selectedItem)
            {
                return;
            }
            if (selectedItem == tabStandardNew)
            {
                if (string.IsNullOrWhiteSpace(txtFlowName.Text))
                {
                    MessageBox.Show("请输入流程名称", "提示", MessageBoxButton.OK);
                    return;
                }
                if (null != CurrentDesinger)
                {
                    var flowSortID = (cbxFlowSortStandard.SelectedItem as BindableObject).GetValue("NO");
                    CurrentDesinger.NewFlow(flowSortID, txtFlowName.Text);
                    this.DialogResult = true;
                }
            }
            if (selectedItem == tabImportNew)
            {
                if (buffer == null || buffer.Length <= 0 || file == null || cbxFlowSortImport.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择模板文件", "提示", MessageBoxButton.OK);
                    return;
                }

                //调用服务上传
                try
                {
                    loadingWindow.Show();
                    _service.UploadfileAsync(buffer, file.Name);
                    _service.UploadfileCompleted += _Service_UploadfileCompleted;

                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    this.DialogResult = false;
                    MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK);
                }

            }

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
                MessageBox.Show("请选择文件！", "提示", MessageBoxButton.OK);
            }
        }

        void _service_GetFlowsCompleted(object sender, DoCompletedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.FromXml(e.Result);

            // 得到默认的流程类别
            int defaultFlowSort = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if(ds.Tables[0].Rows[i]["NAME"] == CurrentFlowSortName)
                {
                    defaultFlowSort = i;
                }
            }

            IList list = ds.Tables[0].GetBindableData(new Connector());

            if (list.Count > 0)
            {
                cbxFlowSortImport.ItemsSource = list;
                cbxFlowSortImport.DisplayMemberPath = "NAME";
                cbxFlowSortImport.SelectedIndex = defaultFlowSort;

                cbxFlowSortStandard.ItemsSource = list;
                cbxFlowSortStandard.DisplayMemberPath = "NAME";
                cbxFlowSortStandard.SelectedIndex = defaultFlowSort;
            }

            _service.DoCompleted -= _service_GetFlowsCompleted;

        }
        
        void _Service_UploadfileCompleted(object sender, UploadfileCompletedEventArgs e)
        {
            if (e.Result.Contains("Error:"))
            {
                loadingWindow.Close();
                MessageBox.Show(e.Result, "Error", MessageBoxButton.OK);
                return;
            }
            _service.FlowTemplete_LoadCompleted += _service_FlowTemplete_LoadCompleted;
            _service.FlowTemplete_LoadAsync((cbxFlowSortImport.SelectedItem as BindableObject).GetValue("NO"), e.Result, true);

        }

        void _service_FlowTemplete_LoadCompleted(object sender, FlowTemplete_LoadCompletedEventArgs e)
        {
            loadingWindow.Close();

            if (null != FlowTempleteLoadCompeletedEventHandler)
            {
                FlowTempleteLoadCompeletedEventHandler(sender, e);
            }
        }
 
	    #endregion    
    }
}

