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
	public class BookExtAttr:BookAttr
	{
		 
	}
	/// <summary>
	/// 文书
	/// </summary> 
	public class BookExt :BookBase
	{
		#region 扩展属性
		 
		#endregion

		#region 基本属性
		/// <summary>
		///  是否归还
		/// </summary>
		public bool IsReturn
		{
			get
			{
				return GetValBooleanByKey(BookExtAttr.IsReturn);
			}
			set
			{
				SetValByKey(BookExtAttr.IsReturn,value);
			}
		}
		/// <summary>
		///  WorkID
		/// </summary>
		public int WorkID
		{
			get
			{
				return GetValIntByKey(BookExtAttr.WorkID);
			}
			set
			{
				SetValByKey(BookExtAttr.WorkID,value);
			}
		}
		/// <summary>
		///  FK_Node
		/// </summary>
		public int FK_Node
		{
			get
			{
				return GetValIntByKey(BookExtAttr.FK_Node);
			}
			set
			{
				SetValByKey(BookExtAttr.FK_Node,value);
			}
		}
		/// <summary>
		///  文书
		/// </summary>
		public int FK_NodeRefFunc
		{
			get
			{
				return GetValIntByKey(BookExtAttr.FK_NodeRefFunc);
			}
			set
			{
				SetValByKey(BookExtAttr.FK_NodeRefFunc,value);
			}
		}
		/// <summary>
		///  管理
		/// </summary>
		public int Admin
		{
			get
			{
				return GetValIntByKey(BookExtAttr.Admin);
			}
			set
			{
				SetValByKey(BookExtAttr.Admin,value);
			}
		}
		 
		/// <summary>
		/// 建立时间。
		/// </summary>
		public string  RecordDateTime
		{
			get
			{
				return GetValStringByKey(BookExtAttr.RecordDateTime);
			}
			set
			{
				SetValByKey(BookExtAttr.RecordDateTime,value);
			}
		}
		/// <summary>
		///  Returner
		/// </summary>
		public int Recorder
		{
			get
			{
				return GetValIntByKey(BookExtAttr.Recorder);
			}
			set
			{
				SetValByKey(BookExtAttr.Recorder,value);
			}
		}
		 
		/// <summary>
		/// 归还人备注
		/// </summary>
		public string  ReturnerNote
		{
			get
			{
				return GetValStringByKey(BookExtAttr.ReturnerNote);
			}
			set
			{
				SetValByKey(BookExtAttr.ReturnerNote,value);
			}
		}
		/// <summary>
		/// 规划日期
		/// </summary>
		public string  ReturnTime
		{
			get
			{
				return GetValStringByKey(BookExtAttr.ReturnTime);
			}
			set
			{
				SetValByKey(BookExtAttr.ReturnTime,value);
			}
		}
		#endregion 

		#region 构造方法
		/// <summary>
		/// 条目
		/// </summary>
		public BookExt(){}
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
				Map map = new Map("WF_BookExt");	
				map.DepositaryOfMap=Depositary.Application;
				//				map.CodeStruct="3";
				map.EnDesc="文书";                
				map.IsDelete=true;
				map.IsInsert=true;
				map.IsUpdate=true;
				map.IsView=true;
				map.AddTBIntPK(BookExtAttr.WorkID,0,"工作ID",true,true);

				map.AddBoolean(BookExtAttr.IsReturn,false,"归还否",true,false);
				map.AddTBString(BookExtAttr.FlowTitle,null,"流程标题",true,true,0,100,5);
				//map.AddDDLEntities(BookExtAttr.FK_Node,null, DataType.AppInt,"节点",new Nodes(),NodeAttr.OID,NodeAttr.Name,false);
				//map.AddDDLEntities(BookExtAttr.Admin,1, DataType.AppInt,"管理员",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBDateTime(BookExtAttr.RecordDateTime,"文书建立时间",true,true);
				map.AddDDLEntities(BookExtAttr.Recorder,1, DataType.AppInt, "归还人",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				//map.AddTBDateTime(BookExtAttr.ReturnTime,"归还时间",true,true);
				map.AddTBString(BookExtAttr.ReturnerNote,null,"备注",true,true,0,100,5);
				map.AddDDLEntitiesPK(BookExtAttr.FK_NodeRefFunc,null, DataType.AppInt,"文书名称",new NodeRefFuncs(),NodeRefFuncAttr.OID,NodeRefFuncAttr.Name,false);


				//map.AddSearchAttr(BookExtAttr.IsReturn);
				//map.AttrsOfSearch.AddFromTo("日期",BookExtAttr.CreateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,7);
				map.AttrsOfSearch.AddFromTo("日期",BookExtAttr.RecordDateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataFormat), DA.DataType.CurrentData,7);
				//map.AddSearchAttr(BookExtAttr.FK_Node);
				//map.AddSearchAttr(BookExtAttr.FK_NodeRefFunc);
				//map.AddSearchAttr(BookExtAttr.Returner);
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
	public class BookExts :BookBases
	{
		#region 方法
		/// <summary>
		/// 根据一个工作人员的ID , 得到他能够考核的项目.
		/// </summary>
		/// <param name="empId">工作人员ID</param>
		/// <returns></returns>
		public static BookExts GetBookExtsByEmpId(int empId)
		{
			string sql=" SELECT * FROM CH_BookExt WHERE No IN ( SELECT FK_BookExt From CH_BookExtVSStation WHERE FK_Station IN  (SELECT FK_Station FROM Port_EmpStation WHERE FK_Emp="+empId+"))" ; 
			BookExts ens = new BookExts();
			ens.InitCollectionByTable(DBAccess.RunSQLReturnTable(sql)) ; 
			return ens;
		}
		#endregion 

		#region 构造方法属性
		/// <summary>
		/// BookExts
		/// </summary>
		public BookExts(){}
		 
		
		#endregion 

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BookExt();
			}
		}
		#endregion
	}
}
