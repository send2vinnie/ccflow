using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_StationOperateAttr : EntityNoNameAttr
    {
        public const string FK_Role = "FK_Role";
        public const string FK_Operate = "FK_Operate";
    }
    
    public partial class Port_StationOperate : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String FK_Role
        {
            get
            {
                return this.GetValStringByKey(Port_StationOperateAttr.FK_Role);
            }
            set
            {
                this.SetValByKey(Port_StationOperateAttr.FK_Role, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String FK_Operate
        {
            get
            {
                return this.GetValStringByKey(Port_StationOperateAttr.FK_Operate);
            }
            set
            {
                this.SetValByKey(Port_StationOperateAttr.FK_Operate, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_StationOperate()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_StationOperate(string No)
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
                Map map = new Map("Port_StationOperate");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "角色和权限关联表";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_StationOperateAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_StationOperateAttr.FK_Role, null, "", true, false, 0,  50, 50);
                map.AddTBString(Port_StationOperateAttr.FK_Operate, null, "", true, false, 0,  10, 10);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_StationOperates : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_StationOperate(); }
        }
    }
}