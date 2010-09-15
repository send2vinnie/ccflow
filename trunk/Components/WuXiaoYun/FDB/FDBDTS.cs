using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using BP.DA;
using FtpSupport;

namespace BP.GE
{
    public static class FDBDTS
    {
        public static string Do(string basePath, int resType,string fk_imgLink)
        {
            try
            {
                ////////test////
                //basePath = @"ftp://192.168.0.164/FDB/01.爱国主义教育";
                //resType = 1;
                ///////////////
                if (basePath.EndsWith ("/"))
                {
                    basePath = basePath.Remove(basePath.LastIndexOf("/"));
                }
                FDBDTS.PathFDBSource = basePath;
                switch (resType)
                {
                    //从FTP读取数据
                    case 1:
                        //清空数据库
                        DBAccess.RunSQL("DELETE FROM GE_FDBDir WHERE MyPK Like '"+fk_imgLink +"@%'");
                        DBAccess.RunSQL("DELETE FROM GE_FDB WHERE MyPK Like '" + fk_imgLink + "@%'");

                        FDBDTS.ReadFTP(basePath, "", fk_imgLink);
                        break;
                    //从磁盘目录读取数据
                    case 2:
                        FDBDTS.intIt(basePath);
                        break;

                    default:
                        break;
                }
                return "执行成功.";
            }
            catch (Exception ex)
            {
                return "执行失败:" + ex.Message;
            }
        }

        #region 从FTP读取数据
        //FTP服务器的IP和根路径
        public static string FtpIP = BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s", "FtpIP");
        public static string FtpPath = BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s", "FtpPath");
        public static string FtpUser = BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s", "FtpUser");
        public static string FtpPass = BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s", "FtpPass");
        public static string PathFDBSource = "";

        //获取FTP连接
        public static FtpConnection FileFtpConn
        {
            get
            {
                //string ftpPath = BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s", "FtpPath");
                //string ftpUrl = "ftp://" + ftpUser + ":" + ftpPass + "@" + ftpIP + ftpPath;
                return new FtpConnection(FtpIP, FtpUser, FtpPass);
            }
        }

        public static void ReadFTP(string ftpPath, string parentDir, string fk_imgLink)
        {
            // 设置在服务器上的文件的路径
            string ftpUrl = "ftp://" + FDBDTS.FtpIP;
            FtpSupport.FtpConnection conn = FDBDTS.FileFtpConn;
            if (ftpPath.ToLower() != ftpUrl.ToLower())
            {
                conn.SetCurrentDirectory(ftpPath.Replace(ftpUrl, ""));
            }

            // 读取目录和文件
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpPath);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(FtpUser, FtpPass);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            // The following streams are used to read the data returned from the server.
            Stream responseStream = null;
            StreamReader readStream = null;
            try
            {
                responseStream = response.GetResponseStream();
                readStream = new StreamReader(responseStream, System.Text.Encoding.Default);

                if (readStream == null)
                {
                    throw new Exception("FTP目录不包含任何数据。");
                }

                string ftpFile = readStream.ReadLine();

                int dirNum = 1;
                int fileNum = 1;
                //bool dirExsist = false;
                while (ftpFile != null)
                {
                    //string[] fileDtls = ftpFile.Split("\r\n",StringSplitOptions.RemoveEmptyEntries);
                    string tempStr = "";

                    //创建日期 (**********待改*************************)
                    string dateStr = ftpFile.Substring(0, ftpFile.IndexOf('M') + 1);
                    string createDate = dateStr.Substring(0, dateStr.IndexOf(' '));

                    //是文件夹或者文件大小
                    tempStr = ftpFile.Replace(dateStr, "").Trim();
                    string fileTypeOrSize = tempStr.Substring(0, tempStr.IndexOf(' ') + 1);
                    //文件或文件夹名称
                    string fileName = tempStr.Replace(fileTypeOrSize, "").Trim();

                    //目录
                    tempStr = tempStr.Replace(fileTypeOrSize, "").Trim();
                    if (fileTypeOrSize.ToUpper().Contains("DIR"))
                    {
                        FDBDir d = new FDBDir();
                        d.No = parentDir + dirNum.ToString().PadLeft(2, '0');
                        d.MyPK = fk_imgLink + "@" + d.No;
                        d.Name = fileName;
                        d.Grade = 1;
                        //d.FileNum = System.IO.Directory.GetFiles(dir).Length; /*本目录下的文件个数*/
                        d.ParentDir = ftpPath.Replace(FDBDTS.PathFDBSource, "");
                        if (d.ParentDir == "")
                        {
                            d.ParentDir = "/";
                        }
                        d.NameFull = ftpPath + "/" + fileName;
                        d.Insert();
                        dirNum++;

                        ReadFTP(d.NameFull, d.No,fk_imgLink);
                    }
                    //文件
                    else
                    {
                        if (conn.GetCurrentDirectory().ToLower() != (ftpPath.Replace(ftpUrl, "")).ToLower())
                        {

                            conn.SetCurrentDirectory(ftpPath.Replace(ftpUrl, ""));
                        }
                        FDB fdb = new FDB();
                        if (parentDir == "")
                        {
                            parentDir = "00";
                        }
                        fdb.No = parentDir + "_" + fileNum;
                        fdb.MyPK = fk_imgLink + "@" + fdb.No;
                        fdb.NameFull = ftpPath + "/" + fileName;
                        fdb.NameS = fileName;
                        fdb.Ext = fileName.Substring(fileName.LastIndexOf(".") + 1);
                        fdb.FK_FDBDir = parentDir;

                        FtpSupport.FtpStream ftpFS = conn.OpenFile(fileName, FtpSupport.GenericRights.Read);
                        fdb.FSize = int.Parse(fileTypeOrSize);
                        fdb.CDT = createDate;
                        //fdb.PayCent = paycent;
                        fdb.Insert();
                        ftpFS.Close();

                        fileNum++;
                    }
                    ftpFile = readStream.ReadLine();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        #endregion


        #region 从磁盘目录读取数据

        /// <summary>
        /// 入口
        /// </summary>
        public static void intIt(string basePath)
        {
            string[] dirs = System.IO.Directory.GetDirectories(basePath); // 获取文件夹。
            DBAccess.RunSQL("DELETE GE_FDBDir");
            DBAccess.RunSQL("DELETE GE_FDB");
            foreach (string dir in dirs)
            {
                string dirShortName = dir.Substring(dir.LastIndexOf("\\") + 1);
                string no = dirShortName.Substring(0, 2);

                FDBDir d = new FDBDir();
                d.No = no;
                d.Name = dirShortName.Substring(3);
                d.Grade = 1;
                d.FileNum = System.IO.Directory.GetFiles(dir).Length; /*本目录下的文件个数*/
                d.ParentDir = "\\";
                d.NameFull = dir;
                d.Insert();
                FDBDTS.intItFiles(d, dir);
                FDBDTS.intIt(dir, no);
            }
        }
        /// <summary>
        /// 递归调用
        /// </summary>
        /// <param name="baseDir"></param>
        /// <param name="parentNo"></param>
        public static void intIt(string baseDir, string parentNo)
        {
            try
            {
                string[] strs = System.IO.Directory.GetDirectories(baseDir);
                foreach (string str in strs)
                {
                    string dirShortName = str.Substring(str.LastIndexOf("\\") + 1);
                    string no = dirShortName.Substring(0, 2);
                    FDBDir dir = new FDBDir();
                    dir.No = parentNo + no;
                    dir.Name = dirShortName.Substring(3);
                    dir.Grade = dir.No.Length / 2;
                    dir.FileNum = System.IO.Directory.GetFiles(str).Length; /*本目录下的文件个数*/
                    dir.ParentDir = baseDir.Replace(FDBDTS.PathFDBSource, "\\");
                    dir.NameFull = str;
                    dir.Insert();

                    FDBDTS.intItFiles(dir, str);
                    FDBDTS.intIt(str, dir.No);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "  baseDir=" + baseDir);
            }
        }
        /// <summary>
        /// 文件信息
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="path"></param>
        public static void intItFiles(FDBDir dir, string path)
        {
            string[] strs = System.IO.Directory.GetFiles(path);
            for (int i = 0; i < strs.Length; i++)
            {
                string file = strs[i];
                string shortName = file.Substring(file.LastIndexOf("\\") + 1);
                string ext = shortName.Substring(shortName.LastIndexOf(".") + 1); // 文件后缀。
                int paycent = 0;
                if (shortName.IndexOf("@") != -1)
                {
                    string centStr = shortName.Substring(shortName.LastIndexOf("@") + 1); // 得到 xxx.doc 部分。
                    centStr = centStr.Replace("." + ext, "");
                    paycent = int.Parse(centStr);
                }

                FDB fdb = new FDB();
                fdb.MyPK = dir.No + "_" + i;
                fdb.NameFull = file;
                fdb.NameS = shortName;
                fdb.Ext = ext;
                fdb.FK_FDBDir = dir.No;
                FileInfo info = new FileInfo(fdb.NameFull);
                fdb.FSize = Convert.ToInt32(BP.DA.DataType.PraseToMB(info.Length));
                fdb.CDT = info.CreationTime.ToString(DataType.SysDataFormat);
                fdb.PayCent = paycent;
                fdb.Insert();
            }
        }

        #endregion
    }
}
