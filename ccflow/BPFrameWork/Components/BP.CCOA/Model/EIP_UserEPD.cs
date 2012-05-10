using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class EIP_UserEPDAttr : EntityNoNameAttr
    {
        public const string UserId = "UserId";
        public const string UserType = "UserType";
        public const string EmpId = "EmpId";
        public const string PostId = "PostId";
        public const string DeptId = "DeptId";
        public const string No = "No";
    }
    
    public partial class EIP_UserEPD : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String UserId
        {
            get
            {
                return this.GetValStringByKey(EIP_UserEPDAttr.UserId);
            }
            set
            {
                this.SetValByKey(EIP_UserEPDAttr.UserId, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UserType
        {
            get
            {
                return this.GetValStringByKey(EIP_UserEPDAttr.UserType);
            }
            set
            {
                this.SetValByKey(EIP_UserEPDAttr.UserType, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String EmpId
        {
            get
            {
                return this.GetValStringByKey(EIP_UserEPDAttr.EmpId);
            }
            set
            {
                this.SetValByKey(EIP_UserEPDAttr.EmpId, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String PostId
        {
            get
            {
                return this.GetValStringByKey(EIP_UserEPDAttr.PostId);
            }
            set
            {
                this.SetValByKey(EIP_UserEPDAttr.PostId, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String DeptId
        {
            get
            {
                return this.GetValStringByKey(EIP_UserEPDAttr.DeptId);
            }
            set
            {
                this.SetValByKey(EIP_UserEPDAttr.DeptId, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(EIP_UserEPDAttr.No);
            }
            set
            {
                this.SetValByKey(EIP_UserEPDAttr.No, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public EIP_UserEPD()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public EIP_UserEPD(string No)
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
                Map map = new Map("EIP_UserEPD");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBString(EIP_UserEPDAttr.UserId, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_UserEPDAttr.UserType, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_UserEPDAttr.EmpId, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_UserEPDAttr.PostId, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_UserEPDAttr.DeptId, null, "", true, false, 0,  50, 50);
                map.AddTBStringPK(EIP_UserEPDAttr.No, null, "", true, true, 0, 50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class EIP_UserEPDs : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new EIP_UserEPD(); }
        }
    }
}