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
	/// 文书属性
	/// </summary>
	public class BookAttr
	{
		#region 属性
		/// <summary>
		/// MID
		/// </summary>
		public const string OID="OID";
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
		public const string BookSendState="BookSendState";
		/// <summary>
		/// 发送时间
		/// </summary>
		public const string SendDate="SendDate";
		/// <summary>
		/// 纳税人编号
		/// </summary>
		public const string FK_Taxpayer="FK_Taxpayer";
		/// <summary>
		/// 名称
		/// </summary>
		public const string TaxpayerName="TaxpayerName";

		
		/// <summary>
		/// BookNo
		/// </summary>
		public const string BookNo="BookNo";
		/// <summary>
		/// 管理员
		/// </summary>
		public const string Admin="Admin";
		/// <summary>
		/// 记录时间．
		/// </summary>
		public const string RecordDateTime="RecordDateTime";
		/// <summary>
		/// 记录人．
		/// </summary>
		public const string Recorder="Recorder";
		/// <summary>
		/// 部门
		/// </summary>
		public const string ReturnerDept="ReturnerDept";
		/// <summary>
		/// 征收机关
		/// </summary>
		public const string ReturnerZSJG="ReturnerZSJG";
		/// <summary>
		/// 归还时间
		/// </summary>
		public const string ReturnTime="ReturnTime";
		/// <summary>
		/// 归还人备注
		/// </summary>
		public const string ReturnerNote="ReturnerNote";
		#endregion		
	}
	/// <summary>
	/// 文书
	/// </summary> 
	public class Book :BookBase
	{
	 
		#region 基本属性
		/// <summary>
		///  是否送达
		/// </summary>
		public bool IsSend
		{
			get
			{
				return GetValBooleanByKey(BookAttr.IsSend);
			}
			set
			{
				SetValByKey(BookAttr.IsSend,value);
			}
		}
		/// <summary>
		///  BookSendState文书送达状态。
		/// </summary>
		public BookSendState HisBookSendState
		{
			get
			{
				return (BookSendState)GetValIntByKey(BookAttr.BookSendState);
			}
			set
			{
				SetValByKey(BookAttr.BookSendState,(int)value);
			}
		}
		#endregion

		#region 构造方法
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
				Map map = new Map( BookBase.PTable );
				map.DepositaryOfMap=Depositary.None;
				map.EnDesc="文书";
			 

				map.AddTBMID();
				map.AddTBIntPK(BookAttr.WorkID,0,"工作ID",false,true);
				map.AddDDLEntities(BookAttr.FK_Node,0, DataType.AppInt,"节点",new Nodes(),NodeAttr.OID,NodeAttr.Name,false);
				//map.AddTBString(BookAttr.BookNo,null,"编号",true,true,0,100,5);
				map.AddDDLEntitiesPK(BookAttr.FK_NodeRefFunc,0, DataType.AppInt,"文书",new NodeRefFuncs(),NodeRefFuncAttr.OID,NodeRefFuncAttr.Name,false);
				map.AddTBString(BookAttr.BookNo,null,"编号",true,true,0,100,5);
				map.AddDDLSysEnum(WorkAttr.BookSendState,0,"文书送达状态",true,false);

				map.AddBoolean(BookAttr.IsSend,false,"送达否",true,true);
				map.AddTBDateTime(BookAttr.SendDate,"送达时间",true,true);

				map.AddBoolean(BookAttr.IsReturn,false,"归还否",true,true);
				map.AddTBDate(BookAttr.ReturnTime,"归还时间",true,true);
				map.AddTBString(BookAttr.FK_Taxpayer,null,"纳税人编号",true,true,0,100,5);
				map.AddTBString(BookAttr.TaxpayerName,null,"纳税人名称",true,true,0,100,5);

				map.AddTBDate(BookAttr.RecordDateTime,"记录时间",true,true);
				map.AddDDLEntities(BookAttr.Recorder,Web.WebUser.OID, DataType.AppInt, "记录人",new Emps(),EmpAttr.OID,EmpAttr.Name,false);

				//map.AddTBString(BookAttr.ReturnerNote,null,"备注",true,true,0,100,5);
				map.AddTBString(BookAttr.ReturnerDept,WebUser.FK_Dept ,"部门",true,true,0,100,5);
				map.AddTBString(BookAttr.ReturnerZSJG,TaxUser.FK_ZSJG,"征收机关",true,true,0,100,5);

				map.AddSearchAttr(BookAttr.IsReturn);
				map.AddSearchAttr(BookAttr.BookSendState);

				map.AttrsOfSearch.AddFromTo("日期",BookAttr.RecordDateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,12);
 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		public void DoSendWork( int bookstate )
		{
			try
			{
				BP.WF.Node nd =  new BP.WF.Node(this.FK_Node);
				Work wk = nd.HisWork;
				wk.SetValByKey("OID",this.WorkID);
				wk.Retrieve(); // 查询出来工作。

				if (wk.EnMap.Attrs.Contains(WorkAttr.SendDateTime) && wk.EnMap.Attrs.Contains(WorkAttr.BookSendState))
				{
					wk.Update(WorkAttr.BookSendState, bookstate ,WorkAttr.SendDateTime,DataType.CurrentDataTime);
				}

				/*
				wk.IsSend=true;
				wk.SendDateTime=DataType.CurrentDataTime;
				wk.DirectUpdate();
				*/
			}
			catch(Exception ex)
			{
				throw new Exception("文书送达失败:"+ex.Message);
			}

		}
		public void DoSend( int bookstate )
		{
			this.DoSendWork(bookstate);
			//this.IsSend=true;
			//this.SendDate=DataType.CurrentData;
			this.Update(BookAttr.BookSendState, bookstate ,BookAttr.SendDate,DataType.CurrentDataTime);
		}

		#region 
		protected override bool beforeUpdateInsertAction()
		{
			return base.beforeUpdateInsertAction ();
  
			if (this.IsReturn)
			{
				/*如果文书已经归还了，说明他已经缴销了*/
				if (this.MyBookSendState==BookSendState.UnSend)
				{
					//this.DoSendWork( (int)BookSendState.Send );
					this.MyBookSendState=BookSendState.Send;
					this.SendDate=DataType.CurrentData;
				}
			}
			 
			return base.beforeUpdateInsertAction ();
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
	public class Books :BookBases
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
