using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using BP.Port;
using System.Windows.Forms;
using BP.WF;

namespace WFWord2007
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            try
            {
                WebUser.HisRib.ReSetState();
            }
            catch (Exception ex)
            {
            }
            //  this.HisRibbon1.ReSetState();
            //object index = 1;
            //Word.Bookmark firstBookmark = this.Application.ActiveDocument.Bookmarks.get_Item(ref index);
            //Document extendedDocument = this.Application.ActiveDocument.GetVstoObject();
            //Bookmark vstoBookmark = extendedDocument.Controls.AddBookmark(
            //        firstBookmark, "VSTOBookmark");
            //Microsoft.Office.Tools.Word.Bookmark firstParagraph;
            //firstParagraph = this.Controls.AddBookmark(this.Paragraphs[1].Range,
            //    "FirstParagraph");
            //    Globals.ThisAddIn.Application.DocumentOpen += new Microsoft.Office.Interop.Word.ApplicationEvents4_DocumentOpenEventHandler(Application_DocumentOpen);
            //  this.HisRibbon1 = this.CreateRibbonObjects();
            //Ribbon1 rb = new Ribbon1();
            //rb.ReSetState();
        }

        void Application_DocumentOpen(Microsoft.Office.Interop.Word.Document Doc)
        {
            MessageBox.Show(Doc.Name + " is open");
            //HisRibbon1.ReSetState();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            if (Profile.IsExitProfile == false)
                return;

            //DialogResult dl = MessageBox.Show("您要安全退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if (dl == DialogResult.OK)
            //{
            //    System.IO.File.Delete("C:\\WF\\Profile.txt");
            //    BP.Port.WebUser.WriterCookes();
            //}
        }

        #region 共用方法
        public void DoSave()
        {
            if (WebUser.FK_Flow == null)
            {
                MessageBox.Show("您没有选择流程您不能存盘。");
                return;
            }




            if (WebUser.WorkID == 0)
            {
                //通过模板新的一个公文，创建一个草稿，生成工作ID。
                Work wk = new Work();
                wk.NodeID = WebUser.FK_Node;
                wk.Title = "公文" + DateTime.Now.ToString("MM月dd号");
                wk.RDT = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
                wk.CDT = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
                wk.Rec = WebUser.No;
                wk.FK_Dept = WebUser.FK_Dept;
                wk.Insert();
                WebUser.WorkID = wk.OID;
                WebUser.HisWork = wk;
            }
            else
            {
                if (WebUser.IsStartNode == false)
                {
                    //*判断是否可以执行当前的工作，如果不可以就提示不让保存。 */
                    //string sql = "SELECT COUNT( a.Workid) FROM dbo.WF_GenerWorkFlow a , dbo.WF_GenerWorkerList b WHERE a.workid=b.workid and a.fk_node=b.fk_node and b.fk_node=" + WebUser.FK_Node + " and b.fk_emp='" + WebUser.No + "' and b.IsEnable=1 and a.workid=" + WebUser.WorkID;
                    //if (DBAccess.RunSQLReturnTable(sql).Rows[0][0].ToString() == "0")
                    //{
                    //    MessageBox.Show("您不能将此文件存储在服务器上。原因如下：\t\n1，此工作已经完成，文件您不能在修改它。\t\n2、当前文件非您创建或者编辑的。", "保存错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                }
            }

            #region 把文件放在服务器上
            FtpSupport.FtpConnection conn = Glo.HisFtpConn;
            try
            {
                conn.SetCurrentDirectory("/DocFlow/" + WebUser.FK_Flow + "/" + WebUser.WorkID + "/");
            }
            catch
            {
                if (conn.DirectoryExist("/DocFlow/" + WebUser.FK_Flow + "/") == false)
                    conn.CreateDirectory("/DocFlow/" + WebUser.FK_Flow + "/");

                if (conn.DirectoryExist("/DocFlow/" + WebUser.FK_Flow + "/" + WebUser.WorkID + "/") == false)
                    conn.CreateDirectory("/DocFlow/" + WebUser.FK_Flow + "/" + WebUser.WorkID);

                conn.SetCurrentDirectory("/DocFlow/" + WebUser.FK_Flow + "/" + WebUser.WorkID + "/");
            }
            string file = Glo.PathOfTInstall + DateTime.Now.ToString("MMddhhmmss") + ".doc";
            ThisAddIn.SaveAs(file);

            System.IO.File.Copy(file, "c:\\Tmp.doc", true);
            conn.PutFile("c:\\Tmp.doc", WebUser.FK_Node + "@" + WebUser.No + ".doc");
            conn.PutFile("c:\\Tmp.doc", WebUser.WorkID + ".doc");
            conn.Close();
            #endregion 把文件放在服务器上

            System.IO.File.Delete("c:\\Tmp.doc");
            MessageBox.Show("您的文件已经保存到服务器上", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
            this.Application.DocumentChange += new Microsoft.Office.Interop.Word.ApplicationEvents4_DocumentChangeEventHandler(Application_DocumentChange);
        }
        void Application_DocumentChange()
        {
            // System.Windows.Forms.MessageBox.Show("Application_DocumentChange");
            //  WebUser.HisRib.ReSetState();
        }

        public static void SaveAs(string file)
        {
            object missing = Type.Missing;
            object FileName = file;
            object FileFormat = Word.WdSaveFormat.wdFormatDocument;
            object LockComments = missing;
            object AddToRecentFiles = missing;
            object ReadOnlyRecommended = missing;
            object EmbedTrueTypeFonts = missing;
            object SaveNativePictureFormat = missing;
            object SaveFormsData = missing;
            object SaveAsAOCELetter = missing;
            object Encoding = missing;
            object InsertLineBreaks = missing;
            object AllowSubstitutions = missing;
            object LineEnding = missing;
            object AddBiDiMarks = missing;

            Globals.ThisAddIn.Application.ActiveWindow.Document.Save();
            Globals.ThisAddIn.Application.ActiveWindow.Document.SaveAs(ref FileName, ref FileFormat, ref
            LockComments, ref missing, ref AddToRecentFiles, ref missing, ref
            ReadOnlyRecommended, ref EmbedTrueTypeFonts, ref SaveNativePictureFormat, ref
            SaveFormsData, ref SaveAsAOCELetter, ref Encoding, ref InsertLineBreaks, ref
            AllowSubstitutions, ref LineEnding, ref AddBiDiMarks);
        }

        public static void SaveAs_bak(string file)
        {
            object FileName = file;
            object FileFormat = Word.WdSaveFormat.wdFormatDocument;
            object LockComments = false;
            object AddToRecentFiles = true;
            object ReadOnlyRecommended = true;
            object EmbedTrueTypeFonts = true;
            object SaveNativePictureFormat = true;
            object SaveFormsData = false;
            object SaveAsAOCELetter = false;
            object Encoding = 20127;
            object InsertLineBreaks = false;
            object AllowSubstitutions = false;
            object LineEnding = Word.WdLineEndingType.wdCRLF;
            object AddBiDiMarks = false;
            object missing = Type.Missing;

            Globals.ThisAddIn.Application.ActiveWindow.Document.SaveAs(ref FileName, ref FileFormat, ref
            LockComments, ref missing, ref AddToRecentFiles, ref missing, ref
            ReadOnlyRecommended, ref EmbedTrueTypeFonts, ref SaveNativePictureFormat, ref
            SaveFormsData, ref SaveAsAOCELetter, ref Encoding, ref InsertLineBreaks, ref
            AllowSubstitutions, ref LineEnding, ref AddBiDiMarks);
        }
        #endregion
    }
}
