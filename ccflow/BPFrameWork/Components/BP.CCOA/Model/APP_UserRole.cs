using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_UserRoleAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string User_Id = "User_Id";
        public const string Role_Id = "Role_Id";
    }
    
    public partial class APP_UserRole : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_UserRoleAttr.No);
            }
            set
            {
                this.SetValByKey(APP_UserRoleAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String User_Id
        {
            get
            {
                return this.GetValStringByKey(APP_UserRoleAttr.User_Id);
            }
            set
            {
                this.SetValByKey(APP_UserRoleAttr.User_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Role_Id
        {
            get
            {
                return this.GetValStringByKey(APP_UserRoleAttr.Role_Id);
            }
            set
            {
                this.SetValByKey(APP_UserRoleAttr.Role_Id, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_UserRole()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_UserRole(string No)
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
                Map map = new Map("APP_UserRole");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(APP_UserRoleAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(APP_UserRoleAttr.User_Id, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_UserRoleAttr.Role_Id, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_UserRoles : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_UserRole(); }
        }
    }
}