using System;
using System.Runtime.InteropServices;
using BP.CTI.App;
using BP.DA;


namespace BP.CTI
{
	/// <summary>
	/// 函数
	/// </summary>
	public class TVCard : Card
	{
		/// <summary>
		/// 是否申请了极性反转
		/// 一般的都没有申请他。
		/// </summary>
		public static bool  ReverseFlag=false;
		/// <summary>
		/// /
		/// </summary>
		public static int  SIG_SILENCE=64;
		/// <summary>
		/// 外拨号音
		/// </summary>
		public static int  SIG_DIAL=65;
		/// <summary>
		/// 是否全部的都是空闲的
		/// </summary>
		public static bool IsAllCHFree
		{
			get
			{
				foreach(TVCH ch in TVCard.HisCHs)
				{
					if ( ch.CHState==TVCHState.Dialing)
						return false;
					if ( ch.CHState==TVCHState.Connect)
						return false;
					if ( ch.CHState==TVCHState.Play)
						return false;
				}
				return true;
			}
		}

		#region 系统属性
		 
		/// <summary>
		/// 通道
		/// </summary>
		public static TVCHs HisCHs=null;
		
		#region count
		/// <summary>
		/// 得到ch状态的个.
		/// </summary>
		/// <param name="state">CHState</param>
		/// <returns>num</returns>
		public static int GetCHsCount(TVCHState state)
		{
			if (HisCHs==null)
				return 0;
			int i=0;
			foreach(TVCH ch in HisCHs)
			{
				if (ch.CHState==state)
					i++;
			}
			return i;
		}
		#endregion
		 
		#endregion

		#region 控制方法	
		
		 
		/// <summary>
		/// 出使化语音卡.
		/// </summary>
		/// <returns></returns>
		public static void Initialize()
		{
			Card.beforeInitialize(); //调用基类初始化

			Card.ChCount=pcmn7api.TV_Installed();

			if ( Card.ChCount <= 0)
			{
				throw new Exception("未安装驱动!!!");
			}

			 
			if (pcmn7api.TV_Initialize(0) < 0)
			{
				string msg="错误TV_Initialize errornum = "+pcmn7api.TV_Initialize(0);
				Log.DefaultLogWriteLineError(msg);
				Card.ChCount=0;
				throw new Exception(msg);
			}

			
			// init chs.
			TVCard.HisCHs = new TVCHs();			 

			for(int i=0;  i<  Card.ChCount; i++ )
			{
				TVCard.HisCHs.Add(new TVCH( i ));
			}

			Card.HisCardWorkState =CardWorkState.Runing;


			// 数字卡与模拟卡都用同一种
			Card.CompressRatio(0); // 设置声音压缩比例。

			//  检查是否收录了此板卡ＩＤ

//			if (Card.MySerial.IndexOf( Card.Serial )==-1)
//			{
//				//TVCard.HisCHs =null;
//				Log.DefaultLogWriteLineInfo("语音卡错误"+Card.Serial);
//			}

			Log.DefaultLogWriteLineInfo("语音卡初始化成功!Serial:"+Card.Serial);

		 




			
		}
		
		#endregion

	 
		public static TVCH GetHangUpCH()
		{
			foreach(TVCH ch in TVCard.HisCHs )
			{
				if (ch.CHState==TVCHState.HangUp)
					return ch;
			}
			return null;
		}
		#region 呼出
		public static void DoCall(int chNum, CallList cl)
		{	
			Initialize(); // 初始化卡子.

			CH ch =new CH(chNum);
			ch.Call(cl); // 呼出他.

			DoCallRemainder();

			Card.Disable();
		}
	  
		/// <summary>
		/// 呼出
		/// </summary>
		public static void DoCall()
		{	
			TVCard.Initialize(); // 初始化卡子.

		   Card.GetCurrentContextByTelType(0); //  GetCurrentContextByTelType(int teltype)

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

				TVCH ch =TVCard.GetHangUpCH(); //取出一个空闲的通道.
				if (ch!=null)
				{
					/* 如果有空闲的通道 */
					CallList cl= CallLists.GetCall(); // 得到一个呼出。
					if (cl==null)
					{
						/*  如果没有可以呼出的电话 */
						DoCallRemainder(); //执行呼出剩余的．
						System.Threading.Thread.Sleep(3000); // 停止1秒,让其在判断是否有可以呼出的信息。
					}
					else
					{
						//cl.DoCalling(Card.CurrentCallContext.Context);  // 设置状态为呼出装态.
						ch.Call(cl); // 呼出他.
						//Log.DebugWriteInfo("使用CH="+ch.No+"呼出Tel="+cl.Tel);
					}
				}
				// 执行呼出工作.
				TVCard.CallOn();
			}

			// 在跳出来后,呼出正在呼出的数据.			
			Card.HisCardWorkState=CardWorkState.Stop;
			Card.Disable(); // 停止板卡的工作.
		}
		
		#endregion

	}

}