using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_RuleAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string Permission = "Permission";
        public const string RulePolicy = "RulePolicy";
        public const string RuleGroup = "RuleGroup";
        public const string Description = "Description";
        public const string Domain_Id = "Domain_Id";
        public const string Perfix = "Perfix";
        public const string RuleType = "RuleType";
    }
    
    public partial class APP_Rule : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 主键
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_RuleAttr.No);
            }
            set
            {
                this.SetValByKey(APP_RuleAttr.No, value);
            }
        }
        
        /// <summary>
        /// 需要控制的权限名称
        /// </summary>
        public String Permission
        {
            get
            {
                return this.GetValStringByKey(APP_RuleAttr.Permission);
            }
            set
            {
                this.SetValByKey(APP_RuleAttr.Permission, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String RulePolicy
        {
            get
            {
                return this.GetValStringByKey(APP_RuleAttr.RulePolicy);
            }
            set
            {
                this.SetValByKey(APP_RuleAttr.RulePolicy, value);
            }
        }
        
        /// <summary>
        /// 规则的分组，提现友好性
        /// </summary>
        public String RuleGroup
        {
            get
            {
                return this.GetValStringByKey(APP_RuleAttr.RuleGroup);
            }
            set
            {
                this.SetValByKey(APP_RuleAttr.RuleGroup, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Description
        {
            get
            {
                return this.GetValStringByKey(APP_RuleAttr.Description);
            }
            set
            {
                this.SetValByKey(APP_RuleAttr.Description, value);
            }
        }
        
        /// <summary>
        /// 所属管理域
        /// </summary>
        public String Domain_Id
        {
            get
            {
                return this.GetValStringByKey(APP_RuleAttr.Domain_Id);
            }
            set
            {
                this.SetValByKey(APP_RuleAttr.Domain_Id, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Perfix
        {
            get
            {
                return this.GetValStringByKey(APP_RuleAttr.Perfix);
            }
            set
            {
                this.SetValByKey(APP_RuleAttr.Perfix, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String RuleType
        {
            get
            {
                return this.GetValStringByKey(APP_RuleAttr.RuleType);
            }
            set
            {
                this.SetValByKey(APP_RuleAttr.RuleType, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_Rule()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_Rule(string No)
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
                Map map = new Map("APP_Rule");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(APP_RuleAttr.No, null, "主键", true, true, 0, 50, 50);
                map.AddTBString(APP_RuleAttr.Permission, null, "需要控制的权限名称", true, false, 0,  100, 100);
                map.AddTBString(APP_RuleAttr.RulePolicy, null, "", true, false, 0,  200, 200);
                map.AddTBString(APP_RuleAttr.RuleGroup, null, "规则的分组，提现友好性", true, false, 0,  50, 50);
                map.AddTBString(APP_RuleAttr.Description, null, "", true, false, 0,  250, 250);
                map.AddTBString(APP_RuleAttr.Domain_Id, null, "所属管理域", true, false, 0,  50, 50);
                map.AddTBString(APP_RuleAttr.Perfix, null, "", true, false, 0,  2, 2);
                map.AddTBString(APP_RuleAttr.RuleType, null, "", true, false, 0,  1, 1);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_Rules : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_Rule(); }
        }
    }
}