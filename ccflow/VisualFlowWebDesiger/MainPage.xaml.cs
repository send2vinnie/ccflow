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
using Ccflow.Web.UI.Control.Workflow.Designer;
using WF.Designer;
//using WF.Designer;

namespace WF
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            Designers ce = new Designers();
            this.Content = ce;


            if (System.Windows.Browser.HtmlPage.Document.QueryString.ContainsKey("WorkID") || System.Windows.Browser.HtmlPage.Document.QueryString.ContainsKey("FK_Flow"))
            {
                string workid = "";
                string fk_flow = "";
                try
                {
                    workid = System.Windows.Browser.HtmlPage.Document.QueryString["WorkID"].ToString();
                }
                catch { }
                try
                {
                    fk_flow = System.Windows.Browser.HtmlPage.Document.QueryString["FK_Flow"].ToString();

                }
                catch { }

                SelContainer sc = new SelContainer(fk_flow, workid);
                this.Content = sc;
            }
            else
            {
                Designers c = new Designers();
                this.Content = c;
            }
        }
    }
}
