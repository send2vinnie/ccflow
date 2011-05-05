using System;
using BP.En.Base;
using BP.En;
using BP.DA;

namespace BP.WF
{
	/// <summary>
	/// 备注工作工作类属性
	/// </summary>
	public class NoteWorkAttr:CheckWorkAttr
	{
		/// <summary>
		/// 备注
		/// </summary>
		public const string Note1="Note1";
		/// <summary>
		/// 备注
		/// </summary>
		public const string Note2="Note2";
	}
	/// <summary>
	/// SimpleCheckWork 的摘要说明。
	/// 备注工作工作类
	/// </summary>
	public class NoteWork : Work
	{
		#region 基本属性
		public string FK_Taxpayer
		{
			get
			{
				return this.GetValStringByKey(NoteWorkAttr.FK_Taxpayer);
			}
			set
			{
				this.SetValByKey(NoteWorkAttr.FK_Taxpayer,value);
			}
		}
		/// <summary>
		/// NodeID
		/// </summary>
		public int NodeID
		{
			get
			{
				return this.GetValIntByKey(NoteWorkAttr.NodeID);
			}
			set
			{
				this.SetValByKey(NoteWorkAttr.NodeID,value);
			}
		}
		#endregion

		#region 构造
		/// <summary>
		/// 备注工作工作类
		/// </summary>
		public NoteWork()
		{
		}
		/// <summary>
		/// 备注工作工作类
		/// </summary>
		/// <param name="wfoid">工作流程ID</param>
		public NoteWork(int wfoid)
		{
			this.OID=wfoid;
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(NoteWorkAttr.OID,this.OID) ;
			if (qo.DoQuery() > 1)
				throw new Exception("@此工作流程上面有两个备注工作节点,不能用此方法得到审核的金额.请调用 new NoteWork(oid, nodeId) 方法 ");
		}
		/// <summary>
		/// 备注工作工作类
		/// </summary>
		/// <param name="oid">工作流程ID</param>
		/// <param name="nodeId">节点ID</param>
		public NoteWork(int oid, int nodeId)
		{
			this.OID = oid;
			this.NodeID = nodeId;
			this.Retrieve();
		}
		/// <summary>
		/// 属性
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("WF_NoteWork");
				map.EnDesc="备注节点";

				map.AddDDLEntities(NoteWorkAttr.Sender,Web.WebUser.No,"发送人",new En.Emps(),false);
				map.AddTBDateTime(NoteWorkAttr.RDT,"发送日期",true,true);


				map.AddTBStringDoc(NoteWorkAttr.Note1,null,"备注1",true,false);
				map.AddTBStringDoc(NoteWorkAttr.Note2,null,"备注2",true,true);

				
				map.AddDDLEntities(NoteWorkAttr.Recorder,Web.WebUser.No,"记录人",new En.Emps(),false);
				map.AddTBInt(NoteWorkAttr.NodeState,0,"NodeState",false,true);

				map.AddTBDateTime(StandardCheckAttr.CDT,"无","完成日期",true,true);
				map.AddTBString(NoteWorkAttr.FK_Taxpayer,null,"FK_Taxpayer",true,false,0,100,100);

				map.AddTBIntPK(NoteWorkAttr.OID,0,"工作流程ID",false,true);
				map.AddTBIntPK(NoteWorkAttr.NodeID,0,"节点ID",false,true);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 备注工作工作类集合
	/// </summary>
	public class NoteWorks :Works
	{
		#region 属性
		/// <summary>
		/// _nodeId
		/// </summary>
		public int _nodeId=0;
		/// <summary>
		/// _flowNo
		/// </summary>
		public string _flowNo="";
		/// <summary>
		/// 节点ID
		/// </summary>
		public override int NodeId
		{
			get
			{
				return this._nodeId;
			}			 
		}
		/// <summary>
		/// 工作流程编号
		/// </summary>
		public override string FlowNo
		{
			get
			{
				
				return this._flowNo;
			}
		}
		#endregion

		/// <summary>
		/// 标准审核
		/// </summary>
		public NoteWorks()
		{
		}
		/// <summary>
		/// 工作列表s
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new NoteWork();
			}
		}
	}
}
