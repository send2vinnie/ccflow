using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FtpSupport;

namespace BP.GE
{
    public class GloDK
    {
        public static string NetDiskFtpIP
        {
            get
            {
                return SystemConfig.AppSettings["NetDiskFtpIP"];
            }
        }

        public static string NetDiskFtpUser
        {
            get
            {
                return SystemConfig.AppSettings["NetDiskFtpUser"];
            }
        }

        public static string NetDiskFtpPass
        {
            get
            {
                return SystemConfig.AppSettings["NetDiskFtpPass"];
            }
        }

        public static string NetDiskFtpPath
        {
            get
            {
                return SystemConfig.AppSettings["NetDiskFtpPath"];
            }
        }

        public static FtpConnection FileFtpConn
        {
            get
            {
                return new FtpConnection(GloDK.NetDiskFtpIP, GloDK.NetDiskFtpUser, GloDK.NetDiskFtpPass);
            }
        }

        public static string ConvertFileSize(object value)
        {
            string size = "0 Byte";

            if (value != null)
            {
                try
                {
                    long byteCount = Convert.ToInt64(value);

                    if (byteCount >= 1073741824)
                        size = String.Format("{0:#0.#}", byteCount / 1073741824.0) + " GB";
                    else if (byteCount >= 1048576)
                        size = String.Format("{0:#0.#}", byteCount / 1048576.0) + " MB";
                    else if (byteCount >= 0)
                        size = String.Format("{0:#0.#}", byteCount / 1024.0) + " KB";
                }
                catch
                {
                    return value.ToString() + " Byte";
                }
            }

            return size;
        }
        public static float ConvertFileSizeToMB(object value)
        {
            float size = 0;

            if (value != null)
            {
                try
                {
                    long byteCount = Convert.ToInt64(value);
                    size = (float)byteCount / (float)1048576;
                    size = Convert.ToSingle(size.ToString("f2"));
                    if (size == 0)
                        size = (float)0.01;
                }
                catch { size = 0; }
            }

            return size;
        }
    }
}
