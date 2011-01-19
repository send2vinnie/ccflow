using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Tax666.SystemFramework
{
	/// <summary>
	/// 处理各种跟踪信息并记录日志。
	/// <remarks>
	///  ApplicationLog通过Trace类来使用.NET Framework提供的跟踪机制；
	///  Trace类甚至可以对采用Release生成方式编译出的应用程序进行跟踪调试。
	/// </remarks>
	/// </summary>
	public class ApplicationLog
	{
		// 跟踪开关；
		private static TraceSwitch debugSwitch;
		// 跟踪记录器；
		private static StreamWriter debugWriter;
		// 事件日志的跟踪级别；
		private static TraceLevel eventLogTraceLevel;

		/// <summary>
		/// 将错误级别写入事件日志或跟踪文件中。
		/// </summary>
		/// <param name="message">产生的错误信息</param>
		public static void WriteError(String message)
		{
			WriteLog(TraceLevel.Error, message);
		}

		/// <summary>
		/// 将警告级别写入事件日志或跟踪文件中。
		/// </summary>
		/// <param name="message">产生的警告信息</param>
		public static void WriteWarning(String message)
		{
			WriteLog(TraceLevel.Warning, message);
		}

		/// <summary>
		/// 将信息级别写入事件日志或跟踪文件中。
		/// </summary>
		/// <param name="message">产生的信息</param>
		public static void WriteInfo(String message)
		{
			WriteLog(TraceLevel.Info, message);
		}

		/// <summary>
		/// 将跟踪级别写入事件日志或跟踪文件中。
		/// </summary>
		/// <param name="message"></param>
		public static void WriteTrace(String message)
		{
		WriteLog(TraceLevel.Verbose, message);
		}

		/// <summary>
		///  错误信息表达式的格式定义。
		/// </summary>
		/// <param name="ex"></param>
		/// <param name="catchInfo"></param>
		/// <returns></returns>
		public static String FormatException(Exception ex, String catchInfo)
		{
			StringBuilder strBuilder = new StringBuilder();
			if (catchInfo != String.Empty)
			{
				strBuilder.Append(catchInfo).Append("\r\n");
			}
			strBuilder.Append(ex.Message).Append("\r\n").Append(ex.StackTrace);
			return strBuilder.ToString();
		}

		/// <summary>
		///  向日志中写入错误级别和错误信息。
		/// </summary>
		/// <param name="level"></param>
		/// <param name="messageText"></param>
		private static void WriteLog(TraceLevel level, String messageText)
		{
			try
			{
				if (debugWriter != null)
				{
					if (level <= debugSwitch.Level)
					{
						lock(debugWriter)
						{
							Debug.WriteLine(messageText);
							debugWriter.Flush();
						}
					}
				}

				//将错误信息写入系统事件日志中。
				if (level <= eventLogTraceLevel)
				{
					EventLogEntryType LogEntryType;
					switch (level)
					{
						case TraceLevel.Error:
							LogEntryType = EventLogEntryType.Error;
							break;
						case TraceLevel.Warning:
							LogEntryType = EventLogEntryType.Warning;
							break;
						case TraceLevel.Info:
							LogEntryType = EventLogEntryType.Information;
							break;
						case TraceLevel.Verbose:
							LogEntryType = EventLogEntryType.SuccessAudit;
							break;
						default:
							LogEntryType = EventLogEntryType.SuccessAudit;
							break;
					}

					EventLog eventLog = new EventLog("Application", ApplicationConfiguration.EventLogMachineName, ApplicationConfiguration.EventLogSourceName );
					eventLog.WriteEntry(messageText, LogEntryType);
				}
			}
			catch
			{
				//忽略所有异常；
			}
		}

		/// <summary>
		/// 构造应用程序日志。
		/// </summary>
		static ApplicationLog()
		{
			// 从配置文件中读取当前的设置，确定是文件跟踪还是日志跟踪，或者同时使用两者跟踪；
			Type myType = typeof(ApplicationLog);

			try
			{
				if (!Monitor.TryEnter(myType))
				{
					Monitor.Enter(myType);
					return;
				}

				bool clearSettings = true;
				try
				{
					if (ApplicationConfiguration.TracingEnabled)
					{
						String tracingFile = ApplicationConfiguration.TracingTraceFile;
						if (tracingFile != String.Empty)
						{
							//从跟踪中读取跟踪开关名称，并创建开关；
							String switchName = ApplicationConfiguration.TracingSwitchName;
							if (switchName != String.Empty)
							{
								FileInfo file = new FileInfo(tracingFile);
								debugWriter = new StreamWriter(file.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
								Debug.Listeners.Add(new TextWriterTraceListener(debugWriter));

								debugSwitch = new TraceSwitch(switchName, ApplicationConfiguration.TracingSwitchDescription);
								debugSwitch.Level = ApplicationConfiguration.TracingTraceLevel;
							}
							clearSettings = false;
						}
					}
				}
				catch
				{
				}

				if (clearSettings)
				{
					debugSwitch = null;
					debugWriter = null;
				}

				if(ApplicationConfiguration.EventLogEnabled)
					eventLogTraceLevel = ApplicationConfiguration.EventLogTraceLevel;
				else
					eventLogTraceLevel = TraceLevel.Off;
			}
			finally
			{
				Monitor.Exit(myType);
			}
		}

	}
}
