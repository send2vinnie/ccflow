using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_SMSAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string SenderNumber = "SenderNumber";
        public const string ReciveNumber = "ReciveNumber";
        public const string SendContent = "SendContent";
        public const string ReciveConent = "ReciveConent";
        public const string SendTime = "SendTime";
        public const string ReciveTime = "ReciveTime";
    }
    
    public partial class OA_SMS : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_SMSAttr.No);
            }
            set
            {
                this.SetValByKey(OA_SMSAttr.No, value);
            }
        }
        
        /// <summary>
        /// 发送号码
        /// </summary>
        public String SenderNumber
        {
            get
            {
                return this.GetValStringByKey(OA_SMSAttr.SenderNumber);
            }
            set
            {
                this.SetValByKey(OA_SMSAttr.SenderNumber, value);
            }
        }
        
        /// <summary>
        /// 接收号码
        /// </summary>
        public String ReciveNumber
        {
            get
            {
                return this.GetValStringByKey(OA_SMSAttr.ReciveNumber);
            }
            set
            {
                this.SetValByKey(OA_SMSAttr.ReciveNumber, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String SendContent
        {
            get
            {
                return this.GetValStringByKey(OA_SMSAttr.SendContent);
            }
            set
            {
                this.SetValByKey(OA_SMSAttr.SendContent, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String ReciveConent
        {
            get
            {
                return this.GetValStringByKey(OA_SMSAttr.ReciveConent);
            }
            set
            {
                this.SetValByKey(OA_SMSAttr.ReciveConent, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String SendTime
        {
            get
            {
                return this.GetValStringByKey(OA_SMSAttr.SendTime);
            }
            set
            {
                this.SetValByKey(OA_SMSAttr.SendTime, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String ReciveTime
        {
            get
            {
                return this.GetValStringByKey(OA_SMSAttr.ReciveTime);
            }
            set
            {
                this.SetValByKey(OA_SMSAttr.ReciveTime, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_SMS()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_SMS(string No)
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
                Map map = new Map("OA_SMS");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_SMSAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(OA_SMSAttr.SenderNumber, null, "发送号码", true, false, 0,  50, 50);
                map.AddTBString(OA_SMSAttr.ReciveNumber, null, "接收号码", true, false, 0,  4000, 4000);
                map.AddTBString(OA_SMSAttr.SendContent, null, "", true, false, 0,  16, 16);
                map.AddTBString(OA_SMSAttr.ReciveConent, null, "", true, false, 0,  16, 16);
                map.AddTBString(OA_SMSAttr.SendTime, null, "", true, false, 0,  50, 50);
                map.AddTBString(OA_SMSAttr.ReciveTime, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_SMSs : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_SMS(); }
        }
    }
}