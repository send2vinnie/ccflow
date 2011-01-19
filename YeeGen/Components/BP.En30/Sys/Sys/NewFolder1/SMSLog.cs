using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// 短消息
	/// </summary>
    public class SMSLogAttr
    {
        /// <summary>
        /// OID
        /// </summary>
        public const string OID = "OID";
        /// <summary>
        /// 发送人
        /// </summary>
        public const string Asker = "Asker";
        /// <summary>
        /// 发送时间
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 标题
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// 内容
        /// </summary>
        public const string Docs = "Docs";
        /// <summary>
        /// dtl num
        /// </summary>
        public const string DtlNum = "DtlNum";
        /// <summary>
        /// ReadNum
        /// </summary>
        public const string ReadNum = "ReadNum";
    }
	/// <summary>
	/// 短消息
	/// </summary> 
    public class SMSLog : EntityMyPK
    {
        #region 基本属性
        /// <summary>
        /// AskerText
        /// </summary>
        public string AskerText
        {
            get
            {
                return this.GetValRefTextByKey(SMSLogAttr.Asker);
            }
        }
        /// <summary>
        /// 发送人
        /// </summary>
        public string Asker
        {
            get
            {
                return this.GetValStringByKey(SMSLogAttr.Asker);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.Asker, value);
            }
        }
        /// <summary>
        /// 发送日期时间
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SMSLogAttr.RDT);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.RDT, value);
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(SMSLogAttr.Title);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.Title, value);
            }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Docs
        {
            get
            {
                return this.GetValStringByKey(SMSLogAttr.Docs);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.Docs, value);
            }
        }
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(SMSLogAttr.Docs);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.Docs, value);
            }
        }
        public string DocsHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(SMSLogAttr.Docs);
            }
        }
        /// <summary>
        /// dtl Num
        /// </summary>
        public int DtlNum
        {
            get
            {
                return this.GetValIntByKey(SMSLogAttr.DtlNum);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.DtlNum, value);
            }
        }
        /// <summary>
        /// 阅读个
        /// </summary>
        public int ReadNum
        {
            get
            {
                return this.GetValIntByKey(SMSLogAttr.ReadNum);
            }
            set
            {
                this.SetValByKey(SMSLogAttr.ReadNum, value);
            }
        }
        #endregion

        #region 构造方法
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
        /// 短消息
        /// </summary>
        public SMSLog() { }
        /// <summary>
        /// 短消息
        /// </summary>
        /// <param name="oid"></param>
        public SMSLog(string oid)
            : base(oid)
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
                Map map = new Map("Sys_SMSLog");
                map.EnDesc = "短消息";
                map.EnType = EnType.Admin;

                map.AddMyPK();
                map.AddDDLEntities(SMSLogAttr.Asker, Web.WebUser.No, "提问人", new Emps(), false);
                map.AddTBString(SMSLogAttr.Title, null, "标题", true, false, 0, 500, 20);
                map.AddTBStringDoc(SMSLogAttr.Docs, null, "问题内容", true, false);
                map.AddTBDateTime(SMSLogAttr.RDT, "提问时间", true, false);
                map.AddTBInt(SMSLogAttr.DtlNum, 0, "回答个数", true, false);
                map.AddTBInt(SMSLogAttr.ReadNum, 0, "阅读数", true, false);

                //map.AddDtl( new SMSLogDtls(), SMSLogDtlAttr.FK_SMSLog);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 短消息
	/// </summary> 
    public class SMSLogs : EntitiesMyPK
    {
        #region 构造函数
        /// <summary>
        /// 关于实体访问的构造
        /// </summary>
        public SMSLogs()
        {
        }
        /// <summary>
        /// New entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SMSLog();
            }
        }
        #endregion
    }
}
