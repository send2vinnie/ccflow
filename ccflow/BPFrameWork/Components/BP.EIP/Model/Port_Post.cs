using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_PostAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string PostNo = "PostNo";
        public const string PostName = "PostName";
        public const string Description = "Description";
        public const string UpDT = "UpDT";
        public const string UpUser = "UpUser";
        public const string Status = "Status";
    }

    public partial class Port_Post : BaseEntity
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(Port_PostAttr.No);
            }
            set
            {
                this.SetValByKey(Port_PostAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String PostNo
        {
            get
            {
                return this.GetValStringByKey(Port_PostAttr.PostNo);
            }
            set
            {
                this.SetValByKey(Port_PostAttr.PostNo, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String PostName
        {
            get
            {
                return this.GetValStringByKey(Port_PostAttr.PostName);
            }
            set
            {
                this.SetValByKey(Port_PostAttr.PostName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Description
        {
            get
            {
                return this.GetValStringByKey(Port_PostAttr.Description);
            }
            set
            {
                this.SetValByKey(Port_PostAttr.Description, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpDT
        {
            get
            {
                return this.GetValDateTime(Port_PostAttr.UpDT);
            }
            set
            {
                this.SetValByKey(Port_PostAttr.UpDT, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UpUser
        {
            get
            {
                return this.GetValStringByKey(Port_PostAttr.UpUser);
            }
            set
            {
                this.SetValByKey(Port_PostAttr.UpUser, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Object Status
        {
            get
            {
                return this.GetValBooleanByKey(Port_PostAttr.Status);
            }
            set
            {
                this.SetValByKey(Port_PostAttr.Status, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_Post()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_Post(string No)
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
                Map map = new Map("Port_Post");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_PostAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_PostAttr.PostNo, null, "", true, false, 0,  10, 10);
                map.AddTBString(Port_PostAttr.PostName, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_PostAttr.Description, null, "", true, false, 0,  1000, 1000);
                map.AddTBDateTime(Port_PostAttr.UpDT, "", true, false);
                map.AddTBString(Port_PostAttr.UpUser, null, "", true, false, 0,  50, 50);
                map.AddBoolean(Port_PostAttr.Status, true, "", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_Posts : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_Post(); }
        }
    }
}