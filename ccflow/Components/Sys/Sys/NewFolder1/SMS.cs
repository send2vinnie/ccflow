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
    public class SMSAttr
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
    public class SMS : EntityMyPK
    {
        #region 基本属性
        /// <summary>
        /// AskerText
        /// </summary>
        public string AskerText
        {
            get
            {
                return this.GetValRefTextByKey(SMSAttr.Asker);
            }
        }
        /// <summary>
        /// 发送人
        /// </summary>
        public string Asker
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Asker);
            }
            set
            {
                this.SetValByKey(SMSAttr.Asker, value);
            }
        }
        /// <summary>
        /// 发送日期时间
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.RDT);
            }
            set
            {
                this.SetValByKey(SMSAttr.RDT, value);
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Title);
            }
            set
            {
                this.SetValByKey(SMSAttr.Title, value);
            }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Docs
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Docs);
            }
            set
            {
                this.SetValByKey(SMSAttr.Docs, value);
            }
        }
        public string Doc
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Docs);
            }
            set
            {
                this.SetValByKey(SMSAttr.Docs, value);
            }
        }
        public string DocsHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(SMSAttr.Docs);
            }
        }
        /// <summary>
        /// dtl Num
        /// </summary>
        public int DtlNum
        {
            get
            {
                return this.GetValIntByKey(SMSAttr.DtlNum);
            }
            set
            {
                this.SetValByKey(SMSAttr.DtlNum, value);
            }
        }
        /// <summary>
        /// 阅读个
        /// </summary>
        public int ReadNum
        {
            get
            {
                return this.GetValIntByKey(SMSAttr.ReadNum);
            }
            set
            {
                this.SetValByKey(SMSAttr.ReadNum, value);
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
        public SMS() { }
        /// <summary>
        /// 短消息
        /// </summary>
        /// <param name="oid"></param>
        public SMS(string oid)
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
                Map map = new Map("Sys_SMS");
                map.EnDesc = "短消息";
                map.EnType = EnType.Admin;

                map.AddMyPK();
                map.AddDDLEntities(SMSAttr.Asker, Web.WebUser.No, "提问人", new Emps(), false);
                map.AddTBString(SMSAttr.Title, null, "标题", true, false, 0, 500, 20);
                map.AddTBStringDoc(SMSAttr.Docs, null, "问题内容", true, false);
                map.AddTBDateTime(SMSAttr.RDT, "提问时间", true, false);
                map.AddTBInt(SMSAttr.DtlNum, 0, "回答个数", true, false);
                map.AddTBInt(SMSAttr.ReadNum, 0, "阅读数", true, false);

                //map.AddDtl( new SMSDtls(), SMSDtlAttr.FK_SMS);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// 短消息
	/// </summary> 
	public class SMSs : EntitiesMyPK
	{
		#region 构造函数
		/// <summary>
		/// 关于实体访问的构造
		/// </summary>
		public SMSs()
		{
		}
		/// <summary>
		/// New entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SMS();
			}
		}
		#endregion
	
	}
}
