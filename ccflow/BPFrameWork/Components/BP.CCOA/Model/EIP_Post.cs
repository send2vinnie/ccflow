using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class EIP_PostAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string PostNo = "PostNo";
        public const string PostName = "PostName";
        public const string Description = "Description";
        public const string UpDT = "UpDT";
        public const string UpUser = "UpUser";
        public const string Status = "Status";
    }
    
    public partial class EIP_Post : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(EIP_PostAttr.No);
            }
            set
            {
                this.SetValByKey(EIP_PostAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String PostNo
        {
            get
            {
                return this.GetValStringByKey(EIP_PostAttr.PostNo);
            }
            set
            {
                this.SetValByKey(EIP_PostAttr.PostNo, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String PostName
        {
            get
            {
                return this.GetValStringByKey(EIP_PostAttr.PostName);
            }
            set
            {
                this.SetValByKey(EIP_PostAttr.PostName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Description
        {
            get
            {
                return this.GetValStringByKey(EIP_PostAttr.Description);
            }
            set
            {
                this.SetValByKey(EIP_PostAttr.Description, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpDT
        {
            get
            {
                return this.GetValDateTime(EIP_PostAttr.UpDT);
            }
            set
            {
                this.SetValByKey(EIP_PostAttr.UpDT, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpUser
        {
            get
            {
                return this.GetValStringByKey(EIP_PostAttr.UpUser);
            }
            set
            {
                this.SetValByKey(EIP_PostAttr.UpUser, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Object Status
        {
            get
            {
                return this.GetValBooleanByKey(EIP_PostAttr.Status);
            }
            set
            {
                this.SetValByKey(EIP_PostAttr.Status, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public EIP_Post()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public EIP_Post(string No)
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
                Map map = new Map("EIP_Post");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(EIP_PostAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(EIP_PostAttr.PostNo, null, "", true, false, 0,  10, 10);
                map.AddTBString(EIP_PostAttr.PostName, null, "", true, false, 0,  50, 50);
                map.AddTBString(EIP_PostAttr.Description, null, "", true, false, 0,  1000, 1000);
                map.AddTBDateTime(EIP_PostAttr.UpDT, "", true, false);
                map.AddTBString(EIP_PostAttr.UpUser, null, "", true, false, 0,  50, 50);
                map.AddBoolean(EIP_PostAttr.Status, true, "", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class EIP_Posts : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new EIP_Post(); }
        }
    }
}