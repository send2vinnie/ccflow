using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace BP.CTI
{
//	public enum CONNECT_STATUS
//	{	
//		SI_WORK0,
//		SI_WORK1,
//		SI_WORK2,
//		SI_WAIT_ACM,
//		SI_WORK3_1,
//		SI_WORK3_2,
//		SI_WAIT_ANS,
//		SI_WORK4,
//		SI_TALK,
//		SI_WORK6,
//		SI_WORK7,
//		SI_WORK8,
//		SO_WORK0,
//		SO_WORK1,
//		SO_WORK2,
//		SO_WORK3,
//		SO_CONNECT,
//		SO_TALK,
//		SO_WORK7,
//		SO_WORK8
//	}
	/// <summary>
	/// RATE
	/// </summary>
	public class RATE
	{
		public const int RATE_64K=0;
		public const int RATE_32K=1;
		public const int RATE_16K=2;
	}
	public class pcmn7api:Usbid
	{
		#region 初始化等函数
		/// <summary>
		/// 初始化函数
		/// </summary>
		/// <returns>可用通道数</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_Installed();
		/// <summary>
		/// 初始化函数
		/// </summary>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_Initialize();
		/// <summary>
		/// 得到序列号函数
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_GetSerial(  byte[] s);
		/// <summary>
		/// 禁止数字卡工作
		/// 在 PCM7 应用程序退出前, 一般可调用此函数。PCM7_Diable()
		///  关闭驱动的中断程序，挂断所有中继线，停止所有 DTMF码收发，停止所有录放音。调用本函数后，数字卡的中断被关闭；数字卡要再次开始工作，
		///  可调用函数PCM7_Initialize() 重新打开中断。
		/// </summary>
		/// <returns>出错时为错误号</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_Disable();
		/// <summary>
		/// 检查通到是否有传真输出．
		/// </summary>
		/// <param name="Ch">通道号码</param>
		/// <returns>0 or 1</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_CheckFax(int Ch);

		/// <summary>
		/// 检查Link 状态．
		/// </summary>
		/// <param name="Ch">通道号码</param>
		/// <returns>0 or 1</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_GetLinkStatus(int Ch);
		#endregion

		#region 通道连接函数
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_ConnectPcm2Pcm(int Ch1, int ch2);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>	 
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_DisconnectConPcm2Pcm(int Ch1);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_ConnectPcmPcm(int Ch1, int ch2);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_DisConnectPcmPcm(int Ch1, int ch2);
		#endregion

		#region 收发 DTMF码函数
		/// <summary>
		/// 接受用户的按键
		/// </summary>
		/// <param name="Ch">通道号码</param>
		/// <returns>返回的编码</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_GetDtmfChar(int Ch);

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StartDtmfDetect(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StopDtmfDetect(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_SendDtmf(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StopSendDtmf(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_SendDtmfRest(int Ch);
		#endregion

		#region 电话接续函数
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_SendSig(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_ReceiveSig(byte[]  Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_CallOutPara(int  type,byte[] ss, string ss2,int type1  );
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_GetCallOutChn();
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_ChnCallOutPara();
		
		/// <summary>
		/// 呼出电话
		/// </summary>
		/// <param name="ch">通道</param>
		/// <param name="calledNo">被叫号码</param>
		/// <param name="callingNo">主叫号码</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StartCallOut(int ch, byte[] calledNo, byte[] callingNo);

		//public static extern int PCM7_StartCallOut(int ch,string calledNo,string callingNo);
		/// <summary>
		/// 得到当前的状态
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_CallOutStatus(int ch);
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_PhoneInPara();

		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_ChnPhoneInPara();
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_PhoneInDetect();


		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_PhoneInStatus();


		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_GetTeleNo();

		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_SendACM();


		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_SendUBM();

		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_SendAnswer();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_HangUpDetect(int ch);

		/// <summary>
		/// 通道
		/// </summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_HangUpCtrl(int ch);
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_ResetChannel(int ch);
		/// <summary>
		/// 得到挂机的原因
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_GetHangUpReason(int ch);
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_GetChannelStatus(int ch);
		#endregion

		#region 录放音函数
		/// <summary>
		/// 设置录放音的压缩比
		/// </summary>
		/// <param name="bte"></param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_CompressRatio(int bte );
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StartRecord();
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_RecordRest();
		/// <summary>
		/// 停止录音
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StopRecord();

		/// <summary>
		/// 开始放音.
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StartPlay();

		/// <summary>
		/// 调整放音
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_PlayRest();

		/// <summary>
		/// 停止放音
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StopPlay();

		/// <summary>
		/// 开始录音
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StartRecordFile();

		/// <summary>
		/// 录音
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_RecordFileRest();

		/// <summary>
		/// 停止录音
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StopRecordFile();

		/// <summary>
		/// 放音函数
		/// </summary>
		/// <param name="ch">通道</param>
		/// <param name="fileName">文件名称</param>
		/// <param name="startBye">开始放音的字节</param>
		/// <param name="playlen">放音长度</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StartPlayFile(int ch,  byte[] fileName, int startBye,int playlen);
		/// <summary>
		/// 继续文件放音, 并不断将语音文件中未放音的部分读入缓冲区
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_PlayFileRest(int ch);

		/// <summary>
		/// 强制停止某一通道的文件放音
		/// </summary>
		/// <param name="ch">通道号码</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StopPlayFile(int ch);

		　
		#region 多文件放音
		/// <summary>
		/// 造句
		/// </summary>
		/// <param name="ch">通道</param>
		/// <param name="files">自己定义的文件名称</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int PCM7_StartPlaySentence(int ch,  byte[] fls);

		/// <summary>
		/// 放音
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_PlaySentenceRest(int ch);

		/// <summary>
		/// 停止放音
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StopPlaySentence(int ch);

		/// <summary>
		/// 造句
		/// </summary>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_MakeSentence();
		#endregion
		
		#endregion

		#region 计时器函数
		/// <summary>
		/// 开始计时
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <param name="mm">时间</param>
		/// <returns>错误号码</returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_StartTimer(int ch, int mm);

		/// <summary>
		/// 开始计时
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <returns></returns>
		[DllImport("pcmn7api.dll")]
		public static extern int  PCM7_TimerElapsed(int ch);
		#endregion

		#region 模拟卡函数
		#endregion

		#region 信令监视函数
		#endregion

	}
	/// <summary>
	/// 错误编号
	/// </summary>
	public class ErrorCode
	{
		public const int EP_ERR=-1 ;    /* general error */
		public const int EP_DRIVER=-2;  /* PCMN7 driver not installed */
		public const int EP_CHANNEL=-3;   /* Invalid channel number */
		public const int EP_ERR_ARGUMENT= -4;  /* error argument */
		public const int EP_ERR_NOT_INIT=-5; //没有初始化
		public const int EP_ERR_SYNC=-6;		// synchronization object error
		public const int EP_ERR_HARDWARE_CONFIG=-7;
		public const int EP_TFSYN=-10;
		public const int EP_MFSYN=-11;
		public const int EP_RXAIS=-12;
		public const int EP_RXTS16AIS= -13;
		public const int EP_RCV_OVERFLOW=-20;
		public const int EP_ERR_TELENO=-30;
		public const int EP_DIAL_STRING_TOO_LONG=-31;
		public const int EP_PLAY_RECORD_CONFLICT=-40;
		public const int EP_RECORD_BUSY=-41;
		public const int EP_PLAY_BUSY=-42;
		public const int EP_OUT_OF_MEMORY= -43;
		public const int EP_FILEOPEN  = -44;
		public const int EP_TIMEOUT= -50; /* time out */
		public const int PCM_RP_BUF_SIZE=0x4000;
	}
}