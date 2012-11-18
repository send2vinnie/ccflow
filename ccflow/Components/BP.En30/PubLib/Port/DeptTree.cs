using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.Port
{
    /// <summary>
    /// 部门属性
    /// </summary>
    public class DeptTreeAttr : EntityTreeAttr
    {
    }
    /// <summary>
    /// 部门
    /// </summary>
    public class DeptTree : EntityTree
    {
        #region 构造函数
        /// <summary>
        /// 部门
        /// </summary>
        public DeptTree() { }
        /// <summary>
        /// 部门
        /// </summary>
        /// <param name="id">编号</param>
        public DeptTree(string id) : base(id) { }
        #endregion

        #region 重写方法
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //连接到的那个数据库上. (默认的是: AppCenterDSN )
                map.PhysicsTable = "Port_DeptTree";
                map.EnType = EnType.Admin;
                map.EnDesc = "部门"; // "部门";// 实体的描述.
                map.DepositaryOfEntity = Depositary.Application; //实体map的存放位置.
                map.DepositaryOfMap = Depositary.Application;    // Map 的存放位置.
                map.CodeStruct = "22222222";
                map.IsAllowRepeatNo = false;
                map.IsCheckNoLength = false;
                map.AdjunctType = AdjunctType.None;

                map.AddTBStringPK(DeptTreeAttr.No, null, this.ToE("No", "编号"), true, false, 1, 20, 20);
                map.AddTBString(DeptTreeAttr.Name, null, this.ToE("Name", "名称"), true, false, 0, 100, 30);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    ///得到集合
    /// </summary>
    public class DeptTrees : EntitiesNoName
    {
        /// <summary>
        /// 查询全部。
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {

            if (Web.WebUser.No == "admin")
                return base.RetrieveAll();

            QueryObject qo11 = new QueryObject(this);
            qo11.AddWhere(DeptTreeAttr.No, " like ", Web.WebUser.FK_DeptTree + "%");
            return qo11.DoQuery();
        }
        /// <summary>
        /// 得到一个新实体
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new DeptTree();
            }
        }
        /// <summary>
        /// 部门集合
        /// </summary>
        public DeptTrees()
        {
        }
    }
}
