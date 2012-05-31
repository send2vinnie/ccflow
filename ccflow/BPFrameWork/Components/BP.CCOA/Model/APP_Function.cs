using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_FunctionAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string FunctionName = "FunctionName";
        public const string FunctionDesc = "FunctionDesc";
        public const string ClassName = "ClassName";
        public const string DomainId = "DomainId";
    }
    
    public partial class APP_Function : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 主键
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_FunctionAttr.No);
            }
            set
            {
                this.SetValByKey(APP_FunctionAttr.No, value);
            }
        }
        
        /// <summary>
        /// 功能名称
        /// </summary>
        public String FunctionName
        {
            get
            {
                return this.GetValStringByKey(APP_FunctionAttr.FunctionName);
            }
            set
            {
                this.SetValByKey(APP_FunctionAttr.FunctionName, value);
            }
        }
        
        /// <summary>
        /// 功能描述
        /// </summary>
        public String FunctionDesc
        {
            get
            {
                return this.GetValStringByKey(APP_FunctionAttr.FunctionDesc);
            }
            set
            {
                this.SetValByKey(APP_FunctionAttr.FunctionDesc, value);
            }
        }
        
        /// <summary>
        /// 类名
        /// </summary>
        public String ClassName
        {
            get
            {
                return this.GetValStringByKey(APP_FunctionAttr.ClassName);
            }
            set
            {
                this.SetValByKey(APP_FunctionAttr.ClassName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String DomainId
        {
            get
            {
                return this.GetValStringByKey(APP_FunctionAttr.DomainId);
            }
            set
            {
                this.SetValByKey(APP_FunctionAttr.DomainId, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_Function()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_Function(string No)
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
                
                map.AddTBStringPK(APP_FunctionAttr.No, null, "主键", true, true, 0, 50, 50);
                map.AddTBString(APP_FunctionAttr.FunctionName, null, "功能名称", true, false, 0,  50, 50);
                map.AddTBString(APP_FunctionAttr.FunctionDesc, null, "功能描述", true, false, 0,  100, 100);
                map.AddTBString(APP_FunctionAttr.ClassName, null, "类名", true, false, 0,  50, 50);
                map.AddTBString(APP_FunctionAttr.DomainId, null, "", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_Functions : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_Function(); }
        }
    }
}