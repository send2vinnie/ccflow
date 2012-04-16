using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.SSO
{
	/// <summary>
	/// ���Ÿ�λ
	/// </summary>
	public class DeptStationAttr  
	{
		#region ��������
		/// <summary>
		/// ������ԱID
		/// </summary>
		public const  string FK_Dept="FK_Dept";
		/// <summary>
		/// ������λ
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		#endregion	
	}
	/// <summary>
    /// ���Ÿ�λ ��ժҪ˵����
	/// </summary>
    public class DeptStation : Entity
    {
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }

        #region ��������
        /// <summary>
        /// ������ԱID
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(DeptStationAttr.FK_Dept);
            }
            set
            {
                SetValByKey(DeptStationAttr.FK_Dept, value);
            }
        }
        public string FK_StationT
        {
            get
            {
                return this.GetValRefTextByKey(DeptStationAttr.FK_Station);
            }
        }
        /// <summary>
        ///������λ
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(DeptStationAttr.FK_Station);
            }
            set
            {
                SetValByKey(DeptStationAttr.FK_Station, value);
            }
        }
        #endregion

        #region ��չ����

        #endregion

        #region ���캯��
        /// <summary>
        /// �������Ÿ�λ
        /// </summary> 
        public DeptStation() { }
        /// <summary>
        /// ������Ա������λ��Ӧ
        /// </summary>
        /// <param name="_empoid">������ԱID</param>
        /// <param name="wsNo">������λ���</param> 	
        public DeptStation(string _empoid, string wsNo)
        {
            this.FK_Dept = _empoid;
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

                Map map = new Map("Port_DeptStation");
                map.EnDesc = "���Ÿ�λ";
                map.EnType = EnType.Dot2Dot;   
                map.AddDDLEntitiesPK(DeptStationAttr.FK_Dept, null, "����", new Depts(), true);
                map.AddDDLEntitiesPK(DeptStationAttr.FK_Station, null, "��λ", new Stations(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ���Ÿ�λ 
	/// </summary>
    public class DeptStations : Entities
    {
        #region ����
        /// <summary>
        /// �������Ÿ�λ
        /// </summary>
        public DeptStations()
        {
        }
        /// <summary>
        /// ������Ա�빤����λ����
        /// </summary>
        public DeptStations(string stationNo)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(DeptStationAttr.FK_Station, stationNo);
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
                return new DeptStation();
            }
        }
        #endregion
    }
}
