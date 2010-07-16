using System;
using System.Collections;
using BP.CTI.App;
using BP.DA;

namespace BP.CTI
{
	 

	/// <summary>
	/// 通道状态
	/// </summary>
	public enum DTCHState
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
		Play
	}
	/// <summary>
	/// CH 的摘要说明。
	/// </summary>
	public class DTCH:CH
	{
		#region 通到属性
		/// <summary>
		/// 通道状态
		/// </summary>
		public DTCHState CHState=DTCHState.HangUp;
		#endregion

		#region 构造
		/// <summary>
		/// 建立
		/// </summary>
		public DTCH()
		{
		}
		/// <summary>
		/// 建立
		/// </summary>
		/// <param name="no">编号</param>
		public DTCH(int no)
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
			pcmn7api.PCM7_StartTimer(this.No,mm);
		}
		/// <summary>
		/// 返回是否到了时间
		/// </summary>
		/// <returns></returns>
		protected bool TimerElapsed()
		{
			if  ( pcmn7api.PCM7_TimerElapsed(this.No) >= this.RingLong )
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
		protected bool HangUpDetect_del()
		{
			if (pcmn7api.PCM7_HangUpDetect(this.No)==1)
				return true;
			else
				return false;
		}
		/// <summary>
		/// 连接一个电话
		/// </summary>
		/// <param name="tel">电话</param>		
		/// <returns></returns>
		public override void CallOut()
		{
			int i= pcmn7api.PCM7_StartCallOut(this.No, DataType.StringToByte(this.HisCallList.Tel), DataType.StringToByte(Card.DefaultCalledTel) ) ;
			this.CHState=DTCHState.Connect; // 设置他的状态, 为正在连接状态.
			pcmn7api.PCM7_StartTimer(this.No,300);
			//return i;
		}
		/// <summary>
		/// 获取对方挂机原因
		/// </summary>
		/// <returns></returns>
		public int GetHangUpReason()
		{
			return pcmn7api.PCM7_GetHangUpReason(this.No);					 
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
		/// 挂机
		/// </summary>
		protected void HangUpCtrl()
		{
			this.CHState=DTCHState.HangUp;
			pcmn7api.PCM7_HangUpCtrl(this.No);
		}
		#endregion

	 
		/// <summary>
		/// 执行呼出.
		/// </summary>
		public override  void CallOn()
		{
			switch(this.CHState)
			{
				case BP.CTI.DTCHState.HangUp:
					return;
				case BP.CTI.DTCHState.Connect:
					int state= pcmn7api.PCM7_CallOutStatus(this.No);
					if (state==CONNECT_STATUS.SO_TALK )
					{
						//Log.DebugWriteInfo("Connect 2 ch="+this.No+" GetCONNECT_STATUS="+state);
						/* 如果对方已经拿起了电话机，就在开始在这个通道里面放音 */
						//int bys = StartPlayFile(this.HisCallList.FK_ContextText);
						int bys = StartPlay(0);
						if ( bys > 0)
							this.CHState = DTCHState.Play; //设置放音。
						else
						{
							/* 如果放音失败 */		
							this.HisCallList.DoInit("放音失败"+this.No);
							this.HangUpCtrl(); // 挂机。
							this.CHState = DTCHState.HangUp; //设置挂机状态.
							return;
						}
					}

					if (this.TimerElapsed())
					{
						/* 如果到了规定的时间 */
						this.HisCallList.DoTimeOut("呼叫超时"+this.No);
						this.HangUpCtrl(); // 挂机。
						this.CHState = DTCHState.HangUp; // 
						return;
					}

					if ( pcmn7api.PCM7_HangUpDetect(this.No)==1 ) 
					{
						/*如果对方挂机 1     对方挂机   0     对方未挂机.*/
						Log.DebugWriteInfo("检测到对方挂机 3  HangUpDetect ch="+this.No+"  GetCONNECT_STATUS="+state);
						/* 如果检查到,对方挂机, 号码不对. 包括对方占线，空号 */
						this.HisCallList.DoTimeOut("检测到对方挂机,或者空号码"+this.No);
						this.HangUpCtrl(); //关闭通道.
						this.CHState=DTCHState.HangUp;
					}
					break;
				case BP.CTI.DTCHState.Play:
					int bye=this.PlayRest(); // 只有始终调用它才能播放出来.
					if ( bye < 0 )
					{
						/* 放音失败 */
						this.StopPlay(); //停止放音。
						this.HangUpCtrl(); //  控制挂机。
						this.CHState=DTCHState.HangUp;
						this.HisCallList.DoError("文件播放失败");
						return;
					}
					else if (bye==0 )
					{
						/* 放音完毕 */
						this.CHState=DTCHState.HangUp;
						this.StopPlay();
						this.HangUpCtrl(); //控制挂机。

						this.HisCallList.DoOK(); //播放完毕。
						return;
					}
					else if (bye>0)
					{
						/*正常播放状态就不处理.*/
						if ( pcmn7api.PCM7_HangUpDetect(this.No)==1 ) 
						{
							/*如果对方挂机 1     对方挂机   0     对方未挂机.*/
							//Log.DebugWriteInfo("HangUpDetect ch="+this.No+"， GetCONNECT_STATUS="+ pcmn7api.PCM7_CallOutStatus(this.No) + GetCONNECT_STATUS+" re ="+ pcmn7api.PCM7_GetHangUpReason(this.No));
							/* 检测对方是否挂机 */
							this.HisCallList.Update(CallListAttr.CallingState,(int)CallingState.OK,		
								CallListAttr.CallDate,DataType.CurrentData,
								CallListAttr.CallDateTime,DataType.CurrentTime,
								CallListAttr.Note,"开始播放后,对方挂机");
							this.StopPlay(); // 停止放音。
							this.HangUpCtrl(); // 挂机。
							this.CHState=DTCHState.HangUp;
							return ;
						}
						break;
					}				

					break;
			}
		}
	}
	/// <summary>
	/// 通道集合
	/// </summary>
	public class DTCHs: CHs
	{
		
		/// <summary>
		/// 通道集合
		/// </summary>
		public DTCHs()
		{
		}
		/// <summary>
		/// 增加一个通道
		/// </summary>
		/// <param name="en"></param>
		public void Add(DTCH en)
		{
			this.InnerList.Add(en);
		}
		/// <summary>
		/// 根据位置取得数据
		/// </summary>
		public DTCH this[int index]
		{
			get 
			{
				return (DTCH)this.InnerList[index];
			}
		}
	}
}
