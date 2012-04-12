using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.WF.Port;

namespace BP.TA
{
	/// <summary>
	/// ��Ϣ״̬
	/// </summary>
	public enum SMSSta
	{
		/// <summary>
		/// δ��ʼ
		/// </summary>
		UnRun,
		/// <summary>
		/// �ɹ�
		/// </summary>
		RunOK,
        /// <summary>
        /// ʧ��
        /// </summary>
	    RunError
	}
	/// <summary>
	/// ��Ϣ����
	/// </summary>
	public class SMSAttr:EntityMyPKAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string SMSSta="SMSSta";
		/// <summary>
		/// ��¼��
		/// </summary>
		public const string FK_Emp="FK_Emp";
        /// <summary>
        /// Tel
        /// </summary>
        public const string Tel = "Tel"; 
		/// <summary>
		/// ��¼����
		/// </summary>
		public const string RDT="RDT"; 	
		/// <summary>
		/// ��ע
		/// </summary>
        public const string SDT = "SDT"; 	
        /// <summary>
        /// Doc
        /// </summary>
        public const string Doc = "Doc";
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        public const string AlertType = "AlertType";

        public const string TelDoc = "TelDoc";
        public const string EmailTitle = "EmailTitle";
        public const string EmailDoc = "EmailDoc";
        public const string Email = "Email";
        /// <summary>
        /// ������
        /// </summary>
        public const string Sender = "Sender";
	}
	/// <summary>
	/// ��Ϣ
	/// </summary> 
    public class SMS : EntityMyPK
    {
        public int AlertType
        {
            get
            {
                return this.GetValIntByKey(SMSAttr.AlertType);
            }
            set
            {
                this.SetValByKey(SMSAttr.AlertType, value);
            }
        }

        #region ͨ�÷���
        public static void AddMsg(string pk, string fk_emp, AlertWay at, string tel, string telDoc,
            string email, string mailTitle, string emailDoc)
        {


            if (at == AlertWay.None)
                at = AlertWay.Email;

            if (mailTitle == null)
                mailTitle = telDoc;

            if (at == AlertWay.AppSystemMsg)
            {
                Paras pss =new Paras();
                pss.Add("Sender", BP.Web.WebUser.No);
                pss.Add("Receivers", fk_emp);
                pss.Add("Title", mailTitle);
                pss.Add("Context", emailDoc);
                BP.DA.DBAccess.RunSP("CCstaff", pss);
                return;
            }

            SMS sms = new SMS();
            sms.MyPK = pk;
            if (sms.IsExits)
                return;

            sms.FK_Emp = fk_emp;
            sms.Tel = tel;
            sms.TelDoc = telDoc;

            sms.Email = email;
            sms.EmailTitle = mailTitle;
            sms.EmailDoc = emailDoc;
            sms.RDT = BP.DA.DataType.CurrentDataTime;
            sms.Insert();
        }
        #endregion

        #region  ����
        public SMSSta HisSMSSta
        {
            get
            {
                return (SMSSta)this.GetValIntByKey(SMSAttr.SMSSta);
            }
            set
            {
                this.SetValByKey(SMSAttr.SMSSta, (int)value);
            }
        }
        public string Sender
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Sender);
            }
            set
            {
                SetValByKey(SMSAttr.Sender, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.RDT);
            }
            set
            {
                SetValByKey(SMSAttr.RDT, value);
            }
        }
        public string Email
        {
            get
            {
                string s = this.GetValStringByKey(SMSAttr.Email);
                if (string.IsNullOrEmpty(s))
                    s = "ccflow@ccflow.org";
                return s;
            }
            set
            {
                SetValByKey(SMSAttr.Email, value);
            }
        }
        public string TelDoc
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.TelDoc);
            }
            set
            {
                SetValByKey(SMSAttr.TelDoc, value);
            }
        }
        public string EmailTitle
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.EmailTitle);
            }
            set
            {
                SetValByKey(SMSAttr.EmailTitle, value);
            }
        }
        public string EmailDoc
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.EmailDoc).Replace('~','\'');
            }
            set
            {
                SetValByKey(SMSAttr.EmailDoc, value);
            }
        }
        public string Accepter
        {
            get
            {
                return this.FK_Emp;
            }
            set
            {
               this.FK_Emp= value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.FK_Emp);
            }
            set
            {
                SetValByKey(SMSAttr.FK_Emp, value);
            }
        }
        public string  Title
        {
            get
            {
                return this.EmailTitle;
            }
            set
            {
                this.EmailTitle = value;
            }
        }
        public string Tel
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Tel);
            }
            set
            {
                SetValByKey(SMSAttr.Tel, value);
            }
        }
        #endregion

        #region ���캯��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenAll();
                return uac;
            }
        }
        /// <summary>
        /// ��Ϣ
        /// </summary>
        public SMS()
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

                Map map = new Map("TA_SMS");
                map.EnDesc = "��������";

                map.AddMyPK();

                map.AddTBString(SMSAttr.Sender, null, "������", false, true, 0, 20, 20);

                map.AddTBString(SMSAttr.Tel, null, "�ֻ���", false, true, 0, 20, 20);
                map.AddTBString(SMSAttr.TelDoc, null, "����", false, true, 0, 3000, 20);

                map.AddTBString(SMSAttr.FK_Emp, null, "���͸�", false, false, 0, 90, 150);
                map.AddDDLSysEnum(SMSAttr.SMSSta, (int)SMSSta.UnRun, "״̬", true, true, SMSAttr.SMSSta, "@0=δ��ʼ@1=ִ�����");

                map.AddDDLSysEnum(SMSAttr.AlertType, 0, "��ʾ��Ϣ����", true, true);

                map.AddTBString(SMSAttr.Email, null, "Email", false, true, 0, 200, 20);
                map.AddTBString(SMSAttr.EmailTitle, null, "�ʼ�����", false, true, 0, 3000, 20);
                map.AddTBStringDoc(SMSAttr.EmailDoc, null, "�ʼ�����", false, true);

                map.AddTBDateTime(SMSAttr.RDT, "��¼ʱ��", true, false);
                map.AddTBDateTime(SMSAttr.SDT, "����ʱ��", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ��Ϣs
	/// </summary> 
    public class SMSs : Entities
    {
        public override Entity GetNewEntity
        {
            get
            {
                return new SMS();
            }
        }
        public SMSs()
        {
        }
    }
}
 