using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// 消息状态
    /// </summary>
    public enum MsgSta
    {
        /// <summary>
        /// 未阅读
        /// </summary>
        UnRead,
        /// <summary>
        /// 已经阅读
        /// </summary>
        Read,
        /// <summary>
        /// 已经回复
        /// </summary>
        Reply
    }
	/// <summary>
	/// 消息
	/// </summary>
    public class MsgAttr
    {
        /// <summary>
        /// OID
        /// </summary>
        public const string OID = "OID";
        /// <summary>
        /// 发送人
        /// </summary>
        public const string Sender = "Sender";
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
        public const string Doc = "Doc";
        /// <summary>
        /// 接受人
        /// </summary>
        public const string Accepter = "Accepter";
        /// <summary>
        /// 状态
        /// </summary>
        public const string MsgSta = "MsgSta";
        /// <summary>
        /// ODT
        /// </summary>
        public const string ODT = "ODT";
       /// <summary>
       /// 发送人
       /// </summary>
        public const string SenderT = "SenderT";
        /// <summary>
        /// 消息的ID
        /// </summary>
        public const string MsgID = "MsgID";
    }
	/// <summary>
	/// 消息
	/// </summary> 
	public class Msg:EntityOID 
	{
		#region 基本属性
        /// <summary>
        /// 消息ID
        /// </summary>
        public string MsgID
        {
            get
            {
                return this.GetValStringByKey(MsgAttr.MsgID);
            }
            set
            {
                this.SetValByKey(MsgAttr.MsgID, value);
            }
        }
		/// <summary>
		/// 发送人名称
		/// </summary>
		public  string  SenderText
		{
            get
			{
				return this.GetValStringByKey(MsgAttr.SenderT);
			}
			set
			{
                this.SetValByKey(MsgAttr.SenderT, value);
			}
		}
		/// <summary>
		/// 发送人
		/// </summary>
		public  string  Sender
		{
			get
			{
				return this.GetValStringByKey(MsgAttr.Sender);
			}
			set
			{
				this.SetValByKey(MsgAttr.Sender,value);
			}
		}
		/// <summary>
		/// 发送日期时间
		/// </summary>
		public  string  RDT
		{
			get
			{
				return this.GetValStringByKey(MsgAttr.RDT);
			}
			set
			{
				this.SetValByKey(MsgAttr.RDT,value);
			}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public  string  Title
		{
			get
			{
				return this.GetValStringByKey(MsgAttr.Title);
			}
			set
			{
				this.SetValByKey(MsgAttr.Title,value);
			}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public  string  Doc
		{
			get
			{
				return this.GetValStringByKey(MsgAttr.Doc);
			}
			set
			{
				this.SetValByKey(MsgAttr.Doc,value);
			}
		}
        /// <summary>
        /// 信息
        /// </summary>
		public  string  DocHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(MsgAttr.Doc);
			}
		}
		/// <summary>
		/// 接受人
		/// </summary>
		public  string  Accepter
		{
			get
			{
				return this.GetValStringByKey(MsgAttr.Accepter);
			}
			set
			{
				this.SetValByKey(MsgAttr.Accepter,value);
			}
		}
		/// <summary>
		/// 阅读个
		/// </summary>
        public MsgSta HisMsgSta
		{
			get
			{
                return (MsgSta)this.GetValIntByKey(MsgAttr.MsgSta);
			}
			set
			{
				this.SetValByKey(MsgAttr.MsgSta,(int)value);
			}
		}
		#endregion 

        #region 方法
        public Msg(int oid)
            : base(oid)
        {

        }
        public string DoSend(string accepter, string doc)
        {
            this.HisMsgSta = MsgSta.UnRead;
            this.Sender = Web.WebUser.No;
            this.Accepter = accepter;
            this.Doc = doc;
            this.RDT = DataType.CurrentDataTimeCN;
            this.Insert();
            return "发送成功<hr>信息成功的发送给:" + accepter;
        }
        public string DoSend(string accepter, string doc, string msgID)
        {
            if (this.Retrieve(MsgAttr.MsgID, msgID) != 0)
                return "发送成功<hr>信息成功的发送给:" + accepter;

            this.HisMsgSta = MsgSta.UnRead;
            this.Sender = Web.WebUser.No;
            this.Accepter = accepter;
            this.Doc = doc;
            this.RDT = DataType.CurrentDataTimeCN;
            this.Insert();
            return "发送成功<hr>信息成功的发送给:" + accepter;
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
		/// 消息
		/// </summary>
		public Msg(){}
		 
		/// <summary>
		/// Map
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_Msg");
                map.EnDesc = "消息";
                map.EnType = EnType.Admin;

                map.AddTBIntPKOID();

                map.AddTBString(MsgAttr.MsgID, null, "消息ID", true, false, 0, 500, 20);


                map.AddTBString(MsgAttr.Sender, null, "发送人", true, false, 0, 500, 20);
                map.AddTBString(MsgAttr.SenderT, null, "发送人T", true, false, 0, 500, 20);

                map.AddTBString(MsgAttr.Accepter, null, "接受人", true, false, 0, 500, 20);
                map.AddDDLSysEnum(MsgAttr.MsgSta, 0, "状态", true, false, "MsgSta", "@0=未读@1=已读");

                map.AddTBString(MsgAttr.Title, null, "标题", true, false, 0, 500, 20);
                map.AddTBStringDoc(MsgAttr.Doc, null, "内容", true, false);

                map.AddTBDateTime(MsgAttr.ODT, "打开时间", true, false);
                map.AddTBDateTime(MsgAttr.RDT, "发送时间", true, false);

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 
	}
	/// <summary>
	/// 消息
	/// </summary> 
    public class Msgs : EntitiesOID
    {
        #region 构造函数
        /// <summary>
        /// 关于实体访问的构造
        /// </summary>
        public Msgs()
        {
        }
        /// <summary>
        /// New entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Msg();
            }
        }
        #endregion
    }
}
