using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class EIP_LayoutAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string UserId = "UserId";
    }
    
    public partial class EIP_Layout : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(EIP_LayoutAttr.No);
            }
            set
            {
                this.SetValByKey(EIP_LayoutAttr.No, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String UserId
        {
            get
            {
                return this.GetValStringByKey(EIP_LayoutAttr.UserId);
            }
            set
            {
                this.SetValByKey(EIP_LayoutAttr.UserId, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public EIP_Layout()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public EIP_Layout(string No)
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
                Map map = new Map("EIP_Layout");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(EIP_LayoutAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(EIP_LayoutAttr.UserId, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class EIP_Layouts : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new EIP_Layout(); }
        }
    }
}