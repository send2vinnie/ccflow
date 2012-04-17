using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.SSO
{
	/// <summary>
	/// 信息块岗位
	/// </summary>
	public class BarStationAttr  
	{
		#region 基本属性
		/// <summary>
		/// 信息块
		/// </summary>
		public const  string FK_Bar="FK_Bar";
		/// <summary>
		/// 工作岗位
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		#endregion	
	}
	/// <summary>
    /// 信息块岗位 的摘要说明。
	/// </summary>
    public class BarStation : Entity
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
        /// 信息块
        /// </summary>
        public string FK_Bar
        {
            get
            {
                return this.GetValStringByKey(BarStationAttr.FK_Bar);
            }
            set
            {
                SetValByKey(BarStationAttr.FK_Bar, value);
            }
        }
        public string FK_StationT
        {
            get
            {
                return this.GetValRefTextByKey(BarStationAttr.FK_Station);
            }
        }
        /// <summary>
        ///工作岗位
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(BarStationAttr.FK_Station);
            }
            set
            {
                SetValByKey(BarStationAttr.FK_Station, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 信息块岗位
        /// </summary> 
        public BarStation() { }
        /// <summary>
        /// 信息块岗位
        /// </summary>
        /// <param name="_Temoid"></param>
        /// <param name="wsNo"></param>
        public BarStation(string fk_bar, string fk_station)
        {
            this.FK_Bar = fk_bar;
            this.FK_Station = fk_station;
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

                Map map = new Map("SSO_BarStation");
                map.EnDesc = "信息块岗位";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(BarStationAttr.FK_Bar, null, "信息块", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(BarStationAttr.FK_Station, null, "工作岗位", new Stations(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// 信息块岗位 
	/// </summary>
	public class BarStations : Entities
	{
		#region 构造
		/// <summary>
        /// 信息块岗位
		/// </summary>
		public BarStations()
		{
		}
		/// <summary>
        /// 信息块岗位s
		/// </summary>
		public BarStations(string stationNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(BarStationAttr.FK_Station, stationNo);
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
				return new BarStation();
			}
		}	
		#endregion 
	}
}
