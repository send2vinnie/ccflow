using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.SSO
{
	/// <summary>
	/// 系统岗位
	/// </summary>
	public class TemStationAttr  
	{
		#region 基本属性
		/// <summary>
		/// 工作人员ID
		/// </summary>
		public const  string FK_STem="FK_STem";
		/// <summary>
		/// 工作岗位
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		#endregion	
	}
	/// <summary>
    /// 系统岗位 的摘要说明。
	/// </summary>
    public class TemStation : Entity
    {

        #region 访问权限控制
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        #endregion 访问权限控制


        #region 基本属性
        /// <summary>
        /// 工作人员ID
        /// </summary>
        public string FK_STem
        {
            get
            {
                return this.GetValStringByKey(TemStationAttr.FK_STem);
            }
            set
            {
                SetValByKey(TemStationAttr.FK_STem, value);
            }
        }
        public string FK_StationT
        {
            get
            {
                return this.GetValRefTextByKey(TemStationAttr.FK_Station);
            }
        }
        /// <summary>
        ///工作岗位
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(TemStationAttr.FK_Station);
            }
            set
            {
                SetValByKey(TemStationAttr.FK_Station, value);
            }
        }
        #endregion


        #region 构造函数
        /// <summary>
        /// 系统岗位
        /// </summary> 
        public TemStation() { }
        /// <summary>
        /// 系统岗位
        /// </summary>
        /// <param name="_Temoid">工作人员ID</param>
        /// <param name="wsNo">工作岗位编号</param> 	
        public TemStation(string _Temoid, string wsNo)
        {
            this.FK_STem = _Temoid;
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

                Map map = new Map("SSO_STemStation");
                map.EnDesc = "系统岗位";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(TemStationAttr.FK_STem, null, "系统", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(TemStationAttr.FK_Station, null, "工作岗位", new Stations(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// 系统岗位 
	/// </summary>
	public class TemStations : Entities
	{
		#region 构造
		/// <summary>
        /// 系统岗位
		/// </summary>
		public TemStations()
		{
		}
		/// <summary>
        /// 系统岗位s
		/// </summary>
		public TemStations(string stationNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(TemStationAttr.FK_Station, stationNo);
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
				return new TemStation();
			}
		}	
		#endregion 
	}
}
