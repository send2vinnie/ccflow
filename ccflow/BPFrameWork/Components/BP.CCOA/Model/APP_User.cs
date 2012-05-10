using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_UserAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string UserName = "UserName";
        public const string Password = "Password";
        public const string UpUser = "UpUser";
        public const string Emp_Id = "Emp_Id";
        public const string CreateDate = "CreateDate";
        public const string UpDT = "UpDT";
        public const string Status = "Status";
    }
    
    public partial class APP_User : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_UserAttr.No);
            }
            set
            {
                this.SetValByKey(APP_UserAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UserName
        {
            get
            {
                return this.GetValStringByKey(APP_UserAttr.UserName);
            }
            set
            {
                this.SetValByKey(APP_UserAttr.UserName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Password
        {
            get
            {
                return this.GetValStringByKey(APP_UserAttr.Password);
            }
            set
            {
                this.SetValByKey(APP_UserAttr.Password, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpUser
        {
            get
            {
                return this.GetValStringByKey(APP_UserAttr.UpUser);
            }
            set
            {
                this.SetValByKey(APP_UserAttr.UpUser, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Emp_Id
        {
            get
            {
                return this.GetValStringByKey(APP_UserAttr.Emp_Id);
            }
            set
            {
                this.SetValByKey(APP_UserAttr.Emp_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String CreateDate
        {
            get
            {
                return this.GetValStringByKey(APP_UserAttr.CreateDate);
            }
            set
            {
                this.SetValByKey(APP_UserAttr.CreateDate, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpDT
        {
            get
            {
                return this.GetValStringByKey(APP_UserAttr.UpDT);
            }
            set
            {
                this.SetValByKey(APP_UserAttr.UpDT, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(APP_UserAttr.Status);
            }
            set
            {
                this.SetValByKey(APP_UserAttr.Status, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_User()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_User(string No)
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
                Map map = new Map("APP_User");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(APP_UserAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(APP_UserAttr.UserName, null, "", true, false, 0,  100, 100);
                map.AddTBString(APP_UserAttr.Password, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_UserAttr.UpUser, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_UserAttr.Emp_Id, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_UserAttr.CreateDate, null, "", true, false, 0,  50, 50);
                map.AddTBString(APP_UserAttr.UpDT, null, "", true, false, 0,  50, 50);
                map.AddTBInt(APP_UserAttr.Status, 0, "", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_Users : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_User(); }
        }
    }
}