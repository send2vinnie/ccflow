using System;
using BP.En;
using BP.En;
using BP.DA;
using BP.Web;

namespace BP.WF
{
    /// <summary>
    /// 会签审核节点属性
    /// </summary>
    public class MulCheckAttr : CheckWorkAttr
    {
    }
    /// <summary>
    /// 会签审核节点
    /// </summary>
    public class MulCheck : CheckWork
    {
        #region 构造

        /// <summary>
        /// 会签审核节点
        /// </summary>
        public MulCheck()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public MulCheck(int nodeid)
        {
            this.NodeID = nodeid;
        }
        /// <summary>
        /// 会签审核节点
        /// </summary>
        /// <param name="oid"></param>
        public MulCheck(Int64 workid, int fk_node, string fk_emp)
        {
            this.NodeID = fk_node;
            this.OID = workid;
            this.Rec = fk_emp;
            this.Retrieve();
        }
        #endregion
    }
    /// <summary>
    /// 会签审核节点集合
    /// </summary>
    public class MulChecks : CheckWorks
    {
        #region 构造
        /// <summary>
        /// 标准审核
        /// </summary>
        public MulChecks()
        {
        }
        /// <summary>
        /// MulChecks
        /// </summary>
        /// <param name="nodeid"></param>
        public MulChecks(int nodeid)
            : base(nodeid)
        {
        }
        /// <summary>
        /// MulChecks
        /// </summary>
        /// <param name="nodeid">nodeid</param>
        /// <param name="from">from</param>
        /// <param name="to">to</param>
        public MulChecks(int nodeid, string from, string to)
            : base(nodeid, from, to)
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
                    return new MulCheck();
                return new MulCheck(this.NodeID);
            }
        }
        #endregion
    }
}
