using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 单位通讯录属性
    /// </summary>
    public class ListDeptAttr : EntityOIDNameAttr
    {
        /// <summary>
        /// No
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// Addr
        /// </summary>
        public const string Addr = "Addr";
        /// <summary>
        /// 电话
        /// </summary>
        public const string Tel = "Tel";
        /// <summary>
        /// 邮件
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// 传真
        /// </summary>
        public const string Fax = "Fax";
        /// <summary>
        /// 领导
        /// </summary>
        public const string Learder = "Learder";
        /// <summary>
        /// 值班手机
        /// </summary>
        public const string HandSet = "HandSet";
        public const string Note = "Note";
        public const string WorkFloor = "WorkFloor";
    }
    /// <summary>
    /// 单位通讯录
    /// </summary>
    public class ListDept : BP.En.EntityNoName
    {
        #region 基本属性
        /// <summary>
        /// 管理员电话
        /// </summary>
        public string HandSet
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.HandSet);
            }
            set
            {
                this.SetValByKey(ListDeptAttr.HandSet, value);
            }
        }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string Learder
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Learder);
            }
            set
            {
                this.SetValByKey(ListDeptAttr.Learder, value);
            }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Addr);
            }
            set
            {
                this.SetValByKey(ListDeptAttr.Addr, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Note);
            }
        }
        public string Fax
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Fax);
            }
        }
        public string Email
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Email);
            }
        }

        public string Tel
        {
            get
            {
                return this.GetValStringByKey(ListDeptAttr.Tel);
            }
            set
            {
                this.SetValByKey(ListDeptAttr.Tel, value);
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 单位通讯录
        /// </summary>
        public ListDept()
        {
        }
        /// <summary>
        /// 单位通讯录
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("GE_ListDept");
                map.EnType = EnType.Sys;
                map.EnDesc = "单位通讯录";
                map.DepositaryOfEntity = Depositary.None;
                map.MoveTo = ListDeptAttr.WorkFloor;

                // map.AddTBIntPKOID();
                map.AddTBStringPK(ListDeptAttr.No, null, "单位编号", true, false, 2, 10, 10);
                map.AddTBString(ListDeptAttr.Name, null, "单位名称", true, false, 0, 100, 100);
                map.AddTBString(ListDeptAttr.Learder, null, "领导", true, false, 0, 100, 10);
                map.AddTBString(ListDeptAttr.HandSet, null, "值班手机", true, false, 0, 100, 10);
                map.AddTBString(ListDeptAttr.Tel, null, "联系电话", true, false, 0, 100, 10);
                map.AddTBString(ListDeptAttr.Fax, null, "Fax", true, false, 0, 100, 10);
                map.AddTBString(ListDeptAttr.Email, null, "Email", true, false, 0, 100, 10);
                map.AddTBString(ListDeptAttr.Note, null, "备注", true, false, 0, 500, 10);

                map.AddDtl(new ListEmps(), ListEmpAttr.FK_Dept);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 单位通讯录s
    /// </summary>
    public class ListDepts : EntitiesNoName
    {
        /// <summary>
        /// 单位通讯录s
        /// </summary>
        public ListDepts()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ListDept();
            }
        }
    }
}
