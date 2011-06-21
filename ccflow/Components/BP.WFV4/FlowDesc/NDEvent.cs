using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
using BP.Web;

namespace BP.WF
{
    public enum EventDoType
    {
        /// <summary>
        /// 禁用
        /// </summary>
        Disable,
        /// <summary>
        /// 执行SQL
        /// </summary>
        SQL,
        /// <summary>
        /// 执行URL
        /// </summary>
        URL
    }
    /// <summary>
    /// 事件标记列表
    /// </summary>
    public class EventListOfNode
    {
        #region 节点事件
        /// <summary>
        /// 当节点保存前
        /// </summary>
        public const string SaveBefore = "SaveBefore";
        /// <summary>
        /// 当节点保存后
        /// </summary>
        public const string SaveAfter = "SaveAfter";
        /// <summary>
        /// 节点发送前
        /// </summary>
        public const string SendWhen = "SendWhen";
        /// <summary>
        /// 节点发送成功
        /// </summary>
        public const string SendSuccess = "SendSuccess";
        /// <summary>
        /// 节点发送失败
        /// </summary>
        public const string SendError = "SendError";
        /// <summary>
        /// 当节点退回前
        /// </summary>
        public const string ReturnBefore = "ReturnBefore";
        /// <summary>
        /// 当节点退后
        /// </summary>
        public const string ReturnAfter = "ReturnAfter";
        /// <summary>
        /// 当节点撤销发送前
        /// </summary>
        public const string UndoneBefore = "UndoneBefore";
        /// <summary>
        /// 当节点撤销发送后
        /// </summary>
        public const string UndoneAfter = "UndoneAfter";
        #endregion 节点事件

        #region 流程事件
        /// <summary>
        /// 流程完成时
        /// </summary>
        public const string WhenFlowOver = "WhenFlowOver";
        /// <summary>
        /// 流程删除前
        /// </summary>
        public const string BeforeFlowDel = "BeforeFlowDel";
        /// <summary>
        /// 流程删除后
        /// </summary>
        public const string AfterFlowDel = "AfterFlowDel";
        #endregion 流程事件
    }
	/// <summary>
	/// 事件属性
	/// </summary>
    public class NDEventAttr
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public const string FK_Event = "FK_Event";
        /// <summary>
        /// 节点ID
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// 执行类型
        /// </summary>
        public const string DoType = "DoType";
        /// <summary>
        /// 执行内容
        /// </summary>
        public const string DoDoc = "DoDoc";
        /// <summary>
        /// 标签
        /// </summary>
        public const string MsgOK = "MsgOK";
        /// <summary>
        /// 执行错误提示
        /// </summary>
        public const string MsgErr = "MsgErr";
        /// <summary>
        /// FK_Flow
        /// </summary>
        public const string FK_Flow = "FK_Flow";
    }
	/// <summary>
	/// 事件
	/// 节点的节点保存事件有两部分组成.	 
	/// 记录了从一个节点到其他的多个节点.
	/// 也记录了到这个节点的其他的节点.
	/// </summary>
    public class NDEvent : EntityMyPK
    {
        #region 基本属性
        public override En.UAC HisUAC
        {
            get
            {
                UAC uac = new En.UAC();
                uac.IsAdjunct = false;
                uac.IsDelete = false;
                uac.IsInsert = false;
                uac.IsUpdate = true;
                return uac;
            }
        }
        /// <summary>
        /// 节点
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(NDEventAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(NDEventAttr.FK_Node, value);
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.GetValStrByKey(NDEventAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(NDEventAttr.FK_Flow, value);
            }
        }
        public string DoDoc
        {
            get
            {
                return this.GetValStringByKey(NDEventAttr.DoDoc);
            }
            set
            {
                this.SetValByKey(NDEventAttr.DoDoc, value);
            }
        }
        /// <summary>
        /// 执行成功提示
        /// </summary>
        public string MsgOK
        {
            get
            {
                return this.GetValStringByKey(NDEventAttr.MsgOK);
            }
            set
            {
                this.SetValByKey(NDEventAttr.MsgOK, value);
            }
        }
        /// <summary>
        /// 错误提示
        /// </summary>
        public string MsgError
        {
            get
            {
                return this.GetValStringByKey(NDEventAttr.MsgErr);
            }
            set
            {
                this.SetValByKey(NDEventAttr.MsgErr, value);
            }
        }
        public string FK_Event
        {
            get
            {
                return this.GetValStringByKey(NDEventAttr.FK_Event);
            }
            set
            {
                this.SetValByKey(NDEventAttr.FK_Event, value);
            }
        }
        /// <summary>
        /// 执行类型
        /// </summary>
        public EventDoType HisDoType
        {
            get
            {
                return (EventDoType)this.GetValIntByKey(NDEventAttr.DoType);
            }
            set
            {
                this.SetValByKey(NDEventAttr.DoType, (int)value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 事件
        /// </summary>
        public NDEvent()
        {
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

                Map map = new Map("WF_Event");
                map.EnDesc = "事件";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.AddMyPK();

                map.AddTBString(NDEventAttr.FK_Event, null, "事件", true, true, 0, 400, 10);

                map.AddTBString(NDEventAttr.FK_Flow, null, "FK_Flow", true, true, 0, 3, 4);
                map.AddTBInt(NDEventAttr.FK_Node, 0, "FK_Node", true, true);

                map.AddTBInt(NDEventAttr.DoType, 0, "程序类型(Disable/SQL/URL)", true, true);
                map.AddTBString(NDEventAttr.DoDoc, null, "执行内容", true, true, 0, 400, 10);
                map.AddTBString(NDEventAttr.MsgOK, null, "提示", true, true, 0, 400, 10);
                map.AddTBString(NDEventAttr.MsgErr, null, "提示", true, true, 0, 400, 10);


                //map.AddTBString(NDEventAttr.SaveBefore, null, "当节点保存前", true, false, 0, 400, 10,true);
                //map.AddTBString(NDEventAttr.SaveAfter, null, "当节点保存后", true, false, 0, 400, 10, true);

                //map.AddTBString(NDEventAttr.SendWhen, null, "当节点发送时", true, false, 0, 400, 10, true);
                //map.AddTBString(NDEventAttr.SendSuccess, null, "节点发送成功", true, false, 0, 400, 10, true);
                //map.AddTBString(NDEventAttr.SendError, null, "节点发送失败", true, false, 0, 400, 10, true);

                //map.AddTBString(NDEventAttr.ReturnBefore, null, "当节点退回前", true, false, 0, 400, 10, true);
                //map.AddTBString(NDEventAttr.ReturnAfter, null, "当节点退回后", true, false, 0, 400, 10, true);

                //map.AddTBString(NDEventAttr.UndoneBefore, null, "当节点撤销发送前", true, false, 0, 400, 10, true);
                //map.AddTBString(NDEventAttr.UndoneAfter, null, "当节点撤销发送后", true, false, 0, 400, 10, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        protected override bool beforeUpdateInsertAction()
        {
            Node nd = new Node(this.FK_Node);
            this.FK_Flow = nd.FK_Flow;

            this.MyPK = this.FK_Node + "_" + this.FK_Event;
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
	/// 事件
	/// </summary>
    public class NDEvents : EntitiesOID
    {
        /// <summary>
        /// 执行事件，事件标记是 EventList.
        /// </summary>
        /// <param name="dotype"></param>
        /// <param name="wk"></param>
        /// <returns></returns>
        public string DoEventNode(string dotype, Work wk)
        {
            if (this.Count == 0)
                return null;

            NDEvent nev = this.GetEntityByKey(NDEventAttr.FK_Event, dotype) as NDEvent;
            if (nev == null || nev.HisDoType == EventDoType.Disable)
                return null;

            string doc = nev.DoDoc.Trim();
            if (doc == null || doc == "")
                return null;

            #region 处理执行内容
            Attrs attrs = wk.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (doc.Contains("@" + attr.Key) == false)
                    continue;
                if (attr.MyDataType == DataType.AppString)
                    doc = doc.Replace("@" + attr.Key, "'" + wk.GetValStrByKey(attr.Key) + "'");
                else
                    doc = doc.Replace("@" + attr.Key, wk.GetValStrByKey(attr.Key));
            }

            //doc = doc.Replace("@FK_Node", nev.FK_Node.ToString());
            //doc = doc.Replace("@WorkID", wk.OID.ToString());
            //doc = doc.Replace("@FID", wk.OID.ToString());
            //doc = doc.Replace("@UserNo", BP.Web.WebUser.No);
            //doc = doc.Replace("@UserName", BP.Web.WebUser.Name);
            //doc = doc.Replace("@SID", BP.Web.WebUser.SID);
            //doc = doc.Replace("@UserDept", BP.Web.WebUser.FK_Dept);
            // doc += "&FK_Flow=" + nev.FK_Node.ToString();

            if (nev.HisDoType != EventDoType.SQL)
            {
                doc += "&FK_Flow=" + nev.FK_Flow.ToString();
                doc += "&FK_Node=" + nev.FK_Node.ToString();
                doc += "&WorkID=" + wk.OID.ToString();
                doc += "&FID=" + wk.FID.ToString();
                doc += "&UserNo=" + WebUser.No;
                doc += "&SID=" + WebUser.SID;
                doc += "&FK_Dept=" + WebUser.FK_Dept;
                doc += "&FK_Unit=" + WebUser.FK_Unit;
            }
            #endregion 处理执行内容

            switch (nev.HisDoType)
            {
                case EventDoType.SQL:
                    try
                    {
                        DBAccess.RunSQL(doc);
                        return "@" + nev.MsgOK + " -执行成功。";
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@" + nev.MsgOK + " Error:" + ex.Message);
                    }
                    break;
                case EventDoType.URL:
                    doc = doc.Replace("@AppPath", System.Web.HttpContext.Current.Request.ApplicationPath);
                    try
                    {
                        string text = DataType.ReadURLContext(doc, 800, System.Text.Encoding.UTF8);
                        if (text != null && text.Substring(0, 7).Contains("Err"))
                            throw new Exception(text);

                        Log.DebugWriteInfo(doc + " ------ " + text);
                        return "@" + nev.MsgOK + text;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@" + nev.MsgError + ex.Message);
                    }
                    break;
                default:
                    throw new Exception("@no such way.");
            }
        }
        /// <summary>
        /// 事件
        /// </summary>
        public NDEvents() { }
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="fk_node">节点</param>
        public NDEvents(int fk_node)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(NDEventAttr.FK_Node, fk_node);
            qo.DoQuery();
        }
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new NDEvent();
            }
        }
    }
}
