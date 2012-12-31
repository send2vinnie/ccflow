using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Port
{	 
	/// <summary>
	/// ��λ����
	/// </summary>
    public class StationAttr : EntityNoNameAttr
    {
        public const string StaGrade = "StaGrade";
    }
	/// <summary>
	/// ��λ
	/// </summary>
    public class Station : EntityNoName
    {
        #region ʵ�ֻ����ķ�����
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        public new string Name
        {
            get
            {
                if (BP.Web.WebUser.SysLang == "B5")
                    return Sys.Language.Turn2Traditional(this.GetValStrByKey("Name"));

                return this.GetValStrByKey("Name");
            }
        }
        public int Grade
        {
            get
            {
                return this.No.Length / 2;
            }
        }
        public int StaGrade
        {
            get
            {
                return this.GetValIntByKey(StationAttr.StaGrade);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��λ
        /// </summary> 
        public Station()
        {
        }
        /// <summary>
        /// ��λ
        /// </summary>
        /// <param name="_No"></param>
        public Station(string _No) : base(_No) { }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Port_Station");
                map.EnDesc = this.ToE("Station", "��λ"); // "��λ";
                map.EnType = EnType.Admin;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.CodeStruct = "2222222"; // ��󼶱��� 7 .
                map.IsAllowRepeatNo = false;

                map.AddTBStringPK(EmpAttr.No, null, "���", true, false, 1, 20, 100);
                map.AddTBString(EmpAttr.Name, null, "����", true, false, 0, 100, 100);
                map.AddDDLSysEnum(StationAttr.StaGrade, 0, "����", true, false, StationAttr.StaGrade,
                    "@1=�߲��@2=�в��@3=ִ�и�");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	 /// <summary>
	 /// ��λs
	 /// </summary>
	public class Stations : EntitiesNoName
	{
		/// <summary>
		/// ��λ
		/// </summary>
        public Stations() { }
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Station();
			}
		}
	}
}
