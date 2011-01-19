using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// 用户问题明晰
	/// </summary>
	public class FAQDtlAttr  //EntityEnsNameAttr
	{
		/// <summary>
		/// OID
		/// </summary>
		public const string OID="OID";
		/// <summary>
		/// FK_FAQ
		/// </summary>
		public const string FK_FAQ="FK_FAQ";
		/// <summary>
		/// 发送人
		/// </summary>
		public const string Answer="Answer";
		/// <summary>
		/// 发送时间
		/// </summary>
		public const string RDT="RDT";
		/// <summary>
		/// 内容
		/// </summary>
		public const string Docs="Docs";
	}
	/// <summary>
	/// 用户问题明晰
	/// </summary> 
	public class FAQDtl:EntityOID 
	{
		#region 基本属性
		public  int  FK_FAQ
		{
			get
			{
				return this.GetValIntByKey(FAQDtlAttr.FK_FAQ);
			}
			set
			{
				this.SetValByKey(FAQDtlAttr.FK_FAQ,value);
			}
		}
		/// <summary>
		/// 答复人
		/// </summary>
		public  string  Answer
		{
			get
			{
				return this.GetValStringByKey(FAQDtlAttr.Answer);
			}
			set
			{
				this.SetValByKey(FAQDtlAttr.Answer,value);
			}
		}
		public  string  AnswerText
		{
			get
			{
				return this.GetValRefTextByKey(FAQDtlAttr.Answer);
			}
		}
		/// <summary>
		/// 发送日期时间
		/// </summary>
		public  string  RDT
		{
			get
			{
				return this.GetValStringByKey(FAQDtlAttr.RDT);
			}
			set
			{
				this.SetValByKey(FAQDtlAttr.RDT,value);
			}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public  string  Docs
		{
			get
			{
				return this.GetValStringByKey(FAQDtlAttr.Docs);
			}
			set
			{
				this.SetValByKey(FAQDtlAttr.Docs,value);
			}
		}
		public  string  DocsHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(FAQDtlAttr.Docs);
			}
		}
		 
		#endregion 

		#region 构造方法
		/// <summary>
		/// 用户问题明晰
		/// </summary>
		public FAQDtl(){}
		/// <summary>
		/// 用户问题明晰
		/// </summary>
		/// <param name="oid"></param>
		public FAQDtl(int oid ) : base(oid)
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
				Map map = new Map("Sys_FAQDtl");
				map.EnDesc="用户问题明晰";
				map.EnType=EnType.Admin;
				map.EnDBUrl= new DBUrl(DBUrlType.DBAccessOfOracle9i) ; 
				map.AddTBIntPKOID();
				map.AddDDLEntities(FAQDtlAttr.FK_FAQ,0,DataType.AppInt,"FAQ",new FAQs(),"OID","Title", false);
				map.AddDDLEntities(FAQDtlAttr.Answer,null,"答复人",new Emps(),false);
				map.AddTBStringDoc(FAQDtlAttr.Docs,null,"答复内容",true,false);
				map.AddTBDateTime(FAQDtlAttr.RDT,"答复时间",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// 用户问题明晰
	/// </summary> 
	public class FAQDtls : EntitiesOID
	{
		#region 构造函数
		/// <summary>
		/// 关于实体访问的构造
		/// </summary>
		public FAQDtls(){}
		public FAQDtls(int fk_faq)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere( FAQDtlAttr.FK_FAQ, fk_faq);
			qo.addOrderByDesc( FAQDtlAttr.OID) ;
			qo.DoQuery();
		}
		/// <summary>
		/// New entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new FAQDtl();
			}
		}
		#endregion
	}
}
