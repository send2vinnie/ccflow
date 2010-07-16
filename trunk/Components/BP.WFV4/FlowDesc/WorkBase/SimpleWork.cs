using System;
using System.Collections;
using BP.DA;
using BP.En;
 
 
using BP.En;

namespace BP.WF
{
	
	/// <summary>
	/// 简单的work
	/// </summary>
	abstract public class SimpleWork : Work
	{		 
		#region 构造
		/// <summary>
		/// ss
		/// </summary>
		public SimpleWork()
		{
			//this.No  = this.GenerNewNo ;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="oid"></param>
		public SimpleWork(int oid) : base(oid){}
		/// <summary>
		/// map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map(this.PhysicsTable);
				map.EnDesc=this.Desc;
				
				map.AddTBIntPK(WorkAttr.OID,0,"OID",false,true);
				map.AddDDLSysEnum(WorkAttr.NodeState,0,"节点状态",true,false,"NodeState");

				map.AddDDLEntities(WorkAttr.Rec,Web.WebUser.No,"记录人",new Port.Emps(),false);
				map.AddTBDateTime(WorkAttr.RDT,"记录日期",true,true);
				map.AddTBDateTime(WorkAttr.CDT,"完成日期",true,true);

				//map.AddTBStringDoc("Docs",null,"内容",true,false,0,500,50,20);
				map.AddTBStringDoc(WorkAttr.Note,null,"备注1",true,false);

				map.AddTBStringDoc("Note1",null,"备注2",true,false);

				map.AddTBString("FK_Taxpayer",null,"纳税人编号",false,false,0,100,100);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 		

		#region 需要子类重写的方法
		/// <summary>
		/// 指定表
		/// </summary>
		protected abstract string PhysicsTable{get;}
		/// <summary>
		/// 描述
		/// </summary>
		public abstract string Desc{get;}
		#endregion

	}
	/// <summary>
	/// SimpleWorks
	/// </summary>
	abstract public class SimpleWorks : Works
	{
		/// <summary>
		/// SimpleWorks
		/// </summary>
		public SimpleWorks()
		{}
	}
}
