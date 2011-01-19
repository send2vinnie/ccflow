using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// 用户问题
	/// </summary>
    public class FAQAttr  //EntityEnsNameAttr
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
	/// 用户问题
	/// </summary> 
	public class FAQ:EntityOID 
	{
		#region 基本属性
		/// <summary>
		/// AskerText
		/// </summary>
		public  string  AskerText
		{
			get
			{
				return this.GetValRefTextByKey(FAQAttr.Asker);
			}
		}
		/// <summary>
		/// 发送人
		/// </summary>
		public  string  Asker
		{
			get
			{
				return this.GetValStringByKey(FAQAttr.Asker);
			}
			set
			{
				this.SetValByKey(FAQAttr.Asker,value);
			}
		}
		/// <summary>
		/// 发送日期时间
		/// </summary>
		public  string  RDT
		{
			get
			{
				return this.GetValStringByKey(FAQAttr.RDT);
			}
			set
			{
				this.SetValByKey(FAQAttr.RDT,value);
			}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public  string  Title
		{
			get
			{
				return this.GetValStringByKey(FAQAttr.Title);
			}
			set
			{
				this.SetValByKey(FAQAttr.Title,value);
			}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public  string  Docs
		{
			get
			{
				return this.GetValStringByKey(FAQAttr.Docs);
			}
			set
			{
				this.SetValByKey(FAQAttr.Docs,value);
			}
		}
        public  string  Doc
		{
			get
			{
				return this.GetValStringByKey(FAQAttr.Docs);
			}
			set
			{
				this.SetValByKey(FAQAttr.Docs,value);
			}
		}
		public  string  DocsHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(FAQAttr.Docs);
			}
		}
		/// <summary>
		/// dtl Num
		/// </summary>
		public  int  DtlNum
		{
			get
			{
				return this.GetValIntByKey(FAQAttr.DtlNum);
			}
			set
			{
				this.SetValByKey(FAQAttr.DtlNum,value);
			}
		}
		/// <summary>
		/// 阅读个
		/// </summary>
		public  int  ReadNum
		{
			get
			{
				return this.GetValIntByKey(FAQAttr.ReadNum);
			}
			set
			{
				this.SetValByKey(FAQAttr.ReadNum,value);
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
		/// 用户问题
		/// </summary>
		public FAQ(){}
		/// <summary>
		/// 用户问题
		/// </summary>
		/// <param name="oid"></param>
		public FAQ(int oid ) : base(oid)
		{
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null)
					return this._enMap;
				Map map = new Map("Sys_FAQ");
				map.EnDesc="用户问题";
				map.EnType=EnType.Admin;
				map.EnDBUrl= new DBUrl(DBUrlType.DBAccessOfOracle9i) ; 

				map.AddTBIntPKOID();
				map.AddDDLEntities(FAQAttr.Asker,Web.WebUser.No ,"提问人",new Emps(),false);
				map.AddTBString(FAQAttr.Title,null,"标题",true,false,0,500,20);
				map.AddTBStringDoc(FAQAttr.Docs,null,"问题内容",true,false);
				map.AddTBDateTime(FAQAttr.RDT,"提问时间",true,false);
				map.AddTBInt(FAQAttr.DtlNum,0,"回答个数",true,false);
				map.AddTBInt(FAQAttr.ReadNum,0,"阅读数",true,false);

				map.AddDtl( new FAQDtls(), FAQDtlAttr.FK_FAQ);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 用户问题
	/// </summary> 
	public class FAQs : EntitiesOID
	{
		#region 构造函数
		/// <summary>
		/// 关于实体访问的构造
		/// </summary>
		public FAQs()
		{
		}
		/// <summary>
		/// New entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new FAQ();
			}
		}
		#endregion
	
	}
}
