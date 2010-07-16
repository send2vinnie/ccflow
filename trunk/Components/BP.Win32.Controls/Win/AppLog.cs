using System;
using System.IO;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	/// <summary>
	/// 运行日志
	/// </summary>
	public class AppLog
	{
		static AppLog()
		{
			LogPathDir = Application.StartupPath +"\\Log\\";
			Directory.CreateDirectory( LogPathDir);
		}
		public static readonly string LogPathDir = "";
		public static void WriteLineLog(string filename ,string log)
		{
			string file = LogPathDir +filename ;
			StreamWriter wr = new StreamWriter( file ,true );
			wr.WriteLine( log );
			wr.Close();
		}
		public static void WriteLineAppConfigLog(string log)
		{
			string file = Application.StartupPath+"\\AppConfig.log" ;
			StreamWriter wr = new StreamWriter( file ,true );
			wr.WriteLine( log );
			wr.Close();
		}
		public static void WriteLineAppLog(string log)
		{
			string file = Application.StartupPath+"\\App.log" ;
			StreamWriter wr = new StreamWriter( file ,true );
			wr.WriteLine( log );
			wr.Close();
		}
	}
}
