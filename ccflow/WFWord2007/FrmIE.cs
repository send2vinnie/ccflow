using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BP.Port;
using BP.En;
using System.IO;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;

namespace BP.Comm
{
    public partial class FrmIE : Form
    {
        public FrmIE()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 显示url
        /// </summary>
        /// <param name="url"></param>
        public void ShowUrl(string url)
        {
            this.webBrowser1.Url = new Uri(url);
        }
        public void OpenDoc(string file, bool _isReadonly)
        {
            _isReadonly = false;
            try
            {
                object obj = Type.Missing;
                WFWord2007.Globals.ThisAddIn.Application.ActiveDocument.Close(ref obj, ref obj, ref obj);
            }
            catch
            {
            }
            if (file == null)
            {
                file = BP.WF.Glo.PathOfTInstall + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".doc";
                System.IO.StreamWriter sr;
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);
                sr = new System.IO.StreamWriter(file, false, System.Text.Encoding.GetEncoding("GB2312"));
                sr.Write(DateTime.Now.ToString("yyyy年MM月dd日") + " 无公文模板");
                sr.Close();
            }

            object fileName = file;
            object readOnly = _isReadonly;
            object missing = Type.Missing;

            try
            {
                WFWord2007.Globals.ThisAddIn.Application.Documents.Open(ref fileName, ref missing, ref readOnly,
                    ref missing, ref missing, ref missing,
        ref missing, ref missing, ref missing, ref missing, ref missing,
        ref missing, ref missing, ref missing, ref missing, ref missing);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开文件期间异常：" + ex.Message);
                return;
            }

            // object isReadonly = _isReadonly;
            // object ConfirmConversions = false;
            // object addtoreconfiles = false;
            // object passwordDocument = false;
            // object paaawordTempleate = false;
            // object revert = false;
            // object writepassword = "";
            //// object FileFormat = Word.WdSaveFormat.wdFormatDocument;

            // object waritepasswordtemplate = "";
            // object format = false;
            // object encoding = false;
            // object visible = false;
            // object openandrepair = false;
            // object documentDirection = false;
            // object noEncodingDialog = true;
            // object xmltransfrom = false;
            // WFWord2007.Globals.ThisAddIn.Application.Documents.Open(ref fileName, ref  ConfirmConversions, ref isReadonly, ref addtoreconfiles,
            //     ref passwordDocument, ref paaawordTempleate, ref revert, ref writepassword, ref waritepasswordtemplate, ref format,
            //     ref encoding, ref visible, ref openandrepair, ref documentDirection, ref noEncodingDialog, ref xmltransfrom);
        }
        /// <summary>
        /// 执行发送
        /// </summary>
        /// <param name="para"></param>
        public void DoSend(BP.DA.AtPara para)
        {
            WFWord2007.Globals.ThisAddIn.DoSave();
            object obj = Type.Missing;
            WFWord2007.Globals.ThisAddIn.Application.ActiveDocument.Close(ref obj, ref obj, ref obj);
        }
        public void DoOpenDoc(BP.DA.AtPara para)
        {
            string fk_flow = para.GetValStrByKey("FK_Flow");
            int workid = para.GetValIntByKey("WorkID");
            int fk_node = para.GetValIntByKey("FK_Node");

            string file = BP.WF.Glo.PathOfTInstall + workid + "@" + WebUser.No + ".doc";
            if (System.IO.File.Exists(file) == false)
            {
                try
                {
                    FtpSupport.FtpConnection conn = BP.WF.Glo.HisFtpConn;
                    if (conn.DirectoryExist("/DocFlow/" + fk_flow + "/" + workid) == true)
                    {
                        conn.SetCurrentDirectory("/DocFlow/" + fk_flow + "/" + workid);
                        conn.GetFile(workid + ".doc", file, true, FileAttributes.Archive);
                        conn.Close();
                    }
                    else
                    {
                        throw new Exception("@没有找到文件，流程错误。");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("@打开公文错误@技术信息:" + ex.Message + "@流程编号：" + WebUser.FK_Flow);
                    return;
                }
            }

            WebUser.FK_Flow = fk_flow;
            WebUser.FK_Node = fk_node;
            WebUser.WorkID = workid;

            /*如果存在这个文件，就激活它。*/
            WebUser.WriterIt(BP.WF.StartFlag.DoOpenDoc, fk_flow, fk_node, workid);

            this.OpenDoc(file, false);
            this.HisRibbon1.ReSetState();
            this.Close();
        }
        /// <summary>
        /// 打开流程
        /// </summary>
        /// <param name="para"></param>
        public void DoOpenFlow(BP.DA.AtPara para)
        {
            string fk_flow = para.GetValStrByKey("FK_Flow");
            int workid = para.GetValIntByKey("WorkID");
            int fk_node = para.GetValIntByKey("FK_Node");

            if (WebUser.WorkID == workid && WebUser.FK_Node == fk_node)
            {
                // return;
                if (MessageBox.Show("当前流程已经打开，您想重新加载吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    this.webBrowser1.GoBack();
                    return;
                }
            }

            string file = BP.WF.Glo.PathOfTInstall + workid + "@" + WebUser.No + ".doc";
            if (System.IO.File.Exists(file) == false)
            {
                try
                {
                    FtpSupport.FtpConnection conn = BP.WF.Glo.HisFtpConn;
                    if (conn.DirectoryExist("/DocFlow/" + fk_flow + "/" + workid) == true)
                    {
                        conn.SetCurrentDirectory("/DocFlow/" + fk_flow + "/" + workid);
                        conn.GetFile(workid + ".doc", file, true, FileAttributes.Archive);
                        conn.Close();
                    }
                    else
                    {
                        throw new Exception("@没有找到文件，流程文件丢失错误。");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("@打开公文错误@技术信息:" + ex.Message + "@流程编号：" + WebUser.FK_Flow);
                    return;
                }
            }

            WebUser.FK_Flow = fk_flow;
            WebUser.FK_Node = fk_node;
            WebUser.WorkID = workid;

            /*如果存在这个文件，就激活它。*/
            WebUser.WriterIt(BP.WF.StartFlag.DoOpenFlow, fk_flow, fk_node, workid);
            this.OpenDoc(file, false);
            this.Close();
        }
        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="para"></param>
        public void DoStartFlow(BP.DA.AtPara para)
        {
            string fk_flow = para.GetValStrByKey("FK_Flow");
            string workid = para.GetValStrByKey("WorkID");
            string file = BP.WF.Glo.PathOfTInstall + workid + "@" + WebUser.No + ".doc";

            if (System.IO.File.Exists(file) == false)
            {
                try
                {
                    FtpSupport.FtpConnection conn = BP.WF.Glo.HisFtpConn;
                    if (conn.DirectoryExist("/DocFlow/" + fk_flow + "/" + workid) == true)
                    {
                        conn.SetCurrentDirectory("/DocFlow/" + fk_flow + "/" + workid);
                        if (conn.FileExist(WebUser.FK_Node + "@" + WebUser.No + ".doc") == true)
                            conn.GetFile(WebUser.FK_Node + "@" + WebUser.No + ".doc", file, true, FileAttributes.Archive);
                        else
                            file = null;
                    }
                    else
                    {
                        file = null;
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("@流程设计错误，没有为该流程维护公文模板。@技术信息:" + ex.Message + "@流程编号：" + WebUser.FK_Flow);
                    return;
                }
            }

            WebUser.FK_Flow = fk_flow;
            WebUser.WorkID = int.Parse( workid);

            /*如果存在这个文件，就激活它。*/
            WebUser.WriterIt(BP.WF.StartFlag.DoNewFlow, fk_flow, int.Parse(fk_flow + "01"), int.Parse(workid));
            this.OpenDoc(file, false);
            this.Close();
        }
        /// <summary>
        /// 生成模板按照
        /// </summary>
        /// <param name="para"></param>
        public void DoStartFlowByTemple(BP.DA.AtPara para)
        {
            string fk_flow = para.GetValStrByKey("FK_Flow");
            // 下载流程模板 
            FtpSupport.FtpConnection conn = BP.WF.Glo.HisFtpConn;
            string file = BP.WF.Glo.PathOfTInstall + fk_flow + "@" + DateTime.Now.ToString("MM月dd日hh时mm分ss秒") + ".doc";
            try
            {

                conn.SetCurrentDirectory("/DocFlowTemplete/");
                if (conn.FileExist(fk_flow + ".doc") == false)
                    throw new Exception("@没有为公文启动设置模板。");

                conn.GetFile(fk_flow + ".doc", file, true, FileAttributes.Archive);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                file = null;
                MessageBox.Show("@异常信息:" + ex.Message + "\t\n@流程编号：" + WebUser.FK_Flow + "\t\n@可能的原因如下：1，设计人员没有正确的设置ftp服务器。 \t\n2，没有该流程的公文模板。");
            }

            WebUser.WorkID = 0;
            WebUser.FK_Flow = fk_flow;
            WebUser.FK_Node = int.Parse(fk_flow + "01");
            this.HisRibbon1.ReSetState();
            WebUser.WriterIt(BP.WF.StartFlag.DoNewFlow, fk_flow, int.Parse(fk_flow + "01"), 0);
            this.OpenDoc(file, false);
            this.Close();
        }
        public WFWord2007.Ribbon1 HisRibbon1 = null;
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string url = e.Url.AbsoluteUri;
            this.statusStrip1.Text = url;
            string pageID = url.Substring(url.LastIndexOf('/') + 1);

            this.toolStripStatusLabel1.Text = url;

            pageID = pageID.Substring(0, pageID.IndexOf(".aspx"));
            url = url.Substring(url.IndexOf(".aspx"));
            url = url.Replace("?", "@");
            url = url.Replace("&", "@");
            url = url.Replace(".aspx", "");

            BP.DA.AtPara para = new BP.DA.AtPara(url);
            switch (pageID)
            {
                case "DoClient":
                    try
                    {
                        switch (para.DoType)
                        {
                            case BP.WF.DoType.DoStartFlow:
                                this.DoStartFlow(para);
                                break;
                            case BP.WF.DoType.DoStartFlowByTemple:
                                this.DoStartFlowByTemple(para);
                                break;
                            case BP.WF.DoType.OpenFlow:
                                this.DoOpenFlow(para);
                                break;
                            case BP.WF.DoType.OpenDoc:
                                this.DoOpenDoc(para);
                                break;
                            case BP.WF.DoType.Send: //执行发送
                                this.DoSend(para);
                                break;
                            case BP.WF.DoType.DelFlow: //执行删除流程。
                                WebUser.FK_Flow = null;
                                WebUser.FK_Node = 0;
                                WebUser.WorkID = 0;
                                this.Close();
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("@错误：" + ex.Message + " PageID=" + pageID + "  DoType=" + para.DoType, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        private void FrmIE_Load(object sender, EventArgs e)
        {

        }
    }
}
