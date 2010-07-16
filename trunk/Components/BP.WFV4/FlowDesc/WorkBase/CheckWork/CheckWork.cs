
using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.En;

namespace BP.WF
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum CheckState
    {
        /// <summary>
        /// 暂停
        /// </summary>
        Pause = 2,
        /// <summary>
        /// 同意
        /// </summary>
        Agree = 1,
        /// <summary>
        /// 不同意
        /// </summary>
        Dissent = 0
    }
    /// <summary>
    /// 采集信息基类属性
    /// </summary>
    public class CheckWorkAttr : WorkAttr
    {
        /// <summary>
        /// 审核状态( 0, 不同意, 1, 表示同意, 2 挂起) 
        /// </summary>
        public const string CheckState = "CheckState";
        /// <summary>
        /// NodeID
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// 参考消息
        /// </summary>
        public const string RefMsg = "RefMsg";
        /// <summary>
        /// 发送人
        /// </summary>
        public const string Sender = "Sender";
        /// <summary>
        /// FID
        /// </summary>
        public const string FID = "FID";
    }
    /// <summary>
    /// WorkBase 的摘要说明。
    /// 工作流程采集信息的 审核基类.
    /// </summary>
    abstract public class CheckWork : Work
    {
        #region 基本属性
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(CheckWorkAttr.Note);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(CheckWorkAttr.Note);
            }
            set
            {
                this.SetValByKey(CheckWorkAttr.Note, value);
            }
        }
        /// <summary>
        /// 关联的消息
        /// </summary>
        public string RefMsg
        {
            get
            {
                return this.GetValStringByKey(CheckWorkAttr.RefMsg);
            }
            set
            {
                this.SetValByKey(CheckWorkAttr.RefMsg, value);
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public CheckState HisCheckState
        {
            get
            {
                return (CheckState)this.CheckState;
            }
        }
        /// <summary>
        /// 审核状态( 0,不同意,1,同意,2挂起) 
        /// </summary>
        public CheckState CheckState
        {
            get
            {
                return (CheckState)this.GetValIntByKey(CheckWorkAttr.CheckState);
            }
            set
            {
                this.SetValByKey(CheckWorkAttr.CheckState, (int)value);
            }
        }
        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStringByKey(CheckWorkAttr.Sender);
            }
            set
            {
                this.SetValByKey(CheckWorkAttr.Sender, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 工作流程
        /// </summary>
        protected CheckWork()
        {

        }
        /// <summary>
        /// 工作流程
        /// </summary>
        /// <param name="oid">工作流程的ID</param>
        protected CheckWork(int oid) : base(oid) { }
        #endregion

        #region  重写基类的方法
        #endregion

        #region 公共方法
        #endregion
    }
    /// <summary>
    /// 审核工作的基类 集合
    /// </summary>
    abstract public class CheckWorks : Works
    {
        #region 构造方法
        /// <summary>
        /// 审核工作集合
        /// </summary>
        public CheckWorks()
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="NodeId">节点ID</param>
        /// <param name="fromDateTime">纪录日期从</param>
        /// <param name="toDateTime">到</param>
        public CheckWorks(int nodeId, string fromDateTime, string toDateTime)
        {
            this.NodeID = nodeId;
            QueryObject qo = new QueryObject(this);
            qo.Top = 100000;
            qo.AddWhere(CheckWorkAttr.RDT, ">=", fromDateTime);
            qo.addAnd();
            qo.AddWhere(CheckWorkAttr.RDT, "<=", toDateTime);
            qo.DoQuery();
        }
        public int NodeID = 0;
        #endregion


        #region 公共方法
        /// <summary>
        /// 审核节点查询 
        /// </summary>
        /// <param name="empId">工作人员(all)</param>
        /// <param name="nodeStat">节点状态</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="fromdate">记录日期从</param>
        /// <param name="todate">记录日期到</param>
        /// <returns></returns>
        public int RetrieveCheckWork(string empId, string fromdate, string todate)
        {
            QueryObject qo = new QueryObject(this);
            if (empId == "all")
            {
                qo.AddWhere(WorkAttr.Rec, " in  ", "(" + Web.WebUser.HisEmpsOfPower.ToStringOfPK(",", true) + ")");
            }
            else
            {
                qo.AddWhere(WorkAttr.Rec, empId);
            }

            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, ">=", fromdate);
            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, "<=", todate);

            return qo.DoQuery();
        }
        /// <summary>
        /// 审核节点查询 
        /// </summary>
        /// <param name="empId">工作人员(all)</param>
        /// <param name="nodeStat">节点状态</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="fromdate">记录日期从</param>
        /// <param name="todate">记录日期到</param>
        /// <returns></returns>
        public int Retrieve(string empId, int nodeId, string fromdate, string todate)
        {
            QueryObject qo = new QueryObject(this);
            if (empId == "all")
            {
                qo.AddWhere(WorkAttr.Rec, " in  ", "(" + Web.WebUser.HisEmpsOfPower.ToStringOfPK(",", true) + ")");
            }
            else
            {
                qo.AddWhere(WorkAttr.Rec, empId);
            }

            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, ">=", fromdate);
            qo.addAnd();
            qo.AddWhere(WorkAttr.RDT, "<=", todate);
            return qo.DoQuery();
        }
        #endregion
    }
}
