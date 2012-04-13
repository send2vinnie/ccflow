using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public class SSOCustomerModuleAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public const string UserNo = "UserNo";
        /// <summary>
        /// 排列顺序
        /// </summary>
        public const string ModuleOrder = "ModuleOrder";
    }

    public class SSOCustomerModule : EntityNoName
    {
        public string UserNo
        {
            get
            {
                return this.GetValStringByKey(SSOCustomerModuleAttr.UserNo);
            }
            set
            {
                this.SetValByKey(SSOCustomerModuleAttr.UserNo, value);
            }
        }

        public string ModuleOrder
        {
            get
            {
                return this.GetValStringByKey(SSOCustomerModuleAttr.ModuleOrder);
            }
            set
            {
                this.SetValByKey(SSOCustomerModuleAttr.ModuleOrder, value);
            }
        }

        public SSOCustomerModule()
        {
        }

        public SSOCustomerModule(string no)
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
                Map map = new Map("SSO_CustomerModule");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "SSO用户自定义表";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(SSOCustomerModuleAttr.No, null, "编号", true, true, 2, 50, 20);
                map.AddTBString(SSOCustomerModuleAttr.UserNo, null, "模块编号", true, false, 0, 100, 20);
                map.AddTBString(SSOCustomerModuleAttr.ModuleOrder, null, "模块名称", true, false, 0, 100, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
    }
}
