using System;

namespace BP.CTI
{
	/// <summary>
	/// App 的摘要说明。
	/// </summary>
	public class GEApp
	{
		


		#region 控制
		/// <summary>
		/// 启动
		/// </summary>
		public static void StartCall()
		{

			Card.Initialize();
			
			pcmn7api.PCM7_CompressRatio(64);
			pcmn7api.PCM7_CallOutPara(1, "0x18", "0x400", 0);

			while(true)
			{
				// 得到一个空闲的通道。




			}
			// api PCM7_CompressRatio (RATE_64K);
		}
		/// <summary>
		/// 停止
		/// </summary>
		public static void Disable()
		{
		}
		#endregion

		#region 呼出
		/// <summary>
		/// 呼出一个号码
		/// </summary>
		/// <param name="ch"></param>
		/// <param name="tel"></param>
		/// <param name="file"></param>
		public static void Call(int ch, string tel,string file)
		{
		}
		#endregion

		
	}
}
