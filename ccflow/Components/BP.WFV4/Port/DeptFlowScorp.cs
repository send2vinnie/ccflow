using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.WF.Port
{
    /// <summary>
    /// 部门数据查询权限
    /// </summary>
    public class DeptFlowScorpAttr
    {
        #region 基本属性
        /// <summary>
        /// 工作人员ID
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// 部门
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        #endregion
    }
    /// <summary>
    /// 部门数据查询权限 的摘要说明。
    /// </summary>
    public class DeptFlowScorp : Entity
    {
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                if (BP.Web.WebUser.No == "admin")
                {
                    uac.IsView = true;
                    uac.IsDelete = true;
                    uac.IsInsert = true;
                    uac.IsUpdate = true;
                    uac.IsAdjunct = true;
                }
                return uac;
            }
        }

        #region 基本属性
        /// <summary>
        /// 工作人员ID
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(DeptFlowScorpAttr.FK_Emp);
            }
            set
            {
                SetValByKey(DeptFlowScorpAttr.FK_Emp, value);
            }
        }
        public string FK_DeptT
        {
            get
            {
                return this.GetValRefTextByKey(DeptFlowScorpAttr.FK_Dept);
            }
        }
        /// <summary>
        ///部门
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(DeptFlowScorpAttr.FK_Dept);
            }
            set
            {
                SetValByKey(DeptFlowScorpAttr.FK_Dept, value);
            }
        }
        #endregion

        #region 扩展属性

        #endregion

        #region 构造函数
        /// <summary>
        /// 部门数据查询权限
        /// </summary> 
        public DeptFlowScorp() { }
        /// <summary>
        /// 部门数据查询权限
        /// </summary>
        /// <param name="_empoid">工作人员ID</param>
        /// <param name="wsNo">部门编号</param> 	
        public DeptFlowScorp(string _empoid, string wsNo)
        {
            this.FK_Emp = _empoid;
            this.FK_Dept = wsNo;
            if (this.Retrieve() == 0)
                this.Insert();
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Port_DeptFlowScorp");
                map.EnDesc = "部门数据查询权限";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(DeptFlowScorpAttr.FK_Emp, null, "操作员", true, true, 1, 50, 11);
                map.AddDDLEntitiesPK(DeptFlowScorpAttr.FK_Dept, null, "部门", new BP.WF.Port.Depts(), true);
                // map.AddDDLEntitiesPK(DeptFlowScorpAttr.FK_Emp, null, "操作员", new Emps(), true);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region 重载基类方法
        /// <summary>
        /// 插入前所做的工作
        /// </summary>
        /// <returns>true/false</returns>
        protected override bool beforeInsert()
        {
            return base.beforeInsert();
        }
        /// <summary>
        /// 更新前所做的工作
        /// </summary>
        /// <returns>true/false</returns>
        protected override bool beforeUpdate()
        {
            return base.beforeUpdate();
        }
        /// <summary>
        /// 删除前所做的工作
        /// </summary>
        /// <returns>true/false</returns>
        protected override bool beforeDelete()
        {
            return base.beforeDelete();
        }
        #endregion
    }
    /// <summary>
    /// 部门数据查询权限 
    /// </summary>
    public class DeptFlowScorps : Entities
    {
        #region 构造
        /// <summary>
        /// 部门数据查询权限
        /// </summary>
        public DeptFlowScorps() { }
        /// <summary>
        /// 部门数据查询权限
        /// </summary>
        /// <param name="FK_Emp">FK_Emp</param>
        public DeptFlowScorps(string FK_Emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DeptFlowScorpAttr.FK_Emp, FK_Emp);
            qo.DoQuery();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new DeptFlowScorp();
            }
        }
        #endregion

        #region 查询方法
        #endregion
    }
}
