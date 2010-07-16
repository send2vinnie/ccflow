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
	public class BookAttr
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
		/// 相关功能
		/// </summary>
		public const string FK_NodeRefFunc="FK_NodeRefFunc";
		/// <summary>
		/// 是否归还
		/// </summary>
		public const string IsReturn="IsReturn";
		/// <summary>
		/// FlowTitle
		/// </summary>
		public const string FlowTitle="FlowTitle";
		/// <summary>
		/// 管理员
		/// </summary>
		public const string Admin="Admin";
		/// <summary>
		/// 建立时间．
		/// </summary>
		public const string CreateTime="CreateTime";
		/// <summary>
		/// 归还人．
		/// </summary>
		public const string Returner="Returner";
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
	public class Book :Entity
	{
		#region 基本属性
		/// <summary>
		///  MID
		/// </summary>
		public int MID
		{
			get
			{
				return GetValIntByKey(BookAttr.MID);
			}
			set
			{
				SetValByKey(BookAttr.MID,value);
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
		public string  FlowTitle
		{
			get
			{
				return GetValStringByKey(BookAttr.FlowTitle);
			}
			set
			{
				SetValByKey(BookAttr.FlowTitle,value);
			}
		}
		/// <summary>
		/// 建立时间。
		/// </summary>
		public string  CreateTime
		{
			get
			{
				return GetValStringByKey(BookAttr.CreateTime);
			}
			set
			{
				SetValByKey(BookAttr.CreateTime,value);
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
		public int Returner
		{
			get
			{
				return GetValIntByKey(BookAttr.Returner);
			}
			set
			{
				SetValByKey(BookAttr.Returner,value);
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
		/// 规划日期
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
		private static string PTable="WF_Book";

		#region 构造方法
		/// <summary>
		/// 条目
		/// </summary>
		public Book(){}
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
				Map map = new Map( Book.PTable );
				map.DepositaryOfMap=Depositary.None;

				map.EnDesc="文书";                
				map.IsDelete=true;
				map.IsInsert=true;
				map.IsUpdate=true;
				map.IsView=true;
				map.AddTBMID();
				map.AddTBIntPK(BookAttr.WorkID,0,"工作ID",true,true);
				map.AddBoolean(BookAttr.IsReturn,false,"归还否",true,true);
				map.AddTBString(BookAttr.FlowTitle,null,"流程标题",true,true,0,100,5);
				//map.AddDDLEntities(BookAttr.FK_Node,null, DataType.AppInt,"节点",new Nodes(),NodeAttr.OID,NodeAttr.Name,false);
				//map.AddDDLEntities(BookAttr.Admin,1, DataType.AppInt,"管理员",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBInt(BookAttr.Admin,1,"管理员",false,false);

				map.AddTBDateTime(BookAttr.CreateTime,"建立时间",true,true);
				map.AddDDLEntities(BookAttr.Returner,1, DataType.AppInt, "归还人",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBString(BookAttr.ReturnerNote,null,"备注",true,true,0,100,5);
				map.AddTBDateTime(BookAttr.ReturnTime,"归还时间",false,true);
				map.AddTBString(BookAttr.ReturnerNote,null,"备注",true,true,0,100,5);
				map.AddTBIntPK(BookAttr.FK_NodeRefFunc,0,"文书",false,true);
				map.AddTBString(BookAttr.ReturnerDept,null,"部门",true,true,0,100,5);
				map.AddTBString(BookAttr.ReturnerZSJG,null,"征收机关",true,true,0,100,5);

				//map.AddDDLEntitiesPK(BookAttr.FK_NodeRefFunc,null, DataType.AppInt,"文书名称",new NodeRefFuncs(),NodeRefFuncAttr.OID,NodeRefFuncAttr.Name,false);
				
				map.AddSearchAttr(BookAttr.IsReturn);
				//map.AttrsOfSearch.AddFromTo("日期",BookAttr.CreateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,7);
				map.AttrsOfSearch.AddFromTo("日期",BookAttr.CreateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataFormat), DA.DataType.CurrentData,7);

				//map.AddSearchAttr(BookAttr.FK_Node);
				//map.AddSearchAttr(BookAttr.FK_NodeRefFunc);
				//map.AddSearchAttr(BookAttr.Returner);
				//map.AttrsOfSearch.AddFromTo("日前",);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 条目
	/// </summary>
	public class Books :Entities
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
