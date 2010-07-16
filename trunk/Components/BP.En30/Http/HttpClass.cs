using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Net;
using System.IO;

namespace BP.HTTP
{
    public class HTTPClass
    {
        public static long DownLoadFile(string url, string savePath, int timeOut)
        {
            CookieContainer cookie  =new CookieContainer();;
            long Filelength = 0;
            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.CookieContainer = cookie;
            req.AllowAutoRedirect = true;
            req.Timeout = timeOut;

            HttpWebResponse res = req.GetResponse() as HttpWebResponse;
            System.IO.Stream stream = res.GetResponseStream();
            try
            {
                Filelength = res.ContentLength;

                byte[] b = new byte[512];
                int nReadSize = 0;
                nReadSize = stream.Read(b, 0, 512);

                System.IO.FileStream fs = System.IO.File.Create(savePath);
                try
                {
                    while (nReadSize > 0)
                    {
                        fs.Write(b, 0, nReadSize);
                        nReadSize = stream.Read(b, 0, 512);
                    }
                }
                finally
                {
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("下载文件错误：" + ex.Message);
            }
            finally
            {
                res.Close();
                stream.Close();
            }
            return Filelength;
        }

        public static string GenerTempFileByUrl(string url)
        {
            string tempfile = url;
            tempfile = tempfile.Replace("http://", "");
            tempfile = tempfile.Replace("HTTP://", "");
            tempfile = tempfile.Replace("/", "");
            tempfile = tempfile.Replace("?", "_");
            tempfile = tempfile.Replace("&", "_");
            tempfile = tempfile.Replace("=", "_");
            tempfile = tempfile.Replace(":", "_");
            tempfile = "C:\\StealTempFile\\" + tempfile + ".htm";
            return tempfile;
        }
        public static string ReadContext(string url, int timeOut, Encoding encode )
        {
            // string tempfile = GenerTempFileByUrl(url);
            HttpWebRequest webRequest = null;
            try
            {
                // Uri uri = new Uri(this.Url);
                webRequest = (HttpWebRequest)WebRequest.Create(url);
                // webRequest.Timeout = Program.PageTimeout;
                webRequest.Timeout = timeOut;

                // webRequest.KeepAlive = false;
                string str = webRequest.Address.AbsoluteUri;
                // this.HostName = webRequest.Address.Host;
                str = str.Substring(0, str.LastIndexOf("/"));
                //this.Host = str;
            }
            catch (Exception ex)
            {
                try
                {
                    BP.DA.Log.DefaultLogWriteLineWarning("@读取URL出现错误:URL=" + url + "@错误信息：" + ex.Message);
                    return null;
                }
                catch
                {
                    return ex.Message;
                }
            }

            //	因为它返回的实例类型是WebRequest而不是HttpWebRequest,因此记得要进行强制类型转换
            //  接下来建立一个HttpWebResponse以便接收服务器发送的信息，它是调用HttpWebRequest.GetResponse来获取的：

            HttpWebResponse webResponse;
            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                //webResponse
            }
            catch (Exception ex)
            {
                try
                {
                    // 如果出现死连接。
                    BP.DA.Log.DefaultLogWriteLineWarning("@获取url=" + url + "失败。异常信息:" + ex.Message, true);
                    return null;
                }
                catch
                {
                    return ex.Message;
                }
            }

            //如果webResponse.StatusCode的值为HttpStatusCode.OK，表示成功，那你就可以接着读取接收到的内容了：
            // 获取接收到的流

            Stream stream = webResponse.GetResponseStream();
            System.IO.StreamReader streamReader = new StreamReader(stream, encode);
            string content = streamReader.ReadToEnd();
            webResponse.Close();
            return content;
        }
        public static string StrReplaceChar(string str)
        {
            str = str.Replace(";", "；");
            str = str.Replace(":", "：");
            str = str.Replace("\r\n", "");
            str = str.Replace("?", "？");
            str = str.Replace("!", "！");
            str = str.Replace(",", "，");
            str = str.Replace("\"", "“");
            str = str.Replace("\n", "");
            str = str.Replace("\t", "");
            str = str.Replace("\t\n", "");
            str = str.Replace("\r", "");
            str = str.Replace(" ", "");

            return str;
        }
        #region 对字符串处理
        public static string ParseHtmlToText(string val)
        {
            if (val == null)
                return val;

            val = val.Replace("欢迎光临", "");
            val = val.Replace("字体：大中小", "");
            val = val.Replace("尾页", "");
            val = val.Replace("上一页", "");
            val = val.Replace("下一页", "");
            val = val.Replace("尾页", "");
            val = val.Replace("页次", "");

            val = val.Replace("网站介绍", "");
            val = val.Replace("联系我们", "");
            val = val.Replace("友情链接", "");
            val = val.Replace("网站地图", "");
            val = val.Replace("广告报价", "");

            val = val.Replace("&nbsp;", " ");
            val = val.Replace("&raquo;", " ");

             
            val = val.Replace("  ", " ");

            val = val.Replace("</td>", "");
            val = val.Replace("</TD>", "");

            val = val.Replace("</tr>", "");
            val = val.Replace("</TR>", "");

            val = val.Replace("<tr>", "");
            val = val.Replace("<TR>", "");

            val = val.Replace("</font>", "");
            val = val.Replace("</FONT>", "");

            val = val.Replace("</table>", "");
            val = val.Replace("</TABLE>", "");


            val = val.Replace("<BR>", "\n\t");
            val = val.Replace("<BR>", "\n\t");
            val = val.Replace("&nbsp;", " ");

            val = val.Replace("<BR><BR><BR><BR>", "<BR><BR>");
            val = val.Replace("<BR><BR><BR><BR>", "<BR><BR>");
            val = val.Replace("<BR><BR>", "<BR>");

            val = StrDelFunc(val);

            val = val.Replace("<script", "{");
            val = val.Replace("</script>", "}");

            val = val.Replace("<!--", "{");
            val = val.Replace("-->", "}");

            val = StrDelBigBracket(val);
            val = StrDelBigBracket(val);
            val = StrDelBigBracket(val);
            val = StrDelBigBracket(val);


            char[] chs = val.ToCharArray();
            bool isStartRec = false;
            string recStr = "";
            foreach (char c in chs)
            {
                if (c == '<')
                {
                    recStr = "";
                    isStartRec = true; /* 开始记录 */
                }

                if (isStartRec)
                {
                    recStr += c.ToString();
                }

                if (c == '>')
                {

                    isStartRec = false;
                    if (recStr == "")
                    {
                        isStartRec = false;
                        continue;
                    }

                    /* 开始分析这个标记内的东西。*/
                    string market = recStr.ToLower();
                    val = val.Replace(recStr, "");
                    isStartRec = false;
                    recStr = "";

                }
            }




            val = val.Replace("&gt;", "");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");

            val = val.Replace("\t", "");
            val = val.Replace("\n", "");
            val = val.Replace("\r", "");
            val = val.Replace("|", " ");

          

          

           
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("  ", " ");
            val = val.Replace("|", " ");
            return val;
        }
        public static string StrDelFunc(string val)
        {
            int idx = val.IndexOf("function");
            if (idx == -1)
                return val;

            val = val.Replace("function", "^function");
            char[]  chs = val.ToCharArray();
            bool startRec = false;
            string funcStr = "";
            int timesL = 0;
            int timesR = 0;
            foreach (char c in chs)
            {
                if (c == '^')
                {
                    startRec = true;
                    funcStr = "";
                    timesL = 0;
                    timesR = 0;
                }

                if (startRec)
                    funcStr += c;


                if (startRec)
                {
                    if (c == '}')
                    {
                        timesR++;
                    }

                    if (c == '{')
                    {
                        timesL++;
                    }
                }

                if (timesL == timesR)
                {
                    if (timesL >= 1)
                    {
                        val = val.Replace(funcStr, "");
                        startRec = false;
                        funcStr = "";
                        timesL = 0;
                        timesR = 0;
                    }
                }
            }

            return val;
        }
        /// <summary>
        /// 删除大括号中间的部分
        /// </summary>
        /// <param name="docs"></param>
        /// <returns></returns>
        public static string StrDelBigBracket(string val)
        {
            if (val.IndexOf("{") == -1)
                return val;


            char[] chs = val.ToCharArray();
            bool startRec = false;
            string funcStr = "";
            int timesL = 0;
            int timesR = 0;
            foreach (char c in chs)
            {
                if (c == '{')
                {
                    startRec = true;
                    funcStr = "";
                    timesL = 0;
                    timesR = 0;
                }

                if (startRec)
                    funcStr += c;

                if (startRec)
                {
                    if (c == '}')
                    {
                        val = val.Replace(funcStr, "");
                        startRec = false;
                        funcStr = "";
                    }
                }
            }
            return val;
        }
        #endregion

        public static bool SaveAsFile(string url, string fileName)
        {
            string doc = BP.HTTP.HTTPClass.ReadContext(url, 5000, System.Text.Encoding.GetEncoding("gb2312"));
            StreamWriter sw = new StreamWriter(fileName, true);
            sw.Write(doc );
            //sw.Write(doc.Replace(" ", ""));
            sw.Close();
            return true;
        }

        public static string StrDelHeadBody(string htmldoc)
        {
            if (htmldoc == null)
                return "";

            string s= HTTPClass.StrGetContext("<body", "</body>", htmldoc);
            return s.Substring(1);
        }

        public static string StrGetContext(string from, string to, string myDocs)
        {
            // return myDocs;
            //from = "12";
            //to = "78";
            //myDocs = "0123456789";

            if (from == "" && to == "")
                return myDocs;

            string msg = "";
            string dealDocs = "";

            try
            {

                int fromPos = myDocs.IndexOf(from);
                if (fromPos == -1)
                {
                    msg += "@没找到[" + from + "]，请确定 from 标记是否正确。";
                    dealDocs = myDocs;
                }
                else
                    dealDocs = myDocs.Substring(fromPos + from.Length);

                int toPos = dealDocs.LastIndexOf(to);
                if (toPos == -1)
                    msg += "@没找到[" + to + "]，请确定 to 标记是否正确。";
                else
                    dealDocs = dealDocs.Substring(0, toPos);

                if (msg != "")
                    BP.DA.Log.DefaultLogWriteLineWarning("GetContext Error:" + msg);
            }
            catch (Exception ex)
            {
                throw new Exception(" StrGetContext 错误:" + ex.Message + "@位置:" + msg);
            }

            dealDocs = dealDocs.Replace("    ", " ");
            dealDocs = dealDocs.Replace("    ", " ");
            dealDocs = dealDocs.Replace("    ", " ");
            return dealDocs.Trim();
        }
        /// <summary>
        /// 根据一块文本获取hrefs,
        /// </summary>
        /// <param name="from">从</param>
        /// <param name="to">到</param>
        /// <param name="myDocs">文本</param>
        /// <param name="ysURL">原始的URL，用来判断基路径</param>
        /// <param name="hrefFlag"></param>
        /// <returns></returns>
        public static Hrefs GenerHrefs(string from, string to, string myDocs, string ysURL, string hrefFlag)
        {
            if (myDocs == "EXIT")
                return new Hrefs(); /* 如果存在这个文件，就不要读取了。*/

            string docs = StrGetContext(from, to, myDocs);
            if (docs == null)
            {
                BP.DA.Log.DefaultLogWriteLineInfo("获取 Hrefs 期间出错，没有在指定的文本中找到from or to。请检查url=" + ysURL);
            }

            Hrefs hrefs = new Hrefs();
            if (docs == null || docs == "")
            {
                string msg = " 获取 url 列表失败，请确定标记是否正确 @From=" + from + "@To=" + to + "@更多的信息参考日志。";
                throw new Exception(msg);
            }
            return GenerHrefs(docs, hrefFlag, ysURL);
        }
        /// <summary>
        /// 根据一个url 获取它的基本路径。
        /// http://www.chichengsoft.com/update/demo.htm
        /// return 
        /// http://www.chichengsoft.com/update/
        /// </summary>
        /// <param name="url">原始的URL</param>
        /// <returns>返回基本信息</returns>
        public static string GetBasePath(string url)
        {
            int i = url.LastIndexOf('/');
            string str =  url.Substring(0, i+1);
            if (str.Length == 7 && str.Contains("://"))
                return url;
            return str;
        }
        /// <summary>
        /// 进入http://www.xyx.com/sww/sh.htm 返回 http://www.xyx.com/
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHost(string url)
        {
            Uri uri = new Uri(url);
            return uri.Host;
        }
        /// <summary>
        /// 找到周围的数据
        /// </summary>
        /// <param name="docs">从文本块中</param>
        /// <param name="lab">要找的目标</param>
        /// <param name="span">目标左右范围</param>
        /// <returns>执行的内容，如果不包含就返回lab.</returns>
        public static string StrGetSpan(string docs, string lab, int span)
        {
            int from = docs.IndexOf(lab);
            if (from == -1)
                return lab;
            from = from - span;
            if (from < 0)
                from = 0;

            try
            {
                return docs.Substring(from, span * 2 + lab.Length);
            }
            catch
            {
                return docs.Substring(from);
            }
        }
        /// <summary>
        /// 根据文本获取url.
        /// </summary>
        /// <param name="docs">文本</param>
        /// <param name="hrefFlag">url中的标记，表明特定的标记才给被收录，否则放弃。如果为空表示全部录取</param>
        /// <param name="ysURL">原始的URL，用于表示基本路径</param>
        /// <returns>结果集合</returns>
        public static Hrefs GenerHrefs(string docs, string hrefFlag, string ysURL)
        {
            //  docs = "template <a href=' asb.htm ' target=_blank >mylab</a> template <a href=' asb1.htm ' target=_blank >mylab2</a> wewww";

            Uri ysUri = new Uri(ysURL);
            string basePath = HTTPClass.GetBasePath(ysURL);
            Hrefs hfs = new Hrefs();
            char[] chars = docs.ToCharArray();
            bool isStartRecordMarket = false; // 开始记录 < 
            bool isStartRecordLab = false; // 开始记录 lab 

            string url = "";
            string str2 = "";
            string str3 = "";
            string Recing = "";
            string doc = "";

            bool isChecUrl = false;
            foreach (char c in chars)
            {
                try
                {
                    #region 开始处理str1
                    if (c == '<')
                    {
                        /*开始记录标记*/
                        isStartRecordMarket = true;
                        Recing = "";
                        isStartRecordLab = false; // 停止记录lab.
                    }

                    if (c == '>')
                    {
                        isStartRecordMarket = false; // 停止记录标记
                        //判断记录东西的内容。
                        doc = Recing.ToLower();
                        if (doc.IndexOf("href") == -1)
                        {
                            /* 说明不是 href 部分 */
                            if (doc.IndexOf("/a") == -1)
                            {
                                /* 说明也不是 str3 部分 */
                                Recing = "";
                                continue; //记录是的无效的标记。
                            }
                        }

                        if (doc.ToLower().IndexOf("href") != -1)
                        {
                            /* 它是 str1 的内容。*/
                            url = Recing;
                            isStartRecordLab = true; // 开始记录 标签。
                            //Recing = "";
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

                    Recing += c.ToString();

                    if (isStartRecordLab)
                        str2 += c.ToString();

                    #endregion

                }
                catch (Exception ex)
                {
                    throw new Exception("error0000" + ex.Message);
                }


                if (isChecUrl == false)
                    continue;

                // 如果三个都没有记录到，就把他们返回掉。
                if (url == "" || str2 == "" || str3 == "")
                    continue;

                try
                {

                    #region 清理特殊标记
                    // 处理 str1 . href='ab.htm   ' ;
                    if (url.IndexOf("HREF") != -1)
                        url = url.Substring(url.IndexOf("HREF=") + 5);
                    else
                        url = url.Substring(url.IndexOf("href=") + 5);


                    int pos = url.IndexOf("target");
                    if (pos != -1)
                    {
                        url = url.Substring(0, pos); //去掉target 后面的部分。
                        // 说明 有 target . 
                    }

                    pos = url.IndexOf("class");
                    if (pos != -1)
                    {
                        url = url.Substring(0, pos); //去掉class 后面的部分。
                        // 说明 有 class . 
                    }

                    pos = url.IndexOf("title");
                    if (pos != -1)
                    {
                        url = url.Substring(0, pos); //去掉title 后面的部分。
                        // 说明 有 class . 
                    }

                    pos = url.IndexOf("style");
                    if (pos != -1)
                    {
                        url = url.Substring(0, pos); //去掉style 后面的部分。
                        // 说明 有 class . 
                    }

                    #endregion 清理特殊标记

                }
                catch (Exception ex)
                {
                    throw new Exception("error1111" + ex.Message);
                }

                url = url.Replace("<", "");
                url = url.Replace(">", "");
                url = url.Replace(" ", "");
                url = url.Replace("'", "");
                url = url.Replace(".html/", "html");
                url = url.Replace(".HTML/", "HTML");

                url = url.Replace(".html\\", "html");
                url = url.Replace(".HTML\\", "HTML");
                url = url.Replace("\"", "");

                str2 = str2.Replace(">", "");
                str2 = str2.Replace("<", "");

                if (url.ToLower().Contains(":") == false)
                {

                    /* 如果不包含基础路径，就要考虑基础路径加 当前的连接。
                     * 比如： str1= '/N1232/N2323/xya.htm'  
                     */

                    if (url.Substring(0, 1) == "/")
                    {
                        /*如果是从基础路径开始的。*/
                        url = "http://" + ysUri.Authority + "" + url;
                    }
                    else
                    {

                        string[] strs = url.Split('/');
                        string myurl = basePath + "/";
                        foreach (string s in strs)
                        {
                            if (s == null)
                                continue;

                            if (myurl.Contains("/" + s) == false)
                                myurl += "/" + s;
                        }
                        url = myurl.Replace("//", "/").Replace("//", "/"); // = basePath + str1;
                        url = url.Replace(":/", "://");
                    }
                }

                #region 把执行的结果放进去。
                Href href = new Href();
                try
                {
                    href.Url = url.Trim();
                }
                catch
                {
                    //恢复设置变量
                    isChecUrl = false;
                    url = "";
                    str2 = "";
                    str3 = "";
                    Recing = "";
                    doc = "";
                    isStartRecordLab = false;
                    isStartRecordMarket = false;
                    continue;
                }

                href.Lab = str2.Trim();
                if (hrefFlag != null)
                {
                    if (href.Url.Contains(hrefFlag) == false)
                        continue;
                }

                if (hfs.Contains(href) == false)
                {
                    hfs.Add(href);
                }

                //恢复设置变量
                isChecUrl = false;
                url = "";
                str2 = "";
                str3 = "";
                Recing = "";
                doc = "";
                isStartRecordLab = false;
                isStartRecordMarket = false;
                #endregion

            }
            return hfs;
        }
        public static void MakeDir(string dirFullName)
        {
            try
            {
                System.IO.Directory.CreateDirectory(dirFullName);
            }
            catch
            {
            }
        }
    }
}
