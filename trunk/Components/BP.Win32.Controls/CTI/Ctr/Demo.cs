using System;
using BP.CTI;
using BP.CTI.App;
using BP.DA;

namespace BP.CTI
{
	/// <summary>
	/// TVDemo 的摘要说明。
	/// </summary>
	public class Demo
	{
		public static void TVCall(string tel,string file, int je, int ch)
		{
			Log.DebugWriteInfo("测试程序启动。。。");
			
			int  SIG_SILENCE=64;  //静, 寂静, 沉默, 静默
			int  SIG_DIAL=65; // 拨
			 
			bool ReverseFlag=false;
			//初始化
			Usbid.TV_Installed();
			Usbid.TV_Initialize(0);

			Usbid.TV_OffHookCtrl(ch); //摘机
			Usbid.TV_StartTimer(ch,4); 

			int sigpara1=0,sigpara2=0;

			// 等待拨号音。			 
			Log.DebugWriteInfo("开始等待拨号音");
			while (Usbid.TV_CheckSignal(ch, ref sigpara1,ref sigpara2) != SIG_DIAL)
			{
				if (Usbid.TV_TimerElapsed (ch) < 0)
				{
					Usbid.TV_HangUpCtrl (ch);

					Log.DebugWriteInfo("等待超时，无拨号音，如果您确认您的线路是正常的，请在试一次。");
					throw new Exception("等待超时，无拨号音，如果您确认您的线路是正常的，请在试一次。");
				}
			}
			
			Log.DebugWriteInfo("已经获取拨号音，开始拨号 ");
			Usbid.TV_StartDial(ch, DataType.StringToByte(tel) ); // 拨号
			while (Usbid.TV_DialRest (ch) != 0)
			{
			}

			Usbid.TV_StopDial(ch);
			Log.DebugWriteInfo("拨号完毕 ");
			//Usbid.TV_OffHookCtrl(ch);

			while (Usbid.TV_TimerElapsed (ch) <=2 )
			{
			}

			Usbid.TV_StartTimer (ch, 40); // 设置时间.
			Usbid.TV_StartMonitor (ch);   // 开始监听

			while (true)
			{
				if ( Usbid.TV_ListenerOffHook (ch) > 0 )
				{
					Log.DebugWriteInfo("判断对方摘机 TV_ListenerOffHook");
					break;
				}

				if (!ReverseFlag) 
				{
					if (Usbid.TV_MonitorOffHook(ch, 25) != 0 )	/* 1 Second */
					{
						Log.DebugWriteInfo("判断对方摘机, ReverseFlag, TV_MonitorOffHook "+Usbid.TV_MonitorOffHook (ch, 25));
						break;
					}
					Log.DebugWriteInfo("TV_MonitorOffHook (ch, 25)="+Usbid.TV_MonitorOffHook (ch, 25));
				}

				int	Sig=0, SigCount=0, SigLen=0;			

				Sig = Usbid.TV_CheckSignal (ch, ref SigCount, ref SigLen);

				Log.DebugWriteInfo("Sig="+Sig+",SigCount="+SigCount+",SigLen="+SigLen+", TV_ListenerOffHook"+Usbid.TV_ListenerOffHook (ch)+" TV_MonitorOffHook (Channel, 25)= "+Usbid.TV_MonitorOffHook (ch, 25));
				if (  (Sig == 1 || Sig == 2  )  && SigCount >= 3  ) 
				{
					Usbid.TV_HangUpCtrl (ch);
					Log.DebugWriteInfo("对方忙音");
					throw new Exception("对方忙音");
				 
				}
				 
				 
				if (Usbid.TV_TimerElapsed (ch) < 0)
				{
					Usbid.TV_HangUpCtrl (ch);
					Log.DebugWriteInfo("呼叫超时");
					throw new Exception("呼叫超时");
				 
				}
			}

			// 播放文件			 
			Log.DebugWriteInfo("开始播放文件..");

			if (Usbid.TV_StartPlayFile(ch, DataType.StringToByte( file ), 0, 0)== -1) 
			{
				throw new Exception("file error . ");
			}

			while (Usbid.TV_PlayFileRest (ch) > 0) 
			{
			 
				if (Usbid.TV_MonitorBusy (ch, 1, 5)!=0 || Usbid.TV_MonitorBusy (ch, 2, 5)!=0 ) 
				{
					Log.DebugWriteInfo("对方忙音，执行挂机 ");

					Usbid.TV_HangUpCtrl (ch);
				}
			}

			Usbid.TV_HangUpCtrl (ch);

			Log.DebugWriteInfo("play file ok ");

			Usbid.TV_Disable();




		}

		public static void TVCall_old(string tel,string file, int je, int yf)
		{
			
            int  SIG_SILENCE=64;  //静, 寂静, 沉默, 静默
            int  SIG_DIAL=65; // 拨

			int ch=0;
			bool ReverseFlag=false;
			
			//初始化
			Usbid.TV_Installed();
			Usbid.TV_Initialize(0);


			//Usbid.TV_SetChannelFreq(0,450); // 频率分析的是450.

			//Usbid.TV_SetSignalParam(3,450,); // 频率分析的是450.
		//	Usbid.TV_SetSignalParamEx(3,450,); // 频率分析的是450.




			Usbid.TV_OffHookCtrl(ch); //摘机
			Usbid.TV_StartTimer(ch,4); 

			int sigpara1=0,sigpara2=0;

			// 等待拨号音。
			 
			Log.DebugWriteInfo("开始等待拨号音");

			while (Usbid.TV_CheckSignal(ch, ref sigpara1,ref sigpara2) != SIG_DIAL)
			{
				 

				if (Usbid.TV_TimerElapsed (ch) < 0)
				{
					Log.DebugWriteInfo("等待超时,无拨号音");
					Usbid.TV_HangUpCtrl (ch);
					return;
				}
			}


			
			Log.DebugWriteInfo("已经获取拨号音，开始拨号 ");

			Usbid.TV_StartDial(ch, DataType.StringToByte(tel) ); // 拨号


			while (Usbid.TV_DialRest (ch) != 0)
			{
			}
			Usbid.TV_StopDial(ch);

			Log.DebugWriteInfo("拨号完毕 ");

			Usbid.TV_OffHookCtrl(ch);




			while (Usbid.TV_TimerElapsed (ch) <=2 )
			{
			}

			Usbid.TV_StartTimer (ch, 40); // 设置时间.
			Usbid.TV_StartMonitor (ch);   // 开始监听

			while (true)
			{
			
				if ( Usbid.TV_ListenerOffHook (ch) > 0 )
				{
					Log.DebugWriteInfo("判断对方摘机 TV_ListenerOffHook");
					break;
				}

				if (!ReverseFlag) 
				{
					if (Usbid.TV_MonitorOffHook(ch, 25) != 0 )	/* 1 Second */
					{
						Log.DebugWriteInfo("判断对方摘机, ReverseFlag, TV_MonitorOffHook "+Usbid.TV_MonitorOffHook (ch, 25));
						break;
					}
					Log.DebugWriteInfo("TV_MonitorOffHook (ch, 25)="+Usbid.TV_MonitorOffHook (ch, 25));
				}

				int	Sig=0, SigCount=0, SigLen=0;			

				Sig = Usbid.TV_CheckSignal (ch, ref SigCount, ref SigLen);

				Log.DebugWriteInfo("Sig="+Sig+",SigCount="+SigCount+",SigLen="+SigLen+", TV_ListenerOffHook"+Usbid.TV_ListenerOffHook (ch)+" TV_MonitorOffHook (Channel, 25)= "+Usbid.TV_MonitorOffHook (ch, 25));
				if (  (Sig == 1 || Sig == 2  )  && SigCount >= 3  ) 
				{
					Log.DebugWriteInfo("对方忙音");
					Usbid.TV_HangUpCtrl (ch);
					return;
				}
//				if (Sig==64)
//				{
//					Log.DebugWriteInfo("Sig=64");
//					break;
//				}
				 
				if (Usbid.TV_TimerElapsed (ch) < 0)
				{
					Log.DebugWriteInfo("呼叫超时");
					Usbid.TV_HangUpCtrl (ch);
					return;
				}

			}

			// 播放文件

			 
			Log.DebugWriteInfo("开始播放文件 ");

			if (Usbid.TV_StartPlayFile(ch, DataType.StringToByte( file ), 0, 0)== -1) 
			{
				throw new Exception("file error . ");
			}

			while (Usbid.TV_PlayFileRest (ch) > 0) 
			{
			 
				if (Usbid.TV_MonitorBusy (ch, 1, 5)!=0 || Usbid.TV_MonitorBusy (ch, 2, 5)!=0 ) 
				{
					Log.DebugWriteInfo("对方忙音，执行挂机 ");

					Usbid.TV_HangUpCtrl (ch);
				}
			}

			Usbid.TV_HangUpCtrl (ch);

			Log.DebugWriteInfo("play file ok ");

			Usbid.TV_Disable();




		}
		
	}
}
