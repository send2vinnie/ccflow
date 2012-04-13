using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public class SSOModuleAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        public const string ModuleNo = "ModuleNo";
        /// <summary>
        /// 模块名称
        /// </summary>
        public const string ModuleName = "ModuleName";
        /// <summary>
        /// 模块类型
        /// </summary>
        public const string ModuleType = "ModuleType";
        /// <summary>
        /// 模块SQL
        /// </summary>
        public const string ModuleSQL = "ModuleSQL";
        /// <summary>
        /// 模块Url
        /// </summary>
        public const string ModuleUrl = "ModuleUrl";
    }

    public class SSOModule : EntityNoName
    {
        public string ModuleNo
        {
            get
            {
                return this.GetValStringByKey(SSOModuleAttr.ModuleNo);
            }
            set
            {
                this.SetValByKey(SSOModuleAttr.ModuleNo, value);
            }
        }

        public string ModuleName
        {
            get
            {
                return this.GetValStringByKey(SSOModuleAttr.ModuleName);
            }
            set
            {
                this.SetValByKey(SSOModuleAttr.ModuleName, value);
            }
        }

        public string ModuleType
        {
            get
            {
                return this.GetValStringByKey(SSOModuleAttr.ModuleType);
            }
            set
            {
                this.SetValByKey(SSOModuleAttr.ModuleType, value);
            }
        }

        public string ModuleSQL
        {
            get
            {
                return this.GetValStringByKey(SSOModuleAttr.ModuleSQL);
            }
            set
            {
                this.SetValByKey(SSOModuleAttr.ModuleSQL, value);
            }
        }

        public string ModuleUrl
        {
            get
            {
                return this.GetValStringByKey(SSOModuleAttr.ModuleUrl);
            }
            set
            {
                this.SetValByKey(SSOModuleAttr.ModuleUrl, value);
            }
        }

        public SSOModule()
        {
        }

        public SSOModule(string no)
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
                Map map = new Map("SSO_Module");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "SSO模块表";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(SSOModuleAttr.No, null, "编号", true, true, 2, 50, 20);
                map.AddTBString(SSOModuleAttr.ModuleNo, null, "模块编号", true, false, 0, 100, 20);
                map.AddTBString(SSOModuleAttr.ModuleName, null, "模块名称", true, false, 0, 100, 20);
                map.AddTBString(SSOModuleAttr.ModuleType, null, "模块类型", true, false, 0, 50, 20);
                map.AddTBString(SSOModuleAttr.ModuleSQL, null, "模块SQL", true, false, 0, 50, 20);
                map.AddTBString(SSOModuleAttr.ModuleUrl, null, "模块模块Url", true, false, 0, 200, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
    }
}
