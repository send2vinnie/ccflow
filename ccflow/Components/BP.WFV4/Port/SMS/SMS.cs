using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.WF.Port;

namespace BP.TA
{
	/// <summary>
	/// 消息状态
	/// </summary>
	public enum SMSSta
	{
		/// <summary>
		/// 未开始
		/// </summary>
		UnRun,
		/// <summary>
		/// 成功
		/// </summary>
		RunOK,
        /// <summary>
        /// 失败
        /// </summary>
	    RunError
	}
	/// <summary>
	/// 消息属性
	/// </summary>
	public class SMSAttr:EntityMyPKAttr
	{
		/// <summary>
		/// 日期
		/// </summary>
		public const string SMSSta="SMSSta";
		/// <summary>
		/// 记录人
		/// </summary>
		public const string FK_Emp="FK_Emp";
        /// <summary>
        /// Tel
        /// </summary>
        public const string Tel = "Tel"; 
		/// <summary>
		/// 记录日期
		/// </summary>
		public const string RDT="RDT"; 	
		/// <summary>
		/// 备注
		/// </summary>
        public const string SDT = "SDT"; 	
        /// <summary>
        /// Doc
        /// </summary>
        public const string Doc = "Doc";
        /// <summary>
        /// 提示信息
        /// </summary>
        public const string AlertType = "AlertType";

        public const string TelDoc = "TelDoc";
        public const string EmailTitle = "EmailTitle";
        public const string EmailDoc = "EmailDoc";
        public const string Email = "Email";

	}
	/// <summary>
	/// 消息
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

        #region 通用方法
        public static void AddMsg(string pk, string fk_emp, AlertWay at, string tel, string telDoc,
            string email, string mailTitle, string emailDoc)
        {
            if (at == AlertWay.None)
                at = AlertWay.Email;

            if (mailTitle == null)
                mailTitle = telDoc;

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

        #region  属性
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
                return this.GetValStringByKey(SMSAttr.Email);
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

        #region 构造函数
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
        /// 消息
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
                map.EnDesc = "待办事项";

                map.AddMyPK();

                map.AddTBString(SMSAttr.Tel, null, "手机号", false, true, 0, 20, 20);
                map.AddTBString(SMSAttr.TelDoc, null, "内容", false, true, 0, 3000, 20);

                map.AddTBString(SMSAttr.FK_Emp, null, "发送给", false, false, 0, 90, 150);
                map.AddDDLSysEnum(SMSAttr.SMSSta, (int)SMSSta.UnRun, "状态", true, true, SMSAttr.SMSSta, "@0=未开始@1=执行完成");

                map.AddDDLSysEnum(SMSAttr.AlertType, 0, "提示信息类型", true, true, SMSAttr.AlertType,
                    "@0=短信@1=邮件@2=邮件与短信");

                map.AddTBString(SMSAttr.Email, null, "Email", false, true, 0, 200, 20);
                map.AddTBString(SMSAttr.EmailTitle, null, "邮件标题", false, true, 0, 3000, 20);
                map.AddTBStringDoc(SMSAttr.EmailDoc, null, "邮件内容", false, true);

                map.AddTBDateTime(SMSAttr.RDT, "记录时间", true, false);
                map.AddTBDateTime(SMSAttr.SDT, "发送时间", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 消息s
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
 