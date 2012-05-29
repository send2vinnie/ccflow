using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_DomainAttr : EntityNoNameAttr
    {
        public const string ParentId = "ParentId";
        public const string DomainName = "DomainName";
        public const string Description = "Description";
    }

    public partial class Port_Domain : BaseEntity
    {
        #region 属性
      
        /// <summary>
        /// 
        /// </summary>
        public String ParentId
        {
            get
            {
                return this.GetValStringByKey(Port_DomainAttr.ParentId);
            }
            set
            {
                this.SetValByKey(Port_DomainAttr.ParentId, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String DomainName
        {
            get
            {
                return this.GetValStringByKey(Port_DomainAttr.DomainName);
            }
            set
            {
                this.SetValByKey(Port_DomainAttr.DomainName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Description
        {
            get
            {
                return this.GetValStringByKey(Port_DomainAttr.Description);
            }
            set
            {
                this.SetValByKey(Port_DomainAttr.Description, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_Domain()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_Domain(string No)
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
                Map map = new Map("Port_Domain");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_DomainAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_DomainAttr.ParentId, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_DomainAttr.DomainName, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_DomainAttr.Description, null, "", true, false, 0,  16, 16);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_Domains : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_Domain(); }
        }
    }
}