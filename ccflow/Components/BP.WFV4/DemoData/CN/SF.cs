using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;
//using BP.ZHZS.DS;


namespace BP.CN
{
	/// <summary>
	/// ʡ��
	/// </summary>
	public class SFAttr: EntityNoNameAttr
	{
		#region ��������
		public const  string FK_PQ="FK_PQ";
        public const string Names = "Names";
        public const string JC = "JC";

		#endregion
	}
	/// <summary>
    /// ʡ��
	/// </summary>
	public class SF :EntityNoName
	{	
		#region ��������
        public string FK_PQ
        {
            get
            {
                return this.GetValStrByKey(SFAttr.FK_PQ);
            }
        }
        public string Names
        {
            get
            {
                return this.GetValStrByKey(SFAttr.Names);
            }
        }
        public string JC
        {
            get
            {
                return this.GetValStrByKey(SFAttr.JC);
            }
        }

		 
		#endregion 

		#region ���캯��
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
		}
		/// <summary>
		/// ʡ��
		/// </summary>		
		public SF(){}
		public SF(string no):base(no)
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
                Map map = new Map();

                #region ��������
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "CN_SF";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.IsAllowRepeatNo = false;
                map.IsCheckNoLength = false;
                map.EnDesc = "ʡ��";
                map.EnType = EnType.App;
                map.CodeStruct = "4";
                #endregion

                #region �ֶ�
                map.AddTBStringPK(SFAttr.No, null, "���", true, false, 0, 50, 50);
                map.AddTBString(SFAttr.Name, null, "����", true, false, 0, 50, 200);
                map.AddTBString(SFAttr.Names, null, "С��", true, false, 0, 50, 200);
                map.AddTBString(SFAttr.JC, null, "���", true, false, 0, 50, 200);


                map.AddDDLEntities(SFAttr.FK_PQ, null, "Ƭ��", new PQs(), true);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
		}
		#endregion
		 
	}
	/// <summary>
	/// ʡ��
	/// </summary>
	public class SFs : EntitiesNoName
	{
		#region 
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SF();
			}
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// ʡ��s
		/// </summary>
		public SFs(){}
		#endregion
	}
	
}
