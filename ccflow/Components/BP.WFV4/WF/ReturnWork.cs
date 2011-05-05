

using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port; 
using BP.Port; 
using BP.En;


namespace BP.WF
{
	/// <summary>
	/// 退回记录
	/// </summary>
	public class ReturnWorkAttr 
	{
		#region 基本属性
		/// <summary>
		/// 工作ID
		/// </summary>
		public const  string WorkID="WorkID";
		/// <summary>
		/// 节点
		/// </summary>
		public const  string NodeId="NodeId";
		/// <summary>
		/// 工作人员
		/// </summary>
		public const  string Worker="Worker";
		/// <summary>
		/// 退回原因
		/// </summary>
		public const  string Note="Note";
		#endregion
	}
	/// <summary>
	/// 退回记录
	/// </summary>
	public class ReturnWork : Entity
	{		
		#region 基本属性
		/// <summary>
		/// 工作ID
		/// </summary>
        public Int64 WorkID
		{
			get
			{
				return this.GetValInt64ByKey(ReturnWorkAttr.WorkID);
			}
			set
			{
				SetValByKey(ReturnWorkAttr.WorkID,value);
			}
		}
		/// <summary>
		/// NodeId
		/// </summary>
		public int  NodeId
		{
			get
			{
				return this.GetValIntByKey(ReturnWorkAttr.NodeId);
			}
			set
			{
				SetValByKey(ReturnWorkAttr.NodeId,value);
			}
		}
		public string  Note
		{
			get
			{
				return this.GetValStringByKey(ReturnWorkAttr.Note);
			}
			set
			{
				SetValByKey(ReturnWorkAttr.Note,value);
			}
		}
		public string  NoteHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(ReturnWorkAttr.Note);
			}
		}
		#endregion 

		#region 构造函数
		/// <summary>
		/// 退回记录
		/// </summary>
		public ReturnWork(){}

		public ReturnWork(int workid, int nodeid)
		{
			this.WorkID = workid;
			this.NodeId = nodeid;
			this.Retrieve();
		}
		/// <summary>
		/// 重写基类方法
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_ReturnWork");
                map.EnDesc = this.ToE("ReturnRec", "退回记录"); // "退回记录";
                map.EnType = EnType.App;

                map.AddTBIntPK(ReturnWorkAttr.WorkID, 0, "WorkID", true, true);
                map.AddTBIntPK(ReturnWorkAttr.NodeId, 0, "NodeId", true, true);
                map.AddTBString(ReturnWorkAttr.Note, "", "Note", true, true, 0, 4000, 10);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion	 
	}
	/// <summary>
	/// 退回记录s 
	/// </summary>
	public class ReturnWorks : Entities
	{	 
		#region 构造
		/// <summary>
		/// 退回记录s
		/// </summary>
		public ReturnWorks()
		{

		}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ReturnWork();
			}
		}
		#endregion
	}
	
}
