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
	public class BookXHXHAttr:BookBaseAttr
	{
		
	}
	/// <summary>
	/// 文书
	/// </summary> 
	public class BookXH :BookBase
	{
		 
		//private static string PTable="WF_BookXH";

		#region 构造方法
		/// <summary>
		/// 条目
		/// </summary>
		public BookXH(){}
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

				map.AddTBDateTime(BookAttr.RecordDateTime,"建立时间",true,true);
				map.AddDDLEntities(BookAttr.Recorder,1, DataType.AppInt, "归还人",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBString(BookAttr.ReturnerNote,null,"备注",true,true,0,100,5);
				map.AddTBDateTime(BookAttr.ReturnTime,"归还时间",false,true);
				map.AddTBString(BookAttr.ReturnerNote,null,"备注",true,true,0,100,5);
				map.AddTBIntPK(BookAttr.FK_NodeRefFunc,0,"文书",false,true);
				map.AddTBString(BookAttr.ReturnerDept,null,"部门",true,true,0,100,5);
				map.AddTBString(BookAttr.ReturnerZSJG,null,"征收机关",true,true,0,100,5);
				
				map.AddSearchAttr(BookAttr.IsReturn);
				map.AttrsOfSearch.AddFromTo("日期",BookAttr.RecordDateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataFormat), DA.DataType.CurrentData,7);
 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 条目
	/// </summary>
	public class BookXHs :BookBases
	{
		 

		#region 构造方法属性
		 
		 
		
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
