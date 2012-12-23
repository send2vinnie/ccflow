using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// ����
	/// </summary>
    public class DirectionAttr : EntityNoNameAttr
    {
        /// <summary>
        /// FK_Dept
        /// </summary>
        public const string FK_Dept = "FK_Dept";
    }
	/// <summary>
	/// ����
	/// </summary>
	public class Direction : EntityNoName
	{
		#region  ����
		/// <summary>
		///  �������ű��
		/// </summary>
		public string  FK_Dept
		{
			get
			{
				return this.GetValStringByKey(DirectionAttr.FK_Dept);
			}
			set
			{
				SetValByKey(DirectionAttr.FK_Dept,value);
			}
		}		
		#endregion 
		 
		#region ���캯��
		/// <summary>
		/// ����
		/// </summary>
		public Direction(){}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="_No"></param>
		public Direction(string _No) :base(_No)
		{ 
		}
		/// <summary>
		/// Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_Direction");
                map.EnDesc = "����";

                map.EnType = EnType.App;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;

                map.AddTBStringPK(DirectionAttr.No, null, "���", true, false, 4, 20, 100);
                map.AddTBString(DirectionAttr.Name, null, "����", true, false, 0, 50, 200);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

	}
	/// <summary>
	/// ���򼯺�
	/// </summary>
    public class Directions : EntitiesNoName
    {
        /// <summary>
        /// GetNewEntity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Direction();
            }
        }
        /// <summary>
        /// ���򼯺�()
        /// </summary>
        public Directions() { }
    }
}
 