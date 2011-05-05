using System;
using BP.En;
using BP.En;
using BP.DA;

namespace BP.WF
{
	/// <summary>
	/// 数量审核工作类属性
	/// </summary>
    public class NumCheckAttr : CheckWorkAttr
    {
        /// <summary>
        /// 数量
        /// </summary>
        public const string Num = "Num";
    }
	/// <summary>
	/// SimpleCheckWork 的摘要说明。
	/// 数量审核工作类
	/// </summary>
    public class NumCheck : CheckWork
    {
        #region 基本属性
        /// <summary>
        /// 审核Num
        /// </summary>
        public float Num
        {
            get
            {
                return this.GetValFloatByKey(NumCheckAttr.Num);
            }
            set
            {
                this.SetValByKey(NumCheckAttr.Num, value);
            }
        }
        #endregion

        #region 构造
        /// <summary>
        /// 数量审核工作类
        /// </summary>
        public NumCheck()
        {
        }
        /// <summary>
        /// 数量审核工作类
        /// </summary>
        /// <param name="wfoid">工作流程ID</param>
        public NumCheck(int wfoid)
        {
            this.OID = wfoid;
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NumCheckAttr.OID, this.OID);
            if (qo.DoQuery() > 1)
                throw new Exception("@此工作流程上面有两个数量审核节点,不能用此方法得到审核的金额.请调用 new NumCheck(oid, nodeId) 方法 ");
        }
        /// <summary>
        /// 数量审核工作类
        /// </summary>
        /// <param name="oid">工作流程ID</param>
        /// <param name="nodeId">节点ID</param>
        public NumCheck(int oid, int nodeId)
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
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_NumCheck");
                map.EnDesc = "数量审批节点";

               // map.AddTBString(NumCheckAttr.FK_Taxpayer, null, "纳税人编号", true, true, 0, 30, 100);
               // map.AddTBString(NumCheckAttr.TaxpayerName, null, "名称", true, true, 0, 150, 100);

                map.AddDDLSysEnum(NumCheckAttr.CheckState, 1, "审核状态", true, false);
                map.AddTBInt(NumCheckAttr.Num, 0, "审核金额", true, false);

                map.AddTBStringDoc(NumCheckAttr.Note, "同意.", "审核意见", true, false);
                map.AddTBStringDoc(CheckWorkAttr.RefMsg, null, "辅助信息", true, true);

                map.AddDDLEntities(NumCheckAttr.Sender, Web.WebUser.No, "发送人", new Port.Emps(), false);
                map.AddTBDateTime(NumCheckAttr.RDT, "发送日期", true, true);

                map.AddDDLEntities(NumCheckAttr.Rec, Web.WebUser.No, "审核人", new Port.Emps(), false);
                map.AddTBInt(NumCheckAttr.NodeState, 0, "NodeState", false, true);

                map.AddTBDateTime(StandardCheckAttr.CDT, "无", "完成日期", true, true);
               // map.AddTBString(NumCheckAttr.FK_Taxpayer, null, "FK_Taxpayer", false, false, 0, 100, 100);

                map.AddTBIntPK(NumCheckAttr.OID, 0, "工作流程ID", false, true);
                map.AddTBInt(NumCheckAttr.FID, 0, "FID", false, true);

                map.AddTBIntPK(NumCheckAttr.NodeID, 0, "节点ID", false, true);
                map.AddTBString(StandardCheckAttr.Emps, null, "Emps", false, false, 0, 500, 100);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    
    }
	/// <summary>
	/// 数量审核工作类集合
	/// </summary>
    public class NumChecks : CheckWorks
    {
        #region 属性
        #endregion

        /// <summary>
        /// 标准审核
        /// </summary>
        public NumChecks()
        {
        }
        /// <summary>
        /// 生成工作节点
        /// </summary>
        /// <param name="nodeid"></param>
        public NumChecks(int nodeid)
            : base(nodeid)
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="nodeid">nodeid</param>
        /// <param name="fromDateTime">fromDateTime</param>
        /// <param name="toDateTime">toDateTime</param>
        public NumChecks(int nodeid, string fromDateTime, string toDateTime)
            : base(nodeid, fromDateTime, toDateTime)
        {
        }
        /// <summary>
        /// 工作列表s
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NumCheck();
            }
        }
    }
}
