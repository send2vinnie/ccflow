using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_EmpDomainAttr : EntityNoNameAttr
    {
        public const string FK_Emp = "FK_Emp";
        public const string FK_Domain = "FK_Domain";
    }
    
    public partial class Port_EmpDomain : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String FK_Emp
        {
            get
            {
                return this.GetValStringByKey(Port_EmpDomainAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(Port_EmpDomainAttr.FK_Emp, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String FK_Domain
        {
            get
            {
                return this.GetValStringByKey(Port_EmpDomainAttr.FK_Domain);
            }
            set
            {
                this.SetValByKey(Port_EmpDomainAttr.FK_Domain, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_EmpDomain()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_EmpDomain(string No)
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
                Map map = new Map("Port_EmpDomain");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_EmpDomainAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_EmpDomainAttr.FK_Emp, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_EmpDomainAttr.FK_Domain, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_EmpDomains : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_EmpDomain(); }
        }
    }
}