using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace BP.CTI
{
	public class tw16vid
	{
		#region TV
		/// <summary>
		/// 初始化函数
		/// </summary>
		/// <returns>可用通道数</returns>
		[DllImport("tw16vid.dll")]
		public static extern int TV_Installed();
		/// <summary>
		/// 初始化函数
		/// </summary>
		[DllImport("tw16vid.dll")]
		public static extern void TV_Initialize();

		/// <summary>
		/// 得到序列号函数
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		[DllImport("tw16vid.dll")]
		public static extern int TV_GetSerial(byte[] s);
		#endregion
		 
		#region 应用
		/// <summary>
		/// 得到序列号
		/// </summary>
		public static void GetSerial()
		{
			byte[] bs = new byte[20];
			char[] cs = new char[20];
			string s;

			if (TV_Installed() > 0)
			{
				TV_Initialize();
				TV_GetSerial(bs);
				for(int i = 0; i < cs.Length ; i++)
					cs[i] = Convert.ToChar(bs[i]);					;
				s = new string(cs);
				//return s;
				//MessageBox.Show(s);
				//Console.WriteLine(s);
			}
			else
			{
				throw new Exception("未安装驱动!!!");
			}
		}
		#endregion

	}
}