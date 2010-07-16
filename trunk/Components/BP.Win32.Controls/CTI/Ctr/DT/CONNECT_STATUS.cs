using System;

namespace BP.CTI
{
	/// <summary>
	/// 链接状态
	/// </summary>
	public class CONNECT_STATUS
	{
		public  const int SI_WORK0=0;   
		public  const int SI_WORK1=1;
		public  const int SI_WORK2=2;
		/// <summary>
		/// SI_WAIT_ACM
		/// </summary>
		public  const int SI_WAIT_ACM=3;
		public  const int SI_WORK3_1=4;   
		public  const int SI_WORK3_2=5;
		public  const int SI_WAIT_ANS=6;
		public  const int SI_WORK4=7;
		public  const int SI_TALK=8; 
		public  const int SI_WORK6=9; 
		public  const int SI_WORK7=10;
		public  const int SI_WORK8=11;
		public  const int SO_WORK0=12;
		/// <summary>
		/// 
		/// </summary>
		public  const int SO_WORK1=13;
		public  const int SO_WORK2=14;
		public  const int SO_WORK3=15;
		/// <summary>
		/// 连接状态
		/// </summary>
		public  const int SO_CONNECT=16;
		/// <summary>
		/// 对方挂机
		/// </summary>
		public  const int SO_TALK=17;
		/// <summary>
		/// 对方挂机
		/// </summary>
		public  const int SO_WORK7=18;
		public  const int SO_WORK8=19;
	}
}
