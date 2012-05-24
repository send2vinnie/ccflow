﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.EIP
{
    public partial class Port_StaffDeptAttr : EntityNoNameAttr
    {
        public const string FK_Staff = "FK_Staff";
        public const string FK_Dept = "FK_Dept";
    }

    public partial class Port_StaffDept : EntityNoName
    {
        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public String FK_Staff
        {
            get
            {
                return this.GetValStringByKey(Port_StaffDeptAttr.FK_Staff);
            }
            set
            {
                this.SetValByKey(Port_StaffDeptAttr.FK_Staff, value);
            }
        }

        /// <summary>
        /// 部门, 主外键:对应物理表:Port_Dept,表描述:部门
        /// </summary>
        public String FK_Dept
        {
            get
            {
                return this.GetValStringByKey(Port_StaffDeptAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(Port_StaffDeptAttr.FK_Dept, value);
            }
        }

        #endregion

        #region 构造方法
        /// <summary>
        /// 操作员与工作部门
        /// </summary>
        public Port_StaffDept()
        {
        }
        /// <summary>
        /// 操作员与工作部门
        /// </summary>
        /// <param name="No"></param>
        public Port_StaffDept(string No)
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
                Map map = new Map("Port_StaffDept");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "操作员与工作部门";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(Port_StaffDeptAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(Port_StaffDeptAttr.FK_Staff, null, "", true, false, 0, 50, 50);
                map.AddTBString(Port_StaffDeptAttr.FK_Dept, null, "部门, 主外键:对应物理表:Port_Dept,表描述:部门", true, false, 0, 50, 50);

                this._enMap = map;
                return this._enMap;
            }
        }
    }

    public partial class Port_StaffDepts : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new Port_StaffDept(); }
        }
    }
}