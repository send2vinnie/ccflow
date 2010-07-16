using System;

namespace BP.CTI
{
	/// <summary>
	/// 通道状态
	/// </summary>
	public class CH_STATUS
	{
		/// <summary>
		/// 空闲
		/// </summary>
		public  const int CH_SYN=0;
		/// <summary>
		/// 
		/// </summary>
		public  const int CH_SS7=1;
		/// <summary>
		/// 空闲
		/// </summary>
		public  const int CH_FREE=2;
		/// <summary>
		/// 呼出
		/// </summary>
		public  const int CH_CALLOUT=3;
		/// <summary>
		/// 电话打入
		/// </summary>
		public  const int CH_PHONEIN=4;
		/// <summary>
		/// 空的
		/// </summary>
		public  const int CH_EMPTY=5;
	}
}
