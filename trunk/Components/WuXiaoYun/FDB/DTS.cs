using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using BP.DA;

namespace BP.GE
{
    public class FDBDTS : BP.En.Method
    {
        public FDBDTS()
        {
            this.Title = "读取目录资源到数据库";
        }
        public override bool IsCanDo
        {
            get { return true; }
        }

        /// <summary>
        /// 设置执行变量
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
            this.Warning = "您确定要执行吗？";
            HisAttrs.AddTBString("P1", @"E:\文件柜测试\", "资源的路径", true, false, 0, 50, 100);
        }

        public override object Do()
        {
            try
            {
                string basePath = this.GetValStrByKey("P1");
                //从FTP读取数据
                if (basePath.ToLower().StartsWith("ftp://"))
                {
                    DoFTP(basePath);
                }
                //从磁盘目录读取数据
                else
                {
                    FDBDir.PathFDBSource = basePath;
                    FDBDir.intIt(basePath);
                }

                return "执行成功.";
            }
            catch (Exception ex)
            {
                return "执行失败:" + ex.Message;
            }
        }
        public void GetFileList(string ftpUrl)
        {
            string[] downloadFiles; 
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(ftpUrl); reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential("S1\\Administrator", "863");
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory; WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine(); while (line != null)
                {
                    result.Append(line); result.Append("\n"); line = reader.ReadLine();
                }
                //toremovethetrailing＇\n＇result.Remove(result.ToString().LastIndexOf(＇\n＇),1);reader.Close();response.Close();returnresult.ToString().Split(＇\n＇);}catch(Exceptionex){System.Windows.Forms.MessageBox.Show(ex.Message);downloadFiles=null;returndownloadFiles;}}
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DoFTP(string ftpUrl)
        {
            /////////////////////////////
            Uri serverUri = new Uri(ftpUrl);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential("S1\\Administrator", "863");
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            // The following streams are used to read the data returned from the server.
            Stream responseStream = null;
            StreamReader readStream = null;
            try
            {
                responseStream = response.GetResponseStream();
                readStream = new StreamReader(responseStream, System.Text.Encoding.UTF8);

                if (readStream == null)
                {
                    throw new Exception("FTP目录不包含任何数据。");
                }
                //清空数据库
                DBAccess.RunSQL("DELETE GE_FDBDir");
                DBAccess.RunSQL("DELETE GE_FDB");

                string line = readStream.ReadLine();
                while (line != null)
                {
                     
                    //if(line.)
                    //string dirShortName = dir.Substring(dir.LastIndexOf("\\") + 1);
                    //string no = dirShortName.Substring(0, 2);

                    //FDBDir d = new FDBDir();
                    //d.No = no;
                    //d.Name = dirShortName.Substring(3);
                    //d.Grade = 1;
                    //d.FileNum = System.IO.Directory.GetFiles(dir).Length; /*本目录下的文件个数*/
                    //d.ParentDir = "\\";
                    //d.NameFull = dir;
                    //d.Insert();
                    //FDBDir.intItFiles(d, dir);
                    //FDBDir.intIt(dir, no);

                    //line = reader.ReadLine();
                }

            }
            finally
            {
                if (readStream != null)
                {
                    readStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }


        }

    }
}
