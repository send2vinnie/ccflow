using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.EIP
{
    public static class Tool
    {
        public static string ArrayToString(string[] arrStr)
        {
            string str = "";
            if (arrStr.Length > 0)
            {
                for (int i = 0; i < arrStr.Length; i++)
                {
                    str = str + "'" + arrStr[i] + "',";
                }
                str = str.Substring(0, str.Length - 1);
            }

            return str;
        }
    }
}
