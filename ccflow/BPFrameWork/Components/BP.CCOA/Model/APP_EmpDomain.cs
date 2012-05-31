using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_EmpDomainAttr : EntityNoNameAttr
    {
        public const string User_Id = "User_Id";
        public const string Domain_Id = "Domain_Id";
        public const string No = "No";
    }
    
    public partial class APP_EmpDomain : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String User_Id
        {
            get
            {
                return this.GetValStringByKey(APP_EmpDomainAttr.User_Id);
            }
            set
            {
                this.SetValByKey(APP_EmpDomainAttr.User_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Domain_Id
        {
            get
            {
                return this.GetValStringByKey(APP_EmpDomainAttr.Domain_Id);
            }
            set
            {
                this.SetValByKey(APP_EmpDomainAttr.Domain_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_EmpDomainAttr.No);
            }
            set
            {
                this.SetValByKey(APP_EmpDomainAttr.No, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_EmpDomain()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_EmpDomain(string No)
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
                Map map = new Map("APP_EmpDomain");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBString(APP_EmpDomainAttr.User_Id, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_EmpDomainAttr.Domain_Id, null, "", true, false, 0,  50, 50);
                map.AddTBStringPK(APP_EmpDomainAttr.No, null, "", true, true, 0, 50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_EmpDomains : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_EmpDomain(); }
        }
    }
}