using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.WF.Port;

namespace BP.Sys
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
        /// ��Ϣ���
        /// </summary>
        public const string MsgFlag = "MsgFlag";
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
        /// <summary>
        /// �ֻ�����
        /// </summary>
        public const string TelDoc = "TelDoc";
        /// <summary>
        /// ����
        /// </summary>
        public const string EmailTitle = "EmailTitle";
        /// <summary>
        /// ����
        /// </summary>
        public const string EmailDoc = "EmailDoc";
        /// <summary>
        /// �ʼ���ַ
        /// </summary>
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
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="pk">��Ϣ����</param>
        /// <param name="sendToEmp">Ҫ֪ͨ����Ա</param>
        /// <param name="telDoc">������Ϣ</param>
        /// <param name="mailTitle">�ʼ�����</param>
        /// <param name="emailDoc">�ʼ�����</param>
        public static void AddMsg(string pk, string sendToEmp,string telDoc,string mailTitle, string emailDoc)
        {
           // if (at == AlertWay.None)
             AlertWay   at = AlertWay.None;

            if (mailTitle == null)
                mailTitle = telDoc;

            if (at == AlertWay.AppSystemMsg)
            {
                Paras pss = new Paras();
                pss.Add("Sender", BP.Web.WebUser.No);
                pss.Add("Receivers", sendToEmp);
                pss.Add("Title", mailTitle);
                pss.Add("Context", emailDoc);
                BP.DA.DBAccess.RunSP("CCstaff", pss);
                return;
            }

            //// ��Ա.
            //WFEmp emp = new WFEmp(fk_emp);

            SMS sms = new SMS();
            sms.MyPK = pk;
            sms.FK_Emp = sendToEmp;
          //  sms.Tel = emp.Tel;
            sms.TelDoc = telDoc;
            sms.Sender = BP.Web.WebUser.No;
           // sms.Email = emp.Email;
            sms.EmailTitle = mailTitle;
            sms.EmailDoc = emailDoc;
            sms.RDT = BP.DA.DataType.CurrentDataTime;
            try
            {
                sms.Insert();
            }
            catch
            {
                sms.Update();
            }
        }
        public static void AddMsg_Del(string pk, string fk_emp, AlertWay at, string tel, string telDoc,
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
            sms.FK_Emp = fk_emp;
            sms.Tel = tel;
            sms.TelDoc = telDoc;
            sms.Sender = BP.Web.WebUser.No;
            sms.Email = email;
            sms.EmailTitle = mailTitle;
            sms.EmailDoc = emailDoc;
            sms.RDT = BP.DA.DataType.CurrentDataTime;
            try
            {
                sms.Insert();
            }
            catch
            {
                sms.Update();
            }
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
        /// <summary>
        /// ��Ϣ���
        /// </summary>
        public string MsgFlag
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.MsgFlag);
            }
            set
            {
                SetValByKey(SMSAttr.MsgFlag, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
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
        /// <summary>
        /// ��¼����
        /// </summary>
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
        /// <summary>
        /// �ʼ���ַ
        /// </summary>
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
        /// <summary>
        /// ��������
        /// </summary>
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
        /// <summary>
        /// �ʼ�����
        /// </summary>
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
        /// <summary>
        /// �ʼ�����
        /// </summary>
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
        /// <summary>
        /// ������
        /// </summary>
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
        /// ���͸�
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
        /// <summary>
        /// ����
        /// </summary>
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
        /// <summary>
        /// �绰
        /// </summary>
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
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
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

                Map map = new Map("Sys_SMS");
                map.EnDesc = "��Ϣ";

                map.AddMyPK();
                map.AddTBString(SMSAttr.Sender, null, "������", false, true, 0, 20, 20);
                map.AddTBString(SMSAttr.MsgFlag, null, "��Ϣ���", false, true, 0, 20, 20);
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
 