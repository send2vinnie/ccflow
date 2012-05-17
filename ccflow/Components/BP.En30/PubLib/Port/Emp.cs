using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.Port
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
        /// FK_Unit
        /// </summary>
        public const string FK_Unit = "FK_Unit";
        /// <summary>
        /// ����
        /// </summary>
        public const string Pass = "Pass";
        ///// <summary>
        ///// PID
        ///// </summary>
        //public const string PID = "PID";
        ///// <summary>
        ///// pin
        ///// </summary>
        //public const string PIN = "PIN";
        ///// <summary>
        ///// UKEY����
        /////</summary>
        //public const string KeyPass = "KeyPass";
        ///// <summary>
        ///// �Ƿ�ʹ��UKEY
        ///// </summary>
        //public const string IsUSBKEY = "IsUSBKEY";
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

        //public new string PID
        //{
        //    get
        //    {              
        //        return this.GetValStrByKey("PID");
        //    }
        //    set
        //    {
        //        this.SetValByKey("PID", value);
        //    }
        //}
        //public new string PIN
        //{
        //    get
        //    {
        //        return this.GetValStrByKey("PIN");
        //    }
        //    set
        //    {
        //        this.SetValByKey("PIN", value);
        //    }
        //}

        //public new string KeyPass
        //{
        //    get
        //    {
        //        return this.GetValStrByKey("KeyPass");
        //    }
        //    set
        //    {
        //        this.SetValByKey("KeyPass", value);
        //    }
        //}

        //public new string IsUSBKEY
        //{
        //    get
        //    {
        //        return this.GetValStrByKey("IsUSBKEY");
        //    }
        //    set
        //    {
        //        this.SetValByKey("IsUSBKEY", value);
        //    }
        //}
        #region ��չ����
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
        /// �������ż���
        /// </summary>
        public Depts HisDepts
        {
            get
            {
                EmpDepts sts = new EmpDepts();
                Depts dpts = sts.GetHisDepts(this.No);
                if (dpts.Count==0)
                {
                    string sql = "select fk_dept from port_emp where no='"+this.No+"' and fk_dept in(select no from port_dept)";
                    string fk_dept = BP.DA.DBAccess.RunSQLReturnVal(sql) as string;
                    if (fk_dept == null)
                        return dpts;

                    Dept dept = new Dept(fk_dept);
                    dpts.AddEntity(dept);
                }
                return dpts;
            }
        }
        private BP.Port.Unit _HisUnit = null;
        public BP.Port.Unit HisUnit
        {
            get
            {
                if (_HisUnit == null)
                {
                    string sql = "SELECT FK_Unit FROM Port_Emp WHERE No='" + this.No+"'";
                    string no= BP.DA.DBAccess.RunSQLReturnString(sql);
                    _HisUnit = new Unit(no);
                }
                return _HisUnit;
            }
        }
        /// <summary>
        /// ��λ
        /// </summary>
        public string FK_Unit
        {
            get
            {
              return  this.HisUnit.No;
            }
        }
        public string FK_UnitText
        {
            get
            {
                return this.HisUnit.Name;
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
                map.EnDesc = this.ToE("Emp", "�û�"); // "�û�"; // ʵ�������.
                map.EnType = EnType.App;   //ʵ�����͡�
                #endregion

                #region �ֶ�
                /*�����ֶ����Ե����� */
                map.AddTBStringPK(EmpAttr.No, null, this.ToE("No", "���"), true, false, 1, 20, 30);
                map.AddTBString(EmpAttr.Name, null, this.ToE("Name", "����"), true, false, 0, 100, 30);

                
                map.AddTBString(EmpAttr.Pass, "pub", this.ToE("Pass", "����"), false, false, 0, 20, 10);
                map.AddDDLEntities(EmpAttr.FK_Dept, null, this.ToE("Dept", "����"), new Port.Depts(), true);


                //map.AddTBString(EmpAttr.PID, null, this.ToE("PID", "UKEY��PID"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.PIN, null, this.ToE("PIN", "UKEY��PIN"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.KeyPass, null, this.ToE("KeyPass", "UKEY��KeyPass"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.IsUSBKEY, null, this.ToE("IsUSBKEY", "�Ƿ�ʹ��usbkey"), true, false, 0, 100, 30);
                // map.AddDDLSysEnum("Sex", 0, "�Ա�", "@0=Ů@1=��");
                #endregion �ֶ�

                map.AddSearchAttr(EmpAttr.FK_Dept);

                #region ���ӵ�Զ�����
                //���Ĳ���Ȩ��
                map.AttrsOfOneVSM.Add(new EmpDepts(), new Depts(), EmpDeptAttr.FK_Emp, EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "����Ȩ��");
                map.AttrsOfOneVSM.Add(new EmpStations(), new Stations(), EmpStationAttr.FK_Emp, EmpStationAttr.FK_Station,
                    DeptAttr.Name, DeptAttr.No, "��λȨ��");
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        public override Entities GetNewEntities
        {
            get { return new Emps(); } 
        }
    }
	/// <summary>
	/// ������Ա
	// </summary>
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
 