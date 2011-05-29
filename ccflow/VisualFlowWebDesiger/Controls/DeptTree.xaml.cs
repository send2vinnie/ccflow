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
using WF.DataServiceReference;
using Silverlight;
using Ccflow.Web.UI.Control.Workflow.Designer;

namespace WF.Controls
{
    public partial class DeptTree : UserControl
    {
        //new BasicHttpBinding(), address);

        WebServiceSoapClient service = null;
        System.Windows.Threading.DispatcherTimer _doubleClickTimer;
        public Designers Designer { get; set; }
        public DeptTree()
        {
            this.TvwDept.Height = Application.Current.Host.Content.ActualHeight - 35;
            InitializeComponent();
            this.BindTree();
        }
        public void BindTree()
        {
            string sql = "SELECT No,Name FROM Port_Dept";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);

            TvwDept.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode node = new TreeNode();
                node.Title = dr["Name"].ToString();
                node.FK_Dept = dr["No"].ToString();
                node.Name = dr["No"].ToString();
                node.isDept = true;
                TvwDept.Nodes.Add(node);
            }
        }
        private void MuDept_ItemSelected(object sender, Liquid.MenuEventArgs e)
        {
            if (e.Tag == null)
                return;

            TreeNode SelectNode = TvwDept.Selected as TreeNode;
            switch (e.Tag.ToString())
            {
                case "Modify":
                    if (SelectNode.isDept)
                    {
                        service.MaintainDeptAsync(SelectNode.Name);
                        service.MaintainDeptCompleted += new EventHandler<MaintainDeptCompletedEventArgs>(service_MaintainDeptCompleted);
                    }
                    else
                    {
                        service.MaintainEmpAsync(SelectNode.Name);
                        service.MaintainEmpCompleted += new EventHandler<MaintainEmpCompletedEventArgs>(service_MaintainEmpCompleted);
                    }
                    break;
                case "Refresh":
                   // service.GetDeptsAsync();
                    break;
            }
            MuDept.Hide();
        }
        void service_MaintainEmpCompleted(object sender, MaintainEmpCompletedEventArgs e)
        {
            Designer.WinOpen(e.Result, "人员维护", 500, 700);
           // service.GetDeptsAsync();
            service.MaintainEmpCompleted -= new EventHandler<MaintainEmpCompletedEventArgs>(service_MaintainEmpCompleted);
        }

        void service_MaintainDeptCompleted(object sender, MaintainDeptCompletedEventArgs e)
        {
            Designer.WinOpen(e.Result, "部门维护", 500, 700);
           // service.GetDeptsAsync();
            service.MaintainDeptCompleted -= new EventHandler<MaintainDeptCompletedEventArgs>(service_MaintainDeptCompleted);
        }

        private void TvwDept_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_doubleClickTimer.IsEnabled)
            {
                _doubleClickTimer.Stop();
            }
            else
            {
                _doubleClickTimer.Start();

            }
        }
        private void TvwDept_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(TvwDept);
            TreeNode node = TvwDept.GetNodeAtPoint(position) as TreeNode;

            if (node != null)
            {
                Point contextMenuPos = TvwDept.TransformToVisual(TvwDept).Transform(position);
                node.IsSelected = true;
                TvwDept.ClearSelected();
                TvwDept.SetSelected(node);

                MuDept.SetValue(Canvas.LeftProperty, contextMenuPos.X);
                MuDept.SetValue(Canvas.TopProperty, contextMenuPos.Y);
                MuDept.Show();

            }
            e.Handled = true;
        }
        private void TvwDept_SelectionChanged(object sender, Liquid.TreeEventArgs e)
        {
            MuDept.Hide();
        }
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MuDept.Hide();
        }
    }
}
