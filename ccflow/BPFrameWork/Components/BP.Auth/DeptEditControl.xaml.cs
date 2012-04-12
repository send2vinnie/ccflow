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
    public partial class DeptEditControl : UserControl
    {
        public OperationType OType { set; get; }
        public string UpDeptName { set; get; }
        public string DeptNo { set; get; }

        DeptEditModel editDept;
        public DeptEditModel EditDept
        {
            set
            {
                editDept = value;
                this.DataContext = value;
                IsNeedRefresh = false;
            }
            get
            {
                return editDept;
            }
        }
        public bool IsNeedRefresh { set; get; }
        public OrgDomainContext Domain { set; get; }
        public DeptEditControl()
        {
            InitializeComponent();
            Save.Click += new RoutedEventHandler(Save_Click);
            Cancel.Click += new RoutedEventHandler(Cancel_Click);
            //switch (OType)
            //{
            //    case OperationType.AddNew:
            //        Port_Dept NewDept = new Port_Dept() { No = DeptNo };
            //        this.DataContext = EditDept;
            //        break;
            //    case OperationType.Edit:
            //        this.DataContext = EditDept;
            //        break;
            //    case OperationType.Delete:
            //        break;
            //    default:
            //        break;
            //}
        }

        void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Domain.RejectChanges();
            var w = this.Parent as ChildWindow;
            w.Close();
        }

        void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Domain.HasChanges)
            {
                IsNeedRefresh = true;
                Domain.SubmitChanges().Completed += new EventHandler(DeptEditControl_Completed);
            }
            else
            {
                var w = this.Parent as ChildWindow;
                w.Close();
            }
        }

        void DeptEditControl_Completed(object sender, EventArgs e)
        {
            var Domain = sender as SubmitOperation;
            Domain.Completed -= new EventHandler(DeptEditControl_Completed);
            var w = this.Parent as ChildWindow;
            w.Close();
        }
    }

    public class DeptEditModel
    {
        public string UpDeptName { set; get; }
        public Port_Dept EditDept { set; get; }
    }
}
