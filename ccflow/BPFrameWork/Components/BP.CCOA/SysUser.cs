using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public class SysUserAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public const string UserName = "UserName";
        /// <summary>
        /// 密码
        /// </summary>
        public const string Password = "Password";
        /// <summary>
        /// 系统ID
        /// </summary>
        public const string SysID = "SysID";
    }
    public class SysUser : EntityNoName
    {
        public string UserName
        {
            get
            {
                return this.GetValStringByKey(SysUserAttr.UserName);
            }
            set
            {
                this.SetValByKey(SysUserAttr.UserName, value);
            }
        }

        public string Password
        {
            get
            {
                return this.GetValStringByKey(SysUserAttr.Password);
            }
            set
            {
                this.SetValByKey(SysUserAttr.Password, value);
            }
        }

        public string SysID
        {
            get
            {
                return this.GetValStringByKey(SysUserAttr.SysID);
            }
            set
            {
                this.SetValByKey(SysUserAttr.SysID, value);
            }
        }

        public SysUser()
        {
        }

        public SysUser(string no)
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
                Map map = new Map("Port_SysUser");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "系统用户集合";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(SysUserAttr.No, null, "编号", true, true, 2, 50, 20);
                map.AddTBString(SysUserAttr.UserName, null, "用户名", true, false, 0, 100, 20);
                map.AddTBString(SysUserAttr.Password, null, "密码", true, false, 0, 100, 20);
                map.AddTBString(SysUserAttr.SysID, null, "系统ID", true, false, 0, 30, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
    }
}
