using System;
using System.Collections;
using BP.DA;
using BP.Sys;
using BP.En;
using BP.WF.Port;

namespace BP.WF
{
    /// <summary>
    /// Selector属性
    /// </summary>
    public class SelectorAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 流程
        /// </summary>
        public const string NodeID = "NodeID";
        /// <summary>
        /// 接受模式
        /// </summary>
        public const string SelectorRole = "SelectorRole";

        public const string SelectorP1 = "SelectorP1";
        public const string SelectorP2 = "SelectorP2";

        /// <summary>
        /// 数据显示方式(表格与树)
        /// </summary>
        public const string SelectorDBShowWay = "SelectorDBShowWay";
    }
    /// <summary>
    /// 选择器
    /// </summary>
    public class Selector : Entity
    {
        #region 基本属性
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(SelectorAttr.NodeID);
            }
            set
            {
                this.SetValByKey(SelectorAttr.NodeID, value);
            }
        }

        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsDelete = false;
                uac.IsInsert = false;
                uac.IsView = false;
                if (BP.Web.WebUser.No == "admin")
                {
                    uac.IsUpdate = true;
                    uac.IsView = true;
                }
                return uac;
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// Accpter
        /// </summary>
        public Selector() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeid"></param>
        public Selector(int nodeid)
        {
            this.NodeID = nodeid;
            this.Retrieve();
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

                Map map = new Map("WF_Node");
                map.EnDesc = "节点标签";

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBIntPK(SelectorAttr.NodeID, 0, "NodeID", true, true);
                map.AddDDLSysEnum(SelectorAttr.SelectorRole, 0, "窗口模式", true, true, SelectorAttr.SelectorRole,
                    "@0=按岗位@1=按部门@2=按人员@3=按SQL@4=自定义Url");

                map.AddTBString(SelectorAttr.SelectorP1, null, "参数1", true, false, 0, 200, 10, true);
                map.AddTBString(SelectorAttr.SelectorP2, null, "参数2", true, false, 0, 200, 10, true);
                map.AddDDLSysEnum(SelectorAttr.SelectorDBShowWay, 0, "数据显示方式", true, true, 
                    SelectorAttr.SelectorDBShowWay, "@0=表格显示@1=树形显示");


                // 相关功能。
                map.AttrsOfOneVSM.Add(new BP.WF.NodeStations(), new BP.WF.Port.Stations(),
                    NodeStationAttr.FK_Node, NodeStationAttr.FK_Station,
                    DeptAttr.Name, DeptAttr.No, this.ToE("NodeSta", "节点岗位"));

                map.AttrsOfOneVSM.Add(new BP.WF.NodeDepts(), new BP.WF.Port.Depts(), NodeDeptAttr.FK_Node, NodeDeptAttr.FK_Dept, DeptAttr.Name,
                DeptAttr.No, this.ToE("AccptDept", "节点部门"));

                map.AttrsOfOneVSM.Add(new BP.WF.NodeEmps(), new BP.WF.Port.Emps(), NodeEmpAttr.FK_Node, EmpDeptAttr.FK_Emp, DeptAttr.Name,
                    DeptAttr.No, this.ToE("Accpter", "接受人员"));


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// Accpter
    /// </summary>
    public class Selectors : Entities
    {
        /// <summary>
        /// Accpter
        /// </summary>
        public Selectors()
        {
        }
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Selector();
            }
        }
    }
}
