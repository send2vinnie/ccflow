using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_FunctionOperateAttr : EntityNoNameAttr
    {
        public const string FK_Function = "FK_Function";
        public const string OperateName = "OperateName";
        public const string OperateDesc = "OperateDesc";
        public const string Control_Name = "Control_Name";
    }
    
    public partial class Port_FunctionOperate : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 所属功能ID
        /// </summary>
        public String FK_Function
        {
            get
            {
                return this.GetValStringByKey(Port_FunctionOperateAttr.FK_Function);
            }
            set
            {
                this.SetValByKey(Port_FunctionOperateAttr.FK_Function, value);
            }
        }
        
        /// <summary>
        /// 操作名称
        /// </summary>
        public String OperateName
        {
            get
            {
                return this.GetValStringByKey(Port_FunctionOperateAttr.OperateName);
            }
            set
            {
                this.SetValByKey(Port_FunctionOperateAttr.OperateName, value);
            }
        }
        
        /// <summary>
        /// 功能描述
        /// </summary>
        public String OperateDesc
        {
            get
            {
                return this.GetValStringByKey(Port_FunctionOperateAttr.OperateDesc);
            }
            set
            {
                this.SetValByKey(Port_FunctionOperateAttr.OperateDesc, value);
            }
        }
        
        /// <summary>
        /// 控件名称
        /// </summary>
        public String Control_Name
        {
            get
            {
                return this.GetValStringByKey(Port_FunctionOperateAttr.Control_Name);
            }
            set
            {
                this.SetValByKey(Port_FunctionOperateAttr.Control_Name, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public Port_FunctionOperate()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public Port_FunctionOperate(string No)
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
                Map map = new Map("Port_FunctionOperate");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_FunctionOperateAttr.No, null, "主键", true, true, 0, 50, 50);
                map.AddTBString(Port_FunctionOperateAttr.FK_Function, null, "所属功能ID", true, false, 0,  50, 50);
                map.AddTBString(Port_FunctionOperateAttr.OperateName, null, "操作名称", true, false, 0,  50, 50);
                map.AddTBString(Port_FunctionOperateAttr.OperateDesc, null, "功能描述", true, false, 0,  1000, 1000);
                map.AddTBString(Port_FunctionOperateAttr.Control_Name, null, "控件名称", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_FunctionOperates : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_FunctionOperate(); }
        }
    }
}