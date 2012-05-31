using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.DA;
using BP.En;

namespace BP.CCOA
{
    public partial class EIP_DeptAttr : EntityNoNameAttr
    {
        public const string DeptNo = "DeptNo";
        public const string DeptName = "DeptName";
        public const string FullName = "FullName";
        public const string Pid = "Pid";
        public const string CreateDate = "CreateDate";
        public const string UpDT = "UpDT";
        public const string UpUser = "UpUser";
        public const string Status = "Status";
    }

    public partial class EIP_Dept : EntityNoName
    {
        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public String DeptNo
        {
            get
            {
                return this.GetValStringByKey(EIP_DeptAttr.DeptNo);
            }
            set
            {
                this.SetValByKey(EIP_DeptAttr.DeptNo, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String DeptName
        {
            get
            {
                return this.GetValStringByKey(EIP_DeptAttr.DeptName);
            }
            set
            {
                this.SetValByKey(EIP_DeptAttr.DeptName, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FullName
        {
            get
            {
                return this.GetValStringByKey(EIP_DeptAttr.FullName);
            }
            set
            {
                this.SetValByKey(EIP_DeptAttr.FullName, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Pid
        {
            get
            {
                return this.GetValStringByKey(EIP_DeptAttr.Pid);
            }
            set
            {
                this.SetValByKey(EIP_DeptAttr.Pid, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return this.GetValDateTime(EIP_DeptAttr.CreateDate);
            }
            set
            {
                this.SetValByKey(EIP_DeptAttr.CreateDate, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime UpDT
        {
            get
            {
                return this.GetValDateTime(EIP_DeptAttr.UpDT);
            }
            set
            {
                this.SetValByKey(EIP_DeptAttr.UpDT, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String UpUser
        {
            get
            {
                return this.GetValStringByKey(EIP_DeptAttr.UpUser);
            }
            set
            {
                this.SetValByKey(EIP_DeptAttr.UpUser, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Object Status
        {
            get
            {
                return this.GetValBooleanByKey(EIP_DeptAttr.Status);
            }
            set
            {
                this.SetValByKey(EIP_DeptAttr.Status, value);
            }
        }

        #endregion

        #region 构造方法
        /// <summary>
        /// 
        /// </summary>
        public EIP_Dept()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="No"></param>
        public EIP_Dept(string No)
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
                Map map = new Map("EIP_Dept");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "";
                map.EnType = EnType.Sys;
                map.IsAutoGenerNo = false;

                map.AddTBStringPK(EIP_DeptAttr.No, null, "", true, true, 0, 50, 50);
                map.AddTBString(EIP_DeptAttr.DeptNo, null, "", true, false, 0, 20, 20);
                map.AddTBString(EIP_DeptAttr.DeptName, null, "", true, false, 0, 50, 50);
                map.AddTBString(EIP_DeptAttr.FullName, null, "", true, false, 0, 100, 100);
                map.AddTBString(EIP_DeptAttr.Pid, null, "", true, false, 0, 50, 50);
                map.AddTBDateTime(EIP_DeptAttr.CreateDate, "", true, false);
                map.AddTBDateTime(EIP_DeptAttr.UpDT, "", true, false);
                map.AddTBString(EIP_DeptAttr.UpUser, null, "", true, false, 0, 50, 50);
                map.AddTBInt(EIP_DeptAttr.Status, 1, "状态", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
    }

    public partial class EIP_Depts : Entities
    {
        public override Entity GetNewEntity
        {
            get { return new EIP_Dept(); }
        }
    }
}