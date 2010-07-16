using System;
using System.Runtime.InteropServices;
using BP.CTI.App;
using BP.DA;


namespace BP.CTI
{
	/// <summary>
	/// 板卡工作状态
	/// </summary>
	public enum CardWorkState
	{
		/// <summary>
		/// 运行
		/// </summary>
		Runing,
		/// <summary>
		/// 停止
		/// </summary>
		Stop,
		/// <summary>
		/// 正在暂停
		/// </summary>
		Stoping,
		/// <summary>
		/// 暂停
		/// </summary>
		Pause,
	}
	/// <summary>
	/// 板卡类型
	/// </summary>
	public enum CardType
	{
		/// <summary>
		/// 未知
		/// </summary>
		unknown,
		/// <summary>
		/// pcmn7api
		/// </summary>
		pcmn7api,
		/// <summary>
		/// tw16vid
		/// </summary>
		tw16vid,
		/// <summary>
		/// Usbid
		/// </summary>
		Usbid
		
	}
	/// <summary>
	/// 函数
	/// </summary>
	public class Card
	{
		/// <summary>
		/// 重新调整系统参数
		/// </summary>
		public static void ResetParas()
		{
			Sys.SysConfig config = new BP.Sys.SysConfig("CallTel");
			DefaultCalledTel=config.Val;

			config = new BP.Sys.SysConfig("MinJE"); // 最小的呼出金额
			DefaultMinJE=float.Parse(config.Val);

			Log.DebugWriteInfo("Card 变量初始化成功...");

		}

		#region 系统属性
		/// <summary>
		/// 板卡的工作状态
		/// </summary>
		public static CardWorkState HisCardWorkState=CardWorkState.Stop;
		/// <summary>
		/// 默认的主叫号码
		/// </summary>
		public static string DefaultCalledTel="2251000";
		/// <summary>
		/// 最小的呼出金额
		/// </summary>
		public static float DefaultMinJE=0;  
		/// <summary>
		/// 得到当前的呼出内容
		/// </summary>
		private static CallContext _CurrentCallContext=null;
		/// <summary>
		/// 得到当前的呼出内容
		/// </summary>
		public static CallContext CurrentCallContext
		{
			get
			{
				if (_CurrentCallContext==null)
				{
					_CurrentCallContext=CallContexts.GetCurrentCallContext;
				}

				if ( !(_CurrentCallContext.DateFrom < DateTime.Now.Day && 
					_CurrentCallContext.DateTo > DateTime.Now.Day))
				{ 
					/* 日期已经确定要变换.*/
					_CurrentCallContext= CallContexts.GetCurrentCallContext;
				}
				return _CurrentCallContext;
			}
		}
		/// <summary>
		/// 通道
		/// </summary>
		public static CHs HisCHs=null;
		
		#region count
		/// <summary>
		/// 得到ch状态的个.
		/// </summary>
		/// <param name="state">CHState</param>
		/// <returns>num</returns>
		public static int GetCHsCount(CHState state)
		{
			if (HisCHs==null)
				return 0;
			int i=0;
			foreach(CH ch in HisCHs)
			{
				if (ch.CHState==state)
					i++;
			}
			return i;
		}
		#endregion
		/// <summary>
		/// 板卡序列号码
		/// </summary>
		public static string Serial
		{
			get
			{
				// 获取序列号。
				byte[] bs = new byte[20];
				//char[] cs = new char[20];
				//string s;

				switch(Card.HisCardType)
				{
					case CardType.pcmn7api:
						pcmn7api.PCM7_GetSerial(bs);
						break;
					case CardType.Usbid:
						pcmn7api.TV_GetSerial(bs);
						break;
					default:
						throw new Exception("unknow card type");
				}
				return DataType.ByteToString(bs);				
			}
		}
		/// <summary>
		/// 通到数量
		/// </summary>
		public static int ChCount=0;
		/// <summary>
		/// 板卡类型
		/// </summary>
		public static CardType HisCardType=CardType.unknown;
		#endregion

		#region 控制方法	
		
		/// <summary>
		/// 设置录放音的压缩比
		/// </summary>
		/// <param name="bye">1,2,3, 分别是64,32,16通常为 64. </param>
		public static void CompressRatio(int bye)
		{
			pcmn7api.PCM7_CompressRatio(bye);
		}
		/// <summary>
		/// 出使化语音卡.
		/// </summary>
		/// <returns></returns>
		public static void Initialize()
		{
			if (SystemConfig.CustomerNo==CustomerNoList.YSNet)
				Card.HisCardType=CardType.pcmn7api;
			else
				Card.HisCardType=CardType.Usbid;

			// 把没有呼出成功的数据呼出出去.不应该存在这样的问题.
			DBAccess.RunSQL("update CTI_CallList set CallingState=0 where CallingState=1 or CallingState is null");
			// clear free list.
			DBAccess.RunSQL("delete CTI_CallList where tel in (select tel from CTI_Release)");

			ResetParas(); //设置系统参数.

			// 设置系统变量，用于得到序列号。

			// 出使化通道， 返回卡通道个数。
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					Card.ChCount=pcmn7api.PCM7_Installed();
					break;
				case CardType.Usbid:
					Card.ChCount=pcmn7api.TV_Installed();
					break;
				default:
					throw new Exception("un know card type");
			}

			if ( Card.ChCount <= 0)
				throw new Exception("未安装驱动!!!");

			 
			// 说明启动成功。
			int i =-1;				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					i=pcmn7api.PCM7_Initialize();
					break;
				case CardType.Usbid:
					i=pcmn7api.TV_Initialize();
					break;
				default:
					throw new Exception("un know card type");
			}

			if (i < 0)
			{
				string msg="错误Initialize errornum = "+i.ToString();
				Log.DefaultLogWriteLineError(msg);
				throw new Exception(msg);
			}

			
			// init chs.
			Card.HisCHs = new CHs();
			//CH ch8 =new CH(8);
			//CH ch9 =new CH(9);
			//Card.HisCHs.Add( ch8 );
			//Card.HisCHs.Add( ch9 );

			
			for(int ch=1; ch < Card.ChCount; ch++)
				Card.HisCHs.Add(new CH(ch));

			Card.HisCardWorkState =CardWorkState.Runing;

			Card.CompressRatio(0); // 设置声音压缩比例。
			Log.DefaultLogWriteLineInfo("语音卡初始化成功!Serial:"+Card.Serial);
		}
		/// <summary>
		/// 停止语音卡工作
		/// </summary>		
		public static void Disable()
		{
			
			
			// 给值
			Card.ChCount=0;
			Card.HisCHs=null;
			
			 

			Card.HisCardWorkState=CardWorkState.Stop;

			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					pcmn7api.PCM7_Disable();
					break;
				case CardType.Usbid:
					pcmn7api.TV_Disable();
					break;
				default:					
					return;
			}
		}
		#endregion

		#region 通道控制
		public static CH GetCallOutChn()
		{
			
			foreach(CH ch in Card.HisCHs)
			{
				if (ch.CHState==CHState.HangUp)
				{
					if (ch.GetCONNECT_STATUS()==2)
					{
						/* 如果是空闲的状态　*/
						return ch;
					}
					else
					{
						continue;
						//throw  new Exception("ch state error "+ch.GetCONNECT_STATUS());
					}					
				}
			}
			return null;
		}

		/// <summary>
		/// 得到一个空闲的通到
		/// </summary>
		/// <returns>一个空闲的通道</returns>
		public static CH GetHangUpCH_del()
		{
			
//			switch(Card.HisCardType)
//			{
//				case CardType.pcmn7api:
//					return pcmn7api.PCM7_GetCallOutChn();
//				case CardType.Usbid:
//					return pcmn7api.TV_GetCallOutChn();					
//					break;
//				default:
//					throw new Exception("unknow card type");
//			}
//			return ;
			int i=0;

			if (i==0)
				return null;
			else 
			{
				foreach(CH ch in Card.HisCHs)
				{
					if (ch.No==i)
					{
						if (ch.CHState==CHState.HangUp)
							return ch;
						else
						{
							Log.DebugWriteError("error ch num");
							return ch;
						}
					}						
				}
			}
			throw new Exception("dssssssssssss");

		}
		#endregion

		#region 呼出
		/// <summary>
		/// 执行暂停
		/// </summary>
		public static void DoStop()
		{
			if (HisCardWorkState==CardWorkState.Pause
				|| HisCardWorkState==CardWorkState.Runing)
			{			
				HisCardWorkState =CardWorkState.Stop;
				DoCallRemainder(); // 呼出剩余的
			}
		}
		/// <summary>
		/// 执行暂停
		/// </summary>
		public static void DoPause()
		{
			HisCardWorkState =CardWorkState.Pause;
			DoCallRemainder();
		}
		/// <summary>
		/// 呼出
		/// </summary>
		public static void DoCall()
		{	

			
			Card.Initialize(); // 初始化卡子.
			//进入工作状态
			while(true)
			{
				if (Card.HisCardWorkState ==CardWorkState.Pause)
				{					
					DoCallRemainder(); //执行呼出剩余的．
				}
				while (Card.HisCardWorkState ==CardWorkState.Pause)
				{
					/* 如果是暂停状态，就在这里循环。*/
					DoCallRemainder(); //执行呼出剩余的．	
					System.Threading.Thread.Sleep(1000); // 避免过多的浪费资源，在这里休眠。
				}
				if (Card.HisCardWorkState ==CardWorkState.Stop)
				{
					/* 如果是停止控制 */
					DoCallRemainder(); //执行呼出剩余的．
					break;
				}

				CH ch =Card.GetCallOutChn(); //取出一个空闲的通道.
				if (ch!=null)
				{
					/* 如果有空闲的通道 */
					CallList cl= CallLists.GetCall(); // 得到一个呼出。
					if (cl==null)
					{
						/*  如果没有可以呼出的电话 */
						DoCallRemainder(); //执行呼出剩余的．
						//System.Threading.Thread.Sleep(100); // 停止30秒,让其在判断是否有可以呼出的信息。
					}
					else
					{
						cl.CallingStateOfEnum=CallingState.Calling;
						cl.Update(); // 避免下次再取出他.
						ch.Call(cl); // 呼出他.
						Log.DebugWriteInfo("使用CH="+ch.No+"呼出Tel="+cl.Tel);
					}
				}
				// 执行呼出工作.
				Card.CallOn();
			}

			// 在跳出来后,呼出正在呼出的数据.			
			Card.HisCardWorkState=CardWorkState.Stop;
			Card.Disable(); // 停止板卡的工作.
		}
		/// <summary>
		/// 运行
		/// </summary>
		public static void Run()
		{
			Log.DefaultLogWriteLineInfo("******before run "+HisCardWorkState.ToString());
			while(true)
			{
				if (HisCardWorkState==CardWorkState.Runing)
				{
					/* 如果当前的状态是运行的. */
					DoCall();
				}				
				System.Threading.Thread.Sleep(5000);
				//Log.DefaultLogWriteLineInfo("************* end run");

			}
			Log.DefaultLogWriteLineInfo("************* end run");
		}
		/// <summary>
		/// 执行呼出剩余的
		/// </summary>
		public static void DoCallRemainder()
		{
			while(true)
			{
				Card.CallOn();
				if (Card.IsAllCHFree) //如果都是空闲的就跳出来.
					break;
			}
		}
		public static bool IsAllCHFree
		{
			get
			{
				foreach(CH ch in Card.HisCHs)
				{
					if (ch.CHState==CHState.Connect || ch.CHState==CHState.Play)
						return false;
				}
				return true;
			}
		}
		/// <summary>
		/// 让Card工作起来
		/// </summary>
		public static void CallOn()
		{
			foreach(CH ch in Card.HisCHs)
				ch.CallOn();
		}
		/// <summary>
		/// 停止
		/// </summary>
		public static void Stop()
		{
			Card.HisCardWorkState=CardWorkState.Stop;
		}
		/// <summary>
		/// 暂停
		/// </summary>
		public static void Pause()
		{
			Card.HisCardWorkState=CardWorkState.Pause;
		}		 
		#endregion

	}

}