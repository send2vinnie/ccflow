using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;

namespace BP.GE
{
    /// <summary>
    ///WuXiaoyun 的摘要说明
    /// </summary>
    public static class WuXiaoyun
    {
        /// <summary>
        /// 文件大小转换(最小单位：KB)
        /// </summary>
        /// <param name="value">文件的大小：字节</param>
        /// <returns>转换成0.0格式</returns>
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

        /// <summary>
        /// 根据文件扩展名判断是否是视频文件
        /// </summary>
        /// <param name="ext">文件的扩展名</param>
        /// <returns></returns>
        public static bool IsVideoExt(string ext)
        {
            string videoExt = @"(avi|wmv|asf|mov|rm|ra|ram|flv|rmvb|swf|wav|wma)";
            return (Regex.IsMatch(ext.ToLower(), videoExt));
        }

        //设置Table超过3列时TD的交替背景颜色
        public static void AddTDBg(ref bool isTDbg, ref string tdStyle, int colNum)
        {
            if (colNum < 3)
            {
                return;
            }
            if (isTDbg)
            {
                tdStyle = "background-color:#f4fafe;";
                isTDbg = false;
            }
            else
            {
                tdStyle = "";
                isTDbg = true;
            }
        }

        /// <summary>
        /// 自动识别超链接
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string UrlSearch(string doc)
        {
            //用正则表达式识别URL超链接http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?
            Regex UrlRegex = new Regex(@"((http|ftp):\/\/([\w.]+\/?)\S*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            //用正则来查询
            MatchCollection matches = UrlRegex.Matches(doc);
            foreach (Match match in matches)
            {
                doc = doc.Replace(match.Value, string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", match.Value, match.Value));
            }
            return doc;
        }
    }
}
