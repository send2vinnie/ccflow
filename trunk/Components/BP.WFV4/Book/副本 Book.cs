using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
using BP.Web;

namespace BP.WF
{
	/// <summary>
	/// 文书送达状态
	/// </summary>
	public enum BookState
	{
		/// <summary>
		/// 未送达
		/// </summary>
		UnSend,
		/// <summary>
		/// 逾期未送达
		/// </summary>
		UnSendTimeout,
		/// <summary>
		/// 已送达
		/// </summary>
		Send,
		/// <summary>
		/// 未找到人
		/// </summary>
		Notfind,
		/// <summary>
		///已经归档
		/// </summary>
		Pigeonhole
	}
	/// <summary>
	/// 文书属性
	/// </summary>
	public class BookAttr
	{
		#region 属性
		/// <summary>
		/// 工作ID
		/// </summary>
		public const string WorkID="WorkID";
		/// <summary>
		/// 节点
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// 相关功能
		/// </summary>
		public const string FK_NodeRefFunc="FK_NodeRefFunc";
		/// <summary>
		/// 送达否
		/// </summary>
		public const string BookState="BookState";
	
		/// <summary>
		/// 退回时间
		/// </summary>
		public const string ReturnDateTime="ReturnDateTime";
		/// <summary>
		/// 纳税人编号
		/// </summary>
		public const string FK_Taxpayer="FK_Taxpayer";
		/// <summary>
		/// 名称
		/// </summary>
		public const string TaxpayerName="TaxpayerName";
		/// <summary>
		/// TaxpayerAddr
		/// </summary>
		public const string TaxpayerAddr="TaxpayerAddr";
		/// <summary>
		/// TaxpayerTel
		/// </summary>
		public const string TaxpayerTel="TaxpayerTel";
		/// <summary>
		/// BookNo
		/// </summary>
		public const string BookNo="BookNo";
		/// <summary>
		/// 管理员
		/// </summary>
		public const string BookAdmin="BookAdmin";
		/// <summary>
		/// 记录时间．
		/// </summary>
		public const string RDT="RDT";
		/// <summary>
		/// 应送达时间
		/// </summary>
		public const string ShouldSendDT="ShouldSendDT";
		/// <summary>
		/// 记录人．
		/// </summary>
		public const string Recorder="Recorder";
		/// <summary>
		/// 管理机关
		/// </summary>
		public const string FK_XJ="FK_XJ";
		/// <summary>
		/// 管理机关
		/// </summary>
		public const string FK_ZSJG="FK_ZSJG";
		///<summary>
		///送达人
		///</summary>
		public const string Sender="Sender";
		/// <summary>
		/// 受送达人
		/// </summary>
		public const string Accepter="Accepter";
		/// <summary>
		/// 送达地点
		/// </summary>
		public const string AccepterAddr="AccepterAddr";
		/// <summary>
		/// 收件日期
		/// </summary>
		public const string AccepterDateTime="AccepterDateTime";

		/// <summary>
		/// 代收人代收理由
		/// </summary>
		public const string AccepterNote="AccepterNote";
		/// <summary>
		/// 受送达人拒收理由和日期
		/// </summary>
		public const string AccepterDisNote="AccepterDisNote";
		/// <summary>
		/// 见证人签名或盖章
		/// </summary>
		public const string JZR="JZR";
		/// <summary>
		/// 年月
		/// </summary>
		public const string FK_NY="FK_NY";
		#endregion
	}
	/// <summary>
	/// 文书
	/// </summary> 
	public class Book :EntityOID
	{
		#region 统计信息的属性
		/// <summary>
		/// 未送达
		/// </summary>
		public static int NumOfUnSend
		{
			get
			{
				if ( int.Parse( DateTime.Now.ToString("hh")) <9  )
				{
					string sq1l="UPDATE WF_Book SET BookState="+(int)BookState.UnSendTimeout +"  WHERE ShouldSendDT > '"+DataType.CurrentData+"' AND  BookState="+(int)BookState.UnSend ;
					DBAccess.RunSQL(sq1l);
				}
				string sql="SELECT COUNT(*)  FROM WF_Book WHERE  Recorder='"+WebUser.No+"' AND BookState="+(int)BookState.UnSend ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		/// <summary>
		/// 逾期未送到
		/// </summary>
		public static int NumOfUnSendTimeout
		{
			get
			{
				string sql="SELECT COUNT(*)  FROM WF_Book WHERE  Recorder='"+WebUser.No+"' AND  BookState="+(int)BookState.UnSendTimeout ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		/// <summary>
		/// 已经送达
		/// </summary>
		public static int NumOfSend
		{
			get
			{
				string sql="SELECT COUNT(*)  FROM WF_Book WHERE  Recorder='"+WebUser.No+"' AND BookState="+(int)BookState.Send ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		/// <summary>
		/// 没有发现人
		/// </summary>
		public static int NumOfNotfind
		{
			get
			{
				string sql="SELECT COUNT(*)  FROM WF_Book WHERE  Recorder='"+WebUser.No+"' AND BookState="+(int)BookState.Notfind ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		/// <summary>
		/// 已经归档
		/// </summary>
		public static int NumOfPigeonhole
		{
			get
			{
				string sql="SELECT COUNT(*)  FROM WF_Book WHERE  Recorder='"+WebUser.No+"' AND BookState="+(int)BookState.Pigeonhole ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		#endregion

		#region 基本属性
		/// <summary>
		///   文书送达状态。
		/// </summary>
		public BookState BookState
		{
			get
			{
				return (BookState)GetValIntByKey(BookAttr.BookState);
			}
			set
			{
				SetValByKey(BookAttr.BookState,(int)value);
			}
		}
		/// <summary>
		///  文书编号
		/// </summary>
		public string BookNo
		{
			get
			{
				return this.GetValStringByKey(BookAttr.BookNo);
			}
			set
			{
				this.SetValByKey(BookAttr.BookNo,value);
			}
		}
		public string FK_NodeRefFuncText
		{
			get
			{
				return this.GetValRefTextByKey(BookAttr.FK_NodeRefFunc);
			}			
		}
		public int FK_NodeRefFunc
		{
			get
			{
				return this.GetValIntByKey(BookAttr.FK_NodeRefFunc);
			}
			set
			{
				this.SetValByKey(BookAttr.FK_NodeRefFunc,value);
			}
		}
		public int WorkID
		{
			get
			{
				return this.GetValIntByKey(BookAttr.WorkID);
			}
			set
			{
				this.SetValByKey(BookAttr.WorkID,value);
			}
		}
		/// <summary>
		/// Node
		/// </summary>
		public int FK_Node
		{
			get
			{
				return this.GetValIntByKey(BookAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(BookAttr.FK_Node,value);
			}
		}
		/// <summary>
		/// 送达时间
		/// </summary>
		public string AccepterDateTime
		{
			get
			{
				return this.GetValStringByKey(BookAttr.AccepterDateTime);
			}
			set
			{
				this.SetValByKey(BookAttr.AccepterDateTime,value);
			}
		}
		public string ShouldSendDT
		{
			get
			{
				return this.GetValStringByKey(BookAttr.ShouldSendDT);
			}
			set
			{
				this.SetValByKey(BookAttr.ShouldSendDT,value);
			}
		}
		/// <summary>
		/// 归还时间
		/// </summary>
		public string ReturnDateTime
		{
			get
			{
				return this.GetValStringByKey(BookAttr.ReturnDateTime);
			}
			set
			{
				this.SetValByKey(BookAttr.ReturnDateTime,value);
			}
		}
		public string FK_Taxpayer
		{
			get
			{
				return this.GetValStringByKey(BookAttr.FK_Taxpayer);
			}
			set
			{
				this.SetValByKey(BookAttr.FK_Taxpayer,value);
			}
		}
		/// <summary>
		/// 纳税人名称
		/// </summary>
		public string TaxpayerName
		{
			get
			{
				return this.GetValStringByKey(BookAttr.TaxpayerName);
			}
			set
			{
				this.SetValByKey(BookAttr.TaxpayerName,value);
			}
		}
		/// <summary>
		/// 文书打印时间
		/// </summary>
		public string RDT
		{
			get
			{
				return this.GetValStringByKey(BookAttr.RDT);
			}
			set
			{
				this.SetValByKey(BookAttr.RDT,value);
			}
		}
		/// <summary>
		/// 打印人
		/// </summary>
		public string Recorder
		{
			get
			{
				return this.GetValStringByKey(BookAttr.Recorder);
			}
			set
			{
				this.SetValByKey(BookAttr.Recorder,value);
			}
		}
		/// <summary>
		/// 管理机关
		/// </summary>
		public string FK_ZSJG
		{
			get
			{
				return this.GetValStringByKey(BookAttr.FK_ZSJG);
			}
			set
			{
				this.SetValByKey(BookAttr.FK_ZSJG,value);
			}
		}
		/// <summary>
		/// 县局
		/// </summary>
		public string FK_XJ
		{
			get
			{
				return this.GetValStringByKey(BookAttr.FK_XJ);
			}
			set
			{
				this.SetValByKey(BookAttr.FK_XJ,value);
			}
		}

		/// <summary>
		/// 送达人
		/// </summary>
		public string Sender
		{
			get
			{
				return this.GetValStringByKey(BookAttr.Sender);
			}
			set
			{
				this.SetValByKey(BookAttr.Sender,value);
			}
		}
		public string Accepter
		{
			get
			{
				return this.GetValStringByKey(BookAttr.Accepter);
			}
			set
			{
				this.SetValByKey(BookAttr.Accepter,value);
			}
		}
		public string AccepterAddr
		{
			get
			{
				return this.GetValStringByKey(BookAttr.AccepterAddr);
			}
			set
			{
				this.SetValByKey(BookAttr.AccepterAddr,value);
			}
		}

		public string AccepterDisNote
		{
			get
			{
				return this.GetValStringByKey(BookAttr.AccepterDisNote);
			}
			set
			{
				this.SetValByKey(BookAttr.AccepterDisNote,value);
			}
		}

		
		public string AccepterNote
		{
			get
			{
				return this.GetValStringByKey(BookAttr.AccepterNote);
			}
			set
			{
				this.SetValByKey(BookAttr.AccepterNote,value);
			}
		}

		 



		public string JZR
		{
			get
			{
				return this.GetValStringByKey(BookAttr.JZR);
			}
			set
			{
				this.SetValByKey(BookAttr.JZR,value);
			}
		}
		#endregion

		#region 构造方法
		/// <summary>
		/// HisUAC
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.IsDelete=false;
				uac.IsInsert=false;
				uac.IsUpdate=false;
				uac.IsView=true;
				return uac;
			}
		}
		/// <summary>
		/// 条目
		/// </summary>
		public Book(){}
		/// <summary>
		/// mid
		/// </summary>
		/// <param name="mid">mid</param>
		public Book(int mid):base(mid){}
		#endregion 

		#region Map
		/// <summary>
		/// EnMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map( "WF_Book" );
				map.DepositaryOfMap=Depositary.None;
				map.EnDesc="文书";
				map.AddTBIntPKOID();
				map.AddTBInt(BookAttr.WorkID,0,"工作ID",false,true);
				map.AddDDLEntities(BookAttr.FK_Node, 0 , DataType.AppInt,"节点名称",new Nodes(),NodeAttr.NodeID,NodeAttr.Name, false );
				map.AddDDLEntities(BookAttr.FK_NodeRefFunc,"","文书名称",new NodeRefFuncExts(),false);
				map.AddTBString(BookAttr.BookNo,null,"编号",true,true,0,100,5);
				map.AddDDLSysEnum(BookAttr.BookState,1,"文书状态",true,true);

				map.AddTBString(BookAttr.FK_Taxpayer,null,"税务管理码",true,true,0,100,5);
				map.AddTBString(BookAttr.TaxpayerName,null,"纳税人名称",true,true,0,100,5);
				map.AddTBString(BookAttr.TaxpayerAddr,null,"地址",true,true,0,100,5);
				map.AddTBString(BookAttr.TaxpayerTel,null,"电话",true,true,0,100,5);

				map.AddTBDate(BookAttr.RDT,"文书打印时间",true,true);
				map.AddTBDate(BookAttr.ShouldSendDT,"应送达时间",true,true);

				map.AddDDLEntities(BookAttr.Recorder,Web.WebUser.No,"打印人",new Emps(),false);
				//送达人
				map.AddTBString(BookAttr.Sender,null,"送达人",true,true,0,100,5);
				//送达地点
				map.AddTBString(BookAttr.Accepter,null,"受送达人",true,true,0,100,5);
				map.AddTBString(BookAttr.AccepterAddr,null,"送达地点",true,true,0,100,5);
				map.AddTBString(BookAttr.AccepterDateTime,null,"收件日期",true,true,0,100,5);
				map.AddTBString(BookAttr.AccepterNote,null,"代收人代收理由",true,true,0,100,5);
				map.AddTBString(BookAttr.AccepterDisNote,null,"受送达人拒收理由和日期",true,true,0,100,5);
				map.AddTBString(BookAttr.JZR,null,"见证人签名或盖章",true,true,0,100,5);

				map.AddDDLEntities(BookAttr.FK_ZSJG,TaxUser.FK_ZSJG,"管理机关",new BP.Tax.ZSJGs(),false);
				map.AddDDLEntities(BookAttr.FK_NY,DataType.CurrentYearMonth,"隶属年月",new BP.Pub.NYs(),false);

				//map.AddDDLEntities(BookAttr.FK_XJ,TaxUser.FK_ZSJGOfXJ,"县局",new BP.Tax.XJs(),false);

				map.AddSearchAttr(BookAttr.FK_NY);
				map.AddSearchAttr(BookAttr.FK_ZSJG);
				map.AddSearchAttr(BookAttr.Recorder);
				map.AddSearchAttr(BookAttr.BookState);
				//map.AttrsOfSearch.AddFromTo("日期",BookAttr.RDT, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,6);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

//		public void DoSendWork( int bookstate )
//		{
//			try
//			{
//				BP.WF.Node nd =  new BP.WF.Node(this.FK_Node);
//				Work wk = nd.HisWork;
//				wk.SetValByKey("OID",this.WorkID);
//				wk.Retrieve(); // 查询出来工作。
//
//				if (wk.EnMap.Attrs.Contains(WorkAttr.SendDateTime) && wk.EnMap.Attrs.Contains(WorkAttr.BookState))
//				{
//					wk.Update(WorkAttr.BookState, bookstate ,WorkAttr.SendDateTime,DataType.CurrentDataTime);
//				}
//
//				/*
//				wk.IsSend=true;
//				wk.SendDateTime=DataType.CurrentDataTime;
//				wk.DirectUpdate();
//				*/
//			}
//			catch(Exception ex)
//			{
//				throw new Exception("文书送达失败:"+ex.Message);
//			}
//
//		}
//		public void DoSend( int bookstate )
//		{
//			this.DoSendWork(bookstate);
//			//this.IsSend=true;
//			//this.SendDate=DataType.CurrentData;
//			this.Update(BookAttr.BookState, bookstate ,BookAttr.SendDate,DataType.CurrentDataTime);
//		}

		#region 
		protected override bool beforeUpdateInsertAction()
		{
			//			return base.beforeUpdateInsertAction ();
			//  
			//			if (this.IsReturn)
			//			{
			//				/*如果文书已经归还了，说明他已经缴销了*/
			//				if (this.MyBookState==BookState.UnSend)
			//				{
			//					//this.DoSendWork( (int)BookState.Send );
			//					this.MyBookState=BookState.Send;
			//					this.SendDate=DataType.CurrentData;
			//				}
			//			}
			//			 
			return base.beforeUpdateInsertAction();
		}
		/// <summary>
		/// 更新之前处理的工作。
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			
			return base.beforeUpdate ();
		}
		#endregion
	}
	/// <summary>
	/// 条目
	/// </summary>
	public class Books :EntitiesOID
	{
		#region 构造方法属性
		/// <summary>
		/// Books
		/// </summary>
		public Books(){}
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Book();
			}
		}
		#endregion
	}
}
