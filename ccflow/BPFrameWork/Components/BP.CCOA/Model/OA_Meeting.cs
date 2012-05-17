using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_MeetingAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string Topic = "Topic";
        public const string PlanStartTime = "PlanStartTime";
        public const string PlanEndTime = "PlanEndTime";
        public const string PlanAddress = "PlanAddress";
        public const string PlanMembers = "PlanMembers";
        public const string RealStartTime = "RealStartTime";
        public const string RealEndTime = "RealEndTime";
        public const string RealAddress = "RealAddress";
        public const string RealMembers = "RealMembers";
        public const string Recorder = "Recorder";
        public const string Summary = "Summary";
        public const string UpUser = "UpUser";
        public const string UpDT = "UpDT";
        public const string Status = "Status";
    }
    
    public partial class OA_Meeting : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 主键Id
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttr.No);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.No, value);
            }
        }
        
        /// <summary>
        /// 议题
        /// </summary>
        public String Topic
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttr.Topic);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.Topic, value);
            }
        }
        
        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlanStartTime
        {
            get
            {
                return this.GetValDateTime(OA_MeetingAttr.PlanStartTime);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.PlanStartTime, value);
            }
        }
        
        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlanEndTime
        {
            get
            {
                return this.GetValDateTime(OA_MeetingAttr.PlanEndTime);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.PlanEndTime, value);
            }
        }
        
        /// <summary>
        /// 计划召开地址
        /// </summary>
        public String PlanAddress
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttr.PlanAddress);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.PlanAddress, value);
            }
        }
        
        /// <summary>
        /// 计划参加人员
        /// </summary>
        public String PlanMembers
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttr.PlanMembers);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.PlanMembers, value);
            }
        }
        
        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime RealStartTime
        {
            get
            {
                return this.GetValDateTime(OA_MeetingAttr.RealStartTime);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.RealStartTime, value);
            }
        }
        
        /// <summary>
        /// 实际结束时间
        /// </summary>
        public DateTime RealEndTime
        {
            get
            {
                return this.GetValDateTime(OA_MeetingAttr.RealEndTime);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.RealEndTime, value);
            }
        }
        
        /// <summary>
        /// 实际召开地址
        /// </summary>
        public String RealAddress
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttr.RealAddress);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.RealAddress, value);
            }
        }
        
        /// <summary>
        /// 实际参加人员
        /// </summary>
        public String RealMembers
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttr.RealMembers);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.RealMembers, value);
            }
        }
        
        /// <summary>
        /// 记录人
        /// </summary>
        public String Recorder
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttr.Recorder);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.Recorder, value);
            }
        }
        
        /// <summary>
        /// 会议纪要
        /// </summary>
        public String Summary
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttr.Summary);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.Summary, value);
            }
        }
        
        /// <summary>
        /// 更新人
        /// </summary>
        public String UpUser
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttr.UpUser);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.UpUser, value);
            }
        }
        
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpDT
        {
            get
            {
                return this.GetValDateTime(OA_MeetingAttr.UpDT);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.UpDT, value);
            }
        }
        
        /// <summary>
        /// 状态：0-未召开1-已召开
        /// </summary>
        public bool Status
        {
            get
            {
                return this.GetValBooleanByKey(OA_MeetingAttr.Status);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttr.Status, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_Meeting()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_Meeting(string No)
        {
            this.No = No;
            this.Retrieve();
        }
        #endregion
        
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("OA_Meeting");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_MeetingAttr.No, null, "主键Id", true, true, 0, 50, 50);
                map.AddTBString(OA_MeetingAttr.Topic, null, "议题", true, false, 0,  50, 50);
                map.AddTBDateTime(OA_MeetingAttr.PlanStartTime, "计划开始时间", true, false);
                map.AddTBDateTime(OA_MeetingAttr.PlanEndTime, "计划结束时间", true, false);
                map.AddTBString(OA_MeetingAttr.PlanAddress, null, "计划召开地址", true, false, 0,  100, 100);
                map.AddTBString(OA_MeetingAttr.PlanMembers, null, "计划参加人员", true, false, 0,  1000, 1000);
                map.AddTBDateTime(OA_MeetingAttr.RealStartTime, "实际开始时间", true, false);
                map.AddTBDateTime(OA_MeetingAttr.RealEndTime, "实际结束时间", true, false);
                map.AddTBString(OA_MeetingAttr.RealAddress, null, "实际召开地址", true, false, 0,  100, 100);
                map.AddTBString(OA_MeetingAttr.RealMembers, null, "实际参加人员", true, false, 0,  1000, 1000);
                map.AddTBString(OA_MeetingAttr.Recorder, null, "记录人", true, false, 0,  50, 50);
                map.AddTBString(OA_MeetingAttr.Summary, null, "会议纪要", true, false, 0,  16, 16);
                map.AddTBString(OA_MeetingAttr.UpUser, null, "更新人", true, false, 0,  50, 50);
                map.AddTBDateTime(OA_MeetingAttr.UpDT, "更新时间", true, false);
                map.AddBoolean(OA_MeetingAttr.Status, true, "状态：0-未召开1-已召开", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_Meetings : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_Meeting(); }
        }
    }
}