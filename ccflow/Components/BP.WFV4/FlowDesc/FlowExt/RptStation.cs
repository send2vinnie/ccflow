using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// 报表岗位
	/// </summary>
	public class RptStationAttr  
	{
		#region 基本属性
		/// <summary>
		/// 报表
		/// </summary>
		public const  string FK_Rpt="FK_Rpt";
		/// <summary>
		/// 工作岗位
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		#endregion	
	}
	/// <summary>
    /// 报表岗位 的摘要说明。
	/// </summary>
    public class RptStation : Entity
    {

        #region 基本属性
        /// <summary>
        /// 报表
        /// </summary>
        public string FK_Rpt
        {
            get
            {
                return this.GetValStringByKey(RptStationAttr.FK_Rpt);
            }
            set
            {
                SetValByKey(RptStationAttr.FK_Rpt, value);
            }
        }
        /// <summary>
        ///工作岗位
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(RptStationAttr.FK_Station);
            }
            set
            {
                SetValByKey(RptStationAttr.FK_Station, value);
            }
        }
        #endregion

        #region 扩展属性

        #endregion

        #region 构造函数
        /// <summary>
        /// 工作报表岗位
        /// </summary> 
        public RptStation() { }
        /// <summary>
        /// 工作人员工作岗位对应
        /// </summary>
        /// <param name="_empoid">报表</param>
        /// <param name="wsNo">工作岗位编号</param> 	
        public RptStation(string _empoid, string wsNo)
        {
            this.FK_Rpt = _empoid;
            this.FK_Station = wsNo;
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

                Map map = new Map("WF_RptStation");
                map.EnDesc = "报表岗位";
                map.EnType = EnType.Dot2Dot;

                map.AddDDLEntitiesPK(RptStationAttr.FK_Rpt, null, "操作员", new WFRpts(), true);
                map.AddDDLEntitiesPK(RptStationAttr.FK_Station, null, "工作岗位", new Stations(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// 报表岗位 
	/// </summary>
    public class RptStations : Entities
    {
        #region 构造
        /// <summary>
        /// 工作报表岗位
        /// </summary>
        public RptStations()
        {
        }
        /// <summary>
        /// 报表岗位
        /// </summary>
        public RptStations(string stationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(RptStationAttr.FK_Station, stationNo);
            qo.DoQuery();
        }
        /// <summary>
        /// 报表岗位
        /// </summary>
        /// <param name="empId">RptID</param>
        public RptStations(int empId)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(RptStationAttr.FK_Rpt, empId);
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
                return new RptStation();
            }
        }
        #endregion
    }
}
