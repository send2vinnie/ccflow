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
    public class GERptAttr
    {

        public const string Title = "Title";
        public const string BillNo = "BillNo";
        public const string FlowEmps = "FlowEmps";
        public const string FID = "FID";
        public const string OID = "OID";
        public const string FK_NY = "FK_NY";
        public const string FlowStarter = "FlowStarter";
        public const string FlowStartRDT = "FlowStartRDT";
        public const string FK_Dept = "FK_Dept";
        public const string WFState = "WFState";
        public const string MyNum = "MyNum";
        /// <summary>
        /// 结束人
        /// </summary>
        public const string FlowEnder = "FlowEnder";
        public const string FlowEnderRDT = "FlowEnderRDT";
        /// <summary>
        /// 跨度
        /// </summary>
        public const string FlowDaySpan = "FlowDaySpan";
    }
    /// <summary>
    /// 报表
    /// </summary>
    public class GERpt : BP.En.EntityOID
    {
        #region attrs
        /// <summary>
        /// 流程发起人
        /// </summary>
        public string FlowStarter
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FlowStarter);
            }
            set
            {
                this.SetValByKey(GERptAttr.FlowStarter, value);
            }
        }
        public string FlowStartRDT
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FlowStartRDT);
            }
            set
            {
                this.SetValByKey(GERptAttr.FlowStartRDT, value);
            }
        }
        /// <summary>
        /// 流程结束者
        /// </summary>
        public string FlowEnder
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FlowEnder);
            }
            set
            {
                this.SetValByKey(GERptAttr.FlowEnder, value);
            }
        }
        /// <summary>
        /// 流程结束时间
        /// </summary>
        public string FlowEnderRDT
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FlowEnderRDT);
            }
            set
            {
                this.SetValByKey(GERptAttr.FlowEnderRDT, value);
            }
        }
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(GERptAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(GERptAttr.FK_Dept, value);
            }
        }
        public int WFState
        {
            get
            {
                return this.GetValIntByKey(GERptAttr.WFState);
            }
            set
            {
                this.SetValByKey(GERptAttr.WFState, value);
            }
        }
        #endregion attrs

        #region attrs - attrs 
        public string RptName = null;
        public override Map EnMap
        {
            get
            {
                if (this.RptName == null)
                {
                    BP.Port.Emp emp = new BP.Port.Emp();
                    return emp.EnMap;
                }

                MapData md = new MapData(RptName);
                this._enMap = md.GenerHisMap();
                return this._enMap;
            }
        }
        /// <summary>
        /// 报表
        /// </summary>
        /// <param name="rptName"></param>
        public GERpt(string rptName)
        {
            this.RptName = rptName;
        }
        public GERpt()
        {
        }
        /// <summary>
        /// 报表
        /// </summary>
        /// <param name="rptName"></param>
        /// <param name="oid"></param>
        public GERpt(string rptName, int oid)
        {
            this.RptName = rptName;
          
            this.Retrieve();
        }
        #endregion attrs
    }
    /// <summary>
    /// 报表集合
    /// </summary>
    public class GERpts : BP.En.EntitiesOID
    {
        /// <summary>
        /// 报表集合
        /// </summary>
        public GERpts()
        {
        }

        public override Entity GetNewEntity
        {
            get
            {
                return new GERpt();
            }
        }
    }
}
