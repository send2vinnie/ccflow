using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.SSO
{
	/// <summary>
	/// ��Ϣ���λ
	/// </summary>
	public class BarStationAttr  
	{
		#region ��������
		/// <summary>
		/// ��Ϣ��
		/// </summary>
		public const  string FK_Bar="FK_Bar";
		/// <summary>
		/// ������λ
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		#endregion	
	}
	/// <summary>
    /// ��Ϣ���λ ��ժҪ˵����
	/// </summary>
    public class BarStation : Entity
    {
        #region ����Ȩ�޿���
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        #endregion ����Ȩ�޿���

        #region ��������
        /// <summary>
        /// ��Ϣ��
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
        ///������λ
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

        #region ���캯��
        /// <summary>
        /// ��Ϣ���λ
        /// </summary> 
        public BarStation() { }
        /// <summary>
        /// ��Ϣ���λ
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
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("SSO_BarStation");
                map.EnDesc = "��Ϣ���λ";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(BarStationAttr.FK_Bar, null, "��Ϣ��", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(BarStationAttr.FK_Station, null, "������λ", new Stations(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ��Ϣ���λ 
	/// </summary>
	public class BarStations : Entities
	{
		#region ����
		/// <summary>
        /// ��Ϣ���λ
		/// </summary>
		public BarStations()
		{
		}
		/// <summary>
        /// ��Ϣ���λs
		/// </summary>
		public BarStations(string stationNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(BarStationAttr.FK_Station, stationNo);
			qo.DoQuery();
		}		 
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
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
