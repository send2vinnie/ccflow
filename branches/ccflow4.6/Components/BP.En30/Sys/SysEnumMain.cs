using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// sss
	/// </summary>
    public class SysEnumMainAttr : EntityNoNameAttr
	{
        public const string CfgVal = "CfgVal";

        public const string Lang = "Lang";

	}
	/// <summary>
	/// SysEnumMain
	/// </summary>
    public class SysEnumMain : EntityNoName
    {
        #region ʵ�ֻ����ķ�����
        public string CfgVal
        {
            get
            {
                return this.GetValStrByKey(SysEnumMainAttr.CfgVal);
            }
            set
            {
                this.SetValByKey(SysEnumMainAttr.CfgVal, value);
            }
        }
        public string Lang
        {
            get
            {
                return this.GetValStrByKey(SysEnumMainAttr.Lang);
            }
            set
            {
                this.SetValByKey(SysEnumMainAttr.Lang, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// SysEnumMain
        /// </summary>
        public SysEnumMain() { }
        /// <summary>
        /// SysEnumMain
        /// </summary>
        /// <param name="no"></param>
        public SysEnumMain(string no) : base(no) { }
        /// <summary>
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("Sys_EnumMain");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ö��";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(SysEnumMainAttr.No, null, "���", true, false, 1, 40, 8);
                map.AddTBString(SysEnumMainAttr.Name, null, "����", true, false, 0, 40, 8);
                map.AddTBString(SysEnumMainAttr.CfgVal, null, "������Ϣ", true, false, 0, 1500, 8);
                map.AddTBString(SysEnumMainAttr.Lang, "CH", "����", true, false, 0, 10, 8);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ��˰�˼��� 
	/// </summary>
	public class SysEnumMains : EntitiesNoName
	{
		/// <summary>
		/// SysEnumMains
		/// </summary>
		public SysEnumMains(){}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysEnumMain();
			}
		}
	}
}
