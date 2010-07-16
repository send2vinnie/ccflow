using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using BP.En;
using BP.DA;
using System.Web;
using System.Text.RegularExpressions;

namespace BP.RB
{
    public class PubClass
    {
        #region sds
        public static string StringCutHtml(string docs)
        {
            docs = docs.Replace("&nbsp;", " ");
            docs = docs.Replace("  ", " ");

            // 替换一些标记。
            docs = docs.Replace("javascipt", "");
            docs = docs.Replace("function", "");

            // 去掉 html 标记。
            char[] chars = docs.ToCharArray();
            bool begainRec = true;
            string str = "";
            foreach (char c in chars)
            {
                if (c == '<')
                {
                    begainRec = false;
                    continue;
                }

                if (c == '>')
                {
                    begainRec = true;
                    continue;
                }

                if (c == '\t')
                    continue;

                if (c == '\n')
                    continue;

                if (c == '\r')
                    continue;

                if (begainRec)
                    str += c.ToString();
            }


            // 去掉javascript 部分。
            chars = str.ToCharArray();
            str = "";
            begainRec = true;

            foreach (char c in chars)
            {
                if (c == '{')
                {
                    begainRec = false;
                    continue;
                }

                if (c == '}')
                {
                    begainRec = true;
                    continue;
                }

                if (begainRec)
                    str += c.ToString();
            }
            return str.Trim();
        }
        #endregion

        #region sss

        public static int NumOfAll=0;
        public static int NumOfThisSite=0;
        public static void DoPage(Href hf)
        {
            if (hf.IsLocalHost == false)
                return;

            if (hf.IsFile)
                return; /* 如果是文件就不考虑了。*/

            Page pg = new Page();
            pg.No = hf.Url;

            if (pg.IsExits)
                return;

            pg.FK_WebSite = hf.HostName;
            if (hf.IsLocalHost == false)
                return;

            string docs = PubClass.ReadContext(hf.Url, hf.HisEncoder);
            if (docs == null)
                return;


            Hrefs hfs = PubClass.GetHrefs(hf.Url, hf.HisEncoder);
            pg.No = hf.Url;
            pg.Name = hf.Lab;
            pg.Url = hf.Url;
            pg.HostName = hf.HostName;
            pg.DocHtml = docs;
            pg.DocText = PubClass.StringCutHtml(docs);
            pg.NumOfHrefs = hfs.Count;
            pg.Insert();

            pg.GenerEmails(); // 生成email.
            pg.GenerFiles(hfs, pg.Url ); //生成文件。

            foreach (Href myhf in hfs)
            {
                PubClass.DoPage(myhf);
            }
        }
        #endregion

        #region 共用方法

        public static int PageTimeout
        {
            get
            {
                return int.Parse(SystemConfig.AppSettings["PageTimeout"]);
            }
        }
        /// <summary>
        /// 产生临时文件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GenerTempFileByUrl(string url, string hostName)
        {

            string dirName = "C:\\StealTempFile\\" + hostName + "\\";

            if (Directory.Exists(dirName) == false)
                Directory.CreateDirectory(dirName);

            string tempfile = url;
            tempfile = tempfile.Replace("http://", "");
            tempfile = tempfile.Replace("HTTP://", "");
            tempfile = tempfile.Replace("/", "@");
            tempfile = tempfile.Replace("?", "_");
            tempfile = tempfile.Replace("&", "_");
            tempfile = tempfile.Replace("=", "_");
            tempfile = tempfile.Replace(":", "_");
            tempfile = dirName + "\\" + tempfile + ".htm";
            return tempfile;
        }
        public static string ReadContextMsg = null;
        public static Encoding ParseFormat(string url)
        {
            return Encoding.Default;
            try
            {

                string html = getHTML_del(url, Encoding.ASCII.EncodingName, PubClass.PageTimeout);
                if (html == null)
                    return Encoding.Default;


                Regex reg_charset = new Regex(@"charset\b\s*=\s*(?<charset>[^""]*)");

                string enconding = null;
                if (reg_charset.IsMatch(html))
                {
                    enconding = reg_charset.Match(html).Groups["charset"].Value;
                }
                else
                {
                    enconding = Encoding.Default.EncodingName;
                }

                return Encoding.GetEncoding(enconding);

            }
            catch (UriFormatException ex)
            {
                return Encoding.Default;
            }
        }
        public static Int64 GetFileSize(string url, int timeOut)
        {
            return 100;

            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Timeout = timeOut;

                WebResponse webResponse = webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                long size = stream.Length;

                Int64 s = Int64.Parse(size.ToString()) / 100000;
                return s;
            }
            catch (UriFormatException ex)
            {
                return 0;
            }
            catch (WebException ex)
            {
                return 0;
            }
        }

        static string getHTML_del(string url, string encodingName, int timeOut)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Timeout = timeOut;

                WebResponse webResponse = webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding(encodingName));
                string html = sr.ReadToEnd();
                return html;
            }
            catch (UriFormatException ex)
            {
                return null;
            }
            catch (WebException ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 读取一个url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ReadContext(string url, Encoding  encodeing )
        {
            try
            {

                HttpWebRequest webRequest = null;
                HttpWebResponse webResponse;
                Stream stream;
                string hostName = "";
                try
                {
                    webRequest = (HttpWebRequest)WebRequest.Create(url);
                    webRequest.Timeout = PubClass.PageTimeout;
                    webResponse = (HttpWebResponse)webRequest.GetResponse();
                    stream = webResponse.GetResponseStream();
                    webRequest.AllowAutoRedirect = true;

                    string str = webRequest.Address.AbsoluteUri;
                    hostName = webRequest.Address.Host;
                }
                catch (Exception ex)
                {
                    PubClass.ReadContextMsg = "@读取URL出现错误:URL=" + url + "@错误信息：" + ex.Message;
                    Log.DefaultLogWriteLineInfo("获取URL" + url + "错误:" + ex.Message);
                    return null;
                }

                string tempfile = GenerTempFileByUrl(url, hostName);

                //Encoding  encoding = PubClass.ParseFormat(url);
                //if (encoding == null)
                //    return null;

                System.IO.StreamReader streamReader = new StreamReader(stream, encodeing);

                // 读取流字符串内容.
                string content = streamReader.ReadToEnd();

                //StreamWriter sw = new StreamWriter(tempfile);
                //sw.Write(content);
                //sw.Close();

                // 关闭相关对象
                streamReader.Close();
                webResponse.Close();
                PubClass.ReadContextMsg = "@读取数据[" + url + "]成功。";
                return content;
            }
            catch (Exception ex)
            {

                string msg = "读取[" + url + "]期间出现意外的错误。" + ex.Message;
                Log.DefaultLogWriteLineError(msg);
                return null;
            }

        }
        public static bool CheckUrl(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Timeout = PubClass.PageTimeout;
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            webRequest.AllowAutoRedirect = true;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="docs"></param>
        /// <param name="hrefFlag"></param>
        /// <returns></returns>
        public static Hrefs GetHrefs(string url, System.Text.Encoding encoder)
        {
            string docs = PubClass.ReadContext(url, encoder);
            Uri uri = new Uri(url);
            string path = uri.AbsolutePath;
            path = path.Substring(0, path.LastIndexOf('/') + 1);
            path = path.ToLower();

            string hostName = uri.Authority.ToLower();

            Hrefs hfs = new Hrefs();
            if (docs == null)
                return hfs;


            //docs = "中国人员 <a href='as.aspx' > 连接标题 </a>";

            char[] chars = docs.ToCharArray();

            bool isStartRecordMarket = false; // 开始记录 < 
            bool isStartRecordLab = false; // 开始记录 lab 

            string str1 = "";
            string str2 = "";
            string str3 = "";
            string recordering = "";
            string doc = "";

            bool isChecUrl = false;
            foreach (char c in chars)
            {
                #region 开始处理str1
                if (c == '<')
                {
                    /*开始记录标记*/
                    isStartRecordMarket = true;
                    recordering = "";
                    isStartRecordLab = false; // 停止记录lab.
                }


                if (c == '>')
                {
                    isStartRecordMarket = false; // 停止记录标记
                    //判断记录东西的内容。
                    doc = recordering.ToLower();
                    if (doc.IndexOf("href") == -1 || doc.Contains("window"))
                    {
                        /* 说明不是 href 部分 */
                        if (doc.IndexOf("/a") == -1)
                        {
                            /* 说明也不是 str3 部分 */
                            recordering = "";
                            continue; //记录是的无效的标记。
                        }
                    }

                    if (doc.ToLower().IndexOf("href") != -1)
                    {
                        /* 它是 str1 的内容。*/
                        str1 = recordering;
                        isStartRecordLab = true; // 开始记录 标签。
                        //recordering = "";
                        str2 = "";
                        str3 = "";
                    }

                    if (doc.IndexOf("/a") != -1)
                    {
                        /* 它是 str3 的内容。*/
                        str3 = doc;
                        isChecUrl = true; //执行检查url ，完成这个url 的记录。
                        isStartRecordLab = false; // 开始记录 标签。
                    }
                }

                recordering += c.ToString();
                if (isStartRecordLab)
                    str2 += c.ToString();
                #endregion


                if (isChecUrl == false)
                    continue;

                // 如果三个都没有记录到，就把他们返回掉。
                if (str1 == "" || str2 == "" || str3 == "")
                    continue;

                // 处理 str1 . href='ab.htm   ' ;
                str1 = str1.Replace(" ",""); //替换掉空格。

                try
                {
                    if (str1.IndexOf("HREF") != -1)
                        str1 = str1.Substring(str1.IndexOf("HREF=") + 5);
                    else
                        str1 = str1.Substring(str1.IndexOf("href=") + 5);
                }
                catch (Exception ex)
                {
                    throw new Exception("错误：" + str1 + ex.Message);
                }

                int pos = str1.IndexOf("target");
                if (pos != -1)
                {
                    str1 = str1.Substring(0, pos); //去掉target 后面的部分。
                    // 说明 有 target . 
                }

                pos = str1.IndexOf("class");
                if (pos != -1)
                {
                    str1 = str1.Substring(0, pos); //去掉 class 后面的部分。
                    // 说明 有 class . 
                }


                pos = str1.IndexOf("style=");
                if (pos != -1)
                {
                    str1 = str1.Substring(0, pos); // 去掉 class 后面的部分。
                    // 说明 有 class . 
                }

                pos = str1.IndexOf("title");
                if (pos != -1)
                {
                    str1 = str1.Substring(0, pos); // 去掉 class 后面的部分。
                    // 说明 有 class . 
                }

                str1 = str1.Replace("<", "");
                str1 = str1.Replace(">", "");
                str1 = str1.Replace(" ", "");
                str1 = str1.Replace("'", "");
                str1 = str1.Replace(".html/", "html");
                str1 = str1.Replace(".HTML/", "HTML");

                str1 = str1.Replace(".html\\", "html");
                str1 = str1.Replace(".HTML\\", "HTML");
                str1 = str1.Replace("\"", "");

                str2 = str2.Replace(">", "");
                str2 = str2.Replace("<", "");

                if (str1.ToLower().Contains("to:"))
                    continue;

                if (str1.Contains("/#"))
                    continue;

                if (str1.ToLower().Contains(".com") || str1.ToLower().Contains(".net") || str1.ToLower().Contains(".cn"))
                    continue;
                

                string temUrl = str1;

                Href href = new Href();
                if (temUrl.Substring(0, 1) == "/")
                {
                    /*
                      /zone/abc.aspx 的情况。
                     */
                    //  temUrl = path + "/" + temUrl;
                }
                else
                {
                    /*
                        abc.aspx 的情况。
                    */
                    if (temUrl.ToLower().Contains("http://") == false)
                        temUrl = path + "/" + temUrl;
                }

                if (str1.Contains(":"))
                {
                    href.Url = temUrl;
                }
                else
                {
                    href.Url = "http://" + hostName + temUrl;
                }

                //try
                //{
                //    PubClass.CheckUrl(href.Url);
                //}
                //catch(Exception ex)
                //{
                //    int i = 100;
                //}

                href.Lab = str2.Trim();
                href.PageUrl = str1; //原始的url.

                

                href.HostName = hostName;
                href.HisEncoder = encoder;
                href.Path = path.ToLower();

                string[] strs = href.Url.Split('?');

                if (strs.Length > 2)
                {
                    Log.DefaultLogWriteLineInfo("放弃了:"+href.Url );
                    continue;
                }

                if (hfs.Contains(href) == false)
                    hfs.Add(href);


                //恢复设置变量
                isChecUrl = false;
                str1 = "";
                str2 = "";
                str3 = "";
                recordering = "";
                doc = "";
                isStartRecordLab = false;
                isStartRecordMarket = false;
            }
            return hfs;
        }
        
        #endregion

    }
}
