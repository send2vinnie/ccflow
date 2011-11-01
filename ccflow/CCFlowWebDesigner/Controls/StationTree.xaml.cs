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
using WF.WS;
using Silverlight;
using Ccflow.Web.UI.Control.Workflow.Designer;

namespace WF.Controls
{
    public partial class StationTree : UserControl
    {
        WSDesignerSoapClient service = null;
        System.Windows.Threading.DispatcherTimer _doubleClickTimer;
        public Designers Designer { get; set; }
        public StationTree()
        {
          
            InitializeComponent();
            try
            {
                service = new WSDesignerSoapClient();
             //   service.GetStationsAsync();  
               // service.GetStationsCompleted += new EventHandler<GetStationsCompletedEventArgs>(_Service_GetStationsCompleted);
           
            service.GetStationEmpsCompleted += new EventHandler<GetStationEmpsCompletedEventArgs>(_Service_GetStationEmpsCompleted); 
                this.TvwStation.Height = Application.Current.Host.Content.ActualHeight-35;
            }
            catch { }




            _doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
            _doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, SystemConst.DoubleClickTime);
            _doubleClickTimer.Tick += new EventHandler(DoubleClick_Timer);
         
        }




        void DoubleClick_Timer(object sender, EventArgs e)
        {
            _doubleClickTimer.Stop();
        }
        DataSet dsStationEmp = new DataSet();
        void _Service_GetStationEmpsCompleted(object sender, GetStationEmpsCompletedEventArgs e)
        {
            int i = 0;

            dsStationEmp = new DataSet();
            dsStationEmp.FromXml(e.Result);

            foreach (DataRow d in dsStationEmp.Tables[0].Rows)
            {

                TreeNode node = new TreeNode();

                node.Title = d["EmpName"].ToString();
                node.FK_Station = d["No"].ToString();

                node.Name = d["EmpNo"].ToString()+(++i);
                node.EmpNo = d["EmpNo"].ToString();

                foreach (TreeNode ne in firstNodeByStation.Nodes)
                {
                    try
                    {

                        if (ne.FK_Station == d["No"].ToString())
                        {
                            ne.Nodes.Add(node);


                        }

                    }
                    catch { }

                }



            }


        }

        TreeNode firstNodeByStation = new TreeNode();
        //void _Service_GetStationsCompleted(object sender, GetStationsCompletedEventArgs e)
        //{
        //    firstNodeByStation = new TreeNode();
        //    TvwStation.Clear();
        //    DataSet ds = new DataSet();
        //    ds.FromXml(e.Result);
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {

        //        TreeNode node = new TreeNode();
        //        node.isStation = true;
        //        node.Title = dr["Name"].ToString();
        //        node.FK_Station = dr["No"].ToString();
        //        node.Name = dr["No"].ToString();
        //        firstNodeByStation.Nodes.Add(node);

        //        TvwStation.Nodes.Add(node);
        //    }
        //    service.GetStationEmpsAsync();
        //}
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MuStation.Hide();
        }

        private void TvwStation_SelectionChanged(object sender, Liquid.TreeEventArgs e)
        {
            MuStation.Hide();

        }

        private void TvwStation_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(TvwStation);
            TreeNode node = TvwStation.GetNodeAtPoint(position) as TreeNode;

            if (node != null)
            {
                Point contextMenuPos = TvwStation.TransformToVisual(TvwStation).Transform(position);
                node.IsSelected = true;
                TvwStation.ClearSelected();
                TvwStation.SetSelected(node);

                MuStation.SetValue(Canvas.LeftProperty, contextMenuPos.X);
                MuStation.SetValue(Canvas.TopProperty, contextMenuPos.Y);
                MuStation.Show();

            }

            e.Handled = true;
        }

        private void TvwStation_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void MuStation_ItemSelected(object sender, Liquid.MenuEventArgs e)
        {
            if (e.Tag == null)
            {
                return;
            }

  TreeNode SelectNode = TvwStation.Selected as TreeNode;
            switch (e.Tag.ToString())
            {

          

                 
                case "Modify":

    if (SelectNode.isStation)
            {
                service.MaintainStationAsync(SelectNode.Name);
                service.MaintainStationCompleted += new EventHandler<MaintainStationCompletedEventArgs>(service_MaintainStationCompleted);
            }
            else
            {
                service.MaintainEmpAsync(SelectNode.EmpNo);
                service.MaintainEmpCompleted += new EventHandler<MaintainEmpCompletedEventArgs>(service_MaintainEmpCompleted);
            }

                    break;
                case "Refresh":
                //    service.GetStationsAsync();


                    break;
            }
          
        

            MuStation.Hide();
        }

        void service_MaintainEmpCompleted(object sender, MaintainEmpCompletedEventArgs e)
        {
            Designer.OpenDialog(e.Result, "人员维护",300,400);
          
            service.MaintainEmpCompleted -= new EventHandler<MaintainEmpCompletedEventArgs>(service_MaintainEmpCompleted);
           // service.GetStationsAsync();
        }

        void service_MaintainStationCompleted(object sender, MaintainStationCompletedEventArgs e)
        {
            Designer.OpenDialog(e.Result, "岗位维护", 300, 400);
         //   service.GetStationsAsync();
          
            service.MaintainStationCompleted -= new EventHandler<MaintainStationCompletedEventArgs>(service_MaintainStationCompleted);
        }
    }
}
