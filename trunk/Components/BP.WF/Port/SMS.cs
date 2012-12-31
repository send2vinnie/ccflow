using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.WF.Port;

namespace BP.Sys
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
        /// 消息标记
        /// </summary>
        public const string MsgFlag = "MsgFlag";
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
        /// <summary>
        /// 手机内容
        /// </summary>
        public const string TelDoc = "TelDoc";
        /// <summary>
        /// 标题
        /// </summary>
        public const string EmailTitle = "EmailTitle";
        /// <summary>
        /// 内容
        /// </summary>
        public const string EmailDoc = "EmailDoc";
        /// <summary>
        /// 邮件地址
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// 发送人
        /// </summary>
        public const string Sender = "Sender";
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
        /// <summary>
        /// 插入消息
        /// </summary>
        /// <param name="pk">消息主键</param>
        /// <param name="sendToEmp">要通知的人员</param>
        /// <param name="telDoc">短信消息</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="emailDoc">邮件内容</param>
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

            //// 人员.
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
        /// <summary>
        /// 消息标记
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
        /// 发送人
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
        /// 记录日期
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
        /// 邮件地址
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
        /// 短信内容
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
        /// 邮件标题
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
        /// 邮件内容
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
        /// 接受人
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
        /// 发送给
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
        /// 标题
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
        /// 电话
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

        #region 构造函数
        /// <summary>
        /// UI界面上的访问控制
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

                Map map = new Map("Sys_SMS");
                map.EnDesc = "消息";

                map.AddMyPK();
                map.AddTBString(SMSAttr.Sender, null, "发送人", false, true, 0, 20, 20);
                map.AddTBString(SMSAttr.MsgFlag, null, "消息标记", false, true, 0, 20, 20);
                map.AddTBString(SMSAttr.Tel, null, "手机号", false, true, 0, 20, 20);
                map.AddTBString(SMSAttr.TelDoc, null, "内容", false, true, 0, 3000, 20);

                map.AddTBString(SMSAttr.FK_Emp, null, "发送给", false, false, 0, 90, 150);
                map.AddDDLSysEnum(SMSAttr.SMSSta, (int)SMSSta.UnRun, "状态", true, true, SMSAttr.SMSSta, "@0=未开始@1=执行完成");

                map.AddDDLSysEnum(SMSAttr.AlertType, 0, "提示信息类型", true, true);

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
 