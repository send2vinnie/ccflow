using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CY.Net
{
    public class IISFtp : FTP
    {
        #region 构造函数

        public IISFtp(string server)
            : base(server)
        {
            //this.ResponseEncoding = System.Text.Encoding.GetEncoding("gb2312");
            this.ResponseEncoding = System.Text.Encoding.GetEncoding("utf-8");
        }

        public IISFtp(string server, string userName, string password)
            : this(server)
        {
            this.UserName = userName;
            this.Password = password;
        }

        #endregion

        #region 实现基类函数

        internal override bool ValidateStucture(string line)
        {
            return true;
        }

        internal override string GetName(string line)
        {
            string[] arr = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return line.Substring(line.LastIndexOf(arr[2]) + arr[2].Length + 1).Trim();
        }

        internal override bool IsFolder(string line)
        {
            string[] arr = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (arr.Length >= 3 && arr[2] == "<DIR>")
            {
                return true;
            }

            return false;
        }

        internal override long GetFileSize(string line)
        {
            if (!IsFolder(line))
            {
                return 0;
            }
            return 0;
        }

        internal override DateTime GetLastModifyTime(string line)
        {
            string[] arr = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int year, month, day, hour, minute;

            string[] date = arr[0].Split("-".ToCharArray());
            string[] time = arr[1].Split("-".ToCharArray());

            string nowYear = DateTime.Now.Year.ToString();
            year = Convert.ToInt32(nowYear.Substring(0, (nowYear.Length - date[2].Length)) + date[2]);
            month = Convert.ToInt32(date[0]);
            day = Convert.ToInt32(date[1]);

            if (arr[1].EndsWith("AM"))
            {
                time = arr[1].Replace("AM", "").Split(":".ToCharArray());
            }
            else if (arr[1].EndsWith("PM"))
            {
                time = arr[1].Replace("PM", "").Split(":".ToCharArray());
                time[0] = Convert.ToString(Convert.ToInt32(time[0]) + 12);
            }

            hour = Convert.ToInt32(time[0]);
            minute = Convert.ToInt32(time[1]);

            return new DateTime(year, month, day, hour, minute, 0);
        }

        #endregion
    }
}
