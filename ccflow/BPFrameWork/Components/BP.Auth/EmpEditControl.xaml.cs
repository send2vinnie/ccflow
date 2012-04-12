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
using BP.Auth.Web;
using System.ServiceModel.DomainServices.Client;

namespace BP.Auth
{
    public partial class EmpEditControl : UserControl
    {
        Port_Emp dataContextObject;
        public Port_Emp DataContextObject
        {
            set
            {
                dataContextObject = value;
                this.DataContext = value;

            }
            get { return dataContextObject; }
        }


        public OrgDomainContext Domain { set; get; }

        public EmpEditControl()
        {
            // 为初始化变量所必需
            InitializeComponent();
            Save.Click += new RoutedEventHandler(Save_Click);
            Cancel.Click += new RoutedEventHandler(Cancel_Click);

            this.Loaded += new RoutedEventHandler(EmpEditControl_Loaded);
        }

        void EmpEditControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetBaseData();
        }

        void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Domain.RejectChanges();
            var w = this.Parent as ChildWindow;
            w.Close();
        }

        void Save_Click(object sender, RoutedEventArgs e)
        {
            domain.GetSpell(DataContextObject.Name).Completed += new EventHandler(GetSpell_Completed);
        }

        void GetSpell_Completed(object sender, EventArgs e)
        {
            var GetSpell = sender as InvokeOperation<string>;
            DataContextObject.No = GetSpell.Value.ToLower();

            LoadOperation<Port_EmpStation> LoadES
                = domain.Load<Port_EmpStation>(domain.GetPort_EmpStationQuery().
                Where(a => a.FK_Emp == DataContextObject.No));
            LoadES.Completed += new EventHandler(LoadES_Completed);
        }

        void LoadES_Completed(object sender, EventArgs e)
        {
            var LoadES = sender as LoadOperation<Port_EmpStation>;
            Port_EmpStation emp = new Port_EmpStation();
            if (LoadES.Entities.Count() > 0)
            {
                emp = LoadES.Entities.First();
                emp.FK_Station = stationcombo.SelectedValue.ToString();
                emp.FK_Emp = DataContextObject.No;
            }
            else
            {
                emp.FK_Station = stationcombo.SelectedValue.ToString();
                emp.FK_Emp = DataContextObject.No;
                Domain.Port_EmpStations.Add(emp);
            }

            //DataContextObject.No = Hz2Py.Convert(DataContextObject.Name);
            if (Domain.HasChanges)
                Domain.SubmitChanges().Completed += new EventHandler(EmpEditControl_Completed);
            else
            {
                var w = this.Parent as ChildWindow;
                w.Close();
            }
        }

        void EmpEditControl_Completed(object sender, EventArgs e)
        {
            var Domain = sender as SubmitOperation;
            Domain.Completed -= new EventHandler(EmpEditControl_Completed);
            var w = this.Parent as ChildWindow;
            w.Close();
        }

        OrgDomainContext domain = new OrgDomainContext();
        void GetBaseData()
        {
            LoadOperation<Port_Station> LoadStation
                = domain.Load<Port_Station>(domain.GetPort_StationQuery());
            LoadStation.Completed += new EventHandler(LoadStation_Completed);
        }

        void LoadStation_Completed(object sender, EventArgs e)
        {
            var LoadStation = sender as LoadOperation<Port_Station>;
            stationcombo.ItemsSource = LoadStation.Entities;
            stationcombo.DisplayMemberPath = "Name";
            stationcombo.SelectedValuePath = "No";
            LoadStation.Completed -= new EventHandler(LoadStation_Completed);

            //domain.Port_Depts.Clear();
            LoadOperation<Port_Dept> LoadDept
                = domain.Load<Port_Dept>(domain.GetPort_DeptQuery());
            LoadDept.Completed += new EventHandler(LoadDept_Completed);
        }

        void LoadDept_Completed(object sender, EventArgs e)
        {
            var LoadDept = sender as LoadOperation<Port_Dept>;
            deptcombo.ItemsSource = LoadDept.Entities;
            deptcombo.DisplayMemberPath = "Name";
            deptcombo.SelectedValuePath = "No";
            //this.DataContext = DataContextObject;
            LoadDept.Completed -= new EventHandler(LoadDept_Completed);

            LoadOperation<Port_EmpStation> LoadESs
                = domain.Load<Port_EmpStation>(domain.GetPort_EmpStationQuery().
                Where(a => a.FK_Emp == DataContextObject.No));
            LoadESs.Completed += new EventHandler(LoadESs_Completed);
        }

        void LoadESs_Completed(object sender, EventArgs e)
        {
            var LoadESs = sender as LoadOperation<Port_EmpStation>;
            var value = LoadESs.Entities.Count() > 0 ? LoadESs.Entities.First().FK_Station : null;
            if (value == null)
                return;
            stationcombo.SelectedValue = value;
        }
    }
}
