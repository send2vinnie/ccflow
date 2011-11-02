using System;
using log4net;

/// <summary>
/// 日志处理
/// </summary>
public class AppLog
{
    /// <summary>
    /// 静态类
    /// </summary>
    private AppLog() { }

    /// <summary>
    /// 记录错误日志 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public  static  void LogError(string message, Exception ex)
    {
        Write(message + @"\n" +  ex);
    }

    
    /// <summary>
    /// 写入日志
    /// </summary>
    /// <param name="message">日志信息</param>
    /// <param name="messageType">日志类型</param>
    /// <param name="type"></param>
    public static void Write(string message)
    {
       BP.DA.Log.DebugWriteError(message);
    }
    
   
}