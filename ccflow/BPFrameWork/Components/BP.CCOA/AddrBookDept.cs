using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.OA
{
    /// <summary>
    /// 部门
    /// </summary>
    public class AddrBookDeptAttr : EntityNoNameAttr
    {
    }
    /// <summary>
    /// 部门
    /// </summary>
    public class AddrBookDept : EntityNoName
    {
        #region 属性
        #endregion

        #region 构造方法
        /// <summary>
        /// 部门
        /// </summary>
        public AddrBookDept()
        {
        }
        /// <summary>
        /// 部门
        /// </summary>
        /// <param name="mypk"></param>
        public AddrBookDept(string no)
        {
            this.No = no;
            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("OA_AddrBookDept");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "部门";
                map.EnType = EnType.App;

                map.AddTBStringPK(AddrBookDeptAttr.No, null, "编号", true, true, 8, 8, 8);
                map.AddTBString(AddrBookDeptAttr.Name, null, "名称", true, false, 0, 3900, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 部门s
    /// </summary>
    public class AddrBookDepts : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 部门s
        /// </summary>
        public AddrBookDepts()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new AddrBookDept();
            }
        }
        #endregion
    }
}
