using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
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
using WF.WS;

namespace BP
{
    public class UrlFlag
    {
        /// <summary>
        /// 节点属性
        /// </summary>
        public const string NodeP = "NodeP";
        /// <summary>
        /// 流程属性
        /// </summary>
        public const string FlowP = "FlowP";
        /// <summary>
        /// 运行流程
        /// </summary>
        public const string RunFlow = "RunFlow";
        /// <summary>
        /// 流程检查
        /// </summary>
        public const string FlowCheck = "FlowCheck";
        /// <summary>
        /// 报表定义
        /// </summary>
        public const string WFRpt = "WFRpt";
        /// <summary>
        /// 设置方向条件
        /// </summary>
        public const string Dir = "Dir";
        /// <summary>
        /// 节点表单设计
        /// </summary>
        public const string MapDef = "MapDef";
        /// <summary>
        /// 节点岗位
        /// </summary>
        public const string StaDef = "StaDef";
    }
    /// <summary>
    /// 窗口打开方式 
    /// </summary>
    public enum WindowModelEnum
    {
        Dialog,
        Window
    }
    public class Glo
    {
        #region 属性
        /// <summary>
        /// 当前BPMHost
        /// </summary>
        public static string BPMHost = null;
        /// <summary>
        /// 当前的流程编号
        /// </summary>
        public static string FK_Flow = null;
        #endregion

        #region 共用方法
        /// <summary>
        /// 设置打开网页窗口的属性
        /// </summary>
        /// <param name="lang">语言</param>
        /// <param name="dotype">窗口类型</param>
        /// <param name="fk_flow">工作流ID</param>
        /// <param name="node1">结点1</param>
        /// <param name="node2">结点2</param>
        public static void WinOpenByDoType(string lang, string dotype, string fk_flow, string node1, string node2)
        {
            Glo.DoTypeNow = dotype;
            string url = "";
            switch (Glo.DoTypeNow)
            {
                case UrlFlag.StaDef:
                    url = "/WF/Admin/XAP/DoPort.aspx?DoType=StaDef&PK=" + node1 + "&Lang=CH";
                    Glo.OpenDialog(Glo.BPMHost + url, "执行", 500, 400);
                    return;
                case UrlFlag.NodeP:
                    url = "/WF/Admin/XAP/DoPort.aspx?DoType=En&EnName=BP.WF.Node&PK=" + node1 + "&Lang=CH";
                    Glo.OpenDialog(Glo.BPMHost + url, "执行", 500, 400);
                    return;
                case UrlFlag.FlowP: // 节点属性与流程属性。
                    url = "/WF/Admin/XAP/DoPort.aspx?DoType=En&EnName=BP.WF.Flow&PK=" + fk_flow + "&Lang=CH";
                    Glo.OpenDialog(Glo.BPMHost + url, "执行", 500, 400);
                    return;
                case UrlFlag.MapDef: // 节点表单设计。
                    url = "/WF/Admin/XAP/DoPort.aspx?DoType=MapDef&PK=ND" + node1 + "&FK_Node=" + node1 + "&Lang=CH";
                    Glo.OpenWindowOrDialog(Glo.BPMHost + url, "节点表单设计", "Height:600px;Width:800px;", WindowModelEnum.Window);
                    return;
                case UrlFlag.Dir: // 方向条件。
                    url = "/WF/Admin/Cond.aspx?FK_Flow=" + fk_flow + "&FK_MainNode=" + node1 + "&FK_Node=" + node1 + "&ToNodeID=" + node2 + "&CondType=2" + "&Lang=CH";
                    Glo.OpenDialog(Glo.BPMHost + url, "方向条件", 550, 500);
                    return;
                case "RunFlow": // 运行流程。
                    url = "/WF/Admin/TestFlow.aspx?FK_Flow=" + fk_flow + "&Lang=CH";
                    Glo.WinOpen(Glo.BPMHost + url, "运行流程", 850, 990);
                    return;
                case "FlowCheck": // 流程设计。
                    url = "/WF/Admin/DoType.aspx?RefNo=" + fk_flow + "&DoType=" + dotype + "&Lang=CH";
                    Glo.WinOpen(Glo.BPMHost + url, "运行流程", 850, 990);
                    return;
                case "LoginPage": // 流程设计。
                    url = @"/WF/Login.aspx?Lang=CH";
                    Glo.WinOpen(Glo.BPMHost + url, "运行流程", 850, 990);
                    return;
                default:
                    MessageBox.Show("没有判断的url执行标记:" + dotype);
                    return;
            }
            Glo.WinOpen(Glo.BPMHost + "/WF/Admin/XAP/DoType=" + dotype + "&FK_Flow=" + fk_flow + "&FK_Node1=" + node1 + "&Lang=CH", "节点表单设计", 850, 990);
            return;
        }
        private static string DoTypeNow = "";
        public static void OpenDialog(string url, string title, int h, int w)
        {
            OpenWindowOrDialog(url, title, string.Format("dialogHeight:{0}px;dialogWidth:{1}px", h, w), WindowModelEnum.Dialog);
        }
        public static void WinOpen(string url, string title, int h, int w)
        {
            OpenWindowOrDialog(url, title, string.Format("height={0},width={1}", h, w), WindowModelEnum.Window);
        }
        public static void OpenDialog(string url, string title)
        {
            OpenWindowOrDialog(url, title, "dialogHeight:600px;dialogWidth:800px", WindowModelEnum.Dialog);
        }
        /// <summary>
        /// 得到WebService对象
        /// </summary>
        /// <returns></returns>
        public static WSDesignerSoapClient GetDesignerServiceInstance()
        {
            var basicBinding = new BasicHttpBinding()
            {
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
                Name = "WSDesignerSoap"
            };
            basicBinding.Security.Mode = BasicHttpSecurityMode.None;
            var endPoint = new EndpointAddress(Glo.BPMHost + "/WF/Admin/XAP/WebService.asmx");
            var ctor =
                typeof(WSDesignerSoapClient).GetConstructor(new Type[] { typeof(Binding), typeof(EndpointAddress) });
            return (WSDesignerSoapClient)ctor.Invoke(new object[] { basicBinding, endPoint });
        }
        #endregion 共用方法

        /// <summary>
        /// 弹出网页窗口
        /// </summary>
        /// <param name="url">网页地址</param>
        private static void OpenWindowOrDialog(string url, string title, string property, WindowModelEnum windowModel)
        {
            if (url.Contains("ttp://") == false)
                url = Glo.BPMHost + url;

            if (WindowModelEnum.Dialog == windowModel)
            {
                HtmlPage.Window.Eval(
                    string.Format("window.showModalDialog('{0}',window,'dialogHeight:600px;dialogWidth:800px;help:no;scroll:auto;resizable:yes;status:no;');",
                        url));
            }
            else
            {
                HtmlPage.Window.Eval("window.open('" + url + "','_blank')");
                //HtmlPage.Window.Eval(
                //    string.Format(
                //        "window.open('{0}','{1}','{2};help=no,resizable=yes,status=no,scrollbars=1');", url,
                //        title, property));
            }
        }
    }
}
