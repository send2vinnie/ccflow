using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_FunctionAttr : EntityNoNameAttr
    {
        public const string FunctionName = "FunctionName";
        public const string FunctionDesc = "FunctionDesc";
        public const string ClassName = "ClassName";
        public const string FK_Domain = "FK_Domain";
    }

    public partial class Port_Function : BaseEntity
    {
        #region 属性
        
        /// <summary>
        /// 功能名称
        /// </summary>
        public String FunctionName
        {
            get
            {
                return this.GetValStringByKey(Port_FunctionAttr.FunctionName);
            }
            set
            {
                this.SetValByKey(Port_FunctionAttr.FunctionName, value);
            }
        }
        
        /// <summary>
        /// 功能描述
        /// </summary>
        public String FunctionDesc
        {
            get
            {
                return this.GetValStringByKey(Port_FunctionAttr.FunctionDesc);
            }
            set
            {
                this.SetValByKey(Port_FunctionAttr.FunctionDesc, value);
            }
        }
        
        /// <summary>
        /// 类名
        /// </summary>
        public String ClassName
        {
            get
            {
                return this.GetValStringByKey(Port_FunctionAttr.ClassName);
            }
            set
            {
                this.SetValByKey(Port_FunctionAttr.ClassName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String FK_Domain
        {
            get
            {
                return this.GetValStringByKey(Port_FunctionAttr.FK_Domain);
            }
            set
            {
                this.SetValByKey(Port_FunctionAttr.FK_Domain, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_Function()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_Function(string No)
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
                Map map = new Map("Port_Function");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_FunctionAttr.No, null, "主键", true, true, 0, 50, 50);
                map.AddTBString(Port_FunctionAttr.FunctionName, null, "功能名称", true, false, 0,  50, 50);
                map.AddTBString(Port_FunctionAttr.FunctionDesc, null, "功能描述", true, false, 0,  100, 100);
                map.AddTBString(Port_FunctionAttr.ClassName, null, "类名", true, false, 0,  50, 50);
                map.AddTBString(Port_FunctionAttr.FK_Domain, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_Functions : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_Function(); }
        }
    }
}