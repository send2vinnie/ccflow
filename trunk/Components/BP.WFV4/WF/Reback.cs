

using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.WF;
using BP.Tax; 
using BP.Port; 
using BP.En;


namespace BP.WF
{
	/// <summary>
	/// 撤消记录
	/// </summary>
    public class RebackAttr
    {
        #region 基本属性
        /// <summary>
        /// 工作ID
        /// </summary>
        public const string WorkId = "WorkId";
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
        #endregion
    }
	/// <summary>
	/// 撤消记录
	/// </summary>
	public class Reback : Entity
	{		
		#region 基本属性
		/// <summary>
		/// 工作ID
		/// </summary>
		public int  WorkId
		{
			get
			{
				return this.GetValIntByKey(RebackAttr.WorkId);
			}
			set
			{
				SetValByKey(RebackAttr.WorkId,value);
			}
		}
		/// <summary>
		/// NodeId
		/// </summary>
		public int  NodeId
		{
			get
			{
				return this.GetValIntByKey(RebackAttr.NodeId);
			}
			set
			{
				SetValByKey(RebackAttr.NodeId,value);
			}
		}
		public string  Note
		{
			get
			{
				return this.GetValStringByKey(RebackAttr.Note);
			}
			set
			{
				SetValByKey(RebackAttr.Note,value);
			}
		}
        /// <summary>
        /// 接受人
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStringByKey(RebackAttr.Emps);
            }
            set
            {
                SetValByKey(RebackAttr.Emps, value);
            }
        }
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(RebackAttr.FK_Emp);
            }
            set
            {
                SetValByKey(RebackAttr.FK_Emp, value);
            }
        }
        public string FK_EmpText
        {
            get
            {
                return this.GetValRefTextByKey(RebackAttr.FK_Emp);
            }
        }
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(RebackAttr.Note);
            }
        }
		#endregion 

		#region 构造函数
		/// <summary>
		/// 撤消记录
		/// </summary>
		public Reback(){}

		public Reback(int workid, int nodeid)
		{
			this.WorkId = workid;
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

                Map map = new Map("WF_Reback");
                map.EnDesc = "撤消记录";
                map.EnType = EnType.App;

                map.AddTBIntPK(RebackAttr.WorkId, 0, "工作ID", true, true);
                map.AddTBIntPK(RebackAttr.NodeId, 0, "NodeId", true, true);
                map.AddTBString(RebackAttr.FK_Emp, "", "FK_Emp", true, true, 0, 4000, 10);
                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion	 
	}
	/// <summary>
	/// 撤消记录s 
	/// </summary>
	public class Rebacks : Entities
	{	 
		#region 构造
		/// <summary>
		/// 撤消记录s
		/// </summary>
		public Rebacks()
		{

		}
		/// <summary>
		/// 得到它的 Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Reback();
			}
		}
		#endregion
	}
	
}
