using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class EIP_EmpAttr : EntityNoNameAttr
    {
        public const string No = "No";
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
    
    public partial class EIP_Emp : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.No);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String EmpNo
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.EmpNo);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.EmpNo, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Age
        {
            get
            {
                return this.GetValIntByKey(EIP_EmpAttr.Age);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.Age, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String IDCard
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.IDCard);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.IDCard, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Phone
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.Phone);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.Phone, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Email
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.Email);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.Email, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpUser
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.UpUser);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.UpUser, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Fk_Dept
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.Fk_Dept);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.Fk_Dept, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String EmpName
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.EmpName);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.EmpName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Sex
        {
            get
            {
                return this.GetValIntByKey(EIP_EmpAttr.Sex);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.Sex, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Birthday
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.Birthday);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.Birthday, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Address
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.Address);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.Address, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String CreateDate
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.CreateDate);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.CreateDate, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpDT
        {
            get
            {
                return this.GetValStringByKey(EIP_EmpAttr.UpDT);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.UpDT, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(EIP_EmpAttr.Status);
            }
            set
            {
                this.SetValByKey(EIP_EmpAttr.Status, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public EIP_Emp()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public EIP_Emp(string No)
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
                Map map = new Map("EIP_Emp");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(EIP_EmpAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(EIP_EmpAttr.EmpNo, null, "", true, false, 0,  50, 50);
                map.AddTBInt(EIP_EmpAttr.Age, 0, "", true, false);
                map.AddTBString(EIP_EmpAttr.IDCard, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_EmpAttr.Phone, null, "", true, false, 0,  20, 20);
                map.AddTBString(EIP_EmpAttr.Email, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_EmpAttr.UpUser, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_EmpAttr.Fk_Dept, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_EmpAttr.EmpName, null, "", true, false, 0,  50, 50);
                map.AddTBInt(EIP_EmpAttr.Sex, 0, "", true, false);
                map.AddTBString(EIP_EmpAttr.Birthday, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_EmpAttr.Address, null, "", true, false, 0,  100, 100);
                map.AddTBString(EIP_EmpAttr.CreateDate, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_EmpAttr.UpDT, null, "", true, false, 0,  50, 50);
                map.AddTBInt(EIP_EmpAttr.Status, 0, "", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class EIP_Emps : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new EIP_Emp(); }
        }
    }
}