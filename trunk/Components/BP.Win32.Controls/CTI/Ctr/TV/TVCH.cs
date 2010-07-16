using System;
using System.Collections;
using BP.CTI.App;
using BP.DA;

namespace BP.CTI
{
	 

	/// <summary>
	/// 通道状态
	/// </summary>
	public enum TVCHState
	{
		/// <summary>
		/// 挂机
		/// </summary>
		HangUp,
		/// <summary>
		/// 检测拨号音
		/// </summary>
		CheckSign,
		/// <summary>
		/// 等待外拨号音
		/// </summary>
		Waiting,
		/// <summary>
		/// 拨号
		/// </summary>
		Dialing,
		/// <summary>
		/// 连接
		/// </summary>
		Connect,
		/// <summary>
		/// 播放
		/// </summary>
		Play
	}
	/// <summary>
	/// TVCH 的摘要说明。
	/// </summary>
	public class TVCH:CH
	{
		#region 通到属性
		/// <summary>
		/// 通道状态
		/// </summary>
		public TVCHState CHState=TVCHState.HangUp;
		 
		#endregion

		#region 构造
		/// <summary>
		/// 建立
		/// </summary>
		public TVCH()
		{
		}
		/// <summary>
		/// 建立
		/// </summary>
		/// <param name="no">编号</param>
		public TVCH(int no)
		{
			
			this.No=no;
		}
		#endregion

		#region 通道函数

		 
		 
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
		/// 挂机
		/// </summary>
		protected void HangUpCtrl()
		{

			this.CHState=TVCHState.HangUp;

			pcmn7api.TV_HangUpCtrl (this.No);
 
		}

		#region 通到方法
	 
		  
		/// <summary>
		/// 连接一个电话
		/// </summary>
		/// <param name="tel">电话</param>		
		/// <returns></returns>
		public  override void  CallOut()
		{
			pcmn7api.TV_OffHookCtrl(this.No); // 控制它摘机.

			this.CHState=TVCHState.CheckSign ; // 进入拨号状态.

			//this.CHState=TVCHState.CheckSign ; // 检查拨号音.
			pcmn7api.TV_StartTimer(this.No,4); // 设置拨号的长度..
			this.CallOn();
		}
		 
		public override void CallOn()
		{
			switch(this.CHState )
			{
				case BP.CTI.TVCHState.HangUp: //如果是挂机状态。
					return ;

				case BP.CTI.TVCHState.CheckSign: //如果是检查外拨号音
					if ( pcmn7api.TV_TimerElapsed(this.No) < 0 )
					{
						/* 如果检测拨号音超时 */
						pcmn7api.TV_HangUpCtrl(this.No); // 挂机
						pcmn7api.TV_StartTimer(this.No,4); // 设置下次检测的时间段。
						this.CHState=TVCHState.Waiting; // 设置状态为等待下次拨号.
					}
					 
					// 判断是否有外拨号音
					int sigpara1=0,sigpara2=0;
					if (Usbid.TV_CheckSignal(this.No, ref sigpara1,ref sigpara2) ==TVCard.SIG_DIAL)
					{
						/* 如果检测出有信号, 就开始执行外拨.*/
						Log.DebugWriteInfo("已经获取拨号音，开始拨号. ");
						pcmn7api.TV_StartTimer(this.No,4); // 设置拨号时间长度。
						Usbid.TV_StartDial(this.No, DataType.StringToByte(this.HisCallList.Tel) ); //开始拨号.
						this.CHState=TVCHState.Dialing ; // 进入拨号状态.
					}
					break ;
				case BP.CTI.TVCHState.Waiting: //如果是等待外拨号音。
					if ( pcmn7api.TV_TimerElapsed(this.No) < 0 )
					{
						/*如果等待到了设置的时间， 再次检测外拨号音 */
						pcmn7api.TV_OffHookCtrl(this.No); // 控制它摘机.
						this.CHState=TVCHState.CheckSign ; // 检查拨号音.
						pcmn7api.TV_StartTimer(this.No,4); // 设置拨号的长度..
					}
					return ;
				case BP.CTI.TVCHState.Dialing:  // 正在拨号的状态。

					if (pcmn7api.TV_DialRest(this.No) <= 0)
					{
						pcmn7api.TV_StopDial(this.No); //停止拨号
						this.CHState=TVCHState.Connect ; // 进入连接状态.
						pcmn7api.TV_StartMonitor(this.No); //设置监听。

						pcmn7api.TV_StartTimer(this.No,this.RingLong); //对方接电话的时间。
						Log.DebugWriteInfo("拨号完毕,监听与时间都已经设置,等待用户接听 TVCH="+this.No+" pcmn7api.TV_DialRest(this.No) "+pcmn7api.TV_DialRest(this.No)+"Tel="+this.HisCallList.Tel);
					}
					if ( pcmn7api.TV_TimerElapsed(this.No) < 0 )
					{
						/* 如果拨号超出了指定的时间 */
						this.CallOut();
					}					  
					break; 
				case BP.CTI.TVCHState.Connect:  // 播完号码后连接状态。

					//Log.DebugWriteInfo("Usbid.TV_ListenerOffHook (this.No):=" +Usbid.TV_ListenerOffHook (this.No) );

					if ( Usbid.TV_ListenerOffHook (this.No) != 0 )
					{
						/* 如果对方摘机 */
						Log.DebugWriteInfo("对方接受者开始接听.TV_ListenerOffHook 开始播放语音文件" );

						/* 检测对方是否摘机 0 ： 被呼叫方没有摘机; 非 0 ： 被呼叫方已经摘机 						 
						 * 如果摘机进入 play 状态.
						 * */
						int bys = StartPlay(0);  //开始播放第一个文件.
						if ( bys > 0)
						{
							this.CHState = TVCHState.Play; //进入放音状态。
							break;
						}
						else
						{
							/* 如果放音失败 */
							//							this.HisCallList.Note="放音失败"+this.No;
							//							this.HisCallList.CallDegree=this.HisCallList.CallDegree+1;
							//							this.HisCallList.CallingStateOfEnum=CallingState.Init;

							this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
								CallListAttr.CallingState,(int)CallingState.Init,
								"Note","放音失败"+this.No);

							this.HangUpCtrl(); // 挂机。
							this.CHState = TVCHState.HangUp; //设置挂机状态.
							return;
						}
					}

					if (!TVCard.ReverseFlag) 
					{
						/* 如果是极性反转 */
						if (Usbid.TV_MonitorOffHook(this.No, 25) != 0 )	/* 1 Second */
						{
							/* 1, 这里对手机判断是否接听错误。
							 * 大约在播完号3秒后，对方没有接听，他也认为接听了。
							 * 
							 * 2, 对于空号判断错误,呼出一个不存在的电话号码.大约过了3-4秒后,判断是接听,
							 * 就执行了播放文件.实际上它是根本不能呼出的电话号码.
							 * 
							 * */
							Log.DebugWriteInfo("对方接受者开始接听.TV_MonitorOffHook., 开始播放语音文件" );

							int bys = StartPlay(0);  //开始播放第一个文件.
							if ( bys > 0)
							{
								this.CHState = TVCHState.Play; //进入放音状态。
								break;
							}
							else
							{
								/* 如果放音失败 */
								
								this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
									CallListAttr.CallingState,(int)CallingState.Init,
									"Note","放音失败"+this.No);


								this.HangUpCtrl(); // 挂机。
								this.CHState = TVCHState.HangUp; //设置挂机状态.
								return;
							}
						}
						//Log.DebugWriteInfo("TV_MonitorOffHook (ch, 25)="+Usbid.TV_MonitorOffHook (ch, 25));
					}

					// 检测忙音
					int	Sig=0, SigCount=0, SigLen=0;
					Sig = Usbid.TV_CheckSignal (this.No, ref SigCount, ref SigLen);

					//Log.DebugWriteInfo("Sig="+Sig+",SigCount="+SigCount+",SigLen="+SigLen+", TV_ListenerOffHook"+Usbid.TV_ListenerOffHook (this.No)+" TV_MonitorOffHook (Channel, 25)= "+Usbid.TV_MonitorOffHook (this.No, 25));
					if (  (Sig == 1 || Sig == 2  )  && SigCount >= 3  ) 
					{
						/* 如果检测对方忙音   */
					 						
						this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
							CallListAttr.CallingState,(int)CallingState.Init,
							"Note","对方忙,可能是呼叫时间过长"+this.No);
 
						Usbid.TV_HangUpCtrl (this.No); // 挂机
						this.CHState=TVCHState.HangUp; // 设置
						Log.DebugWriteInfo("对方忙音");
						break;
					}
					
					// 检测超时
					if (Usbid.TV_TimerElapsed (this.No) < 0)
					{
						this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
							CallListAttr.CallingState,(int)CallingState.Init,
							"Note","呼叫超时"+this.No);
 
						 
						Usbid.TV_HangUpCtrl (this.No); // 控制摘机
						this.CHState=TVCHState.HangUp; // 设置
						Log.DebugWriteInfo("呼叫超时");
						break;												 
					}					
					break;
				case BP.CTI.TVCHState.Play:
					int bye=this.PlayRest(); // 只有始终调用它才能播放出来.
					if ( bye < 0 )
					{
						/* 放音失败 */
						this.StopPlay(); //停止放音。
						this.HangUpCtrl(); //  控制挂机。
						this.CHState=TVCHState.HangUp;

						this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
							CallListAttr.CallingState,(int)CallingState.Error,
							"Note","语音文件播放失败"+this.No);
						return;
					}
					else if (bye==0 )
					{
						Log.DebugWriteInfo("播放OK..TVCH="+this.No+" Tel="+this.HisCallList.Tel);

						/* 放音完毕 */
						this.CHState=TVCHState.HangUp;
						pcmn7api.TV_HangUpCtrl(this.No); // 挂机	
						this.StopPlay();					 
						this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
							CallListAttr.CallingState,(int)CallingState.OK,
							"Note","成功完全呼出,声音完全播放完毕ch="+this.No+"");
						return;
					}
					else if (bye>0)
					{
						/*正常播放状态就不处理.*/
					}

					if (pcmn7api.TV_MonitorBusy (this.No, 1, 5)!=0
						|| pcmn7api.TV_MonitorBusy (this.No, 2, 5)!=0) 
					{
						
						Log.DebugWriteInfo("播放期间对方挂机,播放OK..TVCH="+this.No+" Tel="+this.HisCallList.Tel);

						/* 放音完毕 */
						this.CHState=TVCHState.HangUp;
						pcmn7api.TV_HangUpCtrl(this.No); // 挂机
						this.StopPlay();
						
						this.HisCallList.Update(CallListAttr.CallDegree,this.HisCallList.CallDegree+1,
							CallListAttr.CallingState,(int)CallingState.OK,
							"Note","播放期间对方挂机,播放OK"+this.No);
						return;
					}
					break;
				default:
					throw new Exception("stat error .");
			}
		}
		#endregion

	}
	/// <summary>
	/// 通道集合
	/// </summary>
	public class TVCHs: CollectionBase
	{
		
		/// <summary>
		/// 通道集合
		/// </summary>
		public TVCHs()
		{
		}
		/// <summary>
		/// 增加一个通道
		/// </summary>
		/// <param name="en"></param>
		public void Add(TVCH en)
		{
			this.InnerList.Add(en);
		}
		/// <summary>
		/// 根据位置取得数据
		/// </summary>
		public TVCH this[int index]
		{
			get 
			{
				return (TVCH)this.InnerList[index];
			}
		}
	}
}
