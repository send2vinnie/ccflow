using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_ChangeLogAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string Domain = "Domain";
        public const string ChangeDigest = "ChangeDigest";
        public const string ChangeDetail = "ChangeDetail";
        public const string ChangeType = "ChangeType";
        public const string UpUser = "UpUser";
        public const string UpDT = "UpDT";
    }
    
    public partial class APP_ChangeLog : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_ChangeLogAttr.No);
            }
            set
            {
                this.SetValByKey(APP_ChangeLogAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Domain
        {
            get
            {
                return this.GetValStringByKey(APP_ChangeLogAttr.Domain);
            }
            set
            {
                this.SetValByKey(APP_ChangeLogAttr.Domain, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String ChangeDigest
        {
            get
            {
                return this.GetValStringByKey(APP_ChangeLogAttr.ChangeDigest);
            }
            set
            {
                this.SetValByKey(APP_ChangeLogAttr.ChangeDigest, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String ChangeDetail
        {
            get
            {
                return this.GetValStringByKey(APP_ChangeLogAttr.ChangeDetail);
            }
            set
            {
                this.SetValByKey(APP_ChangeLogAttr.ChangeDetail, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int ChangeType
        {
            get
            {
                return this.GetValIntByKey(APP_ChangeLogAttr.ChangeType);
            }
            set
            {
                this.SetValByKey(APP_ChangeLogAttr.ChangeType, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpUser
        {
            get
            {
                return this.GetValStringByKey(APP_ChangeLogAttr.UpUser);
            }
            set
            {
                this.SetValByKey(APP_ChangeLogAttr.UpUser, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpDT
        {
            get
            {
                return this.GetValStringByKey(APP_ChangeLogAttr.UpDT);
            }
            set
            {
                this.SetValByKey(APP_ChangeLogAttr.UpDT, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_ChangeLog()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_ChangeLog(string No)
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
                Map map = new Map("APP_ChangeLog");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(APP_ChangeLogAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(APP_ChangeLogAttr.Domain, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_ChangeLogAttr.ChangeDigest, null, "", true, false, 0,  250, 250);
                map.AddTBString(APP_ChangeLogAttr.ChangeDetail, null, "", true, false, 0,  1000, 1000);
                map.AddTBInt(APP_ChangeLogAttr.ChangeType, 0, "", true, false);
                map.AddTBString(APP_ChangeLogAttr.UpUser, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_ChangeLogAttr.UpDT, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_ChangeLogs : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_ChangeLog(); }
        }
    }
}