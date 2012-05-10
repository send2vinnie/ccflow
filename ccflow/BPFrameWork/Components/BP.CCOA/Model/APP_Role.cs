using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_RoleAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string RoleName = "RoleName";
        public const string Description = "Description";
    }
    
    public partial class APP_Role : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_RoleAttr.No);
            }
            set
            {
                this.SetValByKey(APP_RoleAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String RoleName
        {
            get
            {
                return this.GetValStringByKey(APP_RoleAttr.RoleName);
            }
            set
            {
                this.SetValByKey(APP_RoleAttr.RoleName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Description
        {
            get
            {
                return this.GetValStringByKey(APP_RoleAttr.Description);
            }
            set
            {
                this.SetValByKey(APP_RoleAttr.Description, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_Role()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_Role(string No)
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
                Map map = new Map("APP_Role");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(APP_RoleAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(APP_RoleAttr.RoleName, null, "", true, false, 0,  100, 100);
                map.AddTBString(APP_RoleAttr.Description, null, "", true, false, 0,  500, 500);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_Roles : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_Role(); }
        }
    }
}