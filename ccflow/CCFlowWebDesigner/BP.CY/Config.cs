using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BP.CY
{
    public class Config
    {
        /// <summary>
        /// 解析config配置
        /// </summary>
        /// <param name="strConfig">server=127.0.0.1;username=awg;pwd=123456;</param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseDic(string strConfig)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(strConfig))
            {
                string[] arr = strConfig.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string[] item;

                if (arr.Length > 0)
                {
                    foreach (string val in arr)
                    {
                        item = val.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (item.Length == 2 && !string.IsNullOrEmpty(item[0].Trim()) && !dic.ContainsKey(item[0].Trim()))
                        {
                            dic.Add(item[0].ToLower().Trim(), item[1].Trim());
                        }
                        else
                        {
                            throw new Exception("配置格式不正确");
                        }
                    }
                }
                else
                {
                    throw new Exception("配置不正确");
                }
            }

            return dic;
        }
    }
}
