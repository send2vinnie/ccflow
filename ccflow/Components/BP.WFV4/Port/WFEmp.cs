using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port; 
using BP.Port; 
using BP.En;
using BP.Web;
using System.Drawing;
using System.Text;
using System.IO;

namespace BP.WF.Port
{
    public enum AlertWay
    {
        /// <summary>
        /// ����ʾ
        /// </summary>
        None,
        /// <summary>
        /// �ֻ�����
        /// </summary>
        SMS,
        /// <summary>
        /// �ʼ�
        /// </summary>
        Email,
        /// <summary>
        /// �ֻ�����+�ʼ�
        /// </summary>
        SMSAndEmail,
        /// <summary>
        /// �ڲ���Ϣ
        /// </summary>
        AppSystemMsg
    }
	/// <summary>
	/// ����Ա
	/// </summary>
    public class WFEmpAttr
    {
        #region ��������
        /// <summary>
        /// No
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ������
        /// </summary>
        public const string Name = "Name";
        public const string LoginData = "LoginData";
        public const string Tel = "Tel";
        /// <summary>
        /// ��Ȩ��
        /// </summary>
        public const string Author = "Author";
        /// <summary>
        /// ��Ȩ����
        /// </summary>
        public const string AuthorDate = "AuthorDate";
        /// <summary>
        /// �Ƿ�����Ȩ״̬
        /// </summary>
        public const string AuthorIsOK = "AuthorIsOK";
        /// <summary>
        /// ��Ȩ�Զ��ջ�����
        /// </summary>
        public const string AuthorToDate = "AuthorToDate";
        public const string Email = "Email";
        public const string AlertWay = "AlertWay";
        public const string Stas = "Stas";
        public const string FK_Dept = "FK_Dept";
        public const string Idx = "Idx";
        public const string FtpUrl = "FtpUrl";
        public const string Style = "Style";
        public const string Msg = "Msg";
        public const string TM = "TM";
        #endregion
    }
	/// <summary>
	/// ����Ա
	/// </summary>
    public class WFEmp : EntityNoName
    {
        #region ��������
        public string HisAlertWayT
        {
            get
            {
                return this.GetValRefTextByKey(WFEmpAttr.AlertWay);
            }
        }
        public AlertWay HisAlertWay
        {
            get
            {
                return (AlertWay)this.GetValIntByKey(WFEmpAttr.AlertWay);
            }
            set
            {
                SetValByKey(WFEmpAttr.AlertWay, (int)value);
            }
        }
        public string FK_Dept
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.FK_Dept);
            }
            set
            {
                SetValByKey(WFEmpAttr.FK_Dept, value);
            }
        }
        public string Style
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Style);
            }
            set
            {
                this.SetValByKey(WFEmpAttr.Style, value);
            }
        }
        public string TM
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.TM);
            }
            set
            {
                this.SetValByKey(WFEmpAttr.TM, value);
            }
        }
        public string Tel
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Tel);
            }
            set
            {
                SetValByKey(WFEmpAttr.Tel, value);
            }
        }
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(WFEmpAttr.Idx);
            }
            set
            {
                SetValByKey(WFEmpAttr.Idx, value);
            }
        }
        public string TelHtml
        {
            get
            {
                if (this.Tel.Length == 0)
                    return this.ToE("NotSet","δ����");
                else
                    return "<a href=\"javascript:WinOpen('./Msg/SMS.aspx?Tel=" + this.Tel + "');\"  ><img src=./Img/SMS.gif border=0/>" + this.Tel + "</a>";
            }
        }
        public string EmailHtml
        {
            get
            {
                if (this.Email==null || this.Email.Length == 0)
                    return this.ToE("NotSet","δ����");
                else
                    return "<a href='Mailto:" + this.Email + "' ><img src=./Img/SMS.gif border=0/>" + this.Email + "</a>";
            }
        }
        public string Email
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.Email);
            }
            set
            {
                SetValByKey(WFEmpAttr.Email, value);
            }
        }
        public string Author
        {
            get
            {
                return this.GetValStrByKey(WFEmpAttr.Author);
            }
            set
            {
                SetValByKey(WFEmpAttr.Author, value);
            }
        }
        public string AuthorDate
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.AuthorDate);
            }
            set
            {
                SetValByKey(WFEmpAttr.AuthorDate, value);
            }
        }
        public string AuthorToDate
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.AuthorToDate);
            }
            set
            {
                SetValByKey(WFEmpAttr.AuthorToDate, value);
            }
        }
        public string FtpUrl
        {
            get
            {
                return this.GetValStringByKey(WFEmpAttr.FtpUrl);
            }
            set
            {
                SetValByKey(WFEmpAttr.FtpUrl, value);
            }
        }
        public string Stas
        {
            get
            {
                string s= this.GetValStringByKey(WFEmpAttr.Stas);
                if (s == "")
                {
                    EmpStations ess = new EmpStations();
                    ess.Retrieve(EmpStationAttr.FK_Emp, this.No);
                    foreach (EmpStation es in ess)
                    {
                        s += es.FK_StationT + ",";
                    }

                    if (ess.Count != 0)
                    {
                        this.Stas = s;
                        this.Update();
                        //this.Update(WFEmpAttr.Stas, s);
                    }
                    return s;
                }
                else
                {
                    return s;
                }
            }
            set
            {
                SetValByKey(WFEmpAttr.Stas, value);
            }
        }
        public bool AuthorIsOK
        {
            get
            {
                bool b= this.GetValBooleanByKey(WFEmpAttr.AuthorIsOK);
                if (b == false)
                    return false;

                if (this.AuthorToDate.Length < 4)
                    return true;

                DateTime dt = DataType.ParseSysDateTime2DateTime(this.AuthorToDate);
                if (dt > DateTime.Now)
                    return false;

                return true;
            }
            set
            {
                SetValByKey(WFEmpAttr.AuthorIsOK, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ����Ա
        /// </summary>
        public WFEmp() { }
        /// <summary>
        /// ����Ա
        /// </summary>
        /// <param name="no"></param>
        public WFEmp(string no)
        {
            this.No = no;
            try
            {
                if (this.RetrieveFromDBSources() == 0)
                {
                    Emp emp = new Emp(no);
                    this.Copy(emp);
                    this.Insert();
                }
            }
            catch
            {
                this.CheckPhysicsTable();
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

                Map map = new Map("WF_Emp");
                map.EnDesc = "����Ա";
                map.EnType = EnType.App;
                map.AddTBStringPK(WFEmpAttr.No, null, "No", true, true, 1, 50, 20);
                map.AddTBString(WFEmpAttr.Name, null, "Name", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.Tel, null, "Tel", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.FK_Dept, null, "FK_Dept", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.Email, null, "Email", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.TM, null, "��ʱͨѶ��", true, true, 0, 50, 20);
                map.AddDDLSysEnum(WFEmpAttr.AlertWay, 3, "������ʽ", true, true, WFEmpAttr.AlertWay);
                map.AddTBString(WFEmpAttr.Author, null, "��Ȩ��", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.AuthorDate, null, "��Ȩ����", true, true, 0, 50, 20);
                map.AddTBInt(WFEmpAttr.AuthorIsOK, 0, "�Ƿ���Ȩ�ɹ�", true, true);
                map.AddTBDate(WFEmpAttr.AuthorToDate, null, "��Ȩ������", true, true);
                map.AddTBString(WFEmpAttr.Stas, null, "��λs", true, true, 0, 3000, 20);
                map.AddTBString(WFEmpAttr.FtpUrl, null, "FtpUrl", true, true, 0, 50, 20);
                map.AddTBString(WFEmpAttr.Msg, null, "Msg", true, true, 0, 4000, 20);
                map.AddTBInt(WFEmpAttr.Style, 0, "Style", false, false);
                map.AddTBInt(WFEmpAttr.Idx, 0, "Idx", false, false);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region ����
        protected override bool beforeUpdate()
        {
            string msg = "";
            //if (this.Email.Length == 0)
            //{
            //    if (this.HisAlertWay == AlertWay.SMSAndEmail || this.HisAlertWay == AlertWay.Email)
            //        msg += "��������������e-mail������Ϣ��������û������e-mail��";
            //}

            //if (this.Tel.Length == 0)
            //{
            //    if (this.HisAlertWay == AlertWay.SMSAndEmail || this.HisAlertWay == AlertWay.SMS)
            //        msg += "�������������ö��Ž�����Ϣ��������û�������ֻ��š�";
            //}

            EmpStations ess = new EmpStations();
            ess.Retrieve(EmpStationAttr.FK_Emp, this.No);
            string sts = "";
            foreach (EmpStation es in ess)
            {
                sts += es.FK_StationT + " ";
            }
            this.Stas = sts;

            if (msg != "")
                throw new Exception(msg);

            return base.beforeUpdate();
        }
        #endregion

        public static void DTSData()
        {
            string sql = "select No from port_emp where No not in (select no from wf_emp ) ";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                BP.Port.Emp emp1 = new BP.Port.Emp(dr["No"].ToString());
                BP.WF.Port.WFEmp empWF = new BP.WF.Port.WFEmp();
                empWF.Copy(emp1);
                try
                {
                    empWF.DirectInsert();
                }
                catch
                {
                }
            }
        }
        public void DoUp()
        {
            this.DoOrderUp("FK_Dept", this.FK_Dept, "Idx");
            return;

            //string sql = "SELECT No,Idx FROM WF_Emp WHERE FK_Dept='" + this.FK_Dept + "' order by IDX";
            //DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            //int idx = 0;
            //string beforeNo = "";
            //string myNo = "";
            //bool isMeet = false;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    idx++;
            //    myNo = dr["No"].ToString();
            //    if (myNo == this.No)
            //        isMeet = true;

            //    if (isMeet == false)
            //        beforeNo = myNo;
            //    DBAccess.RunSQL("UPDATE WF_Emp SET idx=" + idx + " WHERE No='" + myNo + "'");
            //}
            //DBAccess.RunSQL("UPDATE WF_Emp SET Idx=Idx+1 WHERE No='" + beforeNo + "'");
            //DBAccess.RunSQL("UPDATE WF_Emp SET Idx=Idx-1 WHERE No='" + this.No + "'");
        }
        public void DoDown()
        {
            this.DoOrderDown("FK_Dept", this.FK_Dept, "Idx");
            return;
            //string sql = "SELECT No,Idx FROM WF_Emp WHERE FK_Dept='" + this.FK_Dept + "' order by IDX";
            //DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            //int idx = 0;
            //string nextNo = "";
            //string myNo = "";
            //bool isMeet = false;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    myNo = dr["No"].ToString();
            //    if (isMeet == true)
            //    {
            //        nextNo = myNo;
            //        isMeet = false;
            //    }

            //    idx++;

            //    if (myNo == this.No)
            //        isMeet = true;

               
            //    DBAccess.RunSQL("UPDATE WF_Emp SET idx=" + idx + " WHERE No='" + myNo + "'");
            //}

            //DBAccess.RunSQL("UPDATE WF_Emp SET Idx=Idx-1 WHERE No='" + nextNo + "'");
            //DBAccess.RunSQL("UPDATE WF_Emp SET Idx=Idx+1 WHERE No='" + this.No + "'");
        }
    }
	/// <summary>
	/// ����Աs 
	/// </summary>
	public class WFEmps : EntitiesNoName
	{	 
		#region ����
		/// <summary>
		/// ����Աs
		/// </summary>
		public WFEmps()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new WFEmp();
			}
		}

        public override int RetrieveAll()
        {
            return base.RetrieveAll("FK_Dept","Idx");
        }
		#endregion
	}
	
}
