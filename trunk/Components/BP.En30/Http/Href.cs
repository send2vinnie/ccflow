using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.IO;
using BP.En;

namespace BP.HTTP
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
    }
    /// <summary>
    /// Href
    /// </summary>
    public class Href
    {
        #region 属性
        /// <summary>
        /// Tag
        /// </summary>
        private string _Tag = "";
        /// <summary>
        /// Tag
        /// </summary>
        public string Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                _Tag = HTTPClass.ParseHtmlToText(value);
            }
        }
        #endregion


        public string _Url = "";
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
        public bool IsLocalHost(string hostName)
        {
            if (this.Url.Contains(hostName))
                return true;
            return false;
        }
        public string Url
        {
            get
            {
                if (_Url.IndexOf(":") == -1)
                {
                    if (this._Url == null)
                        return null;

                    string str = "http://" + this.HisUri.Host + "/"+this._Url;

                    str = str.Replace("\"", "");
                    return str;
                }
                return _Url;
            }
            set
            {
                _Url = value;
                try
                {
                    this.HisUri = new Uri(_Url);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "原始url=" + value);
                }
            }
        }
        public Uri HisUri=null;

        public string Lab = "";
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
                if (h.Url == href.Url)
                    return true;

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

            if (h.Url.Contains(".css") || h.Url.Contains(".CSS"))
                return;

            if (h.Url.Contains(".jsp") || h.Url.Contains(".JSP"))
            {

            }
            else
            {
                if (h.Url.Contains(".js") || h.Url.Contains(".JS"))
                    return;
            }

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
                if (h.Url.Contains(".rar") || h.Url.Contains(".RAR"))
                {
                    ens.Add(h);
                    continue;
                }


                if (h.Url.Contains(".iso") || h.Url.Contains(".ISO"))
                {
                    ens.Add(h);
                    continue;
                }


                if (h.Url.Contains(".zip") || h.Url.Contains(".ZIP"))
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
