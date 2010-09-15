using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using BP.DA;
using FtpSupport;
using Microsoft.Win32;
using System.Web;

namespace BP.GE
{
    public static class GloFTP
    {
        #region  属性
        //FTP服务器的IP和根路径
        public static string FtpIP
        {
            get
            {
                return BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s", "FtpIP");
            }
        }

        public static string FtpUser
        {
            get
            {
                return BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s", "FtpUser");
            }
        }
        public static string FtpPass
        {
            get
            {
                return BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s", "FtpPass");
            }
        }

        public static string FtpPath
        {
            get
            {
                return BP.Sys.EnsAppCfgs.GetValString("BP.GE.ImgLink1s", "FtpPath");
            }
        }
        #endregion

        // 获取FTP连接

        public static FtpConnection FileFtpConn
        {
            get
            {
                return new FtpConnection(FtpIP, FtpUser, FtpPass);
            }
        }

        #region 打开FTP资源管理器
        public static void OpenFtp()
        {
            string ftpUrl = "ftp://" + GloFTP.FtpUser + ":" + GloFTP.FtpPass + "@" + GloFTP.FtpIP + "/" + GloFTP.FtpPath;
            System.Diagnostics.Process.Start("Explorer.exe", ftpUrl);
        }
        #endregion

        #region FTP上传和下载
        ///// <summary>
        ///// FTP上传
        ///// </summary>
        ///// <param name="ext"></param>
        ///// <param name="dkFile"></param>
        //private static void UploadFile(string ftpDir, string fileName)
        //{
        //    //保存文件
        //    try
        //    {
        //        // 设置文件路径
        //        string folder = GetUploadFolder();
        //        if (Directory.Exists(Server.MapPath("/FDB/YanFa/YunHui")) == false)
        //            Directory.CreateDirectory(Server.MapPath("/FDB/YanFa/YunHui"));

        //        // 文件
        //        string localFile = folder + dkFile.MyFileName;


        //        // 设置在服务器上的文件的路径
        //        FtpSupport.FtpConnection conn = FDBDTS.FileFtpConn;
        //        conn.SetCurrentDirectory("/FDB");
        //        if (!conn.DirectoryExist(FDBDTS.FtpPath))
        //            conn.CreateDirectory(FDBDTS.FtpPath);
        //        conn.SetCurrentDirectory("/" + FDBDTS.FtpPath);
        //        if (!conn.DirectoryExist(DKFileAdds.FK_Dept))
        //            conn.CreateDirectory(DKFileAdds.FK_Dept);
        //        conn.SetCurrentDirectory("/" + FDBDTS.FtpPath + "/" + DKFileAdds.FK_Dept);
        //        if (!conn.DirectoryExist(DKFileAdds.FK_Emp))
        //            conn.CreateDirectory(DKFileAdds.FK_Emp);
        //        conn.SetCurrentDirectory("/" + FDBDTS.FtpPath + "/" + DKFileAdds.FK_Dept + "/" + DKFileAdds.FK_Emp);

        //        //try
        //        //{
        //        // 向服务器上存放文件
        //        conn.PutFile(Server.MapPath(localFile), dkFile.OID + "." + ext);

        //        // 删除IIS服务器上的文件
        //        File.Delete(Server.MapPath(localFile));

        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    if (conn.FileExist("/" + DKFileAdds.FK_Dept + "/" + DKFileAdds.FK_Emp + "/" + dkFile.OID + "." + ext) == false)
        //        //        throw ex;
        //        //}s
        //    }
        //    catch (Exception ex1)
        //    {
        //        dkFile.Delete();

        //        return;
        //    }
        //}

        /// <summary>
        /// FTP下载(/FTP/file.ext)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DownLoadFile(string ftpDir)
        {
            // 文件的存放目录
            string filePath = ftpDir;
            string fileName = ftpDir.Substring(ftpDir.LastIndexOf("/") + 1);
            ftpDir = ftpDir.Replace(fileName, "");

            string fileExt = fileName.Substring(fileName.LastIndexOf(".") + 1);
            string DEFAULT_CONTENT_TYPE = "application/unknown";
            RegistryKey regKey, fileExtKey;
            string fileContentType;
            try
            {
                regKey = Registry.ClassesRoot;
                fileExtKey = regKey.OpenSubKey(fileExt);
                fileContentType = fileExtKey.GetValue("Content   Type", DEFAULT_CONTENT_TYPE).ToString();
            }
            catch
            {
                fileContentType = DEFAULT_CONTENT_TYPE;
            }
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;   fileName=" + HttpUtility.UrlEncode(fileName).Replace("+", "%2b").Replace("=", "%3d"));
            System.Web.HttpContext.Current.Response.ContentType = fileContentType;

            try
            {
                // 获取FTP的链接
                FtpConnection conn = FDBDTS.FileFtpConn;
                conn.SetCurrentDirectory(ftpDir);
                if (conn.FileExist(fileName))
                {
                    FtpSupport.FtpStream ftpFS = conn.OpenFile(fileName, FtpSupport.GenericRights.Read);
                    byte[] buffer = new byte[10240];
                    int n = ftpFS.Read(buffer, 0, buffer.Length);
                    while (n > 0)
                    {
                        System.Web.HttpContext.Current.Response.BinaryWrite(buffer);
                        n = ftpFS.Read(buffer, 0, buffer.Length);
                    }
                    ftpFS.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("下载失败，详细信息：" + ex.Message);
            }
            System.Web.HttpContext.Current.Response.End();
        }

        #endregion

    }
}
