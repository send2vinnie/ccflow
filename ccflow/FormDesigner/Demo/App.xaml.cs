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
using System.Windows.Browser;

namespace Demo 
{
    public class Tools
    {
        public const string Mouse = "Mouse";
        public const string Line = "Line";
        /// <summary>
        /// 标签
        /// </summary>
        public const string Label = "Label";
        /// <summary>
        /// 文本框
        /// </summary>
        public const string TextBox = "TextBox";
        /// <summary>
        /// 下拉框
        /// </summary>
        public const string DDL = "DDL";
        /// <summary>
        /// 单选按钮
        /// </summary>
        public const string RBS = "RBS";
        /// <summary>
        /// 选择框
        /// </summary>
        public const string CheckBox = "CheckBox";
        /// <summary>
        /// 图片
        /// </summary>
        public const string Img = "Img";
        public const string Dtl = "Dtl";
        public const string M2M = "M2M";
    }
    public class Glo
    {
        public static void WinOpen(string url)
        {
            HtmlPage.Window.Eval( "window.showModalDialog('" + url + "',window,'dialogHeight:600px;dialogWidth:800px;center:Yes;help:No;scroll:auto;resizable:No;status:No;');");
            
        }
        public static void IE_ShowAddFGuide()
        {
            Glo.WinOpen("http://127.0.0.1/Flow/WF/MapDef/Do.aspx?DoType=AddF&MyPK="+Glo.FK_MapData);
        }
        public static string FK_MapData
        {
            get
            {
                return "ND501";
            }
        }
    }

    public partial class App : Application
    {
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
            InitializeComponent();
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
          //  this.RootVisual = new DrogTest();
          this.RootVisual = new MainPage();
          //  this.RootVisual = new Test();
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
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
