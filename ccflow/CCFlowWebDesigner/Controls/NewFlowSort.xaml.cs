﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ServiceModel;
using WF.WS;
using Ccflow.Web.UI.Control.Workflow.Designer;

namespace WF.Controls
{
    public partial class NewFlowSort : ChildWindow
    {
        /// <summary>
        /// 显示类型枚举
        /// </summary>
        public enum DisplayTypeEnum
        {
            Add,
            Edit
        }
        public string No { get; set; }
        public string FK_FlowSort { get; set; }
        /// <summary>
        /// 流程类别名称
        /// </summary>
        public string FlowSortName { get; set; }
        /// <summary>
        /// 显示类型
        /// </summary>
        public DisplayTypeEnum DisplayType { get; set; }
        public event EventHandler<DoCompletedEventArgs> ServiceDoCompletedEvent;
        public NewFlowSort(MainPage contaniner): this()
        {
            this._container = contaniner;
        }
        public NewFlowSort()
        {
            InitializeComponent();
        }
        MainPage _container;
        public MainPage Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }
        /// <summary>
        /// 初始化控件内容 
        /// </summary>
        /// <param name="no"></param>
        /// <param name="name"></param>
        public void InitControl(string no, string name)
        {
            this.No = no;
            txtFlowNodeName.Text = name;
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if(DisplayTypeEnum.Add == DisplayType)
            {
                Container._Service.DoAsync("NewFlowSort", txtFlowNodeName.Text, true);
                Container._Service.DoCompleted += new EventHandler<DoCompletedEventArgs>(_service_DoCompleted);
            }

            if(DisplayTypeEnum.Edit == DisplayType)
            {
                Container._Service.DoAsync("EditFlowSort", this.No + "," + txtFlowNodeName.Text, true);
                Container._Service.DoCompleted += new EventHandler<DoCompletedEventArgs>(_service_DoCompleted);
            }
            this.DialogResult = true;
        }
        void _service_DoCompleted(object sender, DoCompletedEventArgs e)
        {
            this.No=  e.Result;
            if(null != ServiceDoCompletedEvent)
                ServiceDoCompletedEvent(this, e);
            Container._Service.DoCompleted -= _service_DoCompleted;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

