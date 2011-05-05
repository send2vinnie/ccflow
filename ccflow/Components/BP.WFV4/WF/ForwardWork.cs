

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
	/// 转发记录
	/// </summary>
    public class ForwardWorkAttr
    {
        #region 基本属性
        /// <summary>
        /// 工作ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 节点
        /// </summary>
        public const string NodeId = "NodeId";
        /// <summary>
        /// 工作人员
        /// </summary>
        public const string Worker = "Worker";
        /// <summary>
        /// 退回原因
        /// </summary>
        public const string Note = "Note";
        public const string FK_Emp = "FK_Emp";
        public const string Emps = "Emps";
        /// <summary>
        /// 是否是收回
        /// </summary>
        public const string IsTakeBack = "IsTakeBack";
        #endregion
    }
	/// <summary>
	/// 转发记录
	/// </summary>
	public class ForwardWork : Entity
	{		
		#region 基本属性
        /// <summary>
        /// 是否是收回
        /// </summary>
        public bool IsTakeBack
        {
            get
            {
                return this.GetValBooleanByKey(ForwardWorkAttr.IsTakeBack);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.IsTakeBack, value);
            }
        }
		/// <summary>
		/// 工作ID
		/// </summary>
		public int  WorkID
		{
			get
			{
				return this.GetValIntByKey(ForwardWorkAttr.WorkID);
			}
			set
			{
				SetValByKey(ForwardWorkAttr.WorkID,value);
			}
		}
		/// <summary>
		/// NodeId
		/// </summary>
		public int  NodeId
		{
			get
			{
				return this.GetValIntByKey(ForwardWorkAttr.NodeId);
			}
			set
			{
				SetValByKey(ForwardWorkAttr.NodeId,value);
			}
		}
		public string  Note
		{
			get
			{
				return this.GetValStringByKey(ForwardWorkAttr.Note);
			}
			set
			{
				SetValByKey(ForwardWorkAttr.Note,value);
			}
		}
        /// <summary>
        /// 接受人
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStringByKey(ForwardWorkAttr.Emps);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.Emps, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(ForwardWorkAttr.FK_Emp);
            }
            set
            {
                SetValByKey(ForwardWorkAttr.FK_Emp, value);
            }
        }
        public string FK_EmpText
        {
            get
            {
                return this.GetValRefTextByKey(ForwardWorkAttr.FK_Emp);
            }
        }
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(ForwardWorkAttr.Note);
            }
        }
		#endregion 

		#region 构造函数
		/// <summary>
		/// 转发记录
		/// </summary>
		public ForwardWork(){}

        public ForwardWork(int workid, int nodeid)
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

                Map map = new Map("WF_ForwardWork");
                map.EnDesc = "转发记录";
                map.EnType = EnType.App;

                map.AddTBIntPK(ForwardWorkAttr.WorkID, 0, "工作ID", true, true);
                map.AddTBIntPK(ForwardWorkAttr.NodeId, 0, "NodeId", true, true);
                map.AddTBString(ForwardWorkAttr.Note, "", "Note", true, true, 0, 4000, 10);
                map.AddTBString(ForwardWorkAttr.Emps, "", "Emps", true, true, 0, 4000, 10);
                map.AddTBString(ForwardWorkAttr.FK_Emp, "", "FK_Emp", true, true, 0, 4000, 10);
                map.AddBoolean(ForwardWorkAttr.IsTakeBack, false, "是否是收回", true, true);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion	 
	}
	/// <summary>
	/// 转发记录s 
	/// </summary>
	public class ForwardWorks : Entities
	{	 
		#region 构造
		/// <summary>
		/// 转发记录s
		/// </summary>
		public ForwardWorks()
		{

		}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ForwardWork();
			}
		}
		#endregion
	}
	
}
