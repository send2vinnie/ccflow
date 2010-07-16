using System;
using System.Collections.Generic;
using System.Text;

namespace BP.En20
{
    public class Function
    {
public static string getUserIP() 
{ 
return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); 
} 
/// <summary> 
/// 去除字符串最后一个','号 
/// </summary> 
/// <param name="chr">:要做处理的字符串</param> 
/// <returns>返回已处理的字符串</returns> 
public static string Lost(string chr) 
{ 
if (chr == null || chr == string.Empty) 
{ 
return ""; 
} 
else 
{ 
chr = chr.Remove(chr.LastIndexOf(",")); 
return chr; 
} 
} 
/// <summary> 
/// 去除字符串第一个'/'号 
/// </summary> 
/// <param name="chr">要做处理的字符串</param> 
/// <returns>返回已处理的字符串</returns> 
public static string lostfirst(string chr) 
{ 
string flg = ""; 
if (chr != string.Empty || chr != null) 
{ 
if (chr.Substring(0, 1) == "/") 
flg = chr.Replace(chr.Substring(0, 1), ""); 
else 
flg = chr; 
} 
return flg; 
} 
/// <summary> 
/// 替换html中的特殊字符 
/// </summary> 
/// <param name="theString">需要进行替换的文本。</param> 
/// <returns>替换完的文本。</returns> 
public static string HtmlEncode(string theString) 
{ 
theString = theString.Replace(">", ">"); 
theString = theString.Replace("<", "<"); 
theString = theString.Replace(" ", " "); 
theString = theString.Replace(" ", " "); 
theString = theString.Replace("\"", """) ;
theString = theString.Replace("\'", "'"); 
theString = theString.Replace("\n", "<br/> "); 
return theString; 
} 
/// <summary> 
/// 恢复html中的特殊字符 
/// </summary> 
/// <param name="theString">需要恢复的文本。</param> 
/// <returns>恢复好的文本。</returns> 
public static string HtmlDiscode(string theString) 
{ 
theString = theString.Replace(">", ">"); 
theString = theString.Replace("<", "<"); 
theString = theString.Replace(" ", " "); 
theString = theString.Replace(" ", " "); 
theString = theString.Replace(""", "\""); 
theString = theString.Replace("'", "\'"); 
theString = theString.Replace("<br/> ", "\n"); 
return theString; 
} 

/// <summary> 
/// 生成随机数字 
/// </summary> 
/// <param name="length">生成长度</param> 
/// <returns></returns> 
public static string Number(int Length) 
{ 
return Number(Length, false); 
} 
/// <summary> 
/// 生成随机数字 
/// </summary> 
/// <param name="Length">生成长度</param> 
/// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param> 
/// <returns></returns> 
public static string Number(int Length, bool Sleep) 
{ 
if (Sleep) 
System.Threading.Thread.Sleep(3); 
string result = ""; 
System.Random random = new Random(); 
for (int i = 0; i < Length; i++) 
{ 
result += random.Next(10).ToString(); 
} 
return result; 
}

    }
    
}
