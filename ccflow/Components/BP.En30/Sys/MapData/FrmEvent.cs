using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
using BP.Web;

namespace BP.Sys
{
    public enum EventDoType
    {
        /// <summary>
        /// 禁用
        /// </summary>
        Disable,
        /// <summary>
        /// 执行存储过程.
        /// </summary>
        SP,
        /// <summary>
        /// 执行SQL
        /// </summary>
        SQL,
        /// <summary>
        /// 执行URL
        /// </summary>
        URL,
        /// <summary>
        /// WebServices
        /// </summary>
        WebServices,
        /// <summary>
        /// EXE
        /// </summary>
        EXE,
        /// <summary>
        /// JS
        /// </summary>
        Javascript
    }
    public class EventListFrm
    {
        /// <summary>
        /// 载入前
        /// </summary>
        public const string FrmLoadBefore = "FrmLoadBefore";
        /// <summary>
        /// 载入后
        /// </summary>
        public const string FrmLoadAfter = "FrmLoadAfter";
        /// <summary>
        /// 保存前
        /// </summary>
        public const string SaveBefore = "SaveBefore";
        /// <summary>
        /// 保存后
        /// </summary>
        public const string SaveAfter = "SaveAfter";
    }
    /// <summary>
    /// 事件标记列表
    /// </summary>
    public class EventListOfNode : EventListFrm
    {
        #region 节点事件
      
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
    public class FrmEventAttr
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public const string FK_Event = "FK_Event";
        /// <summary>
        /// 节点ID
        /// </summary>
        public const string FK_MapData = "FK_MapData";
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
        public const string MsgError = "MsgError";
    }
	/// <summary>
	/// 事件
	/// 节点的节点保存事件有两部分组成.	 
	/// 记录了从一个节点到其他的多个节点.
	/// 也记录了到这个节点的其他的节点.
	/// </summary>
    public class FrmEvent : EntityMyPK
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
        public string FK_MapData
        {
            get
            {
                return this.GetValStringByKey(FrmEventAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(FrmEventAttr.FK_MapData, value);
            }
        }
        public string DoDoc
        {
            get
            {
                return this.GetValStringByKey(FrmEventAttr.DoDoc).Replace("~","'");
            }
            set
            {
                string doc = value.Replace("'","~");
                this.SetValByKey(FrmEventAttr.DoDoc, doc);
            }
        }
        /// <summary>
        /// 执行成功提示
        /// </summary>
        public string MsgOK(Entity en)
        {
            string val = this.GetValStringByKey(FrmEventAttr.MsgOK);
            if (val.Trim() == "")
                return null;

            if (val.IndexOf('@') == -1)
                return val;

            foreach (Attr attr in en.EnMap.Attrs)
            {
                val = val.Replace("@" + attr.Key, en.GetValStringByKey(attr.Key));
            }
            return val;
        }
        public string MsgOKString
        {
            get
            {
                return this.GetValStringByKey(FrmEventAttr.MsgOK);
            }
            set
            {
                this.SetValByKey(FrmEventAttr.MsgOK, value);
            }
        }
        public string MsgErrorString
        {
            get
            {
                return this.GetValStringByKey(FrmEventAttr.MsgError);
            }
            set
            {
                this.SetValByKey(FrmEventAttr.MsgError, value);
            }
        }
        /// <summary>
        /// 错误或异常提示
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public string MsgError(Entity en)
        {
            string val = this.GetValStringByKey(FrmEventAttr.MsgError);
            if (val.Trim() == "")
                return null;

            if (val.IndexOf('@') == -1)
                return val;

            foreach (Attr attr in en.EnMap.Attrs)
            {
                val = val.Replace("@" + attr.Key, en.GetValStringByKey(attr.Key));
            }
            return val;
        }
 
        public string FK_Event
        {
            get
            {
                return this.GetValStringByKey(FrmEventAttr.FK_Event);
            }
            set
            {
                this.SetValByKey(FrmEventAttr.FK_Event, value);
            }
        }
        /// <summary>
        /// 执行类型
        /// </summary>
        public EventDoType HisDoType
        {
            get
            {
                return (EventDoType)this.GetValIntByKey(FrmEventAttr.DoType);
            }
            set
            {
                this.SetValByKey(FrmEventAttr.DoType, (int)value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 事件
        /// </summary>
        public FrmEvent()
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

                Map map = new Map("Sys_FrmEvent");
                map.EnDesc = "事件";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.AddMyPK();
                 
                map.AddTBString(FrmEventAttr.FK_Event, null, "事件名称", true, true, 0, 400, 10);
                map.AddTBString(FrmEventAttr.FK_MapData, null, "FK_MapData", true, true, 0, 400, 10);

                map.AddTBInt(FrmEventAttr.DoType, 0, "事件类型", true, true);
                map.AddTBString(FrmEventAttr.DoDoc, null, "执行内容", true, true, 0, 400, 10);
                map.AddTBString(FrmEventAttr.MsgOK, null, "成功执行提示", true, true, 0, 400, 10);
                map.AddTBString(FrmEventAttr.MsgError, null, "异常信息提示", true, true, 0, 400, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        protected override bool beforeUpdateInsertAction()
        {
            this.MyPK = this.FK_MapData + "_" + this.FK_Event;
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
	/// 事件
	/// </summary>
    public class FrmEvents : EntitiesOID
    {
        /// <summary>
        /// 执行事件，事件标记是 EventList.
        /// </summary>
        /// <param name="dotype"></param>
        /// <param name="en"></param>
        /// <returns></returns>
        public string DoEventNode(string dotype, Entity en)
        {
            if (this.Count == 0)
                return null;

            FrmEvent nev = this.GetEntityByKey(FrmEventAttr.FK_Event, dotype) as FrmEvent;
            if (nev == null || nev.HisDoType == EventDoType.Disable)
                return null;

            string doc = nev.DoDoc.Trim();
            if (doc == null || doc == "")
                return null;

            #region 处理执行内容
            Attrs attrs = en.EnMap.Attrs;

            string MsgOK = "";
            string MsgErr = "";
            foreach (Attr attr in attrs)
            {
                if (doc.Contains("@" + attr.Key) == false)
                    continue;
                if (attr.MyDataType == DataType.AppString)
                    doc = doc.Replace("@" + attr.Key, "'" + en.GetValStrByKey(attr.Key) + "'");
                else
                    doc = doc.Replace("@" + attr.Key, en.GetValStrByKey(attr.Key));
            }
            doc = doc.Replace("~", "'");

            if (nev.HisDoType == EventDoType.URL)
            {
                //doc += "&FK_Flow=" + nev.FK_Flow.ToString();
                //doc += "&FK_MapData=" + nev.FK_MapData.ToString();
                //doc += "&WorkID=" + en.GetValStrByKey("OID");
                //doc += "&FID=" + en.GetValStrByKey("FID");

                doc += "&UserNo=" + WebUser.No;
                doc += "&SID=" + WebUser.SID;
                doc += "&FK_Dept=" + WebUser.FK_Dept;
                doc += "&FK_Unit=" + WebUser.FK_Unit;
            }
            #endregion 处理执行内容

            switch (nev.HisDoType)
            {
                case EventDoType.SP:
                    try
                    {
                        DBAccess.RunSP(doc);
                        return nev.MsgOK(en);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(nev.MsgError(en) + " Error:" + ex.Message);
                    }
                    break;
                case EventDoType.SQL:
                    try
                    {
                        DBAccess.RunSQL(doc);
                        return nev.MsgOK(en);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(  nev.MsgError(en) + " Error:" + ex.Message);
                    }
                    break;
                case EventDoType.URL:
                    doc = doc.Replace("@AppPath", System.Web.HttpContext.Current.Request.ApplicationPath);
                    try
                    {
                        string text = DataType.ReadURLContext(doc, 800, System.Text.Encoding.UTF8);
                        if (text != null && text.Substring(0, 7).Contains("Err"))
                            throw new Exception(text);

                        if (text == null || text.Trim() == "")
                            return null;
                        return text;

                        //Log.DebugWriteInfo(doc + " ------ " + text);
                        //return "@" + nev.MsgOK + text;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@" + nev.MsgError(en) + " Error:" + ex.Message);
                    }
                    break;
                default:
                    throw new Exception("@no such way.");
            }
        }
        /// <summary>
        /// 事件
        /// </summary>
        public FrmEvents() { }
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="FK_MapData">FK_MapData</param>
        public FrmEvents(string fk_MapData)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(FrmEventAttr.FK_MapData, fk_MapData);
            qo.DoQuery();
        }
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmEvent();
            }
        }
    }
}
