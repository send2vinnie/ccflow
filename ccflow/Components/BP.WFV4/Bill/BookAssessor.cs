using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.WF
{
	 
	/// <summary>
	/// 文书审核属性
	/// </summary>
	public class BookAssessorAttr
	{
		#region 属性 
		/// <summary>
		/// 流程
		/// </summary>
		public const string FK_Flow="FK_Flow";
		/// <summary>
		/// 工作ID
		/// </summary>
		public const string WorkID="WorkID";
		/// <summary>
		/// 节点
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// 审核人
		/// </summary>
		public const string Assessor="Assessor";
		/// <summary>
		/// 审核状态
		/// </summary>
		public const string AssessorState="AssessorState";
		/// <summary>
		/// 审核意见
		/// </summary>
		public const string Note="Note";
		/// <summary>
		/// 记录时间
		/// </summary>
		public const string RecorderDateTime="RecorderDateTime";
		/// <summary>
		/// 产生日期
		/// </summary>
		public const string GenerDateTime="GenerDateTime";
		#endregion		
	}
	/// <summary>
	/// 文书审核
	/// </summary> 
	public class BookAssessor :EntityOID
	{
		#region 基本属性		 
		/// <summary>
		///  WorkID
		/// </summary>
		public int WorkID
		{
			get
			{
				return GetValIntByKey(BookAssessorAttr.WorkID);
			}
			set
			{
				SetValByKey(BookAssessorAttr.WorkID,value);
			}
		}
		/// <summary>
		///  FK_Node
		/// </summary>
		public int FK_Node
		{
			get
			{
				return GetValIntByKey(BookAssessorAttr.FK_Node);
			}
			set
			{
				SetValByKey(BookAssessorAttr.FK_Node,value);
			}
		}
		 
		/// <summary>
		///  管理
		/// </summary>
		public int Assessor
		{
			get
			{
				return GetValIntByKey(BookAssessorAttr.Assessor);
			}
			set
			{
				SetValByKey(BookAssessorAttr.Assessor,value);
			}
		}
		/// <summary>
		/// 流程标题
		/// </summary>
		public string  Note
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.Note);
			}
			set
			{
				SetValByKey(BookAssessorAttr.Note,value);
			}
		}
		/// <summary>
		/// 建立时间。
		/// </summary>
		public string  CreateTime
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.CreateTime);
			}
			set
			{
				SetValByKey(BookAssessorAttr.CreateTime,value);
			}
		}
		/// <summary>
		/// 部门。
		/// </summary>
		public string  ReturnerDept
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.ReturnerDept);
			}
			set
			{
				SetValByKey(BookAssessorAttr.ReturnerDept,value);
			}
		}
		/// <summary>
		/// 征收机关。
		/// </summary>
		public string  ReturnerZSJG
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.ReturnerZSJG);
			}
			set
			{
				SetValByKey(BookAssessorAttr.ReturnerZSJG,value);
			}
		}
		/// <summary>
		///  Returner
		/// </summary>
		public int Returner
		{
			get
			{
				return GetValIntByKey(BookAssessorAttr.Returner);
			}
			set
			{
				SetValByKey(BookAssessorAttr.Returner,value);
			}
		}
		 
		/// <summary>
		/// 归还人备注
		/// </summary>
		public string  ReturnerNote
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.ReturnerNote);
			}
			set
			{
				SetValByKey(BookAssessorAttr.ReturnerNote,value);
			}
		}
		/// <summary>
		/// 规划日期
		/// </summary>
		public string  ReturnTime
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.ReturnTime);
			}
			set
			{
				SetValByKey(BookAssessorAttr.ReturnTime,value);
			}
		}
		#endregion 

	 

		#region 构造方法
		/// <summary>
		/// 条目
		/// </summary>
		public BookAssessor(){}


		 
		public BookAssessor(int nodeId,int workId)
		{

		}


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
				Map map = new Map( BookAssessor.PTable );
				map.DepositaryOfMap=Depositary.None;

				map.EnDesc="文书审核";                
				map.IsDelete=true;
				map.IsInsert=true;
				map.IsUpdate=true;
				map.IsView=true;
				map.AddTBIntPKOID();

				map.AddTBIntPK(BookAssessorAttr.WorkID,0,"工作ID",true,true);
			
				map.AddBoolean(BookAssessorAttr.IsReturn,false,"归还否",true,true);
				map.AddTBString(BookAssessorAttr.FlowTitle,null,"流程标题",true,true,0,100,5);
				//map.AddDDLEntities(BookAssessorAttr.FK_Node,null, DataType.AppInt,"节点",new Nodes(),NodeAttr.OID,NodeAttr.Name,false);
				//map.AddDDLEntities(BookAssessorAttr.Admin,1, DataType.AppInt,"管理员",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBInt(BookAssessorAttr.Admin,1,"管理员",false,false);

				map.AddTBDateTime(BookAssessorAttr.CreateTime,"建立时间",true,true);
				map.AddDDLEntities(BookAssessorAttr.Returner,1, DataType.AppInt, "归还人",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBString(BookAssessorAttr.ReturnerNote,null,"备注",true,true,0,100,5);
				map.AddTBDateTime(BookAssessorAttr.ReturnTime,"归还时间",false,true);
				map.AddTBString(BookAssessorAttr.ReturnerNote,null,"备注",true,true,0,100,5);
				map.AddTBIntPK(BookAssessorAttr.FK_NodeRefFunc,0,"文书审核",false,true);
				map.AddTBString(BookAssessorAttr.ReturnerDept,null,"部门",true,true,0,100,5);
				map.AddTBString(BookAssessorAttr.ReturnerZSJG,null,"征收机关",true,true,0,100,5);

				//map.AddDDLEntitiesPK(BookAssessorAttr.FK_NodeRefFunc,null, DataType.AppInt,"文书审核名称",new NodeRefFuncs(),NodeRefFuncAttr.OID,NodeRefFuncAttr.Name,false);
				
				map.AddSearchAttr(BookAssessorAttr.IsReturn);
				//map.AttrsOfSearch.AddFromTo("日期",BookAssessorAttr.CreateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,7);
				map.AttrsOfSearch.AddFromTo("日期",BookAssessorAttr.CreateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataFormat), DA.DataType.CurrentData,7);

				//map.AddSearchAttr(BookAssessorAttr.FK_Node);
				//map.AddSearchAttr(BookAssessorAttr.FK_NodeRefFunc);
				//map.AddSearchAttr(BookAssessorAttr.Returner);
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
	public class BookAssessors :EntitiesOID
	{
		 

		#region 构造方法属性
		/// <summary>
		/// BookAssessors
		/// </summary>
		public BookAssessors(){}
		#endregion

		#region 属性
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BookAssessor();
			}
		}
		#endregion
	}
}
