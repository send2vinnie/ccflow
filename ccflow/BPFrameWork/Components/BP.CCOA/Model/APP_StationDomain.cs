using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_StationDomainAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string Role_Id = "Role_Id";
        public const string Domain_Id = "Domain_Id";
    }
    
    public partial class APP_StationDomain : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_StationDomainAttr.No);
            }
            set
            {
                this.SetValByKey(APP_StationDomainAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Role_Id
        {
            get
            {
                return this.GetValStringByKey(APP_StationDomainAttr.Role_Id);
            }
            set
            {
                this.SetValByKey(APP_StationDomainAttr.Role_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Domain_Id
        {
            get
            {
                return this.GetValStringByKey(APP_StationDomainAttr.Domain_Id);
            }
            set
            {
                this.SetValByKey(APP_StationDomainAttr.Domain_Id, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_StationDomain()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_StationDomain(string No)
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
                Map map = new Map("Port_StationDomain");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(APP_StationDomainAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(APP_StationDomainAttr.Role_Id, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_StationDomainAttr.Domain_Id, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_StationDomains : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_StationDomain(); }
        }
    }
}