using System;
using System.IO;
using System.Windows.Forms;

namespace BP.Win.Controls
{
	/// <summary>
	/// LogBase 的摘要说明。
	/// </summary>
	public class LogBase
	{
		static LogBase()
		{
			LogPathDir = Application.StartupPath +"\\Log\\";
			Directory.CreateDirectory( LogPathDir);
		}
		public static readonly string LogPathDir = "";
		public static void WriteLogLine(string filename ,string log)
		{
			string file = LogPathDir +filename ;
			StreamWriter wr = new StreamWriter( file ,true );
			wr.WriteLine( log );
			wr.Close();
		}
	}
}
