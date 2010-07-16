using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.WF
{
	 
	/// <summary>
	/// 文书属性
	/// </summary>
	public class BookBaseAttr
	{
		#region 属性
		/// <summary>
		/// MID
		/// </summary>
		public const string MID="MID";
		/// <summary>
		/// 工作ID
		/// </summary>
		public const string WorkID="WorkID";
		/// <summary>
		/// 节点
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// 
		/// </summary>
		public const string IsSend="IsSend";

		/// <summary>
		/// 相关功能
		/// </summary>
		public const string FK_NodeRefFunc="FK_NodeRefFunc";
		/// <summary>
		/// 是否归还
		/// </summary>
		public const string IsReturn="IsReturn";
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
	abstract public class BookBase :EntityMID
	{
		#region 基本属性
		 
		/// <summary>
		///  是否归还
		/// </summary>
		public BookSendState MyBookSendState
		{
			get
			{
				return (BookSendState)GetValIntByKey(BookAttr.BookSendState);
			}
			set
			{
				SetValByKey(BookAttr.BookSendState,value);
			}
		}
		/// <summary>
		///  是否归还
		/// </summary>
		public bool IsReturn
		{
			get
			{
				return GetValBooleanByKey(BookAttr.IsReturn);
			}
			set
			{
				SetValByKey(BookAttr.IsReturn,value);
			}
		}
		/// <summary>
		///  WorkID
		/// </summary>
		public int WorkID
		{
			get
			{
				return GetValIntByKey(BookAttr.WorkID);
			}
			set
			{
				SetValByKey(BookAttr.WorkID,value);
			}
		}
		/// <summary>
		///  FK_Node
		/// </summary>
		public int FK_Node
		{
			get
			{
				return GetValIntByKey(BookAttr.FK_Node);
			}
			set
			{
				SetValByKey(BookAttr.FK_Node,value);
			}
		}
		/// <summary>
		///  文书
		/// </summary>
		public int FK_NodeRefFunc
		{
			get
			{
				return GetValIntByKey(BookAttr.FK_NodeRefFunc);
			}
			set
			{
				SetValByKey(BookAttr.FK_NodeRefFunc,value);
			}
		}
		/// <summary>
		///  管理
		/// </summary>
		public int Admin
		{
			get
			{
				return GetValIntByKey(BookAttr.Admin);
			}
			set
			{
				SetValByKey(BookAttr.Admin,value);
			}
		}
		/// <summary>
		/// 流程标题
		/// </summary>
		public string  BookNo
		{
			get
			{
				return GetValStringByKey(BookAttr.BookNo);
			}
			set
			{
				SetValByKey(BookAttr.BookNo,value);
			}
		}
		/// <summary>
		/// 流程标题
		/// </summary>
		public string  FK_Taxpayer
		{
			get
			{
				return GetValStringByKey(BookAttr.FK_Taxpayer);
			}
			set
			{
				SetValByKey(BookAttr.FK_Taxpayer,value);
			}
		}
		public string  TaxpayerName
		{
			get
			{
				return GetValStringByKey(BookAttr.TaxpayerName);
			}
			set
			{
				SetValByKey(BookAttr.TaxpayerName,value);
			}
		}

		/// <summary>
		/// RecordDateTime。
		/// </summary>
		public string  RecordDateTime
		{
			get
			{
				return GetValStringByKey(BookAttr.RecordDateTime);
			}
			set
			{
				SetValByKey(BookAttr.RecordDateTime,value);
			}
		}
		/// <summary>
		/// 部门。
		/// </summary>
		public string  ReturnerDept
		{
			get
			{
				return GetValStringByKey(BookAttr.ReturnerDept);
			}
			set
			{
				SetValByKey(BookAttr.ReturnerDept,value);
			}
		}
		/// <summary>
		/// 征收机关。
		/// </summary>
		public string  ReturnerZSJG
		{
			get
			{
				return GetValStringByKey(BookAttr.ReturnerZSJG);
			}
			set
			{
				SetValByKey(BookAttr.ReturnerZSJG,value);
			}
		}
		/// <summary>
		///  Returner
		/// </summary>
		public int Recorder
		{
			get
			{
				return GetValIntByKey(BookAttr.Recorder);
			}
			set
			{
				SetValByKey(BookAttr.Recorder,value);
			}
		}
		 
		/// <summary>
		/// 归还人备注
		/// </summary>
		public string  ReturnerNote
		{
			get
			{
				return GetValStringByKey(BookAttr.ReturnerNote);
			}
			set
			{
				SetValByKey(BookAttr.ReturnerNote,value);
			}
		}
		/// <summary>
		/// 送达日期
		/// </summary>
		public string SendDate
		{
			get
			{
				return GetValStringByKey(BookAttr.SendDate);
			}
			set
			{
				SetValByKey(BookAttr.SendDate,value);
			}
		}
		/// <summary>
		/// 归还日期
		/// </summary>
		public string  ReturnTime
		{
			get
			{
				return GetValStringByKey(BookAttr.ReturnTime);
			}
			set
			{
				SetValByKey(BookAttr.ReturnTime,value);
			}
		}
		#endregion 

		public void GenerNewBook(string table)
		{
			Book.PTable=table;
			this._enMap=null;
			
		}
		/// <summary>
		/// 
		/// </summary>
		public static string PTable="WF_Book";

		#region 构造方法
		public BookBase() {}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mid"></param>
		public BookBase(int mid):base(mid){}		 
		#endregion 

		 
	}
	/// <summary>
	/// 条目
	/// </summary>
	abstract public class BookBases :Entities
	{
		#region 方法
		/// <summary>
		/// 根据一个工作人员的ID , 得到他能够考核的项目.
		/// </summary>
		/// <param name="empId">工作人员ID</param>
		/// <returns></returns>
		public static Books GetBooksByEmpId(int empId)
		{
			string sql=" SELECT * FROM CH_Book WHERE No IN ( SELECT FK_Book From CH_BookVSStation WHERE FK_Station IN  (SELECT FK_Station FROM Port_EmpStation WHERE FK_Emp="+empId+"))" ; 
			Books ens = new Books();
			ens.InitCollectionByTable(DBAccess.RunSQLReturnTable(sql)) ; 
			return ens;
		}
		#endregion 

		#region 构造方法属性
		/// <summary>
		/// Books
		/// </summary>
		public BookBases(){}
		 
		
		#endregion 

		 
	}
}
