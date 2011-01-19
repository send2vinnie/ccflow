using System;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using System.Xml;
using System.Collections.Specialized;

namespace Tax666.SystemFramework
{
	/// <summary>
	/// 从config文件中读取配置信息并完成相应的初始化工作。
	/// <remarks>
	///  解析Web项目中的web.config配置文件中的“ApplicationConfiguration”节下的配置设置。
	///  作为配置节处理程序，必须实现IConfigurationSectionHandler接口，才能参与配置设置的解析。
	/// </remarks>
	/// <example>
	///  在global.asax文件中的Application_OnStart()事件中应包含如下的代码:
	///    ApplicationConfiguration.OnApplicationStart(Context);
	/// </example>
	/// </summary>
	public class ApplicationConfiguration : IConfigurationSectionHandler
	{
		// SystemFramework标准常量定义;
		private const String TRACING_ENABLED			= "SystemFramework.Tracing.Enabled";
		private const String TRACING_TRACEFILE			= "SystemFramework.Tracing.TraceFile";
		private const String TRACING_TRACELEVEL			= "SystemFramework.Tracing.TraceLevel";
		private const String TRACING_SWITCHNAME			= "SystemFramework.Tracing.SwitchName";
		private const String TRACING_SWITCHDESCRIPTION	= "SystemFramework.Tracing.SwitchDescription";
		private const String EVENTLOG_ENABLED			= "SystemFramework.EventLog.Enabled";
		private const String EVENTLOG_MACHINENAME		= "SystemFramework.EventLog.Machine";
		private const String EVENTLOG_SOURCENAME		= "SystemFramework.EventLog.SourceName";
		private const String EVENTLOG_TRACELEVEL		= "SystemFramework.EventLog.LogLevel";

		//静态成员变量.其值在Config.Web中定义,或采用默认值;
		private static bool tracingEnabled;
		private static String tracingTraceFile;
		private static TraceLevel tracingTraceLevel;
		private static String tracingSwitchName;
		private static String tracingSwitchDescription;
		private static bool eventLogEnabled;
		private static String eventLogMachineName;
		private static String eventLogSourceName;
		private static TraceLevel eventLogTraceLevel;

		//默认配置的常量值;
		private const bool TRACING_ENABLED_DEFAULT				= true;
		private const String TRACING_TRACEFILE_DEFAULT			= "ApplicationTrace.txt";
		private const TraceLevel TRACING_TRACELEVEL_DEFAULT		= TraceLevel.Verbose;
		private const String TRACING_SWITCHNAME_DEFAULT			= "ApplicationTraceSwitch";
		private const String TRACING_SWITCHDESCRIPTION_DEFAULT	= "Application error and tracing information";
		private const bool EVENTLOG_ENABLED_DEFAULT				= true;
		private const String EVENTLOG_MACHINENAME_DEFAULT		= ".";
		private const String EVENTLOG_SOURCENAME_DEFAULT		= "WebApplication";
		private const TraceLevel EVENTLOG_TRACELEVEL_DEFAULT = TraceLevel.Error;

		//应用程序的根目录(物理路径)，调用Global.asax中的OnApplicationStart赋值；
		private static String appRoot;

		/// <summary>
		/// 调用OnApplicationStart，其初始化设置来自Web.Config文件中的值。
		/// </summary>
		/// <remarks>
		/// Create()将数据转给一个NameValueSectionHandler做实际的配置读取，然后检查是否有实际定义的配置值；
		/// 如果有，则将这些配置值传递给预先定义好的一组静态变量保存；
		/// 反之，则把预定义的默认值赋给这些静态变量。
		/// </remarks>
		/// <param name="parent"></param>
		/// <param name="configContext">配置的内容</param>
		/// <param name="section">读取的配置节</param>
		/// <returns>输出值为对象类型，null:  错误返回值</returns>
		public Object Create(Object parent, object configContext, XmlNode section)
		{
			//声明一个名称/值对集合变量settings；
			NameValueCollection settings;
			//
			//使用一个NameValueSectionHandler实例来解析配置，然后把解析得到的名称/值对集合传递给settings；
			//
			try
			{
				NameValueSectionHandler baseHandler = new NameValueSectionHandler();
				settings = (NameValueCollection)baseHandler.Create(parent, configContext, section);
			}
			catch
			{
				settings = null;
			}

			if (settings == null)
			{
				tracingEnabled				= TRACING_ENABLED_DEFAULT;
				tracingTraceFile			= TRACING_TRACEFILE_DEFAULT;
				tracingTraceLevel			= TRACING_TRACELEVEL_DEFAULT;
				tracingSwitchName			= TRACING_SWITCHNAME_DEFAULT;
				tracingSwitchDescription	= TRACING_SWITCHDESCRIPTION_DEFAULT;
				eventLogEnabled				= EVENTLOG_ENABLED_DEFAULT;
				eventLogMachineName			= EVENTLOG_MACHINENAME_DEFAULT;
				eventLogSourceName			= EVENTLOG_SOURCENAME_DEFAULT;
				eventLogTraceLevel			= EVENTLOG_TRACELEVEL_DEFAULT;
			}
			//
			//如果有实际的配置值，则调用ReadSetting()方法从settings中取得相应的配置值赋给私有静态变量；
			//
			else
			{
				tracingEnabled				= ReadSetting(settings, TRACING_ENABLED, TRACING_ENABLED_DEFAULT);
				tracingTraceFile			= ReadSetting(settings, TRACING_TRACEFILE, TRACING_TRACEFILE_DEFAULT);
				tracingTraceLevel			= ReadSetting(settings, TRACING_TRACELEVEL, TRACING_TRACELEVEL_DEFAULT);
				tracingSwitchName			= ReadSetting(settings, TRACING_SWITCHNAME, TRACING_SWITCHNAME_DEFAULT);
				tracingSwitchDescription	= ReadSetting(settings, TRACING_SWITCHDESCRIPTION, TRACING_SWITCHDESCRIPTION_DEFAULT);
				eventLogEnabled				= ReadSetting(settings, EVENTLOG_ENABLED, EVENTLOG_ENABLED_DEFAULT);
				eventLogMachineName			= ReadSetting(settings, EVENTLOG_MACHINENAME, EVENTLOG_MACHINENAME_DEFAULT);
				eventLogSourceName			= ReadSetting(settings, EVENTLOG_SOURCENAME, EVENTLOG_SOURCENAME_DEFAULT);
				eventLogTraceLevel			= ReadSetting(settings, EVENTLOG_TRACELEVEL, EVENTLOG_TRACELEVEL_DEFAULT);
			}

			return null;
		}

		/// <summary>
		/// 读写设置的String版本。
		/// </summary>
		/// <param name="settings">名称/值对集合</param>
		/// <param name="key">节点属性</param>
		/// <param name="defaultValue">默认值(字符串型)</param>
		/// <returns>字符串型</returns>
		public static String ReadSetting(NameValueCollection settings, String key, String defaultValue)
		{
			try
			{
				Object setting = settings[key];
				return (setting == null) ? defaultValue : (String)setting;
			}
			catch
			{
				return defaultValue;
			}
		}

		/// <summary>
		/// 读写设置的String版本。
		/// </summary>
		/// <param name="settings">名称/值对集合</param>
		/// <param name="key">节点属性</param>
		/// <param name="defaultValue">是否为默认值</param>
		/// <returns>bool型</returns>
		public static bool ReadSetting(NameValueCollection settings, String key, bool defaultValue)
		{
			try
			{
				Object setting = settings[key];
				return (setting == null) ? defaultValue : Convert.ToBoolean((String)setting);
			}
			catch
			{
				return defaultValue;
			}
		}

		/// <summary>
		/// 读写设置的String版本。
		/// </summary>
		/// <param name="settings">名称/值对集合</param>
		/// <param name="key">节点属性</param>
		/// <param name="defaultValue">默认值(int型)</param>
		/// <returns>int型</returns>
		public static int ReadSetting(NameValueCollection settings, String key, int defaultValue)
		{
			try
			{
				Object setting = settings[key];
				return (setting == null) ? defaultValue : Convert.ToInt32((String)setting);
			}
			catch
			{
				return defaultValue;
			}
		}

		/// <summary>
		/// 读写设置的String版本。
		/// </summary>
		/// <param name="settings">名称/值对集合</param>
		/// <param name="key">节点属性</param>
		/// <param name="defaultValue">默认值(TraceLevel)</param>
		/// <returns>跟踪级别TraceLevel</returns>
		public static TraceLevel ReadSetting(NameValueCollection settings, String key, TraceLevel defaultValue)
		{
			try
			{
				Object setting = settings[key];
				return (setting == null) ? defaultValue : (TraceLevel)Convert.ToInt32((String)setting);
			}
			catch
			{
				return defaultValue;
			}
		}

		/// <summary>
		/// Application_OnStart功能描述，在应用程序的根目录中初始化。
		/// </summary>
		/// <param name="myAppPath">应用程序运行的路径</param>
		public static void OnApplicationStart(String myAppPath)
		{
			//指定应用程序的路径；
			appRoot = myAppPath;

			//使用ConfigurationSettings()方法返回自定义配置节的配置设置，然后会触发配置节处理程序ApplicationConfiguration的Create()方法；
            ConfigurationManager.GetSection("ApplicationConfiguration");
			//触发配置节处理程序Tax666Configuration的Create()方法；
            ConfigurationManager.GetSection("Tax666Configuration");
		}

		/// <value>
		///  属性: 获取应用程序根目录的路径(只读类型)
		/// </value>
		public static String AppRoot
		{
			get
			{
				return appRoot;
			}
		}

		//
		//以下为跟踪/调试的各项属性设置(均为只读类型)
		//
		/// <value>
		///  属性: 是否允许跟踪
		/// </value>
		public static bool TracingEnabled
		{
			get
			{
				return tracingEnabled;
			}
		}

		/// <value>
		///  属性: 跟踪调试的记录文件
		/// </value>
		public static String TracingTraceFile
		{
			get
			{
				return tracingTraceFile;
			}
		}

		/// <value>
		///  属性: 跟踪等级
		/// </value>
		public static TraceLevel TracingTraceLevel
		{
			get
			{
				return tracingTraceLevel;
			}
		}

		/// <value>
		///  属性: 跟踪开关名称
		/// </value>
		public static String TracingSwitchName
		{
			get
			{
				return tracingSwitchName;
			}
		}

		/// <value>
		///  属性: 跟踪开关属性描述
		/// </value>
		public static String TracingSwitchDescription
		{
			get
			{
				return tracingSwitchDescription;
			}
		}

		/// <value>
		///  属性: 是否允许记录事件日志
		/// </value>
		public static bool EventLogEnabled
		{
			get
			{
				return eventLogEnabled;
			}
		}

		/// <value>
		///  属性: 记录事件日志的机器名称
		/// </value>
		public static String EventLogMachineName
		{
			get
			{
				return eventLogMachineName;
			}
		}

		/// <value>
		///  属性: 事件日志源名称
		/// </value>
		public static String EventLogSourceName
		{
			get
			{
				return eventLogSourceName;
			}
		}

		/// <value>
		///  属性: 事件日志跟踪等级
		/// </value>
		public static TraceLevel EventLogTraceLevel
		{
			get
			{
				return eventLogTraceLevel;
			}
		}

	}
}
