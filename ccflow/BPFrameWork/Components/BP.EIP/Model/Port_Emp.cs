using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_EmpAttr : EntityNoNameAttr
    {
        public const string Pass = "Pass";
        public const string FK_Dept = "FK_Dept";
        public const string PID = "PID";
        public const string PIN = "PIN";
        public const string KeyPass = "KeyPass";
        public const string IsUSBKEY = "IsUSBKEY";
        public const string FK_Emp = "FK_Emp";
        public const string IsLogin = "IsLogin";
        public const string AuditStatus = "AuditStatus";
        public const string Status = "Status";
    }

    public partial class Port_Emp : BaseEntity
    {
        #region 属性

        /// <summary>
        /// 密码
        /// </summary>
        public String Pass
        {
            get
            {
                return this.GetValStringByKey(Port_EmpAttr.Pass);
            }
            set
            {
                this.SetValByKey(Port_EmpAttr.Pass, value);
            }
        }

        /// <summary>
        /// 部门, 外键:对应物理表:Port_Dept,表描述:部门
        /// </summary>
        public String FK_Dept
        {
            get
            {
                return this.GetValStringByKey(Port_EmpAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(Port_EmpAttr.FK_Dept, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String PID
        {
            get
            {
                return this.GetValStringByKey(Port_EmpAttr.PID);
            }
            set
            {
                this.SetValByKey(Port_EmpAttr.PID, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String PIN
        {
            get
            {
                return this.GetValStringByKey(Port_EmpAttr.PIN);
            }
            set
            {
                this.SetValByKey(Port_EmpAttr.PIN, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String KeyPass
        {
            get
            {
                return this.GetValStringByKey(Port_EmpAttr.KeyPass);
            }
            set
            {
                this.SetValByKey(Port_EmpAttr.KeyPass, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String IsUSBKEY
        {
            get
            {
                return this.GetValStringByKey(Port_EmpAttr.IsUSBKEY);
            }
            set
            {
                this.SetValByKey(Port_EmpAttr.IsUSBKEY, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FK_Emp
        {
            get
            {
                return this.GetValStringByKey(Port_EmpAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(Port_EmpAttr.FK_Emp, value);
            }
        }

        /// <summary>
        /// 是否已登录
        /// </summary>
        public int IsLogin
        {
            get
            {
                return this.GetValIntByKey(Port_EmpAttr.IsLogin);
            }
            set
            {
                this.SetValByKey(Port_EmpAttr.IsLogin, value);
            }
        }

        /// <summary>
        /// 授权状态
        /// </summary>
        public int AuditStatus
        {
            get
            {
                return this.GetValIntByKey(Port_EmpAttr.AuditStatus);
            }
            set
            {
                this.SetValByKey(Port_EmpAttr.AuditStatus, value);
            }
        }

        /// <summary>
        /// 状态：0-失效；1-有效
        /// </summary>
        public int Status
        {
            get
            {
                return this.GetValIntByKey(Port_EmpAttr.Status);
            }
            set
            {
                this.SetValByKey(Port_EmpAttr.Status, value);
            }
        }

        #endregion

        #region 构造方法
        /// <summary>
        /// 用户
        /// </summary>
        public Port_Emp()
        {
        }
        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="No"></param>
        public Port_Emp(string No)
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
                Map map = new Map("Port_Emp");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "用户";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(Port_EmpAttr.No, null, "", true, true, 0, 20, 20);
                map.AddTBString(Port_EmpAttr.Name, null, "名称", true, false, 0, 100, 100);
                map.AddTBString(Port_EmpAttr.Pass, null, "密码", true, false, 0, 20, 20);
                map.AddTBString(Port_EmpAttr.FK_Dept, null, "部门, 外键:对应物理表:Port_Dept,表描述:部门", true, false, 0, 50, 50);
                map.AddTBString(Port_EmpAttr.PID, null, "", true, false, 0, 100, 100);
                map.AddTBString(Port_EmpAttr.PIN, null, "", true, false, 0, 100, 100);
                map.AddTBString(Port_EmpAttr.KeyPass, null, "", true, false, 0, 100, 100);
                map.AddTBString(Port_EmpAttr.IsUSBKEY, null, "", true, false, 0, 100, 100);
                map.AddTBString(Port_EmpAttr.FK_Emp, null, "", true, false, 0, 50, 50);
                map.AddTBInt(Port_EmpAttr.IsLogin, 1, "是否已登录", true, false);
                map.AddTBInt(Port_EmpAttr.AuditStatus, 1, "授权状态", true, false);
                map.AddTBInt(Port_EmpAttr.Status, 1, "状态", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
    }

    public partial class Port_Emps : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_Emp(); }
        }
    }
}