using System;
//
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;

namespace BP.Pub
{
    
	public class DayScop:EntityNoName
	{
		#region ��������
        public int F
        {
            get
            {
                return this.GetValIntByKey("F");
            }
        }
        public int T
        {
            get
            {
                return this.GetValIntByKey("T");
            }
        }
		#endregion 

		#region ���캯��
		public DayScop(){}
		#endregion

		#region ��дmap
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Pub_DayScop");
                map.EnDesc = "���ڷ�Χ";
                map.EnType = EnType.Admin;
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK("No", null, "���", true, false, 0, 50, 20);
                map.AddTBString("Name", null, "����", true, false, 0, 50, 20);

                map.AddTBInt("F", 0, "F", false, false);
                map.AddTBInt("T", 0, "T", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion
	}
	public class DayScops:EntitiesNoName
	{
		#region ���캯��
        public DayScops() { }
		#endregion

		#region ����
        public override Entity GetNewEntity
        {
            get
            {
                return new DayScop();
            }
        }
		#endregion
	}
}
