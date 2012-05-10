using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_MeetingAttachAttr : EntityNoNameAttr
    {
        public const string Meeting_Id = "Meeting_Id";
        public const string Attechment_Id = "Attechment_Id";
        public const string No = "No";
    }
    
    public partial class OA_MeetingAttach : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String Meeting_Id
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttachAttr.Meeting_Id);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttachAttr.Meeting_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Attechment_Id
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttachAttr.Attechment_Id);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttachAttr.Attechment_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_MeetingAttachAttr.No);
            }
            set
            {
                this.SetValByKey(OA_MeetingAttachAttr.No, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_MeetingAttach()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_MeetingAttach(string No)
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
                Map map = new Map("OA_MeetingAttach");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBString(OA_MeetingAttachAttr.Meeting_Id, null, "", true, false, 0,  50, 50);
                map.AddTBString(OA_MeetingAttachAttr.Attechment_Id, null, "", true, false, 0,  50, 50);
                map.AddTBStringPK(OA_MeetingAttachAttr.No, null, "", true, true, 0, 50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_MeetingAttachs : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_MeetingAttach(); }
        }
    }
}