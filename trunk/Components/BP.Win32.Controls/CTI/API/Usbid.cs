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
	/// 通道的类型
	/// </summary>
	public enum TVChannelType
	{
		/// <summary>
		/// 内线通道
		/// </summary>
		CT_INTERNAL,
		/// <summary>
		/// 外线通道
		/// </summary>
		CT_EXTERNAL,
		/// <summary>
		/// 空通道
		/// </summary>
		CT_EMPTY
	}
	 
	public class Usbid
	{
		#region 特殊函数
		

		/// <summary>
		/// 用户自定义信号音
		/// </summary>
		/// <param name="ss">信号音变量(0 SIG_RING, 1 SIG_BUSY1, 2 SIG_BUSY2已设置)用户从3开始设起 只能是3 或4 两种自定义忙音</param>
		/// <param name="hlen">信号音长度 单位40ms</param>
		/// <param name="llen">静音的长度 单位40ms  该参数在 参数4，5 为 0 的时候有效</param>
		/// <param name="linterval1">信号音之间静音的间隔长度(第一种情况)</param>
		/// <param name="linterval2">信号音之间静音的间隔长度(第二种情况)</param>
		[DllImport("USBID.dll")]
		public static extern  int TV_SetSignalParamEx( int ss, double hlen, double llen,double linterval1,double linterval2);
		/// <summary>
		/// 用户自定义信号音
		/// </summary>
		/// <param name="para1">信号音变量(0 SIG_RING, 1 SIG_BUSY1, 2 SIG_BUSY2已设置)用户从3开始设起</param>
		/// <param name="para2">信号音零值 时间最小范围值</param>
		/// <param name="para3">信号音零值 时间最大范围值</param>
		/// <param name="para4">信号音峰值 时间最小范围值</param>
		/// <param name="para5">信号音峰值 时间最大范围值</param>
		[DllImport("USBID.dll")]
		public static extern  void TV_SetSignalParam(int para1,int para2,int para3,int para4,int para5);
		
		/// <summary>
		/// 设置通道所用的信号音频率，可动态改变
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <param name="hz">频率 （如450HZ）</param>
		/// <returns>错误代码</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_SetChannelFreq(int ch, int hz);
		/// <summary>
		/// 根据忙音监视挂机状态
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <param name="sign">忙音信号音类型</param>
		/// <param name="num">忙音个数</param>
		/// <returns></returns> 
		[DllImport("USBID.dll")]
		public static extern int TV_MonitorBusy(int ch, int sign, int num);
		/// <summary>
		/// 初始化函数
		/// </summary>
		[DllImport("USBID.dll")]
		public static extern int TV_Initialize(int i);

		/// <summary>
		/// 控制某一外线通道摘机
		/// </summary>
		/// <returns>通道号</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_OffHookCtrl(int ch);
		
		/// <summary>
		///  某一通道进行自动拨号
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <param name="DialNum">拨号字符串</param>
		/// <returns>上次拨号时没有拨完的字符数</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StartDial(int ch,byte[] DialNum);
		
		/// <summary>
		/// 查询某一通道的类型
		/// </summary>
		/// <param name="ch">通道号</param>		
		/// <returns>0CT_INTERNAL内线通道,1CT_EMPTY外线通道,2 CT_EMPTY空通道</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_ChannelType(int ch);


		/// <summary>
		/// 停止某一通道的自动 DTMF 拨号
		/// </summary>
		/// <param name="ch">通道号</param>		
		/// <returns>没有拨完的字符数</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StopDial(int ch);


		/// <summary>
		/// 查询某一通道有多少字节没有拨完
		/// </summary>
		/// <returns>通道号</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_CheckSignal(int ch, ref int a, ref int b );

		/// <summary>
		/// 查询某一通道有多少字节没有拨完
		/// </summary>
		/// <returns>通道号</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_DialRest(int ch);


		/// <summary>
		/// 开始监视被叫方的摘机状态
		/// </summary>
		/// <returns>通道号</returns>
		[DllImport("USBID.dll")]
		public static extern void TV_StartMonitor(int ch);



		/// <summary>
		/// 在控制某一外线通道摘机并调用 TV_StartDial(...) 自动拨号之后, 
		/// 此函数可以用来查询被呼叫方是否已经摘机
		/// </summary>
		/// <returns> 0 false, 1 true</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_ListenerOffHook(int ch);
		
		/// <summary>
		/// TV_MonitorOffHook
		/// </summary>
		/// <param name="ch"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]		 
		public static extern int TV_MonitorOffHook(int ch, int time);


		#endregion

		#region 初始化等函数
		/// <summary>
		/// 初始化函数
		/// </summary>
		/// <returns>可用通道数</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_Installed();
		
		/// <summary>
		/// 得到序列号函数
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_GetSerial(byte[] s);
		/// <summary>
		/// 禁止数字卡工作
		/// 在 PCM7 应用程序退出前, 一般可调用此函数。TV_Diable()
		///  关闭驱动的中断程序，挂断所有中继线，停止所有 DTMF码收发，停止所有录放音。调用本函数后，数字卡的中断被关闭；数字卡要再次开始工作，
		///  可调用函数TV_Initialize() 重新打开中断。
		/// </summary>
		/// <returns>出错时为错误号</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_Disable();
		/// <summary>
		/// 检查通到是否有传真输出．
		/// </summary>
		/// <param name="Ch">通道号码</param>
		/// <returns>0 or 1</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_CheckFax(int Ch);

		/// <summary>
		/// 检查Link 状态．
		/// </summary>
		/// <param name="Ch">通道号码</param>
		/// <returns>0 or 1</returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_GetLinkStatus(int Ch);
		#endregion

		#region 通道连接函数
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_ConnectPcm2Pcm(int Ch1, int ch2);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>	 
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_DisconnectConPcm2Pcm(int Ch1);
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_ConnectPcmPcm(int Ch1, int ch2);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch1"></param>
		/// <param name="ch2"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_DisConnectPcmPcm(int Ch1, int ch2);
		#endregion

		#region 收发 DTMF码函数
		/// <summary>
		/// 接受用户的按键
		/// </summary>
		/// <param name="Ch">通道号码</param>
		/// <returns>返回的编码</returns>
		[DllImport("USBID.dll")]
		public static extern int TV_GetDtmfChar(int Ch);

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StartDtmfDetect(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StopDtmfDetect(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_SendDtmf(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StopSendDtmf(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_SendDtmfRest(int Ch);
		#endregion

		#region 电话接续函数
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_SendSig(int Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_ReceiveSig(byte[]  Ch);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_CallOutPara(int  type,byte[] ss, string ss2,int type1  );
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_GetCallOutChn();
		[DllImport("USBID.dll")]
		public static extern int TV_ChnCallOutPara();
		
		/// <summary>
		/// 呼出电话
		/// </summary>
		/// <param name="ch">通道</param>
		/// <param name="calledNo">被叫号码</param>
		/// <param name="callingNo">主叫号码</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StartCallOut(int ch, byte[] calledNo, byte[] callingNo);

		//public static extern int TV_StartCallOut(int ch,string calledNo,string callingNo);
		/// <summary>
		/// 得到当前的状态
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_CallOutStatus(int ch);
		[DllImport("USBID.dll")]
		public static extern int TV_PhoneInPara();

		[DllImport("USBID.dll")]
		public static extern int TV_ChnPhoneInPara();
		[DllImport("USBID.dll")]
		public static extern int TV_PhoneInDetect();


		[DllImport("USBID.dll")]
		public static extern int TV_PhoneInStatus();


		[DllImport("USBID.dll")]
		public static extern int  TV_GetTeleNo();

		[DllImport("USBID.dll")]
		public static extern int  TV_SendACM();


		[DllImport("USBID.dll")]
		public static extern int  TV_SendUBM();

		[DllImport("USBID.dll")]
		public static extern int  TV_SendAnswer();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_HangUpDetect(int ch);

		/// <summary>
		/// 通道
		/// </summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_HangUpCtrl(int ch);
		[DllImport("USBID.dll")]
		public static extern int  TV_ResetChannel(int ch);
		/// <summary>
		/// 得到挂机的原因
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_GetHangUpReason(int ch);
		[DllImport("USBID.dll")]
		public static extern int  TV_GetChannelStatus(int ch);
		#endregion

		#region 录放音函数
		/// <summary>
		/// 设置录放音的压缩比
		/// </summary>
		/// <param name="bte"></param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_CompressRatio(int bte );
		[DllImport("USBID.dll")]
		public static extern int  TV_StartRecord();
		[DllImport("USBID.dll")]
		public static extern int  TV_RecordRest();
		/// <summary>
		/// 停止录音
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StopRecord();

		/// <summary>
		/// 开始放音.
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StartPlay();

		/// <summary>
		/// 调整放音
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_PlayRest();

		/// <summary>
		/// 停止放音
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StopPlay();

		/// <summary>
		/// 开始录音
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StartRecordFile();

		/// <summary>
		/// 录音
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_RecordFileRest();

		/// <summary>
		/// 停止录音
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StopRecordFile();

		/// <summary>
		/// 放音函数
		/// </summary>
		/// <param name="ch">通道</param>
		/// <param name="fileName">文件名称</param>
		/// <param name="startBye">开始放音的字节</param>
		/// <param name="playlen">放音长度</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StartPlayFile(int ch,  byte[] fileName, int startBye,int playlen);
		/// <summary>
		/// 继续文件放音, 并不断将语音文件中未放音的部分读入缓冲区
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_PlayFileRest(int ch);

		/// <summary>
		/// 强制停止某一通道的文件放音
		/// </summary>
		/// <param name="ch">通道号码</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StopPlayFile(int ch);

		　
		#region 多文件放音
		/// <summary>
		/// 造句
		/// </summary>
		/// <param name="ch">通道</param>
		/// <param name="files">自己定义的文件名称</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int TV_StartPlaySentence(int ch,  byte[] fls);

		/// <summary>
		/// 放音
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_PlaySentenceRest(int ch);

		/// <summary>
		/// 停止放音
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StopPlaySentence(int ch);

		/// <summary>
		/// 造句
		/// </summary>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_MakeSentence();
		#endregion
		
		#endregion

		#region 计时器函数
		/// <summary>
		/// 开始计时
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <param name="mm">时间</param>
		/// <returns>错误号码</returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_StartTimer(int ch, int mm);

		/// <summary>
		/// 开始计时
		/// </summary>
		/// <param name="ch">通道号</param>
		/// <returns></returns>
		[DllImport("USBID.dll")]
		public static extern int  TV_TimerElapsed(int ch);
		#endregion

		#region 模拟卡函数
		#endregion

		#region 信令监视函数
		#endregion

	}
	 
}