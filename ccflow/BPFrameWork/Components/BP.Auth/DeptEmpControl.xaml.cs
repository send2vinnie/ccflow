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
using System.ServiceModel.DomainServices.Client;
using BP.Auth.Web;
using Telerik.Windows.Controls;
using System.Windows.Browser;

namespace BP.Auth
{
    public partial class DeptEmpControl : UserControl
    {
        OrgDomainContext domain = new OrgDomainContext();
        RadContextMenu rcm = new RadContextMenu() { };

        RadContextMenu listboxrcm = new RadContextMenu() { };
        public DeptEmpControl()
        {
            // 为初始化变量所必需
            InitializeComponent();
            DeptWindow.Closed += new EventHandler(DeptWindow_Closed);
            EmpWindow.Closed += new EventHandler(EmpWindow_Closed);
            dec.Domain = domain;
            RadMenuItem AddNew = new RadMenuItem() { Header = "添加子节点" };
            AddNew.Click += new Telerik.Windows.RadRoutedEventHandler(AddNew_Click);
            RadMenuItem Delete = new RadMenuItem() { Header = "删除当前节点" };
            Delete.Click += new Telerik.Windows.RadRoutedEventHandler(Delete_Click);
            RadMenuItem Edit = new RadMenuItem() { Header = "编辑当前节点" };
            Edit.Click += new Telerik.Windows.RadRoutedEventHandler(Edit_Click);
            rcm.Items.Add(AddNew);
            rcm.Items.Add(Edit);
            rcm.Items.Add(Delete);

            RadMenuItem listAddNew = new RadMenuItem() { Header = "添加角色" };
            listAddNew.Click += new Telerik.Windows.RadRoutedEventHandler(listAddNew_Click);
            RadMenuItem listDelete = new RadMenuItem() { Header = "删除角色" };
            listDelete.Click += new Telerik.Windows.RadRoutedEventHandler(listDelete_Click);
            RadMenuItem listEdit = new RadMenuItem() { Header = "编辑角色" };
            listEdit.Click += new Telerik.Windows.RadRoutedEventHandler(listEdit_Click);
            listboxrcm.Items.Add(listAddNew);
            listboxrcm.Items.Add(listEdit);
            //listboxrcm.Items.Add(listDelete);

            RadContextMenu.SetContextMenu(EmpList, listboxrcm);

            treeview.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler(treeview_SelectionChanged);
            RadContextMenu.SetContextMenu(treeview, rcm);
            TreeViewRefresh();
        }

        void EmpListRefresh()
        {

            if (SelectedItem == null)
            {
                EmpList.ItemsSource = null;
                return;
            }
            if (SelectedItem.DataContext is Port_Dept)
            {
                ListBusy.IsBusy = true;
                LoadOperation<Port_Emp> LoadEmp =
                    domain.Load<Port_Emp>(domain.GetEmpByDeptNoQuery
                    ((SelectedItem.DataContext as Port_Dept).No));
                LoadEmp.Completed += new EventHandler(LoadEmp_Completed);
            }
            else
            {
                ListBusy.IsBusy = true;
                LoadOperation<Port_Emp> LoadEmp =
                    domain.Load<Port_Emp>(domain.GetEmpByStationNoQuery
                    ((SelectedItem.DataContext as StationModel).StationNo,
                    (SelectedItem.DataContext as StationModel).DeptNo));
                LoadEmp.Completed += new EventHandler(LoadEmp_Completed);
            }
        }

        void LoadSEmp_Completed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void EmpWindow_Closed(object sender, EventArgs e)
        {
            EmpListRefresh();
        }

        void listEdit_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (EmpList.SelectedItem == null)
                return;
            var uri = "http://localhost/Plant/(S(aym4qrya2bjchgnuckuudt5s))/Comm/RefFunc/UIEn.aspx?EnName=BP.WF.Port.Emp&PK=" 
                + (EmpList.SelectedItem as Port_Emp).No;

            HtmlPage.PopupWindow(new Uri(uri), "new",
                new HtmlPopupWindowOptions()
                {
                    //Resizeable = false,
                    //Menubar = false,
                    Height = 1000,
                    Width = 1000,
                    //Status = false,
                    //Toolbar = false,
                    //Directories = false,
                    //Left = 0,
                    //Top = 0,
                    //Location = false
                });
            //if (EmpList.SelectedItem == null)
            //    return;
            //edc.DataContextObject = EmpList.SelectedItem as Port_Emp;
            //edc.Domain = domain;
            //EmpWindow.Content = edc;
            //EmpWindow.Show();
        }

        void listDelete_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (MessageBox.Show("是否确认删除？", "确认提示"
                        , MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                domain.Port_Emps.Remove(EmpList.SelectedItem as Port_Emp);

                LoadOperation<Port_EmpStation> LoadES
                    = domain.Load<Port_EmpStation>(domain.GetPort_EmpStationQuery().
                    Where(a => a.FK_Emp == (EmpList.SelectedItem as Port_Emp).No));
                LoadES.Completed += new EventHandler(LoadES_Completed);
            }
            else
                return;
        }

        void LoadES_Completed(object sender, EventArgs e)
        {
            var LoadES = sender as LoadOperation<Port_EmpStation>;
            var del = LoadES.Entities.FirstOrDefault();
            if (del != null)
                domain.Port_EmpStations.Remove(del);

            domain.SubmitChanges().Completed += new EventHandler(emp_Completed);

        }

        void emp_Completed(object sender, EventArgs e)
        {
            var submit = sender as SubmitOperation;
            if (submit.HasError)
            {
                MessageBox.Show(submit.Error.Message);
                domain.RejectChanges();
            }
            EmpListRefresh();
        }

        void listAddNew_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            Port_Emp NewEmp = new Port_Emp();
            NewEmp.FK_Dept = (SelectedItem.DataContext as Port_Dept).No;
            edc.stationcombo.SelectedItem = null;
            //NewEmp.No = string.Format("N");
            edc.DataContextObject = NewEmp;
            domain.Port_Emps.Add(NewEmp);
            edc.Domain = domain;

            EmpWindow.Content = edc;
            EmpWindow.Show();
        }

        void treeview_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            EmpListRefresh();
        }

        void LoadEmp_Completed(object sender, EventArgs e)
        {
            var LoadEmp = sender as LoadOperation<Port_Emp>;

            EmpList.ItemsSource = LoadEmp.Entities;
            ListBusy.IsBusy = false;
        }

        void TreeViewRefresh()
        {
            treeview.Items.Clear();
            TreeBusy.IsBusy = true;
            LoadDept
                = domain.Load<Port_Dept>
                (domain.GetPort_DeptQuery().Where(a => a.No.Trim().Length <= 4));
            LoadDept.Completed += new EventHandler(LoadDept_Completed);
        }

        void DeptWindow_Closed(object sender, EventArgs e)
        {
            var w = sender as ChildWindow;
            var c = w.Content as DeptEditControl;
            if (c.IsNeedRefresh)
                TreeViewRefresh();
        }

        void Edit_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (SelectedItem == null)
                return;
            var selectDept = SelectedItem.DataContext as Port_Dept;
            switch (SelectedItem.TreeViewType)
            {
                case TreeViewType.Dept:
                    DeptWindow.Content = dec;
                    dec.OType = OperationType.Edit;
                    dec.EditDept = new DeptEditModel()
                    {
                        UpDeptName = SelectedItem.ParentItem == null ? "无" :
                          ((SelectedItem.ParentItem as CustomTreeViewItem).DataContext as Port_Dept).Name,
                        EditDept = selectDept
                    };
                    DeptWindow.Show();
                    break;
                case TreeViewType.Emp:
                    break;
                default:
                    break;
            }
        }

        public CustomTreeViewItem SelectedItem
        {
            get
            {
                return treeview.SelectedContainer as CustomTreeViewItem;
            }
        }

        ChildWindow DeptWindow = new ChildWindow();
        ChildWindow EmpWindow = new ChildWindow();
        DeptEditControl dec = new DeptEditControl() { };
        EmpEditControl edc = new EmpEditControl() { };
        void Delete_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (SelectedItem == null)
                return;

            switch (SelectedItem.TreeViewType)
            {
                case TreeViewType.Dept:
                    if ((SelectedItem.DataContext as Port_Dept).No.Length == 2)
                    {
                        MessageBox.Show("根节点不能删除");
                        return;
                    }
                    if (MessageBox.Show("是否确认删除？", "确认提示"
                        , MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        domain.Port_Depts.Remove(SelectedItem.DataContext as Port_Dept);
                        domain.SubmitChanges().Completed += new EventHandler(DeptEmpControl_Completed);
                    }
                    break;
                case TreeViewType.Emp:
                    break;
                default:
                    break;
            }
        }

        void DeptEmpControl_Completed(object sender, EventArgs e)
        {
            var submit = sender as SubmitOperation;
            if (submit.HasError)
            {
                MessageBox.Show(submit.Error.Message);
                domain.RejectChanges();
            }
            TreeViewRefresh();
        }

        void AddNew_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (SelectedItem == null)
                return;
            var selectDept = SelectedItem.DataContext as Port_Dept;
            switch (SelectedItem.TreeViewType)
            {
                case TreeViewType.Dept:
                    DeptWindow.Content = dec;
                    var items = LoadDept.Entities.Where
                        (a => a.No.StartsWith(selectDept.No) && a.No.Length == selectDept.No.Length + 2);
                    List<int> TempList = new List<int>();
                    foreach (var item in items)
                    {
                        TempList.Add(Int32.Parse(item.No));
                    }
                    //var castitems = items.Cast<Int32>();
                    var subNo = TempList.Count > 0 ? TempList.Max().ToString() : selectDept.No + string.Format("00");
                    var abc = subNo.Substring(subNo.Length - (subNo.Length - (subNo.Length - 2)), 2);
                    var intno = Int32.Parse(abc) + 1;
                    Port_Dept NewItem = new Port_Dept() { No = selectDept.No + intno.ToString("00") };
                    dec.EditDept = new DeptEditModel()
                    {
                        UpDeptName = SelectedItem.ParentItem == null ? "无" :
                          ((SelectedItem.ParentItem as CustomTreeViewItem).DataContext as Port_Dept).Name,
                        EditDept = NewItem
                    };
                    //dec.DeptNo = selectDept.No + (Int32.Parse(subNo.Substring(subNo.Length - 3, 2)) + 1).ToString();
                    domain.Port_Depts.Add(NewItem);
                    DeptWindow.Show();
                    break;
                case TreeViewType.Emp:
                    break;
                default:
                    break;
            }
        }

        LoadOperation<Port_Dept> LoadDept;
        LoadOperation<Port_EmpStation> LoadEmpStation;
        LoadOperation<Port_Emp> LoadTreeEmp;
        LoadOperation<Port_Station> LoadTreeStation;
        void LoadDept_Completed(object sender, EventArgs e)
        {
            LoadDept = sender as LoadOperation<Port_Dept>;
            LoadOperation<Port_EmpStation> LoadEmpStation
                = domain.Load<Port_EmpStation>
                (domain.GetPort_EmpStationQuery());
            LoadEmpStation.Completed += new EventHandler(LoadEmpStation_Completed);
        }

        void LoadEmpStation_Completed(object sender, EventArgs e)
        {
            LoadEmpStation = sender as LoadOperation<Port_EmpStation>;
            LoadOperation<Port_Emp> LoadTreeEmp
                        = domain.Load<Port_Emp>
                        (domain.GetPort_EmpQuery());
            LoadTreeEmp.Completed += new EventHandler(LoadTreeEmp_Completed);
        }

        void LoadTreeEmp_Completed(object sender, EventArgs e)
        {
            LoadTreeEmp = sender as LoadOperation<Port_Emp>;
            LoadOperation<Port_Station> LoadTreeStation
                        = domain.Load<Port_Station>
                        (domain.GetPort_StationQuery());
            LoadTreeStation.Completed += new EventHandler(LoadTreeStation_Completed);
        }

        void LoadTreeStation_Completed(object sender, EventArgs e)
        {
            LoadTreeStation = sender as LoadOperation<Port_Station>;
            var EmpToStation = (from es in LoadEmpStation.Entities
                                join te in LoadTreeEmp.Entities on es.FK_Emp equals te.No
                                join ts in LoadTreeStation.Entities on es.FK_Station equals ts.No
                                select new StationModel
                                {
                                    StationName = ts.Name,
                                    DeptNo = te.FK_Dept,
                                    StationNo = ts.No
                                });


            foreach (var item in LoadDept.Entities.Where(a => a.No.Length == 2))
            {
                CustomTreeViewItem tvi = new CustomTreeViewItem(TreeViewType.Dept)
                {
                    Header = item.Name,
                    DataContext = item,
                    TreeViewType = TreeViewType.Dept,
                    DefaultImageSrc = "/BP.Auth;component/Tree.jpg"
                };
                treeview.Items.Add(tvi);
                foreach (var es in EmpToStation.Where(a => a.DeptNo == item.No).Distinct())
                {
                    CustomTreeViewItem estvi = new CustomTreeViewItem(TreeViewType.Station)
                    {
                        Header = es.StationName,
                        DataContext = es,
                        //DataContext = item,
                        //TreeViewType = TreeViewType.Dept,
                        DefaultImageSrc = "/BP.Auth;component/Emp.jpg"
                    };
                    tvi.Items.Add(estvi);
                }
                foreach (var subitem in LoadDept.Entities.Where(a =>
                    a.No.StartsWith(item.No) && a.No.Length == 4))
                {
                    CustomTreeViewItem subtvi = new CustomTreeViewItem(TreeViewType.Dept)
                    {
                        Header = subitem.Name,
                        DataContext = subitem,
                        TreeViewType = TreeViewType.Dept,
                        DefaultImageSrc = "/BP.Auth;component/Tree.jpg"
                    };
                    tvi.Items.Add(subtvi);

                    foreach (var es in EmpToStation.Where(a => a.DeptNo == subitem.No).Distinct())
                    {
                        CustomTreeViewItem estvi = new CustomTreeViewItem(TreeViewType.Station)
                        {
                            Header = es.StationName,
                            DataContext = es,
                            //DataContext = item,
                            //TreeViewType = TreeViewType.Dept,
                            DefaultImageSrc = "/BP.Auth;component/Emp.jpg"
                        };
                        subtvi.Items.Add(estvi);
                    }
                }
            }
            //treeview.ExpandAll();
            LoadDept.Completed -= new EventHandler(LoadDept_Completed);
            TreeBusy.IsBusy = false;
        }
    }

    public class StationModel : IEquatable<StationModel>
    {
        public string StationName { set; get; }
        public string DeptNo { set; get; }
        public string StationNo { set; get; }

        public bool Equals(StationModel other)
        {

            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            bool abc = StationName.Equals(other.StationName) && StationNo.Equals(other.StationNo);

            return StationName.Equals(other.StationName) && StationNo.Equals(other.StationNo);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashProductName = StationNo == null ? 0 : StationNo.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = StationName.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }
    }
}
