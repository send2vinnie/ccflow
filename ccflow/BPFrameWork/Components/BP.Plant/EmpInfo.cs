using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.En;
using BP.DA;

namespace BP.Port
{
    public class EmpInfoAttr : EntityNoAttr
    {
        public const string FK_Emp = "FK_Emp";

        public const string Email = "Email";

        public const string Phone = "Phone";

        public const string Sex = "Sex";
    }

    public class EmpInfo : EntityNo
    {
        //public string EmpName
        //{
        //    get
        //    {
        //        return this.GetValStringByKey(Port.EmpAttr.Name);
        //    }
        //}

        public string Email
        {
            get
            {
                return this.GetValStringByKey(EmpInfoAttr.Email);
            }
            set
            {
                this.SetValByKey(EmpInfoAttr.Email, value);
            }
        }

        public string Phone
        {
            get
            {
                return this.GetValStringByKey(EmpInfoAttr.Phone);
            }
            set
            {
                this.SetValByKey(EmpInfoAttr.Phone, value);
            }
        }

        public string Sex
        {
            get
            {
                return this.GetValStringByKey(EmpInfoAttr.Sex);
            }
            set
            {
                this.SetValByKey(EmpInfoAttr.Sex, value);
            }
        }

        public EmpInfo()
        {
        }

        public EmpInfo(string no)
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
                Map map = new Map("Port_EmpInfo");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "用户信息表";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(EmpInfoAttr.No, null, "编号", true, true, 2, 50, 50);
                map.AddTBString(EmpInfoAttr.FK_Emp, null, "Emp外键", true, false, 0, 50, 50);
                map.AddTBString(EmpInfoAttr.Email, null, "邮件地址", true, false, 0, 100, 20);
                map.AddTBString(EmpInfoAttr.Phone, null, "电话号码", true, false, 0, 50, 20);
                map.AddTBInt(EmpInfoAttr.Sex, 0, "性别", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }


    }

    public class EmpInfos : Entities
    {

        public override Entity GetNewEntity
        {
            get { return new EmpInfo(); }
        }
    }
}
