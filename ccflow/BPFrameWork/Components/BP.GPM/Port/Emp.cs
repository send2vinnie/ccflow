using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.GPM
{
	/// <summary>
	/// ������Ա����
	/// </summary>
    public class EmpAttr : BP.En.EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ����
        /// </summary>
        public const string Pass = "Pass";
        /// <summary>
        /// SID
        /// </summary>
        public const string SID = "SID";
        /// <summary>
        /// �˵�����ʱ��
        /// </summary>
        public const string UpdateMenu = "UpdateMenu";
        /// <summary>
        /// StaffID
        /// </summary>
        public const string StaffID = "StaffID";
        /// <summary>
        /// DepartmentID
        /// </summary>
        public const string DepartmentID = "DepartmentID";
        #endregion
    }
	/// <summary>
	/// Emp ��ժҪ˵����
	/// </summary>
    public class Emp : EntityNoName
    {
        public new string Name
        {
            get
            {
                if (BP.Web.WebUser.SysLang == "B5")
                    return Sys.Language.Turn2Traditional(this.GetValStrByKey("Name"));
                return this.GetValStrByKey("Name");
            }
            set
            {
                this.SetValByKey("Name", value);
            }
        }

        #region ��չ����
        public int StaffID
        {
            get
            {
                return this.GetValIntByKey(EmpAttr.StaffID);
            }
            set
            {
                this.SetValByKey(EmpAttr.StaffID, value);
            }
        }
        /// <summary>
        /// ��Ҫ�Ĳ��š�
        /// </summary>
        public Dept HisDept
        {
            get
            {

                try
                {
                    return new Dept(this.FK_Dept);
                }
                catch (Exception ex)
                {
                    throw new Exception("@��ȡ����Ա" + this.No + "����[" + this.FK_Dept + "]���ִ���,������ϵͳ����Աû�и���ά������.@" + ex.Message);
                }
            }
        }
        /// <summary>
        /// ������λ���ϡ�
        /// </summary>
        public Stations HisStations
        {
            get
            {
                EmpStations sts = new EmpStations();
                Stations mysts = sts.GetHisStations(this.No);
                return mysts;
                //return new Station(this.FK_Station);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(EmpAttr.FK_Dept, value);
            }
        }
        public string FK_DeptText
        {
            get
            {
                return this.GetValRefTextByKey(EmpAttr.FK_Dept);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Pass
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.Pass);
            }
            set
            {
                this.SetValByKey(EmpAttr.Pass, value);
            }
        }
        /// <summary>
        /// SID
        /// </summary>
        public string SID
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.SID);
            }
            set
            {
                this.SetValByKey(EmpAttr.SID, value);
            }
        }
        #endregion

        public bool CheckPass(string pass)
        {
            if (this.Pass == pass)
                return true;
            return false;
        }
        /// <summary>
        /// ������Ա
        /// </summary>
        public Emp()
        {

        }
        /// <summary>
        /// ������Ա���
        /// </summary>
        /// <param name="_No">No</param>
        public Emp(string no)
        {
            this.No = no.Trim();
            if (this.No.Length == 0)
                throw new Exception("@Ҫ��ѯ�Ĳ���Ա���Ϊ�ա�");
            try
            {
                this.Retrieve();
            }
            catch (Exception ex1)
            {
                int i = this.RetrieveFromDBSources();
                if (i == 0)
                    throw new Exception("@�û������������[" + no + "]�������ʺű�ͣ�á�@������Ϣ(���ڴ��в�ѯ���ִ���)��ex1=" + ex1.Message);
            }
        }
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForAppAdmin();
                return uac;
            }
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

                Map map = new Map();

                #region ��������
                map.EnDBUrl =
                    new DBUrl(DBUrlType.AppCenterDSN); //Ҫ���ӵ�����Դ����ʾҪ���ӵ����Ǹ�ϵͳ���ݿ⣩��
                map.PhysicsTable = "Port_Emp"; // Ҫ�����
                map.DepositaryOfMap = Depositary.Application;    //ʵ��map�Ĵ��λ��.
                map.DepositaryOfEntity = Depositary.Application; //ʵ����λ��
                map.EnDesc = this.ToE("Emp", "�û�"); // "�û�";       // ʵ�������.
                map.EnType = EnType.App;   //ʵ�����͡�
                map.Helper = "Emp.html";
                #endregion

                #region �ֶ�
                /*�����ֶ����Ե����� */
                map.AddTBStringPK(EmpAttr.No, null, "���", true, false, 1, 20, 100);
                map.AddTBString(EmpAttr.Name, null, "����", true, false, 0, 100, 100);
                map.AddTBString(EmpAttr.Pass, "pub", "����", false, false, 0, 20, 10);
                map.AddDDLEntities(EmpAttr.FK_Dept, null, "����", new BP.Port.Depts(), true);
                map.AddTBString(EmpAttr.SID, null, "SID", false, false, 0, 200, 10);

                map.AddTBInt(EmpAttr.StaffID, 0, "StaffID", false, false);
                map.AddTBInt(EmpAttr.DepartmentID, 0, "DepartmentID", false, false);
                
                
                #endregion �ֶ�

                map.AddSearchAttr(EmpAttr.FK_Dept);

                #region ���ӵ�Զ�����
                //���Ĳ���Ȩ��
                map.AttrsOfOneVSM.Add(new EmpStations(), new Stations(), EmpStationAttr.FK_Emp, EmpStationAttr.FK_Station, DeptAttr.Name, DeptAttr.No, "��λȨ��");
                map.AttrsOfOneVSM.Add(new EmpDepts(), new Depts(), EmpDeptAttr.FK_Emp, EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "��������");
                map.AttrsOfOneVSM.Add(new DeptSearchScorps(), new Depts(), EmpDeptAttr.FK_Emp, EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "��ѯȨ��");
                #endregion

                RefMethod rm = new RefMethod();
                rm.Title = "��CCIM����ͬ��";
                rm.ClassMethodName = this.ToString() + ".DoSubmitToCCIM";
                rm.IsForEns = false;
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        public string DoSubmitToCCIM()
        {
            try
            {
                Emps ens = new Emps();
                ens.RetrieveAllFromDBSource();
                foreach (Emp en in ens)
                {
                    if (en.StaffID == 0)
                    {
                        en.StaffID = DBAccess.GenerOID();
                        en.Update();
                    }

                    Paras ps = new Paras();
                    ps.Add("UserID", en.StaffID);
                    BP.DA.DBProcedure.RunSP("sp_UpdateUser", ps);
                }

                /* �п����ǲ��ŷ����仯. */
                Dept dept = new Dept();
                dept.DoSubmitToCCIM();

                return "���е�����ͬ��ִ�гɹ���";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

             
        }
        public override Entities GetNewEntities
        {
            get { return new Emps(); }
        }
    }
	/// <summary>
	/// ������Ա
	/// </summary>
    public class Emps : EntitiesNoName
    {
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Emp();
            }
        }
        /// <summary>
        /// ������Աs
        /// </summary>
        public Emps()
        {
        }
        /// <summary>
        /// ��ѯȫ��
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(EmpAttr.FK_Dept, " like ", BP.Web.WebUser.FK_Dept + "%");
            qo.addOrderBy(EmpAttr.No);
            return qo.DoQuery();
        }
    }
}
 