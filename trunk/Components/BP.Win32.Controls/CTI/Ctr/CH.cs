using System;
using System.Collections;
using BP.CTI.App;
using BP.DA;

namespace BP.CTI
{
	 

	 
	/// <summary>
	/// 通道基类。
	/// </summary>
	public class CH
	{
		#region 通到属性
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
			//Log.DebugWriteInfo("开始播放文件:"+this.Playfiles[fileIndex] +" ch="+this.No);
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
		/// <summary>
		/// 呼出一个实例
		/// </summary>
		/// <param name="cl">实例</param>
		/// <returns></returns>
		public void Call(CallList cl)
		{
			cl.DoCalling(Card.GetCurrentContextByTelType(cl.FK_TelType) );
			// set call list.
			this.HisCallList = cl;
			// ch, 属性
			this.CurrPlayIndex=0;


			//case BP.CTI.TVCHState.CheckSign: //如果是检查外拨号音
			//if ( pcmn7api.TV_TimerElapsed(this.No) < 0 )
			//{
			/* 如果检测拨号音超时 */
			pcmn7api.TV_HangUpCtrl(this.No); // 挂机
			pcmn7api.TV_StartTimer(this.No,4); // 设置下次检测的时间段。
			//this.CHState=TVCHState.Waiting; // 设置状态为等待下次拨号.
			//}
					 
			// 判断是否有外拨号音
			//int sigpara1=0,sigpara2=0;
			//if (Usbid.TV_CheckSignal(this.No, ref sigpara1,ref sigpara2) ==TVCard.SIG_DIAL)
			//{
			// ///* 如果检测出有信号, 就开始执行外拨.*/
			Log.DebugWriteInfo("已经获取拨号音，开始拨号. ");
			pcmn7api.TV_StartTimer(this.No,4); // 设置拨号时间长度。
			Usbid.TV_StartDial(this.No, DataType.StringToByte(this.HisCallList.Tel) ); //开始拨号.
			//this.CHState=TVCHState.Dialing ; // 进入拨号状态.
			//}
			//break ;
			//Log.DebugWriteInfo("Will call fils ="+context);

			this.RingLong=40; //cl.HisTelType.RingLong;
			this.Playfiles=cl.CallFiles;
			this.CallOut();  // 用这个电话开始建立连接.
			this.CallOn(); // 呼出他.
		}
		#endregion



		#region 让子类重写。
		public virtual  void CallOut()
		{
			

		}
		public virtual  void CallOn()
		{
			

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
		 
		 
	}
}
