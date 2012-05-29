using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_StaffAttr : EntityNoNameAttr
    {
        public const string EmpNo = "EmpNo";
        public const string Age = "Age";
        public const string IDCard = "IDCard";
        public const string Phone = "Phone";
        public const string Email = "Email";
        public const string UpUser = "UpUser";
        public const string Fk_Dept = "Fk_Dept";
        public const string EmpName = "EmpName";
        public const string Sex = "Sex";
        public const string Birthday = "Birthday";
        public const string Address = "Address";
        public const string CreateDate = "CreateDate";
        public const string UpDT = "UpDT";
        public const string Status = "Status";
    }

    public partial class Port_Staff : BaseEntity
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String EmpNo
        {
            get
            {
                return this.GetValStringByKey(Port_StaffAttr.EmpNo);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.EmpNo, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Age
        {
            get
            {
                return this.GetValIntByKey(Port_StaffAttr.Age);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.Age, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String IDCard
        {
            get
            {
                return this.GetValStringByKey(Port_StaffAttr.IDCard);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.IDCard, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Phone
        {
            get
            {
                return this.GetValStringByKey(Port_StaffAttr.Phone);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.Phone, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Email
        {
            get
            {
                return this.GetValStringByKey(Port_StaffAttr.Email);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.Email, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpUser
        {
            get
            {
                return this.GetValStringByKey(Port_StaffAttr.UpUser);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.UpUser, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Fk_Dept
        {
            get
            {
                return this.GetValStringByKey(Port_StaffAttr.Fk_Dept);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.Fk_Dept, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String EmpName
        {
            get
            {
                return this.GetValStringByKey(Port_StaffAttr.EmpName);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.EmpName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Sex
        {
            get
            {
                return this.GetValIntByKey(Port_StaffAttr.Sex);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.Sex, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Birthday
        {
            get
            {
                return this.GetValStringByKey(Port_StaffAttr.Birthday);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.Birthday, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Address
        {
            get
            {
                return this.GetValStringByKey(Port_StaffAttr.Address);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.Address, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String CreateDate
        {
            get
            {
                return this.GetValStringByKey(Port_StaffAttr.CreateDate);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.CreateDate, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpDT
        {
            get
            {
                return this.GetValStringByKey(Port_StaffAttr.UpDT);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.UpDT, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(Port_StaffAttr.Status);
            }
            set
            {
                this.SetValByKey(Port_StaffAttr.Status, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_Staff()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_Staff(string No)
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
                Map map = new Map("Port_Staff");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_StaffAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_StaffAttr.EmpNo, null, "", true, false, 0,  50, 50);
                map.AddTBInt(Port_StaffAttr.Age, 0, "", true, false);
                map.AddTBString(Port_StaffAttr.IDCard, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_StaffAttr.Phone, null, "", true, false, 0,  20, 20);
                map.AddTBString(Port_StaffAttr.Email, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_StaffAttr.UpUser, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_StaffAttr.Fk_Dept, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_StaffAttr.EmpName, null, "", true, false, 0,  50, 50);
                map.AddTBInt(Port_StaffAttr.Sex, 0, "", true, false);
                map.AddTBString(Port_StaffAttr.Birthday, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_StaffAttr.Address, null, "", true, false, 0,  100, 100);
                map.AddTBString(Port_StaffAttr.CreateDate, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_StaffAttr.UpDT, null, "", true, false, 0,  50, 50);
                map.AddTBInt(Port_StaffAttr.Status, 0, "", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_Staffs : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_Staff(); }
        }
    }
}