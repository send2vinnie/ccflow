using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Port
{
    /// <summary>
    /// ��λ
    /// </summary>
    public class UnitAttr : EntityNoNameAttr
    {
        public const string Flag = "Flag";
    }
	/// <summary>
    ///  ��λ
	/// </summary>
	public class Unit :EntityNoName
	{
        public int Grade
        {
            get
            {
                return  this.No.Length / 2;
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string Flag
        {
            get
            {
                return this.GetValStringByKey(UnitAttr.Flag);
            }
            set
            {
                this.SetValByKey(UnitAttr.Flag, value);
            }
        }

		#region ���췽��
		/// <summary>
		/// ��λ
		/// </summary>
		public Unit()
        {
        }
        /// <summary>
        /// ��λ
        /// </summary>
        /// <param name="_No"></param>
        public Unit(string _No) : base(_No) { }
		#endregion 

		/// <summary>
		/// ��λMap
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Port_Unit");
                map.EnDesc = "��λ";
                map.CodeStruct = "2";

                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.IsAllowRepeatNo = false;

                map.AddTBStringPK(UnitAttr.No, null, "���", true, false, 2, 2, 2);
                map.AddTBString(UnitAttr.Name, null, "����", true, false, 1, 20, 20);

                //map.AddTBString(UnitAttr.Flag, null, "���", true, false, 1, 20, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
	}
	/// <summary>
    /// ��λ
	/// </summary>
    public class Units : EntitiesNoName
	{
		/// <summary>
		/// ��λs
		/// </summary>
        public Units() { }
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
                return new Unit();
			}
		}
        public override int RetrieveAll()
        {
            QueryObject qo11 = new QueryObject(this);
            qo11.AddWhere(DeptAttr.No, " like ", Web.WebUser.FK_Unit + "%");
            return qo11.DoQuery();
        }
	}
}
