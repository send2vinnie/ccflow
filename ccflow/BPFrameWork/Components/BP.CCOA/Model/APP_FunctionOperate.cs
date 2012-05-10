using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class APP_FunctionOperateAttr : EntityNoNameAttr
    {
        public const string No = "No";
        public const string FunctionId = "FunctionId";
        public const string OperateName = "OperateName";
        public const string OperateDesc = "OperateDesc";
        public const string Control_Name = "Control_Name";
    }
    
    public partial class APP_FunctionOperate : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 主键
        /// </summary>
        public String No
        {
            get
            {
                return this.GetValStringByKey(APP_FunctionOperateAttr.No);
            }
            set
            {
                this.SetValByKey(APP_FunctionOperateAttr.No, value);
            }
        }
        
        /// <summary>
        /// 所属功能ID
        /// </summary>
        public String FunctionId
        {
            get
            {
                return this.GetValStringByKey(APP_FunctionOperateAttr.FunctionId);
            }
            set
            {
                this.SetValByKey(APP_FunctionOperateAttr.FunctionId, value);
            }
        }
        
        /// <summary>
        /// 操作名称
        /// </summary>
        public String OperateName
        {
            get
            {
                return this.GetValStringByKey(APP_FunctionOperateAttr.OperateName);
            }
            set
            {
                this.SetValByKey(APP_FunctionOperateAttr.OperateName, value);
            }
        }
        
        /// <summary>
        /// 功能描述
        /// </summary>
        public String OperateDesc
        {
            get
            {
                return this.GetValStringByKey(APP_FunctionOperateAttr.OperateDesc);
            }
            set
            {
                this.SetValByKey(APP_FunctionOperateAttr.OperateDesc, value);
            }
        }
        
        /// <summary>
        /// 控件名称
        /// </summary>
        public String Control_Name
        {
            get
            {
                return this.GetValStringByKey(APP_FunctionOperateAttr.Control_Name);
            }
            set
            {
                this.SetValByKey(APP_FunctionOperateAttr.Control_Name, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public APP_FunctionOperate()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public APP_FunctionOperate(string No)
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
                Map map = new Map("APP_FunctionOperate");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(APP_FunctionOperateAttr.No, null, "主键", true, true, 0, 50, 50);
                map.AddTBString(APP_FunctionOperateAttr.FunctionId, null, "所属功能ID", true, false, 0,  50, 50);
                map.AddTBString(APP_FunctionOperateAttr.OperateName, null, "操作名称", true, false, 0,  50, 50);
                map.AddTBString(APP_FunctionOperateAttr.OperateDesc, null, "功能描述", true, false, 0,  1000, 1000);
                map.AddTBString(APP_FunctionOperateAttr.Control_Name, null, "控件名称", true, false, 0,  50, 50);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class APP_FunctionOperates : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new APP_FunctionOperate(); }
        }
    }
}