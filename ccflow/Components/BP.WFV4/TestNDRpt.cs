using System;
using System.Collections.Generic;
using System.Text;
using BP.En;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    ///  属性
    /// </summary>
    public class TestNDRptAttr
    {
        /// <summary>
        /// 标题
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// 流程编号
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// 参与成员
        /// </summary>
        public const string FlowEmps = "FlowEmps";
        /// <summary>
        /// 流程ID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// 工作ID
        /// </summary>
        public const string OID = "OID";
        /// <summary>
        /// 年月
        /// </summary>
        public const string FK_NY = "FK_NY";
        /// <summary>
        /// 流程发起人
        /// </summary>
        public const string FlowStarter = "FlowStarter";
        /// <summary>
        /// 流程发起日期
        /// </summary>
        public const string FlowStartRDT = "FlowStartRDT";
        /// <summary>
        /// 部门发起部门
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// 流程数量
        /// </summary>
        public const string WFState = "WFState";
        /// <summary>
        /// 个数(统计分析用)
        /// </summary>
        public const string MyNum = "MyNum";
        /// <summary>
        /// 结束人
        /// </summary>
        public const string FlowEnder = "FlowEnder";
        /// <summary>
        /// 流程结束日期
        /// </summary>
        public const string FlowEnderRDT = "FlowEnderRDT";
        /// <summary>
        /// 跨度
        /// </summary>
        public const string FlowDaySpan = "FlowDaySpan";
    }
    /// <summary>
    /// 报表
    /// </summary>
    public class TestNDRpt : BP.En.EntityOID
    {
        #region attrs
        /// <summary>
        /// 流程发起人
        /// </summary>
        public string FlowStarter
        {
            get
            {
                return this.GetValStringByKey(TestNDRptAttr.FlowStarter);
            }
            set
            {
                this.SetValByKey(TestNDRptAttr.FlowStarter, value);
            }
        }
        public string FlowStartRDT
        {
            get
            {
                return this.GetValStringByKey(TestNDRptAttr.FlowStartRDT);
            }
            set
            {
                this.SetValByKey(TestNDRptAttr.FlowStartRDT, value);
            }
        }
        /// <summary>
        /// 流程结束者
        /// </summary>
        public string FlowEnder
        {
            get
            {
                return this.GetValStringByKey(TestNDRptAttr.FlowEnder);
            }
            set
            {
                this.SetValByKey(TestNDRptAttr.FlowEnder, value);
            }
        }
        /// <summary>
        /// 流程结束时间
        /// </summary>
        public string FlowEnderRDT
        {
            get
            {
                return this.GetValStringByKey(TestNDRptAttr.FlowEnderRDT);
            }
            set
            {
                this.SetValByKey(TestNDRptAttr.FlowEnderRDT, value);
            }
        }
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(TestNDRptAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(TestNDRptAttr.FK_Dept, value);
            }
        }
        public int WFState
        {
            get
            {
                return this.GetValIntByKey(TestNDRptAttr.WFState);
            }
            set
            {
                this.SetValByKey(TestNDRptAttr.WFState, value);
            }
        }
        #endregion attrs

        #region attrs - attrs 
        public string RptName = null;
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("V_FlowData1");
                map.EnDesc = "流程数据";
                map.EnType = EnType.Admin;

                map.AddTBIntPKOID(TestNDRptAttr.OID, "WorkID");
                map.AddTBString(TestNDRptAttr.Title, null, "标题", true, true, 0, 100, 100);
              //  map.AddDDLEntities(TestNDRptAttr.FlowStarter, null, "发起人", new WF.Port.WFEmps(), false);

            //    map.AddTBDateTime(TestNDRptAttr.FlowStartRDT, null, "发起日期", true, true);
                map.AddDDLSysEnum(TestNDRptAttr.WFState, 0, "流程状态", true, true);
                map.AddTBString(TestNDRptAttr.FlowEmps, null, "参与人", true, true, 0, 100, 100);
                map.AddDDLEntities(TestNDRptAttr.FK_NY, null, "年月", new BP.Pub.NYs(), false);
                map.AddDDLEntities(TestNDRptAttr.FK_Dept, null, "部门", new Port.Depts(), false);
                map.AddDDLEntities(TestNDRptAttr.FK_Flow, null, "流程", new Flows(), false);
              //  map.AddTBDateTime(TestNDRptAttr.FlowEnderRDT, null, "结束日期", true, true);
                map.AddTBInt(TestNDRptAttr.FlowDaySpan, 0, "跨度(天)", true, true);
                map.AddTBInt(TestNDRptAttr.MyNum, 1, "个数", true, true);

                map.AddSearchAttr(TestNDRptAttr.FK_Dept);
                map.AddSearchAttr(TestNDRptAttr.FK_NY);
                map.AddSearchAttr(TestNDRptAttr.FK_Flow);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion attrs
    }
    /// <summary>
    /// 报表集合
    /// </summary>
    public class TestNDRpts : BP.En.EntitiesOID
    {
        /// <summary>
        /// 报表集合
        /// </summary>
        public TestNDRpts()
        {
        }

        public override Entity GetNewEntity
        {
            get
            {
                return new TestNDRpt();
            }
        }
    }
}
