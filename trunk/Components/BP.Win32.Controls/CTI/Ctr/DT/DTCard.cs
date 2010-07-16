using System;
using System.Runtime.InteropServices;
using BP.CTI.App;
using BP.DA;


namespace BP.CTI
{ 
	/// <summary>
	/// 数字卡
	/// </summary>
	public class DTCard:Card
	{
		#region 系统属性 
		/// <summary>
		/// 是否全部的都是空闲的
		/// </summary>
		public static bool IsAllCHFree
		{
			get
			{
				foreach(DTCH ch in DTCard.HisCHs)
				{
					if (ch.CHState==DTCHState.Connect || ch.CHState==DTCHState.Play)
						return false;
				}
				return true;
			}
		}
		
		public static DTCHs HisCHs=null;

		#region count
		/// <summary>
		/// 得到ch状态的个.
		/// </summary>
		/// <param name="state">CHState</param>
		/// <returns>num</returns>
		public static int GetCHsCount(DTCHState state)
		{
			if (HisCHs==null)
				return 0;
			int i=0;
			foreach(DTCH ch in DTCard.HisCHs)
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
			Card.beforeInitialize(); 

			 
			Card.ChCount=pcmn7api.PCM7_Installed();
			 

			if ( Card.ChCount <= 0)
				return;
			//throw new Exception("未安装驱动!!!");

			 
			// 说明启动成功。
			int i =pcmn7api.PCM7_Initialize();				
		 

			if (i < 0)
			{
				string msg="错误Initialize errornum = "+i.ToString();
				Log.DefaultLogWriteLineError(msg);
				Card.ChCount=0;
				throw new Exception(msg);
			}			
		 
			DTCard.HisCHs = new DTCHs();	 

			// 必须从1开始使用.
			for(int ch=0; ch < Card.ChCount-1; ch++)
			{
				if (ch==32 || ch==0 || ch==1 )
					continue;
				DTCard.HisCHs.Add(new DTCH(ch));
			}

			Card.HisCardWorkState =CardWorkState.Runing;

			// 数字卡与模拟卡都用同一种
			Card.CompressRatio(0); // 设置声音压缩比例。

			Log.DefaultLogWriteLineInfo("语音卡初始化成功!Serial:"+Card.Serial);
		}
		 
		#endregion

		#region 通道控制
		public static DTCH GetCallOutChn()
		{
			
			foreach(DTCH ch in DTCard.HisCHs)
			{
				if (ch.CHState==DTCHState.HangUp)
					return ch;
			}
			return null;
		}

		#endregion

		#region 呼出
		public static void DoCall(int chNum, CallList cl)
		{	
			Initialize(); // 初始化卡子.

			CH ch =new CH(chNum);
			ch.Call(cl);  // 呼出他.

			DoCallRemainder();

			Card.Disable();
		}

		  
		/// <summary>
		/// 呼出
		/// </summary>
		public static void DoCall()
		{	
			Initialize(); // 初始化卡子.

			//进入工作状态
			while(true)
			{
				if (Card.dtOfContext==null)
				{
					Card.GetCurrentContextByTelType(0);
				}
				if ( Card.dtOfContext.Rows.Count <=0 )
				{
					/* 没有当前可以呼出的内容。*/
					DoCallRemainder(); //执行呼出剩余的．
					System.Threading.Thread.Sleep(30000); // 避免过多的浪费资源，在这里休眠。
					continue;
				}
				if (Card.HisCardWorkState ==CardWorkState.Pause)
				{
					DoCallRemainder(); //执行呼出剩余的．
				}
				while (Card.HisCardWorkState ==CardWorkState.Pause)
				{
					/* 如果是暂停状态，就在这里循环。*/
					DoCallRemainder(); //执行呼出剩余的．	
					System.Threading.Thread.Sleep(10); // 避免过多的浪费资源，在这里休眠。
				}
				if (Card.HisCardWorkState ==CardWorkState.Stop)
				{
					/* 如果是停止控制 */
					DoCallRemainder(); //执行呼出剩余的．
					break;
				}

				DTCH ch = DTCard.GetCallOutChn(); //取出一个空闲的通道.
				if (ch!=null)
				{
					/* 如果有空闲的通道 */
					CallList cl= CallLists.GetCall(); // 得到一个呼出。
					if (cl==null)
					{
						/*  如果没有可以呼出的电话 */
						Card.CallOn();
						//DoCallRemainder(); //执行呼出剩余的．
						//System.Threading.Thread.Sleep(10); // 停止1秒,让其在判断是否有可以呼出的信息。
					}
					else
					{
						//cl.DoCalling(Card.CurrentCallContext.Context);  // 设置状态为呼出装态.
						ch.Call(cl); // 呼出他...
					}
				}
				// 执行呼出工作.
				Card.CallOn();
			}

			// 在跳出来后,呼出正在呼出的数据.			
			Card.HisCardWorkState=CardWorkState.Stop;
			Card.Disable(); // 停止板卡的工作.
		}
		 
		 
	   
		#endregion

	}

}