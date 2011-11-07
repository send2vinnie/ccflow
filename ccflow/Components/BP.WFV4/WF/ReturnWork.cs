using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port; 

namespace BP.WF
{
	/// <summary>
	/// 退回轨迹
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
		public const  string FK_Node="FK_Node";
		/// <summary>
		/// 工作人员
		/// </summary>
		public const  string Worker="Worker";
		/// <summary>
		/// 退回原因
		/// </summary>
		public const  string Note="Note";
        /// <summary>
        /// 退回日期
        /// </summary>
        public const string RDT = "RDT";

        /// <summary>
        /// 退回人
        /// </summary>
        public const string Returner = "Returner";
		#endregion
	}
	/// <summary>
	/// 退回轨迹
	/// </summary>
    public class ReturnWork : EntityMyPK
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
                SetValByKey(ReturnWorkAttr.WorkID, value);
            }
        }
        /// <summary>
        /// FK_Node
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(ReturnWorkAttr.FK_Node);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// 退回人
        /// </summary>
        public string Returner
        {
            get
            {
                return this.GetValStringByKey(ReturnWorkAttr.Returner);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.Returner, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(ReturnWorkAttr.Note);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.Note, value);
            }
        }
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(ReturnWorkAttr.Note);
            }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(ReturnWorkAttr.RDT);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.RDT, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 退回轨迹
        /// </summary>
        public ReturnWork() { }
        /// <summary>
        /// 退回轨迹
        /// </summary>
        /// <param name="workid"></param>
        /// <param name="FK_Node"></param>
        public ReturnWork(int workid, int FK_Node)
        {
            this.WorkID = workid;
            this.FK_Node = FK_Node;
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
                map.EnDesc = this.ToE("ReturnRec", "退回轨迹");
                map.EnType = EnType.App;

                map.AddMyPK();

                map.AddTBInt(ReturnWorkAttr.WorkID, 0, "WorkID", true, true);
                map.AddTBInt(ReturnWorkAttr.FK_Node, 0, "FK_Node", true, true);
                map.AddTBString(ReturnWorkAttr.Note, "", "Note", true, true, 0, 4000, 10);

                map.AddTBString(ReturnWorkAttr.Returner, null, "退回人", true, true, 0, 4000, 10);
                map.AddTBDateTime(ReturnWorkAttr.RDT, null, "退回日期", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        protected override bool beforeInsert()
        {
            this.Returner = BP.Web.WebUser.No;
            this.RDT =DataType.CurrentDataTime;
            return base.beforeInsert();
        }
    }
	/// <summary>
	/// 退回轨迹s 
	/// </summary>
	public class ReturnWorks : Entities
	{	 
		#region 构造
		/// <summary>
		/// 退回轨迹s
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
