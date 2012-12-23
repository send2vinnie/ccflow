using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.WF
{
	/// <summary>
    ///  ��������
	/// </summary>
    public class BillType : EntityNoName
    {
        public string FK_Flow
        {
            get
            {
                return this.GetValStrByKey("FK_Flow");
            }
            set
            {
                this.SetValByKey("FK_Flow", value);
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        #region ���췽��
        /// <summary>
        /// ��������
        /// </summary>
        public BillType()
        {
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="_No"></param>
        public BillType(string _No) : base(_No) { }
        #endregion

        /// <summary>
        /// ��������Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_BillType");
                map.EnDesc = this.ToE("BillType", "��������") ;
                map.CodeStruct = "2";
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.IsAllowRepeatNo = false;
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(SimpleNoNameAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(SimpleNoNameAttr.Name, null, "����", true, false, 1, 50, 50);
                map.AddTBString("FK_Flow", null, "����", true, false, 1, 50, 50);

             //   map.AddDDLEntities( "FK_Flow", null, "����", new Flows(), true);

                map.AddTBInt("IDX", 0, "IDX", false, false);
                this._enMap = map;
                return this._enMap;
            }
        }
    }
	/// <summary>
    /// ��������
	/// </summary>
	public class BillTypes :SimpleNoNames
	{
		/// <summary>
		/// ��������s
		/// </summary>
		public BillTypes(){}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BillType();
			}
		}
	}
}
