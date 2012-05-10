using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_RoleDomainAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string Role_Id = "Role_Id";
        public const string Domain_Id = "Domain_Id";
    }
    
    public partial class APP_RoleDomain : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_RoleDomainAttr.No);
            }
            set
            {
                this.SetValByKey(APP_RoleDomainAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Role_Id
        {
            get
            {
                return this.GetValStringByKey(APP_RoleDomainAttr.Role_Id);
            }
            set
            {
                this.SetValByKey(APP_RoleDomainAttr.Role_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Domain_Id
        {
            get
            {
                return this.GetValStringByKey(APP_RoleDomainAttr.Domain_Id);
            }
            set
            {
                this.SetValByKey(APP_RoleDomainAttr.Domain_Id, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_RoleDomain()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_RoleDomain(string No)
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
                Map map = new Map("APP_RoleDomain");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(APP_RoleDomainAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(APP_RoleDomainAttr.Role_Id, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_RoleDomainAttr.Domain_Id, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_RoleDomains : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_RoleDomain(); }
        }
    }
}