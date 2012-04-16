using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.GPM
{
	/// <summary>
	/// ��������
	/// </summary>
    public class DeptAttr : EntityNoNameAttr
	{
		/// <summary>
		/// ��������
		/// </summary>
		public const string WorkCharacter="WorkCharacter";
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_Dept="FK_Dept";
		/// <summary>
		/// ��������
		/// </summary>
		public const string WorkFloor="WorkFloor";
		/// <summary>
		/// ��������
		/// </summary>
		public const string DeptType="DeptType";
	}
	/// <summary>
	/// ����
	/// </summary>
	public class Dept:EntityNoName
	{
		#region ����
        public new string Name
        {
            get
            {
                if (BP.Web.WebUser.SysLang == "B5")
                    return Sys.Language.Turn2Traditional(this.GetValStrByKey("Name"));

                return this.GetValStrByKey("Name");
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int Grade
        {
            get
            {
                return this.No.Length / 2;
            }
        }
		#endregion

		#region ���캯��
		/// <summary>
		/// ����
		/// </summary>
		public Dept(){}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="no">���</param>
        public Dept(string no) : base(no){}
		#endregion

		#region ��д����
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
		/// Map
		/// </summary>
		public override Map EnMap
		{
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //���ӵ����Ǹ����ݿ���. (Ĭ�ϵ���: AppCenterDSN )
                map.PhysicsTable = "Port_Dept";
                map.EnType = EnType.Admin;

                map.EnDesc = this.ToE("Dept", "����"); // "����";// ʵ�������.
                map.DepositaryOfEntity = Depositary.Application; //ʵ��map�Ĵ��λ��.
                map.DepositaryOfMap = Depositary.Application;    // Map �Ĵ��λ��.
                map.CodeStruct = "22222222";
                map.IsAllowRepeatNo = false;
                map.IsCheckNoLength = false;
                map.IsAutoGenerNo = false;

                map.AdjunctType = AdjunctType.None;
                map.AddTBStringPK(DeptAttr.No, null, null, true, false, 2, 20, 40);
                map.AddTBString(DeptAttr.Name, null,null, true, false, 0, 60, 400);
                //   map.AddTBInt(DeptAttr.Grade, 0, "����", true, false);
                //  map.AddBoolean(DeptAttr.IsDtl, false, "�Ƿ���ϸ", true, true);
                this._enMap = map;
                return this._enMap;
            }
		}
        protected override bool beforeUpdateInsertAction()
        {
           // this.Grade = this.No.Length / 2;
            return base.beforeUpdateInsertAction();
        }
		#endregion
	}
	/// <summary>
	///�õ�����
	/// </summary>
	public class Depts: EntitiesNoName
	{
		/// <summary>
		/// ��ѯȫ����
		/// </summary>
		/// <returns></returns>
        public override int RetrieveAll()
        {

            if (Web.WebUser.No == "admin")
                return base.RetrieveAll();

            QueryObject qo11 = new QueryObject(this);
            qo11.AddWhere(DeptAttr.No, " like ", Web.WebUser.FK_Dept + "%");
            return qo11.DoQuery();
        }
		/// <summary>
		/// �õ�һ����ʵ��
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Dept();
			}
		}
		/// <summary>
		/// create ens
		/// </summary>
		public Depts(){}
		
	}
}
