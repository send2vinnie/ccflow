using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.IO;
using BP.En;
using BP.En.Base;

namespace BP.RB
{
    public class FileType
    {
        public const string txt = "txt";
        public const string ppt = "ppt";
        public const string doc = "doc";
        public const string rtf = "rtf";
        public const string xls = "xls";
        public const string rar = "rar";
        public const string zip = "zip";
        public const string exe = "exe";
        public const string pdf = "pdf";

    }
    /// <summary>
    /// Href
    /// </summary>
    public class Href
    {
        public System.Text.Encoding HisEncoder = Encoding.UTF7;

        public string _Url = "";
        public string HostName = "";
        public string HostNameOfUrl
        {
            get
            {
                if (this.IsLocalHost)
                    return this.HostName;

                string str = this.Url.ToLower();
                str = str.Replace("http://", "");

                if (str.Contains("/"))
                    str = str.Substring(0, str.IndexOf('/'));

                return str;
            }
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path = "";

        /// <summary>
        /// 是否是一个文件
        /// </summary>
        public string GetFileType
        {
            get
            {
                string str = this.Url;
                str = str.ToLower();

                if (str.Contains(".doc"))
                    return FileType.doc;
                else if (str.Contains(".xls"))
                    return FileType.xls;
                else if (str.Contains(".rar"))
                    return FileType.rar;
                else if (str.Contains(".zip"))
                    return FileType.zip;
                else if (str.Contains(".ppt"))
                    return FileType.ppt;
                else if (str.Contains(".txt"))
                    return FileType.txt;

                else if (str.Contains(".rtf"))
                    return FileType.rtf;

                else if (str.Contains(".pdf"))
                    return FileType.pdf;

                else if (str.Contains(".exe"))
                    return FileType.exe;
                else
                    return null;
            }
        }
        public bool IsFile
        {
            get
            {
                if (this.GetFileType == null)
                    return false;

                return true;
            }
        }
        /// <summary>
        /// 是否在本机上
        /// </summary>
        public bool IsLocalHost
        {
            get
            {
                string v1 = this.Url.ToLower();
                string v2 = this.HostName.ToLower().Replace("www", "");
                if (v1.Contains(v2))
                    return true;

                return false;
            }
        }
        /// <summary>
        /// 在page 中表示出来的url .
        /// </summary>
        public string PageUrl = null;
        public string Url
        {
            get
            {
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");
                _Url = _Url.Replace("//", "/");

                _Url = _Url.Replace(":/", "://");

                return _Url;
            }
            set
            {
                _Url = value;
            }
        }
        private string _Lab = "";
        public string Lab
        {
            get
            {
                if (_Lab == "")
                    return "无标题";

                return _Lab;
            }
            set
            {
                _Lab = value;
            }
        }

        public string Host_del = "";
        public string Context = "";
        public int Idx = 0;

        public Href()
        {

        }
    }

    public class Hrefs:System.Collections.CollectionBase
    {
        public bool Contains(string url)
        {
            foreach (Href h in this)
                if (h.Url == url)
                    return true;

            return false;
        }
        public bool Contains(Href href)
        {
            foreach (Href h in this)
            {
                if (h.Url == href.Url
                    || h.Url.Contains(href.Url)
                    || href.Url.Contains(h.Url)
                    )
                {
                    return true;
                }
            }
            return false;
        }

        public Hrefs()
        { 

        }
        /// <summary>
        /// 增加一个href
        /// </summary>
        /// <param name="h"></param>
        public void Add(Href h)
        {
            if (this.Contains(h))
                return;


            h.Idx = this.Count;
            this.InnerList.Add(h);
        }
        public void Add(Hrefs hfs)
        {
            foreach (Href hf in hfs)
                this.Add(hf);
        }
        public Href GetByIdx(int idx)
        {
            return this.InnerList[idx] as Href;
        }
        /// <summary>
        /// 获取文件url.
        /// </summary>
        /// <returns></returns>
        public Hrefs GetFies()
        {
            Hrefs ens = new Hrefs();
            foreach (Href h in this)
            {
                if (h.Url.Contains(".rar") || h.Url.Contains(".rar"))
                {
                    ens.Add(h);
                    continue;
                }


                if (h.Url.Contains(".iso") || h.Url.Contains(".iso"))
                {
                    ens.Add(h);
                    continue;
                }


                if (h.Url.Contains(".zip") || h.Url.Contains(".zip"))
                {
                    ens.Add(h);
                    continue;
                }


                if (h.Url.Contains(".xls") || h.Url.Contains(".XLS"))
                {
                    ens.Add(h);
                    continue;
                }

                if (h.Url.Contains(".doc") || h.Url.Contains(".DOC"))
                {
                    ens.Add(h);
                    continue;
                }

                if (h.Url.Contains(".txt") || h.Url.Contains(".TXT"))
                {
                    ens.Add(h);
                    continue;
                }

                if (h.Url.Contains(".pdf") || h.Url.Contains(".PDF"))
                {
                    ens.Add(h);
                    continue;
                }

            }


            return ens;
        }
    }
}
