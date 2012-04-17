using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.SSO
{
	/// <summary>
	/// ϵͳ��λ
	/// </summary>
	public class TemStationAttr  
	{
		#region ��������
		/// <summary>
		/// ������ԱID
		/// </summary>
		public const  string FK_STem="FK_STem";
		/// <summary>
		/// ������λ
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		#endregion	
	}
	/// <summary>
    /// ϵͳ��λ ��ժҪ˵����
	/// </summary>
    public class TemStation : Entity
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
        /// ������ԱID
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
        ///������λ
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


        #region ���캯��
        /// <summary>
        /// ϵͳ��λ
        /// </summary> 
        public TemStation() { }
        /// <summary>
        /// ϵͳ��λ
        /// </summary>
        /// <param name="_Temoid">������ԱID</param>
        /// <param name="wsNo">������λ���</param> 	
        public TemStation(string _Temoid, string wsNo)
        {
            this.FK_STem = _Temoid;
            this.FK_Station = wsNo;
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

                Map map = new Map("SSO_STemStation");
                map.EnDesc = "ϵͳ��λ";
                map.EnType = EnType.Dot2Dot;

                map.AddTBStringPK(TemStationAttr.FK_STem, null, "ϵͳ", false, false, 1, 15, 1);
                map.AddDDLEntitiesPK(TemStationAttr.FK_Station, null, "������λ", new Stations(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ϵͳ��λ 
	/// </summary>
	public class TemStations : Entities
	{
		#region ����
		/// <summary>
        /// ϵͳ��λ
		/// </summary>
		public TemStations()
		{
		}
		/// <summary>
        /// ϵͳ��λs
		/// </summary>
		public TemStations(string stationNo)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(TemStationAttr.FK_Station, stationNo);
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
				return new TemStation();
			}
		}	
		#endregion 
	}
}
