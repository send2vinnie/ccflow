using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public class SysInfoAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public const string SysName = "SysName";
        /// <summary>
        /// 描述
        /// </summary>
        public const string SysDescription = "SysDescription";
        /// <summary>
        /// 系统地址
        /// </summary>
        public const string SysUrl = "SysUrl";
        /// <summary>
        /// 显示排序
        /// </summary>
        public const string SysOrder = "SysOrder";
        /// <summary>
        /// 组别
        /// </summary>
        public const string SysGroup = "SysGroup";
        /// <summary>
        /// 组别名称
        /// </summary>
        public const string SysGroupName = "SysGroupName";
    }

    public class SysInfo : EntityNoName
    {
        public string SysName
        {
            get
            {
                return this.GetValStringByKey(SysInfoAttr.SysName);
            }
            set
            {
                this.SetValByKey(SysInfoAttr.SysName, value);
            }
        }

        public string SysDescription
        {
            get
            {
                return this.GetValStringByKey(SysInfoAttr.SysDescription);
            }
            set
            {
                this.SetValByKey(SysInfoAttr.SysDescription, value);
            }
        }

        public string SysUrl
        {
            get
            {
                return this.GetValStringByKey(SysInfoAttr.SysUrl);
            }
            set
            {
                this.SetValByKey(SysInfoAttr.SysUrl, value);
            }
        }
        public string SysOrder
        {
            get
            {
                return this.GetValStringByKey(SysInfoAttr.SysOrder);
            }
            set
            {
                this.SetValByKey(SysInfoAttr.SysOrder, value);
            }
        }

        public string SysGroup
        {
            get
            {
                return this.GetValStringByKey(SysInfoAttr.SysGroup);
            }
            set
            {
                this.SetValByKey(SysInfoAttr.SysGroup, value);
            }
        }

        public string SysGroupName
        {
            get
            {
                return this.GetValStringByKey(SysInfoAttr.SysGroupName);
            }
            set
            {
                this.SetValByKey(SysInfoAttr.SysGroupName, value);
            }
        }
        
        public SysInfo()
        {
        }

        public SysInfo(string no)
        {
            this.No = no;
            this.Retrieve();
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Port_SysInfo");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "系统表";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(SysUserAttr.No, null, "编号", true, true, 2, 50, 20);
                map.AddTBString(SysInfoAttr.SysName, null, "系统名称", true, false, 0, 100, 20);
                map.AddTBString(SysInfoAttr.SysDescription, null, "描述", true, false, 0, 100, 20);
                map.AddTBString(SysInfoAttr.SysUrl, null, "系统地址", true, false, 0, 200, 20);
                map.AddTBString(SysInfoAttr.SysOrder, null, "显示排序", true, false, 0, 10, 20);
                map.AddTBString(SysInfoAttr.SysGroup, null, "所属组别", true, false, 0, 10, 20);
                map.AddTBString(SysInfoAttr.SysGroupName, null, "组别名称", true, false, 0, 30, 20);
                
                this._enMap = map;
                return this._enMap;
            }
        }

    }
}
