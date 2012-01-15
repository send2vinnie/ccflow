using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Browser;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Browser;
using System.IO;
using Silverlight;
using Ccflow.Web.UI.Control.Workflow.Designer;
using WF.Designer;

namespace BP
{
    public partial class App : Application
    {
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
            InitializeComponent();
        }
        /// <summary>
        /// Application_Startup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            bool registerResult = WebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
            bool httpsResult = WebRequest.RegisterPrefix("https://", WebRequestCreator.ClientHttp);

            //设置当前线程的culture,以加载指定语言的字符
            var culture = new CultureInfo("zh-cn");
            Thread.CurrentThread.CurrentUICulture = culture;

            Glo.BPMHost = GetHostUrl();

            //WF.Frm.FrmLib lab = new WF.Frm.FrmLib();
            //this.RootVisual = lab;
            //return;

            if (System.Windows.Browser.HtmlPage.Document.QueryString.ContainsKey("WorkID")
             || System.Windows.Browser.HtmlPage.Document.QueryString.ContainsKey("FK_Flow"))
            {
                var workId = string.Empty;
                var flowId = string.Empty;
                var queryString = System.Windows.Browser.HtmlPage.Document.QueryString;
                if (queryString.ContainsKey("WorkID"))
                    workId = queryString["WorkID"];

                if (queryString.ContainsKey("FK_Flow"))
                    flowId = queryString["FK_Flow"];

                BP.Track track = new BP.Track(flowId, workId);
                this.RootVisual = track;
            }
            else
            {
                MainPage c = new MainPage();
                this.RootVisual = c;
            }
        }
        private void Application_Exit(object sender, EventArgs e)
        {
        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // 如果应用程序是在调试器外运行的，则使用浏览器的
            // 异常机制报告该异常。在 IE 上，将在状态栏中用一个 
            // 黄色警报图标来显示该异常，而 Firefox 则会显示一个脚本错误。
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // 注意: 这使应用程序可以在已引发异常但尚未处理该异常的情况下
                // 继续运行。 
                // 对于生产应用程序，此错误处理应替换为向网站报告错误
                // 并停止应用程序。
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
            errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

            string alert = "请您按如下方式处理这个错误。";
            alert += "\t\n1，如果是第一次使用，请打开安装文件中有常见的问题,此文件位于D:\\ccflow\\Documents\\.";
            alert += "\t\n2，进入官方网站(http://ccflow.org)加入QQ群论坛，获得更多的ccflow爱好者帮助。";
            alert += "\t\n3，把此屏幕copy一个图片，发送到 hiflow@qq.com，我们会尽快给您回复。";
            alert += "\t\n";

            MessageBox.Show(alert + errorMsg, "ccflow err:",
                MessageBoxButton.OK);
            //HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
        }
        /// <summary>
        /// 得到当前所在网站的根目录，如Http://localhost/flow
        /// 注意站点名字必须是Flow,否则会报错。
        /// </summary>
        /// <returns></returns>
        private string GetHostUrl()
        {
            var location = (HtmlPage.Window.GetProperty("location")) as ScriptObject;
            var hrefObject = location.GetProperty("href");
            string url = hrefObject.ToString();
            string[] strs = url.Split('/');
            return strs[0] + "//" + strs[1] + strs[2] + "/" + strs[3];
            //string url = hrefObject.ToString().Substring(0, hrefObject.ToString().IndexOf("Flow/") + 5);
            //return url;
        }
    }
}
