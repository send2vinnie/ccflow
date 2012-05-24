using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_ChangeLogAttr : EntityNoNameAttr
    {
        public const string Domain = "Domain";
        public const string ChangeDigest = "ChangeDigest";
        public const string ChangeDetail = "ChangeDetail";
        public const string ChangeType = "ChangeType";
        public const string UpUser = "UpUser";
        public const string UpDT = "UpDT";
    }
    
    public partial class Port_ChangeLog : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String Domain
        {
            get
            {
                return this.GetValStringByKey(Port_ChangeLogAttr.Domain);
            }
            set
            {
                this.SetValByKey(Port_ChangeLogAttr.Domain, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String ChangeDigest
        {
            get
            {
                return this.GetValStringByKey(Port_ChangeLogAttr.ChangeDigest);
            }
            set
            {
                this.SetValByKey(Port_ChangeLogAttr.ChangeDigest, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String ChangeDetail
        {
            get
            {
                return this.GetValStringByKey(Port_ChangeLogAttr.ChangeDetail);
            }
            set
            {
                this.SetValByKey(Port_ChangeLogAttr.ChangeDetail, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int ChangeType
        {
            get
            {
                return this.GetValIntByKey(Port_ChangeLogAttr.ChangeType);
            }
            set
            {
                this.SetValByKey(Port_ChangeLogAttr.ChangeType, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpUser
        {
            get
            {
                return this.GetValStringByKey(Port_ChangeLogAttr.UpUser);
            }
            set
            {
                this.SetValByKey(Port_ChangeLogAttr.UpUser, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpDT
        {
            get
            {
                return this.GetValStringByKey(Port_ChangeLogAttr.UpDT);
            }
            set
            {
                this.SetValByKey(Port_ChangeLogAttr.UpDT, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_ChangeLog()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_ChangeLog(string No)
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
                Map map = new Map("Port_ChangeLog");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_ChangeLogAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_ChangeLogAttr.Domain, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_ChangeLogAttr.ChangeDigest, null, "", true, false, 0,  250, 250);
                map.AddTBString(Port_ChangeLogAttr.ChangeDetail, null, "", true, false, 0,  1000, 1000);
                map.AddTBInt(Port_ChangeLogAttr.ChangeType, 0, "", true, false);
                map.AddTBString(Port_ChangeLogAttr.UpUser, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_ChangeLogAttr.UpDT, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_ChangeLogs : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_ChangeLog(); }
        }
    }
}