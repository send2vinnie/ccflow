using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_DeptAttr : EntityNoNameAttr
    {
        public const string FullName = "FullName";
        public const string Pid = "Pid";
        public const string Status = "Status";
    }
    
    public partial class Port_Dept : EntityNoName
    {
        #region 属性
        
        /// <summary>
        /// 
        /// </summary>
        public String FullName
        {
            get
            {
                return this.GetValStringByKey(Port_DeptAttr.FullName);
            }
            set
            {
                this.SetValByKey(Port_DeptAttr.FullName, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public String Pid
        {
            get
            {
                return this.GetValStringByKey(Port_DeptAttr.Pid);
            }
            set
            {
                this.SetValByKey(Port_DeptAttr.Pid, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(Port_DeptAttr.Status);
            }
            set
            {
                this.SetValByKey(Port_DeptAttr.Status, value);
            }
        }
        
        #endregion
        
        #region 构造方法
        /// <summary>
        /// 部门
        /// </summary>
        public Port_Dept()
        {
        }
        /// <summary>
        /// 部门
        /// </summary>
        /// <param name="No"></param>
        public Port_Dept(string No)
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
                Map map = new Map("Port_Dept");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "部门";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;
                
                map.AddTBStringPK(Port_DeptAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_DeptAttr.Name, null, "名称", true, false, 0,  100, 100);
                map.AddTBString(Port_DeptAttr.FullName, null, "", true, false, 0,  100, 100);
                map.AddTBString(Port_DeptAttr.Pid, null, "", true, false, 0,  50, 50);
                map.AddTBInt(Port_DeptAttr.Status, 0, "", true, false);
              
                this._enMap = map;
                return this._enMap;
            }
        }
    }
    
    public partial class Port_Depts : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_Dept(); }
        }
    }
}