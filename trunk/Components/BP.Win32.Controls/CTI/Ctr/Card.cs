using System;
using System.Runtime.InteropServices;
using BP.CTI.App;
using BP.DA;
using BP.Sys;
using System.Data;


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
		/// 暂停
		/// </summary>
		Pause,
		/// <summary>
		/// 停止
		/// </summary>
		Stop,
		/// <summary>
		/// 失去能力的
		/// </summary>
		Disable
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
		#region 得到当前的呼出内容
		public static DataTable dtOfContext=null;
		/// <summary>
		/// 得到当前的呼出内容
		/// </summary>
		/// <param name="teltype">电话类型</param>
		/// <returns>如果没有当前的呼出内容，就返回null.</returns>
		public static string GetCurrentContextByTelType(int teltype)
		{
			if (dtOfContext==null)
			{
				string sql="SELECT  a.Name as '呼出名称 ', b.No '呼出内容编号', b.Context '呼出内容', b.FK_DTS '调度', b.FK_TelTypeOfFit '适合用户类型' FROM CTI_Schedule a, CTI_Context b WHERE "
					+"a.FK_YF='"+DataType.CurrentMonth+"' and (a.DateFrom <='"+DataType.CurrentDay+"' and a.DateTo>='"+DataType.CurrentDay+"') and a.FK_Context=b.No";

				dtOfContext=DBAccess.RunSQLReturnTable(sql);
			}

			foreach(DataRow dr in dtOfContext.Rows)
			{
				int type=int.Parse(dr["适合用户类型"].ToString());

				if ( type==0 || type==teltype)
					return dr["呼出内容"].ToString();
			}
			return null;
		}
		#endregion


		#region  测试
		public static void Test( int ch, CallList cl )
		{
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					DTCard.DoCall(ch,cl);
					break;
				case CardType.Usbid:
					TVCard.DoCall(ch,cl);
					break;
				default:
					throw new Exception("unknow card type");
			}

		}
		 
		#endregion


		#region 显示当前的工作情况
		public static DataTable _StateDT=null;
		public static DataTable StateDT
		{
			get
			{
				if (_StateDT==null)
				{
					_StateDT= new DataTable("mstable");
					_StateDT.Columns.Add( new DataColumn("CH",typeof(string)));
					_StateDT.Columns.Add( new DataColumn("State",typeof(string)));
					_StateDT.Columns.Add( new DataColumn("Tel",typeof(string)));
					//_StateDT.Columns.Add( new DataColumn("CH",typeof(int)));
				}

				_StateDT.Rows.Clear();
				if (Card.HisCardWorkState==CardWorkState.Stop)
					return _StateDT;

				switch(Card.HisCardType)
				{
					case CardType.pcmn7api:
						foreach(DTCH ch in DTCard.HisCHs)
						{
							DataRow dr =_StateDT.NewRow();
							if (ch.CHState!=DTCHState.HangUp)
							{
								dr[0]=ch.No;
								dr[1]=ch.CHState.ToString();
								dr[2]=ch.HisCallList.Tel;
							}
							else
							{
								dr[0]=ch.No;
								dr[1]=ch.CHState.ToString();
								dr[2]="";
							}
							_StateDT.Rows.Add(dr);
						}
						return _StateDT;
					case CardType.Usbid:
						foreach(TVCH ch in TVCard.HisCHs)
						{
							DataRow dr =_StateDT.NewRow();
							if (ch.CHState!=TVCHState.HangUp)
							{
								dr[0]=ch.No;
								dr[1]=ch.CHState.ToString();
								dr[2]=ch.HisCallList.Tel;
							}
							else
							{
								dr[0]=ch.No;
								dr[1]=ch.CHState.ToString();
								dr[2]="";
							}
							_StateDT.Rows.Add(dr);
						}
						return _StateDT;
					default:
						throw new Exception("error _StateDT ");
				}
			}
		}
		#endregion

		/// <summary>
		/// 初始化时间表
		/// </summary>
		public static void InitSchedule_del()
		{
			/*
			#region 开始设置调度, 自动更新他。
			//开始设置调度, 自动更新他。
			BP.DTS.SysDTSs.InitDataIOEns();
			BP.DTS.SysDTS dts = new BP.DTS.SysDTS();
			dts.Name="获取呼叫数据";
			if (dts.RetrieveByName()==0)
			{
				Log.DefaultLogWriteLineError("没有找到呼叫调度确认名称是否是(获取呼叫数据)");
				return;
			}

			string yf=DateTime.Now.ToString("MM");
			Schedule sc = new Schedule(yf);

			int start=int.Parse(sc.DateOfStart);
			int end=int.Parse(sc.DateOfEnd);
			string strs="";
			for(int i=start; i<=end; i++)
			{
				strs+=i.ToString().PadLeft(2,'0')+",";				
			}
			dts.EveryMonth="00";
			dts.EveryDay=strs;
			dts.EveryHour=sc.RunHH;
			dts.EveryMinute=sc.Runmm;
			dts.Update();
			#endregion

			#region 开始调整呼出内容。呼出内容 pk =1 ,2 是保留的。
//			CallContext cc =new CallContext("1");
//			cc.DateFrom=int.Parse(sc.DateOfStart);
//			cc.DateTo=int.Parse(sc.DateOfBalance);
//			cc.Update();
//
//			CallContext cc1 =new CallContext("2");
//			cc1.DateFrom=int.Parse(sc.DateOfBalance)+1;
//			cc1.DateTo=int.Parse(sc.DateOfEnd);
//			cc1.Update();
			#endregion
			*/
		}
		/// <summary>
		/// 重新调整系统参数
		/// </summary>
		public static void ResetParas()
		{
			return;
			
			SysConfigs.ReSetVal(); // 重新设置系统配置。

			DefaultCalledTel=SysConfigs.GetValByKey("CallTel","2252291");
			DefaultMinJE=SysConfigs.GetValByKeyFloat("MinJE",0);  // 最小的呼出金额
			Log.DebugWriteInfo("Card 变量初始化成功...");


			Card._CurrentDTSs=null;
			Card._StateDT=null;
			Card.dtOfContext=null; 
			

			//
			//
			//
			//			// 设置当前的呼出内容。
			//
			//
			//			Card.CurrentSchedule=Schedules.CurrentSchedule;
			//
			//			if (Card.CurrentSchedule==null)
			//			{
			//				Card.CurrentCallContext=null;
			//				Card.CurrentDTS=null;
			//			}
			//			else
			//			{
			//				Card.CurrentCallContext=Card.CurrentSchedule.HisCallContext;
			//				Card.CurrentDTS=Card.CurrentCallContext.HisDTS;
			//			}
			// 设置当前的调度.
			// 设置当前的时间表。
		}
		 
		/// <summary>
		/// 控制用变量控制板卡工作。
		/// </summary>
		public static void CtlCardByParas()
		{
			return;
			SysConfigs.ReSetVal();
			CardWorkState cws =(CardWorkState)SysConfigs.GetValByKeyInt("CardWorkState",0);
			switch(cws)
			{
				case CardWorkState.Stop:
					Card.DoStop();
					break;
				case CardWorkState.Pause:
					Card.DoPause();
					break;
				case CardWorkState.Runing:
					Card.HisCardWorkState = CardWorkState.Runing;
					break;
				default:
					Log.DebugWriteInfo("外部控制不能使他中断");
					break;
			}
		}

		#region 系统属性
		/// <summary>
		/// 板卡的工作状态
		/// </summary>
		public static CardWorkState HisCardWorkState=CardWorkState.Disable;
		/// <summary>
		/// 默认的主叫号码
		/// </summary>
		public static string DefaultCalledTel="2251213";
		/// <summary>
		/// 最小的呼出金额
		/// </summary>
		public static float DefaultMinJE=0;		 
	    public static BP.DTS.SysDTSs _CurrentDTSs=null;		
		public static BP.DTS.SysDTSs CurrentDTSs
		{
			get
			{
				if (_CurrentDTSs==null)
				{
					if (Card.dtOfContext==null)					 
						Card.GetCurrentContextByTelType(0);


					_CurrentDTSs = new BP.DTS.SysDTSs();

					foreach(DataRow dr in Card.dtOfContext.Rows)
					{
						BP.DTS.SysDTS dts = new BP.DTS.SysDTS( dr["调度"].ToString() );
						BP.CTI.App.CallContext text= new CallContext( dr["呼出内容编号"].ToString() ); 
						dts.EveryMonth="00";
						dts.EveryDay=text.DTSDay;
						dts.EveryHour=text.DTSHH;
						dts.EveryMinute=text.DTSmm;
						_CurrentDTSs.AddEntity( dts );
					}
				}
				return _CurrentDTSs;
			}
		}
		//public static BP.CTI.App.Schedule CurrentSchedule=null;
		public static string MySerial="YSNet=04650108,YSDS=05260161,YSGS=05260124";
		/// <summary>
		/// 板卡序列号码
		/// </summary>
		public static string Serial
		{
			get
			{
				switch(Card.HisCardType)
				{
					case CardType.pcmn7api:
						return Card.Serial_PCM;
						//return "04650108";
					case CardType.Usbid:
						return Card.Serial_Usbid;
					default:
						throw new Exception("unknow card type");
				}
			}
		}
		private static string _Serial_Usbid=null;

		public static string Serial_Usbid
		{
			get
			{
				if (_Serial_Usbid!=null)
					return _Serial_Usbid;

				if (Card.ChCount <=1 )
					return null;byte[] bs = new byte[20];

				char[] cs = new char[20];
				string s;
				pcmn7api.TV_GetSerial(bs);
				for(int i = 0; i < cs.Length ; i++)
					cs[i] = Convert.ToChar(bs[i]);					;
				s = new string(cs);
				_Serial_Usbid=s;
				return _Serial_Usbid;
			}
		}
		private static string _Serial_PCM=null;
		public static string Serial_PCM
		{
			get
			{
				if (_Serial_PCM!=null)
					return _Serial_PCM;

				if (Card.ChCount <=1 )
					return null;

				byte[] bs = new byte[20];
				char[] cs = new char[20];
				string s;
				pcmn7api.PCM7_GetSerial(bs);
				for(int i = 0; i < cs.Length ; i++)
					cs[i] = Convert.ToChar(bs[i]);					;
				s = new string(cs);
				_Serial_PCM=s;
				return _Serial_PCM;

				/*
				if ( TV_Installed() >0 )
				{
					//pcmn7api.TV_Initialize();
					
				}
				else
				{
					return null;				
				}
				*/
			}
		}
		/// <summary>
		/// 通到数量(如果通道数量==0说明没有初始化成功。)
		/// </summary>
		public static int ChCount=0;
		/// <summary>
		/// 板卡类型
		/// </summary>
		public static CardType _HisCardType=CardType.unknown;
		public static CardType HisCardType
		{
			get
			{
				if (_HisCardType==CardType.unknown)
				{
					if (SystemConfig.CustomerNo==CustomerNoList.YSNet)
						_HisCardType=CardType.pcmn7api;
					else
						_HisCardType=CardType.Usbid;
				}
				return _HisCardType;
			}
		}

		#endregion

		#region 控制方法		
		/// <summary>
		/// 设置录放音的压缩比
		/// </summary>
		/// <param name="bye">1,2,3, 分别是64,32,16通常为 64. </param>
		public static void CompressRatio(int bye)
		{
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					pcmn7api.PCM7_CompressRatio(bye);					
					break;
				case CardType.Usbid:
					pcmn7api.TV_CompressRatio(bye);
					break;
				default:
					throw new Exception("un know card type");
			}
		}
		
		/// <summary>
		/// 出使化语音卡.
		/// </summary>
		/// <returns></returns>
		public static void beforeInitialize()
		{
			 
			#region 数据库与运行环境设置。
			// 把没有呼出成功的数据呼出出去.不应该存在这样的问题.
			DBAccess.RunSQL("update CTI_CallList set CallingState=0 where CallingState=1 or CallingState is null");
			// clear free list.
			///DBAccess.RunSQL("delete CTI_CallList where tel in (select tel from CTI_Release)");
			//设置系统参数
			ResetParas();
			#endregion
			 
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
					throw new Exception("unknow card type");
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
					i=pcmn7api.TV_Initialize(0);
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
 		}		 
		#endregion

		 

		#region 呼出
		public bool IsClient
		{
			get
			{
				if ( Card.ChCount <= 0)
					return true;
				else
					return false; 
			}
		}
		/// <summary>
		/// 停止语音卡工作
		/// </summary>		
		public static void Disable()
		{
			// 给值			
			Card.HisCardWorkState=CardWorkState.Disable;
			//Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Disable);

			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					DTCard.ChCount=0;
					DTCard.HisCHs=null;
					pcmn7api.PCM7_Disable();
					break;
				case CardType.Usbid:
					TVCard.ChCount=0;
					TVCard.HisCHs=null;
					pcmn7api.TV_Disable();
					break;
				default:					
					return;
			}
		}
		/// <summary>
		/// 执行暂停
		/// </summary>
		public static void DoStop()
		{
			//Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Stop);
			if (HisCardWorkState==CardWorkState.Pause
				|| HisCardWorkState==CardWorkState.Runing)
			{
				HisCardWorkState =CardWorkState.Stop;
				//DoCallRemainder(); // 呼出剩余的
			}
		}
		public static void DoRuning()
		{
			//Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Runing);
			HisCardWorkState =CardWorkState.Runing;
		}
		/// <summary>
		/// 执行暂停
		/// </summary>
		public static void DoPause()
		{
			//Sys.SysConfigs.SetValByKey("CardWorkState",(int)CardWorkState.Pause);

			HisCardWorkState =CardWorkState.Pause;
			//DoCallRemainder();
		}
		 
		/// <summary>
		/// 运行
		/// </summary>
		public static void Run()
		{
			//if (SystemConfigOfTax.FK_ZSJG=="YSNET")
				//return;

			//Card.InitSchedule();

			Log.DefaultLogWriteLineInfo("******before card run "+HisCardWorkState.ToString());
			while(true)
			{
				if (HisCardWorkState==CardWorkState.Runing)
				{
					/* 如果当前的状态是运行的. */
					switch(Card.HisCardType)
					{
						case CardType.pcmn7api:
							DTCard.DoCall();
							break;
						case CardType.Usbid:
							TVCard.DoCall();
							break;
						default:
							throw new Exception("unkonw card type");
					}
				}
				System.Threading.Thread.Sleep(3000);
				//Log.DefaultLogWriteLineInfo("************* end run");
			}
			Log.DefaultLogWriteLineInfo("************* end card run");
		}
		/// <summary>
		/// 执行呼出剩余的
		/// </summary>
		public static void DoCallRemainder()
		{
			while(true)
			{
				Card.CallOn();
				if (Card.IsAllCHsFree) //如果都是空闲的就跳出来.
					break;
			}
			System.Threading.Thread.Sleep(0); // 避免过多的浪费资源，在这里休眠。
		}
		/// <summary>
		/// 让Card工作起来
		/// </summary>
		public static void CallOn()
		{
			switch(Card.HisCardType)
			{
				case CardType.pcmn7api:
					foreach(DTCH ch in DTCard.HisCHs)
						ch.CallOn();
					break;
				case CardType.Usbid:
					foreach(TVCH ch in TVCard.HisCHs)
						ch.CallOn();
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// 是否全部的都是空闲的。
		/// </summary>
		private static bool IsAllCHsFree
		{
			get
			{
				switch(Card.HisCardType)
				{
					case CardType.pcmn7api:
						return DTCard.IsAllCHFree;
					case CardType.Usbid:
						return TVCard.IsAllCHFree;
					default:
						throw new Exception("sss");
				}
			}
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

