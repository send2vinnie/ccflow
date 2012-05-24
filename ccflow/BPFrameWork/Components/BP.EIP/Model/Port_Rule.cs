using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_RuleAttr : EntityNoNameAttr
    {
        public const string Permission = "Permission";
        public const string RulePolicy = "RulePolicy";
        public const string RuleGroup = "RuleGroup";
        public const string Description = "Description";
        public const string Perfix = "Perfix";
        public const string RuleType = "RuleType";
        public const string FK_Domain = "FK_Domain";
    }
    
    public partial class Port_Rule : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 需要控制的权限名称
        /// </summary>
        public String Permission
        {
            get
            {
                return this.GetValStringByKey(Port_RuleAttr.Permission);
            }
            set
            {
                this.SetValByKey(Port_RuleAttr.Permission, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String RulePolicy
        {
            get
            {
                return this.GetValStringByKey(Port_RuleAttr.RulePolicy);
            }
            set
            {
                this.SetValByKey(Port_RuleAttr.RulePolicy, value);
            }
        }
        
        /// <summary>
        /// 规则的分组，提现友好性
        /// </summary>
        public String RuleGroup
        {
            get
            {
                return this.GetValStringByKey(Port_RuleAttr.RuleGroup);
            }
            set
            {
                this.SetValByKey(Port_RuleAttr.RuleGroup, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Description
        {
            get
            {
                return this.GetValStringByKey(Port_RuleAttr.Description);
            }
            set
            {
                this.SetValByKey(Port_RuleAttr.Description, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Perfix
        {
            get
            {
                return this.GetValStringByKey(Port_RuleAttr.Perfix);
            }
            set
            {
                this.SetValByKey(Port_RuleAttr.Perfix, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String RuleType
        {
            get
            {
                return this.GetValStringByKey(Port_RuleAttr.RuleType);
            }
            set
            {
                this.SetValByKey(Port_RuleAttr.RuleType, value);
            }
        }
        
        /// <summary>
        /// 所属管理域
        /// </summary>
        public String FK_Domain
        {
            get
            {
                return this.GetValStringByKey(Port_RuleAttr.FK_Domain);
            }
            set
            {
                this.SetValByKey(Port_RuleAttr.FK_Domain, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_Rule()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_Rule(string No)
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
                Map map = new Map("Port_Rule");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_RuleAttr.No, null, "主键", true, true, 0, 50, 50);
                map.AddTBString(Port_RuleAttr.Permission, null, "需要控制的权限名称", true, false, 0,  100, 100);
                map.AddTBString(Port_RuleAttr.RulePolicy, null, "", true, false, 0,  200, 200);
                map.AddTBString(Port_RuleAttr.RuleGroup, null, "规则的分组，提现友好性", true, false, 0,  50, 50);
                map.AddTBString(Port_RuleAttr.Description, null, "", true, false, 0,  250, 250);
                map.AddTBString(Port_RuleAttr.Perfix, null, "", true, false, 0,  2, 2);
                map.AddTBString(Port_RuleAttr.RuleType, null, "", true, false, 0,  2, 2);
                map.AddTBString(Port_RuleAttr.FK_Domain, null, "所属管理域", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_Rules : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_Rule(); }
        }
    }
}