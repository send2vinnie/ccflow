using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.Caching;
using System.Reflection;
namespace HelpSoft
{
    /// <summary>
    /// 大文件上传上下文
    /// </summary>
    public class UploadContext
    {
        private string m_TempFileDir;
        private string[] m_FileNames;
        private int m_TotalLength;
        private int m_ReadedLength;
        private DateTime m_StartReadDateTime;
        private string[] m_FileConId;
        private string m_GUID;
        private bool m_abort;
        private uploadStatus m_status;
        /// <summary>
        /// 大文件上传上下文类，构造函数
        /// </summary>
        /// <param name="page">上传文件的页面</param>
        /// <param name="TempFileDir">上传文件存放的临时目录</param>
        internal UploadContext(System.Web.UI.Page page, string TempFileDir)
        {
            m_TempFileDir = TempFileDir;
            if (m_TempFileDir.Trim()[m_TempFileDir.Trim().Length - 1] != '\\')
            {
                m_TempFileDir = m_TempFileDir.Trim() + "\\";
            }
            m_GUID = Guid.NewGuid().ToString();

            page.ClientScript.RegisterHiddenField("UploadID", m_GUID);
            page.Response.Expires = 1;
            m_status = uploadStatus.Initializing;
        }
        /// <summary>
        /// 上传临时文件存放路径
        /// </summary>
        public string TmepFileDir
        {
            get
            {
                return m_TempFileDir;
            }
            set
            {
                m_TempFileDir = value;
            }
        }

        /// <summary>
        /// 上传的文件名列表
        /// </summary>
        public string[] FileNames
        {
            get
            {
                return m_FileNames;
            }
            set
            {
                m_FileNames = value;
            }
        }
        /// <summary>
        /// 发送信息总长度
        /// </summary>
        public int TotalLength
        {
            get
            {
                return m_TotalLength;
            }
            set
            {
                m_TotalLength = value;
            }
        }
        /// <summary>
        /// 已接收的信息长度
        /// </summary>
        public int Readedlength
        {
            get
            {
                return m_ReadedLength;
            }
            set
            {
                m_ReadedLength = value;
            }
        }

        /// <summary>
        /// 开始接收时间
        /// </summary>
        public DateTime StartReadDateTime
        {
            get
            {
                return m_StartReadDateTime;
            }
            set
            {
                m_StartReadDateTime = value;
            }
        }
        /// <summary>
        /// 页面文件上传控件ID列表
        /// </summary>
        public string[] FileConIds
        {
            get
            {
                return m_FileConId;
            }
            set
            {
                m_FileConId = value;
            }
        }

        public string GUID
        {
            get
            {
                return m_GUID;
            }
        }
        public bool Abort
        {
            get
            {
                return m_abort;
            }
            set
            {
                m_abort = value;
            }
        }
        /// <summary>
        /// 上传速率,返回每秒上传的字节数
        /// </summary>
        public int Ratio
        {
            get
            {
                if (m_ReadedLength < 1)
                {
                    return 0;
                }
                else
                {
                    TimeSpan time = (DateTime.Now - m_StartReadDateTime);
                    if (time.TotalSeconds > 0)
                    {
                        return Convert.ToInt32(Math.Floor(m_ReadedLength / time.TotalSeconds));
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        /// <summary>
        /// 获取格式化的上传速度，以适当的字节，K字节，M字节表示
        /// </summary>
        public string FormatRatio
        {
            get
            {
                int ratio = Ratio;
                if (ratio < 1024)
                {
                    return ratio.ToString() + "B/秒";
                }
                else
                {
                    if (ratio < 1024 * 1024)
                    {
                        return Math.Round(ratio / 1024.0, 2).ToString() + "KB/秒";
                    }
                    else
                    {
                        return Math.Round(ratio / (1024.0 * 1024.0), 2).ToString() + "MB/秒";
                    }
                }
            }
        }
        /// <summary>
        /// 估计上传剩余时间,以秒为单位
        /// </summary>
        public int LeftTime
        {
            get
            {
                if (Ratio > 0)
                {
                    return Convert.ToInt32((m_TotalLength - m_ReadedLength) / Ratio);
                }
                else
                {
                    return -1;
                }
            }

        }
        /// <summary>
        /// 获取格式上的上传剩余时间，适当的以小时，分钟，秒表示
        /// </summary>
        public string FormatLeftTime
        {
            get
            {
                int leftTime = LeftTime;
                if (leftTime < 1)
                {
                    return "";
                }
                else
                {
                    if (leftTime < 60)
                    {
                        return leftTime.ToString() + "秒";
                    }
                    else
                    {
                        if (leftTime < 60 * 60)
                        {
                            return (leftTime / 60).ToString() + "分钟";
                        }
                        else
                        {
                            return (leftTime / 3600).ToString() + "小时";
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 当前正在处理的上传文件
        /// </summary>
        public string CurrentFile
        {
            get
            {
                if ((m_FileNames != null) && (m_FileNames.Length > 0))
                    return m_FileNames[m_FileNames.Length - 1];
                else
                    return "";
            }
        }
        /// <summary>
        /// 上传状态
        /// </summary>
        public uploadStatus Status
        {
            set
            {
                m_status = value;
            }
            get
            {
                return m_status;
            }
        }
        public string FormatStatus
        {
            get
            {
                switch (m_status)
                {
                    case uploadStatus.Complete:
                        {
                            return "成功上传完成";
                        }
                    case uploadStatus.HasError:
                        {
                            return "发生错误，上传取消";
                        }
                    case uploadStatus.Initializing:
                        {
                            return "初始化中...";
                        }
                    case uploadStatus.Uploading:
                        {
                            return "正在上传中...";
                        }
                    case uploadStatus.UserCancel:
                        {
                            return "用户中断了上传操作";
                        }

                }
                return "";

            }
        }
        /// <summary>
        /// 另存上传的数据文件
        /// </summary>
        /// <remarks>注意：这里不检测目标文件夹，目标文件上否有写权限，目标文件是否存在等情况，由调用者检测
        /// 另外，这里为了节约处理成本，采取的是移动文件的办法，所以只能成功调用一次本方法
        /// </remarks>
        /// <param name="fileControlId">文件控件名称，要求是客户名称</param>
        /// <param name="newPath">新存放的路径,可以包括文件名，如不指定文件名，则利用客户端上传时的文件名</param>
        /// <returns>操作成功返回真，否则返回假</returns>
        public bool SaveFile(string fileControlId, string newPath)
        {
            int index = Array.IndexOf(this.m_FileConId, fileControlId.ToLower());
            if (index < 0) return false;
            string fileName = this.m_GUID + (index + 1).ToString() + GetFileName(this.m_FileNames[index]);
            if (!File.Exists(this.TmepFileDir + fileName)) return false;
            if (GetFileName(newPath).Trim() == "")
            {
                newPath += GetFileName(this.m_FileNames[index]);
            }
            File.Move(this.TmepFileDir + fileName, newPath);
            return true;
        }
        /// <summary>
        /// 根据页面文件上传控件名称，获取上传的文件名
        /// </summary>
        /// <param name="fileControlId">文件上传控件名称，要求是客户端名称，页面的控件名称要求唯一</param>
        /// <returns>若上传有文件，则返回文件名</returns>
        public string GetFileNameByControl(string fileControlId)
        {
            int index = Array.IndexOf(this.m_FileConId, fileControlId.ToLower());
            if (index < 0) return "";
            return GetFileName(this.m_FileNames[index]);
        }
        /// <summary>
        /// 删除临时文件
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (m_FileNames == null) return;
            foreach (string FileName in m_FileNames)
            {
                try
                {
                    if (File.Exists(m_TempFileDir + m_GUID + FileName))
                    {
                        File.Delete(m_TempFileDir + m_GUID + FileName);
                    }
                }
                catch
                {
                }
            }
        }
        private string GetFileName(string PathFileName)
        {
            int index = PathFileName.LastIndexOf("\\");
            return PathFileName.Substring(index + 1, PathFileName.Length - index - 1);
        }
    }
    /// <summary>
    /// 上传文件工厂类
    /// </summary>
    public class UploadContextFactory
    {
        /// <summary>
        /// 根据页面对象和上传文件临时目录文件夹，获取一个文件上传上下文类实例
        /// </summary>
        /// <remarks>用户在页面上传初始化上传会话，要求页面每次刷新时均用此方法初始化一个上传上下文</remarks>
        /// <param name="page">页面对象</param>
        /// <param name="TempFileDir">文件存放临时文件夹</param>
        /// <returns>文件上传上下文</returns>
        public static UploadContext InitUploadContext(System.Web.UI.Page page, string TempFileDir)
        {
            UploadContext context = new UploadContext(page, TempFileDir);
            HttpContext.Current.Cache.Add(context.GUID, context, null, DateTime.Now.AddDays(10), TimeSpan.Zero, CacheItemPriority.High, null);
            return context;
        }
        /// <summary>
        /// 根据页面发送的上传会话编号，获取文件上传上下文
        /// </summary>
        /// <remarks>用于页面将文件上传后，在页面处理逻辑中，访问文件上传信息</remarks>
        /// <returns>文件上传上下文</returns>
        public static UploadContext GetUploadContext()
        {
            return (UploadContext)HttpContext.Current.Cache[HttpContext.Current.Request["UploadID"]];
        }
        /// <summary>
        /// 根据会话编号的GUID获取文件上传的上下文
        /// </summary>
        /// <remarks>用于在上传时记录上传信息</remarks>
        /// <param name="GUID">会话编号的GUID</param>
        /// <returns>文件上传上下文</returns>
        public static UploadContext GetUploadContext(string GUID)
        {
            return (UploadContext)HttpContext.Current.Cache[GUID];
        }
        /// <summary>
        /// 在页面逻辑处理完成后，释放上传上下文，并删除临时文件
        /// </summary>
        public static void Release()
        {
            UploadContext up = (UploadContext)HttpContext.Current.Cache[HttpContext.Current.Request["UploadID"]];
            up.Dispose(true);
            HttpContext.Current.Cache.Remove(HttpContext.Current.Request["UploadID"]);
        }

    }

    /// <summary>
    /// 上传状态
    /// </summary>
    public enum uploadStatus
    {
        Initializing,
        Uploading,
        HasError,
        UserCancel,
        Complete
    }
}
