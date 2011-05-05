using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

//using BP.ZHZS.DS;


namespace BP.WF
{
	/// <summary>
	/// 报表操作员
	/// </summary>
	public class RptEmpAttr  
	{
		#region 基本属性
		/// <summary>
		/// 报表
		/// </summary>
		public const  string FK_Rpt="FK_Rpt";
		/// <summary>
		/// 工作操作员
		/// </summary>
		public const  string FK_Emp="FK_Emp";		 
		#endregion	
	}
	/// <summary>
    /// 报表操作员 的摘要说明。
	/// </summary>
    public class RptEmp : Entity
    {
        #region 基本属性
        /// <summary>
        /// 报表
        /// </summary>
        public string FK_Rpt
        {
            get
            {
                return this.GetValStringByKey(RptEmpAttr.FK_Rpt);
            }
            set
            {
                SetValByKey(RptEmpAttr.FK_Rpt, value);
            }
        }
        /// <summary>
        ///工作操作员
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(RptEmpAttr.FK_Emp);
            }
            set
            {
                SetValByKey(RptEmpAttr.FK_Emp, value);
            }
        }
        #endregion

        #region 扩展属性

        #endregion

        #region 构造函数
        /// <summary>
        /// 工作报表操作员
        /// </summary> 
        public RptEmp() { }
        /// <summary>
        /// 工作人员工作操作员对应
        /// </summary>
        /// <param name="_empoid">报表</param>
        /// <param name="wsNo">工作操作员编号</param> 	
        public RptEmp(string _empoid, string wsNo)
        {
            this.FK_Rpt = _empoid;
            this.FK_Emp = wsNo;
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

                Map map = new Map("WF_RptEmp");
                map.EnDesc = "报表操作员";
                map.EnType = EnType.Dot2Dot;

                map.AddDDLEntitiesPK(RptEmpAttr.FK_Rpt, null, "操作员", new WFRpts(), true);
                map.AddDDLEntitiesPK(RptEmpAttr.FK_Emp, null, "工作操作员", new Emps(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// 报表操作员 
	/// </summary>
    public class RptEmps : Entities
    {
        #region 构造
        /// <summary>
        /// 工作报表操作员
        /// </summary>
        public RptEmps()
        {
        }
        /// <summary>
        /// 报表操作员
        /// </summary>
        public RptEmps(string stationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(RptEmpAttr.FK_Emp, stationNo);
            qo.DoQuery();
        }
        /// <summary>
        /// 报表操作员
        /// </summary>
        /// <param name="empId">RptID</param>
        public RptEmps(int empId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(RptEmpAttr.FK_Rpt, empId);
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
                return new RptEmp();
            }
        }
        #endregion
    }
}
