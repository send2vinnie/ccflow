
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
    public class GECheckStandAttr : WorkAttr
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
    /// 审核工作
    /// </summary>
    public class GECheckStand : GEWork
    {
        #region 基本属性
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(GECheckStandAttr.Note);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(GECheckStandAttr.Note);
            }
            set
            {
                this.SetValByKey(GECheckStandAttr.Note, value);
            }
        }
        /// <summary>
        /// 关联的消息
        /// </summary>
        public string RefMsg
        {
            get
            {
                return this.GetValStringByKey(GECheckStandAttr.RefMsg);
            }
            set
            {
                this.SetValByKey(GECheckStandAttr.RefMsg, value);
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
                return (CheckState)this.GetValIntByKey(GECheckStandAttr.CheckState);
            }
            set
            {
                this.SetValByKey(GECheckStandAttr.CheckState, (int)value);
            }
        }
        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStringByKey(GECheckStandAttr.Sender);
            }
            set
            {
                this.SetValByKey(GECheckStandAttr.Sender, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 审核工作
        /// </summary>
        public GECheckStand()
        {
        }
        /// <summary>
        /// 审核工作
        /// </summary>
        /// <param name="nodeid">节点ID</param>
        public GECheckStand(int nodeid)
        {
            this.NodeID = nodeid;
            this.SQLCash = null;
        }
        /// <summary>
        /// 审核工作
        /// </summary>
        /// <param name="nodeid">节点ID</param>
        /// <param name="_oid">OID</param>
        public GECheckStand(int nodeid, Int64 _oid)
        {
            this.NodeID = nodeid;
            this.OID = _oid;
            this.SQLCash = null;
        }
        #endregion

        #region Map
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                //if (this._enMap != null)
                //    return this._enMap;

                //   BP.Sys.MapData md = new BP.Sys.MapData();
                this._enMap = BP.Sys.MapData.GenerHisMap("ND" + this.NodeID.ToString());
                return this._enMap;
            }
        }
        /// <summary>
        /// GECheckStands
        /// </summary>
        public override Entities GetNewEntities
        {
            get
            {
                if (this.NodeID == 0)
                    return new GECheckStands();
                return new GECheckStands(this.NodeID);
            }
        }
        #endregion
    }
    /// <summary>
    /// 审核工作s
    /// </summary>
    public class GECheckStands : GEWorks
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
                    return new GECheckStand();
                return new GECheckStand(this.NodeID);
            }
        }
        /// <summary>
        /// 审核工作ID
        /// </summary>
        public GECheckStands()
        {
        }
        /// <summary>
        /// 审核工作ID
        /// </summary>
        /// <param name="nodeid"></param>
        public GECheckStands(int nodeid)
        {
            this.NodeID = nodeid;
        }
        #endregion
    }
}
