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
namespace WF
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            if (System.Windows.Browser.HtmlPage.Document.QueryString.ContainsKey("WorkID")
                || System.Windows.Browser.HtmlPage.Document.QueryString.ContainsKey("FK_Flow"))
            {
                var workId = string.Empty;
                var flowId = string.Empty;
                var queryString = System.Windows.Browser.HtmlPage.Document.QueryString;
                if(queryString.ContainsKey("WorkID"))
                {
                    workId = queryString["WorkID"];
                }

                if(queryString.ContainsKey("FK_Flow"))
                {
                    flowId = queryString["FK_Flow"];
                }
                SelContainer sc = new SelContainer(flowId, workId);
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
