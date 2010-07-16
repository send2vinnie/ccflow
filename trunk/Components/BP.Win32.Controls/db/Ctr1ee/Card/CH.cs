using System;
using System.Collections;
using BP.CTI.App;
using BP.DA;

namespace BP.CTI
{
	 

	/// <summary>
	/// 通道状态
	/// </summary>
	public enum CHState
	{
		/// <summary>
		/// 挂机
		/// </summary>
		HangUp,
		/// <summary>
		/// 连接
		/// </summary>
		Connect,
		/// <summary>
		/// 播放
		/// </summary>
		Play,
		/// <summary>
		/// 拨号中(对于数字卡没有意义.)
		/// </summary>
		Dialing,
	}
	/// <summary>
	/// CH 的摘要说明。
	/// </summary>
	public class CH
	{
		#region 通到属性
		/// <summary>
		/// 通道状态
		/// </summary>
		public CHState CHState=CHState.HangUp;
		/// <summary>
		/// 通道编号
		/// </summary>
		public int No=-1;
		/// <summary>
		/// 他的呼出事例
		/// </summary>
		public CallList HisCallList=null;
		#endregion

		#region 构造
		/// <summary>
		/// 建立
		/// </summary>
		public CH()
		{
		}
		/// <summary>
		/// 建立
		/// </summary>
		/// <param name="no">编号</param>
		public CH(int no)
		{
			
			this.No=no;
		}
		#endregion

		#region 通道函数

		#region 时间涵数		
		/// <summary>
		/// 开始计时
		/// </summary>
		/// <param name="mm">要计算的时间</param>
		protected void StartTimer(int mm)
		{
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					pcmn7api.PCM7_StartTimer(this.No,mm);
					break;
				case CardType.Usbid:
					pcmn7api.TV_StartTimer(this.No,mm);
					break;
				default:
					throw new Exception("un know card type");
			}
		}
		/// <summary>
		/// 返回是否到了时间
		/// </summary>
		/// <returns></returns>
		protected bool TimerElapsed()
		{
			int i = -10;				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					i= pcmn7api.PCM7_TimerElapsed(this.No);
					Log.DebugWriteInfo("pcmn7api.PCM7_TimerElapsed(this.No)="+pcmn7api.PCM7_TimerElapsed(this.No) );
					 
					break;
				case CardType.Usbid:
					i= pcmn7api.TV_TimerElapsed(this.No);
					if (i<=0)
						return true;
					else
						return false;
					//Log.DebugWriteInfo("pcmn7api.TV_TimerElapsed(this.No)="+pcmn7api.TV_TimerElapsed(this.No) );
				default:
					throw new Exception("unknow card type");
			}
			if  (i >= this.RingLong )
				return true;
			else
				return false;
		}
		#endregion

		/// <summary>
		/// 连接一个电话
		/// </summary>
		/// <param name="tel">电话</param>		
		/// <returns></returns>
		protected int CallOut(string tel)
		{
			// 开始建立一个通道
			int i = -10;				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					i= pcmn7api.PCM7_StartCallOut(this.No, DataType.StringToByte(tel), DataType.StringToByte(Card.DefaultCalledTel) ) ; 				 
					this.CHState=CHState.Connect; // 设置他的状态, 为正在连接状态.
					this.StartTimer(500000); // 开始连接计算时间.
					break;
				case CardType.Usbid:
					this.StartTimer(500000); // 开始连接计算时间.

					pcmn7api.TV_HangUpCtrl(this.No); // 这边挂机.

					pcmn7api.TV_OffHookCtrl(this.No); // 呼叫方 摘机.
					Log.DebugWriteInfo("开始拨号 ch="+this.No+" Tel="+this.HisCallList.Tel);
					i= pcmn7api.TV_StartDial(this.No, DataType.StringToByte(tel) ) ; // 开始播出这个电话号码.
					this.CHState=CHState.Dialing ; // 当前的状态拨号中.
					Log.DebugWriteInfo("进入拨号状态 ch="+this.No+" Tel="+this.HisCallList.Tel);
					break;
				default:
					throw new Exception("unknow card type");
			}
 
			return i;
		}
		/// <summary>
		/// 检测对方是否挂机
		/// 包括对方占线，空号等
		/// </summary>
		/// <returns>0,1</returns>
		protected bool HangUpDetect()
		{
			int i = -10;				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					if (pcmn7api.PCM7_HangUpDetect(this.No)==1)
						return true;
					else
						return false;
					 
				case CardType.Usbid:
					if (pcmn7api.TV_MonitorBusy(this.No, 1, 5) ==0
						|| pcmn7api.TV_MonitorBusy (this.No, 2, 5)==0 ) 
						return false;
					else
						return true;
				default:
					throw new Exception("unknow card type");
			}
			 
//			if (i==1)
//				return true;
//			else if (i==0)
//				return false;
//			else
//			{
//				Log.DefaultLogWriteLineError("PCM7_HangUpDetect error num"+i);
//				return true;
//				//throw new Exception("PCM7_HangUpDetect error num"+i);
//			}
		}
		/// <summary>
		/// 获取对方挂机原因
		/// </summary>
		/// <returns></returns>
		public int GetHangUpReason()
		{
			 				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					return pcmn7api.PCM7_GetHangUpReason(this.No);					 
				case CardType.Usbid:
					return pcmn7api.TV_GetHangUpReason(this.No);					 
				default:
					throw new Exception("unknow card type");
			}

		 
		}
		/// <summary>
		/// 挂机原因
		/// </summary>
		/// <returns>文字描述的原因</returns>
		public string GetHangUpReasonStr_del()
		{
			return this.GetHangUpReasonStr(pcmn7api.PCM7_GetHangUpReason(this.No));
		}
		/// <summary>
		/// 取出挂机原因的字串.
		/// </summary>
		/// <param name="i">挂机编号.</param>
		/// <returns>文字描述的原因</returns>
		public string GetHangUpReasonStr(int i)
		{
			switch(i)
			{
				case 21: //0x15
					return "交换设备拥塞信号。编号=21";
				case 37: // 0x25
					return "电路群拥塞信号。编号=37";
				case 53: //0x35
					return "国内网拥塞信号。编号=53";
				case 69: //0x45
					return "地址不全信号。编号=69";
				case 85: //0x55
					return "呼叫失败信号。编号=85";
				case 101: //0x65
					return "用户忙信号(电的)。编号=101";
				case 117: //0x75
					return "空号信号。编号=117";
				case 133: //0x85
					return "线路不工作信号。编号=133";
				case 149: //0x95
					return "发送专用信息音信号。编号=149";
				case 165: //0xa5
					return "接入拒绝信号。编号=165";
				case 181: //0xb5
					return "不提供数字通路信号。编号=181";
				case 197: //0xc5 
					return "误拨中继前缀。编号=197";
				default:
					return "没有此错误编号的信息"+i.ToString();
			}
		}
		/// <summary>
		/// 重新调整
		/// </summary>
		/// <returns>播放的字节</returns>
		public int PlayRest()
		{
			int i = -10;				
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					i= pcmn7api.PCM7_PlayFileRest(this.No);					 
					break;
				case CardType.Usbid:
					i= pcmn7api.TV_PlayFileRest(this.No);					 
					break;
				default:
					throw new Exception("unknow card type");
			}
			 

			if( i==0 )
			{
				/* 如果当前的文件已经播放完毕, 开始播放下一个文件 */
				this.CurrPlayIndex++;
				if (this.Playfiles.Length <= this.CurrPlayIndex)
				{
					/*如果到了最后一个文件。*/
					return 0;
				}
				else
				{

					/*播放下一个文件*/
					return this.StartPlay( this.CurrPlayIndex);
				}
			}
			else
			{
				return i;
			}
		}

			
		/// <summary>
		/// 挂机
		/// </summary>
		protected void HangUpCtrl()
		{

			this.CHState=CHState.HangUp;

			int i = -10;
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					i= pcmn7api.PCM7_HangUpCtrl(this.No);					 
					break;
				case CardType.Usbid:
					pcmn7api.TV_HangUpCtrl(this.No);
					//pcmn7api.TV_StopDial(this.No);
					//pcmn7api.TV_OffHookCtrl(this.No);
					break;
				default:
					throw new Exception("unknow card type");
			}
 
		}
		/// <summary>
		/// 停止播放文件
		/// </summary>
		public double StopPlay()
		{
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					return pcmn7api.PCM7_StopPlayFile(this.No);					 
				case CardType.Usbid:
					return pcmn7api.TV_StopPlayFile(this.No);
				default:
					throw new Exception("unknow card type");
			}
		}
		/// <summary>
		/// 开始播放一个文件
		/// </summary>
		/// <param name="fileIndex">文件</param>
		/// <returns></returns>
		protected int StartPlay(int fileIndex)
		{
			Log.DebugWriteInfo("开始播放文件:"+this.Playfiles[fileIndex] +" ch="+this.No);
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					return pcmn7api.PCM7_StartPlayFile(this.No,DataType.StringToByte(this.Playfiles[fileIndex]),0,0);					 
				case CardType.Usbid:
					return pcmn7api.TV_StartPlayFile(this.No,DataType.StringToByte(this.Playfiles[fileIndex]),0,0);					 
				default:
					throw new Exception("unknow card type");
			}
		}
		/// <summary>
		/// 连接状态
		/// </summary>
		/// <returns></returns>
		public int GetCONNECT_STATUS()
		{
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api: 
					return pcmn7api.PCM7_CallOutStatus(this.No);
				case CardType.Usbid:
					return pcmn7api.TV_CallOutStatus(this.No); 
				default:
					throw new Exception("unknow card type");
			}
		}		 
		#endregion

		#region 播放文件
		/// <summary>
		/// 播放的文件s
		/// </summary>
		public string[] Playfiles=null;
		/// <summary>
		/// 当前文件的Index
		/// </summary>
		public int CurrPlayIndex=0;
		#endregion

		

		#region 通到方法
		/// <summary>
		/// 最大连接长度.默 50 
		/// </summary>
		public int RingLong=50;
		//public string[] 
		/// <summary>
		/// 呼出一个实例
		/// </summary>
		/// <param name="cl">实例</param>
		/// <returns></returns>
		public void Call(CallList cl)
		{
			// set call list
			this.HisCallList = cl;

			// ch, 属性
			this.CurrPlayIndex=0;
			// play files
			string context =Card.CurrentCallContext.Context;
			if (context.IndexOf(",")!=-1)
			{
				/*说明是多文件播放*/
				context=context.Replace("@YF@", "D"+DateTime.Now.AddMonths(-1).Month+".TW");
				context=context.Replace("@Tel@", this.HisCallList.TelOfFile);
				context=context.Replace("@JE@", this.HisCallList.JEOfFile);
			}
			context=context.Replace(",,", ",");
			Log.DebugWriteInfo("Will call fils ="+context);

			this.RingLong=cl.HisTelType.RingLong;
			this.Playfiles=context.Split(',');
			this.CallOut(cl.Tel);  // 用这个电话开始建立连接.
			this.CallOn(); // 呼出他.
		}
		public void CallOn()
		{
			if (this.CHState==CHState.HangUp)
				return;

			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					this.CallOnOfPCM();
					break;
				case CardType.Usbid:
					this.CallOnOfTV();
					break;
				default:
					break;
					
			}

		}
		/// <summary>
		/// 对方是否忙
		/// </summary>
		public bool IsBusyOfTV
		{
			get
			{
				if (pcmn7api.TV_MonitorBusy(this.No, 1, 5) >0) 
					return true;
				if ( pcmn7api.TV_MonitorBusy (this.No,2,5)>0)
					return true;
				return false;
			}
		}
		 
		private void CallOnOfTV()
		{
			switch(this.CHState)
			{
				case BP.CTI.CHState.Dialing:  // 正在拨号的状态。
					while (pcmn7api.TV_DialRest(this.No) > 0)
					{
						//Log.DebugWriteInfo("拨号ch="+this.No+" pcmn7api.TV_DialRest(this.No) "+pcmn7api.TV_DialRest(this.No)+"Tel="+this.HisCallList.Tel);
					}

					pcmn7api.TV_StopDial(this.No);

					Log.DebugWriteInfo("拨号完毕 ch="+this.No+" pcmn7api.TV_DialRest(this.No) "+pcmn7api.TV_DialRest(this.No)+"Tel="+this.HisCallList.Tel);

					CHState=CHState.Connect ; // 进入连接状态.

					pcmn7api.TV_StartTimer(this.No,40); //设置时间。
					pcmn7api.TV_StartMonitor(this.No); //设置监听。

					Log.DebugWriteInfo("监听与时间都已经设置,等待用户接听 ch="+this.No+" pcmn7api.TV_DialRest(this.No) "+pcmn7api.TV_DialRest(this.No)+"Tel="+this.HisCallList.Tel);
					break; 
				case BP.CTI.CHState.Connect:  // 播完号码后连接状态。

					int SigCount=0,SigLen=0;
					int Sig = pcmn7api.TV_CheckSignal (this.No, ref SigCount, ref SigLen);

					if (((Sig == 1 || Sig == 2) && SigCount >= 3) || pcmn7api.TV_TimerElapsed (this.No) < 0) 
					{
						Log.DebugWriteInfo("用户挂机");
						

						/* 如果放音失败 */
						this.HisCallList.Note="用户挂机"+this.No;
						this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;
						this.HisCallList.CallingStateOfEnum=CallingState.Init;
						this.HisCallList.Update();
						//this.HangUpCtrl(); // 挂机。
						this.CHState = CHState.HangUp; //设置挂机状态.
						pcmn7api.TV_HangUpCtrl (this.No);
						return;
					}

					if ( pcmn7api.TV_ListenerOffHook (this.No) !=0 )
					{
						int bys = StartPlay(0);  //开始播放第一个文件.
						if ( bys > 0)
						{
							this.CHState = CHState.Play; //进入放音状态。
							break;
						}
						else
						{
							/* 如果放音失败 */
							this.HisCallList.Note="放音失败"+this.No;
							this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;
							this.HisCallList.CallingStateOfEnum=CallingState.Init;
							this.HisCallList.Update();
							this.HangUpCtrl(); // 挂机。
							this.CHState = CHState.HangUp; //设置挂机状态.
							return;
						}
					}

					if (this.TimerElapsed())
					{
						Log.DebugWriteInfo("对方没有接听,呼叫超时 ch="+this.No+"    "+this.HisCallList.Tel);

						/* 如果到了规定的时间 */
						this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;
						this.HisCallList.CallingStateOfEnum=CallingState.Init;
						this.HisCallList.Note="呼叫超时"+this.No;
						this.HisCallList.Update(); //超时间就让下次再呼出他。
						//this.StopPlay();
						this.HangUpCtrl(); // 挂机。
						this.CHState = CHState.HangUp; // 挂起
						return;
					}

					
					break;
				case BP.CTI.CHState.Play:

					//Log.DebugWriteInfo("播放文件..ch="+this.No+" Tel="+this.HisCallList.Tel);

					int bye=this.PlayRest(); // 只有始终调用它才能播放出来.
					Log.DebugWriteInfo(" CHState.Play, ch="+this.No+" PlayRest="+bye);
					if ( bye < 0 )
					{
						/* 放音失败 */
						this.StopPlay(); //停止放音。
						this.HangUpCtrl(); //  控制挂机。
						this.CHState=CHState.HangUp;
						this.HisCallList.CallingStateOfEnum=CallingState.Error;
						this.HisCallList.Note="语音文件播放失败.";  //delete record. 
						this.HisCallList.Update();
						return;
					}
					else if (bye==0 )
					{
						Log.DebugWriteInfo("播放OK..ch="+this.No+" Tel="+this.HisCallList.Tel);

						/* 放音完毕 */
						this.CHState=CHState.HangUp;
						this.HisCallList.Note="成功完全呼出, 声音完全播放完毕。";  //delete record. 
						this.HisCallList.CallingStateOfEnum=CallingState.OK;						
						this.HisCallList.Update();

						this.HangUpCtrl(); //控制挂机。

						return;
					}
					else if (bye>0)
					{
						/*正常播放状态就不处理.*/
					}
					if (this.HangUpDetect()) 
					{  
						Log.DebugWriteInfo("HangUpDetect ch="+this.No );
						/* 检测对方是否挂机 */
						this.HisCallList.Note="对方挂机";
						this.HisCallList.CallingStateOfEnum=CallingState.OK;
						this.HisCallList.Update();
						this.StopPlay(); // 停止放音。
						this.HangUpCtrl(); // 挂机。
						this.CHState=CHState.HangUp;
						return ;
					}
					break;
			 
			}

		}
		/// <summary>
		/// 执行呼出.
		/// </summary>
		private void CallOnOfPCM()
		{
			 

			switch(this.CHState)
			{
				case BP.CTI.CHState.Connect:
					int state= this.GetCONNECT_STATUS();
					Log.DebugWriteInfo("Connect 1  ch="+this.No+" GetCONNECT_STATUS="+state);
					if (state==CONNECT_STATUS.SO_TALK )
					{
						Log.DebugWriteInfo("Connect 2 ch="+this.No+" GetCONNECT_STATUS="+state);

						/* 如果对方已经拿起了电话机，就在开始在这个通道里面放音 */
						//int bys = StartPlayFile(this.HisCallList.FK_ContextText);
						int bys = StartPlay(0);
						if ( bys > 0)
						{
							this.CHState = CHState.Play; //设置放音。
						}
						else
						{
							/* 如果放音失败 */
							this.HisCallList.Note="放音失败"+this.No;
							this.HisCallList.Update();
							this.HangUpCtrl(); // 挂机。
							this.CHState = CHState.HangUp; //设置挂机状态
							return;
						}
					}					 

					if (this.TimerElapsed())
					{
						/* 如果到了规定的时间 */
						this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;
						this.HisCallList.CallingStateOfEnum=CallingState.Init;
						this.HisCallList.Note="呼叫超时"+this.No;
						this.HisCallList.Update(); //超时间就让下次再呼出他。
						//this.StopPlay();
						this.HangUpCtrl(); // 挂机。
						this.CHState = CHState.HangUp; // w
						return;
					}

					if ( this.HangUpDetect() )
					{
						Log.DebugWriteInfo(" Connect 3  HangUpDetect ch="+this.No+"  GetCONNECT_STATUS="+state);

						/* 如果检查到,对方挂机, 号码不对. 包括对方占线，空号 */
						int reason=this.GetHangUpReason();
						this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;						
						this.HisCallList.Note="检测到对方挂机";
						this.HisCallList.CallingStateOfEnum=CallingState.Init;
						//this.HisCallList.CallingState=0;
						this.HisCallList.Update();
						this.StopPlay();
						this.HangUpCtrl(); //关闭通道.
						this.CHState=CHState.HangUp;
					}
					break;
				case BP.CTI.CHState.Play:

					int bye=this.PlayRest(); // 只有始终调用它才能播放出来.
					Log.DebugWriteInfo(" CHState.Play, ch="+this.No+" PlayRest="+bye);
					if ( bye < 0 )
					{
						/* 放音失败 */
						this.StopPlay(); //停止放音。
						this.HangUpCtrl(); //  控制挂机。
						this.CHState=CHState.HangUp;
						this.HisCallList.CallingStateOfEnum=CallingState.Error;
						this.HisCallList.Note="语音文件播放失败.";  //delete record. 
						this.HisCallList.Update();
						return;
					}
					else if (bye==0 )
					{
						/* 放音完毕 */
						this.CHState=CHState.HangUp;
						this.HangUpCtrl(); //控制挂机。
						this.HisCallList.Note="成功完全呼出, 声音完全播放完毕。";  //delete record. 
						this.HisCallList.CallingStateOfEnum=CallingState.OK;						
						this.HisCallList.Update();
						return;
					}
					else if (bye>0)
					{
						/*正常播放状态就不处理.*/
					}


					if (this.HangUpDetect()) 
					{  
						Log.DebugWriteInfo("HangUpDetect ch="+this.No+"  GetCONNECT_STATUS="+this.GetCONNECT_STATUS());

						/* 检测对方是否挂机 */
						this.HisCallList.Note="开始播放后,对方挂机原因:"+this.GetHangUpReason();
						this.HisCallList.CallingStateOfEnum=CallingState.OK;
						this.HisCallList.Update();
						this.StopPlay(); // 停止放音。
						this.HangUpCtrl(); // 挂机。
						this.CHState=CHState.HangUp;
						return ;
					}
					break;
			 
			}
		}
		#endregion

	}
	/// <summary>
	/// 通道集合
	/// </summary>
	public class CHs: CollectionBase
	{
		
		/// <summary>
		/// 通道集合
		/// </summary>
		public CHs()
		{
		}
		/// <summary>
		/// 增加一个通道
		/// </summary>
		/// <param name="en"></param>
		public void Add(CH en)
		{
			this.InnerList.Add(en);
		}
		/// <summary>
		/// 根据位置取得数据
		/// </summary>
		public CH this[int index]
		{
			get 
			{
				return (CH)this.InnerList[index];
			}
		}
	}
}
