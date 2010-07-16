
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.En;

namespace BP.WF
{
    /// <summary>
    /// 审核工作
    /// </summary>
    public class GECheckMulAttr : WorkAttr
    {
    }
    /// <summary>
    /// 审核工作
    /// </summary>
    public class GECheckMul : GECheckStand
    {
        #region 构造函数
        /// <summary>
        /// 审核工作
        /// </summary>
        public GECheckMul()
        {
        }
        /// <summary>
        /// 审核工作
        /// </summary>
        /// <param name="nodeid">节点ID</param>
        public GECheckMul(int nodeid)
        {
            this.NodeID = nodeid;
            this.SQLCash = null;
        }
        /// <summary>
        /// 审核工作
        /// </summary>
        /// <param name="nodeid">节点ID</param>
        /// <param name="_oid">OID</param>
        public GECheckMul(int nodeid, Int64 _oid)
        {
            this.NodeID = nodeid;
            this.OID = _oid;
            this.SQLCash = null;
        }
        #endregion
    }
    /// <summary>
    /// 审核工作s
    /// </summary>
    public class GECheckMuls : GECheckStands
    {
        #region 重载基类方法
        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeID = 0;
        #endregion

        #region 方法
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                if (this.NodeID == 0)
                    return new GECheckMul();
                return new GECheckMul(this.NodeID);
            }
        }
        /// <summary>
        /// 审核工作ID
        /// </summary>
        public GECheckMuls()
        {
        }
        /// <summary>
        /// 审核工作ID
        /// </summary>
        /// <param name="nodeid"></param>
        public GECheckMuls(int nodeid)
        {
            this.NodeID = nodeid;
        }
        #endregion
    }
}
