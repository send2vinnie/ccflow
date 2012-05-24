using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_StationDomainAttr : EntityNoNameAttr
    {
        public const string FK_Station = "FK_Station";
        public const string FK_Domain = "FK_Domain";
    }
    
    public partial class Port_StationDomain : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String FK_Station
        {
            get
            {
                return this.GetValStringByKey(Port_StationDomainAttr.FK_Station);
            }
            set
            {
                this.SetValByKey(Port_StationDomainAttr.FK_Station, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String FK_Domain
        {
            get
            {
                return this.GetValStringByKey(Port_StationDomainAttr.FK_Domain);
            }
            set
            {
                this.SetValByKey(Port_StationDomainAttr.FK_Domain, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_StationDomain()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_StationDomain(string No)
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
                
                map.AddTBStringPK(Port_StationDomainAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_StationDomainAttr.FK_Station, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_StationDomainAttr.FK_Domain, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_StationDomains : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_StationDomain(); }
        }
    }
}