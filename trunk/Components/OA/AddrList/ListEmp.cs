using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// 人员通讯录属性
    /// </summary>
    public class ListEmpAttr : EntityOIDNameAttr
    {
        public const string No = "No";
        /// <summary>
        /// 地址
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
        /// <summary>
        /// 备注
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// 部门
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// 职务
        /// </summary>
        public const string FK_Duty = "FK_Duty";
    }
    /// <summary>
    /// 人员通讯录
    /// </summary>
    public class ListEmp : BP.En.EntityNoName
    {
        #region 基本属性
        /// <summary>
        /// 管理员电话
        /// </summary>
        public string HandSet
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.HandSet);
            }
            set
            {
                this.SetValByKey(ListEmpAttr.HandSet, value);
            }
        }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string Learder
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Learder);
            }
            set
            {
                this.SetValByKey(ListEmpAttr.Learder, value);
            }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Addr
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Addr);
            }
            set
            {
                this.SetValByKey(ListEmpAttr.Addr, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Note);
            }
        }
        public string Fax
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Fax);
            }
        }
        public string Email
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Email);
            }
        }

        public string Tel
        {
            get
            {
                return this.GetValStringByKey(ListEmpAttr.Tel);
            }
            set
            {
                this.SetValByKey(ListEmpAttr.Tel, value);
            }
        }
       
        #endregion

        #region 构造方法
        /// <summary>
        /// 人员通讯录
        /// </summary>
        public ListEmp()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("GE_ListEmp");
                map.EnType = EnType.Sys;
                map.EnDesc = "人员通讯录";
                map.DepositaryOfEntity = Depositary.None;
                map.MoveTo = ListEmpAttr.FK_Dept;
                map.IsAutoGenerNo = true;
                map.CodeStruct = "6";
                map.TitleExt = " - <a href='Ens.aspx?EnsName=BP.GE.ListDutys' >职务维护</a> - <a href='Ens.aspx?EnsName=BP.GE.ListDepts' >部门维护</a>";

                //map.AddTBIntPKOID();
                map.AddTBStringPK(ListEmpAttr.No, null, "编号", true, true, 6, 6, 6);
                map.AddTBString(ListEmpAttr.Name, null, "姓名", true, false, 0, 100, 100);
                map.AddTBString(ListEmpAttr.HandSet, null, "手机", true, false, 0, 100, 10);
                map.AddTBString(ListEmpAttr.Tel, null, "办公电话", true, false, 0, 100, 10);
                map.AddTBString(ListEmpAttr.Fax, null, "Fax", true, false, 0, 100, 10);
                map.AddTBString(ListEmpAttr.Email, null, "Email", true, false, 0, 100, 10);
                map.AddTBString(ListEmpAttr.Note, null, "备注", true, false, 0, 500, 10);

                map.AddDDLEntities(ListEmpAttr.FK_Dept, null, "单位", new BP.GE.ListDepts(), true);
                map.AddDDLEntities(ListEmpAttr.FK_Duty, null, "职务", new BP.GE.ListDutys(), true);

                map.AddSearchAttr(ListEmpAttr.FK_Dept);
                map.AddSearchAttr(ListEmpAttr.FK_Duty);

                // map.AddTBString(ListEmpAttr.FK_ZJ, null, "ZJ", true, false, 0, 100, 10);
                // map.AddTBString(ListEmpAttr.Addr, null, "课时", true, false, 0, 20, 10);
                // map.AddDDLEntities(ListEmpAttr.Fax, null, "从", new ListEmpWeeks(), true);
                // map.AddDDLEntities(ListEmpAttr.Email, null, "到", new ListEmpWeeks(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 人员通讯录s
    /// </summary>
    public class ListEmps : EntitiesNoName
    {
        /// <summary>
        /// 人员通讯录s
        /// </summary>
        public ListEmps()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ListEmp();
            }
        }
    }
}
