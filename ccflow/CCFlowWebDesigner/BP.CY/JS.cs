using System;
using Microsoft.JScript;

namespace BP.CY
{
    public class JS
    {
        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public static string Escape(string info)
        {
            return GlobalObject.escape(info);
        }

        /// <summary>
        /// 反编码
        /// </summary>
        /// <returns></returns>
        public static string Unescape(string info)
        {
            return GlobalObject.unescape(info);
        }
    }
}
