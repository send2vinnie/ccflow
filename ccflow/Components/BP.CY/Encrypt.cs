using System;
using System.Text;
using System.Security.Cryptography;

namespace BP.CY
{
    public class Encrypt
    {
        /// <summary>
        /// 返回加密后的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            
            return BitConverter.ToString(md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))).Replace("-", "").ToLower();
        }
    }
}
