using System;
using System.Collections.Generic;
using System.Text;
using BP.En;
using BP.DA;
using BP.Sys;

namespace BP.WF
{
    public enum ActionType
    {
        /// <summary>
        /// 发起
        /// </summary>
        Start,
        /// <summary>
        /// 前进
        /// </summary>
        Forward,
        /// <summary>
        /// 退回
        /// </summary>
        Return,
        /// <summary>
        /// 移交
        /// </summary>
        Shift,
        /// <summary>
        /// 撤消移交
        /// </summary>
        UnShift,
        /// <summary>
        /// 撤消
        /// </summary>
        Undo,
        /// <summary>
        /// 分流前进
        /// </summary>
        ForwardFL,
        /// <summary>
        /// 合流前进
        /// </summary>
        ForwardHL
    }
    /// <summary>
    ///  属性
    /// </summary>
    public class TrackAttr
    {
        /// <summary>
        /// 记录日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 完成日期
        /// </summary>
        public const string CDT = "CDT";
        /// <summary>
        /// FID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// WorkID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 活动类型
        /// </summary>
        public const string ActionType = "ActionType";
        /// <summary>
        /// 时间跨度
        /// </summary>
        public const string WorkTimeSpan = "WorkTimeSpan";
        /// <summary>
        /// 节点数据
        /// </summary>
        public const string NodeData = "NodeData";
        /// <summary>
        /// 流程编号
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 轨迹字段
        /// </summary>
        public const string TrackFields = "TrackFields";
        /// <summary>
        /// 备注
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// 从节点
        /// </summary>
        public const string NDFrom = "NDFrom";
        /// <summary>
        /// 到节点
        /// </summary>
        public const string NDTo = "NDTo";
        /// <summary>
        /// 从人员
        /// </summary>
        public const string EmpFrom = "EmpFrom";
        /// <summary>
        /// 到人员
        /// </summary>
        public const string EmpTo = "EmpTo";
        /// <summary>
        /// 审核
        /// </summary>
        public const string Msg = "Msg";
        /// <summary>
        /// EmpFromT
        /// </summary>
        public const string EmpFromT = "EmpFromT";
        /// <summary>
        /// NDFromT
        /// </summary>
        public const string NDFromT = "NDFromT";
        /// <summary>
        /// NDToT
        /// </summary>
        public const string NDToT = "NDToT";
        /// <summary>
        /// EmpToT
        /// </summary>
        public const string EmpToT = "EmpToT";
    }
    /// <summary>
    /// 轨迹
    /// </summary>
    public class Track : BP.En.EntityMyPK
    {
        #region attrs
        /// <summary>
        /// 节点从
        /// </summary>
        public int NDFrom
        {
            get
            {
                return this.GetValIntByKey(TrackAttr.NDFrom);
            }
            set
            {
                this.SetValByKey(TrackAttr.NDFrom, value);
            }
        }
        /// <summary>
        /// 节点到
        /// </summary>
        public int NDTo
        {
            get
            {
                return this.GetValIntByKey(TrackAttr.NDTo);
            }
            set
            {
                this.SetValByKey(TrackAttr.NDTo, value);
            }
        }
        /// <summary>
        /// 流程编号
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(TrackAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// 从人员
        /// </summary>
        public string EmpFrom
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.EmpFrom);
            }
            set
            {
                this.SetValByKey(TrackAttr.EmpFrom, value);
            }
        }
        /// <summary>
        /// 到人员
        /// </summary>
        public string EmpTo
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.EmpTo);
            }
            set
            {
                this.SetValByKey(TrackAttr.EmpTo, value);
            }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.RDT);
            }
            set
            {
                this.SetValByKey(TrackAttr.RDT, value);
            }
        }
        /// <summary>
        /// fid
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(TrackAttr.FID);
            }
            set
            {
                this.SetValByKey(TrackAttr.FID, value);
            }
        }
        /// <summary>
        /// Workid
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(TrackAttr.WorkID);
            }
            set
            {
                this.SetValByKey(TrackAttr.WorkID, value);
            }
        }
        /// <summary>
        /// 活动类型
        /// </summary>
        public ActionType HisActionType
        {
            get
            {
                return (ActionType)this.GetValIntByKey(TrackAttr.ActionType);
            }
            set
            {
                this.SetValByKey(TrackAttr.ActionType, (int)value);
            }
        }
        public string HisActionTypeT
        {
            get
            {
                switch (this.HisActionType)
                {
                    case ActionType.Forward:
                        return "前进";
                    case ActionType.Return:
                        return "退回";
                    case ActionType.Shift:
                        return "移交";
                    case ActionType.UnShift:
                        return "撤消移交";
                    case ActionType.Start:
                        return "发起";
                    case ActionType.Undo:
                        return "撤消发起";
                    case ActionType.ForwardFL:
                        return " -前进(分流点)";
                    case ActionType.ForwardHL:
                        return " -向合流点发送";
                    default:
                        return "未知";
                }
            }
        }
        /// <summary>
        /// 节点数据
        /// </summary>
        public string NodeData
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.NodeData);
            }
            set
            {
                this.SetValByKey(TrackAttr.NodeData, value);
            }
        }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string Msg
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.Msg);
            }
            set
            {
                this.SetValByKey(TrackAttr.Msg, value);
            }
        }
        public string MsgHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(TrackAttr.Msg);
            }
        }
        public string EmpToT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.EmpToT);
            }
            set
            {
                this.SetValByKey(TrackAttr.EmpToT, value);
            }
        }
        public string EmpFromT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.EmpFromT);
            }
            set
            {
                this.SetValByKey(TrackAttr.EmpFromT, value);
            }
        }

        public string NDFromT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.NDFromT);
            }
            set
            {
                this.SetValByKey(TrackAttr.NDFromT, value);
            }
        }
        public string NDToT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.NDToT);
            }
            set
            {
                this.SetValByKey(TrackAttr.NDToT, value);
            }
        }
        #endregion attrs

        #region 属性
        public string RptName = null;
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();

                #region 基本属性
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //要连接的数据源（表示要连接到的那个系统数据库）。
                map.PhysicsTable = "WF_Track"; // 要物理表。
                map.EnDesc = "轨迹表";
                map.EnType = EnType.App;
                #endregion

                #region 字段
                map.AddMyPK();
                map.AddTBString(TrackAttr.FK_Flow, null, "流程", true, false, 0, 100, 100);
                map.AddDDLSysEnum(TrackAttr.ActionType, 0, "操作类型", true, false, TrackAttr.ActionType,
                  "@0=发起@1=前进@2=后退@3=移交@4=删除");

                map.AddTBInt(TrackAttr.FID, 0, "流程ID", true, false);
                map.AddTBInt(TrackAttr.WorkID, 0, "工作ID", true, false);

                map.AddTBInt(TrackAttr.NDFrom, 0, "从节点", true, false);
                map.AddTBString(TrackAttr.NDFromT, null, "从节点(名称)", true, false, 0, 100, 100);

                map.AddTBInt(TrackAttr.NDTo, 0, "到节点", true, false);
                map.AddTBString(TrackAttr.NDToT, null, "到节点(名称)", true, false, 0, 100, 100);


                map.AddTBString(TrackAttr.EmpFrom, null, "从人员", true, false, 0, 100, 100);
                map.AddTBString(TrackAttr.EmpFromT, null, "从人员(名称)", true, false, 0, 100, 100);

                map.AddTBString(TrackAttr.EmpTo, null, "到人员", true, false, 0, 4000, 100);
                map.AddTBString(TrackAttr.EmpToT, null, "到人员(名称)", true, false, 0, 100, 100);

                map.AddTBString(TrackAttr.RDT, null, "到人员", true, false, 0, 30, 100);

                map.AddTBFloat(TrackAttr.WorkTimeSpan, 0, "时间跨度(天)", true, false);
                map.AddTBStringDoc(TrackAttr.Msg, null, "消息", true, false);
                map.AddTBStringDoc(TrackAttr.NodeData, null, "节点数据(日志信息)", true, false);
                #endregion 字段

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// 轨迹
        /// </summary>
        /// <param name="rptName"></param>
        public Track(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        /// <summary>
        /// 轨迹
        /// </summary>
        public Track()
        {
        }
       
        #endregion attrs
    }
    /// <summary>
    /// 轨迹集合
    /// </summary>
    public class Tracks : BP.En.Entities
    {
        /// <summary>
        /// 轨迹集合
        /// </summary>
        public Tracks()
        {
        }
        public override Entity GetNewEntity
        {
            get
            {
                return new Track();
            }
        }
    }
}
