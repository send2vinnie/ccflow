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
        Shift
    }
    /// <summary>
    ///  属性
    /// </summary>
    public class TrackAttr
    {
        /// <summary>
        /// 从人员
        /// </summary>
        public const string FromEmp = "FromEmp";
        /// <summary>
        /// 到人员
        /// </summary>
        public const string ToEmp = "ToEmp";
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
    }
    /// <summary>
    /// 轨迹
    /// </summary>
    public class Track : BP.En.EntityMyPK
    {
        #region attrs
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
        public string FromEmp
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.FromEmp);
            }
            set
            {
                this.SetValByKey(TrackAttr.FromEmp, value);
            }
        }
        /// <summary>
        /// 到人员
        /// </summary>
        public string ToEmp
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.ToEmp);
            }
            set
            {
                this.SetValByKey(TrackAttr.ToEmp, value);
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
        /// 完成日期
        /// </summary>
        public string CDT
        {
            get
            {
                return this.GetValStringByKey(TrackAttr.CDT);
            }
            set
            {
                this.SetValByKey(TrackAttr.CDT, value);
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
        /// <summary>
        /// 流程结束时间
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

                map.AddTBString(TrackAttr.FromEmp, null, "从人员", true, false, 0, 100, 100);
                map.AddTBString(TrackAttr.ToEmp, null, "到人员", true, false, 0, 100, 100);
                map.AddTBDateTime(TrackAttr.RDT, null, "记录日期", true, false);
                map.AddTBDateTime(TrackAttr.CDT, null, "完成日期", true, false);

                map.AddTBInt(TrackAttr.FID, 0, "流程ID", true, false);
                map.AddTBInt(TrackAttr.WorkID, 0, "工作ID", true, false);
              
                map.AddTBFloat(TrackAttr.WorkTimeSpan, 0, "时间跨度(天)", true, false);
                map.AddTBStringDoc(TrackAttr.NodeData, null, "节点数据", true, false);
                #endregion 字段

                this._enMap = map;
                return this._enMap;
            }
        }
        /// <summary>
        /// 轨迹
        /// </summary>
        /// <param name="rptName"></param>
        public Track(string rptName)
        {
            this.RptName = rptName;
        }
        /// <summary>
        /// 轨迹
        /// </summary>
        public Track()
        {
        }
        /// <summary>
        /// 轨迹
        /// </summary>
        /// <param name="rptName"></param>
        /// <param name="WorkID"></param>
        public Track(string rptName, int WorkID)
        {
            this.RptName = rptName;
            this.Retrieve();
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
