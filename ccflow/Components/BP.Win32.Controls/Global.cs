using System;
using System.Collections;
using System.IO;
using System.Data;
using System.Windows.Forms;
using BP.En;


namespace BP.Win32
{
	/// <summary>
	/// Global 的摘要说明。
	/// </summary>
	public class Global
	{
		public static void Run(string fileName)
		{
			try
			{
				System.Diagnostics.Process.Start(fileName)  ;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message+" file= "+fileName );
			}
		}
		public static readonly string AppConfigPath = Application.StartupPath+"\\App.config";

		static Global()
		{
			SystemConfig.IsBSsystem = false;
			///System.DateTime dt = File.GetLastWriteTime( Application.ExecutablePath );
			//VersionDate = dt.ToString("yyyy-MM-dd HH:mm");
		}

		public static bool LoadConfig()
		{
			SystemConfig.IsBSsystem = false;
			if( !File.Exists(AppConfigPath))
			{
				throw new Exception("找不到配置文件["+AppConfigPath+"]3");
				//	MessageBox.Show();
				//	return false;
			}
			DataSet ds = new DataSet("config");

			try
			{
				ds.ReadXml( AppConfigPath);
			}
			catch(Exception ex)
			{
				MessageBox.Show("加载配置文件["+AppConfigPath+"]失败！\n"+ex.Message,"启动失败！");
				return false;
			}

			#region 加载 Web.Config 文件配置
			DataTable tb = ds.Tables["ConfigFilePath"];
			if( tb!=null)
			{
				string cfgFile =Application.StartupPath +"//"+tb.Rows[0]["value"].ToString();
				if( !File.Exists(cfgFile))
					throw new Exception("找不到配置文件==>["+cfgFile+"]4");

				StreamReader read = new StreamReader( cfgFile );
				string firstline = read.ReadLine();
				string cfg = read.ReadToEnd();
				read.Close();

				int start = cfg.ToLower().IndexOf("<appsettings>");
				int end = cfg.ToLower().IndexOf("</appsettings>");

				cfg = cfg.Substring(start,end-start+"</appsettings".Length+1);

				cfgFile = Application.StartupPath +"\\__$AppConfig.cfg";
				StreamWriter write =new StreamWriter( cfgFile );
				write.WriteLine( firstline);
				write.Write( cfg);
				write.Flush();
				write.Close();

				DataSet dscfg =new DataSet("cfg");
				try
				{
					dscfg.ReadXml( cfgFile);
				}
				catch(Exception ex)
				{
					MessageBox.Show("加载配置文件["+cfgFile+"]失败！\n"+ex.Message,"启动失败！");
					return false;
				}

				BP.SystemConfig.CS_AppSettings = new System.Collections.Specialized.NameValueCollection();

				BP.SystemConfig.CS_DBConnctionDic.Clear();
				foreach(DataRow row in dscfg.Tables["add"].Rows)
				{
					BP.SystemConfig.CS_AppSettings.Add(row["key"].ToString().Trim(),row["value"].ToString().Trim());
				}

				
				dscfg.Dispose();
			}
			else
			{
				MessageBox.Show("没找到配置配置节[ConfigFilePath]，系统无法启动！","启动失败！");
			}
			#endregion 

			ds.Dispose();
			return true;
		}
	}
}
