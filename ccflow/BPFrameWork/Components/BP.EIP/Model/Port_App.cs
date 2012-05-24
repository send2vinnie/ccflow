using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_AppAttr : EntityNoNameAttr
    {
        public const string AppName = "AppName";
        public const string Description = "Description";
    }
    
    public partial class Port_App : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public string AppName
        {
            get
            {
                return this.GetValStringByKey(Port_AppAttr.AppName);
            }
            set
            {
                this.SetValByKey(Port_AppAttr.AppName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get
            {
                return this.GetValStringByKey(Port_AppAttr.Description);
            }
            set
            {
                this.SetValByKey(Port_AppAttr.Description, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 部门
        /// </summary>
        public Port_App()
        {
        }
        /// <summary>
        /// 部门
        /// </summary>
        /// <param name="No"></param>
        public Port_App(string No)
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
                Map map = new Map("Port_App");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "应用程序";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_AppAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_AppAttr.AppName, null, "名称", true, false, 0, 100, 100);
                map.AddTBString(Port_AppAttr.Description, null, "描述", true, false, 0, 1000, 1000);
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_Apps : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_App(); }
        }
    }
}