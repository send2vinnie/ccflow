using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Web;

namespace BP.CY.Net
{
    public enum Authentication
    {
        Anonymous = 0,//匿名
        Basic = 1//基本
    }

    public abstract class FTP
    {
        #region 构造函数

        public FTP(string server)
        {
            this.Server = server;
        }

        public FTP(string server, string userName, string password)
            : this(server)
        {
            this.UserName = userName;
            this.Password = password;
        }

        #endregion

        #region 字段

        //月份字典
        internal Dictionary<string, int> dicMonth = new Dictionary<string, int>{
            {"jan",1},
            {"feb",2},
            {"mar",3},
            {"apr",4},
            {"may",5},
            {"jun",6},
            {"jul",7},
            {"aug",8},
            {"sep",9},
            {"oct",10},
            {"nov",11},
            {"dec",12}
        };


        FtpWebRequest ftpRequest = null;
        FtpWebResponse ftpResponse = null;

        private Authentication authenticationType = Authentication.Basic;
        private Encoding responseEncoding = Encoding.Default;

        #endregion

        #region 属性

        /// <summary>
        /// 服务器
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 请求的路径
        /// </summary>
        public string RequestPath { get; set; }

        /// <summary>
        /// 身份验证类型
        /// </summary>
        public Authentication AuthenticationType
        {
            get
            {
                return this.authenticationType;
            }
            set
            {
                this.authenticationType = value;
            }
        }

        /// <summary>
        /// 响应编码
        /// </summary>
        public Encoding ResponseEncoding
        {
            get
            {
                return this.responseEncoding;
            }
            set
            {
                this.responseEncoding = value;
            }
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 初始化FTPRequest
        /// </summary>
        internal void InitFtpRequest()
        {
            InitFtpRequest("");
        }

        /// <summary>
        /// 初始化FTPRequest
        /// </summary>
        internal void InitFtpRequest(string serverPath)
        {
            switch (AuthenticationType)
            {
                case Authentication.Anonymous:
                    {
                        break;
                    }
                case Authentication.Basic:
                    {
                        if (string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.Password))
                        {
                            throw new Exception("用户名或密码不能为空！");
                        }

                        ftpRequest = (FtpWebRequest)WebRequest.Create(serverPath);
                        ftpRequest.Credentials = new NetworkCredential(this.UserName, this.Password);
                        ftpRequest.KeepAlive = false;
                        break;
                    }
            }
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        internal bool IsNumber(string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                return (new Regex("^[0-9]*$").Match(val)).Success;
            }
            return false;
        }

        /// <summary>
        /// 解析参数
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        internal FsItem ParseItem(string line)
        {
            if (!string.IsNullOrEmpty(line) && ValidateStucture(line))
            {
                FsItem item = new FsItem();

                item.Name = GetName(line);

                item.Path = this.RequestPath + item.Name;
                item.IsFolder = IsFolder(line);
                item.LastModifyTime = GetLastModifyTime(line);

                if (!item.IsFolder)
                {
                    item.Size = GetFileSize(line);
                }

                return item;
            }

            return null;
        }

        #endregion

        #region 抽象函数

        /// <summary>
        /// 验证结构是是否是文件夹或文件
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        internal abstract bool ValidateStucture(string line);

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        internal abstract string GetName(string line);

        /// <summary>
        /// 判断是否是文件夹
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        internal abstract bool IsFolder(string line);

        /// <summary>
        /// 解析文件大小
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        internal abstract long GetFileSize(string line);

        /// <summary>
        /// 获取最后修改时间
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        internal abstract DateTime GetLastModifyTime(string line);

        #endregion

        #region 公共函数

        /// <summary>
        /// 转换成ftp服务器的路径 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ToServerPath(string path)
        {
            string serverPath = string.Empty;

            if (string.IsNullOrEmpty(path))
            {
                serverPath = "ftp://" + this.Server;
            }
            else if (!path.ToLower().StartsWith("ftp"))
            {
                serverPath = "ftp://" + this.Server + "/" + path;
            }
            else
            {
                serverPath = path;
            }

            return serverPath;
        }

        /// <summary>
        /// 验证路径是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CheckFileExists(string path)
        {
            path = path.Trim();

            if (path.Substring(path.LastIndexOf("/") + 1).LastIndexOf(".") < 0)
            {
                return false;
            }

            bool exists = true;

            try
            {
                InitFtpRequest(path);

                ftpRequest.Timeout = 1000;
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

                ftpResponse.Close();
            }
            catch (Exception ex)
            {
                exists = false;
            }

            return exists;
        }

        /// <summary>
        /// 列出文件详细信息
        /// </summary>
        public List<FsItem> GetDirectoryList(string path)
        {
            StreamReader reader = null;
            List<FsItem> lstFile = new List<FsItem>();
            path = ToServerPath(path);

            if (!path.EndsWith("/"))
            {
                path += "/";
            }

            this.RequestPath = path;

            try
            {
                InitFtpRequest(path);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                reader = new StreamReader(ftpRequest.GetResponse().GetResponseStream(), this.responseEncoding);

                FsItem item = null;
                string line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    item = ParseItem(line);

                    if (item != null)
                    {
                        item.Path = path + item.Name;
                        lstFile.Add(item);
                    }

                    line = reader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return lstFile;
        }

        /// <summary>
        /// 上传文件 
        /// </summary>
        /// <param name="path"></param>
        public void UploadFile(string srcFilePath, string targetFilePath)
        {
            if (!string.IsNullOrEmpty(srcFilePath) && !string.IsNullOrEmpty(targetFilePath))
            {
                targetFilePath = ToServerPath(targetFilePath);
                InitFtpRequest(targetFilePath);

                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.UseBinary = true;

                byte[] buff = new byte[1024];
                FileStream stream = null;

                try
                {
                    stream = new FileStream(srcFilePath, FileMode.Open, FileAccess.Read);
                    ftpRequest.ContentLength = stream.Length;
                    Stream ftpStream = ftpRequest.GetRequestStream();

                    int bytesRead;
                    while (true)
                    {
                        bytesRead = stream.Read(buff, 0, buff.Length);

                        if (bytesRead == 0)
                        {
                            break;
                        }
                        ftpStream.Write(buff, 0, bytesRead);
                    }

                    ftpStream.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">要下载的文件路径</param>
        /// <param name="targeFilePath">目标存放路径</param>
        public void DownloadFile(string srcFilePath, string saveFilePath)
        {
            InitFtpRequest(srcFilePath);

            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;

            Stream responseStream = null;
            Stream fileStream = null;

            try
            {
                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                responseStream = ftpResponse.GetResponseStream();
                fileStream = File.Create(saveFilePath);
                byte[] buffer = new byte[1024];

                int bytesRead;
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    fileStream.Write(buffer, 0, bytesRead);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
                if (ftpResponse != null)
                {
                    ftpResponse.Close();
                }
            }
        }

        #endregion
    }
}
