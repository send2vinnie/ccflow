using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_UserDomainAttr : EntityNoNameAttr
    {
        public const string User_Id = "User_Id";
        public const string Domain_Id = "Domain_Id";
        public const string No = "No";
    }
    
    public partial class APP_UserDomain : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String User_Id
        {
            get
            {
                return this.GetValStringByKey(APP_UserDomainAttr.User_Id);
            }
            set
            {
                this.SetValByKey(APP_UserDomainAttr.User_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Domain_Id
        {
            get
            {
                return this.GetValStringByKey(APP_UserDomainAttr.Domain_Id);
            }
            set
            {
                this.SetValByKey(APP_UserDomainAttr.Domain_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_UserDomainAttr.No);
            }
            set
            {
                this.SetValByKey(APP_UserDomainAttr.No, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_UserDomain()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_UserDomain(string No)
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
                Map map = new Map("APP_UserDomain");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBString(APP_UserDomainAttr.User_Id, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_UserDomainAttr.Domain_Id, null, "", true, false, 0,  50, 50);
                map.AddTBStringPK(APP_UserDomainAttr.No, null, "", true, true, 0, 50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_UserDomains : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_UserDomain(); }
        }
    }
}