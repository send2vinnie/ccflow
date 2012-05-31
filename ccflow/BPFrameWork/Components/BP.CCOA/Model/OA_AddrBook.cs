using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class OA_AddrBookAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string Mobile = "Mobile";
        public const string WorkPhone = "WorkPhone";
        public const string HomePhone = "HomePhone";
        public const string Grouping = "Grouping";
        public const string Name = "Name";
        public const string NickName = "NickName";
        public const string Sex = "Sex";
        public const string Birthday = "Birthday";
        public const string Email = "Email";
        public const string QQ = "QQ";
        public const string WorkUnit = "WorkUnit";
        public const string WorkAddress = "WorkAddress";
        public const string HomeAddress = "HomeAddress";
        public const string Status = "Status";
        public const string Remarks = "Remarks";
    }
    
    public partial class OA_AddrBook : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 主键Id
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.No);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.No, value);
            }
        }
        
        /// <summary>
        /// 手机
        /// </summary>
        public String Mobile
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.Mobile);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.Mobile, value);
            }
        }
        
        /// <summary>
        /// 工作电话
        /// </summary>
        public String WorkPhone
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.WorkPhone);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.WorkPhone, value);
            }
        }
        
        /// <summary>
        /// 家庭电话
        /// </summary>
        public String HomePhone
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.HomePhone);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.HomePhone, value);
            }
        }
        
        /// <summary>
        /// 分组
        /// </summary>
        public int Grouping
        {
            get
            {
                return this.GetValIntByKey(OA_AddrBookAttr.Grouping);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.Grouping, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Name
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.Name);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.Name, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String NickName
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.NickName);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.NickName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Sex
        {
            get
            {
                return this.GetValIntByKey(OA_AddrBookAttr.Sex);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.Sex, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime Birthday
        {
            get
            {
                return this.GetValDateTime(OA_AddrBookAttr.Birthday);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.Birthday, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Email
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.Email);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.Email, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String QQ
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.QQ);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.QQ, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String WorkUnit
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.WorkUnit);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.WorkUnit, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String WorkAddress
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.WorkAddress);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.WorkAddress, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String HomeAddress
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.HomeAddress);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.HomeAddress, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(OA_AddrBookAttr.Status);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.Status, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Remarks
        {
            get
            {
                return this.GetValStringByKey(OA_AddrBookAttr.Remarks);
            }
            set
            {
                this.SetValByKey(OA_AddrBookAttr.Remarks, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public OA_AddrBook()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public OA_AddrBook(string No)
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
                Map map = new Map("OA_AddrBook");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(OA_AddrBookAttr.No, null, "主键Id", true, true, 0, 50, 50);
                map.AddTBString(OA_AddrBookAttr.Mobile, null, "手机", true, false, 0,  20, 20);
                map.AddTBString(OA_AddrBookAttr.WorkPhone, null, "工作电话", true, false, 0,  20, 20);
                map.AddTBString(OA_AddrBookAttr.HomePhone, null, "家庭电话", true, false, 0,  20, 20);
                map.AddTBInt(OA_AddrBookAttr.Grouping, 0, "分组", true, false);
                map.AddTBString(OA_AddrBookAttr.Name, null, "", true, false, 0,  10, 10);
                map.AddTBString(OA_AddrBookAttr.NickName, null, "", true, false, 0,  10, 10);
                map.AddTBInt(OA_AddrBookAttr.Sex, 0, "", true, false);
                map.AddTBDateTime(OA_AddrBookAttr.Birthday, "生日", true, false);
                map.AddTBString(OA_AddrBookAttr.Email, null, "", true, false, 0,  50, 50);
                map.AddTBString(OA_AddrBookAttr.QQ, null, "", true, false, 0,  20, 20);
                map.AddTBString(OA_AddrBookAttr.WorkUnit, null, "", true, false, 0,  50, 50);
                map.AddTBString(OA_AddrBookAttr.WorkAddress, null, "", true, false, 0,  100, 100);
                map.AddTBString(OA_AddrBookAttr.HomeAddress, null, "", true, false, 0,  100, 100);
                map.AddTBInt(OA_AddrBookAttr.Status, 0, "", true, false);
                map.AddTBString(OA_AddrBookAttr.Remarks, null, "", true, false, 0,  1000, 1000);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class OA_AddrBooks : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new OA_AddrBook(); }
        }
    }
}