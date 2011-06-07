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
    public partial class Orgs : UserControl
    {
        public Designers Designer { get; set; }
        public Orgs()
        {
            //this.TreeView1.Height = Application.Current.Host.Content.ActualHeight - 35;
            //InitializeComponent();
         //   this.BindTree();
        }
        public void BindTree()
        {
            string sql = "SELECT No,Name FROM Port_Dept";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            this.TreeView1.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode node = new TreeNode();
                node.Title = dr["Name"].ToString();
                node.FK_Dept = dr["No"].ToString();
                node.Name = dr["No"].ToString();
                node.isDept = true;
                this.TreeView1.Nodes.Add(node);
            }
        }
    }
}
