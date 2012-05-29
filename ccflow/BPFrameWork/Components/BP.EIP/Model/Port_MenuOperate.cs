using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_MenuOperateAttr : EntityNoNameAttr
    {
        public const string FK_Menu = "FK_Menu";
        public const string FK_Operate = "FK_Operate";
    }

    public partial class Port_MenuOperate : BaseEntity
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String FK_Menu
        {
            get
            {
                return this.GetValStringByKey(Port_MenuOperateAttr.FK_Menu);
            }
            set
            {
                this.SetValByKey(Port_MenuOperateAttr.FK_Menu, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String FK_Operate
        {
            get
            {
                return this.GetValStringByKey(Port_MenuOperateAttr.FK_Operate);
            }
            set
            {
                this.SetValByKey(Port_MenuOperateAttr.FK_Operate, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_MenuOperate()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_MenuOperate(string No)
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
                Map map = new Map("Port_MenuOperate");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_MenuOperateAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_MenuOperateAttr.FK_Menu, null, "", true, false, 0,  10, 10);
                map.AddTBString(Port_MenuOperateAttr.FK_Operate, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_MenuOperates : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_MenuOperate(); }
        }
    }
}