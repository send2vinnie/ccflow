using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

namespace BP.CY
{
    public class StringHandle
    {
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStr(string text, int length)
        {
            if (text.Length > length)
            {
                return text.Substring(0, length);
            }

            return text;
        }

        /// <summary>
        /// 移除html标签元素
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveHTML(string text)
        {
            string Htmlstring = Regex.Replace(text, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

            //删除HTML  
            //Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            //Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            //Htmlstring.Replace("<", "");
            //Htmlstring.Replace(">", "");
            //Htmlstring.Replace("\r\n", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            Htmlstring = Regex.Replace(Htmlstring, @"</?[^>]+>", "", RegexOptions.IgnoreCase);

            return Htmlstring;
        }

        /// <summary>
        /// 将流转换字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ToString(Stream stream)
        {
            byte[] b = (stream as MemoryStream).GetBuffer();
            return Convert.ToBase64String(b);
        }

        /// <summary>
        /// 将字符串转换成流
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Stream ToStream(string str)
        {
            byte[] b = Convert.FromBase64String(str);

            return (new MemoryStream(b));
        }
    }
}
