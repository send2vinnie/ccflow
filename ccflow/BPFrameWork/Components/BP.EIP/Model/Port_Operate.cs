using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_OperateAttr : EntityNoNameAttr
    {
        public const string OperateName = "OperateName";
    }
    
    public partial class Port_Operate : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String OperateName
        {
            get
            {
                return this.GetValStringByKey(Port_OperateAttr.OperateName);
            }
            set
            {
                this.SetValByKey(Port_OperateAttr.OperateName, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_Operate()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_Operate(string No)
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
                Map map = new Map("Port_Operate");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_OperateAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_OperateAttr.OperateName, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_Operates : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_Operate(); }
        }
    }
}