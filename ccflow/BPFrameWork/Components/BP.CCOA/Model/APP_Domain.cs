using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_DomainAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string ParentId = "ParentId";
        public const string Menu_Ids = "Menu_Ids";
        public const string DomainName = "DomainName";
        public const string Description = "Description";
    }
    
    public partial class APP_Domain : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_DomainAttr.No);
            }
            set
            {
                this.SetValByKey(APP_DomainAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String ParentId
        {
            get
            {
                return this.GetValStringByKey(APP_DomainAttr.ParentId);
            }
            set
            {
                this.SetValByKey(APP_DomainAttr.ParentId, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Menu_Ids
        {
            get
            {
                return this.GetValStringByKey(APP_DomainAttr.Menu_Ids);
            }
            set
            {
                this.SetValByKey(APP_DomainAttr.Menu_Ids, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String DomainName
        {
            get
            {
                return this.GetValStringByKey(APP_DomainAttr.DomainName);
            }
            set
            {
                this.SetValByKey(APP_DomainAttr.DomainName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Description
        {
            get
            {
                return this.GetValStringByKey(APP_DomainAttr.Description);
            }
            set
            {
                this.SetValByKey(APP_DomainAttr.Description, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_Domain()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_Domain(string No)
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
                
                map.AddTBStringPK(APP_DomainAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(APP_DomainAttr.ParentId, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_DomainAttr.Menu_Ids, null, "", true, false, 0,  2000, 2000);
                map.AddTBString(APP_DomainAttr.DomainName, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_DomainAttr.Description, null, "", true, false, 0,  16, 16);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_Domains : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_Domain(); }
        }
    }
}