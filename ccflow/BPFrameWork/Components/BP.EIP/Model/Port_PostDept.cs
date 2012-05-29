using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;
using BP.EIP;

namespace BP.CCOA
{
    public partial class EIP_PostDeptAttr : EntityNoNameAttr
    {
        public const string PostId = "PostId";
        public const string OrgId = "OrgId";
        public const string No = "No";
    }

    public partial class EIP_PostDept : BaseEntity
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String PostId
        {
            get
            {
                return this.GetValStringByKey(EIP_PostDeptAttr.PostId);
            }
            set
            {
                this.SetValByKey(EIP_PostDeptAttr.PostId, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String OrgId
        {
            get
            {
                return this.GetValStringByKey(EIP_PostDeptAttr.OrgId);
            }
            set
            {
                this.SetValByKey(EIP_PostDeptAttr.OrgId, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(EIP_PostDeptAttr.No);
            }
            set
            {
                this.SetValByKey(EIP_PostDeptAttr.No, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public EIP_PostDept()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public EIP_PostDept(string No)
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
                Map map = new Map("EIP_PostDept");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBString(EIP_PostDeptAttr.PostId, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_PostDeptAttr.OrgId, null, "", true, false, 0,  50, 50);
                map.AddTBStringPK(EIP_PostDeptAttr.No, null, "", true, true, 0, 50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class EIP_PostDepts : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new EIP_PostDept(); }
        }
    }
}