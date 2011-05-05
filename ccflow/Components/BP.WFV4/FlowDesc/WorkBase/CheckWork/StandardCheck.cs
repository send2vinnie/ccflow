using System;
using BP.En;
using BP.En;
using BP.DA;
using BP.Web;

namespace BP.WF
{
	/// <summary>
	/// 标准的审核工作类属性
	/// </summary>
    public class GECheckStandAttr : GECheckStandAttr
    {

    }
	/// <summary>
	/// SimpleGECheckStand 的摘要说明。
	/// 标准的审核工作类
	/// </summary>
    public class GECheckStand :GECheckStand
    {
        #region 构造
        /// <summary>
        /// 标准的审核工作类
        /// </summary>
        public GECheckStand()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public GECheckStand(int nodeid)
        {
            this.NodeID = nodeid;
        }
        /// <summary>
        /// 标准的审核工作类
        /// </summary>
        /// <param name="oid"></param>
        public GECheckStand(Int64 workid, int fk_node)
        {
            this.NodeID = fk_node;
            this.OID = workid;

            this.MyPK = fk_node + "_" + workid;
            this.Retrieve();
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            this.MyPK = this.NodeID + "_" + this.OID;
            return base.beforeInsert();
        }
        /// <summary>
        /// 属性
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_GECheckStand");
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "标准审批节点";

                map.AddMyPK();

                map.AddTBInt(NumCheckAttr.NodeID, 0, "节点ID", false, true);
                map.AddTBInt(NumCheckAttr.OID, 0, "工作ID", false, true);
                map.AddTBInt(NumCheckAttr.FID, 0, "FID", false, true);

             
                map.AddDDLEntities(NumCheckAttr.Sender, null, "发送人", new Port.Emps(), false);
                map.AddDDLSysEnum(NumCheckAttr.CheckState, 1, "审核状态", true, false);

                map.AddTBStringDoc(NumCheckAttr.Note, "同意.", "审核意见", true, false);
                map.AddTBStringDoc(GECheckStandAttr.RefMsg, null, "辅助信息", true, true);

                map.AddTBDateTime(NumCheckAttr.RDT, "发送日期", true, true);
                map.AddDDLEntities(GECheckStandAttr.Rec, null, "审核人", new Port.Emps(), false);

                map.AddTBDateTime(NumCheckAttr.RDT, "记录日期", false, true);
                map.AddTBInt(NumCheckAttr.NodeState, 0, "NodeState", false, true);

                map.AddTBDateTime(GECheckStandAttr.CDT, "完成日期", false, true);
          
                map.AddTBString(GECheckStandAttr.Emps, null, "Emps", false, false, 0, 500, 100);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public override int RetrieveFromDBSources()
        {
            this.MyPK = this.NodeID + "_" + this.OID;
            return base.RetrieveFromDBSources();
        }
    }
	/// <summary>
	/// 标准的审核工作类集合
	/// </summary>
    public class GECheckStands : GECheckStands
	{
		#region 构造
		/// <summary>
		/// 标准审核
		/// </summary>
		public GECheckStands()
		{
		}
		/// <summary>
		/// GECheckStands
		/// </summary>
		/// <param name="nodeid"></param>
		public GECheckStands(int nodeid):base(nodeid)
		{
		}
		/// <summary>
		/// GECheckStands
		/// </summary>
		/// <param name="nodeid">nodeid</param>
		/// <param name="from">from</param>
		/// <param name="to">to</param>
		public GECheckStands(int nodeid,string from ,string to):base(nodeid,from,to)
		{
		}
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                if (this.NodeID == 0)
                    return new GECheckStand();
                return new GECheckStand(this.NodeID);
            }
        }
		#endregion
	}
}
